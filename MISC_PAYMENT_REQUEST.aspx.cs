using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using SoftEra.JHT.Infrastructure;

public partial class MISC_PAYMENT_REQUEST : Page
{
    protected bool IsValidRequest = false;
    protected string RequestPayload = "";
    protected string ErrorMessage = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        string encrypted = Request.QueryString["q"];
        if (string.IsNullOrWhiteSpace(encrypted))
        {
            Reject("Missing JEGRAS request.");
            return;
        }

        try
        {
            string[] data = PayGetWayResponseDTO.MakeDecryptedData(encrypted).Split('|');
            if (data.Length < 13) { Reject("Invalid JEGRAS request format."); return; }

            string deptTranId = data[3];
            decimal amount;
            int appId;
            if (!decimal.TryParse(data[4], NumberStyles.Number, CultureInfo.InvariantCulture, out amount) ||
                amount <= 0 || !int.TryParse(data[8], out appId))
            { Reject("Invalid payment amount or application reference."); return; }

            DataTable tx = MiscPaymentService.GetTransaction(deptTranId);
            if (tx.Rows.Count != 1) { Reject("Payment transaction was not found."); return; }
            DataRow row = tx.Rows[0];

            bool valid = Convert.ToString(row["TRN_STATUS"]) == "INITIATED" &&
                Convert.ToString(row["APPLICATION_NUMBER"]) == data[5] &&
                Convert.ToInt32(row["APP_ID"]) == appId &&
                Convert.ToDecimal(row["AMOUNT"]) == amount &&
                string.Equals(Convert.ToString(row["OPERATION_FOR"]), data[7], StringComparison.Ordinal) &&
                string.Equals(data[2], Convert.ToString(row["APPLICANT_NAME"]).Replace("&", "and"), StringComparison.Ordinal) &&
                string.Equals(data[9], Convert.ToString(row["TR_TYPE"]), StringComparison.Ordinal) &&
                string.Equals(data[9], "MISC-" + Convert.ToString(row["AUTHORITY_CODE"]) + "-" + Convert.ToString(row["REASON_ID"]), StringComparison.Ordinal) &&
                Convert.ToString(row["CFG_DEPTID"]) == data[0] &&
                Convert.ToString(row["CFG_RECEIPTHEADCODE"]) == data[1] &&
                Convert.ToString(row["CFG_TREASCODE"]) == data[10] &&
                Convert.ToString(row["CFG_IFMSOFFICECODE"]) == data[11];

            if (!valid) { Reject("Payment request does not match the stored transaction."); return; }
            MiscPaymentService.MarkRequested(deptTranId);
            RequestPayload = encrypted;
            requestPayload.Value = encrypted;
            pnlValidRequest.Visible = true;
            pnlInvalidRequest.Visible = false;
            IsValidRequest = true;
        }
        catch
        {
            Reject("Unable to validate the JEGRAS payment request.");
        }
    }

    private void Reject(string message)
    {
        IsValidRequest = false;
        ErrorMessage = message;
        pnlValidRequest.Visible = false;
        pnlInvalidRequest.Visible = true;
        litErrorMessage.Text = HttpUtility.HtmlEncode(message);
    }
}