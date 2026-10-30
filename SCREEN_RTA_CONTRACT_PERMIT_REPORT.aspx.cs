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
using System.Globalization;
using System.Collections;
using System.IO;



public partial class SCREEN_RTA_CONTRACT_PERMIT_REPORT : System.Web.UI.Page
{
    
 
    protected List<QueryString> QueryStr = new List<QueryString>();
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
            || UserRole == null || UserRole == string.Empty || UserRole == "" || UserType != "RTA" || UserRole != "Super Admin")
        {
            Response.Redirect("Login.aspx");
            return;
        }
        if (!IsPostBack)
        {

            Page.Title = "RTA CONTRACT PERMIT REPORT";
            PageHeading.InnerText = "RTA CONTRACT PERMIT REPORT";
            PageHeading1.InnerText = "RTA CONTRACT PERMIT REPORT";
       filltable();


        }
    }

    private void filltable()
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "SCREEN_RTA_CONTRACT_PERMIT_APPLICATION_MASTER_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@MODE", "RTA_CONTRACT_REPORT");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" +(++i) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["RTO_NAME"]) + "</td>");
                        if (Convert.ToInt32(dr["CNT"]) != 0)
                        {
                            sb.Append("<td>" + Convert.ToString(dr["CNT"]) + "</td>");
                            sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnViewRTAContract('" + CryptorEngine.Encrypt(Convert.ToString(dr["RTO_NAME"]), true) + "');\">View</a></td>");
                        }
                        else
                        {
                            sb.Append("<td>" + Convert.ToString(dr["CNT"]) + "</td>");
                            sb.Append("<td></td>");
                        }
                        sb.Append("</tr>");
                    }

                    FillStateDetails.InnerHtml = sb.ToString();
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "TableDatatablesButtons.init();", true);
                    
                }
            }
            catch(Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-DASHBOARD.aspx'", true);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
    }
   
   



    protected void btnReset_Click(object sender, EventArgs e)
    {


    }
}