using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class STAGE_CARRIAGE_EDIT_FORM : System.Web.UI.Page
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
            if (Request.QueryString["q"] == null)
                throw new Exception("Invalid request");

            string decrypted = CryptorEngine.Decrypt(Request.QueryString["q"], true);

            string appIdStr = "";
            string[] parts = decrypted.Split('&');

            foreach (string part in parts)
            {
                string[] kv = part.Split('=');
                if (kv.Length == 2 && kv[0] == "ID")
                {
                    appIdStr = kv[1];
                    break;
                }
            }

            if (appIdStr == "")
                throw new Exception("Application ID missing");

            int appId = Convert.ToInt32(appIdStr);
            hdnID.Value = appId.ToString();

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("RTA_EDIT_APPLICATION_LIST", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@APP_ID", appId);
                cmd.Parameters.AddWithValue("@MODE", "LOAD_APPLICATION");

                DataSet ds = new DataSet();
                new SqlDataAdapter(cmd).Fill(ds);

                DataTable dtHeader = ds.Tables[0];
                DataTable dtRoute = ds.Tables[1];

                DataRow r = dtHeader.Rows[0];

                // HEADER
                lblApplicationNo.Text = Safe(r["APPLICATION_NUMBER"]);
                lblApplicationStatus.Text = Safe(r["APPLICATION_STATUS"]);
                lblPermitNo.Text = Safe(r["PERMIT_NUMBER"]);
                lblPermitType.Text = Safe(r["PERMIT_TYPE_ID"]);

                // PERSONAL
                txtOwnerName.Text = Safe(r["OWNER_NAME"]);
                txtCareOf.Text = Safe(r["CARE_OF"]);
                txtPresentAddress.Text = Safe(r["PRESENT_ADDRESS"]);
                txtPermanentAddress.Text = Safe(r["PERMANENT_ADDRESS"]);

                // VEHICLE
                VORegNumber.Text = Safe(r["REGISTRATION_NUMBER"]);
                VType.Text = Safe(r["VEHICLE_CLASS_TYPE"]);
                txtPermitFrom.Text = FormatDate(r["VALID_FROM"]);
                txtPermitTo.Text = FormatDate(r["VALID_UP_TO"]);

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

                // OTHER
                txtMobileNo.Text = Safe(r["MOBILE_NUMBER"]);
                txtEmailId.Text = Safe(r["EMAIL_ID"]);

                string gender = Safe(r["GENDER"]);
                txtMale.Checked = (gender == "Male");
                txtFemale.Checked = (gender == "Female");

                SetDropdown(ddlQualification, Safe(r["APPLICANT_QUALIFICATION"]));
                SetDropdown(ddlCategory, Safe(r["APPLICANT_CATEGORY"]));

                txtIdentityNo.Text = Safe(r["IDENTITY_PROOF_NO"]);

                SetRadio(txtExArmyY, txtExArmyN, r["EX_ARMY"]);
                SetRadio(txtMinorityY, txtMinorityN, r["MINORITY"]);
                SetRadio(txtHandicappedY, txtHandicappedN, r["HANDICAPPED"]);
                SetRadio(txtHMLY, txtHMLN, r["HEAVY_MOTOR_LICENCE"]);
                SetRadio(txtACY, txtACN, r["AIR_CONDITIONED"]);
                SetRadio(txtDeluxeY, txtDeluxeN, r["DELUXE"]);
                SetRadio(txtSleeperY, txtSleeperN, r["SLEEPER"]);

                // ROUTE
                RouteHDRInfo.Text = Safe(r["ROUTE"]);
                BuildRouteTable(dtRoute);
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

    private void SetRadio(RadioButton yes, RadioButton no, object val)
    {
        string v = Safe(val);
        yes.Checked = (v == "Yes");
        no.Checked = (v == "No");
    }

    private void SetDropdown(DropDownList ddl, string value)
    {
        if (ddl.Items.FindByValue(value) != null)
            ddl.SelectedValue = value;
    }
    private void BuildRouteTable(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table class='table table-bordered table-striped'>");

        sb.Append("<thead>");
        sb.Append("<tr>");
        sb.Append("<th colspan='3' class='text-center'>Up</th>");
        sb.Append("<th></th>");
        sb.Append("<th colspan='3' class='text-center'>Down</th>");
        sb.Append("</tr>");

        sb.Append("<tr>");
        sb.Append("<th>Route</th><th>Arrival</th><th>Departure</th>");
        sb.Append("<th></th>");
        sb.Append("<th>Route</th><th>Arrival</th><th>Departure</th>");
        sb.Append("</tr>");
        sb.Append("</thead><tbody>");

        List<string> ids = new List<string>();

        foreach (DataRow row in dt.Rows)
        {
            string id = Safe(row["ID"]);
            if (!ids.Contains(id))
                ids.Add(id);
        }

        ids.Sort();

        int index = 0;

        foreach (string id in ids)
        {
            DataRow up = null;
            DataRow down = null;

            foreach (DataRow row in dt.Rows)
            {
                if (Safe(row["ID"]) == id)
                {
                    if (Safe(row["MODE"]) == "UP")
                        up = row;

                    if (Safe(row["MODE"]) == "DOWN")
                        down = row;
                }
            }

            sb.Append("<tr>");

            // ================= UP =================
            if (up != null)
            {
                sb.Append("<td>" + Safe(up["VIA_CITY"]) + "</td>");

                sb.Append("<td><input type='time' step='60' name='arr_" + index + "' value='" +
                    NormalizeTime(up["ARRIVAL_TIMING"]) + "' class='form-control form-control-sm' /></td>");

                sb.Append("<td><input type='time' step='60' name='dep_" + index + "' value='" +
                    NormalizeTime(up["DEPARTURE_TIMING"]) + "' class='form-control form-control-sm' /></td>");

                sb.Append("<input type='hidden' name='id_" + index + "' value='" + Safe(up["ID"]) + "' />");
                sb.Append("<input type='hidden' name='mode_" + index + "' value='UP' />");
            }
            else
            {
                sb.Append("<td></td><td></td><td></td>");
            }

            sb.Append("<td></td>");

            index++;

            // ================= DOWN =================
            if (down != null)
            {
                sb.Append("<td>" + Safe(down["VIA_CITY"]) + "</td>");

                sb.Append("<td><input type='time' step='60' name='arr_" + index + "' value='" +
                    NormalizeTime(down["ARRIVAL_TIMING"]) + "' class='form-control form-control-sm' /></td>");

                sb.Append("<td><input type='time' step='60' name='dep_" + index + "' value='" +
                    NormalizeTime(down["DEPARTURE_TIMING"]) + "' class='form-control form-control-sm' /></td>");

                sb.Append("<input type='hidden' name='id_" + index + "' value='" + Safe(down["ID"]) + "' />");
                sb.Append("<input type='hidden' name='mode_" + index + "' value='DOWN' />");
            }
            else
            {
                sb.Append("<td></td><td></td><td></td>");
            }

            index++;

            sb.Append("</tr>");
        }

        sb.Append("</tbody></table>");

        RouteTimeInfo.InnerHtml = sb.ToString();
    }

    private string NormalizeTime(object timeValue)
    {
        if (timeValue == null) return "";

        string t = timeValue.ToString().Trim();

        if (string.IsNullOrEmpty(t) || t.Contains("...."))
            return "";

        // Remove "Hrs." or any text
        t = t.Replace("Hrs.", "").Replace("Hrs", "").Trim();

        DateTime dt;
        if (DateTime.TryParse(t, out dt))
        {
            return dt.ToString("HH:mm"); // REQUIRED FORMAT
        }

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

                // ================= 1. UPDATE MASTER =================
                using (SqlCommand cmd = new SqlCommand("[UPDATE_RTA_STAGE_CARRIAGE_ALL]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));

                    cmd.Parameters.AddWithValue("@OWNER_NAME", txtOwnerName.Text.Trim());
                    cmd.Parameters.AddWithValue("@CARE_OF", txtCareOf.Text.Trim());
                    cmd.Parameters.AddWithValue("@PRESENT_ADDRESS", txtPresentAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", txtPermanentAddress.Text.Trim());

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

                    cmd.Parameters.AddWithValue("@VALID_FROM", ParseDate(txtPermitFrom.Text));
                    cmd.Parameters.AddWithValue("@VALID_UP_TO", ParseDate(txtPermitTo.Text));
                    cmd.Parameters.AddWithValue("@FITNESS_VALID_UPTO", ParseDate(VFitness.Text));
                    cmd.Parameters.AddWithValue("@TAX_PAID_UPTO", ParseDate(VTax.Text));

                    cmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());

                    cmd.Parameters.AddWithValue("@GENDER", txtMale.Checked ? "Male" : "Female");
                    cmd.Parameters.AddWithValue("@APPLICANT_QUALIFICATION", ddlQualification.SelectedValue);
                    cmd.Parameters.AddWithValue("@APPLICANT_CATEGORY", ddlCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@IDENTITY_PROOF_NO", txtIdentityNo.Text.Trim());

                    cmd.Parameters.AddWithValue("@EX_ARMY", GetRadio(txtExArmyY));
                    cmd.Parameters.AddWithValue("@MINORITY", GetRadio(txtMinorityY));
                    cmd.Parameters.AddWithValue("@HANDICAPPED", GetRadio(txtHandicappedY));
                    cmd.Parameters.AddWithValue("@HEAVY_MOTOR_LICENCE", GetRadio(txtHMLY));
                    cmd.Parameters.AddWithValue("@AIR_CONDITIONED", GetRadio(txtACY));
                    cmd.Parameters.AddWithValue("@DELUXE", GetRadio(txtDeluxeY));
                    cmd.Parameters.AddWithValue("@SLEEPER", GetRadio(txtSleeperY));

                    cmd.Parameters.AddWithValue("@ROUTE", RouteHDRInfo.Text.Trim());

                    cmd.ExecuteNonQuery();
                }

             

                // ================= 2. Update ROUTE =================
                int i = 0;

                while (Request.Form["id_" + i] != null)
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE_RTA_STAGE_ROUTE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Request.Form["id_" + i]));

                        cmd.Parameters.AddWithValue("@MODE", Request.Form["mode_" + i]);

                        cmd.Parameters.AddWithValue("@ARRIVAL_TIMING",
                            ParseTime(Request.Form["arr_" + i]));

                        cmd.Parameters.AddWithValue("@DEPARTURE_TIMING",
                            ParseTime(Request.Form["dep_" + i]));

                        cmd.ExecuteNonQuery();
                    }

                    i++;
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

    private object ParseTime(string input)
    {
        if (string.IsNullOrWhiteSpace(input) || input == "----")
            return DBNull.Value;

        TimeSpan time;
        if (TimeSpan.TryParse(input, out time))
            return time;

        return DBNull.Value;
    }

    private string GetRadio(RadioButton yesRadio)
    {
        return yesRadio.Checked ? "Yes" : "No";
    }
}