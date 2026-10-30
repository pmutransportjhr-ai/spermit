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

/// <summary>
/// Summary description for FileUploadValidations
/// </summary>
public class PageTitle
{

    public DataTable PageHeading(Int32 TypeId, Int32 TypeServiceId)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;

            sCmd.CommandText = "SP_GET_TYPE";
            sCmd.CommandType = CommandType.StoredProcedure;
            sCmd.Parameters.Clear();

            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", TypeId);
            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", TypeServiceId);
            sCmd.Parameters.AddWithValue("@MODE", "GET_STA_TYPE");
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sCmd);
            da.Fill(ds);


            DataTable dt1 = new DataTable();
            dt1 = ds.Tables[0];
            //if (dt1.Rows.Count > 0)
            //{
            //    Page.Title = dt1.Rows[0]["PERMIT_TYPE"].ToString() + " | Apply for Permit (Permit)";
            //    PageHeading.InnerText = dt1.Rows[0]["PERMIT_TYPE"].ToString();

            //    PageHeading1.InnerText = dt1.Rows[0]["PERMIT_TYPE"].ToString() + " | Apply for Permit (Permit)";

            //}
            return dt1;




        }
    }
    public DataTable PageRTAHeading(Int32 TypeId, Int32 TypeServiceId)
    {
        String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        using (SqlConnection sCon = new SqlConnection(sConnectionString))
        {
            sCon.Open();
            SqlCommand sCmd = new SqlCommand();
            sCmd.Connection = sCon;

            sCmd.CommandText = "SP_GET_TYPE";
            sCmd.CommandType = CommandType.StoredProcedure;
            sCmd.Parameters.Clear();

            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_ID", TypeId);
            sCmd.Parameters.AddWithValue("@PERMIT_TYPE_OPTION_SERVICE_ID", TypeServiceId);
            sCmd.Parameters.AddWithValue("@MODE", "GET_RTA_TYPE");
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sCmd);
            da.Fill(ds);


            DataTable dt1 = new DataTable();
            dt1 = ds.Tables[0];
            //if (dt1.Rows.Count > 0)
            //{
            //    Page.Title = dt1.Rows[0]["PERMIT_TYPE"].ToString() + " | Apply for Permit (Permit)";
            //    PageHeading.InnerText = dt1.Rows[0]["PERMIT_TYPE"].ToString();

            //    PageHeading1.InnerText = dt1.Rows[0]["PERMIT_TYPE"].ToString() + " | Apply for Permit (Permit)";

            //}
            return dt1;




        }
    }
}