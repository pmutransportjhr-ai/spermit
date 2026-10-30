using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.IO;
using System.Configuration;
using System.Text;
using System.Web.Services;
public partial class STA_PENDENCY_REPORT : System.Web.UI.Page
{

    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["USER_TYPE"]) != "STA")
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            FillList();
        }
    }
    private static DataTable resultDT(string userLevel, string Reporttype)
    {
        DataTable dt = new DataTable();
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@USER_LEVEL", userLevel);
            cmd.Parameters.Add("@MODE", Reporttype);
            cmd.CommandText = "sp_STA_PERMTI_PENDENCY_REPORT";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sCon;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
        }
        return dt;
    }

    private void FillList()
    {
        DataTable dt = resultDT("ASSISTANT", "PERMIT_PENDENCY");
        StringBuilder sb = new StringBuilder();
        if (dt.Rows.Count > 0)
        {

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["STATE_NAME"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["NEW_PERMIT_CNT"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["TRANSFER_CNT"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["SURRENDER_CNT"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["REPLACEMENT_CNT"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["RENEWAL_CNT"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["DUPLICATE_CNT"]));
                sb.Append("</td>");
                sb.Append("</tr>");

            }
            tbdInterStatePermitList.InnerHtml = "";
            tbdInterStatePermitList.InnerHtml = sb.ToString();
        }


    }

    [WebMethod]
    public static string GetFilteredList(getFilterData objFilterData)
    {
        string userlevel= null;
        switch(objFilterData.userlevel){
            case "1":
                userlevel = "ASSISTANT";
                break;
            case "2":
                userlevel = "SO";
                break;
            case "3":
                userlevel = "JTC";
                break;
            case "4":
                userlevel = "Approver";
                break;

        }


        DataTable dtResult = resultDT(userlevel, objFilterData.type == "Permit" ? "PERMIT_PENDENCY" : "CS");
        StringBuilder sb = new StringBuilder();
        if (dtResult.Rows.Count > 0)
        {
            foreach (DataRow dr in dtResult.Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["STATE_NAME"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["NEW_PERMIT_CNT"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["TRANSFER_CNT"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["SURRENDER_CNT"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["REPLACEMENT_CNT"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["RENEWAL_CNT"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["DUPLICATE_CNT"]));
                sb.Append("</td>");
                sb.Append("</tr>");

            }

            return sb.ToString();
        }
        else
        {
            return "";
        }

    }

    public class getFilterData
    {
        public string userlevel { get; set; }
        public string type { get; set; }
    }
}