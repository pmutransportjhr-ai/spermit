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
using System.Globalization;

public partial class STA_Category_Certificate_view : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    public string filllUploadDocumetList { set; get; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                using (SqlConnection sCon = new SqlConnection(sConnectionString))
                {
                    try
                    {
                        String queryString = Convert.ToString(Context.Request.QueryString["q"]);

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SP_GET_ONLINE_DATA";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", queryString);
                        DataSet dt = new DataSet();
                        DataTable ds = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(dt);
                        DataTable ds1 = dt.Tables[0];
                        DataTable ds2 = dt.Tables[1];

                        sCon.Close();
                        StringBuilder sb = new StringBuilder();

                        if (ds1.Rows.Count == 1)
                            if (ds1.Rows.Count > 0)
                            {
                                DataRow dr = ds1.Rows[0];

                                lblapplicationnumber.Text = dr["APPLICATION_NUMBER"].ToString();
                                lblRegNumber.Text = dr["REGISTRATION_NUMBER"].ToString();
                                lblmobilenumber.Text = dr["MOBILE_NUMBER"].ToString();
                                lblseatingcapacity.Text = dr["SEATING_CAPACITY"].ToString();
                                lblsleeper.Text = dr["SLEEPER_CAPACITY"].ToString();
                                lblcategory.Text = dr["VEHICLE_CLASS_TYPE"].ToString();


                            }


                        if (ds2.Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds2.Rows)
                            {

                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "CC")
                                {

                                    string newstr = "JHT/STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                    string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                    divcategoryfile.InnerHtml = strlink;

                                }

                                //            sb.Append("<tr>");
                                //        sb.Append("<td>" + Convert.ToString(dr["APP_ID"]) + "</td>");
                                //        sb.Append("<td>" + Convert.ToString(dr["UPLOAD_HEADS"]) + "</td>");

                                //            sb.Append("<td>" + Convert.ToString(dr["FILE_NAME"]) + "</td>");

                                //            sb.Append("</tr>");
                                //        filllUploadDocumetList = sb.ToString();


                            }
                        }
                    }







                    catch (Exception ex)
                    {
                        String Message = ex.Message;
                        Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-DASHBOARD.aspx'", true);

                    }
                }
            }
        }
    }
}
              