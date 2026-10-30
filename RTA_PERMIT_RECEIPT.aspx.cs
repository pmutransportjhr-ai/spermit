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



public partial class RTA_PERMIT_RECEIPT : System.Web.UI.Page
{

   protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA_PERMIT.aspx'", true);
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
                parameters[0] = new SqlParameter("@ROUTE_ID", Convert.ToInt32(ID));
                parameters[1] = new SqlParameter("@PERMIT_TYPE_ID", DBNull.Value);
                parameters[2] = new SqlParameter("@PERMIT_TYPE_OPTION_ID", DBNull.Value);
                parameters[3] = new SqlParameter("@REGISTRATION_NUMBER", DBNull.Value);
                parameters[4] = new SqlParameter("@MODE", "VIEW_RECEIPT");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_RTA_GET_PERMIT_INFO", parameters);
                DataTable dt1 = ds.Tables[0];
                String EncryptedStr = "";
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        lblApplicationNo.Text = dt1.Rows[i]["APPLICATION_NUMBER"].ToString();
                        lblEnrollmentOn.Text = dt1.Rows[i]["ENROLLMENT_ON"].ToString();
                        lblOwnerName.Text = dt1.Rows[i]["OWNER_NAME"].ToString();
                        lblRegistrationNo.Text = dt1.Rows[i]["REGISTRATION_NUMBER"].ToString();
                        lblCareOf.Text = dt1.Rows[i]["CARE_OF"].ToString();
                        lblAddress.Text = dt1.Rows[i]["ADDRESS"].ToString();
                        lblChasisNumber.Text = dt1.Rows[i]["CHASIS_NUMBER"].ToString();
                        lblEngineNumber.Text = dt1.Rows[i]["ENGINE_NUMBER"].ToString();
                        lblPermitFee.Text = dt1.Rows[i]["FEE"].ToString();
                        String RandNo = Convert.ToString(DateTime.Now.Ticks);
                        EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102030101", Convert.ToString(dt1.Rows[0]["OWNER_NAME"]), RandNo, Convert.ToString(dt1.Rows[i]["FEE"]), Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]), "NA", "RTA PERMIT ISSUE", Convert.ToString(dt1.Rows[0]["APP_ID"]), "RTA", Convert.ToString(dt1.Rows[0]["TREASCODE"]), Convert.ToString(dt1.Rows[0]["IFMSOFFICECODE"]), "sec1234");
                        EncryptedParameter.Value = EncryptedStr;
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
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

            }


        }
    }
    
    protected void btnYes_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "window.location ='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "'", true);

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");

    }
}