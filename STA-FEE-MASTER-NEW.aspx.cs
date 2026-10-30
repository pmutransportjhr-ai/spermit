using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json;
using System.Collections;


public partial class STA_FEE_MASTER_NEW : System.Web.UI.Page
{
    public string strUploadedFile { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-FEE-MASTER.aspx'", true);
                return;
            }
            else
            {
                String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString))
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
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

                        QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                        hdnID.Value = IDx.Value;
                        Bind_ddlFeeFor();

                        if (hdnID.Value == "0")
                        {

                        }
                        else
                        {
                            sCmd.CommandText = "STA_SP_FEES_MSTR";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@FEE_ID", hdnID.Value);
                            sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                            sCmd.Parameters.AddWithValue("@MODE", "FEE_LIST_BYID");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();

                           
                            dt = ds.Tables[0];

                            feeDetail objfeeDetail = new feeDetail();
                            if (dt.Rows.Count > 0)
                            {
                                objfeeDetail.FEEFOR = dt.Rows[0]["PERMIT_TYPE_ID"].ToString();
                                objfeeDetail.FEEOPTION = dt.Rows[0]["PERMIT_TYPE_OPTION_ID"].ToString();
                                objfeeDetail.FEESERVICE = dt.Rows[0]["PERMIT_TYPE_OPTION_SERVICE_ID"].ToString();
                                objfeeDetail.FEETYPE = dt.Rows[0]["FEE_TYPE_ID"].ToString();
                                objfeeDetail.FEE = Convert.ToDouble(dt.Rows[0]["FEE"]);
                                objfeeDetail.STATUS = Convert.ToString(dt.Rows[0]["STATUS"]);
                                objfeeDetail.FILEPATH = Convert.ToString(dt.Rows[0]["FILEPATH"]);

                            }
                            txtUploadFileEdit.Value = Convert.ToString(objfeeDetail.FILEPATH);
                            ddlFeeFor.SelectedValue = Convert.ToString(objfeeDetail.FEEFOR);
                            loadFeeOption(objfeeDetail.FEEFOR);
                            ddlFeeOption.SelectedValue = Convert.ToString(dt.Rows[0]["PERMIT_TYPE_OPTION_ID"]);
                            loadFeeService(objfeeDetail.FEEFOR, objfeeDetail.FEEOPTION);
                            ddlFeeService.SelectedValue = Convert.ToString(dt.Rows[0]["PERMIT_TYPE_OPTION_SERVICE_ID"]);
                            loadFeeTYPE(objfeeDetail.FEEFOR, objfeeDetail.FEEOPTION, objfeeDetail.FEESERVICE);
                            ddlFeeType.SelectedValue = Convert.ToString(dt.Rows[0]["FEE_TYPE_ID"]);
                            txtFee.Text = Convert.ToString(objfeeDetail.FEE);
                            //ddlStatus.SelectedValue = Convert.ToString(objfeeDetail.STATUS);
                            lblFileUpload.Text = txtUploadFileEdit.Value.ToString().Substring(txtUploadFileEdit.Value.LastIndexOf("@@---@@") + 7);
                            string newstr = "JHT/STAUploadedFiles/" + Convert.ToString(objfeeDetail.FILEPATH).Replace("@@---@@", string.Empty);
                            string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>Uploaded File: {1}  </a>", newstr, lblFileUpload.Text);
                            strUploadedFile = strlink;
                          
                        }
                    }
                    catch (Exception ex)
                    {
                        String Message = ex.Message;
                        Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-FEE-MASTER.aspx'", true);
                    }
                }
            }
        }
    }
    private class feeDetail
    {
        public string FEEFOR { get; set; }
        public string FEEOPTION { get; set; }
        public string FEESERVICE { get; set; }
        public string FEETYPE { get; set; }
        public double FEE { get; set; }
        public string STATUS { get; set; }
        public string FILEPATH { get; set; }
    }
    private void Bind_ddlFeeFor()
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.CommandText = "STA_SP_FEES_MSTR";
            sCmd.CommandType = CommandType.StoredProcedure;
            sCmd.Parameters.Clear();
            sCmd.Parameters.AddWithValue("@MODE", "PERMIT_TYPE_LIST");
            SqlDataReader dr = sCmd.ExecuteReader();



            ddlFeeFor.DataSource = dr;
            ddlFeeFor.Items.Clear();
            ddlFeeFor.Items.Add(new ListItem("--select--", "0"));
            ddlFeeFor.DataTextField = "PERMIT_TYPE";
            ddlFeeFor.DataValueField = "PERMIT_TYPE_ID";
            ddlFeeFor.DataBind();
            ddlFeeFor.SelectedValue = "0";
        }
    }
    

   

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("STA-FEE-MASTER.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
       using (SqlConnection sCon = new SqlConnection(sConnectionString))
       {
           try
           {
               String PERMIT_TYPE_ID = ddlFeeFor.SelectedValue;
               String PERMIT_TYPE_OPTION_ID = ddlFeeOption.SelectedValue;
               String PERMIT_TYPE_OPTION_SERVICE_ID = ddlFeeService.SelectedValue;
               String FEE_TYPE_ID = ddlFeeType.SelectedValue;

               sCon.Open();
               SqlCommand sCmd = new SqlCommand();
               sCmd.Connection = sCon;
               sCmd.CommandText = "STA_SP_FEES_MSTR";
               sCmd.CommandType = CommandType.StoredProcedure;
               sCmd.Parameters.Clear();
               sCmd.Parameters.AddWithValue("@FEE_ID", hdnID.Value);
               sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PERMIT_TYPE_ID);
               sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", PERMIT_TYPE_OPTION_ID);
               sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", PERMIT_TYPE_OPTION_SERVICE_ID);
               sCmd.Parameters.AddWithValue("@FEE_TYPE_ID", FEE_TYPE_ID);
               sCmd.Parameters.AddWithValue("@FEE", txtFee.Text.ToString());
               sCmd.Parameters.AddWithValue("@STATUS", "Active");
               sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                

               


               if (hdnID.Value == "0")
               {
                   //string FilePathNew = "";//UploadFunction().Substring(UploadFunction().IndexOf("@@---@@")+1);
                   string FilePathNew = "";

                   sCmd.Parameters.AddWithValue("@FILEPATH", "");
                   sCmd.Parameters.AddWithValue("@MODE", "DUPLICACY");
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
                   var num1 = sCmd.ExecuteScalar();
                    if (Convert.ToInt32(num1) == 0)
                   {

                       FilePathNew = UploadFunction();

                       sCmd.CommandText = "STA_SP_FEES_MSTR";
                       sCmd.CommandType = CommandType.StoredProcedure;
                       sCmd.Parameters["@FILEPATH"].Value = FilePathNew;
                       sCmd.Parameters["@MODE"].Value = "ADD";
                       sCmd.ExecuteNonQuery();
                  
                       ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Submitted');window.location ='STA-FEE-MASTER.aspx'", true);


                   }
                   else
                   {
                       ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Already exist');window.location ='STA-FEE-MASTER.aspx'", true);

                   }
                }
               else
               {

                 
                   String Message = result;
                   Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                   ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

               }

               }
               else
               {
                   sCmd.Parameters.AddWithValue("@FILEPATH", "");
                   sCmd.Parameters.AddWithValue("@MODE", "DUPLICACY");
                   string FullFilePath = UploadFunction();
                   if (FullFilePath == "")
                   {
                       sCmd.Parameters["@FILEPATH"].Value = txtUploadFileEdit.Value;
                      
                       
                   }
                   else
                   {
                       sCmd.Parameters["@FILEPATH"].Value = FullFilePath;
                       
                   }
                   sCmd.CommandText = "STA_SP_FEES_MSTR";
                   sCmd.CommandType = CommandType.StoredProcedure;
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
                   var num1 = sCmd.ExecuteScalar();
                   if (Convert.ToInt32(num1) == 0)
                   {
                       sCmd.CommandText = "STA_SP_FEES_MSTR";
                       sCmd.CommandType = CommandType.StoredProcedure;
                       sCmd.Parameters["@MODE"].Value = "EDIT";
                       sCmd.ExecuteNonQuery();
                  
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Updated');window.location ='STA-FEE-MASTER.aspx'", true);


                   }
                   else
                   {
                       ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Already exist');window.location ='STA-FEE-MASTER.aspx'", true);

                   }
                }
               else
               {


                   String Message = result;
                   Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                   ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

               }
               }
           }
           catch (Exception ex)
           {
               String Message = ex.Message;
               Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
               ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-FEE-MASTER.aspx'", true);

           }
       }

    }
    private string UploadFunction()
    {
        string Text = "";

        if (pdfupload.HasFile)
        {
            try
            {
                DateTime dt = DateTime.Now;

                string uniqueFileNamPrfix =
                    dt.Day + "_" + dt.Month + "_" + dt.Year + "_" +
                    dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;

                string originalFileName = Path.GetFileName(pdfupload.FileName);

                // Filename stored in DB
                string filename =
                    "FEE_" + uniqueFileNamPrfix + "_@@---@@" + originalFileName;

                // ✅ Network path
                string uploadPath = @"\\10.30.9.199\d\JHT\STAUploadedFiles\";

                // Ensure directory exists
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Physical filename
                string physicalFileName =
                    "FEE_" + uniqueFileNamPrfix + "_" + originalFileName;

                // Save file
                pdfupload.SaveAs(
                    Path.Combine(uploadPath, physicalFileName)
                );

                Text = filename;
            }
            catch (Exception ex)
            {
                string Message = ex.Message
                    .Replace("\r", " ")
                    .Replace("\n", " ")
                    .Replace("'", "");

                ScriptManager.RegisterClientScriptBlock(
                    Page,
                    Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('" + Message + "');window.location='STA-FEE-MASTER.aspx'",
                    true
                );

                Text = "Upload status Error: " + ex.Message;
            }

            return Text;
        }
        else
        {
            return "";
        }
    }

    private ArrayList uploadFileValidate()
    {
        ArrayList resulArray = new ArrayList();
        FileUploadValidations validateFile = new FileUploadValidations();
        if (pdfupload.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(pdfupload.FileName, pdfupload.FileContent));
        }
        

        return resulArray;

    }
    protected void ddlFeeFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {
                String PERMIT_TYPE_ID = ddlFeeFor.SelectedValue;
                if (PERMIT_TYPE_ID == "0")
                {
                    ddlFeeOption.Items.Clear();
                    ddlFeeService.Items.Clear();
                    ddlFeeType.Items.Clear();
                }
                else
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_SP_FEES_MSTR";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PERMIT_TYPE_ID);
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_TYPE_OPTION_LIST_BYID");
                    SqlDataReader dr = sCmd.ExecuteReader();


                    ddlFeeOption.DataSource = dr;
                    ddlFeeOption.Items.Clear();
                    ddlFeeOption.Items.Add(new ListItem("--select option--", "0"));
                    ddlFeeOption.DataTextField = "PERMIT_TYPE_OPTION";
                    ddlFeeOption.DataValueField = "PERMIT_TYPE_OPTION_ID";
                    ddlFeeOption.DataBind();
                    ddlFeeOption.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-FEE-MASTER.aspx'", true);
            }
        }
       
    }
    public void loadFeeOption(String PERMIT_TYPE_ID)
    {
         String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                 sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_SP_FEES_MSTR";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PERMIT_TYPE_ID);
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_TYPE_OPTION_LIST_BYID");
                    SqlDataReader dr = sCmd.ExecuteReader();


                    ddlFeeOption.DataSource = dr;
                    ddlFeeOption.Items.Clear();
                    ddlFeeOption.Items.Add(new ListItem("--select option--", "0"));
                    ddlFeeOption.DataTextField = "PERMIT_TYPE_OPTION";
                    ddlFeeOption.DataValueField = "PERMIT_TYPE_OPTION_ID";
                    ddlFeeOption.DataBind();
       
                 ddlFeeOption.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-FEE-MASTER.aspx'", true);
            }
    }
        }
    protected void ddlFeeOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {
                String PERMIT_TYPE_ID = ddlFeeFor.SelectedValue;
                String PERMIT_TYPE_OPTION_ID = ddlFeeOption.SelectedValue;
                if (PERMIT_TYPE_OPTION_ID == "0")
                {
                    ddlFeeService.Items.Clear();
                    ddlFeeType.Items.Clear();
                }
                else
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_SP_FEES_MSTR";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PERMIT_TYPE_ID);
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", PERMIT_TYPE_OPTION_ID);
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_TYPE_OPTION_SERVICE_LIST_BYID");
                    SqlDataReader dr = sCmd.ExecuteReader();


                    ddlFeeService.DataSource = dr;
                    ddlFeeService.Items.Clear();
                    ddlFeeService.Items.Add(new ListItem("--select service--", "0"));
                    ddlFeeService.DataTextField = "PERMIT_TYPE_OPTION_SERVICES";
                    ddlFeeService.DataValueField = "PERMIT_TYPE_OPTION_SERVICE_ID";
                    ddlFeeService.DataBind();
                    ddlFeeService.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-FEE-MASTER.aspx'", true);
            }
        }
    }
    public void loadFeeService(String PERMIT_TYPE_ID, String PERMIT_TYPE_OPTION_ID)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "STA_SP_FEES_MSTR";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PERMIT_TYPE_ID);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", PERMIT_TYPE_OPTION_ID);
                sCmd.Parameters.AddWithValue("@MODE", "PERMIT_TYPE_OPTION_SERVICE_LIST_BYID");
                SqlDataReader dr = sCmd.ExecuteReader();


                ddlFeeService.DataSource = dr;
                ddlFeeService.Items.Clear();
                ddlFeeService.Items.Add(new ListItem("--select service--", "0"));
                ddlFeeService.DataTextField = "PERMIT_TYPE_OPTION_SERVICES";
                ddlFeeService.DataValueField = "PERMIT_TYPE_OPTION_SERVICE_ID";
                ddlFeeService.DataBind();

                ddlFeeService.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-FEE-MASTER.aspx'", true);
            }
        }
    }
    protected void ddlFeeService_SelectedIndexChanged(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {
                String PERMIT_TYPE_ID = ddlFeeFor.SelectedValue;
                String PERMIT_TYPE_OPTION_ID = ddlFeeOption.SelectedValue;
                String PERMIT_TYPE_OPTION_SERVICE_ID = ddlFeeService.SelectedValue;
                if (PERMIT_TYPE_OPTION_SERVICE_ID == "0")
                {
                    ddlFeeService.Items.Clear();
                }
                else
                {
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_SP_FEES_MSTR";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PERMIT_TYPE_ID);
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", PERMIT_TYPE_OPTION_ID);
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", PERMIT_TYPE_OPTION_SERVICE_ID);
                    sCmd.Parameters.AddWithValue("@MODE", "FEE_TYPE_LIST_BYID");
                    SqlDataReader dr = sCmd.ExecuteReader();


                    ddlFeeType.DataSource = dr;
                    ddlFeeType.Items.Clear();
                    ddlFeeType.Items.Add(new ListItem("--select type--", "0"));
                    ddlFeeType.DataTextField = "FEE_TYPE";
                    ddlFeeType.DataValueField = "FEE_TYPE_ID";
                    ddlFeeType.DataBind();
                    ddlFeeType.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-FEE-MASTER.aspx'", true);
            }
        }

    }
    public void loadFeeTYPE(String PERMIT_TYPE_ID, String PERMIT_TYPE_OPTION_ID, String PERMIT_TYPE_OPTION_SERVICE_ID)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "STA_SP_FEES_MSTR";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PERMIT_TYPE_ID);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", PERMIT_TYPE_OPTION_ID);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", PERMIT_TYPE_OPTION_SERVICE_ID);
                sCmd.Parameters.AddWithValue("@MODE", "FEE_TYPE_LIST_BYID");
                SqlDataReader dr = sCmd.ExecuteReader();


                ddlFeeType.DataSource = dr;
                ddlFeeType.Items.Clear();
                ddlFeeType.Items.Add(new ListItem("--select type--", "0"));
                ddlFeeType.DataTextField = "FEE_TYPE";
                ddlFeeType.DataValueField = "FEE_TYPE_ID";
                ddlFeeType.DataBind();
                ddlFeeType.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-FEE-MASTER.aspx'", true);
            }
        }
    }
}   

 


