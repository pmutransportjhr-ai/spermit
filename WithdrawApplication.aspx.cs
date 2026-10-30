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

public partial class WithdrawApplication : System.Web.UI.Page
{
    public string FillList { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        txtApplicationNumber.MaxLength = 120;
        txtMobileNumber.MaxLength = 10;
        txtOTP.MaxLength = 4;


        if (!IsPostBack)
        {
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        HFTableName.Value = "";
        txtApplicationNumber.Value = "";

        DivMobileValidate.Visible = false;
        DivOTPMatch.Visible = false;
        txtMobileNumber.Value = "";
        txtOTP.Value = "";
        txtOTP.Disabled = false;
        btnVerify.Enabled = true;
        txtMobileNumber.Disabled = false;
        btnOTP.Enabled = true;
        txtApplicationNumber.Disabled = false;

        btnSearch.Enabled = true;
        lblMessage.Text = "";
        lblMessage.Visible = true;
        DivList.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {
                sCmd.CommandText = "SP_VIEW_APPLICATION_STATUS";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                string appNumber = txtApplicationNumber.Value.Trim();
                if (appNumber.Contains("(PTT)"))
                {
                    appNumber = txtApplicationNumber.Value.Trim();
                    sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", appNumber);
                    sCmd.Parameters.AddWithValue("@MODE", "CS_APPLICATION_NUMBER");
                }
                else
                {
                    sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", appNumber);
                    sCmd.Parameters.AddWithValue("@MODE", "VERIFY_APPLICATION_NUMBER");
                }
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Columns[0].ColumnName.ToString() == "MSG")
                {
                    lblMessage.Text = dt.Rows[0]["MSG"].ToString(); ;



                    lblMessage.Visible = true;
                    DivMobileValidate.Visible = false;
                    DivOTPMatch.Visible = false;
                    DivList.Visible = false;
                }

                else
                {
                    HFTableName.Value = dt.Rows[0]["TABLE_NAME"].ToString();

                    ScriptManager.RegisterClientScriptBlock(
                     this,
                     this.GetType(),
                     "tableNameAlert",
                     "alert('Table Name: " + HFTableName.Value + "');",
                     true
                 );

                    if (dt.Columns.Contains("PERMIT_NUMBER"))
                    {
                        HFPermitNo.Value = dt.Rows[0]["PERMIT_NUMBER"].ToString();
                    }
                    else
                    {
                        HFPermitNo.Value = "";
                    }
                    lblMessage.Text = "";
                    lblMessage.Visible = false;
                    DivMobileValidate.Visible = true;
                    DivOTPMatch.Visible = false;
                    DivList.Visible = false;
                    txtApplicationNumber.Disabled = true;
                    btnSearch.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

            }
        }


    }
    protected void btnOTP_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {
                sCmd.CommandText = "SP_VIEW_APPLICATION_STATUS";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", txtApplicationNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", HFPermitNo.Value);
                sCmd.Parameters.AddWithValue("@TABLE_NAME", HFTableName.Value);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "VERIFY_MOBILE_NUMBER");
                var obj1 = sCmd.ExecuteScalar();
                if (obj1.ToString() == "MOBILE NUMBER NOT FOUND")
                {
                    lblMessage.Text = "Mobile number not match";
                    txtMobileNumber.Value = "";
                    lblMessage.Visible = true;
                    DivMobileValidate.Visible = true;
                    DivOTPMatch.Visible = false;
                    DivList.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
                }
                else
                {

                    lblMessage.Text = "";
                    lblMessage.Visible = false;
                    txtMobileNumber.Disabled = true;
                    btnOTP.Enabled = false;
                    DivMobileValidate.Visible = true;
                    DivOTPMatch.Visible = true;
                    DivList.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
                    SMSSender objsms = new SMSSender();
                    objsms.Sendsms(txtMobileNumber.Value.Trim(), Convert.ToString(obj1), "1307161527893297291");

                }


            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

            }
        }
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Response.Redirect("Application_number.aspx");
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();

            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;

            try
            {
                sCmd.CommandText = "SP_VIEW_APPLICATION_STATUS";
                sCmd.CommandType = CommandType.StoredProcedure;

                sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", txtApplicationNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@TABLE_NAME", HFTableName.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", HFPermitNo.Value);
                sCmd.Parameters.AddWithValue("@OTP", txtOTP.Value.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "VERIFY_OTP");

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    lblMessage.Text = "OTP not match. Please Click on Clear button and try again.";
                    lblMessage.Visible = true;
                    DivMobileValidate.Visible = false;
                    DivOTPMatch.Visible = false;
                    DivList.Visible = false;
                    return;
                }

                DataRow row = ds.Tables[0].Rows[0];

                string tableType = Convert.ToString(row["TABLE_TYPE"]);
                string status = Convert.ToString(row["APPLICATION_STATUS"]);
                string permitNo = Convert.ToString(row["PERMIT_NUMBER"]);

                string q = CryptorEngine.Encrypt(
                    "APP_ID=" + Convert.ToString(row["APP_ID"]) +
                    "&TABLE=" + tableType, true);

                StringBuilder sb = new StringBuilder();

                //------------------------------------------
                // STATUS DISPLAY
                //------------------------------------------

                string userPermission = row.Table.Columns.Contains("USER_PERMISSION") ? Convert.ToString(row["USER_PERMISSION"]) : "";
                string appId = Convert.ToString(row["APP_ID"]);

                if (status == "Rejected")
                {
                    sb.Append("Your Application has been rejected. Please contact department.");
                }
                else if (status == "WITHDRAW")
                {
                    sb.Append("Your Application has already been withdrawn.");
                }
                else if (status == "Closed")
                {
                    sb.Append("Your Application has been closed. Please contact department for details.");
                }
                else if (status == "Duplicate Issued")
                {
                    sb.Append("Your duplicate permit certificate has been issued.");
                }
                else if (status == "Transfered")
                {
                    sb.Append("Your transfer certificate has been issued.");
                }
                else if (status == "Surrender")
                {
                    sb.Append("Your surrender certificate has been issued.");
                }
                else if (status == "Approved" || status == "Issued")
                {
                    sb.Append("Your Application has been Approved.");

                    if (!string.IsNullOrEmpty(permitNo))
                    {
                        string R = CryptorEngine.Encrypt("PER_NUMBER=" + permitNo, true);

                        sb.Append("<br/><br/>");
                        sb.Append("<a class='btn btn-outline dark' target='_blank' href='STAPermitIssuedReceipt.aspx?q=" +
                                  HttpUtility.UrlEncode(R, Response.ContentEncoding) +
                                  "'>Download Permit</a>");
                    }
                }
                else if (userPermission == "Yes" && (status == "Applied" || status == "Published" || status == "Objection"))
                {
                    sb.Append("Your Application is under process. You can update documents if required.");
                    sb.Append("<br/><br/>");

                    sb.Append("<a class='btn btn-info' href='STA-PERMIT-APPLICATION-UPDATE.aspx?q=" +
                              HttpUtility.UrlEncode(q, Response.ContentEncoding) +
                              "'>Update / Upload Documents</a>");

                    sb.Append("&nbsp;&nbsp;");

                    sb.Append("<a class='btn btn-danger' href='Application_Withdrawal.aspx?q=" +
                              HttpUtility.UrlEncode(q, Response.ContentEncoding) +
                              "'>Withdraw Application</a>");
                }
                else if (status == "Verified")
                {
                    sb.Append("Your Application has been Verified. Please wait for next information.");
                    sb.Append("<br/><br/>");

                    sb.Append("<a class='btn btn-danger' href='Application_Withdrawal.aspx?q=" +
                              HttpUtility.UrlEncode(q, Response.ContentEncoding) +
                              "'>Withdraw Application</a>");
                }
                else if (status == "Applied" || status == "Published" || status == "Objection")
                {
                    sb.Append("Your Application is under process...");
                    sb.Append("<br/><br/>");

                    sb.Append("<a class='btn btn-danger' href='Application_Withdrawal.aspx?q=" +
                              HttpUtility.UrlEncode(q, Response.ContentEncoding) +
                              "'>Withdraw Application</a>");
                }
                else
                {
                    sb.Append("Please wait for further updates.");
                }

                //------------------------------------------
                // FINAL OUTPUT
                //------------------------------------------

                FillList = sb.ToString();

                lblMessage.Visible = false;
                txtOTP.Disabled = true;
                btnVerify.Enabled = false;
                DivList.Visible = true;
            }
            catch (Exception ex)
            {
                String Message = ex.Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");

                ScriptManager.RegisterClientScriptBlock(
                    Page,
                    Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('" + Message + "');window.location ='PermitHomePage.aspx'",
                    true);
            }
        }
    }

  
}