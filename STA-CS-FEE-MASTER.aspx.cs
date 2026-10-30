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
using System.Text;
using System.Collections;
using System.IO;
using System.Web.Services;

public partial class STA_CS_FEE_MASTER : System.Web.UI.Page
{
    public string FillFeeList { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillFeeListMaster();

        }
    }

    private void FillFeeListMaster()
    {
         String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
         using (SqlConnection sCon = new SqlConnection(sConnectionString))
         {
             try
             {

                 sCon.Open();
                 SqlCommand sCmd = new SqlCommand();
                 sCmd.Connection = sCon;
                 sCmd.CommandText = "SP_TBL_CS_PERMIT_FEE";
                 sCmd.CommandType = CommandType.StoredProcedure;
                 sCmd.Parameters.Clear();
                 sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                 sCmd.Parameters.AddWithValue("@MODE", "LIST");
                 DataSet ds = new DataSet();
                 SqlDataAdapter da = new SqlDataAdapter(sCmd);
                 da.Fill(ds);
                 DataTable dt = new DataTable();
                 string UserRole = Convert.ToString(Session["USER_ROLE"]);
                 dt = ds.Tables[0];
                 StringBuilder sb = new StringBuilder();
                 if (dt.Rows.Count > 0)
                 {
                     //if (UserRole == "Super Admin")
                     //{
                     //    btnAdd.Visible = true;
                     //}
                     //else
                     //{
                     //    btnAdd.Visible = false;
                     //}

                     foreach (DataRow dr in dt.Rows)
                     {


                         sb.Append("<tr>");
                         sb.Append("<td>" + Convert.ToString(dr["FEE_FOR"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["FEE"]) + "</td>");
                 
                         string FilePath1;
                         if (Convert.ToString(dr["FEE_DOCUMENT"]) != "")
                         {
                             string FilePath = Convert.ToString(dr["FEE_DOCUMENT"]);
                             FilePath1 = "JHT/STAUploadedFiles/" + FilePath.Replace("@@---@@", string.Empty);
                             sb.Append("<td> <a href='" + FilePath1 + "' target ='_blank' > click to download file </a></td>");
                         }
                         else
                         {
                             sb.Append("<td> <a href=''>No file </a></td>");
                         }
                         
                        
                         sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnEdit('" + Convert.ToString(dr["ID"])+ "');\">Edit</a></td>");

                         
                         sb.Append("</tr>");
                     }
                     FillFeeList = sb.ToString();
                 }

             }
             catch (Exception ex)
             {
                 String Message = ex.Message;
                 Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                 ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
             }
         }


    }
    
    protected void btnDelete_Click(object sender, System.EventArgs e)
    {

        string Mode = txtModeKey.Value.Trim();
        
          String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
          using (SqlConnection sCon = new SqlConnection(sConnectionString))
          {
              try
              {

                  sCon.Open();
                  SqlCommand sCmd = new SqlCommand();
                  sCmd.Connection = sCon;
                  sCmd.CommandText = "SP_TBL_CS_PERMIT_FEE";
                  sCmd.CommandType = CommandType.StoredProcedure;
                  sCmd.Parameters.Clear();
                  sCmd.Parameters.AddWithValue("@FEE_FOR", ddlFeesFor.SelectedValue.Trim() == "1" ? "Fees for temporary permit" : "Fees for permanent permit");
                  sCmd.Parameters.AddWithValue("@FEE", txtFees.Text.Trim());
                  if (Mode.ToUpper() != "EDIT")
                  {

                      sCmd.Parameters.AddWithValue("@MODE", "DUPLICATE_CHECK");
                  }
                  else
                  {
                      sCmd.Parameters.AddWithValue("@ID", txtID.Value.Trim());
                      sCmd.Parameters.AddWithValue("@MODE", "DUPLICATE_EDIT_CHECK");
                  }
                  DataSet ds = new DataSet();
                  SqlDataAdapter da = new SqlDataAdapter(sCmd);
                  da.Fill(ds);
                  DataTable dt = new DataTable();
                  dt = ds.Tables[0];
                  if (dt.Rows.Count > 0)
                  {
                      if (dt.Rows[0][0].ToString() == "0")
                      {
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
                              if (pdfupload.HasFile)
                              {
                                  string FilePathNew = UploadFunction();
                                  sCmd.Parameters.AddWithValue("@FEE_DOCUMENT", FilePathNew);
                              }
                              if (txtModeKey.Value.Trim().ToUpper() != "EDIT")
                                  sCmd.Parameters["@MODE"].Value = "ADD";
                              else
                                  sCmd.Parameters["@MODE"].Value = "EDIT";
                              sCmd.ExecuteNonQuery();
                              ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Submitted');window.location.href = 'STA-CS-FEE-MASTER.aspx';", true);
                          }

                      }
                      else
                      {
                          ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Already Exists');window.location.href = 'STA-CS-FEE-MASTER.aspx';", true);
                      }
                  }

              }
              catch (Exception ex)
              {
                  String Message = ex.Message;
                  Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                  ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location.href = 'STA-CS-FEE-MASTER.aspx';", true);
                  return;
              }
          }
          txtID.Value = "0";
          txtModeKey.Value = "0";
    }


    [WebMethod]
    public static FileDataCS GetDetails(FileDataCS objFileData)
    {
        FileDataCS objData = objFileData;
        Int64 ID = Convert.ToInt64(objData.ID);
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {


            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.CommandText = "SP_TBL_CS_PERMIT_FEE";
            sCmd.CommandType = CommandType.StoredProcedure;
            sCmd.Parameters.Clear();
            sCmd.Parameters.AddWithValue("@ID", ID);
            sCmd.Parameters.AddWithValue("@MODE", "LIST_BY_ID");
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sCmd);
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                string FilePath = Convert.ToString(dt.Rows[0]["FEE_DOCUMENT"]);
                string FilePath1 = "JHT/STAUploadedFiles/" + FilePath.Replace("@@---@@", string.Empty);
                objData = new FileDataCS()
                {
                    Fees = Convert.ToString(dt.Rows[0]["FEE"]),
                    FeeDocument = FilePath1,
                    FeeFor = Convert.ToString(dt.Rows[0]["FEE_FOR"]) == "Fees for temporary permit" ? "1" : "2",
                    ID = Convert.ToString(dt.Rows[0]["ID"])
                };
                return objData;
            }
            else
                return objData;
        }
    }

    private string UploadFunction()
    {
        string Text = "";

        try
        {
            if (!pdfupload.HasFile)
                return Text;

            DateTime dt = DateTime.Now;

            string uniqueFileNamPrfix =
                dt.Day + "_" + dt.Month + "_" + dt.Year + "_" +
                dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond;

            string originalFileName = Path.GetFileName(pdfupload.FileName);

            // Filename stored in DB
            string fileName =
                "FEE_" + uniqueFileNamPrfix + "_@@---@@" + originalFileName;

            // ✅ Network upload path
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

            Text = fileName;
        }
        catch (Exception ex)
        {
            string message = ex.Message
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Replace("'", "");

            ScriptManager.RegisterClientScriptBlock(
                Page,
                Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('" + message + "');window.location='STA-CS-FEE-MASTER.aspx'",
                true
            );

            Text = "Upload status Error: " + ex.Message;
        }

        return Text;
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
}
public class FileDataCS {
    public string ID {get; set;}
    public string FeeFor {get ; set;}
    public string FeeDocument {get; set;}
    public string Fees {get; set;}
}