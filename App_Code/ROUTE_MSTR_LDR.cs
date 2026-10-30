using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ROUTE_MSTR_LDR
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
    private String _SEQUENCE;
    public String SEQUENCE
    {
        get
        {
            return _SEQUENCE;
        }
        set
        {
            _SEQUENCE = value;
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
}