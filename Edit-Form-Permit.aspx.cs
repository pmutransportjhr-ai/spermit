using SoftEra.JHT.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using QRCoder;
using System.Configuration;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Edit_Form_Permit : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                AlertAndRedirect("You are not permitted to access this page");
                return;
            }

            try
            {
                string decrypted = CryptorEngine.Decrypt(Request.QueryString["q"], true);
                string[] parts = decrypted.Split('=');

                if (parts.Length != 2 || parts[0] != "ID")
                    throw new Exception("Invalid request");

                int appId = Convert.ToInt32(parts[1]);
                hdnID.Value = appId.ToString();

                LoadPermitData(appId);
            }
            catch (Exception ex)
            {
                AlertAndRedirect(ex.Message);
            }
        }
    }


    private object ParseDateSafe(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return DBNull.Value;

        DateTime dt;

        // Your UI uses dd/MM/yyyy
        if (DateTime.TryParseExact(
            value.Trim(),
            "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out dt))
        {
            return dt;
        }

        // fallback
        if (DateTime.TryParse(value, out dt))
            return dt;

        throw new Exception("Invalid date format: " + value);
    }
    // ================= LOAD DATA =================
    private void LoadPermitData(int appId)
    {
        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["connectionstring"].ToString()))
        using (SqlCommand cmd = new SqlCommand("STA_PERMIT_ALL_EDIT_DATA", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@APP_ID", appId);
            

            DataSet ds = new DataSet();
            new SqlDataAdapter(cmd).Fill(ds);

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return;

            DataRow r = ds.Tables[0].Rows[0];

            // ================= APPLICATION DETAILS =================
            lblApplicationNo.Text = r["APPLICATION_NUMBER"].ToString();
            lblApplicationStatus.Text = r["APPLICATION_STATUS"].ToString();
            lblPermitType.Text = r["PERMIT_TYPE"].ToString();
            lblPermitNo.Text = r["PERMIT_NUMBER"].ToString();

            // ================= PERSONAL (EDITABLE) =================
            txtOwnerName.Text = r["OWNER_NAME"].ToString();
            txtCareOf.Text = r["CARE_OF"].ToString();
            txtPresentAddress.Text = r["PRESENT_ADDRESS"].ToString();
            txtPermanentAddress.Text = r["PERMANENT_ADDRESS"].ToString();

            // ================= VEHICLE (READ ONLY) =================
            VORegNumber.Text = r["REGISTRATION_NUMBER"].ToString();
            VType.Text = r["VEHICLE_CLASS_TYPE"].ToString();
            VChassisNumber.Text = r["CHASIS_NUMBER"].ToString();
            VEngineNumber.Text = r["ENGINE_NUMBER"].ToString();
            lblMakerDesc.Text = r["MAKER_DESC"].ToString();
            lblMakermodel.Text = r["MAKER_MODEL"].ToString();
            lblFuelDesc.Text = r["FUEL_DESC"].ToString();
            lblVcolor.Text = r["VEHICLE_COLOR"].ToString();
          
            VULW.Text = r["ULW"].ToString();
            VRLW.Text = r["RLW"].ToString();
            VSeatCapacity.Text = r["SEATING_CAPACITY"].ToString();
            SetDateTextbox(VManufacturingON, r["MANUFACTURING_ON"]);
            SetDateTextbox(VFitness, r["FITNESS_VALID_UPTO"]);
            SetDateTextbox(VTax, r["TAX_PAID_UPTO"]);

            SetDateTextbox(txtPermitFrom, r["VALID_FROM"]);
            SetDateTextbox(txtPermitTo, r["VALID_UP_TO"]);
            SetDateTextbox(txtICValidUpto, r["IC_VALID_UPTO"]);
            SetDateTextbox(txtPCValidUpto, r["PC_VALID_UPTO"]);

            

            // CS Status
            string csStatEdit = Convert.ToString(r["CS_STATUS"]);
            if (csStatEdit == "1" || csStatEdit == "Verified")
                ddlCSStatus.SelectedValue = "Verified";
            else
                ddlCSStatus.SelectedValue = "Not Verified";

            // Identity Proof Number
            txtIdentityNo.Text = r["IDENTITY_PROOF_NO"].ToString();


            // ================= OTHER DETAILS =================
            txtMobileNo.Text = r["MOBILE_NUMBER"].ToString();
            txtEmailId.Text = r["EMAIL_ID"].ToString();

            // Gender
            string gender = r["GENDER"].ToString();
            txtMale.Checked = gender == "Male";
            txtFemale.Checked = gender == "Female";

            // Qualification
            ddlQualification.SelectedValue = r["APPLICANT_QUALIFICATION"].ToString();

            // Category
            ddlCategory.SelectedValue = r["APPLICANT_CATEGORY"].ToString();

            // Ex-Army
            string exArmy = r["EX_ARMY"].ToString();
            txtExArmyY.Checked = exArmy == "Yes";
            txtExArmyN.Checked = exArmy == "No";

            // Minority
            string minority = r["MINORITY"].ToString();
            txtMinorityY.Checked = minority == "Yes";
            txtMinorityN.Checked = minority == "No";

            // Handicapped
            string handicapped = r["HANDICAPPED"].ToString();
            txtHandicappedY.Checked = handicapped == "Yes";
            txtHandicappedN.Checked = handicapped == "No";

            // Heavy Motor Licence
            string hml = r["HEAVY_MOTOR_LICENCE"].ToString();
            txtHMLY.Checked = hml == "Yes";
            txtHMLN.Checked = hml == "No";

            // Identity Proof
            string idProof = r["IDENTITY_PROOF"].ToString();
            txtIdentityProofV.Checked = idProof == "Voter Id";
            txtIdentityProofP.Checked = idProof == "Pan Card";
            txtIdentityProofA.Checked = idProof == "Aadhaar Card";

            // Bus Type
            txtACY.Checked = r["AIR_CONDITIONED"].ToString() == "Yes";
            txtACN.Checked = r["AIR_CONDITIONED"].ToString() == "No";

            txtDeluxeY.Checked = r["DELUXE"].ToString() == "Yes";
            txtDeluxeN.Checked = r["DELUXE"].ToString() == "No";

            txtSleeperY.Checked = r["SLEEPER"].ToString() == "Yes";
            txtSleeperN.Checked = r["SLEEPER"].ToString() == "No";


            // ================= ROUTE HEADER =================
            RouteHDRInfo.Text = r["ROUTE"].ToString();

            // ================= ROUTE TABLE =================
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                RouteTimeInfo.InnerHtml = BuildRouteTable(ds.Tables[1]);
            }

           
        }
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


    private string BuildRouteTable(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table class='table table-bordered'>");
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

        int index = 0;

        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("<tr>");

            /* ================= UP ================= */
            sb.Append("<td>");
            sb.Append(dr["VIA_UP_CITY"]);

            // Row ID
            sb.Append("<input type='hidden' name='rowId_" + index + "' value='" + dr["UP_ID"] + "' />");

            // MODE (REQUIRED)
            sb.Append("<input type='hidden' name='mode_" + index + "' value='UP' />");

            sb.Append("</td>");

            sb.Append("<td><input type='time' name='arr_" + index + "' value='" +
                NormalizeTime(dr["UP_ARR_TIME"]) + "' class='form-control'/></td>");

            sb.Append("<td><input type='time' name='dep_" + index + "' value='" +
                NormalizeTime(dr["UP_DEP_TIME"]) + "' class='form-control'/></td>");

            index++;

            sb.Append("<td></td>");

            /* ================= DOWN ================= */
            sb.Append("<td>");
            sb.Append(dr["VIA_DOWN_CITY"]);

            // Row ID
            sb.Append("<input type='hidden' name='rowId_" + index + "' value='" + dr["DOWN_ID"] + "' />");

            // MODE (REQUIRED)
            sb.Append("<input type='hidden' name='mode_" + index + "' value='DOWN' />");

            sb.Append("</td>");

            sb.Append("<td><input type='time' name='arr_" + index + "' value='" +
                NormalizeTime(dr["DOWN_ARR_TIME"]) + "' class='form-control'/></td>");

            sb.Append("<td><input type='time' name='dep_" + index + "' value='" +
                NormalizeTime(dr["DOWN_DEP_TIME"]) + "' class='form-control'/></td>");

            index++;

            sb.Append("</tr>");
        }

        sb.Append("</tbody></table>");

        return sb.ToString();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        try
        {
            int i = 0;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                while (Request.Form["rowId_" + i] != null)
                {
                    string mode = Request.Form["mode_" + i];

                    // Skip invalid rows safely
                    if (string.IsNullOrEmpty(mode))
                    {
                        i++;
                        continue;
                    }

                    using (SqlCommand cmd = new SqlCommand("UPDATE_STA_PERMIT_DETAILS", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        /* HEADER */
                        cmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                        cmd.Parameters.AddWithValue("@OWNER_NAME", txtOwnerName.Text.Trim());
                        cmd.Parameters.AddWithValue("@CARE_OF", txtCareOf.Text.Trim());
                        cmd.Parameters.AddWithValue("@PRESENT_ADDRESS", txtPresentAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", txtPermanentAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
                        cmd.Parameters.AddWithValue("@REGISTRATION_NO", VORegNumber.Text.Trim());
                        cmd.Parameters.AddWithValue(
                                        "@SEATING_CAPACITY",
                                        string.IsNullOrWhiteSpace(VSeatCapacity.Text)
                                            ? (object)DBNull.Value
                                            : VSeatCapacity.Text.Trim()
                                    );
                        cmd.Parameters.AddWithValue("@MANUFACTURING_DATE",
                         string.IsNullOrEmpty(VManufacturingON.Text)
                         ? (object)DBNull.Value
                         : VManufacturingON.Text);

                        // VALIDITY DATES (Typed)
                        cmd.Parameters.Add("@VALID_FROM", SqlDbType.Date)
                            .Value = ParseDateSafe(txtPermitFrom.Text);

                        cmd.Parameters.Add("@VALID_UP_TO", SqlDbType.Date)
                            .Value = ParseDateSafe(txtPermitTo.Text);

                        cmd.Parameters.Add("@IC_VALID_UPTO", SqlDbType.Date)
                            .Value = ParseDateSafe(txtICValidUpto.Text);

                        cmd.Parameters.Add("@PC_VALID_UPTO", SqlDbType.Date)
                            .Value = ParseDateSafe(txtPCValidUpto.Text);

                        cmd.Parameters.Add("@FITNESS_VALID_UPTO", SqlDbType.Date)
                            .Value = ParseDateSafe(VFitness.Text);

                        cmd.Parameters.Add("@TAX_PAID_UPTO", SqlDbType.Date)
                            .Value = ParseDateSafe(VTax.Text);

                        cmd.Parameters.AddWithValue("@IDENTITY_PROOF_NO", txtIdentityNo.Text.Trim());

                        cmd.Parameters.AddWithValue("@GENDER", txtMale.Checked ? "Male" : "Female");

                        cmd.Parameters.AddWithValue("@APPLICANT_QUALIFICATION", ddlQualification.SelectedValue);
                        cmd.Parameters.AddWithValue("@APPLICANT_CATEGORY", ddlCategory.SelectedValue);

                        cmd.Parameters.AddWithValue("@EX_ARMY", txtExArmyY.Checked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@MINORITY", txtMinorityY.Checked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@HANDICAPPED", txtHandicappedY.Checked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@HEAVY_MOTOR_LICENCE", txtHMLY.Checked ? "Yes" : "No");
                        string csStatusEditVal = (ddlCSStatus.SelectedValue == "1" || ddlCSStatus.SelectedValue == "Verified") ? "Verified" : "Not Verified";
                        cmd.Parameters.AddWithValue("@CS_STATUS", csStatusEditVal);

                        string idProof =
                            txtIdentityProofV.Checked ? "Voter Id" :
                            txtIdentityProofP.Checked ? "Pan Card" :
                            "Aadhaar Card";

                        cmd.Parameters.AddWithValue("@IDENTITY_PROOF", idProof);

                        cmd.Parameters.AddWithValue("@AIR_CONDITIONED", txtACY.Checked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@DELUXE", txtDeluxeY.Checked ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@SLEEPER", txtSleeperY.Checked ? "Yes" : "No");

                        /* ROUTE */
                        cmd.Parameters.AddWithValue("@ROW_ID",
                            Convert.ToInt32(Request.Form["rowId_" + i]));

                        cmd.Parameters.AddWithValue("@MODE", mode);

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
                this, GetType(), Guid.NewGuid().ToString(),
                "alert('Updated successfully');window.location='Edit-Permit-Application.aspx';",
                true);
        }
        catch (Exception ex)
        {
            Alert("Update failed : " + ex.Message);
        }
    }

    private void SetDateTextbox(TextBox txt, object value)
    {
        if (value != DBNull.Value && value != null)
        {
            DateTime dt;
            if (DateTime.TryParse(value.ToString(), out dt))
            {
                txt.Text = dt.ToString("yyyy-MM-dd");
            }
        }
    }



    
    private object ParseTime(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return DBNull.Value;

        TimeSpan ts;
        if (TimeSpan.TryParse(value, out ts))
            return ts;

        return DBNull.Value;
    }


    // ================= UTIL =================
    private void Alert(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(
            this,
            GetType(),
            Guid.NewGuid().ToString(),
            "alert('" + Clean(msg) + "');",
            true
        );
    }

    private void AlertAndRedirect(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(
            this,
            GetType(),
            Guid.NewGuid().ToString(),
            "alert('" + Clean(msg) + "');window.location='Edit-Permit-Application.aspx';",
            true
        );
    }



    private string Clean(string m)
    {
        return m.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
    }

 

}
