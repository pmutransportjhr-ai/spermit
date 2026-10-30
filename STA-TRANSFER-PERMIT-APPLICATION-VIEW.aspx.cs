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

public partial class STA_TRANSFER_PERMIT_APPLICATION_VIEW : System.Web.UI.Page
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

                    sCmd.CommandText = "STA_TRANSFER_PERMIT_APPLICATION_APPROVAL";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", hdnID.Value);
                    sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                    sCmd.Parameters.AddWithValue("@MODE", "TRANSFER_PERMIT_APPLICATION_APPROVAL_LIST");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt1 = ds.Tables[0];
                    DataTable dt2 = ds.Tables[1];
                    DataTable dt3 = ds.Tables[2];
                    DataTable dt4 = ds.Tables[3];
                    DataTable dt5 = ds.Tables[4];

                    StringBuilder sb = new StringBuilder();
                    if (dt1.Rows.Count > 0)
                    {
                        lblownerCondition.Text = Convert.ToString(dt1.Rows[0]["OWNER_LIVE"]);
                        lblRealtion.Text = Convert.ToString( dt1.Rows[0]["RELATION_WITH_OWNER"]);

                        lbljhtTaxToken.Text = Convert.ToString(dt1.Rows[0]["JHT_TAX_TOKEN_VALID_UPTO"]);
                        
                        lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                        lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                        lblApplicationStatus.Text = dt1.Rows[0]["APPLICATION_STATUS"].ToString();
                        lblPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                        VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                        VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                        VOCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                        VOPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                        VOPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();

                        VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                        VType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                        VChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                        VEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();

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
                        lblGender.Text = dt1.Rows[0]["GENDER"].ToString();
                        lblQualification.Text = dt1.Rows[0]["APPLICANT_QUALIFICATION"].ToString();
                        lblCategory.Text = dt1.Rows[0]["APPLICANT_CATEGORY"].ToString();
                        lblExArmy.Text = dt1.Rows[0]["EX_ARMY"].ToString();
                        lblMinority.Text = dt1.Rows[0]["MINORITY"].ToString();
                        lblHandicapped.Text = dt1.Rows[0]["HANDICAPPED"].ToString();
                        lblHML.Text = dt1.Rows[0]["HEAVY_MOTOR_LICENCE"].ToString();
                        lblIdentityProof.Text = dt1.Rows[0]["IDENTITY_PROOF"].ToString();
                        lblAC.Text = dt1.Rows[0]["AIR_CONDITIONED"].ToString();
                        lblDeluxe.Text = dt1.Rows[0]["DELUXE"].ToString();
                        lblSleeper.Text = dt1.Rows[0]["SLEEPER"].ToString();

                        lbICValidUpto.Text = dt1.Rows[0]["IC_VALID_UPTO"].ToString();
                        lbPCValidUpto.Text = dt1.Rows[0]["PC_VALID_UPTO"].ToString();
                        lbFCValidUpto.Text = dt1.Rows[0]["FC_VALID_UPTO"].ToString();
                        lblIDNo.Text = dt1.Rows[0]["IDENTITY_PROOF_NO"].ToString();

                        RouteHDRInfo.Text = dt1.Rows[0]["ROUTE"].ToString();
                  

                    }

                    if (dt4.Rows.Count > 0)
                    {
                        for (int k = 1; k <= dt4.Rows.Count; k++)
                        {
                            if (dt2.Rows.Count > 0)
                            {
                                DataRow[] dr = dt2.Select("SEQUENCE='" + k + "'");

                                if (dr.Length > 0)
                                {
                                    //Int32 length = dt2.Rows.Count - 1;
                                    Int32 length = dr.Length - 1;
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
                                    for (int i = 0; i < dr.Length; i++)
                                    {


                                        if (i == 0)
                                        {
                                            sb.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                            sb.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                            sb.Append("<td> ..... </td>");
                                            sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "</label></td>");
                                            sb.Append("<td></td>");
                                            sb.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                            sb.Append("<td> ..... </td>");
                                            sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                            sb.Append("</tr>");
                                        }
                                        else if (i == length)
                                        {
                                            sb.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                            sb.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                            sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "</label></td>");
                                            sb.Append("<td> ..... </td>");
                                            sb.Append("<td></td>");
                                            sb.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                            sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                            sb.Append("<td> ..... </td>");
                                            sb.Append("</tr>");

                                        }
                                        else
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
                                    sb.Append("<tbody>");
                                    sb.Append("</table>");

                                }
                            }
                        }

                        RouteTimeInfo.InnerHtml = sb.ToString();

                     
                    }
                    if (dt5.Rows.Count > 0)
                    {

                        oldVOName.Text = dt5.Rows[0]["OWNER_NAME"].ToString();
                        oldVOCareOf.Text = dt5.Rows[0]["CARE_OF"].ToString();
                        oldVOPresentAddress.Text = dt5.Rows[0]["PRESENT_ADDRESS"].ToString();
                        oldVOPermanentAddress.Text = dt5.Rows[0]["PERMANENT_ADDRESS"].ToString();
                        oldVOMobileNo.Text = dt5.Rows[0]["MOBILE_NUMBER"].ToString();
                        oldVOEmailID.Text = dt5.Rows[0]["EMAIL_ID"].ToString();
                        oldlblGender.Text = dt5.Rows[0]["GENDER"].ToString();
                        oldlblQualification.Text = dt5.Rows[0]["APPLICANT_QUALIFICATION"].ToString();
                        oldlblCategory.Text = dt5.Rows[0]["APPLICANT_CATEGORY"].ToString();
                        oldlblExArmy.Text = dt5.Rows[0]["EX_ARMY"].ToString();
                        oldlblMinority.Text = dt5.Rows[0]["MINORITY"].ToString();
                        oldlblHandicapped.Text = dt5.Rows[0]["HANDICAPPED"].ToString();
                        oldlblHML.Text = dt5.Rows[0]["HEAVY_MOTOR_LICENCE"].ToString();
                        oldlblIdentityProof.Text = dt5.Rows[0]["IDENTITY_PROOF"].ToString();
                        oldlblAC.Text = dt5.Rows[0]["AIR_CONDITIONED"].ToString();
                        oldlblDeluxe.Text = dt5.Rows[0]["DELUXE"].ToString();
                        oldlblSleeper.Text = dt5.Rows[0]["SLEEPER"].ToString();


                    }



                    if (dt3.Rows.Count > 0)
                    {
                       
                  foreach (DataRow dr in dt3.Rows)
                        {
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "RC")
                            {

                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divrcfile.InnerHtml = strlink;

                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "SPEED")
                            {

                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divspeedfile.InnerHtml = strlink;

                            }


                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "INSURANCE")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divinsurFile.InnerHtml = strlink;

                            }

                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "POLLUTION")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divrollution.InnerHtml = strlink;
                            }

                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "FITNESS")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divfitness.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "QUALIFICATION")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divQualification.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "SCST")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divSCST.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "EXARMY")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divExArmy.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "IDPROOF")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divIdentityProof.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "HANDICAPPED")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divHandicapped.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "HML")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divHML.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "PERMIT")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divPermit.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "DEATH")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                DeathCertificate.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "AFFIDAVIT")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divAffidavit.InnerHtml = strlink;
                            }

                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "JHT_TAX_TOKEN")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divJhataxtokencertificate.InnerHtml = strlink;
                            }
                           


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
    
    



    