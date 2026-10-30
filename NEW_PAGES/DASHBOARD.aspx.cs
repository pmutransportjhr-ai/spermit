using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@MODE", "MENULIST");

            DataSet dsMenu = SQLHelper.ExecuteDataset(
                SQLHelper.GetConnString(),
                CommandType.StoredProcedure,
                "SP_GET_MENU_ITEMS_CLIENT",
                parameters
            );

            if (dsMenu != null && dsMenu.Tables.Count > 0)
            {
                BindMenu(dsMenu.Tables[0]);
            }

            // DEFAULT 1 YEAR FILTER
            DateTime fromDate = DateTime.Now.AddYears(-1);
            DateTime toDate = DateTime.Now;

            // CHECK 1 YEAR FILTER UI
            rdo1Year.Checked = true;

            // LOAD TOP SUMMARY
            LoadTopSummary(fromDate, toDate);

       
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

    protected void BindMenu(DataTable dtMenu)
    {
        string menuHtml = "<ul class='navbar-nav'>";

        DataRow[] topMenus = dtMenu.Select("SUB_MENU='N'");

        foreach (DataRow drTop in topMenus)
        {
            string menuId = drTop["MENU_ID"].ToString();
            string menuText = drTop["MENU_ITEM"].ToString();
            string url = drTop["URL"].ToString();

            DataRow[] childMenus = dtMenu.Select("PARENT_MENU_ID=" + menuId);

            // ================= TOP MENU =================
            if (childMenus.Length > 0)
            {
                menuHtml += "<li class='nav-item dropdown'>";
                menuHtml += "<a class='nav-link dropdown-toggle' href='#' data-bs-toggle='dropdown'>";
                menuHtml += menuText;
                menuHtml += "</a>";
                menuHtml += "<ul class='dropdown-menu'>";

                // ================= LEVEL 1 =================
                foreach (DataRow drChild in childMenus)
                {
                    string childId = drChild["MENU_ID"].ToString();
                    string childText = drChild["MENU_ITEM"].ToString();
                    string childUrl = drChild["URL"].ToString();

                    DataRow[] subChild = dtMenu.Select("PARENT_MENU_ID=" + childId);

                    if (subChild.Length > 0)
                    {
                        // 🔥 FIXED SUBMENU (NO dropdown-toggle here)
                        menuHtml += "<li class='dropdown-submenu'>";
                        menuHtml += "<a class='dropdown-item submenu-toggle' href='#'>";
                        menuHtml += childText;
                        menuHtml += "</a>";
                        menuHtml += "<ul class='dropdown-menu'>";

                        // ================= LEVEL 2 =================
                        foreach (DataRow drSub in subChild)
                        {
                            string subId = drSub["MENU_ID"].ToString();
                            string subText = drSub["MENU_ITEM"].ToString();
                            string subUrl = drSub["URL"].ToString();

                            DataRow[] thirdChild = dtMenu.Select("PARENT_MENU_ID=" + subId);

                            if (thirdChild.Length > 0)
                            {
                                menuHtml += "<li class='dropdown-submenu'>";
                                menuHtml += "<a class='dropdown-item submenu-toggle' href='#'>";
                                menuHtml += subText;
                                menuHtml += "</a>";
                                menuHtml += "<ul class='dropdown-menu'>";

                                foreach (DataRow drThird in thirdChild)
                                {
                                    string thirdText = drThird["MENU_ITEM"].ToString();
                                    string thirdUrl = drThird["URL"].ToString();

                                    string q = HttpUtility.UrlEncode(
                                        CryptorEngine.Encrypt(Convert.ToString(drThird["Q_STRING"]), true)
                                    );

                                    menuHtml += "<li>";
                                    menuHtml += "<a class='dropdown-item' href='" + thirdUrl + "?q=" + q + "'>";
                                    menuHtml += thirdText;
                                    menuHtml += "</a>";
                                    menuHtml += "</li>";
                                }

                                menuHtml += "</ul></li>";
                            }
                            else
                            {
                                string q = HttpUtility.UrlEncode(
                                    CryptorEngine.Encrypt(Convert.ToString(drSub["Q_STRING"]), true)
                                );

                                menuHtml += "<li>";
                                menuHtml += "<a class='dropdown-item' href='" + subUrl + "?q=" + q + "'>";
                                menuHtml += subText;
                                menuHtml += "</a>";
                                menuHtml += "</li>";
                            }
                        }

                        menuHtml += "</ul></li>";
                    }
                    else
                    {
                        // Normal child
                        menuHtml += "<li>";

                        if (!string.IsNullOrEmpty(childUrl))
                        {
                            menuHtml += "<a class='dropdown-item' href='" + childUrl + "'>";
                            menuHtml += childText;
                            menuHtml += "</a>";
                        }
                        else
                        {
                            menuHtml += "<a class='dropdown-item' href='#'>";
                            menuHtml += childText;
                            menuHtml += "</a>";
                        }

                        menuHtml += "</li>";
                    }
                }

                menuHtml += "</ul></li>";
            }
            else
            {
                // Simple menu
                menuHtml += "<li class='nav-item'>";
                menuHtml += "<a class='nav-link' href='" + url + "'>";
                menuHtml += menuText;
                menuHtml += "</a>";
                menuHtml += "</li>";
            }
        }

        menuHtml += "</ul>";

        ulMenu.InnerHtml = menuHtml;
    }

}