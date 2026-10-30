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
public partial class STA_CS_ISSUED_RECEIPT : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('You are not permitted to access this page');window.location ='STA-DASHBOARD.aspx'", true);
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

                    QueryString IDx = QueryStr.Where(l => l.Name == "PER_NUMBER").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                    String ID = IDx.Value;

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_SP_GET_COUNTER_SIGNATURE_MASTER_DATA";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", ID);
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_ISSUED_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt1 = ds.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        DataTable dtSource = ds.Tables[1];
                        DataTable dtdestination = ds.Tables[2];
                        string sourceSatation = dtSource.Rows[0]["VIA_CITY"].ToString();
                        string sourceSatationDeparture = dtSource.Rows[0]["DEPARTURE_TIMING"].ToString();
                        string DeatinationSatation = dtdestination.Rows[0]["VIA_CITY"].ToString();
                        string DeatinationSatationDepart = dtdestination.Rows[0]["DEPARTURE_TIMING"].ToString();

                        lblSourceStation.Text = sourceSatation;
                        lblSourDepTime.Text = sourceSatationDeparture + "Hrs.";
                        lblDestinationStation.Text = DeatinationSatation;
                        lblDestDepTime.Text = DeatinationSatationDepart + "Hrs.";



                        string permitNumber =  HttpUtility.UrlEncode(dt1.Rows[0]["PERMIT_NUMBER"].ToString());
                        
                        string code = "http://spermit.jharkhand.gov.in/QR_CS_VIEW.aspx?Q=" + permitNumber;
                       
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
                        
                        
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            if (Convert.ToString(dt1.Rows[i]["APPLICATION_STATUS"]) == "Replaced")
                            {
                                trReplace.Visible = true;
                                lblReplace.Text = Convert.ToString(dt1.Rows[i]["LABEL_REPLACE"]);
                            }


                            csno.Text = Convert.ToString(dt1.Rows[i]["COUNTER_SIGNATURE_NUMBER"]);
                            lblName.Text = Convert.ToString(dt1.Rows[i]["OWNER_NAME"]);
                            lblresidentialaddr.Text = Convert.ToString(dt1.Rows[i]["PERMANENT_ADDRESS"]);
                            lblbusinessaddress.Text = Convert.ToString(dt1.Rows[i]["PRESENT_ADDRESS"]);
                            lblprimaryPermitno.Text = Convert.ToString(dt1.Rows[i]["PERMIT_NUMBER"]);
                            lblissuedstate.Text = Convert.ToString(dt1.Rows[i]["STATE_NAME"]);
                            lblValidupto.Text = Convert.ToString(dt1.Rows[i]["PERMIT_VALID_UPTO"]);
                            lblRegNo.Text = Convert.ToString(dt1.Rows[i]["REGISTRATION_NUMBER"]);

                            lblModel.Text = Convert.ToString(dt1.Rows[i]["MANUFACTURING_ON"]);
                            lblSeatCapacity.Text = Convert.ToString(dt1.Rows[i]["SEATING_CAPACITY"]);
                            lblRouteDertailswithvia.Text = Convert.ToString(dt1.Rows[i]["ROUTE"]);
                            lblValidtyFrom.Text = Convert.ToString(dt1.Rows[i]["PERMIT_VALID_FROM"]);
                            lblValidityto.Text = Convert.ToString(dt1.Rows[i]["PERMIT_VALID_UPTO"]);

                            lblTaxFrom.Text = Convert.ToString(dt1.Rows[i]["cs_valid_from"]); 
                            lblTaxto.Text = Convert.ToString(dt1.Rows[i]["cs_valid_to"]);
                            lblAmount.Text = Convert.ToString(dt1.Rows[i]["FEE_TAX_AMOUNT"]);
                            lblReferenceno.Text = Convert.ToString(dt1.Rows[i]["FEE_TAX_PAY_TRANSACTION_ID"]);
                            lblTransactiondt.Text = Convert.ToString(dt1.Rows[i]["FEE_TAX_PAID_ON"]); 
                        
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No data Found');window.location ='STA-DASHBOARD.aspx'", true);
                        return;
                    }
                }


                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    Messqge = Messqge.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location ='STA-DASHBOARD.aspx'", true);

                }
            }

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}


