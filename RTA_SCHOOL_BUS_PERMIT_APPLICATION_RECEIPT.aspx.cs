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


public partial class RTA_SCHOOL_BUS_PERMIT_APPLICATION_RECEIPT : System.Web.UI.Page
{
   // private static string ID;
  
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

                    dt = PT.PageRTAHeading(Convert.ToInt32(x1), Convert.ToInt32(x2));


                    if (dt.Rows.Count > 0)
                    {
                        Page.Title = dt.Rows[0]["RTA_TITLE"].ToString() + " - Receipt";
                        PageHeading.InnerText = dt.Rows[0]["RTA_HEADING"].ToString();
                        PageHeading1.InnerText = dt.Rows[0]["PERMIT_TYPE"].ToString();

                    }
                    hdnID.Value = IDx.Value;

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "RTA_SCHOOL_BUS_PERMIT_APPLICATION_APPROVAL";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID",Convert.ToInt32(hdnID.Value));
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_SCHOOL_BUS_APP_APPROVAL_LIST");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);


                    DataTable dt1 = ds.Tables[0];
                    DataTable dt3 = ds.Tables[1];
                 
                    StringBuilder sb = new StringBuilder();
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

                    lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    VOCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                    VOPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                    VOPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();

                    VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    VType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                    VChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                    VEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();
                    lblFrom.Text = dt1.Rows[0]["ST_FROM"].ToString();
                    //lblTo.Text = dt1.Rows[0]["ST_TO"].ToString();
                    lblGender.Text = dt1.Rows[0]["GENDER"].ToString();
                    lblRTOName.Text = dt1.Rows[0]["RTO_NAME"].ToString();
                    lblMakerDesc.Text = dt1.Rows[0]["MAKER_DESC"].ToString();
                    lblMakermodel.Text = dt1.Rows[0]["MAKER_MODEL"].ToString();
                    lblFuelDesc.Text = dt1.Rows[0]["FUEL_DESC"].ToString();
                    lblVcolor.Text = dt1.Rows[0]["VEHICLE_COLOR"].ToString();
                    VFitness.Text = dt1.Rows[0]["FITNESS_VALID_UPTO"].ToString();
                    VTax.Text = dt1.Rows[0]["TAX_PAID_UPTO"].ToString();
                    VULW.Text = dt1.Rows[0]["ULW"].ToString();
                    VRLW.Text = dt1.Rows[0]["RLW"].ToString();
                    VSeatCapacity.Text = dt1.Rows[0]["SEATING_CAPACITY"].ToString();
                    VManufacturingON.Text = dt1.Rows[0]["MANUFACTURING_ON"].ToString();


                    VOMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                    VOEmailID.Text = dt1.Rows[0]["EMAIL_ID"].ToString();


                    lbICValidUpto.Text = dt1.Rows[0]["IC_VALID_UPTO"].ToString();
                    lbPCValidUpto.Text = dt1.Rows[0]["PC_VALID_UPTO"].ToString();
                    lbFCValidUpto.Text = dt1.Rows[0]["FC_VALID_UPTO"].ToString();

                    lblPermitFees.Text = dt1.Rows[0]["FEE"].ToString();
                    lblPaymentStatus.Text = dt1.Rows[0]["PAYMENT_STATUS"].ToString();
                    if (dt1.Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                    {
                        PaymentDiv.Visible = true;
                    }
                    else
                    {
                        PaymentDiv.Visible = false;
                    }

                      String RandNo = Convert.ToString(DateTime.Now.Ticks);
                    string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102030101", Convert.ToString(dt1.Rows[0]["OWNER_NAME"]),
                        RandNo, Convert.ToString(dt1.Rows[0]["FEE"]), Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]), "NA", "RTA PERMIT APPLICATION SCHOOL BUS",
                        hdnID.Value, "RTA-APPLICATION-SCHOOL-BUS", Convert.ToString(dt1.Rows[0]["TREASCODE"]), Convert.ToString(dt1.Rows[0]["IFMSOFFICECODE"]), "sec1234");
                    EncryptedParameter.Value = EncryptedStr;




                    if (dt3.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt3.Rows)
                        {
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "RC")
                            {

                                //string newstr = "STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                //string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divrcfile.InnerHtml = "Uploaded"; //strlink;

                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "SPEED")
                            {

                                //string newstr = "STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                //string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divspeedfile.InnerHtml = "Uploaded"; //strlink;

                            }

                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "INSURANCE")
                            {
                                //string newstr = "STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                //string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divinsurFile.InnerHtml = "Uploaded"; //strlink;

                            }

                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "POLLUTION")
                            {
                                //string newstr = "STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                //string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divrollution.InnerHtml = "Uploaded"; //strlink;
                            }

                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "FITNESS")
                            {
                                //string newstr = "STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                //string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divfitness.InnerHtml = "Uploaded"; //strlink;
                            }


                        }
                    }
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


        


    protected void btnYes_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "window.location ='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "'", true);

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");

    }
  
}
    
    



    