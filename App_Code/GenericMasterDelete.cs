using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;

public class GenericMasterDelete
{
    public static String DeleteMaster(String ID,String MODE)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@ID", ID);
            parameters[1] = new SqlParameter("@MODE", MODE);
            var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_MASTER_DELETE", parameters);
            
            return obj.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
	
}