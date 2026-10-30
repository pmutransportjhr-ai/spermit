<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-TRANSACTION-REPORT.aspx.cs" Inherits="RTA_TRANSACTION_REPORT" %>

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
    <input id="txtScreen" type="text" value="RTA-TRANSACTION-REPORT.aspx" style="display: none;" />
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
                                <span>RTA Transaction Report</span>
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
                                                <span class="caption-subject font-red sbold uppercase">RTA Transaction Report</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="table-toolbar">
                                                <div class="row">
                                                    
                                                   
                                                        <label class="col-md-3 control-label">RTA<span class="required" aria-required="true">*</span></label>
                                                        <label class="col-md-3 control-label">Services<span class="required" aria-required="true">*</span></label>
                                                        <label class="col-md-6 control-label">Transaction from To</label>
                                                    </div>
                                                    <div class="row">
                                                    
                                                        <div class="col-md-3"><asp:DropDownList ID="ddlRTA" OnSelectedIndexChanged="ddlRTA_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true"  CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="-1">--Select RTA--</asp:ListItem>
                                                            <asp:ListItem Value="RTA APPLICATION">Stage Carriage Permit </asp:ListItem>
                                                            <asp:ListItem Value="GOODS APPLICATION">Goods Carriage Permit</asp:ListItem>
                                                            <asp:ListItem Value="CONTRACT APPLICATION">Contract Carriage Permit</asp:ListItem>
                                                            <asp:ListItem Value="AUTO APPLICATION">Auto Rickshaw Permit</asp:ListItem>
                                                            <asp:ListItem Value="SCHOOL BUS APPLICATION">School Bus Permit</asp:ListItem>
                                                            
                                                            
                                                           
                                                        </asp:DropDownList></div>
                                                      
                                                           <div class="col-md-3"><asp:DropDownList ID="ddlServices" CssClass="form-control select2" runat="server">                                                          
                                                          
                                                           
                                                        </asp:DropDownList></div>
                                                         <div class="col-md-6">
                                                                 <div class="input-group input-large date-picker input-daterange" data-date="10/11/2012" data-date-format="dd/mm/yyyy">
                                                                        <asp:TextBox ID="txtFrom" Width="210px" CssClass="regreadonly form-control" runat="server"></asp:TextBox>
                                                                        <span class="input-group-addon">To </span>
                                                                        <asp:TextBox ID="txtTo" Width="210px" CssClass="regreadonly form-control" runat="server"></asp:TextBox>
                                                                    </div>

                                                     </div>
                                                        </div>
                                                            <br /> <div class="row">
                                                       
                                                     <div class="col-md-3"><div class="btn-group-devided">
                                                                 <asp:Button ID="btnRun"  OnClick="btnRun_Click" OnClientClick="javascript:return fnValidateReport();" runat="server" Text="Run" CssClass="btn green" />
                                                        </div></div>
                                                        <div class="col-md-9"></div>
                                                    
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
                                 <Triggers>
                                                          <asp:AsyncPostBackTrigger ControlID ="ddlRTA" />
                                                         </Triggers>
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

            if ($('select[id*=ddlRTA]').val() == "-1" || $('select[id*=ddlRTA]').val() == "") {
                alert("Please Select RTA");
                return false;

            }
            return true;
        }

    </script>
</asp:Content>

