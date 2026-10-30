<%@ WebHandler Language="C#" Class="Vehicle" %>

using System;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Web.Script.Serialization;
using Newtonsoft.Json;


public class Vehicle : IHttpHandler {

    protected List<QueryString> QueryStr = new List<QueryString>();

    public void ProcessRequest(HttpContext context)
    {
        // Do not resolve the database connection string before dispatching:
        // GetPaymentHistory validates it inside its own try/catch so errors can
        // be returned as JSON instead of the site's generic HTML error page.
        string sConnectionString = null;
        string caller = Convert.ToString(context.Request["caller"]);

        switch (caller)
        {

            case "GetPaymentHistory":
                GetPaymentHistory(context);
                return;

            case "GetPaymentStatus":

                sConnectionString =
                    ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;

                string queryString =
                    Convert.ToString(context.Request["q"]);

                string applicationNumber = (queryString ?? "").Trim();
                string strResult = string.Empty;

                string verificationId = PaymentLogger.GenerateVerificationId();

                context.Response.ContentType = "text/plain";
                context.Response.ContentEncoding = Encoding.UTF8;

                if (string.IsNullOrWhiteSpace(applicationNumber))
                {
                    context.Response.StatusCode = 400;
                    context.Response.Write("Success~exp~Application number is required.");
                    break;
                }

                // Read the selected payment ID.
                // If it is missing, NULL preserves the previous latest-payment behavior.
                long selectedPaymentId;
                long? selectedPaymentIdNullable = null;

                string selectedPaymentIdText =
                    context.Request["selectedPaymentId"];

                if (!string.IsNullOrWhiteSpace(selectedPaymentIdText))
                {
                    if (!long.TryParse(
                        selectedPaymentIdText,
                        out selectedPaymentId))
                    {
                        context.Response.StatusCode = 400;
                        context.Response.Write(
                            "Success~exp~Invalid selected payment ID.");
                        break;
                    }

                    selectedPaymentIdNullable = selectedPaymentId;
                }

                PaymentLogger.Info(
                    verificationId,
                    applicationNumber,
                    "",
                    "PAYMENT_STATUS_CHECK_STARTED",
                    "Payment status check initiated for application: "
                        + applicationNumber
                        + "; Selected Payment ID: "
                        + (selectedPaymentIdNullable.HasValue
                            ? selectedPaymentIdNullable.Value.ToString()
                            : "Latest payment"));

                using (SqlConnection sCon =
                    new SqlConnection(sConnectionString))
                {
                    try
                    {
                        sCon.Open();

                        DataTable dt = new DataTable();

                        using (SqlCommand sCmd =
                            new SqlCommand("SP_PRE_PAYMENT_DETAIL", sCon))
                        {
                            sCmd.CommandType = CommandType.StoredProcedure;

                            sCmd.Parameters.Add(
                                "@APPLICATION_NUMBER",
                                SqlDbType.VarChar,
                                150
                            ).Value = applicationNumber.ToUpperInvariant();

                            sCmd.Parameters.Add(
                                "@MODE",
                                SqlDbType.VarChar,
                                100
                            ).Value = "APPLICATION_SEARCH_PAYMENT_TRANS";

                            sCmd.Parameters.Add(
                                "@SELECTED_PAYMENT_ID",
                                SqlDbType.BigInt
                            ).Value = selectedPaymentIdNullable.HasValue
                                ? (object)selectedPaymentIdNullable.Value
                                : DBNull.Value;

                            using (SqlDataAdapter da =
                                new SqlDataAdapter(sCmd))
                            {
                                da.Fill(dt);
                            }
                        }

                        // Ensure the stored procedure returned a result.
                        if (dt.Rows.Count == 0)
                        {
                            strResult = "Success~Not Found";
                        }
                        else
                        {
                            DataRow row = dt.Rows[0];

                            string message =
                                dt.Columns.Contains("MSG")
                                    ? Convert.ToString(row["MSG"]).Trim()
                                    : "";

                            if (message ==
                                "Payment Submitted Successfully.")
                            {
                                // SUCCESS transaction:
                                // Preserve the existing encrypted payload format.
                                string deptTranId =
                                    Convert.ToString(row["DEPTTRANID"]).Trim();

                                string appId =
                                    Convert.ToString(row["APP_ID"]);

                                string appNumber =
                                    Convert.ToString(row["APPLICATION_NUMBER"]);

                                string operationFor =
                                    Convert.ToString(row["OPERATION_FOR"]);

                                string trType =
                                    Convert.ToString(row["TR_TYPE"]);

                                string encryptedData =
                                    deptTranId + "~"
                                    + appId + "~"
                                    + appNumber + "~"
                                    + operationFor + "~"
                                    + trType + "~SUCCESS";

                                strResult = "Success~Submitted~"
                                    + CryptorEngine.Encrypt(
                                        encryptedData, true);

                                PaymentLogger.Info(
                                    verificationId,
                                    applicationNumber,
                                    deptTranId,
                                    "PAYMENT_STATUS_CHECK_STARTED",
                                    "Selected transaction returned SUCCESS.",
                                    appId,
                                    "SUCCESS",
                                    operationFor);
                            }
                            else if (message ==
                                "Payment Failed Please Try Again Later.")
                            {
                                // Use the selected FAIL transaction's details
                                // returned by SP_PRE_PAYMENT_DETAIL.
                                // Do not query for another failed transaction.
                                string deptTranId =
                                    Convert.ToString(row["DEPTTRANID"]).Trim();

                                string appId =
                                    Convert.ToString(row["APP_ID"]);

                                string appNumber =
                                    Convert.ToString(row["APPLICATION_NUMBER"]);

                                string operationFor =
                                    Convert.ToString(row["OPERATION_FOR"]);

                                string trType =
                                    Convert.ToString(row["TR_TYPE"]);

                                string encryptedData =
                                    deptTranId + "~"
                                    + appId + "~"
                                    + appNumber + "~"
                                    + operationFor + "~"
                                    + trType + "~FAIL";

                                strResult = "Success~encry~"
                                    + CryptorEngine.Encrypt(
                                        encryptedData, true);

                                PaymentLogger.Info(
                                    verificationId,
                                    applicationNumber,
                                    deptTranId,
                                    "PREVIOUS_FAIL_RECHECK_STARTED",
                                    "Selected FAIL transaction returned for verification.",
                                    appId,
                                    "FAIL",
                                    operationFor);
                            }
                            else if (message ==
                                "Please Check Payment Detail after 2 hours.")
                            {
                                strResult = "Success~2 Hrs.";

                                PaymentLogger.Info(
                                    verificationId,
                                    applicationNumber,
                                    "",
                                    "PAYMENT_STATUS_CHECK_STARTED",
                                    "Selected BOOKED transaction is not yet eligible for recheck.");
                            }
                            else if (message == "BOOKED")
                            {
                                // BOOKED transaction eligible for re-verification.
                                string deptTranId =
                                    Convert.ToString(row["DEPTTRANID"]).Trim();

                                string appId =
                                    Convert.ToString(row["APP_ID"]);

                                string appNumber =
                                    Convert.ToString(row["APPLICATION_NUMBER"]);

                                string operationFor =
                                    Convert.ToString(row["OPERATION_FOR"]);

                                string trType =
                                    Convert.ToString(row["TR_TYPE"]);

                                string encryptedData =
                                    deptTranId + "~"
                                    + appId + "~"
                                    + appNumber + "~"
                                    + operationFor + "~"
                                    + trType + "~BOOKED";

                                strResult = "Success~encry~"
                                    + CryptorEngine.Encrypt(
                                        encryptedData, true);

                                PaymentLogger.Info(
                                    verificationId,
                                    applicationNumber,
                                    deptTranId,
                                    "PAYMENT_STATUS_CHECK_STARTED",
                                    "Selected BOOKED transaction returned for verification.",
                                    appId,
                                    "BOOKED",
                                    operationFor);
                            }
                            else if (
                                message ==
                                    "Application/payment transaction not found."
                                || message == "Application not found")
                            {
                                strResult = "Success~Not Found";

                                PaymentLogger.Info(
                                    verificationId,
                                    applicationNumber,
                                    "",
                                    "PAYMENT_STATUS_CHECK_STARTED",
                                    "No matching payment transaction was found.");
                            }
                            else
                            {
                                // Handles unrecognized statuses or unexpected
                                // stored procedure responses.
                                strResult = "Success~exp~"
                                    + (string.IsNullOrWhiteSpace(message)
                                        ? "Unexpected payment status response."
                                        : message);

                                PaymentLogger.Warning(
                                    verificationId,
                                    applicationNumber,
                                    "",
                                    "PAYMENT_STATUS_CHECK_STARTED",
                                    "Unexpected stored procedure response: " + message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string errorMessage = ex.Message
                            .Replace("\r", " ")
                            .Replace("\n", " ")
                            .Replace("~", "-");

                        strResult = "Success~exp~" + errorMessage;

                        PaymentLogger.Error(
                            verificationId,
                            applicationNumber,
                            "",
                            "PAYMENT_STATUS_CHECK_STARTED",
                            "Handler exception: " + ex);
                    }
                }

                context.Response.Write(strResult);
               break;

                
            case "Reprintdata":

                     sConnectionString =
                         ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;

                     queryString = Convert.ToString(context.Request.QueryString["q"]);
                     applicationNumber = queryString.Trim();
                     strResult = string.Empty;
                
                    using (SqlConnection sCon = new SqlConnection(sConnectionString))
                    {
                        
                        try
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "SP_PRE_PAYMENT_DETAIL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", applicationNumber.Trim().ToUpper());
                            sCmd.Parameters.AddWithValue("@MODE", "APPLICATION_SEARCH_PAYMENT_TRANS");
                            sCmd.Parameters.AddWithValue("@PType", "Reprint");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];

                            if (Convert.ToString(dt.Rows[0]["MSG"]) != "Application not found")
                            {
                                if (Convert.ToString(dt.Rows[0]["MSG"]) == "Payment Submitted Successfully.")
                                {
                                    //string TxAmount = Convert.ToString(dt.Rows[0]["TXN_AMOUNT"]);
                                    //string TxRefN = Convert.ToString(dt.Rows[0]["TRN_REF_NO"]);
                                    //strResult = "Success~Submitted~" + TxAmount + "~" + TxRefN;
                                    string DEPTTRANDID = Convert.ToString(dt.Rows[0]["DEPTTRANID"]);
                                    string App_id = Convert.ToString(dt.Rows[0]["APP_ID"]);
                                    string Application_number = Convert.ToString(dt.Rows[0]["APPLICATION_NUMBER"]);
                                    string operation_for = Convert.ToString(dt.Rows[0]["OPERATION_FOR"]);
                                    string tr_type = Convert.ToString(dt.Rows[0]["TR_TYPE"]);
                                    strResult = DEPTTRANDID.Trim();
                                    strResult += "~" + App_id + "~" + Application_number + "~" + operation_for + "~" + tr_type;
                                    strResult = "Success~encry~" + CryptorEngine.Encrypt(strResult, true);
                                }
                                else if (Convert.ToString(dt.Rows[0]["MSG"]) == "Please Check Payment Detail after 2 hours.")
                                {
                                    strResult = "Success~2 Hrs.";
                                }
                                else if (Convert.ToString(dt.Rows[0]["MSG"]) == "Payment Failed Please Try Again Later.")
                                {
                                    strResult = "Success~Failed";

                                }

                                else if (Convert.ToString(dt.Rows[0]["MSG"]) == "BOOKED")
                                {
                                    string DEPTTRANDID = Convert.ToString(dt.Rows[0]["DEPTTRANID"]);
                                    string App_id = Convert.ToString(dt.Rows[0]["APP_ID"]);
                                    string Application_number = Convert.ToString(dt.Rows[0]["APPLICATION_NUMBER"]);
                                    string operation_for = Convert.ToString(dt.Rows[0]["OPERATION_FOR"]);
                                    string tr_type = Convert.ToString(dt.Rows[0]["TR_TYPE"]);
                                    strResult = DEPTTRANDID.Trim();
                                    strResult += "~" + App_id + "~" + Application_number+ "~"+ operation_for+"~"+tr_type;
                                    strResult = "Success~encry~"+CryptorEngine.Encrypt(strResult, true);
                                    
                                    
                                }
                            }
                            else
                            {
                                strResult = "Success~Not Found";

                            }
                        }
                        catch (Exception ex)
                        {
                            String Message = ex.Message;
                            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                            strResult = "Success~exp~"+ Message;

                        }
                    }

                   
                    context.Response.ContentType = "application/text";
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.Write(strResult);
                    
                break;
            
        }

    }

   private void GetPaymentHistory(HttpContext context)
    {
        context.Response.Clear();
        context.Response.ContentType = "application/json";
        context.Response.ContentEncoding = Encoding.UTF8;

        string applicationNumber = (context.Request["q"] ?? "").Trim();

        if (string.IsNullOrWhiteSpace(applicationNumber))
        {
            context.Response.StatusCode = 400;
            context.Response.Write(
                "{\"error\":\"Application number is required.\"}");
            return;
        }


        try
        {
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings["connectionstring"];

            if (settings == null ||
                string.IsNullOrWhiteSpace(settings.ConnectionString))
            {
                throw new ConfigurationErrorsException(
                    "Connection string 'connectionstring' was not found.");
            }

            string connectionString = settings.ConnectionString;

            DataTable dt = new DataTable();

            using (SqlConnection con =
                new SqlConnection(connectionString))
            using (SqlCommand cmd =
                new SqlCommand("dbo.SP_PRE_PAYMENT_DETAIL", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(
                    "@APPLICATION_NUMBER",
                    SqlDbType.VarChar,
                    150
                ).Value = applicationNumber;

                cmd.Parameters.Add(
                    "@MODE",
                    SqlDbType.VarChar,
                    100
                ).Value = "APPLICATION_PAYMENT_HISTORY";

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            // Convert each DataRow to a plain dictionary. JavaScriptSerializer
            // serializes dictionaries/lists reliably; it does not serialize
            // DataTable objects directly in all ASP.NET versions.
            List<Dictionary<string, object>> records =
                new List<Dictionary<string, object>>();

            foreach (DataRow dataRow in dt.Rows)
            {
                Dictionary<string, object> record =
                    new Dictionary<string, object>();

                foreach (DataColumn column in dt.Columns)
                {
                    record[column.ColumnName] =
                        dataRow.IsNull(column) ? null : dataRow[column];
                }

                records.Add(record);
            }

            JavaScriptSerializer serializer =
                new JavaScriptSerializer();

            context.Response.Clear();
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.TrySkipIisCustomErrors = true;
            context.Response.Write(serializer.Serialize(records));
        }
        catch (Exception ex)
        {
            // Detailed exception goes to the configured application trace listener.
            System.Diagnostics.Trace.TraceError(
                "GetPaymentHistory failed for application number " +
                applicationNumber + ". Exception: " + ex);

            System.Diagnostics.Trace.Flush();

            // Also send the error to the existing application logger, if available.
            try
            {
                string verificationId =
                    PaymentLogger.GenerateVerificationId();

                PaymentLogger.Error(
                    verificationId,
                    applicationNumber,
                    "",
                    "GET_PAYMENT_HISTORY_FAILED",
                    "Payment history retrieval failed: " + ex);
            }
            catch (Exception loggingException)
            {
                System.Diagnostics.Trace.TraceError(
                    "Payment history error logging failed: " +
                    loggingException);
            }

            context.Response.StatusCode = 500;
            context.Response.TrySkipIisCustomErrors = true;

            JavaScriptSerializer serializer =
                new JavaScriptSerializer();

            context.Response.Write(
                serializer.Serialize(
                    new
                    {
                        error = "Unable to retrieve payment history.",
                        detail = ex.Message
                    }));
        }
}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

}