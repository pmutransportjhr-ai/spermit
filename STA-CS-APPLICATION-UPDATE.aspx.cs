using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.IO;
using System.Configuration;
using System.Text;
using QRCoder;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Collections;


public partial class STA_CS_APPLICATION_UPDATE : System.Web.UI.Page
{
    // private static string ID;

    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                return;
            }
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
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

                    QueryString IDx = QueryStr.Where(l => l.Name == "APP_ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);

                    hdnID.Value = IDx.Value;

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                    sCmd.Parameters.AddWithValue("@MODE", "VIEW_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt1 = ds.Tables[0];
                    
                    lblApplicationNo.Text = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);
                    VOName.Text = Convert.ToString(dt1.Rows[0]["OWNER_NAME"]);
                    VOCareOf.Text = Convert.ToString(dt1.Rows[0]["CARE_OF"]);
                    VOPresentAddress.Text = Convert.ToString(dt1.Rows[0]["PRESENT_ADDRESS"]);
                    VOPermanentAddress.Text = Convert.ToString(dt1.Rows[0]["PERMANENT_ADDRESS"]);

                    VORegNumber.Text = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                    lblPermitNnumber.Text = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);
                    lblPermitfrom.Text = Convert.ToString(dt1.Rows[0]["PERMIT_VALID_FROM"]);
                    lblPermitUpto.Text = Convert.ToString(dt1.Rows[0]["PERMIT_VALID_UPTO"]);
                    

                    
                    }

               

                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                }

            }
        }
    }


    protected void btnYes_Click(object sender, EventArgs e)
    {
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            SqlTransaction objTrans = null;
            Int32 PermitId;
            PermitId = Convert.ToInt32(hdnID.Value.ToString());
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
             try
             {
                 sCon.Open();
                 objTrans = sCon.BeginTransaction();
                 SqlCommand sCmd = new SqlCommand();
                 sCmd.Connection = sCon;
                 sCmd.Transaction = objTrans;
            
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
                     uploadFile(PermitId, sCmd);
                     objTrans.Commit();
                     ScriptManager.RegisterClientScriptBlock((sender as Control), Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Updated');window.location ='PermitHomePage.aspx'", true);
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

                 ScriptManager.RegisterClientScriptBlock((sender as Control), Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.reload();", true);
             }

            }

    }
    private ArrayList uploadFileValidate()
    {
        ArrayList resulArray = new ArrayList();
        FileUploadValidations fileValidate = new FileUploadValidations();
        if (uploadRC.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadRC.FileName, uploadRC.FileContent));
        }

        if (uploadInsur.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadInsur.FileName, uploadInsur.FileContent));
        }
        if (uploadPollution.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadPollution.FileName, uploadPollution.FileContent));
        }
        if (uploadFitness.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadFitness.FileName, uploadFitness.FileContent));
        }
        if (uploadSpeed.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadSpeed.FileName, uploadSpeed.FileContent));
        }
        if (uploadPermit.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadPermit.FileName, uploadPermit.FileContent));
        }
        if (uploadTimetable.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadTimetable.FileName, uploadTimetable.FileContent));
        }
        if (uploadLetter.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadLetter.FileName, uploadLetter.FileContent));
        }
        if (uploadTax.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(uploadTax.FileName, uploadTax.FileContent));
        }
        return resulArray;

    }
    private void uploadFile(int permitId, SqlCommand sCmd)
    {
        string uploadBasePath = @"\\10.30.9.199\d\JHT\STAUploadedCSFiles\";

        UploadSingleFile(uploadRC, "RC", "COUNTERSIGNATURE_RC_", permitId, sCmd, uploadBasePath);
        UploadSingleFile(uploadSpeed, "SPEED", "COUNTERSIGNATURE_SPEED_", permitId, sCmd, uploadBasePath);
        UploadSingleFile(uploadInsur, "INSURANCE", "COUNTERSIGNATURE_INSURANCE_", permitId, sCmd, uploadBasePath);
        UploadSingleFile(uploadPollution, "POLLUTION", "COUNTERSIGNATURE_POLLUTION_", permitId, sCmd, uploadBasePath);
        UploadSingleFile(uploadFitness, "FITNESS", "COUNTERSIGNATURE_FITNESS_", permitId, sCmd, uploadBasePath);
        UploadSingleFile(uploadPermit, "PERMIT", "COUNTERSIGNATURE_PERMIT_", permitId, sCmd, uploadBasePath);
        UploadSingleFile(uploadTax, "TAX", "COUNTERSIGNATURE_TAX_", permitId, sCmd, uploadBasePath);
        UploadSingleFile(uploadTimetable, "TIMETABLE", "COUNTERSIGNATURE_TIMETABLE_", permitId, sCmd, uploadBasePath);
        UploadSingleFile(uploadLetter, "RECOMMENDATION", "COUNTERSIGNATURE_RECOMMENDATION_", permitId, sCmd, uploadBasePath);
    }

    private void UploadSingleFile(
    FileUpload control,
    string uploadHead,
    string filePrefix,
    int permitId,
    SqlCommand sCmd,
    string uploadBasePath)
    {
        if (!control.HasFile)
            return;

        DateTime dt = DateTime.Now;

        string timeStamp =
            dt.Day + "_" + dt.Month + "_" + dt.Year + "_" +
            dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;

        string originalFileName = Path.GetFileName(control.FileName);

        // File name stored in DB
        string dbFileName =
            filePrefix + permitId + "_" + timeStamp + "_@@---@@" + originalFileName;

        // File name saved on disk
        string physicalFileName =
            filePrefix + permitId + "_" + timeStamp + "_" + originalFileName;

        string result = uploadProceudureCall(
            Convert.ToString(permitId),
            uploadHead,
            dbFileName,
            sCmd
        );

        if (result == "Done")
        {
            control.SaveAs(uploadBasePath + physicalFileName);
        }
    }


    private string uploadProceudureCall(string permitId, string uploadHeads, string fileName, SqlCommand sCmd )
    {

            sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_UPLOAD_AED";
            sCmd.CommandType = CommandType.StoredProcedure;
            sCmd.Parameters.Clear();
            sCmd.Parameters.AddWithValue("@APP_ID", permitId);
            sCmd.Parameters.AddWithValue("@UPLOAD_HEADS", uploadHeads);
            sCmd.Parameters.AddWithValue("@FILE_NAME", fileName);
            sCmd.ExecuteNonQuery();
            return "Done";

    }
  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");

    }

   
}

    



    