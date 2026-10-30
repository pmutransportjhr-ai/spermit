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
using Newtonsoft.Json;

public partial class STA_STATE_MASTER_NEW : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
       
        if (!IsPostBack)
        {
            
                if (string.IsNullOrEmpty(Request.QueryString["q"]))
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-STATE-MASTER.aspx'", true);
                }
                else
                {

                    String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                    using (SqlConnection sCon = new SqlConnection(sConnectionString))
                    {
                        try
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
                            StateFlag.Enabled = false;
                                       
                            if (hdnID.Value == "0")
                            {
                                txtStateCode.Text = hdnID.Value;
                                txtStateName.Text = String.Empty;
                                txtStateAbbr.Text = String.Empty;
                               
                                //ddlStatus.SelectedValue = "0";
                            }
                            else
                            {
                                sCon.Open();
                                SqlCommand sCmd = new SqlCommand();
                                sCmd.Connection = sCon;
                                sCmd.CommandText = "STA_SP_STATE_MSTR";
                                sCmd.CommandType = CommandType.StoredProcedure;
                                sCmd.Parameters.Clear();
                                sCmd.Parameters.AddWithValue("@STATE_CODE", hdnID.Value);
                                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                                sCmd.Parameters.AddWithValue("@MODE", "STATE_LIST_BYID");
                                DataSet ds = new DataSet();
                                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                                da.Fill(ds);
                                DataTable dt = new DataTable();
                                DataTable dt2 = new DataTable();

                                string UserRole = Convert.ToString(Session["USER_ROLE"]);

                                dt = ds.Tables[0];
                                dt2 = ds.Tables[1];

                                if (dt.Rows.Count > 0)
                                {
                                    txtStateCode.Text = dt.Rows[0]["STATE_CODE"].ToString();
                                    txtStateName.Text = dt.Rows[0]["STATE_NAME"].ToString();
                                    txtStateAbbr.Text = dt.Rows[0]["STATE_ABBREVIATION"].ToString(); 
                                    if (dt.Rows[0]["HOME_STATE"].ToString() == "Y")
                                    {
                                        StateFlag.Checked = true;
                                        divTrips.Visible = false;
                                    }
                                    else
                                    {
                                        divTrips.Visible = true;
                                    }
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
                                List<STATE_MSTR_HDR> rDTO= new List<STATE_MSTR_HDR>();
                                STATE_MSTR_HDR y = new STATE_MSTR_HDR();
                                        
                                          
                                if (dt2.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt2.Rows)
                                    {
                                        STATE_MSTR_LDR x = new STATE_MSTR_LDR();
                                        x.KM_UPTO = Convert.ToString(dr["KM_UPTO"].ToString());
                                        x.NUMBER_OF_ROUND_TRIP = dr["NUMBER_OF_ROUND_TRIP"].ToString();
                                        y.AddSTATE_MSTR_LDR(x);
                                    }
                                    rDTO.Add(y);
                                }
                                txtStatejson.Value = JsonConvert.SerializeObject(rDTO);
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "fnEdit1()", true);


                            }


                        }



                        catch (Exception ex)
                        {
                            String Message = ex.Message;
                            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-STATE-MASTER.aspx'", true);

                        }
                    }
    }
        }


    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        String input = "";
        input = txtStatejson.Value;
      
        Int32 StateCode = Convert.ToInt32(Server.HtmlEncode(txtStateCode.Text.Trim()));

        String StateName = Server.HtmlEncode(txtStateName.Text.Trim());
        String StateAbbr = Server.HtmlEncode(txtStateAbbr.Text.Trim());
        String HomeState = "";
        if (StateFlag.Checked)
        {
            HomeState = "Y";
        }
        else
        {
            HomeState = "N";
        }
        //String Status = Server.HtmlEncode(ddlStatus.SelectedValue);
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;

        STATE_MSTR_HDR rDTO = new JavaScriptSerializer().Deserialize<STATE_MSTR_HDR>(input);
       
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
               
            try
            {

                
                sCmd.CommandText = "STA_SP_STATE_MSTR";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@STATE_CODE", StateCode);
                sCmd.Parameters.AddWithValue("@STATE_NAME", StateName);
                sCmd.Parameters.AddWithValue("@STATE_ABBREVIATION", StateAbbr);
                sCmd.Parameters.AddWithValue("@HOME_STATE", HomeState);
                sCmd.Parameters.AddWithValue("@STATUS", "Active");
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                if (HomeState == "Y")
                {
                    sCmd.Parameters.AddWithValue("@Mode", "HOMESTATE");
                    var obj = sCmd.ExecuteScalar();
                    if (Convert.ToInt32(obj) == 0)
                    {
                        sCmd.CommandText = "STA_SP_STATE_MSTR";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters["@MODE"].Value = "DUPLICACY";

                        var obj1 = sCmd.ExecuteScalar(); ;
                        if (Convert.ToInt32(obj1) == 0)
                        {
                            if (StateCode == 0)
                            {


                                sCmd.CommandText = "STA_SP_STATE_MSTR";
                                sCmd.CommandType = CommandType.StoredProcedure;
                                sCmd.Parameters["@MODE"].Value = "ADD";
                                var code = sCmd.ExecuteScalar();

                                Int32 StateId;
                                StateId = Convert.ToInt32(code);
                                foreach (STATE_MSTR_LDR dr in rDTO.STATE_MSTR_LDR)
                                {
                                    sCmd.CommandText = "STA_SP_STATE_MSTR";
                                    sCmd.CommandType = CommandType.StoredProcedure;
                                    sCmd.Parameters.Clear();

                                    sCmd.Parameters.AddWithValue("@STATE_CODE", StateId);
                                    sCmd.Parameters.AddWithValue("@KM_UPTO", dr.KM_UPTO);
                                    sCmd.Parameters.AddWithValue("@NUMBER_OF_ROUND_TRIP", dr.NUMBER_OF_ROUND_TRIP);
                                    sCmd.Parameters.AddWithValue("@MODE", "ADD_LDR");
                                    sCmd.ExecuteNonQuery();

                                }

                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully saved');window.location ='STA-STATE-MASTER.aspx'", true);
                            }
                            else
                            {
                                sCmd.CommandText = "STA_SP_STATE_MSTR";
                                sCmd.CommandType = CommandType.StoredProcedure;
                                sCmd.Parameters["@MODE"].Value = "EDIT";

                                sCmd.ExecuteNonQuery();

                                sCmd.CommandText = "STA_SP_STATE_MSTR";
                                sCmd.CommandType = CommandType.StoredProcedure;
                                sCmd.Parameters.Clear();

                                sCmd.Parameters.AddWithValue("@STATE_CODE", StateCode);
                                sCmd.Parameters.AddWithValue("@KM_UPTO", DBNull.Value);
                                sCmd.Parameters.AddWithValue("@NUMBER_OF_ROUND_TRIP", DBNull.Value);
                                sCmd.Parameters.AddWithValue("@MODE", "DELETE_LDR");
                                sCmd.ExecuteNonQuery();



                                foreach (STATE_MSTR_LDR dr in rDTO.STATE_MSTR_LDR)
                                {
                                    sCmd.CommandText = "STA_SP_STATE_MSTR";
                                    sCmd.CommandType = CommandType.StoredProcedure;
                                    sCmd.Parameters.Clear();

                                    sCmd.Parameters.AddWithValue("@STATE_CODE", StateCode);
                                    sCmd.Parameters.AddWithValue("@KM_UPTO", dr.KM_UPTO);
                                    sCmd.Parameters.AddWithValue("@NUMBER_OF_ROUND_TRIP", dr.NUMBER_OF_ROUND_TRIP);
                                    sCmd.Parameters.AddWithValue("@MODE", "ADD_LDR");
                                    sCmd.ExecuteNonQuery();

                                }
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully updated');window.location ='STA-STATE-MASTER.aspx'", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('State mame is already exist');", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Home state is already defined');", true);

                    }


                }
                else
                {
                    sCmd.Parameters.AddWithValue("@Mode", "DUPLICACY");
                    
                    var obj2 = sCmd.ExecuteScalar();

                    if (Convert.ToInt32(obj2) == 0)
                    {
                        if (StateCode == 0)
                        {

                            sCmd.CommandText = "STA_SP_STATE_MSTR";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters["@MODE"].Value = "ADD";
                            var code = sCmd.ExecuteScalar();

                            Int32 StateId;
                            StateId = Convert.ToInt32(code);
                            foreach (STATE_MSTR_LDR dr in rDTO.STATE_MSTR_LDR)
                            {
                                sCmd.CommandText = "STA_SP_STATE_MSTR";
                                sCmd.CommandType = CommandType.StoredProcedure;
                                sCmd.Parameters.Clear();

                                sCmd.Parameters.AddWithValue("@STATE_CODE", StateId);
                                sCmd.Parameters.AddWithValue("@KM_UPTO", dr.KM_UPTO);
                                sCmd.Parameters.AddWithValue("@NUMBER_OF_ROUND_TRIP", dr.NUMBER_OF_ROUND_TRIP);
                                sCmd.Parameters.AddWithValue("@MODE", "ADD_LDR");
                                sCmd.ExecuteNonQuery();

                            }

                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully saved');window.location ='STA-STATE-MASTER.aspx'", true);
                        }
                        else
                        {
                            sCmd.CommandText = "STA_SP_STATE_MSTR";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters["@MODE"].Value = "EDIT";
                            sCmd.ExecuteNonQuery();
                            sCmd.CommandText = "STA_SP_STATE_MSTR";
                            sCmd.CommandType = CommandType.StoredProcedure;
                            sCmd.Parameters.Clear();

                            sCmd.Parameters.AddWithValue("@STATE_CODE", StateCode);
                            sCmd.Parameters.AddWithValue("@KM_UPTO", DBNull.Value);
                            sCmd.Parameters.AddWithValue("@NUMBER_OF_ROUND_TRIP", DBNull.Value);
                            sCmd.Parameters.AddWithValue("@MODE", "DELETE_LDR");
                            sCmd.ExecuteNonQuery();



                            foreach (STATE_MSTR_LDR dr in rDTO.STATE_MSTR_LDR)
                            {
                                sCmd.CommandText = "STA_SP_STATE_MSTR";
                                sCmd.CommandType = CommandType.StoredProcedure;
                                sCmd.Parameters.Clear();

                                sCmd.Parameters.AddWithValue("@STATE_CODE", StateCode);
                                sCmd.Parameters.AddWithValue("@KM_UPTO", dr.KM_UPTO);
                                sCmd.Parameters.AddWithValue("@NUMBER_OF_ROUND_TRIP", dr.NUMBER_OF_ROUND_TRIP);
                                sCmd.Parameters.AddWithValue("@MODE", "ADD_LDR");
                                sCmd.ExecuteNonQuery();

                            }
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully updated');window.location ='STA-STATE-MASTER.aspx'", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('State mame is already exist');", true);

                    }
                }
                objTrans.Commit();
               
            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
            }
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("STA-STATE-MASTER.aspx");
    }
    protected void StateFlag_CheckedChanged(object sender, EventArgs e)
    {
        if (StateFlag.Checked)
        {
           divTrips.Visible = false;
        }
        else
        {
            divTrips.Visible = true;
        }
    }
}