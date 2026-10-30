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
using System.Net;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;
using System.Collections;



public partial class SCREEN_RTA_GOODS_PERMIT_APPLICATION_EDIT : System.Web.UI.Page
{
  
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        txtMobileNo.MaxLength = 10;
   //     txtVehicleNumber.MaxLength = 12;

        if (!IsPostBack)
        {


            Page.Title = "RTA GOODS PERMIT APPLICATION EDIT ";
            PageHeading.InnerText = "RTA GOODS PERMIT APPLICATION EDIT";
            PageHeading1.InnerText = "RTA GOODS PERMIT APPLICATION EDIT";

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
                  String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                  using (SqlConnection sCon = new SqlConnection(sConnectionString))
                  {
                      hdnID.Value = IDx.Value;
                      string permittypeid = string.Empty;
                      string permittypeoptionserviceid = string.Empty;
                      sCon.Open();
                      SqlCommand sCmd = new SqlCommand();
                      sCmd.Connection = sCon;
                      sCmd.CommandText = "RTA_SP_GET_GOODS_MASTER_DATA";
                      sCmd.CommandType = CommandType.StoredProcedure;
                      sCmd.Parameters.Clear();
                      sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                      sCmd.Parameters.AddWithValue("@MODE", "MASTER_VIEW");
                      DataSet ds = new DataSet();
                      SqlDataAdapter da = new SqlDataAdapter(sCmd);
                      da.Fill(ds);


                      DataTable dt1 = ds.Tables[0];
                      DataTable dt2 = ds.Tables[1];
                      DataTable dt3 = ds.Tables[2];

                      StringBuilder sb = new StringBuilder();
                      if (dt1.Rows.Count > 0)
                      {
                         

                          if (Convert.ToString(dt1.Rows[0]["PERMIT_TYPE"]) == "Temporary")
                          {
                              ddlPermitType.SelectedValue = "2";
                              permittypeid = "4";
                              permittypeoptionserviceid = "41";
                          }
                          else
                          {
                              ddlPermitType.SelectedValue = "1";
                              permittypeid = "3";
                              permittypeoptionserviceid = "29";
                          }

                          txtpermitNo.Text = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);

                          txtpermitFrom.Value = Convert.ToString(dt1.Rows[0]["VALID_FROM"]);
                          txtpermitTo.Value = Convert.ToString(dt1.Rows[0]["VALID_UP_TO"]);

                          VORegNumber.Text = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                          VOName.Text = Convert.ToString(dt1.Rows[0]["OWNER_NAME"]);
                          VOCareOf.Text = Convert.ToString(dt1.Rows[0]["CARE_OF"]);
                          VOPresentAddress.Text = Convert.ToString(dt1.Rows[0]["PRESENT_ADDRESS"]);
                          VOPermanentAddress.Text = Convert.ToString(dt1.Rows[0]["PERMANENT_ADDRESS"]);

                          VORegNumber.Text = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                          VType.Text = Convert.ToString(dt1.Rows[0]["VEHICLE_CLASS_TYPE"]);
                          VChassisNumber.Text = Convert.ToString(dt1.Rows[0]["CHASIS_NUMBER"]);
                          VEngineNumber.Text = Convert.ToString(dt1.Rows[0]["ENGINE_NUMBER"]);

                          VMakerDesc.Text = Convert.ToString(dt1.Rows[0]["MAKER_DESC"]);
                          VMakerModel.Text = Convert.ToString(dt1.Rows[0]["MAKER_MODEL"]);
                          VFuelDesc.Text = Convert.ToString(dt1.Rows[0]["FUEL_DESC"]);
                          VColor.Text = Convert.ToString(dt1.Rows[0]["VEHICLE_COLOR"]);
                          VFitness.Value = Convert.ToString(dt1.Rows[0]["FITNESS_VALID_UPTO"]);
                          VTax.Value = Convert.ToString(dt1.Rows[0]["TAX_PAID_UPTO"]);
                          VULW.Text = Convert.ToString(dt1.Rows[0]["ULW"]);
                          VRLW.Text = Convert.ToString(dt1.Rows[0]["RLW"]);

                          VManufacturingON.Value = Convert.ToString(dt1.Rows[0]["MANUFACTURING_ON"]);

                          txtVCapacity.Text = Convert.ToString(dt1.Rows[0]["VEHICLE_CAPACITY"]);
                          txtRates.Text = Convert.ToString(dt1.Rows[0]["RATES_OF_CHARGES"]);
                          VNatureofGoods.Text = Convert.ToString(dt1.Rows[0]["NATURE_OF_GOODS"]);


                          txtMobileNo.Text = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);
                          txtEmailId.Text = Convert.ToString(dt1.Rows[0]["EMAIL_ID"]);

                          if (Convert.ToString(dt1.Rows[0]["GENDER"]) == "Male")
                          {
                              txtMale.Checked = true;
                              txtFemale.Checked = false;
                          }
                          else
                          {

                              txtMale.Checked = false;
                              txtFemale.Checked = true;
                          }



                          txtICValidUpto.Value = Convert.ToString(dt1.Rows[0]["IC_VALID_UPTO"]);
                          txtPCValidUpto.Value = Convert.ToString(dt1.Rows[0]["PC_VALID_UPTO"]);
                          txtFCValidUpto.Value = Convert.ToString(dt1.Rows[0]["FC_VALID_UPTO"]);

                          //RTO_LIST
                       
                          if(Convert.ToString(dt1.Rows[0]["PERMIT_SELF"]) == "YES")
                          {
                              WholeStateCheckbox.Checked = false;
                              SelfCheckBox.Checked = true;
                          }
                          if (Convert.ToString(dt1.Rows[0]["PERMIT_WHOLE_JHARKHAND"]) == "YES")
                          {
                              WholeStateCheckbox.Checked = true;
                              SelfCheckBox.Checked = false;
                          }



                          sCmd.CommandText = "SCREEN_RTA_GOODS_PERMIT_APPLICATION_MASTER_AED";
                          sCmd.CommandType = CommandType.StoredProcedure;
                          sCmd.Parameters.Clear();

                          sCmd.Parameters.AddWithValue("@APP_ID", 0);
                          sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text.Trim());
                          sCmd.Parameters.AddWithValue("@MODE", "RTO_LIST");
                          DataSet ds1 = new DataSet();
                          SqlDataAdapter da1 = new SqlDataAdapter(sCmd);
                          da1.Fill(ds1);


                          DataTable dt11 = ds1.Tables[0]; //RTO CODE & RTO NAME
                          DataTable dt21 = ds1.Tables[1]; // To Apply For (RTO) 
                          DataTable dt31 = ds1.Tables[2]; // another state
                          StringBuilder sb1 = new StringBuilder();
                          if (dt11.Rows.Count > 0)
                          {
                              RTOCode.Value = dt11.Rows[0]["RTO_CODE"].ToString();


                          }
                          if (dt21.Rows.Count > 0)
                          {
                              sb1.Append("<div class='input-group'>");
                              sb1.Append("<div class='icheck-list'>");
                              foreach (DataRow dr in dt21.Rows)
                              {
                                  if (Convert.ToString(dt1.Rows[0]["PERMIT_ANOTHER_RTO_CODE"]) != "NO")
                                  {
                                      if (Convert.ToString(dr["RTO_CODE"]) == Convert.ToString(dt1.Rows[0]["PERMIT_ANOTHER_RTO_CODE"]))
                                      {
                                          sb1.Append("<div class='col-md-12 col-sm-12'>");
                                          sb1.Append("<input type='radio' checked ='checked' value='" + Convert.ToString(dr["RTO_NAME"]) + "&" + Convert.ToString(dr["RTO_CODE"]) + "' class='rtoHdr' name ='otherrto' id='RadioButton_" + Convert.ToString(dr["RTO_CODE"]) + "'");
                                          sb1.Append("onclick=\"javascript:otherRTOCode1('" + CryptorEngine.Encrypt("RTO_CODE=" + Convert.ToString(dr["RTO_CODE"]), true) + "')\" >    " + Convert.ToString(dr["RTO_NAME"]).ToUpper() + "</div>");
                                      }
                                      else
                                      {
                                          sb1.Append("<div class='col-md-12 col-sm-12'>");
                                          sb1.Append("<input type='radio' value='" + Convert.ToString(dr["RTO_NAME"]) + "&" + Convert.ToString(dr["RTO_CODE"]) + "' class='rtoHdr' name ='otherrto' id='RadioButton_" + Convert.ToString(dr["RTO_CODE"]) + "'");
                                          sb1.Append("onclick=\"javascript:otherRTOCode1('" + CryptorEngine.Encrypt("RTO_CODE=" + Convert.ToString(dr["RTO_CODE"]), true) + "')\" >    " + Convert.ToString(dr["RTO_NAME"]).ToUpper() + "</div>");
                                      }
                                  }
                                  else
                                  {
                                      sb1.Append("<div class='col-md-12 col-sm-12'>");
                                      sb1.Append("<input type='radio' value='" + Convert.ToString(dr["RTO_NAME"]) + "&" + Convert.ToString(dr["RTO_CODE"]) + "' class='rtoHdr' name ='otherrto' id='RadioButton_" + Convert.ToString(dr["RTO_CODE"]) + "'");
                                      sb1.Append("onclick=\"javascript:otherRTOCode1('" + CryptorEngine.Encrypt("RTO_CODE=" + Convert.ToString(dr["RTO_CODE"]), true) + "')\" >    " + Convert.ToString(dr["RTO_NAME"]).ToUpper() + "</div>");
                                  }
                              }
                              sb1.Append("</div>");
                              sb1.Append("</div>");
                              RTORouteDiv.InnerHtml = sb1.ToString();

                          }
                          if (dt31.Rows.Count > 0)
                          {
                              StringBuilder sbstate = new StringBuilder();


                              sbstate.Append("<div class='input-group'>");
                              sbstate.Append("<div class='icheck-list'>");
                              if (ddlPermitType.SelectedValue == "2")   // For Temporary
                              {

                                  foreach (DataRow dr in dt31.Rows)
                                  {
                                      if (Convert.ToString(dt1.Rows[0]["PERMIT_ANOTHER_STATE_CODE_FIRST"]) != "NO")
                                      {
                                          if (Convert.ToString(dr["STATE_CODE"]) == Convert.ToString(dt1.Rows[0]["PERMIT_ANOTHER_STATE_CODE_FIRST"]))
                                          {
                                              sbstate.Append("<div class='col-md-12 col-sm-12'>");
                                              sbstate.Append("<input type='checkbox' checked ='checked' value='" + Convert.ToString(dr["STATE_CODE"]) + "&" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]) + "'  id='STATE_" + Convert.ToString(dr["STATE_CODE"]) + "' class = 'StateHDR'");
                                              sbstate.Append("onclick=\"javascript:otherSate1('" + CryptorEngine.Encrypt("STATE_CODE=" + Convert.ToString(dr["STATE_CODE"]) + "& VALIDITY=" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]), true) + "')\" >    " + Convert.ToString(dr["STATE_NAME"]).ToUpper() + " <b> - (PERMIT VALID FOR " + dr["PERMIT_VALIDITY_IN_MONTH"] + "  MONTHS) </b>" + "</div>");
                                          }
                                          else if (Convert.ToString(dr["STATE_CODE"]) == Convert.ToString(dt1.Rows[0]["PERMIT_ANOTHER_STATE_CODE_SECOND"]))
                                          {
                                              sbstate.Append("<div class='col-md-12 col-sm-12'>");
                                              sbstate.Append("<input type='checkbox' checked ='checked' value='" + Convert.ToString(dr["STATE_CODE"]) + "&" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]) + "'  id='STATE_" + Convert.ToString(dr["STATE_CODE"]) + "' class = 'StateHDR'");
                                              sbstate.Append("onclick=\"javascript:otherSate1('" + CryptorEngine.Encrypt("STATE_CODE=" + Convert.ToString(dr["STATE_CODE"]) + "& VALIDITY=" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]), true) + "')\" >    " + Convert.ToString(dr["STATE_NAME"]).ToUpper() + " <b> - (PERMIT VALID FOR " + dr["PERMIT_VALIDITY_IN_MONTH"] + "  MONTHS) </b>" + "</div>");
                                          }
                                          else
                                          {
                                              sbstate.Append("<div class='col-md-12 col-sm-12'>");
                                              sbstate.Append("<input type='checkbox' value='" + Convert.ToString(dr["STATE_CODE"]) + "&" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]) + "'  id='STATE_" + Convert.ToString(dr["STATE_CODE"]) + "' class = 'StateHDR'");
                                              sbstate.Append("onclick=\"javascript:otherSate1('" + CryptorEngine.Encrypt("STATE_CODE=" + Convert.ToString(dr["STATE_CODE"]) + "& VALIDITY=" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]), true) + "')\" >    " + Convert.ToString(dr["STATE_NAME"]).ToUpper() + " <b> - (PERMIT VALID FOR " + dr["PERMIT_VALIDITY_IN_MONTH"] + "  MONTHS) </b>" + "</div>");
                                          }
                                         
                                      }
                                      else
                                      {
                                          sbstate.Append("<div class='col-md-12 col-sm-12'>");
                                          sbstate.Append("<input type='checkbox' value='" + Convert.ToString(dr["STATE_CODE"]) + "&" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]) + "'  id='STATE_" + Convert.ToString(dr["STATE_CODE"]) + "' class = 'StateHDR'");
                                          sbstate.Append("onclick=\"javascript:otherSate1('" + CryptorEngine.Encrypt("STATE_CODE=" + Convert.ToString(dr["STATE_CODE"]) + "& VALIDITY=" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]), true) + "')\" >    " + Convert.ToString(dr["STATE_NAME"]).ToUpper() + " <b> - (PERMIT VALID FOR " + dr["PERMIT_VALIDITY_IN_MONTH"] + "  MONTHS) </b>" + "</div>");
                                      }
                                     

                                      

                                  }
                              }
                              else
                              {
                                  foreach (DataRow dr in dt31.Rows)
                                  {

                                      sbstate.Append("<div class='col-md-12 col-sm-12'>");
                                      sbstate.Append("<input disabled='disabled' type='checkbox' value='" + Convert.ToString(dr["STATE_CODE"]) + "&" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]) + "'  id='STATE_" + Convert.ToString(dr["STATE_CODE"]) + "' class = 'StateHDR'");
                                      sbstate.Append("onclick=\"javascript:otherSate1('" + CryptorEngine.Encrypt("STATE_CODE=" + Convert.ToString(dr["STATE_CODE"]) + "& VALIDITY=" + Convert.ToString(dr["PERMIT_VALIDITY_IN_MONTH"]), true) + "')\" >    " + Convert.ToString(dr["STATE_NAME"]).ToUpper() + " <b> - (PERMIT VALID FOR " + dr["PERMIT_VALIDITY_IN_MONTH"] + "  MONTHS) </b>" + "</div>");
                                  }
                              }

                              sbstate.Append("</div>");
                              sbstate.Append("</div>");
                              otherStateDiv.InnerHtml = sbstate.ToString();
                          }

                      }
                  }


            }

            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='SCREEN_RTA_GOODS_PERMIT_REPORT.aspx'", true);

            }




        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RTA-DASHBOARD.aspx");
    }
 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        SqlTransaction objTrans = null;
        string permittypeid = string.Empty;
        string permittypeoptionserviceId = string.Empty;
        if (ddlPermitType.SelectedValue == "2")
        {
            permittypeid = "3";
            permittypeoptionserviceId = "29";
        }
        else
        {
            permittypeid = "4";
            permittypeoptionserviceId = "41";

        }
        
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            objTrans = sCon.BeginTransaction();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;
            sCmd.Transaction = objTrans;
            try
            {
               
                
                //Int32 ROUTE_ID = Convert.ToInt32(hdnID.Value);
                sCmd.CommandText = "SCREEN_RTA_GOODS_PERMIT_APPLICATION_MASTER_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();
                sCmd.Parameters.AddWithValue("@APP_ID", hdnID.Value);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", permittypeid);
                sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", Convert.ToInt32(permittypeoptionserviceId));
                sCmd.Parameters.AddWithValue("@RTO_CODE", RTOCode.Value);
                string selectedGender, wholestate, permitSelf; 
                if (txtMale.Checked)
                {
                    selectedGender = txtMale.Value;
                }
                else
                {
                    selectedGender = txtFemale.Value;
                }

                if (WholeStateCheckbox.Checked)
                {
                    wholestate = "YES";
                }
                else
                {
                    wholestate = "NO";
                }
                if (SelfCheckBox.Checked)
                {
                    permitSelf = "YES";
                }
                else
                {
                    permitSelf = "NO";
                }
                string OtherRTOCode = "";
                if(otherRTOCode.Value != "")
                {
                    OtherRTOCode = CryptorEngine.Decrypt(otherRTOCode.Value, true).ToString().Substring(9);
                }
                else
                {
                     OtherRTOCode = "NO";
                }

                int validity = 0;
                string FirstStatecode = "NO";
                string SecondStatecode = "NO";
                if (hdnStateCode.Value != "S" && hdnStateCode.Value != "")
                {
                    string[] stateCodevalidoty = hdnStateCode.Value.Split('-');
                    string finalstring = stateCodevalidoty[0].Substring(1);
                    FirstStatecode = finalstring.Split('&')[0];
                    SecondStatecode = stateCodevalidoty[1].Split('&')[0];
                    if(SecondStatecode=="")
                    {
                        SecondStatecode = "NO";
                        int num1 = Convert.ToInt32(finalstring.Split('&')[1]);
                        validity = num1;
   
                        
                    }
                    else
                    {
                        int num1 = Convert.ToInt32(finalstring.Split('&')[1]);
                        int num2 = Convert.ToInt32(stateCodevalidoty[1].Split('&')[1]);
                        validity = num1 < num2 ? num1 : num2;

                        
                    }

                    
                }

                sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", txtpermitNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@PERMIT_SELF", permitSelf);
                sCmd.Parameters.AddWithValue("@PERMIT_ANOTHER_RTO_CODE", OtherRTOCode);
                sCmd.Parameters.AddWithValue("@PERMIT_WHOLE_JHARKHAND", wholestate);
                sCmd.Parameters.AddWithValue("@PERMIT_ANOTHER_STATE_CODE_FIRST", FirstStatecode);
                sCmd.Parameters.AddWithValue("@PERMIT_ANOTHER_STATE_CODE_SECOND", SecondStatecode);
                sCmd.Parameters.AddWithValue("@PERMIT_VALIDITY_IN_MONTH", validity);
                sCmd.Parameters.AddWithValue("@NATURE_OF_GOODS", VNatureofGoods.Text);
                sCmd.Parameters.AddWithValue("@RATES_OF_CHARGES", Convert.ToDecimal(txtRates.Text));
                sCmd.Parameters.AddWithValue("@REGISTRATION_NUMBER", VORegNumber.Text);
                sCmd.Parameters.AddWithValue("@OWNER_NAME", VOName.Text);
                sCmd.Parameters.AddWithValue("@CARE_OF", VOCareOf.Text);
                sCmd.Parameters.AddWithValue("@PRESENT_ADDRESS", VOPresentAddress.Text);
                sCmd.Parameters.AddWithValue("@PERMANENT_ADDRESS", VOPermanentAddress.Text);
              
                
               
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
                ViewState["manufactDate"] = VManufacturingON.Value;
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
                sCmd.Parameters.AddWithValue("@ULW", Convert.ToInt32(VULW.Text));
                sCmd.Parameters.AddWithValue("@RLW", Convert.ToInt32(VRLW.Text));
                sCmd.Parameters.AddWithValue("@VEHICLE_CAPACITY", Convert.ToDecimal(txtVCapacity.Text));
                sCmd.Parameters.AddWithValue("@MANUFACTURING_ON", ManufacturingON);
                //sCmd.Parameters.AddWithValue("@ST_TO", txtTo.Text.Trim());
                //sCmd.Parameters.AddWithValue("@ST_FROM", txtFrom.Text.Trim());
                
                sCmd.Parameters.AddWithValue("@MOBILE_NUMBER", txtMobileNo.Text.Trim());
                sCmd.Parameters.AddWithValue("@EMAIL_ID", txtEmailId.Text.Trim());
                sCmd.Parameters.AddWithValue("@GENDER", selectedGender);
             
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));

                sCmd.Parameters.AddWithValue("@VALID_FROM", DateTime.ParseExact(txtpermitFrom.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@VALID_UP_TO", DateTime.ParseExact(txtpermitTo.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                
               
                sCmd.Parameters.AddWithValue("@APPLICATION_NUMBER", DBNull.Value);

                sCmd.Parameters.AddWithValue("@MODE", "CHECK_EXIST1");
                var Cnt = sCmd.ExecuteScalar();
                if (Convert.ToInt32(Cnt) == 0)
                {
                    sCmd.Parameters["@MODE"].Value = "UPDATE";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    objTrans.Commit();
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('successfully updated');window.location ='SCREEN_RTA_GOODS_PERMIT_REPORT.aspx'", true);

                }
                else
                {
                    objTrans.Rollback();
                    String Message = "This permit and registration number already exists.";
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    VManufacturingON.Value = Convert.ToString(ViewState["manufactDate"]);
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
                }

            }
            catch (Exception ex)
            {
                objTrans.Rollback();
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='SCREEN_RTA_GOODS_PERMIT_REPORT.aspx'", true);
                // Response.Redirect("PermitHomePage.aspx");
            }
        }
    }
 
}