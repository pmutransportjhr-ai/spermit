using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using ClosedXML.Excel;

public partial class STA_VERIFY : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Session["AdminLogin"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            this.BindGrid();
            panelGrid.Visible = true; 
            panelUpdate.Visible = false;
            txtrc_no.Enabled = false;
            txtChasis.Enabled = false;
        }
    }
    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        string query = "SELECT * FROM covid19_no_use_vehicle where authority='STA' and status='Applied and Pending at STA Level' order by applied_on desc";
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
            {
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        
        verifyGrid();
    }

    private void verifyGrid()
    {
        try
        {
            panelGrid.Visible = false;
            panelVerify.Visible = true;
            panelUpdate.Visible = false;
            string constr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            string query = "SELECT * FROM covid19_no_use_vehicle where authority='STA' and status in('Approved','Rejected','Application Pending at DTO Level') order by applied_on desc";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridVerify.DataSource = dt;
                        GridVerify.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.SelectedRow;
        txtrc_no.Text = row.Cells[1].Text;
        txtChasis.Text = row.Cells[2].Text;
        txtMobile.Text = row.Cells[4].Text;
        panelGrid.Visible = false;
        panelUpdate.Visible = true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlstatus.SelectedValue == null || ddlstatus.SelectedValue == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('please select status'); </script>");
            }
            else
            {
                string constr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                string rta = Session["AdminLogin"].ToString();
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                string stats = "";
                con = new SqlConnection(constr);
                con.Open();
                string qry = "";
                if (ddlstatus.SelectedValue == "Verify")
                {
                    stats = "Application Pending at DTO Level";
                    qry = "Update covid19_no_use_vehicle set sta_verify_by='" + rta + "', sta_verify_on='" + date + "', sta_verify_comment='" + txtRemarks.Text + "', status='" + stats + "' where rc_no='" + txtrc_no.Text + "' and chasis_no='" + txtChasis.Text + "'";
                }
                else
                {
                    stats = "Rejected";
                    string rc = "R_" + txtrc_no.Text;
                    qry = "Update covid19_no_use_vehicle set sta_verify_by='" + rta + "', sta_verify_on='" + date + "', sta_verify_comment='" + txtRemarks.Text + "', status='" + stats + "', rc_no='" + rc + "' where rc_no='" + txtrc_no.Text + "' and chasis_no='" + txtChasis.Text + "'";
                }
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.ExecuteNonQuery();
                con.Close();
                SMSAlter();
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Verified Successfully');window.location='STA_VERIFY.aspx' </script>");
                GridView1.EditIndex = -1;
                this.BindGrid();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    protected void SMSAlter()
    {
        try
        {
            string mobno = txtMobile.Text;
            string text = "";
            if(ddlstatus.SelectedValue == "Verify")
            {
                text = "Dear applicant your Tax Exemption application for Vehicle no. "+ txtrc_no.Text +" is verified by STA";
            }
            else
            {
                text= "Dear applicant your Tax Exemption application for Vehicle no. "+ txtrc_no.Text +" is rejected by STA due to incorrect details";
            }
             
            SMSSender ss = new SMSSender();
            ss.Sendsms(mobno, text, "Permit-TimeTable");
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Message + "');window.location ='STA_VERIFY.aspx'", true);
        }
    }

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }
    protected void OnPaging1(object sender, GridViewPageEventArgs e)
    {
        GridVerify.PageIndex = e.NewPageIndex;
        this.verifyGrid();
    }
    protected void lkbLogout_Click(object sender, EventArgs e)
    {
        Session.Remove("AdminLogin");
        Response.Redirect("Login.aspx");
    }
    protected void btnPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("Change_password.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string constr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        SqlConnection con = new SqlConnection(constr);
        try
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();

                string qry = "select * from covid19_no_use_vehicle where rc_no= '" + txtSearch.Text + "' and status='Applied and pending at STA Level'";
                SqlDataAdapter sda = new SqlDataAdapter(qry, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.Visible = true;
                    GridView1.DataBind();

                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('No Details Found');window.location='STA_VERIFY.aspx' </script>");
                    GridView1.DataSource = null;
                    GridView1.Visible = false;
                    GridView1.DataBind();
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            string g = ex.Message;
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('" + g + "')</script>");
        }
        finally
        {
            con.Close();
            //con.Dispose();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("STA_VERIFY.aspx");
    }

    
}