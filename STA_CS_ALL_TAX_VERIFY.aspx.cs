using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

public partial class STA_CS_ALL_TAX_VERIFY : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessReceipt();
        }
    }

    // ================================================================
    // MAIN RECEIPT PROCESS
    // ================================================================

    private void ProcessReceipt()
    {
        string currentStep = "START";

        try
        {
            // ========================================================
            // STEP 1: READ QUERY STRING
            // ========================================================

            currentStep = "READ_QUERYSTRING";

            string encryptedRequest =
                Request.QueryString["reprint"];

            if (string.IsNullOrWhiteSpace(encryptedRequest))
            {
                ShowErrorAndRedirect(
                    "Invalid receipt request."
                );

                return;
            }


            // ========================================================
            // STEP 2: DECRYPT REQUEST
            // ========================================================

            currentStep = "DECRYPT_REQUEST";

            encryptedRequest =
                HttpUtility.UrlDecode(encryptedRequest);

            // Important for Base64/encrypted query strings
            encryptedRequest =
                encryptedRequest.Replace(" ", "+");

            string decryptedRequest = "";

            try
            {
                decryptedRequest =
                    CryptorEngine.Decrypt(
                        encryptedRequest,
                        true
                    );
            }
            catch (Exception ex)
            {
                ShowErrorAndRedirect(
                    "Unable to decrypt receipt request. " +
                    ex.Message
                );

                return;
            }

            if (string.IsNullOrWhiteSpace(decryptedRequest))
            {
                ShowErrorAndRedirect(
                    "Decryption returned empty value."
                );

                return;
            }


            // ========================================================
            // STEP 3: PARSE REQUEST
            // ========================================================

            currentStep = "PARSE_REQUEST";

            Dictionary<string, string> requestValues =
                ParseReceiptRequest(decryptedRequest);


            // ========================================================
            // STEP 4: GET APPLICATION NUMBER
            // ========================================================
            //
            // Existing system may send:
            //
            // APP_ID=JH-CS-VR-0102
            //
            // Although the key is APP_ID, the value is actually
            // APPLICATION_NUMBER.
            //
            // Newer URL may send:
            //
            // APPLICATION_NUMBER=JH-CS-VR-0102
            //
            // Support both.
            // ========================================================

            string applicationNumber =
                GetValue(
                    requestValues,
                    "APPLICATION_NUMBER"
                );

            if (string.IsNullOrWhiteSpace(applicationNumber))
            {
                applicationNumber =
                    GetValue(
                        requestValues,
                        "APP_ID"
                    );
            }


            // ========================================================
            // STEP 5: GET OPTIONAL VALUES FROM REQUEST
            // ========================================================

            string grn =
                GetValue(
                    requestValues,
                    "GRN"
                );

            string deptTranId =
                GetValue(
                    requestValues,
                    "DEPTTRANID"
                );

            string trnRefNo =
                GetValue(
                    requestValues,
                    "TRN_REF_NO"
                );

            string requestStatus =
                GetValue(
                    requestValues,
                    "STATUS"
                );


            currentStep =
                "REQUEST_VALUES" +
                " | APPLICATION_NUMBER=" + applicationNumber +
                " | GRN=" + grn +
                " | DEPTTRANID=" + deptTranId +
                " | TRN_REF_NO=" + trnRefNo +
                " | STATUS=" + requestStatus;


            // ========================================================
            // STEP 6: APPLICATION NUMBER IS THE ONLY REQUIRED INPUT
            // ========================================================

            if (string.IsNullOrWhiteSpace(applicationNumber))
            {
                ShowErrorAndRedirect(
                    "Application number is missing."
                );

                return;
            }


            // ========================================================
            // STEP 7: DATABASE LOOKUP
            // ========================================================
            //
            // APPLICATION_NUMBER is the primary lookup.
            //
            // GRN / DEPTTRANID / TRN_REF_NO are optional.
            //
            // The database returns the actual values.
            // ========================================================

            currentStep = "DATABASE_LOOKUP";


            SqlParameter[] parameters =
                new SqlParameter[4];


            // APPLICATION NUMBER
            parameters[0] =
                new SqlParameter(
                    "@APPLICATION_NUMBER",
                    applicationNumber
                );


            // GRN - OPTIONAL
            parameters[1] =
                new SqlParameter(
                    "@GRN",
                    string.IsNullOrWhiteSpace(grn)
                        ? (object)DBNull.Value
                        : grn
                );


            // DEPT TRAN ID - OPTIONAL
            parameters[2] =
                new SqlParameter(
                    "@DEPTTRANID",
                    string.IsNullOrWhiteSpace(deptTranId)
                        ? (object)DBNull.Value
                        : deptTranId
                );


            // TRANSACTION REFERENCE - OPTIONAL
            parameters[3] =
                new SqlParameter(
                    "@TRN_REF_NO",
                    string.IsNullOrWhiteSpace(trnRefNo)
                        ? (object)DBNull.Value
                        : trnRefNo
                );


            DataSet ds =
                SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(),
                    CommandType.StoredProcedure,
                    "STA_GET_CS_TAX_PAYMENT_FOR_RECEIPT",
                    parameters
                );


            // ========================================================
            // STEP 8: VALIDATE DATABASE RESPONSE
            // ========================================================

            currentStep =
                "DATABASE_RESULT";


            if (ds == null)
            {
                ShowErrorAndRedirect(
                    "Database returned NULL DataSet."
                );

                return;
            }


            if (ds.Tables.Count == 0)
            {
                ShowErrorAndRedirect(
                    "Database returned no tables."
                );

                return;
            }


            if (ds.Tables[0].Rows.Count == 0)
            {
                ShowErrorAndRedirect(
                    "No payment record found for application number: " +
                    applicationNumber
                );

                return;
            }


            // ========================================================
            // STEP 9: GET DATABASE ROW
            // ========================================================

            DataRow row =
                ds.Tables[0].Rows[0];


            currentStep =
                "READ_DATABASE_ROW";


            // ========================================================
            // IMPORTANT:
            //
            // These are now taken FROM DATABASE.
            //
            // We DO NOT trust GRN / DEPTTRANID / TRN_REF_NO
            // from the incoming URL.
            // ========================================================


            // Actual numeric APP_ID
            string dbAppId =
                GetDatabaseValue(
                    row,
                    "APP_ID"
                );


            // Actual application number
            string dbApplicationNumber =
                GetDatabaseValue(
                    row,
                    "APPLICATION_NUMBER"
                );


            // Actual transaction status
            string dbStatus =
                GetDatabaseValue(
                    row,
                    "TRN_STATUS"
                );


            // Actual GRN
            string dbGrn =
                GetDatabaseValue(
                    row,
                    "GRN"
                );


            // Actual Department Transaction ID
            string dbDeptTranId =
                GetDatabaseValue(
                    row,
                    "DEPTTRANID"
                );


            // Actual transaction reference
            string dbTrnRefNo =
                GetDatabaseValue(
                    row,
                    "TRN_REF_NO"
                );


            // Actual transaction type
            string trType =
                GetDatabaseValue(
                    row,
                    "TR_TYPE"
                );


            // Actual operation/service
            string operationFor =
                GetDatabaseValue(
                    row,
                    "OPERATION_FOR"
                );


            // Actual applicant
            string applicantName =
                GetDatabaseValue(
                    row,
                    "APPLICANT_NAME"
                );


            // Actual amount
            string amount =
                GetDatabaseValue(
                    row,
                    "AMOUNT"
                );


            currentStep =
                "DB_VALUES" +
                " | APP_ID=" + dbAppId +
                " | APPLICATION_NUMBER=" + dbApplicationNumber +
                " | GRN=" + dbGrn +
                " | DEPTTRANID=" + dbDeptTranId +
                " | TRN_REF_NO=" + dbTrnRefNo +
                " | STATUS=" + dbStatus +
                " | TR_TYPE=" + trType +
                " | OPERATION_FOR=" + operationFor +
                " | APPLICANT=" + applicantName +
                " | AMOUNT=" + amount;


            // ========================================================
            // STEP 10: VALIDATE DB APP_ID
            // ========================================================

            if (string.IsNullOrWhiteSpace(dbAppId))
            {
                ShowErrorAndRedirect(
                    "APP_ID is missing in payment transaction."
                );

                return;
            }


            // ========================================================
            // STEP 11: VALIDATE DB APPLICATION NUMBER
            // ========================================================

            if (string.IsNullOrWhiteSpace(
                    dbApplicationNumber))
            {
                ShowErrorAndRedirect(
                    "Application number is missing in payment transaction."
                );

                return;
            }


            // ========================================================
            // STEP 12: VALIDATE PAYMENT STATUS
            // ========================================================
            //
            // Receipt is generated only for SUCCESS.
            //
            // No JEgras verification is performed.
            // ========================================================

            if (!string.Equals(
                    dbStatus,
                    "SUCCESS",
                    StringComparison.OrdinalIgnoreCase))
            {
                ShowErrorAndRedirect(
                    "Payment status is " +
                    dbStatus +
                    ". Receipt cannot be generated."
                );

                return;
            }


            // ========================================================
            // STEP 13: VALIDATE TRANSACTION TYPE
            // ========================================================

            if (string.IsNullOrWhiteSpace(trType))
            {
                ShowErrorAndRedirect(
                    "Transaction type is missing in payment transaction."
                );

                return;
            }


            // ========================================================
            // STEP 14: DETERMINE TABLE TYPE
            // ========================================================

            currentStep =
                "DETERMINE_TABLE_TYPE";


            string tableType =
                GetTableType(trType);


            // ========================================================
            // STEP 15: DETERMINE APPLICATION SERVICE
            // ========================================================
            //
            // IMPORTANT:
            // Do NOT hardcode:
            //
            // STA COUNTER SIGNATURE TAX PAY
            //
            // Use actual OPERATION_FOR from DB.
            // ========================================================

            string applicationService =
                operationFor;


            // ========================================================
            // STEP 16: BUILD RECEIPT QUERY
            // ========================================================

            currentStep =
                "BUILD_RECEIPT_QUERY";


            /*
             * IMPORTANT:
             *
             * Receipt page expects numeric APP_ID.
             *
             * Example:
             *
             * APP_ID = 179
             *
             * NOT:
             *
             * APP_ID = JH-CS-VR-0102
             *
             */

            string receiptQuery =
                "APP_ID=" +
                HttpUtility.UrlEncode(dbAppId) +

                "&GRN=" +
                HttpUtility.UrlEncode(dbGrn) +

                "&TBL=" +
                HttpUtility.UrlEncode(tableType) +

                "&TYPE=" +
                HttpUtility.UrlEncode(trType) +

                "&APPLICATION_SERVICE=" +
                HttpUtility.UrlEncode(applicationService) +

                "&TRN_REF_NO=" +
                HttpUtility.UrlEncode(dbTrnRefNo);


            // ========================================================
            // STEP 17: ENCRYPT RECEIPT QUERY
            // ========================================================

            currentStep =
                "ENCRYPT_RECEIPT_QUERY";


            string encryptedReceiptQuery =
                CryptorEngine.Encrypt(
                    receiptQuery,
                    true
                );


            // ========================================================
            // STEP 18: REDIRECT TO RECEIPT
            // ========================================================

            currentStep =
                "REDIRECT_TO_RECEIPT";


            string redirectUrl =
                "STA_PERMIT_COUNTER_SIGNATURE_TRANSACTION_RECEIPT_CLNT_NEW.aspx?q=" +
                HttpUtility.UrlEncode(
                    encryptedReceiptQuery
                );


            /*
             * IMPORTANT:
             *
             * false prevents ThreadAbortException.
             *
             * Do NOT change this to:
             *
             * Response.Redirect(url);
             *
             * because older ASP.NET WebForms can throw
             * ThreadAbortException.
             */

            Response.Redirect(
                redirectUrl,
                false
            );


            Context.ApplicationInstance
                   .CompleteRequest();
        }
        catch (Exception ex)
        {
            // ========================================================
            // GLOBAL ERROR
            // ========================================================

            string errorMessage =
                "STEP: " +
                currentStep +
                " | ERROR: " +
                ex.Message;


            if (ex.InnerException != null)
            {
                errorMessage +=
                    " | INNER: " +
                    ex.InnerException.Message;
            }


            ShowErrorAndRedirect(
                errorMessage
            );
        }
    }


    // ================================================================
    // PARSE RECEIPT REQUEST
    // ================================================================
    //
    // Supports:
    //
    // 1. KEY/VALUE FORMAT
    //
    // APP_ID=JH-CS-VR-0102
    // &GRN=2602215253
    // &DEPTTRANID=639141825319578697
    // &TRN_REF_NO=9255925118237
    //
    //
    // 2. LEGACY PIPE FORMAT
    //
    // JH-CS-VR-0102|2602215253|639141825319578697|SUCCESS
    //
    // ================================================================

    private Dictionary<string, string>
        ParseReceiptRequest(
            string decryptedRequest)
    {
        Dictionary<string, string> values =
            new Dictionary<string, string>(
                StringComparer.OrdinalIgnoreCase
            );


        if (string.IsNullOrWhiteSpace(
                decryptedRequest))
        {
            return values;
        }


        // ============================================================
        // KEY=VALUE FORMAT
        // ============================================================

        if (decryptedRequest.Contains("="))
        {
            string[] parts =
                decryptedRequest.Split('&');


            foreach (string part in parts)
            {
                if (string.IsNullOrWhiteSpace(part))
                {
                    continue;
                }


                int separatorIndex =
                    part.IndexOf('=');


                if (separatorIndex <= 0)
                {
                    continue;
                }


                string key =
                    part.Substring(
                        0,
                        separatorIndex
                    ).Trim();


                string value =
                    part.Substring(
                        separatorIndex + 1
                    ).Trim();


                try
                {
                    value =
                        HttpUtility.UrlDecode(
                            value
                        );
                }
                catch
                {
                    // Keep original value
                }


                if (!values.ContainsKey(key))
                {
                    values.Add(
                        key,
                        value
                    );
                }
            }


            return values;
        }


        // ============================================================
        // LEGACY PIPE FORMAT
        // ============================================================
        //
        // ApplicationNumber
        // GRN
        // DEPTTRANID
        // STATUS
        //
        // ============================================================

        if (decryptedRequest.Contains("|"))
        {
            string[] parts =
                decryptedRequest.Split('|');


            if (parts.Length > 0)
            {
                values["APPLICATION_NUMBER"] =
                    parts[0];
            }


            if (parts.Length > 1)
            {
                values["GRN"] =
                    parts[1];
            }


            if (parts.Length > 2)
            {
                values["DEPTTRANID"] =
                    parts[2];
            }


            if (parts.Length > 3)
            {
                values["STATUS"] =
                    parts[3];
            }
        }


        return values;
    }


    // ================================================================
    // GET REQUEST VALUE
    // ================================================================

    private string GetValue(
        Dictionary<string, string> values,
        string key)
    {
        if (values == null)
        {
            return "";
        }


        string value;


        if (values.TryGetValue(
                key,
                out value))
        {
            return value ?? "";
        }


        return "";
    }


    // ================================================================
    // GET DATABASE VALUE
    // ================================================================

    private string GetDatabaseValue(
        DataRow row,
        string columnName)
    {
        if (row == null)
        {
            return "";
        }


        if (!row.Table.Columns.Contains(
                columnName))
        {
            return "";
        }


        if (row[columnName] == DBNull.Value)
        {
            return "";
        }


        return Convert.ToString(
            row[columnName]
        ).Trim();
    }


    // ================================================================
    // DETERMINE TABLE TYPE
    // ================================================================
    //
    // Example:
    //
    // STA-APPLICATION_q_MSTR
    //
    // returns:
    //
    // MSTR
    //
    // ================================================================

    private string GetTableType(
        string trType)
    {
        if (string.IsNullOrWhiteSpace(
                trType))
        {
            return "MSTR";
        }


        string[] parts =
            trType.Split('_');


        if (parts.Length > 0)
        {
            string lastPart =
                parts[parts.Length - 1]
                    .Trim();


            if (string.Equals(
                    lastPart,
                    "MSTR",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "MSTR";
            }


            if (string.Equals(
                    lastPart,
                    "DTL",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "DTL";
            }


            if (string.Equals(
                    lastPart,
                    "DETAIL",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "DTL";
            }


            // If another table identifier is already
            // present in TR_TYPE, use it.
            if (!string.IsNullOrWhiteSpace(
                    lastPart))
            {
                return lastPart;
            }
        }


        return "MSTR";
    }


    // ================================================================
    // ERROR HANDLER
    // ================================================================

    private void ShowErrorAndRedirect(
        string message)
    {
        string cleanMessage =
            (message ??
             "Unable to process request.")
            .Replace(
                "\\",
                "\\\\"
            )
            .Replace(
                "'",
                "\\'"
            )
            .Replace(
                "\r",
                " "
            )
            .Replace(
                "\n",
                " "
            );


        string script =
            "alert('" +
            cleanMessage +
            "');" +
            "window.location='PermitHomePage.aspx';";


        ScriptManager.RegisterStartupScript(
            this,
            GetType(),
            Guid.NewGuid().ToString(),
            script,
            true
        );
    }
}