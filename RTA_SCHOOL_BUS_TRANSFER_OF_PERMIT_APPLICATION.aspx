<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="RTA_SCHOOL_BUS_TRANSFER_OF_PERMIT_APPLICATION.aspx.cs" Inherits="RTA_SCHOOL_BUS_TRANSFER_OF_PERMIT_APPLICATION" %>

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
<form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
     <asp:HiddenField ID="hdnTypeID" runat="server" />
    <asp:HiddenField ID ="hdnTypeOptionID" runat="server" />  
    <asp:Label ID="hdnApplicationNo" runat ="server" CssClass="display:none" ></asp:Label>
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
                                            <span class="caption-subject font-red sbold uppercase" id="PageHeading1" runat="server"></span>
                                        </div>
                                    </div>
<div class="portlet-body">
 <asp:UpdatePanel ID="UPpanel" runat="server">
                                                <ContentTemplate>                                        
                                           <div class="row">
     <div class="col-md-2"><label class="control-label">Vehicle Reg. No. :</label><span style="color:red">*</span></div>
                                                                    <div class="col-md-2"><asp:Textbox ID="txtExistRegNo" CssClass="form-control" runat="server"></asp:Textbox></div>
                                                                    <div class="col-md-2"><label class="control-label">Permit No. :</label><span style="color:red">*</span></div>
                                                                    <div class="col-md-2"><asp:Textbox ID="txtExistPermitNo" CssClass="form-control"  runat="server"></asp:Textbox></div>
                                                                    <div class="col-md-4"><asp:Button ID="btnGO" runat="server" CssClass="btn green" OnClick="btnGO_Click" OnClientClick="return fnSTAReplacementValidate()" Text="GO" />
                                                                        &nbsp;&nbsp;<asp:Button ID="btnExistClear" runat="server" CssClass="btn default" OnClientClick="return fnClearPermitNew()"  OnClick="btnExistClear_Click"  Text="Clear" />
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
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <tr>
                                                                                 <td><b>Permit Type : </b></td>
                                                                                <td>  <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbPermitId" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td></td>
                                                                                <td></td>
                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Permit Number : </b></td>
                                                                                <td><asp:Label ID="lblPermitNo" runat="server"></asp:Label>
                                                                                    
                                                                                </td>
                                                                                <td><b>Valid Up to: </b></td>
                                                                                <td><asp:Label ID="lblValidUpto" runat="server"></asp:Label>
                                                                                    
                                                                                </td>
                                                                                
                                                                            </tr>
                                                                           
                                                                            
                                                                            
                                                                        </table>
                                                                      
                                                                    </div>
                                                                </div>
                                                 <div class="row">
                                                 <div class="col-md-12 col-sm-12"><h4><b><u>Personal Information</b></u></h4></div>
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                
                                                                <tr>
                                                                <td><b>Name : </b></td>
                                                                <td>
                                                                    <asp:Label ID="labelName" runat="server"></asp:Label></td>
                                                                <td><b>C/O :  </b></td>
                                                                <td>
                                                                    <asp:Label ID="labelCareOf" runat="server"></asp:Label></td>

                                                            </tr>
                                                            <tr>
                                                                <td><b>Present Address: </b></td>
                                                                <td>
                                                                    <asp:Label ID="labelPresentAddress" runat="server"></asp:Label><span id=""></span> </td>
                                                                <td><b>Permanent Address : </b></td>
                                                                <td>
                                                                    <asp:Label ID="labelPermanentAddress" runat="server"></asp:Label></td>

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
                                                                   <td><b>RTO Name: </b></td>
                                                                                  <td> 
                                                                                      <asp:Label ID="lblRTOName" runat="server" ></asp:Label></td>
                                                                          <td></td>
                                                                                <td>
<asp:Label ID="lblRTOCode"  runat="server"  Visible="false"></asp:Label>
                                                                                </td>
                                                                                 
                                                                            
                                                                            </tr>
                                                             <tr>
                                                                    
                                                                    <td><b>Vehicle Registration No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="labelRegNumber" runat="server"></asp:Label></td>
                                                                   <td><b>Vehicle Class Description:</b></td>
                                                                    <td>
                                                                        <asp:Label ID="labelType" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Chassis Number : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="labelChassisNumber" runat="server"></asp:Label></td>
                                                                    <td><b>Engine Number  : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="labelEngineNumber" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Maker Description : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="labelMakerDesc" runat="server"></asp:Label></td>
                                                                    <td><b>Maker Model No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="labelMakermodel" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Fuel Description :  </b></td>
                                                                    <td>
                                                                        <asp:Label ID="labelFuelDesc" runat="server"></asp:Label></td>
                                                                    <td><b>Color : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="labelcolor" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Fitness Valid Upto: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="labelFitness" runat="server"></asp:Label></td>

                                                                  <td><b>Tax Paid UpTo: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="labelTax" runat="server"></asp:Label></td>
                                                                  </tr>
                                                                <tr>

                                                                                <td><b>U. L. W. as per RC: </b></td>
                                                                                <td>  <asp:Label ID="labelULW" runat="server"></asp:Label></td>
                                                                                <td><b>R. L. W. as per RC: </b></td>
                                                                                <td> <asp:Label ID="labelRLW" runat="server"></asp:Label></td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                               
                                                                                
                                                                                <td><b>Seating Capacity: </b></td>
                                                                                <td> <asp:Label ID="labelSeatCapacity" runat="server"></asp:Label></td>
                                                                               <td><b>Manufacturing Date:</b></td>
                                                                               <td> <asp:Label ID="labelManufacturingON" runat="server"></asp:Label></td>
                                                                                                                                                           
                                                                               </tr>
                                                            </table>
                                                       
                                                        

                                                    </div>
                                                </div>

                                            
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Other Details</u></b></h4>
                                                    </div>
                                                    <div class="col-md-12 col-sm-12">
                                                        <table class="table table-striped table-bordered table-hover">
                                                           <tr>
                                                                                 <td><b>School Location : </b></td>
                                                                                <td> <asp:Label ID="lblFrom"  runat="server"></asp:Label></td>
                                                                                <td><%--<b>To (Station) : </b>--%></td>
                                                                                <td><%--<asp:Label ID="lblTo"  runat="server"></asp:Label>--%></td>
                                                                                
                                                                            </tr>
                                                                              
                                                            
                                                            <tr>
                                                                    <td><b>Mobile No: </b></td>
                                                                    <td><asp:Label ID="labelMobileNo" runat="server"></asp:Label> 
                                                                         </td>
                                                                 <td><b>Email ID: </b></td>
                                                                   <td>
                                                                       <asp:Label ID="labelEmailId" runat="server"></asp:Label>
                                                                       </td>
                                                                               
                                                                                
                                                                </tr>
                                                              
                                                        </table>
                                                    </div>
                                                </div>
                                                 
                                                 
                                                           
                                        
                                            <br />
                                            
                                            
                                           
                                                    

                                    <div class="row">
                                            
                                            <div class="col-md-4 col-sm-4">
                                                <asp:HiddenField ID="txtVehicleJson" runat="server" />
                                                    <label for="form_control"><b>Re-Enter Vehicle Registration Number:  <span style="color:red">*</span></b></label>
                                                
                                            </div>
                                            <div class="col-md-4 col-sm-4">
                                                
                                                    <asp:TextBox ID="txtVehicleNumber" CssClass="regtxtnumber form-control"  runat="server"></asp:TextBox>
                                                    
                                                
                                            </div>
                                                <div class="col-md-1 col-sm-1"></div>
                                            <div class="col-md-3 col-sm-3">
                                               
                                                    
                                                          <asp:Button ToolTip="Search" ID="btnSearch" runat="server" CssClass="btn green" Text="Search" OnClientClick="return fnSearchValidate()"  OnClick="btnSearch_Click"  />
                                                    &nbsp;<asp:Button ToolTip="Clear" ID="btnClear" runat="server" CssClass="btn default" OnClientClick="return fnClearPermitNew()"  Text="Clear" OnClick="btnClear_Click" />
                                                    
                                               
                                            </div>
                                            
                                       </div>
    </div>
    </asp:Panel>
                                        <br/>
                                       

                                   
                                                    
                                      <div class="portlet-body">
                                          <div class="row">
                                          <div class="col-md-12 col-sm-12">
                                          <asp:Label ID="lblMSg" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                              </div>
                                              </div>
                                              <br />
                                          <asp:Panel CssClass="row" ID="Panel2" runat="server" Visible="false">
                                           
                                                    <div class="col-md-12 col-sm-12">
                                                       <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Personal Information
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"> </a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row" style="align-content:center">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12" style="align-content:center">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            
                                                                            <tr>
                                                                                 <td><b> Name : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VOName" MaxLength="40" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                                                   <%-- <asp:Label ID="VOName" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>C/O :  <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                     <asp:TextBox ID="VOCareOf" MaxLength="40" CssClass="regtxt form-control" runat="server"></asp:TextBox> 
                                                                                    <%--<asp:Label ID="VOCareOf" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                                 <td><b>Present Address: <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                   <asp:TextBox TextMode="MultiLine" Columns="5" ID="VOPresentAddress" CssClass="address form-control" runat="server"></asp:TextBox>   
                                                                                    <%--<asp:Label ID="VOPresentAddress" runat="server"></asp:Label>--%>
                                                                                     </td>
                                                                                <td><b> Permanent Address : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox TextMode="MultiLine" Columns="5"  ID="VOPermanentAddress" CssClass="address form-control" runat="server"></asp:TextBox> 
                                                                                   <%-- <asp:Label ID="VOPermanentAddress" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                
                                                                            </tr>
                                                                            
                                                                            
                                                                        </table>
                                                                      
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                      
                                                             
                                                        
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Other Details
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"> </a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <tr>
                                                                                   <td><b>Mobile No : <span style="color:red">*</span></b></td>
                                                                                <td> <asp:TextBox ID="txtMobileNo" MaxLength="10" CssClass="number form-control" runat="server"></asp:TextBox></td>
                                                                                <td><b>Email ID : </b></td>
                                                                                <td><asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                
                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                
                                                                                
                                                                                <td><b> Gender : <span style="color:red">*</span></b></td>
                                                                                
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="gender" id="txtMale" value="Male" />
                                                        Male
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="gender" id="txtFemale" value="Female" />
                                                        Female
																		
                                                    </label>
                                                </div></td>
                                                                              
                                                                               <td></td>
                                                                                <td></td>
                                                                            </tr>
                                                                        </table>
                                                                      
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                   Upload Documents
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"> </a>
                                                                </div>
                                                            </div>
                                                          
                                                            <div class="portlet-body">
                                                            <br />
                                                               
                                                                <div id="uploadDocuments"> 
                                                                    <div class="row">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                           <tr>
                                                                              <td>Registration Certificate (RC)  <span style="color:red">*</span></td>
                                                                                <td>
                                                                                    <asp:FileUpload runat="server" id="uploadRC" accept="application/pdf, .pdf"  ></asp:FileUpload></td>
                                                                               <td>Speed Governor Certificate <span style="color:red">*</span></td>
                                                                               <td>
                                                                                   <asp:FileUpload runat="server" id="uploadSpeed" accept="application/pdf, .pdf"  ></asp:FileUpload></td>
                                                                               </tr>
                                                                            
                                                                           <tr>
                                                                              <td>Insurance Certificate <span style="color:red">*</span></td>
                                                                                <td><asp:FileUpload runat="server" id="uploadInsur" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                           <td>Insurance Certificate Valid Upto <span style="color:red">*</span></td>
                                                                               <td><div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">
                                                                <input type="text" id="txtICValidUpto" class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div></td>    
                                                                           </tr>
                                                                             
                                                                           <tr>
                                                                              <td>Pollution Certificate <span style="color:red">*</span></td>
                                                                                <td><asp:FileUpload runat="server" id="uploadPollution" accept="application/pdf, .pdf" ></asp:FileUpload></td>
                                                                           <td>Pollution Certificate Valid Upto <span style="color:red">*</span></td>
                                                                               <td><div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">
                                                                <input type="text" id="txtPCValidUpto" class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div></td>    
                                                                           </tr>
                                                                           
                                                                            <tr>
                                                                              <td>Fitness Certificate <span style="color:red">*</span></td>
                                                                                <td><asp:FileUpload runat="server" id="uploadFitness" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                            <td>Fitness Certificate Valid Upto <span style="color:red">*</span></td>
                                                                               <td><div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">
                                                                <input type="text" id="txtFCValidUpto" class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div></td>   
                                                                            </tr>
                                                                            <tr><td>Permit Certificate <span style="color:red">*</span></td>
                                                                                <td><asp:FileUpload runat="server" id="uploadPermit" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                 <td></td><td></td></tr>
                                                                                   
                                                                            </table>
                                                                        <br />
                                                                         </div>
                                                                        </div>
                                                                 

                                                                 </div>               
                                                               
                                                            </div>
                                                        </div>
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Transfer of Permit Fee Details
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"> </a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-md-3 col-sm-3"><b>Transfer of Permit Fees :</b></div>
                                                                    <div class="col-md-3 col-sm-3"><asp:Label ID="lblReplacevehicleFees" runat="server"></asp:Label></div>
                                                                    <div class="col-md-3 col-sm-3"></div>
                                                                    <div class="col-md-3 col-sm-3"></div>
                                                                </div>
                                                               
                                                               
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12">
                                                                <div style="text-align:center"><asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Proceed" OnClick="btnSave_Click" OnClientClick="return fnRVApplicationValidate()" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnreset" runat="server" CssClass="btn red" Text="Reset" OnClientClick="return fnClearPermitNew()"   OnClick="btnreset_Click" />
                                                                </div>
                                                                
                                                            </div>
                                                            
                                                        </div>
                                                        
                                                    </div>
                                              </asp:Panel>
                                                         
                                          

                                           
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
    <script src="assets/js/RTA_SCHOOL_BUS_TRANSFER_OF_PERMIT_APPLICATION.js" type="text/javascript"></script>    
    
</asp:Content>

