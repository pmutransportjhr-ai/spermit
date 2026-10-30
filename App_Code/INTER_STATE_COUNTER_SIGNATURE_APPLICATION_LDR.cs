using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class INTER_STATE_COUNTER_SIGNATURE_APPLICATION_LDR
{

    private String _ROUTE_ID;
    public String ROUTE_ID
    {
        get
        {
            return _ROUTE_ID;
        }
        set
        {
            _ROUTE_ID = value;
        }
    }
    private String _ROUTE_DETAIL_ID;
    public String ROUTE_DETAIL_ID
    {
        get
        {
            return _ROUTE_DETAIL_ID;
        }
        set
        {
            _ROUTE_DETAIL_ID = value;
        }
    }
    private String _VIA_CITY;
    public String VIA_CITY
    {
        get
        {
            return _VIA_CITY;
        }
        set
        {
            _VIA_CITY = value;
        }
    }
    private String _MODE;
    public String MODE
    {
        get
        {
            return _MODE;
        }
        set
        {
            _MODE = value;
        }
    }
    private String _ARRIVAL_TIMING;
    public String ARRIVAL_TIMING
    {
        get
        {
            return _ARRIVAL_TIMING;
        }
        set
        {
            _ARRIVAL_TIMING = value;
        }
    }
    private String _DEPARTURE_TIMING;
    public String DEPARTURE_TIMING
    {
        get
        {
            return _DEPARTURE_TIMING;
        }
        set
        {
            _DEPARTURE_TIMING = value;
        }
    }
    private List<INTER_STATE_COUNTER_SIGNATURE_APPLICATION_LDR> _lstINTER_STATE_COUNTER_SIGNATURE_APPLICATION_LDR = new List<INTER_STATE_COUNTER_SIGNATURE_APPLICATION_LDR>();
    public List<INTER_STATE_COUNTER_SIGNATURE_APPLICATION_LDR> INTER_STATE_COUNTER_SIGNATURE_APPLICATION_LDRs
    {
        get
        {
            return _lstINTER_STATE_COUNTER_SIGNATURE_APPLICATION_LDR;
        }
        set
        {
            _lstINTER_STATE_COUNTER_SIGNATURE_APPLICATION_LDR = value;
        }
    }
}