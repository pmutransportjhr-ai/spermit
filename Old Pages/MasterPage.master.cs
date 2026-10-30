using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SoftEra.JHT.Infrastructure;
using System.Data.SqlClient;

public partial class MasterPage : System.Web.UI.MasterPage, System.Web.SessionState.IRequiresSessionState
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string EmpId = Convert.ToString(Session["EMPLOYEE_ID"]);
        string EmpName = Convert.ToString(Session["EMPLOYEE_NAME"]);
        string UserRole = Convert.ToString(Session["USER_ROLE"]);
        string UserType = Convert.ToString(Session["USER_TYPE"]);

        if (string.IsNullOrEmpty(EmpId) || string.IsNullOrEmpty(EmpName)
            || string.IsNullOrEmpty(UserRole) || UserType != "STA")
        {
            Response.Redirect("Login.aspx");
            return;
        }

        HtmlGenericControl myname = (HtmlGenericControl)Page.Master.FindControl("username");
        if (myname != null)
            myname.InnerText = "Welcome " + EmpName;

        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EmployeeId", EmpId);
        parameters[1] = new SqlParameter("@USER_TYPE", Convert.ToString(Session["USER_TYPE"]));

        DataSet dsMenu = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(),
            CommandType.StoredProcedure, "STA_USER_MENU_ITEMS_LIST", parameters);

        BindMenu(dsMenu.Tables[0], UserRole);
    }


    // protected void BindMenu(DataTable dtMenu, string sUserRole)
    // {
    //     DataRow[] TopMenu = dtMenu.Select("SUB_MENU='N'");
    //     string sMenu = "";

    //     foreach (DataRow drTop in TopMenu)
    //     {
    //         int visibility = Convert.ToInt32(drTop["VISIBILITY"]);
    //         string title = drTop["MENU_ITEM"].ToString().Trim().ToUpper();

    //         // Hide Master menu for non–Super Admin
    //         if (title == "MASTER" && sUserRole != "Super Admin")
    //             continue;

    //         // Hide Mobile Update for non–Super Admin
    //         if (title == "MOBILE UPDATE" && sUserRole != "Super Admin")
    //             continue;

    //         if (visibility > 0 || (visibility == 0 && sUserRole == "Super Admin"))
    //         {
    //             sMenu += "<li class='menu-dropdown classic-menu-dropdown'>";

    //             string url = Convert.ToString(drTop["URL"]);
    //             sMenu += "<a href='" + (url == "" ? "#" : url) + "'>" +
    //                      drTop["MENU_ITEM"] + "<span class='arrow'></span></a>";

    //             // Load Children
    //             DataRow[] childMenus = dtMenu.Select("PARENT_MENU_ID=" + drTop["MENU_ID"]);
    //             if (childMenus.Length > 0)
    //             {
    //                 sMenu += "<ul class='dropdown-menu pull-left'>";
    //                 foreach (DataRow drChild in childMenus)
    //                 {
    //                     AddChildMenu(dtMenu, drChild, ref sMenu, sUserRole);
    //                 }
    //                 sMenu += "</ul>";
    //             }

    //             sMenu += "</li>";
    //         }
    //     }

    //     HtmlGenericControl menuControl = (HtmlGenericControl)FindControl("ulMenu");
    //     if (menuControl != null)
    //         menuControl.InnerHtml = sMenu;
    // }

protected void BindMenu(DataTable dtMenu, string sUserRole)
{
    DataRow[] TopMenu = dtMenu.Select("SUB_MENU='N'");
    string sMenu = "";

    foreach (DataRow drTop in TopMenu)
    {
        int visibility = Convert.ToInt32(drTop["VISIBILITY"]);
        string title = drTop["MENU_ITEM"].ToString().Trim().ToUpper();
        string url = Convert.ToString(drTop["URL"]);

        // ============================
        // 🔒 GUEST RESTRICTION
        // ============================
        if (sUserRole.ToUpper() == "GUEST")
        {
            if (title != "MASTER")
                continue;
        }
        else
        {
            if (title == "MASTER" && sUserRole != "Super Admin")
                continue;

            if (title == "MOBILE UPDATE" && sUserRole != "Super Admin")
                continue;
        }

        if (visibility > 0 ||
            (visibility == 0 && sUserRole == "Super Admin") ||
            sUserRole.ToUpper() == "GUEST")
        {
            sMenu += "<li class='menu-dropdown classic-menu-dropdown'>";

            sMenu += "<a href='" + (url == "" ? "#" : url) + "'>" +
                     drTop["MENU_ITEM"] + "<span class='arrow'></span></a>";

            // Load children
            DataRow[] childMenus = dtMenu.Select("PARENT_MENU_ID=" + drTop["MENU_ID"]);

            if (childMenus.Length > 0)
            {
                sMenu += "<ul class='dropdown-menu pull-left'>";

                foreach (DataRow drChild in childMenus)
                {
                    AddChildMenu(dtMenu, drChild, ref sMenu, sUserRole);
                }

                sMenu += "</ul>";
            }

            sMenu += "</li>";
        }
    }

    HtmlGenericControl menuControl = (HtmlGenericControl)FindControl("ulMenu");
    if (menuControl != null)
        menuControl.InnerHtml = sMenu;
}


   private void AddChildMenu(DataTable dtMenu, DataRow drChild, ref string sMenu, string sUserRole)
{
    int visibility = Convert.ToInt32(drChild["VISIBILITY"]);
    string title = drChild["MENU_ITEM"].ToString().Trim().ToUpper();
    string url = Convert.ToString(drChild["URL"]);

    // ============================
    // 🔒 GUEST LOGIC
    // ============================
    if (sUserRole.ToUpper() == "GUEST")
    {
        if (title != "OFFICE NOTICE")
            return;
    }
    else
    {
        // Super Admin only restrictions
        if (title == "MASTER" && sUserRole != "Super Admin") return;
        if (title == "MOBILE UPDATE" && sUserRole != "Super Admin") return;
        if (title == "EDIT PERMIT DETAILS" && sUserRole != "Super Admin") return;
        if (title == "EDIT COUNTER SIGNATURE" && sUserRole != "Super Admin") return;
    }

    if (visibility > 0 ||
        (visibility == 0 && sUserRole == "Super Admin") ||
        sUserRole.ToUpper() == "GUEST")
    {
        DataRow[] subChild = dtMenu.Select("PARENT_MENU_ID=" + drChild["MENU_ID"]);

        // If has further children
        if (subChild.Length > 0)
        {
            sMenu += "<li class='dropdown-submenu'>";
            sMenu += "<a href='#' class='nav-link nav-toggle'>" +
                     drChild["MENU_ITEM"] + "</a>";

            sMenu += "<ul class='dropdown-menu'>";

            foreach (DataRow drSub in subChild)
            {
                AddChildMenu(dtMenu, drSub, ref sMenu, sUserRole);
            }

            sMenu += "</ul></li>";
        }
        else
        {
            sMenu += "<li>";
            sMenu += "<a href='" + (url == "" ? "#" : url) + "'>" +
                     drChild["MENU_ITEM"] + "</a>";
            sMenu += "</li>";
        }
    }
}
}
