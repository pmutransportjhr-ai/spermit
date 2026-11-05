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
        txtAmount.Attributes["min"] = "0.01";
        txtAmount.Attributes["step"] = "0.01";
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
        txtAmount.ReadOnly = fixedAmount;
        txtAmount.Value = fixedAmount ? Convert.ToDecimal(rows[0]["FIXED_AMOUNT"]).ToString("0.00", CultureInfo.InvariantCulture) : "";
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
                string options = Convert.ToString(row["FIELD_OPTIONS"]);
                foreach (string option in options.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                    list.Items.Add(new ListItem(option.Trim(), option.Trim()));
                phDynamicFields.Controls.Add(list);
            }
            else
            {
                TextBox input = new TextBox();
                input.ID = "field_" + fieldId;
                input.CssClass = "form-control";
                if (type == "DATE") input.TextMode = TextBoxMode.Date;
                else if (type == "NUMBER") input.TextMode = TextBoxMode.Number;
                else if (type == "TEXTAREA") input.TextMode = TextBoxMode.MultiLine;
                phDynamicFields.Controls.Add(input);
            }
            phDynamicFields.Controls.Add(new LiteralControl("</div>"));
        }
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
            values[Convert.ToString(row["FIELD_KEY"])] = value;
        }
        return values.Count == 0 ? null : MiscPaymentService.ToJson(values);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Session.Remove("MISC_APP_VERIFIED");
        Session.Remove("MISC_APP_ID");
        Session.Remove("MISC_APP_NAME");
        Session.Remove("MISC_APP_NUMBER");
        Session.Remove("MISC_APP_AUTHORITY");
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
            cmd.Parameters.Add("@PERMIT_NUMBER", SqlDbType.VarChar, 100).Value = HFPermitNo.Value;
            cmd.Parameters.Add("@TABLE_NAME", SqlDbType.VarChar, 128).Value = HFTableName.Value;
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
            cmd.Parameters.Add("@TABLE_NAME", SqlDbType.VarChar, 128).Value = HFTableName.Value;
            cmd.Parameters.Add("@PERMIT_NUMBER", SqlDbType.VarChar, 100).Value = HFPermitNo.Value;
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

            decimal amount;
            if (!decimal.TryParse(txtAmount.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out amount) || amount <= 0)
                throw new InvalidOperationException("Enter a valid payment amount greater than zero.");
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
            string encrypted = PayGetWayDTO.MakeEncryptedData(deptId, receiptHead,
                Convert.ToString(Session["MISC_APP_NAME"]), deptTranId,
                amount.ToString("0.00", CultureInfo.InvariantCulture), appNumber, "NA",
                operationFor, Convert.ToString(Session["MISC_APP_ID"]),
                reasonInfo, treasury, office, securityCode);
            try { Convert.FromBase64String(encrypted); }
            catch { throw new InvalidOperationException("Unable to prepare JEGRAS payment request. Check the Finance configuration."); }

            // Persist the exact transaction before redirecting to the gateway.
            MiscPaymentService.CreateTransaction(deptTranId, Convert.ToInt32(Session["MISC_APP_ID"]),
                appNumber, Convert.ToString(Session["MISC_APP_NAME"]), authority, reasonId, amount,
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