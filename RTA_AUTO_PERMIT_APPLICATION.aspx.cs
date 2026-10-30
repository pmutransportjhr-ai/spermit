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
public partial class RTA_AUTO_PERMIT_APPLICATION : System.Web.UI.Page
{
    //  private static string ID;
    // private static string TYPE;
    //private static string TYPE_OPTION_ID; 
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        txtMobileNo.MaxLength = 10;
        txtVehicleNumber.MaxLength = 15;

        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                return;
            }
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
                //QueryString IDx2 = QueryStr.Where(l => l.Name == "TYPE_OPTION_ID").FirstOrDefault();
                hdnID.Value = IDx.Value;
                hdnType.Value = IDx1.Value;
              //  hdnTypeOptionID.Value = IDx2.Value;
                PageTitle PT = new PageTitle();
                DataTable dt1 = new DataTable();
                dt1 = PT.PageRTAHeading(Convert.ToInt32(hdnID.Value), Convert.ToInt32(hdnType.Value));


                if (dt1.Rows.Count > 0)
                {
                    Page.Title = dt1.Rows[0]["RTA_TITLE"].ToString();
                    PageHeading.InnerText = dt1.Rows[0]["RTA_HEADING"].ToString();
                    PageHeading1.InnerText = dt1.Rows[0]["RTA_TITLE"].ToString();

                }

            }

            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid URL');window.location ='PermitHomePage.aspx'", true);

            }

        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtVehicleNumber.Text = "";
        VORegNumber.Text = "";
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


                    //var url = "http://10.92.194.177/Vahan4API/api/get/spermit/nic@123/" + regNo1;
                    //var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
                    //using (var response = webrequest.GetResponse())
                    //using (var reader = new StreamReader(response.GetResponseStream()))
                    //{
                    //    var result = reader.ReadToEnd();
                    //    txtVehicleJson.Value = Convert.ToString(result);
                    //}
                    //String input = "";
                    //input = (txtVehicleJson.Value).Replace("[]", "\"\"");                    
                    //VEHICLE_INFO myclass = new JavaScriptSerializer().Deserialize<VEHICLE_INFO>(input);

                    VahanService myclass = new VahanService();
                    myclass = VahanResponse.GetVahanResponse(regNo1);
                    if (myclass != null)
                    {
                        if (myclass.status == "OK" && myclass.rc_catg == "TRANSPORT")
                        {
                            VORegNumber.Text = regNo1;

                            if (myclass.rc_owner_name == "" || myclass.rc_owner_name == "NA" ||
                                myclass.rc_owner_name == "N/A" || myclass.rc_owner_name == "Not Available"
                                || myclass.rc_owner_name == null)
                            {
                                VOName.ReadOnly = false;
                            }
                            else
                            {
                                VOName.Text = myclass.rc_owner_name;
                                VOName.ReadOnly = true;
                            }

                            if (myclass.rc_f_name == "" || myclass.rc_f_name == "NA" ||
                                myclass.rc_f_name == "N/A" || myclass.rc_f_name == "Not Available"
                                || myclass.rc_f_name == null)
                            {
                                VOCareOf.ReadOnly = false;
                            }
                            else
                            {

                                VOCareOf.ReadOnly = true;
                                VOCareOf.Text = myclass.rc_f_name;
                            }
                            if (myclass.rc_present_address == "" || myclass.rc_present_address == "NA" ||
                                myclass.rc_present_address == "N/A" || myclass.rc_present_address == "Not Available"
                                || myclass.rc_present_address == null)
                            {
                                VOPresentAddress.ReadOnly = false;
                            }
                            else
                            {
                                VOPresentAddress.ReadOnly = true;
                                VOPresentAddress.Text = myclass.rc_present_address;
                            }

                            if (myclass.rc_permanent_address == "" || myclass.rc_permanent_address == "NA" ||
                                myclass.rc_permanent_address == "N/A" || myclass.rc_permanent_address == "Not Available"
                                || myclass.rc_permanent_address == null)
                            {
                                VOPermanentAddress.ReadOnly = false;

                            }
                            else
                            {
                                VOPermanentAddress.ReadOnly = true;
                                VOPermanentAddress.Text = myclass.rc_permanent_address;
                            }

                            if (myclass.rc_permanent_address == "" || myclass.rc_permanent_address == "NA" ||
                                myclass.rc_permanent_address == "N/A" || myclass.rc_permanent_address == "Not Available"
                                || myclass.rc_permanent_address == null)
                            {
                                VOPermanentAddress.ReadOnly = false;
                            }
                            else
                            {
                                VOPermanentAddress.ReadOnly = true;
                                VOPermanentAddress.Text = myclass.rc_permanent_address;
                            }

                            if (myclass.rc_chasi_no == "" || myclass.rc_chasi_no == "NA" ||
                                myclass.rc_chasi_no == "N/A" || myclass.rc_chasi_no == "Not Available"
                                || myclass.rc_chasi_no == null)
                            {
                                VChassisNumber.ReadOnly = false;
                            }
                            else
                            {
                                VChassisNumber.ReadOnly = true;
                                VChassisNumber.Text = myclass.rc_chasi_no;
                            }
                            if (myclass.rc_eng_no == "" || myclass.rc_eng_no == "NA" ||
                                myclass.rc_eng_no == "N/A" || myclass.rc_eng_no == "Not Available"
                                || myclass.rc_eng_no == null)
                            {
                                VEngineNumber.ReadOnly = false;
                            }
                            else
                            {
                                VEngineNumber.ReadOnly = true;
                                VEngineNumber.Text = myclass.rc_eng_no;
                            }

                            if (myclass.rc_maker_desc == "" || myclass.rc_maker_desc == "NA" ||
                                     myclass.rc_maker_desc == "N/A" || myclass.rc_maker_desc == "Not Available"
                                     || myclass.rc_maker_desc == null)
                            {
                                VMakerDesc.ReadOnly = false;
                            }
                            else
                            {
                                VMakerDesc.ReadOnly = true;
                                VMakerDesc.Text = myclass.rc_maker_desc;
                            }

                            if (myclass.rc_maker_model == "" || myclass.rc_maker_model == "NA" ||
                                      myclass.rc_maker_model == "N/A" || myclass.rc_maker_model == "Not Available"
                                      || myclass.rc_maker_model == null)
                            {
                                VMakerDesc.ReadOnly = false;
                            }
                            else
                            {
                                VMakerModel.ReadOnly = true;
                                VMakerModel.Text = myclass.rc_maker_model;
                            }

                            if (myclass.rc_fuel_desc == "" || myclass.rc_fuel_desc == "NA" ||
                                      myclass.rc_fuel_desc == "N/A" || myclass.rc_fuel_desc == "Not Available"
                                      || myclass.rc_fuel_desc == null)
                            {
                                VFuelDesc.ReadOnly = false;
                            }
                            else
                            {
                                VFuelDesc.ReadOnly = true;
                                VFuelDesc.Text = myclass.rc_fuel_desc;
                            }

                            if (myclass.rc_color == "" || myclass.rc_color == "NA" ||
                                       myclass.rc_color == "N/A" || myclass.rc_color == "Not Available"
                                       || myclass.rc_color == null)
                            {
                                VColor.ReadOnly = false;
                            }
                            else
                            {
                                VColor.ReadOnly = true;
                                VColor.Text = myclass.rc_color;
                            }


                            if (myclass.rc_seat_capacity == "" || myclass.rc_seat_capacity == "NA" ||
                                         myclass.rc_seat_capacity == "N/A" || myclass.rc_seat_capacity == "Not Available"
                                         || myclass.rc_seat_capacity == null)
                            {
                                VSeatCapacity.ReadOnly = false;
                            }
                            else
                            {
                                VSeatCapacity.ReadOnly = true;
                                VSeatCapacity.Text = myclass.rc_seat_capacity;
                            }

                            if (myclass.rc_tax_upto == "" || myclass.rc_tax_upto == "NA" ||
                                          myclass.rc_tax_upto == "N/A" || myclass.rc_tax_upto == "Not Available"
                                          || myclass.rc_tax_upto == null)
                            {
                                btn2.Disabled = false;

                            }
                            else
                            {
                                btn2.Disabled = true;
                                VTax.Value = myclass.rc_tax_upto;
                            }
                            if (myclass.rc_fit_upto == "" || myclass.rc_fit_upto == "NA" ||
                                          myclass.rc_fit_upto == "N/A" || myclass.rc_fit_upto == "Not Available"
                                          || myclass.rc_fit_upto == null)
                            {
                                btn1.Disabled = false;
                            }
                            else
                            {
                                btn1.Disabled = true;
                                VFitness.Value = myclass.rc_fit_upto;
                            }

                            if (myclass.rc_vh_class_desc == "" || myclass.rc_vh_class_desc == "NA" ||
                                          myclass.rc_vh_class_desc == "N/A" || myclass.rc_vh_class_desc == "Not Available"
                                          || myclass.rc_vh_class_desc == null)
                            {
                                VType.ReadOnly = false;

                            }
                            else
                            {
                                VType.ReadOnly = true;
                                VType.Text = myclass.rc_vh_class_desc;
                            }

                            if (myclass.rc_ulw == "" || myclass.rc_ulw == "NA" ||
                                         myclass.rc_ulw == "N/A" || myclass.rc_ulw == "Not Available"
                                         || myclass.rc_ulw == null)
                            {
                                VULW.ReadOnly = false;

                            }
                            else
                            {
                                VULW.ReadOnly = true;
                                VULW.Text = myclass.rc_ulw;
                            }
                            if (myclass.rc_rlw == "" || myclass.rc_rlw == "NA" ||
                                         myclass.rc_rlw == "N/A" || myclass.rc_rlw == "Not Available"
                                         || myclass.rc_rlw == null)
                            {
                                VRLW.ReadOnly = false;

                            }
                            else
                            {

                                VRLW.ReadOnly = true;
                                VRLW.Text = myclass.rc_rlw;
                            }
                            if (myclass.rc_manufacturing_on == "" || myclass.rc_manufacturing_on == "NA" ||
                                        myclass.rc_manufacturing_on == "N/A" || myclass.rc_manufacturing_on == "Not Available"
                                        || myclass.rc_manufacturing_on == null)
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
                        {/*
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
                            VSeatCapacity.ReadOnly = false;
                            VTax.Disabled = false;
                            VFitness.Disabled = false;
                            VManufacturingON.Disabled = false;
                            btn1.Disabled = false;
                            btn2.Disabled = false;
                            //btn3.Disabled = false;

                            VType.ReadOnly = false;
                            VULW.ReadOnly = false;
                            VRLW.ReadOnly = false;
                            HDRegisteredat.Value = "";
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('input[id*=VManufacturingON]').Monthpicker({minYear:" + y + ",maxYear:" + x + ", minValue: '" + startMonth + "',maxValue: '" + endMonth + "'});", true);
                                             */
 ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Vahaan Service is not Running. Try Again Later...');location.reload();", true);
                            return;
  }
                        }
                        else
                        {/*
                            VORegNumber.Text = regNo1.ToUpper();
                            VOName.ReadOnly=false;
                            VOCareOf.ReadOnly = false;
                            VOPresentAddress.ReadOnly = false;
                            VOPermanentAddress.ReadOnly = false;
                            VChassisNumber.ReadOnly = false;
                            VEngineNumber.ReadOnly = false;
                            VMakerDesc.ReadOnly = false;
                            VMakerModel.ReadOnly = false;
                            VFuelDesc.ReadOnly = false;
                            VColor.ReadOnly = false;
                            VSeatCapacity.ReadOnly = false;
                            VTax.Disabled = false;
                            VFitness.Disabled = false;
                            VManufacturingON.Disabled = false;
                            btn1.Disabled = false;
                            btn2.Disabled = false;
                            //btn3.Disabled = false;
                           
                            VType.ReadOnly = false;
                            VULW.ReadOnly = false;
                            VRLW.ReadOnly = false;
                            HDRegisteredat.Value = "";
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('input[id*=VManufacturingON]').Monthpicker({minYear:" + y + ",maxYear:" + x + ", minValue: '" + startMonth + "',maxValue: '" + endMonth + "'});", true);
                       */
 ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Vahaan Service is not Running. Try Again Later...');location.reload();", true);
                            return;


 }



                        sCmd.CommandText = "RTA_AUTO_PERMIT_APPLY_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnID.Value);
                        sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnType.Value);
                        sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", regNo1);
                        sCmd.Parameters.AddWithValue("@MODE", "RTO_AND_FEE_LIST");
                        DataSet ds1 = new DataSet();
                        SqlDataAdapter da1 = new SqlDataAdapter(sCmd);
                        da1.Fill(ds1);
                
                    
                            DataTable dt1 = ds1.Tables[0];
                            DataTable dt2 = ds1.Tables[1];
                            StringBuilder sb = new StringBuilder();
                            if (dt1.Rows.Count > 0)
                            {
                                txtRTOCode.Value = Convert.ToString(dt1.Rows[0]["RTO_CODE"]);
                                txtRTOName.Text = Convert.ToString(dt1.Rows[0]["RTO_NAME"]);
                                //RouteHDRInfo.InnerHtml = sb.ToString();

                            }
                            if (dt2.Rows.Count > 0)
                            {
                               
                                lblPermitFees.Text = dt2.Rows[0]["FEE"].ToString();

                            }
                            else
                            {
                                lblPermitFees.Text = "";
                            }

                            Panel1.Visible = true;
                       
                       

                }
             

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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        String input = "";
        input = txtroutjson.Value;
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
                sCmd.CommandText = "RTA_AUTO_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", hdnID.Value);
                sCmd.Parameters.AddWithValue("@RTO_CODE", txtRTOCode.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", hdnType.Value);

                
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
                sCmd.Parameters.AddWithValue("@ST_FROM", txtFrom.Text);
                sCmd.Parameters.AddWithValue("@ST_TO", txtTo.Text);



                String selectedGender = "";
                DateTime? Tax; DateTime? Fitness; DateTime? ManufacturingON;

                if (txtMale.Checked)
                {
                    selectedGender = txtMale.Value;
                }
                else
                {
                    selectedGender = txtFemale.Value;
                }
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

                sCmd.Parameters.AddWithValue("@GENDER", selectedGender);
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
                
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
               
                
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
              //  sCmd.Parameters.AddWithValue("@IDENTITY_PROOF_NO", txtIdentityNo.Text.ToString());
               
                
                sCmd.Parameters.AddWithValue("@FEE", lblPermitFees.Text);

                ArrayList validate = uploadFileValidate();
                string result = string.Empty;
                bool result1 = true;
                foreach (var str in validate)
                {
                    if (str == "OK") { }
                    else
                    {
                        result1 = false;
                        result = result + "  " + str;

                    }

                }
                if (result1 == true)
                {
                    sCmd.Parameters.AddWithValue("@MODE", "ADD");

                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables[0];
                    Int32 PermitId;
                    PermitId = Convert.ToInt32(dt1.Rows[0]["APP_ID"]);

                    uploadFile(PermitId, sCmd);

                    objTrans.Commit();
                    string RegistrationNo = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                    string permitNo = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);
                    string mobileNo = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);
                    string ApplicationNo = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);
                    string permitType = Convert.ToString(dt1.Rows[0]["PERMIT_TYPE"]);
                    string sevicename = Convert.ToString(dt1.Rows[0]["APPLICATION_SERVICE"]);
                    SMSSender objSms = new SMSSender();

                    objSms.ApplicationNumberSms(RegistrationNo, permitNo, sevicename, permitType, mobileNo, ApplicationNo);
                    string str = HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(PermitId), true));
                    // Response.Redirect("STA_INTER_STATE_PERMIT_RECEIPT.aspx?q="+str);
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully apply for Permit');window.location ='RTA_AUTO_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(PermitId) + "&TYPEID=" + Convert.ToString(hdnID.Value) + "&OPTIONID=" + Convert.ToString(hdnType.Value), true), Response.ContentEncoding) + "'", true);

                }
                else
                {
                    objTrans.Rollback();
                    String Message = result;
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
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);
                // Response.Redirect("PermitHomePage.aspx");
            }
        }
    }
    private ArrayList uploadFileValidate()
    {
        ArrayList resulArray = new ArrayList();
        FileUploadValidations filevalidate = new FileUploadValidations();
        
        if (uploadRC.HasFile)
        {
           // Session["uploadRCFile"] = uploadRC;
            resulArray.Add(filevalidate.FileValidate(uploadRC.FileName, uploadRC.FileContent));
        }
        if (uploadInsur.HasFile)
        {
            resulArray.Add(filevalidate.FileValidate(uploadInsur.FileName, uploadInsur.FileContent));
        }

        if (uploadPollution.HasFile)
        {
            resulArray.Add(filevalidate.FileValidate(uploadPollution.FileName, uploadPollution.FileContent));
        }

        if (uploadFitness.HasFile)
        {
            resulArray.Add(filevalidate.FileValidate(uploadFitness.FileName, uploadFitness.FileContent));
        }
        if (uploadSpeed.HasFile)
        {
            resulArray.Add(filevalidate.FileValidate(uploadSpeed.FileName, uploadSpeed.FileContent));
        }
      
        return resulArray;

    }
    private void uploadFile(int permitId, SqlCommand sCmd)
    {
        string Text;

        DateTime dt = new DateTime();


        string uniqueFileNamPrfix = "";

        bool str = uploadRC.HasFile;
       // uploadRC = (FileUpload)Session["uploadRCFile"];

        if (uploadRC.HasFile)
        {
            
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "AUTO_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);

                string result = uploadProceudureCall(Convert.ToString(permitId), "RC", filename, sCmd);
                if (result == "Done")
                    uploadRC.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AUTO_PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadRC.FileName));

                Text = filename;


            

        }

        if (uploadInsur.HasFile)
        {
            
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "AUTO_PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadInsur.FileName);
                string result = uploadProceudureCall(Convert.ToString(permitId), "INSURANCE", filename, sCmd);
                if (result == "Done")
                    uploadInsur.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AUTO_PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadInsur.FileName));

                Text = filename;


            

        }

        if (uploadPollution.HasFile)
        {
           
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "AUTO_PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPollution.FileName);
                string result = uploadProceudureCall(Convert.ToString(permitId), "POLLUTION", filename, sCmd);
                if (result == "Done")
                    uploadPollution.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AUTO_PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPollution.FileName));

                Text = filename;


           

        }

        if (uploadFitness.HasFile)
        {
           
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "AUTO_PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadFitness.FileName);
                string result = uploadProceudureCall(Convert.ToString(permitId), "FITNESS", filename, sCmd);
                if (result == "Done")
                    uploadFitness.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AUTO_PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadFitness.FileName));

                Text = filename;


           

        }
        if (uploadSpeed.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "AUTO_PERMIT_SPEED_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadSpeed.FileName);
            string result = uploadProceudureCall(Convert.ToString(permitId), "SPEED", filename, sCmd);
            if (result == "Done")
                uploadSpeed.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "AUTO_PERMIT_SPEED_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }
      
    }
    private string uploadProceudureCall(string permitId, string uploadHeads, string fileName, SqlCommand sCmd)
    {


        sCmd.CommandText = "RTA_AUTO_PERMIT_APPLICATION_UPLOAD_AED";
        sCmd.CommandType = CommandType.StoredProcedure;
        sCmd.Parameters.Clear();
        sCmd.Parameters.AddWithValue("@APP_ID", permitId);
        sCmd.Parameters.AddWithValue("@UPLOAD_HEADS", uploadHeads);
        sCmd.Parameters.AddWithValue("@FILE_NAME", fileName);
        sCmd.ExecuteNonQuery();
        return "Done";
        

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {


        txtVehicleNumber.Text = "";
        VORegNumber.Text = "";
        lblMSg.Text = "";
        Panel1.Visible = false;
    }
}