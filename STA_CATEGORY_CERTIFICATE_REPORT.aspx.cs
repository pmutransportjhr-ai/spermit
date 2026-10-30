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
using System.Globalization;

public partial class STA_CATEGORY_CERTIFICATE_REPORT : System.Web.UI.Page
{
    String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
    public string FillInterStatePermitList { set; get; }

   
    protected void Page_Load(object sender, EventArgs e)
    {
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlDataAdapter adp = new SqlDataAdapter("select * from dbo.STA_COUNTER_SIGNATURE_APPLICATION_HDR", sCon);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["MOBILE_NUMBER"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["SEATING_CAPACITY"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["SLEEPER_CAPACITY"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["VEHICLE_CLASS_TYPE"]) + "</td>");

                    //sb.Append("<td>" + Convert.ToString(dr["OTHER"]) + "</td>");
                    sb.Append("<td><a class='btn btn-outline dark' href=\'STA_Category_Certificate_view.aspx?q=" + Convert.ToString(dr["APP_ID"]) + "\'>View</a></td>");
                    sb.Append("</tr>");
                    FillInterStatePermitList = sb.ToString();
                }
            }

        }

    }
}