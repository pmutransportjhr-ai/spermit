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

public partial class STA_PERMIT_OBJECTION_APPROVAL_LIST : System.Web.UI.Page
{
    
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_ddlDesState();
            

        }
    }
    public void Bind_ddlDesState()
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");
        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters);
        ddlDesState.DataSource = dr;
        ddlDesState.Items.Clear();
        ddlDesState.Items.Add(new ListItem("All", "0"));
        ddlDesState.DataTextField = "STATE_NAME";
        ddlDesState.DataValueField = "STATE_CODE";
        ddlDesState.DataBind();
        ddlDesState.SelectedValue = "0";
    }
    protected void btnRun_Click(object sender, EventArgs e)
    {
         String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
         using (SqlConnection sCon = new SqlConnection(sConnectionString))
         {
             try
             {

                 String State = ddlDesState.SelectedValue;
                 String Status = ddlStatus.SelectedValue;
                 String ObjType = ddlType.SelectedValue;

                 sCon.Open();
                 SqlCommand sCmd = new SqlCommand();
                 sCmd.Connection = sCon;
                 sCmd.CommandText = "STA_PERMIT_OBJECTION_APPLICATION_APPROVAL";
                 sCmd.CommandType = CommandType.StoredProcedure;
                 sCmd.Parameters.Clear();
                 sCmd.Parameters.AddWithValue("@STATE_CODE", State);
                 sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", Status);
                 sCmd.Parameters.AddWithValue("@OBJECTION_TYPE", ObjType);
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

                         sb.Append("<tr>");
                         sb.Append("<td>" + Convert.ToString(dr["OBJECTOR_NAME"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["OBJECTOR_REGISTRATION_NUMBER"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["OBJECTOR_PERMIT_NUMBER"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["MOBILE_NUMBER"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["OBJECTION_ON_REGISTRATION_NO"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["OBJECTION_ON_APPLICATION_NUMBER"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["OBJECTION_TILL"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["PAYMENT_STATUS"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["OBJECTION_TYPE"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["CREATED_ON"]) + "</td>");
                         sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + q + "');\">View</a></td>");
                         sb.Append("</tr>");
                     }


                     FillObjectionDetail.InnerHtml = sb.ToString();
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
                 String Message = ex.Message;
                 Message = Message.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                 ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Message + "');", true);


             }
         }
    }
   
}