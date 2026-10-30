using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Script.Serialization;


namespace WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]

    public class STAPARIVAHAN : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetSTADetails(string reg_no)
        {
            STA stadetail = new STA();
            stadetail.STATUS = "ERROR";
            string app_no = "";
            stadetail.APPLICATION_NUMBER = app_no;
            STA_ROUTE staroute = new STA_ROUTE();
            JavaScriptSerializer js = new JavaScriptSerializer();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            List<STA_ROUTE> list = new List<STA_ROUTE>();
            string cs = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                da = new SqlDataAdapter("SpermitWebService", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@MODE", "STA");
                da.SelectCommand.Parameters.AddWithValue("@REG_NO", reg_no);
                da.Fill(ds);
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                dt = ds.Tables[0];
               
                   int i = dt.Rows.Count;
                    for (int j = 0; j < i; j++)
                    {
                        staroute = new STA_ROUTE();
                        staroute.state_cd = dt.Rows[j]["state_cd"].ToString();
                        staroute.off_cd = Convert.ToInt32(dt.Rows[j]["off_cd"]);
                        staroute.appl_no = dt.Rows[j]["appl_no"].ToString();
                        staroute.regn_no = dt.Rows[j]["regn_no"].ToString();
                        staroute.journey_day = Convert.ToInt32(dt.Rows[j]["journey_day"]);
                        staroute.route_cd = dt.Rows[j]["route_cd"].ToString();
                        staroute.sr_no = j+1;
                        staroute.trip = Convert.ToInt32(dt.Rows[j]["trip"]);
                        staroute.stoppage = dt.Rows[j]["stoppage"].ToString();
                        staroute.arrival_time = dt.Rows[j]["arrival_time"].ToString();
                        staroute.departure_time = dt.Rows[j]["departure_time"].ToString();
                        staroute.halt = dt.Rows[j]["halt"].ToString();
                        
                        list.Add(staroute);
                        //Context.Response.Write(js.Serialize(staroute));
                    }
                   
                }

                Context.Response.Write(js.Serialize(list));

            }

        [WebMethod]
        public void GetSTACSDetails(string app_no, string reg_no)
        {
            CounterSignature stacsdetail = new CounterSignature();
            stacsdetail.STATUS = "ERROR";
            stacsdetail.APPLICATION_NUMBER = app_no;
            CS_ROUTE csroute = new CS_ROUTE();
            JavaScriptSerializer jscs = new JavaScriptSerializer();
            SqlDataAdapter dacs = new SqlDataAdapter();
            DataSet dscs = new DataSet();
            List<CS_ROUTE> listcs = new List<CS_ROUTE>();

            string stacs = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            using (SqlConnection con = new SqlConnection(stacs))
            {
                con.Open();
                dacs = new SqlDataAdapter("SpermitWebService", con);
                dacs.SelectCommand.CommandType = CommandType.StoredProcedure;
                dacs.SelectCommand.Parameters.AddWithValue("@MODE", "CS");
                dacs.SelectCommand.Parameters.AddWithValue("@APP_NO", app_no);
                dacs.SelectCommand.Parameters.AddWithValue("@REG_NO", reg_no);
                dacs.Fill(dscs);
                DataTable dtcs1 = new DataTable();
                DataTable dtcs2 = new DataTable();
                dtcs1 = dscs.Tables[0];
                dtcs2 = dscs.Tables[1];
                if (dtcs1.Rows.Count != 0)
                {
                    int x = 0;
                    int y = 0;
                    //CounterSignature staCSdetail = new CounterSignature();
                    stacsdetail.APP_ID = Convert.ToInt32(dtcs1.Rows[0]["APP_ID"]);
                    stacsdetail.PERMIT_TYPE_ID = Convert.ToInt32(dtcs1.Rows[0]["PERMIT_TYPE_ID"]);
                    stacsdetail.PERMIT_TYPE_OPTION_ID = Convert.ToInt32(dtcs1.Rows[0]["PERMIT_TYPE_OPTION_ID"]);
                    stacsdetail.PERMIT_TYPE_OPTION_SERVICE_ID = Convert.ToInt32(dtcs1.Rows[0]["PERMIT_TYPE_OPTION_SERVICE_ID"]);
                    stacsdetail.OWNER_NAME = dtcs1.Rows[0]["OWNER_NAME"].ToString();
                    stacsdetail.CARE_OF = dtcs1.Rows[0]["CARE_OF"].ToString();
                    stacsdetail.PRESENT_ADDRESS = dtcs1.Rows[0]["PRESENT_ADDRESS"].ToString();
                    stacsdetail.PERMANENT_ADDRESS = dtcs1.Rows[0]["PERMANENT_ADDRESS"].ToString();
                    stacsdetail.PERMIT_NUMBER = dtcs1.Rows[0]["PERMIT_NUMBER"].ToString();
                    stacsdetail.ISSUED_BY_STATE = dtcs1.Rows[0]["ISSUED_BY_STATE"].ToString();
                    stacsdetail.PERMIT_VALID_FROM = dtcs1.Rows[0]["PERMIT_VALID_FROM"].ToString();
                    stacsdetail.PERMIT_VALID_UPTO = dtcs1.Rows[0]["PERMIT_VALID_UPTO"].ToString();
                    stacsdetail.ROUTE_ID = Convert.ToInt32(dtcs1.Rows[0]["ROUTE_ID"]);
                    stacsdetail.REGISTRATION_NUMBER = dtcs1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    stacsdetail.VEHICLE_CLASS_TYPE = dtcs1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                    stacsdetail.CHASIS_NUMBER = dtcs1.Rows[0]["CHASIS_NUMBER"].ToString();
                    stacsdetail.ENGINE_NUMBER = dtcs1.Rows[0]["ENGINE_NUMBER"].ToString();
                    stacsdetail.MAKER_DESC = dtcs1.Rows[0]["MAKER_DESC"].ToString();
                    stacsdetail.MAKER_MODEL = dtcs1.Rows[0]["MAKER_MODEL"].ToString();
                    stacsdetail.MANUFACTURING_ON = dtcs1.Rows[0]["MANUFACTURING_ON"].ToString();
                    stacsdetail.TAX_PAID_UPTO = dtcs1.Rows[0]["TAX_PAID_UPTO"].ToString();
                    stacsdetail.IC_VALID_UPTO = dtcs1.Rows[0]["IC_VALID_UPTO"].ToString();
                    stacsdetail.FC_VALID_UPTO = dtcs1.Rows[0]["FC_VALID_UPTO"].ToString();
                    stacsdetail.SEATING_CAPACITY = dtcs1.Rows[0]["SEATING_CAPACITY"].ToString();
                    stacsdetail.PAYMENT_STATUS = dtcs1.Rows[0]["PAYMENT_STATUS"].ToString();
                    stacsdetail.MOBILE_NUMBER = dtcs1.Rows[0]["MOBILE_NUMBER"].ToString();
                    stacsdetail.EMAIL_ID = dtcs1.Rows[0]["EMAIL_ID"].ToString();
                    stacsdetail.APPLICATION_STATUS = dtcs1.Rows[0]["APPLICATION_STATUS"].ToString();
                    stacsdetail.APPLICATION_NUMBER = dtcs1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    //List_STA_CS_HDR.Add(staCSdetail);
                    // Context.Response.Write(jscs.Serialize(stacsdetail));
                    x = dtcs2.Rows.Count;
                    for (y = 0; y < x; y++)
                    {
                        csroute = new CS_ROUTE();

                        csroute.APP_ID = Convert.ToInt32(dtcs2.Rows[y]["APP_ID"]);
                        csroute.SEQUENCE = Convert.ToInt32(dtcs2.Rows[y]["SEQUENCE"]);
                        csroute.VIA_CITY = dtcs2.Rows[y]["VIA_CITY"].ToString();
                        csroute.ARRIVAL_TIMING = dtcs2.Rows[y]["ARRIVAL_TIMING"].ToString();
                        csroute.DEPARTURE_TIMING = dtcs2.Rows[y]["DEPARTURE_TIMING"].ToString();
                        csroute.MODE = dtcs2.Rows[y]["MODE"].ToString();
                        csroute.ID = Convert.ToInt32(dtcs2.Rows[y]["ID"]);
                        listcs.Add(csroute);
                        //Context.Response.Write(jscs.Serialize(csroute));
                    }
                }
                stacsdetail.route = listcs;
                Context.Response.Write(jscs.Serialize(stacsdetail));
                //JavaScriptSerializer js = new JavaScriptSerializer();
            }

        }

         [WebMethod]
        public void GetRTADetails(string app_no, string reg_no)
        {
            RTA rtadetail = new RTA();
            rtadetail.STATUS = "ERROR";
            rtadetail.APPLICATION_NUMBER = app_no;
            RTA_ROUTE rtaroute = new RTA_ROUTE();
            JavaScriptSerializer js1 = new JavaScriptSerializer();
            SqlDataAdapter da1 = new SqlDataAdapter();
            DataSet ds1 = new DataSet();
            List<RTA_ROUTE> listRTA = new List<RTA_ROUTE>();
            string csrta = ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csrta))
            {
                con.Open();
                da1 = new SqlDataAdapter("SpermitWebService", con);
                da1.SelectCommand.CommandType = CommandType.StoredProcedure;
                da1.SelectCommand.Parameters.AddWithValue("@MODE", "RTA");
                da1.SelectCommand.Parameters.AddWithValue("@APP_NO", app_no);
                da1.SelectCommand.Parameters.AddWithValue("@REG_NO", reg_no);
                da1.Fill(ds1);
                DataTable dtrta1 = new DataTable();
                DataTable dtrta2 = new DataTable();
                dtrta1 = ds1.Tables[0];
                dtrta2 = ds1.Tables[1];

                if (dtrta1.Rows.Count != 0)
                {
                    int a = 0;
                    int b = 0;
                    //RTA rtadetail = new RTA();
                    rtadetail.APP_ID = Convert.ToInt32(dtrta1.Rows[0]["APP_ID"]);
                    //rtadetail.PERMIT_TYPE = dtrta1.Rows[0]["PERMIT_TYPE"].ToString();
                    rtadetail.ROUTE_ID = Convert.ToInt32(dtrta1.Rows[0]["ROUTE_ID"]);
                    rtadetail.REGISTRATION_NUMBER = dtrta1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    rtadetail.OWNER_NAME = dtrta1.Rows[0]["OWNER_NAME"].ToString();
                    rtadetail.CARE_OF = dtrta1.Rows[0]["CARE_OF"].ToString();
                    rtadetail.PRESENT_ADDRESS = dtrta1.Rows[0]["PRESENT_ADDRESS"].ToString();
                    rtadetail.PERMANENT_ADDRESS = dtrta1.Rows[0]["PERMANENT_ADDRESS"].ToString();
                    rtadetail.VEHICLE_CLASS_TYPE = dtrta1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                    rtadetail.CHASIS_NUMBER = dtrta1.Rows[0]["CHASIS_NUMBER"].ToString();
                    rtadetail.ENGINE_NUMBER = dtrta1.Rows[0]["ENGINE_NUMBER"].ToString();
                    rtadetail.MAKER_DESC = dtrta1.Rows[0]["MAKER_DESC"].ToString();
                    rtadetail.MAKER_MODEL = dtrta1.Rows[0]["MAKER_MODEL"].ToString();
                    rtadetail.FITNESS_VALID_UPTO = dtrta1.Rows[0]["FITNESS_VALID_UPTO"].ToString();
                    rtadetail.TAX_PAID_UPTO = dtrta1.Rows[0]["TAX_PAID_UPTO"].ToString();
                    rtadetail.SEATING_CAPACITY = dtrta1.Rows[0]["SEATING_CAPACITY"].ToString();
                    rtadetail.MANUFACTURING_ON = dtrta1.Rows[0]["MANUFACTURING_ON"].ToString();
                    rtadetail.VEHICLE_REGISTERED_AT = dtrta1.Rows[0]["VEHICLE_REGISTERED_AT"].ToString();
                    rtadetail.MOBILE_NUMBER = dtrta1.Rows[0]["MOBILE_NUMBER"].ToString();
                    rtadetail.EMAIL_ID = dtrta1.Rows[0]["EMAIL_ID"].ToString();
                    rtadetail.GENDER = dtrta1.Rows[0]["GENDER"].ToString();
                    rtadetail.APPLICANT_QUALIFICATION = dtrta1.Rows[0]["APPLICANT_QUALIFICATION"].ToString();
                    rtadetail.APPLICANT_CATEGORY = dtrta1.Rows[0]["APPLICANT_CATEGORY"].ToString();
                    rtadetail.EX_ARMY = dtrta1.Rows[0]["EX_ARMY"].ToString();
                    rtadetail.MINORITY = dtrta1.Rows[0]["MINORITY"].ToString();
                    rtadetail.HANDICAPPED = dtrta1.Rows[0]["HANDICAPPED"].ToString();
                    rtadetail.HEAVY_MOTOR_LICENCE = dtrta1.Rows[0]["HEAVY_MOTOR_LICENCE"].ToString();
                    rtadetail.IDENTITY_PROOF = dtrta1.Rows[0]["IDENTITY_PROOF"].ToString();
                    rtadetail.AIR_CONDITIONED = dtrta1.Rows[0]["AIR_CONDITIONED"].ToString();
                    rtadetail.DELUXE = dtrta1.Rows[0]["DELUXE"].ToString();
                    rtadetail.NO_OF_TRIPS = dtrta1.Rows[0]["NO_OF_TRIPS"].ToString();
                    rtadetail.IDENTITY_PROOF_NO = dtrta1.Rows[0]["IDENTITY_PROOF_NO"].ToString();
                    rtadetail.PAYMENT_STATUS = dtrta1.Rows[0]["PAYMENT_STATUS"].ToString();
                    rtadetail.APPLICATION_STATUS = dtrta1.Rows[0]["APPLICATION_STATUS"].ToString();
                    rtadetail.ROUTE = dtrta1.Rows[0]["ROUTE"].ToString();
                    rtadetail.APPLICATION_NUMBER = dtrta1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    //Context.Response.Write(js1.Serialize(rtadetail));
                    //List_RTA_HDR.Add(rtadetail);
                    a = dtrta2.Rows.Count;
                    for (b = 0; b < a; b++)
                    {
                        rtaroute = new RTA_ROUTE();
                        rtaroute.APP_ID = Convert.ToInt32(dtrta2.Rows[b]["APP_ID"]);
                        rtaroute.SEQUENCE = Convert.ToInt32(dtrta2.Rows[b]["SEQUENCE"]);
                        rtaroute.VIA_CITY = dtrta2.Rows[b]["VIA_CITY"].ToString();
                        rtaroute.ARRIVAL_TIMING = dtrta2.Rows[b]["ARRIVAL_TIMING"].ToString();
                        rtaroute.DEPARTURE_TIMING = dtrta2.Rows[b]["DEPARTURE_TIMING"].ToString();
                        rtaroute.MODE = dtrta2.Rows[b]["MODE"].ToString();
                        rtaroute.ID = Convert.ToInt32(dtrta2.Rows[b]["ID"]);
                        listRTA.Add(rtaroute);
                        //Context.Response.Write(js1.Serialize(rtaroute));
                    }
                    rtadetail.STATUS = "OK";
                    rtadetail.route = listRTA;
                }
                Context.Response.Write(js1.Serialize(rtadetail));
            }

        }
        }

        

       

        //JavaScriptSerializer js = new JavaScriptSerializer();
        //Context.Response.Write(js.Serialize(List_RTA_HDR));
    }
    
