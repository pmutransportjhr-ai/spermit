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
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;
using System.Collections;



public partial class STA_COUNTER_SIGNATURE_APPLICATION : System.Web.UI.Page
{

    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        txtMobileNo.MaxLength = 10;
        txtMobileOtp.MaxLength = 10;

        try
        {
            // =========================
            // FIRST TIME PAGE LOAD
            // =========================

            if (!IsPostBack)
            {
                // HIDE MAIN FORM INITIALLY
                pnlMainForm.Visible = false;

                // OTP NOT VERIFIED
                hdnOtpVerified.Value = "false";

                // CHECK QUERYSTRING
                if (string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        Guid.NewGuid().ToString(),
                        "alert('You are not permitted to access this page');" +
                        "window.location ='PermitHomePage.aspx';",
                        true);

                    return;
                }

                // =========================
                // DECRYPT QUERYSTRING
                // =========================

                string queryString =
                    CryptorEngine.Decrypt(
                        Convert.ToString(Request.QueryString["q"]),
                        true);

                foreach (string str in queryString.Split('&'))
                {
                    QueryString qs = new QueryString();

                    qs.Name = str.Split('=')[0];
                    qs.Value = str.Split('=')[1];

                    QueryStr.Add(qs);
                }

                QueryString typeId =
                    QueryStr.Where(x => x.Name == "TYPEID")
                    .FirstOrDefault();

                QueryString optionId =
                    QueryStr.Where(x => x.Name == "OPTIONID")
                    .FirstOrDefault();

                if (typeId == null || optionId == null)
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        Guid.NewGuid().ToString(),
                        "alert('Invalid Request');" +
                        "window.location ='PermitHomePage.aspx';",
                        true);

                    return;
                }

                hdnType.Value = typeId.Value;
                hdnTypeOptionID.Value = optionId.Value;

                // =========================
                // PAGE HEADING
                // =========================

                PageTitle PT = new PageTitle();

                DataTable dt1 =
                    PT.PageHeading(
                        Convert.ToInt32(hdnType.Value),
                        Convert.ToInt32(hdnTypeOptionID.Value));

                if (dt1.Rows.Count > 0)
                {
                    Page.Title =
                        dt1.Rows[0]["STA_TITLE"].ToString();

                    PageHeading.InnerText =
                        dt1.Rows[0]["STA_HEADING"].ToString();

                    PageHeading1.InnerText =
                        dt1.Rows[0]["STA_TITLE"].ToString() +
                        " - For Other State Permit Holder";
                }

                // =========================
                // BIND STATE DROPDOWN
                // =========================

                Bind_ddlStateWHS();

                // =========================
                // LOAD APPLICATION FEES
                // =========================

                string sConnectionString =
                    ConfigurationManager
                    .ConnectionStrings["connectionstring"]
                    .ToString();

                using (SqlConnection sCon =
                    new SqlConnection(sConnectionString))
                {
                    using (SqlCommand sCmd =
                        new SqlCommand(
                            "STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED",
                            sCon))
                    {
                        sCon.Open();

                        sCmd.CommandType =
                            CommandType.StoredProcedure;

                        sCmd.Parameters.Clear();

                        sCmd.Parameters.AddWithValue(
                            "@PERMIT_TYPE_OPTION_SERVICE_ID",
                            hdnTypeOptionID.Value);

                        sCmd.Parameters.AddWithValue(
                            "@MODE",
                            "FEE_DETAILS");

                        SqlDataAdapter da =
                            new SqlDataAdapter(sCmd);

                        DataSet ds = new DataSet();

                        da.Fill(ds);

                        if (ds.Tables.Count > 0 &&
                            ds.Tables[0].Rows.Count > 0)
                        {
                            lblApplicationFees.Text =
                                Convert.ToString(
                                    ds.Tables[0].Rows[0]["FEE"]);
                        }
                    }
                }
            }

            // =========================
            // OTP VERIFIED STATE
            // =========================

            if (hdnOtpVerified.Value == "true")
            {
                pnlMainForm.Visible = true;

                txtMobileOtp.ReadOnly = true;

                btnSendOtp.Enabled = false;

                btnVerifyOtp.Enabled = false;

                txtOtp.Enabled = false;

                txtMobileNo.Text =
                    txtMobileOtp.Text;

                txtMobileNo.ReadOnly = true;

                txtMobileNo.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            string message = ex.Message;

            message = message
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Replace("'", "");

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                Guid.NewGuid().ToString(),
                "alert('" + message + "');" +
                "window.location ='PermitHomePage.aspx';",
                true);
        }
    }
    public void Bind_ddlStateWHS()
    {


        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");
        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters);

        ddlStateIssuedBy.DataSource = dr;
        ddlStateIssuedBy.Items.Clear();
        ddlStateIssuedBy.Items.Add(new ListItem("--Please select state--", "0"));
        ddlStateIssuedBy.DataTextField = "STATE_NAME";
        ddlStateIssuedBy.DataValueField = "STATE_CODE";
        ddlStateIssuedBy.DataBind();
        ddlStateIssuedBy.SelectedValue = "0";


    }
    protected void ddlStateIssuedBy_SelectedIndexChanged(object sender, EventArgs e)
    {

        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('show');", true);
        string abcdxcc = ddlStateIssuedBy.SelectedValue;
        if (abcdxcc.Contains("|") || abcdxcc.Contains("&") || abcdxcc.Contains(";") || abcdxcc.Contains("$") || abcdxcc.Contains("%") || abcdxcc.Contains("@") || abcdxcc.Contains("'") || abcdxcc.Contains("\"")
            || abcdxcc.Contains("[") || abcdxcc.Contains("]") || abcdxcc.Contains("}") || abcdxcc.Contains("{") || abcdxcc.Contains("\\") || abcdxcc.Contains("/") || abcdxcc.Contains("<") || abcdxcc.Contains(">") || abcdxcc.Contains("(") || abcdxcc.Contains(")")
            || abcdxcc.Contains("+") || abcdxcc.Contains("\n") || abcdxcc.Contains("\r") || abcdxcc.Contains("=") || abcdxcc.Contains(","))
        {
            RouteHDRInfo.InnerHtml = "";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Value passed');$('#loadingmessage').modal('hide');ComponentsDateTimePickers.init();", true);

           
        }
                   
        else if (ddlStateIssuedBy.SelectedValue == "0")
        {
            RouteHDRInfo.InnerHtml = "";
        }
        else
        {
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {
                    sCon.Open();

                    using (SqlCommand sCmd = new SqlCommand("STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED", sCon))
                    {
                        sCmd.CommandType = CommandType.StoredProcedure;

                        sCmd.Parameters.AddWithValue("@STATE_CODE",
                            Convert.ToInt64(ddlStateIssuedBy.SelectedValue));

                        sCmd.Parameters.AddWithValue("@MODE", "ROUTE_LIST_BYID");

                        SqlDataAdapter da = new SqlDataAdapter(sCmd);

                        DataSet ds = new DataSet();

                        da.Fill(ds);

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];

                            StringBuilder sb = new StringBuilder();

                            foreach (DataRow dr in dt.Rows)
                            {
                                string routeId =
                                    Convert.ToString(dr["ROUTE_ID"]);

                                string routeName =
                                    Convert.ToString(dr["ROUTE_NAME"]);

                                string encryptedRoute =
                                    CryptorEngine.Encrypt(
                                        "ROUTE_ID=" + routeId,
                                        true);

                                sb.Append("<div class='row' style='margin-bottom:5px;'>");

                                // RADIO BUTTON
                                sb.Append("<div class='col-md-1'>");

                                sb.Append("<input type='radio' " +
                                          "name='routeHdr' " +
                                          "value='" + routeId + "' " +
                                          "id='RadioButton_" + routeId + "' " +

                                          "onclick=\"fnShowRoute('" +
                                          encryptedRoute +
                                          "');\" />");

                                sb.Append("</div>");

                                // ROUTE NAME
                                sb.Append("<div class='col-md-11'>");

                                sb.Append(routeName);

                                sb.Append("</div>");

                                sb.Append("</div>");
                            }

                            RouteHDRInfo.InnerHtml = sb.ToString();
                        }
                        else
                        {
                            RouteHDRInfo.InnerHtml =
                                "<div class='alert alert-warning'>" +
                                "No Route Found." +
                                "</div>";

                            RouteTimeInfo.InnerHtml = "";
                        }
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;

                    message = message
                        .Replace("\r", " ")
                        .Replace("\n", " ")
                        .Replace("'", "");

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        Guid.NewGuid().ToString(),
                        "alert('" + message + "');",
                        true);
                }
            }
        }
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');ComponentsDateTimePickers.init(); fnManufactDate();", true);

              
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        // ✅ OTP SECURITY CHECK (ADD THIS)
        if (hdnOtpVerified == null || hdnOtpVerified.Value != "true")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(),
                "alert('Please verify mobile number before proceeding');", true);
            return;
        }

        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;

        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {



            String input = "";
            input = txtroutjson.Value;
            PERMIT_APPLICATION_LDR rDTO = new JavaScriptSerializer().Deserialize<PERMIT_APPLICATION_LDR>(input);

            ShowPopup("Input Data "+rDTO);

            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
                sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                DateTime? ManufacturingON;
                ViewState["manufatDate"] = VManufacturingON.Value;
                if (!String.IsNullOrEmpty(VManufacturingON.Value))
                {
                    VManufacturingON.Value = "01/" + VManufacturingON.Value;
                    ManufacturingON = DateTime.ParseExact(VManufacturingON.Value, "d/M/yyyy", CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);
                }
                else
                {
                    ManufacturingON = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }
                string x = txtvalidityfrom.Value.ToString();
                string y = txtvalidityto.Value.ToString();
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnType.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnTypeOptionID.Value);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtPermitNo.Text);
                sCmd.Parameters.AddWithValue("@PERMIT_VALID_FROM", DateTime.ParseExact(txtvalidityfrom.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PERMIT_VALID_UPTO", DateTime.ParseExact(txtvalidityto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@MAKER_DESC", VMakerDesc.Text);
                sCmd.Parameters.AddWithValue("@MAKER_MODEL", VMakerModel.Text);
                sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", ddlCategory.SelectedValue);
                sCmd.Parameters.AddWithValue("@CHASIS_NUMBER", VChassisNumber.Text);
                sCmd.Parameters.AddWithValue("@ENGINE_NUMBER", VEngineNumber.Text);
                sCmd.Parameters.AddWithValue("@SEATING_CAPACITY", VSeatCapacity.Text);
                sCmd.Parameters.AddWithValue("@SLEEPER_CAPACITY", txtSleeperCapacity.Text);
                sCmd.Parameters.AddWithValue("@ULW", txtULW.Text);
                sCmd.Parameters.AddWithValue("@PERMIT_FEE_AMOUNT", lblApplicationFees.Text);
                sCmd.Parameters.AddWithValue("@RLW", txtRLW.Text);
                sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", ManufacturingON);
                sCmd.Parameters.AddWithValue("@NATURE_OF_GOODS_CARRIED", txtNatureOfGoods.Text.Trim());
                sCmd.Parameters.AddWithValue("@ISSUED_BY_STATE", Convert.ToInt64(ddlStateIssuedBy.SelectedValue));
                sCmd.Parameters.AddWithValue("@ROUTE_ID", rDTO.ROUTE_DETAIL_ID);
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO", DateTime.ParseExact(txtTPValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@RECOMMENDATION_NUMBER",txtRecommendationNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@RECOMMENDATION_DATE", DateTime.ParseExact(txtRecommendationNoDated.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
                sCmd.Parameters.AddWithValue("@FIRST_TIME_TAX","Yes");
                sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXISTS");
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
                    if (Convert.ToString(dt1.Rows[0]["STATUS"]) == "SUCCESS")
                    {
                        sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters["@MODE"].Value = "ADD";
                    //    var obj = sCmd.ExecuteScalar();
                        DataSet ds1 = new DataSet();
                        SqlDataAdapter da1 = new SqlDataAdapter(sCmd);
                        da1.Fill(ds1);
                        DataTable dt2 = new DataTable();
                        dt2 = ds1.Tables[0];
                        Int32 CSId;
                        CSId = Convert.ToInt32(dt2.Rows[0]["APP_ID"]);
                        foreach (PERMIT_APPLICATION_LDR dr in rDTO.PERMIT_APPLICATION_LDRs)
                        {
                            sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_LDR_AED";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@APP_ID", CSId);
                            sCmd.Parameters.AddWithValue("@SEQUENCE", dr.ROUTE_DETAIL_ID);
                            sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                            sCmd.Parameters.AddWithValue("@ARRIVAL_TIMING", dr.ARRIVAL_TIMING.ToString());
                            sCmd.Parameters.AddWithValue("@DEPARTURE_TIMING", dr.DEPARTURE_TIMING.ToString());
                            sCmd.Parameters.AddWithValue("@MODE", dr.MODE);
                            sCmd.Parameters.AddWithValue("@ID", dr.ID);
                            sCmd.Parameters.AddWithValue("@OP_MODE", "ADD");
                            sCmd.ExecuteNonQuery();

                        }
                        uploadFile(CSId, sCmd);
                        objTrans.Commit();
                        string RegistrationNo = Convert.ToString(dt2.Rows[0]["REGISTRATION_NUMBER"]);
                        string permitNo = Convert.ToString(dt2.Rows[0]["PERMIT_NUMBER"]);
                        string mobileNo = Convert.ToString(dt2.Rows[0]["MOBILE_NUMBER"]);
                        string ApplicationNo = Convert.ToString(dt2.Rows[0]["APPLICATION_NUMBER"]);
                        string permitType = Convert.ToString(dt2.Rows[0]["PERMIT_TYPE"]);
                        string sevicename = Convert.ToString(dt2.Rows[0]["APPLICATION_SERVICE"]);
                        SMSSender objSms = new SMSSender();
                        objSms.ApplicationNumberSms(RegistrationNo, permitNo, sevicename, permitType, mobileNo, ApplicationNo);
                        string str = HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(CSId), true));
                        // Response.Redirect("STA_INTER_STATE_PERMIT_RECEIPT.aspx?q="+str);
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for Counter Signature');window.location ='STA_COUNTER_SIGNATURE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(CSId) + "&TYPEID=" + Convert.ToString(hdnType.Value) + "&OPTIONID=" + Convert.ToString(hdnTypeOptionID.Value), true), Response.ContentEncoding) + "'", true);

                    }
                    else
                    {
                        objTrans.Commit();
                        // Response.Redirect("PermitHomePage.aspx");
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt1.Rows[0]["MSG"]) + "');window.location ='PermitHomePage.aspx'", true);
                    }
                }
                else
                {
                    objTrans.Rollback();
                    String Message = result;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    VManufacturingON.Value = Convert.ToString(ViewState["manufatDate"]);
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');fnManufactDate();", true);
                }
            }
            //catch (Exception ex)
            //{
            //    objTrans.Rollback();
            //    String Message = ex.Message;
            //    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true); 
            //}
            catch (Exception ex)
            {
                if (objTrans != null)
                {
                    objTrans.Rollback();
                }

                string Message = ex.ToString();

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('" + Message.Replace("'", "") + "');",
                    true
                );
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
        if (uploadtimetable.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadtimetable.FileName, uploadtimetable.FileContent));
        }
        if (uploadpermit.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadpermit.FileName, uploadpermit.FileContent));
        }
        if (recommendationUpload.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(recommendationUpload.FileName, recommendationUpload.FileContent));
        }
        return resulArray;

    }
    private void uploadFile(int CSId, SqlCommand sCmd)
    {
        string Text;

        DateTime dt = new DateTime();


        string uniqueFileNamPrfix = "";

        bool str = uploadRC.HasFile;
        if (uploadRC.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "COUNTERSIGNATURE_RC_" + CSId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);

            string result = uploadProceudureCall(Convert.ToString(CSId), "RC", filename, sCmd);
            if (result == "Done")
                uploadRC.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "COUNTERSIGNATURE_RC_" + CSId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadRC.FileName));

            Text = filename;




        }
        if (uploadSpeed.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "COUNTERSIGNATURE_SPEED_" + CSId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadSpeed.FileName);
            string result = uploadProceudureCall(Convert.ToString(CSId), "SPEED", filename, sCmd);
            if (result == "Done")
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "COUNTERSIGNATURE_SPEED_" + CSId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }
        if (uploadInsur.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "COUNTERSIGNATURE_INSURANCE_" + CSId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadInsur.FileName);
            string result = uploadProceudureCall(Convert.ToString(CSId), "INSURANCE", filename, sCmd);
            if (result == "Done")
                uploadInsur.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "COUNTERSIGNATURE_INSURANCE_" + CSId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadInsur.FileName));

            Text = filename;




        }

        if (uploadPollution.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "COUNTERSIGNATURE_POLLUTION_" + CSId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPollution.FileName);
            string result = uploadProceudureCall(Convert.ToString(CSId), "POLLUTION", filename, sCmd);
            if (result == "Done")
                uploadPollution.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "COUNTERSIGNATURE_POLLUTION_" + CSId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPollution.FileName));

            Text = filename;




        }

        if (uploadFitness.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "COUNTERSIGNATURE_FITNESS_" + CSId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadFitness.FileName);
            string result = uploadProceudureCall(Convert.ToString(CSId), "FITNESS", filename, sCmd);
            if (result == "Done")
                uploadFitness.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "COUNTERSIGNATURE_FITNESS_" + CSId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadFitness.FileName));

            Text = filename;




        }
        if (uploadpermit.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "COUNTERSIGNATURE_PERMIT_" + CSId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadpermit.FileName);
            string result = uploadProceudureCall(Convert.ToString(CSId), "PERMIT", filename, sCmd);
            if (result == "Done")
                uploadpermit.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "COUNTERSIGNATURE_PERMIT_" + CSId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadpermit.FileName));

            Text = filename;




        }
        if (uploadTax.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "COUNTERSIGNATURE_TAX_" + CSId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadTax.FileName);
            string result = uploadProceudureCall(Convert.ToString(CSId), "TAX", filename, sCmd);
            if (result == "Done")
                uploadTax.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "COUNTERSIGNATURE_TAX_" + CSId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadTax.FileName));

            Text = filename;




        }
        if (uploadtimetable.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "COUNTERSIGNATURE_TIMETABLE_" + CSId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadtimetable.FileName);
            string result = uploadProceudureCall(Convert.ToString(CSId), "TIMETABLE", filename, sCmd);
            if (result == "Done")
                uploadtimetable.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "COUNTERSIGNATURE_TIMETABLE_" + CSId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadtimetable.FileName));

            Text = filename;




        }
        if (recommendationUpload.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "COUNTERSIGNATURE_RECOMMENDATION_" + CSId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(recommendationUpload.FileName);
            string result = uploadProceudureCall(Convert.ToString(CSId), "RECOMMENDATION", filename, sCmd);
            if (result == "Done")
                recommendationUpload.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles" + "COUNTERSIGNATURE_RECOMMENDATION_" + CSId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(recommendationUpload.FileName));

            Text = filename;




        }

    }
    private string uploadProceudureCall(string CSId, string uploadHeads, string fileName, SqlCommand sCmd)
    {


        sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_UPLOAD_AED";
        sCmd.CommandType = CommandType.StoredProcedure;
        sCmd.Parameters.Clear();
        sCmd.Parameters.AddWithValue("@APP_ID", CSId);
        sCmd.Parameters.AddWithValue("@UPLOAD_HEADS", uploadHeads);
        sCmd.Parameters.AddWithValue("@FILE_NAME", fileName);
        sCmd.ExecuteNonQuery();
        return "Done";


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {

        ddlStateIssuedBy.SelectedValue = "0";
        RouteHDRInfo.InnerHtml = "";
        RouteTimeInfo.InnerHtml = "";
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "ComponentsDateTimePickers.init();", true);


    }

    private void ShowPopup(string msg)
    {
        string script = "$('#errorModal .modal-body').text('" +
                        msg.Replace("'", "").Replace("\r", "").Replace("\n", "") +
                        "');$('#errorModal').modal('show');";

        if (ScriptManager.GetCurrent(this.Page) != null &&
            ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", script, true);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "pop", script, true);
        }
    }

    protected void btnSendOtp_Click(object sender, EventArgs e)
    {
        try
        {
            // OTP MOBILE FIELD
            string mobile = txtMobileOtp.Text.Trim();

            // VALIDATE
            if (mobile.Length != 10)
            {
                lblOtpStatus.Text = "Enter valid mobile number.";
                lblOtpStatus.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // PREVENT RESEND SPAM
            if (Session["OTP_TIME"] != null)
            {
                DateTime lastSent =
                    Convert.ToDateTime(Session["OTP_TIME"]);

                if ((DateTime.Now - lastSent).TotalSeconds < 30)
                {
                    lblOtpStatus.Text =
                        "Please wait before requesting OTP again.";

                    lblOtpStatus.ForeColor =
                        System.Drawing.Color.Red;

                    return;
                }
            }

            // GENERATE OTP
            Random rnd = new Random();

            string otp =
                rnd.Next(100000, 999999).ToString();

            // SAVE SESSION
            Session["OTP"] = otp;
            Session["OTP_TIME"] = DateTime.Now;
            Session["OTP_MOBILE"] = mobile;

            // RESET VERIFICATION
            hdnOtpVerified.Value = "false";

            pnlMainForm.Visible = false;

            // SEND SMS
            string message =
                "To verify your mobile number your OTP is " +
                otp +
                " Transport Department, Jharkhand";

            SMSSender sms = new SMSSender();

            sms.Sendsms(
                mobile,
                message,
                "1307161527893297291");

            lblOtpStatus.Text =
                "OTP sent successfully." +otp;

            lblOtpStatus.ForeColor =
                System.Drawing.Color.Green;
        }
        catch (Exception ex)
        {
            lblOtpStatus.Text = ex.Message;

            lblOtpStatus.ForeColor =
                System.Drawing.Color.Red;
        }
    }
    protected void btnVerifyOtp_Click(object sender, EventArgs e)
    {
        try
        {
            string enteredOtp =
                txtOtp.Text.Trim();

            string mobile =
                txtMobileOtp.Text.Trim();

            // OTP EXISTS?
            if (Session["OTP"] == null)
            {
                lblOtpStatus.Text =
                    "OTP expired. Please resend.";

                lblOtpStatus.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }

            // MOBILE MATCH
            if (Session["OTP_MOBILE"] == null ||
                Session["OTP_MOBILE"].ToString() != mobile)
            {
                lblOtpStatus.Text =
                    "Mobile number mismatch.";

                lblOtpStatus.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }

            // OTP EXPIRY
            DateTime otpTime =
                Convert.ToDateTime(Session["OTP_TIME"]);

            if ((DateTime.Now - otpTime).TotalMinutes > 5)
            {
                Session.Remove("OTP");

                lblOtpStatus.Text =
                    "OTP expired.";

                lblOtpStatus.ForeColor =
                    System.Drawing.Color.Red;

                return;
            }

            // OTP MATCH
            if (enteredOtp == Session["OTP"].ToString())
            {
                hdnOtpVerified.Value = "true";

                // SHOW FULL FORM
                pnlMainForm.Visible = true;

                // LOCK OTP CONTROLS
                txtMobileOtp.ReadOnly = true;

                txtOtp.Enabled = false;

                btnSendOtp.Enabled = false;

                btnVerifyOtp.Enabled = false;

                // SET VERIFIED MOBILE
                txtMobileNo.Text =
                    txtMobileOtp.Text.Trim();

                txtMobileNo.ReadOnly = true;

                txtMobileNo.Enabled = false;

                lblOtpStatus.Text =
                    "OTP Verified Successfully.";

                lblOtpStatus.ForeColor =
                    System.Drawing.Color.Green;

                // CLEAR SESSION
                Session.Remove("OTP");
                Session.Remove("OTP_MOBILE");
                Session.Remove("OTP_TIME");
            }
            else
            {
                hdnOtpVerified.Value = "false";

                pnlMainForm.Visible = false;

                lblOtpStatus.Text =
                    "Invalid OTP.";

                lblOtpStatus.ForeColor =
                    System.Drawing.Color.Red;
            }
        }
        catch (Exception ex)
        {
            lblOtpStatus.Text = ex.Message;

            lblOtpStatus.ForeColor =
                System.Drawing.Color.Red;
        }
    }

}