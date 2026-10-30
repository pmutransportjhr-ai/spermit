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
using QRCoder;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Collections;


public partial class STA_PERMIT_ROUTE_UPDATE : System.Web.UI.Page
{
    // private static string ID;

    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-DASHBOARD.aspx'", true);
                return;
            }
             String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
             using (SqlConnection sCon = new SqlConnection(sConnectionString))
             {
                 try
                 {
                     String queryString = CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["q"]), true);
                     foreach (String str in queryString.Split(new Char[] { '&' }))
                     {
                         QueryString qs = new QueryString();
                         qs.Name = str.Split(new Char[] { '=' })[0];
                         qs.Value = str.Split(new Char[] { '=' })[1];
                         QueryStr.Add(qs);
                     }

                     QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                     QueryString IDx1 = QueryStr.Where(l => l.Name == "TYPE").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);

                     hdnID.Value = IDx.Value;
                     hdnType.Value = IDx1.Value;
                     sCon.Open();
                     SqlCommand sCmd = new SqlCommand();
                     sCmd.Connection = sCon;
                     sCmd.CommandText = "STA_PERMIT_ROUTE_UPDATE";
                     sCmd.CommandType = CommandType.StoredProcedure;
                     sCmd.Parameters.Clear();
                     sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                     sCmd.Parameters.AddWithValue("@TYPE", Convert.ToString(hdnType.Value)); //
                     sCmd.Parameters.AddWithValue("@MODE", "PERMIT_ROUTE_VIEW");
                     DataSet ds = new DataSet();
                     SqlDataAdapter da = new SqlDataAdapter(sCmd);
                     da.Fill(ds);

                    DataTable dt1 = ds.Tables[0];
                    if (dt1.Columns[0].ColumnName.ToString() == "MSG")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You can not Update Route currently');window.location ='STA-DASHBOARD.aspx'", true);
                        return;
                    }
                    else
                    { 
                    DataTable dt2 = ds.Tables[1];
                    DataTable dt3 = ds.Tables[2];
                    StringBuilder sb = new StringBuilder();


                    lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    lblApplicationStatus.Text = dt1.Rows[0]["APPLICATION_STATUS"].ToString();
                    lblPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();


                    RouteHDRInfo.Text = Convert.ToString(dt1.Rows[0]["ROUTE"]);

                   
                    if (dt3.Rows.Count > 0)
                    {
                        txtNoRoundTrip.Value = dt3.Rows.Count.ToString();
                        for (int k = 1; k <= dt3.Rows.Count;k++ )
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
                                    sb.Append("<tbody id='tbdRouteInfo_" + k + "'>");
                                    for (int i = 0; i < dr.Length; i++)
                                    {


                                        if (i == 0)
                                        {

                                            sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                            sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                            sb.Append("<td> ..... </td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td></td>");
                                            sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                            sb.Append("<td> ..... </td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + " class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("</tr>");



                                        }
                                        else if (i == length)
                                        {

                                            sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                            sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td> ..... </td>");
                                            sb.Append("<td></td>");
                                            sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "'  placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td> ..... </td>");
                                            sb.Append("</tr>");



                                        }
                                        else
                                        {

                                            sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                            sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + " class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + " class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td></td>");
                                            sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                            sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' value=" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' value=" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "  class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder=''/> &nbsp;&nbsp;Hrs.</div></td>");
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

                    }

               

                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-DASHBOARD.aspx'", true);

                }

            }
        }
    }


    protected void btnYes_Click(object sender, EventArgs e)
    {
           
       
            String input = "";
            input = txtroutjson.Value;
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            SqlTransaction objTrans = null;
            

            PERMIT_APPLICATION_LDR rDTO = new JavaScriptSerializer().Deserialize<PERMIT_APPLICATION_LDR>(input);

            Int32 masterAppId;
            
            
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
             try
             { 
                sCon.Open();
                objTrans = sCon.BeginTransaction();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.Transaction = objTrans;
                sCmd.CommandText = "STA_PERMIT_ROUTE_UPDATE";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                sCmd.Parameters.AddWithValue("@TYPE", Convert.ToString(hdnType.Value));
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@MODE", "PERMIT_ROUTE_UPDATE");
                masterAppId = Convert.ToInt32(sCmd.ExecuteScalar());
                    
                        

                        
                         if (masterAppId != 0)
                        {

                            sCmd.CommandText = "STA_PERMIT_APPLICATION_LDR_AED";
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@APP_ID", masterAppId);
                            sCmd.Parameters.AddWithValue("@OP_MODE", "MASTER_LDR_DELETE");
                            sCmd.ExecuteNonQuery();

                            foreach (PERMIT_APPLICATION_LDR dr in rDTO.PERMIT_APPLICATION_LDRs)
                            {

                                sCmd.CommandText = "STA_PERMIT_APPLICATION_LDR_AED";
                                sCmd.CommandType = CommandType.StoredProcedure;
                                sCmd.Parameters.Clear();

                                sCmd.Parameters.AddWithValue("@APP_ID", masterAppId);
                                sCmd.Parameters.AddWithValue("@SEQUENCE", dr.ROUTE_DETAIL_ID);
                                sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                                sCmd.Parameters.AddWithValue("@ARRIVAL_TIMING", dr.ARRIVAL_TIMING.ToString());
                                sCmd.Parameters.AddWithValue("@DEPARTURE_TIMING", dr.DEPARTURE_TIMING.ToString());
                                sCmd.Parameters.AddWithValue("@MODE", dr.MODE);
                                sCmd.Parameters.AddWithValue("@ID", dr.ID);
                                sCmd.Parameters.AddWithValue("@OP_MODE", "MASTER_LDR_INSERT");
                                sCmd.ExecuteNonQuery();

                            }


                        }
                        objTrans.Commit();
                        ScriptManager.RegisterClientScriptBlock((sender as Control), Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Updated');window.location ='STA-DASHBOARD.aspx'", true);
                    
                
             }
             catch (Exception ex)
             {
                 objTrans.Rollback();
                 String Message = ex.Message;
                 Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");

                 ScriptManager.RegisterClientScriptBlock((sender as Control), Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.reload();", true);
             }
            
            }
            

        

    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("STA-DASHBOARD.aspx");

    }

   
}

    



    