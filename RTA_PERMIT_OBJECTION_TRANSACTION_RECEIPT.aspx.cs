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



public partial class RTA_PERMIT_OBJECTION_TRANSACTION_RECEIPT : System.Web.UI.Page
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
                String abc = CryptorEngine.Encrypt("APP_ID=" + Convert.ToString(1), true);
                QueryString IDx = QueryStr.Where(l => l.Name == "APP_ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                String ID = IDx.Value;
                //String ID = "1";

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ID", ID);
                parameters[1] = new SqlParameter("@EMPLOYEE_ID","");
                parameters[2] = new SqlParameter("@MODE", "TAX_RECEIPT_COPY_PERMIT_OBJECTION");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST_BYID", parameters);
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        lblApplicationNumber.InnerText = dt1.Rows[i]["APPLICATION_NUMBER"].ToString();
                        lblApplicationn.InnerText = dt1.Rows[i]["APPLICATION_NUMBER"].ToString();
                        lblFormNo1.InnerText = dt1.Rows[i]["APPLICATION_NUMBER"].ToString();
                        

                        lblApplicantName.InnerText = dt1.Rows[i]["APPLICANT_NAME"].ToString();
                        lblApplicantName2.InnerText = dt1.Rows[i]["APPLICANT_NAME"].ToString();


                        lblRegNo.InnerText = dt1.Rows[i]["REGISTRATION_NUMBER"].ToString();
                        lblRegNo2.InnerText = dt1.Rows[i]["REGISTRATION_NUMBER"].ToString();


                        lblTaxAmount.InnerText = dt1.Rows[i]["TXN_AMOUNT"].ToString();
                        lblTaxAmount3.InnerText = dt1.Rows[i]["TXN_AMOUNT"].ToString();


                        

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                    return;
                }
            }


            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='PermitHomePage.aspx'", true);

            }


        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}