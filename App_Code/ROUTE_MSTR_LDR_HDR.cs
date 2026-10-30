using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ROUTE_MSTR_LDR_HDR
{
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
    private String _TOTAL_DISTANCE;
    public String TOTAL_DISTANCE
    {
        get
        {
            return _TOTAL_DISTANCE;
        }
        set
        {
            _TOTAL_DISTANCE = value;
        }
    }
    private String _DISTANCE_IN_HOME_STATE;
    public String DISTANCE_IN_HOME_STATE
    {
        get
        {
            return _DISTANCE_IN_HOME_STATE;
        }
        set
        {
            _DISTANCE_IN_HOME_STATE = value;
        }
    }
    private String _ROUTE;
    public String ROUTE
    {
        get
        {
            return _ROUTE;
        }
        set
        {
            _ROUTE = value;
        }
    }
}