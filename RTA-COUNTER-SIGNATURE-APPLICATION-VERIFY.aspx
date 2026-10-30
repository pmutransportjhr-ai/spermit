<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-COUNTER-SIGNATURE-APPLICATION-VERIFY.aspx.cs" Inherits="RTA_COUNTER_SIGNATURE_APPLICATION_VERIFY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
  <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="RTA-COUNTER-SIGNATURE-APPLICATION-VERIFY.aspx" style="display: none;" />
    <%--<input type="text" id="txtColCnt" style="display: none;" value="8" />--%>
    
    <form runat="server" autocomplete="off">
    <asp:HiddenField ID="hdnID" runat="server" />
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
                                <span>RTA Counter Signature Application</span>
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
                                                        <span class="caption-subject font-red sbold uppercase">RTA Counter Signature Application</span>
                                                    </div>
                                                </div>
                                     <div class="portlet-body">
                                                        
                                                           <div class="row" >
                                                               <div class="col-md-12">
                                                                    <p style="text-align:center; font-size:large;">
                                                                        Government of Jharkhand
                                                                        <br /><b>Office of Transport Commissioner
                                                                            <br />State Transport Authority, Jharkhand</b>
                                                                   <br />F.F.P. Building,HEC Campus,Dhurwa, Ranchi
                                                                   <br /><b>FORM P.C.S.</b>
                                                                   <br />[See rule 73 (7) (ix)]
                                                                   <br /><b><i>Countersignature of the permit</i></b></p>
                                                               </div>
                                                           </div>
                                                           <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                               <div class="col-md-7">
                                                                    No.&nbsp;<b><label style="font-weight:bold" id="lblAppNumber" runat="server"></label></b>
                                                               </div>
                                                               <div class="col-md-3">
                                                                  Ranchi,dated.&nbsp;<b><label style="font-weight:bold" id="lblAppDt" runat="server"></label></b>
                                                               </div>
                                                                <div class="col-md-1">
                                                                 
                                                               </div>
                                                           </div>
                                                             <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                               <div class="col-md-7">
                                                                   
                                                               </div>
                                                               <div class="col-md-3">
                                                                  Countersignature No.&nbsp;<label style="font-weight:bold" id="lblAppNumber1" runat="server"></label>
                                                               </div>
                                                                <div class="col-md-1">
                                                                 
                                                               </div>
                                                           </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                               <div class="col-md-10">
                                                                    <p>The State Transport Authority, Jharkhand,Ranchi</p>
                                                               </div>
<div class="col-md-1">
                                                                 
                                                               </div>
                                                               
                                                           </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-11">
                                                                    <p>1. Full name of the holder:&nbsp;<label style="font-weight:bold" id="lblName" runat="server"></label> </p>
                                                                    <p>2. Full Residential Address:&nbsp;<label style="font-weight:bold" id="lblResAdd" runat="server"></label> </p>
                                                                    <p>3. Full Business Address :&nbsp;<label style="font-weight:bold" id="lblBuAdd" runat="server"></label> </p>
                                                                    
                                                               </div>
                                                           </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                    <p>&nbsp;&nbsp;&nbsp;&nbsp; Mobile Number:&nbsp;<label style="font-weight:bold" id="lblMobileNumber" runat="server"></label> </p>
                                                                    
                                                               </div>
                                                                <div class="col-md-1">
                                                                 
                                                               </div>
                                                           </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-7">
                                                                    <p>4. Primary Permit No :&nbsp;<label style="font-weight:bold" id="lblPermitNo" runat="server"></label> </p>
                                                                    </div>
                                                                <div class="col-md-3">
                                                                    <p>Valid upto:&nbsp;<label style="font-weight:bold" id="lblValiUpto" runat="server"></label> </p>
                                                                    </div>
                                                                <div class="col-md-1">
                                                                 
                                                               </div>
                                                            </div>
                                                             <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-7">
                                                                    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Issued by :&nbsp;<label style="font-weight:bold" id="lblIssuedBY" runat="server"></label> </p>
                                                                    </div>
                                                                <div class="col-md-4">
                                                                    
                                                                    </div>
                                                            </div>
                                                             <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                    <p>5. Registration Mark of the State Carriage/Contact Carriage/Private/Service Vehicle/Goods Carriage :&nbsp;<label style="font-weight:bold" id="lblRegNo" runat="server"></label> 
                                                                  
                                                                    </p>
                                                                   </div>
                                                                <div class="col-md-1">
                                                                     
                                                                 </div>
                                                            </div>
                                                             <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                      <p>6. The route of the routes or the area of areas for which the permit is countersigned :&nbsp;<label style="font-weight:bold" id="lblRoute" runat="server"></label> </p>
                                                                    <p>7. Validity of countersignature from :&nbsp;<label style="font-weight:bold" id="lblValidityfrom" runat="server"></label> To  &nbsp;<label style="font-weight:bold" id="lblValidityto" runat="server"></label></p>
                                                                    <p>8. Nature of goods to be carried, if any : &nbsp;<label style="font-weight:bold" id="lblNatureofGoods" runat="server"></label></p>
                                                               
                                                                    </div>
                                                                <div class="col-md-1">
                                                                     
                                                                 </div>
                                                            </div>
                                                             <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                    <p>9. Conditions:</p>
                                                                   
                                                                </div>
                                                                <div class="col-md-1">
                                                                 </div>
                                                                 </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                               
                                                                <div class="col-md-10">
                                                                    <ol type="a">
                                                                        <li>This Countersignature is subject to the conditions laid down in section 84 of the act and in rule (76) of rule of the Bihar Motor Vehicle Rules.</li>
                                                                        <li>This counter Signature is also subject to the condition attached to the primary/permit.</li>
                                                                        <li>This counter signature is subject to the Payment to tax of Jharkhand State. </li>
                                                                    </ol>
                                                                        
                                                                 </div>
                                                                 <div class="col-md-1">
                                                                  
                                                                   
                                                                </div>
                                                            </div>
                                                              <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                    <p>10. Timing:</p>
                                                                   
                                                                </div>
                                                                <div class="col-md-1">
                                                                 </div>
                                                                 </div>
                                                             <div class="row" >
                                                               <div class="col-md-2">
                                                                 
                                                               </div>
                                                              
                                                                
                                                                <div class="col-md-9">
                                                                    <div id="RouteTimeInfo" runat="server"></div>
                                                                 </div>
                                                                 <div class="col-md-1">
                                                                    
                                                                   
                                                                </div>
                                                                 </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                     <p>11. The Road Tax of Jharkhand is paid From :&nbsp;<label style="font-weight:bold" id="lblTaxDetails" runat="server"></label> </p>
                                                                    <p>12. Approval of Timing :&nbsp;<label style="font-weight:bold" id="lblApprovalTiming" runat="server"></label> </p>
                                                                   
                                                                </div>
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                                 </div>

                                                            
                                                              <div class="row" >
                                                               <div class="col-md-2">
                                                                 
                                                               </div>
                                                                  <div class="col-md-9">
                                                                      <table style="width:100%;">
                                                                          <tr style="width:100%;">
                                                                               <td style="width:30%;"><span>RC</span></td>
                                                                               <td style="width:70%;"><div id="dvrc" runat="server">Not Uploaded</div></td>
                                                                          </tr>
                                                                          <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                                                          <tr style="width:100%;">
                                                                               <td style="width:30%;"><span>Insurance</span></td>
                                                                               <td style="width:70%;"><div id="dvinsurance" runat="server">Not Uploaded</div></td>
                                                                          </tr>
                                                                          <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                                                          <tr style="width:100%;">
                                                                               <td style="width:30%;"><span>Pollution</span></td>
                                                                               <td style="width:70%;"><div id="dvpolution" runat="server">Not Uploaded</div></td>
                                                                          </tr>
                                                                          <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                                                          <tr style="width:100%;">
                                                                               <td style="width:30%;"><span>Fitness</span></td>
                                                                               <td style="width:70%;"><div id="dvFitness" runat="server">Not Uploaded</div></td>
                                                                          </tr>
                                                                          <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
                                                                          <tr style="width:100%;">
                                                                               <td style="width:30%;"><span>Permit</span></td>
                                                                               <td style="width:70%;"><div id="dvPermit" runat="server">Not Uploaded</div></td>
                                                                          </tr>
                                                                          
                                                                      </table>
                                                                    
                                                                    
                                                                      </div>
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                                 </div>
                                         <br />
                                                                          <div class="row">
                                                                              <div class="col-md-1"></div>
                                                                              <div class="col-md-2">Approval Status</div>
                                                                              <div class="col-md-3">
                                                                                  <asp:DropDownList ID="ddlApplicationStatus" AppendDataBoundItems="true" CssClass="form-control" runat="server">
                                                                                         <%--<asp:ListItem Value="Verified">Verified</asp:ListItem>
                                                                                          <asp:ListItem Value="Rejected">Rejected</asp:ListItem> --%>
                                                                                  </asp:DropDownList>
                                                                              </div>
                                                                              <div class ="col-md-2">
                                                                              
                                                                              </div>
                                                                              <div class ="col-md-2">
                                                                              
                                                                              </div>
                                                                              <div class="col-md-2"></div>

                                                                           </div>
                                                            <div class="row">
                                                        <br /><br />
                                                    <div class="col-md-5"></div>
                                                    <div class="col-md-5">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Verify" OnClientClick="return fnApplicationStatusValidation();"  OnClick="btnSave_Click" />
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
                        <h4 class="modal-title"><b>Are you sure, you want to Verify counter signature?</b></h4>
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
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
    
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>

    


</asp:Content>

