using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Script.Serialization;

public static class MiscPaymentService
{
    private static string ConnectionString
    {
        get { return ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString; }
    }

    public static DataTable ExecuteReason(string mode, string authorityCode, int? reasonId)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_MISC_PAYMENT_REASON_AED", con))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 30).Value = mode;
            cmd.Parameters.Add("@AUTHORITY_CODE", SqlDbType.VarChar, 3).Value = (object)authorityCode ?? DBNull.Value;
            cmd.Parameters.Add("@REASON_ID", SqlDbType.Int).Value = (object)reasonId ?? DBNull.Value;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public static void MarkRequested(string deptTranId)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_MISC_PAYMENT_TRANSACTION_AED", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 30).Value = "MARK_REQUESTED";
            cmd.Parameters.Add("@DEPTTRANID", SqlDbType.VarChar, 40).Value = deptTranId;
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static DataTable GetTransaction(string deptTranId)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_MISC_PAYMENT_TRANSACTION_AED", con))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 30).Value = "GET_BY_DEPTTRANID";
            cmd.Parameters.Add("@DEPTTRANID", SqlDbType.VarChar, 40).Value = deptTranId;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public static DataTable GetTransactionsByApplication(string applicationNumber)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_MISC_PAYMENT_TRANSACTION_AED", con))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 30).Value = "GET_BY_APPLICATION";
            cmd.Parameters.Add("@APPLICATION_NUMBER", SqlDbType.VarChar, 120).Value = applicationNumber;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public static void CreateTransaction(string deptTranId, int appId, string applicationNumber,
        string applicantName, string authorityCode, int reasonId, decimal amount, string fieldsJson,
        string deptId, string receiptHead, string treasury, string office)
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_MISC_PAYMENT_TRANSACTION_AED", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 30).Value = "CREATE";
            cmd.Parameters.Add("@DEPTTRANID", SqlDbType.VarChar, 40).Value = deptTranId;
            cmd.Parameters.Add("@APP_ID", SqlDbType.Int).Value = appId;
            cmd.Parameters.Add("@APPLICATION_NUMBER", SqlDbType.VarChar, 120).Value = applicationNumber;
            cmd.Parameters.Add("@APPLICANT_NAME", SqlDbType.NVarChar, 200).Value = applicantName;
            cmd.Parameters.Add("@AUTHORITY_CODE", SqlDbType.VarChar, 3).Value = authorityCode;
            cmd.Parameters.Add("@REASON_ID", SqlDbType.Int).Value = reasonId;
            cmd.Parameters.Add("@AMOUNT", SqlDbType.Decimal).Value = amount;
            cmd.Parameters["@AMOUNT"].Precision = 18;
            cmd.Parameters["@AMOUNT"].Scale = 2;
            cmd.Parameters.Add("@ADDITIONAL_FIELDS_JSON", SqlDbType.NVarChar, -1).Value = (object)fieldsJson ?? DBNull.Value;
            cmd.Parameters.Add("@DEPTID", SqlDbType.VarChar, 20).Value = deptId;
            cmd.Parameters.Add("@RECEIPTHEADCODE", SqlDbType.VarChar, 30).Value = receiptHead;
            cmd.Parameters.Add("@TREASCODE", SqlDbType.VarChar, 20).Value = treasury;
            cmd.Parameters.Add("@IFMSOFFICECODE", SqlDbType.VarChar, 30).Value = office;
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static void SaveGatewayResponse(string deptTranId, string status, string message, string reference,
        string payMode, DateTime? txnDate, decimal? txnAmount, string challanUrl, string grn, string cin,
        string deptId, string receiptHead, string panNo, string treasury, string office, string addInfo5,
        string mode = "UPDATE_RESPONSE")
    {
        using (SqlConnection con = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_MISC_PAYMENT_TRANSACTION_AED", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 30).Value = mode;
            cmd.Parameters.Add("@DEPTTRANID", SqlDbType.VarChar, 40).Value = deptTranId;
            cmd.Parameters.Add("@TRN_STATUS", SqlDbType.VarChar, 20).Value = status;
            cmd.Parameters.Add("@TRN_MESSAGE", SqlDbType.NVarChar, 1000).Value = (object)message ?? DBNull.Value;
            cmd.Parameters.Add("@TRN_REF_NO", SqlDbType.VarChar, 100).Value = (object)reference ?? DBNull.Value;
            cmd.Parameters.Add("@PAY_MODE", SqlDbType.VarChar, 50).Value = (object)payMode ?? DBNull.Value;
            cmd.Parameters.Add("@TXN_DATE", SqlDbType.DateTime2).Value = (object)txnDate ?? DBNull.Value;
            SqlParameter amount = cmd.Parameters.Add("@TXN_AMOUNT", SqlDbType.Decimal);
            amount.Precision = 18; amount.Scale = 2; amount.Value = (object)txnAmount ?? DBNull.Value;
            cmd.Parameters.Add("@CHALLAN_URL", SqlDbType.NVarChar, 1000).Value = (object)challanUrl ?? DBNull.Value;
            cmd.Parameters.Add("@GRN", SqlDbType.VarChar, 100).Value = (object)grn ?? DBNull.Value;
            cmd.Parameters.Add("@CIN", SqlDbType.VarChar, 100).Value = (object)cin ?? DBNull.Value;
            cmd.Parameters.Add("@DEPTID", SqlDbType.VarChar, 20).Value = (object)deptId ?? DBNull.Value;
            cmd.Parameters.Add("@RECEIPTHEADCODE", SqlDbType.VarChar, 30).Value = (object)receiptHead ?? DBNull.Value;
            cmd.Parameters.Add("@PANNO", SqlDbType.VarChar, 20).Value = (object)panNo ?? DBNull.Value;
            cmd.Parameters.Add("@TREASCODE", SqlDbType.VarChar, 20).Value = (object)treasury ?? DBNull.Value;
            cmd.Parameters.Add("@IFMSOFFICECODE", SqlDbType.VarChar, 30).Value = (object)office ?? DBNull.Value;
            cmd.Parameters.Add("@ADDINFO5", SqlDbType.NVarChar, 500).Value = (object)addInfo5 ?? DBNull.Value;
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public static string ToJson(IDictionary<string, string> values)
    {
        return new JavaScriptSerializer().Serialize(values);
    }
}