using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class PayGetWayResponseDTO
{
    public static String MakeDecryptedData(String EncryptedVal)
    {
        try
        {
            String Data = EncryptedVal;
            clsEncrypt enc = new clsEncrypt();
            String DecryptedData = enc.Decrypt(Data, "key1234");
            return DecryptedData;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}