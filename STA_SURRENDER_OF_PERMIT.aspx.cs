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



public partial class STA_SURRENDER_OF_PERMIT : System.Web.UI.Page
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
                    String Messqge = ex.Message;
                    Messqge = Messqge.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location ='PermitHomePage.aspx'", true);

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
                sCmd.CommandText = "STA_SURRENDER_OF_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@MODE", "SURRENDER_SEARCH");
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
               // sCmd.CommandText = "STA_SURRENDER_OF_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
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
                sCmd.CommandText = "STA_SURRENDER_OF_PERMIT_APPLICATION_HDR_AED";
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
                   
                    StringBuilder sb = new StringBuilder();

                    hdnApplicationNo.Text = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);

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
                    
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');ComponentsDateTimePickers.init();", true);
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
        txtComments.Text = "";
      

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
                sCmd.CommandText = "STA_SURRENDER_OF_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
            
                
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@OLD_APPLICATION_NUMBER", hdnApplicationNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnTypeOptionID.Value);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", lblPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.ToString().Trim());
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.ToString().Trim());
                sCmd.Parameters.AddWithValue("@COMMENTS", txtComments.Text.ToString().Trim());
                //sCmd.Parameters.AddWithValue("@MODE", "DUPLICACYEXIST");
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

                    //sCmd.CommandText = "STA_SURRENDER_OF_PERMIT_APPLICATION_HDR_AED";
                    //sCmd.CommandType = CommandType.StoredProcedure;
                    //sCmd.Parameters["@MODE"].Value = "ADD";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);


                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0];

                    Int32 AppId;
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

                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for Surrender of Permit');window.location ='STA-SURRENDER-OF-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(AppId) + "&TYPEID=" + Convert.ToString(hdnTypeID.Value) + "&OPTIONID=" + Convert.ToString(hdnTypeOptionID.Value), true), Response.ContentEncoding) + "'", true);
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
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Surrender of Permit is already exists for this Registration Number');window.location ='PermitHomePage.aspx'", true);
                //}

           }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location ='PermitHomePage.aspx'", true);
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
       
        if (uploadPermit.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadPermit.FileName, uploadPermit.FileContent));
        }
        if (uploadCS.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadCS.FileName, uploadCS.FileContent));
        }
        if (uploadOther.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadOther.FileName, uploadOther.FileContent));
        }

        if (uploadeJhtTaxToken.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadeJhtTaxToken.FileName, uploadeJhtTaxToken.FileContent));
        }
        if(vehicleFitness.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(vehicleFitness.FileName, vehicleFitness.FileContent));
        }
        if (VehicleInsurance.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(VehicleInsurance.FileName, VehicleInsurance.FileContent));
        }
        if (VehicleSLD.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(VehicleSLD.FileName, VehicleSLD.FileContent));
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
            string filename = "SURRENDER_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);

            string result = uploadProceudureCall(Convert.ToString(permitId), "RC", filename, sCmd);
            if (result == "Done")
                uploadRC.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "SURRENDER_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadRC.FileName));

            Text = filename;




        }
        if (uploadCS.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SURRENDER_PERMIT_CS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadCS.FileName);

            string result = uploadProceudureCall(Convert.ToString(permitId), "CS", filename, sCmd);
            if (result == "Done")
                uploadCS.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "SURRENDER_PERMIT_CS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadCS.FileName));

            Text = filename;




        }



        if (uploadOther.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SURRENDER_PERMIT_OTHER_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadOther.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "OTHER", filename, sCmd);
            if (result == "Done")
                uploadOther.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "SURRENDER_PERMIT_OTHER_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadOther.FileName));

            Text = filename;




        }
        if (uploadPermit.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SURRENDER_PERMIT_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPermit.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "PERMIT", filename, sCmd);
            if (result == "Done")
                uploadPermit.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "SURRENDER_PERMIT_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPermit.FileName));

            Text = filename;




        }


        if (uploadeJhtTaxToken.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SURRENDER_PERMIT_JHT_TAX_TOKEN_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadeJhtTaxToken.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "JHT_TAX_TOKEN", filename, sCmd);
            if (result == "Done")
                uploadeJhtTaxToken.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "SURRENDER_PERMIT_JHT_TAX_TOKEN_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadeJhtTaxToken.FileName));

            Text = filename;




        }
        if (vehicleFitness.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SURRENDER_PERMIT_VFITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(vehicleFitness.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "VFITNESS", filename, sCmd);
            if (result == "Done")
                vehicleFitness.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "SURRENDER_PERMIT_VFITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(vehicleFitness.FileName));

            Text = filename;




        }
        if (VehicleInsurance.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SURRENDER_PERMIT_INS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(VehicleInsurance.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "INS", filename, sCmd);
            if (result == "Done")
                VehicleInsurance.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "SURRENDER_PERMIT_INS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(VehicleInsurance.FileName));

            Text = filename;




        }
        if (VehicleSLD.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SURRENDER_PERMIT_SLD_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(VehicleSLD.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "SLD", filename, sCmd);
            if (result == "Done")
                VehicleSLD.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "SURRENDER_PERMIT_SLD_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(VehicleSLD.FileName));

            Text = filename;




        }

        

    }

    private string uploadProceudureCall(string appId, string uploadHeads, string fileName, SqlCommand sCmd)
    {
        sCmd.CommandText = "STA_SURRENDER_OF_PERMIT_APPLICATION_UPLOAD_AED";
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
        txtComments.Text = "";

       
    }
}