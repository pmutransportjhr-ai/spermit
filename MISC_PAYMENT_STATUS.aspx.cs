using System;
using System.Data;
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
    protected void Page_Load(object sender, EventArgs e) { }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string applicationNumber = txtApplicationNumber.Text.Trim();
        if (string.IsNullOrWhiteSpace(applicationNumber))
        {
            lblMessage.Text = "Enter an application number.";
            lblMessage.Visible = true;
            gvTransactions.DataSource = null;
            gvTransactions.DataBind();
            return;
        }
        BindTransactions(applicationNumber);
    }

    protected void gvTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "VERIFY") return;
        try
        {
            VerifyTransaction(Convert.ToString(e.CommandArgument));
            BindTransactions(txtApplicationNumber.Text.Trim());
            lblMessage.Text = "JEGRAS verification completed. Review the latest transaction status below.";
            lblMessage.Visible = true;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Verification could not be completed: " + Server.HtmlEncode(ex.Message);
            lblMessage.Visible = true;
        }
    }

    private void BindTransactions(string applicationNumber)
    {
        DataTable dt = MiscPaymentService.GetTransactionsByApplication(applicationNumber);
        gvTransactions.DataSource = dt;
        gvTransactions.DataBind();
        lblMessage.Visible = dt.Rows.Count == 0;
        if (dt.Rows.Count == 0) lblMessage.Text = "No miscellaneous payment transactions found.";
    }

    private void VerifyTransaction(string deptTranId)
    {
        DataTable tx = MiscPaymentService.GetTransaction(deptTranId);
        if (tx.Rows.Count != 1) throw new InvalidOperationException("Transaction not found.");
        DataRow stored = tx.Rows[0];
        string currentStatus = Convert.ToString(stored["TRN_STATUS"]);
        if (currentStatus == "SUCCESS") return;
        if (currentStatus != "BOOKED" && currentStatus != "FAIL" && currentStatus != "INITIATED")
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
        if (data.Length < 22 || !string.Equals(data[3], deptTranId, StringComparison.Ordinal))
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