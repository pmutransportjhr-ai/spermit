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
using System.Web.UI.HtmlControls;



public partial class Application_Withdrawal : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string queryString = CryptorEngine.Decrypt(Request.QueryString["q"], true);
            foreach (String str in queryString.Split(new Char[] { '&' }))
            {
                QueryString qs = new QueryString();
                qs.Name = str.Split(new Char[] { '=' })[0];
                qs.Value = str.Split(new Char[] { '=' })[1];
                QueryStr.Add(qs);
            }
            QueryString IDx = QueryStr.Where(l => l.Name == "APP_ID").FirstOrDefault();
            QueryString TableNamex = QueryStr.Where(l => l.Name == "TABLE").FirstOrDefault();
            String Application_Id = IDx.Value;
            String TableName = TableNamex.Value;
            hdnAppID.Value = Application_Id;
            hdnTable.Value = TableName;
            using (SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ToString())) 
            {
                scon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_withDraw_aplication";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                cmd.Parameters.AddWithValue("@APP_ID", Application_Id);
                cmd.Parameters.AddWithValue("@TABLE", TableName);
                cmd.Parameters.AddWithValue("@MODE", "GET_DETAIL");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                lblApplicationNumber.Text = Convert.ToString(dt.Rows[0]["APPLICATION_NUMBER"]);
                lblRegistration.Text = Convert.ToString(dt.Rows[0]["REGISTRATION_NUMBER"]);
                

            }





        }
    }

    protected void btnWithdraw_Click(object sender, EventArgs e)
    {
        string App_id = hdnAppID.Value.Trim();
        string tableName = hdnTable.Value.Trim();
        string appNumber = lblApplicationNumber.Text.Trim();

        using (SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ToString()))
        {
            SqlTransaction obTrans = null;

            try
            {
                scon.Open();

                obTrans = scon.BeginTransaction();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_withDraw_aplication";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = scon;
                cmd.Transaction = obTrans;

                cmd.Parameters.AddWithValue("@APP_ID", App_id);
                cmd.Parameters.AddWithValue("@TABLE", tableName);
                cmd.Parameters.AddWithValue("@MODE", "UPDATE");

                cmd.ExecuteNonQuery();

                obTrans.Commit();

                string successMsg = "Application No. " + appNumber + " has been successfully withdrawn.";

                ScriptManager.RegisterClientScriptBlock(
                    Page,
                    Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('" + successMsg + "'); window.location.href='PermitHomePage.aspx';",
                    true);
            }
            catch (Exception ex)
            {
                if (obTrans != null)
                {
                    obTrans.Rollback();
                }

                string errorMsg = "Unable to withdraw the application at the moment. Please try again later or contact the Transport Department.";

                ScriptManager.RegisterClientScriptBlock(
                    Page,
                    Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('" + errorMsg + "'); window.location.href='PermitHomePage.aspx';",
                    true);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Permithomepage.aspx");
    }
}