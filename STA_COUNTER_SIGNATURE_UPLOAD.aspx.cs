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

public partial class STA_COUNTER_SIGNATURE_UPLOAD : System.Web.UI.Page
{
    public string FillAPPlyRouteList { get; set; }
    //private static string TYPE_ID;
    //private static string TYPE_OPTION_ID;
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtExistRegNo.MaxLength = 12;
        txtExistPermitNo.MaxLength = 20;
        txtMobileNumber.MaxLength = 10;
        txtOTP.MaxLength = 4;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
           
               

                
           
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
                sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "CS_UPLOAD_SEARCH");
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

                    sCmd.CommandText = "SP_STA_CS_COMM_HISTORY";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@REG_NUMBER", txtExistRegNo.Text.Trim());
                    sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                    sCmd.Parameters.AddWithValue("@MODE", "CHECK");
                    DataSet ds1 = new DataSet();
                    SqlDataAdapter da1 = new SqlDataAdapter(sCmd);
                    da1.Fill(ds1);
                    DataTable dt11 = new DataTable();
                    dt11 = ds1.Tables[0];
                    if (dt11.Rows[0][0].ToString() != "0")
                    {
                        Message.Text = "Already Applied";
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
                //sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                sCmd.CommandText = "SP_VERIFY_SEARCH";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "CS_UPLOAD_VERIFY_MOBILE_NUMBER");
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
                sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@OTP", txtOTP.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "VERIFY_CS_OTP");
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

                    lblPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    lblValidUpto.Text = dt1.Rows[0]["VALID_UP_TO"].ToString();
                    VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    VORegNo.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    HiddenField1.Text = dt1.Rows[0]["APP_ID"].ToString();



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
                    string ICValidUpto = txtICValidUpto.Value.Trim();
                    ICValidUpto = ICValidUpto.Split('/')[2] + ICValidUpto.Split('/')[1] + ICValidUpto.Split('/')[0];
	                string PCValidUpto = txtPCValidUpto.Value.Trim();
                    PCValidUpto = PCValidUpto.Split('/')[2] + PCValidUpto.Split('/')[1] + PCValidUpto.Split('/')[0];
	                string FCValidUpto  = txtFCValidUpto.Value.Trim();
                    FCValidUpto = FCValidUpto.Split('/')[2] + FCValidUpto.Split('/')[1] + FCValidUpto.Split('/')[0];
	                string JharkhandTaxToken = txtJharkhandTaxToken.Value.Trim();
                    JharkhandTaxToken = JharkhandTaxToken.Split('/')[2] + JharkhandTaxToken.Split('/')[1] + JharkhandTaxToken.Split('/')[0];
                   
                    Int32 AppId = Convert.ToInt32(HiddenField1.Text.ToString());
                    uploadFile(AppId, sCmd);
                    string cmdInsert = "SP_STA_CS_COMM_HISTORY";
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", AppId);
                    sCmd.Parameters.AddWithValue("@REG_NUMBER", txtExistRegNo.Text.Trim());
                    sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                    sCmd.Parameters.AddWithValue("@ICValidUpto", ICValidUpto.Trim());
                    sCmd.Parameters.AddWithValue("@PCValidUpto", PCValidUpto.Trim());
                    sCmd.Parameters.AddWithValue("@FCValidUpto", FCValidUpto.Trim());
                    sCmd.Parameters.AddWithValue("@JharkhandTaxToken", JharkhandTaxToken.Trim());
                 
                    sCmd.Parameters.AddWithValue("@MODE", "INSERT");
                    sCmd.CommandText = cmdInsert;
                    sCmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    string application_number = Convert.ToString(dt.Rows[0]["APPLICATION_NUMBER"]);
                    string app_id = Convert.ToString(dt.Rows[0]["APP_ID"]);
                    string mobileNumber = Convert.ToString(dt.Rows[0]["Mobile_Number"]);
                    objTrans.Commit();

                    SMSSender objSms = new SMSSender();
                    objSms.Sendsms(mobileNumber, "Successfully Uploaded Counter Signature, Your Application No: "+ application_number,"");


                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), 
                        "alert('Successfully Uploaded Counter Signature Certificate');window.location ='STA_COUNTER_SIGNATURE_UPLOAD_RECEIPT.aspx?q=" +app_id+ "'", true);

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
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');window.location ='PermitHomePage.aspx';", true);
                // Response.Redirect("PermitHomePage.aspx");
            }
        }
    }
    private ArrayList uploadFileValidate()
    {
        ArrayList resulArray = new ArrayList();
        FileUploadValidations validateFile = new FileUploadValidations();
        if (uploadCS.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadCS.FileName, uploadCS.FileContent));
        }
        if (uploadApprovedTT.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadApprovedTT.FileName, uploadApprovedTT.FileContent));
        }
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
        if (uploadPermit.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadPermit.FileName, uploadPermit.FileContent));
        }
        if (uploadTempoTT.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadTempoTT.FileName, uploadTempoTT.FileContent));
        }
        if (jhTaxPaidUptoCopy.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(jhTaxPaidUptoCopy.FileName, jhTaxPaidUptoCopy.FileContent));
        }
        return resulArray;

    }
    private void uploadFile(int permitId, SqlCommand sCmd)
    {
        string Text;
        DateTime dt = new DateTime();
        string uniqueFileNamPrfix = "";
        if (uploadCS.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "PERMIT_CS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadCS.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "CS", filename, sCmd);
            if (result == "Done")
                uploadCS.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles/" + "PERMIT_CS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadCS.FileName));
            Text = filename;
        }
        if (uploadApprovedTT.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "APPROVED_TIME_TABLE_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadApprovedTT.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "APPROVED_TIME_TABLE_CS", filename, sCmd);
            if (result == "Done")
                uploadApprovedTT.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles/" + "APPROVED_TIME_TABLE_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadApprovedTT.FileName));
            Text = filename;
        }
        if (uploadRC.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "RC_CS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "RC_CS", filename, sCmd);
            if (result == "Done")
                uploadRC.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles/" + "RC_CS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadRC.FileName));
            Text = filename;
        }
        if (uploadSpeed.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "SPEED_CS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadSpeed.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "SPEED_CS", filename, sCmd);
            if (result == "Done")
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles/" + "SPEED_CS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));
            Text = filename;
        }
        if (uploadInsur.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "INSURANCE_CS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadInsur.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "INSURANCE_CS", filename, sCmd);
            if (result == "Done")
                uploadInsur.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles/" + "INSURANCE_CS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadInsur.FileName));
            Text = filename;
        }
        if (uploadPollution.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "POLLUTION_CS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPollution.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "POLLUTION_CS", filename, sCmd);
            if (result == "Done")
                uploadPollution.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles/" + "POLLUTION_CS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPollution.FileName));
            Text = filename;
        }
       
        if (uploadFitness.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "FITNESS_CS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadFitness.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "FITNESS_CS", filename, sCmd);
            if (result == "Done")
                uploadFitness.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles/" + "FITNESS_CS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadFitness.FileName));
            Text = filename;
        }
        if (uploadPermit.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "PERMIT_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPermit.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "PERMIT", filename, sCmd);
            if (result == "Done")
                uploadPermit.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles/" + "PERMIT_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPermit.FileName));
            Text = filename;
        }
        if (uploadTempoTT.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TEMPO_TIME_TABLE_CS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadTempoTT.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "TEMPO_TIME_TABLE_CS", filename, sCmd);
            if (result == "Done")
                uploadTempoTT.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles/" + "TEMPO_TIME_TABLE_CS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadTempoTT.FileName));
            Text = filename;
        }
        if (jhTaxPaidUptoCopy.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "JHT_TAX_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(jhTaxPaidUptoCopy.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "JHT_TAX", filename, sCmd);
            if (result == "Done")
                jhTaxPaidUptoCopy.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedCSFiles/" + "JHT_TAX_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(jhTaxPaidUptoCopy.FileName));
            Text = filename;
        }
    }
    private string uploadProceudureCall(string appId, string uploadHeads, string fileName, SqlCommand sCmd)
    {
        
            
                sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", appId);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@UPLOAD_HEADS", uploadHeads);
                sCmd.Parameters.AddWithValue("@FILE_NAME", fileName);
                sCmd.Parameters.AddWithValue("@MODE", "UPLOAD_CS");
                Object strr=sCmd.ExecuteScalar();
                String strert = "";
                if (Convert.ToString(strr) == "Successfully Uploaded Counter Signature Certificate")
                {
                    strert = "Done";
                }
                else
                {
                    strert = "Error";
                }
                return strert;
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