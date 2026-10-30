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



public partial class STA_PERMIT_OBJECTION_RECEIPT : System.Web.UI.Page
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
                    QueryString IDx1 = QueryStr.Where(l => l.Name == "TBL_MODE").FirstOrDefault();
                    String ID = IDx.Value;
                    String TBL_MODE = IDx1.Value;

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_OBJECTION_APPLICATION_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(ID));
                    sCmd.Parameters.AddWithValue("@TBL_MODE", TBL_MODE);
                    sCmd.Parameters.AddWithValue("@MODE", "VIEW_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt = new DataTable();
                    String EncryptedStr = "";
                    DataTable dt1 = ds.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            if (dt1.Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                PaymentDiv.Visible = true;
                            }
                            else
                            {
                                PaymentDiv.Visible = false;
                            }

                            lblObjectionNo.Text = dt1.Rows[i]["APPLICATION_NUMBER"].ToString();
                            lblEnrollmentOn.Text = dt1.Rows[i]["ENROLLMENT_ON"].ToString();
                            lblOnRegistrationNo.Text = dt1.Rows[i]["OBJECTION_ON_REGISTRATION_NO"].ToString();
                            lblOnRefrenceNo.Text = dt1.Rows[i]["OBJECTION_ON_APPLICATION_NUMBER"].ToString();
                            lblObjectorRegNo.Text = dt1.Rows[i]["OBJECTOR_REGISTRATION_NUMBER"].ToString();
                            lblObjectorPermitNo.Text = dt1.Rows[i]["OBJECTOR_PERMIT_NUMBER"].ToString();
                            lblObjType.Text = dt1.Rows[i]["OBJECTION_TYPE"].ToString();
                            lblObjRemarks.Text = dt1.Rows[i]["OBJECTOR_REMARKS"].ToString();
                            lblObjectorName.Text = dt1.Rows[i]["OWNER_NAME"].ToString();
                            lblAmount.Text = dt1.Rows[i]["FEE"].ToString();

                            Page.Title = dt1.Rows[0]["PERMIT_TYPE"].ToString() + " | OBJECTION (PERMIT) - Receipt";
                            PageHeading.InnerText = "STA > " + dt1.Rows[0]["PERMIT_TYPE"].ToString() + " > PERMIT > OBJECTION";
                            PageHeading1.InnerText = dt1.Rows[0]["PERMIT_TYPE"].ToString() + " | OBJECTION (PERMIT) - Receipt";
                          
                            String RandNo = Convert.ToString(DateTime.Now.Ticks);

                            if (TBL_MODE == "PERMIT")
                            {
                                EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(dt1.Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(dt1.Rows[i]["FEE"]), Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "STA PERMIT OBJECTION PERMIT", Convert.ToString(dt1.Rows[0]["APP_ID"]), "STA-APPLICATION", "PRJ", "PRJSES026", "sec1234");
                            }
                            else
                            {
                                EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(dt1.Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(dt1.Rows[i]["FEE"]), Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "STA PERMIT OBJECTION CS", Convert.ToString(dt1.Rows[0]["APP_ID"]), "STA-APPLICATION", "PRJ", "PRJSES026", "sec1234");
                            }
                            EncryptedParameter.Value = EncryptedStr;


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