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
using System.Net;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Resources;
using System.Threading;
using System.Reflection;

public partial class STA_TRANSFER_OF_PERMIT : System.Web.UI.Page
{
    //public string FillAPPlyRouteList { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {

        txtMobileNo.MaxLength = 10;
        txtMobileNumber.MaxLength = 10;
        txtExistRegNo.MaxLength = 12;
        txtExistPermitNo.MaxLength = 20;
        txtOTP.MaxLength = 4;

        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                return;
            }
            else
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

                    QueryString IDx = QueryStr.Where(l => l.Name == "TYPEID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                    QueryString IDx1 = QueryStr.Where(l => l.Name == "OPTIONID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);

                    hdnTypeID.Value = IDx.Value;
                    hdnTypeOptionID.Value = IDx1.Value;
                    PageTitle PT = new PageTitle();
                    DataTable dt1 = new DataTable();
                    dt1 = PT.PageHeading(Convert.ToInt32(hdnTypeID.Value), Convert.ToInt32(hdnTypeOptionID.Value));


                    if (dt1.Rows.Count > 0)
                    {
                        Page.Title = dt1.Rows[0]["STA_TITLE"].ToString();
                        PageHeading.InnerText = dt1.Rows[0]["STA_HEADING"].ToString();
                        PageHeading1.InnerText = dt1.Rows[0]["STA_TITLE"].ToString();

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
    protected void btnGO_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {
                sCmd.CommandText = "STA_TRANSFER_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@MODE", "TRANSFER_PERMIT_SEARCH");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);


                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[0];
                if (dt1.Columns[0].ColumnName == "MSG")
                {
                    Message.Text = dt1.Rows[0]["MSG"].ToString();
                    Message.Visible = true;
                    Panel1.Visible = false;
                    DivMobileValidate.Visible = false;
                    DivOTPMatch.Visible = false;

                }

                else
                {
                    DivMobileValidate.Visible = true;
                    DivOTPMatch.Visible = false;
                    Panel1.Visible = false;
                    txtExistRegNo.Enabled = false;
                    txtExistPermitNo.Enabled = false;
                    btnGO.Enabled = false;
                    Message.Text = "";
                    Message.Visible = false;

                }

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Message + "');window.location ='PermithomePage.aspx'", true);

            }
        }

    }
    protected void btnOTP_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {

                //sCmd.CommandText = "STA_TRANSFER_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandText = "SP_VERIFY_SEARCH";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "VERIFY_MOBILE_NUMBER");
                //var obj1 = sCmd.ExecuteScalar();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);

                DataTable dt1 = new DataTable();
                dt1 = ds.Tables[0];
                if (dt1.Columns[0].ColumnName == "MSG")
                {
                    Message.Text = "Mobile number not match";
                    txtMobileNumber.Value = "";
                    Message.Visible = true;
                    DivMobileValidate.Visible = true;
                    DivOTPMatch.Visible = false;
                    Panel1.Visible = false;


                }
                else
                {
                    string mobNo = Convert.ToString(txtMobileNumber.Value.Trim());
                    string text = Convert.ToString(dt1.Rows[0]["OTP_TEXT"].ToString());
                    SMSSender ss = new SMSSender();
                    ss.Sendsms(mobNo, text, "1307161527893297291");
                    Message.Text = "";
                    Message.Visible = false;
                    txtMobileNumber.Disabled = true;
                    btnOTP.Enabled = false;
                    DivMobileValidate.Visible = true;
                    DivOTPMatch.Visible = true;
                    Panel1.Visible = false;



                }
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

            }
        }
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        String regNo2 = txtExistRegNo.Text.Replace(" ", "").Trim().ToUpper().ToString();
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {
                sCmd.CommandText = "STA_TRANSFER_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtExistRegNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtExistPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@OTP", txtOTP.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "VERIFY_OTP");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);
                DataTable dt1 = ds.Tables[0];

                if (dt1.Columns[0].ColumnName == "MSG")
                {

                    Message.Text = "OTP not match. Please Click on Clear button and try again.";
                    Message.Visible = true;
                    DivMobileValidate.Visible = false;
                    DivOTPMatch.Visible = false;
                    Panel1.Visible = false;

                }

                else
                {
                    DataTable dt2 = ds.Tables[1];
                    DataTable dt3 = ds.Tables[2];

                    StringBuilder sb = new StringBuilder();
                    
                    hdnOldApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    txtVehicleNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    txtVehicleNumber.ReadOnly = true;
                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    lblPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                    lblValidUpto.Text = dt1.Rows[0]["VALID_UP_TO"].ToString();
                    labelRegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    labelName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    labelCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                    labelPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                    labelPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();

                    labelType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                    labelChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                    labelEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();

                    labelMakerDesc.Text = dt1.Rows[0]["MAKER_DESC"].ToString();
                    labelMakermodel.Text = dt1.Rows[0]["MAKER_MODEL"].ToString();
                    labelFuelDesc.Text = dt1.Rows[0]["FUEL_DESC"].ToString();
                    labelcolor.Text = dt1.Rows[0]["VEHICLE_COLOR"].ToString();
                    //labelFitness.Text = dt1.Rows[0]["FITNESS_VALID_UPTO"].ToString();
                    //labelTax.Text = dt1.Rows[0]["TAX_PAID_UPTO"].ToString();
                    labelULW.Text = dt1.Rows[0]["ULW"].ToString();
                    labelRLW.Text = dt1.Rows[0]["RLW"].ToString();
                    labelSeatCapacity.Text = dt1.Rows[0]["SEATING_CAPACITY"].ToString();
                    labelManufacturingON.Text = dt1.Rows[0]["MANUFACTURING_ON"].ToString();


                    labelMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                    labelEmailId.Text = dt1.Rows[0]["EMAIL_ID"].ToString();
                    labelGender.Text = dt1.Rows[0]["GENDER"].ToString();
                    labelQualification.Text = dt1.Rows[0]["APPLICANT_QUALIFICATION"].ToString();
                    labelCategory.Text = dt1.Rows[0]["APPLICANT_CATEGORY"].ToString();
                    labelExArmy.Text = dt1.Rows[0]["EX_ARMY"].ToString();
                    labelMinority.Text = dt1.Rows[0]["MINORITY"].ToString();
                    labelHandicapped.Text = dt1.Rows[0]["HANDICAPPED"].ToString();
                    labelHML.Text = dt1.Rows[0]["HEAVY_MOTOR_LICENCE"].ToString();
                    labelIdentityProof.Text = dt1.Rows[0]["IDENTITY_PROOF"].ToString();
                    labelAC.Text = dt1.Rows[0]["AIR_CONDITIONED"].ToString();
                    labelDeluxe.Text = dt1.Rows[0]["DELUXE"].ToString();
                    labelSleeper.Text = dt1.Rows[0]["SLEEPER"].ToString();
                    labelIDNo.Text = dt1.Rows[0]["IDENTITY_PROOF_NO"].ToString();

                    VahanService myclass = new VahanService();
                    myclass = VahanResponse.GetVahanResponse(regNo2);
                    if (myclass != null)
                    {
                        if (myclass.status == "OK" && myclass.rc_catg == "TRANSPORT")
                        {
                            if (myclass.rc_tax_upto == "" || myclass.rc_tax_upto == "NA" ||
                                          myclass.rc_tax_upto == "N/A" || myclass.rc_tax_upto == "Not Available"
                                          || myclass.rc_tax_upto == null)
                            {
                                labelTax.Text = Convert.ToString(dt1.Rows[0]["TAX_PAID_UPTO"]);
                            }
                            else
                            {
                                //VTax.Enabled = true;
                                labelTax.Text = myclass.rc_tax_upto;
                            }
                            if (myclass.rc_tax_upto == "" || myclass.rc_tax_upto == "NA" ||
                                          myclass.rc_tax_upto == "N/A" || myclass.rc_tax_upto == "Not Available"
                                          || myclass.rc_tax_upto == null)
                            {
                                txtuploadJhtTaxToken.Value = Convert.ToString(dt1.Rows[0]["TAX_PAID_UPTO"]);
                            }
                            else
                            {
                                //VTax.Enabled = true;
                                txtuploadJhtTaxToken.Value = myclass.rc_tax_upto;
                            }

                            if (myclass.rc_fit_upto == "" || myclass.rc_fit_upto == "NA" ||
                                          myclass.rc_fit_upto == "N/A" || myclass.rc_fit_upto == "Not Available"
                                          || myclass.rc_fit_upto == null)
                            {
                                labelFitness.Text = Convert.ToString(dt1.Rows[0]["FITNESS_VALID_UPTO"]);
                            }
                            else
                            {
                                //btn1.Disabled = true;
                                labelFitness.Text = myclass.rc_fit_upto;
                            }

                            if (myclass.rc_fit_upto == "" || myclass.rc_fit_upto == "NA" ||
                                          myclass.rc_fit_upto == "N/A" || myclass.rc_fit_upto == "Not Available"
                                          || myclass.rc_fit_upto == null)
                            {
                                txtFCValidUpto.Value = Convert.ToString(dt1.Rows[0]["FITNESS_VALID_UPTO"]);
                            }
                            else
                            {
                                //btn1.Disabled = true;
                                txtFCValidUpto.Value = myclass.rc_fit_upto;
                            }
                            if (myclass.rc_insurance_upto == "" || myclass.rc_insurance_upto == "NA" ||
                                          myclass.rc_insurance_upto == "N/A" || myclass.rc_insurance_upto == "Not Available"
                                          || myclass.rc_insurance_upto == null)
                            {
                                txtICValidUpto.Value = Convert.ToString(dt1.Rows[0]["IC_VALID_UPTO"]);
                            }
                            else
                            {
                                //btn1.Disabled = true;
                                txtICValidUpto.Value = myclass.rc_fit_upto;
                            }
                        }

                    }

                    RouteHDRInfo.Text = dt1.Rows[0]["ROUTE"].ToString();

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

                    txtOTP.Disabled = true;
                    btnVerify.Enabled = false;
                    Message.Visible = false;
                    Panel1.Visible = true;


                }
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

            }
        }
    }


    protected void btnExistClear_Click(object sender, EventArgs e)
    {
        Message.Text = "";
        txtExistRegNo.Text = "";
        txtExistPermitNo.Text = "";
        Message.Visible = false;
        txtExistRegNo.Enabled = true;
        txtExistPermitNo.Enabled = true;
        btnGO.Enabled = true;
        Panel1.Visible = false;
        DivMobileValidate.Visible = false;
        DivOTPMatch.Visible = false;
        txtMobileNumber.Value = "";
        txtOTP.Value = "";
        txtOTP.Disabled = false;
        btnVerify.Enabled = true;
        txtMobileNumber.Disabled = false;
        btnOTP.Enabled = true;

        txtVehicleNumber.Text = "";
        lblMSg.Text = "";
        lblMSg.Visible = false;
        Panel2.Visible = false;


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        String regNo1 = txtVehicleNumber.Text.Trim();
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {
                
                    lblMSg.Visible = true;

                    
                    

                        VOName.ReadOnly = false;
                        VOCareOf.ReadOnly = false;
                        VOPresentAddress.ReadOnly = false;
                        VOPermanentAddress.ReadOnly = false;

                    
                    sCmd.CommandText = "STA_TRANSFER_PERMIT_APPLICATION_HDR_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnTypeOptionID.Value);
                    sCmd.Parameters.AddWithValue("@MODE", "FEES");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da1 = new SqlDataAdapter(sCmd);
                    da1.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblReplacevehicleFees.Text = ds.Tables[0].Rows[0]["FEE"].ToString();

                    }
                    else
                    {
                        lblReplacevehicleFees.Text = "";

                    }


                    Panel2.Visible = true;
                        





                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');ComponentsDateTimePickers.init();", true);
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

            }

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtVehicleNumber.Text = "";
        lblMSg.Text = "";
        lblMSg.Visible = false;
        Panel2.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;

        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
                DateTime ManufacturingON = Convert.ToDateTime(null);
                string selectedGender, ExArmy, IdentityProof, Minority, Handicapped, HML;

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
                string Owner_Dead = ddlAlive.Value.Trim();
                string Relation = ddlRelation.Value.Trim();

               
                sCmd.CommandText = "STA_TRANSFER_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@APP_ID", 0);

                sCmd.Parameters.AddWithValue("@OLD_APPLICATION_NUMBER",hdnOldApplicationNo.Text.Trim());

                sCmd.Parameters.AddWithValue("@OWNERCONDITION", Owner_Dead);

                string ddlRealtionString ;
                if (Relation == "Other")
                {
                    ddlRealtionString = txtRealtion.Value.Trim();
                }
                else
                {
                    ddlRealtionString = Relation;
                }


                sCmd.Parameters.AddWithValue("@RELATION", ddlRealtionString);

             
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnTypeID.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnTypeOptionID.Value);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", labelRegNumber.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", lblPermitNo.Text);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
                sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", labelType.Text);
                sCmd.Parameters.AddWithValue("@CHASIS_NUMBER", labelChassisNumber.Text);
                sCmd.Parameters.AddWithValue("@ENGINE_NUMBER", labelEngineNumber.Text);
                sCmd.Parameters.AddWithValue("@MAKER_DESC", labelMakerDesc.Text);
                sCmd.Parameters.AddWithValue("@MAKER_MODEL", labelMakermodel.Text);
                sCmd.Parameters.AddWithValue("@FUEL_DESC", labelFuelDesc.Text);
                sCmd.Parameters.AddWithValue("@VEHICLE_COLOR", labelcolor.Text);
                //sCmd.Parameters.AddWithValue("@JHT_TAX_TOKEN_VALID_UPTO", DateTime.ParseExact(txtuploadJhtTaxToken.Value.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture));

                if (DateTime.ParseExact(txtuploadJhtTaxToken.Value.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture) > DateTime.Now)
                {
                    sCmd.Parameters.AddWithValue("@JHT_TAX_TOKEN_VALID_UPTO", DateTime.ParseExact(txtuploadJhtTaxToken.Value.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your Tax is expired.');location.reload();", true);
                    return;
                }

                //sCmd.Parameters.AddWithValue("@FITNESS_VALID_UPTO", DateTime.ParseExact(labelFitness.Text, "d/M/yyyy", CultureInfo.InvariantCulture));
                //sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO", DateTime.ParseExact(labelTax.Text, "d/M/yyyy", CultureInfo.InvariantCulture));

                if (DateTime.ParseExact(labelFitness.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture) > DateTime.Now)
                {
                    sCmd.Parameters.AddWithValue("@FITNESS_VALID_UPTO", DateTime.ParseExact(labelFitness.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Fitness is failed.');location.reload();", true);
                    return;
                }
                if (DateTime.ParseExact(labelTax.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture) > DateTime.Now)
                {
                    sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO", DateTime.ParseExact(labelTax.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your Tax is expired.');location.reload();", true);
                    return;
                }

                

                sCmd.Parameters.AddWithValue("@ULW", labelULW.Text);
                sCmd.Parameters.AddWithValue("@RLW", labelRLW.Text);
                sCmd.Parameters.AddWithValue("@SEATING_CAPACITY", labelSeatCapacity.Text);
                labelManufacturingON.Text = "01/" + labelManufacturingON.Text;
                ManufacturingON = DateTime.ParseExact(labelManufacturingON.Text, "d/M/yyyy", CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);
                sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", ManufacturingON);
                sCmd.Parameters.AddWithValue("@VEHICLE_REGISTERED_AT", "");
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
                sCmd.Parameters.AddWithValue("@AIR_CONDITIONED", labelAC.Text.Trim());
                sCmd.Parameters.AddWithValue("@DELUXE", labelDeluxe.Text.Trim());
                sCmd.Parameters.AddWithValue("@SLEEPER", labelSleeper.Text.Trim());
                //sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));

                if (DateTime.ParseExact(txtICValidUpto.Value.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture) > DateTime.Now)
                {
                    sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture));
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your Insurance is failed.');location.reload();", true);
                    return;
                }

                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));

                //if (DateTime.ParseExact(txtFCValidUpto.Value.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture) > DateTime.Now)
                //{
                //    sCmd.Parameters.AddWithValue("@FC_VALID_UPTO, ", DateTime.ParseExact(txtFCValidUpto.Value.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture));
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your Fitness is failed.');location.reload();", true);
                //      return;
                //}

                sCmd.Parameters.AddWithValue("@IDENTITY_PROOF_NO", txtIdentityNo.Text.ToString());
                sCmd.Parameters.AddWithValue("@FEE", Convert.ToDecimal(lblReplacevehicleFees.Text));
                sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", DBNull.Value);
                //sCmd.Parameters.AddWithValue("@MODE", "DUPLICACYEXIST");
                sCmd.Parameters.AddWithValue("@MODE", "ADD");
                //var obj3 = sCmd.ExecuteScalar();

                //Int32 cntval;
                //cntval = Convert.ToInt32(obj3);
                //if (cntval == 0)
                //{
                    ArrayList validate = uploadFileValidate();
                    string result = string.Empty;
                    bool result1 = true;
                    foreach (var str in validate)
                    {
                        if (str == "OK")
                        {


                        }
                        else
                        {
                            result1 = false;
                            result = result + "  " + str;

                        }

                    }
                    if (result1 == true)
                    {

                        //sCmd.CommandText = "STA_TRANSFER_PERMIT_APPLICATION_HDR_AED";
                        //sCmd.CommandType = CommandType.StoredProcedure;
                        //sCmd.Parameters["@MODE"].Value = "ADD";
                        DataSet ds = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(sCmd);
                        da.Fill(ds);


                        DataTable dt1 = new DataTable();
                        dt1 = ds.Tables[0];

                        if (dt1.Columns[0].ColumnName != "MSG")
                        {

                            Int32 AppId;
                            AppId = Convert.ToInt32(dt1.Rows[0]["APP_ID"]);

                            uploadFile(AppId, sCmd);

                            objTrans.Commit();
                            string RegistrationNo = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                            string permitNo = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);
                            string mobileNo = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);
                            string ApplicationNo = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);
                            string permitType = Convert.ToString(dt1.Rows[0]["PERMIT_TYPE"]);
                            string sevicename = Convert.ToString(dt1.Rows[0]["APPLICATION_SERVICE"]);
                            SMSSender objSms = new SMSSender();

                            objSms.ApplicationNumberSms(RegistrationNo, permitNo, sevicename, permitType, mobileNo, ApplicationNo);
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for Transfer');window.location ='STA-TRANSFER-OF-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(AppId) + "&TYPEID=" + Convert.ToString(hdnTypeID.Value) + "&OPTIONID=" + Convert.ToString(hdnTypeOptionID.Value), true), Response.ContentEncoding) + "'", true);
                        }
                        else
                        {
                            objTrans.Rollback();
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt1.Rows[0]["MSG"]) + "');", true);
                           
                        }
                    }
                    else
                    {

                        objTrans.Rollback();
                        String Message = result;
                        Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                    }
               


            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);
              
            }
        }
    }
    private ArrayList uploadFileValidate()
    {
        ArrayList resulArray = new ArrayList();
        FileUploadValidations validateFile = new FileUploadValidations();
        if (uploadRC.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadRC.FileName, uploadRC.FileContent));
        }
        if (uploadInsur.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadInsur.FileName, uploadInsur.FileContent));
        }

        if (uploadPollution.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadPollution.FileName, uploadPollution.FileContent));
        }

        if (uploadFitness.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadFitness.FileName, uploadFitness.FileContent));
        }
        if (uploadSpeed.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadSpeed.FileName, uploadSpeed.FileContent));
        }
        if (uploadSCST.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadSCST.FileName, uploadSCST.FileContent));
        }
        if (uploadEXArmy.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadEXArmy.FileName, uploadEXArmy.FileContent));
        }
        if (uploadIdentityProof.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadIdentityProof.FileName, uploadIdentityProof.FileContent));
        }
        if (uploadQualification.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadQualification.FileName, uploadQualification.FileContent));
        }
        if (uploadHML.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadHML.FileName, uploadHML.FileContent));
        }
        if (uploadHandicaped.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadHandicaped.FileName, uploadHandicaped.FileContent));
        }
        if (uploadPermit.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadPermit.FileName, uploadPermit.FileContent));
        }
        if (uploadDeathCertificate.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadDeathCertificate.FileName, uploadDeathCertificate.FileContent));
        }
        if (uploadAffidavit.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadAffidavit.FileName, uploadAffidavit.FileContent));
        }
        if (uploadJhtTaxToken.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadJhtTaxToken.FileName, uploadJhtTaxToken.FileContent));
        }

        return resulArray;

    }
    private void uploadFile(int appId, SqlCommand sCmd)
    {
        string Text;

        DateTime dt = new DateTime();


        string uniqueFileNamPrfix = "";

        bool str = uploadRC.HasFile;
        if (uploadRC.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_RC_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);

            string result = uploadProceudureCall(Convert.ToString(appId), "RC", filename, sCmd);
            if (result == "Done")
                uploadRC.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_RC_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadRC.FileName));

            Text = filename;




        }
        if (uploadSpeed.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_SPEED_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadSpeed.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "SPEED", filename, sCmd);
            if (result == "Done")
                uploadSpeed.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_SPEED_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }
        if (uploadInsur.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_INSURANCE_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadInsur.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "INSURANCE", filename, sCmd);
            if (result == "Done")
                uploadInsur.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_INSURANCE_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadInsur.FileName));

            Text = filename;




        }

        if (uploadPollution.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_POLLUTION_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPollution.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "POLLUTION", filename, sCmd);
            if (result == "Done")
                uploadPollution.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_POLLUTION_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPollution.FileName));

            Text = filename;




        }

        if (uploadFitness.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_FITNESS_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadFitness.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "FITNESS", filename, sCmd);
            if (result == "Done")
                uploadFitness.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_FITNESS_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadFitness.FileName));

            Text = filename;





        }
        if (uploadSCST.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_SCST_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadSCST.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "SCST", filename, sCmd);
            if (result == "Done")
                uploadSCST.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_SCST_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSCST.FileName));

            Text = filename;



        }
        if (uploadEXArmy.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_EXARMY_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadEXArmy.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "EXARMY", filename, sCmd);
            if (result == "Done")
                uploadEXArmy.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_EXARMY_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadEXArmy.FileName));

            Text = filename;




        }
        if (uploadIdentityProof.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_IDPROOF_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadIdentityProof.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "IDPROOF", filename, sCmd);
            uploadIdentityProof.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_IDPROOF_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadIdentityProof.FileName));

            Text = filename;




        }
        if (uploadQualification.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_QUALIFICATION_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadQualification.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "QUALIFICATION", filename, sCmd);
            if (result == "Done")
                uploadQualification.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_QUALIFICATION_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadQualification.FileName));

            Text = filename;



        }
        if (uploadHML.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_HML_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadHML.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "HML", filename, sCmd);
            if (result == "Done")
                uploadHML.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_HML_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadHML.FileName));

            Text = filename;




        }
        if (uploadHandicaped.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_HANDICAPPED_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadHandicaped.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "HANDICAPPED", filename, sCmd);
            if (result == "Done")
                uploadHandicaped.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_HANDICAPPED_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadHandicaped.FileName));

            Text = filename;



        }
        if (uploadPermit.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_PERMIT_PERMIT_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPermit.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "PERMIT", filename, sCmd);
            if (result == "Done")
                uploadPermit.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_PERMIT_PERMIT_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPermit.FileName));

            Text = filename;




        }
        if (uploadDeathCertificate.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_DEATH_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadDeathCertificate.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "DEATH", filename, sCmd);
            if (result == "Done")
                uploadDeathCertificate.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_DEATH_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadDeathCertificate.FileName));

            Text = filename;




        }
        if (uploadAffidavit.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_AFFIDAVIT_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadAffidavit.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "AFFIDAVIT", filename, sCmd);
            if (result == "Done")
                uploadAffidavit.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_AFFIDAVIT_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadAffidavit.FileName));

            Text = filename;




        }

        if (uploadJhtTaxToken.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "TRANSFER_JHT_TAX_TOKEN_" + appId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadJhtTaxToken.FileName);
            string result = uploadProceudureCall(Convert.ToString(appId), "JHT_TAX_TOKEN", filename, sCmd);
            if (result == "Done")
                uploadJhtTaxToken.SaveAs(@"\\10.30.9.199\d\JHT\STAUploadedFiles\" + "TRANSFER_JHT_TAX_TOKEN_" + appId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadJhtTaxToken.FileName));

            Text = filename;




        }

    }
    private string uploadProceudureCall(string appId, string uploadHeads, string fileName, SqlCommand sCmd)
    {
        sCmd.CommandText = "STA_TRANSFER_PERMIT_APPLICATION_UPLOAD_AED";
        sCmd.CommandType = CommandType.StoredProcedure;
        sCmd.Parameters.Clear();
        sCmd.Parameters.AddWithValue("@APP_ID", appId);
        sCmd.Parameters.AddWithValue("@UPLOAD_HEADS", uploadHeads);
        sCmd.Parameters.AddWithValue("@FILE_NAME", fileName);
        sCmd.ExecuteNonQuery();
        return "Done";


    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Message.Text = "";
        txtExistRegNo.Text = "";
        txtExistPermitNo.Text = "";
        Message.Visible = false;
        txtExistRegNo.Enabled = true;
        txtExistPermitNo.Enabled = true;
        btnGO.Enabled = true;
        Panel1.Visible = false;
        DivMobileValidate.Visible = false;
        DivOTPMatch.Visible = false;
        txtMobileNumber.Value = "";
        txtOTP.Value = "";
        txtOTP.Disabled = false;
        btnVerify.Enabled = true;
        txtMobileNumber.Disabled = false;
        btnOTP.Enabled = true;

        txtVehicleNumber.Text = "";
        lblMSg.Text = "";
        lblMSg.Visible = false;
        Panel2.Visible = false;
    }
}