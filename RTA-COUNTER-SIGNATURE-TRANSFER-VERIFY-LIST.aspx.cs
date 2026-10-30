using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.IO;
using System.Configuration;
using System.Text;

public partial class RTA_COUNTER_SIGNATURE_TRANSFER_VERIFY_LIST : System.Web.UI.Page
{
    public string FillInterStatePermitList { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["USER_LABEL"]) == "RTA")
        {
            divMark.Visible = true;

        }
        else
        {
            divMark.Visible = false;
        }
        if (!IsPostBack)
        {
           
            

        }
    }
   
    



    protected void btnRun_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            try
            {
                String Status = ddlStatus.SelectedValue;
                String PermitType = ddlType.SelectedValue;

                sCmd.CommandText = "RTA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", Status);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PermitType);
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@MODE", "LISTAPPLIED");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0];


                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);
                        string q1 = CryptorEngine.Encrypt("REGNO=" + Convert.ToString(dr["REGISTRATION_NUMBER"]), true);

                        sb.Append("<tr>");
                        sb.Append("<td>" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["MOBILE_NUMBER"]) + "</td>");
                        sb.Append("<td><a  href=\"javascript:fnRegDetail('" + q1 + "');\">" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</a></td>");
                        sb.Append("<td>" + Convert.ToString(dr["ROUTE"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["FEE"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["PAYMENT_STATUS"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["FEE_PAY_TRANSACTION_ID"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["VALID_FROM"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["APPLICATION_PENDING_ON"]) + "</td>");
                       
                       
                            sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                        
                        if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                        {
                            if (Convert.ToString(Session["USER_LABEL"]) == "RTA")
                            {
                                sb.Append("<td></td>");
                            }
                            else
                            {
                                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnAction('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "','" + Convert.ToString(dr["APPLICATION_STATUS"]) + "','" + Convert.ToString(dr["USER_LABEL"]) + "','" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]) + "','" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]) + "','" + Convert.ToString(dr["RTA_VERIFY_COMMENTS"]) + "');\">Action</a></td>");
                            }
                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }
                        sb.Append("<td>" + Convert.ToString(dr["MEETING_MARKED"]) + "</td>");
                        sb.Append("</tr>");
                    }


                    FillInterStatePermitList = sb.ToString();
                    tblISPermit.Visible = true;
                }
                else
                {
                    tblISPermit.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('No Data Found');", true);
                }
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');TableDatatablesScroller.init();", true);

            }
            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');", true);


            }
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "RTA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", CryptorEngine.Decrypt(txtDelKey.Value, true));
                sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", ddlReplaceAction.Value);
                sCmd.Parameters.AddWithValue("@VERIFY_COMMENTS", txtCommentsReject.Text.ToString());
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@MODE", "ACTION");
                Object obj = sCmd.ExecuteScalar();
                string msg = obj.ToString().Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + msg + "');window.location ='RTA-COUNTER-SIGNATURE-TRANSFER-VERIFY-LIST.aspx'", true);
            }
            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');window.location ='RTA-COUNTER-SIGNATURE-TRANSFER-VERIFY-LIST.aspx'", true);
            }
        }
    }
    protected void btnStaMeetingSubmit_Click(object sender, EventArgs e)
    {
        string AppId = staData.Value;



        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        SqlTransaction objTrans = null;

        string AppId1 = staData.Value.Substring(2, staData.Value.Length - 2);
        staData.Value = "0";
        var dt = DateTime.Now;
        string uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
        momFileUpload.SaveAs(Server.MapPath("~/JHT/RTAUploadedCSFiles/") + "TRANSFER_PERMIT_MOM_" + uniqueFileNamPrfix + "_" + Path.GetFileName(momFileUpload.FileName));
        string Filename = "TRANSFER_PERMIT_MOM_" + uniqueFileNamPrfix + "_" + Path.GetFileName(momFileUpload.FileName);

        string[] AppID_Arr = AppId1.Split('~');
        string AppId_Apporved = string.Empty;
        string AppId_Rejected = string.Empty;

        int indexRejected = 0;

        for (int i = 0; i < AppID_Arr.Length; i++)
        {
            if (AppID_Arr[i] == "R")
            {
                indexRejected = i;
            }
        }

        for (int j = 0; j < indexRejected; j++)
        {
            AppId_Apporved += CryptorEngine.Decrypt(AppID_Arr[j], true) + "~";
        }

        for (int j = indexRejected + 1; j < AppID_Arr.Length; j++)
        {
            AppId_Rejected += CryptorEngine.Decrypt(AppID_Arr[j], true) + "~";
        }



        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();

            SqlCommand sCmd = new SqlCommand();
            sCmd.Transaction = objTrans;
            try
            {
                sCmd.Connection = sCon;
                sCmd.CommandText = "RTA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID_SET", AppId_Apporved);
                sCmd.Parameters.AddWithValue("@APP_ID_SET_REJECTED", AppId_Rejected);
                sCmd.Parameters.AddWithValue("@VERIFY_COMMENTS", txtFinalComments.Text.Trim());
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@FILE_NAME_MOM", Filename);
                sCmd.Parameters.AddWithValue("@MODE", "RTA_ACTION");
                SqlDataAdapter da = new SqlDataAdapter(sCmd);

                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtVerified = ds.Tables[0];
                DataTable dtRejected = ds.Tables[1];

                SMSSender objSmsSend = new SMSSender();
                foreach (DataRow dr in dtVerified.Rows)
                {
                    string mobileNo = Convert.ToString(dr["MOBILE_NUMBER"]);

                    string text = string.Format(@"Your Application No {0} for Registration No {1} has been Verified. Please wait for further processing", Convert.ToString(dr["APPLICATION_NUMBER"]), Convert.ToString(dr["REGISTRATION_NUMBER"]));
                    objSmsSend.Sendsms(mobileNo, text,"jh-permit-verify");
                }
                foreach (DataRow dr in dtRejected.Rows)
                {
                    string mobileNo = Convert.ToString(dr["MOBILE_NUMBER"]);

                    string text = string.Format(@"Your Application No {0} for Registration No {1} has been rejected.", Convert.ToString(dr["APPLICATION_NUMBER"]), Convert.ToString(dr["REGISTRATION_NUMBER"]));
                    objSmsSend.Sendsms(mobileNo, text,"jh-application");
                }

                objTrans.Commit();
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Submitted Successfully');window.location ='RTA-COUNTER-SIGNATURE-TRANSFER-VERIFY-LIST.aspx'", true);

            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location ='RTA-COUNTER-SIGNATURE-TRANSFER-VERIFY-LIST.aspx'", true);

            }
        }



    }                                /// Final Approval and Rejection
    protected void btnMeetingReject_Click(object sender, EventArgs e)
    {
        string AppId1 = staData.Value.Substring(2, staData.Value.Length - 2);
        staData.Value = "0";
        string[] AppID_Arr = AppId1.Split('~');
        string AppId_Apporved = string.Empty;
        string AppId_Rejected = string.Empty;

        int indexRejected = 0;

        for (int i = 0; i < AppID_Arr.Length; i++)
        {
            if (AppID_Arr[i] == "R")
            {
                indexRejected = i;
            }
        }

        for (int j = 0; j < indexRejected; j++)
        {
            AppId_Apporved += CryptorEngine.Decrypt(AppID_Arr[j], true) + "~";
        }

        for (int j = indexRejected + 1; j < AppID_Arr.Length; j++)
        {
            AppId_Rejected += CryptorEngine.Decrypt(AppID_Arr[j], true) + "~";
        }

        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        SqlTransaction objTrans = null;
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();

            SqlCommand sCmd = new SqlCommand();
            sCmd.Transaction = objTrans;
            try
            {
                sCmd.Connection = sCon;
                sCmd.CommandText = "RTA_COUNTER_SIGNATURE_TRANSFER_APPLICATION_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID_SET", AppId_Apporved);
                sCmd.Parameters.AddWithValue("@APP_ID_SET_REJECTED", AppId_Rejected);
                sCmd.Parameters.AddWithValue("@VERIFY_COMMENTS", txtRejectionComments.Text.Trim());
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));

                sCmd.Parameters.AddWithValue("@MODE", "RTA_MEETING_REJECT");

                SqlDataAdapter da = new SqlDataAdapter(sCmd);

                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dtRejected = ds.Tables[0];
                SMSSender objSmsSend = new SMSSender();
                foreach (DataRow dr in dtRejected.Rows)
                {
                    string mobileNo = Convert.ToString(dr["MOBILE_NUMBER"]);

                    string text = string.Format(@"Your Application No {0} for Registration No {1} has been rejected.", Convert.ToString(dr["APPLICATION_NUMBER"]), Convert.ToString(dr["REGISTRATION_NUMBER"]));
                    objSmsSend.Sendsms(mobileNo, text,"jh-application");
                }
                objTrans.Commit();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Submitted Successfully');window.location ='RTA-COUNTER-SIGNATURE-TRANSFER-VERIFY-LIST.aspx'", true);

            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location ='RTA-COUNTER-SIGNATURE-TRANSFER-VERIFY-LIST.aspx'", true);

            }
        }


    }
}