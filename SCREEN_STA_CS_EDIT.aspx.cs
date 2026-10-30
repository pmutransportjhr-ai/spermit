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
using System.Web.Script.Serialization;

public partial class SCREEN_STA_CS_EDIT : System.Web.UI.Page
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
                     sCmd.CommandText = "SCREEN_STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                     sCmd.CommandType = CommandType.StoredProcedure;
                     sCmd.Parameters.Clear();
                     sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                     sCmd.Parameters.AddWithValue("@MODE", "MASTER_VIEW");
                     DataSet ds = new DataSet();
                     SqlDataAdapter da = new SqlDataAdapter(sCmd);
                     da.Fill(ds);


                     DataTable dt1 = ds.Tables[0];
                     DataTable dt2 = ds.Tables[1];
                     DataTable dt3 = ds.Tables[2];
                     DataTable dt4 = ds.Tables[3];
                    

                     txtNoRoundTrip.Value = Convert.ToString(dt3.Rows.Count);
                  
                     StringBuilder sb = new StringBuilder();
                     if (dt1.Rows.Count > 0)
                     {
                        



                         VOName.Text = Convert.ToString(dt1.Rows[0]["OWNER_NAME"]);
                         VOCareOf.Text = Convert.ToString(dt1.Rows[0]["CARE_OF"]);
                         VOPresentAddress.Text = Convert.ToString(dt1.Rows[0]["PRESENT_ADDRESS"]);
                         VOPermanentAddress.Text  = Convert.ToString(dt1.Rows[0]["PERMANENT_ADDRESS"]);
                         VORegNumber.Text  = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                         txtPermitNo.Text = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);

                         txtCounterSignatureNumber.Text = Convert.ToString(dt1.Rows[0]["COUNTER_SIGNATURE_NUMBER"]);
                         
                         txtvalidityfrom.Value = Convert.ToString(dt1.Rows[0]["PERMIT_VALID_FROM"]);
                         txtvalidityto.Value = Convert.ToString(dt1.Rows[0]["PERMIT_VALID_UPTO"]);
                         VType.Text = Convert.ToString(dt1.Rows[0]["VEHICLE_CLASS_TYPE"]);
                         VChassisNumber.Text = Convert.ToString(dt1.Rows[0]["CHASIS_NUMBER"]);
                         VEngineNumber.Text = Convert.ToString(dt1.Rows[0]["ENGINE_NUMBER"]);
                         VMakerDesc.Text = Convert.ToString(dt1.Rows[0]["MAKER_DESC"]);
                         VMakerModel.Text = Convert.ToString(dt1.Rows[0]["MAKER_MODEL"]);
                         txtULW.Text = Convert.ToString(dt1.Rows[0]["ULW"]);
                         txtRLW.Text = Convert.ToString(dt1.Rows[0]["RLW"]);
                         txtNatureOfGoods.Text = Convert.ToString(dt1.Rows[0]["NATURE_OF_GOODS_CARRIED"]);
                         VSeatCapacity.Text = Convert.ToString(dt1.Rows[0]["SEATING_CAPACITY"]);
                         txtsleeper.Text = Convert.ToString(dt1.Rows[0]["SLEEPER_CAPACITY"]);
                         VManufacturingON.Value = Convert.ToString(dt1.Rows[0]["MANUFACTURING_ON"]);
                         txtICValidUpto.Value = Convert.ToString(dt1.Rows[0]["IC_VALID_UPTO"]);
                         txtPCValidUpto.Value = Convert.ToString(dt1.Rows[0]["PC_VALID_UPTO"]);
                         txtTPValidUpto.Value = Convert.ToString(dt1.Rows[0]["TAX_PAID_UPTO"]);
                         txtFCValidUpto.Value = Convert.ToString(dt1.Rows[0]["FC_VALID_UPTO"]);
                         txtMobileNo.Text = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);
                         txtEmailId.Text = Convert.ToString(dt1.Rows[0]["EMAIL_ID"]);

                         txttaxRefNo.Text = Convert.ToString(dt1.Rows[0]["FEE_TAX_PAY_TRANSACTION_ID"]);
                         txttaxRefDated.Value = Convert.ToString(dt1.Rows[0]["FEE_TAX_PAID_ON"]);

                         txtRecommendationNo.Text = Convert.ToString(dt1.Rows[0]["RECOMMENDATION_NUMBER"]);
                         txtRecommendationNoDated.Value = Convert.ToString(dt1.Rows[0]["RECOMMENDATION_DATE"]);


                         txttaxpayAmount.Text = Convert.ToString(dt1.Rows[0]["FEE_TAX_AMOUNT"]);
                         txtTAXpaidfrom.Value = Convert.ToString(dt1.Rows[0]["FEE_TAX_PAID_FROM"]);
                         txtTAXpaidupto.Value = Convert.ToString(dt1.Rows[0]["FEE_TAX_PAID_UP_TO"]);
                         ddltaxperiod.SelectedValue = Convert.ToString(dt4.Rows[0]["PAY_MODE"]);
                         if (Convert.ToString(dt1.Rows[0]["PERMIT_TYPE"]) == "Temporary")
                             ddlPermitType.SelectedValue = "1";
                         else
                             ddlPermitType.SelectedValue = "2";
                         
                         RouteHDRInfo.Text = dt1.Rows[0]["ROUTE"].ToString();
                     
                     }

                     if (dt3.Rows.Count > 0)
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
                         sb.Append("<tbody id=tbdRouteInfo_>");
                         StringBuilder ss = new StringBuilder();
                         try
                         {
                            

                             for (int k = 1; k <= dt3.Rows.Count; k++)
                             {
                                 if (dt2.Rows.Count > 0)
                                 {
                                     DataRow[] drw = dt2.Select("SEQUENCE='" + k + "'");
                                     DataRow dr = drw[0];
                                     if (drw.Length > 0)
                                     {

                                         Int32 length = dt3.Rows.Count;




                                         if (k == 1)
                                         {

                                             ss.Append("<tr id='tr" + k + "_" + Convert.ToString(dr[2]) + "'>");
                                             ss.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dr[2]) + "'>" + Convert.ToString(dr[3]) + "</label></td>");
                                             ss.Append("<td> ..... </td>");
                                             ss.Append("<td><div class='input-group'><input type='text' value=" + Convert.ToString(dr[5]) + "  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dr[2]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dr[2]) + "\")" + "' placeholder='' /></div></td>");
                                             ss.Append("<td></td>");
                                             ss.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dr[2]) + "'>" + Convert.ToString(dr[6]) + "</td>");
                                             ss.Append("<td> ..... </td>");
                                             ss.Append("<td><div class='input-group'><input type='text' value=" + Convert.ToString(dr[8]) + " class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dr[2]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dr[2]) + "\")" + "' placeholder='' /></div></td>");
                                             ss.Append("</tr>");



                                         }
                                         else if (k == length)
                                         {

                                             ss.Append("<tr id='tr" + k + "_" + Convert.ToString(dr[2]) + "'>");
                                             ss.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dr[2]) + "'>" + Convert.ToString(dr[3]) + "</label></td>");
                                             ss.Append("<td><div class='input-group'><input type='text' value=" + Convert.ToString(dr[4]) + "  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dr[2]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dr[2]) + "\")" + "' placeholder='' /></div></td>");
                                             ss.Append("<td> ..... </td>");
                                             ss.Append("<td></td>");
                                             ss.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dr[2]) + "'>" + Convert.ToString(dr[6]) + "</td>");
                                             ss.Append("<td><div class='input-group'><input type='text' value=" + Convert.ToString(dr[7]) + "  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dr[2]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dr[2]) + "\")" + "'  placeholder=''/></div></td>");
                                             ss.Append("<td> ..... </td>");
                                             ss.Append("</tr>");



                                         }
                                         else
                                         {

                                             ss.Append("<tr id='tr" + k + "_" + Convert.ToString(dr[2]) + "'>");
                                             ss.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dr[2]) + "'>" + Convert.ToString(dr[3]) + "</label></td>");
                                             ss.Append("<td><div class='input-group'><input type='text' value=" + Convert.ToString(dr[4]) + " class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dr[2]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dr[2]) + "\")" + "' placeholder='' /></div></td>");
                                             ss.Append("<td><div class='input-group'><input type='text' value=" + Convert.ToString(dr[5]) + " class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dr[2]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dr[2]) + "\")" + "' placeholder='' /></div></td>");
                                             ss.Append("<td></td>");
                                             ss.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dr[2]) + "'>" + Convert.ToString(dr[6]) + "</td>");
                                             ss.Append("<td><div class='input-group'><input  type='text' value=" + Convert.ToString(dr[7]) + "  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dr[2]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dr[2]) + "\")" + "' placeholder=''/></div></td>");
                                             ss.Append("<td><div class='input-group'><input  type='text' value=" + Convert.ToString(dr[8]) + "  class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dr[2]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dr[2]) + "\")" + "' placeholder=''/></div></td>");
                                             ss.Append("</tr>");


                                         }




                                     }
                                 }
                             }
                         }
                         catch (Exception ee)
                         {
                             ss = new StringBuilder();
                             String route = RouteHDRInfo.Text;//Dumka - Sainthia Via PATTABARI, BARAMASIA, ASABANI, RANISHWAR, MAHESHKOLA, SAWARKURI, MD BAZAR
                             String[] route_list;
                             String[] route_mast_list;
                             string[] stringSeparators = new string[] { "<b>Via</b>" };
                             route_list = route.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                             route_mast_list = route_list[0].Split('-');
                             route_list = route_list[1].Split('-');
                             route_list = route_list[0].Split(',');
                             int k = 0;


                             ss.Append("<tr id='tr1_1'>");
                             ss.Append("<td><label id='lblUPRoute1_1'>" + route_mast_list[1].Trim() + "</label></td>");
                             ss.Append("<td><div class='input-group'>---</div></td>");
                             ss.Append("<td><div class='input-group'><input type='text' value='' class='form-control mask_route' id='txtUPDepRoute" + "1" + "_" + "1" + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + "1" + "_" + 1 + "\")" + "' placeholder='' /></div></td>");
                             ss.Append("<td></td>");
                             ss.Append("<td id='lblDownRoute" + 1 + "_" + 1 + "'>" + route_mast_list[0].Trim() + "</td>");
                             ss.Append("<td><div class='input-group'>----</div></td>");
                             ss.Append("<td><div class='input-group'><input  type='text' value=''  class='form-control mask_route' id='txtDownDepRoute" + 1 + "_" + 1 + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + 1 + "_" + 1 + "\")" + "' placeholder=''/></div></td>");
                             ss.Append("</tr>");

                             for (k = 0; k<route_list.Length ; k++)
                             {
                                 ss.Append("<tr id='tr" + (k+2) + "_" + (k+2) + "'>");
                                 ss.Append("<td><label id='lblUPRoute" + (k + 2) + "_" + (k + 2) + "'>" + route_list[route_list.Length - 1 - k].Trim() + "</label></td>");
                                 ss.Append("<td><div class='input-group'><input type='text' value='' class='form-control mask_route' id='txtUPArrRoute" + (k + 2) + "_" + (k + 2) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + (k + 2) + "_" + (k + 2) + "\")" + "' placeholder='' /></div></td>");
                                 ss.Append("<td><div class='input-group'><input type='text' value='' class='form-control mask_route' id='txtUPDepRoute" + (k + 2) + "_" + (k + 2) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + (k + 2) + "_" + (k + 2) + "\")" + "' placeholder='' /></div></td>");
                                 ss.Append("<td></td>");
                                 ss.Append("<td id='lblDownRoute" + (k + 2) + "_" + (k + 2) + "'>" + route_list[k].Trim() + "</td>");
                                 ss.Append("<td><div class='input-group'><input  type='text' value=''  class='form-control mask_route' id='txtDownArrRoute" + (k + 2) + "_" + (k + 2) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + (k + 2) + "_" + (k + 2) + "\")" + "' placeholder=''/></div></td>");
                                 ss.Append("<td><div class='input-group'><input  type='text' value=''  class='form-control mask_route' id='txtDownDepRoute" + (k + 2) + "_" + (k + 2) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + (k + 2) + "_" + (k + 2) + "\")" + "' placeholder=''/></div></td>");
                                 ss.Append("</tr>");
                             }


                             k = route_list.Length;
                             ss.Append("<tr id='"+ (k+2)+"_"+ (k+2) +"'>");
                             ss.Append("<td><label id='lblUPRoute" + (k + 2) + "_" + (k + 2) + "'>" + route_mast_list[0].Trim() + "</label></td>");
                             ss.Append("<td><div class='input-group'><input type='text' value=''  class='form-control mask_route' id='txtUPArrRoute" + (k+2) + "_" + (k+2) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + (k+2) + "_" + (k+2) + "\")" + "' placeholder='' /></div></td>");
                             ss.Append("<td>----</td>");
                             ss.Append("<td></td>");
                             ss.Append("<td id='lblDownRoute" + (k + 2) + "_" + (k + 2) + "'>" + route_mast_list[1].Trim() + "</td>");
                             ss.Append("<td><div class='input-group'><input type='text' value='' class='form-control mask_route' id='txtDownArrRoute" + (k+2) + "_" + (k+2) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + (k+2) + "_" + (k+2)+ "\")" + "'  placeholder=''/></div></td>");
                             ss.Append("<td>----</td>");
                             ss.Append("</tr>");

                             

                         }
                         sb.Append(ss);
                         sb.Append("<tbody>");
                         sb.Append("</table>");
                         RouteTimeInfo.InnerHtml = sb.ToString();
                    
                     }
                    
                     

                 }

                 catch (Exception ex)
                 {
                  

                     String Message = ex.Message;
                     Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                     ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='SCREEN_STA_CS_REPORT.aspx'", true);

                 }
             }

  
        }
    }

    private void Alert(string msg)
    {
        if (string.IsNullOrEmpty(msg))
            msg = "Unknown error";

        msg = msg.Replace("\r", " ")
                 .Replace("\n", " ")
                 .Replace("'", "");

        ScriptManager.RegisterClientScriptBlock(
            this,
            GetType(),
            Guid.NewGuid().ToString(),
            "alert('" + msg + "');",
            true);
    }

    private DateTime? SafeParseDate(string value)
    {
        DateTime dt;

        if (!string.IsNullOrWhiteSpace(value) &&
            DateTime.TryParse(value.Trim(), out dt))
        {
            return dt;
        }

        return null;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            /* ================= BASIC SAFETY CHECKS ================= */

            if (Session["EMPLOYEE_ID"] == null)
            {
                Alert("Session expired. Please login again.");
                return;
            }

            if (string.IsNullOrWhiteSpace(hdnID.Value))
            {
                Alert("Application ID missing.");
                return;
            }

            int appId = Convert.ToInt32(hdnID.Value);
            string permitNo = txtPermitNo.Text.Trim();

            DateTime? permitValidFromDate = SafeParseDate(txtvalidityfrom.Value);
            DateTime? permitValidUptoDate = SafeParseDate(txtvalidityto.Value);

            string sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                sCon.Open();

                using (SqlTransaction objTrans = sCon.BeginTransaction())
                using (SqlCommand sCmd = new SqlCommand())
                {
                    sCmd.Connection = sCon;
                    sCmd.Transaction = objTrans;

                    try
                    {
                        /* ================= MASTER UPDATE ================= */

                        sCmd.CommandText = "SCREEN_STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();

                        sCmd.Parameters.AddWithValue("@APP_ID", appId);
                        sCmd.Parameters.AddWithValue("@APPROVED_BY", Session["EMPLOYEE_ID"].ToString());

                        sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", permitNo);
                        sCmd.Parameters.AddWithValue("@PERMIT_VALID_FROM", (object)permitValidFromDate ?? DBNull.Value);
                        sCmd.Parameters.AddWithValue("@PERMIT_VALID_UPTO", (object)permitValidUptoDate ?? DBNull.Value);
                        sCmd.Parameters.AddWithValue("@COUNTER_SIGNATURE_NUMBER", txtCounterSignatureNumber.Text.Trim());

                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text.Trim());
                        sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text.Trim());
                        sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text.Trim());
                        sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text.Trim());
                        sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text.Trim());
                        sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", ddlPermitType.SelectedValue);

                        if (ddlPermitType.SelectedValue == "1")
                        {
                            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", "15");
                            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", "2");
                        }
                        else
                        {
                            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", "16");
                            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", "4");
                        }

                        /* ================= MANUFACTURING DATE ================= */

                        DateTime? manufacturingDate = SafeParseDate(VManufacturingON.Value);

                        sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", VType.Text);
                        sCmd.Parameters.AddWithValue("@CHASIS_NUMBER", VChassisNumber.Text);
                        sCmd.Parameters.AddWithValue("@ENGINE_NUMBER", VEngineNumber.Text);
                        sCmd.Parameters.AddWithValue("@MAKER_DESC", VMakerDesc.Text);
                        sCmd.Parameters.AddWithValue("@MAKER_MODEL", VMakerModel.Text);
                        sCmd.Parameters.AddWithValue("@ULW", txtULW.Text);
                        sCmd.Parameters.AddWithValue("@RLW", txtRLW.Text);
                        sCmd.Parameters.AddWithValue("@SEATING_CAPACITY", VSeatCapacity.Text);
                        sCmd.Parameters.AddWithValue("@SLEEPER_CAPACITY", txtsleeper.Text);
                        sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", (object)manufacturingDate ?? DBNull.Value);
                        sCmd.Parameters.AddWithValue("@NATURE_OF_GOODS_CARRIED", txtNatureOfGoods.Text);

                        sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                        sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());

                        /* ================= SAFE DATE FIELDS ================= */

                        sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", (object)SafeParseDate(txtICValidUpto.Value) ?? DBNull.Value);
                        sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", (object)SafeParseDate(txtPCValidUpto.Value) ?? DBNull.Value);
                        sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO", (object)SafeParseDate(txtTPValidUpto.Value) ?? DBNull.Value);
                        sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", (object)SafeParseDate(txtFCValidUpto.Value) ?? DBNull.Value);

                        sCmd.Parameters.AddWithValue("@FEE_TAX_AMOUNT", txttaxpayAmount.Text.Trim());
                        sCmd.Parameters.AddWithValue("@FEE_TAX_PAID_FROM", (object)SafeParseDate(txtTAXpaidfrom.Value) ?? DBNull.Value);
                        sCmd.Parameters.AddWithValue("@FEE_TAX_PAID_UP_TO", (object)SafeParseDate(txtTAXpaidupto.Value) ?? DBNull.Value);
                        sCmd.Parameters.AddWithValue("@PAY_MODE", ddltaxperiod.SelectedValue);

                        sCmd.Parameters.AddWithValue("@FEE_TAX_PAY_TRANSACTION_ID", txttaxRefNo.Text.Trim());
                        sCmd.Parameters.AddWithValue("@FEE_TAX_PAID_ON", (object)SafeParseDate(txttaxRefDated.Value) ?? DBNull.Value);

                        sCmd.Parameters.AddWithValue("@RECOMMENDATION_NUMBER", txtRecommendationNo.Text.Trim());
                        sCmd.Parameters.AddWithValue("@RECOMMENDATION_DATE", (object)SafeParseDate(txtRecommendationNoDated.Value) ?? DBNull.Value);

                        sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXIST1");

                        var Cnt = sCmd.ExecuteScalar();

                        if (Convert.ToInt32(Cnt) != 0)
                        {
                            Alert("Vehicle Registration Number already exists.");
                            return;
                        }

                        /* ================= UPDATE MASTER ================= */

                        sCmd.Parameters["@MODE"].Value = "MSTR_UPDATE";
                        sCmd.ExecuteNonQuery();

                        /* ================= ROUTE JSON SAFETY ================= */

                        if (string.IsNullOrWhiteSpace(txtroutjson.Value))
                        {
                            throw new Exception("Route data missing.");
                        }

                        var serializer = new JavaScriptSerializer();
                        PERMIT_APPLICATION_LDR rDTO =
                            serializer.Deserialize<PERMIT_APPLICATION_LDR>(txtroutjson.Value);

                        if (rDTO == null || rDTO.PERMIT_APPLICATION_LDRs == null)
                        {
                            throw new Exception("Invalid route data.");
                        }

                        foreach (PERMIT_APPLICATION_LDR dr in rDTO.PERMIT_APPLICATION_LDRs)
                        {
                            sCmd.CommandText = "SCREEN_STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();

                            sCmd.Parameters.AddWithValue("@APP_ID", appId);
                            sCmd.Parameters.AddWithValue("@SEQUENCE", dr.ROUTE_DETAIL_ID);
                            sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                            sCmd.Parameters.AddWithValue("@ARRIVAL_TIMING", dr.ARRIVAL_TIMING);
                            sCmd.Parameters.AddWithValue("@DEPARTURE_TIMING", dr.DEPARTURE_TIMING);
                            sCmd.Parameters.AddWithValue("@MODE", dr.MODE);
                            sCmd.Parameters.AddWithValue("@OP_MODE", "LDR_INSERT_MSTR");

                            sCmd.ExecuteNonQuery();
                        }

                        objTrans.Commit();

                        ScriptManager.RegisterClientScriptBlock(
                            this, GetType(), Guid.NewGuid().ToString(),
                            "alert('Successfully Updated');window.location ='SCREEN_STA_CS_REPORT.aspx';",
                            true);
                    }
                    catch (Exception ex)
                    {
                        objTrans.Rollback();
                        Alert("Submit Failed: " + ex.Message);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Alert("Unexpected Error: " + ex.Message);
        }
    }
}
    
    



    