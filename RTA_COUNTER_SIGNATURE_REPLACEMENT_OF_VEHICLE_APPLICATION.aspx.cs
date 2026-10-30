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
using System.Net;
using System.IO;
using System.Globalization;
using System.Collections;



public partial class RTA_COUNTER_SIGNATURE_REPLACEMENT_OF_VEHICLE_APPLICATION : System.Web.UI.Page
{
    public string FillAPPlyRouteList { get; set; }
    //private static string TYPE_ID;
    //private static string TYPE_OPTION_ID;
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
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
            else
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

                    hdnType.Value = IDx.Value;
                    hdnTypeOptionID.Value = IDx1.Value;
                    PageTitle PT = new PageTitle();
                    DataTable dt1 = new DataTable();
                    dt1 = PT.PageRTAHeading(Convert.ToInt32(hdnType.Value), Convert.ToInt32(hdnTypeOptionID.Value));


                    if (dt1.Rows.Count > 0)
                    {
                        Page.Title = dt1.Rows[0]["RTA_TITLE"].ToString();
                        PageHeading.InnerText = dt1.Rows[0]["RTA_HEADING"].ToString();
                        PageHeading1.InnerText = dt1.Rows[0]["RTA_TITLE"].ToString();

                    }

                }

                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

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
                sCmd.CommandText = "RTA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnType.Value);
                sCmd.Parameters.AddWithValue("@MODE", "VEHICLE_REPLACE_SEARCH");
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
                //sCmd.CommandText = "RTA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_HDR_AED";
                sCmd.CommandText = "SP_VERIFY_SEARCH";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnType.Value);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "RTA_VERIFY_MOBILE_NUMBER");
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
                    ss.Sendsms(mobNo, text,"1307161527893297291");
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
                sCmd.CommandText = "RTA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnType.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnTypeOptionID.Value);
                sCmd.Parameters.AddWithValue("@OTP", txtOTP.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "VERIFY_OTP");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);
                DataTable dt1 = ds.Tables[0];

                if (dt1.Columns[0].ColumnName == "MSG")
                {

                    //Message.Text = "OTP not match. Please Click on Clear button and try again.";
                    Message.Text = dt1.Rows[0]["MSG"].ToString();
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
                    DataTable dt5 = ds.Tables[4];

                       StringBuilder sb = new StringBuilder();
                    StringBuilder sb1 = new StringBuilder();

                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    lblPermitRTO.Text = dt1.Rows[0]["RTO_NAME"].ToString();
                    lblPermitRTOCode.Text = dt1.Rows[0]["RTO_CODE"].ToString();
                    lblOldAppNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    lblPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                    lblValidityFrom.Text = dt1.Rows[0]["VALID_FROM"].ToString();
                    lblValidityUpto.Text = dt1.Rows[0]["VALID_UP_TO"].ToString();
                    txtRouteId.Text = dt1.Rows[0]["ROUTE_ID"].ToString();

                    VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    VOCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                    VOPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                    VOPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();


                    VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    VType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                    VChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                    VEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();

                    lblMakerDesc.Text = dt1.Rows[0]["MAKER_DESC"].ToString();
                    lblMakermodel.Text = dt1.Rows[0]["MAKER_MODEL"].ToString();
                    lblFuelDesc.Text = dt1.Rows[0]["FUEL_DESC"].ToString();
                    lblVcolor.Text = dt1.Rows[0]["VEHICLE_COLOR"].ToString();
                    VFitness.Text = dt1.Rows[0]["FITNESS_VALID_UPTO"].ToString();
                    VTax.Text = dt1.Rows[0]["TAX_PAID_UPTO"].ToString();
                    VULW.Text = dt1.Rows[0]["ULW"].ToString();
                    VRLW.Text = dt1.Rows[0]["RLW"].ToString();
                    VSeatCapacity.Text = dt1.Rows[0]["SEATING_CAPACITY"].ToString();
                    VManufacturingON.Text = dt1.Rows[0]["MANUFACTURING_ON"].ToString();


                    VOMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                    VOEmailID.Text = dt1.Rows[0]["EMAIL_ID"].ToString();
                    lblGender.Text = dt1.Rows[0]["GENDER"].ToString();
                    lblQualification.Text = dt1.Rows[0]["APPLICANT_QUALIFICATION"].ToString();
                    lblCategory.Text = dt1.Rows[0]["APPLICANT_CATEGORY"].ToString();
                    lblExArmy.Text = dt1.Rows[0]["EX_ARMY"].ToString();
                    lblMinority.Text = dt1.Rows[0]["MINORITY"].ToString();
                    lblHandicapped.Text = dt1.Rows[0]["HANDICAPPED"].ToString();
                    lblHML.Text = dt1.Rows[0]["HEAVY_MOTOR_LICENCE"].ToString();
                    lblIdentityProof.Text = dt1.Rows[0]["IDENTITY_PROOF"].ToString();
                    lblAC.Text = dt1.Rows[0]["AIR_CONDITIONED"].ToString();
                    lblDeluxe.Text = dt1.Rows[0]["DELUXE"].ToString();
                    lblSleeper.Text = dt1.Rows[0]["SLEEPER"].ToString();



                    RouteHDRInfo.Text = "<b>Route :</b> " + dt1.Rows[0]["ROUTE"].ToString() + " <br /><b>No.of Round Trips : </b>" + dt1.Rows[0]["NO_OF_TRIPS"].ToString();
                    if (dt5.Rows.Count > 0)
                    {
                        ddlCSRTO.Items.Clear();
                        ddlCSRTO.Items.Add(new ListItem("--select--", "0"));

                        sb1.Append("<table class='table table-striped table-bordered table-hover'>");
                        sb1.Append("<thead>");

                        sb1.Append("<tr>");
                        sb1.Append("<th> RTO Name (For Counter Signature) </th>");
                        sb1.Append("<th> Counter Signature Status</th>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        sb1.Append("<tbody>");
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {
                            if (Convert.ToString(dt5.Rows[i]["CS_STATUS"]) == "" || Convert.ToString(dt5.Rows[i]["CS_STATUS"]) == null)
                            {
                                ddlCSRTO.Items.Add(new ListItem(Convert.ToString(dt5.Rows[i]["RTO_NAME"]), Convert.ToString(dt5.Rows[i]["RTO_CODE"])));
                            }

                            sb1.Append("<tr>");
                            sb1.Append("<td><label>" + Convert.ToString(dt5.Rows[i]["RTO_NAME"]) + "</label></td>");
                            sb1.Append("<td><label>" + Convert.ToString(dt5.Rows[i]["CS_STATUS"]) + "</label></td>");
                            sb1.Append("</tr>");

                        }
                        sb1.Append("</tbody>");
                        sb1.Append("</table>");
                        CSRTO.InnerHtml = sb1.ToString();
                        ddlCSRTO.SelectedValue = "0";

                    }
                    else
                    {
                        CSRTO.InnerHtml = "";
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
                                    sb.Append("</tbody>");
                                    sb.Append("</table>");

                                }
                            }
                        }
                        RouteTimeInfo.InnerHtml = sb.ToString();

                    }
                    if (dt4.Rows.Count > 0)
                    {
                        lblReplacevehicleFees.Text = dt4.Rows[0]["FEE"].ToString();
                    }
                    else
                    {
                        lblReplacevehicleFees.Text = "";
                    }

                    txtOTP.Disabled = true;
                    btnVerify.Enabled = false;
                    Message.Visible = false;
                    Panel1.Visible = true;

                    
                }
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');ComponentsDateTimePickers.init();", true);
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
        txtExistRegNo.Text = "";
        txtExistPermitNo.Text = "";
        Message.Visible = false;
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
 
        
               

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;

       using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
                
                sCmd.CommandText = "RTA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnType.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnTypeOptionID.Value);
                sCmd.Parameters.AddWithValue("@RTO_CODE", ddlCSRTO.SelectedValue);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", lblPermitNo.Text);
                sCmd.Parameters.AddWithValue("@PERMIT_VALID_FROM", DateTime.ParseExact(lblValidityFrom.Text, "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PERMIT_VALID_UPTO", DateTime.ParseExact(lblValidityUpto.Text, "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@ROUTE_ID", txtRouteId.Text);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
                sCmd.Parameters.AddWithValue("@FEE_TAX_AMOUNT", lblReplacevehicleFees.Text);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", VOMobileNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@EMAIL_ID", VOEmailID.Text.Trim());
                sCmd.Parameters.AddWithValue("@OLD_APPLICATION_NUMBER", lblOldAppNo.Text);
                sCmd.Parameters.AddWithValue("@MODE", "DUPLICACYEXIST");
                var obj3 = sCmd.ExecuteScalar();

                Int32 cntval;
                cntval = Convert.ToInt32(obj3);
                ArrayList validate = uploadFileValidate();
                string result = string.Empty;
                bool result1 = true;
                foreach (var str in validate)
                {
                    if (str == "OK") { }
                    else
                    {
                        result1 = false;
                        result = result + "  " + str;

                    }

                }
                if (result1 == true)
                {




                    if (cntval == 0)
                    {
                        sCmd.CommandText = "RTA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_HDR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters["@MODE"].Value = "ADD";
                       

                        Int32 AppId;
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);


                        DataTable dt1 = new DataTable();
                        dt1 = ds.Tables[0];
                        AppId = Convert.ToInt32(dt1.Rows[0]["APP_ID"]);

                        uploadFile(AppId, sCmd);

                        objTrans.Commit();
                        string RegistrationNo = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                        string permitNo = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);
                        string mobileNo = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);
                        string ApplicationNo = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);
                        string permitType = Convert.ToString(dt1.Rows[0]["PERMIT_TYPE"]);
                        string sevicename = Convert.ToString(dt1.Rows[0]["APPLICATION_SERVICE"]);
                        SMSSender objSms = new SMSSender();

                        objSms.ApplicationNumberSms(RegistrationNo, permitNo, sevicename, permitType, mobileNo, ApplicationNo);
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for Vehicle Replacement');window.location ='RTA_COUNTER_SIGNATURE_REPLACEMENT_OF_VEHICLE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(AppId) + "&TYPEID=" + Convert.ToString(hdnType.Value) + "&OPTIONID=" + Convert.ToString(hdnTypeOptionID.Value), true), Response.ContentEncoding) + "'", true);
                    }
                    else
                    {
                        objTrans.Commit();
                        // Response.Redirect("PermitHomePage.aspx");
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Replacement of Vehicle is already exists for this Registration Number');window.location ='PermitHomePage.aspx'", true);
                    }
                }
                else
                {
                    objTrans.Rollback();
                    String Message = result;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
                }

            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);
                // Response.Redirect("PermitHomePage.aspx");
            }
        }
    }

    private ArrayList uploadFileValidate()
    {
        ArrayList resulArray = new ArrayList();
        FileUploadValidations validatefile = new FileUploadValidations();
        if (uploadpermit.HasFile)
        {
            resulArray.Add(validatefile.FileValidate(uploadpermit.FileName, uploadpermit.FileContent));
        }
        return resulArray;
    
    }

    private void uploadFile(int appId, SqlCommand sCmd)
    {
        string Text;

        DateTime dt = new DateTime();


        string uniqueFileNamPrfix = "";

        
        if (uploadpermit.HasFile)
        {
            
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "CS_REP_OF_VEHICLE_PERMIT_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadpermit.FileName);
                string result = uploadProceudureCall(Convert.ToString(appId), "PERMIT", filename, sCmd);
                if (result == "Done")
                    uploadpermit.SaveAs(@"\\10.30.9.199\d\JHT\RTAUploadedCSFiles/" + "CS_REP_OF_VEHICLE_PERMIT_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadpermit.FileName));

                Text = filename;


           

        }
        
    }
    private string uploadProceudureCall(string appId, string uploadHeads, string fileName, SqlCommand sCmd)
    {
        sCmd.CommandText = "RTA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_UPLOAD_AED";
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
        txtExistRegNo.Text = "";
        txtExistPermitNo.Text = "";
        Message.Visible = false;
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

        
    }
}