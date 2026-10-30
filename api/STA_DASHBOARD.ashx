<%@ WebHandler Language="C#" Class="STA_DASHBOARD" %>

using System;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json;
using System.Text;

public class STA_DASHBOARD : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    protected List<QueryString> QueryStr = new List<QueryString>();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string json = string.Empty;

        switch (context.Request.QueryString["caller"])
        {
            case "GetRevenueDetails":
                String sConnectionString3 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString3))
                {
                    try
                    {
                        
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "STA_SP_GET_DASHBOARD_DETAILS";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                        sCmd.Parameters.AddWithValue("@MODE", "REVENUE_DETAILS");
                        sCmd.Parameters.AddWithValue("@PERIOD", Convert.ToString(context.Request.QueryString["PERIOD"]));
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        DataTable dt1 = ds.Tables[1];
                        StringBuilder sb = new StringBuilder();
                        if (dt.Rows.Count > 0)
                        {
                            sb.Append("<table style='width:100%;' class='table table-bordered table-striped table-condensed flip-content' border='1'>");
                            sb.Append("<thead>");
                            sb.Append("<tr>");
                            sb.Append("<th>S.No</th>");
                            sb.Append("<th>Service Name</th>");
                            sb.Append("<th>Revenue Amount</th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");
                           
                            sb.Append("<tbody>");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["S_NO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["OPERATION"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["REVENUE_AMOUNT"]) + "</td>");
                                sb.Append("</tr>");
                            }
                            if(dt1.Rows.Count>0)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td></td>");
                                sb.Append("<td>Total Revenue</td>");
                                sb.Append("<td>" + Convert.ToString(dt1.Rows[0]["TOTAL"]) + "</td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            context.Response.Write("Success~" + sb.ToString());
                        }
                        else
                        {
                            context.Response.Write("No Data Found");
                        }

                    }
                    catch (Exception ex)
                    {
                        context.Response.Write(ex.Message);
                    }
                }
                break;
            case "GetPermitDetails":
                String sConnectionString4 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString4))
                {
                    try
                    {

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "STA_SP_GET_DASHBOARD_DETAILS";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                        sCmd.Parameters.AddWithValue("@MODE", "PERMIT_DETAILS");
                        sCmd.Parameters.AddWithValue("@PERIOD", Convert.ToString(context.Request.QueryString["PERIOD"]));
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        DataTable dt1 = ds.Tables[1];
                        StringBuilder sb = new StringBuilder();
                        if (dt.Rows.Count > 0)
                        {
                            sb.Append("<table style='width:100%;' class='table table-bordered table-striped table-condensed flip-content' border='1'>");
                            sb.Append("<thead>");
                            sb.Append("<tr>");
                            sb.Append("<th>S.No</th>");
                            sb.Append("<th>Permit Number</th>");
                            sb.Append("<th>Application Status</th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");

                            sb.Append("<tbody>");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["S_NO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["PERMIT_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["APPLICATION_STATUS"]) + "</td>");
                                sb.Append("</tr>");
                            }
                            if (dt1.Rows.Count > 0)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td></td>");
                                sb.Append("<td>Total Permit</td>");
                                sb.Append("<td>" + Convert.ToString(dt1.Rows[0]["CNT"]) + "</td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            context.Response.Write("Success~" + sb.ToString());
                        }
                        else
                        {
                            context.Response.Write("No Data Found");
                        }

                    }
                    catch (Exception ex)
                    {
                        context.Response.Write(ex.Message);
                    }
                }
                break;
            case "GetPendingAppDetails":
                String sConnectionString5 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString5))
                {
                    try
                    {

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "STA_SP_GET_DASHBOARD_DETAILS";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                        sCmd.Parameters.AddWithValue("@MODE", "PENDING_APPLICATIONS");
                        sCmd.Parameters.AddWithValue("@PERIOD", Convert.ToString(context.Request.QueryString["PERIOD"]));
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        DataTable dt1 = ds.Tables[1];
                        StringBuilder sb = new StringBuilder();
                        if (dt.Rows.Count > 0)
                        {
                            sb.Append("<table style='width:100%;' class='table table-bordered table-striped table-condensed flip-content' border='1'>");
                            sb.Append("<thead>");
                            sb.Append("<tr>");
                            sb.Append("<th>S.No</th>");
                            sb.Append("<th>Application Number</th>");
                            sb.Append("<th>Application Status</th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");

                            sb.Append("<tbody>");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["S_NO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["APPLICATION_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["APPLICATION_STATUS"]) + "</td>");
                                sb.Append("</tr>");
                            }
                            if (dt1.Rows.Count > 0)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td></td>");
                                sb.Append("<td>Total Application</td>");
                                sb.Append("<td>" + Convert.ToString(dt1.Rows[0]["CNT"]) + "</td>");
                                sb.Append("</tr>");
                            }
                            sb.Append("</tbody>");
                            sb.Append("</table>");
                            context.Response.Write("Success~" + sb.ToString());
                        }
                        else
                        {
                            context.Response.Write("No Data Found");
                        }

                    }
                    catch (Exception ex)
                    {
                        context.Response.Write(ex.Message);
                    }
                }
                break;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}