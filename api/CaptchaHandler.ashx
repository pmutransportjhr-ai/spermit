<%@ WebHandler Language="C#" Class="CaptchaHandler" %>

using System;
using System.Web;
using System.Drawing;
public class CaptchaHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
{

    public void ProcessRequest(HttpContext context)
    {
        using (Bitmap b = new Bitmap(121, 31, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
        {
            using (Graphics g = Graphics.FromImage(b))
            {
                Rectangle rect = new Rectangle(0, 0, 120,30);
                g.FillRectangle(Brushes.White, rect);

                
               
                Font drawFont = new Font("Arial", 16, FontStyle.Italic | FontStyle.Strikeout);
                using (SolidBrush drawBrush = new SolidBrush(Color.Blue))
                {
                
                    PointF drawPoint = new PointF(5, 10);
                    string drawString = context.Session["captcha"].ToString();
                        
                
                    g.DrawRectangle(new Pen(Color.Red, 0), rect);
                    g.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                
                b.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                context.Response.ContentType = "image/jpeg";
                context.Response.End();
            }
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}