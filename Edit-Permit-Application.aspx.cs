using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.Configuration;
using System.Web.Services;

public partial class Edit_Permit_Application : System.Web.UI.Page
{
    public string FillInterStatePermitList { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_ddlDesState();
        }
    }
    public void Bind_ddlDesState()
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");
        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters);

        ddlDesState.DataSource = dr;
        ddlDesState.Items.Clear();
        ddlDesState.Items.Add(new ListItem("All", "0"));
        ddlDesState.DataTextField = "STATE_NAME";
        ddlDesState.DataValueField = "STATE_CODE";
        ddlDesState.DataBind();
        ddlDesState.SelectedValue = "0";
    }
    protected void btnRun_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {
                String State = ddlDesState.SelectedValue;
                String Status = ddlStatus.SelectedValue;
                String PermitType = ddlType.SelectedValue;

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "STA_SP_GET_MASTER_DATA";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@STATE_CODE", State);
                sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", Status);
                sCmd.Parameters.AddWithValue("@TYPE", PermitType);
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@MODE", "EDIT_LIST_MASTER");
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
                        string encryptedId = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);

                        string validFromText = dr["VALID_FROM"] != DBNull.Value
                      ? dr["VALID_FROM"].ToString().Replace("/", "-")
                      : "-";

                        string validUptoText = dr["VALID_UP_TO"] != DBNull.Value
                            ? dr["VALID_UP_TO"].ToString().Replace("/", "-")
                            : "-";

                        sb.Append("<tr>");

                        // New Column 1: Application No 
                        sb.Append("<td>");
                        sb.Append("<span class='main-data'>App No: " + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</span>");
                        sb.Append("</td>");

                        // New Column 2: Permit No / Permit Type
                        sb.Append("<td>");
                        sb.Append("<span class='main-data'>Permit No: " + Convert.ToString(dr["PERMIT_NUMBER"]) + "</span>");
                        sb.Append("<span class='secondary-data'>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</span>");
                        sb.Append("</td>");

                        // New Column 3:  Reg. No
                        sb.Append("<td>");
                        sb.Append("<span class='main-data'>Reg No: " + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</span>");
                        sb.Append("</td>");

                        // New Column 4: Name 
                        sb.Append("<td>");
                        sb.Append("<span class='main-data'>" + Convert.ToString(dr["OWNER_NAME"]) + "</span>");
                        sb.Append("</td>");

                        // New Column 5: Route (Using route-col class for wrapping)
                        sb.Append("<td class='route-col'>" + Convert.ToString(dr["ROUTE"]) + "</td>");

                        // New Column 6: Valid From / Valid Upto
                        sb.Append("<td>");
                        sb.Append("<span class='main-data'>From: " + validFromText + "</span>");
                        sb.Append("<span class='secondary-data'>Upto: " + validUptoText + "</span>");
                        sb.Append("</td>");

                        // New Column 5: Status
                        string status = dr["APPLICATION_STATUS"].ToString();
                        string cssClass = "label label-default";

                        if (status == "Issued")
                            cssClass = "label label-success";
                        else if (status == "Renewed")
                            cssClass = "label label-info";
                        else if (status == "Replaced")
                            cssClass = "label label-warning";
                        else if (status == "Transfered")
                            cssClass = "label label-primary";
                        else
                            cssClass = "label label-primary";

                        sb.Append("<td><span class='" + cssClass + "'>" + status + "</span></td>");

                        // New Column 6: Action
                        sb.Append("<td>");
                        // Edit Button
           
                        sb.Append("<a class='btn btn-sm btn-primary me-2' href=\"javascript:MasterEdit('" + encryptedId + "');\">Edit</a>");
                        // View Button
                        sb.Append("<a class='btn btn-sm btn-default' href=\"javascript:MasterView('" + encryptedId + "');\">View</a>"); 
                        sb.Append("</td>");
                        sb.Append("</tr>");
                    }

                    divrenewalDetail.InnerHtml = sb.ToString();
                    tblISPermit.Visible = true;
                }
                else
                {
                    tblISPermit.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found');", true);
                }

                // Call the client-side functions again after the postback
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "initDatePickersOldStyle(); initDataTable();", true);

            }
            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');", true);
            }
        }
    }
}