<%@ WebHandler Language="C#" Class="CS" %>

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
using System.Transactions;
using Newtonsoft.Json;
using System.Text;

public class CS : IHttpHandler {


    public void ProcessRequest(HttpContext context)
    {

        switch (context.Request.QueryString["caller"])
        {
            case "GetCSDetail":
                String sConnectionString3 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString3))
                {
                    try
                    {
                        String App_id = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SP_STA_CS_COMM_HISTORY";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(App_id.Trim()));
                        sCmd.Parameters.AddWithValue("@MODE", "LIST_BY_ID");
                        DataSet ds1 = new DataSet();
                        SqlDataAdapter da1 = new SqlDataAdapter(sCmd);
                        da1.Fill(ds1);
                        DataTable dt1 = ds1.Tables[0]; 
                        if (dt1.Rows.Count > 0)
                        {

                            context.Response.ContentType = "application/json";
                            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dt1));

                        }
                        else
                        {
                            context.Response.Write("ERROR");    
                        }

                    }
                    catch (Exception ex)
                    {
                        context.Response.Write(ex.Message);
                    }

                }
                break;
            case "CSApprove":
                String sConnectionString4 = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString4))
                {
                    try
                    {
                        String App_id = CryptorEngine.Decrypt(Convert.ToString(context.Request.QueryString["q"]), true);
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SP_STA_CS_COMM_HISTORY";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(App_id.Trim()));
                        sCmd.Parameters.AddWithValue("@MODE", "CS_Approved");
                        DataSet ds1 = new DataSet();
                        SqlDataAdapter da1 = new SqlDataAdapter(sCmd);
                        da1.Fill(ds1);
                        DataTable dt1 = ds1.Tables[0];
                        DataTable dt2 = ds1.Tables[1];

                        string MobileNumber = Convert.ToString(dt2.Rows[0]["MOBILE_NUMBER"]);
                        string ApplicationNumber =Convert.ToString(dt2.Rows[0]["APPLICATION_NUMBER"]);
                        SMSSender objsms = new SMSSender();
                        objsms.Sendsms(MobileNumber, "Your Application No: " + ApplicationNumber + "has been Approved.","");
                        
                        if (dt1.Rows.Count > 0)
                        {
                            context.Response.ContentType = "application/json";
                            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dt1));
                        }
                        else
                        {
                            context.Response.Write("ERROR");
                        }

                    }
                    catch (Exception ex)
                    {
                        context.Response.Write(ex.Message);
                    }

                }
                break;
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}