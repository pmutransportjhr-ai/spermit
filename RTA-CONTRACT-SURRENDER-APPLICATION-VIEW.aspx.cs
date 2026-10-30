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

public partial class RTA_CONTRACT_SURRENDER_APPLICATION_VIEW : System.Web.UI.Page
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

                    QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);

                    hdnID.Value = IDx.Value;
                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "RTA_CONTRACT_SURRENDER_OF_PERMIT_APPROVAL";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", hdnID.Value);
                    sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_SURRENDER_VIEW");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt = new DataTable();
                   
          
                   
                    DataTable dt1 = ds.Tables[0];
                    DataTable dt2 = ds.Tables[1];
                     StringBuilder sb = new StringBuilder();
                    if (dt1.Rows.Count > 0)
                    {
                        

                        lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();

                        lblApplicationStatus.Text = dt1.Rows[0]["APPLICATION_STATUS"].ToString();
                        lblPermitNo.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                        lblPermitType.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                        VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                        VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();

                      
                        lblMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                        lblEmailId.Text = dt1.Rows[0]["EMAIL_ID"].ToString();
                        lblComments.Text = dt1.Rows[0]["COMMENTS"].ToString();

                    }

                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt2.Rows)
                        {
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "RC")
                            {

                                string newstr = "JHT/RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divRC.InnerHtml = strlink;

                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "PERMIT")
                            {

                                string newstr = "JHT/RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divPermit.InnerHtml = strlink;

                            }


                           
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "OTHER")
                            {
                                string newstr = "JHT/RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divOther.InnerHtml = strlink;
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
  
}
    
    



    