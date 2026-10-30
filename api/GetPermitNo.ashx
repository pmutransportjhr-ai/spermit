<%@ WebHandler Language="C#" Class="GetPermitNo" %>

using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
public class GetPermitNo : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        context.Response.ContentType = "text/plain";

        switch (context.Request.QueryString["caller"])
        {
            case "GetSTAPermitNumber":

                String queryString = Convert.ToString(context.Request.QueryString["q"]);
                string RegistrationNumber = queryString.Trim();
                string strResult = string.Empty;
                using (SqlConnection sCon = new SqlConnection(sConnectionString))
                {

                    try
                    {
                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", RegistrationNumber.Trim().ToUpper());
                        sCmd.Parameters.AddWithValue("@MODE", "GET_PERMIT_NUMBER");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);
                        DataTable dt = new DataTable();
                        dt = ds.Tables[0];
                        if(dt.Rows.Count >0)
                        {
                            strResult = "SUCCESS~" + Convert.ToString(dt.Rows[0]["PERMIT_NUMBER"]);
                        }
                        else
                        {
                            strResult = "Fail~";
                        }
                    }
                    catch (Exception ex)
                    {
                            String Message = ex.Message;
                            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                            strResult = "Fail~"+ Message;

                        }
                    }
                    
                    context.Response.ContentType = "application/text";
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.Write(strResult);
                    break;

            case "GetCSPermitNumber":

                    String queryString1 = Convert.ToString(context.Request.QueryString["q"]);
                    string RegistrationNumber1 = queryString1.Trim();
                    string strResult1 = string.Empty;
                    using (SqlConnection sCon = new SqlConnection(sConnectionString))
                    {

                        try
                        {
                            sCon.Open();
                            SqlCommand sCmd = new SqlCommand();
                            sCmd.Connection = sCon;
                            sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();
                            sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", RegistrationNumber1.Trim().ToUpper());
                            sCmd.Parameters.AddWithValue("@MODE", "GET_CS_PERMIT_NUMBER");
                            DataSet ds = new DataSet();
                            SqlDataAdapter da = new SqlDataAdapter(sCmd);
                            da.Fill(ds);
                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                strResult = "SUCCESS~" + Convert.ToString(dt.Rows[0]["PERMIT_NUMBER"]);
                            }
                            else
                            {
                                strResult = "Fail~";
                            }
                        }
                        catch (Exception ex)
                        {
                            String Message = ex.Message;
                            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                            strResult = "Fail~" + Message;

                        }
                    }

                    context.Response.ContentType = "application/text";
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.Write(strResult);
                    break;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}