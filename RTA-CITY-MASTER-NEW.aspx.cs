using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using SoftEra.JHT.Infrastructure;
using System.Configuration;

public partial class RTA_CITY_MASTER_NEW : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    //private static string ID;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-CITY-MASTER.aspx'", true);
                    return;
                }
                else
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["q"]), true);
                    foreach (String str in queryString.Split(new Char[] { '&' }))
                    {
                        QueryString qs = new QueryString();
                        qs.Name = str.Split(new Char[] { '=' })[0];
                        qs.Value = str.Split(new Char[] { '=' })[1];
                        QueryStr.Add(qs);
                    }

                    QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                    string ID = IDx.Value;
                    if (ID == "0")
                    {
                        txtCityCode.Text = ID;
                    }
                    else
                    {
                        SqlParameter[] parameters = new SqlParameter[3];
                        parameters[0] = new SqlParameter("@ID", ID);
                        parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                        parameters[2] = new SqlParameter("@MODE", "CITY_LIST_BYID");

                        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST_BYID", parameters);
                        DataTable dt = new DataTable();

                        dt = ds.Tables[0];

                        string UserRole = Convert.ToString(Session["USER_ROLE"]);
                        if (dt.Rows.Count > 0)
                        {

                            txtCityCode.Text = dt.Rows[0]["CITY_CODE"].ToString();
                            txtCityName.Text = dt.Rows[0]["CITY_NAME"].ToString();

                            if (UserRole == "Super Admin")
                            {
                                btnSave.Visible = true;

                            }
                            else
                            {
                                btnSave.Visible = false;

                            }
                            //ddlStatus.SelectedValue = dt.Rows[0]["STATUS"].ToString();
                        }

                    }

                }
            }

            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-CITY-MASTER.aspx'", true);

            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int32 CityCode = Convert.ToInt32(Server.HtmlEncode(txtCityCode.Text.Trim()));
        String CityName = Server.HtmlEncode(txtCityName.Text.Trim());
        //String Status = Server.HtmlEncode(ddlStatus.SelectedValue);
        try
        {
            SqlParameter[] parameters = new SqlParameter[5];

            parameters[0] = new SqlParameter("@CITY_CODE", CityCode);
            parameters[1] = new SqlParameter("@CITY_NAME", CityName);
            parameters[2] = new SqlParameter("@STATUS", "Active");
            parameters[3] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
            parameters[4] = new SqlParameter("@Mode", "DUPLICACY");
            var obj1 = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_CITY_MSTR", parameters);

            if ((Int32)obj1 == 0)
            {
                if (CityCode == 0)
                {
                    parameters[4] = new SqlParameter("@Mode", "ADD");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_CITY_MSTR", parameters);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully saved');window.location ='RTA-CITY-MASTER.aspx'", true);
                }
                else
                {
                    parameters[4] = new SqlParameter("@Mode", "EDIT");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_CITY_MSTR", parameters);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully updated');window.location ='RTA-CITY-MASTER.aspx'", true);
                }

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('City name is already exist');", true);

            }

        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("RTA-CITY-MASTER.aspx");
    }
}