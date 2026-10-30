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



public partial class RTA_SCHOOL_BUS_DUPLICATE_OF_PERMIT_APPLICATION : System.Web.UI.Page
{
    public string FillAPPlyRouteList { get; set; }
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
                    dt1 = PT.PageRTAHeading(Convert.ToInt32(hdnTypeID.Value), Convert.ToInt32(hdnTypeOptionID.Value));


                    if (dt1.Rows.Count > 0)
                    {
                        Page.Title = dt1.Rows[0]["RTA_TITLE"].ToString();
                        PageHeading.InnerText = dt1.Rows[0]["RTA_HEADING"].ToString();
                        PageHeading1.InnerText = dt1.Rows[0]["RTA_TITLE"].ToString();

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
                sCmd.CommandText = "RTA_SCHOOL_BUS_DUPLICATE_OF_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@MODE", "DUPLICATE_SEARCH");
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
                //sCmd.CommandText = "RTA_SCHOOL_BUS_DUPLICATE_OF_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandText = "SP_VERIFY_SEARCH";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "SCHOOL_VERIFY_MOBILE_NUMBER");
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
                sCmd.CommandText = "RTA_SCHOOL_BUS_PERMIT_APPLY_AED";
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
                  

                    StringBuilder sb = new StringBuilder();

                    lblRTOCode.Text = dt1.Rows[0]["RTO_CODE"].ToString();
                    lblRTOName.Text = dt1.Rows[0]["RTO_NAME"].ToString();
                    lblFrom.Text = dt1.Rows[0]["ST_FROM"].ToString();
                    //lblTo.Text = dt1.Rows[0]["ST_TO"].ToString();
                    hdnApplicationNo.Text = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);                   
                    lblPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    lblValidUpto.Text = dt1.Rows[0]["VALID_UP_TO"].ToString();
                    lblValidFrom.Text = dt1.Rows[0]["VALID_FROM"].ToString();
                    
                    VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
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


                    txtMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                    txtEmailId.Text = dt1.Rows[0]["EMAIL_ID"].ToString();
                    lblGender.Text = dt1.Rows[0]["GENDER"].ToString();
                    
                    
                    

                    if (dt2.Rows.Count > 0)
                    {
                        lblRenewalPermitFees.Text = dt2.Rows[0]["FEE"].ToString();
                    }
                    else
                    {
                        lblRenewalPermitFees.Text = "";
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
                sCmd.CommandText = "RTA_SCHOOL_BUS_DUPLICATE_OF_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnTypeOptionID.Value);               
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
                sCmd.Parameters.AddWithValue("@RTO_CODE", lblRTOCode.Text.Trim());
                sCmd.Parameters.AddWithValue("@ST_FROM", lblFrom.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", lblPermitNo.Text);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.ToString().Trim());              
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.ToString().Trim());
                sCmd.Parameters.AddWithValue("@FITNESS_VALID_UPTO", DateTime.ParseExact(VFitness.Text.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO", DateTime.ParseExact(VTax.Text, "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@VALID_UPTO", DateTime.ParseExact(lblValidUpto.Text.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FEE", lblRenewalPermitFees.Text);
                string ManufatoringDate = string.Empty;
                sCmd.Parameters.AddWithValue("@OLD_APPLICATION_NUMBER", hdnApplicationNo.Text);/////////////////////////
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
                sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", VType.Text);
                sCmd.Parameters.AddWithValue("@CHASIS_NUMBER", VChassisNumber.Text);
                sCmd.Parameters.AddWithValue("@ENGINE_NUMBER", VEngineNumber.Text);
                sCmd.Parameters.AddWithValue("@MAKER_DESC", lblMakerDesc.Text);
                sCmd.Parameters.AddWithValue("@MAKER_MODEL", lblMakermodel.Text);
                sCmd.Parameters.AddWithValue("@FUEL_DESC", lblFuelDesc.Text);
                sCmd.Parameters.AddWithValue("@VEHICLE_COLOR", lblVcolor.Text);
                sCmd.Parameters.AddWithValue("@ULW", VULW.Text);
                sCmd.Parameters.AddWithValue("@RLW", VRLW.Text);
                sCmd.Parameters.AddWithValue("@SEATING_CAPACITY", VSeatCapacity.Text);
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
                sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", ManufacturingON);//////////////////////////
                sCmd.Parameters.AddWithValue("@GENDER", lblGender.Text);
             
                sCmd.Parameters.AddWithValue("@MODE", "ADD");
              
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
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for Duplicate of Permit');window.location ='RTA_SCHOOL_BUS_DUPLICATE_OF_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(AppId) + "&TYPEID=" + Convert.ToString(hdnTypeID.Value) + "&OPTIONID=" + Convert.ToString(hdnTypeOptionID.Value), true), Response.ContentEncoding) + "'", true);
               

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
        FileUploadValidations filevalidate = new FileUploadValidations();
        if (uploadRC.HasFile)
        {
            resulArray.Add(filevalidate.FileValidate(uploadRC.FileName, uploadRC.FileContent));
        }
        if (uploadInsur.HasFile)
        {
            resulArray.Add(filevalidate.FileValidate(uploadInsur.FileName, uploadInsur.FileContent));
        }

        if (uploadPollution.HasFile)
        {
            resulArray.Add(filevalidate.FileValidate(uploadPollution.FileName, uploadPollution.FileContent));
        }

        if (uploadFitness.HasFile)
        {
            resulArray.Add(filevalidate.FileValidate(uploadFitness.FileName, uploadFitness.FileContent));
        }
        if (uploadSpeed.HasFile)
        {
            resulArray.Add(filevalidate.FileValidate(uploadSpeed.FileName, uploadSpeed.FileContent));
        }
       
        if (uploadPermit.HasFile)
        {
            resulArray.Add(filevalidate.FileValidate(uploadPermit.FileName, uploadPermit.FileContent));
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
            string filename = "SCHOOL_BUS_DUPLICATE_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);

            string result = uploadProceudureCall(Convert.ToString(permitId), "RC", filename, sCmd);
            if (result == "Done")
                
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\RTAUploadedFiles/" + "SCHOOL_BUS_DUPLICATE_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }

        if (uploadInsur.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SCHOOL_BUS_DUPLICATE_PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadInsur.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "INSURANCE", filename, sCmd);
            if (result == "Done")
                
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\RTAUploadedFiles/" + "SCHOOL_BUS_DUPLICATE_PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }

        if (uploadPollution.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SCHOOL_BUS_DUPLICATE_PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPollution.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "POLLUTION", filename, sCmd);
            if (result == "Done")
                
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\RTAUploadedFiles/" + "SCHOOL_BUS_DUPLICATE_PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }

        if (uploadFitness.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SCHOOL_BUS_DUPLICATE_PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadFitness.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "FITNESS", filename, sCmd);
            if (result == "Done")
                
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\RTAUploadedFiles/" + "SCHOOL_BUS_DUPLICATE_PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }
        if (uploadSpeed.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SCHOOL_BUS_DUPLICATE_PERMIT_SPEED_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadSpeed.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "SPEED", filename, sCmd);
            if (result == "Done")
                
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\RTAUploadedFiles/" + "SCHOOL_BUS_DUPLICATE_PERMIT_SPEED_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }
        if (uploadPermit.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SCHOOL_BUS_DUPLICATE_PERMIT_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPermit.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "PERMIT", filename, sCmd);
            if (result == "Done")
                
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\RTAUploadedFiles/" + "SCHOOL_BUS_DUPLICATE_PERMIT_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }
       

    }
    private string uploadProceudureCall(string appId, string uploadHeads, string fileName, SqlCommand sCmd)
    {
        //       STA_INTER_STATE_PERMIT_APPLICATION_UPLOAD_AED](@PERMIT_ID INT,@UPLOAD_HEADS VARCHAR(250),@FILE_NAME VARCHAR(250))
        sCmd.CommandText = "RTA_SCHOOL_BUS_DUPLICATE_OF_PERMIT_APPLICATION_UPLOAD_AED";
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