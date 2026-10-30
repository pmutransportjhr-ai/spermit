using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEra.JHT.Infrastructure;

public partial class _EncryptDecrypt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtCounter.Text = "1";
        }

    }
    protected void Button1_Clicked(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(10000);
        Int32 x = Convert.ToInt32(txtCounter.Text);
        x++;
        txtCounter.Text = Convert.ToString(x);
    }

    protected void btnEncrypt_onClick(object sender, EventArgs e)
    {
        String str = txtString.Text;
        //String encryptedStr = CryptorEngine.Encrypt(str, true);
        clsEncrypt enc = new clsEncrypt();
        String EncryptedData = enc.Encrypt(str, "key1234");
        txtEString.Text = EncryptedData;

    }
    protected void btnDecrypt_onClick(object sender, EventArgs e)
    {
        String encryptedStr = txtEnString.Text;
        //String decryptStr = CryptorEngine.Decrypt(encryptedStr, true);
        clsEncrypt enc = new clsEncrypt();
        String decryptStr = enc.Decrypt(encryptedStr, "key1234");
        txtDString.Text = decryptStr;
    }
}