using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices
{
    public class CounterSignature
    {
        public int APP_ID {get; set; }
        public int PERMIT_TYPE_ID {get; set; }
        public int PERMIT_TYPE_OPTION_ID {get; set; }
        public int PERMIT_TYPE_OPTION_SERVICE_ID {get; set; }
        public string OWNER_NAME {get; set;}
        public string CARE_OF {get; set;}
        public string PRESENT_ADDRESS {get; set;}
        public string PERMANENT_ADDRESS {get; set;}
        public string PERMIT_NUMBER {get; set;}
        public string ISSUED_BY_STATE {get; set;}
        public string PERMIT_VALID_FROM {get; set;}
        public string PERMIT_VALID_UPTO {get; set;}
        public int ROUTE_ID {get; set;}
        public string ROUTE {get; set;}
        public string REGISTRATION_NUMBER {get; set;}
        public string VEHICLE_CLASS_TYPE {get; set;}
        public string CHASIS_NUMBER {get; set;}
        public string ENGINE_NUMBER {get; set;}
        public string MAKER_DESC {get; set;}
        public string MAKER_MODEL {get; set;}
        public string MANUFACTURING_ON {get; set;}
        public string TAX_PAID_UPTO {get; set;}
        public string IC_VALID_UPTO {get; set;}
        public string FC_VALID_UPTO {get; set;}
        public string SEATING_CAPACITY {get; set;}
        public string PAYMENT_STATUS {get; set;}
        public string MOBILE_NUMBER {get; set;}
        public string EMAIL_ID {get; set;}
        public string APPLICATION_NUMBER {get; set;}
        public string APPLICATION_STATUS {get; set;}
        public string STATUS { get; set; }
        public List<CS_ROUTE> route { get; set; }
	
    }

    public class CS_ROUTE
    {
        public int APP_ID { get; set; }
        public int SEQUENCE { get; set; }
        public string VIA_CITY { get; set; }
        public string ARRIVAL_TIMING { get; set; }
        public string DEPARTURE_TIMING { get; set; }
        public string MODE { get; set; }
        public int ID { get; set; }
    }
}