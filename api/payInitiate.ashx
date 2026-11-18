<%@ WebHandler Language="C#" Class="payInitiate" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SoftEra.JHT.Infrastructure;
public class payInitiate : IHttpHandler {
    
public void ProcessRequest (HttpContext context) {
       
           String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
           SqlTransaction objTrans = null;
          
         
           using (SqlConnection sCon = new SqlConnection(sConnectionString))
           {
               sCon.Open();
               objTrans = sCon.BeginTransaction();

               SqlCommand sCmd = new SqlCommand();
               sCmd.Transaction = objTrans;
            
               try
               {
                   string str = context.Request.QueryString["q"].ToString();
                   String DecryptedVal1 = PayGetWayResponseDTO.MakeDecryptedData(context.Request.QueryString["q"].ToString());
                   String[] arr1;
                   arr1 = DecryptedVal1.Split('|');
                   if (arr1.Length < 13) throw new InvalidOperationException("Invalid payment request format.");
                                      
                   string DEPTID = arr1[0];
                   string RECIEPTHEADCODE = arr1[1];
                   string DEPOSITERNAME = arr1[2];
                   string DEPTTRANID = arr1[3];
                   decimal AMOUNT = Convert.ToDecimal(arr1[4]);
                   string DEPOSITERID = arr1[5];
                   string PANNO = arr1[6];
                   string ADDINFO1 = arr1[7];
                   string ADDINFO2 = arr1[8];
		   string TR_TYPE = arr1[9];                 
                   string TREASCODE = arr1[10];
                   string IFMSOFFICECODE = arr1[11];
                   string SECURITY_CODE = arr1[12];

                   // Miscellaneous payments already have their own transaction record.
                   // Identify them by DEPTTRANID, while keeping ADDINFO3 as the configured
                   // JEGRAS TR_TYPE required by the established gateway payload contract.
                   DataTable miscTransactions = MiscPaymentService.GetTransaction(DEPTTRANID);
                   if (miscTransactions.Rows.Count > 0)
                   {
                       if (miscTransactions.Rows.Count != 1)
                           throw new InvalidOperationException("Invalid miscellaneous payment transaction.");
                       string configuredSecurityCode = ConfigurationManager.AppSettings["JEGRAS_SECURITY_CODE"];
                       if (string.IsNullOrWhiteSpace(configuredSecurityCode) ||
                           !string.Equals(SECURITY_CODE, configuredSecurityCode, StringComparison.Ordinal))
                           throw new InvalidOperationException("Invalid JEGRAS security configuration.");

                       int miscAppId;
                       decimal miscAmount;
                       if (!int.TryParse(ADDINFO2, out miscAppId) ||
                           !decimal.TryParse(arr1[4], System.Globalization.NumberStyles.Number,
                               System.Globalization.CultureInfo.InvariantCulture, out miscAmount))
                           throw new InvalidOperationException("Invalid miscellaneous payment reference or amount.");

                       DataRow miscStored = miscTransactions.Rows[0];

                       if (!string.Equals(Convert.ToString(miscStored["TRN_STATUS"]), "REQUESTED", StringComparison.Ordinal) ||
                           miscAppId != Convert.ToInt32(miscStored["APP_ID"]) ||
                           !string.Equals(DEPOSITERID, Convert.ToString(miscStored["APPLICATION_NUMBER"]), StringComparison.Ordinal) ||
                           miscAmount != Convert.ToDecimal(miscStored["AMOUNT"]) ||
                           !string.Equals(ADDINFO1, Convert.ToString(miscStored["OPERATION_FOR"]), StringComparison.Ordinal) ||
                           !string.Equals(DEPOSITERNAME, Convert.ToString(miscStored["APPLICANT_NAME"]).Replace("&", "and"), StringComparison.Ordinal) ||
                           !string.Equals(TR_TYPE, Convert.ToString(miscStored["TR_TYPE"]), StringComparison.Ordinal) ||
                           !string.Equals(DEPTID, Convert.ToString(miscStored["CFG_DEPTID"]), StringComparison.Ordinal) ||
                           !string.Equals(RECIEPTHEADCODE, Convert.ToString(miscStored["CFG_RECEIPTHEADCODE"]), StringComparison.Ordinal) ||
                           !string.Equals(TREASCODE, Convert.ToString(miscStored["CFG_TREASCODE"]), StringComparison.Ordinal) ||
                           !string.Equals(IFMSOFFICECODE, Convert.ToString(miscStored["CFG_IFMSOFFICECODE"]), StringComparison.Ordinal))
                           throw new InvalidOperationException("Miscellaneous payment request does not match the stored transaction.");

                       objTrans.Commit();
                       context.Response.Write("Success");
                       return;
                   }
                   SqlCommand scmd = new SqlCommand("SP_PAYMENT_TRANSACTIONS_HISTORY_AED");
                   scmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(ADDINFO2));
                   scmd.Parameters.AddWithValue("@APPLICATION_NUMBER", DEPOSITERID);
                   scmd.Parameters.AddWithValue("@APPLICANT_NAME", DEPOSITERNAME);
                   scmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);
                   scmd.Parameters.AddWithValue("@OPERATION_FOR", ADDINFO1);
                   scmd.Parameters.AddWithValue("@DEPTID", DEPTID);
                   scmd.Parameters.AddWithValue("@DEPTTRANID", DEPTTRANID);
                   scmd.Parameters.AddWithValue("@TREASCODE", TREASCODE);
                   scmd.Parameters.AddWithValue("@IFMSOFFICECODE", IFMSOFFICECODE);
                   scmd.Parameters.AddWithValue("@SECURITY_CODE", SECURITY_CODE);
                   scmd.Parameters.AddWithValue("@TR_TYPE", TR_TYPE);
                   scmd.Parameters.AddWithValue("@MODE", "ADD_BEFORE_TRANS");
                   scmd.CommandType = CommandType.StoredProcedure;
                   SqlConnection con = new SqlConnection(SQLHelper.GetConnString());
                   scmd.Connection = con;
                   SqlDataAdapter da = new SqlDataAdapter(scmd);
                   DataSet ds1 = new DataSet();
                   da.Fill(ds1);

                   
                   
                   objTrans.Commit();
                   context.Response.Write("Success");
               }
               catch (Exception ex)
               {
                   objTrans.Dispose();
                   context.Response.Write(ex.Message);
               }
           }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}