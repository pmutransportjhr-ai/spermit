<%@ WebHandler Language="C#" Class="Screen_RTA" %>

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

public class Screen_RTA : IHttpHandler {


    public void ProcessRequest(HttpContext context)
    {

        switch (context.Request.QueryString["caller"])
        {
            case "GetScreenRTO":
                String sConnectionString3 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString3))
                {
                    try
                    {
                        String regNumber = Convert.ToString(context.Request.QueryString["q"]).Split('~')[0];
                        string permitType = Convert.ToString(context.Request.QueryString["q"]).Split('~')[1];
                        

                        
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SCREEN_RTA_GOODS_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", 0);
                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", regNumber);
                        sCmd.Parameters.AddWithValue("@MODE", "RTO_LIST");
                        DataSet ds1 = new DataSet();
                        SqlDataAdapter da1 = new SqlDataAdapter(sCmd);
                        da1.Fill(ds1);


                        DataTable dt1 = ds1.Tables[0]; //RTO CODE & RTO NAME
                        DataTable dt2 = ds1.Tables[1]; // To Apply For (RTO) 
                        DataTable dt3 = ds1.Tables[2]; // another state
                   
                        StringBuilder sb = new StringBuilder();
                        if (dt1.Rows.Count > 0)
                        {
                            //    RTOCode.Value = dt1.Rows[0]["RTO_CODE"].ToString();


                        }
                        if (dt2.Rows.Count > 0)
                        {
                            sb.Append("<div class='input-group'>");
                            sb.Append("<div class='icheck-list'>");
                            foreach (DataRow dr in dt2.Rows)
                            {

                                sb.Append("<div class='col-md-12 col-sm-12'>");
                                sb.Append("<input type='radio' value='" + Convert.ToString(dr["RTO_NAME"]) + "&" + Convert.ToString(dr["RTO_CODE"]) + "' class='rtoHdr' name ='otherrto' id='RadioButton_" + Convert.ToString(dr["RTO_CODE"]) + "'");
                                sb.Append("onclick=\"javascript:otherRTOCode1('" + CryptorEngine.Encrypt("RTO_CODE=" + Convert.ToString(dr["RTO_CODE"]), true) + "')\" >    " + Convert.ToString(dr["RTO_NAME"]).ToUpper() + "</div>");
                            }
                            sb.Append("</div>");
                            sb.Append("</div>");
                            //      RTORouteDiv.InnerHtml = sb.ToString();

                        }
                        if (dt3.Rows.Count > 0)
                        {
                            StringBuilder sbstate = new StringBuilder();


                            sbstate.Append("<div class='input-group'>");
                            sbstate.Append("<div class='icheck-list'>");
                            if (permitType == "2")   // For Temporary
                            {

                                foreach (DataRow dr in dt3.Rows)
                                {

                                    sbstate.Append("<div class='col-md-12 col-sm-12'>");
                                    sbstate.Append("<input type='checkbox' value='" + Convert.ToString(dr["STATE_CODE"]) + "&" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]) + "'  id='STATE_" + Convert.ToString(dr["STATE_CODE"]) + "' class = 'StateHDR'");
                                    sbstate.Append("onclick=\"javascript:otherSate1('" + CryptorEngine.Encrypt("STATE_CODE=" + Convert.ToString(dr["STATE_CODE"]) + "& VALIDITY=" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]), true) + "')\" >    " + Convert.ToString(dr["STATE_NAME"]).ToUpper() + " <b> - (PERMIT VALID FOR " + dr["PERMIT_VALIDITY_IN_MONTH"] + "  MONTHS) </b>" + "</div>");
                                }
                            }
                            else
                            {
                                foreach (DataRow dr in dt3.Rows)
                                {

                                    sbstate.Append("<div class='col-md-12 col-sm-12'>");
                                    sbstate.Append("<input disabled='disabled' type='checkbox' value='" + Convert.ToString(dr["STATE_CODE"]) + "&" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]) + "'  id='STATE_" + Convert.ToString(dr["STATE_CODE"]) + "' class = 'StateHDR'");
                                    sbstate.Append("onclick=\"javascript:otherSate1('" + CryptorEngine.Encrypt("STATE_CODE=" + Convert.ToString(dr["STATE_CODE"]) + "& VALIDITY=" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]), true) + "')\" >    " + Convert.ToString(dr["STATE_NAME"]).ToUpper() + " <b> - (PERMIT VALID FOR " + dr["PERMIT_VALIDITY_IN_MONTH"] + "  MONTHS) </b>" + "</div>");
                                }
                            }

                            sbstate.Append("</div>");
                            sbstate.Append("</div>");
                            context.Response.Write("Success~~" + sbstate.ToString() + "~~" + sb.ToString() + "~~" + dt1.Rows[0]["RTO_CODE"].ToString());
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
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}