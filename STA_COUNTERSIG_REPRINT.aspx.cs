using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Net.Security;

public partial class STA_COUNTERSIG_REPRINT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Net.WebClient client = new System.Net.WebClient();
        ServicePointManager.ServerCertificateValidationCallback = delegate
        {
            return true;
        };
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            clsEncrypt encdept = new clsEncrypt();
            string EncryptResponse = "";

            string GRN1 = "";
            string DEPTID1 = "JHTD";

            string[] handlerResponse = CryptorEngine.Decrypt(lblencyValue.Value, true).Split('~');

            string DEPTTRANID1 = handlerResponse[0];
            string App_id = handlerResponse[1];
            string Application_number = handlerResponse[2];
            string operation_for = handlerResponse[3];
            string tr_type = handlerResponse[4];
            string tbl_status = handlerResponse[5];

            string SECURITYCODE1 = "sec1234";
            string Key = "key1234";

            // C# 5 compatible concatenation
            string requestparameter = GRN1 + "|" + DEPTID1 + "|" + DEPTTRANID1 + "|" + SECURITYCODE1;
            string EncryptTxt = encdept.Encrypt(requestparameter, Key);

            string json = "{\"EncryptTxt\": \"" + EncryptTxt + "\",\"REQDEPTID\": \"" + DEPTID1 + "\"}";
            string verificationUrl = "https://finance.jharkhand.gov.in/JEgrasDV/JeGrasRestful.svc/SBIePayDoubleVerification";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(verificationUrl);
            request.ContentType = @"application/json; charset=utf-8";
            request.Timeout = 30000;
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            WebResponse response = request.GetResponse();
            string result = new StreamReader(response.GetResponseStream()).ReadToEnd();

            EncryptResponse = result.Replace(@"\", "");
            int i = EncryptResponse.IndexOf(":") + 2;
            string actualval = EncryptResponse.Substring(i, EncryptResponse.Length - i - 2);

            string DecryptedVal = encdept.Decrypt(actualval, Key);
            string[] arr = DecryptedVal.Split('|');

            string DEPTID = arr.Length > 0 ? arr[0] : "JHTD";
            string RECIEPTHEADCODE = arr.Length > 1 ? arr[1] : "";
            string DEPOSITERNAME = arr.Length > 2 ? arr[2] : "";
            string DEPTTRANID = arr.Length > 3 ? arr[3] : DEPTTRANID1;

            decimal AMOUNT = 0;
            if (arr.Length > 4 && !string.IsNullOrWhiteSpace(arr[4]))
            {
                decimal.TryParse(arr[4], out AMOUNT);
            }

            string DEPOSITERID = (arr.Length > 5 && !string.IsNullOrWhiteSpace(arr[5]) && arr[5] != "NA") ? arr[5] : Application_number;
            string PANNO = arr.Length > 6 ? arr[6] : "";
            string ADDINFO1 = (arr.Length > 7 && !string.IsNullOrWhiteSpace(arr[7]) && arr[7] != "NA") ? arr[7] : operation_for;
            string ADDINFO2 = (arr.Length > 8 && !string.IsNullOrWhiteSpace(arr[8]) && arr[8] != "NA") ? arr[8] : App_id;
            string ADDINFO3 = (arr.Length > 9 && !string.IsNullOrWhiteSpace(arr[9]) && arr[9] != "NA") ? arr[9] : tr_type;
            string TREASCODE = arr.Length > 10 ? arr[10] : "";
            string IFMSOFFICECODE = arr.Length > 11 ? arr[11] : "";
            string STATUS = arr.Length > 12 ? arr[12] : "";
            string statusUpper = (STATUS ?? "").Trim().ToUpper();

            string PAYMENTSTATUSMESSAGE = arr.Length > 13 ? arr[13] : "";
            string GRN = arr.Length > 14 ? arr[14] : "";
            string CIN = arr.Length > 15 ? arr[15] : "";
            string REF_NO = arr.Length > 16 ? arr[16] : "";
            string TXN_DATE = arr.Length > 17 ? arr[17] : "";
            string TXN_AMOUNT = arr.Length > 18 ? arr[18] : "";
            if ((string.IsNullOrWhiteSpace(TXN_AMOUNT) || TXN_AMOUNT == "0" || TXN_AMOUNT == "0.00") && AMOUNT > 0)
            {
                TXN_AMOUNT = AMOUNT.ToString("0.00");
            }

            string CHALLAN_URL = arr.Length > 19 ? arr[19] : "";
            string PMODE = arr.Length > 20 ? arr[20] : "";
            string ADDINFO5 = arr.Length > 21 ? arr[21] : "";

            bool isFailureMessage =
                   PAYMENTSTATUSMESSAGE.ToUpper().Contains("INVALID")
                || PAYMENTSTATUSMESSAGE.ToUpper().Contains("SORRY")
                || PAYMENTSTATUSMESSAGE.ToUpper().Contains("GRN")
                || PAYMENTSTATUSMESSAGE.ToUpper().Contains("NOT");

            // ----------------------------------------------
            // STATUS CHECK
            // ----------------------------------------------
            if (statusUpper == "SUCCESS" || statusUpper == "SUCCESSFUL")
            {
                STATUS = "SUCCESS";
            }
            else if (statusUpper == "FAIL")
            {
                STATUS = "FAIL";
                TXN_AMOUNT = "0";
                TXN_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                ADDINFO2 = App_id;
                DEPOSITERID = Application_number;
                ADDINFO1 = operation_for;
                ADDINFO3 = tr_type;

                PaymentTransaction.AddTransactionDetails(
                    ADDINFO2, DEPOSITERID, DEPOSITERNAME, 0, ADDINFO1,
                    STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "",
                    TXN_DATE, TXN_AMOUNT, CHALLAN_URL, GRN, CIN,
                    DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO,
                    ADDINFO3, TREASCODE, IFMSOFFICECODE,
                    ADDINFO5, "", "", tbl_status);

                ScriptManager.RegisterClientScriptBlock(
                    (sender as Control), this.GetType(), Guid.NewGuid().ToString(),
                    "$('#loadingmessage').modal('hide');alert('Payment Failed. Please try again.');window.location='PermitHomepage.aspx';",
                    true);
                return;
            }
            else if (statusUpper == "BOOKED" && isFailureMessage)
            {
                STATUS = "BOOKED";
                TXN_AMOUNT = "0";
                TXN_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                ADDINFO2 = App_id;
                DEPOSITERID = Application_number;
                ADDINFO1 = operation_for;
                ADDINFO3 = tr_type;

                PaymentTransaction.AddTransactionDetails(
                    ADDINFO2, DEPOSITERID, DEPOSITERNAME, 0, ADDINFO1,
                    STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "",
                    TXN_DATE, TXN_AMOUNT, CHALLAN_URL, GRN, CIN,
                    DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO,
                    ADDINFO3, TREASCODE, IFMSOFFICECODE,
                    ADDINFO5, "", "", tbl_status);

                ScriptManager.RegisterClientScriptBlock(
                    (sender as Control), this.GetType(), Guid.NewGuid().ToString(),
                    "$('#loadingmessage').modal('hide');alert('Your payment is BOOKED but not confirmed. Please recheck after some time.');window.location='PermitHomepage.aspx';",
                    true);
                return;
            }

            // ----------------------------------------------
            // SUCCESS CASE (original logic)
            // ----------------------------------------------
            string MSG;
            string[] aa = new string[3];

            if (ADDINFO1 == "STA COUNTER SIGNATURE TAX PAY" || (ADDINFO3 != null && ADDINFO3.StartsWith("STA-APPLICATION_") && ADDINFO3.Split('_').Length >= 3))
            {
                aa = ADDINFO3.Split('_');
                MSG = PaymentTransaction.AddTransactionDetails(
                    ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT,
                    ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE,
                    "", TXN_DATE, TXN_AMOUNT, CHALLAN_URL, GRN, CIN,
                    DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, aa[0],
                    TREASCODE, IFMSOFFICECODE, ADDINFO5, aa[1], aa[2], tbl_status);
            }
            else
            {
                MSG = PaymentTransaction.AddTransactionDetails(
                    ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT,
                    ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE,
                    "", TXN_DATE, TXN_AMOUNT, CHALLAN_URL, GRN, CIN,
                    DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, ADDINFO3,
                    TREASCODE, IFMSOFFICECODE, ADDINFO5, "", "", tbl_status);
            }

            if (MSG == "Error")
            {
                ScriptManager.RegisterClientScriptBlock(
                    (sender as Control), this.GetType(), Guid.NewGuid().ToString(),
                    "$('#loadingmessage').modal('hide');alert('Oops! Something went wrong.');window.location='PermitHomepage.aspx';", true);
                return;
            }
            else if (MSG == "BOOKED" || MSG == "FAIL")
            {
                string disp = MSG == "FAIL" ? "FAILED" : "BOOKED";
                ScriptManager.RegisterClientScriptBlock(
                    (sender as Control), this.GetType(), Guid.NewGuid().ToString(),
                    "$('#loadingmessage').modal('hide');alert('Your payment status is : " + disp + ".');window.location='PermitHomepage.aspx';", true);
                return;
            }
            else if (MSG.Contains(".aspx"))

            {
                if (ADDINFO1 == "STA COUNTER SIGNATURE TAX PAY")
                {
                    aa = ADDINFO3.Split('_');
                    string targetUrl = MSG.Split('~')[0];
                    string tblType = MSG.Split('~').Length > 1 ? MSG.Split('~')[1] : (aa.Length > 2 ? aa[2] : "");
                    ScriptManager.RegisterClientScriptBlock(
                        (sender as Control), this.GetType(), Guid.NewGuid().ToString(),
                        "$('#loadingmessage').modal('hide');alert('Payment Successful');window.location='" +
                        targetUrl + "?q=" +
                        HttpUtility.UrlEncode(
                            CryptorEngine.Encrypt(
                                "APP_ID=" + ADDINFO2 + "&GRN=" + Convert.ToString(GRN) + "&TBL=" + tblType +
                                "&TYPE=" + ADDINFO3 + "&APPLICATION_SERVICE=" + ADDINFO1,
                                true),
                            Response.ContentEncoding) + "';", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(
                        (sender as Control), this.GetType(), Guid.NewGuid().ToString(),
                        "$('#loadingmessage').modal('hide');alert('Payment Successful');window.location='" +
                        MSG + "?q=" +
                        HttpUtility.UrlEncode(
                            CryptorEngine.Encrypt(
                                "APP_ID=" + ADDINFO2 +
                                "&TYPE=" + ADDINFO3 +
                                "&APPLICATION_SERVICE=" + ADDINFO1,
                                true),
                            Response.ContentEncoding) + "';", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            string Message = ex.Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(
                (sender as Control), this.GetType(), Guid.NewGuid().ToString(),
                "$('#loadingmessage').modal('hide');alert('Error: " + Message + "');window.location='PermitHomepage.aspx';", true);
        }
    }


}