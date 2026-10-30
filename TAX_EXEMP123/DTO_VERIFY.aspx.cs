using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class DTO_VERIFY : System.Web.UI.Page
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

        if (Session["AdminLogin"].ToString() == "DTO_RANCHI")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =1 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_HAZARIBAGH")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =2 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_PALAMU")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =3 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_DUMKA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =4 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_JAMSHEDPUR")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =5 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_CHAIBASA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =6 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_GUMLA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =7 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_LOHARDAGA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =8 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_BOKARO")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =9 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_DHANBAD")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =10 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_GIRIDIH")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =11 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_KODERMA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =12 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_CHATRA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =13 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_GARHWA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =14 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_DEOGHAR")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =15 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_PAKUR")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =16 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_GODDA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =17 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_SAHEBGANJ")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =18 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_LATEHAR")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =19 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_SIMDEGA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =20 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_JAMTARA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =21 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_SARAIKELA")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =22 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_KHUNTI")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =23 and status='Application Pending at DTO Level' order by applied_on desc";
        }
        if (Session["AdminLogin"].ToString() == "DTO_RAMGARH")
        {
            query = "SELECT * FROM covid19_no_use_vehicle where rto_no =24 and status='Application Pending at DTO Level' order by applied_on desc";
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

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        verifygrid();
    }

    private void verifygrid()
    {
        try
        {
            panelGrid.Visible = false;
            panelVerify.Visible = true;
            panelUpdate.Visible = false;
            string constr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            string query = "";
            if (Session["AdminLogin"].ToString() == "DTO_RANCHI")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =1 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_HAZARIBAGH")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =2 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_PALAMU")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =3 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_DUMKA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =4 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_JAMSHEDPUR")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =5 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_CHAIBASA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =6 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_GUMLA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =7 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_LOHARDAGA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =8 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_BOKARO")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =9 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_DHANBAD")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =10 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_GIRIDIH")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =11 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_KODERMA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =12 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_CHATRA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =13 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_GARHWA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =14 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_DEOGHAR")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =15 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_PAKUR")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =16 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_GODDA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =17 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_SAHEBGANJ")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =18 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_LATEHAR")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =19 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_SIMDEGA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =20 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_JAMTARA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =21 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_SARAIKELA")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =22 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_KHUNTI")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =23 and status in('Approved','Rejected') order by applied_on desc";
            }
            if (Session["AdminLogin"].ToString() == "DTO_RAMGARH")
            {
                query = "SELECT * FROM covid19_no_use_vehicle where rto_no =24 and status in('Approved','Rejected') order by applied_on desc";
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
        catch(Exception ex)
        {
            Response.Write(ex);
            
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.SelectedRow;
        txtrc_no.Text = row.Cells[1].Text;
        txtChasis.Text = row.Cells[2].Text;
        txtRTAComment.Text = row.Cells[16].Text;
        txtSTAComment.Text = row.Cells[17].Text;
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
            else if (txtDate.Text == null || txtDate.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('please enter total days'); </script>");
            }
            else
            {
                string constr = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                string rta = Session["AdminLogin"].ToString();
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                string stats="";
                con = new SqlConnection(constr);
                con.Open();
                string qry = "";
                if (ddlstatus.SelectedValue == "Verify")
                {
                    stats = "Approved";
                    qry = "Update covid19_no_use_vehicle set dto_verified_by='" + rta + "', dto_verified_on='" + date + "', total_excemption_date='" + txtDate.Text + "', dto_verify_comment='" + txtRemarks.Text + "', status='" + stats + "' where rc_no='" + txtrc_no.Text + "' and chasis_no='" + txtChasis.Text + "'";
                }
                else
                {
                    stats = "Rejected";
                    string rc = "R_" + txtrc_no.Text;
                    qry = "Update covid19_no_use_vehicle set dto_verified_by='" + rta + "', dto_verified_on='" + date + "', total_excemption_date='" + txtDate.Text + "', dto_verify_comment='" + txtRemarks.Text + "', status='" + stats + "',rc_no='" + rc + "' where rc_no='" + txtrc_no.Text + "' and chasis_no='" + txtChasis.Text + "'";
                }
                
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.ExecuteNonQuery();
                con.Close();
                SMSAlter();
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Verified Successfully');window.location='DTO_VERIFY.aspx' </script>");
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
                text = "Dear applicant your Tax Exemption application for Vehicle no. "+ txtrc_no.Text +" is verified by DTO";
            }
            else
            {
                text= "Dear applicant your Tax Exemption application for Vehicle no. "+ txtrc_no.Text +" is rejected by DTO due to incorrect details";
            }
             
            SMSSender ss = new SMSSender();
            ss.Sendsms(mobno, text,"Permit-TimeTable");
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Message + "');window.location ='DTO_VERIFY.aspx'", true);
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
        this.verifygrid();
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
                string dto = "";
                if (Session["AdminLogin"].ToString() == "DTO_RANCHI")
                {
                    dto= "1";
                }
                if (Session["AdminLogin"].ToString() == "DTO_HAZARIBAGH")
                {
                    dto = "2";
                }
                if (Session["AdminLogin"].ToString() == "DTO_PALAMU")
                {
                    dto = "3";
                }
                if (Session["AdminLogin"].ToString() == "DTO_DUMKA")
                {
                    dto = "4";
                }
                if (Session["AdminLogin"].ToString() == "DTO_JAMSHEDPUR")
                {
                    dto = "5";
                }
                if (Session["AdminLogin"].ToString() == "DTO_CHAIBASA")
                {
                    dto = "6";
                }
                if (Session["AdminLogin"].ToString() == "DTO_GUMLA")
                {
                    dto = "7";
                }
                if (Session["AdminLogin"].ToString() == "DTO_LOHARDAGA")
                {
                    dto = "8";
                }
                if (Session["AdminLogin"].ToString() == "DTO_BOKARO")
                {
                    dto = "9";
                }
                if (Session["AdminLogin"].ToString() == "DTO_DHANBAD")
                {
                    dto = "10";
                }
                if (Session["AdminLogin"].ToString() == "DTO_GIRIDIH")
                {
                    dto = "11";
                }
                if (Session["AdminLogin"].ToString() == "DTO_KODERMA")
                {
                    dto = "12";
                }
                if (Session["AdminLogin"].ToString() == "DTO_CHATRA")
                {
                    dto = "13";
                }
                if (Session["AdminLogin"].ToString() == "DTO_GARHWA")
                {
                    dto = "14";
                }
                if (Session["AdminLogin"].ToString() == "DTO_DEOGHAR")
                {
                    dto = "15";
                }
                if (Session["AdminLogin"].ToString() == "DTO_PAKUR")
                {
                    dto = "16";
                }
                if (Session["AdminLogin"].ToString() == "DTO_GODDA")
                {
                    dto = "17";
                }
                if (Session["AdminLogin"].ToString() == "DTO_SAHEBGANJ")
                {
                    dto = "18";
                }
                if (Session["AdminLogin"].ToString() == "DTO_LATEHAR")
                {
                    dto = "19";
                }
                if (Session["AdminLogin"].ToString() == "DTO_SIMDEGA")
                {
                    dto = "20";
                }
                if (Session["AdminLogin"].ToString() == "DTO_JAMTARA")
                {
                    dto = "21";
                }
                if (Session["AdminLogin"].ToString() == "DTO_SARAIKELA")
                {
                    dto = "22";
                }
                if (Session["AdminLogin"].ToString() == "DTO_KHUNTI")
                {
                    dto = "23";
                }
                if (Session["AdminLogin"].ToString() == "DTO_RAMGARH")
                {
                    dto = "24";
                }
                string qry = "select * from covid19_no_use_vehicle where rc_no= '" + txtSearch.Text + "' and status in('Approved','Rejected','Application Pending at DTO Level') and rto_no='" + dto + "'";
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
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('No Details Found');window.location='DTO_VERIFY.aspx' </script>");
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
        Response.Redirect("DTO_VERIFY.aspx");
    }
    protected void btnPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("Change_password.aspx");
    }

}