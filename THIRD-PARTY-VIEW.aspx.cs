using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Text;
using System.Configuration;
public partial class THIRD_PARTY_VIEW : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string regNumber = txtVReg.Text.Trim();
        string permitType = ddlTA.SelectedValue;

        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "THIRD_PERMIT_VIEW";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", regNumber);
                sCmd.Parameters.AddWithValue("@TYPE", permitType);
                if (txtVReg.Text.Substring(0, 2) == "JH")
                {
                    sCmd.Parameters.AddWithValue("@MODE", "CHECK_SELECT");
                }
                else
                {
                    sCmd.Parameters.AddWithValue("@MODE", "CHECK_SELECT_CS");
                }
                
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);

                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                dt = ds.Tables[0];
                if (Convert.ToString(dt.Rows[0]["MSG"]) != "Found")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]) + "');window.location ='THIRD-PARTY-VIEW.aspx'", true);
                    return;
                }
                dt1 = ds.Tables[1];
                dt2 = ds.Tables[2];

                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb = new StringBuilder();
                sb1.Append("<br/>");
                sb1.Append("<table class='table table-striped table-bordered table-hover'>");
                sb1.Append("<tbody>");
                if (Convert.ToString(dt.Rows[0]["MSG"]) == "Found")
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        sb1.Append("<tr>");
                        sb1.Append("<tr><td style='text-align:left;'><b>Permit Number :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td><td style='text-align:left;'><b>Valid Upto :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td></tr>");
                        sb1.Append("<tr><td style='text-align:left;'><b>Owner Name :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["OWNER_NAME"]) + "</td><td style='text-align:left;'><b>Present Address :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["PRESENT_ADDRESS"]) + "</td></tr>");
                        sb1.Append("<tr><td style='text-align:left;'><b>Vehicle Class Type :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["VEHICLE_CLASS_TYPE"]) + "</td><td style='text-align:left;'><b>Seating Capacity :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["SEATING_CAPACITY"]) + "</td></tr>");
                        sb1.Append("<tr><td style='text-align:left;'><b>Fitness Valid Up To :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["FITNESS_VALID_UPTO"]) + "</td><td style='text-align:left;'><b>Tax Paid Up To :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["TAX_PAID_UPTO"]) + "</td></tr>");
                        sb1.Append("<tr><td style='text-align:left;'><b>Maufacturing On :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["MANUFACTURING_DATE"]) + "</td>");//<td style='text-align:left;'><b>Countersignature No.:</b></td><td style='text-align:left;'>" + Convert.ToString(dr["COUNTER_SIGNATURE_NUMBER"]) + "</td></tr>");


                    }

                    sb1.Append("</tbody>");
                    sb1.Append("</table>");

                    dvNoticeDetails.InnerHtml = sb1.ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]) + "');window.location ='THIRD-PARTY-VIEW.aspx'", true);
                }


                if (dt2.Rows.Count > 0)
                {
                    sb.Append("<table class='table table-striped table-bordered table-hover'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='3' style='text-align:center;'> Up </th>");
                    sb.Append("<th>  </th>");
                    sb.Append("<th colspan='3' style='text-align:center;'> Down </th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th> Route </th>");
                    sb.Append("<th> Arrival </th>");
                    sb.Append("<th> Departure </th>");
                    sb.Append("<th>  </th>");
                    sb.Append("<th> Route </th>");
                    sb.Append("<th> Arrival </th>");
                    sb.Append("<th> Departure </th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tbody id='tbdRouteInfo'>");
                    int i = 0;
                    for (int k = 1; k <= dt2.Rows.Count; k++)
                    {
                        DataRow[] dr = dt2.Select("S_NO='" + k + "'");

                        sb.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                        sb.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                        sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "</label></td>");
                        sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "</label></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                        sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "</label></td>");
                        sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "</label></td>");
                    }

                    sb.Append("<tbody>");
                    sb.Append("</table>");
                    RouteTimeInfo.InnerHtml = sb.ToString();

                }




            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='third-party-view.aspx'", true);

            }
        }





    }
}