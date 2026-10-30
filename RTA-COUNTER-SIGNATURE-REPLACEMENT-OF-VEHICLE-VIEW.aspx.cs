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

public partial class RTA_COUNTER_SIGNATURE_REPLACEMENT_OF_VEHICLE_VIEW : System.Web.UI.Page
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

                    QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);

                    hdnID.Value = IDx.Value;

                    sCmd.CommandText = "RTA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_HDR_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", hdnID.Value);
                   sCmd.Parameters.AddWithValue("@MODE", "VIEW_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt1 = ds.Tables[0];
                    DataTable dt2 = ds.Tables[1];
                    StringBuilder sb = new StringBuilder();
                    if (dt1.Rows.Count > 0)
                    {
                        VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                        lblCSRTOName.Text = dt1.Rows[0]["RTO_NAME"].ToString();
                        VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                        VOPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                        VoPermitFrom.Text = dt1.Rows[0]["PERMIT_VALID_FROM"].ToString();
                        VoPermitUpto.Text = dt1.Rows[0]["PERMIT_VALID_UPTO"].ToString();
                        VOMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                        VOEmailID.Text = dt1.Rows[0]["EMAIL_ID"].ToString();

                        lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                        lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                        lblApplicationStatus.Text = dt1.Rows[0]["APPLICATION_STATUS"].ToString();
                        lblOldRegNo.Text = dt1.Rows[0]["OLD_REGISTRATION_NUMBER"].ToString();
                        
                  

                    }
                    
                    if (dt2.Rows.Count > 0)
                    {
                       
                  foreach (DataRow dr in dt2.Rows)
                        {
                            
                              if (Convert.ToString(dr["UPLOAD_HEADS"]) == "PERMIT")
                            {
                                string newstr = "JHT/RTAUploadedCSFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divPermit.InnerHtml = strlink;
                            }
                              
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-DASHBOARD.aspx'", true);

                }
            }

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RTA-DASHBOARD.aspx");

    }
  
}
    
    



    