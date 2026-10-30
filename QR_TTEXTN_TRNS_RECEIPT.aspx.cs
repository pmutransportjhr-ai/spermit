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
public partial class QR_TTEXTN_TRNS_RECEIPT : System.Web.UI.Page
{
   protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

      /*      if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                return;
            }*/
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {
                    String queryString = Context.Request.QueryString["APP_ID"];

                    /*foreach (String str in queryString.Split(new Char[] { '&' }))
                    {
                        QueryString qs = new QueryString();
                        qs.Name = str.Split(new Char[] { '=' })[0];
                        qs.Value = str.Split(new Char[] { '=' })[1];
                        QueryStr.Add(qs);
                    }

                    QueryString IDx = QueryStr.Where(l => l.Name == "APP_ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                   */
                    String ID = queryString;
                    
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "SP_STA_CS_COMM_HISTORY";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(ID));
                    sCmd.Parameters.AddWithValue("@MODE", "PAYMENT_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];             
                    if (dt.Rows.Count > 0)
                    {
                        string applicationNumber = Convert.ToString(dt.Rows[0]["APPLICATION_NUMBER"]);
                        string RegistrationNumber = Convert.ToString(dt.Rows[0]["REGISTRATION_NUMBER"]);
                        string permitNumber = Convert.ToString(dt.Rows[0]["PERMIT_NUMBER"]);
                        string FeesAmount = Convert.ToString(dt.Rows[0]["FEES"]);
                        string PaymentTransRefNumber = Convert.ToString(dt.Rows[0]["FEE_PAY_TRANSACTION_ID"]);
                        lblApplicationNumber.Text = applicationNumber;
                        lblRegistrationNumber.Text = RegistrationNumber;
                        lblPermitNumber.Text = permitNumber;
                        lblAmount.Text = FeesAmount;
                        lblPaymentTransRefNo.Text = PaymentTransRefNumber;
                       
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), 
                            Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                        return;
                    }
                }


                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), 
                        Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

                }

            }
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
      

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");

    }
}