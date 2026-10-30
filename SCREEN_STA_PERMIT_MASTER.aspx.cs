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
using System.Globalization;
using System.Collections;
using System.IO;



public partial class SCREEN_STA_PERMIT_MASTER : System.Web.UI.Page
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
            || UserRole == null || UserRole == string.Empty || UserRole == "" || UserType != "STA" || UserRole != "Super Admin")
        {
            Response.Redirect("Login.aspx");
            return;
        }
        if (!IsPostBack)
        {

            Page.Title = "STA PERMIT ENTRY FORM";
            PageHeading.InnerText = "STA PERMIT ENTRY FORM";
            PageHeading1.InnerText = "STA PERMIT ENTRY FORM";


            Bind_ddlDesState();

                
         
        }
        
       
    }
   
    public void Bind_ddlDesState()
    {

        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");
        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters);
        
        ddlDesState.DataSource = dr;
        ddlDesState.Items.Clear();
        ddlDesState.Items.Add(new ListItem("--Please select state--", "0"));
        ddlDesState.DataTextField = "STATE_NAME";
        ddlDesState.DataValueField = "STATE_CODE";
        ddlDesState.DataBind();
        ddlDesState.SelectedValue = "0";


    }
    protected void ddlDesState_SelectedIndexChanged(object sender, EventArgs e)
    {

        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('show');", true);
        if (ddlDesState.SelectedValue == "0")
        {
            Message.Visible = false;
            RouteDiv.Visible = false;
        }
        else
        {
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@STATE_CODE", ddlDesState.SelectedValue);
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_OPENING_LIST_BYID_LEGACY");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    StringBuilder sb = new StringBuilder();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + Convert.ToString(dr["S_NO"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["ROUTE_NAME"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["SANCTIONED_PERMIT"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["ISSUED_PERMIT_PER"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["ISSUED_PERMIT_TEMP"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["BALANCED_PERMIT"]) + "</td>");


                            if (Convert.ToString(dr["SELECT_ROUTE_FLG"]) == "Y")
                            {

                                sb.Append("<td><label><input type='radio' name='route' id='RadioButton_" + Convert.ToString(dr["ROUTE_ID"]) + "' onclick=\"javascript:fnShowRoute('" + CryptorEngine.Encrypt("ROUTE_ID=" + Convert.ToString(dr["ROUTE_ID"]), true) + "')\"/></label></td>");

                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                            sb.Append("</tr>");
                        }
                        Int32 x = Convert.ToInt32(DateTime.Now.Year);
                        Int32 y = x - 18;
                        Int16 month = Convert.ToInt16(DateTime.Now.Month);

                        String startMonth = Convert.ToString(month) + "/" + Convert.ToString(y);
                        String endMonth = Convert.ToString(month - 1) + "/" + Convert.ToString(x);

                        FillAPPlyRouteList.InnerHtml = sb.ToString();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');TableDatatablesButtons.init();$('input[id*=VManufacturingON]').Monthpicker({minYear:" + y + ",maxYear:" + x + ", minValue: '" + startMonth + "',maxValue: '" + endMonth + "'});", true);
                        RouteDiv.Visible = true;
                        Message.Visible = false;

                    }
                    else
                    {
                        FillAPPlyRouteList.InnerHtml = "";
                        RouteDiv.Visible = false;
                        Message.Visible = true;
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

  

    protected void btnSave_Click(object sender, EventArgs e)
    {
        String input = "";
        input = txtroutjson.Value;

        string permittype = ddlPermitType.SelectedValue;
        string permitNo = txtpermitNo.Text;
        string permitValidFrom = txtpermitFrom.Value;
        string permitValidUpto = txtpermitTo.Value;

        DateTime? permitValidFromDate;
        DateTime? permitValidUptoDate;

        if(!string.IsNullOrEmpty(permitValidFrom))
        {
           permitValidFromDate = DateTime.ParseExact(permitValidFrom, "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {

           permitValidFromDate = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        
        if(!string.IsNullOrEmpty(permitValidUpto))
        {

            permitValidUptoDate = DateTime.ParseExact(permitValidUpto, "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            permitValidUptoDate = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
        }

        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;

        PERMIT_APPLICATION_LDR rDTO = new JavaScriptSerializer().Deserialize<PERMIT_APPLICATION_LDR>(input);
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
               string ROUTE_ID =  Convert.ToString(CryptorEngine.Decrypt(hdnID.Value, true)).Split('=')[1];

               sCmd.CommandText = "SCREEN_STA_PERMIT_APPLICATION_MASTER_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", permittype);

                if (permittype == "1")
                {
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID","1");
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", "1");
                }
                else
                {
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", "8");
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", "3");
                }
                //@VALID_FROM,@VALID_UP_TO
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", permitNo);
                sCmd.Parameters.AddWithValue("@VALID_FROM", permitValidFromDate);
                sCmd.Parameters.AddWithValue("@VALID_UP_TO", permitValidUptoDate);
                    
                sCmd.Parameters.AddWithValue("@ROUTE_ID", ROUTE_ID);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text.ToString().Trim());
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
                    Tax = DateTime.ParseExact(VTax.Value, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    Tax = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }
                if (!String.IsNullOrEmpty(VFitness.Value))
                {
                    Fitness = DateTime.ParseExact(VFitness.Value, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    Fitness = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }
                if (!String.IsNullOrEmpty(VManufacturingON.Value))
                {
                    VManufacturingON.Value = "01/" + VManufacturingON.Value;

                    ManufacturingON = DateTime.ParseExact(VManufacturingON.Value, "d/M/yyyy", CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);

                    //ManufacturingON = DateTime.ParseExact(VManufacturingON.Value, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
                }
                else
                {


                    ManufacturingON = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }

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


                sCmd.Parameters.AddWithValue("@NO_OF_TRIPS", DBNull.Value);
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", Fitness);
                sCmd.Parameters.AddWithValue("@IDENTITY_PROOF_NO", txtIdentityNo.Text.ToString());


              //sCmd.Parameters.AddWithValue("@FEE", lblPermitFees.Text);
                sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", DBNull.Value);
                sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXIST");

                var Cnt = sCmd.ExecuteScalar();
                if (Convert.ToInt32(Cnt) == 0)
                {

                    sCmd.Parameters["@MODE"].Value= "HDR_ADD";
              
                    var obj = sCmd.ExecuteScalar();
                    Int32 PermitId;
                    PermitId = Convert.ToInt32(obj);
                    foreach (PERMIT_APPLICATION_LDR dr in rDTO.PERMIT_APPLICATION_LDRs)
                    {
                        sCmd.CommandText = "SCREEN_STA_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();

                        sCmd.Parameters.AddWithValue("@APP_ID", PermitId);
                        sCmd.Parameters.AddWithValue("@SEQUENCE", dr.ROUTE_DETAIL_ID);
                        sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                        sCmd.Parameters.AddWithValue("@ARRIVAL_TIMING", dr.ARRIVAL_TIMING.ToString());
                        sCmd.Parameters.AddWithValue("@DEPARTURE_TIMING", dr.DEPARTURE_TIMING.ToString());
                        sCmd.Parameters.AddWithValue("@MODE", dr.MODE);
                        sCmd.Parameters.AddWithValue("@ID", dr.ID);
                        sCmd.Parameters.AddWithValue("@OP_MODE", "LDR_ADD");
                        sCmd.ExecuteNonQuery();

                    }

                    sCmd.CommandText = "SCREEN_STA_PERMIT_APPLICATION_MASTER_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@APP_ID", PermitId);
                    sCmd.Parameters.AddWithValue("@MODE","MSTR_UPD");
                   sCmd.ExecuteNonQuery();
                  
                    objTrans.Commit();
                    // Response.Redirect("STA_INTER_STATE_PERMIT_RECEIPT.aspx?q="+str);
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully inserted');window.location ='STA-DASHBOARD.aspx'", true);
                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Registration or Permit Number is already exist');window.location ='STA-DASHBOARD.aspx'", true);
                }

                


            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-DASHBOARD.aspx'", true);
                // Response.Redirect("PermitHomePage.aspx");
            }
        }
    }
   
   



    protected void btnReset_Click(object sender, EventArgs e)
    {


    }
}