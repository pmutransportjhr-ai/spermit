<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA_COUNTER_SIGNATURE_SURRENDER_APPLICATION.aspx.cs" Inherits="STA_COUNTER_SIGNATURE_SURRENDER_APPLICATION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<form id="FormID1" enctype="multipart/form-data" runat="server" class="form-horizontal" autocomplete="off" method="post">
     <asp:HiddenField ID="hdnTypeID" runat="server" />
    <asp:HiddenField ID ="hdnTypeOptionID" runat="server" />  
     <asp:Label ID="hdnApplicationNo" runat="server"></asp:Label>
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering ="true" />
    <div class="page-container">
        <div class="page-content-wrapper">
            <div class="page-content">
                <div class="container">
                    <ul class="page-breadcrumb breadcrumb">
                        <li>
                            <a href="PermitHomePage.aspx">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>

                        <li>
                            <span id="PageHeading" runat="server"></span>
                           
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase"  id="PageHeading1" runat="server"></span>
                                        </div>
                                    </div>
<div class="portlet-body">
 <asp:UpdatePanel ID="UPpanel" runat="server">
                                                <ContentTemplate>                                        
                                           <div class="row">
     <div class="col-md-2"><label class="control-label">Vehicle Reg. No. : </label><span style="color:red">*</span></div>
                                                                    <div class="col-md-2"><asp:Textbox ID="txtExistRegNo" CssClass="form-control" runat="server"></asp:Textbox></div>
                                                                    <div class="col-md-2"><label class="control-label">Permit No. : </label><span style="color:red">*</span></div>
                                                                    <div class="col-md-2"><asp:Textbox ID="txtExistPermitNo" CssClass="form-control"  runat="server"></asp:Textbox></div>
                                                                    <div class="col-md-4"><asp:Button ID="btnGO" runat="server" CssClass="btn green" OnClick="btnGO_Click" OnClientClick="return fnSTAReplacementValidate()" Text="GO" />
                                                                        &nbsp;&nbsp;<asp:Button ID="btnExistClear" runat="server" CssClass="btn default" OnClick="btnExistClear_Click"  Text="Clear" />
                                                                        &nbsp;<asp:Button ToolTip="Cancel" ID="btnCancel" runat="server" CssClass="btn red" Text="Cancel" OnClick="btnCancel_Click"  />
                                                                    </div>
                                                                    </div><br />
                                            <div class="row">
                                                        <asp:Label CssClass="col-md-12" Font-Bold="True"  ID="Message" runat="server" Visible="False"></asp:Label>
                                                    </div><br />
                                                                                                        <br />
                                               <div id="DivMobileValidate" runat="server" visible="false">
                                                    
                                                <div class="row">
                                                        <div class="col-md-3">Enter Your Mobile Number </div>
                                                        <div class="col-md-3">
                                                            <input type="text" runat="server"  class="number form-control" id="txtMobileNumber" placeholder="Mobile Number" /></div>
                                                        <div class="col-md-6">


                                                            <asp:Button ToolTip="Send OTP" ID="btnOTP" runat="server" CssClass="btn green" Text="Send OTP" OnClick="btnOTP_Click" OnClientClick="return fnMobileValidate()" />



                                                        </div>
                                                     </div> <br />
                                                </div>
                                               
                                                <div id="DivOTPMatch" runat="server" visible="false">
                                                  
                                                 <div class="row">
                                                        <div class="col-md-3">OTP: </div>
                                                        <div class="col-md-3">
                                                            <input type="text" runat="server" class="number form-control" id="txtOTP" placeholder="------" /></div>
                                                        <div class="col-md-6">


                                                            <asp:Button ToolTip="Verify" ID="btnVerify" runat="server" CssClass="btn green" Text="Verify" OnClick="btnVerify_Click" OnClientClick="return fnOTPValidate()" />



                                                        </div>
                                                     </div>
                                                </div>

<asp:Panel CssClass="row" ID="Panel1" runat="server" Visible="false">
                                             <div class="portlet-body">
                                                               <div class="row">
                                                       
                                                                        <div class="col-md-6 col-sm-6">
                                                                    <b  style="font-size:large">Permit Type : </b>
                                                                        <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                            </div>
                                                                    <div class="col-md-6 col-sm-6" style="text-align:right">
                                                               
                                                                    
                                                            </div>
                                                                   <br /><br />
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Personal Information</b></u></h4></div>
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                
                                                                <tr>
                                                                <td><b>Name : </b></td>
                                                                <td>
                                                                    <asp:Label ID="VOName" runat="server"></asp:Label></td>
                                                                <td><b>C/O :  </b></td>
                                                                <td>
                                                                    <asp:Label ID="VOCareOf" runat="server"></asp:Label></td>

                                                            </tr>
                                                            <tr>
                                                                <td><b>Present Address: </b></td>
                                                                <td>
                                                                    <asp:Label ID="VOPresentAddress" runat="server"></asp:Label><span id=""></span> </td>
                                                                <td><b>Permanent Address : </b></td>
                                                                <td>
                                                                    <asp:Label ID="VOPermanentAddress" runat="server"></asp:Label></td>

                                                            </tr>

                                                           
                                                        </table>

                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Vehicle Details</u></b></h4>
                                                    </div>
                                                    <div class="col-md-12 col-sm-12">

                                                         <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    
                                                                    <td><b>Vehicle Registration No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VORegNumber" runat="server"></asp:Label></td>
                                                                     <td><b>Permit No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOPermitNo" runat="server"></asp:Label></td>
                                                                 
                                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Permit Validity From : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VoPermitFrom" runat="server"></asp:Label></td>
                                                                     <td><b>Permit Validity Upto : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VoPermitUpto" runat="server"></asp:Label></td>
                                                                 

                                                                </tr>
                                                                <tr>
                                                                    <td><b>Maker Description : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblMakerDesc" runat="server"></asp:Label></td>
                                                                    <td><b>Maker Model No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblMakermodel" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Type of Vehicle :</b></td>
                                                                    <td>
                                                                        <asp:Label ID="VType" runat="server"></asp:Label></td>
                                                                    <td><b>Chassis Number : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VChassisNumber" runat="server"></asp:Label></td>
                                                                   
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td><b>Engine Number  : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VEngineNumber" runat="server"></asp:Label></td>
                                                                    <td><b>Seating Capacity : </b></td>
                                                                                <td> <asp:Label ID="VSeatCapacity" runat="server"></asp:Label></td>
                                                                               
                                                                </tr>
                                                               
                                                                <tr>

                                                                               <td><b>U.L.W as per RC : </b></td>
                                                                                <td>  <asp:Label ID="VGVW" runat="server"></asp:Label></td>
                                                                                <td><b>R.L.W as per RC </b></td>
                                                                                <td> <asp:Label ID="VPayLoad" runat="server"></asp:Label></td>
                                                                           
                                                                            </tr>
                                                                            <tr>
                                                                               
                                                                                
                                                                                <td><b>Manufacturing Date :</b></td>
                                                                               <td> <asp:Label ID="VManufacturingON" runat="server"></asp:Label></td>
                                                                                  <td><b>Nature of goods to be carried, if any :</b></td>
                                                                               <td> <asp:Label ID="VNatureOfGoods" runat="server"></asp:Label></td>                                                                         
                                                                               </tr>
                                                            </table>
                                                       
                                                        

                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Route Details</u></b></h4>
                                                    </div>

                                                </div><br />
<div class="row">
<div class="col-md-3 col-sm-3"><b>Issued by : </b></div>
<div class="col-md-9 col-sm-9"><asp:Label  ID="lblIssuedBy" runat="server" /></div>
</div> <br />
<div class="row">
<div class="col-md-3 col-sm-3"><b>Route : </b></div>
<div class="col-md-9 col-sm-9"><asp:Label  ID="RouteHDRInfo" runat="server" /></div>
</div> <br />
                                                                Time Format: 24 Hours <br />
                                                <div id="RouteTimeInfo" runat="server"></div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Other Details</u></b></h4>
                                                    </div>
                                                    <div class="col-md-12 col-sm-12">
                                                        <table class="table table-striped table-bordered table-hover">
                                                             <tr>
                                                                    <td><b>Mobile No: <span style="color:red">*</span></b></td>
                                                                    <td><asp:TextBox ID="txtMobileNo" CssClass="number form-control" runat="server"></asp:TextBox></td>
                                                                 <td><b>Email ID: </b></td>
                                                                   <td><asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                               
                                                                                
                                                                </tr>
                                                                 <tr>
                                                                                <td>
                                                                                    <b>Recommendation Letter No: </b>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="VORecommendationNo" runat="server"></asp:Label>
                                                                                    
                                                                                </td>
                                                                                <td>
                                                                                    <b>Recommendation Date:</b>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="VORecommendationNoDated" runat="server"></asp:Label>
                                                                                     
                                                                                </td>
                                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                 

                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Other Information</u></b></h4>
                                                    </div>
                                                    </div>
                                                 <div class="row">
                                                    
                                                        
                                                                              <div class="col-md-3 col-sm-3">Registration Certificate (RC)  <span style="color:red">*</span></div>
                                                                                <div class="col-md-9 col-sm-9">
                                                                                    <asp:FileUpload runat="server" id="uploadRC" accept="application/pdf, .pdf"  ></asp:FileUpload></div>
                                                        </div><br />
                                                 <div class="row">
                                                                                 <div class="col-md-3 col-sm-3">Permit Certificate <span style="color:red">*</span></div>
                                                                                <div class="col-md-9 col-sm-9"><asp:FileUpload runat="server" id="uploadPermit" accept="application/pdf, .pdf"></asp:FileUpload></div>
                                                     </div><br /><div class="row">
                                                                                
                                                                             <div class="col-md-3 col-sm-3">Counter Signature Certificate <span style="color:red">*</span></div>
                                                                                <div class="col-md-9 col-sm-9"><asp:FileUpload runat="server" id="uploadCS" accept="application/pdf, .pdf"></asp:FileUpload></div>
                                                         </div><br />
                                                         <div class="row">
                                                         
                                                                                
                                                                           
                                                                             
                                                                           
                                                                        
                                                                 <div class="col-md-3 col-sm-3">Other Documents </div>
                                                                                <div class="col-md-9 col-sm-9"><asp:FileUpload runat="server" id="uploadOther" accept="application/pdf, .pdf"></asp:FileUpload></div>
                                                             </div><br />
                                                             <div class="row">
                                                             <div class="col-md-3 col-sm-3">Comments (If Any)</div>
                                                                                    <div class="col-md-9 col-sm-9">

                                                                                        <asp:TextBox ID="txtComments" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                          

                                                </div>
                                                
                                                  <div class="row">
                                                          <div class="col-md-12 col-sm-12">
                            
                            <label for="modalAgree"><a style="color:red"  href="javascript:fnTermsCondition('cssapp');"><b>Terms & Conditions</b></a> (By procedding you are agree with the Terms and condition)</label>
                        </div>
                                                            </div><br /><br />
                                                 <div class="row">
                                                            <div class="col-md-4 col-sm-4"></div>
                                                            <div class="col-md-4 col-sm-4">
                                                                <div style="text-align:center">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Proceed" OnClick="btnSave_Click" OnClientClick="return fnRPApplicationValidate()" />
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="btn green" Text="Reset" OnClick="btnReset_Click"   />

                                                                </div>
                                                                
                                                            </div>
                                                            <div class="col-md-4 col-sm-4">
                                                                

                                                            </div>
                                                     
                                                        </div>
                                            <br />
                                            
                                            
                                           
                                                    

                                    
    </asp:Panel>
                                        <br/>
                                       

                                   
                                                    
                                      
                                                         </div>
                                          

                                           

                                       
                                        
 </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btnSave" />
                                                </Triggers>

                                            </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/moment.min.js" type="text/javascript"></script>

    <script src="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>
   <script src="assets/js/STA_COUNTER_SIGNATURE_SURRENDER.js" type="text/javascript"></script>
<script src="assets/js/GetCsNumber.js"></script>
</asp:Content>

