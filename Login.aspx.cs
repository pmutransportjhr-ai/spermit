using SoftEra.JHT.Infrastructure;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Configuration;


public partial class Login : System.Web.UI.Page
{
    string randumid = "";
    Random random = new Random();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.ViewStateUserKey = Session.SessionID;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtLogin.Focus();
            Session["salt"] = ConfigurationManager.AppSettings["SecurityKey"];
        }
    }

    protected void btnLogin_Click1(object sender, EventArgs e)
    {
        try
        {
            login();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    public void login()
    {
        HttpBrowserCapabilities bc = Request.Browser;
        string LOGIN_ID = txtLogin.Text;
        string PASSWORD = txtPassword.Text.Trim();
        string TYPE = ddlType.SelectedValue;
        string Upass = hfpwd.Value;

        Session["msg"] = "";

        HttpCookie cookie = new HttpCookie("Temp");

        Session["Temp"] = random.Next(59999, 199999).ToString();
        cookie.Value = Session["Temp"].ToString();
        Response.Cookies.Add(cookie);

        string Saltval = Session["salt"].ToString();
        string Tpass = hashcodegenerate.GetSHA512(Upass + Saltval);

        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("@LOGIN_ID", LOGIN_ID);
        parameters[1] = new SqlParameter("@PASSWORD", Tpass);

        DataSet dsUser = new DataSet();

        if (TYPE == "STA")
        {
            dsUser = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "STA_SP_LOGIN_VALIDATE_USER", parameters);
        }
        else
        {
            dsUser = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "RTA_SP_LOGIN_VALIDATE_USER", parameters);
        }

        DataTable dtUser = dsUser.Tables[0];

        if (dtUser.Rows.Count > 0)
        {
            Session.Clear();
            Session["EMPLOYEE_ID"] = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_ID"]);
            Session["EMPLOYEE_NAME"] = Convert.ToString(dtUser.Rows[0]["EMPLOYEE_NAME"]);
            Session["EMAIL_ID"] = Convert.ToString(dtUser.Rows[0]["EMAIL_ID"]);
            Session["DEPARTMENT"] = Convert.ToString(dtUser.Rows[0]["DEPARTMENT"]);
            Session["DESIGNATION"] = Convert.ToString(dtUser.Rows[0]["DESIGNATION"]);
            Session["STATUS"] = Convert.ToString(dtUser.Rows[0]["STATUS"]);
            Session["USER_TYPE"] = Convert.ToString(dtUser.Rows[0]["USER_TYPE"]);
            Session["RTO_CODE"] = Convert.ToString(dtUser.Rows[0]["RTO_CODE"]);
            Session["RTO_NAME"] = Convert.ToString(dtUser.Rows[0]["RTO_NAME"]);
            Session["USER_LABEL"] = Convert.ToString(dtUser.Rows[0]["USER_LEVEL"]);
            Session["BROWSER"] = Convert.ToString(bc.Browser);
            Session["USER_TYPES"] = TYPE;

            if (Convert.ToString(dtUser.Rows[0]["USER_ROLE"]) == string.Empty)
                Session["USER_ROLE"] = "One";
            else
                Session["USER_ROLE"] = Convert.ToString(dtUser.Rows[0]["USER_ROLE"]);

            string MSG = ValidateSession.ValidateUserSession(
                Convert.ToString(dtUser.Rows[0]["EMPLOYEE_ID"]),
                getclientIP(),
                TYPE,
                bc.Browser
            );

            if (MSG == "Success")
            {
                if (TYPE == "STA")
                    Response.Redirect("STA-DASHBOARD.aspx");
                else
                    Response.Redirect("RTA-DASHBOARD.aspx");
            }
            else if (MSG == "Sorry You have already logged in with another Browser or Another System")
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('Sorry You have already logged in with another Browser or Another System');", true);
            }
            else
            {
                string Message = MSG.Replace("\n", "").Replace("\r", "");
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                    Guid.NewGuid().ToString(),
                    "alert('" + Message + "');", true);

                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                Guid.NewGuid().ToString(),
                "alert('Sorry! Invalid Login Details');", true);
        }
    }

    protected void btnFP_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForgotPassword.aspx");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");
    }

    public static string getclientIP()
    {
        string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(ip))
        {
            return ip.Split(',')[0];
        }
        else
        {
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
