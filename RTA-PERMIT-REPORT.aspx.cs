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

public partial class RTA_PERMIT_REPORT : System.Web.UI.Page
{
    public string FillInterStatePermitList { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
      
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
               sCmd.CommandText = "RTA_SP_GET_PERMIT_MASTER_DATA";
               sCmd.CommandType = CommandType.StoredProcedure;
               sCmd.Parameters.Clear();
               
               sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", Status);
               sCmd.Parameters.AddWithValue("@TYPE", PermitType);
               sCmd.Parameters.AddWithValue("@DATE_FROM", DateTime.ParseExact(txtFrom.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture));
               sCmd.Parameters.AddWithValue("@DATE_TO", DateTime.ParseExact(txtTo.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture));
               sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
               sCmd.Parameters.AddWithValue("@MODE", "LIST_MASTER");
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

                                                        
                      
                       sb.Append("<tr>");
                       sb.Append("<td>" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["MOBILE_NUMBER"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");
                      
                        sb.Append("<td>" + Convert.ToString(dr["VALID_FROM"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                       sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:MasterView('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true) + "');\">View</a></td>");
                       if (Convert.ToString(dr["APPLICATION_STATUS"]) == "Issued")
                       {
                           sb.Append("<td><a target='_blank' class='btn btn-outline dark' onclick=\"javascript:showIssuedReceipt('" + CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(dr["PERMIT_NUMBER"]), true) + "');\">View Issued Recipt</a></td>");
                       }
                       else
                       {
                           sb.Append("<td><a target='_blank' class='btn btn-outline dark' onclick=\"javascript:showIssuedReceiptGlobal('" + CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(dr["PERMIT_NUMBER"]), true) + "');\">View Issued Recipt</a></td>");
                       }

                       sb.Append("<td><a target='_blank' class='btn btn-outline dark' onclick=\"javascript:showTimetable('" + CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(dr["PERMIT_NUMBER"]), true) + "');\">View Time Table</a></td>");
                      // sb.Append("<td><a target='_blank' class='btn btn-outline dark' href=\"javascript:showForwardingLetter('" + CryptorEngine.Encrypt("PER_NUMBER=" + Convert.ToString(dr["PERMIT_NUMBER"]), true) + "');\">View Forwarding Letter</a></td>");
                      
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
               ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');TableDatatablesScroller.init();ComponentsDateTimePickers.init();", true);

           }
           catch (Exception ex)
           {
               String Messqge = ex.Message;
               Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
               ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');", true);


           }
       }
    }
    
}