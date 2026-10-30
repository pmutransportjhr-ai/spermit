using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class STATE_MSTR_LDR
{
   
    private String _KM_UPTO;
    public String KM_UPTO
    {
        get
        {
            return _KM_UPTO;
        }
        set
        {
            _KM_UPTO = value;
        }
    }
    private String _NUMBER_OF_ROUND_TRIP;
    public String NUMBER_OF_ROUND_TRIP
    {
        get
        {
            return _NUMBER_OF_ROUND_TRIP;
        }
        set
        {
            _NUMBER_OF_ROUND_TRIP = value;
        }
    }
    
}