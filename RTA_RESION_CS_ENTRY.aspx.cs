using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Text;
using System.Configuration;

public partial class RTA_RESION_CS_ENTRY : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divdetail.Visible = false;
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM RTA_PERMIT_MASTER WHERE REGISTRATION_NUMBER ='" + txtRegistration.Text + "' and PERMIT_NUMBER='" + txtPermit.Text + "'"))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    txtAppID.Text = sdr["APP_ID"].ToString();
                    txtApplication.Text = sdr["APPLICATION_NUMBER"].ToString();
                    ddlRTO.SelectedValue = sdr["RTO_CODE"].ToString();
                    txtStatus.Text = sdr["APPLICATION_STATUS"].ToString();
                    txtCSStatus.Text = sdr["CS_STATUS"].ToString();
                    txtDate.Text = sdr["APPROVED_ON"].ToString();
                    txtModifiedby.Text = "Superadmin";
                    txtmodifiedon.Text = DateTime.Now.ToString();
                    divdetail.Visible = true;
                }
                con.Close();
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        DateTime create = Convert.ToDateTime(txtDate.Text);
        DateTime modify = Convert.ToDateTime(txtmodifiedon.Text);
        try
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO RTA_PERMIT_MASTER_CS_LDR (APP_ID,APPLICATION_NUMBER,REGISTRATION_NUMBER,PERMIT_NUMBER,RTO_CODE,PERMIT_STATUS,CS_STATUS,CREATED_ON,MODIFIED_BY,MODIFIED_ON) VALUES (@APP_ID,@APPLICATION_NUMBER,@REGISTRATION_NUMBER,@PERMIT_NUMBER,@RTO_CODE,@PERMIT_STATUS,@CS_STATUS,@CREATED_ON,@MODIFIED_BY,@MODIFIED_ON)"))
                {
                    cmd.Parameters.AddWithValue("@APP_ID", txtAppID.Text.Trim());
                    cmd.Parameters.AddWithValue("@APPLICATION_NUMBER", txtApplication.Text.Trim());
                    cmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", txtRegistration.Text.Trim());
                    cmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtPermit.Text.Trim());
                    cmd.Parameters.AddWithValue("@RTO_CODE", ddlRTO.SelectedValue);
                    cmd.Parameters.AddWithValue("@PERMIT_STATUS", txtStatus.Text.Trim());
                    cmd.Parameters.AddWithValue("@CS_STATUS", txtCSStatus.Text.Trim());
                    cmd.Parameters.AddWithValue("@CREATED_ON", create);
                    cmd.Parameters.AddWithValue("@MODIFIED_ON", modify);
                    cmd.Parameters.AddWithValue("@MODIFIED_BY", txtModifiedby.Text.Trim());

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Details have been saved successfully');window.location ='RTA_RESION_CS_ENTRY.aspx'", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
        
    }
    
}