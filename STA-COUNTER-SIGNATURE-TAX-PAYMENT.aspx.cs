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

public partial class STA_COUNTER_SIGNATURE_TAX_PAYMENT : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(
                    Page,
                    Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'",
                    true
                );
                return;
            }

            try
            {
                String queryString = CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["q"]), true);

                foreach (String str in queryString.Split('&'))
                {
                    QueryString qs = new QueryString();
                    qs.Name = str.Split('=')[0];
                    qs.Value = str.Split('=')[1];
                    QueryStr.Add(qs);
                }

                hdnID.Value = QueryStr.Where(l => l.Name == "APP_ID").FirstOrDefault().Value;
                tblType.Value = QueryStr.Where(l => l.Name == "TBL_TYPE").FirstOrDefault().Value;

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@APP_ID", hdnID.Value);
                parameters[1] = new SqlParameter("@TBL_TYPE", tblType.Value);
                parameters[2] = new SqlParameter("@MODE", "TAX_DETAIL_FOR_COUNTER_SIGNATURE");

                DataSet ds = SQLHelper.ExecuteDataset(
                    SQLHelper.GetConnString(),
                    CommandType.StoredProcedure,
                    "STA_COUNTER_SIGNATURE_APPLICATION_APPROVAL",
                    parameters
                );

                DataTable dt1 = ds.Tables[0];

                if (dt1.Rows.Count > 0)
                {
                    lblApplicationNumber.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    lblApplicantName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    txtRegNo.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    lblMenufecturingDt.Value = dt1.Rows[0]["MANUFACTURING_ON"].ToString();
                    txtAPP_ID.Value = dt1.Rows[0]["APP_ID"].ToString();

                    // Remove Half-Yearly Option - your original logic
                    ddlTaxPeriod.Items.Remove(ddlTaxPeriod.Items[2]);
                }
            }
            catch (Exception ex)
            {
                String msg = ex.Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(
                    Page,
                    Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('" + msg + "');window.location ='PermitHomePage.aspx'",
                    true
                );
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlTaxPeriod.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(
                Page,
                Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('Please Select Period');",
                true
            );
            return;
        }

        if (string.IsNullOrWhiteSpace(TaxAmount.Text))
        {
            ScriptManager.RegisterClientScriptBlock(
                Page,
                Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('Tax Amount cannot be blank');",
                true
            );
            return;
        }

        decimal amount;
        if (!decimal.TryParse(TaxAmount.Text.Trim(), out amount) || amount <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(
                Page,
                Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('Tax Amount must be greater than zero');",
                true
            );
            return;
        }

        ScriptManager.RegisterClientScriptBlock(
            Page,
            Page.GetType(),
            Guid.NewGuid().ToString(),
            "$('#confirm').modal('show');",
            true
        );
    }


   
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            // re-block zero amount before redirect
            decimal amount;
            if (!decimal.TryParse(TaxAmount.Text, out amount) || amount <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(
                    Page, Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('Tax amount cannot be zero. Payment cannot be initiated.');",
                    true
                );
                return;
            }

            if (string.IsNullOrWhiteSpace(EncryptedParameter.Value))
            {
                ScriptManager.RegisterClientScriptBlock(
                    Page, Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('Payment request could not be generated. Please select period again.');",
                    true
                );
                return;
            }

            ScriptManager.RegisterClientScriptBlock(
                (sender as Control),
                this.GetType(),
                Guid.NewGuid().ToString(),
                "window.location ='PaymentRequest.aspx?q=" +
                    HttpUtility.UrlEncode(EncryptedParameter.Value, Response.ContentEncoding) + "'",
                true
            );
        }
        catch (Exception ex)
        {
            String msg = ex.Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(
                Page,
                Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('" + msg + "');window.location ='PermitHomePage.aspx'",
                true
            );
        }
    }



    protected void ddlTaxPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTaxPeriod.SelectedValue == "0")
        {
            Label3.Text = "";
            ScriptManager.RegisterClientScriptBlock(
                Page,
                Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('Please Select Period');",
                true
            );
            return;
        }

        ScriptManager.RegisterClientScriptBlock(
            Page,
            Page.GetType(),
            Guid.NewGuid().ToString(),
            "$('#loadingmessage').modal('show');",
            true
        );

        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {
                sCon.Open();

                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;

                sCmd.Parameters.AddWithValue("@APP_ID", hdnID.Value);
                sCmd.Parameters.AddWithValue("@TAX_PERIOD", ddlTaxPeriod.SelectedValue);
                sCmd.Parameters.AddWithValue("@TBL_TYPE", tblType.Value);
                sCmd.Parameters.AddWithValue("@MODE", "CALCULATE_TAX_AMOUNT_COUNTER_SIGNATURE");

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);

                DataTable dt = ds.Tables[0];

                Label1.Text = dt.Rows[0]["ACTUAL_TAX_AMOUNT"].ToString();
                Label2.Text = dt.Rows[0]["FINE_AMOUNT"].ToString();

                if (tblType.Value == "HDR")
                {
                    Label3.Text = "No of Quarter : " + dt.Rows[0]["NO_PERIOD"].ToString();
                }
                else
                {
                    Label3.Text = "";
                }

                // now enforce zero amount BLOCK
                decimal totalTaxAmount;
                if (!decimal.TryParse(dt.Rows[0]["TOTAL_TAX_AMOUNT"].ToString(), out totalTaxAmount) ||
                    totalTaxAmount <= 0)
                {
                    TaxAmount.Text = "0.00";
                    EncryptedParameter.Value = string.Empty;

                    ScriptManager.RegisterClientScriptBlock(
                        Page,
                        Page.GetType(),
                        Guid.NewGuid().ToString(),
                        "$('#loadingmessage').modal('hide');alert('Tax amount is zero. Payment cannot be initiated. Please contact the department.');",
                        true
                    );
                    return;
                }

                TaxAmount.Text = totalTaxAmount.ToString("0.00");

                String RandNo = DateTime.Now.Ticks.ToString();

                String EncryptedStr = PayGetWayDTO.MakeEncryptedData(
                    "JHTD",
                    "004100102040101",
                    lblApplicantName.Text,
                    RandNo,
                    TaxAmount.Text,
                    lblApplicationNumber.Text,
                    "NA",
                    "STA COUNTER SIGNATURE TAX PAY",
                    txtAPP_ID.Value,
                    "STA-APPLICATION_" + ddlTaxPeriod.SelectedValue + "_" + tblType.Value,
                    "PRJ",
                    "PRJSES026",
                    "sec1234"
               );

                EncryptedParameter.Value = EncryptedStr;

                ScriptManager.RegisterClientScriptBlock(
                    Page,
                    Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "$('#loadingmessage').modal('hide');",
                    true
                );
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(
                    Page,
                    Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "$('#loadingmessage').modal('hide');alert('" + ex.Message + "')",
                    true
                );
            }
        }
    }
}
