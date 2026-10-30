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


public partial class STA_RENEW_PERMIT_APPLICATION_UPDATE : System.Web.UI.Page
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
                    sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                    sCmd.Parameters.AddWithValue("@MODE", "VIEW_RENEW_APPLICATION_UPDATE");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt1 = ds.Tables[0];
                    //DataTable dt3 = ds.Tables[2];
                    //StringBuilder sb = new StringBuilder();


                    lblApplicationNo.Text = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);
                    VOName.Text = Convert.ToString(dt1.Rows[0]["OWNER_NAME"]);
                    VOCareOf.Text = Convert.ToString(dt1.Rows[0]["CARE_OF"]);
                    VOPresentAddress.Text = Convert.ToString(dt1.Rows[0]["PRESENT_ADDRESS"]);
                    VOPermanentAddress.Text = Convert.ToString(dt1.Rows[0]["PERMANENT_ADDRESS"]);

                    VORegNumber.Text = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                    lblPermitNnumber.Text = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);
                    lblPermitfrom.Text = Convert.ToString(dt1.Rows[0]["VALID_FROM"]);
                    lblPermitUpto.Text = Convert.ToString(dt1.Rows[0]["VALID_UPTO"]);
                    VType.Text = Convert.ToString(dt1.Rows[0]["VEHICLE_CLASS_TYPE"]);
                    VChassisNumber.Text = Convert.ToString(dt1.Rows[0]["CHASIS_NUMBER"]);
                    VEngineNumber.Text = Convert.ToString(dt1.Rows[0]["ENGINE_NUMBER"]);
                    lblMakerDesc.Text = Convert.ToString(dt1.Rows[0]["MAKER_DESC"]);
                    lblMakermodel.Text = Convert.ToString(dt1.Rows[0]["MAKER_MODEL"]);
                    lblFuelDesc.Text = Convert.ToString(dt1.Rows[0]["FUEL_DESC"]);
                    lblVcolor.Text = Convert.ToString(dt1.Rows[0]["VEHICLE_COLOR"]);
                    VFitness.Text = Convert.ToString(dt1.Rows[0]["FC_VALID_UPTO"]);
                    VTax.Text = Convert.ToString(dt1.Rows[0]["TAX_PAID_UPTO"]);
                    VULW.Text = Convert.ToString(dt1.Rows[0]["ULW"]);
                    VRLW.Text = Convert.ToString(dt1.Rows[0]["RLW"]);
                    VSeatCapacity.Text = Convert.ToString(dt1.Rows[0]["SEATING_CAPACITY"]);
                    VManufacturingON.Text = Convert.ToString(dt1.Rows[0]["MANUFACTURING_ON"]);
                    lblInsuranceUpto.Text = Convert.ToString(dt1.Rows[0]["IC_VALID_UPTO"]);

                    String RandNo = Convert.ToString(DateTime.Now.Ticks);
                    string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                    Convert.ToString(dt1.Rows[0]["OWNER_NAME"]), RandNo, Convert.ToString(dt1.Rows[0]["FEE"]),
                    Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]), "NA", "STA PERMIT ISSUE", hdnID.Value, "STA", "PRJ", "PRJSES026", "sec1234");
                    EncryptedParameter.Value = EncryptedStr;
                    
                        //if (dt3.Rows.Count > 0)
                        //{
                        //    foreach (DataRow dr in dt3.Rows)
                        //    {
                        //        if (Convert.ToString(dr["UPLOAD_HEADS"]) == "RC")
                        //        {

                        //            string newstr = "JHT/STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        //            string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                        //            divrcfile.InnerHtml = strlink;

                        //        }
                        //        if (Convert.ToString(dr["UPLOAD_HEADS"]) == "SPEED")
                        //        {

                        //            string newstr = "JHT/STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        //            string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                        //            divSpeed.InnerHtml = strlink;

                        //        }
                        //        if (Convert.ToString(dr["UPLOAD_HEADS"]) == "INSURANCE")
                        //        {
                        //            string newstr = "JHT/STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        //            string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                        //            divinsurFile.InnerHtml = strlink;

                        //        }

                        //        if (Convert.ToString(dr["UPLOAD_HEADS"]) == "POLLUTION")
                        //        {
                        //            string newstr = "JHT/STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        //            string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                        //            divrollution.InnerHtml = strlink;
                        //        }

                        //        if (Convert.ToString(dr["UPLOAD_HEADS"]) == "FITNESS")
                        //        {
                        //            string newstr = "JHT/STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        //            string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                        //            divfitness.InnerHtml = strlink;
                        //        }
                                

                        //    }
                        //}

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
            //    sCmd.CommandText = "STA_PERMIT_APPLICATION_HDR_AED";
            //    sCmd.CommandType = CommandType.StoredProcedure;
            //    sCmd.Parameters.Clear();

            //    sCmd.Parameters.AddWithValue("@APP_ID", PermitId);
            //    sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
            //    sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
            //    sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
            //    sCmd.Parameters.AddWithValue("@MODE", "VALIDITY_UPDATE");
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
                     //masterAppId = Convert.ToInt32(sCmd.ExecuteScalar());

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
        if (UploadCS.HasFile)
        {
            resulArray.Add(fileValidate.FileValidate(UploadCS.FileName, UploadCS.FileContent));
        }
        return resulArray;

    }
    private void uploadFile(int permitId, SqlCommand sCmd)
    {
        string Text = "";

        // ✅ Network upload path
        string uploadPath = @"\\10.30.9.199\d\JHT\STAUploadedFiles\";

        // Ensure directory exists
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        DateTime dt;
        string uniqueFileNamPrfix;

        if (uploadRC.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = dt.Day + "_" + dt.Month + "_" + dt.Year + "_" +
                                 dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;

            string filename =
                "RENEWAL_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" +
                Path.GetFileName(uploadRC.FileName);

            string result = uploadProceudureCall(permitId.ToString(), "RC", filename, sCmd);

            if (result == "Done")
                uploadRC.SaveAs(Path.Combine(
                    uploadPath,
                    "RENEWAL_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_" +
                    Path.GetFileName(uploadRC.FileName)));

            Text = filename;
        }

        if (uploadInsur.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = dt.Day + "_" + dt.Month + "_" + dt.Year + "_" +
                                 dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;

            string filename =
                "RENEWAL_PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" +
                Path.GetFileName(uploadInsur.FileName);

            string result = uploadProceudureCall(permitId.ToString(), "INSURANCE", filename, sCmd);

            if (result == "Done")
                uploadInsur.SaveAs(Path.Combine(
                    uploadPath,
                    "RENEWAL_PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_" +
                    Path.GetFileName(uploadInsur.FileName)));

            Text = filename;
        }

        if (uploadPollution.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = dt.Day + "_" + dt.Month + "_" + dt.Year + "_" +
                                 dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;

            string filename =
                "RENEWAL_PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" +
                Path.GetFileName(uploadPollution.FileName);

            string result = uploadProceudureCall(permitId.ToString(), "POLLUTION", filename, sCmd);

            if (result == "Done")
                uploadPollution.SaveAs(Path.Combine(
                    uploadPath,
                    "RENEWAL_PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_" +
                    Path.GetFileName(uploadPollution.FileName)));

            Text = filename;
        }

        if (uploadFitness.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = dt.Day + "_" + dt.Month + "_" + dt.Year + "_" +
                                 dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;

            string filename =
                "RENEWAL_PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" +
                Path.GetFileName(uploadFitness.FileName);

            string result = uploadProceudureCall(permitId.ToString(), "FITNESS", filename, sCmd);

            if (result == "Done")
                uploadFitness.SaveAs(Path.Combine(
                    uploadPath,
                    "RENEWAL_PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_" +
                    Path.GetFileName(uploadFitness.FileName)));

            Text = filename;
        }

        if (uploadSpeed.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = dt.Day + "_" + dt.Month + "_" + dt.Year + "_" +
                                 dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;

            string filename =
                "RENEWAL_PERMIT_SPEED_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" +
                Path.GetFileName(uploadSpeed.FileName);

            string result = uploadProceudureCall(permitId.ToString(), "SPEED", filename, sCmd);

            if (result == "Done")
                uploadSpeed.SaveAs(Path.Combine(
                    uploadPath,
                    "RENEWAL_PERMIT_SPEED_" + permitId + "_" + uniqueFileNamPrfix + "_" +
                    Path.GetFileName(uploadSpeed.FileName)));

            Text = filename;
        }

        if (uploadPermit.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = dt.Day + "_" + dt.Month + "_" + dt.Year + "_" +
                                 dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;

            string filename =
                "RENEWAL_PERMIT_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" +
                Path.GetFileName(uploadPermit.FileName);

            string result = uploadProceudureCall(permitId.ToString(), "PERMIT", filename, sCmd);

            if (result == "Done")
                uploadPermit.SaveAs(Path.Combine(
                    uploadPath,
                    "RENEWAL_PERMIT_" + permitId + "_" + uniqueFileNamPrfix + "_" +
                    Path.GetFileName(uploadPermit.FileName)));

            Text = filename;
        }

        if (UploadCS.HasFile)
        {
            dt = DateTime.Now;
            uniqueFileNamPrfix = dt.Day + "_" + dt.Month + "_" + dt.Year + "_" +
                                 dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;

            string filename =
                "RENEWAL_PERMIT_CS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" +
                Path.GetFileName(UploadCS.FileName);

            string result = uploadProceudureCall(permitId.ToString(), "CS", filename, sCmd);

            if (result == "Done")
                UploadCS.SaveAs(Path.Combine(
                    uploadPath,
                    "RENEWAL_PERMIT_CS_" + permitId + "_" + uniqueFileNamPrfix + "_" +
                    Path.GetFileName(UploadCS.FileName)));

            Text = filename;
        }
    }

    private string uploadProceudureCall(string permitId, string uploadHeads, string fileName, SqlCommand sCmd )
    {

            sCmd.CommandText = "STA_RENEWAL_OF_PERMIT_APPLICATION_UPLOAD_AED";
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

    



    