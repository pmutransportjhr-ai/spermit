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



public partial class RTA_ROUTE_REPORT_NEW : System.Web.UI.Page
{
    //  private static string ID;
    // private static string TYPE;
    //private static string TYPE_OPTION_ID; 
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

                QueryString IDx = QueryStr.Where(l => l.Name == "ROUTE_ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                QueryString IDx1 = QueryStr.Where(l => l.Name == "TYPE").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                QueryString IDx2 = QueryStr.Where(l => l.Name == "TYPE_OPTION_ID").FirstOrDefault();
                hdnID.Value = IDx.Value;
                hdnType.Value = IDx1.Value;
                hdnTypeOptionID.Value = IDx2.Value;
                try
                {

                    String ROUTE_ID = hdnID.Value;
                    
                    SqlParameter[] parameters = new SqlParameter[6];
                    parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                    parameters[1] = new SqlParameter("@ROUTE_ID", ROUTE_ID);
                    parameters[2] = new SqlParameter("@PERMIT_TYPE_ID", DBNull.Value);
                    parameters[3] = new SqlParameter("@PERMIT_TYPE_OPTION_ID", DBNull.Value);
                    parameters[5] = new SqlParameter("@MODE", "LIST");
                    DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_RTA_MERIT_REPORT", parameters);
                    DataTable dt = new DataTable();
                    dt = ds.Tables[0];
                    StringBuilder sb = new StringBuilder();
                    if (dt.Rows.Count > 0)
                    {


                        foreach (DataRow dr in dt.Rows)
                        {

                            //string q = CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["APP_ID"]), true);

                            sb.Append("<tr>");
                            sb.Append("<td>" + Convert.ToString(dr["SNO"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["NAMEADDRESS"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["VEHICLE_DETAILS"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["AIR_CONDITIONED"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["DELUXE"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["APPLICANT_CATEGORY"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["EX_ARMY"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["MINORITY"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["GENDER"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["Q_PG"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["Q_INTERMEDIATE"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["Q_MATRIC"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["HEAVY_MOTOR_LICENCE"]) + "</td>");
                            sb.Append("<td>" + Convert.ToString(dr["HANDICAPPED"]) + "</td>");
                            

                            sb.Append("<td>" + Convert.ToString(dr["MARKES_OBTAINED"]) + "</td>");

                            //if (Convert.ToString(dr["READ_ACCESS"]) == "Y" && Convert.ToString(dr["WRITE_ACCESS"]) == "Y")
                            //{
                            //    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnEdit('" + q + "');\">Edit</a></td>");
                            //}
                            //else
                            //{
                            //    sb.Append("<td></td>");
                            //}

                            //String PublishStatus = Convert.ToString(dr["PUBLISH_STATUS"]);
                            //if (PublishStatus == "Draft")
                            //{
                            //    sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnPublish('" + CryptorEngine.Encrypt(Convert.ToString(dr["APP_ID"]), true) + "');\">Publish</a></td>");
                            //}
                            //else //if (Convert.ToString(dr["PUBLISH_STATUS"]) == "Published")
                            //{
                            //    sb.Append("<td></td>");
                            //}

                            sb.Append("</tr>");
                        }

                        FillSTAPermitApplication.InnerHtml = "";
                        FillSTAPermitApplication.InnerHtml = sb.ToString();
                        tblISPermit.Visible = true;
                    }
                    else
                    {
                        tblISPermit.Visible = false;
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('No Data Found');", true);
                    }
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');TableDatatablesScroller.init();", true);

                }
                catch (Exception ex)
                {
                    String Messqge = ex.Message;
                    Messqge = Messqge.Replace("'", "").Replace("\r", " ").Replace("\n", " ");
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('#loadingmessage').modal('hide');alert('" + Messqge + "');", true);


                }
                

            }

            catch (Exception ex)
            {
                String Messqge = ex.Message;
                Messqge = Messqge.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Invalid URL');window.location ='RTA-DASHBOARD.aspx'", true);

            }

        }

    }


}