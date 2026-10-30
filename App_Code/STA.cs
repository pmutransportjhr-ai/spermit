using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices
{
    public class STA
    {
        public int APP_ID { get; set; }
        public string PERMIT_TYPE { get; set; }
        public int ROUTE_ID { get; set; }
        public string REGISTRATION_NUMBER { get; set; }
        public string OWNER_NAME { get; set; }
        public string CARE_OF { get; set; }
        public string PRESENT_ADDRESS { get; set; }
        public string PERMANENT_ADDRESS { get; set; }
        public string VEHICLE_CLASS_TYPE { get; set; }
        public string CHASIS_NUMBER { get; set; }
        public string ENGINE_NUMBER { get; set; }
        public string MAKER_DESC { get; set; }
        public string MAKER_MODEL { get; set; }
        public string FITNESS_VALID_UPTO { get; set; }
        public string TAX_PAID_UPTO { get; set; }
        public string SEATING_CAPACITY { get; set; }
        public string MANUFACTURING_ON { get; set; }
        public string VEHICLE_REGISTERED_AT { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string EMAIL_ID { get; set; }
        public string GENDER { get; set; }
        public string APPLICANT_QUALIFICATION { get; set; }
        public string APPLICANT_CATEGORY { get; set; }
        public string EX_ARMY { get; set; }
        public string MINORITY { get; set; }
        public string HANDICAPPED { get; set; }
        public string HEAVY_MOTOR_LICENCE { get; set; }
        public string IDENTITY_PROOF { get; set; }
        public string AIR_CONDITIONED { get; set; }
        public string DELUXE { get; set; }
        public string NO_OF_TRIPS { get; set; }
        public string IDENTITY_PROOF_NO { get; set; }
        public string PAYMENT_STATUS { get; set; }
        public string APPLICATION_STATUS { get; set; }
        public string ROUTE { get; set; }
        public string APPLICATION_NUMBER { get; set; }
        public string STATUS { get; set; }
        public List<STA_ROUTE> route { get; set; }
      
    }
    public class STA_ROUTE
    {
        public string state_cd { get; set; }
        public int off_cd { get; set; }
        public string appl_no { get; set; }
        public string regn_no { get; set; }
        public int journey_day { get; set; }
        public string route_cd { get; set; }
        public int sr_no { get; set; }
        public int trip { get; set; }
        public string stoppage { get; set; }
        public string arrival_time { get; set; }
        public string departure_time { get; set; }
        public string halt { get; set; }
        
        
    }
    
}