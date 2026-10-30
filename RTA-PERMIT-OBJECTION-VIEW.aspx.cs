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

public partial class RTA_PERMIT_OBJECTION_VIEW : System.Web.UI.Page
{
   // private static string ID;
  
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-PERMIT-APPLICATION.aspx'", true);
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

                     hdnID.Value = IDx.Value;
                     sCon.Open();
                     SqlCommand sCmd = new SqlCommand();
                     sCmd.Connection = sCon;
                     sCmd.CommandText = "RTA_PERMIT_OBJECTION_APPLICATION_APPROVAL";
                     sCmd.CommandType = CommandType.StoredProcedure;
                     sCmd.Parameters.Clear();
                     sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                     sCmd.Parameters.AddWithValue("@MODE", "PERMIT_OBJECTION_VIEW");
                     DataSet ds = new DataSet();
                     SqlDataAdapter da = new SqlDataAdapter(sCmd);
                     da.Fill(ds);


                     DataTable dt1 = ds.Tables[0];
                     DataTable dt2 = ds.Tables[1];
                     DataTable dt3 = ds.Tables[2];
                     DataTable dt4 = ds.Tables[3];
                     DataTable dt5 = ds.Tables[4];
                  
                     StringBuilder sb = new StringBuilder();
                     StringBuilder sb1 = new StringBuilder();
                     if (dt1.Rows.Count > 0)
                     {
                         
                         lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                         lblApplicationStatus.Text = dt1.Rows[0]["APPLICATION_STATUS"].ToString();
                         lblObjRegNumber.Text = dt1.Rows[0]["OBJECTION_ON_REGISTRATION_NO"].ToString();
                         lblObjAppNumber.Text = dt1.Rows[0]["OBJECTION_ON_APPLICATION_NUMBER"].ToString();

                         VOName.Text = dt1.Rows[0]["OBJECTOR_NAME"].ToString();
                         lblMobileNumber.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                         lbRegNo.Text = dt1.Rows[0]["OBJECTOR_REGISTRATION_NUMBER"].ToString();
                         lblPermitNo.Text = dt1.Rows[0]["OBJECTOR_PERMIT_NUMBER"].ToString();
                         
                         RouteHDRInfo.Text = dt1.Rows[0]["OBJECTION_ROUTE"].ToString();
                         RouteHDRInfo1.Text = dt1.Rows[0]["OBJECTOR_ROUTE"].ToString();
                         lblObjectionType.Text = dt1.Rows[0]["OBJECTION_TYPE"].ToString();
                         lblObjectorRemarks.Text = dt1.Rows[0]["OBJECTOR_REMARKS"].ToString();
                         
                         

                     }
                     if (dt3.Rows.Count > 0)
                     {
                         for (int k = 1; k <= dt3.Rows.Count; k++)
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
                     if (dt5.Rows.Count > 0)
                     {
                         for (int k = 1; k <= dt5.Rows.Count; k++)
                         {
                             if (dt4.Rows.Count > 0)
                             {
                                 DataRow[] dr = dt4.Select("SEQUENCE='" + k + "'");

                                 if (dr.Length > 0)
                                 {
                                     //Int32 length = dt2.Rows.Count - 1;
                                     Int32 length = dr.Length - 1;
                                     sb1.Append("<table class='table table-striped table-bordered table-hover'>");
                                     sb1.Append("<thead>");
                                     sb1.Append("<tr>");
                                     sb1.Append("<th colspan='3' style='text-align:center;'> Up </th>");
                                     sb1.Append("<th>  </th>");
                                     sb1.Append("<th colspan='3' style='text-align:center;'> Down </th>");
                                     sb1.Append("</tr>");
                                     sb1.Append("<tr>");
                                     sb1.Append("<th> Route </th>");
                                     sb1.Append("<th> Arrival </th>");
                                     sb1.Append("<th> Departure </th>");
                                     sb1.Append("<th>  </th>");
                                     sb1.Append("<th> Route </th>");
                                     sb1.Append("<th> Arrival </th>");
                                     sb1.Append("<th> Departure </th>");
                                     sb1.Append("</tr>");
                                     sb1.Append("</thead>");
                                     sb1.Append("<tbody id='tbdRouteInfo'>");
                                     for (int i = 0; i < dr.Length; i++)
                                     {


                                         if (i == 0)
                                         {
                                             sb1.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                             sb1.Append("<td> ..... </td>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "</label></td>");
                                             sb1.Append("<td></td>");
                                             sb1.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                             sb1.Append("<td> ..... </td>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                             sb1.Append("</tr>");
                                         }
                                         else if (i == length)
                                         {
                                             sb1.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "</label></td>");
                                             sb1.Append("<td> ..... </td>");
                                             sb1.Append("<td></td>");
                                             sb1.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                             sb1.Append("<td> ..... </td>");
                                             sb1.Append("</tr>");

                                         }
                                         else
                                         {
                                             sb1.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "</label></td>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "</label></td>");
                                             sb1.Append("<td></td>");
                                             sb1.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                             sb1.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                             sb1.Append("</tr>");
                                         }

                                     }
                                     sb1.Append("<tbody>");
                                     sb1.Append("</table>");

                                 }
                             }
                         }
                         RouteTimeInfo1.InnerHtml = sb1.ToString();
                    
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
   
    
   
}
    
    



    