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
using System.Collections;



public partial class RTA_REPLACE_OF_AUTO_RICKSHAW : System.Web.UI.Page
{
    public string FillAPPlyRouteList { get; set; }
    //private static string TYPE_ID;
    //private static string TYPE_OPTION_ID;
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtMobileNo.MaxLength = 10;
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

                    HiddenField1.Value = IDx.Value;
                    HiddenField2.Value = IDx1.Value;
                    

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
        try
        {
            SqlParameter[] parameters = new SqlParameter[23];
            parameters[0] = new SqlParameter("@APP_ID", 0);
            parameters[1] = new SqlParameter("@PERMIT_TYPE_ID", HiddenField1.Value);
            parameters[2] = new SqlParameter("@OLD_REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
            parameters[3] = new SqlParameter("@REGISTRATION_NUMBER", DBNull.Value);
            parameters[4] = new SqlParameter("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
            parameters[5] = new SqlParameter("@OWNER_NAME", DBNull.Value);
            parameters[6] = new SqlParameter("@CARE_OF", DBNull.Value);
            parameters[7] = new SqlParameter("@PRESENT_ADDRESS", DBNull.Value);
            parameters[8] = new SqlParameter("@PERMANENT_ADDRESS", DBNull.Value);
            parameters[9] = new SqlParameter("@CHASIS_NUMBER", DBNull.Value);
            parameters[10] = new SqlParameter("@ENGINE_NUMBER", DBNull.Value);
            parameters[11] = new SqlParameter("@TAX_PAID_UPTO", DBNull.Value);
            parameters[12] = new SqlParameter("@FITNESS_VALID_UPTO", DBNull.Value);
            parameters[13] = new SqlParameter("@VEHICLE_CLASS_TYPE", DBNull.Value);
            parameters[14] = new SqlParameter("@INSURANCE_COMPANY", DBNull.Value);
            parameters[15] = new SqlParameter("@INSURANCE_POLICY_NO", DBNull.Value);
            parameters[16] = new SqlParameter("@INSURANCE_UPTO", DBNull.Value);
            parameters[17] = new SqlParameter("@FEE", DBNull.Value);
            parameters[18] = new SqlParameter("@APPLICATION_NUMBER", DBNull.Value);
            parameters[19] = new SqlParameter("@MOBILE_NUMBER", DBNull.Value);
            parameters[20] = new SqlParameter("@SEAT_CAPACITY", DBNull.Value);
            parameters[21] = new SqlParameter("@AGREEMENT_WITH ", DBNull.Value);
            parameters[22] = new SqlParameter("@MODE", "SEARCH");
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_AUTO_RICKSHAW_REPLACE_APPLICATION_HDR_AED", parameters);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Columns[0].ColumnName == "MSG")
            {
                lblMessage.Text = dt.Rows[0]["MSG"].ToString();
                lblMessage.Visible = true;
                Panel1.Visible = false;
                lblMSg.Text = "";
                lblMSg.Visible = false;
                Panel2.Visible = false;

            }
           
            else
            {
                DataTable dt2 = ds.Tables[1];
                StringBuilder sb = new StringBuilder();
              
                lblPermitType.Text = dt.Rows[0]["PERMIT_TYPE"].ToString();
                lblPermitNo.Text = dt.Rows[0]["PERMIT_NUMBER"].ToString();
                lblVORegNumber.Text = dt.Rows[0]["REGISTRATION_NUMBER"].ToString();
                lblExistName.Text = dt.Rows[0]["OWNER_NAME"].ToString();
                lblVOCareOf.Text = dt.Rows[0]["CARE_OF"].ToString();
                lblVOPresentAddress.Text = dt.Rows[0]["PRESENT_ADDRESS"].ToString();
                lblVOPermanentAddress.Text = dt.Rows[0]["PERMANENT_ADDRESS"].ToString();
                lblVChassisNumber.Text = dt.Rows[0]["CHASIS_NUMBER"].ToString();
                lblVEngineNumber.Text = dt.Rows[0]["ENGINE_NUMBER"].ToString();
                lblVTax.Text = dt.Rows[0]["TAX_PAID_UPTO"].ToString();
                lblVFitness.Text = dt.Rows[0]["FITNESS_VALID_UPTO"].ToString();
                lblVType.Text = dt.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                lblVInsuranceCmp.Text = dt.Rows[0]["INSURANCE_COMPANY"].ToString();
                lblVInsuranceNo.Text = dt.Rows[0]["INSURANCE_POLICY_NO"].ToString();
                lblVInsuranceUpto.Text = dt.Rows[0]["INSURANCE_UPTO"].ToString();
                lblseat.Text = dt.Rows[0]["SEAT_CAPACITY"].ToString();
                lblagreement.Text = dt.Rows[0]["AGREEMENT_WITH"].ToString();
                lbRTOName.Text = dt.Rows[0]["RTO_NAME"].ToString();
                VOMobileNo.Text = dt.Rows[0]["MOBILE_NUMBER"].ToString();
                
                       
               
            
                Panel1.Visible = true;
                txtExistRegNo.Enabled = false;
                txtExistPermitNo.Enabled = false;
                btnGO.Enabled = false;
                lblMessage.Visible = false;
                lblMSg.Text = "";
                lblMSg.Visible = false;
                Panel2.Visible = false;
            }
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermithomePage.aspx'", true);

        }

    }
    protected void btnExistClear_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        txtExistRegNo.Text = "";
        txtExistPermitNo.Text = "";
        lblMessage.Visible = false;
        txtExistRegNo.Enabled = true;
        txtExistPermitNo.Enabled = true;
        btnGO.Enabled = true;
        Panel1.Visible = false;
        txtVehicleNumber.Text = "";
        txtAgreementWith.Text = "";
        txtSeatingCapacity.Text = "";
        txtMobileNo.Text = "";
        lblMSg.Text = "";
        lblMSg.Visible = false;
        Panel2.Visible = false;
                   

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");
    }

    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
      try
      {
        String regNo1 = txtVehicleNumber.Text.Trim();
        SqlParameter[] parameters1 = new SqlParameter[5];
        parameters1[0] = new SqlParameter("@APP_ID", DBNull.Value);
        parameters1[1] = new SqlParameter("@PERMIT_TYPE_ID", DBNull.Value);
        parameters1[2] = new SqlParameter("@PERMIT_TYPE_OPTION_ID", DBNull.Value);
        parameters1[3] = new SqlParameter("@REGISTRATION_NUMBER", regNo1);
        parameters1[4] = new SqlParameter("@MODE", "CHECK_EXISTS");
        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_RTA_GET_AUTO_RICKSHAW_PERMIT_INFO", parameters1);
        if (Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) == "FAIL")
        {

            lblMSg.Text = ds.Tables[0].Rows[0]["MSG"].ToString();
            lblMSg.Visible = true;
            Panel2.Visible = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) == "SUCCESS")
        {
                lblMSg.Text = ds.Tables[0].Rows[0]["MSG"].ToString();
                lblMSg.Visible = true;
        var url = "http://ws.jhpolice.gov.in/rest/VehicleService/getJSON?pwd=vahan@nic786&regno=" + regNo1;
        var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
        using (var response = webrequest.GetResponse())
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            var result = reader.ReadToEnd();
            txtVehicleJson.Value = Convert.ToString(result);
        }
        String input = "";
        input = (txtVehicleJson.Value).Replace("[]", "\"\"");
        if (input == "" || input == "[\"Vehicle Data Not Found\"]")
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Vehicle data not found OR Server is busy please try again');", true);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
        }
        else
        {
            VEHICLE_INFO myclass = new JavaScriptSerializer().Deserialize<VEHICLE_INFO>(input);

            
                if (myclass.stautsMessage == "OK")
                {
                    VORegNumber.Text = myclass.rc_regn_no;
                    VOName.Text = myclass.rc_owner_name;
                    VOCareOf.Text = myclass.rc_f_name;
                    VOPresentAddress.Text = myclass.rc_present_address;
                    VOPermanentAddress.Text = myclass.rc_permanent_address;

                    VChassisNumber.Text = myclass.rc_chasi_no;
                    VEngineNumber.Text = myclass.rc_eng_no;
                    VTax.Text = myclass.rc_tax_upto;
                    VFitness.Text = myclass.rc_fit_upto;
                    VType.Text = myclass.rc_vh_class_desc;
                    VInsuranceCmp.Text = myclass.rc_insurance_comp;
                    VInsuranceNo.Text = myclass.rc_insurance_policy_no;
                    VInsuranceUpto.Text = myclass.rc_insurance_upto;

                    SqlParameter[] parameters = new SqlParameter[23];
                    parameters[0] = new SqlParameter("@APP_ID", 0);
                    parameters[1] = new SqlParameter("@PERMIT_TYPE_ID", HiddenField1.Value);
                    parameters[2] = new SqlParameter("@OLD_REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                    parameters[3] = new SqlParameter("@REGISTRATION_NUMBER", myclass.rc_regn_no);
                    parameters[4] = new SqlParameter("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                    parameters[5] = new SqlParameter("@OWNER_NAME", DBNull.Value);
                    parameters[6] = new SqlParameter("@CARE_OF", DBNull.Value);
                    parameters[7] = new SqlParameter("@PRESENT_ADDRESS", DBNull.Value);
                    parameters[8] = new SqlParameter("@PERMANENT_ADDRESS", DBNull.Value);
                    parameters[9] = new SqlParameter("@CHASIS_NUMBER", DBNull.Value);
                    parameters[10] = new SqlParameter("@ENGINE_NUMBER", DBNull.Value);
                    parameters[11] = new SqlParameter("@TAX_PAID_UPTO", DBNull.Value);
                    parameters[12] = new SqlParameter("@FITNESS_VALID_UPTO", DBNull.Value);
                    parameters[13] = new SqlParameter("@VEHICLE_CLASS_TYPE", DBNull.Value);
                    parameters[14] = new SqlParameter("@INSURANCE_COMPANY", DBNull.Value);
                    parameters[15] = new SqlParameter("@INSURANCE_POLICY_NO", DBNull.Value);
                    parameters[16] = new SqlParameter("@INSURANCE_UPTO", DBNull.Value);
                    parameters[17] = new SqlParameter("@FEE", HiddenField2.Value);
                    parameters[18] = new SqlParameter("@APPLICATION_NUMBER", DBNull.Value);
                    parameters[19] = new SqlParameter("@MOBILE_NUMBER", DBNull.Value);
                    parameters[20] = new SqlParameter("@SEAT_CAPACITY", DBNull.Value);
                    parameters[21] = new SqlParameter("@AGREEMENT_WITH ", DBNull.Value);
                    parameters[22] = new SqlParameter("@MODE", "DUPLICACY");

                    DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_AUTO_RICKSHAW_REPLACE_APPLICATION_HDR_AED", parameters);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        lblReplacevehicleFees.Text = ds1.Tables[0].Rows[0]["FEE"].ToString();
                    }
                    else
                    {
                        lblReplacevehicleFees.Text = "";
                    }
                    Panel2.Visible = true;


                }
        }
        }
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
      }
      catch (Exception ex)
      {
          String Message = ex.Message;
          Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
          ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

      }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        txtVehicleNumber.Text = "";
        txtAgreementWith.Text = "";
        txtSeatingCapacity.Text = "";
        txtMobileNo.Text = "";
        lblMSg.Text = "";
        lblMSg.Visible = false;
        Panel2.Visible = false;
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
                sCmd.CommandText = "RTA_AUTO_RICKSHAW_REPLACE_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
            
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", HiddenField1.Value );
                sCmd.Parameters.AddWithValue("@OLD_REGISTRATION_NUMBER", lblVORegNumber.Text);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", lblPermitNo.Text);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
                sCmd.Parameters.AddWithValue("@CHASIS_NUMBER", VChassisNumber.Text);
                sCmd.Parameters.AddWithValue("@ENGINE_NUMBER", VEngineNumber.Text);
                sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO", VTax.Text);
                sCmd.Parameters.AddWithValue("@FITNESS_VALID_UPTO", VFitness.Text);
                sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", VType.Text);
                sCmd.Parameters.AddWithValue("@INSURANCE_COMPANY", VInsuranceCmp.Text);
                sCmd.Parameters.AddWithValue("@INSURANCE_POLICY_NO", VInsuranceCmp.Text);
                sCmd.Parameters.AddWithValue("@INSURANCE_UPTO", VInsuranceUpto.Text);
                sCmd.Parameters.AddWithValue("@FEE", lblReplacevehicleFees.Text);
                sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", DBNull.Value);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@SEAT_CAPACITY", txtSeatingCapacity.Text.Trim());
                sCmd.Parameters.AddWithValue("@AGREEMENT_WITH", txtAgreementWith.Text.Trim());
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
                        sCmd.CommandText = "RTA_AUTO_RICKSHAW_REPLACE_APPLICATION_HDR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters["@MODE"].Value = "ADD";
                        var obj = sCmd.ExecuteScalar();

                        Int32 AppId;
                        AppId = Convert.ToInt32(obj);

                        uploadFile(AppId);

                        objTrans.Commit();

                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for Replacement of Vehicle (Auto Rickshaw)');window.location ='RTA-AUTO-RICKSHAW-PERMIT-RENEWAL-APPLICATION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(AppId), true), Response.ContentEncoding) + "'", true);
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
        if (uploadAgreement.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadAgreement.FileName, uploadAgreement.FileContent));
        }
        if (uploadPermit.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadPermit.FileName, uploadPermit.FileContent));
        }
        return resulArray;
    }

    private void uploadFile(int appId)
    {
        string Text;

        DateTime dt = new DateTime();


        string uniqueFileNamPrfix = "";

        bool str = uploadRC.HasFile;
        if (uploadRC.HasFile)
        {
            try
            {
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "AR_REPLACEMENT_RC_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);

                string result = uploadProceudureCall(Convert.ToString(appId), "RC", filename);
                if (result == "Done")
                    uploadRC.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AR_REPLACEMENT_RC_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadRC.FileName));

                Text = filename;


            }
            catch (Exception ex)
            {

                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                Text = "Upload status Error: " + ex.Message;
            }

        }

        if (uploadInsur.HasFile)
        {
            try
            {
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "AR_REPLACEMENT_INSURANCE_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadInsur.FileName);
                string result = uploadProceudureCall(Convert.ToString(appId), "INSURANCE", filename);
                if (result == "Done")
                    uploadInsur.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AR_REPLACEMENT_INSURANCE_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadInsur.FileName));

                Text = filename;


            }
            catch (Exception ex)
            {

                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                Text = "Upload status Error: " + ex.Message;
            }

        }

        if (uploadPollution.HasFile)
        {
            try
            {
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "AR_REPLACEMENT_POLLUTION_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPollution.FileName);
                string result = uploadProceudureCall(Convert.ToString(appId), "POLLUTION", filename);
                if (result == "Done")
                    uploadPollution.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AR_REPLACEMENT_POLLUTION_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPollution.FileName));

                Text = filename;


            }
            catch (Exception ex)
            {

                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                Text = "Upload status Error: " + ex.Message;
            }

        }

        if (uploadFitness.HasFile)
        {
            try
            {
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "AR_REPLACEMENT_FITNESS_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadFitness.FileName);
                string result = uploadProceudureCall(Convert.ToString(appId), "FITNESS", filename);
                if (result == "Done")
                    uploadFitness.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AR_REPLACEMENT_FITNESS_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadFitness.FileName));

                Text = filename;


            }
            catch (Exception ex)
            {

                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                Text = "Upload status Error: " + ex.Message;
            }

        }
        if (uploadPermit.HasFile)
        {
            try
            {
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "AR_REPLACEMENT_PERMIT_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPermit.FileName);
                string result = uploadProceudureCall(Convert.ToString(appId), "PERMIT", filename);
                if (result == "Done")
                    uploadPermit.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AR_REPLACEMENT_PERMIT_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPermit.FileName));

                Text = filename;


            }
            catch (Exception ex)
            {

                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                Text = "Upload status Error: " + ex.Message;
            }

        }
        if (uploadAgreement.HasFile)
        {
            try
            {
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "AR_REPLACEMENT_AGREEMENT_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadAgreement.FileName);
                string result = uploadProceudureCall(Convert.ToString(appId), "AGREEMENT", filename);
                if (result == "Done")
                    uploadAgreement.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AR_REPLACEMENT_AGREEMENT_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadAgreement.FileName));

                Text = filename;


            }
            catch (Exception ex)
            {

                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                Text = "Upload status Error: " + ex.Message;
            }

        }

    }

    private string uploadProceudureCall(string appId, string uploadHeads, string fileName)
    {
        //       RTA_PERMIT_APPLICATION_UPLOAD_AED](@PERMIT_ID INT,@UPLOAD_HEADS VARCHAR(250),@FILE_NAME VARCHAR(250))

        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@APP_ID", appId);
            parameters[1] = new SqlParameter("@UPLOAD_HEADS", uploadHeads);
            parameters[2] = new SqlParameter("@FILE_NAME", fileName);
            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_AUTO_RICKSHAW_REPLACE_APPLICATION_UPLOAD_AED", parameters);
            return "Done";
        }
        catch (Exception ex)
        {
            return "Error" + ex.Message;

        }

    }
}