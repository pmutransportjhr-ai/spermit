using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class STATE_MSTR_HDR
{
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
    
    private List<STATE_MSTR_LDR> _lstSTATE_MSTR_LDR = new List<STATE_MSTR_LDR>();
    public List<STATE_MSTR_LDR> STATE_MSTR_LDR
    {
        get
        {
            return _lstSTATE_MSTR_LDR;
        }
        set
        {
            _lstSTATE_MSTR_LDR = value;
        }
    }
    public void AddSTATE_MSTR_LDR(STATE_MSTR_LDR o)
    {

        _lstSTATE_MSTR_LDR.Add(o);
    }	
}