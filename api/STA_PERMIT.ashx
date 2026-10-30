<%@ WebHandler Language="C#" Class="STA_PERMIT" %>

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

public class STA_PERMIT : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    protected List<QueryString> QueryStr = new List<QueryString>();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string json = string.Empty;

        switch (context.Request.QueryString["caller"])
        {
            case "GetCSRegDetails":
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
                        sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_APPROVAL";
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
                                    sb.Append("<td><a onclick=\"javascript:fnRegComments('" + Convert.ToString(dt.Rows[i]["COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "');\">Comments</a></td>");

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
            case "GetPayDetails":
                String sConnectionString11 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString11))
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

                        QueryString IDx = QueryStr.Where(l => l.Name == "PERNO").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_APPROVAL";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "TAX_HISTORY");
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
                            sb.Append("<th>Permit No.</th>");
                            sb.Append("<th>Tax Amount</th>");
                            sb.Append("<th>Tax Paid On</th>");
                            sb.Append("<th>Tax Paid From</th>");
                            sb.Append("<th>Tax Paid Upto</th>");
                            sb.Append("<th>Transaction Id</th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");
                            sb.Append("<tbody>");
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["PERMIT_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["FEE_TAX_AMOUNT"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["TAX_PAID_ON"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["TAX_PAID_FROM"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["TAX_PAID_UPTO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dt.Rows[i]["TAX_PAY_TRN_ID"]) + "</td>");

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
                String sConnectionString1 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString1))
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
                        sCmd.CommandText = "STA_PERMIT_APPLICATION_APPROVAL";
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
                                    sb.Append("<td><a onclick=\"javascript:fnRegComments('" + Convert.ToString(dt.Rows[i]["COMMENTS"]).Replace("\r", " ").Replace("\n", " ").Replace("'", "") + "');\">Comments</a></td>");

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

            case "GetSTARouteDetails":
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
                        sCmd.CommandText = "STA_PERMIT_APPLY_AED";
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
                            int No = Convert.ToInt32(dt.Rows[0]["NUMBER_OF_ROUND_TRIP"].ToString());
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
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("</tr>");
                                    }
                                    else if (i == length)
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "'  placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("</tr>");

                                    }
                                    else
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><div class='input-group'><input  type='text' style='width:80%'  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td><div class='input-group'><input  type='text' style='width:80%'  class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("</tr>");
                                    }

                                }
                                
                                
                                //for (int i = 0; i < dt.Rows.Count; i++)
                                //{
                                //    if (i == 0)
                                //    {
                                //        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                //        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                //        sb.Append("<td> ..... </td>");
                                //        sb.Append("<td><div class='input-group'><input type='text'  class='form-control timepicker timepicker-24' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' /></div></td>");
                                //        sb.Append("<td></td>");
                                //        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                //        sb.Append("<td> ..... </td>");
                                //        sb.Append("<td><div class='input-group'><input type='text' class='form-control timepicker timepicker-24' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'/></div></td>");
                                //        sb.Append("</tr>");
                                //    }
                                //    else if (i == length)
                                //    {
                                //        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                //        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                //        sb.Append("<td><div class='input-group'><input type='text'  class='form-control timepicker timepicker-24' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' /></div></td>");
                                //        sb.Append("<td> ..... </td>");
                                //        sb.Append("<td></td>");
                                //        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                //        sb.Append("<td><div class='input-group'><input type='text'  class='form-control timepicker timepicker-24' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'/></div></td>");
                                //        sb.Append("<td> ..... </td>");
                                //        sb.Append("</tr>");

                                //    }
                                //    else
                                //    {
                                //        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                //        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                //        sb.Append("<td><div class='input-group'><input type='text'  class='form-control timepicker timepicker-24' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' /></div></td>");
                                //        sb.Append("<td><div class='input-group'><input type='text'  class='form-control timepicker timepicker-24' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' /></div></td>");
                                //        sb.Append("<td></td>");
                                //        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                //        sb.Append("<td><div class='input-group'><input  type='text'  class='form-control timepicker timepicker-24' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'/></div></td>");
                                //        sb.Append("<td><div class='input-group'><input  type='text'  class='form-control timepicker timepicker-24' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'/></div></td>");
                                //        sb.Append("</tr>");
                                //    }

                                //}
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
            case "GetSTARouteDetails1":
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

                        QueryString IDx = QueryStr.Where(l => l.Name == "ROUTE_ID").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "STA_PERMIT_APPLY_AED";
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
                            int No = Convert.ToInt32(dt.Rows[0]["NUMBER_OF_ROUND_TRIP"].ToString());
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
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("</tr>");
                                    }
                                    else if (i == length)
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "'  placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("</tr>");

                                    }
                                    else
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
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
            case "GetSTARouteDetailsCS":
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

                        QueryString IDx = QueryStr.Where(l => l.Name == "ROUTE_ID").FirstOrDefault();
                        String RID = IDx.Value;
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@ROUTE_ID", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "ROUTEDETAILSCS");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);




                        DataTable dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();

                        if (dt.Rows.Count > 0)
                        {
                            int No = Convert.ToInt32(dt.Rows[0]["NUMBER_OF_ROUND_TRIP"].ToString());
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
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("</tr>");
                                    }
                                    else if (i == length)
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "'  placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("</tr>");

                                    }
                                    else
                                    {
                                        sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><div class='input-group'><input  type='text' style='width:80%'  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
                                        sb.Append("<td><div class='input-group'><input  type='text' style='width:80%'  class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "\")" + "' placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
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
            case "GetRouteDetailsBYID":
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

                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@ROUTE_ID", RID);
                    parameters[1] = new SqlParameter("@PERMIT_TYPE", DBNull.Value);
                    parameters[2] = new SqlParameter("@REGISTRATION_NUMBER", DBNull.Value);
                    parameters[3] = new SqlParameter("@MODE", "ROUTEANDFEELISTFORCOUNTERSIGNATURE");

                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_STA_GET_PERMIT_INFO", parameters);
                    DataTable dt = ds.Tables[0];

                    StringBuilder sb = new StringBuilder();

                    if (dt.Rows.Count > 0)
                    {
                        sb.Append("<div class='input-group'>");
                        sb.Append("<div class='icheck-list'>");
                        foreach (DataRow dr in dt.Rows)
                        {

                            sb.Append("<div class='col-md-12 col-sm-12'><input type='radio' value='" + Convert.ToString(dr["ROUTE_ID"]) + "' name='routeHdr' id='RadioButton_" + Convert.ToString(dr["ROUTE_ID"]) + "' onclick=\"javascript:fnShowRoute('" + CryptorEngine.Encrypt("ROUTE_ID=" + Convert.ToString(dr["ROUTE_ID"]), true) + "')\"/> " + Convert.ToString(dr["ROUTE_DETAILS"]) + " (" + Convert.ToString(dr["TOTAL_DISTANCE"]) + " KM)</div>");
                        }
                        sb.Append("</div>");
                        sb.Append("</div>");

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
                break;
            case "GetDetailsByState":
                try
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                    //    context.Response.Write("Success~" + sb.ToString());
                    String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                    using (SqlConnection sCon = new SqlConnection(ConnectionString))
                    {
                        String State = queryString;

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SCREEN_STA_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", 0);
                        sCmd.Parameters.AddWithValue("@STATE_CODE", State);
                        sCmd.Parameters.AddWithValue("@MODE", "PERMIT_DETAIL_BY_STATE");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();



                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {

                                sb.Append("<tr>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");


                                sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");

                                sb.Append("<td>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");



                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterView('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">View</a></td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterEditSTA('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">Edit</a></td>");

                                sb.Append("</tr>");
                            }

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
                break;
            case "GetCSDetailsByState":
                try
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                    //    context.Response.Write("Success~" + sb.ToString());
                    String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                    using (SqlConnection sCon = new SqlConnection(ConnectionString))
                    {
                        String State = queryString;

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SCREEN_STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", 0);
                        sCmd.Parameters.AddWithValue("@STATE_CODE", State);
                        sCmd.Parameters.AddWithValue("@MODE", "CS_DETAIL_BY_STATE");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];

                        StringBuilder sb = new StringBuilder();



                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {

                                sb.Append("<tr>");


                                sb.Append("<td>" + Convert.ToString(dr["COUNTER_SIGNATURE_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_VALID_UPTO"]) + "</td>");
                                sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");



                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterView('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">View</a></td>");
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterEditSTA('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">Edit</a></td>");

                                sb.Append("</tr>");
                            }

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
                break;
            case "GetSTARejMeetingMarked":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String State = Convert.ToString(context.Request.Form["state"]);
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);
                        
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_PERMIT_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@STATE_CODE", State);
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
            case "GetSTAMeetingMarked":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_PERMIT_APPLICATION_APPROVAL";
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
                            sCmd.CommandText = "STA_PERMIT_APPLICATION_APPROVAL";
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
                                    sb.Append("<td><a class='btn btn-outline dark'  href=\"STA-PERMIT-OBJECTION-VIEW.aspx?q=" + q + "\" target='_blank'>View</a></td>");


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

            case "GetSTAMeetingMarked_Transfer":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_TRANSFER_PERMIT_APPLICATION_APPROVAL";
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

            case "GetSTARejMeetingMarked_Transfer":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String State = Convert.ToString(context.Request.Form["state"]);
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_TRANSFER_PERMIT_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@STATE_CODE", State);
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
            case "GetSTAMeetingMarked_RouteExtension":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_PERMIT_ROUTE_EXTENSION_APPROVAL";
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

            case "GetSTARejMeetingMarked_RouteExtension":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String State = Convert.ToString(context.Request.Form["state"]);
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_PERMIT_ROUTE_EXTENSION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@STATE_CODE", State);
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
            case "GetSTAMeetingMarked_PermitRenewal":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_RENEWAL_OF_PERMIT_APPROVAL";
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

            case "GetSTARejMeetingMarked_PermitRenewal":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String State = Convert.ToString(context.Request.Form["state"]);
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_RENEWAL_OF_PERMIT_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@STATE_CODE", State);
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
            case "GetRegistrationRouteDetail":
                String sConnectionStringRouteDetail = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionStringRouteDetail))
                {
                    try
                    {
                        String RID = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);

                        

                        
                         
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", RID);
                        sCmd.Parameters.AddWithValue("@MODE", "REG_ROUTE_DETL");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);

                        DataTable dt = ds.Tables[0];
                        DataTable dt1 = ds.Tables[1];

                        StringBuilder sb = new StringBuilder();

                        if (dt.Rows.Count > 0)
                        {
                            int No = Convert.ToInt32(dt1.Rows[0]["SEQUENCE"].ToString());
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
                                        sb.Append("<tr id='tr_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["UP_DEP_TIME"]) + "</label></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                        sb.Append("</tr>");
                                    }
                                    else if (i == length)
                                    {
                                        sb.Append("<tr id='tr_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["UP_ARR_TIME"]) + "</label></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                        sb.Append("<td> ..... </td>");
                                        sb.Append("</tr>");

                                    }
                                    else
                                    {
                                        sb.Append("<tr id='tr_" + Convert.ToString(dt.Rows[i]["S_NO"]) + "'>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["UP_ARR_TIME"]) + "</label></td>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["UP_DEP_TIME"]) + "</label></td>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td>" + Convert.ToString(dt.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                        sb.Append("<td><label>" + Convert.ToString(dt.Rows[i]["DOWN_DEP_TIME"]) + "</label></td>");
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
            case "CSGetSTARejMeetingMarked":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String State = Convert.ToString(context.Request.Form["state"]);
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@STATE_CODE", State);
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
            case "CSGetSTAMeetingMarked":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_APPROVAL";
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
                            sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_APPROVAL";
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
                                    sb.Append("<td><a class='btn btn-outline dark'  href=\"STA-COUNTER-SIGNATURE-OBJECTION-VIEW.aspx?q=" + q + "\" target='_blank'>View</a></td>");


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
            case "CSGetSTARejMeetingMarked_Transfer":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String State = Convert.ToString(context.Request.Form["state"]);
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@STATE_CODE", State);
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
            case "CSGetSTAMeetingMarked_Transfer":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_APPROVAL";
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
            case "CSGetSTARejMeetingMarked_PermitRenewal":
                {


                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        String State = Convert.ToString(context.Request.Form["state"]);
                        String PermitType = Convert.ToString(context.Request.Form["PermitType"]);

                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_COUNTER_SIGNATURE_RENEWAL_APPLICATION_APPROVAL";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@STATE_CODE", State);
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
            case "CSGetSTAMeetingMarked_PermitRenewal":
                {

                    try
                    {

                        String ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                        using (SqlConnection sCon = new SqlConnection(ConnectionString))
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_COUNTER_SIGNATURE_RENEWAL_APPLICATION_APPROVAL";
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