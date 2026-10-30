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



public partial class RTA_PERMIT_COUNTER_SIGNATURE_ISSUED_RECEIPT : System.Web.UI.Page
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
                String abc = CryptorEngine.Encrypt("ID=" + Convert.ToString(1), true);
                QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);
                String ID = IDx.Value;
                

                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@ID", ID);
                parameters[1] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
                parameters[2] = new SqlParameter("@MODE", "COUNTER_SIGNATURE_LIST");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST_BYID", parameters);
                DataTable dt1 = ds.Tables[0];
                        DataTable dt2 = ds.Tables[1];
                        DataTable dt3 = ds.Tables[2];
                        StringBuilder sb = new StringBuilder();
                        if (dt1.Rows.Count > 0)
                        {
                            lblAppNumber.InnerText = Convert.ToString(dt1.Rows[0]["PERMIT_APPLICATION_NO"]);
                            lblAppNumber1.InnerText = Convert.ToString(dt1.Rows[0]["PERMIT_APPLICATION_NO"]);
                            lblAppDt.InnerText = Convert.ToString(dt1.Rows[0]["CREATED_ON"]);
                            lblName.InnerText = Convert.ToString(dt1.Rows[0]["OWNER_NAME"]);
                            lblResAdd.InnerText = Convert.ToString(dt1.Rows[0]["PERMANENT_ADDRESS"]);
                            lblBuAdd.InnerText = Convert.ToString(dt1.Rows[0]["PRESENT_ADDRESS"]);
                            lblPermitNo.InnerText = Convert.ToString(dt1.Rows[0]["PERMIT_NUMBER"]);
                            lblValiUpto.InnerText = Convert.ToString(dt1.Rows[0]["VALID_UP_TO"]);
                            lblIssuedBY.InnerText = Convert.ToString(dt1.Rows[0]["PERMIT_RTO_NAME"]);
                            lblRegNo.InnerText = Convert.ToString(dt1.Rows[0]["REGISTRATION_NUMBER"]);
                            
                            lblRoute.InnerText = Convert.ToString(dt1.Rows[0]["ROUTE"]);
                            lblValidityfrom.InnerText = Convert.ToString(dt1.Rows[0]["APPROVED_ON"]);
                            lblValidityto.InnerText = Convert.ToString(dt1.Rows[0]["VALID_UP_TO"]);
                            //lblNatureofGoods.InnerText = Convert.ToString(dt1.Rows[0]["NATURE_OF_GOODS_CARRIED"]);
                            lblTaxDetails.InnerText = Convert.ToString(dt1.Rows[0]["FEE_PAY_TRANSACTION_DETAILS"]);
                            lblApprovalTiming.InnerText = Convert.ToString(dt1.Rows[0]["APPROVED_ON"]);
                            lblMobileNumber.InnerText = Convert.ToString(dt1.Rows[0]["MOBILE_NUMBER"]);

                        

                    }
                        if (dt2.Rows.Count > 0)
                        {
                            Int32 length = dt2.Rows.Count - 1;
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
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    sb.Append("<tr id='tr_" + Convert.ToString(dt2.Rows[i]["S_NO"]) + "'>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                    sb.Append("<td> ..... </td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["UP_DEP_TIME"]) + "</label></td>");
                                    sb.Append("<td></td>");
                                    sb.Append("<td>" + Convert.ToString(dt2.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                    sb.Append("<td> ..... </td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                    sb.Append("</tr>");
                                }
                                else if (i == length)
                                {
                                    sb.Append("<tr id='tr_" + Convert.ToString(dt2.Rows[i]["S_NO"]) + "'>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["UP_ARR_TIME"]) + "</label></td>");
                                    sb.Append("<td> ..... </td>");
                                    sb.Append("<td></td>");
                                    sb.Append("<td>" + Convert.ToString(dt2.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                    sb.Append("<td> ..... </td>");
                                    sb.Append("</tr>");

                                }
                                else
                                {
                                    sb.Append("<tr id='tr_" + Convert.ToString(dt2.Rows[i]["S_NO"]) + "'>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["VIA_UP_CITY"]) + "</label></td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["UP_ARR_TIME"]) + "</label></td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["UP_DEP_TIME"]) + "</label></td>");
                                    sb.Append("<td></td>");
                                    sb.Append("<td>" + Convert.ToString(dt2.Rows[i]["VIA_DOWN_CITY"]) + "</td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["DOWN_ARR_TIME"]) + "</label></td>");
                                    sb.Append("<td><label>" + Convert.ToString(dt2.Rows[i]["DOWN_DEP_TIME"]) + "</label></td>");
                                    sb.Append("</tr>");
                                }

                            }
                            sb.Append("<tbody>");
                            sb.Append("</table>");

                            RouteTimeInfo.InnerHtml = sb.ToString();



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