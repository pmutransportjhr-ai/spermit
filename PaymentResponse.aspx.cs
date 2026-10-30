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