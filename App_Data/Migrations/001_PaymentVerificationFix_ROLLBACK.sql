-- ============================================================
-- ROLLBACK: Payment Verification Fix
-- Run this to undo all changes from 001_PaymentVerificationFix.sql
-- ============================================================

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'SP_PAYMENT_VERIFICATION_UPDATE') AND type = 'P')
    DROP PROCEDURE SP_PAYMENT_VERIFICATION_UPDATE;
PRINT 'Dropped SP_PAYMENT_VERIFICATION_UPDATE';
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'SP_GET_FAIL_TRANSACTION_FOR_RECHECK') AND type = 'P')
    DROP PROCEDURE SP_GET_FAIL_TRANSACTION_FOR_RECHECK;
PRINT 'Dropped SP_GET_FAIL_TRANSACTION_FOR_RECHECK';
GO

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PAYMENT_VERIFICATION_LOG')
BEGIN
    -- Safety: only drop if empty or recent (within last day)
    DECLARE @rowcount INT;
    SELECT @rowcount = COUNT(*) FROM PAYMENT_VERIFICATION_LOG;
    IF @rowcount = 0
    BEGIN
        DROP TABLE PAYMENT_VERIFICATION_LOG;
        PRINT 'Dropped empty PAYMENT_VERIFICATION_LOG table';
    END
    ELSE
    BEGIN
        PRINT 'WARNING: PAYMENT_VERIFICATION_LOG has ' + CAST(@rowcount AS VARCHAR) + ' rows. Table NOT dropped for safety.';
        PRINT 'To force drop: DROP TABLE PAYMENT_VERIFICATION_LOG';
    END
END
GO

PRINT '';
PRINT '=== ROLLBACK COMPLETE ===';
GO
