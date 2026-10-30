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

public partial class STA_ROUTE_MASTER_LEGACY : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-ROUTE-MASTER.aspx'", true);
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




                    Bind_ddlSource();
                    Bind_ddlDesState();
                    if (hdnID.Value == "0")
                    {
                        btnSave.Visible = true;
                        txtRouteId.Value = hdnID.Value;
                        txtHomeDistance.Text = "";
                        txtTotalDistance.Text = "";
                    }
                    else
                    {

                        txtRouteId.Value = hdnID.Value;
                        SqlParameter[] parameters = new SqlParameter[3];
                        parameters[0] = new SqlParameter("@ID", hdnID.Value);
                        parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                        parameters[2] = new SqlParameter("@MODE", "ROUTE_LIST_BYID");

                        DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST_BYID", parameters);
                        DataTable dt1 = ds.Tables[0];
                        DataTable dt2 = ds.Tables[1];
                        


                        List<ROUTE_MSTR_HDR> rDTO2 = new List<ROUTE_MSTR_HDR>();
                        
                        if (dt1.Rows.Count > 0)
                        {
                            if(Convert.ToString(dt1.Rows[0]["CAN_DELETE"])=="N")
                            {
                                btnSave.Visible = false;
                            }
                            else
                            {
                                btnSave.Visible = true;
                            }
                            
                            ROUTE_MSTR_HDR rDTO = new ROUTE_MSTR_HDR();
                            rDTO.ROUTE_ID = Convert.ToString(dt1.Rows[0]["ROUTE_ID"].ToString());
                            rDTO.ST_FROM = dt1.Rows[0]["ST_FROM"].ToString();
                            rDTO.STATE_CODE = dt1.Rows[0]["STATE_CODE"].ToString();
                            rDTO.ST_TO = dt1.Rows[0]["ST_TO"].ToString();
                            rDTO.TOTAL_DISTANCE = Convert.ToString(dt1.Rows[0]["TOTAL_DISTANCE"].ToString());
                            rDTO.DISTANCE_IN_HOME_STATE = Convert.ToString(dt1.Rows[0]["DISTANCE_IN_HOME_STATE"].ToString());
                            rDTO.NO_OF_VACANCIES = Convert.ToString(dt1.Rows[0]["NO_OF_VACANCIES"].ToString());
                            rDTO.ROUTE_NUMBER = Convert.ToString(dt1.Rows[0]["ROUTE_NUMBER"].ToString());
                            rDTO.STATUS = dt1.Rows[0]["STATUS"].ToString();
                            if (dt2.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt2.Rows)
                                {
                                    ROUTE_MSTR_LDR rDTO1 = new ROUTE_MSTR_LDR();
                                    rDTO1.SEQUENCE = Convert.ToString(dr["SEQUENCE"].ToString());
                                    rDTO1.VIA_CITY = dr["VIA_CITY"].ToString();
                                    rDTO.AddROUTE_MSTR_LDR(rDTO1);
                                }

                                

                            }

                            rDTO2.Add(rDTO);
                        }

                        txtroutjson.Value = JsonConvert.SerializeObject(rDTO2);
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "fnEdit1()", true);
                    }
                }
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-ROUTE-MASTER.aspx'", true);

            }
        }
    }
    public void Bind_ddlSource()
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "HOME_STATE_CITY_LIST");

        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters);
       
        ddlSource.DataSource = dr;
        ddlSource.Items.Clear();
        ddlSource.Items.Add(new ListItem("--Please select source station--", "0"));
        ddlSource.DataTextField = "CITY_NAME";
        ddlSource.DataValueField = "CITY_CODE";
        ddlSource.DataBind();
        ddlSource.SelectedValue = "0";


    }
    public void Bind_ddlDesState()
    {
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "WITHOUT_HOME_STATE_ACTIVE_STATE_LIST");

        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST", parameters);
      
        ddlDesState.DataSource = dr;
        ddlDesState.Items.Clear();
        ddlDesState.Items.Add(new ListItem("--Please select destination state--", "0"));
        ddlDesState.DataTextField = "STATE_NAME";
        ddlDesState.DataValueField = "STATE_CODE";
        ddlDesState.DataBind();
        ddlDesState.SelectedValue = "0";


    }
    protected void ddlDesState_SelectedIndexChanged(object sender, EventArgs e)
    {



        if (ddlDesState.SelectedValue == "0")
        {
            ddlDestination.Items.Clear();
            ddlDestination.Items.Add(new ListItem("--Please select destination station--", "0"));

        }
        else
        {
            
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@ID", ddlDesState.SelectedValue);
            parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
            parameters[2] = new SqlParameter("@MODE", "CITY_LIST_BY_STATE_CODE");

            SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST_BYID", parameters);
     
            ddlDestination.DataSource = dr;
            if (dr.HasRows)
            {
                ddlDestination.Items.Clear();
                ddlDestination.Items.Add(new ListItem("--Please select destination station--", "0"));
                ddlDestination.DataTextField = "CITY_NAME";
                ddlDestination.DataValueField = "CITY_CODE";
                ddlDestination.DataBind();
                ddlDestination.SelectedValue = txtCityCode.Value;

            }
            else
            {
                ddlDestination.Items.Clear();
                ddlDestination.Items.Add(new ListItem("--Please select destination station--", "0"));
            }

        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("STA-ROUTE-MASTER.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        String input = "";
        input = txtroutjson.Value;
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;

        ROUTE_MSTR_HDR rDTO = new JavaScriptSerializer().Deserialize<ROUTE_MSTR_HDR>(input);
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;

            try
            {
                Int32 ID = Convert.ToInt32(txtRouteId.Value);
                sCmd.CommandText = "STA_ROUTE_MSTR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@ROUTE_ID", ID);
                sCmd.Parameters.AddWithValue("@ST_FROM", rDTO.ST_FROM.Trim());
                sCmd.Parameters.AddWithValue("@ST_TO", rDTO.ST_TO.Trim());
                sCmd.Parameters.AddWithValue("@TOTAL_DISTANCE", rDTO.TOTAL_DISTANCE);
                sCmd.Parameters.AddWithValue("@DISTANCE_IN_HOME_STATE", rDTO.DISTANCE_IN_HOME_STATE);
                sCmd.Parameters.AddWithValue("@NO_OF_VACANCIES", rDTO.NO_OF_VACANCIES);
                sCmd.Parameters.AddWithValue("@ROUTE_NUMBER", rDTO.ROUTE_NUMBER);
                sCmd.Parameters.AddWithValue("@STATUS", "Inactive");
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@MODE", "ADD");
                
                    if (ID == 0)
                    { 
                        var obj = sCmd.ExecuteScalar();

                        Int32 RouteId;
                        RouteId = Convert.ToInt32(obj);
                        foreach (ROUTE_MSTR_LDR dr in rDTO.ROUTE_MSTR_LDR)
                        {
                            sCmd.CommandText = "STA_ROUTE_DETL_AED";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();

                            sCmd.Parameters.AddWithValue("@ROUTE_ID", RouteId);
                            sCmd.Parameters.AddWithValue("@SEQUENCE", dr.SEQUENCE);
                            sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                            sCmd.Parameters.AddWithValue("@MODE", "ADD");
                            sCmd.ExecuteNonQuery();

                        }



                        sCmd.CommandText = "STA_ROUTE_MSTR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@ROUTE_ID", RouteId);
                        sCmd.Parameters.AddWithValue("@ST_FROM", rDTO.ST_FROM.Trim());
                        sCmd.Parameters.AddWithValue("@ST_TO", rDTO.ST_TO.Trim());
                        sCmd.Parameters.AddWithValue("@TOTAL_DISTANCE", rDTO.TOTAL_DISTANCE);
                        sCmd.Parameters.AddWithValue("@DISTANCE_IN_HOME_STATE", rDTO.DISTANCE_IN_HOME_STATE);
                        sCmd.Parameters.AddWithValue("@NO_OF_VACANCIES", rDTO.NO_OF_VACANCIES);
                        sCmd.Parameters.AddWithValue("@ROUTE_NUMBER", rDTO.ROUTE_NUMBER);
                        sCmd.Parameters.AddWithValue("@STATUS", "Inactive");
                        sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                        sCmd.Parameters.AddWithValue("@MODE", "UPD");
                        sCmd.ExecuteNonQuery();

                        objTrans.Commit();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Route Details have been saved successfully');window.location ='STA-ROUTE-MASTER.aspx'", true);
                    }
                    else
                    {
                        sCmd.CommandText = "STA_ROUTE_MSTR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters["@MODE"].Value = "EDIT";
                        sCmd.ExecuteNonQuery();

                        sCmd.CommandText = "STA_ROUTE_DETL_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();

                        sCmd.Parameters.AddWithValue("@ROUTE_ID", ID);
                        sCmd.Parameters.AddWithValue("@SEQUENCE", DBNull.Value);
                        sCmd.Parameters.AddWithValue("@VIA_CITY", DBNull.Value);
                        sCmd.Parameters.AddWithValue("@MODE", "DELETE");
                        sCmd.ExecuteNonQuery();

                        

                        foreach (ROUTE_MSTR_LDR dr in rDTO.ROUTE_MSTR_LDR)
                        {
                            sCmd.CommandText = "STA_ROUTE_DETL_AED";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();

                            sCmd.Parameters.AddWithValue("@ROUTE_ID", ID);
                            sCmd.Parameters.AddWithValue("@SEQUENCE", dr.SEQUENCE);
                            sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                            sCmd.Parameters.AddWithValue("@MODE", "ADD");
                            sCmd.ExecuteNonQuery();

                        }

                       
                        sCmd.CommandText = "STA_ROUTE_MSTR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();
                        sCmd.Parameters.AddWithValue("@ROUTE_ID", ID);
                        sCmd.Parameters.AddWithValue("@ST_FROM", rDTO.ST_FROM.Trim());
                        sCmd.Parameters.AddWithValue("@ST_TO", rDTO.ST_TO.Trim());
                        sCmd.Parameters.AddWithValue("@TOTAL_DISTANCE", rDTO.TOTAL_DISTANCE);
                        sCmd.Parameters.AddWithValue("@DISTANCE_IN_HOME_STATE", rDTO.DISTANCE_IN_HOME_STATE);
                        sCmd.Parameters.AddWithValue("@NO_OF_VACANCIES", rDTO.NO_OF_VACANCIES);
                        sCmd.Parameters.AddWithValue("@ROUTE_NUMBER", rDTO.ROUTE_NUMBER);
                        sCmd.Parameters.AddWithValue("@STATUS", "Inactive");
                        sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                        sCmd.Parameters.AddWithValue("@MODE", "UPD");
                        sCmd.ExecuteNonQuery();

                        objTrans.Commit();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Route Details have been updated successfully');window.location ='STA-ROUTE-MASTER.aspx'", true);
                    }
            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-ROUTE-MASTER.aspx'", true);

            }
        }



    }
}