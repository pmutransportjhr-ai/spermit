using SoftEra.JHT.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.HtmlControls;

public partial class JHTRANSPORT : System.Web.UI.MasterPage,
    System.Web.SessionState.IRequiresSessionState
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMainMenu();
        }
    }

    private void BindMainMenu()
    {
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("@MODE", "MENULIST");

        DataSet dsMenu = SQLHelper.ExecuteDataset(
            SQLHelper.GetConnString(),
            CommandType.StoredProcedure,
            "SP_GET_MENU_ITEMS_CLIENT",
            parameters
        );

        if (dsMenu == null || dsMenu.Tables.Count == 0)
            return;

        BindMenu(dsMenu.Tables[0]);
    }

    protected void BindMenu(DataTable dtMenu)
    {
        DataRow[] topMenus = dtMenu.Select("SUB_MENU='N'");
        string sMenu = "";

        foreach (DataRow top in topMenus)
        {
            sMenu += "<li>";

            string topUrl = Convert.ToString(top["URL"]);
            string topText = Convert.ToString(top["MENU_ITEM"]);

            if (string.IsNullOrEmpty(topUrl))
                sMenu += "<a href='#'>" + topText + "</a>";
            else
                sMenu += "<a href='" + topUrl + "'>" + topText + "</a>";

            DataRow[] firstChildren =
                dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(top["MENU_ID"]));

            if (firstChildren.Length > 0)
            {
                sMenu += "<ul>";

                foreach (DataRow fc in firstChildren)
                {
                    sMenu += "<li>";

                    string fcUrl = Convert.ToString(fc["URL"]);
                    string fcText = Convert.ToString(fc["MENU_ITEM"]);

                    if (string.IsNullOrEmpty(fcUrl))
                        sMenu += "<a href='#'>" + fcText + "</a>";
                    else
                        sMenu += "<a href='" + fcUrl + "'>" + fcText + "</a>";

                    DataRow[] secondChildren =
                        dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(fc["MENU_ID"]));

                    if (secondChildren.Length > 0)
                    {
                        sMenu += "<ul>";

                        foreach (DataRow sc in secondChildren)
                        {
                            sMenu += "<li>";

                            string scUrl = Convert.ToString(sc["URL"]);
                            string scText = Convert.ToString(sc["MENU_ITEM"]);

                            if (string.IsNullOrEmpty(scUrl))
                                sMenu += "<a href='#'>" + scText + "</a>";
                            else
                                sMenu += "<a href='" + scUrl + "'>" + scText + "</a>";

                            DataRow[] thirdChildren =
                                dtMenu.Select("PARENT_MENU_ID=" + Convert.ToString(sc["MENU_ID"]));

                            if (thirdChildren.Length > 0)
                            {
                                sMenu += "<ul>";

                                foreach (DataRow tc in thirdChildren)
                                {
                                    sMenu += "<li>";

                                    string tcText = Convert.ToString(tc["MENU_ITEM"]);
                                    string tcUrl = Convert.ToString(tc["URL"]);

                                    string q = HttpUtility.UrlEncode(
                                        CryptorEngine.Encrypt(
                                            Convert.ToString(tc["Q_STRING"]), true));

                                    if (string.IsNullOrEmpty(tcUrl))
                                        sMenu += "<a href='#'>" + tcText + "</a>";
                                    else
                                        sMenu += "<a href='" + tcUrl + "?q=" + q + "'>" + tcText + "</a>";

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

        HtmlGenericControl menu =
            this.FindControl("ulMenu") as HtmlGenericControl;

        if (menu != null)
            menu.InnerHtml = sMenu;
    }
}
