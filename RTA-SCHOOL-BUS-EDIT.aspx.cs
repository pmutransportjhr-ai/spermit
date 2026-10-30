using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTA_SCHOOL_BUS_EDIT : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            BindRTO();

            //  Lock dropdown for non-moderator
            if (Convert.ToString(Session["USER_LABEL"]) != "MODERATOR")
            {
                ddlRTO.Enabled = false;

                // Auto select user's RTO
                if (Session["RTO_CODE"] != null)
                {
                    ddlRTO.SelectedValue = Convert.ToString(Session["RTO_CODE"]);
                }
            }
            else
            {
                // Moderator can see all
                ddlRTO.Enabled = true;
                ddlRTO.SelectedValue = "0";
            }
        }
    }


    private void BindRTO()
    {
        DataSet ds = SQLHelper.ExecuteDataset(
            SQLHelper.GetConnString(),
            CommandType.Text,
            "SELECT RTO_CODE, RTO_NAME FROM RTA_RTO_MASTER WHERE STATUS='Active'"
        );

        ddlRTO.DataSource = ds;
        ddlRTO.DataBind();

        ddlRTO.Items.Insert(0, new ListItem("All", "0"));
    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        string connStr = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        using (SqlConnection sCon = new SqlConnection(connStr))
        {
            try
            {

                string rto = ddlRTO.SelectedValue;

                sCon.Open();

                using (SqlCommand cmd = new SqlCommand("RTA_EDIT_APPLICATION_LIST", sCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ✅ PASS ALL REQUIRED PARAMS


                    cmd.Parameters.AddWithValue("@RTO_CODE", rto);
                    cmd.Parameters.AddWithValue("@MODE", "SB_LISTAPPLIED");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    StringBuilder sb = new StringBuilder();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {


                            string encryptedId = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);

                            // ✅ Status Badge (modern UI)
                            string statusBadge = "<span class='badge badge-secondary'>" + dr["APPLICATION_STATUS"] + "</span>";

                            if (dr["APPLICATION_STATUS"].ToString() == "Issued")
                                statusBadge = "<span class='badge badge-success'>Issued</span>";
                            else if (dr["APPLICATION_STATUS"].ToString() == "Rejected")
                                statusBadge = "<span class='badge badge-danger'>Rejected</span>";
                            else if (dr["APPLICATION_STATUS"].ToString() == "Pending")
                                statusBadge = "<span class='badge badge-warning'>Pending</span>";

                            sb.Append("<tr>");

                            sb.Append("<td>" + dr["APPLICATION_NUMBER"] + "</td>");
                            sb.Append("<td>" + dr["OWNER_NAME"] + "</td>");
                            sb.Append("<td>" + dr["REGISTRATION_NUMBER"] + "</td>");
                            sb.Append("<td>" + dr["VALID_FROM"] + "</td>");
                            sb.Append("<td>" + dr["VALID_UP_TO"] + "</td>");
                            sb.Append("<td>" + dr["PERMIT_NUMBER"] + "</td>");
                            sb.Append("<td>" + statusBadge + "</td>");

                            // ✅ Modern buttons
                            sb.Append("<td>");
                            sb.Append("<a class='btn btn-sm btn-primary me-2' href=\"javascript:MasterEdit('" + encryptedId + "');\">Edit</a>");
                            // View Button
                            sb.Append("<a class='btn btn-sm btn-default' href=\"javascript:MasterView('" + encryptedId + "');\">View</a>");
                            sb.Append("</td>");
                            sb.Append("</tr>");
                        }

                        FillSTAPermitApplication.Text = sb.ToString(); // ✅ use Text for Literal
                        tblISPermit.Visible = true;
                    }
                    else
                    {
                        tblISPermit.Visible = false;

                        ScriptManager.RegisterClientScriptBlock(this, GetType(),
                            Guid.NewGuid().ToString(),
                            "alert('No Data Found');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "").Replace("\n", " ");

                ScriptManager.RegisterClientScriptBlock(this, GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('" + msg + "');", true);
            }
        }
    }
}