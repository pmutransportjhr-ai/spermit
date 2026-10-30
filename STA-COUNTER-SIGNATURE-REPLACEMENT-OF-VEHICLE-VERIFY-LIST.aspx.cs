using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.IO;
using System.Configuration;
using System.Text;

public partial class STA_COUNTER_SIGNATURE_REPLACEMENT_OF_VEHICLE_VERIFY_LIST : System.Web.UI.Page
{
    public string FillInterStatePermitList { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_ddlDesState();
            if (Convert.ToString(Session["USER_LABEL"]) == "JTC")
            {
                ddlReplaceAction.Items.FindByValue("Verified").Text = "Approved";
                ddlReplaceAction.Items.FindByValue("Verified").Value = "Approved";

            }

            if (Convert.ToString(Session["USER_LABEL"]).Equals("SO", StringComparison.OrdinalIgnoreCase))
            {
                ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("Applied"));
            }

        }
    }
    public void Bind_ddlDesState()
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");

        SqlDataReader dr = SQLHelper.ExecuteReader(
            SQLHelper.GetConnString(),
            CommandType.StoredProcedure,
            "STA_SP_MSTR_LIST",
            parameters);

        ddlDesState.DataSource = dr;
        ddlDesState.Items.Clear();
        ddlDesState.Items.Add(new ListItem("All", "0"));
        ddlDesState.DataTextField = "STATE_NAME";
        ddlDesState.DataValueField = "STATE_CODE";
        ddlDesState.DataBind();

        // Get session values
        string userLabel = Convert.ToString(Session["USER_LABEL"]);
        string rtoCode = Convert.ToString(Session["RTO_CODE"]);

        // Lock dropdown ONLY for ASSISTANT user
        if (!string.IsNullOrEmpty(userLabel) && userLabel == "ASSISTANT")
        {
            if (!string.IsNullOrEmpty(rtoCode))
            {
                ListItem item = ddlDesState.Items.FindByValue(rtoCode);

                if (item != null)
                {
                    ddlDesState.SelectedValue = rtoCode;

                    // Lock dropdown
                    ddlDesState.Attributes.Add("disabled", "disabled");
                }
            }
        }
        else
        {
            // Other users can select freely
            ddlDesState.SelectedValue = "0";
            ddlDesState.Enabled = true;
        }
    }




    protected void btnRun_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {
                String State = ddlDesState.SelectedValue;
                String Status = ddlStatus.SelectedValue;
                String PermitType = ddlType.SelectedValue;

                sCmd.CommandText = "STA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@STATE_CODE", State);
                sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", Status);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PermitType);
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@MODE", "LISTAPPLIED");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0];


                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);
                        string q1 = CryptorEngine.Encrypt("REGNO=" + Convert.ToString(dr["REGISTRATION_NUMBER"]), true);

                        sb.Append("<tr>");
                        sb.Append("<td>" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["OLD_REGISTRATION_NUMBER"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["MOBILE_NUMBER"]) + "</td>");
                        sb.Append("<td><a  href=\"javascript:fnRegDetail('" + q1 + "');\">" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</a></td>");
                        sb.Append("<td>" + Convert.ToString(dr["ROUTE"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["FEE"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["PAYMENT_STATUS"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["FEE_PAY_TRANSACTION_ID"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["VALID_FROM"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["APPLICATION_PENDING_ON"]) + "</td>");                       
                        
                            sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                        
                        if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                        {
                            sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnAction('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "','" + Convert.ToString(dr["APPLICATION_STATUS"]) + "','" + Convert.ToString(dr["USER_LABEL"]) + "','" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]) + "','" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]) + "','" + Convert.ToString(dr["JTC_VERIFY_COMMENTS"]) + "');\">Action</a></td>");
                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }
                        sb.Append("</tr>");
                    }


                    FillInterStatePermitList = sb.ToString();
                    tblISPermit.Visible = true;
                }
                else
                {
                    tblISPermit.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('No Data Found');", true);
                }
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');TableDatatablesScroller.init();", true);

            }
            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');", true);


            }
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "STA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", CryptorEngine.Decrypt(txtDelKey.Value, true));
                sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", ddlReplaceAction.Value);
                sCmd.Parameters.AddWithValue("@VERIFY_COMMENTS", txtCommentsReject.Text.ToString());
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@MODE", "ACTION");
                Object obj = sCmd.ExecuteScalar();
                string msg = obj.ToString().Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + msg + "');window.location ='STA-COUNTER-SIGNATURE-REPLACEMENT-OF-VEHICLE-VERIFY-LIST.aspx'", true);
            }
            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');window.location ='STA-COUNTER-SIGNATURE-REPLACEMENT-OF-VEHICLE-VERIFY-LIST.aspx'", true);
            }
        }
    }
}