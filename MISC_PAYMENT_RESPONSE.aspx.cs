using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using SoftEra.JHT.Infrastructure;

public partial class MISC_PAYMENT_RESPONSE : Page
{
    protected string ResultMessage = "Processing payment response...";
    protected string ReceiptNumber = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        try
        {
            string encryptedResponse = Request.Form["responseparam"];
            if (string.IsNullOrWhiteSpace(encryptedResponse))
                throw new InvalidOperationException("The gateway did not return a response payload.");

            string[] data = PayGetWayResponseDTO.MakeDecryptedData(encryptedResponse).Split('|');
            if (data.Length < 22)
                throw new InvalidOperationException("The gateway response format is incomplete.");

            string deptTranId = data[3];
            DataTable tx = MiscPaymentService.GetTransaction(deptTranId);
            if (tx.Rows.Count != 1)
                throw new InvalidOperationException("The miscellaneous payment transaction was not found.");

            DataRow stored = tx.Rows[0];
            decimal gatewayAmount;
            int gatewayAppId;
            if (!decimal.TryParse(data[4], NumberStyles.Number, CultureInfo.InvariantCulture, out gatewayAmount) ||
                !int.TryParse(data[8], out gatewayAppId))
                throw new InvalidOperationException("The gateway response contains an invalid amount or application ID.");

            if (gatewayAmount != Convert.ToDecimal(stored["AMOUNT"]) ||
                gatewayAppId != Convert.ToInt32(stored["APP_ID"]) ||
                !string.Equals(data[5], Convert.ToString(stored["APPLICATION_NUMBER"]), StringComparison.OrdinalIgnoreCase) ||
                !string.Equals(data[7], "MISCELLANEOUS PAYMENT", StringComparison.Ordinal) ||
                !string.Equals(data[9], "MISC-" + Convert.ToString(stored["AUTHORITY_CODE"]) + "-" + Convert.ToString(stored["REASON_ID"]), StringComparison.Ordinal) ||
                !string.Equals(data[0], Convert.ToString(stored["DEPTID"]), StringComparison.Ordinal) ||
                !string.Equals(data[1], Convert.ToString(stored["RECEIPTHEADCODE"]), StringComparison.Ordinal))
                throw new InvalidOperationException("Gateway response did not match the initiated transaction.");

            string status = (data[12] ?? "").Trim().ToUpperInvariant();
            if (status == "SUCCESSFUL") status = "SUCCESS";
            if (status == "PENDING") status = "BOOKED";
            if (status != "SUCCESS" && status != "FAIL" && status != "BOOKED")
                throw new InvalidOperationException("Unrecognized gateway transaction status: " + status);

            DateTime parsedDate;
            bool dateParsed = DateTime.TryParse(data[17], CultureInfo.GetCultureInfo("en-GB"),
                DateTimeStyles.AssumeLocal, out parsedDate);
            if (!dateParsed) dateParsed = DateTime.TryParse(data[17], CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeLocal, out parsedDate);
            DateTime? txnDate = dateParsed ? (DateTime?)parsedDate : null;
            decimal parsedTxnAmount;
            decimal? txnAmount = status == "FAIL" ? (decimal?)0 :
                (decimal.TryParse(data[18], NumberStyles.Number, CultureInfo.InvariantCulture, out parsedTxnAmount) && parsedTxnAmount > 0
                    ? (decimal?)parsedTxnAmount : gatewayAmount);

            MiscPaymentService.SaveGatewayResponse(deptTranId, status, data[13], data[16], data[20],
                txnDate, txnAmount, data[19], data[14], data[15], data[0], data[1],
                data[6], data[10], data[11], data[21]);

            ReceiptNumber = data[14];
            ResultMessage = status == "SUCCESS" ? "Payment response received successfully. Keep your GRN for reference." :
                status == "FAIL" ? "Payment failed. You may check the transaction status before retrying." :
                "Payment is booked or pending confirmation. Please use Check Miscellaneous Payment Status before retrying.";
        }
        catch (Exception ex)
        {
            ResultMessage = "We could not complete the payment response update: " + ex.Message;
        }
    }
}