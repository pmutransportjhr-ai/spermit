using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;

public partial class PaymentResponse : System.Web.UI.Page
{
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


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String queryString = Convert.ToString(Context.Request.Form["responseparam"]);
            String DecryptedVal = PayGetWayResponseDTO.MakeDecryptedData(queryString.ToString());
            String[] arr;
            arr = DecryptedVal.Split('|');
            if (arr.Length < 22)
            {
                ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(),
                    "alert('Invalid payment response format.');window.location='PermitHomePage.aspx';", true);
                return;
            }
            DEPTID = arr[0];
            RECIEPTHEADCODE = arr[1];
            DEPOSITERNAME = arr[2];
            DEPTTRANID = arr[3]; 
            
            AMOUNT = Convert.ToDecimal(arr[4]);
            DEPOSITERID = arr[5];
            
            PANNO=arr[6];
            ADDINFO1 = arr[7];
          
            ADDINFO2 = arr[8];
            
            ADDINFO3 = arr[9];
            TREASCODE = arr[10];
            IFMSOFFICECODE=arr[11];
            STATUS=arr[12];
            PAYMENTSTATUSMESSAGE=arr[13];
            GRN=arr[14];
            CIN=arr[15];
            REF_NO=arr[16];
            TXN_DATE = arr[17];
            TXN_AMOUNT=arr[18];
            CHALLAN_URL=arr[19];
            PMODE=arr[20];
            ADDINFO5=arr[21];

            // Miscellaneous payments share this whitelisted response endpoint.
            if ((ADDINFO3 ?? "").StartsWith("MISC-", StringComparison.Ordinal))
            {
                HandleMiscellaneousResponse(arr);
                return;
            }
            String[] aa = new string[3];
            String MSG = "";

            string dt = TXN_DATE.Split(new Char[] { ' ' })[0].Split(new Char[] { '/' })[2] + '-' + TXN_DATE.Split(new Char[] { ' ' })[0].Split(new Char[] { '/' })[1] + '-' + TXN_DATE.Split(new Char[] { ' ' })[0].Split(new Char[] { '/' })[0];
            // start
            TXN_DATE=Convert.ToDateTime(TXN_DATE).ToString("yyyy-MM-dd");

            //txtEnc.Text = queryString;
            //txtDec.Text = DecryptedVal;

            //int i = 0;
            //string s = "";
            //foreach (String str in arr)
            //{
            //    s += "Value at " + i + "=" + str + "<br/>";
            //}
            //lbl.Text = s + "Date===" + dt;
			
			//end

            string statusUpper = (STATUS ?? "").Trim().ToUpper();
            if (statusUpper == "SUCCESS" || statusUpper == "SUCCESSFUL")
            {
                STATUS = "SUCCESS";
                if (ADDINFO1 == "STA COUNTER SIGNATURE TAX PAY" || (ADDINFO3 != null && ADDINFO3.StartsWith("STA-APPLICATION_") && ADDINFO3.Split('_').Length >= 3))
                {
                    aa = ADDINFO3.Split('_');
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", dt, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, aa[0], TREASCODE, IFMSOFFICECODE, ADDINFO5, aa[1], aa[2]);
                    if (MSG.StartsWith("Error"))
                    {
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('!Opps something happens wrong on Payment Process');window.location ='PermitHomepage.aspx'", true);
                        return;
                    }
                }
                else
                {
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", dt, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, ADDINFO3, TREASCODE, IFMSOFFICECODE, ADDINFO5, "", "");
                    if (MSG.StartsWith("Error"))
                    {
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('!Opps something happens wrong on Payment Process');window.location ='PermitHomepage.aspx'", true);
                        return;
                    }
                }
            }

            if (statusUpper == "FAIL")
            {
                STATUS = "FAIL";
                if (ADDINFO1 == "STA COUNTER SIGNATURE TAX PAY" || (ADDINFO3 != null && ADDINFO3.StartsWith("STA-APPLICATION_") && ADDINFO3.Split('_').Length >= 3))
                {
                    aa = ADDINFO3.Split('_');
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", dt, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, aa[0], TREASCODE, IFMSOFFICECODE, ADDINFO5, aa[1], aa[2]);
                }
                else
                {
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", dt, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, ADDINFO3, TREASCODE, IFMSOFFICECODE, ADDINFO5, "", "");
                }

                if (MSG.StartsWith("Error"))
                {
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('!Opps something happens wrong on Payment Process');window.location ='PermitHomepage.aspx'", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('The Transaction has been Failed, Try Again after 2 hrs.');window.location ='PermitHomepage.aspx'", true);
                }
            }
            if (statusUpper == "BOOKED")
            {
                STATUS = "BOOKED";
                if (ADDINFO1 == "STA COUNTER SIGNATURE TAX PAY" || (ADDINFO3 != null && ADDINFO3.StartsWith("STA-APPLICATION_") && ADDINFO3.Split('_').Length >= 3))
                {
                    aa = ADDINFO3.Split('_');
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", dt, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, aa[0], TREASCODE, IFMSOFFICECODE, ADDINFO5, aa[1], aa[2]);
                }
                else
                {
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", dt, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, ADDINFO3, TREASCODE, IFMSOFFICECODE, ADDINFO5, "", "");
                }

                if (MSG.StartsWith("Error"))
                {
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('!Opps something happens wrong on Payment Process');window.location ='PermitHomepage.aspx'", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('The Transaction is under process...Plese check after 2 hrs.');window.location ='PermitHomepage.aspx'", true);
                }
            }
            if (STATUS == "SUCCESS")
            {
                if (ADDINFO1 == "STA COUNTER SIGNATURE TAX PAY")
                {
                   aa = ADDINFO3.Split('_');
                    string url = MSG.Split('~')[0];
                    string tblType = MSG.Split('~').Length > 1 ? MSG.Split('~')[1] : (aa.Length > 2 ? aa[2] : "");
                    string Responsefor ="STA COUNTER SIGNATURE TAX PAY"; 
                    string encryptString = url + "?q=" +
                        HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(ADDINFO2) +
                        "&GRN=" + Convert.ToString(GRN) + "&TYPE=" + ADDINFO3 + "&APPLICATION_SERVICE=" + Convert.ToString(ADDINFO1) + "&TBL="+tblType, true));
                    string details = "APP_ID=" + Convert.ToString(ADDINFO2) + "&GRN=" + Convert.ToString(GRN) + "&TYPE=" + ADDINFO3 + "&APPLICATION_SERVICE=" + Convert.ToString(ADDINFO1) + "&TBL=" + tblType;
                    savetoDatabase(encryptString, Responsefor, details);
                    
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(),
                        "alert('The tax has been successfully paid');window.location ='" + url + "?q=" +
                        HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(ADDINFO2) +
                        "&GRN=" + Convert.ToString(GRN) + "&TYPE=" + ADDINFO3 + "&APPLICATION_SERVICE=" + Convert.ToString(ADDINFO1) + "&TBL="+tblType, true), Response.ContentEncoding) + "'", true);
                }
                else
                {


                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('The tax has been successfully paid');window.location ='" + MSG + "?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(ADDINFO2) + "&TYPE=" + Convert.ToString(arr[9]) + "&APPLICATION_SERVICE=" + Convert.ToString(ADDINFO1), true), Response.ContentEncoding) + "'", true);

                }

            }
            
        }
    }

    private void HandleMiscellaneousResponse(string[] data)
    {
        try
        {
            if (data.Length < 22)
                throw new InvalidOperationException("Incomplete gateway response.");

            DataTable transactions = MiscPaymentService.GetTransaction(data[3]);
            if (transactions.Rows.Count != 1)
                throw new InvalidOperationException("Miscellaneous payment transaction was not found.");

            DataRow stored = transactions.Rows[0];
            decimal gatewayAmount;
            int gatewayAppId;
            string expectedMarker = "MISC-" +
                Convert.ToString(stored["AUTHORITY_CODE"]) + "-" +
                Convert.ToString(stored["REASON_ID"]);

            if (!decimal.TryParse(data[4], System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture, out gatewayAmount) ||
                !int.TryParse(data[8], out gatewayAppId) ||
                gatewayAmount != Convert.ToDecimal(stored["AMOUNT"]) ||
                gatewayAppId != Convert.ToInt32(stored["APP_ID"]) ||
                !string.Equals(data[5], Convert.ToString(stored["APPLICATION_NUMBER"]), StringComparison.OrdinalIgnoreCase) ||
                !string.Equals(data[7], Convert.ToString(stored["OPERATION_FOR"]), StringComparison.Ordinal) ||
                !string.Equals(data[9], expectedMarker, StringComparison.Ordinal) ||
                !string.Equals(data[0], Convert.ToString(stored["CFG_DEPTID"]), StringComparison.Ordinal) ||
                !string.Equals(data[1], Convert.ToString(stored["CFG_RECEIPTHEADCODE"]), StringComparison.Ordinal) ||
                !string.Equals(data[2], Convert.ToString(stored["APPLICANT_NAME"]).Replace("&", "and"), StringComparison.Ordinal) ||
                !string.Equals(data[10], Convert.ToString(stored["CFG_TREASCODE"]), StringComparison.Ordinal) ||
                !string.Equals(data[11], Convert.ToString(stored["CFG_IFMSOFFICECODE"]), StringComparison.Ordinal))
                throw new InvalidOperationException("Gateway response did not match the initiated miscellaneous payment.");

            string status = (data[12] ?? "").Trim().ToUpperInvariant();
            if (status == "SUCCESSFUL") status = "SUCCESS";
            if (status == "PENDING") status = "BOOKED";
            if (status != "SUCCESS" && status != "FAIL" && status != "BOOKED")
                throw new InvalidOperationException("Unrecognized gateway transaction status.");

            DateTime parsedDate;
            bool dateParsed = DateTime.TryParse(data[17],
                System.Globalization.CultureInfo.GetCultureInfo("en-GB"),
                System.Globalization.DateTimeStyles.AssumeLocal, out parsedDate);
            if (!dateParsed)
                dateParsed = DateTime.TryParse(data[17],
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AssumeLocal, out parsedDate);
            DateTime? txnDate = dateParsed ? (DateTime?)parsedDate : null;

            decimal parsedTxnAmount;
            decimal? txnAmount = status == "FAIL" ? (decimal?)0 :
                (decimal.TryParse(data[18], System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture, out parsedTxnAmount) && parsedTxnAmount > 0
                    ? (decimal?)parsedTxnAmount : gatewayAmount);

            MiscPaymentService.SaveGatewayResponse(data[3], status, data[13], data[16], data[20],
                txnDate, txnAmount, data[19], data[14], data[15], data[0], data[1],
                data[6], data[10], data[11], data[21]);

            string message = status == "SUCCESS"
                ? "Miscellaneous payment completed successfully."
                : status == "FAIL"
                    ? "Miscellaneous payment failed. Please check payment status before retrying."
                    : "Miscellaneous payment is booked or pending confirmation. Please check payment status later.";

            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(),
                "alert('" + message + "');window.location='MISC_PAYMENT_STATUS.aspx';", true);
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), Guid.NewGuid().ToString(),
                "alert('Unable to verify or update the miscellaneous payment response. Please check payment status.');window.location='MISC_PAYMENT_STATUS.aspx';", true);
        }
    }

 private void savetoDatabase(string encryptString, string Responsefor, string details)
    {
        string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand();
        cmd.Parameters.AddWithValue("@RESPONSE_FOR", Responsefor.Trim());
        cmd.Parameters.AddWithValue("@RESPONSE_URL", encryptString.Trim());
        cmd.Parameters.AddWithValue("@RESPONSE_DETAIL", details.Trim());
        cmd.Parameters.AddWithValue("@MODE", "INSERT");
        cmd.Connection = con;
        cmd.CommandText = "SP_TBL_PAYMENT_RESPONSE";
        cmd.CommandType = CommandType.StoredProcedure;
        using (con)
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }


    }      
}