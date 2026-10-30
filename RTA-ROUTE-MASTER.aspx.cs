using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Text;
using System.Configuration;

public partial class RTA_ROUTE_MASTER : System.Web.UI.Page
{
    public string StrContainer { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //loadRoutelist();
            Bind_ddlDesState();
        }
        string UserRole = Convert.ToString(Session["USER_ROLE"]);
        if (UserRole == "Super Admin")
        {
            btnAdd.Visible = true;
        }
        else
        {
            btnAdd.Visible = false;
        }


    }
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        string q = CryptorEngine.Encrypt("ID=0", true);
        Response.Redirect("RTA-ROUTE-MASTER-NEW.aspx?q=" + q);
        

        
    }
    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        try
        {
        String queryString = CryptorEngine.Decrypt(Convert.ToString(txtDelKey.Value), true);
        foreach (String str in queryString.Split(new Char[] { '&' }))
        {
            QueryString qs = new QueryString();
            qs.Name = str.Split(new Char[] { '=' })[0];
            qs.Value = str.Split(new Char[] { '=' })[1];
            QueryStr.Add(qs);
        }
        QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault();
        String ID = IDx.Value;
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@ID", ID);
        parameters[1] = new SqlParameter("@MODE", "ROUTE_DELETE");

        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MASTER_DELETE", parameters);
        DataTable dt = new DataTable();

        dt = ds.Tables[0];


        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]).Split('~')[1] + "'); window.location ='RTA-ROUTE-MASTER.aspx'", true);
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
            
        }


    }
    public void loadRoutelist()
    {
        try
        {

        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter("@ID", ddlDesState.SelectedValue);
        parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
        parameters[2] = new SqlParameter("@MODE", "ROUTE_LIST_BYRTO");

        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST_BYID", parameters);
        DataTable dt = new DataTable();

        dt = ds.Tables[0];

        string UserRole = Convert.ToString(Session["USER_ROLE"]);

        StringBuilder sb = new StringBuilder();
        
            if (dt.Rows.Count > 0)
            {
                

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + Convert.ToString(dr["ROUTE_NUMBER"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["ST_FROM"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["ST_TO"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["TOTAL_DISTANCE"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["NO_OF_VACANCIES"]) + "</td>");
                    //sb.Append("<td>" + Convert.ToString(dr["STATUS"]) + "</td>");

                    string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["ROUTE_ID"]), true);
                    sb.Append("<td><a  class='btn btn-outline dark' href=\"javascript:fnUpdateVacancies('" + q + "'," + Convert.ToString(dr["NO_OF_VACANCIES"]) + ",'" + Convert.ToString(dr["ROUTE_NUMBER"]) + "');\">Update</a></td>");
                    if (Convert.ToString(dr["STATUS"])=="Active")
                    {
                    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnEdit('" + q + "');\">Edit</a></td>");
                        
                    }
                    else
                    {
                        sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnLegacyEdit('" + q + "');\">Edit</a></td>");
                   
                    }


                    if (UserRole == "Super Admin")
                    {
                        if (Convert.ToString(dr["CAN_DELETE"]) == "Y")
                        {
                            sb.Append("<td><a  class='btn btn-outline dark' href=\"javascript:fnDelete('" + q + "');\">Delete</a></td>");

                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }
                    }
                    else 
                    {
                        sb.Append("<td></td>");
                    }
                    sb.Append("</tr>");
                }
                 StrContainer = sb.ToString();
                 ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "TableDatatablesButtons.init();", true);
                 RouteDiv.Visible = true;
                 Message.Visible = false;
            }
            else
            {

                RouteDiv.Visible = false;
                Message.Visible = true;
            }
            

            
        }

       catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
          ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
        }

        
    }
    protected void btnUpdateYes_Click(object sender, EventArgs e)
    {
        try
        {
            string x = hdUv.Value.ToString();
            String queryString = CryptorEngine.Decrypt(Convert.ToString(hdUv.Value.ToString()), true);

            foreach (String str in queryString.Split(new Char[] { '&' }))
            {
                QueryString qs = new QueryString();
                qs.Name = str.Split(new Char[] { '=' })[0];
                qs.Value = str.Split(new Char[] { '=' })[1];
                QueryStr.Add(qs);
            }

            QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault();

            String RID = IDx.Value;
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@ID", RID);
            parameters[1] = new SqlParameter("@MODE", "ROUTE_VACANCY_UPDATE");
            parameters[2] = new SqlParameter("@OTHER", txtUpdateVacancies.Text.ToString().Trim());
            parameters[3] = new SqlParameter("@ROUTENUMBER", txtRouteNo.Text.ToString().Trim());
            parameters[4] = new SqlParameter("@OLDROUTENUMBER", Convert.ToString(HiddenRouteNo.Value));

            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MASTER_DELETE", parameters);
            DataTable dt = new DataTable();

            dt = ds.Tables[0];



            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]) + "'); window.location ='RTA-ROUTE-MASTER.aspx'", true);
        }

        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-ROUTE-MASTER.aspx'", true);

        }

    }
    public void Bind_ddlDesState()
    {

        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "DD_RTO_LIST");
        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", parameters);

        ddlDesState.DataSource = dr;
        ddlDesState.Items.Clear();
        ddlDesState.Items.Add(new ListItem("--Please select RTO--", "0"));
        ddlDesState.DataTextField = "RTO_NAME";
        ddlDesState.DataValueField = "RTO_CODE";
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
            loadRoutelist();
            
        }
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);

    }
}