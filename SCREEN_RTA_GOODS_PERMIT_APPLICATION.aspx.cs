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
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;
using System.Collections;



public partial class SCREEN_RTA_GOODS_PERMIT_APPLICATION : System.Web.UI.Page
{
  
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        txtMobileNo.MaxLength = 10;
        txtVehicleNumber.MaxLength = 12;

        if (!IsPostBack)
        {


            Page.Title = "RTA GOODS PERMIT APPLICATION ";
            PageHeading.InnerText = "RTA GOODS PERMIT APPLICATION ";
            PageHeading1.InnerText = "RTA GOODS PERMIT APPLICATION ";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RTA-DASHBOARD.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMSg.Text = "";
        Panel1.Visible = false;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        String regNo1 = txtVehicleNumber.Text.Replace(" ", "").Trim().ToUpper().ToString();
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {
                txtRates.Text = "0.0";
                VNatureofGoods.Text = "Unrestricted General Goods";
                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "SP_CHECK_REGISTRATION_NUMBER";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", regNo1);
                sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXISTS");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);
                if (Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) == "FAIL")
                {

                    lblMSg.Text = ds.Tables[0].Rows[0]["MSG"].ToString();
                    lblMSg.Visible = true;
                    Panel1.Visible = false;
                }

                else if (Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) == "SUCCESS")
                {
                    lblMSg.Text = ds.Tables[0].Rows[0]["MSG"].ToString();
                    lblMSg.Visible = true;

                    Int32 x = Convert.ToInt32(DateTime.Now.Year);
                    Int32 y = x - 12;
                    Int16 month = Convert.ToInt16(DateTime.Now.Month);

                    String startMonth = Convert.ToString(month) + "/" + Convert.ToString(y);
                    String endMonth = Convert.ToString(month - 1) + "/" + Convert.ToString(x);

                    VahanService myclass = new VahanService();
                    myclass = VahanResponse.GetVahanResponse(regNo1);

                    if (myclass != null)
                    {
                        if (myclass.status == "OK" && myclass.rc_catg == "TRANSPORT")
                        {
                            VORegNumber.Text = regNo1;

                            if (myclass.rc_owner_name == "" || myclass.rc_owner_name == "NA" || myclass.rc_owner_name == "N/A" || myclass.rc_owner_name == "Not Available" || myclass.rc_owner_name == null)
                            {
                                VOName.ReadOnly = false;
                            }
                            else
                            {
                                VOName.Text = myclass.rc_owner_name;
                                VOName.ReadOnly = true;
                            }

                            if (myclass.rc_f_name == "" || myclass.rc_f_name == "NA" || myclass.rc_f_name == "N/A" || myclass.rc_f_name == "Not Available" || myclass.rc_f_name == null)
                            {
                                VOCareOf.ReadOnly = false;
                            }
                            else
                            {

                                VOCareOf.ReadOnly = true;
                                VOCareOf.Text = myclass.rc_f_name;
                            }
                            if (myclass.rc_present_address == "" || myclass.rc_present_address == "NA" || myclass.rc_present_address == "N/A" || myclass.rc_present_address == "Not Available" || myclass.rc_present_address == null)
                            {
                                VOPresentAddress.ReadOnly = false;
                            }
                            else
                            {
                                VOPresentAddress.ReadOnly = true;
                                VOPresentAddress.Text = myclass.rc_present_address;
                            }

                            if (myclass.rc_permanent_address == "" || myclass.rc_permanent_address == "NA" || myclass.rc_permanent_address == "N/A" || myclass.rc_permanent_address == "Not Available" || myclass.rc_permanent_address == null)
                            {
                                VOPermanentAddress.ReadOnly = false;

                            }
                            else
                            {
                                VOPermanentAddress.ReadOnly = true;
                                VOPermanentAddress.Text = myclass.rc_permanent_address;
                            }

                            if (myclass.rc_permanent_address == "" || myclass.rc_permanent_address == "NA" || myclass.rc_permanent_address == "N/A" || myclass.rc_permanent_address == "Not Available" || myclass.rc_permanent_address == null)
                            {
                                VOPermanentAddress.ReadOnly = false;
                            }
                            else
                            {
                                VOPermanentAddress.ReadOnly = true;
                                VOPermanentAddress.Text = myclass.rc_permanent_address;
                            }

                            if (myclass.rc_chasi_no == "" || myclass.rc_chasi_no == "NA" || myclass.rc_chasi_no == "N/A" || myclass.rc_chasi_no == "Not Available" || myclass.rc_chasi_no == null)
                            {
                                VChassisNumber.ReadOnly = false;
                            }
                            else
                            {
                                VChassisNumber.ReadOnly = true;
                                VChassisNumber.Text = myclass.rc_chasi_no;
                            }
                            if (myclass.rc_eng_no == "" || myclass.rc_eng_no == "NA" || myclass.rc_eng_no == "N/A" || myclass.rc_eng_no == "Not Available" || myclass.rc_eng_no == null)
                            {
                                VEngineNumber.ReadOnly = false;
                            }
                            else
                            {
                                VEngineNumber.ReadOnly = true;
                                VEngineNumber.Text = myclass.rc_eng_no;
                            }

                            if (myclass.rc_maker_desc == "" || myclass.rc_maker_desc == "NA" || myclass.rc_maker_desc == "N/A" || myclass.rc_maker_desc == "Not Available" || myclass.rc_maker_desc == null)
                            {
                                VMakerDesc.ReadOnly = false;
                            }
                            else
                            {
                                VMakerDesc.ReadOnly = true;
                                VMakerDesc.Text = myclass.rc_maker_desc;
                            }

                            if (myclass.rc_maker_model == "" || myclass.rc_maker_model == "NA" || myclass.rc_maker_model == "N/A" || myclass.rc_maker_model == "Not Available" || myclass.rc_maker_model == null)
                            {
                                VMakerDesc.ReadOnly = false;
                            }
                            else
                            {
                                VMakerModel.ReadOnly = true;
                                VMakerModel.Text = myclass.rc_maker_model;
                            }

                            if (myclass.rc_fuel_desc == "" || myclass.rc_fuel_desc == "NA" || myclass.rc_fuel_desc == "N/A" || myclass.rc_fuel_desc == "Not Available" || myclass.rc_fuel_desc == null)
                            {
                                VFuelDesc.ReadOnly = false;
                            }
                            else
                            {
                                VFuelDesc.ReadOnly = true;
                                VFuelDesc.Text = myclass.rc_fuel_desc;
                            }

                            if (myclass.rc_color == "" || myclass.rc_color == "NA" || myclass.rc_color == "N/A" || myclass.rc_color == "Not Available" || myclass.rc_color == null)
                            {
                                VColor.ReadOnly = false;
                            }
                            else
                            {
                                VColor.ReadOnly = true;
                                VColor.Text = myclass.rc_color;
                            }


                            
                            if (myclass.rc_tax_upto == "" || myclass.rc_tax_upto == "NA" || myclass.rc_tax_upto == "N/A" || myclass.rc_tax_upto == "Not Available" || myclass.rc_tax_upto == null)
                            {
                                btn2.Disabled = false;

                            }
                            else
                            {
                                btn2.Disabled = true;
                                VTax.Value = myclass.rc_tax_upto;
                            }
                            if (myclass.rc_fit_upto == "" || myclass.rc_fit_upto == "NA" || myclass.rc_fit_upto == "N/A" || myclass.rc_fit_upto == "Not Available" || myclass.rc_fit_upto == null)
                            {
                                btn1.Disabled = false;
                            }
                            else
                            {
                                btn1.Disabled = true;
                                VFitness.Value = myclass.rc_fit_upto;
                                txtFCValidUpto.Value = myclass.rc_fit_upto;
                            }

                            if (myclass.rc_vh_class_desc == "" || myclass.rc_vh_class_desc == "NA" || myclass.rc_vh_class_desc == "N/A" || myclass.rc_vh_class_desc == "Not Available" || myclass.rc_vh_class_desc == null)
                            {
                                VType.ReadOnly = false;

                            }
                            else
                            {
                                VType.ReadOnly = true;
                                VType.Text = myclass.rc_vh_class_desc;
                            }

                            if (myclass.rc_ulw == "" || myclass.rc_ulw == "NA" || myclass.rc_ulw == "N/A" || myclass.rc_ulw == "Not Available" || myclass.rc_ulw == null)
                            {
                                VULW.ReadOnly = false;

                            }
                            else
                            {
                                VULW.ReadOnly = true;
                                VULW.Text = myclass.rc_ulw;
                            }
                            if (myclass.rc_rlw == "" || myclass.rc_rlw == "NA" || myclass.rc_rlw == "N/A" || myclass.rc_rlw == "Not Available" || myclass.rc_rlw == null)
                            {
                                VRLW.ReadOnly = false;

                            }
                            else
                            {
                                VRLW.ReadOnly = true;
                                VRLW.Text = myclass.rc_rlw;
                            }

                            if(myclass.rc_rlw != "")
                            {
                                txtVCapacity.Text = Convert.ToString(Convert.ToInt32(myclass.rc_rlw) - Convert.ToInt32(myclass.rc_ulw));
                            }


                            if (myclass.rc_manufacturing_on == "" || myclass.rc_manufacturing_on == "NA" || myclass.rc_manufacturing_on == "N/A" || myclass.rc_manufacturing_on == "Not Available" || myclass.rc_manufacturing_on == null)
                            {
                                VManufacturingON.Disabled = false;
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('input[id*=VManufacturingON]').Monthpicker({minYear:" + y + ",maxYear:" + x + ", minValue: '" + startMonth + "',maxValue: '" + endMonth + "'});", true);

                            }
                            else
                            {
                                MFSERVER.Value = "YES";
                                VManufacturingON.Disabled = true;
                                VManufacturingON.Value = myclass.rc_manufacturing_on;
                            }
                            HDRegisteredat.Value = myclass.rc_registered_at;

                        }
                        else if (myclass.status == "OK" && myclass.rc_catg == "NON TRANSPORT")
                        {

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your Vehicle is under non transport section so you can not apply for permit.');location.reload();", true);
                            return;
                        }
                        else
                        {
                            VORegNumber.Text = regNo1;
                            VOName.ReadOnly = false;
                            VOCareOf.ReadOnly = false;
                            VOPresentAddress.ReadOnly = false;
                            VOPermanentAddress.ReadOnly = false;
                            VChassisNumber.ReadOnly = false;
                            VEngineNumber.ReadOnly = false;
                            VMakerDesc.ReadOnly = false;
                            VMakerModel.ReadOnly = false;
                            VFuelDesc.ReadOnly = false;
                            VColor.ReadOnly = false;
                            VNatureofGoods.ReadOnly = false;
                            

                            VTax.Disabled = false;
                            VFitness.Disabled = false;
                            VManufacturingON.Disabled = false;
                            btn1.Disabled = false;
                            btn2.Disabled = false;
                            //   btn3.Disabled = false;

                            VType.ReadOnly = false;
                            VULW.ReadOnly = false;
                            VRLW.ReadOnly = false;
                            HDRegisteredat.Value = "";
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('input[id*=VManufacturingON]').Monthpicker({minYear:" + y + ",maxYear:" + x + ", minValue: '" + startMonth + "',maxValue: '" + endMonth + "'});", true);
                        }
                    }
                    else
                    {
                        VORegNumber.Text = regNo1;
                        VOName.ReadOnly = false;
                        VOCareOf.ReadOnly = false;
                        VOPresentAddress.ReadOnly = false;
                        VOPermanentAddress.ReadOnly = false;
                        VChassisNumber.ReadOnly = false;
                        VEngineNumber.ReadOnly = false;
                        VMakerDesc.ReadOnly = false;
                        VMakerModel.ReadOnly = false;
                        VFuelDesc.ReadOnly = false;
                        VColor.ReadOnly = false;
                        VNatureofGoods.ReadOnly = false;
                        VTax.Disabled = false;
                        VFitness.Disabled = false;
                        VManufacturingON.Disabled = false;
                        btn1.Disabled = false;
                        btn2.Disabled = false;
                        //  btn3.Disabled = false;

                        VType.ReadOnly = false;
                        VULW.ReadOnly = false;
                        VRLW.ReadOnly = false;
                        HDRegisteredat.Value = "";
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('input[id*=VManufacturingON]').Monthpicker({minYear:" + y + ",maxYear:" + x + ", minValue: '" + startMonth + "',maxValue: '" + endMonth + "'});", true);
                    }

                    string permittypeid = string.Empty;
                    string permittypeoptionserviceId = string.Empty;
                    if(ddlPermitType.SelectedValue == "2")
                    {
                        permittypeid     = "3";
                        permittypeoptionserviceId = "29";
                    }
                    else
                    {
                        permittypeid = "4";
                        permittypeoptionserviceId = "41";

                    }


                    sCmd.CommandText = "RTA_GOODS_PERMIT_APPLY_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", permittypeid);
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", permittypeoptionserviceId);
                    sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", regNo1);
                    sCmd.Parameters.AddWithValue("@MODE", "RTO_AND_FEE_LIST");
                    DataSet ds1 = new DataSet();
                    SqlDataAdapter da1 = new SqlDataAdapter(sCmd);
                    da1.Fill(ds1);


                    DataTable dt1 = ds1.Tables[0]; //RTO CODE & RTO NAME
                    DataTable dt2 = ds1.Tables[1]; // To Apply For (RTO) 
                    DataTable dt3 = ds1.Tables[2]; // another state
                    DataTable dt4 = ds1.Tables[3]; // Fees 
                    StringBuilder sb = new StringBuilder();
                    if (dt1.Rows.Count > 0)
                    {
                        RTOCode.Value = dt1.Rows[0]["RTO_CODE"].ToString();


                    }
                    if (dt2.Rows.Count > 0)
                    {
                        sb.Append("<div class='input-group'>");
                        sb.Append("<div class='icheck-list'>");
                        foreach (DataRow dr in dt2.Rows)
                        {

                            sb.Append("<div class='col-md-12 col-sm-12'>");
                            sb.Append("<input type='radio' value='" + Convert.ToString(dr["RTO_NAME"]) + "&" + Convert.ToString(dr["RTO_CODE"]) + "' class='rtoHdr' name ='otherrto' id='RadioButton_" + Convert.ToString(dr["RTO_CODE"]) + "'");
                            sb.Append("onclick=\"javascript:otherRTOCode('" + CryptorEngine.Encrypt("RTO_CODE=" + Convert.ToString(dr["RTO_CODE"]), true) + "')\" >    " + Convert.ToString(dr["RTO_NAME"]).ToUpper() + "</div>");
                        }
                        sb.Append("</div>");
                        sb.Append("</div>");
                        RTORouteDiv.InnerHtml = sb.ToString();

                    }
                    if (dt3.Rows.Count > 0)
                    {
                        StringBuilder sbstate = new StringBuilder();


                        sbstate.Append("<div class='input-group'>");
                        sbstate.Append("<div class='icheck-list'>");
                        if (ddlPermitType.SelectedValue =="2")   // For Temporary
                        {

                            foreach (DataRow dr in dt3.Rows)
                            {

                                sbstate.Append("<div class='col-md-12 col-sm-12'>");
                                sbstate.Append("<input type='checkbox' value='" + Convert.ToString(dr["STATE_CODE"]) + "&" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]) + "'  id='STATE_" + Convert.ToString(dr["STATE_CODE"]) + "' class = 'StateHDR'");
                                sbstate.Append("onclick=\"javascript:otherSate('" + CryptorEngine.Encrypt("STATE_CODE=" + Convert.ToString(dr["STATE_CODE"]) + "& VALIDITY=" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]), true) + "')\" >    " + Convert.ToString(dr["STATE_NAME"]).ToUpper() + " <b> - (PERMIT VALID FOR " + dr["PERMIT_VALIDITY_IN_MONTH"] + "  MONTHS) </b>" + "</div>");
                            }
                        }
                        else
                        {
                            foreach (DataRow dr in dt3.Rows)
                            {

                                sbstate.Append("<div class='col-md-12 col-sm-12'>");
                                sbstate.Append("<input disabled='disabled' type='checkbox' value='" + Convert.ToString(dr["STATE_CODE"]) + "&" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]) + "'  id='STATE_" + Convert.ToString(dr["STATE_CODE"]) + "' class = 'StateHDR'");
                                sbstate.Append("onclick=\"javascript:otherSate('" + CryptorEngine.Encrypt("STATE_CODE=" + Convert.ToString(dr["STATE_CODE"]) + "& VALIDITY=" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]), true) + "')\" >    " + Convert.ToString(dr["STATE_NAME"]).ToUpper() + " <b> - (PERMIT VALID FOR " + dr["PERMIT_VALIDITY_IN_MONTH"] + "  MONTHS) </b>" + "</div>");
                            }
                        }

                        sbstate.Append("</div>");
                        sbstate.Append("</div>");
                        otherStateDiv.InnerHtml = sbstate.ToString();



                    }

                    
                    Panel1.Visible = true;



                }
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');ComponentsDateTimePickers.init();", true);
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-DASHBOARD.aspx'", true);

            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;
        string permittypeid = string.Empty;
        string permittypeoptionserviceId = string.Empty;
        if (ddlPermitType.SelectedValue == "2")
        {
            permittypeid = "3";
            permittypeoptionserviceId = "29";
        }
        else
        {
            permittypeid = "4";
            permittypeoptionserviceId = "41";

        }
        
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
               
                
                //Int32 ROUTE_ID = Convert.ToInt32(hdnID.Value);
                sCmd.CommandText = "SCREEN_RTA_GOODS_PERMIT_APPLICATION_MASTER_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", permittypeid);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", Convert.ToInt32(permittypeoptionserviceId));
                sCmd.Parameters.AddWithValue("@RTO_CODE", RTOCode.Value);
                string selectedGender, wholestate, permitSelf; 
                if (txtMale.Checked)
                {
                    selectedGender = txtMale.Value;
                }
                else
                {
                    selectedGender = txtFemale.Value;
                }

                if (WholeStateCheckbox.Checked)
                {
                    wholestate = "YES";
                }
                else
                {
                    wholestate = "NO";
                }
                if (SelfCheckBox.Checked)
                {
                    permitSelf = "YES";
                }
                else
                {
                    permitSelf = "NO";
                }
                string OtherRTOCode = "";
                if(otherRTOCode.Value != "")
                {
                    OtherRTOCode = CryptorEngine.Decrypt(otherRTOCode.Value, true).ToString().Substring(9);
                }
                else
                {
                     OtherRTOCode = "NO";
                }

                int validity = 0;
                string FirstStatecode = "NO";
                string SecondStatecode = "NO";
                if (hdnStateCode.Value != "S" && hdnStateCode.Value != "")
                {
                    string[] stateCodevalidoty = hdnStateCode.Value.Split('-');
                    string finalstring = stateCodevalidoty[0].Substring(1);
                    FirstStatecode = finalstring.Split('&')[0];
                    SecondStatecode = stateCodevalidoty[1].Split('&')[0];
                    if(SecondStatecode=="")
                    {
                        SecondStatecode = "NO";
                        int num1 = Convert.ToInt32(finalstring.Split('&')[1]);
                        validity = num1;
   
                        
                    }
                    else
                    {
                        int num1 = Convert.ToInt32(finalstring.Split('&')[1]);
                        int num2 = Convert.ToInt32(stateCodevalidoty[1].Split('&')[1]);
                        validity = num1 < num2 ? num1 : num2;

                        
                    }

                    
                }

                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtpermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_SELF", permitSelf);
                sCmd.Parameters.AddWithValue("@PERMIT_ANOTHER_RTO_CODE", OtherRTOCode);
                sCmd.Parameters.AddWithValue("@PERMIT_WHOLE_JHARKHAND", wholestate);
                sCmd.Parameters.AddWithValue("@PERMIT_ANOTHER_STATE_CODE_FIRST", FirstStatecode);
                sCmd.Parameters.AddWithValue("@PERMIT_ANOTHER_STATE_CODE_SECOND", SecondStatecode);
                sCmd.Parameters.AddWithValue("@PERMIT_VALIDITY_IN_MONTH", validity);
                sCmd.Parameters.AddWithValue("@NATURE_OF_GOODS", VNatureofGoods.Text);
                sCmd.Parameters.AddWithValue("@RATES_OF_CHARGES", Convert.ToDecimal(txtRates.Text));
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
              
                
               
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
                ViewState["manufactDate"] = VManufacturingON.Value;
                if (!String.IsNullOrEmpty(VManufacturingON.Value))
                {
                    VManufacturingON.Value = "01/" + VManufacturingON.Value;

                    ManufacturingON = DateTime.ParseExact(VManufacturingON.Value, "d/M/yyyy", CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);
                    
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
                sCmd.Parameters.AddWithValue("@ULW", Convert.ToInt32(VULW.Text));
                sCmd.Parameters.AddWithValue("@RLW", Convert.ToInt32(VRLW.Text));
                sCmd.Parameters.AddWithValue("@VEHICLE_CAPACITY", Convert.ToInt32(txtVCapacity.Text));
                sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", ManufacturingON);
                 
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
                sCmd.Parameters.AddWithValue("@GENDER", selectedGender);
             
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));

                sCmd.Parameters.AddWithValue("@VALID_FROM", DateTime.ParseExact(txtpermitFrom.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@VALID_UP_TO", DateTime.ParseExact(txtpermitTo.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXIST");

                var Cnt = sCmd.ExecuteScalar();
                if (Convert.ToInt32(Cnt) == 0)
                {
                    sCmd.Parameters["@MODE"].Value = "HDR_ADD";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    objTrans.Commit();
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully saved');window.location ='SCREEN_RTA_GOODS_PERMIT_APPLICATION.aspx'", true);

                }
                else
                {
                    objTrans.Rollback();
                    String Message = "Registration number or Permit Number is already exists.";
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    VManufacturingON.Value = Convert.ToString(ViewState["manufactDate"]);
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
                }

            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='SCREEN_RTA_GOODS_PERMIT_APPLICATION.aspx'", true);
                // Response.Redirect("PermitHomePage.aspx");
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {


        txtVehicleNumber.Text = "";
        VORegNumber.Text = "";
        lblMSg.Text = "";
        Panel1.Visible = false;
    }
}