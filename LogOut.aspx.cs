using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Transactions;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;
using System.Collections;

public partial class LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidateSession.DeleteUserSession(Convert.ToString(Session["EMPLOYEE_ID"]), Convert.ToString(Session["USER_TYPES"]), Convert.ToString(Session["BROWSER"]));
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }
	
}