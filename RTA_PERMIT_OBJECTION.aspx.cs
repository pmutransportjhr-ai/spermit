using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;



public partial class RTA_PERMIT_OBJECTION : System.Web.UI.Page
{
    
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
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

                      QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault();
                      QueryString IDx1 = QueryStr.Where(l => l.Name == "TBL_MODE").FirstOrDefault();
                     
                      hdnID.Value = IDx.Value;
                      hdnTableType.Value = IDx1.Value;
                      sCon.Open();
                      SqlCommand sCmd = new SqlCommand();
                      sCmd.Connection = sCon;
                      sCmd.CommandText = "RTA_OBJECTION_APPLICATION_AED";
                      sCmd.CommandType = CommandType.StoredProcedure;
                      sCmd.Parameters.Clear();
                      sCmd.Parameters.AddWithValue("@APP_ID", hdnID.Value);
                      sCmd.Parameters.AddWithValue("@TBL_MODE", hdnTableType.Value);
                      sCmd.Parameters.AddWithValue("@MODE", "OBJECTION_INFO_LIST_BYID");
                      DataSet ds = new DataSet();
                      SqlDataAdapter da = new SqlDataAdapter(sCmd);
                      da.Fill(ds);
                      DataTable dt = new DataTable();
                    
                    
                      DataTable dt1 = ds.Tables[0];
                      DataTable dt2 = ds.Tables[1];
                      DataTable dt3 = ds.Tables[2];
                      DataTable dt4 = ds.Tables[3];
                      StringBuilder sb = new StringBuilder();
                      if (dt1.Rows.Count > 0)
                      {


                         
                              lbPermitId.Text = dt1.Rows[0]["PERMIT_TYPE_ID"].ToString();
                              lblObjTill.Text = dt1.Rows[0]["OBJECTION_TILL"].ToString();
                              lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                              lblAppNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                              lblRTOCode.Text = dt1.Rows[0]["RTO_CODE"].ToString();
                              VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                              VOCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                              VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                              RouteHDRInfo.Text = dt1.Rows[0]["ROUTE"].ToString();
                              
                              Page.Title = dt1.Rows[0]["PERMIT_TYPE"].ToString() + " | OBJECTION";
                              PageHeading.InnerText = "RTA > " + dt1.Rows[0]["PERMIT_TYPE"].ToString() + " > OBJECTION";
                              PageHeading1.InnerText = dt1.Rows[0]["PERMIT_TYPE"].ToString() + " | OBJECTION";
                          
                          

                      }
                      
                      if (dt4.Rows.Count > 0)
                    
                      {
                        for (int k = 1; k <= dt4.Rows.Count; k++)
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

                    }

                    RouteTimeInfo.InnerHtml = sb.ToString();

                          if (dt3.Rows.Count > 0)
                          {
                              lblObjectionFees.Text = Convert.ToString(dt3.Rows[0]["FEE"]);
                          }
                      }
                 

                  catch (Exception ex)
                  {
                      String Message = ex.Message;
                      Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                      ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermithomePage.aspx'", true);

                  }
              }


        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "RTA_OBJECTION_APPLICATION_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", Convert.ToInt32(lbPermitId.Text.Trim()));
                sCmd.Parameters.AddWithValue("@TBL_MODE", hdnTableType.Value);
                sCmd.Parameters.AddWithValue("@OBJECTION_ON_REGISTRATION_NO", VORegNumber.Text.Trim());
                sCmd.Parameters.AddWithValue("@OBJECTION_ON_APPLICATION_NUMBER", lblAppNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@OBJECTION_TILL", DateTime.ParseExact(lblObjTill.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@OBJECTOR_NAME", lblObjectorName.Text.Trim());
                sCmd.Parameters.AddWithValue("@OBJECTOR_REGISTRATION_NUMBER", txtObjRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@OBJECTOR_PERMIT_NUMBER", txtObjPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", lblObjectorMobileNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@OBJECTION_TYPE", ddlObjType.SelectedValue);
                sCmd.Parameters.AddWithValue("@OBJECTOR_REMARKS", txtObjectionRemarks.Text.Trim());
                sCmd.Parameters.AddWithValue("@FEE", Convert.ToDecimal(lblObjectionFees.Text.Trim()));
                sCmd.Parameters.AddWithValue("@RTO_CODE", Convert.ToInt32(lblRTOCode.Text.Trim()));
                sCmd.Parameters.AddWithValue("@OBJECTOR_TYPE", objector_type.SelectedValue);
                sCmd.Parameters.AddWithValue("@MODE", "ADD");

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);


                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[0];
                
                Int32 AppId;
                AppId = Convert.ToInt32(dt1.Rows[0]["APP_ID"]);

                string RegistrationNo = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                string permitNo = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);
                string mobileNo = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);
                string ApplicationNo = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);
                string permitType = Convert.ToString(dt1.Rows[0]["PERMIT_TYPE"]);
                string sevicename = Convert.ToString(dt1.Rows[0]["APPLICATION_SERVICE"]);
                SMSSender objSms = new SMSSender();

                objSms.ApplicationNumberSms(RegistrationNo, permitNo, sevicename, permitType, mobileNo, ApplicationNo);

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for Objection');window.location ='RTA_PERMIT_OBJECTION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(AppId) + "&TBL_MODE=" + Convert.ToString(hdnTableType.Value), true), Response.ContentEncoding) + "'", true);

            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermithomePage.aspx'", true);

            }

        }
    }
    protected void btnGO_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "RTA_OBJECTION_APPLICATION_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@OBJECTION_ON_REGISTRATION_NO", VORegNumber.Text.Trim());
                sCmd.Parameters.AddWithValue("@OBJECTION_ON_APPLICATION_NUMBER", lblAppNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@OBJECTOR_REGISTRATION_NUMBER", txtObjRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@OBJECTOR_PERMIT_NUMBER", txtObjPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@OBJECTOR_TYPE", objector_type.SelectedValue);
                sCmd.Parameters.AddWithValue("@MODE", "OBJECTOR_VERIFY");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);
                DataTable dt = new DataTable();
                    
                dt = ds.Tables[0];
                if (dt.Columns[0].ColumnName == "MSG")
                {
                    lblObjectorMsg.Text = dt.Rows[0]["MSG"].ToString();
                    lblObjectorMsg.Visible = true;
                    divObjSubmit.Visible = false;

                }
                else 
                {
                    lblObjectorMobileNo.Text = dt.Rows[0]["MOBILE_NUMBER"].ToString();
                    lblObjectorName.Text = dt.Rows[0]["OWNER_NAME"].ToString();
                    divObjSubmit.Visible = true;
                    lblObjectorMsg.Visible = false;
                    txtObjRegNo.Enabled = false;
                    txtObjPermitNo.Enabled = false;
                    btnGO.Enabled = false;
                }
                //else
                //{
                //    lblObjectorMsg.Text = "Objector's Reg. No. OR Permit No. not match";
                //    lblObjectorMsg.Visible = true;
                //    divObjSubmit.Visible = false;



                //}
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermithomePage.aspx'", true);

            }
        }

    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        lblObjectorMsg.Text = "";
        txtObjRegNo.Text = "";
        txtObjPermitNo.Text = "";
        lblObjectorMsg.Visible = false;
        divObjSubmit.Visible = false;
        txtObjRegNo.Enabled = true;
        txtObjPermitNo.Enabled = true;
        btnGO.Enabled = true;
    }
}