using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;
using System.Collections;

public partial class STA_COUNTER_REPORT_EDIT_VIEW : System.Web.UI.Page
{
    //private static string ID;
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-DASHBOARD.aspx'", true);
                return;
            }
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["q"]), true);
                    foreach (String str in queryString.Split(new Char[] { '&' }))
                    {
                        QueryString qs = new QueryString();
                        qs.Name = str.Split(new Char[] { '=' })[0];
                        qs.Value = str.Split(new Char[] { '=' })[1];
                        QueryStr.Add(qs);
                    }

                    QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);

                    hdnID.Value = IDx.Value;

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "SCREEN_STA_COUNTER_SIGNATURE_UPDATE";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                    if (Session["status"] == "Verified")
                    {
                        sCmd.Parameters.AddWithValue("@MODE", "INITIAL_VIEW");
                    }
                    else
                    {
                        sCmd.Parameters.AddWithValue("@MODE", "MASTER_VIEW");
                    }
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);
                    
                    DataTable dt1 = ds.Tables[0];
                    
                    StringBuilder sb = new StringBuilder();
                    if (dt1.Rows.Count > 0)
                    {
                        lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                        lblApplicationStatus.Text = dt1.Rows[0]["APPLICATION_STATUS"].ToString();
                        VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                        VOCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                        VOPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                        VOPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();

                        VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                        VOPermitNo.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                        VOCSNo.Text = dt1.Rows[0]["COUNTER_SIGNATURE_NUMBER"].ToString();
                        VoPermitFrom.Text = dt1.Rows[0]["PERMIT_VALID_FROM"].ToString();
                        VoPermitUpto.Text = dt1.Rows[0]["PERMIT_VALID_UPTO"].ToString();
                        lblMakerDesc.Text = dt1.Rows[0]["MAKER_DESC"].ToString();
                        lblMakermodel.Text = dt1.Rows[0]["MAKER_MODEL"].ToString();
                        VType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                        VChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                        VEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();
                        VSeatCapacity.Text = dt1.Rows[0]["SEATING_CAPACITY"].ToString();
                        vsleepercapacity.Text = dt1.Rows[0]["SLEEPER_CAPACITY"].ToString();

                        txtULW.Text = dt1.Rows[0]["ULW"].ToString();
                        txtRLW.Text = dt1.Rows[0]["RLW"].ToString();
                        VManufacturingON.Value = dt1.Rows[0]["MANUFACTURING_ON"].ToString();
                        VNatureOfGoods.Text = dt1.Rows[0]["NATURE_OF_GOODS_CARRIED"].ToString();
                        
                        VOMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                        VOEmailID.Text = dt1.Rows[0]["EMAIL_ID"].ToString();

                        lblRecommendationNo.Text = dt1.Rows[0]["RECOMMENDATION_NUMBER"].ToString();
                        lblRecommendationDate.Text = dt1.Rows[0]["RECOMMENDATION_DATE"].ToString();
                                                
                        lblIssuedBy.Text = dt1.Rows[0]["STATE_NAME"].ToString();
                        RouteHDRInfo.Text = dt1.Rows[0]["ROUTE"].ToString();
                        txtAmount.Text = dt1.Rows[0]["FEE_TAX_AMOUNT"].ToString();
                        txtFromDate.Value = dt1.Rows[0]["FEE_TAX_PAID_FROM"].ToString();
                        txtToDate.Value = dt1.Rows[0]["FEE_TAX_PAID_UP_TO"].ToString();
                        txtPaidOn.Value = dt1.Rows[0]["FEE_TAX_PAID_ON"].ToString();
                        txtPayRef.Text = dt1.Rows[0]["FEE_TAX_PAY_TRANSACTION_ID"].ToString();
                        
                    }
                }

                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-COUNTER-REPORT-EDIT-VIEW.aspx'", true);

                }

            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string EmployeeID = Convert.ToString(Session["EMPLOYEE_ID"]);
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;

        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
                sCmd.CommandText = "SCREEN_STA_COUNTER_SIGNATURE_UPDATE";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                DateTime? ManufacturingON;
                ViewState["manufactDate"] = VManufacturingON.Value;
                if (!String.IsNullOrEmpty(VManufacturingON.Value))
                {
                    VManufacturingON.Value = "01/" + VManufacturingON.Value;
                    ManufacturingON = DateTime.ParseExact(VManufacturingON.Value, "d/M/yyyy", CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);
                }
                else
                {
                    ManufacturingON = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }
                sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", VOPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@MAKER_DESC", lblMakerDesc.Text);
                sCmd.Parameters.AddWithValue("@MAKER_MODEL", lblMakermodel.Text);
                sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", VType.Text);
                sCmd.Parameters.AddWithValue("@CHASIS_NUMBER", VChassisNumber.Text);
                sCmd.Parameters.AddWithValue("@ENGINE_NUMBER", VEngineNumber.Text);
                sCmd.Parameters.AddWithValue("@SEATING_CAPACITY", VSeatCapacity.Text);
                sCmd.Parameters.AddWithValue("@SLEEPER_CAPACITY", vsleepercapacity.Text);
                sCmd.Parameters.AddWithValue("@ULW", txtULW.Text);
                sCmd.Parameters.AddWithValue("@RLW", txtRLW.Text);
                sCmd.Parameters.AddWithValue("@COUNTER_SIGNATURE_NUMBER", VOCSNo.Text);
                sCmd.Parameters.AddWithValue("@FEE_TAX_PAY_TRANSACTION_ID", txtPayRef.Text);
                sCmd.Parameters.AddWithValue("@FEE_TAX_PAID_ON", DateTime.ParseExact(txtPaidOn.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", ManufacturingON);
                sCmd.Parameters.AddWithValue("@FEE_TAX_AMOUNT", txtAmount.Text.Trim());
                sCmd.Parameters.AddWithValue("@PAY_MODE", ddlPayMode.SelectedValue.Trim());
                sCmd.Parameters.AddWithValue("@FEE_TAX_PAID_FROM", DateTime.ParseExact(txtFromDate.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FEE_TAX_PAID_UP_TO", DateTime.ParseExact(txtToDate.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                if (lblApplicationStatus.Text == "Verified")
                {
                    sCmd.Parameters.AddWithValue("@MODE", "VERIFY_UPDATE");
                }
                else 
                {
                    sCmd.Parameters.AddWithValue("@MODE", "MASTER_UPDATE");
                }
                sCmd.ExecuteNonQuery();
                objTrans.Commit();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully saved');window.location ='Counter_Signature_Edit.aspx'", true);
            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='Counter_Signature_Edit.aspx'", true);
                // Response.Redirect("PermitHomePage.aspx");
            }
        }
    }
}