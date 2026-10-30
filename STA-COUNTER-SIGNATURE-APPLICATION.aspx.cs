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

public partial class STA_COUNTER_SIGNATURE_APPLICATION : System.Web.UI.Page
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
        try
        {

            String State = ddlDesState.SelectedValue;
            String Status = ddlStatus.SelectedValue;
            String PermitType = ddlType.SelectedValue;

            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
            parameters[1] = new SqlParameter("@STATE", State);
            parameters[2] = new SqlParameter("@STATUS", Status);
            parameters[3] = new SqlParameter("@TYPE", PermitType);
            parameters[4] = new SqlParameter("@OBJECTIONDATE", DBNull.Value);
            parameters[5] = new SqlParameter("@MODE", "LISTVERIFIED");
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_PERMIT_COUNTER_SIGNATURE_APPLICATION_APPROVAL", parameters);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    

                    string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);
                    sb.Append("<tr>");
                    sb.Append("<td>" + Convert.ToString(dr["APPLICANT_NAME"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["VEHICLE_REGISTRATION_NO"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_TYPE"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["ROUTE_ISSUED"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["PAYMENT_STATUS"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["OBJECTION_TILL"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["REFERENCE_NUMBER"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_VALID_UPTO"]) + "</td>");
                    if (Convert.ToString(dr["READ_ACCESS"]) == "Y" && Convert.ToString(dr["WRITE_ACCESS"]) == "Y")
                    {
                        sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnEdit('" + q + "');\">Edit</a></td>");
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }
                    sb.Append("</tr>");
                }


                FillInterStateCounterSignatureList.InnerHtml = sb.ToString();
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