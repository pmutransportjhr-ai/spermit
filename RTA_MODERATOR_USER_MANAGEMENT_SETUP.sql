-- ============================================================================
-- SQL SCRIPT FOR RTA MODERATOR USER MANAGEMENT
-- Verified Tables: dbo.RTA_USER_MSTR, dbo.RTA_RTO_MASTER
-- ============================================================================

-- ----------------------------------------------------------------------------
-- 1. Insert Menu Entries into RTA_MENU_ITEMS (if not already existing)
-- ----------------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM dbo.RTA_MENU_ITEMS WHERE MENU_ITEM = 'RTA Moderator')
BEGIN
    DECLARE @ParentMenuId INT;
    
    -- Insert Top-Level Menu
    INSERT INTO dbo.RTA_MENU_ITEMS (MENU_ITEM, URL, SUB_MENU, PARENT_MENU_ID)
    VALUES ('RTA Moderator', '', 'N', NULL);

    SET @ParentMenuId = SCOPE_IDENTITY();

    -- Insert Child Menu under 'RTA Moderator'
    INSERT INTO dbo.RTA_MENU_ITEMS (MENU_ITEM, URL, SUB_MENU, PARENT_MENU_ID)
    VALUES ('User Management', 'RTA-MODERATOR-USER-MANAGEMENT.aspx', 'Y', @ParentMenuId);
END
GO

-- ----------------------------------------------------------------------------
-- 2. Stored Procedure: RTA_MODERATOR_USER_LIST_PAGED (SERVER-SIDE PAGINATION)
-- ----------------------------------------------------------------------------
IF OBJECT_ID('dbo.RTA_MODERATOR_USER_LIST_PAGED', 'P') IS NOT NULL
    DROP PROCEDURE dbo.RTA_MODERATOR_USER_LIST_PAGED;
GO

CREATE PROCEDURE dbo.RTA_MODERATOR_USER_LIST_PAGED
    @PAGE_NUMBER INT = 1,
    @PAGE_SIZE INT = 10,
    @SEARCH_TEXT VARCHAR(100) = NULL,
    @STATUS_FILTER VARCHAR(20) = NULL,
    @RTO_FILTER VARCHAR(20) = NULL,
    @EMPLOYEE_ID VARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Sanitize Page Parameters
    IF @PAGE_NUMBER IS NULL OR @PAGE_NUMBER < 1 SET @PAGE_NUMBER = 1;
    IF @PAGE_SIZE IS NULL OR @PAGE_SIZE < 1 SET @PAGE_SIZE = 10;

    SET @SEARCH_TEXT = LTRIM(RTRIM(@SEARCH_TEXT));
    IF @SEARCH_TEXT = '' SET @SEARCH_TEXT = NULL;

    SET @STATUS_FILTER = LTRIM(RTRIM(@STATUS_FILTER));
    IF @STATUS_FILTER = '' OR @STATUS_FILTER = 'ALL' SET @STATUS_FILTER = NULL;

    SET @RTO_FILTER = LTRIM(RTRIM(@RTO_FILTER));
    IF @RTO_FILTER = '' OR @RTO_FILTER = '0' OR @RTO_FILTER = 'ALL' SET @RTO_FILTER = NULL;

    SET @EMPLOYEE_ID = LTRIM(RTRIM(@EMPLOYEE_ID));
    IF @EMPLOYEE_ID = '' SET @EMPLOYEE_ID = NULL;

    WITH UserCTE AS (
        SELECT 
            U.EMPLOYEE_ID,
            U.EMPLOYEE_NAME,
            U.LOGIN_ID,
            U.EMAIL_ID,
            U.MOBILE_NUMBER,
            U.USER_ROLE,
            U.USER_TYPE,
            U.RTO_CODE,
            RM.RTO_NAME,
            U.DEPARTMENT,
            U.DESIGNATION,
            U.USER_LEVEL,
            CASE 
                WHEN UPPER(LTRIM(RTRIM(ISNULL(U.STATUS, '')))) IN ('A', 'ACTIVE', '1') THEN 'Active'
                ELSE 'Inactive'
            END AS STATUS,
            ROW_NUMBER() OVER (ORDER BY U.EMPLOYEE_NAME ASC, U.EMPLOYEE_ID ASC) AS RowNum,
            COUNT(*) OVER() AS TOTAL_RECORDS
        FROM dbo.RTA_USER_MSTR U WITH (NOLOCK)
        LEFT JOIN dbo.RTA_RTO_MASTER RM WITH (NOLOCK) 
            ON CONVERT(VARCHAR(50), RM.RTO_CODE) = CONVERT(VARCHAR(50), U.RTO_CODE)
        WHERE (U.USER_TYPE = 'RTA' OR U.USER_TYPE IS NULL OR U.USER_TYPE = '')
          AND (@EMPLOYEE_ID IS NULL OR U.EMPLOYEE_ID = @EMPLOYEE_ID)
          AND (@RTO_FILTER IS NULL OR CONVERT(VARCHAR(50), U.RTO_CODE) = CONVERT(VARCHAR(50), @RTO_FILTER))
          AND (@STATUS_FILTER IS NULL 
               OR (@STATUS_FILTER = 'Active' AND UPPER(LTRIM(RTRIM(ISNULL(U.STATUS, '')))) IN ('A', 'ACTIVE', '1'))
               OR (@STATUS_FILTER = 'Inactive' AND UPPER(LTRIM(RTRIM(ISNULL(U.STATUS, '')))) NOT IN ('A', 'ACTIVE', '1')))
          AND (@SEARCH_TEXT IS NULL 
               OR U.EMPLOYEE_ID LIKE '%' + @SEARCH_TEXT + '%'
               OR U.EMPLOYEE_NAME LIKE '%' + @SEARCH_TEXT + '%'
               OR U.LOGIN_ID LIKE '%' + @SEARCH_TEXT + '%'
               OR U.EMAIL_ID LIKE '%' + @SEARCH_TEXT + '%'
               OR U.MOBILE_NUMBER LIKE '%' + @SEARCH_TEXT + '%')
    )
    SELECT 
        EMPLOYEE_ID,
        EMPLOYEE_NAME,
        LOGIN_ID,
        EMAIL_ID,
        MOBILE_NUMBER,
        USER_ROLE,
        USER_TYPE,
        RTO_CODE,
        RTO_NAME,
        DEPARTMENT,
        DESIGNATION,
        USER_LEVEL,
        STATUS,
        TOTAL_RECORDS
    FROM UserCTE
    WHERE RowNum BETWEEN ((@PAGE_NUMBER - 1) * @PAGE_SIZE + 1) AND (@PAGE_NUMBER * @PAGE_SIZE)
    ORDER BY RowNum;
END
GO

-- ----------------------------------------------------------------------------
-- 3. Stored Procedure: RTA_MODERATOR_USER_LIST (UNPAGED REPOSITORY COMPATIBILITY)
-- ----------------------------------------------------------------------------
IF OBJECT_ID('dbo.RTA_MODERATOR_USER_LIST', 'P') IS NOT NULL
    DROP PROCEDURE dbo.RTA_MODERATOR_USER_LIST;
GO

CREATE PROCEDURE dbo.RTA_MODERATOR_USER_LIST
    @EMPLOYEE_ID VARCHAR(50) = NULL,
    @SEARCH_TEXT VARCHAR(100) = NULL,
    @STATUS_FILTER VARCHAR(20) = NULL,
    @RTO_FILTER VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @SEARCH_TEXT = LTRIM(RTRIM(@SEARCH_TEXT));
    IF @SEARCH_TEXT = '' SET @SEARCH_TEXT = NULL;

    SET @STATUS_FILTER = LTRIM(RTRIM(@STATUS_FILTER));
    IF @STATUS_FILTER = '' OR @STATUS_FILTER = 'ALL' SET @STATUS_FILTER = NULL;

    SET @RTO_FILTER = LTRIM(RTRIM(@RTO_FILTER));
    IF @RTO_FILTER = '' OR @RTO_FILTER = '0' OR @RTO_FILTER = 'ALL' SET @RTO_FILTER = NULL;

    SET @EMPLOYEE_ID = LTRIM(RTRIM(@EMPLOYEE_ID));
    IF @EMPLOYEE_ID = '' SET @EMPLOYEE_ID = NULL;

    SELECT 
        U.EMPLOYEE_ID,
        U.EMPLOYEE_NAME,
        U.LOGIN_ID,
        U.EMAIL_ID,
        U.MOBILE_NUMBER,
        U.DEPARTMENT,
        U.DESIGNATION,
        U.USER_ROLE,
        U.USER_TYPE,
        U.RTO_CODE,
        RM.RTO_NAME,
        U.USER_LEVEL,
        CASE 
            WHEN UPPER(LTRIM(RTRIM(ISNULL(U.STATUS, '')))) IN ('A', 'ACTIVE', '1') THEN 'Active'
            ELSE 'Inactive'
        END AS STATUS,
        U.CREATED_BY,
        U.CREATED_ON,
        U.MODIFIED_BY,
        U.MODIFIED_ON
    FROM dbo.RTA_USER_MSTR U WITH (NOLOCK)
    LEFT JOIN dbo.RTA_RTO_MASTER RM WITH (NOLOCK) 
        ON CONVERT(VARCHAR(50), RM.RTO_CODE) = CONVERT(VARCHAR(50), U.RTO_CODE)
    WHERE (U.USER_TYPE = 'RTA' OR U.USER_TYPE IS NULL OR U.USER_TYPE = '')
      AND (@EMPLOYEE_ID IS NULL OR U.EMPLOYEE_ID = @EMPLOYEE_ID)
      AND (@RTO_FILTER IS NULL OR CONVERT(VARCHAR(50), U.RTO_CODE) = CONVERT(VARCHAR(50), @RTO_FILTER))
      AND (@STATUS_FILTER IS NULL 
           OR (@STATUS_FILTER = 'Active' AND UPPER(LTRIM(RTRIM(ISNULL(U.STATUS, '')))) IN ('A', 'ACTIVE', '1'))
           OR (@STATUS_FILTER = 'Inactive' AND UPPER(LTRIM(RTRIM(ISNULL(U.STATUS, '')))) NOT IN ('A', 'ACTIVE', '1')))
      AND (@SEARCH_TEXT IS NULL 
           OR U.EMPLOYEE_ID LIKE '%' + @SEARCH_TEXT + '%'
           OR U.EMPLOYEE_NAME LIKE '%' + @SEARCH_TEXT + '%'
           OR U.LOGIN_ID LIKE '%' + @SEARCH_TEXT + '%'
           OR U.EMAIL_ID LIKE '%' + @SEARCH_TEXT + '%'
           OR U.MOBILE_NUMBER LIKE '%' + @SEARCH_TEXT + '%')
    ORDER BY U.EMPLOYEE_NAME ASC, U.EMPLOYEE_ID ASC;
END
GO

-- ----------------------------------------------------------------------------
-- 4. Stored Procedure: RTA_MODERATOR_USER_DETAILS
-- ----------------------------------------------------------------------------
IF OBJECT_ID('dbo.RTA_MODERATOR_USER_DETAILS', 'P') IS NOT NULL
    DROP PROCEDURE dbo.RTA_MODERATOR_USER_DETAILS;
GO

CREATE PROCEDURE dbo.RTA_MODERATOR_USER_DETAILS
    @EMPLOYEE_ID VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        U.EMPLOYEE_ID,
        U.EMPLOYEE_NAME,
        U.LOGIN_ID,
        U.EMAIL_ID,
        U.MOBILE_NUMBER,
        U.DEPARTMENT,
        U.DESIGNATION,
        U.USER_ROLE,
        U.USER_TYPE,
        U.RTO_CODE,
        RM.RTO_NAME,
        U.USER_LEVEL,
        CASE 
            WHEN UPPER(LTRIM(RTRIM(ISNULL(U.STATUS, '')))) IN ('A', 'ACTIVE', '1') THEN 'Active'
            ELSE 'Inactive'
        END AS STATUS,
        U.SECURITY_QUESTIONS,
        U.SECURITY_ANSWERS
    FROM dbo.RTA_USER_MSTR U WITH (NOLOCK)
    LEFT JOIN dbo.RTA_RTO_MASTER RM WITH (NOLOCK)
        ON CONVERT(VARCHAR(50), RM.RTO_CODE) = CONVERT(VARCHAR(50), U.RTO_CODE)
    WHERE U.EMPLOYEE_ID = @EMPLOYEE_ID;
END
GO

-- ----------------------------------------------------------------------------
-- 5. Stored Procedure: RTA_MODERATOR_USER_UPDATE
-- ----------------------------------------------------------------------------
IF OBJECT_ID('dbo.RTA_MODERATOR_USER_UPDATE', 'P') IS NOT NULL
    DROP PROCEDURE dbo.RTA_MODERATOR_USER_UPDATE;
GO

CREATE PROCEDURE dbo.RTA_MODERATOR_USER_UPDATE
    @EMPLOYEE_ID VARCHAR(50),
    @EMPLOYEE_NAME VARCHAR(100),
    @EMAIL_ID VARCHAR(100),
    @MOBILE_NUMBER VARCHAR(20),
    @DEPARTMENT VARCHAR(50),
    @DESIGNATION VARCHAR(50),
    @RTO_CODE VARCHAR(50),
    @USER_LEVEL VARCHAR(50),
    @STATUS VARCHAR(20),
    @MODIFIED_BY VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.RTA_USER_MSTR
    SET EMPLOYEE_NAME = @EMPLOYEE_NAME,
        EMAIL_ID = @EMAIL_ID,
        MOBILE_NUMBER = @MOBILE_NUMBER,
        DEPARTMENT = @DEPARTMENT,
        DESIGNATION = @DESIGNATION,
        RTO_CODE = @RTO_CODE,
        USER_LEVEL = @USER_LEVEL,
        STATUS = @STATUS,
        MODIFIED_BY = @MODIFIED_BY,
        MODIFIED_ON = GETDATE()
    WHERE EMPLOYEE_ID = @EMPLOYEE_ID;

    SELECT 'Success~User details updated successfully.' AS MSG;
END
GO

-- ----------------------------------------------------------------------------
-- 6. Stored Procedure: RTA_MODERATOR_USER_STATUS_UPDATE
-- ----------------------------------------------------------------------------
IF OBJECT_ID('dbo.RTA_MODERATOR_USER_STATUS_UPDATE', 'P') IS NOT NULL
    DROP PROCEDURE dbo.RTA_MODERATOR_USER_STATUS_UPDATE;
GO

CREATE PROCEDURE dbo.RTA_MODERATOR_USER_STATUS_UPDATE
    @EMPLOYEE_ID VARCHAR(50),
    @STATUS VARCHAR(20),
    @MODIFIED_BY VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.RTA_USER_MSTR
    SET STATUS = @STATUS,
        MODIFIED_BY = @MODIFIED_BY,
        MODIFIED_ON = GETDATE()
    WHERE EMPLOYEE_ID = @EMPLOYEE_ID;

    SELECT 'Success~User status updated successfully.' AS MSG;
END
GO

-- ----------------------------------------------------------------------------
-- 7. Stored Procedure: RTA_MODERATOR_USER_PASSWORD_UPDATE
-- ----------------------------------------------------------------------------
IF OBJECT_ID('dbo.RTA_MODERATOR_USER_PASSWORD_UPDATE', 'P') IS NOT NULL
    DROP PROCEDURE dbo.RTA_MODERATOR_USER_PASSWORD_UPDATE;
GO

CREATE PROCEDURE dbo.RTA_MODERATOR_USER_PASSWORD_UPDATE
    @EMPLOYEE_ID VARCHAR(50),
    @PASSWORD VARCHAR(255),
    @MODIFIED_BY VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.RTA_USER_MSTR
    SET PASSWORD = @PASSWORD,
        MODIFIED_BY = @MODIFIED_BY,
        MODIFIED_ON = GETDATE()
    WHERE EMPLOYEE_ID = @EMPLOYEE_ID;

    SELECT 'Success~Password updated successfully.' AS MSG;
END
GO
