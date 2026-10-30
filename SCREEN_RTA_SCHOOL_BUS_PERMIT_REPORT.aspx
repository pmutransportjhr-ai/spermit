<%@ Page Title="" Language="C#" MasterPageFile="~/RTAMasterPage.master" AutoEventWireup="true" CodeFile="SCREEN_RTA_SCHOOL_BUS_PERMIT_REPORT.aspx.cs" Inherits="SCREEN_RTA_SCHOOL_BUS_PERMIT_REPORT" %>

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
                                        <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
                                           
                                            <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                            <asp:UpdatePanel ID="statepanel" runat="server">
                                                <ContentTemplate>
                                              <div id="Panel1">
                                           <div class="portlet-body">
                                             <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>Sr.No</th>
                                                        <th>RTO</th>
                                                        <th>Total Issued Permit</th>
                                                       <%-- <th>Issued Permit</th>--%>
                                                        <th>View</th>
                                                     
                                                    </tr>
                                                </thead>
                                                <tbody id="tbdSta_Details">
                                                 <div id="FillStateDetails" runat="server">

                                                 </div>
                                                </tbody>
                                            </table>
                                            



                                         </div>

                                             

                                                </ContentTemplate>
                                                <Triggers>
                                                  
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </form>

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

     <div class="modal fade" id="showDetails" tabindex="15" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content modal-lg">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <div class="portlet-title">
                        <div class="caption">
                            <span class="caption-subject font-red sbold uppercase"><b>RTA School Bus Permit Details</b> </span>
                        </div>
                    </div>
                     </div>
                <div class="modal-body" style="height: 500px !important; overflow-y: auto !important; overflow-x: auto !important">

                    <table class='table table-striped table-bordered table-hover' id='sample50'>
                        </table>

                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

        </div>
    </div>


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
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
    <script src="assets/js/SCREEN_PERMIT_REPORT.js" type="text/javascript"></script>
</asp:Content>

