using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;


public class ValidateSession
{
	public static String ValidateUserSession(String Employee_ID,String IP,String USER_TYPE,String Browser)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@EMP_ID", Employee_ID);
            parameters[1] = new SqlParameter("@IP", IP);
            parameters[2] = new SqlParameter("@USER_TYPE", USER_TYPE);
            parameters[3] = new SqlParameter("@BROWSER", Browser);
            parameters[4] = new SqlParameter("@MODE", "SAVE");
            var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SESSION_HISTORY_AED", parameters);
            return Convert.ToString(obj);
        }
        catch(Exception ex)
        {
            return ex.Message.ToString();
        }
    }
    public static String DeleteUserSession(String Employee_ID, String USER_TYPE, String Browser)
    {
        SqlParameter[] parameters = new SqlParameter[4];
        parameters[0] = new SqlParameter("@EMP_ID", Employee_ID);
        parameters[1] = new SqlParameter("@USER_TYPE", USER_TYPE);
        parameters[2] = new SqlParameter("@BROWSER", Browser);
        parameters[3] = new SqlParameter("@MODE", "DELETE");
        var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SESSION_HISTORY_AED", parameters);
        return Convert.ToString(obj);
    }
}