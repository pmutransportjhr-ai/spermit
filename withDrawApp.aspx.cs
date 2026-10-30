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

public partial class withDrawApp : System.Web.UI.Page
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
               sCmd.CommandText = "sp_get_withdraw";
               sCmd.CommandType = CommandType.StoredProcedure;
               sCmd.Parameters.Clear();
          //     sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", Services);
               sCmd.Parameters.AddWithValue("@start_date", FROM);
               sCmd.Parameters.AddWithValue("@end_date", TO);
               sCmd.Parameters.AddWithValue("@type", "RTA");
               
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
                       sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["WITHDRAW_ON"]) + "</td>");
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