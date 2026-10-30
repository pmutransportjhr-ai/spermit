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
using System.Web.Script.Serialization;
using System.Configuration;
using System.Text;

public partial class RTA_USER_SETUP_NEW : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    public string menuTable { get; set; }
    //private static string ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        txtMobileNo.MaxLength = 10;
         //txtConfirmPassword.TextMode = TextBoxMode.Password;
        if (!IsPostBack)
        {
             try
                {

                    Session["salt"] = ConfigurationManager.AppSettings["SecurityKey"];
                    //BindMenuItems();
            BindDesignation();
            BindDepartment();
            BindRTO();
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-USER-MASTER.aspx'", true);
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


                    ID.Value = IDx.Value;





                    if (ID.Value == "0")   //Save New User Case
                {

                }
                else    //Edit Existing Case
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@EMPLOYEE_ID", ID.Value);
                    
                    parameters[1] = new SqlParameter("@MODE", "LISTBYID");

                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_USER_MSTR_AED", parameters);
                  
                   

                    
                        DataTable dt1 = ds.Tables[0];
                        //DataTable dtLDR = ds.Tables[1];
                        string UserRole = Convert.ToString(Session["USER_ROLE"]);
                        if (dt1.Rows.Count > 0)
                        {

                            ddlRTO.SelectedValue = dt1.Rows[0]["RTO_CODE"].ToString();
                            txtEmployeeID.Value = dt1.Rows[0]["EMPLOYEE_ID"].ToString();
                            txtName.Text = dt1.Rows[0]["EMPLOYEE_NAME"].ToString();
                            txtEmailId.Text = dt1.Rows[0]["EMAIL_ID"].ToString();
                            ddlULevel.SelectedValue = dt1.Rows[0]["USER_LEVEL"].ToString();
                            txtLoginId.Text = dt1.Rows[0]["LOGIN_ID"].ToString();
                            txtLoginId.ReadOnly = true;
                            txtPassword.Attributes.Add("value",dt1.Rows[0]["PASSWORD"].ToString());
                            txtConfirmPassword.Attributes.Add("value", dt1.Rows[0]["PASSWORD"].ToString());
                            ddlDesignation.SelectedValue = dt1.Rows[0]["DESG_ID"].ToString();
                            txtMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                            ddlDepartment.SelectedValue = dt1.Rows[0]["DEPT_ID"].ToString(); 
                            ddlSecurityQuestion.SelectedValue=dt1.Rows[0]["SECURITY_QUESTIONS"].ToString();
                            ddlStatus.SelectedValue=dt1.Rows[0]["STATUS"].ToString();
                            txtSecurityAnswer.Text = dt1.Rows[0]["SECURITY_ANSWERS"].ToString();
                            if (UserRole == "Super Admin")
                            {
                                btnSave.Visible = true;

                            }
                            else
                            {
                                btnSave.Visible = false;

                            }

                       
                            txtPassword.Attributes.Add("ReadOnly", "false");
                            txtConfirmPassword.Attributes.Add("ReadOnly", "false");
                        }


                        //List<Menuaccess> menulist = new List<Menuaccess>();
                        //foreach (DataRow dr1 in dtLDR.Rows)
                        //{
                        //    Menuaccess LDRmenu = new Menuaccess();
                        //    LDRmenu.MENU_ID = Convert.ToString(dr1["MENU_ID"].ToString());
                        //    LDRmenu.READ_ACCESS = Convert.ToString(dr1["READ_ACCESS"].ToString());
                        //    LDRmenu.WRITE_ACCESS = Convert.ToString(dr1["WRITE_ACCESS"].ToString());
                        //    menulist.Add(LDRmenu);

                        //}



                        //hdnJsonData.Value = JsonConvert.SerializeObject(menulist);
                       // ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "fnEdit1()", true);





                    

                }
            }
                }

             catch (Exception ex)
             {
                 String Message = ex.Message;
                 Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                 ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-USER-MASTER.aspx'", true);

             }

        }

    }

    private void BindRTO()
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "RTO_LIST");

        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", parameters);
     
        ddlRTO.DataSource = dr;
        ddlRTO.Items.Clear();
        ddlRTO.Items.Add(new ListItem("--Please select RTO --", "0"));
        ddlRTO.DataTextField = "RTO_NAME";
        ddlRTO.DataValueField = "RTO_CODE";
        ddlRTO.DataBind();
          
       
    }

    private void BindDepartment()
    {

        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "DD_DEPARTMENT_LIST");

        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", parameters);
     
        ddlDepartment.DataSource = dr;
        ddlDepartment.Items.Clear();
        ddlDepartment.Items.Add(new ListItem("--Please select department --", "0"));
        ddlDepartment.DataTextField = "DEPT_NAME";
        ddlDepartment.DataValueField = "DEPT_ID";
        ddlDepartment.DataBind();
      
    }

    private void BindDesignation()
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "DD_DESIGNATION_LIST");

        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", parameters);
        ddlDesignation.DataSource = dr;
        ddlDesignation.Items.Clear();
        ddlDesignation.Items.Add(new ListItem("--Please select designation --", "0"));
        ddlDesignation.DataTextField = "DESG_NAME";
        ddlDesignation.DataValueField = "DESG_ID";
        ddlDesignation.DataBind();
       

    }

    //private void BindMenuItems()
    //{
    //    SqlParameter[] parameters = new SqlParameter[2];
    //    parameters[0] = new SqlParameter("@EMPLOYEE_ID", null); //Convert.ToString(Context.Session["EMPLOYEE_ID"]));
    //    parameters[1] = new SqlParameter("@MODE", "LIST_RTA_MENU");
    //    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_GET_MENU_ITEMS", parameters);
    //    DataTable dt = new DataTable();
    //    dt = ds.Tables[0];
    //    StringBuilder sb = new StringBuilder();
    //    if (dt.Rows.Count > 0)
    //    {
    //        int i = 1;
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            sb.Append("<tr class='tblparent' id=trcheck_" + Convert.ToString(dr["MENU_ID"]) + ">");
    //            sb.Append("<td id=" + Convert.ToString(dr["MENU_ID"]) + ">" + Convert.ToString(dr["MENU_ITEM"]) + "</td>");
    //            sb.Append("<td><input type='checkbox' id =Read" + Convert.ToString(dr["MENU_ID"]) + " name =Read" + Convert.ToString(dr["MENU_ID"]) + " class=" + Convert.ToString(dr["MENU_ID"]) + "></td>");
    //            sb.Append("<td><input type='checkbox' id =Write" + Convert.ToString(dr["MENU_ID"]) + " name =Write" + Convert.ToString(dr["MENU_ID"]) + " class=" + Convert.ToString(dr["MENU_ID"]) + "></td>");
  
    //            i++;
    //        }
    //    }
    //    menuTable = sb.ToString();

    //}


    protected void btnSave_Click(object sender, EventArgs e)
    {


        String queryString = CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["q"]), true);

        foreach (String str in queryString.Split(new Char[] { '&' }))
        {
            QueryString qs = new QueryString();
            qs.Name = str.Split(new Char[] { '=' })[0];
            qs.Value = str.Split(new Char[] { '=' })[1];
            QueryStr.Add(qs);
        }

        QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault();
        String ID = IDx.Value;

        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;


        //string input = hdnMenuItems.Value;
        //Menuaccess menuaccess = new JavaScriptSerializer().Deserialize<Menuaccess>(input);
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
                sCmd.CommandText = "RTA_USER_MSTR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@RTO_CODE", ddlRTO.SelectedValue);
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", ID);
                sCmd.Parameters.AddWithValue("@EMPLOYEE_NAME", txtName.Text);
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text);
                sCmd.Parameters.AddWithValue("@USER_LEVEL", ddlULevel.SelectedValue);
                sCmd.Parameters.AddWithValue("@DEPARTMENT", ddlDepartment.SelectedValue);
                sCmd.Parameters.AddWithValue("@DESIGNATION", ddlDesignation.SelectedValue);
                sCmd.Parameters.AddWithValue("@STATUS", ddlStatus.SelectedValue);
                sCmd.Parameters.AddWithValue("@LOGIN_ID", txtLoginId.Text);
                //sCmd.Parameters.AddWithValue("@PASSWORD", txtPassword.Text);
                sCmd.Parameters.AddWithValue("@SECURITY_QUESTIONS", ddlSecurityQuestion.Text);
                sCmd.Parameters.AddWithValue("@SECURITY_ANSWERS", txtSecurityAnswer.Text);
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text);
                sCmd.Parameters.AddWithValue("@MODIFIED_BY", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@MODE", "DUPLICACY");

                var obj1 = sCmd.ExecuteScalar();
                if (Convert.ToInt32(obj1) == 0)
                {
                    if (txtEmployeeID.Value == "0")
                    {
                        sCmd.CommandText = "RTA_USER_MSTR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        string Tpass = hashcodegenerate.GetSHA512(hfpwd.Value + Session["salt"].ToString());
                        sCmd.Parameters.AddWithValue("@PASSWORD", Tpass);
                       
                        sCmd.Parameters["@MODE"].Value = "ADD";
                        var objEmployee = sCmd.ExecuteScalar();

                        //sCmd.CommandText = "RTA_USER_MENU_ACCESS_AED";
                        //sCmd.CommandType = CommandType.StoredProcedure;
                        //sCmd.Parameters.Clear();

                        


                        //sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", objEmployee.ToString());
                        //sCmd.Parameters.AddWithValue("@MENU_ID", " ");
                        //sCmd.Parameters.AddWithValue("@READ_ACCESS", " ");
                        //sCmd.Parameters.AddWithValue("@WRITE_ACCESS", " ");
                        //sCmd.Parameters.AddWithValue("@MODE", "DELETE");
                        //sCmd.ExecuteNonQuery();
                        //foreach (Menuaccess menu in menuaccess.MenuaccessM)
                        //{
                        //    sCmd.CommandText = "RTA_USER_MENU_ACCESS_AED";
                        //    sCmd.CommandType = CommandType.StoredProcedure;
                        //    sCmd.Parameters.Clear();
                            
                        //    sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", objEmployee.ToString());
                        //    sCmd.Parameters.AddWithValue("@MENU_ID", menu.MENU_ID);
                        //    sCmd.Parameters.AddWithValue("@READ_ACCESS", menu.READ_ACCESS);
                        //    sCmd.Parameters.AddWithValue("@WRITE_ACCESS", menu.WRITE_ACCESS);
                        //    sCmd.Parameters.AddWithValue("@MODE", "ADD");
                        //    sCmd.ExecuteNonQuery();
                            

                        //}



                        objTrans.Commit();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully saved');window.location ='RTA-USER-MASTER.aspx'", true);
                    }
                    else
                    {
                        sCmd.CommandText = "RTA_USER_MSTR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.AddWithValue("@PASSWORD", txtPassword.Text.Trim());
                        sCmd.Parameters["@MODE"].Value = "EDIT";
                        sCmd.ExecuteNonQuery();

                        //sCmd.CommandText = "RTA_USER_MENU_ACCESS_AED";
                        //sCmd.CommandType = CommandType.StoredProcedure;
                        //sCmd.Parameters.Clear();
                        //sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", ID);
                        //sCmd.Parameters.AddWithValue("@MENU_ID", " ");
                        //sCmd.Parameters.AddWithValue("@READ_ACCESS", " ");
                        //sCmd.Parameters.AddWithValue("@WRITE_ACCESS", " ");
                        //sCmd.Parameters.AddWithValue("@MODE", "DELETE");
                        //sCmd.ExecuteNonQuery();
                        //foreach (Menuaccess menu in menuaccess.MenuaccessM)
                        //{
                        //    sCmd.CommandText = "RTA_USER_MENU_ACCESS_AED";
                        //    sCmd.CommandType = CommandType.StoredProcedure;
                        //    sCmd.Parameters.Clear();
                           
                        //    sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", ID);
                        //    sCmd.Parameters.AddWithValue("@MENU_ID", menu.MENU_ID);
                        //    sCmd.Parameters.AddWithValue("@READ_ACCESS", menu.READ_ACCESS);
                        //    sCmd.Parameters.AddWithValue("@WRITE_ACCESS", menu.WRITE_ACCESS);
                        //    sCmd.Parameters.AddWithValue("@MODE", "ADD");
                        //    sCmd.ExecuteNonQuery();
                            

                        //}

                        objTrans.Commit(); 
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully updated');window.location ='RTA-USER-MASTER.aspx'", true);
                    }

                }
                else
                {
                    objTrans.Commit();
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('User name is already exist');window.location ='RTA-USER-SETUP-NEW.aspx'", true);

                }




            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-USER-SETUP-NEW.aspx'", true);
            }

        }
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("RTA-USER-MASTER.aspx");
    }





}
