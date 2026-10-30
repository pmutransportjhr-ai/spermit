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



public partial class STATimeTable : System.Web.UI.Page
{
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-DASHBOARD.ASPX'", true);
                return;
            }

            StringBuilder sb = new StringBuilder();
            
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
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

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "STA_SP_GET_MASTER_DATA";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@PERMIT_NUMBER", ID);
                    sCmd.Parameters.AddWithValue("@MODE", "TIME_TABLE_RECEIPT");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt1 = ds.Tables[0];
                    DataTable dt2 = ds.Tables[1];
                    DataTable dt3 = ds.Tables[2];
                    if (dt1.Rows.Count > 0)
                    {

                        lblDate.Text = DateTime.UtcNow.Date.ToString("dd-MM-yyyy");
                            lblRoute.Text = dt1.Rows[0]["ROUTE"].ToString();
                            lblName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                            lblPermitNumber.Text = dt1.Rows[0]["PERMIT_NUMBER"].ToString();
                            lblRegNo.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                            if (dt1.Rows[0]["CS_STATUS"].ToString()=="Verified")
                            {
                                lbltext.Text = "Time Table Approved till " + dt1.Rows[0]["VALID_UP_TO"].ToString();
                    
                            }
                            else
                            {
                                lbltext.Text = "Time Table(Provissionally Approved) for two months only";
                    
                            }
                            if (dt3.Rows.Count > 0)
                            {
                                for (int k = 1; k <= dt3.Rows.Count; k++)
                                {
                                    if (dt2.Rows.Count > 0)
                                    {
                                        DataRow[] dr = dt2.Select("SEQUENCE='" + k + "'");

                                        if (dr.Length > 0)
                                        {
                                            //Int32 length = dt2.Rows.Count - 1;
                                            Int32 length = dr.Length - 1;
                                            sb.Append("<table class='table table-striped table-bordered table-hover'>");
                                            sb.Append("<thead>");
                                            sb.Append("<tr>");
                                            sb.Append("<th colspan='3' style='text-align:center;'> Up </th>");
                                            sb.Append("<th>  </th>");
                                            sb.Append("<th colspan='3' style='text-align:center;'> Down </th>");
                                            sb.Append("</tr>");
                                            sb.Append("<tr>");
                                            sb.Append("<th> Route </th>");
                                            sb.Append("<th> Arrival </th>");
                                            sb.Append("<th> Departure </th>");
                                            sb.Append("<th>  </th>");
                                            sb.Append("<th> Route </th>");
                                            sb.Append("<th> Arrival </th>");
                                            sb.Append("<th> Departure </th>");
                                            sb.Append("</tr>");
                                            sb.Append("</thead>");
                                            sb.Append("<tbody id='tbdRouteInfo'>");
                                            for (int i = 0; i < dr.Length; i++)
                                            {


                                                if (i == 0)
                                                {
                                                    sb.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                                    sb.Append("<td> ..... </td>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "</label></td>");
                                                    sb.Append("<td></td>");
                                                    sb.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                                    sb.Append("<td> ..... </td>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                                    sb.Append("</tr>");
                                                }
                                                else if (i == length)
                                                {
                                                    sb.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "</label></td>");
                                                    sb.Append("<td> ..... </td>");
                                                    sb.Append("<td></td>");
                                                    sb.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                                    sb.Append("<td> ..... </td>");
                                                    sb.Append("</tr>");

                                                }
                                                else
                                                {
                                                    sb.Append("<tr id='tr_" + Convert.ToString(dr[i]["S_NO"]) + "'>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + "</label></td>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + "</label></td>");
                                                    sb.Append("<td></td>");
                                                    sb.Append("<td>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                                    sb.Append("<td><label>" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                                    sb.Append("</tr>");
                                                }

                                            }
                                            sb.Append("<tbody>");
                                            sb.Append("</table>");

                                        }
                                    }
                                }

                                RouteTimeinfo.InnerHtml = sb.ToString();


                            }
                            //RouteTimeinfo.Text = "";
                       
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='STA-DASHBOARD.ASPX'", true);
                        return;
                    }
                }


                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    Messqge = Messqge.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Messqge + "');window.location ='STA-DASHBOARD.ASPX'", true);

                }
            }

        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}


