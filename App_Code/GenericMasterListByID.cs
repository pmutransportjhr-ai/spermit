using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;

public class GenericMasterListByID
{
    public static DataTable GetMasterList(String ID, String EMPLOYYEID, String MODE)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@ID", ID);
            parameters[1] = new SqlParameter("@EMPLOYEE_ID", EMPLOYYEID);
            parameters[2] = new SqlParameter("@MODE", MODE);
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST_BYID", parameters);
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
    public static DataSet GetMasterList1(String ID, String EMPLOYYEID, String MODE)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@ID", ID);
            parameters[1] = new SqlParameter("@EMPLOYEE_ID", EMPLOYYEID);
            parameters[2] = new SqlParameter("@MODE", MODE);
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MSTR_LIST_BYID", parameters);

            return ds;
        }
        catch (Exception ex)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("MSG", typeof(string));
            dt.Rows.Add(Convert.ToString(ex.Message));
            ds.Tables.Add(dt);
            return ds;
        }
    }
    public static SqlDataReader GetMasterReaderList(String ID, String EMPLOYYEID, String MODE)
    {

        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter("@ID", ID);
        parameters[1] = new SqlParameter("@EMPLOYEE_ID", EMPLOYYEID);
        parameters[2] = new SqlParameter("@MODE", MODE);
        SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_MASTER_LIST_BYID", parameters);
        return dr;


    }
    
}