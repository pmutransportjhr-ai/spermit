-- ============================================================
-- PAYMENT VERIFICATION FIX — DATABASE MIGRATION
-- Jharkhand Transport (Spermit) Payment System
-- Date: 2026-09-29
-- ============================================================
-- This script:
--   1. Creates PAYMENT_VERIFICATION_LOG table
--   2. Alters SP_PRE_PAYMENT_DETAIL to return transaction data for FAIL status
--   3. Adds a new MODE 'UPDATE_VERIFICATION' to SP_PAYMENT_TRANSACTIONS_HISTORY_AED
--      that updates BY DEPTTRANID and prevents SUCCESS→FAIL downgrade
-- ============================================================

-- ============================================================
-- STEP 1: Create payment verification audit log table
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PAYMENT_VERIFICATION_LOG')
BEGIN
    CREATE TABLE PAYMENT_VERIFICATION_LOG
    (
        LOG_ID              INT IDENTITY(1,1) PRIMARY KEY,
        VERIFICATION_ID     VARCHAR(30)   NOT NULL,
        APPLICATION_NUMBER  VARCHAR(100)  NULL,
        APP_ID              VARCHAR(50)   NULL,
        DEPTTRANID          VARCHAR(50)   NULL,
        DB_TXN_ID           INT           NULL,
        CURRENT_DB_STATUS   VARCHAR(30)   NULL,
        RETURNED_STATUS     VARCHAR(30)   NULL,
        RETURNED_DEPTTRANID VARCHAR(50)   NULL,
        RETURNED_GRN        VARCHAR(100)  NULL,
        RETURNED_AMOUNT     VARCHAR(50)   NULL,
        DB_AMOUNT           DECIMAL(18,2) NULL,
        VERIFICATION_RESULT VARCHAR(30)   NULL,  -- VERIFIED_SUCCESS, VERIFIED_FAIL, MISMATCH, ERROR, TIMEOUT
        UPDATE_ACTION       VARCHAR(30)   NULL,  -- UPDATED_TO_SUCCESS, NO_CHANGE, BLOCKED_DOWNGRADE, MISMATCH_BLOCKED
        OPERATION_FOR       VARCHAR(200)  NULL,
        TR_TYPE             VARCHAR(100)  NULL,
        MESSAGE             VARCHAR(1000) NULL,
        CREATED_ON          DATETIME      DEFAULT GETDATE(),
        MACHINE_NAME        VARCHAR(100)  NULL
    );

    PRINT 'Created table PAYMENT_VERIFICATION_LOG';
END
ELSE
BEGIN
    PRINT 'Table PAYMENT_VERIFICATION_LOG already exists — skipped';
END
GO

-- ============================================================
-- STEP 2: Alter SP_PRE_PAYMENT_DETAIL
-- Add handling for FAIL status to return transaction data
-- instead of just "Payment Failed Please Try Again Later."
-- ============================================================
-- NOTE: This modifies the existing SP. The original FAIL case returned:
--   MSG = 'Payment Failed Please Try Again Later.'
-- The new behavior returns the same columns as BOOKED so that
-- double verification can proceed for FAIL transactions too.
--
-- IMPORTANT: We preserve ALL existing behavior for other statuses.
-- We ONLY change the FAIL branch to return full transaction data.
-- ============================================================

-- First, let's see the current SP definition before modifying
-- (This is a safety check — run this SELECT to see current SP)
-- SELECT OBJECT_DEFINITION(OBJECT_ID('SP_PRE_PAYMENT_DETAIL'))

-- ============================================================
-- The actual ALTER depends on the SP's internal structure.
-- Based on source code analysis, the SP returns these columns:
--   MSG, TXN_AMOUNT, TRN_REF_NO, DEPTTRANID, APP_ID,
--   APPLICATION_NUMBER, OPERATION_FOR, TR_TYPE
--
-- Current behavior when TRN_STATUS = 'FAIL':
--   SELECT 'Payment Failed Please Try Again Later.' AS MSG
--
-- Required behavior when TRN_STATUS = 'FAIL':
--   Return the same columns as BOOKED, but with MSG = 'FAIL_RECHECK'
--   so the handler can proceed with double verification.
--
-- Since we cannot see the full SP source, we add a WRAPPER approach:
-- We modify the handler (doubleVerification.ashx) to query additional
-- data when MSG = 'Payment Failed Please Try Again Later.'
-- This avoids modifying the SP which may have unknown dependencies.
-- ============================================================

-- ALTERNATIVE: If you CAN modify the SP, use this template:
-- (Uncomment and adjust based on actual SP structure)
/*
ALTER PROCEDURE [dbo].[SP_PRE_PAYMENT_DETAIL]
    @APPLICATION_NUMBER VARCHAR(100),
    @MODE VARCHAR(100),
    @PType VARCHAR(50) = NULL
AS
BEGIN
    -- ... existing code ...
    
    -- CHANGE: When status is FAIL, return full transaction data
    -- so double verification can proceed
    IF @TRN_STATUS = 'FAIL'
    BEGIN
        SELECT 
            'FAIL_RECHECK' AS MSG,
            DEPTTRANID,
            APP_ID,
            APPLICATION_NUMBER,
            OPERATION_FOR,
            TR_TYPE,
            TXN_AMOUNT,
            TRN_REF_NO,
            GRN,
            AMOUNT
        FROM PAYMENT_TRANSACTIONS_HISTORY
        WHERE APPLICATION_NUMBER = @APPLICATION_NUMBER
        ORDER BY ID DESC  -- latest transaction
        -- TOP 1 is implied by existing SP logic
    END
    
    -- ... rest of existing code ...
END
*/

-- ============================================================
-- STEP 3: Add UPDATE_VERIFICATION mode to 
-- SP_PAYMENT_TRANSACTIONS_HISTORY_AED
-- ============================================================
-- This new mode updates an EXISTING transaction by DEPTTRANID
-- instead of inserting a new record. It also prevents
-- SUCCESS → FAIL downgrade.
-- ============================================================

-- Since we cannot see the full SP, we create a NEW companion SP
-- that handles verification-specific updates safely.
-- This avoids modifying the existing SP which handles many other operations.

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'SP_PAYMENT_VERIFICATION_UPDATE') AND type = 'P')
    DROP PROCEDURE SP_PAYMENT_VERIFICATION_UPDATE;
GO

CREATE PROCEDURE [dbo].[SP_PAYMENT_VERIFICATION_UPDATE]
    @DEPTTRANID         VARCHAR(50),
    @APPLICATION_NUMBER VARCHAR(100),
    @NEW_STATUS         VARCHAR(30),
    @TRN_MESSAGE        VARCHAR(500)  = '',
    @TRN_REF_NO         VARCHAR(100)  = '',
    @GRN                VARCHAR(100)  = '',
    @CIN                VARCHAR(100)  = '',
    @TXN_DATE           VARCHAR(50)   = '',
    @TXN_AMOUNT         VARCHAR(50)   = '',
    @CHALLAN_URL        VARCHAR(500)  = '',
    @PAY_MODE           VARCHAR(50)   = '',
    @DEPTID             VARCHAR(50)   = '',
    @RECIEPTHEADCODE    VARCHAR(50)   = '',
    @TREASCODE          VARCHAR(50)   = '',
    @IFMSOFFICECODE     VARCHAR(50)   = '',
    @VERIFICATION_ID    VARCHAR(30)   = '',
    @MODE               VARCHAR(50)   = 'UPDATE'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CURRENT_STATUS VARCHAR(30);
    DECLARE @DB_TXN_ID INT;
    DECLARE @RESULT VARCHAR(50);

    -- Find the exact transaction by DEPTTRANID
    SELECT TOP 1
        @CURRENT_STATUS = TRN_STATUS,
        @DB_TXN_ID = ID
    FROM PAYMENT_TRANSACTIONS_HISTORY
    WHERE DEPTTRANID = @DEPTTRANID
      AND APPLICATION_NUMBER = @APPLICATION_NUMBER
    ORDER BY ID DESC;

    -- Transaction not found
    IF @DB_TXN_ID IS NULL
    BEGIN
        SELECT 'NOT_FOUND' AS RESULT, '' AS CURRENT_STATUS, 0 AS DB_TXN_ID;
        RETURN;
    END

    -- READ-ONLY mode: just return current state
    IF @MODE = 'CHECK'
    BEGIN
        SELECT 'FOUND' AS RESULT, @CURRENT_STATUS AS CURRENT_STATUS, @DB_TXN_ID AS DB_TXN_ID;
        RETURN;
    END

    -- PROTECTION: Never downgrade SUCCESS to FAIL
    IF @CURRENT_STATUS = 'SUCCESS' AND @NEW_STATUS = 'FAIL'
    BEGIN
        SELECT 'BLOCKED_DOWNGRADE' AS RESULT, @CURRENT_STATUS AS CURRENT_STATUS, @DB_TXN_ID AS DB_TXN_ID;
        RETURN;
    END

    -- PROTECTION: Don't update if already SUCCESS (idempotent)
    IF @CURRENT_STATUS = 'SUCCESS' AND @NEW_STATUS = 'SUCCESS'
    BEGIN
        SELECT 'ALREADY_SUCCESS' AS RESULT, @CURRENT_STATUS AS CURRENT_STATUS, @DB_TXN_ID AS DB_TXN_ID;
        RETURN;
    END

    -- Perform the update
    BEGIN TRY
        UPDATE PAYMENT_TRANSACTIONS_HISTORY
        SET 
            TRN_STATUS = @NEW_STATUS,
            TRN_MESSAGE = CASE WHEN @TRN_MESSAGE != '' THEN @TRN_MESSAGE ELSE TRN_MESSAGE END,
            TRN_REF_NO = CASE WHEN @TRN_REF_NO != '' THEN @TRN_REF_NO ELSE TRN_REF_NO END,
            GRN = CASE WHEN @GRN != '' THEN @GRN ELSE GRN END,
            CIN = CASE WHEN @CIN != '' THEN @CIN ELSE CIN END,
            TXN_DATE = CASE WHEN @TXN_DATE != '' THEN @TXN_DATE ELSE TXN_DATE END,
            TXN_AMOUNT = CASE WHEN @TXN_AMOUNT != '' AND @TXN_AMOUNT != '0' THEN @TXN_AMOUNT ELSE TXN_AMOUNT END,
            CHALLAN_URL = CASE WHEN @CHALLAN_URL != '' THEN @CHALLAN_URL ELSE CHALLAN_URL END,
            PAY_MODE = CASE WHEN @PAY_MODE != '' THEN @PAY_MODE ELSE PAY_MODE END,
            DEPTID = CASE WHEN @DEPTID != '' THEN @DEPTID ELSE DEPTID END,
            RECIEPTHEADCODE = CASE WHEN @RECIEPTHEADCODE != '' THEN @RECIEPTHEADCODE ELSE RECIEPTHEADCODE END,
            TREASCODE = CASE WHEN @TREASCODE != '' THEN @TREASCODE ELSE TREASCODE END,
            IFMSOFFICECODE = CASE WHEN @IFMSOFFICECODE != '' THEN @IFMSOFFICECODE ELSE IFMSOFFICECODE END
        WHERE ID = @DB_TXN_ID
          AND DEPTTRANID = @DEPTTRANID;

        SET @RESULT = 'UPDATED_TO_' + @NEW_STATUS;
        SELECT @RESULT AS RESULT, @CURRENT_STATUS AS CURRENT_STATUS, @DB_TXN_ID AS DB_TXN_ID;
    END TRY
    BEGIN CATCH
        SELECT 'UPDATE_ERROR' AS RESULT, @CURRENT_STATUS AS CURRENT_STATUS, @DB_TXN_ID AS DB_TXN_ID;
    END CATCH
END
GO

PRINT 'Created stored procedure SP_PAYMENT_VERIFICATION_UPDATE';
GO

-- ============================================================
-- STEP 4: Create a query to get transaction data for FAIL
-- transactions (used by the updated doubleVerification.ashx
-- when SP_PRE_PAYMENT_DETAIL returns "Payment Failed...")
-- ============================================================

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'SP_GET_FAIL_TRANSACTION_FOR_RECHECK') AND type = 'P')
    DROP PROCEDURE SP_GET_FAIL_TRANSACTION_FOR_RECHECK;
GO

CREATE PROCEDURE [dbo].[SP_GET_FAIL_TRANSACTION_FOR_RECHECK]
    @APPLICATION_NUMBER VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- Get the LATEST transaction for this application (regardless of status)
    -- This is used when SP_PRE_PAYMENT_DETAIL returns FAIL,
    -- so that doubleVerification.ashx can still proceed with verification
    SELECT TOP 1
        DEPTTRANID,
        CAST(APP_ID AS VARCHAR(50)) AS APP_ID,
        APPLICATION_NUMBER,
        OPERATION_FOR,
        ISNULL(TR_TYPE, '') AS TR_TYPE,
        TRN_STATUS,
        ISNULL(CAST(AMOUNT AS VARCHAR(50)), '0') AS AMOUNT,
        ISNULL(GRN, '') AS GRN,
        ID AS DB_TXN_ID
    FROM PAYMENT_TRANSACTIONS_HISTORY
    WHERE APPLICATION_NUMBER = @APPLICATION_NUMBER
    ORDER BY ID DESC;
END
GO

PRINT 'Created stored procedure SP_GET_FAIL_TRANSACTION_FOR_RECHECK';
GO

-- ============================================================
-- VERIFICATION COMPLETE
-- ============================================================
PRINT '';
PRINT '=== MIGRATION COMPLETE ===';
PRINT 'Created: PAYMENT_VERIFICATION_LOG table';
PRINT 'Created: SP_PAYMENT_VERIFICATION_UPDATE stored procedure';
PRINT 'Created: SP_GET_FAIL_TRANSACTION_FOR_RECHECK stored procedure';
PRINT '';
PRINT 'NOTE: SP_PRE_PAYMENT_DETAIL was NOT modified.';
PRINT 'The FAIL case is handled in the handler (doubleVerification.ashx)';
PRINT 'by calling SP_GET_FAIL_TRANSACTION_FOR_RECHECK when SP returns FAIL.';
GO
