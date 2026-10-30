using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEra.JHT.Infrastructure;

public partial class RTA_MODERATOR_USER_MANAGEMENT : System.Web.UI.Page
{
    private int PageSize
    {
        get
        {
            int ps;
            if (int.TryParse(hdnPageSize.Value, out ps) && ps > 0) return ps;
            return 10;
        }
    }

    private int CurrentPage
    {
        get
        {
            int cp;
            if (int.TryParse(hdnCurrentPage.Value, out cp) && cp > 0) return cp;
            return 1;
        }
        set
        {
            hdnCurrentPage.Value = value.ToString();
        }
    }

    private int TotalRecords
    {
        get
        {
            int tr;
            if (int.TryParse(hdnTotalRecords.Value, out tr)) return tr;
            return 0;
        }
        set
        {
            hdnTotalRecords.Value = value.ToString();
        }
    }

    private int TotalPages
    {
        get
        {
            if (TotalRecords <= 0) return 1;
            return (int)Math.Ceiling((double)TotalRecords / PageSize);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CheckAuthorization())
        {
            return;
        }

        if (!IsPostBack)
        {
            // Search must be EMPTY on initial load
            txtSearch.Text = string.Empty;
            if (ddlStatusFilter.Items.FindByValue("ALL") != null) ddlStatusFilter.SelectedValue = "ALL";
            BindMasterDropdowns();
            if (ddlRTOFilter.Items.FindByValue("0") != null) ddlRTOFilter.SelectedValue = "0";
            CurrentPage = 1;
            LoadUserList();
        }
    }

    /// <summary>
    /// Server-side authorization check.
    /// Only USER_ROLE = Moderator AND USER_TYPE = RTA can access this page.
    /// </summary>
    private bool CheckAuthorization()
    {
        string empId = Convert.ToString(Session["EMPLOYEE_ID"]);
        string empName = Convert.ToString(Session["EMPLOYEE_NAME"]);
        string userRole = Convert.ToString(Session["USER_ROLE"]);
        string userType = Convert.ToString(Session["USER_TYPE"]);

        if (string.IsNullOrEmpty(empId) || string.IsNullOrEmpty(empName) || userType != "RTA" || userRole != "Moderator")
        {
            Response.Redirect("Login.aspx");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Binds RTO, Department, and Designation dropdowns using the existing
    /// RTA_SP_MSTR_LIST stored procedure (verified pattern from RTA-USER-SETUP-NEW.aspx.cs).
    /// RTO data comes from dbo.RTA_RTO_MASTER via the SP's RTO_LIST mode.
    /// Department/Designation come from their respective master tables via DD_DEPARTMENT_LIST/DD_DESIGNATION_LIST modes.
    /// </summary>
    private void BindMasterDropdowns()
    {
        try
        {
            // Bind RTO Dropdowns from dbo.RTA_RTO_MASTER via RTA_SP_MSTR_LIST
            SqlParameter[] pRTO = new SqlParameter[2];
            pRTO[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
            pRTO[1] = new SqlParameter("@MODE", "RTO_LIST");

            using (SqlDataReader drRTO = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", pRTO))
            {
                DataTable dtRTO = new DataTable();
                dtRTO.Load(drRTO);

                // Filter dropdown (with "All" option)
                ddlRTOFilter.DataSource = dtRTO;
                ddlRTOFilter.DataTextField = "RTO_NAME";
                ddlRTOFilter.DataValueField = "RTO_CODE";
                ddlRTOFilter.DataBind();
                ddlRTOFilter.Items.Insert(0, new ListItem("-- All RTO Offices --", "0"));

                // Edit modal dropdown (with "Select" option)
                ddlEditRTO.DataSource = dtRTO;
                ddlEditRTO.DataTextField = "RTO_NAME";
                ddlEditRTO.DataValueField = "RTO_CODE";
                ddlEditRTO.DataBind();
                ddlEditRTO.Items.Insert(0, new ListItem("-- Select RTO Office --", "0"));
            }

            // Bind Department Dropdown (existing DD_DEPARTMENT_LIST mode returns DEPT_ID, DEPT_NAME)
            SqlParameter[] pDept = new SqlParameter[2];
            pDept[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
            pDept[1] = new SqlParameter("@MODE", "DD_DEPARTMENT_LIST");

            using (SqlDataReader drDept = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", pDept))
            {
                DataTable dtDept = new DataTable();
                dtDept.Load(drDept);

                ddlEditDept.DataSource = dtDept;
                ddlEditDept.DataTextField = "DEPT_NAME";
                ddlEditDept.DataValueField = "DEPT_ID";
                ddlEditDept.DataBind();
                ddlEditDept.Items.Insert(0, new ListItem("-- Select Department --", "0"));
            }

            // Bind Designation Dropdown (existing DD_DESIGNATION_LIST mode returns DESG_ID, DESG_NAME)
            SqlParameter[] pDesg = new SqlParameter[2];
            pDesg[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
            pDesg[1] = new SqlParameter("@MODE", "DD_DESIGNATION_LIST");

            using (SqlDataReader drDesg = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", pDesg))
            {
                DataTable dtDesg = new DataTable();
                dtDesg.Load(drDesg);

                ddlEditDesg.DataSource = dtDesg;
                ddlEditDesg.DataTextField = "DESG_NAME";
                ddlEditDesg.DataValueField = "DESG_ID";
                ddlEditDesg.DataBind();
                ddlEditDesg.Items.Insert(0, new ListItem("-- Select Designation --", "0"));
            }
        }
        catch (Exception ex)
        {
            ShowAlert("Error loading master data: " + CleanMessage(ex.Message));
        }
    }

    /// <summary>
    /// Loads user list using server-side pagination via RTA_MODERATOR_USER_LIST_PAGED.
    /// Falls back to RTA_MODERATOR_USER_LIST (unpaged) if the paged SP is not yet installed.
    /// </summary>
    private void LoadUserList()
    {
        try
        {
            string searchText = txtSearch.Text.Trim();
            string statusFilter = ddlStatusFilter.SelectedValue;
            string rtoFilter = ddlRTOFilter.SelectedValue;
            int pageNum = CurrentPage;

            DataSet ds = null;
            bool usedPagedSP = false;

            // Try paged SP first
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@PAGE_NUMBER", pageNum);
                parameters[1] = new SqlParameter("@PAGE_SIZE", PageSize);
                parameters[2] = new SqlParameter("@SEARCH_TEXT", string.IsNullOrEmpty(searchText) ? (object)DBNull.Value : searchText);
                parameters[3] = new SqlParameter("@STATUS_FILTER", (string.IsNullOrEmpty(statusFilter) || statusFilter == "ALL") ? (object)DBNull.Value : statusFilter);
                parameters[4] = new SqlParameter("@RTO_FILTER", (string.IsNullOrEmpty(rtoFilter) || rtoFilter == "0") ? (object)DBNull.Value : rtoFilter);
                parameters[5] = new SqlParameter("@EMPLOYEE_ID", DBNull.Value);

                ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_MODERATOR_USER_LIST_PAGED", parameters);
                usedPagedSP = true;
            }
            catch (Exception exPaged)
            {
                LogError("RTA_MODERATOR_USER_LIST_PAGED", exPaged.Message, exPaged.StackTrace);

                // Fallback to unpaged SP
                try
                {
                    SqlParameter[] altParams = new SqlParameter[4];
                    altParams[0] = new SqlParameter("@EMPLOYEE_ID", DBNull.Value);
                    altParams[1] = new SqlParameter("@SEARCH_TEXT", string.IsNullOrEmpty(searchText) ? (object)DBNull.Value : searchText);
                    altParams[2] = new SqlParameter("@STATUS_FILTER", (string.IsNullOrEmpty(statusFilter) || statusFilter == "ALL") ? (object)DBNull.Value : statusFilter);
                    altParams[3] = new SqlParameter("@RTO_FILTER", (string.IsNullOrEmpty(rtoFilter) || rtoFilter == "0") ? (object)DBNull.Value : rtoFilter);

                    ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_MODERATOR_USER_LIST", altParams);
                    usedPagedSP = false;
                }
                catch (Exception exUnpaged)
                {
                    LogError("RTA_MODERATOR_USER_LIST", exUnpaged.Message, exUnpaged.StackTrace);

                    // Last resort fallback
                    SqlParameter[] fallbackParams = new SqlParameter[2];
                    fallbackParams[0] = new SqlParameter("@EMPLOYEE_ID", DBNull.Value);
                    fallbackParams[1] = new SqlParameter("@MODE", "LIST");
                    ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_USER_MSTR_AED", fallbackParams);
                    usedPagedSP = false;
                }
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                if (usedPagedSP && dt.Columns.Contains("TOTAL_RECORDS"))
                {
                    // TOTAL_RECORDS is returned in every row via COUNT(*) OVER()
                    TotalRecords = Convert.ToInt32(dt.Rows[0]["TOTAL_RECORDS"]);
                }
                else
                {
                    // Unpaged fallback: total = full row count
                    TotalRecords = dt.Rows.Count;
                }

                rptUsers.DataSource = dt;
                rptUsers.DataBind();
            }
            else
            {
                TotalRecords = 0;
                rptUsers.DataSource = null;
                rptUsers.DataBind();
            }

            // Ensure current page is valid
            if (CurrentPage > TotalPages) CurrentPage = TotalPages;
            if (CurrentPage < 1) CurrentPage = 1;

            UpdatePaginationUI();
        }
        catch (Exception ex)
        {
            LogError("LoadUserList", ex.Message, ex.StackTrace);
            ShowAlert("Error fetching user list: " + CleanMessage(ex.Message));
        }
    }

    /// <summary>
    /// Builds pagination controls: "Showing X to Y of Z users" and page number buttons.
    /// </summary>
    private void UpdatePaginationUI()
    {
        int totalRecs = TotalRecords;
        int currentPage = CurrentPage;
        int totalPages = TotalPages;
        int pageSize = PageSize;

        // Update info label: "Showing 1-10 of 127 users"
        if (totalRecs > 0)
        {
            int startRec = ((currentPage - 1) * pageSize) + 1;
            int endRec = currentPage * pageSize;
            if (endRec > totalRecs) endRec = totalRecs;
            lblPaginationInfo.Text = string.Format("Showing {0}-{1} of {2} users", startRec, endRec, totalRecs);
        }
        else
        {
            lblPaginationInfo.Text = "No records found.";
        }

        // Enable/disable First, Previous, Next, Last buttons
        btnFirst.Enabled = (currentPage > 1);
        btnPrev.Enabled = (currentPage > 1);
        btnNext.Enabled = (currentPage < totalPages);
        btnLast.Enabled = (currentPage < totalPages);

        // Dim disabled buttons visually
        btnFirst.CssClass = btnFirst.Enabled ? "btn btn-sm default" : "btn btn-sm default disabled";
        btnPrev.CssClass = btnPrev.Enabled ? "btn btn-sm default" : "btn btn-sm default disabled";
        btnNext.CssClass = btnNext.Enabled ? "btn btn-sm default" : "btn btn-sm default disabled";
        btnLast.CssClass = btnLast.Enabled ? "btn btn-sm default" : "btn btn-sm default disabled";

        // Build page number buttons with ellipsis
        List<PageItem> pages = new List<PageItem>();
        int maxVisiblePages = 5;

        if (totalPages <= maxVisiblePages + 2)
        {
            // Show all pages
            for (int i = 1; i <= totalPages; i++)
            {
                pages.Add(new PageItem { PageNumber = i, DisplayText = i.ToString(), IsCurrentPage = (i == currentPage), IsEllipsis = false });
            }
        }
        else
        {
            // Always show page 1
            pages.Add(new PageItem { PageNumber = 1, DisplayText = "1", IsCurrentPage = (1 == currentPage), IsEllipsis = false });

            int startPage = Math.Max(2, currentPage - 2);
            int endPage = Math.Min(totalPages - 1, currentPage + 2);

            // Ensure we show at least maxVisiblePages in the middle
            if (endPage - startPage < maxVisiblePages - 1)
            {
                if (startPage == 2)
                    endPage = Math.Min(totalPages - 1, startPage + maxVisiblePages - 1);
                else
                    startPage = Math.Max(2, endPage - maxVisiblePages + 1);
            }

            // Left ellipsis
            if (startPage > 2)
            {
                pages.Add(new PageItem { PageNumber = 0, DisplayText = "...", IsCurrentPage = false, IsEllipsis = true });
            }

            // Middle pages
            for (int i = startPage; i <= endPage; i++)
            {
                pages.Add(new PageItem { PageNumber = i, DisplayText = i.ToString(), IsCurrentPage = (i == currentPage), IsEllipsis = false });
            }

            // Right ellipsis
            if (endPage < totalPages - 1)
            {
                pages.Add(new PageItem { PageNumber = 0, DisplayText = "...", IsCurrentPage = false, IsEllipsis = true });
            }

            // Always show last page
            pages.Add(new PageItem { PageNumber = totalPages, DisplayText = totalPages.ToString(), IsCurrentPage = (totalPages == currentPage), IsEllipsis = false });
        }

        rptPager.DataSource = pages;
        rptPager.DataBind();
    }

    // =========================================================================
    // EDIT USER
    // =========================================================================

    private void PopulateUserForEdit(string empId)
    {
        try
        {
            DataSet ds = null;
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@EMPLOYEE_ID", empId);
                ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_MODERATOR_USER_DETAILS", parameters);
            }
            catch
            {
                // Fallback to existing RTA_USER_MSTR_AED
                SqlParameter[] altParams = new SqlParameter[2];
                altParams[0] = new SqlParameter("@EMPLOYEE_ID", empId);
                altParams[1] = new SqlParameter("@MODE", "LISTBYID");
                ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_USER_MSTR_AED", altParams);
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                hdnEditEmpId.Value = Convert.ToString(dr["EMPLOYEE_ID"]);
                txtEditEmpId.Text = Convert.ToString(dr["EMPLOYEE_ID"]);
                txtEditLoginId.Text = Convert.ToString(dr["LOGIN_ID"]);
                txtEditUserRole.Text = Convert.ToString(dr["USER_ROLE"]);
                txtEditEmpName.Text = Convert.ToString(dr["EMPLOYEE_NAME"]);
                txtEditEmail.Text = Convert.ToString(dr["EMAIL_ID"]);
                txtEditMobile.Text = Convert.ToString(dr["MOBILE_NUMBER"]);

                string status = Convert.ToString(dr["STATUS"]);
                ddlEditStatus.SelectedValue = (status.Equals("Active", StringComparison.OrdinalIgnoreCase) || status.Equals("A", StringComparison.OrdinalIgnoreCase)) ? "Active" : "Inactive";

                // Department: try DEPT_ID first (from paged SP), then DEPARTMENT (raw column)
                string deptVal = dr.Table.Columns.Contains("DEPT_ID") ? Convert.ToString(dr["DEPT_ID"]) : (dr.Table.Columns.Contains("DEPARTMENT") ? Convert.ToString(dr["DEPARTMENT"]) : "");
                // Designation: try DESG_ID first, then DESIGNATION
                string desgVal = dr.Table.Columns.Contains("DESG_ID") ? Convert.ToString(dr["DESG_ID"]) : (dr.Table.Columns.Contains("DESIGNATION") ? Convert.ToString(dr["DESIGNATION"]) : "");

                SetDropdownValue(ddlEditDept, deptVal);
                SetDropdownValue(ddlEditDesg, desgVal);

                // RTO: use DEPARTMENT column which stores RTO_CODE
                string rtoCode = Convert.ToString(dr["RTO_CODE"]);
                SetDropdownValue(ddlEditRTO, rtoCode);

                SetDropdownValue(ddlEditUserLevel, Convert.ToString(dr["USER_LEVEL"]));

                ScriptManager.RegisterStartupScript(this, GetType(), "ShowEdit", "showEditModal();", true);
            }
        }
        catch (Exception ex)
        {
            ShowAlert("Error loading user details: " + CleanMessage(ex.Message));
        }
    }

    // =========================================================================
    // CHANGE PASSWORD MODAL
    // =========================================================================

    private void PrepareChangePasswordModal(string argument)
    {
        // Argument format: EMPLOYEE_ID|EMPLOYEE_NAME|LOGIN_ID
        string[] parts = argument.Split('|');
        string empId = parts.Length > 0 ? parts[0] : "";
        string empName = parts.Length > 1 ? parts[1] : empId;
        string loginId = parts.Length > 2 ? parts[2] : "";

        hdnPwdEmpId.Value = empId;
        lblPwdEmpName.Text = Server.HtmlEncode(empName);
        lblPwdLoginId.Text = Server.HtmlEncode(loginId);
        txtNewPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPwd", "showPasswordModal();", true);
    }

    // =========================================================================
    // STATUS TOGGLE
    // =========================================================================

    private void PrepareStatusToggleModal(string argument)
    {
        string[] parts = argument.Split('|');
        if (parts.Length >= 3)
        {
            string empId = parts[0];
            string currentStatus = parts[1];
            string empName = parts[2];

            string targetStatus = currentStatus.Equals("Active", StringComparison.OrdinalIgnoreCase) ? "Inactive" : "Active";
            string actionText = targetStatus.Equals("Active", StringComparison.OrdinalIgnoreCase) ? "activate" : "deactivate";

            hdnStatusEmpId.Value = empId;
            hdnTargetStatus.Value = targetStatus;
            lblStatusConfirmPrompt.Text = "Are you sure you want to <strong>" + actionText + "</strong> user <strong>" + Server.HtmlEncode(empName) + "</strong> (ID: " + empId + ")?";

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowStatus", "showStatusModal();", true);
        }
    }

    // =========================================================================
    // SAVE EDIT
    // =========================================================================

    protected void btnSaveEdit_Click(object sender, EventArgs e)
    {
        if (!CheckAuthorization()) return;

        try
        {
            string empId = hdnEditEmpId.Value.Trim();
            string empName = txtEditEmpName.Text.Trim();
            string email = txtEditEmail.Text.Trim();
            string mobile = txtEditMobile.Text.Trim();

            if (string.IsNullOrEmpty(empName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mobile))
            {
                ShowAlert("Please fill in all mandatory fields (Name, Email, Mobile).");
                ScriptManager.RegisterStartupScript(this, GetType(), "ReShowEdit", "showEditModal();", true);
                return;
            }

            SqlParameter[] parameters = new SqlParameter[10];
            parameters[0] = new SqlParameter("@EMPLOYEE_ID", empId);
            parameters[1] = new SqlParameter("@EMPLOYEE_NAME", empName);
            parameters[2] = new SqlParameter("@EMAIL_ID", email);
            parameters[3] = new SqlParameter("@MOBILE_NUMBER", mobile);
            parameters[4] = new SqlParameter("@DEPARTMENT", ddlEditDept.SelectedValue);
            parameters[5] = new SqlParameter("@DESIGNATION", ddlEditDesg.SelectedValue);
            parameters[6] = new SqlParameter("@RTO_CODE", ddlEditRTO.SelectedValue);
            parameters[7] = new SqlParameter("@USER_LEVEL", ddlEditUserLevel.SelectedValue);
            parameters[8] = new SqlParameter("@STATUS", ddlEditStatus.SelectedValue);
            parameters[9] = new SqlParameter("@MODIFIED_BY", Convert.ToString(Session["EMPLOYEE_ID"]));

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_MODERATOR_USER_UPDATE", parameters);

            ScriptManager.RegisterStartupScript(this, GetType(), "HideEdit", "hideEditModal();", true);
            ShowAlert("User details updated successfully.");
            LoadUserList(); // Reload current page with current filters
        }
        catch (Exception ex)
        {
            ShowAlert("Unable to update user details: " + CleanMessage(ex.Message));
        }
    }

    // =========================================================================
    // CONFIRM STATUS CHANGE
    // =========================================================================

    protected void btnConfirmStatus_Click(object sender, EventArgs e)
    {
        if (!CheckAuthorization()) return;

        try
        {
            string empId = hdnStatusEmpId.Value.Trim();
            string targetStatus = hdnTargetStatus.Value.Trim();

            if (string.IsNullOrEmpty(empId) || string.IsNullOrEmpty(targetStatus))
            {
                ShowAlert("Invalid status change parameters.");
                return;
            }

            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@EMPLOYEE_ID", empId);
            parameters[1] = new SqlParameter("@STATUS", targetStatus);
            parameters[2] = new SqlParameter("@MODIFIED_BY", Convert.ToString(Session["EMPLOYEE_ID"]));

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_MODERATOR_USER_STATUS_UPDATE", parameters);

            ScriptManager.RegisterStartupScript(this, GetType(), "HideStatus", "hideStatusModal();", true);
            ShowAlert("User status updated successfully.");
            LoadUserList(); // Preserve current page/filters
        }
        catch (Exception ex)
        {
            ShowAlert("Unable to update user status: " + CleanMessage(ex.Message));
        }
    }

    // =========================================================================
    // SAVE PASSWORD
    // Uses same hashing as Login.aspx.cs: hashcodegenerate.GetSHA512(password + saltKey)
    // Salt key from ConfigurationManager.AppSettings["SecurityKey"] (default: "JhTransport")
    // =========================================================================

    protected void btnSavePassword_Click(object sender, EventArgs e)
    {
        if (!CheckAuthorization()) return;

        try
        {
            string empId = hdnPwdEmpId.Value.Trim();
            string newPwd = txtNewPassword.Text.Trim();
            string confirmPwd = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(newPwd) || newPwd.Length < 6)
            {
                ShowAlert("New password must be at least 6 characters long.");
                ScriptManager.RegisterStartupScript(this, GetType(), "ReShowPwd", "showPasswordModal();", true);
                return;
            }

            if (newPwd != confirmPwd)
            {
                ShowAlert("New password and confirm password do not match.");
                ScriptManager.RegisterStartupScript(this, GetType(), "ReShowPwd", "showPasswordModal();", true);
                return;
            }

            // ---------------------------------------------------------------
            // PASSWORD HASHING - MUST MATCH THE EXACT LOGIN MECHANISM
            // ---------------------------------------------------------------
            // The login system uses a 3-layer SHA512 hash chain:
            //
            // Login.aspx (CLIENT-SIDE JavaScript):
            //   var hash = hex_sha512(hex_sha512(PASSWORD) + salt);
            //   document.getElementById('hfpwd').value = hash;
            //
            // Login.aspx.cs (SERVER-SIDE C#):
            //   string Tpass = hashcodegenerate.GetSHA512(Upass + Saltval);
            //   // Upass = hfpwd.Value = client-side hash
            //
            // Stored value = SHA512( SHA512( SHA512(plaintext) + salt ) + salt )
            //
            // RTA-CHANGE-PASSWORD.aspx uses the same pattern (client pre-hash + server hash).
            // RTA-USER-SETUP-NEW.aspx also uses the same pattern.
            //
            // Since we are changing the password SERVER-SIDE ONLY (no client
            // JavaScript), we must replicate ALL 3 layers here in C#.
            // ---------------------------------------------------------------
            string saltKey = Convert.ToString(Session["salt"]);
            if (string.IsNullOrEmpty(saltKey)) saltKey = ConfigurationManager.AppSettings["SecurityKey"];
            if (string.IsNullOrEmpty(saltKey)) saltKey = "JhTransport";

            // Layer 1: SHA512(plaintext)  — equivalent to JavaScript hex_sha512(PASSWORD)
            string layer1 = hashcodegenerate.GetSHA512(newPwd).ToLower();
            // Layer 2: SHA512(layer1 + salt) — equivalent to JavaScript hex_sha512(hex_sha512(PASSWORD) + salt)
            string layer2 = hashcodegenerate.GetSHA512(layer1 + saltKey).ToLower();
            // Layer 3: SHA512(layer2 + salt) — equivalent to server-side hashcodegenerate.GetSHA512(hfpwd.Value + salt)
            string hashedPassword = hashcodegenerate.GetSHA512(layer2 + saltKey);

            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@EMPLOYEE_ID", empId);
            parameters[1] = new SqlParameter("@PASSWORD", hashedPassword);
            parameters[2] = new SqlParameter("@MODIFIED_BY", Convert.ToString(Session["EMPLOYEE_ID"]));

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_MODERATOR_USER_PASSWORD_UPDATE", parameters);

            ScriptManager.RegisterStartupScript(this, GetType(), "HidePwd", "hidePasswordModal();", true);
            ShowAlert("Password changed successfully.");
            LoadUserList();
        }
        catch (Exception ex)
        {
            ShowAlert("Unable to update password: " + CleanMessage(ex.Message));
        }
    }

    // =========================================================================
    // FILTER / SEARCH HANDLERS
    // =========================================================================

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (!CheckAuthorization()) return;
        CurrentPage = 1; // Reset to page 1 when searching
        LoadUserList();
    }

    protected void btnResetFilter_Click(object sender, EventArgs e)
    {
        if (!CheckAuthorization()) return;
        txtSearch.Text = string.Empty;
        if (ddlStatusFilter.Items.FindByValue("ALL") != null) ddlStatusFilter.SelectedValue = "ALL";
        if (ddlRTOFilter.Items.FindByValue("0") != null) ddlRTOFilter.SelectedValue = "0";
        CurrentPage = 1;
        LoadUserList();
    }

    // =========================================================================
    // REPEATER ITEM COMMANDS (Edit, Password, Toggle Status)
    // =========================================================================

    protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (!CheckAuthorization()) return;

        string arg = Convert.ToString(e.CommandArgument);
        if (e.CommandName == "EditUser")
        {
            PopulateUserForEdit(arg);
        }
        else if (e.CommandName == "ChangePwd" || e.CommandName == "ChangePassword")
        {
            PrepareChangePasswordModal(arg);
        }
        else if (e.CommandName == "ToggleStatus")
        {
            PrepareStatusToggleModal(arg);
        }
    }

    // =========================================================================
    // PAGINATION HANDLERS
    // =========================================================================

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        if (!CheckAuthorization()) return;
        CurrentPage = 1;
        LoadUserList();
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        if (!CheckAuthorization()) return;
        int page = CurrentPage - 1;
        if (page < 1) page = 1;
        CurrentPage = page;
        LoadUserList();
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (!CheckAuthorization()) return;
        int page = CurrentPage + 1;
        if (page > TotalPages) page = TotalPages;
        CurrentPage = page;
        LoadUserList();
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        if (!CheckAuthorization()) return;
        CurrentPage = TotalPages;
        LoadUserList();
    }

    protected void rptPager_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (!CheckAuthorization()) return;
        if (e.CommandName == "GoToPage")
        {
            int page;
            if (int.TryParse(Convert.ToString(e.CommandArgument), out page) && page > 0)
            {
                CurrentPage = page;
                LoadUserList();
            }
        }
    }

    // =========================================================================
    // HELPER METHODS
    // =========================================================================

    private void LogError(string procedureName, string message, string stackTrace)
    {
        try
        {
            string empId = Convert.ToString(Session["EMPLOYEE_ID"]);
            System.Diagnostics.Trace.WriteLine(string.Format("[RTA Moderator User Mgmt Error] SP: {0} | User: {1} | Exception: {2} | Stack: {3}", procedureName, empId, message, stackTrace));
        }
        catch
        {
            // Ignore logging exceptions
        }
    }

    private void SetDropdownValue(DropDownList ddl, string value)
    {
        if (ddl != null && !string.IsNullOrEmpty(value))
        {
            ListItem item = ddl.Items.FindByValue(value);
            if (item != null)
            {
                ddl.SelectedValue = value;
            }
        }
    }

    private void ShowAlert(string msg)
    {
        string script = "alert('" + CleanMessage(msg) + "');";
        ScriptManager.RegisterStartupScript(this, GetType(), "UserMgmtAlert", script, true);
    }

    private string CleanMessage(string msg)
    {
        if (string.IsNullOrEmpty(msg)) return string.Empty;
        return msg.Replace("'", "\\'").Replace("\r", " ").Replace("\n", " ");
    }
}

/// <summary>
/// Simple data class for pagination repeater binding.
/// </summary>
public class PageItem
{
    public int PageNumber { get; set; }
    public string DisplayText { get; set; }
    public bool IsCurrentPage { get; set; }
    public bool IsEllipsis { get; set; }
}
