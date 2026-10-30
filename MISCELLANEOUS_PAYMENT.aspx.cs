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

public partial class MISCELLANEOUS_PAYMENT : System.Web.UI.Page
{
    public string FillList { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        txtApplicationNumber.MaxLength = 120;
        txtMobileNumber.MaxLength = 10;
        txtOTP.MaxLength = 4;

        if (!IsPostBack)
        {
            DivMobileValidate.Visible = false;
            DivOTPMatch.Visible = false;
            DivMiscPayment.Visible = false; // ✅ NEW
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        HFTableName.Value = "";
        txtApplicationNumber.Value = "";

        DivMobileValidate.Visible = false;
        DivOTPMatch.Visible = false;
        DivMiscPayment.Visible = false; // ✅ NEW

        txtMobileNumber.Value = "";
        txtOTP.Value = "";

        txtOTP.Disabled = false;
        btnVerify.Enabled = true;
        txtMobileNumber.Disabled = false;
        btnOTP.Enabled = true;
        txtApplicationNumber.Disabled = false;

        btnSearch.Enabled = true;

        lblMessage.Text = "";
        lblMessage.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string conn = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        using (SqlConnection con = new SqlConnection(conn))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_VIEW_APPLICATION_STATUS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            string appNumber = txtApplicationNumber.Value.Trim();

            if (appNumber.Contains("(PTT)"))
            {
                cmd.Parameters.AddWithValue("@APPLICATION_NUMBER", appNumber);
                cmd.Parameters.AddWithValue("@MODE", "CS_APPLICATION_NUMBER");
            }
            else
            {
                cmd.Parameters.AddWithValue("@APPLICATION_NUMBER", appNumber);
                cmd.Parameters.AddWithValue("@MODE", "VERIFY_APPLICATION_NUMBER");
            }

            DataTable dt = new DataTable();
            new SqlDataAdapter(cmd).Fill(dt);

            if (dt.Rows.Count == 0)
            {
                lblMessage.Text = "No data found";
                lblMessage.Visible = true;
                return;
            }

            if (dt.Columns[0].ColumnName == "MSG")
            {
                lblMessage.Text = dt.Rows[0]["MSG"].ToString();
                lblMessage.Visible = true;
            }
            else
            {
                HFTableName.Value = dt.Rows[0]["TABLE_NAME"].ToString();
                HFPermitNo.Value = dt.Columns.Contains("PERMIT_NUMBER") ? dt.Rows[0]["PERMIT_NUMBER"].ToString() : "";

                DivMobileValidate.Visible = true;
                txtApplicationNumber.Disabled = true;
                btnSearch.Enabled = false;

                lblMessage.Visible = false;
            }
        }
    }
    protected void btnOTP_Click(object sender, EventArgs e)
    {
        string conn = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        using (SqlConnection con = new SqlConnection(conn))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_VIEW_APPLICATION_STATUS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@APPLICATION_NUMBER", txtApplicationNumber.Value.Trim());
            cmd.Parameters.AddWithValue("@PERMIT_NUMBER", HFPermitNo.Value);
            cmd.Parameters.AddWithValue("@TABLE_NAME", HFTableName.Value);
            cmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNumber.Value.Trim());
            cmd.Parameters.AddWithValue("@MODE", "VERIFY_MOBILE_NUMBER");

            var otp = cmd.ExecuteScalar();

            if (otp.ToString() == "MOBILE NUMBER NOT FOUND")
            {
                lblMessage.Text = "Mobile number not match";
                lblMessage.Visible = true;
            }
            else
            {
                txtMobileNumber.Disabled = true;
                btnOTP.Enabled = false;

                DivOTPMatch.Visible = true;

                SMSSender objsms = new SMSSender();
                objsms.Sendsms(txtMobileNumber.Value.Trim(), otp.ToString(), "1307161527893297291");
            }
        }
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Response.Redirect("Application_number.aspx");
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        string conn = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        using (SqlConnection con = new SqlConnection(conn))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_VIEW_APPLICATION_STATUS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@APPLICATION_NUMBER", txtApplicationNumber.Value.Trim());
            cmd.Parameters.AddWithValue("@TABLE_NAME", HFTableName.Value);
            cmd.Parameters.AddWithValue("@PERMIT_NUMBER", HFPermitNo.Value);
            cmd.Parameters.AddWithValue("@OTP", txtOTP.Value.Trim());
            cmd.Parameters.AddWithValue("@MODE", "VERIFY_OTP");

            DataTable dt = new DataTable();
            new SqlDataAdapter(cmd).Fill(dt);

            if (dt.Rows.Count == 0)
            {
                lblMessage.Text = "Invalid OTP";
                lblMessage.Visible = true;
                return;
            }

            // ✅ STATUS VALIDATION (YOUR REQUIREMENT)
            string status = dt.Rows[0]["APPLICATION_STATUS"].ToString();

            if (status == "Rejected" || status == "WITHDRAW" || status == "Objection")
            {
                lblMessage.Text = "Payment not allowed for this application status";
                lblMessage.Visible = true;
                return;
            }

            // ✅ SHOW MISC PAYMENT UI
            DivMiscPayment.Visible = true;
            lblMessage.Visible = false;
        }
    }

    protected void btnMiscPay_Click(object sender, EventArgs e)
    {
        string amount = txtAmount.Value.Trim();
        string reason = ddlReason.SelectedValue;

        if (reason == "OTHER")
        {
            reason = txtOtherReason.Value.Trim();
        }

        if (string.IsNullOrEmpty(amount) || Convert.ToDecimal(amount) <= 0)
        {
            lblMessage.Text = "Enter valid amount";
            lblMessage.Visible = true;
            return;
        }

        if (string.IsNullOrEmpty(reason))
        {
            lblMessage.Text = "Select reason";
            lblMessage.Visible = true;
            return;
        }

        string appNo = txtApplicationNumber.Value.Trim();
        string appId = "0"; // 🔥 you can fetch from DB if needed
        string ownerName = "USER";

        string rand = DateTime.Now.Ticks.ToString();

        string encrypted = PayGetWayDTO.MakeEncryptedData(
            "JHTD",
            "004100102040101",
            ownerName,
            rand,
            amount,
            appNo,
            "NA",
            "MISCELLANEOUS PAYMENT", // ✅ KEY
            appId,
            reason,
            "PRJ",
            "PRJSES026",
            "sec1234"
        );

        Response.Redirect("PaymentRequest.aspx?q=" +
            HttpUtility.UrlEncode(encrypted, Response.ContentEncoding));
    }
}