<%@ WebHandler Language="C#" Class="Vehicle" %>

using System;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;

public class Vehicle : IHttpHandler {

    protected List<QueryString> QueryStr = new List<QueryString>();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
      string json = "";
    
        switch (context.Request.QueryString["caller"])
        {
            case "GetVehicle":
                try
                {


                    String queryString = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);

                    foreach (String str in queryString.Split(new Char[] { '&' }))
                    {
                        QueryString qs = new QueryString();
                        qs.Name = str.Split(new Char[] { '=' })[0];
                        qs.Value = str.Split(new Char[] { '=' })[1];
                        QueryStr.Add(qs);
                    }

                    QueryString IDx = QueryStr.Where(l => l.Name == "VNo").FirstOrDefault();

                    String RNo = IDx.Value;
                    //JHVahanDetail.JHVahan obj1 = new JHVahanDetail.JHVahan();
                    //string vehicle_no = context.Request.QueryString["VNo"]; // "JH01T7373";
                    //string pwd = "Jhr#12345";
                    //string imei = null;
                    //string version2 = null;
                    //string mobile = null;
                    //string ip = null;
                    
                    //string exelement = obj1.getVehicleDetails(vehicle_no,pwd,imei,version2,mobile,ip);
                    ////context.Response.Write(exelement);
                    //XmlDocument doc = new XmlDocument();
                    //doc.LoadXml(exelement);
                    String Owner_Name = "LATA SINGH";
                    String Care_Of = "CHANDRA KISHOR SINGH";
                    String Address_Line1 = "AT-BARIATU HOUSING COLONY";
                    String Address_Line2 = "BARIATU ";
                    String City = "RANCHI";
                    String Registration_Date = "04/10/2007";
                    String Vehicle_Type = "BUS";
                    String Chassis_Number = "412073ESZ120527";
                    String Engine_Number = "70E62573301";
                    String Tax_Paid_UpTo = "03/01/2017";
                    String Fitness_Valid_Upto = "01/09/2017";
                    String Insurance_Company = "ANY OTHER";
                    String Insurance_Upto = "12/09/2017";
                    String VNo = RNo;

                    json = JsonConvert.SerializeObject(VNo);
                    context.Response.ContentType = "application/json";
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.Write(json);
           
                    
                    
                    
                        }
                catch (Exception ex)
                {
                    string Msg = "";
                    if (ex.Message.Contains("Unexpected XML declaration."))
                    {
                       Msg = "Vehicle details not found";

                    }
                    else
                    {
                        Msg = ex.Message;
                        Msg = Msg.Replace("\r", " ").Replace("\n", " ");
                
                    }
                    context.Response.Write(Msg);
                }
                break;
            
            

        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}