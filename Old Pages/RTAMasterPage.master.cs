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

        foreach (DataRow drTopMenu in TopMenu)
        {
            //if (Convert.ToInt32(drTopMenu["VISIBILITY"]) > 0 || (Convert.ToInt32(drTopMenu["VISIBILITY"]) == 0 && (sUserRole == "Admin" || sUserRole == "Super Admin")))
            //{
            if (Convert.ToInt32(drTopMenu["VISIBILITY"]) > 0 || (Convert.ToInt32(drTopMenu["VISIBILITY"]) == 0 && (sUserRole == "Super Admin")))
            {
                if (drTopMenu["MENU_ITEM"].ToString() == "Master" && sUserRole != "Super Admin")
                {
                    continue;

                }

                sMenu += "<li class='menu-dropdown classic-menu-dropdown'>";
                if (Convert.ToString(drTopMenu["URL"]) == null || Convert.ToString(drTopMenu["URL"]) == "")
                {
                    sMenu += "<a href='#'>" + Convert.ToString(drTopMenu["MENU_ITEM"]) + "<span class='arrow'></span></a>";
                }
                else
                {
                    sMenu += "<a href='" + Convert.ToString(drTopMenu["URL"]) + "'>" + Convert.ToString(drTopMenu["MENU_ITEM"]) + "<span class='arrow'></span></a>";
                }

                DataRow[] fChldMenu = dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(drTopMenu["MENU_ID"]));
                if (fChldMenu.Length > 0)
                {
                    sMenu += "<ul class='dropdown-menu pull-left'>";
                    foreach (DataRow drFChldMenu in fChldMenu)
                    {
                        if ((Convert.ToString(drFChldMenu["MENU_ITEM"]).Contains("Admin")))
                        {
                            if ("Super Admin" == "Super Admin")
                            {
                                //if (Convert.ToInt32(drFChldMenu["VISIBILITY"]) > 0 || (Convert.ToInt32(drFChldMenu["VISIBILITY"]) == 0 && (sUserRole == "Admin" || sUserRole == "Super Admin")))
                                //{
                                   if (Convert.ToInt32(drTopMenu["VISIBILITY"]) > 0 || (Convert.ToInt32(drTopMenu["VISIBILITY"]) == 0 && (sUserRole == "Super Admin")))
                                    {
                                    DataRow[] sChldMenu = dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(drFChldMenu["MENU_ID"]));
                                    if (sChldMenu.Length > 0)
                                    {
                                        sMenu += "<li class='dropdown-submenu'>";
                                    }
                                    else
                                    {
                                        sMenu += "<li class='dropdown'>";
                                    }

                                    if (Convert.ToString(drFChldMenu["URL"]) == null || Convert.ToString(drFChldMenu["URL"]) == "")
                                    {
                                        sMenu += "<a href='#' class='nav-link nav-toggle'>" + Convert.ToString(drFChldMenu["MENU_ITEM"]) + "</a>";
                                    }
                                    else
                                    {
                                        sMenu += "<a href='" + Convert.ToString(drFChldMenu["URL"]) + "' class='nav-link nav-toggle'>" + Convert.ToString(drFChldMenu["MENU_ITEM"]) + "</a>";
                                    }

                                    if (sChldMenu.Length > 0)
                                    {
                                        sMenu += "<ul class='dropdown-menu'>";
                                        foreach (DataRow drSChldMenu in sChldMenu)
                                        {
                                            
                                            if (Convert.ToInt32(drTopMenu["VISIBILITY"]) > 0 || (Convert.ToInt32(drTopMenu["VISIBILITY"]) == 0 && (sUserRole == "Super Admin")))
                                            {
                                                sMenu += "<li class=''>";
                                                if (Convert.ToString(drSChldMenu["URL"]) == null || Convert.ToString(drSChldMenu["URL"]) == "")
                                                {
                                                    sMenu += "<a href='#' class='nav-link nav-toggle'>" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";
                                                }
                                                else
                                                {
                                                    sMenu += "<a href='" + Convert.ToString(drSChldMenu["URL"]) + "' class='nav-link nav-toggle'>" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";
                                                }
                                                sMenu += "</li>";
                                            }
                                        }
                                        sMenu += "</ul>";
                                    }

                                    sMenu += "</li>";
                                }
                            }
                        }
                        else
                        {
                            //if (Convert.ToInt32(drFChldMenu["VISIBILITY"]) > 0 || (Convert.ToInt32(drFChldMenu["VISIBILITY"]) == 0 && (sUserRole == "Admin" || sUserRole == "Super Admin")))
                            //{
                            if (Convert.ToInt32(drTopMenu["VISIBILITY"]) > 0 || (Convert.ToInt32(drTopMenu["VISIBILITY"]) == 0 && (sUserRole == "Super Admin")))
                            {
                                DataRow[] sChldMenu = dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(drFChldMenu["MENU_ID"]));
                                if (sChldMenu.Length > 0)
                                {
                                    sMenu += "<li class='dropdown-submenu'>";
                                }
                                else
                                {
                                    sMenu += "<li class='dropdown'>";
                                }

                                if (Convert.ToString(drFChldMenu["URL"]) == null || Convert.ToString(drFChldMenu["URL"]) == "")
                                {
                                    sMenu += "<a href='#' class='nav-link nav-toggle'>" + Convert.ToString(drFChldMenu["MENU_ITEM"]) + "</a>";
                                }
                                else
                                {
                                    if (Convert.ToInt32(drFChldMenu["VISIBILITY"]) > 0)
                                    {
                                        sMenu += "<a href='" + Convert.ToString(drFChldMenu["URL"]) + "' class='nav-link nav-toggle'>" + Convert.ToString(drFChldMenu["MENU_ITEM"]) + "</a>";
                                    }
                                    else if(sUserRole == "Super Admin")
                                    {
                                        sMenu += "<a href='" + Convert.ToString(drFChldMenu["URL"]) + "' class='nav-link nav-toggle'>" + Convert.ToString(drFChldMenu["MENU_ITEM"]) + "</a>";
                                    }
                                }

                                if (sChldMenu.Length > 0)
                                {
                                    sMenu += "<ul class='dropdown-menu'>";
                                    foreach (DataRow drSChldMenu in sChldMenu)
                                    {
                                        if (Convert.ToInt32(drTopMenu["VISIBILITY"]) > 0 || (Convert.ToInt32(drTopMenu["VISIBILITY"]) == 0 && (sUserRole == "Super Admin")))
                                         {
                                            sMenu += "<li class=''>";
                                            if (Convert.ToString(drSChldMenu["URL"]) == null || Convert.ToString(drSChldMenu["URL"]) == "")
                                            {
                                                sMenu += "<a href='#' class='nav-link nav-toggle'>" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";
                                            }
                                            else
                                            {
                                                sMenu += "<a href='" + Convert.ToString(drSChldMenu["URL"]) + "' class='nav-link nav-toggle'>" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";
                                            }
                                            sMenu += "</li>";
                                        }
                                    }
                                    sMenu += "</ul>";
                                }

                                sMenu += "</li>";
                            }
                        }
                    }
                    sMenu += "</ul>";
                }
                sMenu += "</li>";
            }
        }

        HtmlGenericControl myMenuControl = (HtmlGenericControl)Page.Master.FindControl("ulMenu");
        if (myMenuControl != null)
        {
            myMenuControl.InnerHtml = sMenu;
        }
    }
}
