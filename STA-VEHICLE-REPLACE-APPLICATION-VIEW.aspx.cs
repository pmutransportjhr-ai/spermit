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

public partial class STA_VEHICLE_REPLACE_APPLICATION_VIEW : System.Web.UI.Page
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

                    sCmd.CommandText = "STA_VEHICLE_REPLACE_APPLICATION_APPROVAL";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", hdnID.Value);
                    sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                    sCmd.Parameters.AddWithValue("@MODE", "VEHICLE_REPLACE_APPLICATION_APPROVAL_LIST");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt1 = ds.Tables[0];
                    DataTable dt2 = ds.Tables[1];
                    DataTable dt3 = ds.Tables[2];
                    DataTable dt4 = ds.Tables[3];
                    DataTable dt5 = null;
                    int dtCount = ds.Tables.Count;
                    if (dtCount != 4)
                    {
                         dt5= ds.Tables[4];
                    }

                    StringBuilder sb = new StringBuilder();
                    if (dt1.Rows.Count > 0)
                    {

                        NewVehicleRegNo.InnerText = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString() + " )";
                        OldVehicleRegNumber.InnerText = dt1.Rows[0]["OLD_REGISTRATION_NUMBER"].ToString() + " )";

                        lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                        lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                        lblApplicationStatus.Text = dt1.Rows[0]["APPLICATION_STATUS"].ToString();
                        lblOldRegNo.Text = dt1.Rows[0]["OLD_REGISTRATION_NUMBER"].ToString();
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

                        txtInsuranceCertificate_validUpto.Text = dt1.Rows[0]["OLD_INSURANCE_VALID_UPTO"].ToString();
                        txtPollutionCertificate_validupto.Text = dt1.Rows[0]["OLD_POLLUTION_VALID_UPTO"].ToString();
                        txtFitnessCertificateValidUpto_old.Text = dt1.Rows[0]["OLD_FITNESS_VALID_UPTO"].ToString();
                        txtPaidUpto_Old.Text = dt1.Rows[0]["OLD_JHT_TAX_TOKEN_VALID_UPTO"].ToString();

                        RouteHDRInfo.Text = dt1.Rows[0]["ROUTE"].ToString();


                    }
                    if (dt5 != null)
                    {

                        VORegNumber_old.Text = dt5.Rows[0]["REGISTRATION_NUMBER"].ToString();
                        VType_old.Text = dt5.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                        VChassisNumber_old.Text = dt5.Rows[0]["CHASIS_NUMBER"].ToString();
                        VEngineNumber_old.Text = dt5.Rows[0]["ENGINE_NUMBER"].ToString();

                        lblMakerDesc_old.Text = dt5.Rows[0]["MAKER_DESC"].ToString();
                        lblMakermodel_old.Text = dt5.Rows[0]["MAKER_MODEL"].ToString();
                        lblFuelDesc_old.Text = dt5.Rows[0]["FUEL_DESC"].ToString();
                        lblVcolor_old.Text = dt5.Rows[0]["VEHICLE_COLOR"].ToString();
                        VFitness_old.Text = dt5.Rows[0]["FITNESS_VALID_UPTO"].ToString();
                        VTax_old.Text = dt5.Rows[0]["TAX_PAID_UPTO"].ToString();
                        VULW_old.Text = dt5.Rows[0]["ULW"].ToString();
                        VRLW_old.Text = dt5.Rows[0]["RLW"].ToString();
                        VSeatCapacity_old.Text = dt5.Rows[0]["SEATING_CAPACITY"].ToString();
                        VManufacturingON_old.Text = dt5.Rows[0]["MANUFACTURING_ON"].ToString();


                        VOMobileNo_old.Text = dt5.Rows[0]["MOBILE_NUMBER"].ToString();
                        VOEmailID_old.Text = dt5.Rows[0]["EMAIL_ID"].ToString();
                        lblGender_old.Text = dt5.Rows[0]["GENDER"].ToString();
                        lblQualification_old.Text = dt5.Rows[0]["APPLICANT_QUALIFICATION"].ToString();
                        lblCategory_old.Text = dt5.Rows[0]["APPLICANT_CATEGORY"].ToString();
                        lblExArmy_old.Text = dt5.Rows[0]["EX_ARMY"].ToString();
                        lblMinority_old.Text = dt5.Rows[0]["MINORITY"].ToString();
                        lblHandicapped_old.Text = dt5.Rows[0]["HANDICAPPED"].ToString();
                        lblHML_old.Text = dt5.Rows[0]["HEAVY_MOTOR_LICENCE"].ToString();
                        lblIdentityProof_old.Text = dt5.Rows[0]["IDENTITY_PROOF"].ToString();
                        lblAC_old.Text = dt5.Rows[0]["AIR_CONDITIONED"].ToString();
                        lblDeluxe_old.Text = dt5.Rows[0]["DELUXE"].ToString();
                        lblSleeper_old.Text = dt5.Rows[0]["SLEEPER"].ToString();

                        lbICValidUpto_old.Text = dt5.Rows[0]["IC_VALID_UPTO"].ToString();
                        lbPCValidUpto_old.Text = dt5.Rows[0]["PC_VALID_UPTO"].ToString();
                        lbFCValidUpto_old.Text = dt5.Rows[0]["FC_VALID_UPTO"].ToString();
                        lblIDNo_old.Text = dt5.Rows[0]["IDENTITY_PROOF_NO"].ToString();
                    
                    
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

                            ////// Old Vehicle Documents ////
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "OLD_REG")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                RegistrationCertificate_old.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "OLD_SPEED")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                SpeedGovernorCertificate_old.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "OLD_INSURANCE")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                InsuranceCertificate_old.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "OLD_POLLU")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                PollutionCertificate_old.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "OLD_FITNESS")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                fitnessCertificate_old.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "OLD_PERMIT")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                permitCertificate_old.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "OLD_JHT_TAX")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                jhTaxToken_certificate_old.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "OLD_SLD")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                sldCertificate_old.InnerHtml = strlink;
                            }
                            if (Convert.ToString(dr["UPLOAD_HEADS"]) == "OLD_NEIBH")
                            {
                                string newstr = "download.aspx?path=STAUploadedFiles&filename=" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                divNeibh.InnerHtml = strlink;
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
    
    



    