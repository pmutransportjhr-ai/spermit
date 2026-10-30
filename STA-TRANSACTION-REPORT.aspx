<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-TRANSACTION-REPORT.aspx.cs" Inherits="STA_TRANSACTION_REPORT" %>

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
    <input id="txtScreen" type="text" value="STA-TRANSACTION-REPORT.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="6" />

    <form runat="server" autocomplete="off">
<asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                            
                                            
                                         
        <div class="page-container">
            <div class="page-content-wrapper">+
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="STA-DASHBOARD.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>STA Transaction Report</span>
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
                                                <span class="caption-subject font-red sbold uppercase">STA Transaction Report</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="table-toolbar">
                                                <div class="row">
                                                    
                                                    <label class="col-md-3 control-label">Services<span class="required" aria-required="true">*</span></label>
                                                   <%-- <label class="col-md-3 control-label">Status<span class="required" aria-required="true">*</span></label>
                                                    <label class="col-md-3 control-label">Type<span class="required" aria-required="true">*</span></label>--%>
                                                    <label class="col-md-6 control-label">Transaction from To</label>
                                                   
                                                    <div class="col-md-2">Status</div>
                                                    <div class="col-md-1"></div>
                                                    
                                                    </div>
                                                    <div class="row">
                                                           <div class="col-md-3"><asp:DropDownList ID="ddlServices" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="STA APPLICATION">Apply Permit Application</asp:ListItem>
                                                            <asp:ListItem Value="STA PERMIT ISSUE">Permit Application</asp:ListItem>
                                                            <asp:ListItem Value="STA PERMIT OBJECTION PERMIT">Objection Application</asp:ListItem>
                                                            <asp:ListItem Value="STA PERMIT DUPLICATE">Duplicate Permit Application</asp:ListItem>
                                                            <asp:ListItem Value="STA PERMIT RENEWAL">Renewal Permit Application Apply</asp:ListItem>
                                                            <asp:ListItem Value="STA RENEWAL PERMIT ISSUE">Renewal Permit Application</asp:ListItem>
                                                            <asp:ListItem Value="STA PERMIT REPLACEMENT OF VEHICLE">Replacement of Vehicle Application</asp:ListItem>
                                                            <asp:ListItem Value="STA PERMIT TRANSFER OF PERMIT">Transfer of Permit Application</asp:ListItem>
                                                            <asp:ListItem Value="STA ROUTE EXTENSION">Route Extension Application</asp:ListItem>
                                                            <asp:ListItem Value="STA COUNTER SIGNATURE TAX PAY">Counter Signature Tax Payment</asp:ListItem>
                                                            <asp:ListItem Value="STA CS APPLICATION">Apply Counter Signature Application</asp:ListItem>
                                                               <asp:ListItem Value="STA APPLICATION CSUPLOAD">Apply Counter Signature Application(Time Table Extension)</asp:ListItem>
                                                            <asp:ListItem Value="STA PERMIT OBJECTION CS">Objection Counter Signature Application</asp:ListItem>
                                                            <asp:ListItem Value="STA COUNTER SIGNATURE DUPLICATE">Duplicate Counter Signature Application</asp:ListItem>
                                                            <asp:ListItem Value="STA COUNTER SIGNATURE RENEWAL">Renewal Counter Signature Application</asp:ListItem>
                                                            <asp:ListItem Value="STA COUNTER SIGNATURE REPLACEMENT OF VEHICLE">Replacement of Vehicle Counter Signature Application</asp:ListItem>
                                                            <asp:ListItem Value="STA COUNTER SIGNATURE TRANSFER">Transfer of Counter Signature Application</asp:ListItem>
                                                          
                                                           <%--STA APPLICATION CSUPLOAD--%>
                                                        </asp:DropDownList></div>
                                                         <div class="col-md-6">
                                                                 <div class="input-group input-large date-picker input-daterange" data-date="10/11/2012" data-date-format="dd/mm/yyyy">
                                                                        <asp:TextBox ID="txtFrom" Width="210px" CssClass="regreadonly form-control" runat="server"></asp:TextBox>
                                                                        <span class="input-group-addon">To </span>
                                                                        <asp:TextBox ID="txtTo" Width="210px" CssClass="regreadonly form-control" runat="server"></asp:TextBox>
                                                                    </div>

                                                     </div>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="SUCCESS">Success</asp:ListItem>
                                                            <asp:ListItem Value="Fail">Fail</asp:ListItem>
                                                              <asp:ListItem Value="BOOKED">Booked</asp:ListItem>
                                                          
                                                           
                                                        </asp:DropDownList>

                                                        </div>
                                                     <div class="col-md-1"><div class="btn-group-devided">
                                                                 <asp:Button ID="btnRun"  OnClick="btnRun_Click" OnClientClick="javascript:return fnValidateReport();" runat="server" Text="Run" CssClass="btn green" />
                                                        </div></div>
                                                        
                                                    
                                                </div> 
                                                
                                               
                                            </div><br /><br /><br />
                                            <div id="tblISPermit" runat="server" visible="false">
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>Application No.</th>
                                                        <th>Name</th>
                                                        <th>Transaction Amount</th>
                                                        <th>Transaction Id</th>
                                                        <th>Transaction On</th>                                                        
                                                        <th>Status</th>
                                                        <th>E-Challan</th>
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

    <script type="text/javascript">
        function fnValidateReport()
        {

            //if ($('select[id*=ddlServices]').val() == "-1" || $('select[id*=ddlServices]').val() == "") {
            //    alert("Please Select Services");
            //    return false;

            //}
            return true;
        }

    </script>
</asp:Content>

