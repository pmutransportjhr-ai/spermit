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

public partial class RTA_AUTO_RICKSHAW_PERMIT_RENEWAL_APPLICATION_APPLIED_RECEIPT : System.Web.UI.Page
{
    //private static string ID;
  
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page'); windows.location = 'PermitHomePage.aspx'", true);
                return;
            }
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

                ID.Value = IDx.Value;
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ID", ID.Value);
                parameters[1] = new SqlParameter("@EMPLOYEE_ID", Session["EMPLOYEE_ID"]);
                parameters[2] = new SqlParameter("@MODE", "16KM_PERMIT_RENEWAL_APPLICATION_VERIFY_LIST_BYID");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST_BYID", parameters);


                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];
                DataTable dt3 = ds.Tables[2];
               
                StringBuilder sb = new StringBuilder();
                if (dt1.Rows.Count > 0)
                {
                                     
                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    lblPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                    lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    
                    VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    VOCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                    VOPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                    VOPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();
                    VOMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                    VChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                    VEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();
                    VTax.Text = dt1.Rows[0]["TAX_PAID_UPTO"].ToString();
                    VFitness.Text = dt1.Rows[0]["FITNESS_VALID_UPTO"].ToString();
                    VType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                    VInsuranceCmp.Text = dt1.Rows[0]["INSURANCE_COMPANY"].ToString();
                    VInsuranceNo.Text = dt1.Rows[0]["INSURANCE_POLICY_NO"].ToString();
                    VInsuranceUpto.Text = dt1.Rows[0]["INSURANCE_UPTO"].ToString();

                    lblPermitFees.Text = dt1.Rows[0]["FEE"].ToString();
                    lblPaymentStatus.Text = dt1.Rows[0]["PAYMENT_STATUS"].ToString();
                    VSeatingCapacity.Text = dt1.Rows[0]["SEAT_CAPACITY"].ToString();
                    VAgreementWith.Text = dt1.Rows[0]["AGREEMENT_WITH"].ToString();
                    lbRTOName.Text = dt1.Rows[0]["RTO_NAME"].ToString();
                    lblRTOCode.Value = dt1.Rows[0]["RTO_CODE"].ToString();

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




                    String RandNo = Convert.ToString(DateTime.Now.Ticks);

                    string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102030101", Convert.ToString(dt1.Rows[0]["OWNER_NAME"]), RandNo,
                    Convert.ToString(dt1.Rows[0]["FEE"]), Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]), "NA", "RTA AUTO RICKSHAW PERMIT RENEW",
                    ID.Value, "RTA", Convert.ToString(dt3.Rows[0]["TREASCODE"]), Convert.ToString(dt3.Rows[0]["IFMSOFFICECODE"]), "sec1234");

                    EncryptedParameter.Value = EncryptedStr;


                }

                foreach (DataRow dr in dt2.Rows)
                {
                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "RC")
                    {

                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                        divrcfile.InnerHtml = strlink;

                    }

                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "INSURANCE")
                    {
                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                        divinsurFile.InnerHtml = strlink;

                    }

                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "POLLUTION")
                    {
                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                        divrollution.InnerHtml = strlink;
                    }

                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "FITNESS")
                    {
                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                        divfitness.InnerHtml = strlink;
                    }
                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "PERMIT")
                    {
                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                        divPermit.InnerHtml = strlink;
                    }
                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "AGREEMENT")
                    {

                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click Here to Download");
                        divagreement.InnerHtml = strlink;

                    }

                }
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

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




