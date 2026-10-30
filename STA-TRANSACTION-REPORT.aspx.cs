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

public partial class STA_TRANSACTION_REPORT : System.Web.UI.Page
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

               String Services = ddlServices.SelectedValue;
               String Status = ddlStatus.SelectedValue;
               DateTime? FROM; DateTime? TO;
               if (!String.IsNullOrEmpty(txtFrom.Text.Trim()))
               {
                   FROM = DateTime.ParseExact(txtFrom.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
               }
               else
               {
                   FROM = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
               }
               if (!String.IsNullOrEmpty(txtTo.Text.Trim()))
               {
                   TO = DateTime.ParseExact(txtTo.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture);
               }
               else
               {
                   TO = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
               }
               sCon.Open();
               SqlCommand sCmd = new SqlCommand();
               sCmd.Connection = sCon;
               sCmd.CommandText = "STA_SP_GET_MASTER_DATA";
               sCmd.CommandType = CommandType.StoredProcedure;
               sCmd.Parameters.Clear();
               sCmd.Parameters.AddWithValue("@STATE_CODE", Services);
               sCmd.Parameters.AddWithValue("@DATE_FROM", FROM);
               sCmd.Parameters.AddWithValue("@DATE_TO", TO);
               sCmd.Parameters.AddWithValue("@STATUS", Status);
               sCmd.Parameters.AddWithValue("@MODE", "TRANSACTION_SUCCESS_LIST");
               DataSet ds = new DataSet();
               SqlDataAdapter da = new SqlDataAdapter(sCmd);
               da.Fill(ds);
               DataTable dt = new DataTable();
               dt = ds.Tables[0];

               decimal total = 0;
               StringBuilder sb = new StringBuilder();
               if (dt.Rows.Count > 0)
               {
                   foreach (DataRow dr in dt.Rows)
                   {
                       total = total + Convert.ToDecimal(dr["TXN_AMOUNT"]);
                                                        
                      
                       sb.Append("<tr>");
                       sb.Append("<td>" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["APPLICANT_NAME"]) + "</td>");
                       sb.Append("<td>" + Convert.ToDecimal(dr["TXN_AMOUNT"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["DEPTTRANID"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["TXN_DATE"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["TRN_STATUS"]) + "</td>");
                       string cUrl = Convert.ToString(dr["CHALLAN_URL"]);
                       if (cUrl != "")
                       {
                           sb.Append("<td><a href='" + Convert.ToString(dr["CHALLAN_URL"]) + "' target='_blank'>E-Challan</td>");
                       }
                       else
                       {
                           sb.Append("<td></td>");
                       }
                      
                       sb.Append("</tr>");
                   }
                   if (Status == "SUCCESS")
                   { 
                   sb.Append("<tr>");
                   sb.Append("<td><b>Total</b></td>");
                   sb.Append("<td>---</td>");
                   sb.Append("<td>" + total + "</td>");
                   sb.Append("<td>---</td>");
                   sb.Append("<td>---</td>");
                   sb.Append("<td>---</td>");
                   sb.Append("<td>---</td>");
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