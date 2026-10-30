using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json;
using System.Text;
using QRCoder;
using System.IO;
using System.Drawing;



public partial class QR_STA_TRNS_RECEIPT : System.Web.UI.Page
{
   protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           /* if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                return;
            }*/
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                try
                {

                   
                    String QueryString  = Convert.ToString(Context.Request.QueryString["APP_ID"]);
                    string ID = QueryString.Split('~')[0];
                    String TYPE = QueryString.Split('~')[1].Split('=')[1];
                    String APPLICATION_SERVICE = QueryString.Split('~')[2].Split('=')[1];
                
                                      //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(ID+","+ TYPE+","+ APPLICATION_SERVICE) + "');", true);

                   /* String ID = "968";
                    String TYPE = "STA-APPLICATION";
                    String APPLICATION_SERVICE = "STA APPLICATION";
                    */
                    sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@APP_ID", ID);
                    sCmd.Parameters.AddWithValue("@TYPE", TYPE);
                    sCmd.Parameters.AddWithValue("@APPLICATION_SERVICE", APPLICATION_SERVICE);
                    sCmd.Parameters.AddWithValue("@MODE", "TAX_RECEIPT_COPY_PERMIT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0];
            
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            lblapnumber.InnerText = dt1.Rows[i]["APPLICATION_NUMBER"].ToString();
                            servicename.InnerText = dt1.Rows[i]["SERVICE_NAME"].ToString();
                            trndate.InnerText = dt1.Rows[i]["TRANSACTION_ON"].ToString();
                            trnno.InnerText = dt1.Rows[i]["DEPTTRANID"].ToString();
                            Amount.InnerText = dt1.Rows[i]["TXN_AMOUNT"].ToString();
                            txtGRN.InnerText = Convert.ToString(dt1.Rows[i]["GRN"]);
                        }
                      
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx';", true);
                        return;
                    }
                }


                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message.ToString() + "');window.location ='PermitHomePage.aspx';", true);

                }


            }
        }
    }
    
}