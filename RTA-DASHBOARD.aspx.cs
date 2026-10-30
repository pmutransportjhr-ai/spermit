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

public partial class RTA_DASHBOARD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadTopData();
    }
    public void LoadTopData()
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@EMPLOYEE_ID", Convert.ToString(Session["EMPLOYEE_ID"]));
            parameters[1] = new SqlParameter("@MODE", "TOP_HDR");
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_GET_DASHBOARD_DETAILS", parameters);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            RTAIssued.InnerText = Convert.ToString(dt.Rows[0]["RTA_ISSUED"]);
            GoodsIssued.InnerText = Convert.ToString(dt.Rows[0]["GOODS_ISSUED"]);
            ContractIssued.InnerText = Convert.ToString(dt.Rows[0]["CONTRACT_ISSUED"]);
            AutoIssued.InnerText = Convert.ToString(dt.Rows[0]["AUTO_ISSUED"]);
            SchoolIssued.InnerText = Convert.ToString(dt.Rows[0]["SCHOOL_ISSUED"]);

            RTAPen.InnerText = Convert.ToString(dt.Rows[0]["RTA_PEN"]);
            GoodsPen.InnerText = Convert.ToString(dt.Rows[0]["GOODS_PEN"]);
            ContractPen.InnerText = Convert.ToString(dt.Rows[0]["CONTRACT_PEN"]);
            AutoPen.InnerText = Convert.ToString(dt.Rows[0]["AUTO_PEN"]);
            SchoolPen.InnerText = Convert.ToString(dt.Rows[0]["SCHOOL_PEN"]);

            

            


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
        }

    }
}