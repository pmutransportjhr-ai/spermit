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


public partial class PasswordReset : System.Web.UI.Page
{

    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["salt"] = ConfigurationManager.AppSettings["SecurityKey"] /*random.Next(59999, 199999).ToString()*/;
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                Response.Redirect("Login.aspx");
            }
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

                QueryString IDx = QueryStr.Where(l => l.Name == "EmailId").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                QueryString IDx1 = QueryStr.Where(l => l.Name == "TYPE").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                QueryString IDx2 = QueryStr.Where(l => l.Name == "AutoCode").FirstOrDefault();
                string EmailID = IDx.Value;
                string TYPE = IDx1.Value;
                string ForgetPassCode = IDx2.Value;

                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@EMAIL_ID", EmailID);
                parameters[1] = new SqlParameter("@PASSWORD", DBNull.Value);
                parameters[2] = new SqlParameter("@TYPE", TYPE);
                parameters[3] = new SqlParameter("@FORGOT_PASS_CODE", ForgetPassCode);
                parameters[4] = new SqlParameter("@Mode", "RESETEMAILEXIST");


                var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "FORGOT_PASSWORD_USER", parameters);

                if (Convert.ToInt32(obj) == 1)
                {
                    txtApplicantEmail.Text = EmailID;
                    txtApplicantPassCode.Text = ForgetPassCode;

                    PanelResetPassword.Visible = true;
                    Panel2.Visible = false;

                }
                else
                {
                    PanelResetPassword.Visible = false;
                    Panel2.Visible = true;


                }
            }

            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='Login.aspx'", true);

            }
        }
 
        
    }

   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
    protected void btnRPassword_Click(object sender, EventArgs e)
    {
        try
        {
            string TpassOld = hashcodegenerate.GetSHA512(txtRNewPassword.Text.ToString() + Session["salt"].ToString());
            String queryString = CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["q"]), true);
            foreach (String str in queryString.Split(new Char[] { '&' }))
            {
                QueryString qs = new QueryString();
                qs.Name = str.Split(new Char[] { '=' })[0];
                qs.Value = str.Split(new Char[] { '=' })[1];

                QueryStr.Add(qs);
            }

            QueryString IDx = QueryStr.Where(l => l.Name == "EmailId").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
            QueryString IDx1 = QueryStr.Where(l => l.Name == "TYPE").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
            QueryString IDx2 = QueryStr.Where(l => l.Name == "AutoCode").FirstOrDefault();
            string Email = IDx.Value;
            string TYPE = IDx1.Value;
            string AutoCode = IDx2.Value;
            
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@EMAIL_ID", Email);
            parameters[1] = new SqlParameter("@PASSWORD", TpassOld);
            parameters[2] = new SqlParameter("@TYPE", TYPE);
            parameters[3] = new SqlParameter("@FORGOT_PASS_CODE", AutoCode);
            parameters[4] = new SqlParameter("@Mode", "UPDATEPASSWORD");

            
            Object objMsg=SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "FORGOT_PASSWORD_USER", parameters);
            if (Convert.ToString(objMsg) == "Success")
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Password updated successfully');window.location ='Login.aspx'", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(objMsg) + "');window.location ='Login.aspx'", true);
            }
            


        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='Login.aspx'", true);

        }
    }
}