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
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Globalization;
using System.Web.Services;

public partial class mobile_update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divMob.Visible = false;
           
            txtNewMobile.MaxLength = 10;
        

        }
    }
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        try
        {
            string regNumber = txtRegNo.Text.Trim();
            string permitType = ddlTA.SelectedValue;

            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "MOBILE_UPDATE_HDR";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", regNumber);
                sCmd.Parameters.AddWithValue("@TYPE", permitType);
                sCmd.Parameters.AddWithValue("@MODE", "CHECK_SELECT");

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);

                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (Convert.ToString(dt.Rows[0]["MSG"]) != "Found")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]) + "');window.location ='mobile_update.aspx'", true);
                    return;
                }
                

                if (Convert.ToString(dt.Rows[0]["MSG"]) == "Found")
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        lblEngineNo.Text = Convert.ToString(dr["ENGINE_NUMBER"]);
                        lblChasisNo.Text = Convert.ToString(dr["CHASIS_NUMBER"]);
                        lblAppNo.Text = Convert.ToString(dr["APPLICATION_NUMBER"]);
                        lblMobileNo.Text = Convert.ToString(dr["MOBILE_NUMBER"]);
                    }
                    if (lblEngineNo.Text == txtEngineNo.Text && lblChasisNo.Text == txtChasisNo.Text)
                    {
                        divMob.Visible = true;
                    
                        ddlTA.Enabled = false;
                        txtChasisNo.Enabled = false;
                        txtEngineNo.Enabled = false;
                        txtRegNo.Enabled = false;
                        txtPermitNo.Enabled = false;
                        btnValidate.Enabled = false;
                    }
                    else 
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Details not matched!.'); window.location.href='mobile_update.aspx'", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]) + "');window.location ='mobile_update.aspx'", true);
                }
                
            }
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please try again.'); window.location.href='mobile_update.aspx'", true);
            //obTrans.Rollback();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("mobile_update.aspx");
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string reg = txtRegNo.Text.Trim();
        string permit = txtPermitNo.Text.Trim();
        string engine = txtEngineNo.Text.Trim();
        string chasis = txtChasisNo.Text.Trim();
        string type = ddlTA.SelectedValue.Trim();

        using (SqlConnection scon = new SqlConnection(
               ConfigurationManager.ConnectionStrings["connectionString"].ToString()))
        {
            SqlTransaction obTrans = null;
            try
            {
                scon.Open();
                obTrans = scon.BeginTransaction();

                SqlCommand cmd = new SqlCommand("MOBILE_UPDATE_HDR", scon, obTrans);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", reg);
                cmd.Parameters.AddWithValue("@PERMIT_NUMBER", permit);
                cmd.Parameters.AddWithValue("@ENGINE_NUMBER", engine);
                cmd.Parameters.AddWithValue("@CHASIS_NUMBER", chasis);
                cmd.Parameters.AddWithValue("@TYPE", type);
                cmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtNewMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@OLD_MOBILE_NUMBER", lblMobileNo.Text);
                cmd.Parameters.AddWithValue("@APPLICATION_NUMBER", lblAppNo.Text);
                cmd.Parameters.AddWithValue("@MODE", "UPDATE");

                cmd.ExecuteNonQuery();
                obTrans.Commit();

                ScriptManager.RegisterClientScriptBlock(
                    Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Updated Successfully'); window.location.href='mobile_update.aspx';",
                    true);
            }
            catch (Exception)
            {
                if (obTrans != null) obTrans.Rollback();

                ScriptManager.RegisterClientScriptBlock(
                    Page, Page.GetType(), Guid.NewGuid().ToString(),
                    "alert('Please try again.'); window.location.href='mobile_update.aspx';",
                    true);
            }
        }
    }

}