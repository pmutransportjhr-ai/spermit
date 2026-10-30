using SoftEra.JHT.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;

public partial class Edit_Form_CS : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();

    public string queryString = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(
                    Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('You are not permitted to access this page');window.location='STA-DASHBOARD.aspx'", true);
                return;
            }

            LoadApplication();
        }
    }

    private object ParseDateOrDBNull(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return DBNull.Value;

        DateTime dt;

        // Your UI uses dd/MM/yyyy
        if (DateTime.TryParseExact(
            value.Trim(),
            "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None,
            out dt))
        {
            return dt;
        }

        // fallback (just in case)
        if (DateTime.TryParse(value, out dt))
            return dt;

        throw new Exception("Invalid date format: " + value);
    }


    private void LoadApplication()
    {
        string conStr = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        try
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
                throw new Exception("Invalid request");

            queryString = CryptorEngine.Decrypt(Request.QueryString["q"], true);

            QueryStr.Clear();
            foreach (string str in queryString.Split('&'))
            {
                string[] kv = str.Split('=');
                if (kv.Length == 2)
                    QueryStr.Add(new QueryString { Name = kv[0], Value = kv[1] });
            }

            var idObj = QueryStr.FirstOrDefault(x => x.Name == "ID");
            if (idObj == null || string.IsNullOrEmpty(idObj.Value))
                throw new Exception("Application ID missing");

            hdnID.Value = idObj.Value;

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("STA_CS_ALL_EDIT_DATA", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));

                DataSet ds = new DataSet();
                new SqlDataAdapter(cmd).Fill(ds);

                if (ds.Tables.Count == 0)
                    throw new Exception("No data found");

                // ================= CORRECT TABLE ORDER =================
                DataTable dtHeader = ds.Tables.Count > 0 ? ds.Tables[0] : null;
                DataTable dtRoute = ds.Tables.Count > 1 ? ds.Tables[1] : null;
                DataTable dtSequence = ds.Tables.Count > 2 ? ds.Tables[2] : null;
                DataTable dtPayMode = ds.Tables.Count > 3 ? ds.Tables[3] : null;

                if (dtHeader == null || dtHeader.Rows.Count == 0)
                    throw new Exception("Application not found");

                DataRow r = dtHeader.Rows[0];

                // ================= HEADER =================
                lblApplicationNo.Text = r["APPLICATION_NUMBER"].ToString();
                lblPermitType.Text = r["PERMIT_TYPE"].ToString();
                lblPermitNo.Text = r["PERMIT_NUMBER"].ToString();
                lblApplicationStatus.Text = r["APPLICATION_STATUS"].ToString();

                // ================= PERSONAL =================
                txtOwnerName.Text = r["OWNER_NAME"].ToString();
                txtCareOf.Text = r["CARE_OF"].ToString();
                txtPresentAddress.Text = r["PRESENT_ADDRESS"].ToString();
                txtPermanentAddress.Text = r["PERMANENT_ADDRESS"].ToString();

                // ================= VEHICLE =================
                VOPermitNo.Text = r["PERMIT_NUMBER"].ToString();
                VORegNumber.Text = r["REGISTRATION_NUMBER"].ToString();
                VType.Text = r["VEHICLE_CLASS_TYPE"].ToString();
                VChassisNumber.Text = r["CHASIS_NUMBER"].ToString();
                VEngineNumber.Text = r["ENGINE_NUMBER"].ToString();
                lblMakerDesc.Text = r["MAKER_DESC"].ToString();
                lblMakermodel.Text = r["MAKER_MODEL"].ToString();
                VGVW.Text = r["ULW"].ToString();
                VPayLoad.Text = r["RLW"].ToString();
                VSeatCapacity.Text = r["SEATING_CAPACITY"].ToString();
                txtSleeperCapacity.Text = r["SLEEPER_CAPACITY"].ToString();
                VManufacturingON.Text = r["MANUFACTURING_ON"].ToString();
                VoPermitFrom.Text = r["PERMIT_VALID_FROM"].ToString();
                VoPermitUpto.Text = r["PERMIT_VALID_UPTO"].ToString();
                VNatureOfGoods.Text = r["NATURE_OF_GOODS_CARRIED"].ToString();

                // ================= VALIDITY =================
                txtICValidUpto.Text = r["IC_VALID_UPTO"].ToString();
                txtPCValidUpto.Text = r["PC_VALID_UPTO"].ToString();
                txtFCValidUpto.Text = r["FC_VALID_UPTO"].ToString();
                txtTPValidUpto.Text = r["TAX_PAID_UPTO"].ToString();

                // ================= COUNTER SIGNATURE =================
                txtCounterSignatureNumber.Text = r["COUNTER_SIGNATURE_NUMBER"].ToString();

                // ================= OTHER =================
                txtMobileNo.Text = r["MOBILE_NUMBER"].ToString();
                txtEmailId.Text = r["EMAIL_ID"].ToString();

                // ================= ROUTE =================
                lblIssuedBy.Text = r["STATE_NAME"].ToString();
                RouteHDRInfo.Text = r["ROUTE"].ToString();

                if (dtRoute != null && dtRoute.Rows.Count > 0 &&
                    dtSequence != null && dtSequence.Rows.Count > 0)
                {
                    BuildRouteTable(dtRoute, dtSequence);
                }
                else
                {
                    RouteTimeInfo.InnerHtml =
                        "<div class='text-muted'>No route details available</div>";
                }

                // ================= TAX PAY MODE =================
                if (dtPayMode != null && dtPayMode.Rows.Count > 0)
                {
                    string payMode = dtPayMode.Rows[0]["PAY_MODE"].ToString();

                    if (ddltaxperiod.Items.FindByValue(payMode) != null)
                        ddltaxperiod.SelectedValue = payMode;
                }

                // ================= TAX DETAILS FROM HEADER =================
                txttaxpayAmount.Text = r["FEE_TAX_AMOUNT"].ToString();
                txtTAXpaidfrom.Value = r["FEE_TAX_PAID_FROM"].ToString();
                txtTAXpaidupto.Value = r["FEE_TAX_PAID_UP_TO"].ToString();
                txttaxRefNo.Text = r["FEE_TAX_PAY_TRANSACTION_ID"].ToString();
                txttaxRefDated.Value = r["FEE_TAX_PAID_ON"].ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(
                Page, Page.GetType(), Guid.NewGuid().ToString(),
                "alert('" + ex.Message.Replace("'", "") + "');", true);
        }
    }
    private string NormalizeTime(object timeValue)
    {
        if (timeValue == null) return "";

        string t = timeValue.ToString().Trim();

        if (string.IsNullOrEmpty(t) || t.Contains("...."))
            return "";

        // Remove "Hrs." or any text
        t = t.Replace("Hrs.", "").Replace("Hrs", "").Trim();

        DateTime dt;
        if (DateTime.TryParse(t, out dt))
        {
            return dt.ToString("HH:mm"); // REQUIRED FORMAT
        }

        return "";
    }



    private void BuildRouteTable(DataTable dt2, DataTable dt4)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table class='table table-bordered'>");
        sb.Append("<thead>");
        sb.Append("<tr>");
        sb.Append("<th colspan='3' class='text-center'>Up</th>");
        sb.Append("<th></th>");
        sb.Append("<th colspan='3' class='text-center'>Down</th>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<th>Route</th><th>Arrival</th><th>Departure</th>");
        sb.Append("<th></th>");
        sb.Append("<th>Route</th><th>Arrival</th><th>Departure</th>");
        sb.Append("</tr>");
        sb.Append("</thead><tbody>");

        int index = 0;

        for (int k = 1; k <= dt4.Rows.Count; k++)
        {
            DataRow[] rows = dt2.Select("SEQUENCE=" + k);
            if (rows.Length == 0) continue;

            DataRow row = rows[0];

            sb.Append("<tr>");

            // ===== UP =====
            sb.Append("<td>" + row["VIA_UP_CITY"] +
                "<input type='hidden' name='seq_" + index + "' value='" + row["SEQUENCE"] + "' />" +
                "<input type='hidden' name='dir_" + index + "' value='UP' />" +
                "</td>");

            sb.Append("<td><input type='time' step='60' name='arr_" + index + "' value='" +
                NormalizeTime(row["UP_ARR_TIME"]) + "' class='form-control' /></td>");

            sb.Append("<td><input type='time' step='60' name='dep_" + index + "' value='" +
                NormalizeTime(row["UP_DEP_TIME"]) + "' class='form-control' /></td>");

            index++;

            sb.Append("<td></td>");

            // ===== DOWN =====
            sb.Append("<td>" + row["VIA_DOWN_CITY"] +
                "<input type='hidden' name='seq_" + index + "' value='" + row["SEQUENCE"] + "' />" +
                "<input type='hidden' name='dir_" + index + "' value='DOWN' />" +
                "</td>");

            sb.Append("<td><input type='time' step='60' name='arr_" + index + "' value='" +
                NormalizeTime(row["DOWN_ARR_TIME"]) + "' class='form-control' /></td>");

            sb.Append("<td><input type='time' step='60' name='dep_" + index + "' value='" +
                NormalizeTime(row["DOWN_DEP_TIME"]) + "' class='form-control' /></td>");

            index++;

            sb.Append("</tr>");
        }

        sb.Append("</tbody></table>");
        RouteTimeInfo.InnerHtml = sb.ToString();
    }



    private object ParseTimeString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return DBNull.Value;

        DateTime dt;
        if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
        {
            return dt.ToString("HH:mm") + " Hrs.";
        }

        return DBNull.Value;
    }



    protected void btnUpdate_Click(object sender, EventArgs e)
    {


        string cs = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

        try
        {
            int i = 0;

            while (Request.Form["seq_" + i] != null)
            {
                using (SqlConnection con = new SqlConnection(cs))
                using (SqlCommand cmd = new SqlCommand("UPDATE_STA_CS_ALL_DATA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // ================= COMMON =================
                    cmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));

                    // ================= PERSONAL =================
                    cmd.Parameters.AddWithValue("@OWNER_NAME", txtOwnerName.Text.Trim());
                    cmd.Parameters.AddWithValue("@CARE_OF", txtCareOf.Text.Trim());
                    cmd.Parameters.AddWithValue("@PRESENT_ADDRESS", txtPresentAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", txtPermanentAddress.Text.Trim());

                    // ================= VEHICLE =================

                    cmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", VType.Text.Trim());
                    cmd.Parameters.AddWithValue("@CHASIS_NUMBER", VChassisNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@ENGINE_NUMBER", VEngineNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@MAKER_DESC", lblMakerDesc.Text.Trim());
                    cmd.Parameters.AddWithValue("@MAKER_MODEL", lblMakermodel.Text.Trim());
                    cmd.Parameters.AddWithValue(
                                 "@SEATING_CAPACITY",
                                 string.IsNullOrWhiteSpace(VSeatCapacity.Text)
                                     ? (object)DBNull.Value
                                     : VSeatCapacity.Text.Trim()
                             );

                    cmd.Parameters.AddWithValue("@SLEEPER_CAPACITY",
                    string.IsNullOrWhiteSpace(txtSleeperCapacity.Text)
                        ? (object)DBNull.Value
                        : txtSleeperCapacity.Text.Trim());

                    cmd.Parameters.AddWithValue("@ULW",
                        string.IsNullOrWhiteSpace(VGVW.Text)
                            ? (object)DBNull.Value
                            : Convert.ToDecimal(VGVW.Text));

                    cmd.Parameters.AddWithValue("@RLW",
                        string.IsNullOrWhiteSpace(VPayLoad.Text)
                            ? (object)DBNull.Value
                            : Convert.ToDecimal(VPayLoad.Text));

                    cmd.Parameters.AddWithValue("@MANUFACTURING_ON",
                        ParseDateOrDBNull(VManufacturingON.Text));

                    cmd.Parameters.AddWithValue("@NATURE_OF_GOODS_CARRIED",
                        VNatureOfGoods.Text.Trim());

                    // ================= VALIDITY =================
                    cmd.Parameters.AddWithValue("@IC_VALID_UPTO",
                        ParseDateOrDBNull(txtICValidUpto.Text));

                    cmd.Parameters.AddWithValue("@PC_VALID_UPTO",
                        ParseDateOrDBNull(txtPCValidUpto.Text));

                    cmd.Parameters.AddWithValue("@FC_VALID_UPTO",
                        ParseDateOrDBNull(txtFCValidUpto.Text));

                    cmd.Parameters.AddWithValue("@TAX_PAID_UPTO",
                        ParseDateOrDBNull(txtTPValidUpto.Text));

                    // ================= PERMIT =================
                    cmd.Parameters.AddWithValue("@PERMIT_VALID_FROM",
                        ParseDateOrDBNull(VoPermitFrom.Text));

                    cmd.Parameters.AddWithValue("@PERMIT_VALID_UPTO",
                        ParseDateOrDBNull(VoPermitUpto.Text));

                    cmd.Parameters.AddWithValue("@ROUTE",
                        RouteHDRInfo.Text.Trim());

                    cmd.Parameters.AddWithValue("@COUNTER_SIGNATURE_NUMBER",
                        string.IsNullOrWhiteSpace(txtCounterSignatureNumber.Text)
                            ? (object)DBNull.Value
                            : txtCounterSignatureNumber.Text.Trim());

                    // ================= OTHER =================
                    cmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
                    cmd.Parameters.AddWithValue("@RECOMMENDATION_NUMBER",
                        txtRecommendationNo.Text.Trim());

                    cmd.Parameters.AddWithValue("@RECOMMENDATION_DATE",
                        ParseDateOrDBNull(txtRecommendationDate.Text));

                    // ================= TAX DETAILS =================
                    cmd.Parameters.AddWithValue("@FEE_TAX_AMOUNT",
                        string.IsNullOrWhiteSpace(txttaxpayAmount.Text)
                            ? (object)DBNull.Value
                            : Convert.ToDecimal(txttaxpayAmount.Text));

                    cmd.Parameters.AddWithValue("@FEE_TAX_PAID_ON",
                        ParseDateOrDBNull(txttaxRefDated.Value));

                    cmd.Parameters.AddWithValue("@FEE_TAX_PAID_FROM",
                        ParseDateOrDBNull(txtTAXpaidfrom.Value));

                    cmd.Parameters.AddWithValue("@FEE_TAX_PAID_UP_TO",
                        ParseDateOrDBNull(txtTAXpaidupto.Value));

                    cmd.Parameters.AddWithValue("@PAY_MODE",
                        string.IsNullOrEmpty(ddltaxperiod.SelectedValue)
                            ? (object)DBNull.Value
                            : ddltaxperiod.SelectedValue);

                    cmd.Parameters.AddWithValue("@FEE_TAX_PAY_TRANSACTION_ID",
                        string.IsNullOrWhiteSpace(txttaxRefNo.Text)
                            ? (object)DBNull.Value
                            : txttaxRefNo.Text.Trim());

                    // ================= ROUTE ROW =================
                    cmd.Parameters.AddWithValue("@SEQUENCE",
                        Convert.ToInt32(Request.Form["seq_" + i]));

                    cmd.Parameters.AddWithValue("@MODE",
                        Request.Form["dir_" + i]);

                    cmd.Parameters.Add("@ARRIVAL_TIMING", SqlDbType.VarChar, 10)
                        .Value = ParseTimeString(Request.Form["arr_" + i]);

                    cmd.Parameters.Add("@DEPARTURE_TIMING", SqlDbType.VarChar, 10)
                        .Value = ParseTimeString(Request.Form["dep_" + i]);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                i++;
            }

            string encryptedQuery = Request.QueryString["q"];

            ScriptManager.RegisterClientScriptBlock(
                Page, Page.GetType(), Guid.NewGuid().ToString(),
                "alert('Application and route timings updated successfully');" +
                "window.location='STA-COUNTER-SIGNATURE-REPORT-VIEW.aspx?q=" + encryptedQuery + "';", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(
                Page, Page.GetType(), Guid.NewGuid().ToString(),
                "alert('Update failed : " + ex.Message.Replace("'", "") + "');", true);
        }
    }

}
