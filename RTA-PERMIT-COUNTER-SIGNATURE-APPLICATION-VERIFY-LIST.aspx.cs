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

public partial class RTA_PERMIT_COUNTER_SIGNATURE_APPLICATION_VERIFY_LIST : System.Web.UI.Page
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
        try
        {

            String Status = ddlStatus.SelectedValue;
            String PermitType = ddlType.SelectedValue;

            string stremp = Convert.ToString(Session["EMPLOYEE_ID"]);
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@APP_ID", 0);
            parameters[1] = new SqlParameter("@TYPE", PermitType);
            parameters[2] = new SqlParameter("@STATUS", Status);
            parameters[3] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
            parameters[4] = new SqlParameter("@MODE", "LISTAPPLIED");
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_PERMIT_COUNTER_SIGNATURE_APPLICATION_APPROVAL", parameters);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    
                    string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);
                                            
                    sb.Append("<tr>");


                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_NUMBER"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_RTO_NAME"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["OWNER_NAME"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["REGISTRATION_NUMBER"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["ROUTE"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_STATUS"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["PAYMENT_STATUS"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["VALID_UP_TO"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["REFRENCE_NUMBER"]) + "</td>");
                    if (Convert.ToString(dr["READ_ACCESS"]) == "Y" && Convert.ToString(dr["WRITE_ACCESS"]) == "Y")
                    {
                        sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnEditVerify('" + q + "');\">Edit</a></td>");
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }
                    sb.Append("</tr>");
                }

                tbdRTCSList.InnerHtml = sb.ToString();
              

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