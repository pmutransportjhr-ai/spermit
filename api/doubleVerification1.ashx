<%@ WebHandler Language="C#" Class="Vehicle" %>

using System;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Web.Script.Serialization;
using Newtonsoft.Json;


public class Vehicle : IHttpHandler {

    protected List<QueryString> QueryStr = new List<QueryString>();
    public void ProcessRequest(HttpContext context)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        context.Response.ContentType = "text/plain";
        
        switch (context.Request.QueryString["caller"])
        {
            case "GetPaymentStatus":
           
                    String queryString = Convert.ToString(context.Request.QueryString["q"]);
                    string applicationNumber = queryString.Trim();
                    string strResult = string.Empty;
                    using (SqlConnection sCon = new SqlConnection(sConnectionString))
                    {
                        
                        try
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "SP_PRE_PAYMENT_DETAIL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", applicationNumber.Trim().ToUpper());
                            sCmd.Parameters.AddWithValue("@MODE", "APPLICATION_SEARCH_PAYMENT_TRANS");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];

                            if (Convert.ToString(dt.Rows[0]["MSG"]) != "Application not found")
                            {
                                if (Convert.ToString(dt.Rows[0]["MSG"]) == "Payment Submitted Successfully.")
                                {
                                    string TxAmount = Convert.ToString(dt.Rows[0]["TXN_AMOUNT"]);
                                    string TxRefN = Convert.ToString(dt.Rows[0]["TRN_REF_NO"]);
                                    strResult = "Success~Submitted~" + TxAmount + "~" + TxRefN;
                                }
                                else if (Convert.ToString(dt.Rows[0]["MSG"]) == "Please Check Payment Detail after 2 hours.")
                                {
                                    strResult = "Success~2 Hrs.";
                                }
                                else if (Convert.ToString(dt.Rows[0]["MSG"]) == "Payment Failed Please Try Again Later.")
                                {
                                    strResult = "Success~Failed";

                                }

                                else if (Convert.ToString(dt.Rows[0]["MSG"]) == "BOOKED")
                                {
                                    string DEPTTRANDID = Convert.ToString(dt.Rows[0]["DEPTTRANID"]);
                                    string App_id = Convert.ToString(dt.Rows[0]["APP_ID"]);
                                    string Application_number = Convert.ToString(dt.Rows[0]["APPLICATION_NUMBER"]);
                                    string operation_for = Convert.ToString(dt.Rows[0]["OPERATION_FOR"]);
                                    string tr_type = Convert.ToString(dt.Rows[0]["TR_TYPE"]);
                                    strResult = DEPTTRANDID.Trim();
                                    strResult += "~" + App_id + "~" + Application_number+ "~"+ operation_for+"~"+tr_type;
                                    strResult = "Success~encry~"+CryptorEngine.Encrypt(strResult, true);
                                    
                                    
                                }
                            }
                            else
                            {
                                strResult = "Success~Not Found";

                            }
                        }
                        catch (Exception ex)
                        {
                            String Message = ex.Message;
                            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                            strResult = "Success~exp~"+ Message;

                        }
                    }
                    
                    context.Response.ContentType = "application/text";
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.Write(strResult);
                    
                break;
            
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}