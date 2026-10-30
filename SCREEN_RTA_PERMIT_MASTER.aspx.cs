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
using System.Globalization;
using System.Collections;



public partial class SCREEN_RTA_PERMIT_MASTER : System.Web.UI.Page
{


    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        string EmpId = Convert.ToString(Session["EMPLOYEE_ID"]);
        string EmpName = Convert.ToString(Session["EMPLOYEE_NAME"]);
        //string EmpType = Convert.ToString(Session["EMP_TYPE"]);
        string UserRole = Convert.ToString(Session["USER_ROLE"]);
        string UserType = Convert.ToString(Session["USER_TYPE"]);

        if (EmpId == null || EmpId == string.Empty || EmpId == ""
            || EmpName == null || EmpName == string.Empty || EmpName == ""
            //  || EmpType == null || EmpType == string.Empty || EmpType == ""
            || UserRole == null || UserRole == string.Empty || UserRole == "" || UserType != "RTA" || UserRole != "Super Admin")
        {
            Response.Redirect("Login.aspx");
            return;
        }
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {

            Page.Title = "RTA PERMIT ENTRY FORM";
            PageHeading.InnerText = "RTA PERMIT ENTRY FORM";
            PageHeading1.InnerText = "RTA PERMIT ENTRY FORM";
            Bind_ddlDesState();
            //String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            //using (SqlConnection sCon = new SqlConnection(sConnectionString))
            //{
            //    try
            //    {

            //        sCon.Open();
            //        SqlCommand sCmd = new SqlCommand();
            //        sCmd.Connection = sCon;
            //        sCmd.CommandText = "RTA_PERMIT_APPLY_AED";
            //        sCmd.CommandType = CommandType.StoredProcedure;
            //        sCmd.Parameters.Clear();
            //        sCmd.Parameters.AddWithValue("@MODE", "RTO_PERMIT_OPENING_LIST_BYID_LEGACY");
            //        DataSet ds = new DataSet();
            //        SqlDataAdapter da = new SqlDataAdapter(sCmd);
            //        da.Fill(ds);

            //        DataTable dt = new DataTable();
            //        dt = ds.Tables[0];
            //        StringBuilder sb = new StringBuilder();
            //        if (dt.Rows.Count > 0)
            //        {
            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                sb.Append("<tr>");
            //                sb.Append("<td>" + Convert.ToString(dr["S_NO"]) + "</td>");
            //                sb.Append("<td>" + Convert.ToString(dr["ROUTE_NAME"]) + "</td>");
            //                sb.Append("<td>" + Convert.ToString(dr["SANCTIONED_PERMIT"]) + "</td>");
            //                sb.Append("<td>" + Convert.ToString(dr["ISSUED_PERMIT_PER"]) + "</td>");
            //                sb.Append("<td>" + Convert.ToString(dr["ISSUED_PERMIT_TEMP"]) + "</td>");
            //                sb.Append("<td>" + Convert.ToString(dr["BALANCED_PERMIT"]) + "</td>");


            //                if (Convert.ToString(dr["SELECT_ROUTE_FLG"]) == "Y")
            //                {

            //                    sb.Append("<td><label><input type='radio' name='route' id='RadioButton_" + Convert.ToString(dr["ROUTE_ID"]) + "' onclick=\"javascript:fnApplyPermit('" + CryptorEngine.Encrypt("ROUTE_ID=" + Convert.ToString(dr["ROUTE_ID"]) + "&TYPE=" + hdnTypeID.Value + "&TYPE_OPTION_ID=" + hdnTypeOptionID.Value, true) + "')\"/></label></td>");

            //                }
            //                else
            //                {
            //                    sb.Append("<td></td>");
            //                }
            //                sb.Append("</tr>");
            //            }
            //            FillAPPlyRouteList.InnerHtml = sb.ToString();

            //            RouteDiv.Visible = true;
            //            Message.Visible = false;
            //            vehicleRegistration.Visible = true;
            //        }
            //        else
            //        {
            //            FillAPPlyRouteList.InnerHtml = "";
            //            RouteDiv.Visible = false;
            //            Message.Visible = true;
            //        }



            //    }
            //    catch (Exception ex)
            //    {
            //        String Message = ex.Message;
            //        Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            //        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='login.aspx'", true);

            //    }
            //}

        }
    }
    public void Bind_ddlDesState()
    {

        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
        parameters[1] = new SqlParameter("@MODE", "DD_RTO_LIST");
        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", parameters);

        ddlDesState.DataSource = dr;
        ddlDesState.Items.Clear();
        ddlDesState.Items.Add(new ListItem("--Please select RTO--", "0"));
        ddlDesState.DataTextField = "RTO_NAME";
        ddlDesState.DataValueField = "RTO_CODE";
        ddlDesState.DataBind();
        ddlDesState.SelectedValue = "0";


    }
    protected void ddlDesState_SelectedIndexChanged(object sender, EventArgs e)
    {

        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('show');", true);
        if (ddlDesState.SelectedValue == "0")
        {
            Message.Visible = false;
            RouteDiv.Visible = false;
        }
        else
        {
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "RTA_PERMIT_APPLY_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@RTO_CODE", ddlDesState.SelectedValue);
                    sCmd.Parameters.AddWithValue("@MODE", "RTO_PERMIT_OPENING_LIST_BYID_LEGACY");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    StringBuilder sb = new StringBuilder();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + Convert.ToString(dr["S_NO"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["ROUTE_NAME"]) + "</td>");
                            //sb.Append("<td>" + Convert.ToString(dr["SANCTIONED_PERMIT"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["ISSUED_PERMIT_PER"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["ISSUED_PERMIT_TEMP"]) + "</td>");
                            //sb.Append("<td>" + Convert.ToString(dr["BALANCED_PERMIT"]) + "</td>");


                            if (Convert.ToString(dr["SELECT_ROUTE_FLG"]) == "Y")
                            {

                                sb.Append("<td><label><input type='radio' name='route' id='RadioButton_" + Convert.ToString(dr["ROUTE_ID"]) + "' onclick=\"javascript:fnApplyPermit('" + CryptorEngine.Encrypt("ROUTE_ID=" + Convert.ToString(dr["ROUTE_ID"]) + "&TYPE=" + hdnTypeID.Value + "&TYPE_OPTION_ID=" + hdnTypeOptionID.Value, true) + "')\"/></label></td>");

                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                            sb.Append("</tr>");
                        }
                        FillAPPlyRouteList.InnerHtml = sb.ToString();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "TableDatatablesButtons.init();", true);

                        RouteDiv.Visible = true;
                        Message.Visible = false;
                        vehicleRegistration.Visible = true;
                    }
                    else
                    {
                        FillAPPlyRouteList.InnerHtml = "";
                        RouteDiv.Visible = false;
                        Message.Visible = true;
                    }

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');", true);

                }
                catch (Exception ex)
                {
                    String Message = ex.Message;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

                }
            }
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtVehicleNumber.Text = "";
        VORegNumber.Text = "";
        lblMSg.Text = "";
        Panel1.Visible = false;

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        String regNo1 = txtVehicleNumber.Text.Replace(" ", "").Trim().ToUpper().ToString();
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            try
            {

                sCon.Open();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.CommandText = "SP_CHECK_REGISTRATION_NUMBER";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();                
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", regNo1);
                sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXISTS");
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(sCmd);
                da.Fill(ds);
                if (Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) == "FAIL")
                {

                    lblMSg.Text = ds.Tables[0].Rows[0]["MSG"].ToString();
                    lblMSg.Visible = true;
                    Panel1.Visible = false;
                }

                else if (Convert.ToString(ds.Tables[0].Rows[0]["STATUS"]) == "SUCCESS")
                {

                    Int32 x = Convert.ToInt32(DateTime.Now.Year);
                    Int32 y = x - 18;
                    Int16 month = Convert.ToInt16(DateTime.Now.Month);

                    String startMonth = Convert.ToString(month) + "/" + Convert.ToString(y);
                    String endMonth = Convert.ToString(month - 1) + "/" + Convert.ToString(x);

                    VahanService myclass = new VahanService();
                    myclass = VahanResponse.GetVahanResponse(regNo1);

                    if (myclass != null)
                    {
                        if (myclass.status == "OK" && myclass.rc_catg == "TRANSPORT")
                        {
                            VORegNumber.Text = regNo1;

                            if (myclass.rc_owner_name == "" || myclass.rc_owner_name == "NA" || myclass.rc_owner_name == "N/A" || myclass.rc_owner_name == "Not Available" || myclass.rc_owner_name == null)
                            {
                                VOName.ReadOnly = false;
                            }
                            else
                            {
                                VOName.Text = myclass.rc_owner_name;
                                VOName.ReadOnly = true;
                            }

                            if (myclass.rc_f_name == "" || myclass.rc_f_name == "NA" || myclass.rc_f_name == "N/A" || myclass.rc_f_name == "Not Available" || myclass.rc_f_name == null)
                            {
                                VOCareOf.ReadOnly = false;
                            }
                            else
                            {

                                VOCareOf.ReadOnly = true;
                                VOCareOf.Text = myclass.rc_f_name;
                            }
                            if (myclass.rc_present_address == "" || myclass.rc_present_address == "NA" || myclass.rc_present_address == "N/A" || myclass.rc_present_address == "Not Available" || myclass.rc_present_address == null)
                            {
                                VOPresentAddress.ReadOnly = false;
                            }
                            else
                            {
                                VOPresentAddress.ReadOnly = true;
                                VOPresentAddress.Text = myclass.rc_present_address;
                            }

                            if (myclass.rc_permanent_address == "" || myclass.rc_permanent_address == "NA" || myclass.rc_permanent_address == "N/A" || myclass.rc_permanent_address == "Not Available" || myclass.rc_permanent_address == null)
                            {
                                VOPermanentAddress.ReadOnly = false;

                            }
                            else
                            {
                                VOPermanentAddress.ReadOnly = true;
                                VOPermanentAddress.Text = myclass.rc_permanent_address;
                            }

                            if (myclass.rc_permanent_address == "" || myclass.rc_permanent_address == "NA" || myclass.rc_permanent_address == "N/A" || myclass.rc_permanent_address == "Not Available" || myclass.rc_permanent_address == null)
                            {
                                VOPermanentAddress.ReadOnly = false;
                            }
                            else
                            {
                                VOPermanentAddress.ReadOnly = true;
                                VOPermanentAddress.Text = myclass.rc_permanent_address;
                            }

                            if (myclass.rc_chasi_no == "" || myclass.rc_chasi_no == "NA" || myclass.rc_chasi_no == "N/A" || myclass.rc_chasi_no == "Not Available" || myclass.rc_chasi_no == null)
                            {
                                VChassisNumber.ReadOnly = false;
                            }
                            else
                            {
                                VChassisNumber.ReadOnly = true;
                                VChassisNumber.Text = myclass.rc_chasi_no;
                            }
                            if (myclass.rc_eng_no == "" || myclass.rc_eng_no == "NA" || myclass.rc_eng_no == "N/A" || myclass.rc_eng_no == "Not Available" || myclass.rc_eng_no == null)
                            {
                                VEngineNumber.ReadOnly = false;
                            }
                            else
                            {
                                VEngineNumber.ReadOnly = true;
                                VEngineNumber.Text = myclass.rc_eng_no;
                            }

                            if (myclass.rc_maker_desc == "" || myclass.rc_maker_desc == "NA" || myclass.rc_maker_desc == "N/A" || myclass.rc_maker_desc == "Not Available" || myclass.rc_maker_desc == null)
                            {
                                VMakerDesc.ReadOnly = false;
                            }
                            else
                            {
                                VMakerDesc.ReadOnly = true;
                                VMakerDesc.Text = myclass.rc_maker_desc;
                            }

                            if (myclass.rc_maker_model == "" || myclass.rc_maker_model == "NA" || myclass.rc_maker_model == "N/A" || myclass.rc_maker_model == "Not Available" || myclass.rc_maker_model == null)
                            {
                                VMakerDesc.ReadOnly = false;
                            }
                            else
                            {
                                VMakerModel.ReadOnly = true;
                                VMakerModel.Text = myclass.rc_maker_model;
                            }

                            if (myclass.rc_fuel_desc == "" || myclass.rc_fuel_desc == "NA" || myclass.rc_fuel_desc == "N/A" || myclass.rc_fuel_desc == "Not Available" || myclass.rc_fuel_desc == null)
                            {
                                VFuelDesc.ReadOnly = false;
                            }
                            else
                            {
                                VFuelDesc.ReadOnly = true;
                                VFuelDesc.Text = myclass.rc_fuel_desc;
                            }

                            if (myclass.rc_color == "" || myclass.rc_color == "NA" || myclass.rc_color == "N/A" || myclass.rc_color == "Not Available" || myclass.rc_color == null)
                            {
                                VColor.ReadOnly = false;
                            }
                            else
                            {
                                VColor.ReadOnly = true;
                                VColor.Text = myclass.rc_color;
                            }


                            if (myclass.rc_seat_capacity == "" || myclass.rc_seat_capacity == "NA" || myclass.rc_seat_capacity == "N/A" || myclass.rc_seat_capacity == "Not Available" || myclass.rc_seat_capacity == null)
                            {
                                VSeatCapacity.ReadOnly = false;
                            }
                            else
                            {
                                VSeatCapacity.ReadOnly = true;
                                VSeatCapacity.Text = myclass.rc_seat_capacity;
                            }

                            if (myclass.rc_tax_upto == "" || myclass.rc_tax_upto == "NA" || myclass.rc_tax_upto == "N/A" || myclass.rc_tax_upto == "Not Available" || myclass.rc_tax_upto == null)
                            {
                                btn2.Disabled = false;

                            }
                            else
                            {
                                btn2.Disabled = true;
                                VTax.Value = myclass.rc_tax_upto;
                            }
                            if (myclass.rc_fit_upto == "" || myclass.rc_fit_upto == "NA" || myclass.rc_fit_upto == "N/A" || myclass.rc_fit_upto == "Not Available" || myclass.rc_fit_upto == null)
                            {
                                btn1.Disabled = false;
                            }
                            else
                            {
                                btn1.Disabled = true;
                                VFitness.Value = myclass.rc_fit_upto;
                            }

                            if (myclass.rc_vh_class_desc == "" || myclass.rc_vh_class_desc == "NA" || myclass.rc_vh_class_desc == "N/A" || myclass.rc_vh_class_desc == "Not Available" || myclass.rc_vh_class_desc == null)
                            {
                                VType.ReadOnly = false;

                            }
                            else
                            {
                                VType.ReadOnly = true;
                                VType.Text = myclass.rc_vh_class_desc;
                            }

                            if (myclass.rc_ulw == "" || myclass.rc_ulw == "NA" || myclass.rc_ulw == "N/A" || myclass.rc_ulw == "Not Available" || myclass.rc_ulw == null)
                            {
                                VULW.ReadOnly = false;

                            }
                            else
                            {
                                VULW.ReadOnly = true;
                                VULW.Text = myclass.rc_ulw;
                            }
                            if (myclass.rc_rlw == "" || myclass.rc_rlw == "NA" || myclass.rc_rlw == "N/A" || myclass.rc_rlw == "Not Available" || myclass.rc_rlw == null)
                            {
                                VRLW.ReadOnly = false;

                            }
                            else
                            {
                                VRLW.ReadOnly = true;
                                VRLW.Text = myclass.rc_rlw;
                            }
                            if (myclass.rc_manufacturing_on == "" || myclass.rc_manufacturing_on == "NA" || myclass.rc_manufacturing_on == "N/A" || myclass.rc_manufacturing_on == "Not Available" || myclass.rc_manufacturing_on == null)
                            {
                                VManufacturingON.Disabled = false;
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('input[id*=VManufacturingON]').Monthpicker({minYear:" + y + ",maxYear:" + x + ", minValue: '" + startMonth + "',maxValue: '" + endMonth + "'});", true);

                            }
                            else
                            {
                                MFSERVER.Value = "YES";
                                VManufacturingON.Disabled = true;
                                VManufacturingON.Value = myclass.rc_manufacturing_on;
                            }




                            HDRegisteredat.Value = myclass.rc_registered_at;

                        }
                        else if (myclass.status == "OK" && myclass.rc_catg == "NON TRANSPORT")
                        {

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Your Vehicle is under non transport section so you can not apply for permit.');location.reload();", true);
                            return;
                        }
                        else
                        {
                            VORegNumber.Text = regNo1;
                            VOName.ReadOnly = false;
                            VOCareOf.ReadOnly = false;
                            VOPresentAddress.ReadOnly = false;
                            VOPermanentAddress.ReadOnly = false;
                            VChassisNumber.ReadOnly = false;
                            VEngineNumber.ReadOnly = false;
                            VMakerDesc.ReadOnly = false;
                            VMakerModel.ReadOnly = false;
                            VFuelDesc.ReadOnly = false;
                            VColor.ReadOnly = false;
                            VSeatCapacity.ReadOnly = false;
                            VTax.Disabled = false;
                            VFitness.Disabled = false;
                            VManufacturingON.Disabled = false;
                            btn1.Disabled = false;
                            btn2.Disabled = false;
                            // btn3.Disabled = false;

                            VType.ReadOnly = false;
                            VULW.ReadOnly = false;
                            VRLW.ReadOnly = false;
                            HDRegisteredat.Value = "";
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('input[id*=VManufacturingON]').Monthpicker({minYear:" + y + ",maxYear:" + x + ", minValue: '" + startMonth + "',maxValue: '" + endMonth + "'});", true);
                        }
                    }
                    else
                    {
                        VORegNumber.Text = regNo1;
                        VOName.ReadOnly = false;
                        VOCareOf.ReadOnly = false;
                        VOPresentAddress.ReadOnly = false;
                        VOPermanentAddress.ReadOnly = false;
                        VChassisNumber.ReadOnly = false;
                        VEngineNumber.ReadOnly = false;
                        VMakerDesc.ReadOnly = false;
                        VMakerModel.ReadOnly = false;
                        VFuelDesc.ReadOnly = false;
                        VColor.ReadOnly = false;
                        VSeatCapacity.ReadOnly = false;
                        VTax.Disabled = false;
                        VFitness.Disabled = false;
                        VManufacturingON.Disabled = false;
                        btn1.Disabled = false;
                        btn2.Disabled = false;
                        //btn3.Disabled = false;

                        VType.ReadOnly = false;
                        VULW.ReadOnly = false;
                        VRLW.ReadOnly = false;
                        HDRegisteredat.Value = "";
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('input[id*=VManufacturingON]').Monthpicker({minYear:" + y + ",maxYear:" + x + ", minValue: '" + startMonth + "',maxValue: '" + endMonth + "'});", true);

                    }


                    string selctedRouteID =  CryptorEngine.Decrypt(routeID.Value, true);
                    selctedRouteID = selctedRouteID.Split('&')[0].Split('=')[1];

                    sCmd.CommandText = "RTA_PERMIT_APPLY_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@ROUTE_ID", selctedRouteID);

                    string permitType = ddlPermitType.SelectedValue;
                    string permitTypeId = null; string permittypeserviceId = null;

                    if(permitType == "1")
                    {
                        permitTypeId = "1";
                            permittypeserviceId = "1";
                    }
                    else
                    {
                         permitTypeId = "2";
                         permittypeserviceId = "15";
                    }


                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", permitTypeId);
                    sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", permittypeserviceId);



                    sCmd.Parameters.AddWithValue("@MODE", "ROUTEANDFEELIST");
                    DataSet ds1 = new DataSet();
                    SqlDataAdapter da1 = new SqlDataAdapter(sCmd);
                    da1.Fill(ds1);


                    DataTable dt1 = ds1.Tables[0];
                    DataTable dt2 = ds1.Tables[1];
                    StringBuilder sb = new StringBuilder();
                    if (dt1.Rows.Count > 0)
                    {
                        //sb.Append("<div class='input-group'>");
                        //sb.Append("<div class='icheck-list'>");


                        //sb.Append("<div class='col-md-6 col-sm-6'><input type='radio' value='" + Convert.ToString(dr["ROUTE_ID"]) + "' name='routeHdr' id='RadioButton_" + Convert.ToString(dr["ROUTE_ID"]) + "' onclick=\"javascript:fnShowRoute('" + CryptorEngine.Encrypt("ROUTE_ID=" + Convert.ToString(dr["ROUTE_ID"]), true) + "')\"/> " + Convert.ToString(dr["ROUTE_NAME"]) + " (" + Convert.ToString(dr["TOTAL_DISTANCE"]) + " KM)</div>");
                        sb.Append("<div class='col-md-6 col-sm-6'><b>Route</b></div>");
                        sb.Append("<div class='col-md-6 col-sm-6'><b>No. of Round Trips</b></div><br/>");
                        sb.Append("<div class='col-md-6 col-sm-6'>" + Convert.ToString(dt1.Rows[0]["ROUTE_NAME"]) + " (" + Convert.ToString(dt1.Rows[0]["TOTAL_DISTANCE"]) + " KM)</div>");
                        sb.Append("<div class='col-md-6 col-sm-6'>");
                        int k = Convert.ToInt32(dt1.Rows[0]["NO_OF_TRIPS"].ToString());
                        sb.Append("<select id='ddlRouteAction' name='ddlPermitAction' onchange=\"javascript:fnShowRoute('" + CryptorEngine.Encrypt("ROUTE_ID=" + Convert.ToString(dt1.Rows[0]["ROUTE_ID"]), true) + "')\" class='form-control select2' >");


                        sb.Append("<option value='0'>--select action--</option>");




                        for (int i = 1; i <= k; i++)
                        {
                            sb.Append("<option value='" + i + "'>" + i + "</option>");
                        }
                        sb.Append("</select>");
                        sb.Append("</div>");
                        //sb.Append("</div>");
                        RouteHDRInfo.InnerHtml = sb.ToString();

                    }
                    if (dt2.Rows.Count > 0)
                    {
                        //lblPermitFees.Text = dt2.Rows[0]["FEE"].ToString();

                    }
                    else
                    {
                       // lblPermitFees.Text = "";
                    }
                    
                    VORegNumber.Text = regNo1;
                    Panel1.Visible = true;
                    vehicleRegistration.Style["display"] = "block";


                } 
               

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');ComponentsDateTimePickers.init();", true);
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-DASHBOARD.aspx'", true);

            }
        }

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {


        txtVehicleNumber.Text = "";
        VORegNumber.Text = "";
        lblMSg.Text = "";
        Panel1.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RTA-DASHBOARD.aspx");
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string permitNo = txtpermitNo.Text;
        string permitValidFrom = txtpermitFrom.Value;
        string permitValidUpto = txtpermitTo.Value;

        DateTime? permitValidFromDate;
        DateTime? permitValidUptoDate;

        if (!string.IsNullOrEmpty(permitValidFrom))
        {
            permitValidFromDate = DateTime.ParseExact(permitValidFrom, "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {

            permitValidFromDate = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
        }

        if (!string.IsNullOrEmpty(permitValidUpto))
        {

            permitValidUptoDate = DateTime.ParseExact(permitValidUpto, "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        else
        {
            permitValidUptoDate = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
        }
        String input = "";
        input = txtroutjson.Value;
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;

        PERMIT_APPLICATION_LDR rDTO = new JavaScriptSerializer().Deserialize<PERMIT_APPLICATION_LDR>(input);
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
                
                string selctedRouteID = CryptorEngine.Decrypt(routeID.Value, true);
                selctedRouteID = selctedRouteID.Split('&')[0].Split('=')[1];

                Int32 ROUTE_ID = Convert.ToInt32(selctedRouteID);

                string permitType = ddlPermitType.SelectedValue;
                string permitTypeId = null; string permittypeserviceId = null;

                if (permitType == "1")
                {
                    permitTypeId = "1";
                    permittypeserviceId = "1";
                }
                else
                {
                    permitTypeId = "2";
                    permittypeserviceId = "15";
                }
                sCmd.CommandText = "SCREEN_RTA_PERMIT_APPLICATION_MASTER_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", 0);
                sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE", permitTypeId);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", permittypeserviceId);
                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", permitNo);
                sCmd.Parameters.AddWithValue("@VALID_FROM", permitValidFromDate);
                sCmd.Parameters.AddWithValue("@VALID_UP_TO", permitValidUptoDate);
              
               
                
                sCmd.Parameters.AddWithValue("@ROUTE_ID", ROUTE_ID);
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
                string selectedGender, ExArmy, IdentityProof, Minority, Handicapped, HML, AC, Deluxe, Sleeper;

                if (txtMale.Checked)
                {
                    selectedGender = txtMale.Value;
                }
                else
                {
                    selectedGender = txtFemale.Value;
                }

                if (txtExArmyY.Checked)
                {
                    ExArmy = txtExArmyY.Value;
                }
                else
                {
                    ExArmy = txtExArmyN.Value;
                }

                if (txtMinorityY.Checked)
                {
                    Minority = txtMinorityY.Value;
                }
                else
                {
                    Minority = txtMinorityN.Value;
                }
                if (txtHandicappedY.Checked)
                {
                    Handicapped = txtHandicappedY.Value;
                }
                else
                {
                    Handicapped = txtHandicappedN.Value;
                }
                if (txtHMLY.Checked)
                {
                    HML = txtHMLY.Value;
                }
                else
                {
                    HML = txtHMLN.Value;
                }
                if (txtACY.Checked)
                {
                    AC = txtACY.Value;
                }
                else
                {
                    AC = txtACN.Value;
                }
                if (txtDeluxeY.Checked)
                {
                    Deluxe = txtDeluxeY.Value;
                }
                else
                {
                    Deluxe = txtDeluxeN.Value;
                }
                if (txtSleeperY.Checked)
                {
                    Sleeper = txtSleeperY.Value;
                }
                else
                {
                    Sleeper = txtSleeperN.Value;
                }
                if (txtIdentityProofV.Checked)
                {
                    IdentityProof = txtIdentityProofV.Value;
                }
                else if (txtIdentityProofP.Checked)
                {
                    IdentityProof = txtIdentityProofP.Value;
                }
                else
                {
                    IdentityProof = txtIdentityProofA.Value;
                }
                DateTime? Tax; DateTime? Fitness; DateTime? ManufacturingON;


                if (!String.IsNullOrEmpty(VTax.Value))
                {
                    Tax = DateTime.ParseExact(VTax.Value, "d/M/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    Tax = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }
                if (!String.IsNullOrEmpty(VFitness.Value))
                {
                    Fitness = DateTime.ParseExact(VFitness.Value, "d/M/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    Fitness = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }
                if (!String.IsNullOrEmpty(VManufacturingON.Value))
                {
                    VManufacturingON.Value = "01/" + VManufacturingON.Value;

                    ManufacturingON = DateTime.ParseExact(VManufacturingON.Value, "d/M/yyyy", CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1);

                }
                else
                {
                    ManufacturingON = DateTime.ParseExact("01/01/1900", "d/M/yyyy", CultureInfo.InvariantCulture);
                }

                sCmd.Parameters.AddWithValue("@VEHICLE_CLASS_TYPE", VType.Text);
                sCmd.Parameters.AddWithValue("@CHASIS_NUMBER", VChassisNumber.Text);
                sCmd.Parameters.AddWithValue("@ENGINE_NUMBER", VEngineNumber.Text);
                sCmd.Parameters.AddWithValue("@MAKER_DESC", VMakerDesc.Text);
                sCmd.Parameters.AddWithValue("@MAKER_MODEL", VMakerModel.Text);
                sCmd.Parameters.AddWithValue("@FUEL_DESC", VFuelDesc.Text);
                sCmd.Parameters.AddWithValue("@VEHICLE_COLOR", VColor.Text);
                sCmd.Parameters.AddWithValue("@FITNESS_VALID_UPTO", Fitness);
                sCmd.Parameters.AddWithValue("@TAX_PAID_UPTO", Tax);
                sCmd.Parameters.AddWithValue("@ULW", VULW.Text);
                sCmd.Parameters.AddWithValue("@RLW", VRLW.Text);
                sCmd.Parameters.AddWithValue("@SEATING_CAPACITY", VSeatCapacity.Text);
                sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", ManufacturingON);
                sCmd.Parameters.AddWithValue("@VEHICLE_REGISTERED_AT", HDRegisteredat.Value);

                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
                sCmd.Parameters.AddWithValue("@GENDER", selectedGender);
                sCmd.Parameters.AddWithValue("@APPLICANT_QUALIFICATION", ddlQualification.Value);
                sCmd.Parameters.AddWithValue("@APPLICANT_CATEGORY", ddlCategory.Value);
                sCmd.Parameters.AddWithValue("@EX_ARMY", ExArmy);
                sCmd.Parameters.AddWithValue("@MINORITY", Minority);
                sCmd.Parameters.AddWithValue("@HANDICAPPED", Handicapped);
                sCmd.Parameters.AddWithValue("@HEAVY_MOTOR_LICENCE", HML);
                sCmd.Parameters.AddWithValue("@IDENTITY_PROOF", IdentityProof);
                sCmd.Parameters.AddWithValue("@AIR_CONDITIONED", AC);
                sCmd.Parameters.AddWithValue("@DELUXE", Deluxe);
                sCmd.Parameters.AddWithValue("@SLEEPER", Sleeper);


                sCmd.Parameters.AddWithValue("@NO_OF_TRIPS", NoRoundTrip.Value);
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", Fitness);
                sCmd.Parameters.AddWithValue("@IDENTITY_PROOF_NO", txtIdentityNo.Text.ToString());


                sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", DBNull.Value);
                sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXIST");

                var Cnt = sCmd.ExecuteScalar();
                if (Convert.ToInt32(Cnt) == 0)
                {



                    sCmd.Parameters["@MODE"].Value = "HDR_ADD";
                    var obj = sCmd.ExecuteScalar();

                    Int32 PermitId;
                    PermitId = Convert.ToInt32(obj);
                    foreach (PERMIT_APPLICATION_LDR dr in rDTO.PERMIT_APPLICATION_LDRs)
                    {
                        sCmd.CommandText = "SCREEN_RTA_PERMIT_APPLICATION_MASTER_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();

                        sCmd.Parameters.AddWithValue("@APP_ID", PermitId);
                        sCmd.Parameters.AddWithValue("@SEQUENCE", dr.ROUTE_DETAIL_ID);
                        sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                        sCmd.Parameters.AddWithValue("@ARRIVAL_TIMING", dr.ARRIVAL_TIMING.ToString());
                        sCmd.Parameters.AddWithValue("@DEPARTURE_TIMING", dr.DEPARTURE_TIMING.ToString());
                        sCmd.Parameters.AddWithValue("@MODE", dr.MODE);
                        sCmd.Parameters.AddWithValue("@ID", dr.ID);
                        sCmd.Parameters.AddWithValue("@OP_MODE", "LDR_ADD");
                      
                    
                        sCmd.ExecuteNonQuery();

                    }

                    sCmd.CommandText = "SCREEN_RTA_PERMIT_APPLICATION_MASTER_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@APP_ID", PermitId);
                    sCmd.Parameters.AddWithValue("@MODE", "MSTR_UPD");
                    sCmd.ExecuteNonQuery();
                    objTrans.Commit();
                    string str = HttpUtility.UrlEncode(CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(PermitId), true));
                    // Response.Redirect("STA_INTER_STATE_PERMIT_RECEIPT.aspx?q="+str);
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Successfully inserted');window.location ='RTA-DASHBOARD.aspx'", true);

                }
                else
                {
                    objTrans.Rollback();
                    String Message = "";
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('Registration or Permit Number is already exist');window.location ='RTA-DASHBOARD.aspx'", true);

                }

            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-DASHBOARD.aspx'", true);
                // Response.Redirect("RTA-DASHBOARD.aspx");
            }
        }
    }

   

}