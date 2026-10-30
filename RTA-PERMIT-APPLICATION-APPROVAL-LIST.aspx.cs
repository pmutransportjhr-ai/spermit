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

public partial class RTA_PERMIT_APPLICATION_APPROVAL_LIST : System.Web.UI.Page
{
    
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
                 sCmd.CommandText = "RTA_PERMIT_APPLICATION_APPROVAL";
                 sCmd.CommandType = CommandType.StoredProcedure;
                 sCmd.Parameters.Clear();
               
                 sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PermitType);
                 sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", Status);
                 sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                 sCmd.Parameters.AddWithValue("@MODE", "LISTVERIFIED");
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

                         sb.Append("<td>" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                         sb.Append("<td><a  href=\"javascript:fnRegDetail('" + q1 + "');\">" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</a></td>");
                         sb.Append("<td>" + Convert.ToString(dr["ROUTE"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_FEE_AMOUNT"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["SURCHARGE_FEE_AMOUNT"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_PAYMENT_STATUS"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_PAY_ID"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["VALID_FROM"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["CS_SUBMITTED"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                         
                         
                             sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                         
                         if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                         {
                             sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnApproveAction('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "','" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]).Replace("'", "''") + "','" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]).Replace("'", "''") + "','" + Convert.ToString(dr["JTC_VERIFY_COMMENTS"]).Replace("'", "''") + "');\">Action</a></td>");
                         }
                         else
                         {
                             sb.Append("<td></td>");
                         }
                         sb.Append("</tr>");
                     }

                     FillInterStatePermitList.InnerHtml = sb.ToString();
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
                sCmd.CommandText = "RTA_PERMIT_APPLICATION_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", CryptorEngine.Decrypt(txtDelKey.Value, true));
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", ddlApproverAction.Value);
                sCmd.Parameters.AddWithValue("@VERIFY_COMMENTS", txtApproverComments.Text.ToString());
                sCmd.Parameters.AddWithValue("@MODE", "APPROVED");
                Object obj = sCmd.ExecuteScalar();
                string msg = obj.ToString().Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + msg + "');window.location ='RTA-PERMIT-APPLICATION-APPROVAL-LIST.aspx'", true);
               
            }
            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');window.location ='RTA-PERMIT-APPLICATION-APPROVAL-LIST.aspx'", true);
            }
        }
    }
}