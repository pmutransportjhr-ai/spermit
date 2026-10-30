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

public partial class JHTRANSPORT : System.Web.UI.MasterPage, System.Web.SessionState.IRequiresSessionState
{
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    SqlParameter[] parameters = new SqlParameter[1];
    //    parameters[0] = new SqlParameter("@MODE", "MENULIST");

    //    DataSet dsMenu = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_GET_MENU_ITEMS_CLIENT", parameters);
    //    DataTable dtMnu = new DataTable();
    //    dtMnu = dsMenu.Tables[0];
    //    BindMenu(dtMnu);


    //}

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
        }
    }

    //protected void BindMenu(DataTable dtMenu)
    //{
    //    string sMenu = "";

    //    DataRow[] topMenus = dtMenu.Select("SUB_MENU='N'");

    //    foreach (DataRow drTop in topMenus)
    //    {
    //        string menuId = drTop["MENU_ID"].ToString();
    //        string menuText = drTop["MENU_ITEM"].ToString();
    //        string url = drTop["URL"].ToString();

    //        DataRow[] childMenus = dtMenu.Select("PARENT_MENU_ID=" + menuId);

    //        // TOP MENU
    //        sMenu += "<li>";

    //        if (childMenus.Length > 0)
    //            sMenu += "<a href='#'>" + menuText + " <span class='menu-arrow'>▼</span></a>";
    //        else if (!string.IsNullOrEmpty(url))
    //            sMenu += "<a href='" + url + "'>" + menuText + "</a>";
    //        else
    //            sMenu += "<a href='#'>" + menuText + "</a>";

    //        // LEVEL 1 CHILD
    //        if (childMenus.Length > 0)
    //        {
    //            sMenu += "<ul>";

    //            foreach (DataRow drChild in childMenus)
    //            {
    //                string childId = drChild["MENU_ID"].ToString();
    //                string childText = drChild["MENU_ITEM"].ToString();
    //                string childUrl = drChild["URL"].ToString();

    //                DataRow[] subChild = dtMenu.Select("PARENT_MENU_ID=" + childId);

    //                sMenu += "<li>";

    //                if (subChild.Length > 0)
    //                    sMenu += "<a href='#'>" + childText + " ▶</a>";
    //                else if (!string.IsNullOrEmpty(childUrl))
    //                    sMenu += "<a href='" + childUrl + "'>" + childText + "</a>";
    //                else
    //                    sMenu += "<a href='#'>" + childText + "</a>";

    //                // LEVEL 2
    //                if (subChild.Length > 0)
    //                {
    //                    sMenu += "<ul>";

    //                    foreach (DataRow drSub in subChild)
    //                    {
    //                        string subId = drSub["MENU_ID"].ToString();
    //                        string subText = drSub["MENU_ITEM"].ToString();
    //                        string subUrl = drSub["URL"].ToString();

    //                        DataRow[] thirdChild = dtMenu.Select("PARENT_MENU_ID=" + subId);

    //                        sMenu += "<li>";

    //                        if (thirdChild.Length > 0)
    //                            sMenu += "<a href='#'>" + subText + " ▶</a>";
    //                        else if (!string.IsNullOrEmpty(subUrl))
    //                        {
    //                            string q = HttpUtility.UrlEncode(
    //                                CryptorEngine.Encrypt(Convert.ToString(drSub["Q_STRING"]), true)
    //                            );

    //                            sMenu += "<a href='" + subUrl + "?q=" + q + "'>" + subText + "</a>";
    //                        }
    //                        else
    //                            sMenu += "<a href='#'>" + subText + "</a>";

    //                        // LEVEL 3
    //                        if (thirdChild.Length > 0)
    //                        {
    //                            sMenu += "<ul>";

    //                            foreach (DataRow drThird in thirdChild)
    //                            {
    //                                string thirdText = drThird["MENU_ITEM"].ToString();
    //                                string thirdUrl = drThird["URL"].ToString();

    //                                string q = HttpUtility.UrlEncode(
    //                                    CryptorEngine.Encrypt(Convert.ToString(drThird["Q_STRING"]), true)
    //                                );

    //                                // Disable only Apply for Permit
    //                                if (thirdText.Trim().Equals("Apply for Permit",
    //                                    StringComparison.OrdinalIgnoreCase))
    //                                {
    //                                    sMenu += "<li><a href='javascript:void(0);' " +
    //                                             "style='pointer-events:none !important;" +
    //                                             "color:#bfbfbf !important;" +
    //                                             "background-color:#6b6b6b !important;" +
    //                                             "cursor:not-allowed !important;" +
    //                                             "opacity:0.7 !important;" +
    //                                             "display:block;'>" +
    //                                             thirdText +
    //                                             "</a></li>";
    //                                }
    //                                else
    //                                {
    //                                    sMenu += "<li><a href='" +
    //                                             thirdUrl + "?q=" + q +
    //                                             "'>" + thirdText + "</a></li>";
    //                                }
    //                            }

    //                            sMenu += "</ul>";
    //                        }

    //                        sMenu += "</li>";
    //                    }

    //                    sMenu += "</ul>";
    //                }

    //                sMenu += "</li>";
    //            }

    //            sMenu += "</ul>";
    //        }

    //        sMenu += "</li>";
    //    }

    //    ulMenu.InnerHtml = sMenu;
    //}

    protected void BindMenu(DataTable dtMenu)
    {
        DataRow[] TopMenu = dtMenu.Select("SUB_MENU='N'");
        String sMenu = "";

        foreach (DataRow drTopMenu in TopMenu)
        {


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

                            //sMenu += "<li class=''>";
                            //if (Convert.ToString(drSChldMenu["URL"]) == null || Convert.ToString(drSChldMenu["URL"]) == "")
                            //{
                            //    sMenu += "<a href='#' class='nav-link nav-toggle'>" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";
                            //}
                            //else
                            //{
                            //    sMenu += "<a href='" + Convert.ToString(drSChldMenu["URL"]) + "' class='nav-link nav-toggle'>" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";
                            //}
                            //sMenu += "</li>";

                            DataRow[] tChldMenu = dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(drSChldMenu["MENU_ID"]));
                            if (tChldMenu.Length > 0)
                            {
                                sMenu += "<li class='dropdown-submenu'>";
                            }
                            else
                            {
                                sMenu += "<li class='dropdown'>";
                            }

                            if (Convert.ToString(drSChldMenu["URL"]) == null || Convert.ToString(drSChldMenu["URL"]) == "")
                            {
                                sMenu += "<a href='#' class='nav-link nav-toggle'>" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";
                            }
                            else
                            {

                                sMenu += "<a href='" + Convert.ToString(drSChldMenu["URL"]) + "' class='nav-link nav-toggle'>" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";

                            }

                            if (tChldMenu.Length > 0)
                            {
                                sMenu += "<ul class='dropdown-menu'>";
                                foreach (DataRow drTChldMenu in tChldMenu)
                                {
                                    string q = HttpUtility.UrlEncode(CryptorEngine.Encrypt(Convert.ToString(drTChldMenu["Q_STRING"]), true));

                                    sMenu += "<li class=''>";
                                    if (Convert.ToString(drTChldMenu["URL"]) == null || Convert.ToString(drTChldMenu["URL"]) == "")
                                    {
                                        sMenu += "<a href='#' class='nav-link nav-toggle'>" + Convert.ToString(drTChldMenu["MENU_ITEM"]) + "</a>";
                                    }
                                    else
                                    {
                                        sMenu += "<a href='" + Convert.ToString(drTChldMenu["URL"]) + "?q=" + q + "' class='nav-link nav-toggle'>" + Convert.ToString(drTChldMenu["MENU_ITEM"]) + "</a>";
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