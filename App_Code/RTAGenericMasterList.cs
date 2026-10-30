using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;

public class RTAGenericMasterList
{
    public static DataTable GetMasterList(String EMPLOYYEID,String MODE)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@EMPLOYEE_ID", EMPLOYYEID);
            parameters[1] = new SqlParameter("@MODE", MODE);
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", parameters);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            return dt;
        }
        catch(Exception ex)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MSG", typeof(string));
            dt.Rows.Add(Convert.ToString(ex.Message));
            return dt;
        }
    }
    public static SqlDataReader GetMasterReaderList(String EMPLOYYEID, String MODE)
    {
       
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@EMPLOYEE_ID", EMPLOYYEID);
            parameters[1] = new SqlParameter("@MODE", MODE);
            SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MSTR_LIST", parameters);
            return dr;
       
        
    }
}