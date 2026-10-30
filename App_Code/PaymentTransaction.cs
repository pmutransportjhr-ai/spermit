using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Globalization;


public class PaymentTransaction
{
    public static String AddTransactionDetails(String APP_ID, String APPLICATION_NUMBER, String APPLICANT_NAME,
    Decimal AMOUNT, String OPERATION_FOR, String TRN_STATUS, String TRN_MESSAGE, String TRN_REF_NO,
    String PAY_MODE, String TRANSACTION_ON, String TXN_DATE, String TXN_AMOUNT, String CHALLAN_URL, String GRN, String CIN, String DEPTID, String RECIEPTHEADCODE,
    String DEPTTRANID, String PANNO, String TR_TYPE, String TREASCODE, String IFMSOFFICECODE, String ADDINFO5, String PERIOD, String TBL_TYPE, String tbl_status="BOOKED")
    {
        try
        {



            SqlParameter[] parameters = new SqlParameter[26];
            parameters[0] = new SqlParameter("@APP_ID", APP_ID);
            parameters[1] = new SqlParameter("@APPLICATION_NUMBER", APPLICATION_NUMBER);
            parameters[2] = new SqlParameter("@APPLICANT_NAME", APPLICANT_NAME);
            parameters[3] = new SqlParameter("@AMOUNT", AMOUNT);
            parameters[4] = new SqlParameter("@OPERATION_FOR", OPERATION_FOR);
            parameters[5] = new SqlParameter("@TRN_STATUS", TRN_STATUS);
            parameters[6] = new SqlParameter("@TRN_MESSAGE", TRN_MESSAGE);
            parameters[7] = new SqlParameter("@TRN_REF_NO", TRN_REF_NO);
            parameters[8] = new SqlParameter("@PAY_MODE", PAY_MODE);
            parameters[9] = new SqlParameter("@TRANSACTION_ON", DBNull.Value);
            parameters[10] = new SqlParameter("@TXN_DATE", TXN_DATE);
            parameters[11] = new SqlParameter("@TXN_AMOUNT", TXN_AMOUNT);
            parameters[12] = new SqlParameter("@CHALLAN_URL", CHALLAN_URL);
            parameters[13] = new SqlParameter("@GRN", GRN);
            parameters[14] = new SqlParameter("@CIN", CIN);

            parameters[15] = new SqlParameter("@DEPTID", DEPTID);
            parameters[16] = new SqlParameter("@RECIEPTHEADCODE", RECIEPTHEADCODE);
            parameters[17] = new SqlParameter("@DEPTTRANID", DEPTTRANID);
            parameters[18] = new SqlParameter("@PANNO", PANNO);
            parameters[19] = new SqlParameter("@TR_TYPE", TR_TYPE);
            parameters[20] = new SqlParameter("@TREASCODE", TREASCODE);
            parameters[21] = new SqlParameter("@IFMSOFFICECODE", IFMSOFFICECODE);
            parameters[22] = new SqlParameter("@ADDINFO5", ADDINFO5);
            parameters[23] = new SqlParameter("@PERIOD", PERIOD);
            parameters[24] = new SqlParameter("@TBL_TYPE", TBL_TYPE);
          //  parameters[24] = new SqlParameter("@TBL_status", tbl_status);

            if (TRN_STATUS == "BOOKED" || TRN_STATUS == "FAIL")
            {
                parameters[25] = new SqlParameter("@MODE", "FAIL_BOOKED");
            }
            else
            {
                parameters[25] = new SqlParameter("@MODE", tbl_status=="BOOKED" ? "ADD" :"REPRINT");
            }
            var obj = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PAYMENT_TRANSACTIONS_HISTORY_AED", parameters);


            return obj.ToString();
        }

        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}