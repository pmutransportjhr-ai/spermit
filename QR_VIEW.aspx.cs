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
public partial class QR_VIEW : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    
        string regNumber = Request.QueryString["REG_NUMBER"];
        string permitType = Request.QueryString["TYPE"];

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
                sCmd.Parameters.AddWithValue("@MODE", "CHECK_SELECT");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);

                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                dt = ds.Tables[0];
                if (Convert.ToString(dt.Rows[0]["MSG"]) != "Found")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]) + "');window.location ='http://spermit.jharkhand.gov.in'", true);
                    return;
                }
                dt1 = ds.Tables[1];
                dt2 = ds.Tables[2];

                StringBuilder sb1 = new StringBuilder(); 
                StringBuilder sb = new StringBuilder();
                sb1.Append("<br/>");
                sb1.Append("<table class='table table-striped table-bordered table-hover'>");
                sb1.Append("<tbody>");
                if(Convert.ToString(dt.Rows[0]["MSG"]) == "Found")
                {
                    string csStatus = Convert.ToString(dt.Rows[0]["CS_STATUS"]);
                    string route = Convert.ToString(dt.Rows[0]["ROUTE"]);
                    if (csStatus == "")
                    {
                        lblTimeTableValidiyty.Text = "Time Table valid for 2 months";
                    }
                    else
                    {
                        lblTimeTableValidiyty.Text = "Time Table Approved till : " + Convert.ToString(dt.Rows[0]["VALID_UP_TO"]);
                    }
                    if (permitType == "RTA")
                    {
                        lblTimeTableValidiyty.Visible = false;
                    }
                    lblRoute.Text = route;
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb1.Append("<tr>");
                        sb1.Append("<tr><td style='text-align:left;'><b>Permit Number :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td><td style='text-align:left;'><b>Valid Upto :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td></tr>");
                        sb1.Append("<tr><td style='text-align:left;'><b>Owner Name :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["OWNER_NAME"]) + "</td><td style='text-align:left;'><b>Present Address :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["PRESENT_ADDRESS"]) + "</td></tr>");
                        sb1.Append("<tr><td style='text-align:left;'><b>Vehicle Class Type :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["VEHICLE_CLASS_TYPE"]) + "</td><td style='text-align:left;'><b>Seating Capacity :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["SEATING_CAPACITY"]) + "</td></tr>");
                        sb1.Append("<tr><td style='text-align:left;'><b>Fitness Valid Up To :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["FITNESS_VALID_UPTO"]) + "</td><td style='text-align:left;'><b>Tax Paid Up To :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["TAX_PAID_UPTO"]) + "</td></tr>");
                        sb1.Append("<tr><td style='text-align:left;'><b>Maufacturing On :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["MANUFACTURING_DATE"]) + "</td>");//<td style='text-align:left;'><b>Maufacturing On :</b></td><td style='text-align:left;'>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td></tr>");
                        
                      
                    }

                    sb1.Append("</tbody>");
                    sb1.Append("</table>");

                    dvNoticeDetails.InnerHtml = sb1.ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]) + "');window.location ='http://spermit.jharkhand.gov.in'", true);
                }


                if (dt1.Rows.Count > 0)
                {
                    for (int k = 1; k <= dt1.Rows.Count; k++)
                    {
                        if (dt2.Rows.Count > 0)
                        {
                            DataRow[] dr = dt2.Select("SEQUENCE='" + k + "'");

                            if (dr.Length > 0)
                            {
                                //Int32 length = dt2.Rows.Count - 1;
                                Int32 length = dr.Length - 1;
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
                                for (int i = 0; i < dr.Length; i++)
                                {


                                    if (i == 0)
                                    {
                                        sb.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "</label></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                        sb.Append("</tr>");
                                    }
                                    else if (i == length)
                                    {
                                        sb.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "</label></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("</tr>");

                                    }
                                    else
                                    {
                                        sb.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "</label></td>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "</label></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                        sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                        sb.Append("</tr>");
                                    }

                                }
                                sb.Append("<tbody>");
                                sb.Append("</table>");

                            }
                        }
                    }
                    RouteTimeInfo.InnerHtml = sb.ToString();

                }




            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='http://spermit.jharkhand.gov.in'", true);

            }
        }





    }
}