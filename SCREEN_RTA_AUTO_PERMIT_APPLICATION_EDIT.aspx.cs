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
public partial class SCREEN_RTA_AUTO_PERMIT_APPLICATION_EDIT : System.Web.UI.Page
{
    //  private static string ID;
    // private static string TYPE;
    //private static string TYPE_OPTION_ID; 
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
        txtMobileNo.MaxLength = 10;
     

        if (!IsPostBack)
        {

                              Page.Title =   "RTA AUTO PERMIT APPLICATION EDIT";
                    PageHeading.InnerText =  "RTA AUTO PERMIT APPLICATION EDIT";
                    PageHeading1.InnerText = "RTA AUTO PERMIT APPLICATION EDIT";

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
                    sCmd.CommandText = "RTA_SP_GET_AUTO_PERMIT_MASTER_DATA";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                    sCmd.Parameters.AddWithValue("@MODE", "MASTER_VIEW");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    DataTable dt1 = ds.Tables[0];
                    if (dt1.Rows.Count > 0)
                    {
                        txtRTOName.Text = Convert.ToString(dt1.Rows[0]["RTO_NAME"]);
                        txtFrom.Text = Convert.ToString(dt1.Rows[0]["ST_FROM"]);
                        txtTo.Text = Convert.ToString(dt1.Rows[0]["ST_TO"]);

                        txtpermitFrom.Value = Convert.ToString(dt1.Rows[0]["VALID_FROM"]);
                        txtpermitTo.Value = Convert.ToString(dt1.Rows[0]["VALID_UP_TO"]);
                        
                        txtpermitNo.Text = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);

                        if (Convert.ToString(dt1.Rows[0]["PERMIT_TYPE"]) == "Temporary")
                            ddlPermitType.SelectedValue = "2";
                        else
                            ddlPermitType.SelectedValue = "1";
                        
                        VORegNumber.Text = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                        VOName.Text = Convert.ToString(dt1.Rows[0]["OWNER_NAME"]);
                        VOCareOf.Text = Convert.ToString(dt1.Rows[0]["CARE_OF"]);
                        VOPresentAddress.Text = Convert.ToString(dt1.Rows[0]["PRESENT_ADDRESS"]);
                        VOPermanentAddress.Text = Convert.ToString(dt1.Rows[0]["PERMANENT_ADDRESS"]);

                        VORegNumber.Text = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                        VType.Text = Convert.ToString(dt1.Rows[0]["VEHICLE_CLASS_TYPE"]);
                        VChassisNumber.Text = Convert.ToString(dt1.Rows[0]["CHASIS_NUMBER"]);
                        VEngineNumber.Text = Convert.ToString(dt1.Rows[0]["ENGINE_NUMBER"]);

                        VMakerDesc.Text = Convert.ToString(dt1.Rows[0]["MAKER_DESC"]);
                        VMakerModel.Text = Convert.ToString(dt1.Rows[0]["MAKER_MODEL"]);
                        VFuelDesc.Text = Convert.ToString(dt1.Rows[0]["FUEL_DESC"]);
                        VColor.Text = Convert.ToString(dt1.Rows[0]["VEHICLE_COLOR"]);
                        VFitness.Value = Convert.ToString(dt1.Rows[0]["FITNESS_VALID_UPTO"]);
                        VTax.Value = Convert.ToString(dt1.Rows[0]["TAX_PAID_UPTO"]);
                        VULW.Text = Convert.ToString(dt1.Rows[0]["ULW"]);
                        VRLW.Text = Convert.ToString(dt1.Rows[0]["RLW"]);
                        VSeatCapacity.Text = Convert.ToString(dt1.Rows[0]["SEATING_CAPACITY"]);
                        VManufacturingON.Value = Convert.ToString(dt1.Rows[0]["MANUFACTURING_ON"]);


                        txtMobileNo.Text = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);
                        txtEmailId.Text = Convert.ToString(dt1.Rows[0]["EMAIL_ID"]);


                        txtICValidUpto.Value = Convert.ToString(dt1.Rows[0]["IC_VALID_UPTO"]);
                        txtPCValidUpto.Value = Convert.ToString(dt1.Rows[0]["PC_VALID_UPTO"]);
                        txtFCValidUpto.Value = Convert.ToString(dt1.Rows[0]["FC_VALID_UPTO"]);

                        if(Convert.ToString(dt1.Rows[0]["GENDER"]) == "Male")
                        {
                            txtMale.Checked = true;
                            txtFemale.Checked = false;
                        }
                        else
                        {

                            txtMale.Checked = false;
                            txtFemale.Checked = true;

                        }

                    }




                }
                catch(Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='SCREEN_RTA_AUTO_PERMIT_REPORT.aspx'", true);
             
                }
            }
           

        }

    }
    
  
    protected void btnSave_Click(object sender, EventArgs e)
    {
      
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;
        string permitypeid = string.Empty;
        string permittypeserviceoptionid = string.Empty;

        if(ddlPermitType.SelectedValue == "2")
        {
            permitypeid = "7";
            permittypeserviceoptionid = "77";
        }
        else
        {
            permitypeid = "8";
            permittypeserviceoptionid = "91";
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
                sCmd.CommandText = "SCREEN_RTA_AUTO_PERMIT_APPLICATION_MASTER_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", hdnID.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtpermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", permitypeid);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", permittypeserviceoptionid);
                sCmd.Parameters.AddWithValue("@RTO_CODE", txtRTOCode.Value);
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

                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
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
                sCmd.Parameters.AddWithValue("@VALID_FROM", DateTime.ParseExact(txtpermitFrom.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@VALID_UP_TO", DateTime.ParseExact(txtpermitTo.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXIST1");
                var Cnt = sCmd.ExecuteScalar();
                if (Convert.ToInt32(Cnt) == 0)
                {
                    sCmd.Parameters["@MODE"].Value = "UPDATE";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                   
                    objTrans.Commit();

                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully updated');window.location ='SCREEN_RTA_AUTO_PERMIT_REPORT.aspx'", true);

                }
                else
                {
                    objTrans.Rollback();
                    String Message = "This Registration Number Already Exists.";
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
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='SCREEN_RTA_AUTO_PERMIT_REPORT.aspx'", true);
                // Response.Redirect("PermitHomePage.aspx");
            }
        }
    }
  
    
}