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

public partial class STA_COUNTER_SIGNATURE_UPLOAD_RECEIPT : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            try
            {
                string appId = Request.QueryString["q"];
                hdnAppID.Value = CryptorEngine.Encrypt(appId, true); 
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@APP_ID", Convert.ToInt32(appId));
                parameters[2] = new SqlParameter("@MODE", "RECEIPT_BY_ID");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_STA_CS_COMM_HISTORY", parameters);
                DataTable dt = ds.Tables[0];
                lblApplicationNumber.Text = Convert.ToString(dt.Rows[0]["APPLICATION_NUMBER"]);
                lblRegistrationNo.Text = Convert.ToString(dt.Rows[0]["REGISTRATION_NUMBER"]);
                lblPermitNo.Text = Convert.ToString(dt.Rows[0]["PERMIT_NUMBER"]);
                lblICValidity.Text = Convert.ToString(dt.Rows[0]["IC_VALIDITY"]);
                lblPollutionValidity.Text = Convert.ToString(dt.Rows[0]["PC_VALIDITY"]);
                lblFitness.Text = Convert.ToString(dt.Rows[0]["FC_VALIDITY"]);
              
                lblJharkhandTaxToken.Text = Convert.ToString(dt.Rows[0]["JH_TAX_TOKEN"]);

                string csFile = "JHT/STAUploadedFiles/" + Convert.ToString(dt.Rows[0]["CS"]).Replace("@@---@@", string.Empty);
   
                string rcFile = "JHT/STAUploadedFiles/" + Convert.ToString(dt.Rows[0]["RC_CS"]).Replace("@@---@@", string.Empty);
                string speedFile = "JHT/STAUploadedFiles/" + Convert.ToString(dt.Rows[0]["SPEED_CS"]).Replace("@@---@@", string.Empty);

                string csInsur = "JHT/STAUploadedFiles/" + Convert.ToString(dt.Rows[0]["INSURANCE_CS"]).Replace("@@---@@", string.Empty);
                string csPollution = "JHT/STAUploadedFiles/" + Convert.ToString(dt.Rows[0]["POLLUTION_CS"]).Replace("@@---@@", string.Empty);

                string csFitenss = "JHT/STAUploadedFiles/" + Convert.ToString(dt.Rows[0]["FITNESS_CS"]).Replace("@@---@@", string.Empty);
                string cspermit = "JHT/STAUploadedFiles/" + Convert.ToString(dt.Rows[0]["PERMIT"]).Replace("@@---@@", string.Empty);
                string csapprovedTimeTable = "JHT/STAUploadedFiles/" + Convert.ToString(dt.Rows[0]["APPROVED_TIME_TABLE_CS"]).Replace("@@---@@", string.Empty);
                string csTEMPOTimeTable = "JHT/STAUploadedFiles/" + Convert.ToString(dt.Rows[0]["TEMPO_TIME_TABLE_CS"]).Replace("@@---@@", string.Empty);
                //string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                string csJHT_TAX = "JHT/STAUploadedFiles/" + Convert.ToString(dt.Rows[0]["JHT_TAX"]).Replace("@@---@@", string.Empty);

                csCopy.InnerHtml = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", csFile, "Click here to download");
                ApprovedTimeTableCopy.InnerHtml = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", csapprovedTimeTable, "Click here to download");

                dvRC.InnerHtml = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", rcFile, "Click here to download");
                dvSpeed.InnerHtml = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", speedFile, "Click here to download");
                dvInssurance.InnerHtml = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", csInsur, "Click here to download");
                dvPollution.InnerHtml = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", csPollution, "Click here to download");
                dvFitness.InnerHtml = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", csFitenss, "Click here to download");
                dvPermit.InnerHtml = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", cspermit, "Click here to download");
                dvTempoTimeTable.InnerHtml = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", csTEMPOTimeTable, "Click here to download");
                dvJHTTaxPaidUptoCertificate.InnerHtml = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", csJHT_TAX, "Click here to download");
                

            }
            catch(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Please try again !');window.location ='permithomepage.aspx'", true); 
            }
        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "window.location ='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "'", true);

    }

    protected void btnWithDraw_Click(object sender, EventArgs e)
    {
        string AppId = hdnAppID.Value;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ToString());
        using (con)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@APP_ID", CryptorEngine.Decrypt(AppId, true));
            cmd.Parameters.AddWithValue("@MODE", "WITHDRAW");
            cmd.CommandText = "SP_STA_CS_COMM_HISTORY";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.ExecuteNonQuery();

            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Application Withdraw Successfully.');window.location ='permithomepage.aspx'", true); 
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("permithomepage.aspx");
    }
}