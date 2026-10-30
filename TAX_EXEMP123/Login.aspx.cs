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
public partial class Login : System.Web.UI.Page
{
    static string str = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
    SqlConnection con = new SqlConnection(str);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["AdminLogin"] == "STA")
            {
                Response.Redirect("STA_VERIFY.aspx");
            }
            else if (Session["AdminLogin"] == "RTA")
            {
                Response.Redirect("RTA_VERIFY.aspx");
            }
            else if (Session["AdminLogin"] == "DTO")
            {
                Response.Redirect("DTO_VERIFY.aspx");
            }
        }
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

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            
            string qry = "Select * from excempt_login where username='" + txtUsername.Text + "' and dept='"+ddlRole.SelectedValue.ToString()+"'";
            SqlDataAdapter sda = new SqlDataAdapter(qry, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["AdminLogin"] = dt.Rows[0]["username"].ToString();
                string pass=Decrypt(dt.Rows[0]["password"].ToString());
                if (ddlRole.SelectedValue == "STA" && txtPassword.Text==pass)
                {
                    Response.Redirect("STA_VERIFY.aspx");
                }
                if (ddlRole.SelectedValue == "RTA" && txtPassword.Text == pass)
                {
                    Response.Redirect("RTA_VERIFY.aspx" );
                }
                if (ddlRole.SelectedValue == "DTO" && txtPassword.Text == pass)
                {
                    Response.Redirect("DTO_VERIFY.aspx");
                }
            }
            else
            {
                lblError.Text = "Please check your Username & Password";
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    
}