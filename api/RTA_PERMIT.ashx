<%@ WebHandler Language="C#" Class="RTA_PERMIT" %>

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

public class RTA_PERMIT : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

   protected List<QueryString> QueryStr = new List<QueryString>();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string json = string.Empty;
        
        switch (context.Request.QueryString["caller"])
        {
            case "GetCSRegDetails":
                String sConnectionString3 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString3))
                {
                    try
                    {
                        String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);

                        foreach (String str in queryString.Split(new Char[] { '&' }))
                        {
                            QueryString qs = new QueryString();
                            qs.Name = str.Split(new Char[] { '=' })[0];
                            qs.Value = str.Split(new Char[] { '=' })[1];
                            QueryStr.Add(qs);
                        }

                        QueryString IDx = QueryStr.Where(l => l.Name == "REGNO").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "RTA_COUNTER_SIGNATURE_APPLICATION_APPROVAL";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "REGISTRATION_NUMBER_HISTORY");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);




                        DataTable dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();

                        if (dt.Rows.Count > 0)
                        {
                            sb.Append("<table class='table table-striped table-hover table-bordered'>");
                            sb.Append("<thead>");
                            sb.Append("<tr>");
                            sb.Append("<th>S. No.</th>");
                            sb.Append("<th>Registration No.</th>");
                            sb.Append("<th>Service Name</th>");
                            sb.Append("<th>Action on</th>");
                            sb.Append("<th>Action by</th>");
                            sb.Append("<th>Designation</th>");
                            sb.Append("<th>Comments</th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");
                            sb.Append("<tbody>");


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["S_NO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["REGISTRATION_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["APPLICATION_STATUS"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["CREATED_ON"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["ACTION_BY"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["USER_LABEL"]) + "</td>");
                                if (Convert.ToString(dt.Rows[i]["COMMENTS"]) == "" || Convert.ToString(dt.Rows[i]["COMMENTS"]) == null)
                                {
                                    sb.Append("<td></td>");
                                }
                                else
                                {
                                    sb.Append("<td><a onclick=\"javascript:fnRegComments('" + Convert.ToString(dt.Rows[i]["COMMENTS"]) + "');\">Comments</a></td>");

                                }
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
            case "GetRegDetails":
                String sConnectionString2 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString2))
                {
                    try
                    {
                        String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);

                        foreach (String str in queryString.Split(new Char[] { '&' }))
                        {
                            QueryString qs = new QueryString();
                            qs.Name = str.Split(new Char[] { '=' })[0];
                            qs.Value = str.Split(new Char[] { '=' })[1];
                            QueryStr.Add(qs);
                        }

                        QueryString IDx = QueryStr.Where(l => l.Name == "REGNO").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "RTA_PERMIT_APPLICATION_APPROVAL";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "REGISTRATION_NUMBER_HISTORY");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);




                        DataTable dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();

                        if (dt.Rows.Count > 0)
                        {
                            sb.Append("<table class='table table-striped table-hover table-bordered'>");
                            sb.Append("<thead>");
                            sb.Append("<tr>");
                            sb.Append("<th>S. No.</th>");
                            sb.Append("<th>Registration No.</th>");
                            sb.Append("<th>Service Name</th>");
                            sb.Append("<th>Action on</th>");
                            sb.Append("<th>Action by</th>");
                            sb.Append("<th>Designation</th>");
                            sb.Append("<th>Comments</th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");
                            sb.Append("<tbody>");

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["S_NO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["REGISTRATION_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["APPLICATION_STATUS"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["CREATED_ON"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["ACTION_BY"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["USER_LABEL"]) + "</td>");
                                if (Convert.ToString(dt.Rows[i]["COMMENTS"]) == "" || Convert.ToString(dt.Rows[i]["COMMENTS"]) == null)
                                {
                                    sb.Append("<td></td>");
                                }
                                else
                                {
                                    sb.Append("<td><a onclick=\"javascript:fnRegComments('" + Convert.ToString(dt.Rows[i]["COMMENTS"]) + "');\">Comments</a></td>");

                                }
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
            case "GetRegDetailsContract":
                
                String sConnectionString4 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString4))
                {
                    try
                    {
                        String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);

                        foreach (String str in queryString.Split(new Char[] { '&' }))
                        {
                            QueryString qs = new QueryString();
                            qs.Name = str.Split(new Char[] { '=' })[0];
                            qs.Value = str.Split(new Char[] { '=' })[1];
                            QueryStr.Add(qs);
                        }

                        QueryString IDx = QueryStr.Where(l => l.Name == "REGNO").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "RTA_CONTRACT_PERMIT_APPLICATION_APPROVAL";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "REGISTRATION_NUMBER_HISTORY");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);




                        DataTable dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();

                        if (dt.Rows.Count > 0)
                        {
                            sb.Append("<table class='table table-striped table-hover table-bordered'>");
                            sb.Append("<thead>");
                            sb.Append("<tr>");
                            sb.Append("<th>S. No.</th>");
                            sb.Append("<th>Registration No.</th>");
                            sb.Append("<th>Service Name</th>");
                            sb.Append("<th>Action on</th>");
                            sb.Append("<th>Action by</th>");
                            sb.Append("<th>Designation</th>");
                            sb.Append("<th>Comments</th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");
                            sb.Append("<tbody>");


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["S_NO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["REGISTRATION_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["APPLICATION_STATUS"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["CREATED_ON"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["ACTION_BY"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["USER_LABEL"]) + "</td>");
                                if (Convert.ToString(dt.Rows[i]["COMMENTS"]) == "" || Convert.ToString(dt.Rows[i]["COMMENTS"]) == null)
                                {
                                    sb.Append("<td></td>");
                                }
                                else
                                {
                                    sb.Append("<td><a onclick=\"javascript:fnRegComments('" + Convert.ToString(dt.Rows[i]["COMMENTS"]) + "');\">Comments</a></td>");

                                }
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
            case "GetRegDetailsGoods":
                String sConnectionString5 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString5))
                {
                    try
                    {
                        String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);

                        foreach (String str in queryString.Split(new Char[] { '&' }))
                        {
                            QueryString qs = new QueryString();
                            qs.Name = str.Split(new Char[] { '=' })[0];
                            qs.Value = str.Split(new Char[] { '=' })[1];
                            QueryStr.Add(qs);
                        }

                        QueryString IDx = QueryStr.Where(l => l.Name == "REGNO").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "RTA_GOODS_PERMIT_APPLICATION_APPROVAL";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "REGISTRATION_NUMBER_HISTORY");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);




                        DataTable dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();

                        if (dt.Rows.Count > 0)
                        {
                            sb.Append("<table class='table table-striped table-hover table-bordered'>");
                            sb.Append("<thead>");
                            sb.Append("<tr>");
                            sb.Append("<th>S. No.</th>");
                            sb.Append("<th>Registration No.</th>");
                            sb.Append("<th>Service Name</th>");
                            sb.Append("<th>Action on</th>");
                            sb.Append("<th>Action by</th>");
                            sb.Append("<th>Designation</th>");
                            sb.Append("<th>Comments</th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");
                            sb.Append("<tbody>");


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["S_NO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["REGISTRATION_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["APPLICATION_STATUS"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["CREATED_ON"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["ACTION_BY"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["USER_LABEL"]) + "</td>");
                                if (Convert.ToString(dt.Rows[i]["COMMENTS"]) == "" || Convert.ToString(dt.Rows[i]["COMMENTS"]) == null)
                                {
                                    sb.Append("<td></td>");
                                }
                                else
                                {
                                    sb.Append("<td><a onclick=\"javascript:fnRegComments('" + Convert.ToString(dt.Rows[i]["COMMENTS"]) + "');\">Comments</a></td>");

                                }
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
            case "GetRegDetailsAuto":
                String sConnectionString6 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString6))
                {
                    try
                    {
                        String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);

                        foreach (String str in queryString.Split(new Char[] { '&' }))
                        {
                            QueryString qs = new QueryString();
                            qs.Name = str.Split(new Char[] { '=' })[0];
                            qs.Value = str.Split(new Char[] { '=' })[1];
                            QueryStr.Add(qs);
                        }

                        QueryString IDx = QueryStr.Where(l => l.Name == "REGNO").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "RTA_AUTO_PERMIT_APPLICATION_APPROVAL";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "REGISTRATION_NUMBER_HISTORY");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);




                        DataTable dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();

                        if (dt.Rows.Count > 0)
                        {
                            sb.Append("<table class='table table-striped table-hover table-bordered'>");
                            sb.Append("<thead>");
                            sb.Append("<tr>");
                            sb.Append("<th>S. No.</th>");
                            sb.Append("<th>Registration No.</th>");
                            sb.Append("<th>Service Name</th>");
                            sb.Append("<th>Action on</th>");
                            sb.Append("<th>Action by</th>");
                            sb.Append("<th>Designation</th>");
                            sb.Append("<th>Comments</th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");
                            sb.Append("<tbody>");


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["S_NO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["REGISTRATION_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["APPLICATION_STATUS"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["CREATED_ON"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["ACTION_BY"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["USER_LABEL"]) + "</td>");
                                if (Convert.ToString(dt.Rows[i]["COMMENTS"]) == "" || Convert.ToString(dt.Rows[i]["COMMENTS"]) == null)
                                {
                                    sb.Append("<td></td>");
                                }
                                else
                                {
                                    sb.Append("<td><a onclick=\"javascript:fnRegComments('" + Convert.ToString(dt.Rows[i]["COMMENTS"]) + "');\">Comments</a></td>");

                                }
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
            case "GetRegDetailsSchoolBus":
                String sConnectionString7 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString7))
                {
                    try
                    {
                        String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);

                        foreach (String str in queryString.Split(new Char[] { '&' }))
                        {
                            QueryString qs = new QueryString();
                            qs.Name = str.Split(new Char[] { '=' })[0];
                            qs.Value = str.Split(new Char[] { '=' })[1];
                            QueryStr.Add(qs);
                        }

                        QueryString IDx = QueryStr.Where(l => l.Name == "REGNO").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "RTA_SCHOOL_BUS_PERMIT_APPLICATION_APPROVAL";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "REGISTRATION_NUMBER_HISTORY");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);




                        DataTable dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();

                        if (dt.Rows.Count > 0)
                        {
                            sb.Append("<table class='table table-striped table-hover table-bordered'>");
                            sb.Append("<thead>");
                            sb.Append("<tr>");
                            sb.Append("<th>S. No.</th>");
                            sb.Append("<th>Registration No.</th>");
                            sb.Append("<th>Service Name</th>");
                            sb.Append("<th>Action on</th>");
                            sb.Append("<th>Action by</th>");
                            sb.Append("<th>Designation</th>");
                            sb.Append("<th>Comments</th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");
                            sb.Append("<tbody>");

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["S_NO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["REGISTRATION_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["APPLICATION_STATUS"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["CREATED_ON"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["ACTION_BY"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["USER_LABEL"]) + "</td>");
                                if (Convert.ToString(dt.Rows[i]["COMMENTS"]) == "" || Convert.ToString(dt.Rows[i]["COMMENTS"]) == null)
                                {
                                    sb.Append("<td></td>");
                                }
                                else
                                {
                                    sb.Append("<td><a onclick=\"javascript:fnRegComments('" + Convert.ToString(dt.Rows[i]["COMMENTS"]) + "');\">Comments</a></td>");

                                }
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
                
            case "GetRTOList":
                try
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@EMPLOYEE_ID", HttpContext.Current.Session["EMPLOYEE_ID"].ToString());
                    parameters[1] = new SqlParameter("@MODE", "RTO_LIST");
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", parameters);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        json = JsonConvert.SerializeObject(ds);
                        context.Response.ContentType = "application/json";
                        context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.Write(json);



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
                break;

            case "GetRTARouteDetails":
                String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString))
                {
                    try
                    {
                        String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);

                        foreach (String str in queryString.Split(new Char[] { '&' }))
                        {
                            QueryString qs = new QueryString();
                            qs.Name = str.Split(new Char[] { '=' })[0];
                            qs.Value = str.Split(new Char[] { '=' })[1];
                            QueryStr.Add(qs);
                        }

                        QueryString IDx = QueryStr.Where(l => l.Name == "ROUTE_ID").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "RTA_PERMIT_APPLY_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@ROUTE_ID", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "ROUTEDETAILS");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);




                        DataTable dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();

                        if (dt.Rows.Count > 0)
                        {
                            int No = Convert.ToInt32(context.Request.QueryString["q1"]);
                            Int32 length = dt.Rows.Count - 1;
                            for (int k = 1; k <= No; k++)
                            {

                                sb.Append("<br />");
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
                                sb.Append("<tbody id='tbdRouteInfo_" + k + "'>");
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("</tr>");
                                    }
                                    else if (i == length)
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("</tr>");

                                    }
                                    else
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("</tr>");
                                    }

                                }
                                sb.Append("<tbody>");
                                sb.Append("</table>");
                            }
                            sb.Append("<input  type='text' style='display:none' value='" + No + "'  id='txtNoRoundTrip' />");
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
            case "GetRTARouteDetails1":
                String sConnectionString10 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString10))
                {
                    try
                    {
                        String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);

                        foreach (String str in queryString.Split(new Char[] { '&' }))
                        {
                            QueryString qs = new QueryString();
                            qs.Name = str.Split(new Char[] { '=' })[0];
                            qs.Value = str.Split(new Char[] { '=' })[1];
                            QueryStr.Add(qs);
                        }

                        QueryString IDx = QueryStr.Where(l => l.Name == "ROUTE_ID").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "RTA_PERMIT_APPLY_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@ROUTE_ID", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "ROUTEDETAILS");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);




                        DataTable dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();

                        if (dt.Rows.Count > 0)
                        {
                            int No = Convert.ToInt32(context.Request.QueryString["q1"]);
                            Int32 length = dt.Rows.Count - 1;
                            for (int k = 1; k <= No; k++)
                            {

                                sb.Append("<br />");
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
                                sb.Append("<tbody id='tbdRouteInfo_" + k + "'>");
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("</tr>");
                                    }
                                    else if (i == length)
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("</tr>");

                                    }
                                    else
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' />&nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("</tr>");
                                    }

                                }
                                sb.Append("<tbody>");
                                sb.Append("</table>");
                            }
                            sb.Append("<input  type='text' style='display:none' value='" + No + "'  id='txtNoRoundTrip' />");
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
            case "GetDetailsByRTO":
                try
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                    //    context.Response.Write("Success~" + sb.ToString());
                    String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                    using (SqlConnection sCon = new SqlConnection(ConnectionString))
                    {
                        String rto = queryString;

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SCREEN_RTA_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", 0);
                        sCmd.Parameters.AddWithValue("@RTO", rto);
                        sCmd.Parameters.AddWithValue("@MODE", "PERMIT_DETAIL_BY_RTO");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();
                         sb.Append("<thead>");
                        sb.Append("<tr>");
                        sb.Append("<th>Permit No</th>");
                        sb.Append("<th>Owner Name </th>");
                        sb.Append("<th>Registration No </th>");
                        sb.Append("<th>Valid Upto</</th>");
                        sb.Append("<th> Permit Type </th>");
                        sb.Append("<th> View </th>");
                        sb.Append("<th> Edit </th>");
                        sb.Append("</tr>");
                        sb.Append("</thead>");
                        sb.Append("<tbody id='tblPermitView'>");


                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");


                                sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");

                                string strDate = Convert.ToDateTime(dr["VALID_UP_TO"]).ToString("dd-MM-yyyy");
                                
                                sb.Append("<td>" + strDate+ "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterViewRTA('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">View</a></td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterEditRTA('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">Edit</a></td>");
                                

                                sb.Append("</tr>");
                            }
                            sb.Append("<tbody>");
                          
                            context.Response.Write("Success~" + sb.ToString());

                        }
                        else
                        {

                            context.Response.Write("No Data Found");
                        }
                    }


                }
                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    context.Response.Write("Messqge");


                }
                break;
            case "GetGoodsDetailsByRTO":
                try
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                    //    context.Response.Write("Success~" + sb.ToString());
                    String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                    using (SqlConnection sCon = new SqlConnection(ConnectionString))
                    {
                        String rto = queryString;

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SCREEN_RTA_GOODS_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", 0);
                        sCmd.Parameters.AddWithValue("@RTO", rto);
                        sCmd.Parameters.AddWithValue("@MODE", "GOODS_PERMIT_DETAIL_BY_RTO");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<thead>");
                        sb.Append("<tr>");
                        sb.Append("<th>Permit No</th>");
                        sb.Append("<th>Owner Name </th>");
                        sb.Append("<th>Registration No </th>");
                        sb.Append("<th>Valid Upto</</th>");
                        sb.Append("<th> Permit Type </th>");
                        sb.Append("<th> View </th>");
                        sb.Append("<th> Edit </th>");
                        sb.Append("</tr>");
                        sb.Append("</thead>");
                        sb.Append("<tbody id='tblPermitView'>");


                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");


                                sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");

                                string strDate = Convert.ToDateTime(dr["VALID_UP_TO"]).ToString("dd-MM-yyyy");

                                sb.Append("<td>" + strDate + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterViewRTAGoods('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">View</a></td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterEditRTAGoods('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">Edit</a></td>");


                                sb.Append("</tr>");
                            }
                            sb.Append("<tbody>");

                            context.Response.Write("Success~" + sb.ToString());

                        }
                        else
                        {

                            context.Response.Write("No Data Found");
                        }
                    }


                }
                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    context.Response.Write("Messqge");


                }
                break;
            case "GetContractDetailsByRTO":
                try
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                    //    context.Response.Write("Success~" + sb.ToString());
                    String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                    using (SqlConnection sCon = new SqlConnection(ConnectionString))
                    {
                        String rto = queryString;

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SCREEN_RTA_CONTRACT_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", 0);
                        sCmd.Parameters.AddWithValue("@RTO", rto);
                        sCmd.Parameters.AddWithValue("@MODE", "CONTRACT_PERMIT_DETAIL_BY_RTO");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<thead>");
                        sb.Append("<tr>");
                        sb.Append("<th>Permit No</th>");
                        sb.Append("<th>Owner Name </th>");
                        sb.Append("<th>Registration No </th>");
                        sb.Append("<th>Valid Upto</</th>");
                        sb.Append("<th> Permit Type </th>");
                        sb.Append("<th> View </th>");
                        sb.Append("<th> Edit </th>");
                        sb.Append("</tr>");
                        sb.Append("</thead>");
                        sb.Append("<tbody id='tblPermitView'>");


                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");


                                sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");

                                string strDate = Convert.ToDateTime(dr["VALID_UP_TO"]).ToString("dd-MM-yyyy");

                                sb.Append("<td>" + strDate + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterViewRTAContract('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">View</a></td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterEditRTAContract('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">Edit</a></td>");


                                sb.Append("</tr>");
                            }
                            sb.Append("<tbody>");

                            context.Response.Write("Success~" + sb.ToString());

                        }
                        else
                        {

                            context.Response.Write("No Data Found");
                        }
                    }


                }
                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    context.Response.Write("Messqge");


                }
                break;
            case "GetAutoDetailsByRTO":
                try
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                    //    context.Response.Write("Success~" + sb.ToString());
                    String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                    using (SqlConnection sCon = new SqlConnection(ConnectionString))
                    {
                        String rto = queryString;

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SCREEN_RTA_AUTO_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", 0);
                        sCmd.Parameters.AddWithValue("@RTO", rto);
                        sCmd.Parameters.AddWithValue("@MODE", "AUTO_PERMIT_DETAIL_BY_RTO");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<thead>");
                        sb.Append("<tr>");
                        sb.Append("<th>Permit No</th>");
                        sb.Append("<th>Owner Name </th>");
                        sb.Append("<th>Registration No </th>");
                        sb.Append("<th>Valid Upto</</th>");
                        sb.Append("<th> Permit Type </th>");
                        sb.Append("<th> View </th>");
                        sb.Append("<th> Edit </th>");
                        sb.Append("</tr>");
                        sb.Append("</thead>");
                        sb.Append("<tbody id='tblPermitView'>");


                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");


                                sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");

                                string strDate = Convert.ToDateTime(dr["VALID_UP_TO"]).ToString("dd-MM-yyyy");

                                sb.Append("<td>" + strDate + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterViewRTAAuto('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">View</a></td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterEditRTAAuto('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">Edit</a></td>");


                                sb.Append("</tr>");
                            }
                            sb.Append("<tbody>");

                            context.Response.Write("Success~" + sb.ToString());

                        }
                        else
                        {

                            context.Response.Write("No Data Found");
                        }
                    }


                }
                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    context.Response.Write("Messqge");


                }
                break;
            case "GetSchoolDetailsByRTO":
                try
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                    //    context.Response.Write("Success~" + sb.ToString());
                    String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                    using (SqlConnection sCon = new SqlConnection(ConnectionString))
                    {
                        String rto = queryString;

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SCREEN_RTA_SCHOOL_BUS_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", 0);
                        sCmd.Parameters.AddWithValue("@RTO", rto);
                        sCmd.Parameters.AddWithValue("@MODE", "SCHOOL_BUS_PERMIT_DETAIL_BY_RTO");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<thead>");
                        sb.Append("<tr>");
                        sb.Append("<th>Permit No</th>");
                        sb.Append("<th>Owner Name </th>");
                        sb.Append("<th>Registration No </th>");
                        sb.Append("<th>Valid Upto</</th>");
                        sb.Append("<th> Permit Type </th>");
                        sb.Append("<th> View </th>");
                        sb.Append("<th> Edit </th>");
                        sb.Append("</tr>");
                        sb.Append("</thead>");
                        sb.Append("<tbody id='tblPermitView'>");


                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");


                                sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");

                                string strDate = Convert.ToDateTime(dr["VALID_UP_TO"]).ToString("dd-MM-yyyy");

                                sb.Append("<td>" + strDate + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterViewRTASchool('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">View</a></td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterEditRTASchool('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">Edit</a></td>");


                                sb.Append("</tr>");
                            }
                            sb.Append("<tbody>");

                            context.Response.Write("Success~" + sb.ToString());

                        }
                        else
                        {

                            context.Response.Write("No Data Found");
                        }
                    }


                }
                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    context.Response.Write("Messqge");


                }
                break;
            case "GetRTARejMeetingMarked":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_PERMIT_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PermitType);
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "LISTSO");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {

                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Permit Type</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Application Status</th>");
                                sb.Append("<th>View</th>");
                                sb.Append("<th>Assistant Status</th>");
                                sb.Append("<th>Assistant Comment</th>");
                                sb.Append("<th>SO Status</th>");
                                sb.Append("<th>SO Comment</th>");
                                sb.Append("<th colspan='3' >Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody>");
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);

                                    sb.Append("<tr>");
                                    sb.Append("<td id=" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    if (Convert.ToString(dr["APPLICATION_STATUS"]) == "Objection")
                                    {
                                        sb.Append("<td><a href=\"javascript:fnObjectionModalView('" + CryptorEngine.Encrypt(Convert.ToString(dr["REGISTRATION_NUMBER"]), true) + "')\">" + Convert.ToString(dr["APPLICATION_STATUS"]) + " (" + Convert.ToString(dr["COUNT_OBJECTION"]) + ")</a></td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                                    }



                                    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_STATUS"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_STATUS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                                    {
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkRTAMeetingApp' class='checkRTAMeetingApproved form-control' onclick = \"javascript:checkRTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Meeting</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkRTAMeetingRej' class='checkRTAMeetingRejected form-control' onclick = \"javascript:checkRTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkRTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");

                                    }
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;
            case "GetRTAMeetingMarked":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_PERMIT_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "MARKED_VIEW");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {
                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Marked on</th>");
                                sb.Append("<th colspan='3'>Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody id= 'rtaApproveRejecttbl'>");
                                foreach (DataRow dr in dt.Rows)
                                {

                                    sb.Append("<tr>");
                                    sb.Append("<td id =" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToDateTime(Convert.ToString(dr["MEETING_MARKED_ON"])).ToString("dd-MM-yyyy") + "</td>");
                                    sb.Append("<td><input type='checkbox' id='checkRTAMeetingApp' class='checkRTAMeetingApproved form-control' onclick = \"javascript:checkRTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Verified</td>");
                                    sb.Append("<td><input type='checkbox' id='checkRTAMeetingRej' class='checkRTAMeetingRejected form-control' onclick = \"javascript:checkRTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                    sb.Append("<td><input type='checkbox' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkRTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;
            case "GetObjectionDetailbyReg":
                {

                    try
                    {
                        String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_PERMIT_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@APP_ID", 0);
                            sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", queryString);
                            sCmd.Parameters.AddWithValue("@MODE", "GET_OBJCTION_DETAIL_BY_REGNO");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {

                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Objection on Registration No.</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Permit No.</th>");
                                sb.Append("<th>Application Status</th>");
                                sb.Append("<th>View</th>");                                
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody>");

                                foreach (DataRow dr in dt.Rows)
                                {

                                    string q = HttpUtility.UrlEncode(CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]) + "&TBL_MODE=PERMIT", true));

                                    sb.Append("<tr>");
                                    sb.Append("<td id =" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OBJECTION_ON_REGISTRATION_NO"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OBJECTOR_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OBJECTOR_REGISTRATION_NUMBER"]) + "</td>");

                                    sb.Append("<td>" + Convert.ToString(dr["OBJECTOR_PERMIT_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                                    sb.Append("<td><a class='btn btn-outline dark'  href=\"RTA-PERMIT-OBJECTION-VIEW.aspx?q=" + q + "\" target='_blank'>View</a></td>");


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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);

                    }
                }
                break;
            case "GetRTAMeetingMarked_Transfer":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_TRANSFER_PERMIT_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "MARKED_VIEW");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {
                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Marked on</th>");
                                sb.Append("<th colspan='3'>Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody id= 'jtcApproveRejecttbl'>");
                                foreach (DataRow dr in dt.Rows)
                                {

                                    sb.Append("<tr>");
                                    sb.Append("<td id =" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToDateTime(Convert.ToString(dr["MEETING_MARKED_ON"])).ToString("dd-MM-yyyy") + "</td>");
                                    sb.Append("<td><input type='checkbox' id='checkSTAMeetingApp' class='checkSTAMeetingApproved form-control' onclick = \"javascript:checkSTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Verified</td>");
                                    sb.Append("<td><input type='checkbox' id='checkSTAMeetingRej' class='checkSTAMeetingRejected form-control' onclick = \"javascript:checkSTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                    sb.Append("<td><input type='checkbox' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkSTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;

            case "GetRTARejMeetingMarked_Transfer":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_TRANSFER_PERMIT_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PermitType);
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "LISTSO");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {

                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Permit Type</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Application Status</th>");
                                sb.Append("<th>View</th>");
                                sb.Append("<th>Assistant Status</th>");
                                sb.Append("<th>Assistant Comment</th>");
                                sb.Append("<th>SO Status</th>");
                                sb.Append("<th>SO Comment</th>");
                                sb.Append("<th colspan='3' >Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody>");
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);

                                    sb.Append("<tr>");
                                    sb.Append("<td id=" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    //if (Convert.ToString(dr["APPLICATION_STATUS"]) == "Objection")
                                    //{
                                    //    sb.Append("<td><a href=\"javascript:fnObjectionModalView('" + CryptorEngine.Encrypt(Convert.ToString(dr["REGISTRATION_NUMBER"]), true) + "')\">" + Convert.ToString(dr["APPLICATION_STATUS"]) + " (" + Convert.ToString(dr["COUNT_OBJECTION"]) + ")</a></td>");
                                    //}
                                    //else
                                    //{
                                        sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                                    //}



                                    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_STATUS"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_STATUS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                                    {
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkSTAMeetingApp' class='checkSTAMeetingApproved form-control' onclick = \"javascript:checkSTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Meeting</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkSTAMeetingRej' class='checkSTAMeetingRejected form-control' onclick = \"javascript:checkSTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkSTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");

                                    }
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;
            case "GetRTAMeetingMarked_PermitRenewal":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_RENEWAL_OF_PERMIT_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "MARKED_VIEW");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {
                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Marked on</th>");
                                sb.Append("<th colspan='3'>Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody id= 'jtcApproveRejecttbl'>");
                                foreach (DataRow dr in dt.Rows)
                                {

                                    sb.Append("<tr>");
                                    sb.Append("<td id =" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToDateTime(Convert.ToString(dr["MEETING_MARKED_ON"])).ToString("dd-MM-yyyy") + "</td>");
                                    sb.Append("<td><input type='checkbox' id='checkSTAMeetingApp' class='checkSTAMeetingApproved form-control' onclick = \"javascript:checkSTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Verified</td>");
                                    sb.Append("<td><input type='checkbox' id='checkSTAMeetingRej' class='checkSTAMeetingRejected form-control' onclick = \"javascript:checkSTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                    sb.Append("<td><input type='checkbox' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkSTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;

            case "GetRTARejMeetingMarked_PermitRenewal":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_RENEWAL_OF_PERMIT_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PermitType);
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "LISTSO");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {

                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Permit Type</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Application Status</th>");
                                sb.Append("<th>View</th>");
                                sb.Append("<th>Assistant Status</th>");
                                sb.Append("<th>Assistant Comment</th>");
                                sb.Append("<th>SO Status</th>");
                                sb.Append("<th>SO Comment</th>");
                                sb.Append("<th colspan='3' >Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody>");
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);

                                    sb.Append("<tr>");
                                    sb.Append("<td id=" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                                    


                                    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_STATUS"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_STATUS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                                    {
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkSTAMeetingApp' class='checkSTAMeetingApproved form-control' onclick = \"javascript:checkSTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Meeting</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkSTAMeetingRej' class='checkSTAMeetingRejected form-control' onclick = \"javascript:checkSTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkSTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");

                                    }
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;
            case "CSGetRTARejMeetingMarked":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_COUNTER_SIGNATURE_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PermitType);
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "LISTSO");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {

                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Permit Type</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Application Status</th>");
                                sb.Append("<th>View</th>");
                                sb.Append("<th>Assistant Status</th>");
                                sb.Append("<th>Assistant Comment</th>");
                                sb.Append("<th>SO Status</th>");
                                sb.Append("<th>SO Comment</th>");
                                sb.Append("<th colspan='3' >Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody>");
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);

                                    sb.Append("<tr>");
                                    sb.Append("<td id=" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    if (Convert.ToString(dr["APPLICATION_STATUS"]) == "Objection")
                                    {
                                        sb.Append("<td><a href=\"javascript:fnObjectionModalView('" + CryptorEngine.Encrypt(Convert.ToString(dr["REGISTRATION_NUMBER"]), true) + "')\">" + Convert.ToString(dr["APPLICATION_STATUS"]) + " (" + Convert.ToString(dr["COUNT_OBJECTION"]) + ")</a></td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                                    }



                                    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_STATUS"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_STATUS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                                    {
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkRTAMeetingApp' class='checkRTAMeetingApproved form-control' onclick = \"javascript:checkRTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Meeting</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkRTAMeetingRej' class='checkRTAMeetingRejected form-control' onclick = \"javascript:checkRTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkRTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");

                                    }
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;
            case "CSGetRTAMeetingMarked":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_COUNTER_SIGNATURE_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "MARKED_VIEW");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {
                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Marked on</th>");
                                sb.Append("<th colspan='3'>Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody id= 'rtaApproveRejecttbl'>");
                                foreach (DataRow dr in dt.Rows)
                                {

                                    sb.Append("<tr>");
                                    sb.Append("<td id =" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToDateTime(Convert.ToString(dr["MEETING_MARKED_ON"])).ToString("dd-MM-yyyy") + "</td>");
                                    sb.Append("<td><input type='checkbox' id='checkRTAMeetingApp' class='checkRTAMeetingApproved form-control' onclick = \"javascript:checkRTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Verified</td>");
                                    sb.Append("<td><input type='checkbox' id='checkRTAMeetingRej' class='checkRTAMeetingRejected form-control' onclick = \"javascript:checkRTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                    sb.Append("<td><input type='checkbox' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkRTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;
            case "CSGetObjectionDetailbyReg":
                {

                    try
                    {
                        String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_COUNTER_SIGNATURE_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@APP_ID", 0);
                            sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", queryString);
                            sCmd.Parameters.AddWithValue("@MODE", "GET_OBJCTION_DETAIL_BY_REGNO");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {

                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Objection on Registration No.</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Permit No.</th>");
                                sb.Append("<th>Application Status</th>");
                                sb.Append("<th>View</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody>");

                                foreach (DataRow dr in dt.Rows)
                                {

                                    string q = HttpUtility.UrlEncode(CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]) + "&TBL_MODE=PERMIT", true));

                                    sb.Append("<tr>");
                                    sb.Append("<td id =" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OBJECTION_ON_REGISTRATION_NO"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OBJECTOR_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OBJECTOR_REGISTRATION_NUMBER"]) + "</td>");

                                    sb.Append("<td>" + Convert.ToString(dr["OBJECTOR_PERMIT_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                                    sb.Append("<td><a class='btn btn-outline dark'  href=\"RTA-COUNTER-SIGNATURE-OBJECTION-VIEW.aspx?q=" + q + "\" target='_blank'>View</a></td>");


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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);

                    }
                }
                break;
            case "CSGetRTARejMeetingMarked_Transfer":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PermitType);
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "LISTSO");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {

                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Permit Type</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Application Status</th>");
                                sb.Append("<th>View</th>");
                                sb.Append("<th>Assistant Status</th>");
                                sb.Append("<th>Assistant Comment</th>");
                                sb.Append("<th>SO Status</th>");
                                sb.Append("<th>SO Comment</th>");
                                sb.Append("<th colspan='3' >Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody>");
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);

                                    sb.Append("<tr>");
                                    sb.Append("<td id=" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    //if (Convert.ToString(dr["APPLICATION_STATUS"]) == "Objection")
                                    //{
                                    //    sb.Append("<td><a href=\"javascript:fnObjectionModalView('" + CryptorEngine.Encrypt(Convert.ToString(dr["REGISTRATION_NUMBER"]), true) + "')\">" + Convert.ToString(dr["APPLICATION_STATUS"]) + " (" + Convert.ToString(dr["COUNT_OBJECTION"]) + ")</a></td>");
                                    //}
                                    //else
                                    //{
                                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                                    //}



                                    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_STATUS"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_STATUS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                                    {
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkSTAMeetingApp' class='checkSTAMeetingApproved form-control' onclick = \"javascript:checkSTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Meeting</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkSTAMeetingRej' class='checkSTAMeetingRejected form-control' onclick = \"javascript:checkSTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkSTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");

                                    }
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;
            case "CSGetRTAMeetingMarked_Transfer":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "MARKED_VIEW");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {
                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Marked on</th>");
                                sb.Append("<th colspan='3'>Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody id= 'jtcApproveRejecttbl'>");
                                foreach (DataRow dr in dt.Rows)
                                {

                                    sb.Append("<tr>");
                                    sb.Append("<td id =" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToDateTime(Convert.ToString(dr["MEETING_MARKED_ON"])).ToString("dd-MM-yyyy") + "</td>");
                                    sb.Append("<td><input type='checkbox' id='checkSTAMeetingApp' class='checkSTAMeetingApproved form-control' onclick = \"javascript:checkSTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Verified</td>");
                                    sb.Append("<td><input type='checkbox' id='checkSTAMeetingRej' class='checkSTAMeetingRejected form-control' onclick = \"javascript:checkSTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                    sb.Append("<td><input type='checkbox' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkSTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;
            case "CSGetRTARejMeetingMarked_PermitRenewal":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_COUNTER_SIGNATURE_RENEWAL_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PermitType);
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "LISTSO");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {

                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Permit Type</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Application Status</th>");
                                sb.Append("<th>View</th>");
                                sb.Append("<th>Assistant Status</th>");
                                sb.Append("<th>Assistant Comment</th>");
                                sb.Append("<th>SO Status</th>");
                                sb.Append("<th>SO Comment</th>");
                                sb.Append("<th colspan='3' >Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody>");
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);

                                    sb.Append("<tr>");
                                    sb.Append("<td id=" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");



                                    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_STATUS"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_STATUS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "</td>");
                                    if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                                    {
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkSTAMeetingApp' class='checkSTAMeetingApproved form-control' onclick = \"javascript:checkSTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Meeting</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' id='checkSTAMeetingRej' class='checkSTAMeetingRejected form-control' onclick = \"javascript:checkSTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                        sb.Append("<td><input type='checkbox' name='meeting' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkSTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td></td>");

                                    }
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


                    }



                }
                break;
            case "CSGetRTAMeetingMarked_PermitRenewal":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "RTA_COUNTER_SIGNATURE_RENEWAL_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "MARKED_VIEW");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            StringBuilder sb = new StringBuilder();
                            if (dt.Rows.Count > 0)
                            {
                                sb.Append("<table class='table table-striped table-hover table-bordered'>");
                                sb.Append("<thead>");
                                sb.Append("<tr>");
                                sb.Append("<th>Application No.</th>");
                                sb.Append("<th>Name</th>");
                                sb.Append("<th>Registration No.</th>");
                                sb.Append("<th>Marked on</th>");
                                sb.Append("<th colspan='3'>Action</th>");
                                sb.Append("</tr>");
                                sb.Append("</thead>");
                                sb.Append("<tbody id= 'jtcApproveRejecttbl'>");
                                foreach (DataRow dr in dt.Rows)
                                {

                                    sb.Append("<tr>");
                                    sb.Append("<td id =" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + ">" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                    sb.Append("<td>" + Convert.ToDateTime(Convert.ToString(dr["MEETING_MARKED_ON"])).ToString("dd-MM-yyyy") + "</td>");
                                    sb.Append("<td><input type='checkbox' id='checkSTAMeetingApp' class='checkSTAMeetingApproved form-control' onclick = \"javascript:checkSTAMeetingApproved('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Verified</td>");
                                    sb.Append("<td><input type='checkbox' id='checkSTAMeetingRej' class='checkSTAMeetingRejected form-control' onclick = \"javascript:checkSTAMeetingRejected('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Reject</td>");
                                    sb.Append("<td><input type='checkbox' checked='checked' id='checkHold' class='checkApplicationHold form-control' onclick = \"javascript:checkSTAMeetingHold('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\" />Hold</td>");
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


                    }
                    catch (Exception ex)
                    {
                        String Messqge = ex.Message;
                        context.Response.Write(Messqge);


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