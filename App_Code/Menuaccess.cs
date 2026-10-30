using System;
using System.Collections.Generic;
using System.Web;


public class Menuaccess
{
    private String _MENU_ID;
    public String MENU_ID
    {
        get
        {
            return _MENU_ID;
        }
        set
        {
            _MENU_ID = value;
        }
    }
    private String _READ_ACCESS;
    public String READ_ACCESS
    {
        get
        {
            return _READ_ACCESS;
        }
        set
        {
            _READ_ACCESS = value;
        }
    }
    private String _WRITE_ACCESS;
    public String WRITE_ACCESS
    {
        get
        {
            return _WRITE_ACCESS;
        }
        set
        {
            _WRITE_ACCESS = value;
        }
    }
    private List<Menuaccess> _lstMenuaccess = new List<Menuaccess>();
    public List<Menuaccess> MenuaccessM
    {
        get
        {
            return _lstMenuaccess;
        }
        set
        {
            _lstMenuaccess = value;
        }
    }
}