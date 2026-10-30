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
using System.Globalization;

public partial class STA_COUNTER_SIGNATURE_APPLICATION_VERIFY : System.Web.UI.Page
{
    
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-COUNTER-SIGNATURE-APPLICATION-VERIFY-LIST.aspx'", true);
                return;
            }
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
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ID", hdnID.Value);
                parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                parameters[2] = new SqlParameter("@MODE", "COUNTER_SIGNATURE_LIST");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST_BYID", parameters);
                
                        DataTable dt1 = ds.Tables[0];
                        DataTable dt2 = ds.Tables[1];
                        DataTable dt3 = ds.Tables[2];
                        StringBuilder sb = new StringBuilder();
                        if (dt1.Rows.Count > 0)
                        {
                            if (Convert.ToString(dt1.Rows[0]["PAYMENT_STATUS"]) == "Paid")
                            {
                                
                                    ddlApplicationStatus.Items.Clear();
                                    ddlApplicationStatus.Items.Add(new ListItem("Verified", "Verified"));
                                    ddlApplicationStatus.Items.Add(new ListItem("Rejected", "Rejected"));
                                    ddlApplicationStatus.DataBind();
                            }
                        if (Convert.ToString(dt1.Rows[0]["PAYMENT_STATUS"]) == "Unpaid")
                        {
                            txtObjectionDate.ReadOnly = true;
                            ddlApplicationStatus.Items.Clear();
                            ddlApplicationStatus.Items.Add(new ListItem("Rejected", "Rejected"));
                            ddlApplicationStatus.DataBind();
                        }


                            //if (Convert.ToString(dt1.Rows[0]["PAYMENT_STATUS"]) == "Paid")
                            //{

                                if (Convert.ToString(dt1.Rows[0]["APPLICATION_STATUS"]) == "Applied")
                                {
                                    btnSave.Visible = true;
                                }
                                else
                                {
                                    btnSave.Visible = false;
                                }

                           

                            lblAppNumber.InnerText = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);
                            lblAppNumber1.InnerText = Convert.ToString(dt1.Rows[0]["APPLICATION_NUMBER"]);
                            lblAppDt.InnerText = Convert.ToString(dt1.Rows[0]["CREATED_ON"]);
                            lblName.InnerText = Convert.ToString(dt1.Rows[0]["APPLICANT_NAME"]);
                            lblResAdd.InnerText = Convert.ToString(dt1.Rows[0]["RESIDENTIAL_ADD"]);
                            lblBuAdd.InnerText = Convert.ToString(dt1.Rows[0]["BUSINESS_ADD"]);
                            lblPermitNo.InnerText = Convert.ToString(dt1.Rows[0]["PRIMARY_PERMIT_NO"]);
                            lblValiUpto.InnerText = Convert.ToString(dt1.Rows[0]["PERMIT_VALID_UPTO"]);
                            lblIssuedBY.InnerText = Convert.ToString(dt1.Rows[0]["PERMIT_ISSUED_BY_STATE"]);
                            lblRegNo.InnerText = Convert.ToString(dt1.Rows[0]["VEHICLE_REGISTRATION_NO"]);
                            lblSeatCapacity.InnerText = Convert.ToString(dt1.Rows[0]["VEHICLE_SEAT_CAPACITY"]);
                            lblRoute.InnerText = Convert.ToString(dt1.Rows[0]["ROUTE_ISSUED"]);
                            lblValidityfrom.InnerText = Convert.ToString(dt1.Rows[0]["VALIDITY_OF_COUNTER_SIGNATURE_FROM"]);
                            lblValidityto.InnerText = Convert.ToString(dt1.Rows[0]["VALIDITY_OF_COUNTER_SIGNATURE_TO"]);
                            lblNatureofGoods.InnerText = Convert.ToString(dt1.Rows[0]["NATURE_OF_GOODS_CARRIED"]);
                            lblTaxDetails.InnerText = Convert.ToString(dt1.Rows[0]["ROAD_TAX_OF_JHARKHAND_DETAILS"]);
                            lblApprovalTiming.InnerText = Convert.ToString(dt1.Rows[0]["APPROVED_ON"]);
                            lblMobileNumber.InnerText = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);
                            txtObjectionDate.Text = Convert.ToString(dt1.Rows[0]["OBJECTION_TILL"]);

                        foreach (DataRow dr in dt3.Rows)
                            {
                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "RC")
                                {
                                    dvrc.InnerHtml = "<a href='STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", "") + "' target='_blank' title='download'>Click here to download</a>";
                                }
                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "INSURANCE")
                                {
                                    dvinsurance.InnerHtml = "<a href='STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", "") + "' target='_blank' title='download'>Click here to download</a>";
                                }
                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "POLLUTION")
                                {
                                    dvpolution.InnerHtml = "<a href='STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", "") + "' target='_blank' title='download'>Click here to download</a>";
                                }
                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "FITNESS")
                                {
                                    dvFitness.InnerHtml = "<a href='STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", "") + "' target='_blank' title='download'>Click here to download</a>";
                                }
                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "PERMIT")
                                {
                                    dvPermit.InnerHtml = "<a href='STAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", "") + "' target='_blank' title='download'>Click here to download</a>";
                                    
                                }


                            }
                            

                        }
                        if (dt2.Rows.Count > 0)
                        {
                            Int32 length = dt2.Rows.Count - 1;
                            sb.Append("<table class='table table-striped table-bordered table-hover'>");
                            sb.Append("<thead>");
                            sb.Append("<tr>");
                            sb.Append("<th colspan='3' style='text-align:center;'> Up </th>");
                            sb.Append("<th>  </th>");
                            sb.Append("<th colspan='3' style='text-align:center;'> Down </th>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<th> Route </th>");
                            sb.Append("<th> Arrival </th>");
                            sb.Append("<th> Departure </th>");
                            sb.Append("<th>  </th>");
                            sb.Append("<th> Route </th>");
                            sb.Append("<th> Arrival </th>");
                            sb.Append("<th> Departure </th>");
                            sb.Append("</tr>");
                            sb.Append("</thead>");
                            sb.Append("<tbody id='tbdRouteInfo'>");
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    sb.Append("<tr id='tr_" + Convert.ToString(dt2.Rows[i]["S_NO"]) + "'>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                    sb.Append("<td> ..... </td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["UP_DEP_TIME"]) + "</label></td>");
                                    sb.Append("<td></td>");
                                    sb.Append("<td>" + Convert.ToString(dt2.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                    sb.Append("<td> ..... </td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                    sb.Append("</tr>");
                                }
                                else if (i == length)
                                {
                                    sb.Append("<tr id='tr_" + Convert.ToString(dt2.Rows[i]["S_NO"]) + "'>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["UP_ARR_TIME"]) + "</label></td>");
                                    sb.Append("<td> ..... </td>");
                                    sb.Append("<td></td>");
                                    sb.Append("<td>" + Convert.ToString(dt2.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                    sb.Append("<td> ..... </td>");
                                    sb.Append("</tr>");

                                }
                                else
                                {
                                    sb.Append("<tr id='tr_" + Convert.ToString(dt2.Rows[i]["S_NO"]) + "'>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["UP_ARR_TIME"]) + "</label></td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["UP_DEP_TIME"]) + "</label></td>");
                                    sb.Append("<td></td>");
                                    sb.Append("<td>" + Convert.ToString(dt2.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                    sb.Append("</tr>");
                                }

                            }
                            sb.Append("<tbody>");
                            sb.Append("</table>");

                            RouteTimeInfo.InnerHtml = sb.ToString();

                        }

            }

            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-COUNTER-SIGNATURE-APPLICATION-VERIFY-LIST.aspx'", true);

            }


        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("STA-COUNTER-SIGNATURE-APPLICATION-VERIFY-LIST.aspx");

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#confirm').modal('show');", true);
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
            parameters[1] = new SqlParameter("@STATE", hdnID.Value);
            parameters[2] = new SqlParameter("@STATUS", ddlApplicationStatus.SelectedValue);
            parameters[3] = new SqlParameter("@TYPE", DBNull.Value);

            if (ddlApplicationStatus.SelectedValue.Trim() == "Verified")
            {

                DateTime objection_till = DateTime.Parse(txtObjectionDate.Text, new CultureInfo("en-CA"));
                parameters[4] = new SqlParameter("@OBJECTIONDATE", objection_till);

            }
            else
                parameters[4] = new SqlParameter("@OBJECTIONDATE", DBNull.Value);

            
            parameters[5] = new SqlParameter("@MODE", "VERIFIED_REJCTION_UPDATE");
            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_PERMIT_COUNTER_SIGNATURE_APPLICATION_APPROVAL", parameters);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Verified');window.location ='STA-COUNTER-SIGNATURE-APPLICATION-VERIFY-LIST.aspx'", true);
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-COUNTER-SIGNATURE-APPLICATION-VERIFY-LIST.aspx'", true);

        }

    }
}
    
    



    