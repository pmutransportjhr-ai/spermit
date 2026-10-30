<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA-CS-APPLICATION-UPDATE.aspx.cs" Inherits="STA_CS_APPLICATION_UPDATE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
     <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-CS-APPLICATION-UPDATE.aspx" style="display: none;" />
    <%--<input type="text" id="txtColCnt" style="display: none;" value="8" />--%>
    
<form id="FormID1" enctype="multipart/form-data" runat="server" class="form-horizontal" autocomplete="off" method="post">


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <asp:HiddenField ID="hdnID" runat="server" />
            
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
                                <span>Counter Signature Application - Update</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <input id="txtroutjson" type="text" runat="server" style="display:none" />
                                     <div class="row">
                                        <div class="col-md-12">
                                            <div class="portlet light portlet-fit">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="icon-settings font-red"></i>
                                                        <span class="caption-subject font-red sbold uppercase">Counter Signature Application - Update</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                   
                                                    <div class="row">
                                                        <div class="col-md-6 col-sm-6">
                                                                <b style="font-size:large">Application No :</b>
                                                            <asp:HiddenField ID="EncryptedParameter" runat="server"></asp:HiddenField>
                                                                    <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                            </div>

                                                                 <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                    <%--<b  style="font-size:large">Permit Type : </b>--%>
                                                                        <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                            </div><br /><br />
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
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Vehicle Details</u></b></h4></div>
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    
                                                                    <td><b>Registration No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VORegNumber" runat="server"></asp:Label></td>
                                                                    <td><b>Permit Number:</b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblPermitNnumber" runat="server"></asp:Label></td>
                                                                   
                                                                </tr>
                                                                <tr>
                                                                    
                                                                    <td><b>Permit Valid From : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblPermitfrom" runat="server"></asp:Label></td>
                                                                   <td><b>Permit Valid Upto:</b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblPermitUpto" runat="server"></asp:Label></td>
                                                                </tr>
                                                                
                                                            </table>

                                                        </div>
                                                    </div>
                                                    
                                                    <div id="RouteTimeInfo" runat="server"></div>
                                                    
                                                      <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Uploaded Documents</u></b></h4></div>
                                                          <input id="HFNoOftrips" type="text" runat="server" style="display:none" />
                                                           <div class="col-md-12 col-sm-12">
                                                               <div id="uploadDocuments"> 
                                                                    <div class="row">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                           <tr>
                                                                               <td>Registration Certificate (RC) <span style="color:red">*</span></td>
                                                                               <td><asp:FileUpload runat="server" id="uploadRC" accept="application/pdf, .pdf" required ></asp:FileUpload></td>
                                                                               <td>Speed Government Certificate <span style="color:red">*</span></td>
                                                                               <td><asp:FileUpload runat="server" id="uploadSpeed" accept="application/pdf, .pdf" required ></asp:FileUpload></td>
                                                                           </tr>
                                                                            
                                                                           <tr>
                                                                               <td>Insurance Certificate <span style="color:red">*</span></td>
                                                                               <td><asp:FileUpload runat="server" id="uploadInsur" accept="application/pdf, .pdf" required></asp:FileUpload></td>
                                                                               <td>Permit Certificate <span style="color:red">*</span></td></td>
                                                                               <td><asp:FileUpload runat="server" id="uploadPermit" accept="application/pdf, .pdf" required ></asp:FileUpload></td>
                                                                           </tr>
                                                                             
                                                                           <tr>
                                                                               <td>Pollution Certificate <span style="color:red">*</span></td>
                                                                               <td><asp:FileUpload runat="server" id="uploadPollution" accept="application/pdf, .pdf" required></asp:FileUpload></td>
                                                                               <td>Time Table <span style="color:red">*</span></td>
                                                                               <td><asp:FileUpload runat="server" id="uploadTimetable" accept="application/pdf, .pdf" required ></asp:FileUpload></td>
                                                                           </tr>
                                                                           
                                                                           <tr>
                                                                               <td>Fitness Certificate <span style="color:red">*</span></td>
                                                                               <td><asp:FileUpload runat="server" id="uploadFitness" accept="application/pdf, .pdf" required></asp:FileUpload></td>
                                                                               <td>Recommendatation Letter <span style="color:red">*</span></td>
                                                                               <td><asp:FileUpload runat="server" id="uploadLetter" accept="application/pdf, .pdf" required ></asp:FileUpload></td>                                                                          
                                                                           </tr>
                                                                           <tr>
                                                                               <td>Tax Token <span style="color:red">*</span></td>
                                                                               <td><asp:FileUpload runat="server" id="uploadTax" accept="application/pdf, .pdf" required></asp:FileUpload></td>
                                                                           </tr>
                                                                        </table>
                                                                        <br />
                                                                         </div>
                                                                        </div>
                                                                        

                                                                 </div>

                                                           
                                                               
                                                    </div>
                                                  </div>
                                                   
                                                     <%--<div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Application Fee Details</u></b></h4></div>
                                                        <div class="col-md-3 col-sm-3"><b>Application Fees :</b></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblPermitFees" runat="server"></asp:Label></div>
                                                        <div class="col-md-3 col-sm-3"><b>Application Payment Status:</b></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblPaymentStatus" runat="server"></asp:Label></div>
                                                    </div>--%>
                             
                                            </div>
                                    </div>
                      
                                                  </div>
                                               <div class="row">
                                                        <div class="col-md-4 col-sm-4"></div>
                                                        <div class="col-md-4 col-sm-4">
                                                            <div style="text-align: center">
                                                                <%--<asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="btn green" Text="Make Payement" /></div>--%>
                                                                <input type="button" class="btn green" value="Update" onclick="javascript: ShowUpdateConfirmModal();" />

                                                        </div>
                                                        <div class="col-md-4 col-sm-4"></div>
                                                    </div>
                                                </div>
                                                <div class="modal fade" id="confirm" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                                                <h4 class="modal-title"><b>Are you sure, you want to update the details ?</b></h4>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn dark btn-outline" data-dismiss="modal">NO</button>
                                                                <asp:Button ID="btnYes" runat="server" CssClass="btn green" Text="YES" OnClientClick="return ()" OnClick="btnYes_Click" />
                                                            </div>
                                                        </div>
                                                        <!-- /.modal-content -->
                                                    </div>
                                                    <!-- /.modal-dialog -->
                                                </div>
                                    </div>
                                
                                </ContentTemplate>
                                <triggers>
                                                   
                                                    <asp:PostBackTrigger ControlID ="btnYes" />
                                                </triggers>
                            </asp:UpdatePanel>
                            
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
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
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
   <script src="assets/js/STA_CS_APPLICATION_UPDATE.js" type="text/javascript"></script>
      <script src="assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js" type="text/javascript"></script>
     <script src="assets/js/form-input-mask1.js" type="text/javascript"></script>
 
   


</asp:Content>

