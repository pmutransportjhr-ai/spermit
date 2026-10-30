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

public partial class RTA_DESIGNATION_MASTER_NEW : System.Web.UI.Page
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
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-DESIGNATION-MASTER.aspx'", true);
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


                    ID.Value = IDx.Value;




                    if (ID.Value == "0")
                    {
                        txtDESG_ID.Text = ID.Value;
                        txtDESG_NAME.Text = String.Empty;

                    }
                    else
                    {

                        SqlParameter[] parameters = new SqlParameter[3];
                        parameters[0] = new SqlParameter("@ID", ID.Value);
                        parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                        parameters[2] = new SqlParameter("@MODE", "DESIGNATION_LIST_BYID");
                        DataTable dt = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST_BYID", parameters).Tables[0];
                        string UserRole = Convert.ToString(Session["USER_ROLE"]);
                        if (dt.Rows.Count > 0)
                        {
                            txtDESG_ID.Text = dt.Rows[0]["DESG_ID"].ToString();
                            txtDESG_NAME.Text = dt.Rows[0]["DESG_NAME"].ToString();
                            if (UserRole == "Super Admin")
                            {
                                btnSave.Visible = true;

                            }
                            else
                            {
                                btnSave.Visible = false;

                            }

                        }

                        
                    }

                }
            }

            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-DESIGNATION-MASTER.aspx'", true);

            }

        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int32 DESG_ID = Convert.ToInt32(Server.HtmlEncode(txtDESG_ID.Text.Trim()));
        String DESG_NAME = Server.HtmlEncode(txtDESG_NAME.Text.Trim());
        
        try
        {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@DESG_ID", DESG_ID);
                    parameters[1] = new SqlParameter("@DESG_NAME", DESG_NAME);
                    parameters[2] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                    parameters[3] = new SqlParameter("@MODE", "DUPLICACY");

                    var obj1 = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_DESG_MSTR", parameters);
                    if ((Int32)obj1 == 0)
                    {
                        if (DESG_ID == 0)
                        {
                            parameters[3] = new SqlParameter("@MODE", "ADD");
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_DESG_MSTR", parameters);
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully saved');window.location ='RTA-DESIGNATION-MASTER.aspx'", true);
                        }
                        else
                        {
                            parameters[3] = new SqlParameter("@MODE", "EDIT");
                            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_DESG_MSTR", parameters);
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully updated');window.location ='RTA-DESIGNATION-MASTER.aspx'", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Designation is already exist');", true);

                    }
               
          
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RTA-DESIGNATION-MASTER.aspx");
    }
}