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
using QRCoder;
using System.Drawing;
using System.IO;



public partial class QR_STA_CS_RECEIPT : System.Web.UI.Page
{
   protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                //return;
            }
            try
            {
                String queryString = Convert.ToString(Context.Request.QueryString["q"]);

                
               
                String appID = queryString.Split('~')[0].Split('=')[1];
                String type =queryString.Split('~')[1].Split('=')[1];
                String appService = queryString.Split('~')[2].Split('=')[1];
                String tableType = queryString.Split('~')[3].Split('=')[1];
               

                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@ID", appID);
                parameters[1] = new SqlParameter("@TBL_TYPE", tableType);
                parameters[2] = new SqlParameter("@APPLICATION_SERVICE", appService);
                parameters[3] = new SqlParameter("@TYPE", type);
                parameters[4] = new SqlParameter("@MODE", "OTHER_STATE_TAX_RECEIPT");
                DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST_BYID", parameters);
                DataTable dt1 = ds.Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        string name = Convert.ToString(dr["OWNER_NAME"]);
                        string registrationNumber = Convert.ToString(dr["REGISTRATION_NUMBER"]);
                        string taxAmount = Convert.ToString(dr["AMOUNT"]);
                        string trnRef = Convert.ToString(dr["TRN_REF_NO"]);
                        string dated = Convert.ToString(dr["TXN_DATED"]);
                        string make = Convert.ToString(dr["VEHICLE_CLASS_TYPE"]);
                        string enginenumber = Convert.ToString(dr["ENGINE_NUMBER"]);
                        string chaisenumber = Convert.ToString(dr["CHASIS_NUMBER"]);
                        string paidFrom = Convert.ToString(dr["FEE_TAX_PAID_FROM"]);
                        string paidUpto = Convert.ToString(dr["FEE_TAX_PAID_UP_TO"]);
                        string grnNumber = Convert.ToString(dr["GRN"]);
                        string seatingCapaity = Convert.ToString(dr["SEATING_CAPACITY"]);
                        string SleeperCapacity = Convert.ToString(dr["SLEEPER_CAPACITY"]);
                        string makeYear = Convert.ToString(dr["MAKER_MODEL"]);
                        lblApplicantName.InnerText = name;
                        lblRegisNo.InnerText = registrationNumber;
                        lblgrnNumber.InnerText = grnNumber;
                        lblDated.InnerText = dated;
                        lblMake.InnerText = make+ "(Seating Capacity: "+seatingCapaity+")"+ "("+makeYear+")"+"(Sleeper Capacity:"+SleeperCapacity+")";
                   
                        lblEngineNumber.InnerText = enginenumber;
                        lblChaiseNo.InnerText = chaisenumber;
                        lblPaidUpto.InnerText = paidUpto;
                        lblRegNo.InnerText = registrationNumber;
                        lblTaxAmount.InnerText = taxAmount;
                        lblTransNumber.InnerText = trnRef;

                        lblTaxperiod.InnerText = paidFrom + " to " + paidUpto;

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