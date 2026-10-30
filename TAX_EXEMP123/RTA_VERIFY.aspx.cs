using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class RTA_VERIFY : System.Web.UI.Page
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
        string query = "";
        //if (Session["AdminLogin"].ToString() == "RTA_RANCHI")
        //{
        //    query = "SELECT * FROM covid19_no_use_vehicle where rto_no in (1,7,20,23,8) and status='Applied and Pending at RTA Level' order by applied_on desc";
        //}
        //if (Session["AdminLogin"].ToString() == "RTA_HAZARIBAGH")
        //{
        //    query = "SELECT * FROM covid19_no_use_vehicle where rto_no in (2,9,10,13,11,12,24) and status='Applied and Pending at RTA Level' order by applied_on desc";
        //}
        //if (Session["AdminLogin"].ToString() == "RTA_PALAMU")
        //{
        //    query = "SELECT * FROM covid19_no_use_vehicle where rto_no in (3,14,19) and status='Applied and Pending at RTA Level' order by applied_on desc";
        //}
        //if (Session["AdminLogin"].ToString() == "RTA_DUMKA")
        //{
        //    query = "SELECT * FROM covid19_no_use_vehicle where rto_no in (4,15,17,21,16,18) and status='Applied and Pending at RTA Level' order by applied_on desc";
        //}
        //if (Session["AdminLogin"].ToString() == "RTA_KOLHAN")
        //{
        //    query = "SELECT * FROM covid19_no_use_vehicle where rto_no in (5,6,22) and status='Applied and Pending at RTA Level' order by applied_on desc";
        //}

        if (Session["AdminLogin"].ToString() == "RTA_RANCHI")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rta_code ='1' and status='Applied and Pending at RTA Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "RTA_HAZARIBAGH")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rta_code ='2' and status='Applied and Pending at RTA Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "RTA_PALAMU")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rta_code ='3' and status='Applied and Pending at RTA Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "RTA_DUMKA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rta_code ='4' and status='Applied and Pending at RTA Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "RTA_KOLHAN")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rta_code ='5' and status='Applied and Pending at RTA Level' order by applied_on desc";
        }

        
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
    private void BindGridVerify()
    {
        try
        {
            panelGrid.Visible = false;
            panelVerify.Visible = true;
            panelUpdate.Visible = false;
            string constr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            string query = "";
            if (Session["AdminLogin"].ToString() == "RTA_RANCHI")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rta_code ='1' and status in('Approved','Rejected','Application Pending at DTO Level') and authority='RTA' order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "RTA_HAZARIBAGH")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rta_code ='2' and status in('Approved','Rejected','Application Pending at DTO Level') and authority='RTA' order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "RTA_PALAMU")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rta_code ='3' and status in('Approved','Rejected','Application Pending at DTO Level') and authority='RTA' order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "RTA_DUMKA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rta_code ='4' and status in('Approved','Rejected','Application Pending at DTO Level') and authority='RTA' order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "RTA_KOLHAN")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rta_code ='5' and status in('Approved','Rejected','Application Pending at DTO Level') and authority='RTA' order by applied_on desc";
            }
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
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        
        BindGridVerify();        
    }
    protected void OnPaging1(object sender, GridViewPageEventArgs e)
    {
        GridVerify.PageIndex = e.NewPageIndex;
        this.BindGridVerify();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.SelectedRow;
        txtrc_no.Text = row.Cells[1].Text;
        txtChasis.Text = row.Cells[2].Text;
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
                    qry = "Update covid19_no_use_vehicle set rta_verified_by='" + rta + "', rta_verified_on='" + date + "', rta_verify_comment='" + txtRemarks.Text + "', status='" + stats + "' where rc_no='" + txtrc_no.Text + "' and chasis_no='" + txtChasis.Text + "'";
                }
                else
                {
                    stats = "Rejected";
                    string rc = "R_" + txtrc_no.Text;
                    qry = "Update covid19_no_use_vehicle set rta_verified_by='" + rta + "', rta_verified_on='" + date + "', rta_verify_comment='" + txtRemarks.Text + "', status='" + stats + "', rc_no='" + rc + "' where rc_no='" + txtrc_no.Text + "' and chasis_no='" + txtChasis.Text + "'";
                }
                
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.ExecuteNonQuery();
                con.Close();
                SMSAlter();
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Updated Successfully');window.location='RTA_VERIFY.aspx' </script>");
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
                text = "Dear applicant your Tax Exemption application for Vehicle no. "+ txtrc_no.Text +" is verified by RTA";
            }
            else
            {
                text= "Dear applicant your Tax Exemption application for Vehicle no. "+ txtrc_no.Text +" is rejected by RTA due to incorrect details";
            }
            SMSSender ss = new SMSSender();
            ss.Sendsms(mobno, text,"Permit-TimeTable");
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Message + "');window.location ='RTA_VERIFY.aspx'", true);
        }
    }

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    
    protected void lkbLogout_Click(object sender, EventArgs e)
    {
        Session.Remove("AdminLogin");
        Response.Redirect("Login.aspx");
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
                string rta = "";
                if (Session["AdminLogin"].ToString() == "RTA_RANCHI")
                {
                    rta = "1";
                }
                if (Session["AdminLogin"].ToString() == "RTA_HAZARIBAGH")
                {
                    rta = "2";
                }
                if (Session["AdminLogin"].ToString() == "RTA_PALAMU")
                {
                    rta = "3";
                }
                if (Session["AdminLogin"].ToString() == "RTA_DUMKA")
                {
                    rta = "4";
                }
                if (Session["AdminLogin"].ToString() == "RTA_KOLHAN")
                {
                    rta = "5";
                }
                string qry = "select * from covid19_no_use_vehicle where rc_no= '" + txtSearch.Text + "' and rta_code='" + rta + "'";
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
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('No Details Found');window.location='RTA_VERIFY.aspx' </script>");
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
        Response.Redirect("RTA_VERIFY.aspx");
    }
    protected void btnPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("Change_password.aspx");
    }
}