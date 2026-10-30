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

public partial class RTA_PERMIT_DUPLICATE_VERIFY_LIST : System.Web.UI.Page
{
    public string FillInterStatePermitList { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            

        }
    }
   
    protected void btnRun_Click(object sender, EventArgs e)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                String Status = ddlStatus.SelectedValue;
                String PermitType = ddlType.SelectedValue;

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "RTA_DUPLICATE_OF_PERMIT_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", Status);
                sCmd.Parameters.AddWithValue("@TYPE", PermitType);
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
                        sb.Append("<td>" + Convert.ToString(dr["RTO_NAME"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                        sb.Append("<td><a  href=\"javascript:fnRegDetail('" + q1 + "');\">" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</a></td>");
                        sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["ROUTE"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["FEE"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["PAYMENT_STATUS"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["FEE_PAY_TRANSACTION_ID"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["VALID_FROM"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["APPLICATION_PENDING_ON"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                       
                        
                            sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                        
                        if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                        {
                            sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnAction('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "','" + Convert.ToString(dr["APPLICATION_STATUS"]) + "','" + Convert.ToString(dr["USER_LABEL"]) + "','" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]) + "','" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]) + "','" + Convert.ToString(dr["JTC_VERIFY_COMMENTS"]) + "');\">Action</a></td>");
                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }
                        sb.Append("</tr>");
                    }


                    divrenewalDetail.InnerHtml = sb.ToString();
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
                sCmd.CommandText = "RTA_DUPLICATE_OF_PERMIT_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", CryptorEngine.Decrypt(txtDelKey.Value, true));
                sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", ddlDuplicateAction.Value);
                sCmd.Parameters.AddWithValue("@VERIFY_COMMENTS", txtCommentsReject.Text.ToString());
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@MODE", "ACTION");
                Object obj = sCmd.ExecuteScalar();
                string msg = obj.ToString().Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + msg + "');window.location ='RTA-PERMIT-DUPLICATE-VERIFY-LIST.aspx'", true);
            }
            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');window.location ='RTA-PERMIT-DUPLICATE-VERIFY-LIST.aspx'", true);
            }
        }
    }
}