using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using SoftEra.JHT.Infrastructure;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Collections;

public partial class RTA_OFFICE_NOTICE_MASTER_NEW : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    //private static string ID;
    protected void Page_Load(object sender, EventArgs e)
    {
              
            if (!IsPostBack)
            {
               try
                    
               {
                if (string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-OFFICE-NOTICE-MASTER.aspx'", true);
                    return;
                }
                else
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
                        
                        if (hdnID.Value == "0")
                        {
                            txtNoticeCode.Text = hdnID.Value;
                            dvdownload.Visible = false;
                        }
                        else
                        {

                            dvdownload.Visible = true;
                            txtNoticeCode.Text = hdnID.Value;
                            SqlParameter[] parameters = new SqlParameter[3];
                            parameters[0] = new SqlParameter("@ID", hdnID.Value);
                            parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                            parameters[2] = new SqlParameter("@MODE", "OFFICE_NOTICE_LIST");

                            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST_BYID", parameters);
                            DataTable dt = new DataTable();

                            dt = ds.Tables[0];
                            string UserRole = Convert.ToString(Session["USER_ROLE"]);
                          
                          
                                if (dt.Rows.Count > 0)
                                {
                                    txtNoticeDetails.Text = dt.Rows[0]["NOTICE_DETAILS"].ToString();
                                    txtstdt.Text = dt.Rows[0]["NOTICE_START_DATE"].ToString();
                                    txtenddt.Text = dt.Rows[0]["NOTICE_END_DATE"].ToString();
                                    if (Convert.ToString(dt.Rows[0]["NOTICE_FILE"]) == null || Convert.ToString(dt.Rows[0]["NOTICE_FILE"]) == "")
                                    {
                                        dvlinkdownload.InnerHtml = "";

                                    }
                                    else
                                    {
                                        string newstr = "download.aspx?path=RTAUploadedFiles&filename=" + Convert.ToString(dt.Rows[0]["NOTICE_FILE"]).Replace("@@---@@", string.Empty);
                                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click Here to Download");
                                        dvlinkdownload.InnerHtml = strlink;
                                        
                                    }

                                    if (UserRole == "Super Admin")
                                    {
                                        btnSave.Visible = true;

                                    }
                                    else
                                    {
                                        btnSave.Visible = false;

                                    }
                                    
                                }
                            
                        }
                  
                }
                    }

               catch (Exception ex)
               {
                   String Message = ex.Message;
                   Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                   ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-OFFICE-NOTICE-MASTER_NEW.aspx'", true);

               }
            }
       
    }

   
    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            String NOTICE_DETAILS = txtNoticeDetails.Text.Trim();
            String NOTICE_CODE = Convert.ToString(txtNoticeCode.Text);
            DateTime? NOTICE_START_DATE; DateTime? NOTICE_END_DATE;
            if (!String.IsNullOrEmpty(txtstdt.Text))
            {
                NOTICE_START_DATE = DateTime.ParseExact(txtstdt.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                NOTICE_START_DATE = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            if (!String.IsNullOrEmpty(txtenddt.Text))
            {
                NOTICE_END_DATE = DateTime.ParseExact(txtenddt.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                NOTICE_END_DATE = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
            }
           
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@NOTICE_CODE", NOTICE_CODE);
            parameters[1] = new SqlParameter("@NOTICE_DETAILS", NOTICE_DETAILS);
            parameters[2] = new SqlParameter("@TYPE", "RTA");
            parameters[3] = new SqlParameter("@NOTICE_START_DATE", NOTICE_START_DATE);
            parameters[4] = new SqlParameter("@NOTICE_END_DATE", NOTICE_END_DATE);
            parameters[5] = new SqlParameter("@EMP_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
            parameters[6] = new SqlParameter("@FILE_NAME", DBNull.Value);
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
                    if (NOTICE_CODE == "0")
                    {
                        
                        parameters[7] = new SqlParameter("@MODE", "ADD");
                        Object obj=SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_OFFICE_NOTICE_AED", parameters);
                        uploadfile(Convert.ToString(obj));
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully saved');window.location ='RTA-OFFICE-NOTICE-MASTER.aspx'", true);
                    }
                    else
                    {
                        parameters[7] = new SqlParameter("@MODE", "EDIT");
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_OFFICE_NOTICE_AED", parameters);
                        uploadfile(Convert.ToString(NOTICE_CODE));
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully updated');window.location ='RTA-OFFICE-NOTICE-MASTER.aspx'", true);
                    }
                }
               else
               {


                   String Message = result;
                   Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                   ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

               }
           
        
        }
        catch(Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('"+ Message +"');", true);
        }
                   
         
    }
    public void uploadfile(String NOTICE_CODE)
    {
        string Text;

        DateTime dt = new DateTime();


        string uniqueFileNamPrfix = "";

        bool str = uploadNotice.HasFile;
        if (uploadNotice.HasFile)
        {
            try
            {
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "OFFICE_NOTICE_" + NOTICE_CODE + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadNotice.FileName);
                string result = uploadProceudureCall(NOTICE_CODE, filename);
                if (result == "Done")
                uploadNotice.SaveAs(@"\\10.30.9.199\d\JHT\RTAUploadedFiles\" + "OFFICE_NOTICE_" + NOTICE_CODE + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadNotice.FileName));


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
    }
    private ArrayList uploadFileValidate()
    {
        ArrayList resulArray = new ArrayList();
        FileUploadValidations validateFile = new FileUploadValidations();
        if (uploadNotice.HasFile)
        {
            resulArray.Add("OK");
            //resulArray.Add(validateFile.FileValidate(uploadNotice.FileName, uploadNotice.FileContent));
        }


        return resulArray;

    }
    public string uploadProceudureCall(string NOTICE_CODE, string fileName)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@NOTICE_CODE", NOTICE_CODE);
            parameters[1] = new SqlParameter("@NOTICE_DETAILS", DBNull.Value);
            parameters[2] = new SqlParameter("@TYPE", DBNull.Value);
            parameters[3] = new SqlParameter("@NOTICE_START_DATE", DBNull.Value);
            parameters[4] = new SqlParameter("@NOTICE_END_DATE", DBNull.Value);
            parameters[5] = new SqlParameter("@EMP_ID", DBNull.Value);
            parameters[6] = new SqlParameter("@FILE_NAME", fileName);
            parameters[7] = new SqlParameter("@MODE", "UPD_FILE");

            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_OFFICE_NOTICE_AED", parameters);
            return "Done";
        }
        catch (Exception ex)
        {
            return "Error" + ex.Message;

        }
    }
    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("RTA-OFFICE-NOTICE-MASTER.aspx");
    }
}