using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using SoftEra.JHT.Infrastructure;

public partial class ForgotPassword : System.Web.UI.Page
{
    private const string SearchProcedure = "ADMIN_SEARCH_USER_FOR_PASSWORD_RESET";
    private const string ResetProcedure = "ADMIN_RESET_USER_PASSWORD";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Bind ViewState to this authenticated session and prevent browser caching.
        Page.ViewStateUserKey = Session.SessionID;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        // Enforce access before page controls/events are processed. This check
        // runs for direct URL requests as well as every postback.
        if (!IsAuthorizedAdmin())
        {
            string redirectUrl = Session["EMPLOYEE_ID"] == null
                ? "Login.aspx"
                : "Login.aspx";

            Response.Redirect(redirectUrl, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsAuthorizedAdmin())
            return;

        if (!IsPostBack)
        {
            Session["salt"] = ConfigurationManager.AppSettings["SecurityKey"];
            pnlUserDetails.Visible = false;
        }
    }

    private bool IsAuthorizedAdmin()
    {
        string employeeId = Convert.ToString(Session["EMPLOYEE_ID"]);
        string role = Convert.ToString(Session["USER_ROLE"]).Trim();

        // USER_ROLE is populated from the authenticated user's database record
        // in Login.aspx.cs. Do not rely on URL parameters or client-side checks.
        return !string.IsNullOrWhiteSpace(employeeId) &&
               string.Equals(role, "Super Admin", StringComparison.OrdinalIgnoreCase);
    }

    private bool IsValidUserType(string userType)
    {
        return userType == "STA" || userType == "RTA";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pnlUserDetails.Visible = false;
        ViewState.Remove("SelectedEmployeeId");
        ViewState.Remove("SelectedUserType");
        ClearMessage();

        string userType = ddlType.SelectedValue;
        string searchText = (txtSearch.Text ?? string.Empty).Trim();

        if (!IsValidUserType(userType))
        {
            ShowMessage("Please select STA or RTA user type.", true);
            return;
        }

        if (searchText.Length == 0 || searchText.Length > 200)
        {
            ShowMessage("Please enter a username, email address or mobile number.", true);
            return;
        }

        try
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TYPE", SqlDbType.VarChar, 3) { Value = userType },
                new SqlParameter("@SEARCH_TEXT", SqlDbType.NVarChar, 200) { Value = searchText }
            };

            DataSet data = SQLHelper.ExecuteDataset(
                SQLHelper.GetConnString(), CommandType.StoredProcedure,
                SearchProcedure, parameters);

            if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
            {
                ShowMessage("No matching user was found for the selected user type.", true);
                return;
            }

            if (data.Tables[0].Rows.Count > 1)
            {
                ShowMessage("More than one user matches this search. Please search using a unique username or mobile number.", true);
                return;
            }

            DataRow user = data.Tables[0].Rows[0];
            string employeeId = Convert.ToString(user["EMPLOYEE_ID"]);

            if (string.IsNullOrWhiteSpace(employeeId))
            {
                ShowMessage("The selected user record does not contain a valid employee ID.", true);
                return;
            }

            lblEmployeeName.Text = Convert.ToString(user["EMPLOYEE_NAME"]);
            lblLoginId.Text = Convert.ToString(user["LOGIN_ID"]);
            lblEmail.Text = Convert.ToString(user["EMAIL_ID"]);
            lblMobile.Text = Convert.ToString(user["MOBILE_NUMBER"]);
            lblStatus.Text = Convert.ToString(user["STATUS"]);

            // Store the selected identity in MAC-protected ViewState rather than
            // trusting a client-editable hidden field during the reset postback.
            ViewState["SelectedEmployeeId"] = employeeId;
            ViewState["SelectedUserType"] = userType;
            pnlUserDetails.Visible = true;
            ShowMessage("User found. Verify the details before resetting the password.", false);
        }
        catch (Exception)
        {
            ShowMessage("Unable to search users right now. Please contact the system administrator.", true);
        }
    }

    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        ClearMessage();

        if (!IsAuthorizedAdmin())
        {
            Response.Redirect("Login.aspx", true);
            return;
        }

        string employeeId = Convert.ToString(ViewState["SelectedEmployeeId"]);
        string userType = Convert.ToString(ViewState["SelectedUserType"]);
        string newPassword = txtNewPassword.Text ?? string.Empty;
        string confirmPassword = txtConfirmPassword.Text ?? string.Empty;

        if (string.IsNullOrWhiteSpace(employeeId) || !IsValidUserType(userType))
        {
            pnlUserDetails.Visible = false;
            ShowMessage("Please search and select a user again before resetting the password.", true);
            return;
        }

        if (!IsStrongPassword(newPassword))
        {
            ShowMessage("Password must be 8-20 characters and contain at least one uppercase letter, one lowercase letter, one number and one special character.", true);
            return;
        }

        if (!string.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
        {
            ShowMessage("New password and confirm password do not match.", true);
            return;
        }

        string salt = Convert.ToString(ConfigurationManager.AppSettings["SecurityKey"]);
        if (string.IsNullOrEmpty(salt))
        {
            ShowMessage("Password security configuration is missing. Please contact the system administrator.", true);
            return;
        }

        try
        {
            // Match the existing Login.aspx flow exactly:
            // browser hash = SHA512(SHA512(plain password) + SecurityKey)
            // stored hash  = SHA512(browser hash + SecurityKey)
            string firstHash = hashcodegenerate.GetSHA512(newPassword);
            string browserHash = hashcodegenerate.GetSHA512(firstHash + salt);
            string storedPasswordHash = hashcodegenerate.GetSHA512(browserHash + salt);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TYPE", SqlDbType.VarChar, 3) { Value = userType },
                new SqlParameter("@EMPLOYEE_ID", SqlDbType.NVarChar, 50) { Value = employeeId },
                new SqlParameter("@PASSWORD", SqlDbType.VarChar, 128) { Value = storedPasswordHash },
                new SqlParameter("@MODIFIED_BY", SqlDbType.NVarChar, 50) { Value = Convert.ToString(Session["EMPLOYEE_ID"]) }
            };

            object result = SQLHelper.ExecuteScalar(
                SQLHelper.GetConnString(), CommandType.StoredProcedure,
                ResetProcedure, parameters);

            if (string.Equals(Convert.ToString(result), "Success", StringComparison.OrdinalIgnoreCase))
            {
                txtNewPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
                ViewState.Remove("SelectedEmployeeId");
                ViewState.Remove("SelectedUserType");
                pnlUserDetails.Visible = false;
                ShowMessage("Password updated successfully.", false);
            }
            else
            {
                ShowMessage("Password was not updated. The user record may have changed; please search again.", true);
            }
        }
        catch (Exception)
        {
            ShowMessage("Unable to reset the password right now. Please contact the system administrator.", true);
        }
    }

    private bool IsStrongPassword(string password)
    {
        return !string.IsNullOrEmpty(password) &&
               Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).{8,20}$");
    }

    private void ClearMessage()
    {
        lblMessage.Text = string.Empty;
        lblMessage.ForeColor = System.Drawing.Color.Empty;
    }

    private void ShowMessage(string message, bool isError)
    {
        lblMessage.Text = message;
        lblMessage.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx", true);
    }
}
