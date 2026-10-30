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

public partial class RTA_TRANSACTION_REPORT : System.Web.UI.Page
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

               String RTA = ddlRTA.SelectedValue;
               String Services = ddlServices.SelectedValue;
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
               sCmd.CommandText = "RTA_SP_GET_PERMIT_MASTER_DATA";
               sCmd.CommandType = CommandType.StoredProcedure;
               sCmd.Parameters.Clear();
               sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", Services);
               sCmd.Parameters.AddWithValue("@DATE_FROM", FROM);
               sCmd.Parameters.AddWithValue("@DATE_TO", TO);
               sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
               sCmd.Parameters.AddWithValue("@MODE", "TRANSACTION_SUCCESS_LIST");
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
                       sb.Append("<td>" + Convert.ToString(dr["APPLICANT_NAME"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["TXN_AMOUNT"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["DEPTTRANID"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["TXN_DATE"]) + "</td>");
                       sb.Append("<td>" + Convert.ToString(dr["TRN_STATUS"]) + "</td>");                      
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

    protected void ddlRTA_SelectedIndexChanged(object sender, EventArgs e)
    {

        String RTA = ddlRTA.SelectedValue;
        tblISPermit.Visible = false;
        if (RTA == "-1")
        {
            ddlServices.Items.Clear();

        }
        else if (RTA == "RTA APPLICATION")
        {
             ddlServices.Items.Clear();
             ddlServices.Items.Add(new ListItem("Apply Permit Application", "RTA PERMIT APPLICATION"));
             ddlServices.Items.Add(new ListItem("Permit Application", "RTA PERMIT ISSUE"));
             ddlServices.Items.Add(new ListItem("Objection Application", "RTA PERMIT OBJECTION PERMIT"));
             ddlServices.Items.Add(new ListItem("Duplicate Permit Application","RTA PERMIT DUPLICATE"));
             ddlServices.Items.Add(new ListItem("Renewal Permit Application Apply","RTA PERMIT RENEWAL"));
             ddlServices.Items.Add(new ListItem("Renewal Permit Application","RTA RENEWAL PERMIT ISSUE"));
             ddlServices.Items.Add(new ListItem("Replacement of Vehicle Application","RTA PERMIT VEHICLE REPLACE"));
             ddlServices.Items.Add(new ListItem("Transfer of Permit Application","RTA PERMIT TRANSFER"));
            
             ddlServices.Items.Add(new ListItem("Apply Counter Signature Application","RTA CS APPLICATION"));
             ddlServices.Items.Add(new ListItem("Counter Signature Issued","RTA COUNTER SIGNATURE TAX PAY"));
             ddlServices.Items.Add(new ListItem("Objection Counter Signature Application", "RTA PERMIT OBJECTION CS"));
             ddlServices.Items.Add(new ListItem("Duplicate Counter Signature Application", "RTA COUNTER SIGNATURE DUPLICATE"));
             ddlServices.Items.Add(new ListItem("Renewal Counter Signature Application", "RTA COUNTER SIGNATURE RENEWAL"));
             ddlServices.Items.Add(new ListItem("Replacement of Vehicle Counter Signature Application", "RTA COUNTER SIGNATURE REPLACEMENT OF VEHICLE"));
             ddlServices.Items.Add(new ListItem("Transfer of Counter Signature Application", "RTA COUNTER SIGNATURE TRANSFER"));
                                                      
            

        }
        else if (RTA == "GOODS APPLICATION")
        {
            ddlServices.Items.Clear();
            ddlServices.Items.Add(new ListItem("Apply Goods Permit Application","RTA PERMIT APPLICATION GOODS"));
            ddlServices.Items.Add(new ListItem("Goods Permit Application","RTA PERMIT ISSUE GOODS"));
            ddlServices.Items.Add(new ListItem("Duplicate Goods Permit Application","RTA PERMIT DUPLICATE GOODS"));
            ddlServices.Items.Add(new ListItem("Renewal Goods Permit Application Apply","RTA PERMIT RENEWAL GOODS"));
            ddlServices.Items.Add(new ListItem("Renewal Goods Permit Application","RTA RENEWAL PERMIT ISSUE GOODS"));
            ddlServices.Items.Add(new ListItem("Goods Replacement of Vehicle Application","RTA PERMIT VEHICLE REPLACE GOODS"));
            ddlServices.Items.Add(new ListItem("Transfer of Goods Permit Application","RTA PERMIT TRANSFER GOODS"));
            
        }
        else if (RTA == "CONTRACT APPLICATION")
        {
            ddlServices.Items.Clear();
            ddlServices.Items.Add(new ListItem("Apply Contract Permit Application","RTA PERMIT APPLICATION CONTRACT"));
            ddlServices.Items.Add(new ListItem("Contract Permit Application","RTA PERMIT ISSUE CONTRACT"));
            ddlServices.Items.Add(new ListItem("Duplicate Contract Permit Application","RTA PERMIT DUPLICATE CONTRACT"));
            ddlServices.Items.Add(new ListItem("Renewal Contract Permit Application Apply","RTA PERMIT RENEWAL CONTRACT"));
            ddlServices.Items.Add(new ListItem("Renewal Contract Permit Application","RTA RENEWAL PERMIT ISSUE CONTRACT"));
            ddlServices.Items.Add(new ListItem("Contract Replacement of Vehicle Application","RTA PERMIT VEHICLE REPLACE CONTRACT"));
            ddlServices.Items.Add(new ListItem("Transfer of Contract Permit Application","RTA PERMIT TRANSFER CONTRACT"));
            
        }
        else if (RTA == "AUTO APPLICATION")
        {
            ddlServices.Items.Clear();
            ddlServices.Items.Add(new ListItem("Apply Auto Permit Application","RTA PERMIT APPLICATION AUTO"));
            ddlServices.Items.Add(new ListItem("Permit Auto Application","RTA PERMIT ISSUE AUTO"));
            ddlServices.Items.Add(new ListItem("Duplicate Auto Permit Application","RTA PERMIT DUPLICATE AUTO"));
            ddlServices.Items.Add(new ListItem("Renewal Auto Permit Application Apply","RTA PERMIT RENEWAL AUTO"));
            ddlServices.Items.Add(new ListItem("Renewal Auto Permit Application","RTA RENEWAL PERMIT ISSUE AUTO"));
            ddlServices.Items.Add(new ListItem("Auto Replacement of Vehicle Application","RTA PERMIT VEHICLE REPLACE AUTO"));
            ddlServices.Items.Add(new ListItem("Transfer of Auto Permit Application","RTA PERMIT TRANSFER AUTO"));
            
        }
        else if (RTA == "SCHOOL BUS APPLICATION")
        {
            ddlServices.Items.Clear();
            ddlServices.Items.Add(new ListItem("Apply School Bus Permit Application","RTA PERMIT APPLICATION SCHOOL BUS"));
            ddlServices.Items.Add(new ListItem("Permit School Bus Application","RTA PERMIT ISSUE SCHOOL BUS"));
            ddlServices.Items.Add(new ListItem("Duplicate School Bus Permit Application","RTA PERMIT DUPLICATE SCHOOL BUS"));
            ddlServices.Items.Add(new ListItem("Renewal School Bus Permit Application Apply","RTA PERMIT RENEWAL SCHOOL BUS"));
            ddlServices.Items.Add(new ListItem("Renewal School Bus Permit Application","RTA RENEWAL PERMIT ISSUE SCHOOL BUS"));
            ddlServices.Items.Add(new ListItem("School Bus Replacement of Vehicle Application","RTA PERMIT VEHICLE REPLACE SCHOOL BUS"));
            ddlServices.Items.Add(new ListItem("Transfer of School Bus Permit Application","RTA PERMIT TRANSFER SCHOOL BUS"));
            
        }
        else 
        {
            ddlServices.Items.Clear();
        }
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');TableDatatablesScroller.init();ComponentsDateTimePickers.init();", true);
    }
}