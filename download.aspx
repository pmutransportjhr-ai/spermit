<%@ Page Language="C#" %>
<%
    string basepath = @"\\10.30.9.199\d\JHT";
    string path = Request.Params.Get("path");

    string filename = Request.Params.Get("filename");
Response.ContentType = "application/pdf";  
Response.AppendHeader("Content-Disposition", "attachment; filename="+filename);  
if(System.IO.File.Exists( basepath + "/" + path+"/"+ filename ))
Response.TransmitFile( basepath + "/" + path+"/"+ filename );  

Response.End();

%>