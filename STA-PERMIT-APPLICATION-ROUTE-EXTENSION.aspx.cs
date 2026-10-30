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


public partial class STA_PERMIT_APPLICATION_ROUTE_EXTENSION : System.Web.UI.Page
{
    // private static string ID;

    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtMobileNo.MaxLength = 10;
        txtMobileNumber.MaxLength = 10;
        txtExistRegNo.MaxLength = 12;
        txtExistPermitNo.MaxLength = 20;
        txtOTP.MaxLength = 4;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
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

                    QueryString IDx = QueryStr.Where(l => l.Name == "TYPEID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                    QueryString IDx1 = QueryStr.Where(l => l.Name == "OPTIONID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);

                    hdnTypeID.Value = IDx.Value;
                    hdnTypeOptionID.Value = IDx1.Value;
                    PageTitle PT = new PageTitle();
                    DataTable dt1 = new DataTable();
                    dt1 = PT.PageHeading(Convert.ToInt32(hdnTypeID.Value), Convert.ToInt32(hdnTypeOptionID.Value));


                    if (dt1.Rows.Count > 0)
                    {
                        Page.Title = dt1.Rows[0]["STA_TITLE"].ToString();
                        PageHeading.InnerText = dt1.Rows[0]["STA_HEADING"].ToString();
                        PageHeading1.InnerText = dt1.Rows[0]["STA_TITLE"].ToString();

                    }

                    }

               

                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                }

            }
            

        }
    }


    protected void btnGO_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {
                sCmd.CommandText = "STA_PERMIT_ROUTE_EXTENSION_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@MODE", "SEARCH");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);


                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[0];
                if (dt1.Columns[0].ColumnName == "MSG")
                {
                    Message.Text = dt1.Rows[0]["MSG"].ToString();
                    Message.Visible = true;
                    Panel1.Visible = false;
                    DivMobileValidate.Visible = false;
                    DivOTPMatch.Visible = false;
                }

                else
                {
                    DivMobileValidate.Visible = true;
                    DivOTPMatch.Visible = false;
                    Panel1.Visible = false;
                    txtExistRegNo.Enabled = false;
                    txtExistPermitNo.Enabled = false;
                    btnGO.Enabled = false;
                    Message.Text = "";
                    Message.Visible = false;
                }

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Message + "');window.location ='PermithomePage.aspx'", true);

            }
        }

    }
    protected void btnOTP_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {
                //sCmd.CommandText = "STA_PERMIT_ROUTE_EXTENSION_APPLICATION_HDR_AED";
                sCmd.CommandText = "SP_VERIFY_SEARCH";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "VERIFY_MOBILE_NUMBER");
                //var obj1 = sCmd.ExecuteScalar();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[0];
                if (dt1.Columns[0].ColumnName == "MSG")
                {
                    Message.Text = "Mobile number not match";
                    txtMobileNumber.Value = "";
                    Message.Visible = true;
                    DivMobileValidate.Visible = true;
                    DivOTPMatch.Visible = false;
                    Panel1.Visible = false;
                }
                else
                {
                    string mobNo = Convert.ToString(txtMobileNumber.Value.Trim());
                    string text = Convert.ToString(dt1.Rows[0]["OTP_TEXT"].ToString());
                    SMSSender ss = new SMSSender();
                    ss.Sendsms(mobNo, text, "1307161527893297291");
                    Message.Text = "";
                    Message.Visible = false;
                    txtMobileNumber.Disabled = true;
                    btnOTP.Enabled = false;
                    DivMobileValidate.Visible = true;
                    DivOTPMatch.Visible = true;
                    Panel1.Visible = false;


                }
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);

            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

            }
        }
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {
                sCmd.CommandText = "STA_PERMIT_ROUTE_EXTENSION_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnTypeOptionID.Value);
                sCmd.Parameters.AddWithValue("@OTP", txtOTP.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "VERIFY_OTP");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);
                DataTable dt1 = ds.Tables[0];

                if (dt1.Columns[0].ColumnName == "MSG")
                {

                    Message.Text = "OTP not match. Please Click on Clear button and try again.";
                    Message.Visible = true;
                    DivMobileValidate.Visible = false;
                    DivOTPMatch.Visible = false;
                    Panel1.Visible = false;

                }

                else
                {
                    DataTable dt2 = ds.Tables[1];
                    DataTable dt3 = ds.Tables[2];
                    DataTable dt4 = ds.Tables[3];

                    StringBuilder sb = new StringBuilder();

                    lblhdnApplicationNo.Text = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);

                    lblPermitNo.Text = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);
                    lblPermitType.Text = Convert.ToString(dt1.Rows[0]["PERMIT_TYPE"]);
                    lblValidUpto.Text = Convert.ToString(dt1.Rows[0]["VALID_UP_TO"]);
                    VORegNumber.Text = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                    VOName.Text = Convert.ToString(dt1.Rows[0]["OWNER_NAME"]);
                    VOCareOf.Text = Convert.ToString(dt1.Rows[0]["CARE_OF"]);
                    VOPresentAddress.Text = Convert.ToString(dt1.Rows[0]["PRESENT_ADDRESS"]);
                    VOPermanentAddress.Text = Convert.ToString(dt1.Rows[0]["PERMANENT_ADDRESS"]);
                    VORegNumber.Text = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                    VType.Text = Convert.ToString(dt1.Rows[0]["VEHICLE_CLASS_TYPE"]);
                    VChassisNumber.Text = Convert.ToString(dt1.Rows[0]["CHASIS_NUMBER"]);
                    VEngineNumber.Text = Convert.ToString(dt1.Rows[0]["ENGINE_NUMBER"]);
                    lblMakerDesc.Text = Convert.ToString(dt1.Rows[0]["MAKER_DESC"]);
                    lblMakermodel.Text = Convert.ToString(dt1.Rows[0]["MAKER_MODEL"]);
                    lblFuelDesc.Text = Convert.ToString(dt1.Rows[0]["FUEL_DESC"]);
                    lblVcolor.Text = Convert.ToString(dt1.Rows[0]["VEHICLE_COLOR"]);
                    VFitness.Text = Convert.ToString(dt1.Rows[0]["FITNESS_VALID_UPTO"]);
                    VTax.Text = Convert.ToString(dt1.Rows[0]["TAX_PAID_UPTO"]);
                    VULW.Text = Convert.ToString(dt1.Rows[0]["ULW"]);
                    VRLW.Text = Convert.ToString(dt1.Rows[0]["RLW"]);
                    VSeatCapacity.Text = Convert.ToString(dt1.Rows[0]["SEATING_CAPACITY"]);
                    VManufacturingON.Text = Convert.ToString(dt1.Rows[0]["MANUFACTURING_ON"]);
                    txtMobileNo.Text = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);
                    txtEmailId.Text = Convert.ToString(dt1.Rows[0]["EMAIL_ID"]);
                    lblGender.Text = Convert.ToString(dt1.Rows[0]["GENDER"]);
                    lblQualification.Text = Convert.ToString(dt1.Rows[0]["APPLICANT_QUALIFICATION"]);
                    lblCategory.Text = Convert.ToString(dt1.Rows[0]["APPLICANT_CATEGORY"]);
                    lblExArmy.Text = Convert.ToString(dt1.Rows[0]["EX_ARMY"]);
                    lblMinority.Text = Convert.ToString(dt1.Rows[0]["MINORITY"]);
                    lblHandicapped.Text = Convert.ToString(dt1.Rows[0]["HANDICAPPED"]);
                    lblHML.Text = Convert.ToString(dt1.Rows[0]["HEAVY_MOTOR_LICENCE"]);
                    lblIdentityProof.Text = Convert.ToString(dt1.Rows[0]["IDENTITY_PROOF"]);
                    lblAC.Text = Convert.ToString(dt1.Rows[0]["AIR_CONDITIONED"]);
                    lblDeluxe.Text = Convert.ToString(dt1.Rows[0]["DELUXE"]);
                    lblSleeper.Text = Convert.ToString(dt1.Rows[0]["SLEEPER"]);
                    lblIDNo.Text = Convert.ToString(dt1.Rows[0]["IDENTITY_PROOF_NO"]);

                    RouteHDRInfo.Text = Convert.ToString(dt1.Rows[0]["ROUTE"]);

                    if (dt3.Rows.Count > 0)
                    {
                        HFNoOftrips.Value = dt3.Rows.Count.ToString();
                       
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
                                    sb.Append("<tbody id='tbdRouteInfo_" + k + "'>");
                                    for (int i = 0; i < dr.Length; i++)
                                    {


                                       
                                        int r = i + 1;
                                        int p = i + 2;
                                        int q = p + 1;
                                       
                                        if (i == 0)
                                        {
                                            sb.Append("<tr id='tr" + k + "_" + r + "'>");
                                            sb.Append("<td><input type='text' class='form-control regtxt' style='width:80%' id='lblUPRoute" + k + "_" + r + "' /></td>");
                                            sb.Append("<td> ..... </td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + r + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + r + "\")" + "' placeholder=''   /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td></td>");
                                            sb.Append("<td><input type='text' class='form-control regtxt' style='width:80%' id='lblDownRoute" + k + "_" + r + "' /></td>");
                                            sb.Append("<td> ..... </td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtDownDepRoute" + k + "_" + r + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + r + "\")" + "' placeholder=''  /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("</tr>");
                                            sb.Append("<tr id='tr" + k + "_" + p + "'>");
                                            sb.Append("<td><label id='lblUPRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td></td>");
                                            sb.Append("<td><label  id='lblDownRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</label></td>");
                                            sb.Append("<td><div class='input-group'><input  type='text' style='width:80%'  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td><div class='input-group'><input  type='text' style='width:80%'  class='form-control mask_route' id='txtDownDepRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("</tr>");
                                      
                                     

                                        }
                                        
                                          else if (i == length)
                                            {
                                                sb.Append("<tr id='tr" + k + "_" + p + "'>");
                                                sb.Append("<td><label id='lblUPRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                                sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("<td></td>");
                                                sb.Append("<td><label  id='lblDownRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</label></td>");
                                                sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control mask_route' id='txtDownArrRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control mask_route' id='txtDownDepRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("</tr>");
                                      
                                    
                                                sb.Append("<tr id='tr" + k + "_" + q + "'>");
                                                sb.Append("<td><input type='text' class='form-control regtxt' style='width:80%' id='lblUPRoute" + k + "_" + q + "' /></td>");
                                                sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + q + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + q + "\")" + "' placeholder=''  /></div></td>");
                                                sb.Append("<td> ..... </td>");
                                                sb.Append("<td></td>");
                                                sb.Append("<td><input type='text' class='form-control regtxt' style='width:80%'  id='lblDownRoute" + k + "_" + q + "' /></td>");
                                                sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtDownArrRoute" + k + "_" + q + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + q + "\")" + "' placeholder=''  /></div></td>");
                                                sb.Append("<td> ..... </td>");
                                                sb.Append("</tr>");
                                            }
                                        else
                                        {
                                            sb.Append("<tr id='tr" + k + "_" + p + "'>");
                                            sb.Append("<td><label id='lblUPRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtUPArrRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtUPDepRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td></td>");
                                            sb.Append("<td><label  id='lblDownRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</label></td>");
                                            sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control mask_route' id='txtDownArrRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                            sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control mask_route' id='txtDownDepRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + p + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
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
                    if (dt4.Rows.Count > 0)
                    {
                        lblRenewalPermitFees.Text = dt4.Rows[0]["FEE"].ToString();
                    }
                    else
                    {
                        lblRenewalPermitFees.Text = "";
                    }
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');ComponentsDateTimePickers.init();", true);
                    string strscript = @"$('.mask_route').inputmask('99:99', {placeholder: '0', });";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), strscript, true);
                    txtOTP.Disabled = true;
                    btnVerify.Enabled = false;
                    Message.Visible = false;
                    Panel1.Visible = true;


                }
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

            }
        }
    }
    protected void btnExistClear_Click(object sender, EventArgs e)
    {
        Message.Text = "";
        Message.Visible = false;
        txtExistRegNo.Text = "";
        txtExistPermitNo.Text = "";
        txtExistRegNo.Enabled = true;
        txtExistPermitNo.Enabled = true;
        btnGO.Enabled = true;
        Panel1.Visible = false;
        DivMobileValidate.Visible = false;
        DivOTPMatch.Visible = false;
        txtMobileNumber.Value = "";
        txtOTP.Value = "";
        txtOTP.Disabled = false;
        btnVerify.Enabled = true;
        txtMobileNumber.Disabled = false;
        btnOTP.Enabled = true;
        txtMobileNo.Text = "";
        txtEmailId.Text = "";
        txtICValidUpto.Value = "";
        txtFCValidUpto.Value = "";
        txtPCValidUpto.Value = "";


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        String input = "";
        input = txtroutjson.Value;
         
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;
        PERMIT_APPLICATION_LDR rDTO = new JavaScriptSerializer().Deserialize<PERMIT_APPLICATION_LDR>(input);

        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
                sCmd.CommandText = "STA_PERMIT_ROUTE_EXTENSION_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@APP_ID", 0);

                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnTypeOptionID.Value);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", lblPermitNo.Text);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.ToString().Trim());
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.ToString().Trim());
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@VALID_UPTO", DateTime.ParseExact(lblValidUpto.Text.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FEE", lblRenewalPermitFees.Text);
               
                
  sCmd.Parameters.AddWithValue("@OLD_APPLICATION_NUMBER",lblhdnApplicationNo.Text);
  sCmd.Parameters.AddWithValue("@CARE_OF",VOCareOf.Text);
  sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS",VOPresentAddress.Text);
  sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS",VOPermanentAddress.Text);
  sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE",VType.Text);
  sCmd.Parameters.AddWithValue("@CHASIS_NUMBER",VChassisNumber.Text);
  sCmd.Parameters.AddWithValue("@ENGINE_NUMBER",VEngineNumber.Text);
  sCmd.Parameters.AddWithValue("@MAKER_DESC",lblMakerDesc.Text);
  sCmd.Parameters.AddWithValue("@MAKER_MODEL",lblMakermodel.Text);
  sCmd.Parameters.AddWithValue("@FUEL_DESC",lblFuelDesc.Text);
  sCmd.Parameters.AddWithValue("@VEHICLE_COLOR",lblVcolor.Text);
  sCmd.Parameters.AddWithValue("@ULW",VULW.Text);
  sCmd.Parameters.AddWithValue("@RLW",VRLW.Text);
  sCmd.Parameters.AddWithValue("@SEATING_CAPACITY",VSeatCapacity.Text);
                DateTime ManufacturingON;
                if (!String.IsNullOrEmpty(VManufacturingON.Text))
                {
                    VManufacturingON.Text = "01/" + VManufacturingON.Text;

                    ManufacturingON = DateTime.ParseExact(VManufacturingON.Text, "d/M/yyyy", CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);
                 
                }
                else
                {

                      
                    ManufacturingON = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }

  sCmd.Parameters.AddWithValue("@MANUFACTURING_ON",ManufacturingON);
  sCmd.Parameters.AddWithValue("@VEHICLE_REGISTERED_AT","");
  sCmd.Parameters.AddWithValue("@GENDER",lblGender.Text);
  sCmd.Parameters.AddWithValue("@APPLICANT_QUALIFICATION",lblQualification.Text);
  sCmd.Parameters.AddWithValue("@APPLICANT_CATEGORY",lblCategory.Text);
  sCmd.Parameters.AddWithValue("@EX_ARMY",lblExArmy.Text);
  sCmd.Parameters.AddWithValue("@MINORITY",lblMinority.Text);
  sCmd.Parameters.AddWithValue("@HANDICAPPED",lblHandicapped.Text);
  sCmd.Parameters.AddWithValue("@HEAVY_MOTOR_LICENCE",lblHML.Text);
  sCmd.Parameters.AddWithValue("@IDENTITY_PROOF",lblIdentityProof.Text);
  sCmd.Parameters.AddWithValue("@AIR_CONDITIONED",lblAC.Text);
  sCmd.Parameters.AddWithValue("@DELUXE",lblDeluxe.Text);
  sCmd.Parameters.AddWithValue("@IDENTITY_PROOF_NO",lblIDNo.Text);
  sCmd.Parameters.AddWithValue("@SLEEPER",lblSleeper.Text);

  sCmd.Parameters.AddWithValue("@FITNESS_VALID_UPTO",DateTime.ParseExact(VFitness.Text, "d/M/yyyy", CultureInfo.InvariantCulture));
  sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO",DateTime.ParseExact(VTax.Text, "d/M/yyyy", CultureInfo.InvariantCulture));
                
                sCmd.Parameters.AddWithValue("@MODE", "ADD");
                //var obj3 = sCmd.ExecuteScalar();

                //Int32 cntval;
                //cntval = Convert.ToInt32(obj3);
                //if (cntval == 0)
                //{
                    ArrayList validate= uploadFileValidate();
               string result = string.Empty;
               bool result1 = true;
               foreach (var str in validate)
               {
                   if(str=="OK")
                   {
                       

                   }
                   else
                   {
                       result1 = false;
                       result = result + "  " + str;

                   }

               }
               if (result1 == true)
                {
                    //sCmd.CommandText = "STA_PERMIT_ROUTE_EXTENSION_APPLICATION_HDR_AED";
                    //sCmd.CommandType = CommandType.StoredProcedure;
                    //sCmd.Parameters["@MODE"].Value = "ADD";
                    var obj = sCmd.ExecuteScalar();

                    Int32 AppId;
                    AppId = Convert.ToInt32(obj);

                   

                    foreach (PERMIT_APPLICATION_LDR dr in rDTO.PERMIT_APPLICATION_LDRs)
                    {

                        sCmd.CommandText = "STA_PERMIT_ROUTE_EXTENSION_APPLICATION_LDR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();

                        sCmd.Parameters.AddWithValue("@APP_ID", AppId);
                        sCmd.Parameters.AddWithValue("@SEQUENCE", dr.ROUTE_DETAIL_ID);
                        sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                        sCmd.Parameters.AddWithValue("@ARRIVAL_TIMING", dr.ARRIVAL_TIMING.ToString());
                        sCmd.Parameters.AddWithValue("@DEPARTURE_TIMING", dr.DEPARTURE_TIMING.ToString());
                        sCmd.Parameters.AddWithValue("@MODE", dr.MODE);
                        sCmd.Parameters.AddWithValue("@ID", dr.ID);
                        sCmd.Parameters.AddWithValue("@OP_MODE", "ADD");
                        sCmd.ExecuteNonQuery();

                    }

                    uploadFile(AppId, sCmd);

                    objTrans.Commit();

                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for Permit Route Extension');window.location ='STA-PERMIT-APPLICATION-ROUTE-EXTENSION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(AppId) + "&TYPEID=" + Convert.ToString(hdnTypeID.Value) + "&OPTIONID=" + Convert.ToString(hdnTypeOptionID.Value), true), Response.ContentEncoding) + "'", true);
                }
               else
               {

                   objTrans.Rollback();
                   String Message = result;
                   Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                   ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

               }
                
                //}
                //else
                //{
                //    objTrans.Commit();
                //    // Response.Redirect("PermitHomePage.aspx");
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Permit Route Extension is already exists for this Registration Number');window.location ='PermitHomePage.aspx'", true);
                //}

            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location ='PermitHomePage.aspx'", true);
                
            }
        }
    }
    private ArrayList uploadFileValidate()
    {
        ArrayList resulArray = new ArrayList();
        FileUploadValidations fileValidate = new FileUploadValidations();


        if (uploadRC.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadRC.FileName, uploadRC.FileContent));
        }
        if (uploadInsur.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadInsur.FileName, uploadInsur.FileContent));
        }

        if (uploadPollution.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadPollution.FileName, uploadPollution.FileContent));
        }

        if (uploadFitness.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadFitness.FileName, uploadFitness.FileContent));
        }
        if (uploadSpeed.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadSpeed.FileName, uploadSpeed.FileContent));
        }

        if (uploadPermit.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadPermit.FileName, uploadPermit.FileContent));
        }
        if (uploadCS.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadCS.FileName, uploadCS.FileContent));
        }
        return resulArray;

    }
    private void uploadFile(int permitId, SqlCommand sCmd)
    {
        string Text;

        DateTime dt = new DateTime();


        string uniqueFileNamPrfix = "";

        bool str = uploadRC.HasFile;
        if (uploadRC.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "ROUTE_EXTENSION_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);

            string result = uploadProceudureCall(Convert.ToString(permitId), "RC", filename, sCmd);
            if (result == "Done")
                uploadRC.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "ROUTE_EXTENSION_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadRC.FileName));

            Text = filename;




        }

        if (uploadInsur.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "ROUTE_EXTENSION_PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadInsur.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "INSURANCE", filename, sCmd);
            if (result == "Done")
                uploadInsur.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "ROUTE_EXTENSION_PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadInsur.FileName));

            Text = filename;




        }

        if (uploadPollution.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "ROUTE_EXTENSION_PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPollution.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "POLLUTION", filename, sCmd);
            if (result == "Done")
                uploadPollution.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "ROUTE_EXTENSION_PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPollution.FileName));

            Text = filename;




        }

        if (uploadFitness.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "ROUTE_EXTENSION_PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadFitness.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "FITNESS", filename, sCmd);
            if (result == "Done")
                uploadFitness.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "ROUTE_EXTENSION_PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadFitness.FileName));

            Text = filename;




        }
        if (uploadSpeed.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "ROUTE_EXTENSION_PERMIT_SPEED_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadSpeed.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "SPEED", filename, sCmd);
            if (result == "Done")
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "ROUTE_EXTENSION_PERMIT_SPEED_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }
        if (uploadPermit.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "ROUTE_EXTENSION_PERMIT_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPermit.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "PERMIT", filename, sCmd);
            if (result == "Done")
                uploadPermit.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "ROUTE_EXTENSION_PERMIT_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPermit.FileName));

            Text = filename;




        }
        if (uploadCS.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "ROUTE_EXTENSION_PERMIT_CS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadCS.FileName);

            string result = uploadProceudureCall(Convert.ToString(permitId), "CS", filename, sCmd);
            if (result == "Done")
                uploadCS.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "ROUTE_EXTENSION_PERMIT_CS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadCS.FileName));

            Text = filename;




        }


    }

    private string uploadProceudureCall(string appId, string uploadHeads, string fileName, SqlCommand sCmd)
    {
        sCmd.CommandText = "STA_PERMIT_ROUTE_EXTENSION_APPLICATION_UPLOAD_AED";
        sCmd.CommandType = CommandType.StoredProcedure;
        sCmd.Parameters.Clear();
        sCmd.Parameters.AddWithValue("@APP_ID", appId);
        sCmd.Parameters.AddWithValue("@UPLOAD_HEADS", uploadHeads);
        sCmd.Parameters.AddWithValue("@FILE_NAME", fileName);
        sCmd.ExecuteNonQuery();
        return "Done";




    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Message.Text = "";
        Message.Visible = false;
        txtExistRegNo.Text = "";
        txtExistPermitNo.Text = "";
        txtExistRegNo.Enabled = true;
        txtExistPermitNo.Enabled = true;
        btnGO.Enabled = true;
        Panel1.Visible = false;
        DivMobileValidate.Visible = false;
        DivOTPMatch.Visible = false;
        txtMobileNumber.Value = "";
        txtOTP.Value = "";
        txtOTP.Disabled = false;
        btnVerify.Enabled = true;
        txtMobileNumber.Disabled = false;
        btnOTP.Enabled = true;
        txtMobileNo.Text = "";
        txtEmailId.Text = "";
        txtICValidUpto.Value = "";
        txtFCValidUpto.Value = "";
        txtPCValidUpto.Value = "";
    }
}

    



    