using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEra.JHT.Infrastructure;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class RTAMasterPage : System.Web.UI.MasterPage, System.Web.SessionState.IRequiresSessionState
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string EmpId = Convert.ToString(Session["EMPLOYEE_ID"]);
        string EmpName = Convert.ToString(Session["EMPLOYEE_NAME"]);
        //string EmpType = Convert.ToString(Session["EMP_TYPE"]);
        string UserRole = Convert.ToString(Session["USER_ROLE"]);
        string UserType = Convert.ToString(Session["USER_TYPE"]);
       

        if (EmpId == null || EmpId == string.Empty || EmpId == ""
            || EmpName == null || EmpName == string.Empty || EmpName == ""
            //  || EmpType == null || EmpType == string.Empty || EmpType == ""
            || UserRole == null || UserRole == string.Empty || UserRole == "" || UserType != "RTA")
        {
            Response.Redirect("Login.aspx");
            return;
        }

        HtmlGenericControl myname = (HtmlGenericControl)Page.Master.FindControl("username");
        myname.InnerText = "Welcome " + EmpName;

        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("@EmployeeId", EmpId);
        

        DataSet dsMenu = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_USER_MENU_ITEMS_LIST", parameters);
        DataTable dtMnu = new DataTable();
        dtMnu = dsMenu.Tables[0];
        BindMenu(dtMnu, UserRole);

    }
    protected void BindMenu(DataTable dtMenu, String sUserRole)
    {
        DataRow[] TopMenu = dtMenu.Select("SUB_MENU='N'");
        String sMenu = "";

        bool hasActiveCol = dtMenu.Columns.Contains("Active");

        foreach (DataRow drTopMenu in TopMenu)
        {
            if (hasActiveCol)
            {
                string active = Convert.ToString(drTopMenu["Active"]);
                if (!string.IsNullOrEmpty(active) && active != "Y" && active != "1" && active != "True")
                    continue;
            }

            string topMenuName = Convert.ToString(drTopMenu["MENU_ITEM"]).Trim();

            if (topMenuName == "Master" && sUserRole != "Super Admin")
            {
                continue;
            }

            if (topMenuName == "Edit Applications" && sUserRole != "Moderator")
            {
                continue;
            }

            if (topMenuName == "RTA Moderator" && sUserRole != "Moderator")
            {
                continue;
            }

            sMenu += "<li class='menu-dropdown classic-menu-dropdown'>";
            if (string.IsNullOrEmpty(Convert.ToString(drTopMenu["URL"])))
            {
                sMenu += "<a href='#'>" + topMenuName + "<span class='arrow'></span></a>";
            }
            else
            {
                sMenu += "<a href='" + Convert.ToString(drTopMenu["URL"]) + "'>" + topMenuName + "<span class='arrow'></span></a>";
            }

            DataRow[] fChldMenu = dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(drTopMenu["MENU_ID"]));
            if (fChldMenu.Length > 0)
            {
                sMenu += "<ul class='dropdown-menu pull-left'>";
                foreach (DataRow drFChldMenu in fChldMenu)
                {
                    if (hasActiveCol)
                    {
                        string childActive = Convert.ToString(drFChldMenu["Active"]);
                        if (!string.IsNullOrEmpty(childActive) && childActive != "Y" && childActive != "1" && childActive != "True")
                            continue;
                    }

                    string childMenuName = Convert.ToString(drFChldMenu["MENU_ITEM"]).Trim();
                    string childUrl = Convert.ToString(drFChldMenu["URL"]);

                    DataRow[] sChldMenu = dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(drFChldMenu["MENU_ID"]));
                    if (sChldMenu.Length > 0)
                    {
                        sMenu += "<li class='dropdown-submenu'>";
                    }
                    else
                    {
                        sMenu += "<li class='dropdown'>";
                    }

                    if (string.IsNullOrEmpty(childUrl))
                    {
                        sMenu += "<a href='#' class='nav-link nav-toggle'>" + childMenuName + "</a>";
                    }
                    else
                    {
                        sMenu += "<a href='" + childUrl + "' class='nav-link nav-toggle'>" + childMenuName + "</a>";
                    }

                    if (sChldMenu.Length > 0)
                    {
                        sMenu += "<ul class='dropdown-menu'>";
                        foreach (DataRow drSChldMenu in sChldMenu)
                        {
                            if (hasActiveCol)
                            {
                                string subActive = Convert.ToString(drSChldMenu["Active"]);
                                if (!string.IsNullOrEmpty(subActive) && subActive != "Y" && subActive != "1" && subActive != "True")
                                    continue;
                            }

                            string subMenuName = Convert.ToString(drSChldMenu["MENU_ITEM"]).Trim();
                            string subUrl = Convert.ToString(drSChldMenu["URL"]);

                            sMenu += "<li class=''>";
                            if (string.IsNullOrEmpty(subUrl))
                            {
                                sMenu += "<a href='#' class='nav-link nav-toggle'>" + subMenuName + "</a>";
                            }
                            else
                            {
                                sMenu += "<a href='" + subUrl + "' class='nav-link nav-toggle'>" + subMenuName + "</a>";
                            }
                            sMenu += "</li>";
                        }
                        sMenu += "</ul>";
                    }

                    sMenu += "</li>";
                }
                sMenu += "</ul>";
            }
            sMenu += "</li>";
        }

        HtmlGenericControl myMenuControl = (HtmlGenericControl)Page.Master.FindControl("ulMenu");
        if (myMenuControl != null)
        {
            myMenuControl.InnerHtml = sMenu;
        }
    }
}
