using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json;
using System.Text;
using QRCoder;
using System.Drawing;
using System.IO;

public partial class RTA_PERMIT_TRANSACTION_RECEIPT : System.Web.UI.Page
{
   protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                return;
            }
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
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
                    QueryString IDx = QueryStr.Where(l => l.Name == "APP_ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                    QueryString IDx1 = QueryStr.Where(l => l.Name == "TYPE").FirstOrDefault();
                    QueryString IDx2 = QueryStr.Where(l => l.Name == "APPLICATION_SERVICE").FirstOrDefault();
                    String ID = IDx.Value;
                    String TYPE = IDx1.Value;
                    String APPLICATION_SERVICE = IDx2.Value;

                    //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(ID+","+ TYPE+","+ APPLICATION_SERVICE) + "');", true);
                  /*  String ID = "1841";
                    String TYPE = "RTA-APPLICATION";
                    String APPLICATION_SERVICE = "RTA PERMIT APPLICATION";*/

                    sCmd.CommandText = "RTA_PERMIT_APPLY_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@APP_ID", ID);
                    sCmd.Parameters.AddWithValue("@TYPE", TYPE);
                    sCmd.Parameters.AddWithValue("@APPLICATION_SERVICE", APPLICATION_SERVICE);
                    sCmd.Parameters.AddWithValue("@MODE", "TAX_RECEIPT_COPY_PERMIT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0];
            
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            lblapnumber.InnerText = dt1.Rows[i]["APPLICATION_NUMBER"].ToString();
                            servicename.InnerText = dt1.Rows[i]["SERVICE_NAME"].ToString();
                            trndate.InnerText = dt1.Rows[i]["TRANSACTION_ON"].ToString();
                            trnno.InnerText = dt1.Rows[i]["DEPTTRANID"].ToString();
                            Amount.InnerText = dt1.Rows[i]["TXN_AMOUNT"].ToString();

                        }
                        string code = "spermit.jharkhand.gov.in/QR_RTA_TRNS_RECEIPT.aspx?APP_ID=" + ID + "~TYPE=" + TYPE + "~APPLICATION_SERVICE=" + HttpUtility
                        .UrlEncode(APPLICATION_SERVICE);
                        //     string code = "spermit.jharkhand.gov.in/QR_STA_TRNS_RECEIPT.aspx";
                      //  Response.Write(code);
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                        imgBarCode.Height = 100;
                        imgBarCode.Width = 100;
                        using (Bitmap bitMap = qrCode.GetGraphic(20))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] byteImage = ms.ToArray();
                                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                            }
                            plBarCode.Controls.Add(imgBarCode);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx';", true);
                        return;
                    }
                }


                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message.ToString() + "');window.location ='PermitHomePage.aspx';", true);

                }


            }
        }
    }
    
}