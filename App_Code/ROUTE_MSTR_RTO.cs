using System;
using System.Collections.Generic;
using System.Web;

public class ROUTE_MSTR_RTO
{
    private String _RTO_CODE;
    public String RTO_CODE
    {
        get
        {
            return _RTO_CODE;
        }
        set
        {
            _RTO_CODE = value;
        }
    }
    private String _RTO_NAME;
    public String RTO_NAME
    {
        get
        {
            return _RTO_NAME;
        }
        set
        {
            _RTO_NAME = value;
        }
    }
    private String _DISTANCE_COVERED;
    public String DISTANCE_COVERED
    {
        get
        {
            return _DISTANCE_COVERED;
        }
        set
        {
            _DISTANCE_COVERED = value;
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
}