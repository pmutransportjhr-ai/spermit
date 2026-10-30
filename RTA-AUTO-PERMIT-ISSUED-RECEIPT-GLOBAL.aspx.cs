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



public partial class RTA_AUTO_PERMIT_ISSUED_RECEIPT_GLOBAL : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-DASHBOARD.aspx'", true);
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
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_GET_AUTO_PERMIT_MASTER_DATA", parameters);
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        lblPermitNo.Text = dt1.Rows[i]["PERMIT_NUMBER"].ToString();
                        lblHdr.InnerText = dt1.Rows[i]["SERVICE_NAME"].ToString();
                        lblmsg.InnerText = dt1.Rows[i]["MSG"].ToString();
                        

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='RTA-DASHBOARD.aspx'", true);
                    return;
                }
            }


            catch (Exception ex)
            {
                String Message = ex.Message;
                Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-DASHBOARD.aspx'", true);

            }


        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}


