using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEra.JHT.Infrastructure;

public partial class MISC_PAYMENT_STATUS : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DivMobileValidate.Visible = false;
            DivOTPMatch.Visible = false;
            gvTransactions.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session.Remove("MISC_STATUS_VERIFIED_APP");
        txtApplicationNumber.ReadOnly = false;
        txtMobileNumber.Disabled = false;
        txtMobileNumber.Value = "";
        txtOTP.Value = "";
        btnOTP.Enabled = true;
        DivMobileValidate.Visible = false;
        DivOTPMatch.Visible = false;
        gvTransactions.Visible = false;
        string applicationNumber = txtApplicationNumber.Text.Trim();
        if (string.IsNullOrWhiteSpace(applicationNumber))
        {
            ShowMessage("Enter an application number.");
            return;
        }

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_VIEW_APPLICATION_STATUS", con))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@APPLICATION_NUMBER", SqlDbType.VarChar, 120).Value = applicationNumber;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 40).Value = applicationNumber.Contains("(PTT)") ? "CS_APPLICATION_NUMBER" : "VERIFY_APPLICATION_NUMBER";
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0 || dt.Columns.Contains("MSG"))
            {
                ShowMessage(dt.Rows.Count == 0 ? "Application not found." : Convert.ToString(dt.Rows[0]["MSG"]));
                return;
            }
            HFTableName.Value = Convert.ToString(dt.Rows[0]["TABLE_NAME"]);
            HFPermitNo.Value = dt.Columns.Contains("PERMIT_NUMBER") ? Convert.ToString(dt.Rows[0]["PERMIT_NUMBER"]) : "";
            DivMobileValidate.Visible = true;
            DivOTPMatch.Visible = false;
            gvTransactions.Visible = false;
            txtApplicationNumber.ReadOnly = true;
            lblMessage.Visible = false;
        }
    }

    protected void btnOTP_Click(object sender, EventArgs e)
    {
        string mobile = txtMobileNumber.Value.Trim();
        if (mobile.Length != 10) { ShowMessage("Enter a valid 10 digit mobile number."); return; }
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_VIEW_APPLICATION_STATUS", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@APPLICATION_NUMBER", SqlDbType.VarChar, 120).Value = txtApplicationNumber.Text.Trim();
            cmd.Parameters.Add("@PERMIT_NUMBER", SqlDbType.VarChar, 100).Value = HFPermitNo.Value;
            cmd.Parameters.Add("@TABLE_NAME", SqlDbType.VarChar, 128).Value = HFTableName.Value;
            cmd.Parameters.Add("@MOBILE_NUMBER", SqlDbType.VarChar, 10).Value = mobile;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 40).Value = "VERIFY_MOBILE_NUMBER";
            con.Open();
            object otp = cmd.ExecuteScalar();
            if (otp == null || Convert.ToString(otp) == "MOBILE NUMBER NOT FOUND")
            {
                ShowMessage("Mobile number does not match the application record.");
                return;
            }
            new SMSSender().Sendsms(mobile, Convert.ToString(otp), "1307161527893297291");
            txtMobileNumber.Disabled = true;
            btnOTP.Enabled = false;
            DivOTPMatch.Visible = true;
            lblMessage.Visible = false;
        }
    }

    protected void btnVerifyOTP_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_VIEW_APPLICATION_STATUS", con))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@APPLICATION_NUMBER", SqlDbType.VarChar, 120).Value = txtApplicationNumber.Text.Trim();
            cmd.Parameters.Add("@TABLE_NAME", SqlDbType.VarChar, 128).Value = HFTableName.Value;
            cmd.Parameters.Add("@PERMIT_NUMBER", SqlDbType.VarChar, 100).Value = HFPermitNo.Value;
            cmd.Parameters.Add("@OTP", SqlDbType.VarChar, 10).Value = txtOTP.Value.Trim();
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 40).Value = "VERIFY_OTP";
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                ShowMessage("Invalid or expired OTP.");
                return;
            }
            Session["MISC_STATUS_VERIFIED_APP"] = txtApplicationNumber.Text.Trim();
            DivOTPMatch.Visible = false;
            gvTransactions.Visible = true;
            BindTransactions(txtApplicationNumber.Text.Trim());
        }
    }

    protected void gvTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "VERIFY") return;
        if (!string.Equals(Convert.ToString(Session["MISC_STATUS_VERIFIED_APP"]),
            txtApplicationNumber.Text.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            ShowMessage("Verify the registered mobile number before checking payment status.");
            return;
        }
        try
        {
            VerifyTransaction(Convert.ToString(e.CommandArgument));
            BindTransactions(txtApplicationNumber.Text.Trim());
            ShowMessage("JEGRAS verification completed. Review the latest transaction status below.");
        }
        catch (Exception ex)
        {
            ShowMessage("Verification could not be completed: " + ex.Message);
        }
    }

    private void BindTransactions(string applicationNumber)
    {
        DataTable dt = MiscPaymentService.GetTransactionsByApplication(applicationNumber);
        gvTransactions.Visible = true;
        gvTransactions.DataSource = dt;
        gvTransactions.DataBind();
        lblMessage.Visible = dt.Rows.Count == 0;
        if (dt.Rows.Count == 0) lblMessage.Text = "No miscellaneous payment transactions found.";
    }

    private void ShowMessage(string message)
    {
        lblMessage.Text = Server.HtmlEncode(message);
        lblMessage.Visible = true;
    }

    private void VerifyTransaction(string deptTranId)
    {
        DataTable tx = MiscPaymentService.GetTransaction(deptTranId);
        if (tx.Rows.Count != 1) throw new InvalidOperationException("Transaction not found.");
        DataRow stored = tx.Rows[0];
        string currentStatus = Convert.ToString(stored["TRN_STATUS"]);
        if (currentStatus == "SUCCESS") return;
        if (currentStatus != "BOOKED" && currentStatus != "FAIL" && currentStatus != "INITIATED" && currentStatus != "REQUESTED")
            throw new InvalidOperationException("This transaction cannot be verified in its current state.");

        string securityCode = System.Configuration.ConfigurationManager.AppSettings["JEGRAS_SECURITY_CODE"];
        string encryptionKey = System.Configuration.ConfigurationManager.AppSettings["JEGRAS_ENCRYPTION_KEY"];
        if (string.IsNullOrWhiteSpace(encryptionKey)) encryptionKey = "key1234";
        if (string.IsNullOrWhiteSpace(securityCode))
            throw new InvalidOperationException("JEGRAS_SECURITY_CODE is not configured.");

        string encrypted = PayGetWayDTO.MakeEncrytedDataforDoubleVerification("",
            Convert.ToString(stored["DEPTID"]), deptTranId, securityCode);
        string json = new JavaScriptSerializer().Serialize(new
        {
            EncryptTxt = encrypted,
            REQDEPTID = Convert.ToString(stored["DEPTID"])
        });

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
            "https://finance.jharkhand.gov.in/JEgrasDV/JeGrasRestful.svc/SBIePayDoubleVerification");
        request.Method = "POST";
        request.ContentType = "application/json; charset=utf-8";
        request.Timeout = 30000;
        byte[] body = Encoding.UTF8.GetBytes(json);
        request.ContentLength = body.Length;
        using (Stream stream = request.GetRequestStream()) stream.Write(body, 0, body.Length);

        string responseText;
        using (WebResponse response = request.GetResponse())
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            responseText = reader.ReadToEnd();

        var responseObject = new JavaScriptSerializer().DeserializeObject(responseText) as System.Collections.Generic.Dictionary<string, object>;
        if (responseObject == null || responseObject.Count == 0)
            throw new InvalidOperationException("JEGRAS returned an empty verification response.");
        string encryptedValue = null;
        foreach (object value in responseObject.Values)
        {
            encryptedValue = Convert.ToString(value);
            if (!string.IsNullOrWhiteSpace(encryptedValue)) break;
        }
        if (string.IsNullOrWhiteSpace(encryptedValue))
            throw new InvalidOperationException("Unable to read the encrypted JEGRAS verification response.");

        string decrypted = new clsEncrypt().Decrypt(encryptedValue.Replace("\\", ""), encryptionKey);
        string[] data = decrypted.Split('|');
        int verifiedAppId;
        if (data.Length < 22 || !string.Equals(data[3], deptTranId, StringComparison.Ordinal) ||
            !int.TryParse(data[8], out verifiedAppId) || verifiedAppId != Convert.ToInt32(stored["APP_ID"]) ||
            !string.Equals(data[5], Convert.ToString(stored["APPLICATION_NUMBER"]), StringComparison.OrdinalIgnoreCase) ||
            !string.Equals(data[2], Convert.ToString(stored["APPLICANT_NAME"]), StringComparison.Ordinal) ||
            !string.Equals(data[7], Convert.ToString(stored["OPERATION_FOR"]), StringComparison.Ordinal) ||
            !string.Equals(data[9], "MISC-" + Convert.ToString(stored["AUTHORITY_CODE"]) + "-" + Convert.ToString(stored["REASON_ID"]), StringComparison.Ordinal) ||
            !string.Equals(data[0], Convert.ToString(stored["DEPTID"]), StringComparison.Ordinal) ||
            !string.Equals(data[1], Convert.ToString(stored["RECEIPTHEADCODE"]), StringComparison.Ordinal))
            throw new InvalidOperationException("JEGRAS verification response did not match the requested transaction.");

        decimal gatewayAmount;
        if (!decimal.TryParse(data[4], NumberStyles.Number, CultureInfo.InvariantCulture, out gatewayAmount) ||
            gatewayAmount != Convert.ToDecimal(stored["AMOUNT"]))
            throw new InvalidOperationException("JEGRAS verification amount does not match the stored transaction.");

        decimal parsedAmount;
        string verifiedStatus = (data[12] ?? "").Trim().ToUpperInvariant();
        decimal? txnAmount = verifiedStatus == "FAIL" ? (decimal?)0 :
            (decimal.TryParse(data[18], NumberStyles.Number, CultureInfo.InvariantCulture, out parsedAmount) && parsedAmount > 0
                ? (decimal?)parsedAmount : gatewayAmount);
        DateTime parsedDate;
        bool dateParsed = DateTime.TryParse(data[17], CultureInfo.GetCultureInfo("en-GB"),
            DateTimeStyles.AssumeLocal, out parsedDate);
        if (!dateParsed) dateParsed = DateTime.TryParse(data[17], CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeLocal, out parsedDate);
        DateTime? txnDate = dateParsed ? (DateTime?)parsedDate : null;
        string status = (data[12] ?? "").Trim().ToUpperInvariant();
        if (status == "SUCCESSFUL") status = "SUCCESS";
        if (status == "PENDING") status = "BOOKED";
        if (status != "SUCCESS" && status != "FAIL" && status != "BOOKED")
            throw new InvalidOperationException("Unrecognized JEGRAS status: " + status);

        MiscPaymentService.SaveGatewayResponse(deptTranId, status, data[13], data[16], data[20],
            txnDate, txnAmount, data[19], data[14], data[15], data[0], data[1],
            data[6], data[10], data[11], data[21], "UPDATE_VERIFICATION");
    }
}