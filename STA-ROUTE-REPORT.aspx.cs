using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json;
using System.Text;



public partial class STA_ROUTE_REPORT : System.Web.UI.Page
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
        ddlDesState.Items.Add(new ListItem("--Please select state--", "0"));
        ddlDesState.DataTextField = "STATE_NAME";
        ddlDesState.DataValueField = "STATE_CODE";
        ddlDesState.DataBind();
        ddlDesState.SelectedValue = "0";


    }
    protected void ddlDesState_SelectedIndexChanged(object sender, EventArgs e)
    {

        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('show');", true); 
        if (ddlDesState.SelectedValue == "0")
        {
            Message.Visible = false;
            RouteDiv.Visible = false;
        }
        else
        {


            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_PERMIT_APPLY_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@STATE_CODE", ddlDesState.SelectedValue);
                    sCmd.Parameters.AddWithValue("@MODE", "PERMIT_OPENING_LIST_BYID");
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
                            sb.Append("<td>" + Convert.ToString(dr["S_NO"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["ROUTE_NAME"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["SANCTIONED_PERMIT"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["ISSUED_PERMIT_PER"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["ISSUED_PERMIT_TEMP"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["BALANCED_PERMIT"]) + "</td>");


                            if (Convert.ToString(dr["SELECT_ROUTE_FLG"]) == "Y")
                            {

                                sb.Append("<td><input type='radio' name='route' id='RadioButton_" + Convert.ToString(dr["ROUTE_ID"]) + "' onclick=\"javascript:fnApplyPermit('" + CryptorEngine.Encrypt("ROUTE_ID=" + Convert.ToString(dr["ROUTE_ID"]) + "&TYPE=" + hdnTypeID.Value + "&TYPE_OPTION_ID=" + hdnTypeOptionID.Value, true) + "')\"/></td>");

                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                            sb.Append("</tr>");
                        }
                        FillAPPlyRouteList.InnerHtml = sb.ToString();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "TableDatatablesButtons.init();", true);
                      
                        RouteDiv.Visible = true;
                        Message.Visible = false;

                    }
                    else
                    {
                        FillAPPlyRouteList.InnerHtml = "";
                        RouteDiv.Visible = false;
                        Message.Visible = true;
                    }


                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);

                }
                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-DASHBOARD.aspx'", true);

                }
            }

            
            
        }
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);
        
    }
}