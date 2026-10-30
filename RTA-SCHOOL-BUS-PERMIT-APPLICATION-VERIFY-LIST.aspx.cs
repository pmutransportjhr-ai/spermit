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

public partial class RTA_SCHOOL_BUS_PERMIT_APPLICATION_VERIFY_LIST : System.Web.UI.Page
{
    
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Bind_ddlDesState();
            

        }
    }
    //public void Bind_ddlDesState()
    //{

    //    SqlParameter[] parameters = new SqlParameter[2];
    //    parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
    //    parameters[1] = new SqlParameter("@MODE", "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");
    //    SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters);
      


    //}
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
                 sCmd.CommandText = "RTA_SCHOOL_BUS_PERMIT_APPLICATION_APPROVAL";
                 sCmd.CommandType = CommandType.StoredProcedure;
                 sCmd.Parameters.Clear();
                 sCmd.Parameters.AddWithValue("@STATE_CODE", 0);
                 sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", PermitType);
                 sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", Status);
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
                         sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                         sb.Append("<td><a  href=\"javascript:fnRegDetail('" + q1 + "');\">" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</a></td>");
                    
                         sb.Append("<td>" + Convert.ToString(dr["FEE"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["PAYMENT_STATUS"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["FEE_PAY_TRANSACTION_ID"]) + "</td>");
                         
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_FEE_AMOUNT"]) + "</td>");
                         
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_PAYMENT_STATUS"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_PAY_ID"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["VALID_FROM"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");
                        
                         //sb.Append("<td>" + Convert.ToString(dr["USER_PERMISSION"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["APPLICATION_PENDING_ON"]) + "</td>");
                         if (Convert.ToString(dr["APPLICATION_STATUS"])=="Objection")
                         {
                             sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + " (" + Convert.ToString(dr["COUNT_OBJECTION"]) + ")</td>");
                         }
                         else
                         {
                             sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                         }
                         
                         
                        
                             sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                        
                         if (Convert.ToString(dr["CAN_TAKE_ACTION"])=="Y")
                         {
                             sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnAction('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "','" + Convert.ToString(dr["APPLICATION_STATUS"]) + "','" + Convert.ToString(dr["USER_LABEL"]) + "','" + Convert.ToString(dr["AST_VERIFY_COMMENTS"]) + "','" + Convert.ToString(dr["SO_VERIFY_COMMENTS"]) + "','" + Convert.ToString(dr["JTC_VERIFY_COMMENTS"]) + "');\">Action</a></td>");
                         }
                         else 
                         {
                             sb.Append("<td></td>");
                         }
                        
                         //if (Convert.ToString(dr["CAN_GIVE_PERMISSION"]) == "Y")
                         //{
                         //    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnPermissionAction('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\">Permission</a></td>");
                         //}
                         //else
                         //{
                         //    sb.Append("<td></td>");
                         //}
                         //if (Convert.ToString(dr["CAN_APPROVE_CS"]) == "Y")
                         //{
                         //    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnCSAction('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\">Permission</a></td>");
                         //}
                         //else
                         //{
                         //    sb.Append("<td></td>");
                         //}
                         sb.Append("</tr>");
                     }


                     FillSTAPermitApplication.InnerHtml = sb.ToString();
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
                DateTime? objection;
                if (txtObjectionDt.Value.Trim() == "" || txtObjectionDt.Value.Trim() == null)
                {
                    objection = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    objection = DateTime.ParseExact(txtObjectionDt.Value.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);

                }
                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "RTA_SCHOOL_BUS_PERMIT_APPLICATION_APPROVAL";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", CryptorEngine.Decrypt(txtDelKey.Value, true));
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", ddlPermitAction.Value);
                sCmd.Parameters.AddWithValue("@VERIFY_COMMENTS", txtCommentsReject.Text.ToString());
                sCmd.Parameters.AddWithValue("@MODE", "ACTION");
                Object obj = sCmd.ExecuteScalar();
                string msg = obj.ToString().Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + msg + "');window.location ='RTA-SCHOOL-BUS-PERMIT-APPLICATION-VERIFY-LIST.aspx'", true);
            }
            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');window.location ='RTA-SCHOOL-BUS-PERMIT-APPLICATION-VERIFY-LIST.aspx'", true);
            }
        }
    }
   
}