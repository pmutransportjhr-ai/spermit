using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;

public partial class MISC_PAYMENT_REQUEST : Page
{
    protected bool IsValidRequest = false;
    protected string RequestPayload = "";
    protected string ErrorMessage = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        // Prefer the query-string request. Fall back to the session value
        // created by MISCELLANEOUS_PAYMENT.aspx before redirecting to this page.
        string encrypted = Request.QueryString["q"];
        if (string.IsNullOrWhiteSpace(encrypted))
            encrypted = Convert.ToString(Session["MISC_JEGRAS_REQUEST"]);

        if (string.IsNullOrWhiteSpace(encrypted))
        {
            Reject("Missing JEGRAS request. Please return to Miscellaneous Payment and submit again.");
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

            // Identify the exact failed comparison without logging or exposing
            // decrypted payload values, applicant details, or the security code.
            var failedChecks = new System.Collections.Generic.List<string>();

            if (Convert.ToString(row["TRN_STATUS"]) != "INITIATED")
                failedChecks.Add("TRN_STATUS");

            if (Convert.ToString(row["APPLICATION_NUMBER"]) != data[5])
                failedChecks.Add("APPLICATION_NUMBER");

            if (Convert.ToInt32(row["APP_ID"]) != appId)
                failedChecks.Add("APP_ID");

            if (Convert.ToDecimal(row["AMOUNT"]) != amount)
                failedChecks.Add("AMOUNT");

            if (!string.Equals(Convert.ToString(row["OPERATION_FOR"]), data[7], StringComparison.Ordinal))
                failedChecks.Add("OPERATION_FOR");

            if (!string.Equals(
                data[2],
                Convert.ToString(row["APPLICANT_NAME"]).Replace("&", "and"),
                StringComparison.Ordinal))
                failedChecks.Add("APPLICANT_NAME");

            // ADDINFO3 contains the MISC reason marker, not TR_TYPE.
            string expectedReason = "MISC-" +
                Convert.ToString(row["AUTHORITY_CODE"]) + "-" +
                Convert.ToString(row["REASON_ID"]);

            if (!string.Equals(data[9], expectedReason, StringComparison.Ordinal))
                failedChecks.Add("ADDINFO3_REASON_MARKER");

            if (!string.Equals(Convert.ToString(row["CFG_DEPTID"]), data[0], StringComparison.Ordinal))
                failedChecks.Add("DEPTID");

            if (!string.Equals(Convert.ToString(row["CFG_RECEIPTHEADCODE"]), data[1], StringComparison.Ordinal))
                failedChecks.Add("RECEIPTHEADCODE");

            if (!string.Equals(Convert.ToString(row["CFG_TREASCODE"]), data[10], StringComparison.Ordinal))
                failedChecks.Add("TREASCODE");

            if (!string.Equals(Convert.ToString(row["CFG_IFMSOFFICECODE"]), data[11], StringComparison.Ordinal))
                failedChecks.Add("IFMSOFFICECODE");

            if (failedChecks.Count > 0)
            {
                Reject("Payment request mismatch: " + string.Join(", ", failedChecks));
                return;
            }
            MiscPaymentService.MarkRequested(deptTranId);

            // Remove the one-time session fallback after the request has passed
            // transaction validation and has been marked as requested.
            if (string.Equals(
                Convert.ToString(Session["MISC_JEGRAS_DEPTTRANID"]),
                deptTranId,
                StringComparison.Ordinal))
            {
                Session.Remove("MISC_JEGRAS_REQUEST");
                Session.Remove("MISC_JEGRAS_DEPTTRANID");
            }

            RequestPayload = encrypted;
            litRequestInput.Text = "<input type=\"hidden\" name=\"requestparam\" value=\"" + HttpUtility.HtmlAttributeEncode(encrypted) + "\" />";
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