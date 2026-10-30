using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.IO;
using System.Configuration;
using System.Text;
using QRCoder;
using System.Drawing;


public partial class STA_SURRENDER_OF_PERMIT_APPLICATION_APPLIED_RECEIPT : System.Web.UI.Page
{
   // private static string ID;
  
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                return;
            }
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
                    QueryString IDx1 = QueryStr.Where(l => l.Name == "TYPEID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                    QueryString IDx2 = QueryStr.Where(l => l.Name == "OPTIONID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);

                    PageTitle PT = new PageTitle();
                    DataTable dt = new DataTable();
                    string x1 = IDx1.Value;
                    string x2 = IDx2.Value;

                    dt = PT.PageHeading(Convert.ToInt32(x1), Convert.ToInt32(x2));


                    if (dt.Rows.Count > 0)
                    {
                        Page.Title = dt.Rows[0]["STA_TITLE"].ToString() + " - Receipt";
                        PageHeading.InnerText = dt.Rows[0]["STA_HEADING"].ToString();
                        PageHeading1.InnerText = dt.Rows[0]["STA_TITLE"].ToString() + " - Receipt";

                    }
                    hdnID.Value = IDx.Value;
                    sCmd.CommandText = "STA_SURRENDER_OF_PERMIT_APPLICATION_HDR_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value.ToString()));
                    sCmd.Parameters.AddWithValue("@MODE", "VIEW_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt1 = ds.Tables[0];

                    
                    lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    lblApplicationStatus.Text = dt1.Rows[0]["APPLICATION_STATUS"].ToString();
                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    LblPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                    VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    
                    string code = "Vehicle No. - " + dt1.Rows[0]["REGISTRATION_NUMBER"].ToString() + " | Application No. - " + dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
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


                    //String EncryptedStr = "";

                    //String RandNo = Convert.ToString(DateTime.Now.Ticks);

                    //EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(dt1.Rows[0]["OWNER_NAME"]),
                    //RandNo, Convert.ToString(dt1.Rows[0]["FEE"]), Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]), "NA", "STA PERMIT SURRENDER",
                    //hdnID.Value, "STA-APPLICATION", "PRJ", "PRJSES026", "sec1234");


                    //EncryptedParameter.Value = EncryptedStr;







                }

                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                }

            }
        }
    }

   
   
}
    
    



    