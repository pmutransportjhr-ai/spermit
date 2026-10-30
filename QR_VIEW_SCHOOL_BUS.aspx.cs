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
public partial class QR_VIEW_SCHOOL_BUS : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "alert('You are not permitted to access this page');window.location ='http://spermit.jharkhand.gov.in'", true);
                return;
            }
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["q"]), true);
                    foreach (String str in queryString.Split(new Char[] { '&' }))
                    {
                        QueryString qs = new QueryString();
                        qs.Name = str.Split(new Char[] { '=' })[0];
                        qs.Value = str.Split(new Char[] { '=' })[1];
                        QueryStr.Add(qs);
                    }
                    QueryString IDx = QueryStr.Where(l => l.Name == "PER_NUMBER").FirstOrDefault();
                    String ID = IDx.Value;
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "RTA_SP_GET_SCHOOL_BUS_PERMIT_MASTER_DATA";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", ID);
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_ISSUED_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt1 = ds.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            lblPermitNo.Text = dt1.Rows[i]["PERMIT_NUMBER"].ToString();
                            lblOwnerName.Text = dt1.Rows[i]["OWNER_NAME"].ToString();
                            lblPermanentAddress.Text = dt1.Rows[i]["PERMANENT_ADDRESS"].ToString();
                            lblPresentAddress.Text = dt1.Rows[i]["PRESENT_ADDRESS"].ToString();
                          //  lblRoute.Text = "NOT APPLICABLE";
                            lblPermitFrom.Text = dt1.Rows[i]["VALID_FROM"].ToString();
                            lblPermitTo.Text = dt1.Rows[i]["VALID_UP_TO"].ToString();
                            lblRegistrationNo.Text = dt1.Rows[i]["REGISTRATION_NUMBER"].ToString();
                            lblChasisNumber.Text = dt1.Rows[i]["CHASIS_NUMBER"].ToString();
                            lblEngineNumber.Text = dt1.Rows[i]["ENGINE_NUMBER"].ToString();

                            lblMake.Text = Convert.ToString(dt1.Rows[i]["MAKER_DESC"]);
                            lblModel.Text = Convert.ToString(dt1.Rows[i]["MANUFACTURING_ON"]);
                            lblVehicleType.Text = Convert.ToString(dt1.Rows[i]["VEHICLE_CLASS_TYPE"]);
                            lblCapacity.Text = Convert.ToString(dt1.Rows[i]["SEATING_CAPACITY"]);
                            lblPermitArea.Text = Convert.ToString(dt1.Rows[i]["ST_FROM"]);
                            lblRegion1.Text = Convert.ToString(dt1.Rows[i]["RTO_NAME"]).Split('(')[0];
                            lblRegion2.Text = Convert.ToString(dt1.Rows[i]["RTO_NAME"]).Split('(')[0];
                            lblRegion3.Text = Convert.ToString(dt1.Rows[i]["RTO_NAME"]).Split('(')[0];




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


