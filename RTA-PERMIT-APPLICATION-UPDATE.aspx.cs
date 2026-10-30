using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SoftEra.JHT.Infrastructure;
using System.IO;
using System.Configuration;
using System.Text;
using QRCoder;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Collections;


public partial class RTA_PERMIT_APPLICATION_UPDATE : System.Web.UI.Page
{
    // private static string ID;

    protected List<QueryString> QueryStr = new List<QueryString>();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('You are not permitted to access this page');window.location ='PermitHomePage.aspx'", true);
                return;
            }
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
                try
                {
                    String queryString = CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["q"]), true);
                    foreach (String str in queryString.Split(new Char[] { '&' }))
                    {
                        QueryString qs = new QueryString();
                        qs.Name = str.Split(new Char[] { '=' })[0];
                        qs.Value = str.Split(new Char[] { '=' })[1];
                        QueryStr.Add(qs);
                    }

                    QueryString IDx = QueryStr.Where(l => l.Name == "APP_ID").FirstOrDefault(); // CryptorEngine.Decrypt(Convert.ToString(Context.Request.QueryString["ID"]), true);

                    hdnID.Value = IDx.Value;

                    sCon.Open();
                    SqlCommand sCmd = new SqlCommand();
                    sCmd.Connection = sCon;
                    sCmd.CommandText = "RTA_PERMIT_APPLY_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();
                    sCmd.Parameters.AddWithValue("@APP_ID", Convert.ToInt32(hdnID.Value));
                    sCmd.Parameters.AddWithValue("@MODE", "VIEW_APPLICATION_UPDATE");
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(sCmd);
                    da.Fill(ds);

                    DataTable dt1 = ds.Tables[0];
                    DataTable dt2 = ds.Tables[1];
                    DataTable dt3 = ds.Tables[2];
                    DataTable dt4 = ds.Tables[3];
                    StringBuilder sb = new StringBuilder();


                    lblApplicationNo.Text = dt1.Rows[0]["APPLICATION_NUMBER"].ToString();
                    lblPermitType.Text = dt1.Rows[0]["PERMIT_TYPE"].ToString();
                    VOName.Text = dt1.Rows[0]["OWNER_NAME"].ToString();
                    VOCareOf.Text = dt1.Rows[0]["CARE_OF"].ToString();
                    VOPresentAddress.Text = dt1.Rows[0]["PRESENT_ADDRESS"].ToString();
                    VOPermanentAddress.Text = dt1.Rows[0]["PERMANENT_ADDRESS"].ToString();

                    VORegNumber.Text = dt1.Rows[0]["REGISTRATION_NUMBER"].ToString();
                    VType.Text = dt1.Rows[0]["VEHICLE_CLASS_TYPE"].ToString();
                    VChassisNumber.Text = dt1.Rows[0]["CHASIS_NUMBER"].ToString();
                    VEngineNumber.Text = dt1.Rows[0]["ENGINE_NUMBER"].ToString();

                    lblMakerDesc.Text = dt1.Rows[0]["MAKER_DESC"].ToString();
                    lblMakermodel.Text = dt1.Rows[0]["MAKER_MODEL"].ToString();
                    lblFuelDesc.Text = dt1.Rows[0]["FUEL_DESC"].ToString();
                    lblVcolor.Text = dt1.Rows[0]["VEHICLE_COLOR"].ToString();
                    VFitness.Text = dt1.Rows[0]["FITNESS_VALID_UPTO"].ToString();
                    VTax.Text = dt1.Rows[0]["TAX_PAID_UPTO"].ToString();
                    VULW.Text = dt1.Rows[0]["ULW"].ToString();
                    VRLW.Text = dt1.Rows[0]["RLW"].ToString();
                    VSeatCapacity.Text = dt1.Rows[0]["SEATING_CAPACITY"].ToString();
                    VManufacturingON.Text = dt1.Rows[0]["MANUFACTURING_ON"].ToString();


                    VOMobileNo.Text = dt1.Rows[0]["MOBILE_NUMBER"].ToString();
                    VOEmailID.Text = dt1.Rows[0]["EMAIL_ID"].ToString();
                    lblGender.Text = dt1.Rows[0]["GENDER"].ToString();
                    lblQualification.Text = dt1.Rows[0]["APPLICANT_QUALIFICATION"].ToString();
                    lblCategory.Text = dt1.Rows[0]["APPLICANT_CATEGORY"].ToString();
                    lblExArmy.Text = dt1.Rows[0]["EX_ARMY"].ToString();
                    lblMinority.Text = dt1.Rows[0]["MINORITY"].ToString();
                    lblHandicapped.Text = dt1.Rows[0]["HANDICAPPED"].ToString();
                    lblHML.Text = dt1.Rows[0]["HEAVY_MOTOR_LICENCE"].ToString();
                    lblIdentityProof.Text = dt1.Rows[0]["IDENTITY_PROOF"].ToString();
                    lblAC.Text = dt1.Rows[0]["AIR_CONDITIONED"].ToString();
                    lblDeluxe.Text = dt1.Rows[0]["DELUXE"].ToString();

                    txtICValidUpto.Value = dt1.Rows[0]["IC_VALID_UPTO"].ToString();
                    txtPCValidUpto.Value = dt1.Rows[0]["PC_VALID_UPTO"].ToString();
                    txtFCValidUpto.Value = dt1.Rows[0]["FC_VALID_UPTO"].ToString();
                
                   
                    RouteHDRInfo.Text = dt1.Rows[0]["ROUTE"].ToString();
                  


                    
                    

                    if (dt4.Rows.Count > 0)
                    {
                        HFNoOftrips.Value = dt4.Rows.Count.ToString();
                        for (int k = 1; k <= dt4.Rows.Count;k++ )
                        {


                            if (dt2.Rows.Count > 0)
                            {
                                 DataRow[] dr = dt2.Select("SEQUENCE='" + k + "'");
                                 Int32 length = dr.Length - 1;
                               
                    sb.Append("<table class='table table-striped table-bordered table-hover'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='3' style='text-align:center;'> Up </th>");
                    sb.Append("<th>  </th>");
                    sb.Append("<th colspan='3' style='text-align:center;'> Down </th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th> Route </th>");
                    sb.Append("<th> Arrival </th>");
                    sb.Append("<th> Departure </th>");
                    sb.Append("<th>  </th>");
                    sb.Append("<th> Route </th>");
                    sb.Append("<th> Arrival </th>");
                    sb.Append("<th> Departure </th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tbody id='tbdRouteInfo_" + k +"'>");
                    for (int i = 0; i < dr.Length; i++)
                    {
                        int p = i + 1;
                        if (i == 0)
                        {
                            sb.Append("<tr id='tr" + k + "_" + p + "'>");
                            sb.Append("<td><label id='lblUPRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                            sb.Append("<td> ..... </td>");
                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + p + "\")" + "' placeholder='' />  &nbsp;&nbsp;Hrs.</div></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td id='lblDownRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                            sb.Append("<td> ..... </td>");
                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%' class='form-control mask_route' id='txtDownDepRoute" + k + "_" + p + "'value=" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + p + "\")" + "' placeholder='' />  &nbsp;&nbsp;Hrs.</div></td>");
                            sb.Append("</tr>");
                   
                        }
                        else if (i == length)
                        {
                            sb.Append("<tr id='tr" + k + "_" + p + "'>");
                            sb.Append("<td><label id='lblUPRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + p + "\")" + "' placeholder='' />  &nbsp;&nbsp;Hrs.</div></td>");
                            sb.Append("<td> ..... </td>");
                            sb.Append("<td></td>");
                            sb.Append("<td id='lblDownRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + p + "\")" + "' placeholder='' />  &nbsp;&nbsp;Hrs.</div></td>");
                            sb.Append("<td> ..... </td>");
                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append("<tr id='tr" + k + "_" + p + "'>");
                            sb.Append("<td><label id='lblUPRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_UP_CITY"]) + "</label></td>");
                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPArrRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["UP_ARR_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtUPArrRoute" + k + "_" + p + "\")" + "' placeholder='' />  &nbsp;&nbsp;Hrs.</div></td>");
                            sb.Append("<td><div class='input-group'><input type='text' style='width:80%'  class='form-control mask_route' id='txtUPDepRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["UP_DEP_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtUPDepRoute" + k + "_" + p + "\")" + "' placeholder='' />  &nbsp;&nbsp;Hrs.</div></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td id='lblDownRoute" + k + "_" + p + "'>" + Convert.ToString(dr[i]["VIA_DOWN_CITY"]) + "</td>");
                            sb.Append("<td><div class='input-group'><input  type='text' style='width:80%'  class='form-control mask_route' id='txtDownArrRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["DOWN_DEP_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtDownArrRoute" + k + "_" + p + "\")" + "' placeholder='' />  &nbsp;&nbsp;Hrs.</div></td>");
                            sb.Append("<td><div class='input-group'><input  type='text' style='width:80%' class='form-control mask_route' id='txtDownDepRoute" + k + "_" + p + "' value=" + Convert.ToString(dr[i]["DOWN_ARR_TIME"]) + " onblur='javascript:timeFunction(" + "\"txtDownDepRoute" + k + "_" + p + "\")" + "' placeholder='' />  &nbsp;&nbsp;Hrs.</div></td>");
                            sb.Append("</tr>");
                        }

                    }
                    sb.Append("<tbody>");
                    sb.Append("</table>");


                                
                            }
                            }
                        RouteTimeInfo.InnerHtml = sb.ToString();
                    }
                
                     
                        if (dt3.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt3.Rows)
                            {
                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "RC")
                                {

                                    string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                    string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                    divrcfile.InnerHtml = strlink;

                                }
                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "SPEED")
                                {

                                    string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                    string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                    divSpeed.InnerHtml = strlink;

                                }
                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "INSURANCE")
                                {
                                    string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                    string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                    divinsurFile.InnerHtml = strlink;

                                }

                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "POLLUTION")
                                {
                                    string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                    string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                    divrollution.InnerHtml = strlink;
                                }

                                if (Convert.ToString(dr["UPLOAD_HEADS"]) == "FITNESS")
                                {
                                    string newstr = "RTAUploadedFiles/" + Convert.ToString(dr["FILE_NAME"]).Replace("@@---@@", string.Empty);
                                    string strlink = string.Format("<a href ='{0}' target='_blank' title='download'>{1}  </a>", newstr, "Click here to download");
                                    divfitness.InnerHtml = strlink;
                                }
                                

                            }
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
    }


    protected void btnYes_Click(object sender, EventArgs e)
    {
           
       
            String input = "";
            input = txtroutjson.Value;
            String sConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            SqlTransaction objTrans = null;
            

            PERMIT_APPLICATION_LDR rDTO = new JavaScriptSerializer().Deserialize<PERMIT_APPLICATION_LDR>(input);


            Int32 PermitId;
            PermitId = Convert.ToInt32(hdnID.Value.ToString());
            using (SqlConnection sCon = new SqlConnection(sConnectionString))
            {
             try
             { 
                sCon.Open();
                objTrans = sCon.BeginTransaction();
                SqlCommand sCmd = new SqlCommand();
                sCmd.Connection = sCon;
                sCmd.Transaction = objTrans;
                sCmd.CommandText = "RTA_PERMIT_APPLICATION_HDR_AED";
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Clear();

                sCmd.Parameters.AddWithValue("@APP_ID", PermitId);
                sCmd.Parameters.AddWithValue("@IC_VALID_UPTO", DateTime.ParseExact(txtICValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@PC_VALID_UPTO", DateTime.ParseExact(txtPCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@FC_VALID_UPTO", DateTime.ParseExact(txtFCValidUpto.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture));
                sCmd.Parameters.AddWithValue("@MODE", "VALIDITY_UPDATE");
                ArrayList validate = uploadFileValidate();
                string result = string.Empty;
                bool result1 = true;
                foreach (var str in validate)
                {
                    if (str == "OK") { }
                    else
                    {
                        result1 = false;
                        result = result + "  " + str;

                    }

                }
                if (result1 == true)
                {

                    sCmd.ExecuteNonQuery();

                    sCmd.CommandText = "RTA_PERMIT_APPLICATION_LDR_AED";
                    sCmd.CommandType = CommandType.StoredProcedure;
                    sCmd.Parameters.Clear();

                    sCmd.Parameters.AddWithValue("@APP_ID", PermitId);
                    sCmd.Parameters.AddWithValue("@OP_MODE", "DELETE");
                    sCmd.ExecuteNonQuery();

                    foreach (PERMIT_APPLICATION_LDR dr in rDTO.PERMIT_APPLICATION_LDRs)
                    {

                        sCmd.CommandText = "RTA_PERMIT_APPLICATION_LDR_AED";
                        sCmd.CommandType = CommandType.StoredProcedure;
                        sCmd.Parameters.Clear();

                        sCmd.Parameters.AddWithValue("@APP_ID", PermitId);
                        sCmd.Parameters.AddWithValue("@SEQUENCE", dr.ROUTE_DETAIL_ID);
                        sCmd.Parameters.AddWithValue("@VIA_CITY", dr.VIA_CITY);
                        sCmd.Parameters.AddWithValue("@ARRIVAL_TIMING", dr.ARRIVAL_TIMING.ToString());
                        sCmd.Parameters.AddWithValue("@DEPARTURE_TIMING", dr.DEPARTURE_TIMING.ToString());
                        sCmd.Parameters.AddWithValue("@MODE", dr.MODE);
                        sCmd.Parameters.AddWithValue("@ID", dr.ID);
                        sCmd.Parameters.AddWithValue("@OP_MODE", "UPDATE");
                        sCmd.ExecuteNonQuery();

                    }
                    uploadFile(PermitId, sCmd);

                    objTrans.Commit();
                    ScriptManager.RegisterClientScriptBlock((sender as Control), Page.GetType(), Guid.NewGuid().ToString(), "alert('Successfully Updated');window.location ='PermitHomePage.aspx'", true);
                }
                else
                {
                    objTrans.Rollback();
                    String Message = result;
                    Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);

                }
             }
             catch (Exception ex)
             {
                 objTrans.Rollback();
                 String Message = ex.Message;
                 Message = Message.Replace("\r", " ").Replace("\n", " ").Replace("'", "");

                 ScriptManager.RegisterClientScriptBlock((sender as Control), Page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');window.location ='RTA-PERMIT-APPLICATION-UPDATE.aspx'", true);
             }
            
            }
            

        

    }
    private ArrayList uploadFileValidate()
    {
        ArrayList resulArray = new ArrayList();
        FileUploadValidations validateFile = new FileUploadValidations();
        if (uploadRC.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadRC.FileName, uploadRC.FileContent));
        }
        if (uploadInsur.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadInsur.FileName, uploadInsur.FileContent));
        }

        if (uploadPollution.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadPollution.FileName, uploadPollution.FileContent));
        }

        if (uploadFitness.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadFitness.FileName, uploadFitness.FileContent));
        }
        if (uploadSpeed.HasFile)
        {
            resulArray.Add(validateFile.FileValidate(uploadSpeed.FileName, uploadSpeed.FileContent));
        }
        
        return resulArray;

    }
    private void uploadFile(int permitId, SqlCommand sCmd)
    {
        string Text;

        DateTime dt = new DateTime();


        string uniqueFileNamPrfix = "";

        bool str = uploadRC.HasFile;
        if (uploadRC.HasFile)
        {
            
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadRC.FileName);

                string result = uploadProceudureCall(Convert.ToString(permitId), "RC", filename, sCmd);
                if (result == "Done")
                    uploadRC.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "PERMIT_RC_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadRC.FileName));

                Text = filename;


            

        }
        if (uploadSpeed.HasFile)
        {

            dt = DateTime.Now;
            uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
            string filename = "PERMIT_SPEED_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadSpeed.FileName);

            string result = uploadProceudureCall(Convert.ToString(permitId), "SPEED", filename, sCmd);
            if (result == "Done")
                uploadSpeed.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "PERMIT_SPEED_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadSpeed.FileName));

            Text = filename;




        }
        if (uploadInsur.HasFile)
        {
            
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadInsur.FileName);
                string result = uploadProceudureCall(Convert.ToString(permitId), "INSURANCE", filename, sCmd);
                if (result == "Done")
                    uploadInsur.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "PERMIT_INSURANCE_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadInsur.FileName));

                Text = filename;



        }

        if (uploadPollution.HasFile)
        {
              dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadPollution.FileName);
                string result = uploadProceudureCall(Convert.ToString(permitId), "POLLUTION", filename, sCmd);
                if (result == "Done")
                    uploadPollution.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "PERMIT_POLLUTION_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadPollution.FileName));

                Text = filename;



        }

        if (uploadFitness.HasFile)
        {
           
                dt = DateTime.Now;
                uniqueFileNamPrfix = Convert.ToString(dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + dt.Hour + "_" + dt.Minute + "_" + dt.Second + "_" + dt.Millisecond);
                string filename = "PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_@@---@@" + Path.GetFileName(uploadFitness.FileName);
                string result = uploadProceudureCall(Convert.ToString(permitId), "FITNESS", filename, sCmd);
                if (result == "Done")
                    uploadFitness.SaveAs(Server.MapPath("~/JHT/RTAUploadedFiles/") + "PERMIT_FITNESS_" + permitId + "_" + uniqueFileNamPrfix + "_" + Path.GetFileName(uploadFitness.FileName));

                Text = filename;


           

        }
        

    }
    private string uploadProceudureCall(string permitId, string uploadHeads, string fileName, SqlCommand sCmd)
    {
        
            sCmd.CommandText = "RTA_PERMIT_APPLICATION_UPLOAD_AED";
            sCmd.CommandType = CommandType.StoredProcedure;
            sCmd.Parameters.Clear();
            sCmd.Parameters.AddWithValue("@APP_ID", permitId);
            sCmd.Parameters.AddWithValue("@UPLOAD_HEADS", uploadHeads);
            sCmd.Parameters.AddWithValue("@FILE_NAME", fileName);
            sCmd.ExecuteNonQuery();
            return "Done";
        
        

    }
  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PermitHomePage.aspx");

    }

   
}

    



    