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



public partial class RTA_AUTO_RICKSHAW_PERMIT_ISSUED_RECEIPT : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                return;
            }
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

                QueryString IDx = QueryStr.Where(l => l.Name == "APP_ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                String ID = IDx.Value;
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@APP_ID", Convert.ToInt32(ID));
                parameters[1] = new SqlParameter("@PERMIT_TYPE_ID", DBNull.Value);
                parameters[2] = new SqlParameter("@PERMIT_TYPE_OPTION_ID", DBNull.Value);
                parameters[3] = new SqlParameter("@REGISTRATION_NUMBER", DBNull.Value);
                parameters[4] = new SqlParameter("@MODE", "VIEW_ISSUED_RECEIPT");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_RTA_GET_AUTO_RICKSHAW_PERMIT_INFO", parameters);
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        lblRTOName.Text = dt1.Rows[i]["RTO_NAME"].ToString();
                        lblPermitNo.Text = dt1.Rows[i]["PERMIT_NUMBER"].ToString();
                        lblOwnerName.Text = dt1.Rows[i]["OWNER_NAME"].ToString();
                        lblPermanentAddress.Text = dt1.Rows[i]["PERMANENT_ADDRESS"].ToString();
                        lblPresentAddress.Text = dt1.Rows[i]["PRESENT_ADDRESS"].ToString();
                        lblChasisNumber.Text = dt1.Rows[i]["CHASIS_NUMBER"].ToString();
                        lblEngineNumber.Text = dt1.Rows[i]["ENGINE_NUMBER"].ToString();
                        lblTypesOfVehichle.Text = dt1.Rows[i]["VEHICLE_CLASS_TYPE"].ToString();
                        lblSeatCapacity.Text = dt1.Rows[i]["SEAT_CAPACITY"].ToString();
                        lblAgreementwith.Text = dt1.Rows[i]["AGREEMENT_WITH"].ToString();
                        lblPermitFrom.Text = dt1.Rows[i]["VALID_FROM"].ToString();
                        lblPermitTo.Text = dt1.Rows[i]["VALID_TO"].ToString();
                        lblRegistrationNo.Text = dt1.Rows[i]["REGISTRATION_NUMBER"].ToString();
                        

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                    return;
                }
            }


            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid URL');window.location ='PermitHomePage.aspx'", true);

            }


        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}


