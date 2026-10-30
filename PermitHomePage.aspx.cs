using SoftEra.JHT.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;



public partial class PermitHomePage : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadNotice("0");
        }
    }


    protected void BindMenu(DataTable dtMenu)
    {
        DataRow[] TopMenu = dtMenu.Select("SUB_MENU='N'");
        String sMenu = "";

        foreach (DataRow drTopMenu in TopMenu)
        {



            if (Convert.ToString(drTopMenu["URL"]) == null || Convert.ToString(drTopMenu["URL"]) == "")
            {
                sMenu += "<li>";
                sMenu += "<a data-toggle='collapse' href='#dropdown-lvl" + Convert.ToString(drTopMenu["MENU_ID"]) + "'><span class='glyphicon glyphicon-off'></span>" + Convert.ToString(drTopMenu["MENU_ITEM"]) + "<span class='caret'></span></a>";
            }
            else
            {
                sMenu += "<li class='panel panel-default' id='dropdown'>";
                sMenu += "<a href='" + Convert.ToString(drTopMenu["URL"]) + "'>" + Convert.ToString(drTopMenu["MENU_ITEM"]) + "</a>";
            }

            DataRow[] fChldMenu = dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(drTopMenu["MENU_ID"]));
            if (fChldMenu.Length > 0)
            {
                sMenu += "<div id='dropdown-lvl" + Convert.ToString(drTopMenu["MENU_ID"]) + "' class='panel-collapse collapse'>";
                sMenu += "<div class='panel-body'>";
                sMenu += "<ul class='nav navbar-cav'>";
                foreach (DataRow drFChldMenu in fChldMenu)
                {


                    DataRow[] sChldMenu = dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(drFChldMenu["MENU_ID"]));

                    sMenu += "<li >";


                    if (Convert.ToString(drFChldMenu["URL"]) == null || Convert.ToString(drFChldMenu["URL"]) == "")
                    {
                        sMenu += "<a data-toggle='collapse' href='#dropdown-lvl" + Convert.ToString(drFChldMenu["MENU_ID"]) + "'><span class='glyphicon glyphicon-off'></span>" + Convert.ToString(drFChldMenu["MENU_ITEM"]) + "<span class='caret'></span></a>";
                    }
                    else
                    {

                        sMenu += "<a href='" + Convert.ToString(drFChldMenu["URL"]) + "' >" + Convert.ToString(drFChldMenu["MENU_ITEM"]) + "</a>";

                    }

                    if (sChldMenu.Length > 0)
                    {
                        sMenu += "<div id='dropdown-lvl" + Convert.ToString(drFChldMenu["MENU_ID"]) + "' class='panel-collapse collapse'>";
                        sMenu += "<div class='panel-body'>";
                        sMenu += "<ul class='nav navbar-cav'>";
                        foreach (DataRow drSChldMenu in sChldMenu)
                        {

                            //sMenu += "<li class=''>";
                            //if (Convert.ToString(drSChldMenu["URL"]) == null || Convert.ToString(drSChldMenu["URL"]) == "")
                            //{
                            //    sMenu += "<a href='#' >" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";
                            //}
                            //else
                            //{
                            //    sMenu += "<a href='" + Convert.ToString(drSChldMenu["URL"]) + "' >" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";
                            //}
                            //sMenu += "</li>";

                            DataRow[] tChldMenu = dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(drSChldMenu["MENU_ID"]));

                            sMenu += "<li >";


                            if (Convert.ToString(drSChldMenu["URL"]) == null || Convert.ToString(drSChldMenu["URL"]) == "")
                            {
                                sMenu += "<a data-toggle='collapse' href='#dropdown-lvl" + Convert.ToString(drSChldMenu["MENU_ID"]) + "'><span class='glyphicon glyphicon-off'></span>" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "<span class='caret'></span></a>";
                            }
                            else
                            {

                                sMenu += "<a href='" + Convert.ToString(drSChldMenu["URL"]) + "' >" + Convert.ToString(drSChldMenu["MENU_ITEM"]) + "</a>";

                            }

                            if (tChldMenu.Length > 0)
                            {
                                sMenu += "<div id='dropdown-lvl" + Convert.ToString(drSChldMenu["MENU_ID"]) + "' class='panel-collapse collapse'>";
                                sMenu += "<div class='panel-body'>";
                                sMenu += "<ul class='nav navbar-cav'>";
                                foreach (DataRow drTChldMenu in tChldMenu)
                                {
                                    string q = HttpUtility.UrlEncode(CryptorEngine.Encrypt(Convert.ToString(drTChldMenu["Q_STRING"]), true));

                                    sMenu += "<li class=''>";
                                    if (Convert.ToString(drTChldMenu["URL"]) == null || Convert.ToString(drTChldMenu["URL"]) == "")
                                    {
                                        sMenu += "<a data-toggle='collapse' href='#dropdown-lvl" + Convert.ToString(drTChldMenu["MENU_ID"]) + "'><span class='glyphicon glyphicon-off'></span>" + Convert.ToString(drTChldMenu["MENU_ITEM"]) + "<span class='caret'></span></a>";
                                    }
                                    else
                                    {
                                        sMenu += "<a href='" + Convert.ToString(drTChldMenu["URL"]) + "?q=" + q + "' >" + Convert.ToString(drTChldMenu["MENU_ITEM"]) + "</a>";
                                    }
                                    sMenu += "</li>";

                                }
                                sMenu += "</ul>";
                                sMenu += "</div>";
                                sMenu += "</div>";
                            }

                            sMenu += "</li>";



                        }
                        sMenu += "</ul>";
                        sMenu += "</div>";
                        sMenu += "</div>";
                    }

                    sMenu += "</li>";


                }
                sMenu += "</ul>";
                sMenu += "</div>";
                sMenu += "</div>";
            }
            sMenu += "</li>";

        }
        //phMenu.InnerHtml = sMenu;

    }

    public void loadNotice(string type)
    {
        SqlParameter[] parameters = new SqlParameter[8];
        parameters[0] = new SqlParameter("@NOTICE_CODE", DBNull.Value);
        parameters[1] = new SqlParameter("@NOTICE_DETAILS", DBNull.Value);
        parameters[2] = new SqlParameter("@TYPE", type.Trim());
        parameters[3] = new SqlParameter("@NOTICE_START_DATE", DBNull.Value);
        parameters[4] = new SqlParameter("@NOTICE_END_DATE", DBNull.Value);
        parameters[5] = new SqlParameter("@EMP_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[6] = new SqlParameter("@FILE_NAME", DBNull.Value);
        parameters[7] = new SqlParameter("@MODE", "DASHBOARD_LIST");

        DataSet ds = SQLHelper.ExecuteDataset(
            SQLHelper.GetConnString(),
            CommandType.StoredProcedure,
            "SP_OFFICE_NOTICE_AED",
            parameters
        );

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            dvNoticeDetails.InnerHtml = "<p class='text-muted'>No notices available</p>";
            return;
        }

        StringBuilder sb = new StringBuilder();

        sb.Append("<table class='table table-bordered table-hover notice-table'>");
        sb.Append("<thead style='background:#f5f5f5'>");
        sb.Append("<tr>");
        sb.Append("<th>Latest Notice</th>");
        sb.Append("<th style='text-align:right'>Date</th>");
        sb.Append("</tr>");
        sb.Append("</thead>");
        sb.Append("<tbody>");

      

        DateTime today = DateTime.Today;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            DateTime createdDate = Convert.ToDateTime(dr["created_on"]);
            bool isToday = createdDate.Date == today;
            bool isNew = createdDate.Date >= today.AddDays(-2);

            string filePath;

            if (Convert.ToString(dr["TYPE"]) == "RTA")
            {
                filePath = "download.aspx?path=RTAUploadedFiles&filename=" +
                           Convert.ToString(dr["NOTICE_FILE"]).Replace("@@---@@", "");
            }
            else
            {
                filePath = "download.aspx?path=STAUploadedFiles&filename=" +
                           Convert.ToString(dr["NOTICE_FILE"]).Replace("@@---@@", "");
            }

            sb.Append("<tr " + (isToday ? "style='background:#ecfdf5'" : "") + ">");

            sb.Append("<td>");

            sb.Append("<a href='" + filePath + "' target='_blank' style='font-weight:600;text-decoration:none;color:#14532d'>");

            sb.Append("<span class='glyphicon glyphicon-file text-danger'></span> ");
            sb.Append(Convert.ToString(dr["NOTICE_DETAILS"]));

            if (isNew)
            {
                sb.Append(" <span class='label label-success'>NEW</span>");
            }

            sb.Append("</a>");
            sb.Append("</td>");

            sb.Append("<td class='text-right text-muted'>");
            sb.Append(createdDate.ToString("dd/MM/yyyy"));
            sb.Append("</td>");

            sb.Append("</tr>");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");

        dvNoticeDetails.InnerHtml = sb.ToString();
    }

}