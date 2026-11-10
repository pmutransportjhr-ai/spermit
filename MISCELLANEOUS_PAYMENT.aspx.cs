using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

public partial class MISCELLANEOUS_PAYMENT : System.Web.UI.Page
{
    private const string SessionTable = "MISC_APP_TABLE";
    private const string SessionAppId = "MISC_APP_ID";
    private const string SessionAppNo = "MISC_APP_NUMBER";
    private const string SessionOwner = "MISC_APP_OWNER";
    private const string SessionAuthority = "MISC_APP_AUTHORITY";

    protected void Page_Load(object sender, EventArgs e)
    {
        txtApplicationNumber.MaxLength = 120;
        txtAmount.Attributes["min"] = "0.01";
        txtAmount.Attributes["max"] = "99999999.99";
        txtAmount.Attributes["step"] = "0.01";

        if (!IsPostBack)
        {
            ClearApplicationSession();
            pnlPayment.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string applicationNumber = (txtApplicationNumber.Value ?? string.Empty).Trim();
            if (applicationNumber.Length == 0)
                throw new InvalidOperationException("Enter an application number.");

            string tableName;
            string authority;
            DataRow application = FetchApplication(applicationNumber, out tableName, out authority);

            string status = Convert.ToString(application["APPLICATION_STATUS"]);
            if (IsPaymentBlockedStatus(status))
                throw new InvalidOperationException("Miscellaneous payment is not allowed for application status: " + status + ".");

            Session[SessionTable] = tableName;
            Session[SessionAppId] = Convert.ToString(application["APP_ID"]);
            Session[SessionAppNo] = Convert.ToString(application["APPLICATION_NUMBER"]);
            Session[SessionOwner] = Convert.ToString(application["OWNER_NAME"]);
            Session[SessionAuthority] = authority;

            BindApplicationDetails(application, authority);
            txtApplicationNumber.Disabled = true;
            btnSearch.Enabled = false;
            pnlPayment.Visible = true;
            lblMessage.Visible = false;
        }
        catch (Exception ex)
        {
            pnlPayment.Visible = false;
            ShowMessage(ex.Message);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearApplicationSession();
        txtApplicationNumber.Value = string.Empty;
        txtApplicationNumber.Disabled = false;
        txtAmount.Value = string.Empty;
        btnSearch.Enabled = true;
        pnlPayment.Visible = false;
        lblMessage.Visible = false;
    }

    protected void btnMiscPay_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session[SessionTable] == null || Session[SessionAppId] == null ||
                Session[SessionAppNo] == null || Session[SessionOwner] == null ||
                Session[SessionAuthority] == null)
                throw new InvalidOperationException("Fetch the application details again before proceeding.");

            decimal amount;
            if (!decimal.TryParse(txtAmount.Value, NumberStyles.Number,
                CultureInfo.InvariantCulture, out amount) || amount <= 0)
                throw new InvalidOperationException("Enter a valid amount greater than zero.");

            if (decimal.Round(amount, 2) != amount || amount > 99999999.99m)
                throw new InvalidOperationException("Amount must have up to two decimal places and must not exceed ₹99,999,999.99.");

            string tableName = Convert.ToString(Session[SessionTable]);
            string applicationNumber = Convert.ToString(Session[SessionAppNo]);
            string authority = Convert.ToString(Session[SessionAuthority]);
            string appId = Convert.ToString(Session[SessionAppId]);

            // Re-read the application from the database before creating the payment request.
            string verifiedTable;
            string verifiedAuthority;
            DataRow application = FetchApplication(applicationNumber, out verifiedTable, out verifiedAuthority);

            if (!string.Equals(tableName, verifiedTable, StringComparison.OrdinalIgnoreCase) ||
                !string.Equals(authority, verifiedAuthority, StringComparison.OrdinalIgnoreCase) ||
                !string.Equals(appId, Convert.ToString(application["APP_ID"]), StringComparison.Ordinal))
                throw new InvalidOperationException("Application details have changed. Fetch the application again.");

            string status = Convert.ToString(application["APPLICATION_STATUS"]);
            if (IsPaymentBlockedStatus(status))
                throw new InvalidOperationException("Miscellaneous payment is not allowed for application status: " + status + ".");

            string deptId;
            string receiptHead;
            string treasury;
            string office;
            GetFinanceConfiguration(authority, out deptId, out receiptHead, out treasury, out office);

            string securityCode = ConfigurationManager.AppSettings["JEGRAS_SECURITY_CODE"];
            if (string.IsNullOrWhiteSpace(securityCode))
                throw new InvalidOperationException("JEGRAS_SECURITY_CODE is not configured in web.config.");

            string deptTransactionId = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
            string operationFor = "MISCELLANEOUS PAYMENT";
            string applicantName = Convert.ToString(application["OWNER_NAME"]);

            if (applicantName.Contains("|") || applicationNumber.Contains("|") || tableName.Contains("|"))
                throw new InvalidOperationException("Application details contain an unsupported character.");

            // Payload positions are consumed by the existing PaymentRequest.aspx,
            // api/payInitiate.ashx and PaymentResponse.aspx payment pipeline.
            string encrypted = PayGetWayDTO.MakeEncryptedData(
                deptId,
                receiptHead,
                applicantName,
                deptTransactionId,
                amount.ToString("0.00", CultureInfo.InvariantCulture),
                applicationNumber,
                "NA",
                operationFor,
                appId,
                tableName,
                treasury,
                office,
                securityCode);

            try
            {
                Convert.FromBase64String(encrypted);
            }
            catch
            {
                throw new InvalidOperationException("Unable to prepare the JEGRAS payment request. Verify the Finance configuration.");
            }

            Response.Redirect("PaymentRequest.aspx?q=" +
                HttpUtility.UrlEncode(encrypted, Response.ContentEncoding), false);
            Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message);
        }
    }

    private DataRow FetchApplication(string applicationNumber, out string tableName, out string authority)
    {
        tableName = string.Empty;
        authority = string.Empty;

        DataTable verification = new DataTable();
        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_VIEW_APPLICATION_STATUS", con))
        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@APPLICATION_NUMBER", SqlDbType.VarChar, 120).Value = applicationNumber;
            cmd.Parameters.Add("@MODE", SqlDbType.VarChar, 40).Value =
                applicationNumber.IndexOf("(PTT)", StringComparison.OrdinalIgnoreCase) >= 0
                    ? "CS_APPLICATION_NUMBER" : "VERIFY_APPLICATION_NUMBER";
            adapter.Fill(verification);
        }

        if (verification.Rows.Count == 0)
            throw new InvalidOperationException("Application number was not found.");

        if (verification.Columns.Contains("MSG"))
            throw new InvalidOperationException(Convert.ToString(verification.Rows[0]["MSG"]));

        if (!verification.Columns.Contains("TABLE_NAME"))
            throw new InvalidOperationException("The application lookup did not return its source table.");

        tableName = Convert.ToString(verification.Rows[0]["TABLE_NAME"]).Trim();

        // The table name comes from the stored procedure, but still validate it
        // before using it as a SQL identifier.
        if (!Regex.IsMatch(tableName, "^[A-Za-z0-9_]+$"))
            throw new InvalidOperationException("Invalid application source table returned by the database.");

        if (tableName.StartsWith("STA_", StringComparison.OrdinalIgnoreCase))
            authority = "STA";
        else if (tableName.StartsWith("RTA_", StringComparison.OrdinalIgnoreCase))
            authority = "RTA";
        else
            throw new InvalidOperationException("Could not determine whether this application belongs to STA or RTA.");

        DataTable details = new DataTable();
        string sql = @"
            SELECT TOP (1)
                APP_ID,
                OWNER_NAME,
                APPLICATION_NUMBER,
                APPLICATION_STATUS
            FROM dbo." + "[" + tableName + @"]
            WHERE LTRIM(RTRIM(APPLICATION_NUMBER)) = @APPLICATION_NUMBER;";

        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand(sql, con))
        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        {
            cmd.Parameters.Add("@APPLICATION_NUMBER", SqlDbType.VarChar, 120).Value = applicationNumber;
            adapter.Fill(details);
        }

        if (details.Rows.Count == 0)
            throw new InvalidOperationException("Application details could not be loaded from the application record.");

        return details.Rows[0];
    }

    private void GetFinanceConfiguration(string authority, out string deptId,
        out string receiptHead, out string treasury, out string office)
    {
        deptId = receiptHead = treasury = office = string.Empty;

        const string sql = @"
            SELECT DEPTID, RECEIPTHEADCODE, TREASCODE, IFMSOFFICECODE
            FROM dbo.MISC_PAYMENT_AUTHORITY_MASTER
            WHERE AUTHORITY_CODE = @AUTHORITY_CODE
              AND IS_ACTIVE = 1;";

        using (SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.Parameters.Add("@AUTHORITY_CODE", SqlDbType.VarChar, 3).Value = authority;
            con.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                    throw new InvalidOperationException("Miscellaneous payment finance configuration is not available for " + authority + ".");

                deptId = Convert.ToString(reader["DEPTID"]);
                receiptHead = Convert.ToString(reader["RECEIPTHEADCODE"]);
                treasury = Convert.ToString(reader["TREASCODE"]);
                office = Convert.ToString(reader["IFMSOFFICECODE"]);
            }
        }

        if (string.IsNullOrWhiteSpace(deptId) || string.IsNullOrWhiteSpace(receiptHead) ||
            string.IsNullOrWhiteSpace(treasury) || string.IsNullOrWhiteSpace(office))
            throw new InvalidOperationException("Finance configuration contains incomplete JEGRAS details.");
    }

    private static bool IsPaymentBlockedStatus(string status)
    {
        return status.Equals("Rejected", StringComparison.OrdinalIgnoreCase) ||
               status.Equals("WITHDRAW", StringComparison.OrdinalIgnoreCase) ||
               status.Equals("Objection", StringComparison.OrdinalIgnoreCase);
    }

    private void BindApplicationDetails(DataRow application, string authority)
    {
        lblApplicantName.Text = HttpUtility.HtmlEncode(Convert.ToString(application["OWNER_NAME"]));
        lblFetchedApplicationNumber.Text = HttpUtility.HtmlEncode(Convert.ToString(application["APPLICATION_NUMBER"]));
        lblAuthority.Text = authority == "STA" ? "State Transport Authority" : "Regional Transport Authority";
        lblApplicationStatus.Text = HttpUtility.HtmlEncode(Convert.ToString(application["APPLICATION_STATUS"]));
    }

    private void ClearApplicationSession()
    {
        Session.Remove(SessionTable);
        Session.Remove(SessionAppId);
        Session.Remove(SessionAppNo);
        Session.Remove(SessionOwner);
        Session.Remove(SessionAuthority);
    }

    private void ShowMessage(string message)
    {
        lblMessage.Text = HttpUtility.HtmlEncode(message);
        lblMessage.Visible = true;
    }
}
