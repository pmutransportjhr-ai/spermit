using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MISCELLANEOUS_PAYMENT : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        int reasonId;
        string postedReason = Request.Form[ddlReason.UniqueID];
        string postedAuthority = Request.Form[ddlAuthority.UniqueID];
        if (int.TryParse(postedReason, out reasonId) && reasonId > 0)
            BindDynamicFields(reasonId, postedAuthority);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtApplicationNumber.MaxLength = 120;
        txtMobileNumber.MaxLength = 10;
        txtOTP.MaxLength = 6;
        if (!IsPostBack)
        {
            DivMobileValidate.Visible = false;
            DivOTPMatch.Visible = false;
            DivMiscPayment.Visible = false;
            BindAuthorities();
        }
    }

    private void BindAuthorities()
    {
        DataTable dt = MiscPaymentService.ExecuteReason("LIST_AUTHORITIES", null, null);
        ddlAuthority.DataSource = dt;
        ddlAuthority.DataTextField = "AUTHORITY_NAME";
        ddlAuthority.DataValueField = "AUTHORITY_CODE";
        ddlAuthority.DataBind();
        ddlAuthority.Items.Insert(0, new ListItem("-- Select Authority --", ""));
    }

    private void BindReasons()
    {
        ddlReason.Items.Clear();
        ddlReason.Items.Add(new ListItem("-- Select Payment Reason --", ""));
        if (string.IsNullOrEmpty(ddlAuthority.SelectedValue)) return;
        DataTable dt = MiscPaymentService.ExecuteReason("LIST_REASONS", ddlAuthority.SelectedValue, null);
        ddlReason.DataSource = dt;
        ddlReason.DataTextField = "REASON_NAME";
        ddlReason.DataValueField = "REASON_ID";
        ddlReason.DataBind();
        ddlReason.Items.Insert(0, new ListItem("-- Select Payment Reason --", ""));
    }

    protected void ddlAuthority_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReasons();
        phDynamicFields.Controls.Clear();
        DivMiscPayment.Visible = false;
    }

    protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        int reasonId;
        if (!int.TryParse(ddlReason.SelectedValue, out reasonId)) return;
        BindDynamicFields(reasonId);
        ApplyReasonAmountRules(reasonId);
    }

    private void ApplyReasonAmountRules(int reasonId)
    {
        DataTable dt = MiscPaymentService.ExecuteReason("LIST_REASONS", ddlAuthority.SelectedValue, null);
        DataRow[] rows = dt.Select("REASON_ID=" + reasonId.ToString(CultureInfo.InvariantCulture));
        if (rows.Length == 0) return;
        bool fixedAmount = Convert.ToString(rows[0]["AMOUNT_MODE"]) == "FIXED";
        DataTable fields = MiscPaymentService.ExecuteReason("LIST_FIELDS", ddlAuthority.SelectedValue, reasonId);
        foreach (DataRow field in fields.Rows)
        {
            string key = Convert.ToString(field["FIELD_KEY"]);
            if (!IsAmountField(key)) continue;
            int fieldId = Convert.ToInt32(field["FIELD_ID"]);
            TextBox amountInput = phDynamicFields.FindControl("field_" + fieldId) as TextBox;
            if (amountInput == null) continue;
            amountInput.Attributes["min"] = "0.01";
            amountInput.Attributes["step"] = "0.01";
            if (fixedAmount)
            {
                amountInput.ReadOnly = true;
                amountInput.Text = Convert.ToDecimal(rows[0]["FIXED_AMOUNT"]).ToString("0.00", CultureInfo.InvariantCulture);
            }
            else
            {
                amountInput.ReadOnly = false;
                amountInput.Text = "";
            }
            break;
        }
    }

    private void BindDynamicFields(int reasonId, string authorityCode = null)
    {
        phDynamicFields.Controls.Clear();
        if (string.IsNullOrEmpty(authorityCode)) authorityCode = ddlAuthority.SelectedValue;
        DataTable fields = MiscPaymentService.ExecuteReason("LIST_FIELDS", authorityCode, reasonId);
        foreach (DataRow row in fields.Rows)
        {
            int fieldId = Convert.ToInt32(row["FIELD_ID"]);
            string type = Convert.ToString(row["FIELD_TYPE"]);
            string label = Convert.ToString(row["FIELD_LABEL"]);
            bool required = Convert.ToBoolean(row["IS_REQUIRED"]);
            phDynamicFields.Controls.Add(new LiteralControl("<div class='form-group col-md-6'><label>" +
                HttpUtility.HtmlEncode(label) + (required ? " <span class='text-danger'>*</span>" : "") + "</label>"));
            if (type == "DROPDOWN")
            {
                DropDownList list = new DropDownList();
                list.ID = "field_" + fieldId;
                list.CssClass = "form-control";
                list.Items.Add(new ListItem("-- Select --", ""));

                DataTable optionRows = GetFieldOptions(reasonId, Convert.ToString(row["FIELD_KEY"]));
                if (optionRows.Rows.Count > 0)
                {
                    string parentFieldKey = Convert.ToString(optionRows.Rows[0]["PARENT_FIELD_KEY"]);
                    string parentOptionCode = "";
                    if (!string.IsNullOrWhiteSpace(parentFieldKey))
                    {
                        DataRow[] parentRows = fields.Select("FIELD_KEY='" + parentFieldKey.Replace("'", "''") + "'");
                        if (parentRows.Length > 0)
                        {
                            string parentControlId = "field_" + Convert.ToString(parentRows[0]["FIELD_ID"]);
                            DropDownList parentList = phDynamicFields.FindControl(parentControlId) as DropDownList;
                            parentOptionCode = Request.Form[parentList != null ? parentList.UniqueID : parentControlId];
                            if (string.IsNullOrWhiteSpace(parentOptionCode) && parentList != null)
                                parentOptionCode = parentList.SelectedValue;
                        }
                    }

                    foreach (DataRow option in optionRows.Rows)
                    {
                        string requiredParent = Convert.ToString(option["PARENT_OPTION_CODE"]);
                        if (!string.IsNullOrWhiteSpace(requiredParent) &&
                            !string.Equals(requiredParent, parentOptionCode, StringComparison.OrdinalIgnoreCase))
                            continue;
                        list.Items.Add(new ListItem(Convert.ToString(option["OPTION_LABEL"]),
                            Convert.ToString(option["OPTION_CODE"])));
                    }
                }
                else
                {
                    string options = Convert.ToString(row["FIELD_OPTIONS"]);
                    foreach (string option in options.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                        list.Items.Add(new ListItem(option.Trim(), option.Trim()));
                }
                phDynamicFields.Controls.Add(list);
            }
            else
            {
                TextBox input = new TextBox();
                input.ID = "field_" + fieldId;
                input.CssClass = "form-control";
                input.MaxLength = 250;
                input.Attributes["maxlength"] = "250";
                if (type == "DATE") input.TextMode = TextBoxMode.Date;
                else if (type == "NUMBER") input.TextMode = TextBoxMode.Number;
                else if (type == "TEXTAREA") input.TextMode = TextBoxMode.MultiLine;
                string autoFillValue = GetAutoFillValue(Convert.ToString(row["FIELD_KEY"]));
                if (!string.IsNullOrWhiteSpace(autoFillValue)) input.Text = autoFillValue;
                if (IsAmountField(Convert.ToString(row["FIELD_KEY"])))
                {
                    input.Attributes["min"] = "0.01";
                    input.Attributes["step"] = "0.01";
                }
                phDynamicFields.Controls.Add(input);
            }
            phDynamicFields.Controls.Add(new LiteralControl("</div>"));
        }
    }

    private DataTable GetFieldOptions(int reasonId, string fieldKey)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand(@"SELECT OPTION_CODE,OPTION_LABEL,PARENT_FIELD_KEY,PARENT_OPTION_CODE
            FROM dbo.MISC_PAYMENT_REASON_OPTION_MASTER
            WHERE REASON_ID=@REASON_ID AND FIELD_KEY=@FIELD_KEY AND IS_ACTIVE=1
            ORDER BY DISPLAY_ORDER,OPTION_LABEL", con))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            cmd.Parameters.Add("@REASON_ID", SqlDbType.Int).Value = reasonId;
            cmd.Parameters.Add("@FIELD_KEY", SqlDbType.VarChar, 50).Value = fieldKey;
            da.Fill(dt);
        }
        return dt;
    }

    private bool IsAmountField(string fieldKey)
    {
        return fieldKey.Equals("AMOUNT_OF_TAX", StringComparison.OrdinalIgnoreCase) ||
               fieldKey.Equals("AMOUNT_OF_FEE", StringComparison.OrdinalIgnoreCase) ||
               fieldKey.Equals("OBJECTION_FEE", StringComparison.OrdinalIgnoreCase) ||
               fieldKey.Equals("FEE", StringComparison.OrdinalIgnoreCase);
    }

    private decimal GetPaymentAmount(int reasonId)
    {
        DataTable fields = MiscPaymentService.ExecuteReason("LIST_FIELDS", ddlAuthority.SelectedValue, reasonId);
        foreach (DataRow row in fields.Rows)
        {
            if (!IsAmountField(Convert.ToString(row["FIELD_KEY"]))) continue;
            int fieldId = Convert.ToInt32(row["FIELD_ID"]);
            TextBox input = phDynamicFields.FindControl("field_" + fieldId) as TextBox;
            decimal amount;
            if (input == null || !decimal.TryParse(input.Text.Trim(), NumberStyles.Number,
                CultureInfo.InvariantCulture, out amount) || amount <= 0)
                throw new InvalidOperationException("Enter a valid " + Convert.ToString(row["FIELD_LABEL"]) + " greater than zero.");
            if (decimal.Round(amount, 2) != amount)
                throw new InvalidOperationException("Payment amount can have a maximum of two decimal places.");
            return amount;
        }
        throw new InvalidOperationException("The selected payment form has no configured amount field. Please contact support.");
    }

    private void LoadVerifiedApplicationFields(int appId, string tableName, Dictionary<string, string> values)
    {
        // TABLE_NAME originates from SP_VIEW_APPLICATION_STATUS, but validate it before
        // using it as a SQL identifier. APP_ID and the table name are not accepted from the browser.
        if (string.IsNullOrWhiteSpace(tableName) ||
            !(tableName.StartsWith("STA_", StringComparison.OrdinalIgnoreCase) ||
              tableName.StartsWith("RTA_", StringComparison.OrdinalIgnoreCase)))
            return;
        foreach (char ch in tableName)
            if (!(char.IsLetterOrDigit(ch) || ch == '_')) return;

        string sql = "SELECT TOP (1) * FROM dbo.[" + tableName + "] WHERE APP_ID=@APP_ID";
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.Parameters.Add("@APP_ID", SqlDbType.Int).Value = appId;
            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (!reader.Read()) return;
                for (int i = 0; i < reader.FieldCount; i++)
                    if (!reader.IsDBNull(i))
                        values[reader.GetName(i)] = Convert.ToString(reader.GetValue(i));
            }
        }
    }

    private string GetAutoFillValue(string fieldKey)
    {
        Dictionary<string, string> values = Session["MISC_APP_AUTOFILL"] as Dictionary<string, string>;
        string mobile = Convert.ToString(Session["MISC_APP_MOBILE"]);
        if (fieldKey.Equals("MOBILE_NUMBER", StringComparison.OrdinalIgnoreCase))
            return mobile;

        string[] candidates = null;
        if (fieldKey.Equals("APPLICANT_NAME", StringComparison.OrdinalIgnoreCase) ||
            fieldKey.Equals("OBJECTOR_NAME", StringComparison.OrdinalIgnoreCase))
            candidates = new string[] { "OWNER_NAME", "APPLICANT_NAME", "NAME" };
        else if (fieldKey.Equals("APPLICANT_ADDRESS", StringComparison.OrdinalIgnoreCase) ||
                 fieldKey.Equals("ADDRESS", StringComparison.OrdinalIgnoreCase))
            candidates = new string[] { "OWNER_ADDRESS", "APPLICANT_ADDRESS", "ADDRESS", "PERMANENT_ADDRESS", "CORRESPONDENCE_ADDRESS" };
        else if (fieldKey.Equals("VEHICLE_NUMBER", StringComparison.OrdinalIgnoreCase) ||
                 fieldKey.Equals("OBJECTOR_VEHICLE_NUMBER", StringComparison.OrdinalIgnoreCase))
            candidates = new string[] { "REGISTRATION_NUMBER", "VEHICLE_NUMBER", "VEHICLE_NO" };
        else if (fieldKey.Equals("PERMIT_NUMBER", StringComparison.OrdinalIgnoreCase) ||
                 fieldKey.Equals("OBJECTOR_PERMIT_NUMBER", StringComparison.OrdinalIgnoreCase))
            candidates = new string[] { "PERMIT_NUMBER" };
        else if (fieldKey.Equals("EMAIL_ID", StringComparison.OrdinalIgnoreCase))
            candidates = new string[] { "EMAIL_ID", "EMAIL", "EMAIL_ADDRESS" };

        if (values == null || candidates == null) return "";
        foreach (string candidate in candidates)
        {
            string value;
            if (values.TryGetValue(candidate, out value) && !string.IsNullOrWhiteSpace(value))
                return value.Trim();
        }
        return "";
    }

    private string CollectDynamicFields(int reasonId)
    {
        DataTable fields = MiscPaymentService.ExecuteReason("LIST_FIELDS", ddlAuthority.SelectedValue, reasonId);
        Dictionary<string, string> values = new Dictionary<string, string>();
        foreach (DataRow row in fields.Rows)
        {
            int fieldId = Convert.ToInt32(row["FIELD_ID"]);
            string value = "";
            DropDownList list = phDynamicFields.FindControl("field_" + fieldId) as DropDownList;
            if (list != null) value = list.SelectedValue.Trim();
            else
            {
                TextBox input = phDynamicFields.FindControl("field_" + fieldId) as TextBox;
                if (input != null) value = input.Text.Trim();
            }
            if (Convert.ToBoolean(row["IS_REQUIRED"]) && string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException("Please enter " + Convert.ToString(row["FIELD_LABEL"]) + ".");
            string fieldType = Convert.ToString(row["FIELD_TYPE"]);
            decimal numericValue;
            DateTime dateValue;
            if (fieldType == "NUMBER" && !string.IsNullOrWhiteSpace(value) &&
                !decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out numericValue))
                throw new InvalidOperationException(Convert.ToString(row["FIELD_LABEL"]) + " must be numeric.");
            if (fieldType == "DATE" && !string.IsNullOrWhiteSpace(value) &&
                !DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                throw new InvalidOperationException(Convert.ToString(row["FIELD_LABEL"]) + " must be a valid date.");
            values[Convert.ToString(row["FIELD_KEY"])] = value;
        }
        return values.Count == 0 ? null : MiscPaymentService.ToJson(values);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Session.Remove("MISC_TABLE_NAME");
        Session.Remove("MISC_PERMIT_NO");
        Session.Remove("MISC_APP_VERIFIED");
        Session.Remove("MISC_APP_ID");
        Session.Remove("MISC_APP_NAME");
        Session.Remove("MISC_APP_NUMBER");
        Session.Remove("MISC_APP_AUTHORITY");
        Session.Remove("MISC_APP_MOBILE");
        Session.Remove("MISC_APP_AUTOFILL");
        Response.Redirect("MISCELLANEOUS_PAYMENT.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string appNumber = txtApplicationNumber.Value.Trim();
        if (string.IsNullOrWhiteSpace(ddlAuthority.SelectedValue)) { ShowMessage("Select STA or RTA first."); return; }
        if (string.IsNullOrWhiteSpace(appNumber)) { ShowMessage("Enter an application number."); return; }
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_VIEW_APPLICATION_STATUS", con))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@APPLICATION_NUMBER", SqlDbType.VarChar, 120).Value = appNumber;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 40).Value = appNumber.Contains("(PTT)") ? "CS_APPLICATION_NUMBER" : "VERIFY_APPLICATION_NUMBER";
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0 || dt.Columns.Contains("MSG"))
            {
                ShowMessage(dt.Rows.Count == 0 ? "Application not found." : Convert.ToString(dt.Rows[0]["MSG"]));
                return;
            }
            HFTableName.Value = Convert.ToString(dt.Rows[0]["TABLE_NAME"]);
            HFPermitNo.Value = dt.Columns.Contains("PERMIT_NUMBER") ? Convert.ToString(dt.Rows[0]["PERMIT_NUMBER"]) : "";
            Session["MISC_TABLE_NAME"] = HFTableName.Value;
            Session["MISC_PERMIT_NO"] = HFPermitNo.Value;
            DivMobileValidate.Visible = true;
            txtApplicationNumber.Disabled = true;
            btnSearch.Enabled = false;
            lblMessage.Visible = false;
        }
    }

    protected void btnOTP_Click(object sender, EventArgs e)
    {
        string mobile = txtMobileNumber.Value.Trim();
        if (mobile.Length != 10) { ShowMessage("Enter a valid 10 digit mobile number."); return; }
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_VIEW_APPLICATION_STATUS", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@APPLICATION_NUMBER", SqlDbType.VarChar, 120).Value = txtApplicationNumber.Value.Trim();
            cmd.Parameters.Add("@PERMIT_NUMBER", SqlDbType.VarChar, 100).Value = Convert.ToString(Session["MISC_PERMIT_NO"]);
            cmd.Parameters.Add("@TABLE_NAME", SqlDbType.VarChar, 128).Value = Convert.ToString(Session["MISC_TABLE_NAME"]);
            cmd.Parameters.Add("@MOBILE_NUMBER", SqlDbType.VarChar, 10).Value = mobile;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 40).Value = "VERIFY_MOBILE_NUMBER";
            con.Open();
            object otp = cmd.ExecuteScalar();
            if (otp == null || Convert.ToString(otp) == "MOBILE NUMBER NOT FOUND")
            {
                ShowMessage("Mobile number does not match the application record.");
                return;
            }
            new SMSSender().Sendsms(mobile, Convert.ToString(otp), "1307161527893297291");
            txtMobileNumber.Disabled = true;
            btnOTP.Enabled = false;
            DivOTPMatch.Visible = true;
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_VIEW_APPLICATION_STATUS", con))
        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@APPLICATION_NUMBER", SqlDbType.VarChar, 120).Value = txtApplicationNumber.Value.Trim();
            cmd.Parameters.Add("@TABLE_NAME", SqlDbType.VarChar, 128).Value = Convert.ToString(Session["MISC_TABLE_NAME"]);
            cmd.Parameters.Add("@PERMIT_NUMBER", SqlDbType.VarChar, 100).Value = Convert.ToString(Session["MISC_PERMIT_NO"]);
            cmd.Parameters.Add("@OTP", SqlDbType.VarChar, 10).Value = txtOTP.Value.Trim();
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 40).Value = "VERIFY_OTP";
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0) { ShowMessage("Invalid or expired OTP."); return; }

            DataRow app = dt.Rows[0];
            string status = dt.Columns.Contains("APPLICATION_STATUS") ? Convert.ToString(app["APPLICATION_STATUS"]) : "";
            if (status.Equals("Rejected", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("WITHDRAW", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Objection", StringComparison.OrdinalIgnoreCase))
            { ShowMessage("Payment is not allowed for this application status."); return; }

            string tableType = dt.Columns.Contains("TABLE_TYPE") ? Convert.ToString(app["TABLE_TYPE"]) : HFTableName.Value;
            string appAuthority = tableType.StartsWith("STA", StringComparison.OrdinalIgnoreCase) ? "STA" :
                                  tableType.StartsWith("RTA", StringComparison.OrdinalIgnoreCase) ? "RTA" : "";
            if (string.IsNullOrEmpty(appAuthority))
            { ShowMessage("Could not determine STA/RTA authority for this application. Contact support."); return; }
            if (!string.Equals(appAuthority, ddlAuthority.SelectedValue, StringComparison.OrdinalIgnoreCase))
            { ShowMessage("The selected authority does not match this application. Please select " + appAuthority + "."); return; }

            Session["MISC_APP_VERIFIED"] = true;
            Session["MISC_APP_ID"] = Convert.ToInt32(app["APP_ID"]);
            Session["MISC_APP_NAME"] = Convert.ToString(app["OWNER_NAME"]);
            Session["MISC_APP_NUMBER"] = Convert.ToString(app["APPLICATION_NUMBER"]);
            Session["MISC_APP_AUTHORITY"] = appAuthority;
            Session["MISC_APP_MOBILE"] = txtMobileNumber.Value.Trim();

            // Keep only values returned by the OTP-verified application record for form prefilling.
            Dictionary<string, string> verifiedValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataColumn column in dt.Columns)
                verifiedValues[column.ColumnName] = Convert.ToString(app[column]);
            LoadVerifiedApplicationFields(Convert.ToInt32(app["APP_ID"]),
                Convert.ToString(Session["MISC_TABLE_NAME"]), verifiedValues);
            Session["MISC_APP_AUTOFILL"] = verifiedValues;
            BindReasons();
            DivMiscPayment.Visible = true;
            DivMobileValidate.Visible = true;
            DivOTPMatch.Visible = false;
            lblMessage.Visible = false;
        }
    }

    protected void btnMiscPay_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["MISC_APP_VERIFIED"] == null || !(bool)Session["MISC_APP_VERIFIED"])
                throw new InvalidOperationException("Verify the application with OTP before proceeding.");
            string appNumber = Convert.ToString(Session["MISC_APP_NUMBER"]);
            string authority = Convert.ToString(Session["MISC_APP_AUTHORITY"]);
            if (authority != ddlAuthority.SelectedValue)
                throw new InvalidOperationException("Selected authority does not match the application authority.");

            int reasonId;
            if (!int.TryParse(ddlReason.SelectedValue, out reasonId))
                throw new InvalidOperationException("Select a payment reason.");
            DataTable reasons = MiscPaymentService.ExecuteReason("LIST_REASONS", authority, null);
            DataRow[] selected = reasons.Select("REASON_ID=" + reasonId.ToString(CultureInfo.InvariantCulture));
            if (selected.Length == 0) throw new InvalidOperationException("Invalid or inactive payment reason.");

            decimal amount = GetPaymentAmount(reasonId);
            if (amount <= 0)
                throw new InvalidOperationException("Enter a valid payment amount greater than zero.");
            if (decimal.Round(amount, 2) != amount)
                throw new InvalidOperationException("Payment amount can have a maximum of two decimal places.");
            if (Convert.ToString(selected[0]["AMOUNT_MODE"]) == "FIXED" &&
                amount != Convert.ToDecimal(selected[0]["FIXED_AMOUNT"]))
                throw new InvalidOperationException("Amount does not match the fixed amount for this reason.");

            string fieldsJson = CollectDynamicFields(reasonId);
            string deptId, receiptHead, treasury, office;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"SELECT DEPTID,RECEIPTHEADCODE,TREASCODE,IFMSOFFICECODE
                FROM dbo.MISC_PAYMENT_AUTHORITY_MASTER WHERE AUTHORITY_CODE=@AUTHORITY_CODE AND IS_ACTIVE=1", con))
            {
                cmd.Parameters.Add("@AUTHORITY_CODE", SqlDbType.VarChar, 3).Value = authority;
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) throw new InvalidOperationException("Payment configuration is not available.");
                    deptId = Convert.ToString(reader["DEPTID"]);
                    receiptHead = Convert.ToString(reader["RECEIPTHEADCODE"]);
                    treasury = Convert.ToString(reader["TREASCODE"]);
                    office = Convert.ToString(reader["IFMSOFFICECODE"]);
                }
            }

            string securityCode = ConfigurationManager.AppSettings["JEGRAS_SECURITY_CODE"];
            if (string.IsNullOrWhiteSpace(securityCode))
                throw new InvalidOperationException("Configure JEGRAS_SECURITY_CODE in application settings before enabling payment.");

            string deptTranId = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
            string reasonInfo = "MISC-" + authority + "-" + reasonId.ToString(CultureInfo.InvariantCulture);
            string operationFor = Convert.ToString(selected[0]["REASON_NAME"]);
            string applicantName = Convert.ToString(Session["MISC_APP_NAME"]);
            if (operationFor.Contains("|") || applicantName.Contains("|") || appNumber.Contains("|"))
                throw new InvalidOperationException("Payment details contain an unsupported separator character.");
            string encrypted = PayGetWayDTO.MakeEncryptedData(deptId, receiptHead,
                applicantName, deptTranId,
                amount.ToString("0.00", CultureInfo.InvariantCulture), appNumber, "NA",
                operationFor, Convert.ToString(Session["MISC_APP_ID"]),
                reasonInfo, treasury, office, securityCode);
            try { Convert.FromBase64String(encrypted); }
            catch { throw new InvalidOperationException("Unable to prepare JEGRAS payment request. Check the Finance configuration."); }

            // Persist the exact transaction before redirecting to the gateway.
            MiscPaymentService.CreateTransaction(deptTranId, Convert.ToInt32(Session["MISC_APP_ID"]),
                appNumber, applicantName, authority, reasonId, amount,
                fieldsJson, reasonInfo, deptId, receiptHead, treasury, office);

            Response.Redirect("MISC_PAYMENT_REQUEST.aspx?q=" +
                HttpUtility.UrlEncode(encrypted, Response.ContentEncoding), false);
            Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex) { ShowMessage(ex.Message); }
    }

    private void ShowMessage(string message)
    {
        lblMessage.Text = HttpUtility.HtmlEncode(message);
        lblMessage.Visible = true;
    }
}