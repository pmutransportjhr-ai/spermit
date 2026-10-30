using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;
public class SMSSender
{
    public void Sendsms(string MobileNumber, string text , string templateid)
    {

        string senderid="JHTRPT";
        string password="JhtrpT@2120";
        string username="uidjharsms-jhtrpt";
        string secureKey = "21b085e2-b27e-45a0-a0b7-26dd8e8b9d5e";
        SMSHttpPostClient objSendSms = new SMSHttpPostClient();
        objSendSms.sendSingleSMS(username, password, senderid, MobileNumber, text, secureKey, templateid);

    }

    public void ApplicationNumberSms(string RegistrationNo, string permitNumber, string serviceName,string permitType, string mobileNumber, string applicationNumber)
    {

        string text1 = "Dear Applicatnt, your application number is "+ applicationNumber +". Pls keep it with you for further reference.";
        string senderid = "JHTRPT";
        string password = "JhtrpT@2120";
        string username = "uidjharsms-jhtrpt";
        string secureKey = "21b085e2-b27e-45a0-a0b7-26dd8e8b9d5e";
        SMSHttpPostClient objSendSms1 = new SMSHttpPostClient();

        string result = objSendSms1.sendSingleSMS(username, password, senderid, mobileNumber, text1, secureKey,"jh-permit-apply");
               
    }
}
public class SMSHttpPostClient
{

    


    public String sendSingleSMS(String username, String password, String senderid, String mobileNo, String message, String secureKey,String templateid)
    {
        String responseFromServer = string.Empty;
        try
        {
            Stream dataStream;
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;//;SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
 //           HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://msdgweb.mgov.gov.in/esms/sendsmsrequest");
 
	//HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://164.100.129.141/esms/sendsmsrequestDLT");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://msdgweb.mgov.gov.in/esms/sendsmsrequestDLT");
           request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            ((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            String encryptedPassword = encryptedPasswod(password);
            String NewsecureKey = hashGenerator(username.Trim(), senderid.Trim(), message.Trim(), secureKey.Trim());
            String smsservicetype = "unicodemsg"; //For single message.

            String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                "&content=" + HttpUtility.UrlEncode(message.Trim()) +
                "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +
                "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
                "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim()) +
		"&templateid="+HttpUtility.UrlEncode(templateid.Trim()) ;


            byte[] byteArray = Encoding.ASCII.GetBytes(query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            dataStream = request.GetRequestStream();

            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();

            String Status = ((HttpWebResponse)response).StatusDescription;

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            responseFromServer = reader.ReadToEnd();

            reader.Close();

            dataStream.Close();

            response.Close();
           
        }
        catch(Exception ex)
        {
            string exs = ex.Message.ToString();
        }
        return responseFromServer;
    }
    protected String encryptedPasswod(String password)
    {

        byte[] encPwd = Encoding.UTF8.GetBytes(password);
        //static byte[] pwd = new byte[encPwd.Length];
        HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");
        byte[] pp = sha1.ComputeHash(encPwd);
        // static string result = System.Text.Encoding.UTF8.GetString(pp);
        StringBuilder sb = new StringBuilder();
        foreach (byte b in pp)
        {

            sb.Append(b.ToString("x2"));
        }
        return sb.ToString();

    }
    protected String hashGenerator(String Username, String sender_id, String message, String secure_key)
    {

        StringBuilder sb = new StringBuilder();
        sb.Append(Username).Append(sender_id).Append(message).Append(secure_key);
        byte[] genkey = Encoding.UTF8.GetBytes(sb.ToString());
        //static byte[] pwd = new byte[encPwd.Length];
        HashAlgorithm sha1 = HashAlgorithm.Create("SHA512");
        byte[] sec_key = sha1.ComputeHash(genkey);

        StringBuilder sb1 = new StringBuilder();
        for (int i = 0; i < sec_key.Length; i++)
        {
            sb1.Append(sec_key[i].ToString("x2"));
        }
        return sb1.ToString();
    }
}
class MyPolicy : ICertificatePolicy
{
    public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
    {
        return true;
    }
}




