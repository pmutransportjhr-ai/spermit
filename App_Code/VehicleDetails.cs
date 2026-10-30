using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;

   public class VehicleDetails
{
       public VEHICLE_DETAILS GetVehicleDetails(String VNo)
    {
        VEHICLE_DETAILS Vd = new VEHICLE_DETAILS();
        try
        {
            String RNo = CryptorEngine.Decrypt(VNo, true);

            Vd.Owner_Name = "LATA SINGH";
            Vd.Care_Of = "CHANDRA KISHOR SINGH";
            Vd.Address_Line1 = "AT-BARIATU HOUSING COLONY";
            Vd.Address_Line2 = "BARIATU ";
            Vd.City = "RANCHI";
            Vd.Registration_Date = "04/10/2007";
            Vd.Vehicle_Type = "BUS";
            Vd.Chassis_Number = "412073ESZ120527";
            Vd.Engine_Number = "70E62573301";
            Vd.Tax_Paid_UpTo = "03/01/2017";
            Vd.Fitness_Valid_Upto = "01/09/2017";
            Vd.Insurance_Company = "ANY OTHER";
            Vd.Insurance_Upto = "12/09/2017";
            Vd.Reg_No = RNo;
            Vd.Status = "True";
            return Vd;
        }
        catch(Exception ex)
        {
            String Messqge = ex.Message;
            Messqge = Messqge.Replace("\r", " ").Replace("\n", " ");
            Vd.Status = "False";
            Vd.Msg = Messqge;
            return Vd;
        }
    }
	
}