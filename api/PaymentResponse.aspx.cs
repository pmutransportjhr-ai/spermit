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
            //if (string.IsNullOrEmpty(Request.Form["responseparam"]))
            //{
            //    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
            //    return;
            //}

           String queryString = Convert.ToString(Context.Request.Form["responseparam"]);
         // String queryString = "nYu/ZxO3/oYTHEkGv9qfyQfxlnIWSGioOIcSESkDnFayJ4M0Yv0tgpCGFatg74Whe+El1UrTCcNOzwsRLBmcuw5i7DAuyRq27uP8hydOn2kEqx5Y02cqY4TETaaXBd5s+pSN3EEI5UMGaHIN60dqtLJaITT7K5A4P2tGpVW4v3GutJY+EY3kvQ8mO2+evuNc4WfoZKD5QmjJcjT6spQgqcSKpRoHu+8RfVigxx6UYwQ+hW03R3YM6fvsT1A8koJXFKrS4hoFJYAJIEBuUuxnDNCCuzokYtRNgadHFK9aJkO99DpoyWFSyLSf3N2hQ5iyoWbmt6w5nua3mbfwn/RHWKEKVpelp8Jkkwv1G1RgvqjU3dGtob7iUnPblVSPa+1NKaXbJtyBv4rg5Y3+UJQpIr7WBHddUmb1/RXLCtbuncc=";
            String DecryptedVal = PayGetWayResponseDTO.MakeDecryptedData(queryString.ToString());
            String[] arr;
            arr = DecryptedVal.Split('|');
            //for(int i=0;i<arr.Length;i++)
            //{
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
            string statusUpper = (STATUS ?? "").Trim().ToUpper();
            if (statusUpper == "SUCCESS" || statusUpper == "SUCCESSFUL")
            {
                STATUS = "SUCCESS";
                if (ADDINFO1 == "STA COUNTER SIGNATURE TAX PAY" || (ADDINFO3 != null && ADDINFO3.StartsWith("STA-APPLICATION_") && ADDINFO3.Split('_').Length >= 3))
                {
                     aa = ADDINFO3.Split('_');
                     MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", TXN_DATE, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, aa[0], TREASCODE, IFMSOFFICECODE, ADDINFO5,aa[1],aa[2]);
                    if(MSG=="Error")
                    {
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('!Opps something happens wrong on Payment Process');window.location ='PermitHomepage.aspx'", true);
                        return;
                    }
                }
                else
                {
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", TXN_DATE, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, ADDINFO3, TREASCODE, IFMSOFFICECODE, ADDINFO5,"","");
                    if (MSG == "Error")
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
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", TXN_DATE, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, aa[0], TREASCODE, IFMSOFFICECODE, ADDINFO5, aa[1], aa[2]);
                }
                else
                {
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", TXN_DATE, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, ADDINFO3, TREASCODE, IFMSOFFICECODE, ADDINFO5, "", "");
                }

                if (MSG == "Error")
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
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", TXN_DATE, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, aa[0], TREASCODE, IFMSOFFICECODE, ADDINFO5, aa[1], aa[2]);
                }
                else
                {
                    MSG = PaymentTransaction.AddTransactionDetails(ADDINFO2, DEPOSITERID, DEPOSITERNAME, AMOUNT, ADDINFO1, STATUS, PAYMENTSTATUSMESSAGE, REF_NO, PMODE, "", TXN_DATE, TXN_AMOUNT, CHALLAN_URL, GRN, CIN, DEPTID, RECIEPTHEADCODE, DEPTTRANID, PANNO, ADDINFO3, TREASCODE, IFMSOFFICECODE, ADDINFO5, "", "");
                }

                if (MSG == "Error")
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
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('The tax has been successfully paid');window.location ='" + url + "?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(ADDINFO2) + "&GRN=" + Convert.ToString(GRN) + "&TBL=" + tblType + "&TYPE=" + Convert.ToString(aa[0] + "_" + aa[2]) + "&APPLICATION_SERVICE=" + Convert.ToString(ADDINFO1), true), Response.ContentEncoding) + "'", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('The tax has been successfully paid');window.location ='" + MSG + "?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(ADDINFO2) + "&TYPE=" + Convert.ToString(arr[9]) + "&APPLICATION_SERVICE=" + Convert.ToString(ADDINFO1), true), Response.ContentEncoding) + "'", true);
                }
            }
            
        }
    }

   
}