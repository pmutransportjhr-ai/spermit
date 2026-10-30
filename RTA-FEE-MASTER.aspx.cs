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
using System.Text;

public partial class RTA_FEE_MASTER : System.Web.UI.Page
{
    public string FillFeeList { get; set; }
    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillFeeListMaster();

        }
    }

    private void FillFeeListMaster()
    {
         String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
         using (SqlConnection sCon = new SqlConnection(sConnectionString))
         {
             try
             {

                 sCon.Open();
                 SqlCommand sCmd = new SqlCommand();
                 sCmd.Connection = sCon;
                 sCmd.CommandText = "RTA_SP_FEES_MSTR";
                 sCmd.CommandType = CommandType.StoredProcedure;
                 sCmd.Parameters.Clear();
                 sCmd.Parameters.AddWithValue("@EMPLOYEE_ID", Convert.ToString(Context.Session["EMPLOYEE_ID"]));
                 sCmd.Parameters.AddWithValue("@MODE", "FEE_LIST");
                 DataSet ds = new DataSet();
                 SqlDataAdapter da = new SqlDataAdapter(sCmd);
                 da.Fill(ds);
                 DataTable dt = new DataTable();




                 dt = ds.Tables[0];
                 string UserRole = Convert.ToString(Session["USER_ROLE"]);


                 StringBuilder sb = new StringBuilder();


                 if (dt.Rows.Count > 0)
                 {
                     if (UserRole == "Super Admin")
                     {
                         btnAdd.Visible = true;
                     }
                     else
                     {
                         btnAdd.Visible = false;
                     }

                     foreach (DataRow dr in dt.Rows)
                     {


                         sb.Append("<tr>");
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE_OPTION"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["PERMIT_TYPE_OPTION_SERVICES"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["FEE_TYPE"]) + "</td>");
                         sb.Append("<td>" + Convert.ToString(dr["FEE"]) + "</td>");
                         string FilePath1;
                         if (Convert.ToString(dr["FILEPATH"]) != "")
                         {
                             string FilePath = Convert.ToString(dr["FILEPATH"]);
                             FilePath1 = "JHT/RTAUploadedFiles/" + FilePath.Replace("@@---@@", string.Empty);
                             sb.Append("<td> <a href='" + FilePath1 + "'  download > click to download file </a></td>");
                         }
                         else
                         {
                             sb.Append("<td> <a href=''>No file </a></td>");
                         }
                         //sb.Append("<td>" + Convert.ToString(dr["STATUS"]) + "</td>");
                        
                         sb.Append("<td><a class='btn btn-outline dark' href=\"javascript:fnEdit('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["FEE_ID"]), true) + "');\">Edit</a></td>");

                         //if (Convert.ToString(dr["READ_ACCESS"]) == "Y" && Convert.ToString(dr["WRITE_ACCESS"]) == "Y")
                         //{
                         //    if (Convert.ToString(dr["CAN_DELETE"]) == "Y")
                         //    {
                         //        sb.Append("<td><a  class='btn btn-outline dark' href=\"javascript:fnDelete('" + CryptorEngine.Encrypt("ID=" + Convert.ToString(dr["FEE_ID"]), true) + "');\">Delete</a></td>");
                         //    }
                         //    else
                         //    {
                         //        sb.Append("<td></td>");
                         //    }
                         //}
                         //else if (Convert.ToString(dr["READ_ACCESS"]) == "Y" && Convert.ToString(dr["WRITE_ACCESS"]) == "N")
                         //{
                         //    sb.Append("<td></td>");
                         //}
                         sb.Append("</tr>");
                     }
                     FillFeeList = sb.ToString();
                 }

             }
             catch (Exception ex)
             {
                 String Message = ex.Message;
                 Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                 ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
             }
         }


    }
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("RTA-FEE-MASTER-NEW.aspx?q=" + CryptorEngine.Encrypt("ID=0", true));

    }
    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        
            String queryString = CryptorEngine.Decrypt(Convert.ToString(txtDelKey.Value), true);
            foreach (String str in queryString.Split(new Char[] { '&' }))
            {
                QueryString qs = new QueryString();
                qs.Name = str.Split(new Char[] { '=' })[0];
                qs.Value = str.Split(new Char[] { '=' })[1];
                QueryStr.Add(qs);
            }
          String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
          using (SqlConnection sCon = new SqlConnection(sConnectionString))
          {
              try
              {
                  QueryString IDx = QueryStr.Where(l => l.Name == "ID").FirstOrDefault();
                  String ID = IDx.Value;

                  sCon.Open();
                  SqlCommand sCmd = new SqlCommand();
                  sCmd.Connection = sCon;
                  sCmd.CommandText = "RTA_SP_FEES_MSTR";
                  sCmd.CommandType = CommandType.StoredProcedure;
                  sCmd.Parameters.Clear();
                  sCmd.Parameters.AddWithValue("@FEE_ID", ID);
                  sCmd.Parameters.AddWithValue("@MODE", "FEE_DELETE");
                  DataSet ds = new DataSet();
                  SqlDataAdapter da = new SqlDataAdapter(sCmd);
                  da.Fill(ds);
                  DataTable dt = new DataTable();
                  dt = ds.Tables[0];

                  ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Convert.ToString(dt.Rows[0]["MSG"]) + "'); window.location ='RTA-FEE-MASTER.aspx'", true);
              }
              catch (Exception ex)
              {
                  String Message = ex.Message;
                  Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                  ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
                  return;
              }
          }
    }

}