using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTA_SCHOOL_BUS_EDIT_FORM : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();

    public string queryString = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(
                    Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('You are not permitted to access this page');window.location='RTA-DASHBOARD.aspx'", true);
                return;
            }

            LoadApplication();
        }
    }

    private void LoadApplication()
    {
        string conStr = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        try
        {
            string decrypted = CryptorEngine.Decrypt(Request.QueryString["q"], true);

            int appId = Convert.ToInt32(decrypted.Split('=')[1]);
            hdnID.Value = appId.ToString();


            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("RTA_EDIT_APPLICATION_LIST", con)) // ✅ your SP name
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@APP_ID", appId);
                cmd.Parameters.AddWithValue("@MODE", "SB_LOAD_APPLICATION");

                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);

                if (dt.Rows.Count == 0)
                    throw new Exception("No data found");

                DataRow r = dt.Rows[0];

                // ================= HEADER =================
                lblApplicationNo.Text = Safe(r["APPLICATION_NUMBER"]);
                lblApplicationStatus.Text = Safe(r["APPLICATION_STATUS"]);
                lblPermitNo.Text = Safe(r["PERMIT_NUMBER"]);
                lblPermitType.Text = Safe(r["PERMIT_TYPE_ID"]);

                // ================= PERSONAL =================
                txtOwnerName.Text = Safe(r["OWNER_NAME"]);
                txtCareOf.Text = Safe(r["CARE_OF"]);
                txtPresentAddress.Text = Safe(r["PRESENT_ADDRESS"]);
                txtPermanentAddress.Text = Safe(r["PERMANENT_ADDRESS"]);

                txtMobileNo.Text = Safe(r["MOBILE_NUMBER"]);
                txtEmailId.Text = Safe(r["EMAIL_ID"]);

                txtMale.Checked = Safe(r["GENDER"]) == "Male";
                txtFemale.Checked = Safe(r["GENDER"]) == "Female";

                // ================= VEHICLE =================
                VORegNumber.Text = Safe(r["REGISTRATION_NUMBER"]);
                VType.Text = Safe(r["VEHICLE_CLASS_TYPE"]);
                VChassisNumber.Text = Safe(r["CHASIS_NUMBER"]);
                VEngineNumber.Text = Safe(r["ENGINE_NUMBER"]);

                lblMakerDesc.Text = Safe(r["MAKER_DESC"]);
                lblMakermodel.Text = Safe(r["MAKER_MODEL"]);
                lblFuelDesc.Text = Safe(r["FUEL_DESC"]);
                lblVcolor.Text = Safe(r["VEHICLE_COLOR"]);

                VFitness.Text = FormatDate(r["FITNESS_VALID_UPTO"]);
                VTax.Text = FormatDate(r["TAX_PAID_UPTO"]);

                VULW.Text = Safe(r["ULW"]);
                VRLW.Text = Safe(r["RLW"]);
                VSeatCapacity.Text = Safe(r["SEATING_CAPACITY"]);

                txtPermitFrom.Text = FormatDate(r["VALID_FROM"]);
                txtPermitTo.Text = FormatDate(r["VALID_UP_TO"]);

                // ================= EXTRA =================
                txtManufacturingDate.Text = FormatDate(r["MANUFACTURING_ON"]);
                txtICValid.Text = FormatDate(r["IC_VALID_UPTO"]);
                txtPCValid.Text = FormatDate(r["PC_VALID_UPTO"]);
                txtFCValid.Text = FormatDate(r["FC_VALID_UPTO"]);
                txtStFrom.Text = Safe(r["ST_FROM"]);

                txtFee.Text = Safe(r["FEE"]);
                txtPermitFee.Text = Safe(r["PERMIT_FEE_AMOUNT"]);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(
                Page, Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('" + ex.Message.Replace("'", "") + "');", true);
        }
    }
    private string Safe(object val)
    {
        if (val == null || val == DBNull.Value)
            return "";
        return val.ToString();
    }

    private string FormatDate(object val)
    {
        if (val == null || val == DBNull.Value)
            return "";

        DateTime dt;
        if (DateTime.TryParse(val.ToString(), out dt))
            return dt.ToString("yyyy-MM-dd");

        return "";
    }

 

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        try
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE_RTA_SCHOOL_BUS_ALL", con)) // ✅ NEW SP
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ================= BASIC =================
                    cmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));

                    // ================= PERSONAL =================
                    cmd.Parameters.AddWithValue("@OWNER_NAME", txtOwnerName.Text.Trim());
                    cmd.Parameters.AddWithValue("@CARE_OF", txtCareOf.Text.Trim());
                    cmd.Parameters.AddWithValue("@PRESENT_ADDRESS", txtPresentAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", txtPermanentAddress.Text.Trim());

                    cmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
                    cmd.Parameters.AddWithValue("@GENDER", txtMale.Checked ? "Male" : "Female");

                    // ================= VEHICLE =================
                    cmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", VType.Text.Trim());

                    cmd.Parameters.AddWithValue("@CHASIS_NUMBER", VChassisNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@ENGINE_NUMBER", VEngineNumber.Text.Trim());

                    cmd.Parameters.AddWithValue("@MAKER_DESC", lblMakerDesc.Text.Trim());
                    cmd.Parameters.AddWithValue("@MAKER_MODEL", lblMakermodel.Text.Trim());
                    cmd.Parameters.AddWithValue("@FUEL_DESC", lblFuelDesc.Text.Trim());
                    cmd.Parameters.AddWithValue("@VEHICLE_COLOR", lblVcolor.Text.Trim());

                    cmd.Parameters.AddWithValue("@SEATING_CAPACITY", SafeInt(VSeatCapacity.Text));
                    cmd.Parameters.AddWithValue("@ULW", SafeDecimal(VULW.Text));
                    cmd.Parameters.AddWithValue("@RLW", SafeDecimal(VRLW.Text));

                    // ================= DATES =================
                    cmd.Parameters.AddWithValue("@VALID_FROM", ParseDate(txtPermitFrom.Text));
                    cmd.Parameters.AddWithValue("@VALID_UP_TO", ParseDate(txtPermitTo.Text));

                    cmd.Parameters.AddWithValue("@FITNESS_VALID_UPTO", ParseDate(VFitness.Text));
                    cmd.Parameters.AddWithValue("@TAX_PAID_UPTO", ParseDate(VTax.Text));

                    cmd.Parameters.AddWithValue("@MANUFACTURING_ON", ParseDate(txtManufacturingDate.Text));

                    cmd.Parameters.AddWithValue("@IC_VALID_UPTO", ParseDate(txtICValid.Text));
                    cmd.Parameters.AddWithValue("@PC_VALID_UPTO", ParseDate(txtPCValid.Text));
                    cmd.Parameters.AddWithValue("@FC_VALID_UPTO", ParseDate(txtFCValid.Text));

                    // ================= FEES =================
                    cmd.Parameters.AddWithValue("@FEE", SafeDecimal(txtFee.Text));
                    cmd.Parameters.AddWithValue("@PERMIT_FEE_AMOUNT", SafeDecimal(txtPermitFee.Text));
                    cmd.Parameters.AddWithValue("@ST_FROM", txtStFrom.Text.Trim());
                    

                    // ================= EXECUTE =================
                    cmd.ExecuteNonQuery();
                }
            }

            ScriptManager.RegisterClientScriptBlock(
                Page, Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('Updated Successfully'); window.close();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(
                Page, Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('Error: " + ex.Message.Replace("'", "") + "');", true);
        }
    }

    // ================= SAFE CONVERSIONS =================

    private int SafeInt(string val)
    {
        int i;
        return int.TryParse(val, out i) ? i : 0;
    }

    private decimal SafeDecimal(string val)
    {
        decimal d;
        return decimal.TryParse(val, out d) ? d : 0;
    }

    private object ParseDate(string val)
    {
        DateTime dt;
        if (DateTime.TryParse(val, out dt))
            return dt;

        return DBNull.Value;
    }

   
}