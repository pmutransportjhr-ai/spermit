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
public partial class APPLICATION_NUMBER : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string regNumber = txtVReg.Text.Trim();
        string permitType = ddlTA.SelectedValue.Trim();
        string service = ddlservice.SelectedValue.Trim();
        string mobileNumber = txtMobileNumber.Text.Trim();
        string rtaService = ddlServiceType.SelectedValue.Trim();

        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {
                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "SP_GET_APPLICATION_NUMBER";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@SERVICE_TYPE",permitType);
	            sCmd.Parameters.AddWithValue("@SERVICE_NAME",service);
	            sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER",regNumber);
	            sCmd.Parameters.AddWithValue("@MOBILE_NUMBER",mobileNumber);
                sCmd.Parameters.AddWithValue("@RTA_SERVICE", rtaService);
                sCmd.Parameters.AddWithValue("@MODE", "GET_APPLICATION_NUMBER");

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToString(dt.Rows[0]["CHECK_FLAG"]) != "0")
                    {
                        lblMessage.Text = string.Format("Application Number: {0}, Applied on : {1} for {2}.",
                            Convert.ToString(dt.Rows[0]["APPLICATION_NUMBER"]),
                            Convert.ToString(dt.Rows[0]["CREATED_ON"]),
                            Convert.ToString(dt.Rows[0]["SERVICE_TYPE"])
                        );
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Application Number Not Found.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMessage.Text = "Application Number Not Found.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='Application_number.aspx'", true);

            }
        }





    }
    protected void ddlTA_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlservice.Items.Clear();
        ddlservice.Items.Insert(0, new ListItem("Select", "0"));
        if (ddlTA.SelectedValue.ToUpper() == "STA")
        {
           
            ddlservice.Items.Insert(1, new ListItem("Apply Permit Application", "STA APPLICATION"));
            //ddlservice.Items.Insert(2, new ListItem("Permit Application", "STA PERMIT ISSUE"));
            ddlservice.Items.Insert(2, new ListItem("Objection Application", "STA PERMIT OBJECTION PERMIT"));
            ddlservice.Items.Insert(3, new ListItem("Duplicate Permit Application", "STA PERMIT DUPLICATE"));
            ddlservice.Items.Insert(4, new ListItem("Renewal Permit Application Apply", "STA PERMIT RENEWAL"));
            //ddlservice.Items.Insert(6, new ListItem("Renewal Permit Application", "STA RENEWAL PERMIT ISSUE"));
            ddlservice.Items.Insert(5, new ListItem("Replacement of Vehicle Application", "STA PERMIT REPLACEMENT OF VEHICLE"));
            ddlservice.Items.Insert(6, new ListItem("Transfer of Permit Application", "STA PERMIT TRANSFER OF PERMIT"));
            ddlservice.Items.Insert(7, new ListItem("Route Extension Application", "STA ROUTE EXTENSION"));
            //ddlservice.Items.Insert(8, new ListItem("Counter Signature Tax Payment", "STA COUNTER SIGNATURE TAX PAY"));
            ddlservice.Items.Insert(8, new ListItem("Apply Counter Signature Application", "STA CS APPLICATION"));
            ddlservice.Items.Insert(9, new ListItem("Objection Counter Signature Application", "STA PERMIT OBJECTION CS"));
            ddlservice.Items.Insert(10, new ListItem("Duplicate Counter Signature Application", "STA COUNTER SIGNATURE DUPLICATE"));
            ddlservice.Items.Insert(11, new ListItem("Renewal Counter Signature Application", "STA COUNTER SIGNATURE RENEWAL"));
            ddlservice.Items.Insert(12, new ListItem("Replacement of Vehicle Counter Signature Application", "STA COUNTER SIGNATURE REPLACEMENT OF VEHICLE"));
            ddlservice.Items.Insert(13, new ListItem("Transfer of Counter Signature Application", "STA COUNTER SIGNATURE TRANSFER"));
        }
        else if(ddlTA.SelectedValue.ToUpper() == "RTA")
        {
            ddlservice.Items.Insert(1, new ListItem("Stage Carriage Permit","RTA APPLICATION"));
            ddlservice.Items.Insert(2, new ListItem("Goods Carriage Permit","GOODS APPLICATION"));
            ddlservice.Items.Insert(3, new ListItem("Contract Carriage Permit","CONTRACT APPLICATION"));
            ddlservice.Items.Insert(4, new ListItem("Auto Rickshaw Permit","AUTO APPLICATION"));
            ddlservice.Items.Insert(5, new ListItem("School Bus Permit", "SCHOOL BUS APPLICATION"));
        }
        else
        {
            ddlservice.Items.Clear();
        }
    }
}