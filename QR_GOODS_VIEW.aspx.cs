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



public partial class QR_GOODS_VIEW : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('You are not permitted to access this page');window.location ='http://spermit.jharkhand.gov.in/'", true);
                return;
            }
             
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {
                    String queryString = Context.Request.QueryString["q"];

                  /*  foreach (String str in queryString.Split(new Char[] { '&' }))
                    {
                        QueryString qs = new QueryString();
                        qs.Name = str.Split(new Char[] { '=' })[0];
                        qs.Value = str.Split(new Char[] { '=' })[1];
                        QueryStr.Add(qs);
                    }

                    QueryString IDx = QueryStr.Where(l => l.Name == "PER_NUMBER").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                    String ID = IDx.Value;*/

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "RTA_SP_GET_GOODS_MASTER_DATA";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", queryString);
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_ISSUED_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt1 = ds.Tables[0];
                    DataTable dt2 = ds.Tables[1];
                    
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            lblPermitNo.Text = dt1.Rows[i]["PERMIT_NUMBER"].ToString();
                            lblOwnerName.Text = dt1.Rows[i]["OWNER_NAME"].ToString();
                            lblPermanentAddress.Text = dt1.Rows[i]["PERMANENT_ADDRESS"].ToString();
                            lblPresentAddress.Text = dt1.Rows[i]["PRESENT_ADDRESS"].ToString();
                            lblRoute.Text = "NOT APPLICABLE";
                            lblPermitFrom.Text = dt1.Rows[i]["VALID_FROM"].ToString();
                            lblPermitTo.Text = dt1.Rows[i]["VALID_UP_TO"].ToString();
                            lblRegistrationNo.Text = dt1.Rows[i]["REGISTRATION_NUMBER"].ToString();
                            lblChasisNumber.Text = dt1.Rows[i]["CHASIS_NUMBER"].ToString();
                            lblEngineNumber.Text = dt1.Rows[i]["ENGINE_NUMBER"].ToString();

                            lblWholeState.Text = dt1.Rows[0]["PERMIT_WHOLE_JHARKHAND"].ToString();
                            lblForSelf.Text = dt1.Rows[0]["PERMIT_SELF"].ToString();
                            lblOtherRTO.Text = dt2.Rows[0]["OTHER_RTO_NAME"].ToString();
                             lblNatureOfGoods .Text = dt1.Rows[0]["NATURE_OF_GOODS"].ToString();
                            lblVehicleCapacity.Text = dt1.Rows[0]["VEHICLE_CAPACITY"].ToString();

                            if (dt2.Rows[0]["OTHER_STATE_FIRST"].ToString() != "NO")
                            {
                                lblOtherState .Text = dt2.Rows[0]["OTHER_STATE_FIRST"].ToString() + ", " + dt2.Rows[0]["OTHER_STATE_SECOND"].ToString() + " (VALID FOR :  " + dt1.Rows[0]["PERMIT_VALIDITY_IN_MONTH"].ToString() + " MONTHS)";
                            }
                            else
                            {
                                lblOtherState .Text = "NO";
                            }
                            lblRates.Text = dt1.Rows[0]["RATES_OF_CHARGES"].ToString();

                            


                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-DASHBOARD.aspx'", true);
                        return;
                    }
                }


                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    Messqge = Messqge.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location ='RTA-DASHBOARD.aspx'", true);

                }
            }

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}


