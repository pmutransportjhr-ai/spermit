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
public partial class STA_PERMIT_EXPIRY : System.Web.UI.Page
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
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
            parameters[1] = new SqlParameter("@MODE", "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");
            SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters);
            ddlStateList.Items.Clear();
            ddlStateList.Items.Add(new ListItem("All", "0"));
            while (dr.Read())
            {
                ddlStateList.Items.Add(new ListItem(Convert.ToString(dr["STATE_NAME"]), Convert.ToString(dr["STATE_CODE"])));

            }
          
            ddlStateList.SelectedValue = "0";

            FillList();
        }
    }
    private static DataTable resultDT(string stateCode)
    {
        DataTable dt = new DataTable();
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@state_code", stateCode);
            cmd.CommandText = "sp_get_expiry_report";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sCon;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
        }
        return dt;
    }

    private void FillList()
    {
        DataTable dt = resultDT("0");
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
                sb.Append(Convert.ToString(dr["REGISTRATION_NUMBER"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["PERMIT_NUMBER"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["OWNER_NAME"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["VALID_UP_TO"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["ROUTE"]));
                sb.Append("</td>");
                //sb.Append("<td>");
                //sb.Append(Convert.ToString(dr["DUPLICATE_CNT"]));
                //sb.Append("</td>");
                sb.Append("</tr>");

            }
            tbdInterStatePermitList.InnerHtml = "";
            tbdInterStatePermitList.InnerHtml = sb.ToString();
        }


    }

    [WebMethod]
    public static string GetFilteredList(getFilterexpiredData objFilterData)
    {
        DataTable dtResult = resultDT(objFilterData.state_code);
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
                sb.Append(Convert.ToString(dr["REGISTRATION_NUMBER"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["PERMIT_NUMBER"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["OWNER_NAME"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["VALID_UP_TO"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["ROUTE"]));
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

    public class getFilterexpiredData
    {
        public string state_code { get; set; }
       
    }
}