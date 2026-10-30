using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ROUTE_MSTR_HDR
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
	private String _ST_FROM;
    public String ST_FROM
    {
        get
        {
            return _ST_FROM;
        }
        set
        {
            _ST_FROM = value;
        }
    }
    private String _STATE_CODE;
    public String STATE_CODE
    {
        get
        {
            return _STATE_CODE;
        }
        set
        {
            _STATE_CODE = value;
        }
    }
	private String _ST_TO;
    public String ST_TO
    {
        get
        {
            return _ST_TO;
        }
        set
        {
            _ST_TO = value;
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
    private String _NO_OF_VACANCIES;
    public String NO_OF_VACANCIES
    {
        get
        {
            return _NO_OF_VACANCIES;
        }
        set
        {
            _NO_OF_VACANCIES = value;
        }
    }
    private String _ROUTE_NUMBER;
    public String ROUTE_NUMBER
    {
        get
        {
            return _ROUTE_NUMBER;
        }
        set
        {
            _ROUTE_NUMBER = value;
        }
    }
    private String _MAX_NUMBER_OF_TRIPS;
    public String MAX_NUMBER_OF_TRIPS
    {
        get
        {
            return _MAX_NUMBER_OF_TRIPS;
        }
        set
        {
            _MAX_NUMBER_OF_TRIPS = value;
        }
    }
    private String _STATUS;
    public String STATUS
    {
        get
        {
            return _STATUS;
        }
        set
        {
            _STATUS = value;
        }
    }

    private List<ROUTE_MSTR_LDR> _lstROUTE_MSTR_LDR = new List<ROUTE_MSTR_LDR>();
    public List<ROUTE_MSTR_LDR> ROUTE_MSTR_LDR
    {
        get
        {
            return _lstROUTE_MSTR_LDR;
        }
        set
        {
            _lstROUTE_MSTR_LDR = value;
        }
    }
    public void AddROUTE_MSTR_LDR(ROUTE_MSTR_LDR o)
    {
       
        _lstROUTE_MSTR_LDR.Add(o);
    }

    private List<ROUTE_MSTR_LDR_HDR> _lstROUTE_MSTR_LDR_HDR = new List<ROUTE_MSTR_LDR_HDR>();
    public List<ROUTE_MSTR_LDR_HDR> ROUTE_MSTR_LDR_HDR
    {
        get
        {
            return _lstROUTE_MSTR_LDR_HDR;
        }
        set
        {
            _lstROUTE_MSTR_LDR_HDR = value;
        }
    }
    public void AddROUTE_MSTR_LDR_HDR(ROUTE_MSTR_LDR_HDR o)
    {

        _lstROUTE_MSTR_LDR_HDR.Add(o);
    }
    private List<ROUTE_MSTR_RTO> _lstROUTE_MSTR_RTO = new List<ROUTE_MSTR_RTO>();
    public List<ROUTE_MSTR_RTO> ROUTE_MSTR_RTO
    {
        get
        {
            return _lstROUTE_MSTR_RTO;
        }
        set
        {
            _lstROUTE_MSTR_RTO = value;
        }
    }
    public void AddROUTE_MSTR_RTO(ROUTE_MSTR_RTO o)
    {

        _lstROUTE_MSTR_RTO.Add(o);
    }	


}