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
using System.Resources;
using System.Threading;
using System.Reflection;

public partial class STA_CS_UPDATE : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    //public string FillAPPlyRouteList { get; set; }
    //int appid = 0;

  
    protected void Page_Load(object sender, EventArgs e)
    {
        txtExistRegNo.MaxLength = 20;
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
                sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "CS_TAX_SEARCH");
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
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermithomePage.aspx'", true);

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
                sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "VERIFY_MOBILE_NUMBER");
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
                sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@OTP", txtOTP.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "VERIFY_OTP_UPDATE");
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

                    StringBuilder sb = new StringBuilder();
                    //appid = Convert.ToInt32(dt1.Rows[0]["APP_ID"].ToString());
                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    lblPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                    VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    VORegNo.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    lblValidFrom.Text = dt1.Rows[0]["PERMIT_VALID_FROM"].ToString();
                    lblValidUpto.Text = dt1.Rows[0]["PERMIT_VALID_UPTO"].ToString();
                    lblPayFrom.Text = dt1.Rows[0]["FEE_TAX_PAID_FROM"].ToString();
                    lblPayUpto.Text = dt1.Rows[0]["FEE_TAX_PAID_UP_TO"].ToString();
                    labelChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                    lblEngineno.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();
                    lblMaker.Text = dt1.Rows[0]["MAKER_DESC"].ToString();
                    lblManufactringDate.Text = dt1.Rows[0]["MANUFACTURING_ON"].ToString();
                    lblModel.Text = dt1.Rows[0]["MAKER_MODEL"].ToString();
                    lblseatingcapacity.Text = dt1.Rows[0]["SEATING_CAPACITY"].ToString();
                    txtSleeperCapacity.Text = dt1.Rows[0]["SLEEPER_CAPACITY"].ToString();
                    lblcategory.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                    HiddenField1.Text = dt1.Rows[0]["APP_ID"].ToString();
                    HiddenField2.Text = dt1.Rows[0]["TBL_TYPE"].ToString();

                    if (lblseatingcapacity.Text != "")
                    {
                        lblseatingcapacity.Enabled = false;
                    }
                    if (txtSleeperCapacity.Text != "")
                    {
                        txtSleeperCapacity.Enabled = false;
                    }
                    if (txtSleeperCapacity.Text != "" || lblcategory.Text == "Ordinary" || lblcategory.Text == "Semi Deluxe" || lblcategory.Text == "Deluxe" || lblcategory.Text == "Volvo")
                    {
                        ddlCategory.Visible = false;
                        btnSave.Visible = false;
                    }

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



    protected void btnReset_Click(object sender, EventArgs e)
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

                sCmd.CommandText = "SCREEN_STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@SEATING_CAPACITY", lblseatingcapacity.Text);
                sCmd.Parameters.AddWithValue("@SLEEPER_CAPACITY", txtSleeperCapacity.Text);
                //sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", ddlBusCategory.SelectedItem.Text);

                sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(HiddenField1.Text));
                sCmd.Parameters.AddWithValue("@MODE", "MSTR_UPDATE_NEW");

                if (ddlCategory.SelectedIndex == 1)
                {
                    sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", "Ordinary");
                }
                else if (ddlCategory.SelectedIndex == 2)
                {
                    sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", "Semi Deluxe");
                }
                else if (ddlCategory.SelectedIndex == 3)
                {
                    sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", "Deluxe");
                }
                else if (ddlCategory.SelectedIndex == 4)
                {
                    sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", "Volvo");
                }
                else
                {
                    sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", "BUS");
                }

                var Cnt = sCmd.ExecuteScalar();
                objTrans.Commit();
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
                    uploadFile(Convert.ToInt32(HiddenField1.Text), sCmd);
                    //var Cnt = sCmd.ExecuteScalar();
                    //objTrans.Commit();
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Updated');window.location ='PermitHomepage.aspx'", true);
                }
            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomepage.aspx'", true);
               
            }

        }
    }
    


    //                sCon.Open();
    //                int i = sCmd.ExecuteNonQuery();
    //                SqlCommand sCmd1 = new SqlCommand();
    //                sCmd1.Connection = sCon;
    //                uploadFile(i, sCmd1);

    //                sCon.Close();

    //            }

    //            var Cnt = sCmd.ExecuteScalar();
    //            objTrans.Commit();
    //            sCon.Close();
    //            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Updated');window.location ='PermitHomepage.aspx'", true);
    //        }



    //        catch (Exception ex)
    //        {
    //            objTrans.Rollback();
    //            String Message = ex.Message;
    //            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
    //            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomepage.aspx'", true);

    //        }

    //    }
    //}


    private ArrayList uploadFileValidate()
    {
        ArrayList resulArray = new ArrayList();
        FileUploadValidations validateFile = new FileUploadValidations();
        if (uploadCC.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadCC.FileName, uploadCC.FileContent));
        }


        return resulArray;

    }
    private void uploadFile(int appId, SqlCommand sCmd)
    {
        string Text;

        DateTime dt = new DateTime();


        string uniqueFileNamPrfix = "";

        bool str = uploadCC.HasFile;

        if (uploadCC.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "Update_CS_CC_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadCC.FileName);

            string result = uploadProceudureCall(Convert.ToString(appId), "CC", filename, sCmd);
            if (result == "Done")
                uploadCC.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "Update_CS_CC_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadCC.FileName));

            Text = filename;
        }


    }


    private string uploadProceudureCall(string appId, string uploadHeads, string fileName, SqlCommand sCmd)
    {
        sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_UPLOAD_AED";
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
     protected void txtSleeperCapacity_TextChanged(object sender, EventArgs e)
     {
         try
         {
             ddlCategory.Disabled = false;
             if (Convert.ToInt32(txtSleeperCapacity.Text) > 0)
             {
                 ddlCategory.Items[1].Attributes.Add("disabled", "disabled");


             }
         }
         catch (Exception ex)
         {
             String Message = ex.Message;
             Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
             ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomepage.aspx'", true);
         }
     }


}





