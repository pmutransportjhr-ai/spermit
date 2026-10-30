<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-CONTRACT-PERMIT-APPLICATION-VERIFY.aspx.cs" Inherits="RTA_CONTRACT_PERMIT_APPLICATION_VERIFY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="RTA-AUTO-RICKSHAW-PERMIT-APPLICATION-VERIFY.aspx" style="display: none;" />
    <%--<input type="text" id="txtColCnt" style="display: none;" value="8" />--%>

    <form runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />



        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="RTA-DASHBOARD.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>RTA Auto Rickshaw Permit Application Verify</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="portlet light portlet-fit">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="icon-settings font-red"></i>
                                                        <span class="caption-subject font-red sbold uppercase">RTA Auto Rickshaw Permit Application Verify</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                   
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12">
                                                            <asp:HiddenField ID="ID" runat="server" />
                                                        </div>
                                                        <div class="col-md-12 col-sm-12"><b style="font-size:large">RTO :</b>
                                                                              <asp:Label ID="lblRTO" runat="server"></asp:Label></div>
                                                            <br /><br />     
                                                        <div class="col-md-6 col-sm-6">
                                                                <b style="font-size:large">Application No :</b>
                                                                    <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                            </div>
                                                                        <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                    <b  style="font-size:large">Permit Type : </b>
                                                                        <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                            </div><br />
                                                        
                                                        
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

                                                                <tr>
                                                                    <td><b>Mobile No: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOMobileNo" runat="server"></asp:Label><span id=""></span> </td>
                                                                    <td></td>
                                                                    <td> </td>

                                                                </tr>

                                                            </table>

                                                        </div>
                                                    </div>
                                                   
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Vehicle Details</b></u></h4></div>
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    
                                                                    <td><b>Registration No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VORegNumber" runat="server"></asp:Label></td>
                                                                   <td></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Chassis Number : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VChassisNumber" runat="server"></asp:Label></td>
                                                                    <td><b>Engine Number  : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VEngineNumber" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Tax Paid UpTo: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VTax" runat="server"></asp:Label></td>
                                                                    <td><b>Fitness Valid Upto: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VFitness" runat="server"></asp:Label></td>

                                                                </tr>
                                                                <tr>

                                                                    <td><b>Vehicle Class Type:</b></td>
                                                                    <td>
                                                                        <asp:Label ID="VType" runat="server"></asp:Label></td>
                                                                    <td><b>Insurance Company: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VInsuranceCmp" runat="server"></asp:Label></td>

                                                                </tr>
                                                                <tr>
                                                                    <td><b>Insurance Policy No: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VInsuranceNo" runat="server"></asp:Label></td>

                                                                    <td><b>Insurance Upto: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VInsuranceUpto" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <td><b>Seating Capacity: </b></td>
                                                                <td>
                                                                    <asp:Label ID="VSeatingCapacity" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <b>Vehical Agreement With:
                                                                        
                                                                    </b>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="VAgreementWith" runat="server"></asp:Label>
                                                                </td>
                                                            </table>

                                                        </div>
                                                    </div>
                                                    
                                                   
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12">
                                                            <h4><b><u>Auto Rickshaw Permit Fee Details</b></u></h4>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3"><b>Auto Rickshaw Permit Fees:</b></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblPermitFees" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3"><b>Payment Status:</b></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblPaymentStatus" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                                                                       
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12">
                                                            <h4><b><u>Uploaded Documents</b></u></h4>
                                                        </div>

                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    <td><b>Registration Certificate</b></td>
                                                                    <td>
                                                                        <div id="divrcfile" runat="server">Not Uploaded</div>


                                                                        <td><b>Insurance Certificate</b></td>
                                                                        <td>
                                                                            <div id="divinsurance" runat="server">Not Uploaded</div>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Pollution Certificate</b></td>
                                                                    <td>
                                                                        <div id="divpollution" runat="server">Not Uploaded</div>



                                                                        <td><b>Fitness Certificate</b></td>
                                                                        <td>
                                                                            <div id="divfitness" runat="server">Not Uploaded</div>


                                                                            <td>
                                                                </tr>

                                                                <tr>
                                                                    <td><b>Agreement Copy</b></td>
                                                                    <td>
                                                                        <div id="divagreement" runat="server">Not Uploaded</div>



                                                                        <td></td>
                                                                        <td>



                                                                            <td>
                                                                </tr>

                                                            </table>

                                                        </div>
                                                    </div>
                                                    <input type="hidden" id="hdnapplicatiostatus" />
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Application Status</b></u></h4></div>
                                                    
                                                           <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    <td><b>Verified/Rejected</b></td>
                                                                    <td><b> <asp:Label ID="lblapplicationstatus" runat="server"></asp:Label></b>
                                                                        <asp:DropDownList runat="server" id="ddlApplicationStatus" >
                                                                       <asp:ListItem Value="0">--select application status--</asp:ListItem>
                                                            <asp:ListItem Value="Verified">Verified</asp:ListItem>
                                                            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                                                   </asp:DropDownList>
                                                                       
                                                                    </td>
                                                                    <td>
                                                                   <label class="control-label" id="lblobjection" runat="server" ><b>Objection till: </b><span class="required" aria-required="true">*</span></label>
                                                                
                                                                
                                                                    </td>
                                                                    <td>
                                                                     <asp:TextBox ID="txtObjectionDate" CssClass="regreadonly form-control date-picker" data-date-format="dd/mm/yyyy" runat="server"></asp:TextBox>
                                                                    </td>

                                                                    </tr>
                                                                
                                                                
                                                                  </table>
                                                               </div>
                                                       </div>
                                                    

                                                    <div class="row">
                                                        <br /><br />
                                                    <div class="col-md-5"></div>
                                                    <div class="col-md-5">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Submit" OnClick="btnSave_Click"  OnClientClick="return fnApplicationStatusValidation()" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Back" OnClick="btnCancel_Click" />

                                                    </div>
                                                    <div class="col-md-2"></div>
                                                </div>
                                                </div>
                                            </div>
                                    </div>
                            </div>
                                    <div class="modal fade" id="confirm" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title"><b>Are you sure, you want to verify/reject this application?</b></h4>
                    </div>
                    <div class="modal-footer">
                         <button type="button" class="btn dark btn-outline" data-dismiss="modal">NO</button>
                        <asp:Button ID="btnYes" runat="server" CssClass="btn green" Text="YES" OnClick="btnYes_Click"  />
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
                                </ContentTemplate>
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
    
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>
    <script src="assets/js/RTA-AUTO-RICKSHAW-APPLICATION-VERIFY.js" type="text/javascript"></script>
   
</asp:Content>

