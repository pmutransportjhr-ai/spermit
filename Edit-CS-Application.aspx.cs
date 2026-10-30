using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using QRCoder;
using System.Configuration;
using System.Web.Services;

public partial class Edit_CS_Application : System.Web.UI.Page
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

        parameters[0] = new SqlParameter("@EMPLOYEE_ID",
        Convert.ToString(Session["EMPLOYEE_ID"]));

        parameters[1] = new SqlParameter("@MODE",
        "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");

        SqlDataReader dr = SQLHelper.ExecuteReader(
            SQLHelper.GetConnString(),
            CommandType.StoredProcedure,
            "STA_SP_MSTR_LIST",
            parameters
        );

        ddlDesState.DataSource = dr;
        ddlDesState.DataTextField = "STATE_NAME";
        ddlDesState.DataValueField = "STATE_CODE";
        ddlDesState.DataBind();

        ddlDesState.SelectedValue = "0";
    }


    protected void btnRun_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(
        ConfigurationManager.ConnectionStrings["connectionstring"].ToString()))
        {
            SqlCommand cmd = new SqlCommand(
            "STA_SP_GET_COUNTER_SIGNATURE_MASTER_DATA", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@STATE_CODE", ddlDesState.SelectedValue);
            cmd.Parameters.AddWithValue("@APPLICATION_STATUS", ddlStatus.SelectedValue);
            cmd.Parameters.AddWithValue("@TYPE", ddlType.SelectedValue);
            cmd.Parameters.AddWithValue("@EMPLOYEE_ID", Session["EMPLOYEE_ID"].ToString());
            cmd.Parameters.AddWithValue("@MODE", "LIST_MASTER");

            DataTable dt = new DataTable();
            new SqlDataAdapter(cmd).Fill(dt);

            if (dt.Rows.Count == 0)
            {
                tblISPermit.Visible = false;
                return;
            }

            StringBuilder sb = new StringBuilder();

            foreach (DataRow dr in dt.Rows)
            {
                string encId = CryptorEngine.Encrypt("ID=" + dr["APP_ID"], true);

                sb.Append("<tr>");

                sb.Append("<td>");
                sb.Append("<span class='main-data'>App No: " + dr["APPLICATION_NUMBER"] + "</span>");
                sb.Append("</td>");

                sb.Append("<td>");
                sb.Append("<span class='main-data'>Permit No: " + dr["PERMIT_NUMBER"] + "</span>");
                sb.Append("</td>");

                sb.Append("<td>");
                sb.Append("<span class='main-data'>Reg No: " + dr["REGISTRATION_NUMBER"] + "</span>");
                sb.Append("</td>");

                sb.Append("<td>");
                sb.Append("<span class='main-data'>Owner Name: " + dr["OWNER_NAME"] + "</span>");
                sb.Append("</td>");

                sb.Append("<td>");
                sb.Append("<span class='main-data'>From: " + dr["VALID_FROM"] + "</span>");
                sb.Append("<span class='secondary-data'>Upto: " + dr["VALID_UP_TO"] + "</span>");
                sb.Append("</td>");

                sb.Append("<td class='route-col'>" + dr["ROUTE"] + "</td>");

                // Bootstrap 3 label
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

                sb.Append("<td>");

                sb.Append("<button class='btn btn-sm btn-primary' onclick=\"MasterEdit('"
                + encId + "')\">Edit</button> ");

                sb.Append("<button class='btn btn-sm btn-default' onclick=\"MasterView('"
                + encId + "')\">View</button>");

                sb.Append("</td>");

                sb.Append("</tr>");
            }

            tbdInterStatePermitList.InnerHtml = sb.ToString();
            tblISPermit.Visible = true;
        }
    }
}