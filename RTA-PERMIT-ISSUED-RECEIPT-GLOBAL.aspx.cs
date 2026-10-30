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


public partial class RTA_PERMIT_ISSUED_RECEIPT_GLOBAL : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-DASHBOARD.aspx'", true);
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
                    sCmd.CommandText = "RTA_SP_GET_PERMIT_MASTER_DATA";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", ID);
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_ISSUED_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt1 = ds.Tables[0];
                    DataTable dt2 = ds.Tables[1];
                    if (dt1.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt1.Rows[0]["PERMIT_TYPE"]).ToUpper().Contains("TEMPORARY"))
                        {
                            permitTypeHeading_per.Visible = false;
                            permitTypeHeading_Tempo.Visible = true;
                            permit_Renewal_div.Visible = false;
                        }
                        else
                        {
                            permitTypeHeading_per.Visible = true;
                            permitTypeHeading_Tempo.Visible = false;
                            permit_Renewal_div.Visible = true;
                        }


                        if (Convert.ToString(dt1.Rows[0]["RTO_NAME"]).Contains("RANCHI"))
                        {
                            lblRegion.Text = "RANCHI";
                            lblPlace.Text = "RANCHI";
                            lblPlace2.Text = "RANCHI";
                            lblPlace3.Text = "RANCHI";
                            lblregion1.Text = "SOUTH CHHOTANAGPUR<br/>REGIONAL TRANSPORT AUTHORITY<br />RANCHI (JHARKHAND)";
                            lblregion2.Text = "SOUTH CHHOTANAGPUR<br/>REGIONAL TRANSPORT AUTHORITY<br />RANCHI (JHARKHAND)";
                            lblregion3.Text = "SOUTH CHHOTANAGPUR<br/>REGIONAL TRANSPORT AUTHORITY<br />RANCHI (JHARKHAND)";
                        }
                        else if (Convert.ToString(dt1.Rows[0]["RTO_NAME"]).Contains("HAZARIBAG"))
                        {
                            lblRegion.Text = "HAZARIBAG";
                            lblPlace.Text = "HAZARIBAG";
                            lblPlace2.Text = "HAZARIBAG";
                            lblPlace3.Text = "HAZARIBAG";
                            lblregion1.Text = "NORTH CHHOTANAGPUR<br/>REGIONAL TRANSPORT AUTHORITY<br />HAZARIBAG (JHARKHAND)";
                            lblregion2.Text = "NORTH CHHOTANAGPUR<br/>REGIONAL TRANSPORT AUTHORITY<br />HAZARIBAG (JHARKHAND)";
                            lblregion3.Text = "NORTH CHHOTANAGPUR<br/>REGIONAL TRANSPORT AUTHORITY<br />HAZARIBAG (JHARKHAND)";

                        }
                        else if (Convert.ToString(dt1.Rows[0]["RTO_NAME"]).Contains("PALAMAU"))
                        {
                            lblRegion.Text = "PALAMAU";
                            lblPlace.Text = "PALAMAU";
                            lblPlace2.Text = "PALAMAU";
                            lblPlace3.Text = "PALAMAU";
                            lblregion2.Text = "PALAMAU<br/>REGIONAL TRANSPORT AUTHORITY<br />PALAMAU (JHARKHAND)";
                            lblregion3.Text = "PALAMAU<br/>REGIONAL TRANSPORT AUTHORITY<br />PALAMAU (JHARKHAND)";
                            lblregion1.Text = "PALAMAU<br/>REGIONAL TRANSPORT AUTHORITY<br />PALAMAU (JHARKHAND)";

                        }
                        else if (Convert.ToString(dt1.Rows[0]["RTO_NAME"]).Contains("DUMKA"))
                        {
                            lblRegion.Text = "DUMKA";
                            lblPlace.Text = "DUMKA";
                            lblPlace2.Text = "DUMKA";
                            lblPlace3.Text = "DUMKA";
                            lblregion1.Text = "DUMKA<br/>REGIONAL TRANSPORT AUTHORITY<br />DUMKA (JHARKHAND)";
                            lblregion2.Text = "DUMKA<br/>REGIONAL TRANSPORT AUTHORITY<br />DUMKA (JHARKHAND)";
                            lblregion3.Text = "DUMKA<br/>REGIONAL TRANSPORT AUTHORITY<br />DUMKA (JHARKHAND)";

                        }
                        else if (Convert.ToString(dt1.Rows[0]["RTO_NAME"]).Contains("KOLHAN"))
                        {
                            lblRegion.Text = "KOLHAN";
                            lblPlace.Text = "KOLHAN";
                            lblPlace2.Text = "KOLHAN";
                            lblPlace3.Text = "KOLHAN";
                            lblregion1.Text = "KOLHAN<br/>REGIONAL TRANSPORT AUTHORITY<br />KOLHAN (JHARKHAND)";
                            lblregion2.Text = "KOLHAN<br/>REGIONAL TRANSPORT AUTHORITY<br />KOLHAN (JHARKHAND)";
                            lblregion3.Text = "KOLHAN<br/>REGIONAL TRANSPORT AUTHORITY<br />KOLHAN (JHARKHAND)";

                        }
                        else
                        {
                            lblRegion.Text = "JHARKHAND";
                            lblPlace.Text = "JHARKHAND";
                            lblPlace2.Text = "JHARKHAND";
                            lblPlace3.Text = "JHARKHAND";
                            lblregion1.Text = "JHARKHAND<br/>REGIONAL TRANSPORT AUTHORITY";
                            lblregion2.Text = "JHARKHAND<br/>REGIONAL TRANSPORT AUTHORITY";
                            lblregion3.Text = "JHARKHAND<br/>REGIONAL TRANSPORT AUTHORITY";

                        }

                        string RegNumber = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();

                        string code = "http://spermit.jharkhand.gov.in/QR_VIEW.aspx?REG_NUMBER=" + RegNumber + "&TYPE=RTA";
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
                        lblPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                        lblOwnerName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                        lblPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();
                        lblPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                        lblRoute.Text = dt1.Rows[0]["ROUTE"].ToString();
                        lblPermitFrom.Text = dt1.Rows[0]["VALID_FROM"].ToString();
                        lblPermitTo.Text = dt1.Rows[0]["VALID_UP_TO"].ToString();
                        lblMake.Text = dt1.Rows[0]["MAKER_DESC"].ToString();
                        lblModel.Text = dt1.Rows[0]["MODEL"].ToString();
                        lblRegistrationNo.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                        lblChasisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                        lblEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();
                        lblCapacity.Text = dt1.Rows[0]["SEATING_CAPACITY"].ToString();
                        lblRENUpto.Text = lblPermitTo.Text = dt1.Rows[0]["VALID_UP_TO"].ToString();
                        lblRENFrom.Text = dt1.Rows[0]["VALID_FROM"].ToString();
                        if (dt2.Rows.Count > 0)
                        {
                            lblMaximumTrip.Text = Convert.ToString(dt2.Rows.Count) + " trip";
                        }
                        else
                        {
                            lblMaximumTrip.Text = "1 trip";
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-DASHBOARD.aspx'", true);
                        return;
                    }
                }


                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    Messqge = Messqge.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location ='RTA-DASHBOARD.aspx'", true);

                }
            }

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}


