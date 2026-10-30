using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RouteMonitoring : System.Web.UI.Page
{
    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitializePage();
        }
    }

    private void InitializePage()
    {
        try
        {
            // Reset Authority
            ddlAuthority.SelectedIndex = 0;

            // Reset Route
            ddlRoute.Items.Clear();
            ddlRoute.Items.Add(
                new ListItem("-- Select Route --", "")
            );

            // Default Page Size
            ddlPageSize.SelectedValue = "20";
            GridView1.PageSize = 20;

            // Clear Dashboard
            ClearRouteDetails();

            // Footer Information
            lblLastRefresh.Text =
                DateTime.Now.ToString("dd MMM yyyy hh:mm tt");

            lblCurrentPage.Text = "1";
            lblTotalPages.Text = "1";

            lblShowingFrom.Text = "0";
            lblShowingTo.Text = "0";
            lblTotalRecords.Text = "0";
            lblTotalRecordsFooter.Text = "0";
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    #endregion


    #region Common Method

    private void ShowMessage(string message)
    {
        ScriptManager.RegisterStartupScript(
            this,
            GetType(),
            Guid.NewGuid().ToString(),
            "alert('" + message.Replace("'", "") + "');",
            true);
    }

    #endregion

    #region Authority & Route

    protected void ddlAuthority_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        try
        {
            // Reset Route Dropdown
            ddlRoute.Items.Clear();
            ddlRoute.Items.Add(
                new ListItem("-- Select Route --", "")
            );

            // Clear Previous Data
            ClearRouteDetails();

            // No Authority Selected
            if (ddlAuthority.SelectedIndex == 0)
                return;

            BindRoutes();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void BindRoutes()
    {
        try
        {
            SqlParameter[] param =
            {
            new SqlParameter("@ACTION","ROUTE_LIST"),
            new SqlParameter("@AUTHORITY",ddlAuthority.SelectedValue)
        };

            DataTable dt =
                SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(),
                    CommandType.StoredProcedure,
                    "SP_ROUTE_MONITORING",
                    param).Tables[0];

            ddlRoute.Items.Clear();

            ddlRoute.Items.Add(
                new ListItem("-- Select Route --", "")
            );

            if (dt.Rows.Count == 0)
                return;

            ddlRoute.DataSource = dt;

            ddlRoute.DataTextField = "ROUTE";

            ddlRoute.DataValueField = "ROUTE_ID";

            ddlRoute.DataBind();

            ddlRoute.Items.Insert(
                0,
                new ListItem("-- Select Route --", "")
            );

            // Dashboard Information

            lblTotalRoutes.Text =
                dt.Rows.Count.ToString();

            lblAuthorityName.Text =
                ddlAuthority.SelectedItem.Text;

            lblLastRefresh.Text =
                DateTime.Now.ToString("dd MMM yyyy hh:mm tt");
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    #endregion

    #region Route Summary

    protected void ddlRoute_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        try
        {
            ClearRouteDetails();

            if (ddlRoute.SelectedIndex == 0)
                return;

            // Wait for user to click "Load Details"
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void btnLoadRoute_Click(
        object sender,
        EventArgs e)
    {
        try
        {
            if (ddlAuthority.SelectedIndex == 0)
            {
                ShowMessage("Please select Authority.");
                ddlAuthority.Focus();
                return;
            }

            if (ddlRoute.SelectedIndex == 0)
            {
                ShowMessage("Please select Route.");
                ddlRoute.Focus();
                return;
            }

            BindRouteSummary();

            BindVehicleGrid();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void BindRouteSummary()
    {
        try
        {
            SqlParameter[] param =
            {
            new SqlParameter("@ACTION","ROUTE_SUMMARY"),
            new SqlParameter("@AUTHORITY",ddlAuthority.SelectedValue),
            new SqlParameter("@ROUTE_ID",Convert.ToInt32(ddlRoute.SelectedValue))
        };

            DataTable dt =
                SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(),
                    CommandType.StoredProcedure,
                    "SP_ROUTE_MONITORING",
                    param).Tables[0];

            if (dt.Rows.Count == 0)
            {
                ClearRouteDetails();
                return;
            }

            DataRow dr = dt.Rows[0];

            //lblSelectedRoute.Text =
            //    Convert.ToString(dr["ROUTE"]);

            lblRouteVia.Text =
                Convert.ToString(dr["ROUTE"]);

            lblRouteNumber.Text =
                Convert.ToString(dr["ROUTE_NUMBER"]);

            lblDistance.Text =
                Convert.ToString(dr["TOTAL_DISTANCE"]);

            lblVacancy.Text =
                Convert.ToString(dr["NO_OF_VACANCIES"]);

            lblTotalVehicle.Text =
                Convert.ToString(dr["TOTAL_VEHICLES"]);

            lblRunning.Text =
                Convert.ToString(dr["ISSUED_VEHICLES"]);

            lblExpired.Text =
                Convert.ToString(dr["EXPIRED_PERMITS"]);

            lblCounterSignature.Text =
                Convert.ToString(dr["COUNTER_SIGNATURES"]);

            lblAuthorityName.Text =
                ddlAuthority.SelectedItem.Text;

            lblLastRefresh.Text =
                DateTime.Now.ToString("dd MMM yyyy hh:mm tt");
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    #endregion

    #region Vehicle Grid

    private void BindVehicleGrid()
    {
        try
        {
            string search = "";

            if (!string.IsNullOrWhiteSpace(txtVehicleNo.Text))
                search = txtVehicleNo.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtPermitNo.Text))
                search = txtPermitNo.Text.Trim();

            SqlParameter[] param =
            {
            new SqlParameter("@ACTION","VEHICLE_LIST"),
            new SqlParameter("@AUTHORITY",ddlAuthority.SelectedValue),
            new SqlParameter("@ROUTE_ID",Convert.ToInt32(ddlRoute.SelectedValue)),
            new SqlParameter("@SEARCH",search)
        };

            DataTable dt =
                SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(),
                    CommandType.StoredProcedure,
                    "SP_ROUTE_MONITORING",
                    param).Tables[0];

            GridView1.DataSource = dt;
            GridView1.DataBind();

            UpdateGridFooter(dt);

            lblLastRefresh.Text =
                DateTime.Now.ToString("dd MMM yyyy hh:mm tt");
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void UpdateGridFooter(DataTable dt)
    {
        if (dt == null)
            dt = new DataTable();

        lblShowingFrom.Text =
            dt.Rows.Count == 0 ? "0" : "1";

        lblShowingTo.Text =
            dt.Rows.Count.ToString();

        lblTotalRecords.Text =
            dt.Rows.Count.ToString();

        lblTotalRecordsFooter.Text =
            dt.Rows.Count.ToString();

        lblCurrentPage.Text =
            (GridView1.PageIndex + 1).ToString();

        lblTotalPages.Text =
            GridView1.PageCount == 0
            ? "1"
            : GridView1.PageCount.ToString();
    }

    #endregion

    protected void GridView1_PageIndexChanging(
    object sender,
    GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        BindVehicleGrid();
    }

    protected void ddlPageSize_SelectedIndexChanged(
    object sender,
    EventArgs e)
    {
        GridView1.PageSize =
            Convert.ToInt32(
                ddlPageSize.SelectedValue);

        GridView1.PageIndex = 0;

        BindVehicleGrid();
    }

    protected void btnPrevious_Click(
    object sender,
    EventArgs e)
    {
        if (GridView1.PageIndex > 0)
        {
            GridView1.PageIndex--;

            BindVehicleGrid();
        }
    }

    protected void btnNext_Click(
    object sender,
    EventArgs e)
    {
        if (GridView1.PageIndex < GridView1.PageCount - 1)
        {
            GridView1.PageIndex++;

            BindVehicleGrid();
        }
    }

    private void ClearRouteDetails()
    {
        // Route Details

        //lblSelectedRoute.Text = "-";

        lblRouteVia.Text = "-";

        lblRouteNumber.Text = "-";

        lblDistance.Text = "0 KM";

        lblVacancy.Text = "0";

        // KPI Cards

        lblTotalVehicle.Text = "0";

        lblRunning.Text = "0";

        lblExpired.Text = "0";

        lblCounterSignature.Text = "0";

        // Grid

        GridView1.DataSource = null;
        GridView1.DataBind();

        // Footer

        lblShowingFrom.Text = "0";

        lblShowingTo.Text = "0";

        lblTotalRecords.Text = "0";

        lblTotalRecordsFooter.Text = "0";

        lblCurrentPage.Text = "1";

        lblTotalPages.Text = "1";
    }

    protected void btnSearchVehicle_Click(
    object sender,
    EventArgs e)
    {
        try
        {
            GridView1.PageIndex = 0;

            BindVehicleGrid();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void btnResetVehicle_Click(
    object sender,
    EventArgs e)
    {
        try
        {
            txtVehicleNo.Text = "";

            txtPermitNo.Text = "";

            GridView1.PageIndex = 0;

            BindVehicleGrid();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void btnRefreshRoutes_Click(
    object sender,
    EventArgs e)
    {
        try
        {
            txtVehicleNo.Text = "";

            txtPermitNo.Text = "";

            ddlRoute.Items.Clear();

            ddlRoute.Items.Add(
                new ListItem("-- Select Route --", "")
            );

            ClearRouteDetails();

            if (ddlAuthority.SelectedIndex > 0)
            {
                BindRoutes();
            }

            lblLastRefresh.Text =
                DateTime.Now.ToString("dd MMM yyyy hh:mm tt");
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    protected void btnExportRoutes_Click(
    object sender,
    EventArgs e)
    {
        try
        {
            string search = "";

            if (!string.IsNullOrWhiteSpace(txtVehicleNo.Text))
                search = txtVehicleNo.Text.Trim();

            if (!string.IsNullOrWhiteSpace(txtPermitNo.Text))
                search = txtPermitNo.Text.Trim();

            SqlParameter[] param =
            {
            new SqlParameter("@ACTION","EXPORT"),
            new SqlParameter("@AUTHORITY",ddlAuthority.SelectedValue),
            new SqlParameter("@ROUTE_ID",ddlRoute.SelectedValue),
            new SqlParameter("@SEARCH",search)
        };

            DataTable dt =
                SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(),
                    CommandType.StoredProcedure,
                    "SP_ROUTE_MONITORING",
                    param).Tables[0];

            ExportToExcel(dt, "RouteMonitoring");
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private void ExportToExcel(
    DataTable dt,
    string fileName)
    {
        Response.Clear();

        Response.Buffer = true;

        Response.AddHeader(
            "content-disposition",
            "attachment;filename=" +
            fileName + ".xls");

        Response.Charset = "";

        Response.ContentType =
            "application/vnd.ms-excel";

        using (StringWriter sw =
            new StringWriter())
        {
            HtmlTextWriter hw =
                new HtmlTextWriter(sw);

            GridView gv =
                new GridView();

            gv.DataSource = dt;

            gv.DataBind();

            gv.RenderControl(hw);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();
        }
    }

    protected void GridView1_RowCommand(
    object sender,
    GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            string appId =
                e.CommandArgument.ToString();

            Response.Redirect(
               "VehicleDetailsPublic.aspx?AppId=" + appId +
               "&Authority=" + ddlAuthority.SelectedValue);
                }
    }

    protected void GridView1_RowDataBound(
    object sender,
    GridViewRowEventArgs e)
    {
        if (e.Row.RowType !=
            DataControlRowType.DataRow)
            return;

        Label lblStatus =
            e.Row.FindControl("lblStatus") as Label;

        if (lblStatus == null)
            return;

        switch (lblStatus.Text)
        {
            case "Issued":

                lblStatus.CssClass =
                    "badge bg-success";

                break;

            case "Pending":

                lblStatus.CssClass =
                    "badge bg-warning";

                break;

            case "Rejected":

                lblStatus.CssClass =
                    "badge bg-danger";

                break;

            default:

                lblStatus.CssClass =
                    "badge bg-secondary";

                break;
        }
    }

    public override void VerifyRenderingInServerForm(
    Control control)
    {

    }


}