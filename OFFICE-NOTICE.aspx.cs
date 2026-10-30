 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.Text;
public partial class JHT_OFFICE_NOTICE : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            loadNotice("0");
        }
    }
    public void loadNotice(string type)
    {
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@NOTICE_CODE", DBNull.Value);
            parameters[1] = new SqlParameter("@NOTICE_DETAILS", DBNull.Value);
            parameters[2] = new SqlParameter("@TYPE", type.Trim());
            parameters[3] = new SqlParameter("@NOTICE_START_DATE", DBNull.Value);
            parameters[4] = new SqlParameter("@NOTICE_END_DATE", DBNull.Value);
            parameters[5] = new SqlParameter("@EMP_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
            parameters[6] = new SqlParameter("@FILE_NAME", DBNull.Value);
            parameters[7] = new SqlParameter("@MODE", "LIST_NOTICE");
            DataSet ds = SQLHelper.ExecuteDataset(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_OFFICE_NOTICE_AED", parameters);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='table table-striped table-bordered table-hover'>");
            sb.Append("<thead>");
            sb.Append(" <tr>");
            sb.Append("<th style='width:80%;'>Notice Details</th>");
            sb.Append("<th style='width:80%;'>Download</th>");
            sb.Append("<th>Type</th>");
            sb.Append("<th>Dated</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            
            foreach(DataRow dr in dt.Rows)
            {
				
			    
                string newstr = "";
               if (Convert.ToString(dr["TYPE"]) == "RTA")
                {
					     newstr = "download.aspx?path=RTAUploadedFiles&filename="+Convert.ToString(dr["NOTICE_FILE"]).Replace("@@---@@", string.Empty);
     
                  //  newstr = @"\\10.30.9.199\d\JHT\RTAUploadedFiles" + Convert.ToString(dr["NOTICE_FILE"]).Replace("@@---@@", string.Empty);
                }
                else
                {
					     newstr = "download.aspx?path=STAUploadedFiles&filename="+Convert.ToString(dr["NOTICE_FILE"]).Replace("@@---@@", string.Empty);
     
                   // newstr = @"\\10.30.9.199\d\JHT\STAUploadedFiles" + Convert.ToString(dr["NOTICE_FILE"]).Replace("@@---@@", string.Empty);
                }
				
				
               string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click Here to Download");
				

                sb.Append("<tr>");
                sb.Append(" <td style='width:80%;text-align:left;'>" + Convert.ToString(dr["NOTICE_DETAILS"]) + "</td>");
                sb.Append("<td style='width:20%;text-align:left;'>" + strlink + "</td>");

                sb.Append("<td style='width:20%;text-align:left;'>" + Convert.ToString(dr["TYPE"]) + "</td>");
                sb.Append("<td style='width:20%;text-align:left;'>" + Convert.ToString(dr["created_on"]) + "</td>");

                sb.Append(" </tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            dvNoticeDetails.InnerHtml = sb.ToString();
            //Response.TransmitFile(strlink);
			
    }
    protected void ddlservice_SelectedIndexChanged(object sender, EventArgs e)
    {
        string serviceType = ddlservice.SelectedValue.Trim();
        loadNotice(serviceType);

    }
}