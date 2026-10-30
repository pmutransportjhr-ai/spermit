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
using System.Globalization;

public partial class RTA_CONTRACT_PERMIT_APPLICATION_VERIFY : System.Web.UI.Page
{
    //private static string ID;
  
        
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-AUTO-RICKSHAW-PERMIT-APPLICATION-VERIFY-LIST.aspx'", true);
                return;
            }
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

                ID.Value = IDx.Value;
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ID", ID.Value);
                parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                parameters[2] = new SqlParameter("@MODE", "16KM_PERMIT_APPLICATION_VERIFY_LIST_BYID");
                string stremployee = Convert.ToString(Context.Session["EMPLOYEE_ID"]);

                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST_BYID", parameters);


                //DataSet ds = RTAGenericMasterListByID.GetMasterList1(ID, Convert.ToString(Session["EMPLOYEE_ID"]), "PERMIT_INTER_STATE_APP_REPLACE_APPROVAL_LIST");

                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];

                StringBuilder sb = new StringBuilder();
                if (dt1.Rows.Count > 0)
                {
                    string APPLICATION_STATUS = dt1.Rows[0]["APPLICATION_STATUS"].ToString();

                    string Payment_status = dt1.Rows[0]["PAYMENT_STATUS"].ToString();
                    if (Payment_status == "Unpaid")
                    {
                        btnSave.Visible = true;
                        if (APPLICATION_STATUS == "Rejected")
                        {
                            ddlApplicationStatus.Visible = false;

                            btnSave.Visible = false;
                            lblapplicationstatus.Text = "Already " + APPLICATION_STATUS;
                        }
                        else
                        {
                            ddlApplicationStatus.Items.Clear();
                            ddlApplicationStatus.Items.Add(new ListItem("--select application status--", "0"));
                            ddlApplicationStatus.Items.Add(new ListItem("Rejected", "Rejected"));
                        }

                    }
                    else
                    {
                        btnSave.Visible = true;


                        if (APPLICATION_STATUS == "Verified" || APPLICATION_STATUS == "Rejected" || APPLICATION_STATUS == "Issued")
                        {
                            btnSave.Visible = false;
                            ddlApplicationStatus.Visible = false;



                            lblapplicationstatus.Text = "Already " + APPLICATION_STATUS;
                            lblapplicationstatus.Visible = true;

                        }
                        else
                        {
                            btnSave.Visible = true;
                            ddlApplicationStatus.Visible = true;


                            lblapplicationstatus.Text = "";
                            lblapplicationstatus.Visible = false;

                        }
                    }



                    lblRTO.Text = dt1.Rows[0]["RTO_NAME"].ToString();
                    lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();

                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString(); 
                    
                    VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    VOCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                    VOPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                    VOPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();
                    VOMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                    VChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                    VEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();
                    VTax.Text = dt1.Rows[0]["TAX_PAID_UPTO"].ToString();
                    VFitness.Text = dt1.Rows[0]["FITNESS_VALID_UPTO"].ToString();
                    VType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                    VInsuranceCmp.Text = dt1.Rows[0]["INSURANCE_COMPANY"].ToString();
                    VInsuranceNo.Text = dt1.Rows[0]["INSURANCE_POLICY_NO"].ToString();
                    VInsuranceUpto.Text = dt1.Rows[0]["INSURANCE_UPTO"].ToString();
                    lblPermitFees.Text = dt1.Rows[0]["FEE"].ToString();
                    lblPaymentStatus.Text = dt1.Rows[0]["PAYMENT_STATUS"].ToString();
                    VSeatingCapacity.Text = dt1.Rows[0]["SEAT_CAPACITY"].ToString();
                    VAgreementWith.Text = dt1.Rows[0]["AGREEMENT_WITH"].ToString();
                    txtObjectionDate.Text = Convert.ToString(dt1.Rows[0]["OBJECTION_TILL"]);
                }


                foreach (DataRow dr in dt2.Rows)
                {
                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "RC")
                    {

                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click Here to Download");
                        divrcfile.InnerHtml = strlink;

                    }

                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "INSURANCE")
                    {
                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click Here to Download");
                        divinsurance.InnerHtml = strlink;

                    }

                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "POLLUTION")
                    {
                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click Here to Download");
                        divpollution.InnerHtml = strlink;
                    }

                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "FITNESS")
                    {
                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click Here to Download");
                       divfitness.InnerHtml = strlink;
                    }
                    

                    if (Convert.ToString(dr["UPLOAD_HEADS"]) == "AGREEMENT")
                    {

                        string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                        string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click Here to Download");
                        divagreement.InnerHtml = strlink;

                    }

                }
            }

            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-CONTRACT-CARRIAGE-PERMIT-APPLICATION-VERIFY-LIST.aspx'", true);

            }


        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RTA-AUTO-RICKSHAW-PERMIT-APPLICATION-VERIFY-LIST.aspx");

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#confirm').modal('show');", true);
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@APP_ID", ID.Value);
            parameters[1] = new SqlParameter("@TYPE", DBNull.Value);
            parameters[2] = new SqlParameter("@STATUS", ddlApplicationStatus.SelectedValue);

            if (ddlApplicationStatus.SelectedValue.Trim() == "Verified")
            {

                DateTime objection_till = DateTime.Parse(txtObjectionDate.Text, new CultureInfo("en-CA"));
                parameters[3] = new SqlParameter("@OBJECTION_TILL", objection_till);

            }
            else
                parameters[3] = new SqlParameter("@OBJECTION_TILL", DBNull.Value);



            parameters[4] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
            parameters[5] = new SqlParameter("@MODE", "VERIFIED_REJCTION_UPDATE");


            SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_AUTO_RICKSHAW_PERMIT_APPLICATION_APPROVAL", parameters);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully submitted');window.location ='RTA-AUTO-RICKSHAW-PERMIT-APPLICATION-VERIFY-LIST.aspx'", true);
        }
        catch (Exception ex)
        {
            String Message = ex.Message;
            Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-AUTO-RICKSHAW-PERMIT-APPLICATION-VERIFY-LIST.aspx'", true);

        }

    }
  
}
    
    



    