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

public partial class MakePayment : System.Web.UI.Page
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
                  
                     if (dt.Columns.Contains("PERMIT_NUMBER"))
                        {
                            HFRegNo.Value = dt.Rows[0]["PERMIT_NUMBER"].ToString();
                        }
                        else
                        {
                            HFRegNo.Value = "";
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
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", HFRegNo.Value);
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
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + txtApplicationNumber.Value.Trim() + "')'", true);
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + HFTableName.Value + "');'", true);
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + txtOTP.Value.Trim() + "');window.location ='PermitHomePage.aspx'", true);
                sCmd.CommandText = "SP_VIEW_APPLICATION_STATUS";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", txtApplicationNumber.Value.Trim());
                sCmd.Parameters.AddWithValue("@TABLE_NAME", HFTableName.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", HFRegNo.Value);
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


                }

                else
                {
                    StringBuilder sb = new StringBuilder();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string q = string.Empty;
                        string x = Convert.ToString(ds.Tables[0].Rows[0]["TABLE_TYPE"]);
                        if (x.Contains("_OBJECTION"))
                        {

                        }
                        else if (x.Contains("STA_CS_COMM_HISTORY"))
                        {
                            q = CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"]) + "&TABLE=" + x, true);
                        }
                        else
                        {
                            q = CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"].ToString()) + "&TYPEID="
                               + Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_TYPE_ID"].ToString()) + "&OPTIONID=" +
                               Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_TYPE_OPTION_SERVICE_ID"].ToString()) + "&TABLE=" + x, true);
                        }

                        //----------------------------------- STA ------------------------------------------------------//


                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_CS_COMM_HISTORY")
                        {

                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");

                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() != "Paid")
                            {
                                string feesCs = ds.Tables[0].Rows[0]["FEES"].ToString();
                                string applicationNumber = ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString();
                                string OwnerName = ds.Tables[0].Rows[0]["OWNER_NAME"].ToString();
                                string appID = ds.Tables[0].Rows[0]["APP_ID"].ToString();

                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", OwnerName, RandNo, feesCs,
                                applicationNumber, "NA", "STA APPLICATION CSUPLOAD", appID, "STA-APPLICATION", "PRJ", "PRJSES026", "sec1234");
                                EncryptedParameter.Value = EncryptedStr;

                                string href = "PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding);
                                sb.Append("Your Application has been Verified. Please make payment of Rs. " + feesCs + " /- from following link .....");
                                sb.Append("<p><a class='btn btn-info' href='" + href + "'>Make Payment</a></p><br />");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Approved")
                            {
                                sb.Append("Your Application has been Approved");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "WITHDRAW")
                            {
                                sb.Append("Application has been Withdrawl");
                            }

                        }

                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_PERMIT_MASTER")
                        {
                            string R = CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_NUMBER"].ToString()), true);
                            //if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Issued")
                            //{
                            //sb.Append("Your Permit certificate  has been Issued and you can collect your certificate from Department");

                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Issued" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes")
                            {
                                sb.Append("<br />");
                                sb.Append("<br /><a class='btn btn-outline dark' href='STA-PERMIT-APPLICATION-UPDATE.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Update Route Timing and/or Documents</a></td>");

                            }
                            else
                            {
                                sb.Append("Your can download your certificates from following link.<br /><br />");
                                sb.Append("<a class='btn btn-outline dark' target='_blank' href='STAPermitIssuedReceipt.aspx?q=" + HttpUtility.UrlEncode(R, Response.ContentEncoding) + "' >Issued  Permit</a></td>");
                                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a class='btn btn-outline dark' target='_blank' href='STAForwardingLetter.aspx?q=" + HttpUtility.UrlEncode(R, Response.ContentEncoding) + "' >Forwarding Letter</a></td>");
                                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;<a class='btn btn-outline dark' target='_blank' href='STATimeTable.aspx?q=" + HttpUtility.UrlEncode(R, Response.ContentEncoding) + "' >Time Table</a></td>");

                            }

                            //}
                            //if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Renewed")
                            //{
                            //    sb.Append("You can collect your Renewed certificate from Department");  
                            //}
                            //if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Replaced")
                            //{
                            //    sb.Append("You can collect your certificate for vehicle replacement from Department");
                            //}
                            //if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Duplicate Issued")
                            //{
                            //    sb.Append("Your duplicate Permit certificate  has been Issued and you can collect your certificate from Department");
                            //}
                            //if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Surrender")
                            //{
                            //    sb.Append("Your Surrender certificate has been Issued and and you can collect your certificate from Department");
                            //}
                            //if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Transfered")
                            //{
                            //    sb.Append("Your Transfer certificate has been Issued and and you can collect your certificate from Department");
                            //}

                        }
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_COUNTER_SIGNATURE_MASTER_HDR")
                        {
                            string R = CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_NUMBER"].ToString()), true);
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Issued")
                            {
                                sb.Append("Your Counter Signature certificate  has been Issued and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Renewed")
                            {
                                sb.Append("You can collect your Renewed certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Replaced")
                            {
                                sb.Append("You can collect your certificate for vehicle replacement from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Duplicate Issued")
                            {
                                sb.Append("Your duplicate Permit certificate  has been Issued and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Surrender")
                            {
                                sb.Append("Your Surrender certificate has been Issued and and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Transfered")
                            {
                                sb.Append("Your Transfer certificate has been Issued and and you can collect your certificate from Department");
                            }

                        }

                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='STA-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }

                            else if ((ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes") ||
                                (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes") ||
                                    (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Objection" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes")
                                )
                            {
                                sb.Append("Your Application has been Applied You can Update your route timing and/or documents....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='STA-PERMIT-APPLICATION-UPDATE.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >UPDATE</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" || ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Objection")
                            {
                                sb.Append("Objection is raised in Your Application please Wait for the notice.....");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ", Surcharge Fee: " + ds.Tables[0].Rows[0]["SURCHARGE_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                               Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                               Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA PERMIT ISSUE", ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");
                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Paid")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 

                            }
                        }
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_COUNTER_SIGNATURE_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='STA_COUNTER_SIGNATURE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if ((ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes") ||
                            (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes") ||
                                (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Objection" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes")
                            )
                            {
                                sb.Append("Your Application has been Applied You can Update your documents....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='STA-CS-APPLICATION-UPDATE.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >UPLOAD DOCUMENT</a></br></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" || ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Objection")
                            {
                                sb.Append("Objection is raised in Your Application please Wait for the notice.....");
                                 

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Verified but Tax Payment is due. Please make payment from following link ....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='STA_COUNTER_SIGNATURE_TAX_PAY.aspx' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 

                            }
                        }

                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_PERMIT_OBJECTION_APPLICATION_HDR") // OBJECTION APPLICATION
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA_PERMIT_OBJECTION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + ds.Tables[0].Rows[0]["APP_ID"].ToString() + "&TBL_MODE=" + Convert.ToString("PERMIT"), true), Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                                // sb.Append("<td><a class='btn btn-outline dark' href='STA_PERMIT_OBJECTION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Closed")
                            {
                                sb.Append("Your Application has been Closed for further query please contact department");
                            }
                            else
                            {
                                sb.Append("Please wait for information. ");
                                 
                            }

                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_COUNTER_SIGNATURE_OBJECTION_APPLICATION_HDR") // OBJECTION APPLICATION
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA_PERMIT_OBJECTION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + ds.Tables[0].Rows[0]["APP_ID"].ToString() + "&TBL_MODE=" + Convert.ToString("CS"), true), Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 

                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Closed")
                            {
                                sb.Append("Your Application has been Closed for further query please contact department");
                            }
                            else
                            {
                                sb.Append("Please wait for information. ");
                                 
                            }

                        }

                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_VEHICLE_REPLACE_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA-VEHICLE-REPLACE-APPLICATION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA_COUNTER_SIGNATURE_REPLACEMENT_OF_VEHICLE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_TRANSFER_PERMIT_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA-TRANSFER-OF-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_RENEWAL_OF_PERMIT_APPLICATION_HDR_NEW")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA-RENEWAL-OF-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            //else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            //{
                            //    sb.Append("Your Application has been under process ....");
                            //     

                            //}
                            else if ((ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes"))
                            {
                                sb.Append("Your Application has been Applied You can Update your route timing and/or documents....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='STA-RENEW-PERMIT-APPLICATION-UPDATE.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "'>UPLOAD DOCUMENTS</a></br></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Renewal Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ", Surcharge Fee: " + ds.Tables[0].Rows[0]["SURCHARGE_FEE_AMOUNT"].ToString() + ", Late Fee: " + ds.Tables[0].Rows[0]["LATE_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                                Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                                Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA RENEWAL PERMIT ISSUE", ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");
                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_COUNTER_SIGNATURE_RENEWAL_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA_COUNTER_SIGNATURE_RENEWAL_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_DUPLICATE_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA-DUPLICATE-OF-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_COUNTER_SIGNATURE_DUPLICATE_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA_COUNTER_SIGNATURE_DUPLICATE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_PERMIT_ROUTE_EXTENSION_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA-PERMIT-APPLICATION-ROUTE-EXTENSION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_SURRENDER_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA-SURRENDER-OF-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "STA_COUNTER_SIGNATURE_SURRENDER_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='STA_COUNTER_SIGNATURE_SURRENDER_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }

                        ///////----------------------------------- RTA Permit Master------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_PERMIT_MASTER")
                        {
                            string R = CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_NUMBER"].ToString()), true);
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Issued")
                            {
                                sb.Append("Your Permit certificate  has been Issued and you can collect your certificate from Department");

                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Renewed")
                            {
                                sb.Append("You can collect your Renewed certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Replaced")
                            {
                                sb.Append("You can collect your certificate for vehicle replacement from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Duplicate Issued")
                            {
                                sb.Append("Your duplicate Permit certificate  has been Issued and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Surrender")
                            {
                                sb.Append("Your Surrender certificate has been Issued and and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Transfered")
                            {
                                sb.Append("Your Transfer certificate has been Issued and and you can collect your certificate from Department");
                            }

                        }
                        ///////----------------------------------- RTA Counter signature Permit Master------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_COUNTER_SIGNATURE_MASTER_HDR")
                        {
                            string R = CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_NUMBER"].ToString()), true);
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Issued")
                            {
                                sb.Append("Your Permit certificate  has been Issued and you can collect your certificate from Department");

                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Renewed")
                            {
                                sb.Append("Your can collect your Renewed certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Replaced")
                            {
                                sb.Append("Your can collect your certificate for vehicle replacement from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Duplicate Issued")
                            {
                                sb.Append("Your duplicate Permit certificate  has been Issued and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Surrender")
                            {
                                sb.Append("Your Surrender certificate has been Issued and and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Transfered")
                            {
                                sb.Append("Your Transfer certificate has been Issued and and you can collect your certificate from Department");
                            }

                        }
                        ///////----------------------------------- RTA Goods Permit Master------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_GOODS_PERMIT_MASTER")
                        {
                            string R = CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_NUMBER"].ToString()), true);
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Issued")
                            {
                                sb.Append("Your Permit certificate  has been Issued and you can collect your certificate from Department");

                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Renewed")
                            {
                                sb.Append("Your can collect your Renewed certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Replaced")
                            {
                                sb.Append("You can collect your certificate for vehicle replacement from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Duplicate Issued")
                            {
                                sb.Append("Your duplicate Permit certificate  has been Issued and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Surrender")
                            {
                                sb.Append("Your Surrender certificate has been Issued and and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Transfered")
                            {
                                sb.Append("Your Transfer certificate has been Issued and and you can collect your certificate from Department");
                            }

                        }
                        ///////----------------------------------- RTA Contract Permit Master------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_CONTRACT_PERMIT_MASTER")
                        {
                            string R = CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_NUMBER"].ToString()), true);
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Issued")
                            {
                                sb.Append("Your Permit certificate  has been Issued and you can collect your certificate from Department");

                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Renewed")
                            {
                                sb.Append("Your can collect your Renewed certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Replaced")
                            {
                                sb.Append("You can collect your certificate for vehicle replacement from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Duplicate Issued")
                            {
                                sb.Append("Your duplicate Permit certificate  has been Issued and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Surrender")
                            {
                                sb.Append("Your Surrender certificate has been Issued and and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Transfered")
                            {
                                sb.Append("Your Transfer certificate has been Issued and and you can collect your certificate from Department");
                            }

                        }
                        ///////----------------------------------- RTA Auto Permit Master------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_AUTO_PERMIT_MASTER")
                        {
                            string R = CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_NUMBER"].ToString()), true);
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Issued")
                            {
                                sb.Append("Your Permit certificate  has been Issued and you can collect your certificate from Department");

                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Renewed")
                            {
                                sb.Append("Your can collect your Renewed certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Replaced")
                            {
                                sb.Append("You can collect your certificate for vehicle replacement from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Duplicate Issued")
                            {
                                sb.Append("Your duplicate Permit certificate  has been Issued and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Surrender")
                            {
                                sb.Append("Your Surrender certificate has been Issued and and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Transfered")
                            {
                                sb.Append("Your Transfer certificate has been Issued and and you can collect your certificate from Department");
                            }

                        }
                        ///////----------------------------------- RTA School Bus Permit Master------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_SCHOOL_BUS_PERMIT_MASTER")
                        {
                            string R = CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_NUMBER"].ToString()), true);
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Issued")
                            {
                                sb.Append("Your Permit certificate  has been Issued and you can collect your certificate from Department");

                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Renewed")
                            {
                                sb.Append("Your can collect your Renewed certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Replaced")
                            {
                                sb.Append("You can collect your certificate for vehicle replacement from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Duplicate Issued")
                            {
                                sb.Append("Your duplicate Permit certificate  has been Issued and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Surrender")
                            {
                                sb.Append("Your Surrender certificate has been Issued and and you can collect your certificate from Department");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Transfered")
                            {
                                sb.Append("Your Transfer certificate has been Issued and and you can collect your certificate from Department");
                            }

                        }
                        //////// //----------------------------------- RTA Permit Application------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='RTA_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }

                            else if ((ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes") ||
                                (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes") ||
                                    (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Objection" && ds.Tables[0].Rows[0]["USER_PERMISSION"].ToString() == "Yes")
                                )
                            {
                                sb.Append("Your Application has been Applied You can Update your time ....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='RTA-PERMIT-APPLICATION-UPDATE.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >UPDATE</a></td>");
                                 

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" || ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Objection")
                            {
                                sb.Append("Objection is raised in Your Application please Wait for the notice.....");
                                 

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ", Surcharge Fee: " + ds.Tables[0].Rows[0]["SURCHARGE_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                // string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                                //Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                                //Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA PERMIT ISSUE", 
                                //ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");

                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"]), Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "RTA PERMIT ISSUE",
                                Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"]), "RTA-PERMIT", Convert.ToString(ds.Tables[0].Rows[0]["TREASCODE"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["IFMSOFFICECODE"]), "sec1234");

                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Paid")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 

                            }
                        }


                        //////// //----------------------------------- RTA Goods Permit Application------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_GOODS_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='RTA-GOODS-PERMIT-APPLICATION-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" || ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");

                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                // string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                                //Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                                //Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA PERMIT ISSUE", 
                                //ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");

                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"]), Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "RTA PERMIT ISSUE GOODS",
                                Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"]), "RTA-PERMIT-GOODS", Convert.ToString(ds.Tables[0].Rows[0]["TREASCODE"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["IFMSOFFICECODE"]), "sec1234");

                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Paid")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 

                            }
                        }

                        //////////----------------------------------- RTA Contract Permit Application------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_CONTRACT_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='RTA_CONTRACT_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                // string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                                //Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                                //Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA PERMIT ISSUE", 
                                //ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");

                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"]), Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "RTA PERMIT ISSUE CONTRACT",
                                Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"]), "RTA-PERMIT-CONTRACT", Convert.ToString(ds.Tables[0].Rows[0]["TREASCODE"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["IFMSOFFICECODE"]), "sec1234");

                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Paid")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 

                            }
                        }
                        //////////----------------------------------- RTA Auto Permit Application------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_AUTO_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='RTA_AUTO_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }


                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" || ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                // string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                                //Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                                //Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA PERMIT ISSUE", 
                                //ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");

                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"]), Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "RTA PERMIT ISSUE AUTO",
                                Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"]), "RTA-PERMIT-AUTO", Convert.ToString(ds.Tables[0].Rows[0]["TREASCODE"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["IFMSOFFICECODE"]), "sec1234");

                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Paid")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 

                            }
                        }
                        //////////----------------------------------- RTA School Bus Permit Application------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_SCHOOL_BUS_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='RTA_SCHOOL_BUS_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" || ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                // string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                                //Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                                //Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA PERMIT ISSUE", 
                                //ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");

                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"]), Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "RTA PERMIT ISSUE SCHOOL BUS",
                                Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"]), "RTA-PERMIT-SCHOOL-BUS", Convert.ToString(ds.Tables[0].Rows[0]["TREASCODE"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["IFMSOFFICECODE"]), "sec1234");

                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Paid")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                        }
                        //////////----------------------------------- RTA Counter Signature Application------------------------------------------------------//
                        if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_COUNTER_SIGNATURE_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<br /><a class='btn btn-outline dark' href='RTA_COUNTER_SIGNATURE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }


                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied" || ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Published")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Objection")
                            {
                                sb.Append("Objection is raised in Your Application please Wait for the notice.....");
                                 

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")

                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Issued")
                            {
                                sb.Append("Your Permit certificate  has been Issued and you can collect your certificate from Department");

                            }
                        }
                        //////////----------------------------------- RTA Permit Objection Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_PERMIT_OBJECTION_APPLICATION_HDR") // OBJECTION APPLICATION
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_PERMIT_OBJECTION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + ds.Tables[0].Rows[0]["APP_ID"].ToString() + "&TBL_MODE=" + Convert.ToString("PERMIT"), true), Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                                // sb.Append("<td><a class='btn btn-outline dark' href='STA_PERMIT_OBJECTION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Closed")
                            {
                                sb.Append("Your Application has been Closed for further query please contact department");
                            }
                            else
                            {
                                sb.Append("Please wait for information. ");
                                 
                            }

                        }
                        //////////----------------------------------- RTA Counter Signature Objection Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_COUNTER_SIGNATURE_OBJECTION_APPLICATION_HDR") // OBJECTION APPLICATION
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_PERMIT_OBJECTION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + ds.Tables[0].Rows[0]["APP_ID"].ToString() + "&TBL_MODE=" + Convert.ToString("CS"), true), Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 

                            }
                            if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Closed")
                            {
                                sb.Append("Your Application has been Closed for further query please contact department");
                            }
                            else
                            {
                                sb.Append("Please wait for information. ");
                                 
                            }

                        }
                        //////////----------------------------------- RTA Vehicle replace Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_VEHICLE_REPLACE_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_PERMIT_REPLACEMENT_OF_VEHICLE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                        }
                        //////////----------------------------------- RTA Goods Vehicle replace Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_GOODS_VEHICLE_REPLACE_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_GOODS_REPLACEMENT_OF_VEHICLE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                        }
                        //////////----------------------------------- RTA Contract Vehicle replace Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_CONTRACT_VEHICLE_REPLACE_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_CONTRACT_REPLACEMENT_OF_VEHICLE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                        }
                        //////////----------------------------------- RTA Auto Vehicle replace Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_AUTO_VEHICLE_REPLACE_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_AUTO_REPLACEMENT_OF_VEHICLE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                        }
                        //////////----------------------------------- RTA School Bus Vehicle replace Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_SCHOOL_BUS_VEHICLE_REPLACE_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_SCHOOL_BUS_REPLACEMENT_OF_VEHICLE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                        }
                        //////////----------------------------------- RTA Counter Signature Vehicle replace Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_COUNTER_SIGNATURE_VEHICLE_REPLACE_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_COUNTER_SIGNATURE_REPLACEMENT_OF_VEHICLE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Replaced")
                            {
                                sb.Append("Your can collect your certificate for vehicle replacement from Department");

                            }

                        }
                        //////////----------------------------------- RTA Transfer Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_TRANSFER_PERMIT_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_PERMIT_TRANSFER_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }



                        }
                        //////////----------------------------------- RTA Goods Transfer Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_GOODS_TRANSFER_PERMIT_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_GOODS_TRANSFER_OF_PERMIT_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA Contract Transfer Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_CONTRACT_TRANSFER_PERMIT_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_CONTRACT_TRANSFER_OF_PERMIT_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA Auto Transfer Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_AUTO_TRANSFER_PERMIT_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_AUTO_TRANSFER_OF_PERMIT_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");

                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA School Bus Transfer Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_SCHOOL_BUS_TRANSFER_PERMIT_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_SCHOOL_BUS_TRANSFER_OF_PERMIT_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA Counter Signature Transfer Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_HDR") // VEHICLE REPLACE ON PERMIT
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Transfered")
                            {
                                sb.Append("Your Transfer certificate has been Issued and and you can collect your certificate from Department");
                            }


                        }
                        //////////----------------------------------- RTA Renewal of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_RENEWAL_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_PERMIT_RENEWAL_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Renewal Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ", Surcharge Fee: " + ds.Tables[0].Rows[0]["SURCHARGE_FEE_AMOUNT"].ToString() + ", Late Fee: " + ds.Tables[0].Rows[0]["LATE_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                               Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                               Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "RTA RENEWAL PERMIT ISSUE", ds.Tables[0].Rows[0]["APP_ID"].ToString(), "RTA-PERMIT", Convert.ToString(ds.Tables[0].Rows[0]["TREASCODE"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["IFMSOFFICECODE"]), "sec1234");
                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }



                        }
                        //////////----------------------------------- RTA Goods Renewal of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_GOODS_RENEWAL_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA-GOODS-RENEWAL-OF-PERMIT-APPLICATION-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Renewal Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ", Late Fee: " + ds.Tables[0].Rows[0]["LATE_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                // string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                                //Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                                //Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA PERMIT ISSUE", 
                                //ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");

                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"]), Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "RTA RENEWAL PERMIT ISSUE GOODS",
                                Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"]), "RTA-PERMIT-GOODS", Convert.ToString(ds.Tables[0].Rows[0]["TREASCODE"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["IFMSOFFICECODE"]), "sec1234");

                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA Contract Renewal of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_CONTRACT_RENEWAL_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA-CONTRACT-RENEWAL-OF-PERMIT-APPLICATION-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Renewal Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ", Late Fee: " + ds.Tables[0].Rows[0]["LATE_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                // string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                                //Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                                //Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA PERMIT ISSUE", 
                                //ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");

                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"]), Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "RTA RENEWAL PERMIT ISSUE CONTRACT",
                                Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"]), "RTA-PERMIT-CONTRACT", Convert.ToString(ds.Tables[0].Rows[0]["TREASCODE"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["IFMSOFFICECODE"]), "sec1234");

                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA Auto Renewal of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_AUTO_RENEWAL_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA-AUTO-RENEWAL-OF-PERMIT-APPLICATION-RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Renewal Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ", Late Fee: " + ds.Tables[0].Rows[0]["LATE_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                // string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                                //Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                                //Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA PERMIT ISSUE", 
                                //ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");

                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"]), Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "RTA RENEWAL PERMIT ISSUE AUTO",
                                Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"]), "RTA-PERMIT-AUTO", Convert.ToString(ds.Tables[0].Rows[0]["TREASCODE"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["IFMSOFFICECODE"]), "sec1234");

                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA School Bus Renewal of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_SCHOOL_BUS_RENEWAL_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_SCHOOL_BUS_RENEWAL_OF_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified" && ds.Tables[0].Rows[0]["PERMIT_PAYMENT_STATUS"].ToString() == "Unpaid")
                            {

                                sb.Append("Your Application has been Verified but Renewal Permit Payment( Permit Fee: " + ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"].ToString() + ", Late Fee: " + ds.Tables[0].Rows[0]["LATE_FEE_AMOUNT"].ToString() + ") is due. Please make permit payment from following link ....");
                                String RandNo = Convert.ToString(DateTime.Now.Ticks);
                                // string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101",
                                //Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"].ToString()), RandNo, Convert.ToString(ds.Tables[0].Rows[0]["TOTAL_FEE_AMOUNT"].ToString()),
                                //Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"].ToString()), "NA", "STA PERMIT ISSUE", 
                                //ds.Tables[0].Rows[0]["APP_ID"].ToString(), "STA-PERMIT", "PRJ", "PRJSES026", "sec1234");

                                string EncryptedStr = PayGetWayDTO.MakeEncryptedData("JHTD", "004100102040101", Convert.ToString(ds.Tables[0].Rows[0]["OWNER_NAME"]),
                                RandNo, Convert.ToString(ds.Tables[0].Rows[0]["PERMIT_FEE_AMOUNT"]), Convert.ToString(ds.Tables[0].Rows[0]["APPLICATION_NUMBER"]),
                                "NA", "RTA RENEWAL PERMIT ISSUE SCHOOL BUS",
                                Convert.ToString(ds.Tables[0].Rows[0]["APP_ID"]), "RTA-PERMIT-SCHOOL-BUS", Convert.ToString(ds.Tables[0].Rows[0]["TREASCODE"]),
                                Convert.ToString(ds.Tables[0].Rows[0]["IFMSOFFICECODE"]), "sec1234");

                                EncryptedParameter.Value = EncryptedStr;

                                sb.Append("<td><a class='btn btn-outline dark' href='PaymentRequest.aspx?q=" + HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA Renewal of Counter Signature Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_COUNTER_SIGNATURE_RENEWAL_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_COUNTER_SIGNATURE_RENEWAL_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Renewed")
                            {
                                sb.Append("Your can collect your Renewed certificate from Department");
                            }


                        }
                        //////////----------------------------------- RTA Duplicate of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_DUPLICATE_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_PERMIT_DUPLICATE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }

                        }

                        //////////----------------------------------- RTA Goods Duplicate of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_GOODS_DUPLICATE_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_GOODS_DUPLICATE_OF_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }

                        }

                        //////////----------------------------------- RTA Contract Duplicate of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_CONTRACT_DUPLICATE_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_CONTRACT_DUPLICATE_OF_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }

                        }
                        //////////----------------------------------- RTA Auto Duplicate of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_AUTO_DUPLICATE_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_AUTO_DUPLICATE_OF_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");

                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }

                        }
                        //////////----------------------------------- RTA School Bus Duplicate of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_SCHOOL_BUS_DUPLICATE_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_SCHOOL_BUS_DUPLICATE_OF_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }

                        }
                        //////////----------------------------------- RTA Counter Signature Duplicate Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_COUNTER_SIGNATURE_DUPLICATE_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_COUNTER_SIGNATURE_DUPLICATE_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Duplicate Issued")
                            {
                                sb.Append("Your duplicate Permit certificate  has been Issued and you can collect your certificate from Department");
                            }


                        }
                        //////////----------------------------------- RTA Surrender of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_SURRENDER_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_PERMIT_SURRENDER_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA Goods Surrender of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_GOODS_SURRENDER_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_GOODS_SURRENDER_OF_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 
                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {

                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA Contract Surrender of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_CONTRACT_SURRENDER_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_CONTRACT_SURRENDER_OF_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA Auto Surrender of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_AUTO_SURRENDER_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_AUTO_SURRENDER_OF_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA School Bus Surrender of permit Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_SCHOOL_BUS_SURRENDER_OF_PERMIT_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_SCHOOL_BUS_SURRENDER_OF_PERMIT_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }


                        }
                        //////////----------------------------------- RTA  Surrender of Counter Signature Application------------------------------------------------------//
                        else if (ds.Tables[0].Rows[0]["TABLE_TYPE"].ToString() == "RTA_COUNTER_SIGNATURE_SURRENDER_APPLICATION_HDR")
                        {
                            if (ds.Tables[0].Rows[0]["PAYMENT_STATUS"].ToString() == "Unpaid")
                            {
                                sb.Append("Your Application has been Applied but Payment is due. Please make payment from following link ....");
                                sb.Append("<td><a class='btn btn-outline dark' href='RTA_COUNTER_SIGNATURE_SURRENDER_APPLICATION_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q, Response.ContentEncoding) + "' >Make Payment</a></td>");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Applied")
                            {
                                sb.Append("Your Application has been under process ....");
                                 

                            }

                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Rejected")
                            {
                                sb.Append("Your Application has been rejected for further query please contact department");
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Verified")
                            {
                                sb.Append("Your Application has been Verified. Please wait for next information.");
                                 
                            }
                            else if (ds.Tables[0].Rows[0]["APPLICATION_STATUS"].ToString() == "Surrender")
                            {
                                sb.Append("Your Surrender certificate has been Issued and and you can collect your certificate from Department");
                            }


                        }




                        FillList = sb.ToString();





                    }

                    lblMessage.Visible = false;
                    txtOTP.Disabled = true;
                    btnVerify.Enabled = false;
                    DivList.Visible = true;
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
}