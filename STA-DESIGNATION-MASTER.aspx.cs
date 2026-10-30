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
using System.Text;

public partial class STA_DESIGNATION_MASTER : System.Web.UI.Page
{
    
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDEsignationListMaster();
        }
    }
    private void FillDEsignationListMaster()
    {
        try
        { 

                 
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE","DD_DESIGNATION_LIST");
        DataTable dt = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters).Tables[0];
        

        StringBuilder sb = new StringBuilder();
        string UserRole = Convert.ToString(Session["USER_ROLE"]);

            if (dt.Rows.Count > 0)
            {
                if (UserRole == "Super Admin")
                {
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                }

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + Convert.ToString(dr["DESG_NAME"]) + "</td>");
                    string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["DESG_ID"]), true);

                    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnEdit('" + q + "');\">Edit</a></td>");

                    if (UserRole == "Super Admin")
                    {
                        if (Convert.ToString(dr["CAN_DELETE"]) == "Y")
                        {
                            sb.Append("<td><a  class='btn btn-outline dark' href=\"javascript:fnDelete('" + q+ "');\">Delete</a></td>");
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
                
                FillDesignationList.InnerHtml = sb.ToString();
               
            }
            
        }
        catch(Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
        }


    }
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        string q = CryptorEngine.Encrypt("ID=0", true);
        Response.Redirect("STA-DESIGNATION-MASTER-NEW.aspx?q=" + q);
        
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
        parameters[1] = new SqlParameter("@MODE", "DESIGNATION_DELETE");

        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MASTER_DELETE", parameters);
        DataTable dt = new DataTable();

        dt = ds.Tables[0];
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]).Split('~')[1] + "'); window.location ='STA-DESIGNATION-MASTER.aspx'", true);
    }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
            return;
        }    
    

       
        
    }

}