using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class STA_COUNTER_SIGNATURE_REPORT_VIEW : System.Web.UI.Page
{
    //private static string ID;
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-DASHBOARD.aspx'", true);
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
                     sCmd.CommandText = "STA_SP_GET_COUNTER_SIGNATURE_MASTER_DATA";
                     sCmd.CommandType = CommandType.StoredProcedure;
                     sCmd.Parameters.Clear();
                     sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                     sCmd.Parameters.AddWithValue("@MODE", "MASTER_VIEW");
                     DataSet ds = new DataSet();
                     SqlDataAdapter da = new SqlDataAdapter(sCmd);
                     da.Fill(ds);

                    
                     DataTable dt1 = ds.Tables[0];
                     DataTable dt2 = ds.Tables[1];
                     DataTable dt3 = ds.Tables[2];
                     DataTable dt4 = ds.Tables[3];
                    
                     StringBuilder sb = new StringBuilder();
                     if (dt1.Rows.Count > 0)
                     {




                         lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                         lblApplicationStatus.Text = dt1.Rows[0]["APPLICATION_STATUS"].ToString();
                         VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                         VOCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                         VOPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                         VOPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();

                         VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                         VOPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                         VOCSNo.Text = dt1.Rows[0]["COUNTER_SIGNATURE_NUMBER"].ToString();
                         VoPermitFrom.Text = dt1.Rows[0]["PERMIT_VALID_FROM"].ToString();
                         VoPermitUpto.Text = dt1.Rows[0]["PERMIT_VALID_UPTO"].ToString();
                         lblMakerDesc.Text = dt1.Rows[0]["MAKER_DESC"].ToString();
                         lblMakermodel.Text = dt1.Rows[0]["MAKER_MODEL"].ToString();
                         VType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                         VChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                         VEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();
                         VSeatCapacity.Text = dt1.Rows[0]["SEATING_CAPACITY"].ToString();
                         vsleepercapacity.Text = dt1.Rows[0]["SLEEPER_CAPACITY"].ToString();

                         txtULW.Text = dt1.Rows[0]["ULW"].ToString();
                         txtRLW.Text = dt1.Rows[0]["RLW"].ToString();
                         VManufacturingON.Text = dt1.Rows[0]["MANUFACTURING_ON"].ToString();
                         VNatureOfGoods.Text = dt1.Rows[0]["NATURE_OF_GOODS_CARRIED"].ToString();


                         VOMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                         VOEmailID.Text = dt1.Rows[0]["EMAIL_ID"].ToString();

                         lbICValidUpto.Text = dt1.Rows[0]["IC_VALID_UPTO"].ToString();
                         lbPCValidUpto.Text = dt1.Rows[0]["PC_VALID_UPTO"].ToString();
                         lbFCValidUpto.Text = dt1.Rows[0]["FC_VALID_UPTO"].ToString();
                         lbTPValidUpto.Text = dt1.Rows[0]["TAX_PAID_UPTO"].ToString();

                         lblRecommendationNo.Text = dt1.Rows[0]["RECOMMENDATION_NUMBER"].ToString();
                         lblRecommendationDate.Text = dt1.Rows[0]["RECOMMENDATION_DATE"].ToString();



                         lblIssuedBy.Text = dt1.Rows[0]["STATE_NAME"].ToString();
                         RouteHDRInfo.Text = dt1.Rows[0]["ROUTE"].ToString();

                         if (dt4.Rows.Count > 0)
                         {
                             sb.Append("<table class='table table-striped table-bordered table-hover'>");
                             sb.Append("<thead>");
                             sb.Append("<tr>");
                             sb.Append("<th colspan='3' style='text-align:center;'> Up </th>");
                             sb.Append("<th>  </th>");
                             sb.Append("<th colspan='3' style='text-align:center;'> Down </th>");
                             sb.Append("</tr>");
                             sb.Append("<tr>");
                             sb.Append("<th> Route </th>");
                             sb.Append("<th> Arrival </th>");
                             sb.Append("<th> Departure </th>");
                             sb.Append("<th>  </th>");
                             sb.Append("<th> Route </th>");
                             sb.Append("<th> Arrival </th>");
                             sb.Append("<th> Departure </th>");
                             sb.Append("</tr>");
                             sb.Append("</thead>");
                             sb.Append("<tbody id='tbdRouteInfo'>");

                             for (int k = 1; k <= dt4.Rows.Count; k++)
                             {
                                 if (dt2.Rows.Count > 0)
                                 {


                                     DataRow[] dr = dt2.Select("SEQUENCE='" + k + "'");

                                     if (dr.Length > 0)
                                     {
                                         //Int32 length = dt2.Rows.Count - 1;
                                         Int32 length = dr.Length - 1;

                                         for (int i = 0; i < dr.Length; i++)
                                         {



                                             sb.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                             sb.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                             sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "</label></td>");
                                             sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "</label></td>");
                                             sb.Append("<td></td>");
                                             sb.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                             sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                             sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                             sb.Append("</tr>");



                                         }
                                     }

                                 }
                             }

                             sb.Append("<tbody>");
                             sb.Append("</table>");
                         }

                        RouteTimeInfo.InnerHtml = sb.ToString();

                        if (dt3.Rows.Count > 0)
                        {

                        foreach (DataRow dr in dt3.Rows)
                                            {
                            string uploadHead = Convert.ToString(dr["UPLOAD_HEADS"]);
                            string fileName   = Convert.ToString(dr["FILE_NAME"]);

                            if (string.IsNullOrEmpty(fileName))
                                continue;

                            switch (uploadHead)
                            {
                                case "RC":
                                    BindDownloadLink(fileName, divrcfile, "STAUploadedCSFiles");
                                    break;

                                case "SPEED":
                                    BindDownloadLink(fileName, divspeedfile, "STAUploadedCSFiles");
                                    break;

                                case "INSURANCE":
                                    BindDownloadLink(fileName, divinsurFile, "STAUploadedCSFiles");
                                    break;

                                case "POLLUTION":
                                    BindDownloadLink(fileName, divrollution, "STAUploadedCSFiles");
                                    break;

                                case "FITNESS":
                                    BindDownloadLink(fileName, divfitness, "STAUploadedCSFiles");
                                    break;

                                case "TAX":
                                    BindDownloadLink(fileName, divTax, "STAUploadedCSFiles");
                                    break;

                                case "PERMIT":
                                    BindDownloadLink(fileName, divPermit, "STAUploadedCSFiles");
                                    break;

                                case "TIMETABLE":
                                    BindDownloadLink(fileName, divTimeTable, "STAUploadedCSFiles");
                                    break;

                                case "RECOMMENDATION":
                                    BindDownloadLink(fileName, divRecommendation, "STAUploadedCSFiles");
                                    break;

                                case "MOM":
                                    BindDownloadLink(fileName, divMOM, "STAUploadedCSFiles");
                                    break;
                            }
                        }

                     }

                   }


                 }

                 catch (Exception ex)
                 {
                     String Message = ex.Message;
                     Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                     ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-COUNTER-SIGNATURE-APPLICATION.aspx'", true);

                 }

             }
        }
    }

    private void BindDownloadLink(
    string fileName,
    HtmlGenericControl targetDiv,
    string folderName)
    {
        string cleanFile = fileName.Replace("@@---@@", string.Empty);

        string downloadUrl = string.Format(
            "download.aspx?path={0}&filename={1}",
            folderName,
            cleanFile
        );

        string link = string.Format(
            "<a href='{0}' target='_blank' title='download'>Click here to download</a>",
            downloadUrl
        );

        targetDiv.InnerHtml = link;
    }
}





