<%@ Page Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-STAGE-CARRIAGE-EDIT.aspx.cs" Inherits="RTA_STAGE_CARRIAGE_EDIT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />
    <style>
        .dashboard-container {
            margin-top: 20px;
        }

        .modern-breadcrumb {
            background: transparent;
            font-size: 13px;
        }

        .modern-input {
            height: 45px;
            border-radius: 10px;
            border: 1px solid #ddd;
            padding: 8px;
        }

        .modern-btn {
            height: 45px;
            border-radius: 10px;
            font-weight: 600;
        }

        .modern-table {
            border-radius: 10px;
            overflow: hidden;
        }

            .modern-table thead {
                background: #4e73df;
                color: white;
            }

            .modern-table tbody tr:hover {
                background-color: #f5f7ff;
                transition: 0.2s;
            }

        .card {
            border-radius: 15px;
            border: none;
        }

        /* Prevents the table from bleeding out of the panel */
        .dataTables_wrapper {
            width: 100% !important;
            margin: 0 auto;
        }

        /* Fixes the header misalignment common in scrolled tables */
        .dataTables_scrollHeadInner {
            width: 100% !important;
        }

            .dataTables_scrollHeadInner table {
                width: 100% !important;
            }

        /* Custom scrollbar styling for a cleaner look */
        .dataTables_scrollBody::-webkit-scrollbar {
            width: 8px;
            height: 8px;
        }

        .dataTables_scrollBody::-webkit-scrollbar-thumb {
            background: #ccc;
            border-radius: 10px;
        }

        /* Fix for the header misalignment */
        .dataTables_scrollHeadInner,
        .dataTables_scrollHeadInner table {
            width: 100% !important;
        }

        /* Ensure the table doesn't overflow its parent container */
        .dataTables_wrapper {
            width: 100% !important;
            margin: 0 auto;
        }

        /* Optional: Improve the look of the badge since your C# uses 'badge-success' etc. */
        .badge {
            padding: 5px 10px;
            border-radius: 4px;
            font-size: 11px;
        }

        .badge-success {
            background-color: #28a745;
            color: #fff;
        }

        .badge-danger {
            background-color: #dc3545;
            color: #fff;
        }

        .badge-warning {
            background-color: #ffc107;
            color: #000;
        }

        .badge-secondary {
            background-color: #6c757d;
            color: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />

        <div class="container dashboard-container">


            <!-- Breadcrumb -->
            <ol class="breadcrumb modern-breadcrumb">
                <li><a href="RTA-DASHBOARD.aspx">Home</a></li>
                <li class="active">Rta-Stage-Carriage-Edit</li>
            </ol>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="panel panel-default">
                        <div class="panel-body">

                            <div class="row">

                                <!-- RTO -->
                                <div class="col-md-4 col-sm-6">
                                    <label><b>RTO</b></label>
                                    <asp:DropDownList ID="ddlRTO"
                                        CssClass="form-control"
                                        runat="server"
                                        DataTextField="RTO_NAME"
                                        DataValueField="RTO_CODE">
                                    </asp:DropDownList>
                                </div>



                                <!-- Button -->
                                <div class="col-md-4 col-sm-12 text-right" style="margin-top: 25px;">
                                    <asp:Button ID="btnRun"
                                        runat="server"
                                        Text="Search"
                                        OnClick="btnRun_Click"
                                        CssClass="btn btn-primary" />
                                </div>

                            </div>

                        </div>
                    </div>

                    <!-- TABLE SECTION -->
                    <div class="panel panel-default" id="tblISPermit" runat="server" visible="false">

                        <div class="panel-heading">
                            <b>Application List</b>
                        </div>

                        <div class="panel-body">

                            <div class="table-responsive">
                                <table class="table table-bordered table-striped table-hover" id="sample_1">

                                    <thead style="background: #337ab7; color: white;">
                                        <tr>
                                            <th>Application No.</th>
                                            <th>Name</th>
                                            <th>Registration No.</th>
                                            <th>Valid From</th>
                                            <th>Valid Upto</th>
                                            <th>Permit No.</th>
                                            <th>Status</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        <asp:Literal ID="FillSTAPermitApplication" runat="server"></asp:Literal>
                                    </tbody>

                                </table>
                            </div>

                        </div>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

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

    <script src="assets/js/RTA-PERMIT-APPLICATION-VERIFY.js" type="text/javascript"></script>

    <script>
        function initDataTable() {
            // 1. Destroy existing instance if it exists
            if ($.fn.DataTable.isDataTable('#sample_1')) {
                $('#sample_1').DataTable().destroy();
            }

            // 2. Initialize
            var table = $('#sample_1').DataTable({
                pageLength: 5,
                lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                autoWidth: false,
                ordering: true,
                scrollX: true,      // Essential for horizontal scrolling
                scrollY: "50vh",    // Vertical scroll height
                scrollCollapse: true,
                // Force the table to take up 100% width
                "initComplete": function (settings, json) {
                    $(this).DataTable().columns.adjust();
                }
            });

            // 3. Small timeout to ensure the DOM has fully rendered the new panel visibility
            setTimeout(function () {
                table.columns.adjust().draw();
            }, 100);
        }

        Sys.WebForms.PageRequestManager
            .getInstance()
            .add_endRequest(function () {

                if ($('#sample_1').length) {
                    initDataTable();
                }

            });

        function MasterEdit(id) {

            window.open(
                "STAGE-CARRIAGE-EDIT-FORM.aspx?q=" + encodeURIComponent(id),
                "_blank"
            );

        }

        function MasterView(id) {

            window.open(
                "RTA-PERMIT-APPLICATION-VIEW.aspx?q=" + encodeURIComponent(id),
                "_blank"
            );

        }
    </script>

</asp:Content>
