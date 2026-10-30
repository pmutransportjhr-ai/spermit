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

public partial class STA_PERMIT_CS_APPLICATIONS : System.Web.UI.Page
{
    
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            Bind_ddlDesState();
            FillListing();

            if (Convert.ToString(Session["USER_LABEL"]) == "JTC")
            {
               ddlPermitAction.Items.Clear();
               ddlPermitAction.Items.Add(new ListItem("All", "0"));
               ddlPermitAction.Items.Add(new ListItem("Verified", "Verified"));
               ddlPermitAction.Items.Add(new ListItem("Rejected", "Rejected"));
            }

            if (Convert.ToString(Session["USER_LABEL"]).Equals("SO", StringComparison.OrdinalIgnoreCase))
            {
                ddlStatus.Items.Remove(ddlStatus.Items.FindByValue("Applied"));
            }

        }
    }
    private void FillListing()
    {
        SqlParameter[] parameters = new SqlParameter[4];
        string sdfds = Convert.ToString(Session["USER_LABEL"]);
        parameters[0] = new SqlParameter("@USER_LABLE", Convert.ToString(Session["USER_LABEL"]));
        parameters[1] = new SqlParameter("@STATE_CODE", '0');
        parameters[2] = new SqlParameter("@APPLICATION_STATUS", '0');
        parameters[3] = new SqlParameter("@MODE", "LIST_APPLIED");
        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_STA_CS_COMM_HISTORY", parameters);
        StringBuilder sb = new StringBuilder();
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string regNumber = Convert.ToString(dr["REGISTRATION_NUMBER"]);
                string permitNumber = Convert.ToString(dr["PERMIT_NUMBER"]);
                string state =  Convert.ToString(dr["STATE"]);
                string status = Convert.ToString(dr["APPLICATION_STATUS"]);
                string pendency = Convert.ToString(dr["PENDENCY"]);
                string paymentStatus = Convert.ToString(dr["PAYMENT_STATUS"]);
                string AppId = CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]),true);
                string csID = CryptorEngine.Encrypt(Convert.ToString(dr["CS_ID"]),true);
                string applicationNumber = Convert.ToString(dr["APPLICATION_NUMBER"]);
                sb.Append("<tr>");
                sb.Append(string.Format(@"<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td>", applicationNumber,
                            regNumber, permitNumber, state, status, pendency, paymentStatus));

                sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + AppId + "');\">View</a></td>");

                if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                {
                    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnAction('" +AppId+"');\">Action</a></td>");

                }
                else if (Convert.ToString(dr["CAN_JTC_APPROVE"]) == "Y")
                {
                    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnApprove('" + AppId + "');\">Approve</a></td>");
                }
                else
                {
                    sb.Append("<td></td>");
                }


                sb.Append("</tr>");

            }
            FillSTAPermitApplication.InnerHtml = sb.ToString();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('No Data Found');", true);
        }
     }
    public void Bind_ddlDesState()
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");

        SqlDataReader dr = SQLHelper.ExecuteReader(
            SQLHelper.GetConnString(),
            CommandType.StoredProcedure,
            "STA_SP_MSTR_LIST",
            parameters);

        ddlDesState.DataSource = dr;
        ddlDesState.Items.Clear();
        ddlDesState.Items.Add(new ListItem("All", "0"));
        ddlDesState.DataTextField = "STATE_NAME";
        ddlDesState.DataValueField = "STATE_CODE";
        ddlDesState.DataBind();

        // Get session values
        string userLabel = Convert.ToString(Session["USER_LABEL"]);
        string rtoCode = Convert.ToString(Session["RTO_CODE"]);

        // Lock dropdown ONLY for ASSISTANT user
        if (!string.IsNullOrEmpty(userLabel) && userLabel == "ASSISTANT")
        {
            if (!string.IsNullOrEmpty(rtoCode))
            {
                ListItem item = ddlDesState.Items.FindByValue(rtoCode);

                if (item != null)
                {
                    ddlDesState.SelectedValue = rtoCode;

                    // Lock dropdown
                    ddlDesState.Attributes.Add("disabled", "disabled");
                }
            }
        }
        else
        {
            // Other users can select freely
            ddlDesState.SelectedValue = "0";
            ddlDesState.Enabled = true;
        }
    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('show');", true);
         String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
         using (SqlConnection sCon = new SqlConnection(sConnectionString))
         {
             try
             {

                 String State = ddlDesState.SelectedValue;
                 String Status = ddlStatus.SelectedValue;
                 String PermitType = "";
                 
                 sCon.Open();
                 SqlCommand sCmd = new SqlCommand();
                 sCmd.Connection = sCon;
                 sCmd.CommandText = "SP_STA_CS_COMM_HISTORY";
                 sCmd.CommandType = CommandType.StoredProcedure;
                 sCmd.Parameters.Clear();
                 sCmd.Parameters.AddWithValue("@STATE_CODE", State);
                 sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", Status);
                 sCmd.Parameters.AddWithValue("@USER_LABLE", Convert.ToString(Session["USER_LABEL"]));
                 sCmd.Parameters.AddWithValue("@MODE", "LIST_APPLIED");
                 DataSet ds = new DataSet();
                 SqlDataAdapter da = new SqlDataAdapter(sCmd);
                 da.Fill(ds);
                 DataTable dt = new DataTable();
                 dt = ds.Tables[0];
                 StringBuilder sb = new StringBuilder();
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         string regNumber = Convert.ToString(dr["REGISTRATION_NUMBER"]);
                         string permitNumber = Convert.ToString(dr["PERMIT_NUMBER"]);
                         string state = Convert.ToString(dr["STATE"]);
                         string status = Convert.ToString(dr["APPLICATION_STATUS"]);
                         string pendency = Convert.ToString(dr["PENDENCY"]);
                         string paymentStatus = Convert.ToString(dr["PAYMENT_STATUS"]);
                         string AppId = CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true);
                         string csID = CryptorEngine.Encrypt(Convert.ToString(dr["CS_ID"]), true);
                         string applicationNumber = Convert.ToString(dr["APPLICATION_NUMBER"]);
                         sb.Append("<tr>");
                         sb.Append(string.Format(@"<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td>", applicationNumber, 
                             regNumber, permitNumber, state, status, pendency, paymentStatus));

                         sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnView('" + AppId + "');\">View</a></td>");

                         if (Convert.ToString(dr["CAN_TAKE_ACTION"]) == "Y")
                         {
                             sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnAction('" + AppId + "');\">Action</a></td>");

                         }
                         else if (Convert.ToString(dr["CAN_JTC_APPROVE"]) == "Y")
                         {
                             sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnApprove('" + AppId + "');\">Approve</a></td>");
                         }
                         else
                         {
                             sb.Append("<td></td>");
                         }

                         sb.Append("</tr>");

                     }
                     FillSTAPermitApplication.InnerHtml = sb.ToString();
                 }
                 else
                 {
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
                sCmd.CommandText = "SP_STA_CS_COMM_HISTORY";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", CryptorEngine.Decrypt(txtDelKey.Value, true));
                sCmd.Parameters.AddWithValue("@USER_LABLE", Convert.ToString(Session["USER_LABEL"]));
                sCmd.Parameters.AddWithValue("@APPLICATION_STATUS", ddlPermitAction.Value);
                sCmd.Parameters.AddWithValue("@VERIFY_COMMENTS", txtCommentsCS.Text.ToString());
                sCmd.Parameters.AddWithValue("@MODE", "ACTION");
                Object obj = sCmd.ExecuteScalar();
                string msg = obj.ToString().Split('~')[0].Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                SMSSender objsms = new SMSSender();
                string applicationNumber = obj.ToString().Split('~')[1];
                string MobileNumber = obj.ToString().Split('~')[2];
                string Fees = obj.ToString().Split('~')[3];
                
                if (Convert.ToString(Session["USER_LABEL"]) == "JTC")
                {
                    if (ddlPermitAction.Value == "Verified")
                    {
                        objsms.Sendsms(MobileNumber, "Your Application (" + applicationNumber + ") has been Verified. Please Pay Fees Online of Rs. " + Fees + "/-","");
                    }
                    else if (ddlPermitAction.Value == "Rejected")
                    {
                        objsms.Sendsms(MobileNumber, "Your Application (" + applicationNumber + ") has been " + ddlPermitAction.Value,"");
                    }
                    else if (ddlPermitAction.Value == "Approved")
                    {
                        objsms.Sendsms(MobileNumber, "Your Application (" + applicationNumber + ") has been " + ddlPermitAction.Value,"");
                    }
                }
                              
              ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg + "');window.location.href='STA-PERMIT-CS-APPLICATIONS.aspx';", true);
            }
            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location.href='STA-PERMIT-CS-APPLICATIONS.aspx'", true);
            }
        }
        
    }
    
}