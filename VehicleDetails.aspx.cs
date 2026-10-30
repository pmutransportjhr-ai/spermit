using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.IO;
using System.Configuration;
using System.Text;

public partial class VehicleDetails : System.Web.UI.Page
{
    
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          

        }
    }


    protected void btnGetDetail_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), " $('#loadingmessage').modal('show');", true);
        if (Page.IsValid)
        {
            StringBuilder sb = new StringBuilder();
            VahanService myclass = new VahanService();
            myclass = VahanResponse.GetVahanResponse(txtRegistrationNumber.Text.Trim());

            sb.Append("<tr><td>Registration Number</td><td>" + myclass.rc_regn_no + "</td></tr>");
            sb.Append("<tr><td>Owner Name</td><td>" + myclass.rc_owner_name + "</td></tr>");
            sb.Append("<tr><td>Father's Name</td><td>" + myclass.rc_f_name + "</td></tr>");
            sb.Append("<tr><td>Present Address</td><td>" + myclass.rc_present_address + "</td></tr>");
            sb.Append("<tr><td>Permanent Address</td><td>" + myclass.rc_permanent_address + "</td></tr>");
            sb.Append("<tr><td>Category</td><td>" + myclass.rc_catg + "</td></tr>");
            sb.Append("<tr><td>Status</td><td>" + myclass.status + "</td></tr>");
            sb.Append("<tr><td>Engine Number</td><td>" + myclass.rc_eng_no + "</td></tr>");
            sb.Append("<tr><td>Color</td><td>" + myclass.rc_color + "</td></tr>");
            sb.Append("<tr><td>RLW</td><td>" + myclass.rc_rlw + "</td></tr>");
            sb.Append("<tr><td>Status As On</td><td>" + myclass.rc_status_as_on + "</td></tr>");
            sb.Append("<tr><td>Registered At</td><td>" + myclass.rc_registered_at + "</td></tr>");
            sb.Append("<tr><td>ULW</td><td>" + myclass.rc_ulw + "</td></tr>");
            sb.Append("<tr><td>NP Upto</td><td>" + myclass.rc_np_upto + "</td></tr>");
            sb.Append("<tr><td>Insurance Upto</td><td>" + myclass.rc_insurance_upto + "</td></tr>");
            sb.Append("<tr><td>Vehicle Class</td><td>" + myclass.rc_vh_class_desc + "</td></tr>");
            sb.Append("<tr><td>Insurance Comp</td><td>" + myclass.rc_insurance_comp + "</td></tr>");
            sb.Append("<tr><td>Tax paid upto</td><td>" + myclass.rc_tax_upto + "</td></tr>");
            sb.Append("<tr><td>Insurance Policy No</td><td>" + myclass.rc_insurance_policy_no + "</td></tr>");
            sb.Append("<tr><td>Fuel Description</td><td>" + myclass.rc_fuel_desc + "</td></tr>");
            sb.Append("<tr><td>Financer</td><td>" + myclass.rc_financer + "</td></tr>");
            sb.Append("<tr><td>Chassis Number</td><td>" + myclass.rc_chasi_no + "</td></tr>");
            sb.Append("<tr><td>Maker Model</td><td>" + myclass.rc_maker_model + "</td></tr>");
            sb.Append("<tr><td>Maker Description</td><td>" + myclass.rc_maker_desc + "</td></tr>");
            sb.Append("<tr><td>Manufacturing On</td><td>" + myclass.rc_manufacturing_on + "</td></tr>");
            sb.Append("<tr><td>Norms Description</td><td>" + myclass.rc_norms_desc + "</td></tr>");
            sb.Append("<tr><td>Seating Capacity</td><td>" + myclass.rc_seat_capacity + "</td></tr>");
            sb.Append("<tr><td>Fitness upto</td><td>" + myclass.rc_fit_upto + "</td></tr>");
            FillCityList.InnerHtml = sb.ToString();
            divTable.Visible = true;
        }
        else
        {
            divTable.Visible = false;
        }
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), " $('#loadingmessage').modal('hide');", true);
    }
}