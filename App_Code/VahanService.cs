using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

public class VahanService
{

    public string rc_regn_no { get; set; }
    public string rc_catg { get; set; }
    public string status { get; set; }

    public string rc_eng_no { get; set; }
    public string rc_color { get; set; }
    public string rc_rlw { get; set; }
    public string rc_status_as_on { get; set; }
    public string rc_permanent_address { get; set; }
    public string rc_registered_at { get; set; }
    public string rc_ulw { get; set; }
    public string rc_np_upto { get; set; }
    public string rc_present_address { get; set; }
    public string rc_insurance_upto { get; set; }
    public string rc_vh_class_desc { get; set; }
    public string rc_insurance_comp { get; set; }
    public string rc_tax_upto { get; set; }
    public string rc_f_name { get; set; }
    public string rc_insurance_policy_no { get; set; }
    public string rc_fuel_desc { get; set; }
    public string rc_financer { get; set; }
    public string rc_chasi_no { get; set; }
    public string rc_maker_model { get; set; }
    public string rc_maker_desc { get; set; }
    public string rc_owner_name { get; set; }
    public string rc_manufacturing_on { get; set; }
    public string rc_norms_desc { get; set; }
    public string rc_seat_capacity { get; set; }
    public string rc_fit_upto { get; set; }
}

public class VahanResponse
{

    public static VahanService GetVahanResponse(string RegNo)
    {
        VahanService objVResponse = new VahanService();
        try
        { 
        var url = "http://10.30.11.198/Vahan4API/api/get/spermit/nic@123/" + RegNo;
        var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
        string result = String.Empty;
        using (var response = webrequest.GetResponse())
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            result = reader.ReadToEnd();
           
        }
	
//result  = result .Replace(@"""", @"\""");
//	result = (result).Replace("\"", "");
   
        objVResponse = new JavaScriptSerializer().Deserialize<VahanService>(result);

        return objVResponse;

            
        }
        catch (Exception EX)
        {
//            string result = @"{'rc_catg':'TRANSPORT','rc_regn_no':'JH01AD7879','rc_eng_no':'KC12EBAGB05302',
//                           'rc_color':'BLACK','rc_rlw':'45','rc_status_as_on':'23/02/2010','rc_permanent_address':'EOOM NO-228 NIC 2ND FLOOR-ENG HOSTEL -2 DHURWA-RANCHI-364-999999',
//                           'rc_registered_at':'1','rc_ulw':'143','rc_np_upto':'NA','rc_present_address':'EOOM NO-228 NIC 2ND FLOOR-ENG HOSTEL -2 DHURWA-RANCHI-364-999999',
//                           'rc_insurance_upto':'30/03/2017','rc_vh_class_desc':'BUS','rc_insurance_comp':'NATIONAL INSURANCE CO LTD.',
//                           'rc_tax_upto':'23\/02\/2010','rc_f_name':'S/O SRI LAKSHMAN PRASAD','rc_insurance_policy_no':'90176','rc_fuel_desc':'PETROL','rc_financer':null,'rc_chasi_no':'MBLKC12ECAGB05639','rc_maker_model':'CBZ X TREME',
//                           'rc_maker_desc':'HERO HONDA MOTORS  LTD','rc_owner_name':'MR. PUNIT VERMA','rc_manufacturing_on':'2/2010','rc_norms_desc':'Not Available','rc_seat_capacity':'42',
//                           'status':'OK','rc_fit_upto':'16/02/2025'}";
//            //string strresponse = new JavaScriptSerializer().Serialize(result);
//            result = (result).Replace("\\", "");
            //objVResponse = new JavaScriptSerializer().Deserialize<VahanService>(result);
            objVResponse = null;
// objVResponse = new VahanService();
//objVResponse.status=result ;
  //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + EX + "');window.location ='RTA-DASHBOARD.aspx'", true);

            return objVResponse  ;
        }
        

    }



}



