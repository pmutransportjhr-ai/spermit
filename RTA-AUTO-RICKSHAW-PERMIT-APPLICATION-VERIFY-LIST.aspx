<%@ Page Title="" Language="C#" MasterPageFile="~/RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-AUTO-RICKSHAW-PERMIT-APPLICATION-VERIFY-LIST.aspx.cs" Inherits="RTA_AUTO_RICKSHAW_PERMIT_APPLICATION_VERIFY_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

     <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="RTA-AUTO-RICKSHAW-PERMIT-APPLICATION-VERIFY-LIST.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="20" />

    <form runat="server" autocomplete="off">
<asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                            
                                            
                                         
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="RTA-DASHBOARD.aspx">HOME</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>RTA AUTO RICKSHAW PERMIT - APPLICATION</span>
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
                                                <span class="caption-subject font-red sbold uppercase">RTA AUTO RICKSHAW PERMIT - APPLICATION</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="table-toolbar">
                                                <div class="row">
                                                    
                                                   
                                                    <label class="col-md-3 control-label">Status<span class="required" aria-required="true">*</span></label>
                                                    <label class="col-md-3 control-label">Type<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-3"></div>
                                                    </div>
                                                    <div class="row">
                                                   
        <div class="col-md-3"><asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="Applied">
                                                Applied
                                                            </asp:ListItem>
            
                                                           
            <asp:ListItem Value="Rejected"> 
                                                Rejected
                                                            </asp:ListItem>
            <asp:ListItem Value="Verified">
                                                Verified
                                                            </asp:ListItem>
            
             <asp:ListItem Value="Issued">
                                                Issued
                                                            </asp:ListItem>
                                                        </asp:DropDownList></div>
                                                        <div class="col-md-3"><asp:DropDownList ID="ddlType" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                             <asp:ListItem Value="Permanent">
                                                Permanent
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="Temporary">
                                                Temporary
                                                            </asp:ListItem>
                                                           
                                                        </asp:DropDownList></div>
                                                         <div class="col-md-3">
                                                             <div class="btn-group-devided">
                                                                 <asp:Button ID="btnRun"  OnClick="btnRun_Click" OnClientClick="javascript:return fnPermitStatusList();" runat="server" Text="Run" CssClass="btn green" />
                                                            

                                                        </div>
                                                         </div>
                                                        
                                                    
                                                </div><br /><br />
                                            </div>
                                            <div id="tblISPermit" runat="server" visible="false">
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>Application No.</th>
                                                        <th>Permit Type</th>
                                                        <th>Name</th>
                                                        <th>Registration No.</th>
                                                    
                                                        <th>Application FEE</th>
                                                        <th>Application FEE Status</th>
                                                        <th>Application FEE Transaction Id</th>
                                                    
                                                        <th>Permit FEE</th>
                                                   
                                                        <th>Permit FEE Status</th>
                                                        <th>Permit FEE Transaction Id</th>
                                                        <th>Valid From</th>
                                                        <th>Valid Upto</th>
                                                        <th>Permit No.</th>
                                                 
                                                          <th>Application Pending on</th>                                                          
                                                        <th>Application Status</th>
                                                        <th>View</th>
                                                        <th>Action</th>
                                                      
                                                       
                                                     </tr>
                                                </thead>
                                                <tbody id="tbdInterStatePermitList">
                                                        <div id="FillSTAPermitApplication" runat="server"></div>
                                                </tbody>
                                            </table>
                                                
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
    <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title"><b>Are you sure, you want to take Action?</b></h4>
                        <br />
                    </div>
                    <div class="modal-body">
                           <div class="row">
                            <div class="col-md-4"><span><b>Action</b><span class="required" style="color:red">*</span></span></div>
                            <div class="col-md-8">
                            <select id="ddlPermitAction" name="ddlPermitAction" onchange="javascript:fnPermitAction();" class="form-control select2" runat="server">

                                                            <option value="0">--select action--</option>
                                                           
                                                            <option value="Rejected">Rejected</option>                           
                                                            <option value="Verified">Verified</option>
                                                            
                                                        </select>
                                </div>
                               </div><br /><br />

                                 <div class="row">
                                      <div id="DivActionPublish" runat="server" style="display:none">
                           
                            <div class="col-md-4"><span><b>Objection Date</b><span class="required" style="color:red">*</span></span></div>
                            <div class="col-md-8">
                                <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+7d">
                                                                <input type="text" id="txtObjectionDt" class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div>
                                <%--<asp:TextBox ID="txtObjectionDt" CssClass="regreadonly form-control date-picker" data-date-start-date="+7d" data-date-format="dd/mm/yyyy" runat="server"></asp:TextBox>--%></div>
                            </div>
                                </div>
                                <div class="row">
                                      <div id="DivActionReject" runat="server" style="display:none">
                           
                            <div class="col-md-4"><span><b>Comments</b><span class="required" style="color:red">*</span></span></div>
                            <div class="col-md-8">
                                
                                <asp:TextBox ID="txtCommentsReject" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                              
</div>
                                </div> </div>
                                    <br />
                              <div class="row">
                                     <div id="dvAstComments" style="display:none">
                                         
                                        <div class="col-md-4">
                                            <span><b>Assistant Comments</b></span>
                                        </div>
                                          <div class="col-md-8">
                                                <label  id="lblAstComments"></label>
                                        </div>
                                     </div>
                                  </div>
                                  <br />
                                  <div class="row">
                                   <div id="dvSOComments"  style="display:none">
                                       
                                        <div class="col-md-4">
                                            <span><b>SO Comments</b></span>
                                        </div>
                                          <div class="col-md-8">
                                                <label id="lblSOComments"></label>
                                        </div>
                                     </div>
                              </div>
                       
                            
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="txtDelKey" runat="Server" Value="0" />
                        <asp:HiddenField ID="txtPermitStatus" runat="Server" Value="" />
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnAction" runat="server" CssClass="btn red" OnClientClick="javascript:return fnValidateAction();" Text="YES" OnClick="btnAction_Click" />
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
    <script src="assets/pages/scripts/table-datatables-scroller.js" type="text/javascript"></script>

     <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="assets/global/plugins/moment.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>

    <script src="assets/js/RTA-AUTO-RICKSHAW-PERMIT-APPLICATION-VERIFY.js" type="text/javascript"></script>


</asp:Content>

