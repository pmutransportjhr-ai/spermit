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



public partial class SCREEN_STA_COUNTER_SIGNATURE_APPLICATION : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string EmpId = Convert.ToString(Session["EMPLOYEE_ID"]);
        string EmpName = Convert.ToString(Session["EMPLOYEE_NAME"]);
        //string EmpType = Convert.ToString(Session["EMP_TYPE"]);
        string UserRole = Convert.ToString(Session["USER_ROLE"]);
        string UserType = Convert.ToString(Session["USER_TYPE"]);

        if (EmpId == null || EmpId == string.Empty || EmpId == ""
            || EmpName == null || EmpName == string.Empty || EmpName == ""
            //  || EmpType == null || EmpType == string.Empty || EmpType == ""
            || UserRole == null || UserRole == string.Empty || UserRole == "" || UserType != "STA" || UserRole != "Super Admin")
        {
            Response.Redirect("Login.aspx");
            return;
        }
        if (!IsPostBack)
        {

            Page.Title = " STA COUNTER SIGNATURE APPLICATION ";
            PageHeading.InnerText = " STA COUNTER SIGNATURE APPLICATION ";
            PageHeading1.InnerText = " STA COUNTER SIGNATURE APPLICATION ";
            Bind_ddlStateWHS();
        }
    }
  
    public void Bind_ddlStateWHS()
    {


        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");
        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters);

        ddlStateIssuedBy.DataSource = dr;
        ddlStateIssuedBy.Items.Clear();
        ddlStateIssuedBy.Items.Add(new ListItem("--Please select state--", "0"));
        ddlStateIssuedBy.DataTextField = "STATE_NAME";
        ddlStateIssuedBy.DataValueField = "STATE_CODE";
        ddlStateIssuedBy.DataBind();
        ddlStateIssuedBy.SelectedValue = "0";


    }
    protected void ddlStateIssuedBy_SelectedIndexChanged(object sender, EventArgs e)
    {

        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('show');", true);
        string abcdxcc = ddlStateIssuedBy.SelectedValue;
        if (abcdxcc.Contains("|") || abcdxcc.Contains("&") || abcdxcc.Contains(";") || abcdxcc.Contains("$") || abcdxcc.Contains("%") || abcdxcc.Contains("@") || abcdxcc.Contains("'") || abcdxcc.Contains("\"")
            || abcdxcc.Contains("[") || abcdxcc.Contains("]") || abcdxcc.Contains("}") || abcdxcc.Contains("{") || abcdxcc.Contains("\\") || abcdxcc.Contains("/") || abcdxcc.Contains("<") || abcdxcc.Contains(">") || abcdxcc.Contains("(") || abcdxcc.Contains(")")
            || abcdxcc.Contains("+") || abcdxcc.Contains("\n") || abcdxcc.Contains("\r") || abcdxcc.Contains("=") || abcdxcc.Contains(","))
        {
            RouteHDRInfo.InnerHtml = "";
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid Value passed');$('#loadingmessage').modal('hide');ComponentsDateTimePickers.init();", true);


        }

        else if (ddlStateIssuedBy.SelectedValue == "0")
        {
            RouteHDRInfo.InnerHtml = "";
        }
        else
        {
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@STATE_CODE", Convert.ToInt64(ddlStateIssuedBy.SelectedValue));
                    sCmd.Parameters.AddWithValue("@MODE", "ROUTE_LIST_BYID");
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
                            sb.Append("<div class='row'>");
                            sb.Append("<div class='col-md-1'><input type='radio' name='routeHdr' value='" + Convert.ToString(dr["ROUTE_ID"]) + "' id='RadioButton_" + Convert.ToString(dr["ROUTE_ID"]) + "' onclick=\"javascript:fnShowRoute('" + CryptorEngine.Encrypt("ROUTE_ID=" + Convert.ToString(dr["ROUTE_ID"]), true) + "')\"/></div>");
                            sb.Append("<div class='col-md-9'>" + Convert.ToString(dr["ROUTE_NAME"]) + "</div>");
                            sb.Append("<div class='col-md-2'></div>");
                            sb.Append("</div>");
                        }
                        RouteHDRInfo.InnerHtml = sb.ToString();



                    }
                    else
                    {
                        RouteHDRInfo.InnerHtml = "";
                    }


                    //ComponentsDateTimePickers.init();
                    
                }

            

                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-DASHBOARD.aspx'", true);

                }

                  

              

            }
        }
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');ComponentsDateTimePickers.init();fnManufactDate();", true);


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string PermitType = ddlPermitType.SelectedValue;
        string EmployeeID = Convert.ToString(Session["EMPLOYEE_ID"]);
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;

        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {

            String input = "";
            input = txtroutjson.Value;
            PERMIT_APPLICATION_LDR rDTO = new JavaScriptSerializer().Deserialize<PERMIT_APPLICATION_LDR>(input);

            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
                sCmd.CommandText = "SCREEN_STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
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



                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                if (PermitType == "1")
                {
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", 1);
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", "2");
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", 15);
                }

                else if (PermitType == "2")
                {
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", 2);
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_ID", "4");
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", 16);
                }

             
                
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtPermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_VALID_FROM", DateTime.ParseExact(txtvalidityfrom.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PERMIT_VALID_UPTO", DateTime.ParseExact(txtvalidityto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));

                sCmd.Parameters.AddWithValue("@MAKER_DESC", VMakerDesc.Text);
                sCmd.Parameters.AddWithValue("@MAKER_MODEL", VMakerModel.Text);
                sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", VType.Text);
                sCmd.Parameters.AddWithValue("@CHASIS_NUMBER", VChassisNumber.Text);
                sCmd.Parameters.AddWithValue("@ENGINE_NUMBER", VEngineNumber.Text);
                sCmd.Parameters.AddWithValue("@SEATING_CAPACITY", VSeatCapacity.Text);
                sCmd.Parameters.AddWithValue("@ULW", txtULW.Text);
                sCmd.Parameters.AddWithValue("@RLW", txtRLW.Text);
                //  sCmd.Parameters.AddWithValue("@PERMIT_FEE_AMOUNT", lblApplicationFees.Text);


                sCmd.Parameters.AddWithValue("@COUNTER_SIGNATURE_NUMBER", txtCounterSignatureNumber.Text);
                sCmd.Parameters.AddWithValue("@RECOMMENDATION_DATE", DateTime.ParseExact(txtRecommendationNoDated.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@RECOMMENDATION_NUMBER", txtRecommendationNo.Text);
                sCmd.Parameters.AddWithValue("@FEE_TAX_PAY_TRANSACTION_ID", txttaxRefNo.Text);
                sCmd.Parameters.AddWithValue("@FEE_TAX_PAID_ON", DateTime.ParseExact(txttaxRefDated.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));


                
                sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", ManufacturingON);
                sCmd.Parameters.AddWithValue("@NATURE_OF_GOODS_CARRIED", txtNatureOfGoods.Text.Trim());
                sCmd.Parameters.AddWithValue("@ISSUED_BY_STATE", Convert.ToInt64(ddlStateIssuedBy.SelectedValue));

                sCmd.Parameters.AddWithValue("@ROUTE_ID", rDTO.ROUTE_DETAIL_ID);
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO", DateTime.ParseExact(txtTPValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@APPROVED_BY", EmployeeID);
                //

                sCmd.Parameters.AddWithValue("@FEE_TAX_AMOUNT", txttaxpayAmount.Text.Trim());
                sCmd.Parameters.AddWithValue("@PAY_MODE", ddltaxperiod.SelectedValue.Trim());
                sCmd.Parameters.AddWithValue("@FEE_TAX_PAID_FROM", DateTime.ParseExact(txtTAXpaidfrom.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FEE_TAX_PAID_UP_TO", DateTime.ParseExact(txtTAXpaidupto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));



                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
                sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXISTS");
                if (Convert.ToInt32(sCmd.ExecuteScalar()) != 0)
                {
                    VManufacturingON.Value = Convert.ToString(ViewState["manufactDate"]);
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Registration number or Permit number is already exists');", true);
                    return;
                }
                else
                {
                    sCmd.Parameters["@MODE"].Value = "HDR_ADD";
                    int appID  =  Convert.ToInt32( sCmd.ExecuteScalar());
                    foreach (PERMIT_APPLICATION_LDR dr in rDTO.PERMIT_APPLICATION_LDRs)
                    {
                        sCmd.CommandText = "SCREEN_STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();

                        sCmd.Parameters.AddWithValue("@APP_ID", appID);
                        sCmd.Parameters.AddWithValue("@SEQUENCE", dr.ROUTE_DETAIL_ID);
                        sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                        sCmd.Parameters.AddWithValue("@ARRIVAL_TIMING", dr.ARRIVAL_TIMING.ToString());
                        sCmd.Parameters.AddWithValue("@DEPARTURE_TIMING", dr.DEPARTURE_TIMING.ToString());
                        sCmd.Parameters.AddWithValue("@MODE", dr.MODE);
                        sCmd.Parameters.AddWithValue("@ID", dr.ID);
                        sCmd.Parameters.AddWithValue("@OP_MODE", "LDR_ADD");
                        sCmd.ExecuteNonQuery();

                    }
                    sCmd.CommandText = "SCREEN_STA_COUNTER_SIGNATURE_APPLICATION_HDR_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@APP_ID", appID);
                    sCmd.Parameters.AddWithValue("@MODE", "MSTR_UPD");
                    sCmd.ExecuteNonQuery();
                  


                    objTrans.Commit();
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully saved');window.location ='SCREEN_STA_COUNTER_SIGNATURE_APPLICATION.aspx'", true);

                }

            }

            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='SCREEN_STA_COUNTER_SIGNATURE_APPLICATION.aspx'", true);
                // Response.Redirect("PermitHomePage.aspx");
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {

        ddlStateIssuedBy.SelectedValue = "0";
        RouteHDRInfo.InnerHtml = "";
        RouteTimeInfo.InnerHtml = "";
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "ComponentsDateTimePickers.init();fnManufactDate();", true);


    }
}