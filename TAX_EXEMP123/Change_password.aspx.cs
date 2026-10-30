using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Security.Cryptography;

public partial class Change_password : System.Web.UI.Page
{
    static string str = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
    SqlConnection con = new SqlConnection(str);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Session["AdminLogin"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
    protected void change()
    {
        try
        {
            string qry = "select * from cordinator where co_id='" + Session["user_cord"] + "'";
            SqlDataAdapter sda = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["user_cord"] = dt.Rows[0]["co_id"].ToString();
                Session["pass_cord"] = Decrypt(dt.Rows[0]["password"].ToString());
            }
            else
            {
                Response.Write("<script>alert('No Record Found');window.location='changepassword.aspx';</script>");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    protected void btnPassword_Click(object sender, EventArgs e)
    {
 
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string qry = "Select password from excempt_login where username='" + Session["AdminLogin"].ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (txtOldPass.Text != Decrypt(dt.Rows[0]["password"].ToString()))
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'> alert('Password you entered is incorrect.');</script>");
                    return;
                }
                else
                {
                    if (txtNewPass.Text == txtConfPass.Text)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            string strSql = "update excempt_login set password=@password where username='" + Session["AdminLogin"].ToString() + "'";
                            cmd.CommandText = strSql;
                            cmd.CommandTimeout = 300;
                            cmd.Parameters.AddWithValue("@password", SqlDbType.VarChar).Value = Encrypt(txtNewPass.Text);
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                            Session.Remove("AdminLogin");
                            string script = @"<script type='text/javascript'>window.location='Login.aspx'; alert('Password Changed Successfully');   </script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "myJScript1", script);

                            //string log = Session["AdminLogin"].ToString().Substring(0,3);
                            //if (log == "DTO")
                            //{
                            //    string script = @"<script type='text/javascript'>window.location='DTO_VERIFY.aspx'; alert('Password Changed Successfully');   </script>";
                            //    ClientScript.RegisterStartupScript(this.GetType(), "myJScript1", script);
                            //}
                            //if (log == "RTA")
                            //{
                            //    string script = @"<script type='text/javascript'>window.location='RTA_VERIFY.aspx'; alert('Password Changed Successfully');   </script>";
                            //    ClientScript.RegisterStartupScript(this.GetType(), "myJScript1", script);
                            //}
                            //if (log == "STA")
                            //{
                            //    string script = @"<script type='text/javascript'>window.location='STA_VERIFY.aspx'; alert('Password Changed Successfully');   </script>";
                            //    ClientScript.RegisterStartupScript(this.GetType(), "myJScript1", script);
                            //}
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'> alert('Password does not matches');</script>");
                        return;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
        finally
        {
            con.Close();
        }
    }
    protected void lkbLogout_Click(object sender, EventArgs e)
    {
        Session.Remove("AdminLogin");
        Response.Redirect("Login.aspx");
    }
    
}