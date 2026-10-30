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
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;


public partial class SCREEN_RTA_PERMIT_MASTER_EDIT : System.Web.UI.Page
{


    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        string EmpId = Convert.ToString(Session["EMPLOYEE_ID"]);
        string EmpName = Convert.ToString(Session["EMPLOYEE_NAME"]);
        //string EmpType = Convert.ToString(Session["EMP_TYPE"]);
        string UserRole = Convert.ToString(Session["USER_ROLE"]);
        string UserType = Convert.ToString(Session["USER_TYPE"]);

        if (EmpId == null || EmpId == string.Empty || EmpId == ""
            || EmpName == null || EmpName == string.Empty || EmpName == ""
            //  || EmpType == null || EmpType == string.Empty || EmpType == ""
            || UserRole == null || UserRole == string.Empty || UserRole == "" || UserType != "RTA" || UserRole != "Super Admin")
        {
            Response.Redirect("Login.aspx");
            return;
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {

            Page.Title = "RTA PERMIT ENTRY FORM";
            PageHeading.InnerText = "RTA PERMIT ENTRY FORM EDIT";
            PageHeading1.InnerText = "RTA PERMIT ENTRY FORM EDIT";
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-PERMIT-APPLICATION.aspx'", true);
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
                        hdnApp_ID.Value = IDx.Value;

                        sCon.Open();
                        SqlCommand sCmd = new SqlCommand();
                        sCmd.Connection = sCon;
                        sCmd.CommandText = "SCREEN_RTA_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(IDx.Value));
                        sCmd.Parameters.AddWithValue("@MODE", "PERMIT_INTER_STATE_APP_APPROVAL_LIST");
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);


                        DataTable dt1 = ds.Tables[0];
                        DataTable dt2 = ds.Tables[1];
                        DataTable dt3 = ds.Tables[2];

                        txtNoRoundTrip.Value = Convert.ToString(dt3.Rows.Count);
                        StringBuilder sb = new StringBuilder();
                        if (dt1.Rows.Count > 0)
                        {
                            ddlPermitType.SelectedValue = Convert.ToString(dt1.Rows[0]["PERMIT_TYPE_ID"]);

                            txtpermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                            txtpermitNo.ReadOnly = true;
                            txtpermitFrom.Value = Convert.ToString(dt1.Rows[0]["VALID_FROM"]);
                            txtpermitTo.Value = Convert.ToString(dt1.Rows[0]["VALID_UP_TO"]);
                            VMakerDesc.Text = dt1.Rows[0]["MAKER_DESC"].ToString();
                            VMakerModel.Text = dt1.Rows[0]["MAKER_MODEL"].ToString();
                            VFuelDesc.Text = dt1.Rows[0]["FUEL_DESC"].ToString();
                            VColor.Text = dt1.Rows[0]["VEHICLE_COLOR"].ToString();

                            VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                            VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                            VOCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                            VOPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                            VOPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();

                            VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                            VType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();

                            string csStatRta = Convert.ToString(dt1.Rows[0]["CS_STATUS"]);
                            if (csStatRta == "1" || csStatRta == "Verified")
                                ddlCSStatus.SelectedValue = "Verified";
                            else
                                ddlCSStatus.SelectedValue = "Not Verified";

                            VChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                            VEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();

                            VFitness.Value = dt1.Rows[0]["FITNESS_VALID_UPTO"].ToString();
                            VTax.Value = dt1.Rows[0]["TAX_PAID_UPTO"].ToString();
                            VULW.Text = dt1.Rows[0]["ULW"].ToString();
                            VRLW.Text = dt1.Rows[0]["RLW"].ToString();
                            VSeatCapacity.Text = dt1.Rows[0]["SEATING_CAPACITY"].ToString();

                            VManufacturingON.Value = dt1.Rows[0]["MANUFACTURING_ON"].ToString();


                            txtMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                            txtEmailId.Text = dt1.Rows[0]["EMAIL_ID"].ToString();


                            txtICValidUpto.Value = dt1.Rows[0]["IC_VALID_UPTO"].ToString();
                            txtPCValidUpto.Value = dt1.Rows[0]["PC_VALID_UPTO"].ToString();

                            txtIdentityNo.Text = dt1.Rows[0]["IDENTITY_PROOF_NO"].ToString();
                            if (Convert.ToString(dt1.Rows[0]["Gender"]) == "Male")
                            {
                                txtMale.Checked = true;
                                txtFemale.Checked = false;
                            }
                            else
                            {
                                txtMale.Checked = false;
                                txtFemale.Checked = true;

                            }
                            if (Convert.ToString(dt1.Rows[0]["EX_ARMY"]) == "No")
                            {
                                txtExArmyN.Checked = true;
                                txtExArmyY.Checked = false;
                            }
                            else
                            {
                                txtExArmyN.Checked = false;
                                txtExArmyY.Checked = true;

                            }
                            if (Convert.ToString(dt1.Rows[0]["MINORITY"]) == "No")
                            {
                                txtMinorityN.Checked = true;
                                txtMinorityY.Checked = false;
                            }
                            else
                            {
                                txtMinorityN.Checked = false;
                                txtMinorityY.Checked = true;

                            }
                            if (Convert.ToString(dt1.Rows[0]["HANDICAPPED"]) == "No")
                            {
                                txtHandicappedN.Checked = true;
                                txtHandicappedY.Checked = false;
                            }
                            else
                            {
                                txtHandicappedN.Checked = false;
                                txtHandicappedY.Checked = true;

                            }
                            if (Convert.ToString(dt1.Rows[0]["HEAVY_MOTOR_LICENCE"]) == "No")
                            {
                                txtHandicappedN.Checked = true;
                                txtHandicappedY.Checked = false;
                            }
                            else
                            {
                                txtHandicappedN.Checked = false;
                                txtHandicappedY.Checked = true;

                            }
                            if (Convert.ToString(dt1.Rows[0]["AIR_CONDITIONED"]) == "No")
                            {
                                txtACN.Checked = true;
                                txtACY.Checked = false;
                            }
                            else
                            {
                                txtACN.Checked = false;
                                txtACY.Checked = true;
                            }
                            if (Convert.ToString(dt1.Rows[0]["DELUXE"]) == "No")
                            {
                                txtDeluxeN.Checked = true;
                                txtDeluxeY.Checked = false;
                            }
                            else
                            {
                                txtDeluxeN.Checked = false;
                                txtDeluxeY.Checked = true;
                            }

                            if (Convert.ToString(dt1.Rows[0]["SLEEPER"]) == "No")
                            {
                                txtSleeperN.Checked = true;
                                txtSleeperY.Checked = false;
                            }
                            else
                            {
                                txtSleeperN.Checked = true;
                                txtSleeperY.Checked = false;
                            }

                            ddlQualification.Value = Convert.ToString(dt1.Rows[0]["APPLICANT_QUALIFICATION"]);
                            ddlCategory.Value = Convert.ToString(dt1.Rows[0]["APPLICANT_CATEGORY"]);

                            string identityProof = Convert.ToString(dt1.Rows[0]["IDENTITY_PROOF"]);
                            if (identityProof == "Voter Id")
                            {
                                txtIdentityProofV.Checked = true;
                                txtIdentityProofP.Checked = false;
                                txtIdentityProofA.Checked = false;

                            }
                            if (identityProof == "Aadhaar Card")
                            {
                                txtIdentityProofV.Checked = false;
                                txtIdentityProofP.Checked = false;
                                txtIdentityProofA.Checked = true;

                            }
                            else if (identityProof == "Pan Card")
                            {
                                txtIdentityProofV.Checked = false;
                                txtIdentityProofP.Checked = true;
                                txtIdentityProofA.Checked = false;

                            }



                        }
                        if (dt3.Rows.Count > 0)
                        {
                            for (int k = 1; k <= dt3.Rows.Count; k++)
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
                                        sb.Append("<tbody id='tbdRouteInfo_" + k + "'>");
                                        for (int i = 0; i < dr.Length; i++)
                                        {


                                            if (i == 0)
                                            {

                                                sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                                sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                                sb.Append("<td> ..... </td>");
                                                sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("<td></td>");
                                                sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                                sb.Append("<td> ..... </td>");
                                                sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + " class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("</tr>");


                                                
                                            }
                                            else if (i == length)
                                            {
                                                sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                                sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                                sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("<td> ..... </td>");
                                                sb.Append("<td></td>");
                                                sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                                sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("<td> ..... </td>");
                                                sb.Append("</tr>");

                                                

                                            }
                                            else
                                            {

                                                sb.Append("<tr id='tr" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                                sb.Append("<td><label id='lblUPRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                                sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("<td><div class='input-group'><input type='text' style='width:80%' value=" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("<td></td>");
                                                sb.Append("<td id='lblDownRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                                sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' value=" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
                                                sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' value=" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "  class='form-control mask_route' id='txtDownDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "' onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + Convert.ToString(dr[i]["S_NO"]) + "\")" + "' placeholder='' /> &nbsp;&nbsp;Hrs.</div></td>");
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




                    }

                    catch (Exception ex)
                    {
                        String Message = ex.Message;
                        Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='screen_rta_permit_report.aspx'", true);

                    }
                }

            }

        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {

    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("screen_rta_permit_report.aspx");
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string permitNo = txtpermitNo.Text;

        string permitValidFrom = txtpermitFrom.Value;
        string permitValidUpto = txtpermitTo.Value;

        DateTime? permitValidFromDate;
        DateTime? permitValidUptoDate;

        
        if (!string.IsNullOrEmpty(permitValidFrom))
        {
            permitValidFromDate = DateTime.ParseExact(permitValidFrom, "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {

            permitValidFromDate = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
        }

        if (!string.IsNullOrEmpty(permitValidUpto))
        {

            permitValidUptoDate = DateTime.ParseExact(permitValidUpto, "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            permitValidUptoDate = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
        }


        string permitType = ddlPermitType.SelectedValue;
        string permitTypeId = null; string permittypeserviceId = null;

        if (permitType == "1")
        {
            permitTypeId = "1";
            permittypeserviceId = "1";
        }
        else
        {
            permitTypeId = "2";
            permittypeserviceId = "15";
        }



        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;

        string regnopattern = "^[a-zA-Z0-9]*$";
        Regex reg = new Regex(regnopattern);
        if (reg.IsMatch(VORegNumber.Text))
        {

            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                sCon.Open();
                objTrans = sCon.BeginTransaction();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.Transaction = objTrans;
                try
                {


                    sCmd.CommandText = "SCREEN_RTA_PERMIT_APPLICATION_MASTER_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnApp_ID.Value));
                    sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));

                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE", permitTypeId);
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", permittypeserviceId);

                    sCmd.Parameters.AddWithValue("@VALID_FROM", permitValidFromDate);
                    sCmd.Parameters.AddWithValue("@VALID_UP_TO", permitValidUptoDate);

                    sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
                    sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", permitNo);
                    sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                    sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                    sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                    sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
                    string selectedGender, ExArmy, IdentityProof, Minority, Handicapped, HML, AC, Deluxe, Sleeper;

                    if (txtMale.Checked)
                    {
                        selectedGender = txtMale.Value;
                    }
                    else
                    {
                        selectedGender = txtFemale.Value;
                    }

                    if (txtExArmyY.Checked)
                    {
                        ExArmy = txtExArmyY.Value;
                    }
                    else
                    {
                        ExArmy = txtExArmyN.Value;
                    }

                    if (txtMinorityY.Checked)
                    {
                        Minority = txtMinorityY.Value;
                    }
                    else
                    {
                        Minority = txtMinorityN.Value;
                    }
                    if (txtHandicappedY.Checked)
                    {
                        Handicapped = txtHandicappedY.Value;
                    }
                    else
                    {
                        Handicapped = txtHandicappedN.Value;
                    }
                    if (txtHMLY.Checked)
                    {
                        HML = txtHMLY.Value;
                    }
                    else
                    {
                        HML = txtHMLN.Value;
                    }
                    if (txtACY.Checked)
                    {
                        AC = txtACY.Value;
                    }
                    else
                    {
                        AC = txtACN.Value;
                    }
                    if (txtDeluxeY.Checked)
                    {
                        Deluxe = txtDeluxeY.Value;
                    }
                    else
                    {
                        Deluxe = txtDeluxeN.Value;
                    }
                    if (txtSleeperY.Checked)
                    {
                        Sleeper = txtSleeperY.Value;
                    }
                    else
                    {
                        Sleeper = txtSleeperN.Value;
                    }
                    if (txtIdentityProofV.Checked)
                    {
                        IdentityProof = txtIdentityProofV.Value;
                    }
                    else if (txtIdentityProofP.Checked)
                    {
                        IdentityProof = txtIdentityProofP.Value;
                    }
                    else
                    {
                        IdentityProof = txtIdentityProofA.Value;
                    }
                    DateTime? Tax; DateTime? Fitness; DateTime? ManufacturingON;


                    if (!String.IsNullOrEmpty(VTax.Value))
                    {
                        Tax = DateTime.ParseExact(VTax.Value, "d/M/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Tax = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (!String.IsNullOrEmpty(VFitness.Value))
                    {
                        Fitness = DateTime.ParseExact(VFitness.Value, "d/M/yyyy", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Fitness = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (!String.IsNullOrEmpty(VManufacturingON.Value))
                    {
                        VManufacturingON.Value = "01/" + VManufacturingON.Value;

                        ManufacturingON = DateTime.ParseExact(VManufacturingON.Value, "d/M/yyyy", CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);

                    }
                    else
                    {
                        ManufacturingON = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                    }

                    string csStatusRtaVal = (ddlCSStatus.SelectedValue == "1" || ddlCSStatus.SelectedValue == "Verified") ? "Verified" : "Not Verified";
                    sCmd.Parameters.AddWithValue("@CS_STATUS", csStatusRtaVal);

                    sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", VType.Text);
                    sCmd.Parameters.AddWithValue("@CHASIS_NUMBER", VChassisNumber.Text);
                    sCmd.Parameters.AddWithValue("@ENGINE_NUMBER", VEngineNumber.Text);
                    sCmd.Parameters.AddWithValue("@MAKER_DESC", VMakerDesc.Text);
                    sCmd.Parameters.AddWithValue("@MAKER_MODEL", VMakerModel.Text);
                    sCmd.Parameters.AddWithValue("@FUEL_DESC", VFuelDesc.Text);
                    sCmd.Parameters.AddWithValue("@VEHICLE_COLOR", VColor.Text);
                    sCmd.Parameters.AddWithValue("@FITNESS_VALID_UPTO", Fitness);
                    sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO", Tax);
                    sCmd.Parameters.AddWithValue("@ULW", VULW.Text);
                    sCmd.Parameters.AddWithValue("@RLW", VRLW.Text);
                    sCmd.Parameters.AddWithValue("@SEATING_CAPACITY", VSeatCapacity.Text);
                    sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", ManufacturingON);
                    sCmd.Parameters.AddWithValue("@VEHICLE_REGISTERED_AT", HDRegisteredat.Value);

                    sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                    sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
                    sCmd.Parameters.AddWithValue("@GENDER", selectedGender);
                    sCmd.Parameters.AddWithValue("@APPLICANT_QUALIFICATION", ddlQualification.Value);
                    sCmd.Parameters.AddWithValue("@APPLICANT_CATEGORY", ddlCategory.Value);
                    sCmd.Parameters.AddWithValue("@EX_ARMY", ExArmy);
                    sCmd.Parameters.AddWithValue("@MINORITY", Minority);
                    sCmd.Parameters.AddWithValue("@HANDICAPPED", Handicapped);
                    sCmd.Parameters.AddWithValue("@HEAVY_MOTOR_LICENCE", HML);
                    sCmd.Parameters.AddWithValue("@IDENTITY_PROOF", IdentityProof);
                    sCmd.Parameters.AddWithValue("@AIR_CONDITIONED", AC);
                    sCmd.Parameters.AddWithValue("@DELUXE", Deluxe);
                    sCmd.Parameters.AddWithValue("@SLEEPER", Sleeper);



                    sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                    sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                    sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", Fitness);
                    sCmd.Parameters.AddWithValue("@IDENTITY_PROOF_NO", txtIdentityNo.Text.ToString());


                    sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", DBNull.Value);
                    sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXIST1");

                    var Cnt = sCmd.ExecuteScalar();
                    if (Convert.ToInt32(Cnt) == 0)
                    {


                        PERMIT_APPLICATION_LDR rDTO = new JavaScriptSerializer().Deserialize<PERMIT_APPLICATION_LDR>(txtroutjson.Value);
                        sCmd.Parameters["@MODE"].Value = "MSTR_UPDATE";

                        sCmd.ExecuteNonQuery();

                        //sCmd.CommandText = "SCREEN_RTA_PERMIT_APPLICATION_MASTER_AED";
                        //sCmd.CommandType = CommandType.StoredProcedure;
                        //sCmd.Parameters.Clear();
                        //sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnApp_ID.Value));
                        //sCmd.Parameters.AddWithValue("@MODE", "LDR_DELETE");
                        //sCmd.ExecuteNonQuery();

                        foreach (PERMIT_APPLICATION_LDR dr in rDTO.PERMIT_APPLICATION_LDRs)
                        {
                            sCmd.CommandText = "SCREEN_RTA_PERMIT_APPLICATION_MASTER_AED";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();

                            sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnApp_ID.Value));
                            sCmd.Parameters.AddWithValue("@SEQUENCE", dr.ROUTE_DETAIL_ID);
                            sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                            sCmd.Parameters.AddWithValue("@ARRIVAL_TIMING", dr.ARRIVAL_TIMING.ToString());
                            sCmd.Parameters.AddWithValue("@DEPARTURE_TIMING", dr.DEPARTURE_TIMING.ToString());
                            sCmd.Parameters.AddWithValue("@MODE", dr.MODE);
                            sCmd.Parameters.AddWithValue("@ID", dr.ID);
                            sCmd.Parameters.AddWithValue("@OP_MODE", "LDR_INSERT_MSTR");
                            sCmd.ExecuteNonQuery();

                        }

                        sCmd.CommandText = "SCREEN_RTA_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", permitNo);
                        sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnApp_ID.Value));
                        sCmd.Parameters.AddWithValue("@MODE", "LDR_INSERT_HDR");
                        sCmd.ExecuteNonQuery();

                        // LDR_INSERT_HDR

                        objTrans.Commit();
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully updated');window.location ='screen_rta_permit_report.aspx'", true);

                    }
                    else
                    {
                        objTrans.Rollback();
                        String Message = "";
                        Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Registration is already exist');window.location ='screen_rta_permit_report.aspx'", true);

                    }

                }
                catch (Exception ex)
                {
                    objTrans.Rollback();
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='screen_rta_permit_report.aspx'", true);
                }
            }
            

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Registration is not valid');window.location ='screen_rta_permit_report.aspx'", true);

        }
        
    }
}