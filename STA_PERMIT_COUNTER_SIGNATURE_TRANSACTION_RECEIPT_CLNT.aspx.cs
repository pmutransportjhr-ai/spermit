using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using QRCoder;
using System.Configuration;
using System.Web.Services;


public partial class STA_PERMIT_COUNTER_SIGNATURE_TRANSACTION_RECEIPT_CLNT : System.Web.UI.Page
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
                String abc = CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(1), true);
                QueryString IDx = QueryStr.Where(l => l.Name == "APP_ID").FirstOrDefault();
                String appID = IDx.Value;

                QueryString IDx3 = QueryStr.Where(l => l.Name == "TBL").FirstOrDefault();
                String tableType = IDx3.Value;

                QueryString IDx1 = QueryStr.Where(l => l.Name == "TYPE").FirstOrDefault();
                String type = IDx1.Value;

                QueryString IDx2 = QueryStr.Where(l => l.Name == "APPLICATION_SERVICE").FirstOrDefault();
                String appService = IDx2.Value;

                QueryString grnQs = QueryStr.Where(l => l.Name == "GRN").FirstOrDefault();
                string grn = grnQs.Value;


                string qeryString = String.Format("APP_ID={0}~TYPE={1}~APPLICATION_SERVICE={2}~TBL={3}", appID, type, appService, tableType);
                SqlParameter[] parameters = new SqlParameter[7];

                parameters[0] = new SqlParameter("@ID", appID);

                parameters[1] = new SqlParameter("@GRN", grn);

                parameters[2] = new SqlParameter("@EMPLOYEE_ID", DBNull.Value); // required param

                parameters[3] = new SqlParameter("@MODE", "OTHER_STATE_TAX_RECEIPT");

                parameters[4] = new SqlParameter("@TYPE", type);

                parameters[5] = new SqlParameter("@TBL_TYPE", tableType);

                parameters[6] = new SqlParameter("@APPLICATION_SERVICE", appService);

                DataSet ds = SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(),
                    CommandType.StoredProcedure,
                    "STA_SP_MSTR_LIST_BYID",
                    parameters);

               DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        string name = Convert.ToString(dr["OWNER_NAME"]);
                        string registrationNumber = Convert.ToString(dr["REGISTRATION_NUMBER"]);
                        string taxAmount = Convert.ToString(dr["AMOUNT"]);
                        string trnRef = Convert.ToString(dr["TRN_REF_NO"]);
                        string dated = Convert.ToString(dr["TXN_DATED"]);
                        string make = Convert.ToString(dr["VEHICLE_CLASS_TYPE"]);
                        string enginenumber = Convert.ToString(dr["ENGINE_NUMBER"]);
                        string chaisenumber = Convert.ToString(dr["CHASIS_NUMBER"]);
                        string paidFrom = Convert.ToString(dr["FEE_TAX_PAID_FROM"]);
                        string paidUpto = Convert.ToString(dr["FEE_TAX_PAID_UP_TO"]);
                        string grnNumber = Convert.ToString(dr["GRN"]);
                        string seatingCapaity = Convert.ToString(dr["SEATING_CAPACITY"]);
                        string makeYear = Convert.ToString(dr["MAKER_MODEL"]);
                        lblApplicantName.InnerText = name;
                        lblRegisNo.InnerText = registrationNumber;
                        lblgrnNumber.InnerText = grnNumber;
                        lblDated.InnerText = dated;
                        lblMake.InnerText = make + "(Seating Capacity: " + seatingCapaity + ")" + "(" + makeYear + ")";
                        lblEngineNumber.InnerText = enginenumber;
                        lblChaiseNo.InnerText = chaisenumber;
                        lblPaidUpto.InnerText = paidUpto;
                        lblRegNo.InnerText = registrationNumber;
                        lblTaxAmount.InnerText = taxAmount;
                        lblTransNumber.InnerText = trnRef;

                        lblTaxperiod.InnerText = paidFrom + " to " + paidUpto;

                    }

                    string code = "http://spermit.jharkhand.gov.in/QR_STA_CS_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(qeryString);
                    //     string code = "spermit.jharkhand.gov.in/QR_STA_TRNS_RECEIPT.aspx";
                    //Response.Write(code);
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
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                    return;
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}