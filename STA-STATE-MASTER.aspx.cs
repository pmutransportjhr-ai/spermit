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

public partial class STA_STATE_MASTER : System.Web.UI.Page
{
    public string FillStateList { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            FillStateListMaster();

        }                          

    }

    private void FillStateListMaster()
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "STA_SP_STATE_MSTR";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@MODE", "STATE_LIST");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);
                DataTable dt = new DataTable();

                

                dt = ds.Tables[0];

                string UserRole = Convert.ToString(Session["USER_ROLE"]);
                StringBuilder sb = new StringBuilder();


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
                        sb.Append("<td>" + Convert.ToString(dr["STATE_NAME"]) + "</td>");
                        //sb.Append("<td>" + Convert.ToString(dr["STATUS"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["STATE_ABBREVIATION"]) + "</td>");
                        sb.Append("<td>" + Convert.ToString(dr["HOME_STATE"]) + "</td>");

                        string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["STATE_CODE"]), true);

                        sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnEdit('" + q + "');\">Edit</a></td>");

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
                    FillStateList = sb.ToString();
                }

            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
            }

        }
    }
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        string q = CryptorEngine.Encrypt("ID=0", true);
        Response.Redirect("STA-STATE-MASTER-NEW.aspx?q=" + q);
        
    }
    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        
            
             String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
             using (SqlConnection sCon = new SqlConnection(sConnectionString))
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


                     sCon.Open();
                     SqlCommand sCmd = new SqlCommand();
                     sCmd.Connection = sCon;
                     sCmd.CommandText = "STA_SP_STATE_MSTR";
                     sCmd.CommandType = CommandType.StoredProcedure;
                     sCmd.Parameters.Clear();
                     sCmd.Parameters.AddWithValue("@STATE_CODE", ID);
                     sCmd.Parameters.AddWithValue("@MODE", "STATE_DELETE");
                     DataSet ds = new DataSet();
                     SqlDataAdapter da = new SqlDataAdapter(sCmd);
                     da.Fill(ds);
                     DataTable dt = new DataTable();

                     dt = ds.Tables[0];

                     ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]) + "'); window.location ='STA-STATE-MASTER.aspx'", true);
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

}