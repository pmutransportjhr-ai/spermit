<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-COUNTER-SIGNATURE-REPORT.aspx.cs" Inherits="STA_COUNTER_SIGNATURE_REPORT" %>

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
    <input id="txtScreen" type="text" value="STA-PERMIT-REPORT.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="13" />

    <form runat="server" autocomplete="off">
<asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                            
                                            
                                         
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="STA-DASHBOARD.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>STA Counter Signature Report</span>
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
                                                <span class="caption-subject font-red sbold uppercase">STA Counter Signature Report</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="table-toolbar">
                                                <div class="row">
                                                    
                                                    <label class="col-md-3 control-label">State<span class="required" aria-required="true">*</span></label>
                                                    <label class="col-md-3 control-label">Status<span class="required" aria-required="true">*</span></label>
                                                    <label class="col-md-3 control-label">Type<span class="required" aria-required="true">*</span></label>
                                                    
                                                    <div class="col-md-3"></div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-3"><asp:DropDownList ID="ddlDesState" AppendDataBoundItems="true" CssClass="form-control select2" runat="server">
                                                            </asp:DropDownList></div>
                                                             <div class="col-md-3"><asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="Issued">Issued</asp:ListItem>
                                                            <asp:ListItem Value="Renewed">Renewed</asp:ListItem>
                                                            <asp:ListItem Value="Replaced">Replaced</asp:ListItem>
                                                            <asp:ListItem Value="Surrender">Surrender</asp:ListItem>
                                                             <asp:ListItem Value="Duplicate Issued">Duplicate Issued</asp:ListItem>
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
                                                         <div class="col-md-3"><div class="btn-group-devided">
                                                                 <asp:Button ID="btnRun"  OnClick="btnRun_Click" OnClientClick="javascript:return fnValidateReport();" runat="server" Text="Run" CssClass="btn green" />
                                                        </div></div>
                                                        
                                                    
                                                </div> 
                                                <br />
                                                <%-- <div class="row">
                                                    
                                                    <label class="col-md-6 control-label">Date from To<span class="required" aria-required="true">*</span></label>
                                                     <div class="col-md-3"></div>
                                                   
                                                    <div class="col-md-3"></div>
                                                 </div>
                                                 <div class="row">
                                                     <div class="col-md-6">
                                                                 <div class="input-group input-large date-picker input-daterange" data-date="10/11/2012" data-date-format="dd/mm/yyyy">
                                                                        <asp:TextBox ID="txtFrom" Width="210px" CssClass="regreadonly form-control" runat="server"></asp:TextBox>
                                                                        <span class="input-group-addon">To </span>
                                                                        <asp:TextBox ID="txtTo" Width="210px" CssClass="regreadonly form-control" runat="server"></asp:TextBox>
                                                                    </div>

                                                     </div>
                                                     
                                                     <div class="col-md-3">
                                                     </div>
                                                 </div>--%>
                                                
                                                <br /><br />
                                            </div>
                                            <div id="tblISPermit" runat="server" visible="false">
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>Application No.</th>
                                                        <th>Permit Type</th>
                                                        <th>Name</th>
                                                        <th>Mobile No.</th>
                                                        <th>Registration No.</th>
                                                        <th>Permit No.</th>
                                                        <th>Route</th>
                                                        <th>Fee Tax Amount</th>
                                                        <th>Tax Paid From</th>
                                                        <th>Tax Paid Upto</th>
                                                        <th>Transaction ID</th>
                                                        <th>Valid From</th>
                                                        <th>Valid Upto</th>
                                                        <th>Application Status</th>

                                                        <th>View</th>
                                                       <th>View Issued Receipt</th>
                                                        <th>Time Table</th>
                                                        <%-- <th>View Time Table</th>
                                                        <th>View Forwarding Letter</th>--%>
                                                        
                                                    </tr>
                                                </thead>
                                                <tbody id="tbdInterStatePermitList">
                                                    <div id="divrenewalDetail" runat="server"></div>
                                                </tbody>
                                            </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
    <div class="modal fade" id="PaymentDetail" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    
                  
                  <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title"><b>History of Tax Payment</b></h4>
                        <br />
                    </div>
                        <div class="modal-body" style=" height: 400px !important;overflow-y:auto !important; overflow-x:auto !important">
                            <div class="row">
                            <div id="Tbody1" runat="server"></div>
                        </div>
                            </div>

                            
                            
                  
                   <div class="modal-footer">
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">CLOSE</button>
                    </div>
                
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div></div>
   
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

    <script src="assets/js/STA-COUNTER-SIGNATURE-REPORT.js" type="text/javascript"></script>
</asp:Content>

