using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEra.JHT.Infrastructure;

public partial class PaymentRequest : System.Web.UI.Page
{
    // These fields are kept for future use if needed
    String DEPTID;
    String RECIEPTHEADCODE;
    String DEPOSITERNAME;
    String DEPTTRANID;
    Decimal AMOUNT;
    String DEPOSITERID;
    String PANNO;
    String ADDINFO1;
    String ADDINFO2;
    String ADDINFO3;
    String TREASCODE;
    String IFMSOFFICECODE;
    String STATUS;
    String PAYMENTSTATUSMESSAGE;
    String GRN;
    String CIN;
    String REF_NO;
    String TXN_DATE;
    String TXN_AMOUNT;
    String CHALLAN_URL;
    String PMODE;
    String ADDINFO5;

    public string enc_val { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // 1. Guard: mandatory encrypted query string
                if (string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    ScriptManager.RegisterClientScriptBlock(
                        Page,
                        Page.GetType(),
                        Guid.NewGuid().ToString(),
                        "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx';",
                        true
                    );
                    return;
                }

                // 2. Decrypt and parse finance dept. payload
                string decryptedVal;
                try
                {
                    decryptedVal = PayGetWayResponseDTO.MakeDecryptedData(Request.QueryString["q"].ToString());
                }
                catch (Exception)
                {
                    ScriptManager.RegisterClientScriptBlock(
                        Page,
                        Page.GetType(),
                        Guid.NewGuid().ToString(),
                        "alert('Invalid payment request data.');window.location ='PermitHomePage.aspx';",
                        true
                    );
                    return;
                }

                string[] arr1 = decryptedVal.Split('|');
                if (arr1.Length < 13)
                {
                    ScriptManager.RegisterClientScriptBlock(
                        Page,
                        Page.GetType(),
                        Guid.NewGuid().ToString(),
                        "alert('Invalid payment request format.');window.location ='PermitHomePage.aspx';",
                        true
                    );
                    return;
                }

                // Map decrypted values
                string deptId = arr1[0];
                string receiptHeadCode = arr1[1];
                string depositorName = arr1[2];
                string deptTranId = arr1[3];

                decimal amount;
                if (!decimal.TryParse(arr1[4], out amount))
                {
                    ScriptManager.RegisterClientScriptBlock(
                        Page,
                        Page.GetType(),
                        Guid.NewGuid().ToString(),
                        "alert('Invalid payment amount.');window.location ='PermitHomePage.aspx';",
                        true
                    );
                    return;
                }

                // 🚫 3. HARD STOP: do not proceed with 0 or negative amount
                if (amount <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(
                        Page,
                        Page.GetType(),
                        Guid.NewGuid().ToString(),
                        "alert('Payment amount must be greater than zero.');window.location ='PermitHomePage.aspx';",
                        true
                    );
                    return;
                }

                string depositorId = arr1[5];
                string panno = arr1[6];
                string addInfo1 = arr1[7]; // OPERATION_FOR (e.g. "STA COUNTER SIGNATURE TAX PAY")
                string addInfo2 = arr1[8]; // APP_ID
                string addInfo3 = arr1[9]; // MISC reason marker for miscellaneous payments
                // arr1[9] is generally ADDINFO3 / TYPE if needed
                string treasCode = arr1[10];
                string ifmsOfficeCode = arr1[11];
                string securityCode = arr1[12];

                // Keep in members if you want further use
                DEPTID = deptId;
                RECIEPTHEADCODE = receiptHeadCode;
                DEPOSITERNAME = depositorName;
                DEPTTRANID = deptTranId;
                AMOUNT = amount;
                DEPOSITERID = depositorId;
                PANNO = panno;
                ADDINFO1 = addInfo1;
                ADDINFO2 = addInfo2;
                TREASCODE = treasCode;
                IFMSOFFICECODE = ifmsOfficeCode;

                enc_val = Request.QueryString["q"].ToString(); // if you want to bind it in markup / JS

                // Miscellaneous payments use the same whitelisted gateway request page,
                // but their transaction is validated against MISC_PAYMENT_TRANSACTION.
                if ((addInfo3 ?? "").StartsWith("MISC-", StringComparison.Ordinal))
                {
                    ValidateMiscellaneousRequest(arr1, amount);
                    return;
                }

                // 4. Check previous transaction status from history table
                using (SqlConnection con = new SqlConnection(SQLHelper.GetConnString()))
                using (SqlCommand scmd = new SqlCommand("SP_PAYMENT_TRANSACTIONS_HISTORY_AED", con))
                {
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(addInfo2));
                    scmd.Parameters.AddWithValue("@APPLICATION_NUMBER", depositorId);
                    scmd.Parameters.AddWithValue("@OPERATION_FOR", addInfo1);
                    scmd.Parameters.AddWithValue("@MODE", "CHECK_BEFORE_PAYMENT");

                    DataSet ds1 = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(scmd);
                    da.Fill(ds1);

                    if (ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds1.Tables[0];
                        string trnStatus = Convert.ToString(dt.Rows[0]["TRN_STATUS"]);

                        // 👍 SUCCESS
                        if (trnStatus == "SUCCESS")
                        {
                            // For all operations except CS tax pay, block further payment
                            if (addInfo1 != "STA COUNTER SIGNATURE TAX PAY")
                            {
                                ScriptManager.RegisterClientScriptBlock(
                                    Page,
                                    Page.GetType(),
                                    Guid.NewGuid().ToString(),
                                    "alert('Already Paid');window.location ='PermitHomePage.aspx';",
                                    true
                                );
                                return;
                            }

                            // For STA COUNTER SIGNATURE TAX PAY:
                            // You can decide whether to still allow or redirect to reprint.
                            // Currently allowing to continue to payment if user initiated again.
                        }

                        // ❌ FAILURE
                        if (trnStatus == "FAIL")
                        {
                            // At this point, the DB already has a FAILED entry.
                            // Just inform the user that the previous attempt failed;
                            // allow them to proceed again to payment page.
                            ScriptManager.RegisterClientScriptBlock(
                                Page,
                                Page.GetType(),
                                Guid.NewGuid().ToString(),
                                "alert('Your previous payment attempt failed. You may proceed to pay again.');",
                                true
                            );
                            // No return here – user is allowed to continue.
                        }

                        // ⏳ BOOKED (pending)
                        if (trnStatus == "BOOKED")
                        {
                            double hrsDifference =
                                DateTime.Now.Subtract(Convert.ToDateTime(dt.Rows[0]["TRANSACTION_ON"])).Hours;

                            if (hrsDifference < 2.0)
                            {
                                ScriptManager.RegisterClientScriptBlock(
                                    Page,
                                    Page.GetType(),
                                    Guid.NewGuid().ToString(),
                                    "alert('Please pay after 2 hours.');window.location='PermitHomePage.aspx';",
                                    true
                                );
                                return;
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(
                                    Page,
                                    Page.GetType(),
                                    Guid.NewGuid().ToString(),
                                    "alert('Please check Payment Status.');window.location='ClientPaymentStatus.aspx';",
                                    true
                                );
                                return;
                            }
                        }
                    }
                }

                // If we reach here:
                // - Amount is > 0
                // - Not blocked by SUCCESS/BOOKED rules
                // The page will render, and your Payment.js / form will post to finance
                // using the encrypted requestparam.
            }
        }
        catch (Exception ex)
        {
            string message = ex.Message.ToString();
            message = message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(
                Page,
                Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('" + message + "');window.location='PermitHomePage.aspx';",
                true
            );
            return;
        }
    }
    private void ValidateMiscellaneousRequest(string[] data, decimal amount)
    {
        int appId;
        if (!int.TryParse(data[8], out appId))
            throw new InvalidOperationException("Invalid miscellaneous payment application ID.");

        DataTable transactions = MiscPaymentService.GetTransaction(data[3]);
        if (transactions.Rows.Count != 1)
            throw new InvalidOperationException("Miscellaneous payment transaction was not found.");

        DataRow stored = transactions.Rows[0];
        string expectedMarker = "MISC-" +
            Convert.ToString(stored["AUTHORITY_CODE"]) + "-" +
            Convert.ToString(stored["REASON_ID"]);

        if (!string.Equals(Convert.ToString(stored["TRN_STATUS"]), "INITIATED", StringComparison.Ordinal) ||
            appId != Convert.ToInt32(stored["APP_ID"]) ||
            !string.Equals(data[5], Convert.ToString(stored["APPLICATION_NUMBER"]), StringComparison.Ordinal) ||
            amount != Convert.ToDecimal(stored["AMOUNT"]) ||
            !string.Equals(data[7], Convert.ToString(stored["OPERATION_FOR"]), StringComparison.Ordinal) ||
            !string.Equals(data[2], Convert.ToString(stored["APPLICANT_NAME"]).Replace("&", "and"), StringComparison.Ordinal) ||
            !string.Equals(data[9], expectedMarker, StringComparison.Ordinal) ||
            !string.Equals(data[0], Convert.ToString(stored["CFG_DEPTID"]), StringComparison.Ordinal) ||
            !string.Equals(data[1], Convert.ToString(stored["CFG_RECEIPTHEADCODE"]), StringComparison.Ordinal) ||
            !string.Equals(data[10], Convert.ToString(stored["CFG_TREASCODE"]), StringComparison.Ordinal) ||
            !string.Equals(data[11], Convert.ToString(stored["CFG_IFMSOFFICECODE"]), StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Miscellaneous payment request does not match the stored transaction.");
        }

        MiscPaymentService.MarkRequested(data[3]);
    }
}
