using QRCoder;
using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Windows.Input;


public partial class STA_CS_ALL_TAX_REPRINT : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        if (!string.IsNullOrEmpty(Request.QueryString["reprint"]))
        {
           
            return;
        }

        if (!IsPostBack)
        {
            tblISPermit.Visible = false;
        }
    }


    protected void btnRun_Click(object sender, EventArgs e)
    {

      

        if (string.IsNullOrWhiteSpace(txtApplicationNo.Text))
        {
            ShowPopup("Please enter Application Number.");
            return;
        }

      

        ConnectionStringSettings cs =
            ConfigurationManager.ConnectionStrings["connectionstring"];

        if (cs == null)
        {
            ShowPopup("Database connection not found.");
            return;
        }

        

        using (SqlConnection con = new SqlConnection(cs.ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_ALL_PRE_PAYMENT_DETAIL", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@APPLICATION_NUMBER", txtApplicationNo.Text.Trim());

            DataTable dt = new DataTable();
            new SqlDataAdapter(cmd).Fill(dt);

            if (dt.Rows.Count == 0)
            {
                tblISPermit.Visible = false;
                ShowPopup("No transactions found.");
                return;
            }

            StringBuilder sb = new StringBuilder();

            foreach (DataRow dr in dt.Rows)
            {

                DateTime txnDate = Convert.ToDateTime(dr["TRANSACTION_ON"]);
                DateTime allowedDate = new DateTime(2026, 3, 31);

                string status = dr["TRN_STATUS"] == DBNull.Value
                    ? ""
                    : dr["TRN_STATUS"].ToString().Trim().ToUpper();

                string appNo = dr["APPLICATION_NUMBER"].ToString();
                string grn = dr["GRN"] == DBNull.Value ? "" : dr["GRN"].ToString();
                string deptTranId = dr["DEPTTRANID"] == DBNull.Value ? "" : dr["DEPTTRANID"].ToString();

                sb.Append("<tr>");
                sb.Append("<td>").Append(appNo).Append("</td>");
                sb.Append("<td>").Append("<br/>").Append(dr["OPERATION_FOR"]).Append("</td>");
                //.Append(dr["TR_TYPE"])
                // ✅ FIX: text-end → text-right
                sb.AppendFormat("<td class='text-right'>₹ {0:N2}</td>", dr["TXN_AMOUNT"]).Append("</td>");

                sb.Append("<td>").Append(Convert.ToDateTime(dr["TRANSACTION_ON"]).ToString("dd-MMM-yyyy hh:mm tt")).Append("</td>");

                // ✅ FIX: badge → label (Bootstrap 3)
                sb.Append("<td>");
                sb.Append(status == "SUCCESS"
                    ? "<span class='label label-success'>SUCCESS</span>"
                    : "<span class='label label-danger'>FAILED</span>");
                sb.Append("</td>");

                sb.Append("<td><b>GRN:</b> ").Append(grn)
                  .Append("<br/><b>Dept Txn:</b> ").Append(deptTranId)
                  .Append("</td>");

                sb.Append("<td>");

                if (status == "SUCCESS" && grn != "" && deptTranId != "")
                {

                    string trnRef = dr["TRN_REF_NO"] == DBNull.Value ? "" : dr["TRN_REF_NO"].ToString();

                    string enc = CryptorEngine.Encrypt(
                        "APP_ID=" + appNo +
                        "&GRN=" + grn +
                        "&DEPTTRANID=" + deptTranId +
                        "&TRN_REF_NO=" + trnRef +
                        "&STATUS=" + status,
                        true);

                    string url = "STA_CS_ALL_TAX_VERIFY.aspx?reprint=" +
                                 HttpUtility.UrlEncode(enc);

                    //sb.Append("<a href='#' onclick=\"openVerify('" + url + "')\" " +
                    //    "class='btn btn-xs btn-primary'>Download Receipt</a>");

                    if (txnDate >= allowedDate)
                    {
                     

                        sb.Append("<a href='#' onclick=\"openVerify('" + url + "')\" " +
                            "class='btn btn-xs btn-primary'>Download Receipt</a>");
                    }
                    else
                    {
                        sb.Append("<button class='btn btn-xs btn-default' disabled " +
                                  "title='Receipt not available for transactions before 31-03-2026'>Not Available</button>");
                    }
                }
                else
                {
                    sb.Append("<span class='text-muted'>N/A</span>");
                }

                sb.Append("</td>");
                sb.Append("</tr>");
            }

            csTransactionList.InnerHtml = sb.ToString();
            tblISPermit.Visible = true;

            ScriptManager.RegisterStartupScript(
                this, GetType(), "dt", "initDataTable();", true);
        }
    }


    private void ShowPopup(string msg)
    {
        string cleanMsg = msg.Replace("'", "")
                             .Replace("\r", "")
                             .Replace("\n", "");

        string script = "$('#errorModal .modal-body').text('" + cleanMsg + "');" +
                        "$('#errorModal').modal('show');";

        if (ScriptManager.GetCurrent(this.Page) != null &&
            ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "pop", script, true);
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "pop", script, true);
        }
    }

}
