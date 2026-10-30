using SoftEra.JHT.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI;

public partial class STA_DASHBOARD : System.Web.UI.Page
{
    private const int PageSize = 10;

    private int VerifyPageIndex
    {
        get { return ViewState["VerifyPageIndex"] == null ? 0 : (int)ViewState["VerifyPageIndex"]; }
        set { ViewState["VerifyPageIndex"] = value; }
    }

    private int ApprovePageIndex
    {
        get { return ViewState["ApprovePageIndex"] == null ? 0 : (int)ViewState["ApprovePageIndex"]; }
        set { ViewState["ApprovePageIndex"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadTopData();
    }

    public void LoadTopData()
    {
        try
        {
            /* ================= TOP HEADER ================= */
            SqlParameter[] p = {
                new SqlParameter("@EMPLOYEE_ID", Session["EMPLOYEE_ID"]),
                new SqlParameter("@MODE", "TOP_HDR")
            };

            DataTable dt = SQLHelper.ExecuteDataset(
                SQLHelper.GetConnString(),
                CommandType.StoredProcedure,
                "STA_SP_GET_DASHBOARD_DETAILS", p).Tables[0];

            MRevenue.InnerText = dt.Rows[0]["M_T_REV"].ToString();
            QRevenue.InnerText = dt.Rows[0]["Q_T_REV"].ToString();
            HRevenue.InnerText = dt.Rows[0]["H_T_REV"].ToString();
            YRevenue.InnerText = dt.Rows[0]["Y_T_REV"].ToString();

            MPTIssued.InnerText = dt.Rows[0]["M_P_ISSUED"].ToString();
            QPTIssued.InnerText = dt.Rows[0]["Q_P_ISSUED"].ToString();
            HPTIssued.InnerText = dt.Rows[0]["H_P_ISSUED"].ToString();
            YPTIssued.InnerText = dt.Rows[0]["Y_P_ISSUED"].ToString();

            MPTApp.InnerText = dt.Rows[0]["M_P_APP"].ToString();
            QPTApp.InnerText = dt.Rows[0]["Q_P_APP"].ToString();
            HPTApp.InnerText = dt.Rows[0]["H_P_APP"].ToString();
            YPTApp.InnerText = dt.Rows[0]["Y_P_APP"].ToString();

            string empId = Session["EMPLOYEE_ID"].ToString();
            string role = Convert.ToString(Session["USER_ROLE"]);

            dvMainTableContent.Visible = dvMainTableContentHeader.Visible =
                (role == "ASSISTANT" || role == "SO" || role == "JTC" || role == "APPROVER");

            /* ================= VERIFY LIST ================= */
            if (role == "ASSISTANT" || role == "SO" || role == "JTC")
            {
                LoadPagedList("LIST_VERIFY", empId, VerifyPageIndex,
                    FillSTAPermitApplication, dvVerifySection,
                    btnVerifyPrev, btnVerifyNext);
            }

            /* ================= APPROVE LIST ================= */
            if (role == "APPROVER" || role == "JTC")
            {
                LoadPagedList("LIST_APPROVE", empId, ApprovePageIndex,
                    FillSTAPermitApplication1, dvApproveSection,
                    btnApprovePrev, btnApproveNext, true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(
                Page, Page.GetType(), Guid.NewGuid().ToString(),
                "alert('" + ex.Message + "');", true);
        }
    }

    private void LoadPagedList(
     string mode,
     string empId,
     int pageIndex,
     System.Web.UI.HtmlControls.HtmlGenericControl target,
     System.Web.UI.WebControls.Panel panel,
     System.Web.UI.WebControls.Button prev,
     System.Web.UI.WebControls.Button next,
     bool isApprove = false)
    {
        SqlParameter[] p = {
            new SqlParameter("@EMPLOYEE_ID", empId),
            new SqlParameter("@MODE", mode)
        };

        DataTable dt = SQLHelper.ExecuteDataset(
            SQLHelper.GetConnString(),
            CommandType.StoredProcedure,
            "STA_SP_GET_DASHBOARD_DETAILS", p).Tables[0];

        if (dt.Rows.Count == 0) return;

        var rows = dt.AsEnumerable()
                     .Skip(pageIndex * PageSize)
                     .Take(PageSize);

        StringBuilder sb = new StringBuilder();

        foreach (DataRow dr in rows)
        {
            sb.Append("<tr>");
            sb.Append("<td>" + dr["APPLICATION_NUMBER"] + "</td>");

            if (isApprove)
                sb.Append("<td>" + dr["PERMIT_NUMBER"] + "</td>");

            sb.Append("<td>" + dr["PERMIT_TYPE"] + "</td>");
            sb.Append("<td>" + dr["APPLICANT_NAME"] + "</td>");
            sb.Append("<td>" + dr["REGISTRATION_NUMBER"] + "</td>");
            sb.Append("<td>" + dr["APPLICATION_STATUS"] + "</td>");
            sb.Append("<td>" + dr["SERVICES_NAME"] + "</td>");
            sb.Append("<td><a class='btn btn-outline-dark btn-sm' href='" + dr["URL"] + "'>Action</a></td>");
            sb.Append("</tr>");
        }

        target.InnerHtml = sb.ToString();
        panel.Visible = true;

        prev.Enabled = pageIndex > 0;
        next.Enabled = ((pageIndex + 1) * PageSize) < dt.Rows.Count;
    }

    protected void btnVerifyPrev_Click(object sender, EventArgs e)
    {
        VerifyPageIndex--;
        LoadTopData();
    }

    protected void btnVerifyNext_Click(object sender, EventArgs e)
    {
        VerifyPageIndex++;
        LoadTopData();
    }

    protected void btnApprovePrev_Click(object sender, EventArgs e)
    {
        ApprovePageIndex--;
        LoadTopData();
    }

    protected void btnApproveNext_Click(object sender, EventArgs e)
    {
        ApprovePageIndex++;
        LoadTopData();
    }
}
