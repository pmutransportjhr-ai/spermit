
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Configuration;
using System.Security;
using System.Security.Principal;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using SoftEra.JHT.Infrastructure;
using System.Transactions;
using System.Net.Mail;


public partial class ForgotPassword : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            //= drawString;
          //  hdnCap.Value = Convert.ToString( Session["captcha"]);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FP();
    }
    public void FP()
    {
       

        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    String Email = txtLogin.Text;
                    String TYPE = ddlType.SelectedValue;
                    String ForgetPassCode = Guid.NewGuid().ToString();


                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@EMAIL_ID", Email);
                    parameters[1] = new SqlParameter("@PASSWORD", DBNull.Value);
                    parameters[2] = new SqlParameter("@TYPE", TYPE);
                    parameters[3] = new SqlParameter("@FORGOT_PASS_CODE", ForgetPassCode);
                    parameters[4] = new SqlParameter("@Mode", "EMAILEXIST");

                    var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "FORGOT_PASSWORD_USER", parameters);
                    if (Convert.ToInt32(obj) == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Sorry!!! this Email Id is inactive or not registered with us');", true);
                        

                    }
                    else
                    {
                       

                        string q = CryptorEngine.Encrypt("EmailId=" + Email + "&TYPE=" + TYPE + "&AutoCode=" + ForgetPassCode, true);
                        parameters[4] = new SqlParameter("@Mode", "UPDATEFCODE");
                        SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "FORGOT_PASSWORD_USER", parameters);
                        StringBuilder sbody = new StringBuilder();
                        sbody.Append("You have recieved this mail in response to your Password Recovery Request" + "<br/><br/>" + "<span style='color:blue'>" + "<a href=http://spermit.jharkhand.gov.in/PasswordReset.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "> Click here to change your password</a>" + "</span>");
                        sbody.Append("<br><br><b>Best wishes,</b><br><b>Transport Team</b>");
                        MailMessage mailMessage = new MailMessage(new MailAddress("spermitjhr@gmail.com"), new MailAddress(Email));
                        mailMessage.Subject = "Password Recovery: TDJ";
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = sbody.ToString();

                        System.Net.NetworkCredential networkCredentials = new System.Net.NetworkCredential("spermitjhr@gmail.com", "spermit@123");
                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.UseDefaultCredentials = false;
			//smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.EnableSsl = true;
                        smtpClient.Host = "smtp.gmail.com";
                        smtpClient.Port = 587;
                        smtpClient.Credentials = networkCredentials;
                        smtpClient.Send(mailMessage);
                        
                        scope.Complete();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('A password request link for reset your password has been sent to your Email id. Please check your spam/junk folder in case the email is not received in the Inbox');", true);
                        
                    }
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='Login.aspx'", true);

                }
            }
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='Login.aspx'", true);

       }


        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}