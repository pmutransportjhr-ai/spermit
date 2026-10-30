using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;


public class clsEncrypt
{
	public clsEncrypt()
{
}
public RijndaelManaged GetRijndaelManaged(String secretKey)
{
var keyBytes = new byte[16];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
return new RijndaelManaged
{
Mode = CipherMode.CBC,
Padding = PaddingMode.PKCS7,
KeySize = 128,
BlockSize = 128,
Key = keyBytes,
IV = keyBytes
};
}
public byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
{
return rijndaelManaged.CreateEncryptor()
.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
}
public byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
{
return rijndaelManaged.CreateDecryptor()
.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
}
public String Encrypt(String plainText, String key)
{
var plainBytes = Encoding.UTF8.GetBytes(plainText);
return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(key)));
}
public String Decrypt(String encryptedText, String key)
{
var encryptedBytes = Convert.FromBase64String(encryptedText);
return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(key)));
}

public string getParamValue(string urlstring, string paramname)
{
if (urlstring.Length == 0)
return "";
string retval = "";
string url = urlstring.Trim();
string pname = "";
string pvalue = "";
string[] keyvalue;
string[] token ;
try
{
token = url.Split('&');
for (int i = 0; i < token.Length; i++)
{
if (token[i].IndexOf("=") >= 0)
{
keyvalue = token[i].Split('=');
if (keyvalue.Length == 2)
{
pname = keyvalue[0];
pvalue = keyvalue[1];
if (pname.ToUpper() == paramname.ToUpper())
{
retval = pvalue;
}
}
else
{
if (paramname.IndexOf("gpf") >= 0)
{
retval += "&" + token[i];
}
}
}
}
}
catch
{
}
return retval;
}
}