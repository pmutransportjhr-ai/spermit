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
using System.Collections;



public partial class RTA_GOODS_PERMIT_APPLICATION : System.Web.UI.Page
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

                    TYPE_ID.Value = IDx.Value;
                    TYPE_OPTION_ID.Value = IDx1.Value;
                    PageTitle PT = new PageTitle();
                    DataTable dt1 = new DataTable();
                    dt1 = PT.PageRTAHeading(Convert.ToInt32(TYPE_ID.Value), Convert.ToInt32(TYPE_OPTION_ID.Value));


                    if (dt1.Rows.Count > 0)
                    {
                        Page.Title = dt1.Rows[0]["RTA_TITLE"].ToString();
                        PageHeading.InnerText = dt1.Rows[0]["RTA_HEADING"].ToString();
                        PageHeading1.InnerText = dt1.Rows[0]["RTA_TITLE"].ToString();

                    }
                    Bind_ddlRTO();

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

    public void Bind_ddlRTO()
    {

        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", DBNull.Value);
        parameters[1] = new SqlParameter("@MODE", "DD_RTO_LIST");
        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", parameters);

        ddlRTO.DataSource = dr;
        ddlRTO.Items.Clear();
        ddlRTO.Items.Add(new ListItem("--Please select RTO--", "0"));
        ddlRTO.DataTextField = "RTO_NAME";
        ddlRTO.DataValueField = "RTO_CODE";
        ddlRTO.DataBind();
        ddlRTO.SelectedValue = "0";



    }
    protected void ddlRTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string strjquery = @"$('input[id*=txtVehicleNumber]').blur(function () { var objValue =  $(this).val(); $(this).val(objValue.toUpperCase()); });";
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('show');", true);
        if (ddlRTO.SelectedValue == "0")
        {
            Message.Visible = false;
            RegNoDiv.Visible = false;
            Panel1.Visible = false;
        }
        else
        {
                 RegNoDiv.Visible = true;
                Message.Visible = false;
                Panel1.Visible = false;

        }
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Message.Text = "";
        Message.Visible = false;
        txtVehicleNumber.Text = "";
        Panel1.Visible = false;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            String regNo1 = txtVehicleNumber.Text.Trim();
            SqlParameter[] parameters1 = new SqlParameter[5];
            parameters1[0] = new SqlParameter("@APP_ID", 0);
            parameters1[1] = new SqlParameter("@PERMIT_TYPE_ID", TYPE_ID.Value);
            parameters1[2] = new SqlParameter("@PERMIT_TYPE_OPTION_ID", TYPE_OPTION_ID.Value);
            parameters1[3] = new SqlParameter("@REGISTRATION_NUMBER", regNo1);
            parameters1[4] = new SqlParameter("@MODE", "CHECK_EXISTS");
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_RTA_GET_GOODS_CARRIAGE_PERMIT_INFO", parameters1);
            if (Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) == "FAIL")
            {

                Message.Text = ds.Tables[0].Rows[0]["MSG"].ToString();
                Message.Visible = true;
                Panel1.Visible = false;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) == "SUCCESS")
            {
                Message.Text = ds.Tables[0].Rows[0]["MSG"].ToString();
                Message.Visible = true;

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
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Vehicle data not found OR Server is busy please try again');location.reload();", true);
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

                        SqlParameter[] parameters = new SqlParameter[5];
                        parameters[0] = new SqlParameter("@APP_ID", 0);
                        parameters[1] = new SqlParameter("@PERMIT_TYPE_ID", TYPE_ID.Value);
                        parameters[2] = new SqlParameter("@PERMIT_TYPE_OPTION_ID", TYPE_OPTION_ID.Value);
                        parameters[3] = new SqlParameter("@REGISTRATION_NUMBER", myclass.rc_regn_no);
                        parameters[4] = new SqlParameter("@MODE", "DUPLICACY");
                        DataSet ds1 = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_RTA_GET_GOODS_CARRIAGE_PERMIT_INFO", parameters);
                        DataTable dt = ds1.Tables[0];


                        if (dt.Rows.Count > 0)
                        {
                            lblPermitFees.Text = dt.Rows[0]["FEE"].ToString();
                        }

                        else
                        {
                            lblPermitFees.Text = "";
                        }





                    }
                    Panel1.Visible = true;


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
                sCmd.CommandText = "SP_RTA_GOODS_CARRIAGE_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", TYPE_ID.Value);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
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
                sCmd.Parameters.AddWithValue("@UNLOAD_WEIGHT", txtUnloadWeight.Text.Trim());
                sCmd.Parameters.AddWithValue("@GROSS_VEHICLE_WEIGHT", txtGrossvehicleWeight.Text.Trim());
                sCmd.Parameters.AddWithValue("@PAY_LOAD", txtPayLoad.Text.Trim());
                sCmd.Parameters.AddWithValue("@RTO_CODE", ddlRTO.SelectedValue);
                sCmd.Parameters.AddWithValue("@AGREEMENT_WITH", txtAgreementWith.Text.Trim());
                sCmd.Parameters.AddWithValue("@FEE", lblPermitFees.Text);
                sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", DBNull.Value);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
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



                    sCmd.Parameters.AddWithValue("@MODE", "ADD");
                    var obj = sCmd.ExecuteScalar();
                    Int32 PermitId;
                    PermitId = Convert.ToInt32(obj);
                    uploadFile(PermitId);
                    objTrans.Commit();
                    string str = HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(PermitId), true));
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for goods carriage Permit');window.location ='RTA-GOODS-CARRIAGE-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(PermitId), true), Response.ContentEncoding) + "'", true);
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
     
        return resulArray;
    }

    private void uploadFile(int permitId)
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
                string filename = "GOODS_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);

                string result = uploadProceudureCall(Convert.ToString(permitId), "RC", filename);
                if (result == "Done")
                    uploadRC.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "GOODS_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadRC.FileName));

                Text = filename;


            }
            catch (Exception ex)
            {

                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ");
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
                string filename = "GOODS_PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadInsur.FileName);
                string result = uploadProceudureCall(Convert.ToString(permitId), "INSURANCE", filename);
                if (result == "Done")
                    uploadInsur.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "GOODS_PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadInsur.FileName));

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
                string filename = "GOODS_PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPollution.FileName);
                string result = uploadProceudureCall(Convert.ToString(permitId), "POLLUTION", filename);
                if (result == "Done")
                    uploadPollution.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "GOODS_PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPollution.FileName));

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
                string filename = "GOODS_PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadFitness.FileName);
                string result = uploadProceudureCall(Convert.ToString(permitId), "FITNESS", filename);
                if (result == "Done")
                    uploadFitness.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "GOODS_PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadFitness.FileName));

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

    private string uploadProceudureCall(string permitId, string uploadHeads, string fileName)
    {
        //       STA_INTER_STATE_PERMIT_APPLICATION_UPLOAD_AED](@APP_ID INT,@UPLOAD_HEADS VARCHAR(250),@FILE_NAME VARCHAR(250))

        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@APP_ID", permitId);
            parameters[1] = new SqlParameter("@UPLOAD_HEADS", uploadHeads);
            parameters[2] = new SqlParameter("@FILE_NAME", fileName);
            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_RTA_GOODS_CARRIAGE_PERMIT_APPLICATION_UPLOAD_AED", parameters);
            return "Done";
        }
        catch (Exception ex)
        {
            return "Error" + ex.Message;

        }

    }
   
}