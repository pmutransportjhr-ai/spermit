
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftEra.JHT.Infrastructure;

public partial class STA_COUNTER_SIGNATURE_TAX_PAY : System.Web.UI.Page
{
    public string FillAPPlyRouteList { get; set; }

    protected List<QueryString> QueryStr =
        new List<QueryString>();


    protected void Page_Load(object sender, EventArgs e)
    {
        txtExistRegNo.MaxLength = 20;
        txtChassisLast5.MaxLength = 5;

        Page.Form.Attributes.Add(
            "enctype",
            "multipart/form-data"
        );

        if (!IsPostBack)
        {
            ResetVerificationFlow();
        }
    }


    // ============================================================
    // STEP 1
    // FETCH VEHICLE DETAILS
    // ============================================================

    protected void btnFetchDetails_Click(object sender, EventArgs e)
    {
        string registrationNumber =
            txtExistRegNo.Text.Trim().ToUpper();

        string chassisLast5 =
            txtChassisLast5.Text.Trim().ToUpper();

        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            ShowMessage(
                "Please enter Vehicle Registration Number."
            );

            return;
        }

        if (string.IsNullOrWhiteSpace(chassisLast5) ||
            chassisLast5.Length != 5)
        {
            ShowMessage(
                "Please enter exactly last 5 characters of Chassis Number."
            );

            return;
        }


        string connectionString =
            ConfigurationManager
                .ConnectionStrings["connectionstring"]
                .ToString();


        using (SqlConnection sCon =
               new SqlConnection(connectionString))
        {
            try
            {
                sCon.Open();

                using (SqlCommand sCmd =
                       new SqlCommand(
                           "STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED",
                           sCon))
                {
                    sCmd.CommandType =
                        CommandType.StoredProcedure;

                    sCmd.Parameters.AddWithValue(
                        "@REGISTRATION_NUMBER",
                        registrationNumber
                    );

                    sCmd.Parameters.AddWithValue(
                        "@CHASIS_NUMBER",
                        chassisLast5
                    );

                    sCmd.Parameters.AddWithValue(
                        "@MODE",
                        "CS_VEHICLE_VERIFY_SEARCH"
                    );


                    DataSet ds = new DataSet();

                    using (SqlDataAdapter da =
                           new SqlDataAdapter(sCmd))
                    {
                        da.Fill(ds);
                    }


                    if (ds.Tables.Count == 0 ||
                        ds.Tables[0].Rows.Count == 0)
                    {
                        ShowMessage(
                            "Vehicle details could not be found. Please verify the Registration Number and Chassis Number."
                        );

                        return;
                    }


                    DataTable dt =
                        ds.Tables[0];


                    if (dt.Columns.Contains("MSG"))
                    {
                        ShowMessage(
                            Convert.ToString(
                                dt.Rows[0]["MSG"]
                            )
                        );

                        return;
                    }


                    if (dt.Rows.Count != 1)
                    {
                        ShowMessage(
                            "Multiple vehicle records found. Please contact the Transport Department."
                        );

                        return;
                    }


                    DataRow row =
                        dt.Rows[0];


                    // --------------------------------------------
                    // STORE SERVER-SIDE IDENTIFIERS
                    // --------------------------------------------

                    HiddenField1.Text =
                        Convert.ToString(
                            row["APP_ID"]
                        );

                    HiddenField2.Text =
                        Convert.ToString(
                            row["TBL_TYPE"]
                        );

                    HiddenField3.Text =
                        Convert.ToString(
                            row["PERMIT_NUMBER"]
                        );


                    // --------------------------------------------
                    // DISPLAY ONLY REQUIRED INFORMATION
                    // --------------------------------------------

                    lblVerifyRegNo.Text =
                        Convert.ToString(
                            row["REGISTRATION_NUMBER"]
                        );

                    lblVerifyPermitNo.Text =
                        Convert.ToString(
                            row["PERMIT_NUMBER"]
                        );

                    lblVerifyOwnerName.Text =
                        Convert.ToString(
                            row["OWNER_NAME"]
                        );

                    lblVerifyMobile.Text =
                        Convert.ToString(
                            row["MASKED_MOBILE"]
                        );

                    lblApplicationNumber.Text =
                      Convert.ToString(
                          row["APPLICATION_NUMBER"]
                      );


                    // --------------------------------------------
                    // LOCK SEARCH INPUT
                    // --------------------------------------------

                    txtExistRegNo.Enabled = false;
                    txtChassisLast5.Enabled = false;

                    btnFetchDetails.Enabled = false;


                    // --------------------------------------------
                    // SHOW CONFIRMATION STEP
                    // --------------------------------------------

                    VerificationInputCard.Visible = true;

                    VerificationDetailsPanel.Visible = true;

                    Panel1.Visible = false;

                    chkConsent.Checked = false;

                    btnVerify.Enabled = false;

                    Message.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }


        HideLoading();
    }


    // ============================================================
    // STEP 2
    // CONSENT + VERIFY
    // ============================================================

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        if (!chkConsent.Checked)
        {
            ShowMessage(
                "Please confirm that the fetched vehicle details are correct."
            );

            return;
        }


        string registrationNumber =
            txtExistRegNo.Text.Trim().ToUpper();

        string chassisLast5 =
            txtChassisLast5.Text.Trim().ToUpper();

        string permitNumber =
            HiddenField3.Text.Trim();


        if (string.IsNullOrWhiteSpace(registrationNumber) ||
            string.IsNullOrWhiteSpace(chassisLast5) ||
            chassisLast5.Length != 5 ||
            string.IsNullOrWhiteSpace(permitNumber))
        {
            ShowMessage(
                "Verification session is invalid. Please start again."
            );

            return;
        }


        string connectionString =
            ConfigurationManager
                .ConnectionStrings["connectionstring"]
                .ToString();


        using (SqlConnection sCon =
               new SqlConnection(connectionString))
        {
            try
            {
                sCon.Open();

                using (SqlCommand sCmd =
                       new SqlCommand(
                           "STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED",
                           sCon))
                {
                    sCmd.CommandType =
                        CommandType.StoredProcedure;


                    sCmd.Parameters.AddWithValue(
                        "@REGISTRATION_NUMBER",
                        registrationNumber
                    );

                    sCmd.Parameters.AddWithValue(
                        "@PERMIT_NUMBER",
                        permitNumber
                    );

                    sCmd.Parameters.AddWithValue(
                        "@CHASIS_NUMBER",
                        chassisLast5
                    );

                    sCmd.Parameters.AddWithValue(
                        "@MODE",
                        "CS_VEHICLE_CONSENT_VERIFY"
                    );


                    DataSet ds =
                        new DataSet();

                    using (SqlDataAdapter da =
                           new SqlDataAdapter(sCmd))
                    {
                        da.Fill(ds);
                    }


                    if (ds.Tables.Count == 0 ||
                        ds.Tables[0].Rows.Count == 0)
                    {
                        ShowMessage(
                            "Verification failed. Please start again."
                        );

                        return;
                    }


                    DataTable dt =
                        ds.Tables[0];


                    if (dt.Columns.Contains("MSG"))
                    {
                        ShowMessage(
                            Convert.ToString(
                                dt.Rows[0]["MSG"]
                            )
                        );

                        return;
                    }


                    if (dt.Rows.Count != 1)
                    {
                        ShowMessage(
                            "Vehicle verification failed. Please contact the Transport Department."
                        );

                        return;
                    }


                    DataRow row =
                        dt.Rows[0];


                    // --------------------------------------------
                    // FINAL SERVER-SIDE VALUES
                    // --------------------------------------------

                    HiddenField1.Text =
                        Convert.ToString(
                            row["APP_ID"]
                        );

                    HiddenField2.Text =
                        Convert.ToString(
                            row["TBL_TYPE"]
                        );


                    // --------------------------------------------
                    // LOAD EXISTING PAYMENT DETAILS
                    // --------------------------------------------

                    lblSeatingCap.Text =
                        Convert.ToString(
                            row["SEATING_CAPACITY"]
                        );

                    lblPermitNo.Text =
                        Convert.ToString(
                            row["PERMIT_NUMBER"]
                        );

                    VOName.Text =
                        Convert.ToString(
                            row["OWNER_NAME"]
                        );

                    VORegNo.Text =
                        Convert.ToString(
                            row["REGISTRATION_NUMBER"]
                        );

                    lblsleepr.Text =
                        Convert.ToString(
                            row["SLEEPER_CAPACITY"]
                        );

                    lblvehicletype.Text =
                        Convert.ToString(
                            row["VEHICLE_CLASS_TYPE"]
                        );

                    lblValidFrom.Text =
                        Convert.ToString(
                            row["PERMIT_VALID_FROM"]
                        );

                    lblValidUpto.Text =
                        Convert.ToString(
                            row["PERMIT_VALID_UPTO"]
                        );

                    lblPayFrom.Text =
                        Convert.ToString(
                            row["FEE_TAX_PAID_FROM"]
                        );

                    lblPayUpto.Text =
                        Convert.ToString(
                            row["FEE_TAX_PAID_UP_TO"]
                        );


                    // --------------------------------------------
                    // MOVE TO STEP 3
                    // --------------------------------------------

                    VerificationDetailsPanel.Visible = false;

                    Panel1.Visible = true;

                    VerificationInputCard.Visible = false;

                    chkConsent.Checked = false;

                    btnVerify.Enabled = false;

                    txtExistRegNo.Enabled = false;

                    txtChassisLast5.Enabled = false;

                    Message.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }


        HideLoading();
    }


    // ============================================================
    // CHANGE VEHICLE DETAILS
    // ============================================================

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ResetVerificationFlow();
    }


    // ============================================================
    // CLEAR
    // ============================================================

    protected void btnExistClear_Click(
        object sender,
        EventArgs e)
    {
        ResetVerificationFlow();
    }


    // ============================================================
    // CANCEL
    // ============================================================

    protected void btnCancel_Click(
        object sender,
        EventArgs e)
    {
        Response.Redirect(
            "PermitHomePage.aspx"
        );
    }


    // ============================================================
    // PAYMENT
    // ============================================================

    protected void btnSave_Click(
        object sender,
        EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(HiddenField1.Text) ||
            string.IsNullOrWhiteSpace(HiddenField2.Text))
        {
            ShowMessage(
                "Verification session expired. Please start again."
            );

            return;
        }


        string encryptedData =
            CryptorEngine.Encrypt(
                "APP_ID=" +
                Convert.ToString(
                    HiddenField1.Text
                ) +
                "&TBL_TYPE=" +
                Convert.ToString(
                    HiddenField2.Text
                ),
                true
            );


        ScriptManager.RegisterClientScriptBlock(
            (sender as Control),
            this.GetType(),
            Guid.NewGuid().ToString(),
            "window.location='STA-COUNTER-SIGNATURE-TAX-PAYMENT.aspx?q=" +
            HttpUtility.UrlEncode(
                encryptedData,
                Response.ContentEncoding
            ) +
            "';",
            true
        );
    }


    // ============================================================
    // RESET
    // ============================================================

    protected void btnReset_Click(
        object sender,
        EventArgs e)
    {
        ResetVerificationFlow();
    }


    // ============================================================
    // RESET FLOW
    // ============================================================

    private void ResetVerificationFlow()
    {
        txtExistRegNo.Text = "";
        txtChassisLast5.Text = "";

        txtExistRegNo.Enabled = true;
        txtChassisLast5.Enabled = true;

        btnFetchDetails.Enabled = true;

        VerificationInputCard.Visible = true;

        VerificationDetailsPanel.Visible = false;

        Panel1.Visible = false;

        chkConsent.Checked = false;

        btnVerify.Enabled = false;

        HiddenField1.Text = "";
        HiddenField2.Text = "";
        HiddenField3.Text = "";

        lblVerifyRegNo.Text = "";
        lblVerifyPermitNo.Text = "";
        lblVerifyOwnerName.Text = "";
        lblVerifyMobile.Text = "";

        lblSeatingCap.Text = "";
        lblPermitNo.Text = "";
        VOName.Text = "";
        VORegNo.Text = "";

        lblsleepr.Text = "";
        lblvehicletype.Text = "";

        lblValidFrom.Text = "";
        lblValidUpto.Text = "";

        lblPayFrom.Text = "";
        lblPayUpto.Text = "";

        Message.Text = "";
        Message.Visible = false;
    }


    // ============================================================
    // MESSAGE
    // ============================================================

    private void ShowMessage(string message)
    {
        Message.Text =
            HttpUtility.HtmlEncode(message);

        Message.Visible = true;

        VerificationDetailsPanel.Visible = false;

        Panel1.Visible = false;

        HideLoading();
    }


    // ============================================================
    // EXCEPTION
    // ============================================================

    private void ShowException(Exception ex)
    {
        string message =
            ex.Message
              .Replace("\r", " ")
              .Replace("\n", " ")
              .Replace("'", "");

        ScriptManager.RegisterClientScriptBlock(
            Page,
            Page.GetType(),
            Guid.NewGuid().ToString(),
            "$('#loadingmessage').modal('hide');" +
            "alert('" + message + "');",
            true
        );
    }


    // ============================================================
    // LOADING
    // ============================================================

    private void HideLoading()
    {
        ScriptManager.RegisterClientScriptBlock(
            Page,
            Page.GetType(),
            Guid.NewGuid().ToString(),
            "$('#loadingmessage').modal('hide');",
            true
        );
    }
}

