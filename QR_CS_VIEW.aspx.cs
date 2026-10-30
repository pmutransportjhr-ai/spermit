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
using System.Drawing;
using System.IO;
public partial class QR_CS_VIEW : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (string.IsNullOrEmpty(Request.QueryString["Q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('You are not permitted to access this page');window.location ='STA-DASHBOARD.aspx'", true);
                return;
            }
             
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {
                    String queryString = Context.Request.QueryString["Q"];

                    /* foreach (String str in queryString.Split(new Char[] { '&' }))
                    {
                        QueryString qs = new QueryString();
                        qs.Name = str.Split(new Char[] { '=' })[0];
                        qs.Value = str.Split(new Char[] { '=' })[1];
                        QueryStr.Add(qs);
                    }

                    QueryString IDx = QueryStr.Where(l => l.Name == "PER_NUMBER").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                    String ID = IDx.Value;
                    */
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_SP_GET_COUNTER_SIGNATURE_MASTER_DATA";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", queryString);
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_ISSUED_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt1 = ds.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        string RegNumber = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                        
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            csno.Text = Convert.ToString(dt1.Rows[i]["COUNTER_SIGNATURE_NUMBER"]);
                            lblName.Text = Convert.ToString(dt1.Rows[i]["OWNER_NAME"]);
                            lblresidentialaddr.Text = Convert.ToString(dt1.Rows[i]["PERMANENT_ADDRESS"]);
                            lblbusinessaddress.Text = Convert.ToString(dt1.Rows[i]["PRESENT_ADDRESS"]);
                            lblprimaryPermitno.Text = Convert.ToString(dt1.Rows[i]["PERMIT_NUMBER"]);
                            lblissuedstate.Text = Convert.ToString(dt1.Rows[i]["STATE_NAME"]);
                            lblValidupto.Text = Convert.ToString(dt1.Rows[i]["PERMIT_VALID_UPTO"]);
                            lblRegNo.Text = Convert.ToString(dt1.Rows[i]["PERMIT_VALID_UPTO"]);

                            lblModel.Text = Convert.ToString(dt1.Rows[i]["MANUFACTURING_ON"]);
                            lblSeatCapacity.Text = Convert.ToString(dt1.Rows[i]["SEATING_CAPACITY"]);
                            lblRouteDertailswithvia.Text = Convert.ToString(dt1.Rows[i]["ROUTE"]);
                            lblValidtyFrom.Text = Convert.ToString(dt1.Rows[i]["cs_valid_from"]);
                            lblValidityto.Text = Convert.ToString(dt1.Rows[i]["cs_valid_to"]);

                            lblTaxFrom.Text = Convert.ToString(dt1.Rows[i]["cs_valid_from"]); 
                            lblTaxto.Text = Convert.ToString(dt1.Rows[i]["cs_valid_to"]);
                            lblAmount.Text = Convert.ToString(dt1.Rows[i]["FEE_TAX_AMOUNT"]);
                            lblReferenceno.Text = Convert.ToString(dt1.Rows[i]["FEE_TAX_PAY_TRANSACTION_ID"]);
                            lblTransactiondt.Text = Convert.ToString(dt1.Rows[i]["FEE_TAX_PAID_ON"]); 
                        
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No data Found');window.location ='STA-DASHBOARD.aspx'", true);
                        return;
                    }
                }


                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    Messqge = Messqge.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location ='STA-DASHBOARD.aspx'", true);

                }
            }

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}


