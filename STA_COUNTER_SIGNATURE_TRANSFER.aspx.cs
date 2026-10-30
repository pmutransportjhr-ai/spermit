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



public partial class STA_COUNTER_SIGNATURE_TRANSFER : System.Web.UI.Page
{
    public string FillAPPlyRouteList { get; set; }
    //private static string TYPE_ID;
    //private static string TYPE_OPTION_ID;
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
                sCmd.CommandText = "STA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@MODE", "TRANSFER_COUNTER_SIGNATURE_SEARCH");
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
               // sCmd.CommandText = "STA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_HDR_AED";

                sCmd.CommandText = "SP_VERIFY_SEARCH";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "CS_VERIFY_MOBILE_NUMBER");
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
                sCmd.CommandText = "STA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_HDR_AED";
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

                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    lblOldAppNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    lblCSNo.Text = dt1.Rows[0]["COUNTER_SIGNATURE_NUMBER"].ToString();
                   
                    
                    labelName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    labelCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                    labelPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                    labelPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();

                    labelRegNo.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                   
                    lablePermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                    labelPermitFrom.Text = dt1.Rows[0]["PERMIT_VALID_FROM"].ToString();
                    labelPermitUpto.Text = dt1.Rows[0]["PERMIT_VALID_UPTO"].ToString();
                    labelMakerDesc.Text = dt1.Rows[0]["MAKER_DESC"].ToString();
                    labelMakermodel.Text = dt1.Rows[0]["MAKER_MODEL"].ToString();
                    labelVehicleType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                    labelChassisNo.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                    labelEngineNo.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();
                    labelSeatcapacity.Text = dt1.Rows[0]["SEATING_CAPACITY"].ToString();
                   

                    labelGVW.Text = dt1.Rows[0]["ULW"].ToString();
                    labelPayLoad.Text = dt1.Rows[0]["RLW"].ToString();
                    labelmanufacturingOn.Text = dt1.Rows[0]["MANUFACTURING_ON"].ToString();
                    labelNatureOfGoods.Text = dt1.Rows[0]["NATURE_OF_GOODS_CARRIED"].ToString();
                  

                    labelMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                    txtMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                    labelEmailId.Text = dt1.Rows[0]["EMAIL_ID"].ToString();
                    txtEmailId.Text = dt1.Rows[0]["EMAIL_ID"].ToString();
                    lblIssuedBy.Text = dt1.Rows[0]["STATE_NAME"].ToString();
                    VORecommendationNo.Text = dt1.Rows[0]["RECOMMENDATION_NUMBER"].ToString();
                    VORecommendationNoDated.Text = dt1.Rows[0]["RECOMMENDATION_DATE"].ToString();
                   


                    RouteHDRInfo.Text = dt1.Rows[0]["ROUTE"].ToString();

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

                sCmd.CommandText = "STA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                DateTime? ManufacturingON;
                if (!String.IsNullOrEmpty(labelmanufacturingOn.Text))
                {
                    labelmanufacturingOn.Text = "01/" + labelmanufacturingOn.Text;
                    ManufacturingON = DateTime.ParseExact(labelmanufacturingOn.Text, "d/M/yyyy", CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);
                }
                else
                {
                    ManufacturingON = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnTypeOptionID.Value);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", labelRegNo.Text);
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", lablePermitNo.Text);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);

                sCmd.Parameters.AddWithValue("@MAKER_DESC", labelMakerDesc.Text);
                sCmd.Parameters.AddWithValue("@MAKER_MODEL", labelMakermodel.Text);
                sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", labelVehicleType.Text);
                sCmd.Parameters.AddWithValue("@CHASIS_NUMBER", labelChassisNo.Text);
                sCmd.Parameters.AddWithValue("@ENGINE_NUMBER", labelEngineNo.Text);
                sCmd.Parameters.AddWithValue("@SEATING_CAPACITY", labelSeatcapacity.Text);
                sCmd.Parameters.AddWithValue("@ULW", labelGVW.Text);
                sCmd.Parameters.AddWithValue("@RLW", labelPayLoad.Text);
                sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", ManufacturingON);
                sCmd.Parameters.AddWithValue("@NATURE_OF_GOODS_CARRIED", labelNatureOfGoods.Text.Trim());
             
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO", DateTime.ParseExact(txtTPValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));

                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
                sCmd.Parameters.AddWithValue("@RECOMMENDATION_NUMBER", txtRecommendationNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@RECOMMENDATION_DATE", DateTime.ParseExact(txtRecommendationDate.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@OLD_APPLICATION_NUMBER", lblOldAppNo.Text);/////////////////////////
                sCmd.Parameters.AddWithValue("@COUNTER_SIGNATURE_NUMBER", lblCSNo.Text);
               
                sCmd.Parameters.AddWithValue("@FEE", Convert.ToDecimal(lblReplacevehicleFees.Text));
                //sCmd.Parameters.AddWithValue("@MODE", "DUPLICACYEXIST");
                sCmd.Parameters.AddWithValue("@MODE", "ADD");
                //var obj3 = sCmd.ExecuteScalar();

                //Int32 cntval;
                //cntval = Convert.ToInt32(obj3);
                ArrayList validate = uploadFileValidate();
                string result = string.Empty;
                bool result1 = true;
                foreach (var str in validate)
                {
                    if (str == "OK")
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

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);


                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0];
                    Int32 AppId;
                    AppId = Convert.ToInt32(dt1.Rows[0]["APP_ID"]);

                    uploadFile(AppId,sCmd);

                    string RegistrationNo = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                    string permitNo = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);
                    string mobileNo = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);
                    string ApplicationNo = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);
                    string permitType = Convert.ToString(dt1.Rows[0]["PERMIT_TYPE"]);
                    string sevicename = Convert.ToString(dt1.Rows[0]["APPLICATION_SERVICE"]);
                    SMSSender objSms = new SMSSender();

                    objSms.ApplicationNumberSms(RegistrationNo, permitNo, sevicename, permitType, mobileNo, ApplicationNo);
                    objTrans.Commit();
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for Transfer of Counter Signature');window.location ='STA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(AppId) + "&TYPEID=" + Convert.ToString(hdnTypeID.Value) + "&OPTIONID=" + Convert.ToString(hdnTypeOptionID.Value), true), Response.ContentEncoding) + "'", true);
                
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
        FileUploadValidations validateFile = new FileUploadValidations();
        if (uploadRC.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadRC.FileName, uploadRC.FileContent));
        }
        if (uploadInsur.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadInsur.FileName, uploadInsur.FileContent));
        }

        if (uploadPollution.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadPollution.FileName, uploadPollution.FileContent));
        }

        if (uploadFitness.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadFitness.FileName, uploadFitness.FileContent));
        }
        if (uploadSpeed.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadSpeed.FileName, uploadSpeed.FileContent));
        }
        if (uploadTax.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadTax.FileName, uploadTax.FileContent));
        }

        if (uploadPermit.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadPermit.FileName, uploadPermit.FileContent));
        }
        if (uploadCS.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadCS.FileName, uploadCS.FileContent));
        }
        if (recommendationUpload.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(recommendationUpload.FileName, recommendationUpload.FileContent));
        }
        return resulArray;

    }
    private void uploadFile(int appId, SqlCommand sCmd)
    {
        string Text;

        DateTime dt = new DateTime();


        string uniqueFileNamPrfix = "";

        bool str = uploadRC.HasFile;
        if (uploadRC.HasFile)
        {
           
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "CS_TRANSFER_RC_"+ appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);

                string result = uploadProceudureCall(Convert.ToString(appId), "RC", filename, sCmd);
                if (result == "Done")
                    uploadRC.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "CS_TRANSFER_RC_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadRC.FileName));

                Text = filename;


           

        }
        if (uploadSpeed.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "CS_TRANSFER_SPEED_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadSpeed.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "SPEED", filename, sCmd);
            if (result == "Done")
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "CS_TRANSFER_SPEED_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }
        if (uploadInsur.HasFile)
        {
            
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "CS_TRANSFER_INSURANCE_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadInsur.FileName);
                string result = uploadProceudureCall(Convert.ToString(appId), "INSURANCE", filename, sCmd);
                if (result == "Done")
                    uploadInsur.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "CS_TRANSFER_INSURANCE_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadInsur.FileName));

                Text = filename;


            

        }

        if (uploadPollution.HasFile)
        {
            
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "CS_TRANSFER_POLLUTION_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPollution.FileName);
                string result = uploadProceudureCall(Convert.ToString(appId), "POLLUTION", filename, sCmd);
                if (result == "Done")
                    uploadPollution.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "CS_TRANSFER_POLLUTION_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPollution.FileName));

                Text = filename;


           

        }

        if (uploadFitness.HasFile)
        {
            
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "CS_TRANSFER_FITNESS_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadFitness.FileName);
                string result = uploadProceudureCall(Convert.ToString(appId), "FITNESS", filename, sCmd);
                if (result == "Done")
                    uploadFitness.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "CS_TRANSFER_FITNESS_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadFitness.FileName));

                Text = filename;


           
            

        }
        if (uploadTax.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "CS_TRANSFER_TAX_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadTax.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "TAX", filename, sCmd);
            if (result == "Done")
                uploadTax.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "CS_TRANSFER_TAX_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadTax.FileName));

            Text = filename;




        }
        if (uploadPermit.HasFile)
        {
            
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "CS_TRANSFER_PERMIT_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPermit.FileName);
                string result = uploadProceudureCall(Convert.ToString(appId), "PERMIT", filename, sCmd);
                if (result == "Done")
                    uploadPermit.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "CS_TRANSFER_PERMIT_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPermit.FileName));

                Text = filename;


           

        }
        if (uploadCS.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "CS_TRANSFER_CS_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadCS.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "CS", filename, sCmd);
            if (result == "Done")
                uploadCS.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "CS_TRANSFER_CS_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadCS.FileName));

            Text = filename;




        }
        if (recommendationUpload.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "CS_TRANSFER_RECOMMENDATION_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(recommendationUpload.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "RECOMMENDATION", filename, sCmd);
            if (result == "Done")
                recommendationUpload.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "CS_TRANSFER_RECOMMENDATION_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(recommendationUpload.FileName));

            Text = filename;




        }
    }
    private string uploadProceudureCall(string appId, string uploadHeads, string fileName, SqlCommand sCmd)
    {
        sCmd.CommandText = "STA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_UPLOAD_AED";
        sCmd.CommandType = CommandType.StoredProcedure;
        sCmd.Parameters.Clear();
        sCmd.Parameters.AddWithValue("@APP_ID", appId);
        sCmd.Parameters.AddWithValue("@UPLOAD_HEADS", uploadHeads);
        sCmd.Parameters.AddWithValue("@FILE_NAME", fileName);
        sCmd.ExecuteNonQuery();
        return "Done";
        

    }
    protected void btnreset_Click(object sender, EventArgs e)
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