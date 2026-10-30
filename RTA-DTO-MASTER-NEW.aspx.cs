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

public partial class RTA_DTO_MASTER_NEW : System.Web.UI.Page
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
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-DTO-MASTER.aspx'", true);
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


                    IDXVAL.Value = IDx.Value;


                    Bind_ddlRTO();

                if (IDXVAL.Value == "0")
                {
                    dtocodess.Value = "0";
                    txtDTOCode.Text = String.Empty;
                    txtDTOName.Text = String.Empty;
                    txtPhoneNo.Text = String.Empty;
                    txtEmailId.Text = String.Empty;
                    txtAddress.Text = String.Empty;
                  
                          
                    //ddlStatus.SelectedValue = "0";
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@DTO_CODE", IDXVAL.Value);
                    parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                    parameters[2] = new SqlParameter("@MODE", "DTO_LIST_BYID");
                    

                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_DTO_MASTER_AED", parameters);
                    DataTable dt = new DataTable();

                    dt = ds.Tables[0];
                    string UserRole = Convert.ToString(Session["USER_ROLE"]);
                   
                    
                        if (dt.Rows.Count > 0)
                        {
                            dtocodess.Value = dt.Rows[0]["DTO_CODE"].ToString();
                            txtDTOCode.Text = dt.Rows[0]["DTO_CODE"].ToString();
                            txtDTOName.Text = dt.Rows[0]["DTO_NAME"].ToString();
                            txtPhoneNo.Text = dt.Rows[0]["PHONE_NO"].ToString();
                            ddlRTOCode.SelectedValue = dt.Rows[0]["RTO_CODE"].ToString();
                            txtEmailId.Text = dt.Rows[0]["EMAIL"].ToString();
                            txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();


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
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-DTO-MASTER.aspx'", true);

            }
        }

    }
    public void Bind_ddlRTO()
    {

        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", DBNull.Value);
        parameters[1] = new SqlParameter("@MODE", "DD_RTO_LIST");
        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", parameters);

        ddlRTOCode.DataSource = dr;
        ddlRTOCode.Items.Clear();
        ddlRTOCode.Items.Add(new ListItem("--Please select RTO--", "0"));
        ddlRTOCode.DataTextField = "RTO_NAME";
        ddlRTOCode.DataValueField = "RTO_CODE";
        ddlRTOCode.DataBind();
        ddlRTOCode.SelectedValue = "0";



    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            String DTOCode = Server.HtmlEncode(txtDTOCode.Text.Trim());
            String DTOName = Server.HtmlEncode(txtDTOName.Text.Trim());
           // String Status = Server.HtmlEncode(ddlStatus.SelectedValue);

            SqlParameter[] parameters = new SqlParameter[11];
            parameters[0] = new SqlParameter("@DTO_CODE", DTOCode);
            parameters[1] = new SqlParameter("@DTO_NAME", DTOName);
            parameters[2] = new SqlParameter("@RTO_CODE", ddlRTOCode.SelectedValue);
            parameters[3] = new SqlParameter("@PHONE_NO", Server.HtmlEncode(txtPhoneNo.Text.Trim()));
            parameters[4] = new SqlParameter("@EMAIL", Server.HtmlEncode(txtEmailId.Text.Trim()));
            parameters[5] = new SqlParameter("@ADDRESS", Server.HtmlEncode(txtAddress.Text.Trim()));
            parameters[6] = new SqlParameter("@STATUS", "Active");
            parameters[7] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));

            if (dtocodess.Value == "0")
            {
                parameters[8] = new SqlParameter("@MODE", "DUPLICACY");
                
                var obj2 = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_DTO_MASTER_AED", parameters);


                if ((Int32)obj2 == 0)
                {


                    parameters[8] = new SqlParameter("@MODE", "ADD");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_DTO_MASTER_AED", parameters);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully saved');window.location ='RTA-DTO-MASTER.aspx'", true);

                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('RTO Code OR RTO Name is already exist');", true);

                }

            }

            else
            {

                parameters[8] = new SqlParameter("@MODE", "DUPLICACYEDIT");
                var obj2 = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_DTO_MASTER_AED", parameters);


                if ((Int32)obj2 == 0)
                {

                    parameters[8] = new SqlParameter("@MODE", "EDIT");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_DTO_MASTER_AED", parameters);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully updated');window.location ='RTA-DTO-MASTER.aspx'", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('RTO Code OR RTO Name is already exist');", true);
                }

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
        Response.Redirect("RTA-DTO-MASTER.aspx");
    }
}