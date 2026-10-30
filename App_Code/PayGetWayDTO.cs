using System;


public class PayGetWayDTO
{
    public static String MakeEncryptedData(
        String DEPTID,
        String RECIEPTHEADCODE, 
        String DEPOSITERNAME, 
        String DEPTTRANID, 
        String AMOUNT, 
        String DEPOSITERID, 
        String PANNO, 
        String ADDINFO1, 
        String ADDINFO2, 
        String ADDINFO3, 
        String TREASCODE, 
        String IFMSOFFICECODE,
        String SECURITYCODE
   )
    {
        try
        {
            DEPOSITERNAME = DEPOSITERNAME.Replace("&", "and");
            String Data = "";
            Data = DEPTID + "|" + RECIEPTHEADCODE + "|" + DEPOSITERNAME + "|" + DEPTTRANID + "|" + AMOUNT + "|" + DEPOSITERID + "|" + PANNO + "|" + ADDINFO1 + "|" + ADDINFO2 + "|" + ADDINFO3 + "|" + TREASCODE + "|" + IFMSOFFICECODE + "|" + SECURITYCODE;
            clsEncrypt enc = new clsEncrypt();
            String EncryptedData = enc.Encrypt(Data, "key1234");

            return EncryptedData;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string MakeEncrytedDataforDoubleVerification(string GRN, string DEPID, string DEPTTRANDID, string SECURITYCODE)
    {
        try
        {
            String Data = "";
            Data = GRN + "|" + DEPID + "|" + DEPTTRANDID+ "|" + SECURITYCODE;
            clsEncrypt enc = new clsEncrypt();
            String EncryptedData = enc.Encrypt(Data, "key1234");

            return EncryptedData;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

}