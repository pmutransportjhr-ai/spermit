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



public partial class STA_PERMIT_ISSUED_RECEIPT_GLOBAL : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
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

                QueryString IDx = QueryStr.Where(l => l.Name == "PER_NUMBER").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                String ID = IDx.Value;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@PERMIT_NUMBER", ID);
                parameters[1] = new SqlParameter("@MODE", "ISSUED_RECEIPT");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_GET_MASTER_DATA", parameters);
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0]["APPLICATION_STATUS"].ToString() == "Renewed")
                    {
                        PanelRenewal.Visible = true;
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            lblRenYear.Text = dt1.Rows[0]["APPROVED_ON_YEAR"].ToString();
                            lblRenRef.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                            lblRenDate.Text = dt1.Rows[0]["APPROVED_DATE"].ToString();
                            lblRenSubject.Text = dt1.Rows[0]["SUBJECT_LINE"].ToString();
                            lblRenPermitType1.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                            lblRenPermitNo1.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                            lblRenOwnerName.Text = dt1.Rows[0]["APPLICANT_NAME"].ToString();
                            lblRenRoute1.Text = dt1.Rows[0]["ACT_ROUTE"].ToString();
                            lblRenRegNo.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                            lblRenModel.Text = dt1.Rows[0]["MODEL_NUMBER"].ToString();
                            lblRenOldValidUpto.Text = dt1.Rows[0]["OLD_VALID_UP_TO"].ToString();
                            lblRenDuration.Text = dt1.Rows[0]["DURATION"].ToString();
                            lblRenValidUpto.Text = dt1.Rows[0]["VALID_UP_TO"].ToString();
                        
                        }
                    }
                    if (dt1.Rows[0]["APPLICATION_STATUS"].ToString() == "Replaced")
                    {
                        PanelReplacement.Visible = true;
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {

                            lblRepYear.Text = dt1.Rows[0]["APPROVED_ON_YEAR"].ToString();
                            lblRepRef.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                            lblRepDate.Text = dt1.Rows[0]["APPROVED_DATE"].ToString();
                            lblRepSubject.Text = dt1.Rows[0]["SUBJECT_LINE"].ToString();
                            lblRepPermitType1.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                            lblRepPermitNo1.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                            lblRepOldRegNo1.Text = dt1.Rows[0]["OLD_REGISTRATION_NUMBER"].ToString();
                            lblRepOldModel1.Text = dt1.Rows[0]["OLD_MODEL_NUMBER"].ToString();
                            lblRepRegNo1.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                            lblRepModel1.Text = dt1.Rows[0]["NEW_MODEL_NUMBER"].ToString();
                            
                    
                        }
                    }
                    if (dt1.Rows[0]["APPLICATION_STATUS"].ToString() == "Route Extend")
                    {
                        PanelRouteExtend.Visible = true;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {

                        lblREYear.Text = dt1.Rows[0]["APPROVED_ON_YEAR"].ToString();
                        lblRERef.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                        lblREDate.Text = dt1.Rows[0]["APPROVED_DATE"].ToString();
                        lblRESubject.Text = dt1.Rows[0]["SUBJECT_LINE"].ToString();
                        lblRERoute1.Text = dt1.Rows[0]["ACT_ROUTE"].ToString();
                        lblREStation1.Text = dt1.Rows[0]["EXTEND_UPTO"].ToString();
                        lblREApprovedDate.Text = dt1.Rows[0]["APPROVED_DATE"].ToString();
                        lblRERoute2.Text = dt1.Rows[0]["ACT_ROUTE"].ToString();
                        lblREStation2.Text = dt1.Rows[0]["ROUTE_NEW"].ToString();
                                


                    }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-DASHBOARD.aspx'", true);
                    return;
                }
            }


            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='STA-DASHBOARD.aspx'", true);

            }


        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}


