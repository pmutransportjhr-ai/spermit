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

public partial class JHT_STA_CHANGE_PASSWORD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string EmpId = Convert.ToString(Session["EMPLOYEE_ID"]);
        string EmpName = Convert.ToString(Session["EMPLOYEE_NAME"]);
        //string EmpType = Convert.ToString(Session["EMP_TYPE"]);
        string UserRole = Convert.ToString(Session["USER_ROLE"]);
        string UserType = Convert.ToString(Session["USER_TYPE"]);

        if (EmpId == null || EmpId == string.Empty || EmpId == ""
            || EmpName == null || EmpName == string.Empty || EmpName == ""
            //  || EmpType == null || EmpType == string.Empty || EmpType == ""
            || UserRole == null || UserRole == string.Empty || UserRole == "" || UserType != "STA")
        {
            Response.Redirect("Login.aspx");
            return;
        }
        //this.Page.ClientScript.RegisterClientScriptInclude("SCRIPT1", ResolveUrl("~/js/sha512.js"));
         if (!Page.IsPostBack)
        {
            Session["salt"] = ConfigurationManager.AppSettings["SecurityKey"] /*random.Next(59999, 199999).ToString()*/;
       
            if (Convert.ToString(Session["EMPLOYEE_ID"]) != null || Convert.ToString(Session["EMPLOYEE_ID"]) != "")
            {
                LoadData();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
    public void LoadData()
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "LISTBYID_CH_PSD");
        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_USER_MSTR_AED", parameters);
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        if(dt.Rows.Count>0)
        {
            txtoldPasswordhide.Value=Convert.ToString(dt.Rows[0]["PASSWORD"]);
            txtUserKey.Value = Convert.ToString(dt.Rows[0]["EMPLOYEE_ID"]);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string TpassOld = hashcodegenerate.GetSHA512(hfpwdold.Value.ToString() + Session["salt"].ToString());
            if (TpassOld == txtoldPasswordhide.Value)
            {

                string Tpass = hashcodegenerate.GetSHA512(hfpwd.Value.ToString() + Session["salt"].ToString());
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                parameters[1] = new SqlParameter("@PASSWORD", Tpass);
                parameters[2] = new SqlParameter("@MODE", "EDIT_PSD");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_USER_MSTR_AED", parameters);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0]["MSG"]).Contains("Success"))
                    {
                        String message = Convert.ToString(dt.Rows[0]["MSG"]).Split('~')[1].Replace("\n", "").Replace("\r", "").Replace("'", "");
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');window.location ='STA-DASHBOARD.aspx'", true);
                    }
                    else
                    {
                        String message = Convert.ToString(dt.Rows[0]["MSG"]).Split('~')[1].Replace("\n", "").Replace("\r", "").Replace("'", "");
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');window.location ='STA-DASHBOARD.aspx'", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Password does not matched with Old password');window.location ='STA-DASHBOARD.aspx'", true);
            }
        }
        catch(Exception ex)
        {
            String message = ex.Message.ToString().Replace("\n", "").Replace("\r", "").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');window.location ='STA-DASHBOARD.aspx'", true);
        }
        
    }
}