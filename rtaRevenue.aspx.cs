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
public partial class rtaRevenue : System.Web.UI.Page
{
    protected static string resultData { get; set; }

    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {

        
    }
    private static DataSet resultDT(getFilterData objfiterdata)
    {
        DataSet ds = new DataSet();
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@from_date", objfiterdata.start_date);
            cmd.Parameters.AddWithValue("@to_date", objfiterdata.todate);
            cmd.Parameters.AddWithValue("@mode", "get_report");

            cmd.CommandText = "sp_rta_revenue";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sCon;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            da.Fill(ds);
        }
        return ds;
    }

  

    [WebMethod]
    public static string GetFilteredList(getFilterData objFilterData)
    {
       DataSet dsResult  = resultDT(objFilterData);
       DataTable dtResult = dsResult.Tables[0];
       DataTable dtTotal = dsResult.Tables[1];
        StringBuilder sb = new StringBuilder();
        if (dtResult.Rows.Count > 0)
        {
            foreach (DataRow dr in dtResult.Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["RTO_NAME"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["Transaction Amount"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["Month Name"]));
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(Convert.ToString(dr["Year"]));
                sb.Append("</td>");
                sb.Append("</tr>");

            }

            if (dtTotal.Rows.Count > 0)
            {
                StringBuilder sbTotal = new StringBuilder();
                foreach (DataRow dr in dtTotal.Rows)
                {
                    sbTotal.Append("<tr>");
                    sbTotal.Append("<td>");
                    sbTotal.Append(Convert.ToString(dr["RTO_NAME"]));
                    sbTotal.Append("</td>");
                    sbTotal.Append("<td>");
                    sbTotal.Append(Convert.ToString(dr["Transaction Amount"]));
                    sbTotal.Append("</td>");
                    sbTotal.Append("</tr>");
                }
                resultData = sbTotal.ToString();
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
        public string start_date { get; set; }
        public string todate { get; set; }
       
    }
}