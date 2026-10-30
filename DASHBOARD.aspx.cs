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

public partial class DASHBOARD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
          
            DateTime fromDate = DateTime.Now.AddYears(-1);
            DateTime toDate = DateTime.Now;

            rdo1Year.Checked = true;

            LoadTopSummary(fromDate, toDate);

            // LOAD PENDENCY
            BindPendency();

            //Load Revenue
            //BindRevenue();

            BindAnalytics();

        }
    }

    /* =========================================================
      LOAD TOP SUMMARY
   ========================================================= */

    protected void LoadTopSummary(DateTime? fromDate, DateTime? toDate)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];

            if (fromDate.HasValue)
                param[0] = new SqlParameter("@FROM_DATE", fromDate.Value);
            else
                param[0] = new SqlParameter("@FROM_DATE", DBNull.Value);

            if (toDate.HasValue)
                param[1] = new SqlParameter("@TO_DATE", toDate.Value);
            else
                param[1] = new SqlParameter("@TO_DATE", DBNull.Value);

            DataSet ds = SQLHelper.ExecuteDataset(
                SQLHelper.GetConnString(),
                CommandType.StoredProcedure,
                "SP_DASHBOARD_TOP_SUMMARY",
                param
            );

            if (ds != null && ds.Tables.Count > 0)
            {
                // =====================================================
                // RTA PERMIT
                // =====================================================

                if (ds.Tables.Count > 0)
                {
                    rptRTAPermit.DataSource = ds.Tables[0];
                    rptRTAPermit.DataBind();
                }

                // =====================================================
                // RTA CS
                // =====================================================

                if (ds.Tables.Count > 1)
                {
                    rptRTACS.DataSource = ds.Tables[1];
                    rptRTACS.DataBind();
                }

                // =====================================================
                // SCHOOL BUS
                // =====================================================

                if (ds.Tables.Count > 2)
                {
                    rptSchoolBus.DataSource = ds.Tables[2];
                    rptSchoolBus.DataBind();
                }

                // =====================================================
                // STA PERMIT
                // =====================================================

                if (ds.Tables.Count > 3)
                {
                    rptSTAPermit.DataSource = ds.Tables[3];
                    rptSTAPermit.DataBind();
                }

                // =====================================================
                // STA CS
                // =====================================================

                if (ds.Tables.Count > 4)
                {
                    rptSTACS.DataSource = ds.Tables[4];
                    rptSTACS.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "alert",
                "alert('Error : " + ex.Message.Replace("'", "") + "');",
                true
            );
        }
    }

    /* =========================================================
      APPLY FILTER BUTTON
   ========================================================= */

    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        DateTime fromDate = DateTime.Now.AddDays(-15);
        DateTime toDate = DateTime.Now;

        try
        {
            // =====================================================
            // 15 DAYS
            // =====================================================

            if (rdo15Days.Checked)
            {
                fromDate = DateTime.Now.AddDays(-15);
                toDate = DateTime.Now;
            }

            // =====================================================
            // 30 DAYS
            // =====================================================

            else if (rdo30Days.Checked)
            {
                fromDate = DateTime.Now.AddDays(-30);
                toDate = DateTime.Now;
            }

            // =====================================================
            // 3 MONTHS
            // =====================================================

            else if (rdo3Months.Checked)
            {
                fromDate = DateTime.Now.AddMonths(-3);
                toDate = DateTime.Now;
            }

            // =====================================================
            // 1 YEAR
            // =====================================================

            else if (rdo1Year.Checked)
            {
                fromDate = DateTime.Now.AddYears(-1);
                toDate = DateTime.Now;
            }

            // =====================================================
            // CUSTOM DATE
            // =====================================================

            else if (rdoCustom.Checked)
            {
                if (!string.IsNullOrEmpty(txtFromDate.Text))
                    fromDate = Convert.ToDateTime(txtFromDate.Text);

                if (!string.IsNullOrEmpty(txtToDate.Text))
                    toDate = Convert.ToDateTime(txtToDate.Text);
            }

            // =====================================================
            // LOAD DATA
            // =====================================================

            LoadTopSummary(fromDate, toDate);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "alert",
                "alert('Filter Error : " + ex.Message.Replace("'", "") + "');",
                true
            );
        }
    }

    /* =========================================================
    RESET FILTER
 ========================================================= */

    protected void btnResetFilter_Click(object sender, EventArgs e)
    {
        rdo15Days.Checked = false;
        rdo30Days.Checked = false;
        rdo3Months.Checked = false;
        rdo1Year.Checked = true;
        rdoCustom.Checked = false;

        txtFromDate.Text = "";
        txtToDate.Text = "";

        LoadTopSummary(
              DateTime.Now.AddYears(-1),
              DateTime.Now
          );
    }

    /* =========================================================
   LOAD PENDENCY DASHBOARD
========================================================= */

    private void BindPendency()
    {

        DataSet ds =
        SQLHelper.ExecuteDataset(
            SQLHelper.GetConnString(),
            CommandType.StoredProcedure,
            "SP_GET_DASHBOARD_PENDENCY"
        );


        DataTable dt = ds.Tables[0];



        rptRTAPendency.DataSource =
            dt.Select("CATEGORY='RTA PERMIT'")
              .CopyToDataTable();

        rptRTAPendency.DataBind();




        rptRTACSPendency.DataSource =
            dt.Select("CATEGORY='RTA CS'")
              .CopyToDataTable();

        rptRTACSPendency.DataBind();




        rptSchoolBusPendency.DataSource =
            dt.Select("CATEGORY='SCHOOL BUS'")
              .CopyToDataTable();

        rptSchoolBusPendency.DataBind();




        rptSTAPendency.DataSource =
            dt.Select("CATEGORY='STA PERMIT'")
              .CopyToDataTable();

        rptSTAPendency.DataBind();




        rptSTACSPendency.DataSource =
            dt.Select("CATEGORY='STA CS'")
              .CopyToDataTable();

        rptSTACSPendency.DataBind();

    }

    protected void btnDownloadPendency_Click(
        object sender,
        EventArgs e)
    {
        try
        {

            DataSet ds = SQLHelper.ExecuteDataset(
                SQLHelper.GetConnString(),
                CommandType.StoredProcedure,
                "SP_GET_DASHBOARD_PENDENCY"
            );


            if (ds == null ||
                ds.Tables.Count == 0 ||
                ds.Tables[0].Rows.Count == 0)
            {
                return;
            }


            DataTable dt = ds.Tables[0];


            StringWriter sw = new StringWriter();


            HtmlTextWriter hw =
                new HtmlTextWriter(sw);



            hw.Write(
            "<h2 style='text-align:center'>" +
            "Pendency Report</h2>"
            );



            ExportSection(
                hw,
                dt,
                "RTA PERMIT",
                "RTA PERMIT DETAILS"
            );


            ExportSection(
                hw,
                dt,
                "RTA CS",
                "RTA COUNTER SIGNATURE DETAILS"
            );



            ExportSection(
                hw,
                dt,
                "SCHOOL BUS",
                "SCHOOL BUS PERMIT DETAILS"
            );



            ExportSection(
                hw,
                dt,
                "STA PERMIT",
                "STA PERMIT DETAILS"
            );



            ExportSection(
                hw,
                dt,
                "STA CS",
                "STA COUNTER SIGNATURE DETAILS"
            );





            Response.Clear();

            Response.Buffer = true;


            Response.AddHeader(
                "content-disposition",
                "attachment;filename=Pendency_Report.xls"
            );


            Response.Charset = "";


            Response.ContentType =
            "application/vnd.ms-excel";


            Response.Write(sw.ToString());


            Response.Flush();


            Response.End();


        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "alert",
            "alert('Download Error : "
            + ex.Message.Replace("'", "")
            + "');",
            true);

        }

    }

    private void ExportSection(
    HtmlTextWriter hw,
    DataTable dt,
    string category,
    string title)
    {


        DataRow[] rows =
            dt.Select(
            "CATEGORY='" + category + "'"
            );


        if (rows.Length == 0)
            return;



        DataTable section =
            rows.CopyToDataTable();



        hw.Write("<br/>");


        hw.Write(
        "<h3 style='background:#198754;color:white;padding:8px;'>"
        + title +
        "</h3>"
        );



        GridView gv =
            new GridView();


        gv.GridLines =
            GridLines.Both;


        gv.HeaderStyle.BackColor =
            System.Drawing.Color.LightGray;


        gv.DataSource =
            section;


        gv.DataBind();


        gv.RenderControl(hw);


        hw.Write("<br/>");

    }

    private void BindRevenue()
    {
        try
        {

            SqlParameter[] p =
            {
            new SqlParameter(
                "@FROM_DATE",
                string.IsNullOrEmpty(txtRevFrom.Text)
                ?
                (object)DBNull.Value
                :
                Convert.ToDateTime(txtRevFrom.Text)
            ),


            new SqlParameter(
                "@TO_DATE",
                string.IsNullOrEmpty(txtRevTo.Text)
                ?
                (object)DBNull.Value
                :
                Convert.ToDateTime(txtRevTo.Text)
            ),


            new SqlParameter(
                "@TYPE",
                string.IsNullOrEmpty(ddlRevenueType.SelectedValue)
                ?
                (object)DBNull.Value
                :
                ddlRevenueType.SelectedValue
            )

        };



            DataSet ds =
            SQLHelper.ExecuteDataset(
                SQLHelper.GetConnString(),
                CommandType.StoredProcedure,
                "SP_GET_DASHBOARD_REVENUE",
                p
            );



            if (ds == null ||
               ds.Tables.Count == 0 ||
               ds.Tables[0].Rows.Count == 0)
            {

                rptRevenue.DataSource = null;
                rptRevenue.DataBind();


                lblTotalRevenue.Text = "0.00";


                hdRevenueLabels.Value = "";
                hdRevenueValues.Value = "";


                return;

            }



            DataTable dt =
            ds.Tables[0];



            // Bind Cards

            rptRevenue.DataSource = dt;
            rptRevenue.DataBind();




            decimal totalRevenue = 0;


            List<string> labels =
                new List<string>();


            List<string> values =
                new List<string>();




            foreach (DataRow row in dt.Rows)
            {


                decimal amount = 0;


                if (row["TOTAL_AMOUNT"] != DBNull.Value)
                {
                    amount =
                    Convert.ToDecimal(
                        row["TOTAL_AMOUNT"]
                    );
                }



                totalRevenue += amount;




                // IMPORTANT:
                // No single quote here

                labels.Add(
                    row["CATEGORY"]
                    .ToString()
                    .Replace(",", " ")
                );



                values.Add(
                    amount.ToString(
                        System.Globalization.CultureInfo.InvariantCulture
                    )
                );


            }




            // Total Revenue

            lblTotalRevenue.Text =
            totalRevenue.ToString("N2");





            // Send data to JS chart

            hdRevenueLabels.Value =
            string.Join(",", labels);



            hdRevenueValues.Value =
            string.Join(",", values);



        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "revErr",
                "alert('Revenue Error : "
                + ex.Message.Replace("'", "")
                + "');",
                true
            );

        }

    }

    protected void btnRevenueFilter_Click(
        object sender,
        EventArgs e)
    {

        BindRevenue();

    }

    protected void btnRevenueExcel_Click(
    object sender,
    EventArgs e)
    {

    }

    private void BindAnalytics()
    {

        DataSet ds =
        SQLHelper.ExecuteDataset(
        SQLHelper.GetConnString(),
        CommandType.StoredProcedure,
        "SP_GET_DASHBOARD_ANALYTICS"
        );



        SetChartData(
        ds.Tables[0],
        "RTO_NAME",
        "TOTAL_PENDING",
        hdBarLabel,
        hdBarValue
        );


        SetChartData(
        ds.Tables[1],
        "MONTH_NAME",
        "TOTAL_APPLICATION",
        hdLineLabel,
        hdLineValue
        );


        SetChartData(
        ds.Tables[2],
        "CATEGORY",
        "TOTAL",
        hdPieLabel,
        hdPieValue
        );


        SetChartData(
        ds.Tables[3],
        "CATEGORY",
        "TOTAL_REVENUE",
        hdDonutLabel,
        hdDonutValue
        );


    }

    private void SetChartData(
            DataTable dt,
            string labelColumn,
            string valueColumn,
            HiddenField labels,
            HiddenField values)
    {

        List<string> l =
        new List<string>();

        List<string> v =
        new List<string>();


        foreach (DataRow r in dt.Rows)
        {

            l.Add(
            r[labelColumn].ToString()
            );


            v.Add(
            r[valueColumn].ToString()
            );

        }


        labels.Value =
        string.Join(",", l);


        values.Value =
        string.Join(",", v);

    }


}