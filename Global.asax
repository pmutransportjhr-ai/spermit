<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>
<script runat="server">
    void Application_BeginRequest(object sender, EventArgs e)
    {
         HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");
        //HttpContext.Current.Response.AddHeader("x-frame-options", "SAMEORIGIN");
        //HttpContext.Current.Response.AddHeader("Content-Type", " text/html; charset=utf-8");
        
    }
    void Application_Start(object sender, EventArgs e) 
    {
        
        
        
        // Code that runs on application startup
        //long gCount = 0L;
        //FileInfo fin = new FileInfo(Server.MapPath(gFile));
        //if (!fin.Exists)
        //{
        //    FileStream fs = fin.Create();
        //    fs.Close();
        //    StreamWriter sw = new StreamWriter(Server.MapPath(gFile));
        //    sw.WriteLine("0");
        //    sw.Close();
        //    sw.Dispose();
        //}

        //using (StreamReader sr = new StreamReader(Server.MapPath(gFile)))
        //{
        //    string strLine = sr.ReadLine();
        //    gCount = Convert.ToInt64(strLine);
        //    //sr.Close();
        //    //sr.Dispose();
        //}

        //Application["Guest"] = gCount;//0L;//boxing
       

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        //Session.Clear();
        //Session.Abandon();
        
            

    }

   
    void Application_Error(object sender, EventArgs e) 
    {



       
        
        // Code that runs when an unhandled error occurs
       // Response.Redirect("~/errorPage.html");

    }
   
    //void Session_Start(object sender, EventArgs e) 
    //{
    //    // Code that runs when a new session is started
    //    Session.Timeout =20;


    //    Application.Lock();
    //    Application["Guest"] = (long)Application["Guest"] + 1;


    //    using (StreamWriter sw = new StreamWriter(Server.MapPath(gFile)))
    //    {
    //        sw.WriteLine(Application["Guest"]);
    //    }

    //    Application.UnLock();

    //}
    

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        //Application.Lock();
        //Application["Live"] =  (Int32)Application["Live"] - 1;
        //Application.UnLock();

    }
       
</script>
