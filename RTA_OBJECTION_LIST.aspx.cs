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

public partial class RTA_OBJECTION_LIST : System.Web.UI.Page
{
    public string FillObjectionList { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
         using (SqlConnection sCon = new SqlConnection(sConnectionString))
         {
             try
             {
                 sCon.Open();
                 SqlCommand sCmd = new SqlCommand();
                 sCmd.Connection = sCon;
                 sCmd.CommandText = "RTA_OBJECTION_APPLICATION_AED";
                 sCmd.CommandType = CommandType.StoredProcedure;
                 sCmd.Parameters.Clear();
                  sCmd.Parameters.AddWithValue("@MODE", "OBJECTION_LIST");
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
                    string q = HttpUtility.UrlEncode(CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]) + "&TBL_MODE=" + Convert.ToString(dr["TBL_MODE"]), true));

                    sb.Append("<tr>");
                    sb.Append("<td>" + Convert.ToString(dr["S_NO"]) + "</td>");
                    sb.Append("<td><a href='RTA_PERMIT_OBJECTION.aspx?q=" + q + "' target='_blank'>" + Convert.ToString(dr["VEHICLE_REGISTRATION_NO"]) + "</a></td>");
                    sb.Append("<td>" + Convert.ToString(dr["APPLICATION_NUMBER"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["APPLICANT_NAME"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["ROUTE_DETAIL"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["OBJECTION_TILL"]) + "</td>");
                    sb.Append("<td>" + Convert.ToString(dr["OBJECTIONS"]) + "</td>");


                    sb.Append("</tr>");
                }

                FillObjectionList = sb.ToString();
            }
             }
             catch (Exception ex)
             {
                 String Messqge = ex.Message;
                 Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                 ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');window.location ='PermithomePage.aspx'", true);


             }
         }
        }
    }
}