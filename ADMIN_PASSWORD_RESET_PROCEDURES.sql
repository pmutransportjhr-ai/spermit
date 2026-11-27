/*
    Admin-only STA/RTA password reset procedures.
    Deploy this script to the same database used by SQLHelper.GetConnString().
    The application hashes the password using the existing Login.aspx SHA-512 flow.
*/
IF OBJECT_ID('dbo.ADMIN_SEARCH_USER_FOR_PASSWORD_RESET', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ADMIN_SEARCH_USER_FOR_PASSWORD_RESET;
GO

CREATE PROCEDURE dbo.ADMIN_SEARCH_USER_FOR_PASSWORD_RESET
    @TYPE        VARCHAR(3),
    @SEARCH_TEXT NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SET @SEARCH_TEXT = LTRIM(RTRIM(@SEARCH_TEXT));

    IF @TYPE = 'STA'
    BEGIN
        SELECT TOP (2)
            CONVERT(NVARCHAR(50), EMPLOYEE_ID) AS EMPLOYEE_ID,
            EMPLOYEE_NAME,
            LOGIN_ID,
            EMAIL_ID,
            MOBILE_NUMBER,
            STATUS
        FROM dbo.STA_USER_MSTR
        WHERE LOGIN_ID = @SEARCH_TEXT
           OR EMAIL_ID = @SEARCH_TEXT
           OR MOBILE_NUMBER = @SEARCH_TEXT
        ORDER BY EMPLOYEE_ID;
    END
    ELSE IF @TYPE = 'RTA'
    BEGIN
        SELECT TOP (2)
            CONVERT(NVARCHAR(50), EMPLOYEE_ID) AS EMPLOYEE_ID,
            EMPLOYEE_NAME,
            LOGIN_ID,
            EMAIL_ID,
            MOBILE_NUMBER,
            STATUS
        FROM dbo.RTA_USER_MSTR
        WHERE LOGIN_ID = @SEARCH_TEXT
           OR EMAIL_ID = @SEARCH_TEXT
           OR MOBILE_NUMBER = @SEARCH_TEXT
        ORDER BY EMPLOYEE_ID;
    END
    ELSE
    BEGIN
        RAISERROR('Invalid user type. Only STA and RTA are supported.', 16, 1);
        RETURN;
    END
END
GO

IF OBJECT_ID('dbo.ADMIN_RESET_USER_PASSWORD', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ADMIN_RESET_USER_PASSWORD;
GO

CREATE PROCEDURE dbo.ADMIN_RESET_USER_PASSWORD
    @TYPE        VARCHAR(3),
    @EMPLOYEE_ID NVARCHAR(50),
    @PASSWORD    VARCHAR(128),
    @MODIFIED_BY NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @TYPE NOT IN ('STA', 'RTA')
    BEGIN
        SELECT 'InvalidUserType' AS Result;
        RETURN;
    END

    IF @PASSWORD IS NULL OR LEN(@PASSWORD) <> 128
    BEGIN
        SELECT 'InvalidPasswordHash' AS Result;
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @TYPE = 'STA'
        BEGIN
            UPDATE dbo.STA_USER_MSTR
               SET PASSWORD = @PASSWORD,
                   MODIFIED_BY = @MODIFIED_BY,
                   MODIFIED_ON = GETDATE(),
                   FORGOT_PASS_CODE = NULL
             WHERE CONVERT(NVARCHAR(50), EMPLOYEE_ID) = @EMPLOYEE_ID;
        END
        ELSE
        BEGIN
            UPDATE dbo.RTA_USER_MSTR
               SET PASSWORD = @PASSWORD,
                   MODIFIED_BY = @MODIFIED_BY,
                   MODIFIED_ON = GETDATE(),
                   FORGOT_PASS_CODE = NULL
             WHERE CONVERT(NVARCHAR(50), EMPLOYEE_ID) = @EMPLOYEE_ID;
        END

        IF @@ROWCOUNT = 1
        BEGIN
            COMMIT TRANSACTION;
            SELECT 'Success' AS Result;
        END
        ELSE
        BEGIN
            ROLLBACK TRANSACTION;
            SELECT 'NotFound' AS Result;
        END
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
