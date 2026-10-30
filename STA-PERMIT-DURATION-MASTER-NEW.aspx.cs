using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json;


public partial class STA_PERMIT_DURATION_MASTER_NEW : System.Web.UI.Page
{

    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-PERMIT-DURATION-MASTER.aspx'", true);
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
                    hdnID.Value = IDx.Value;

                    Bind_Permit();

                    if (hdnID.Value == "0")
                    {

                    }
                    else
                    {
                        SqlParameter[] parameters = new SqlParameter[3];
                        parameters[0] = new SqlParameter("@ID", hdnID.Value);
                        parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                        parameters[2] = new SqlParameter("@MODE", "PERMIT_DURATION_LIST_BYID");

                        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST_BYID", parameters);
                        DataTable dt = new DataTable();

                        dt = ds.Tables[0];
                        string UserRole = Convert.ToString(Session["USER_ROLE"]);

                        if (UserRole != "Super Admin")
                        {
                            btnSave.Visible = false;

                        }
                        else
                        {
                            btnSave.Visible = true;
                        }


                        if (dt.Rows.Count > 0)
                        {
                            ddlPermitType.SelectedValue = Convert.ToString(dt.Rows[0]["PERMIT_TYPE_ID"]);
                            ddlYM.SelectedValue = Convert.ToString(dt.Rows[0]["DURATION_TYPE"]);
                            txtPermitDuration.Text = Convert.ToString(dt.Rows[0]["DURATION"]);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            if (Message.Contains("Base-64"))
            {
                Message = "Invalid Url Passed";
            }
            Message = Message.Replace("\r", " ").Replace("\n", " ");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-PERMIT-DURATION-MASTER.aspx'", true);
            return;
        }
    }

    private void Bind_Permit()
    {

        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "PERMIT_TYPE_LIST");

        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters);

        ddlPermitType.DataSource = dr;
        ddlPermitType.Items.Clear();
        ddlPermitType.Items.Add(new ListItem("--select--", "0"));
        ddlPermitType.DataTextField = "PERMIT_TYPE";
        ddlPermitType.DataValueField = "PERMIT_TYPE_ID";
        ddlPermitType.DataBind();
        ddlPermitType.SelectedValue = "0";
        


    }




    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("STA-PERMIT-DURATION-MASTER.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
           try
        {
            
            //string permitType = ddlPermitType.Items[ddlPermitType.SelectedIndex].Text;

            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@DURATION_ID", Convert.ToInt32(hdnID.Value));
            parameters[1] = new SqlParameter("@PERMIT_TYPE_ID", Convert.ToInt32(ddlPermitType.SelectedValue));
            parameters[2] = new SqlParameter("@DURATION", Convert.ToString(txtPermitDuration.Text));
            parameters[3] = new SqlParameter("@DURATION_TYPE", Convert.ToString(ddlYM.SelectedValue));
            parameters[4] = new SqlParameter("@EMPLOYEE_ID", Convert.ToInt32(Context.Session["employee_id"]));

            if (hdnID.Value == "0")
            {
                parameters[5] = new SqlParameter("@MODE", "DUPLICACY");
                var num1 = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_PERMIT_DURATION_MSTR", parameters);

                if (Convert.ToInt32(num1) == 0)
                {
                    parameters[5] = new SqlParameter("@MODE", "ADD");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_PERMIT_DURATION_MSTR", parameters);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Submitted');window.location ='STA-PERMIT-DURATION-MASTER.aspx'", true);


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Already exist');window.location ='STA-PERMIT-DURATION-MASTER.aspx'", true);

                }


            }
            else
            {


                parameters[5] = new SqlParameter("@MODE", "DUPLICACY");
                var num1 = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_PERMIT_DURATION_MSTR", parameters);
                if ((Int32)num1 == 0)
                {
                    parameters[5] = new SqlParameter("@MODE", "EDIT");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_PERMIT_DURATION_MSTR", parameters);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Updated');window.location ='STA-PERMIT-DURATION-MASTER.aspx'", true);


                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Already exist');window.location ='STA-PERMIT-DURATION-MASTER.aspx'", true);

                }
            }
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-PERMIT-DURATION-MASTER.aspx'", true);

        }

    }


}

 
