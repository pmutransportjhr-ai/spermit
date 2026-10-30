<%@ Page Title="Edit Counter Signature Application"
    Language="C#"
    MasterPageFile="MasterPage.master"
    AutoEventWireup="true"
    CodeFile="Edit-CS-Application.aspx.cs"
    Inherits="Edit_CS_Application" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <!-- Bootstrap 3 -->
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <!-- DataTables Bootstrap 3 -->
    <link href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background: #f7f9fb;
            font-size: 13px;
        }

        .page-title {
            font-weight: 700;
            color: #0B6B3A;
            font-size: 18px;
        }

        /* Panel */
        .panel {
            border-radius: 8px;
            box-shadow: 0 5px 15px rgba(0,0,0,.1);
        }

        /* Compact filters */

        .filter-panel .form-group {
            margin-bottom: 10px;
        }

        .filter-panel label {
            font-size: 12px;
            margin-bottom: 3px;
        }

        .filter-panel .form-control {
            height: 30px;
            font-size: 12px;
        }

        /* Table */

        #sample_1 th {
            font-size: 12px;
        }

        #sample_1 td {
            font-size: 12px;
        }

        .table th, .table td {
            vertical-align: middle !important;
        }

        /* nowrap */

        .nowrap {
            white-space: nowrap;
        }

        /* route column */

        .route-col {
            white-space: normal !important;
            min-width: 300px;
        }

        /* action */

        .action-col {
            width: 120px;
        }

        /* datatable */

        .dataTables_wrapper .dataTables_length,
        .dataTables_wrapper .dataTables_filter {
            font-size: 12px;
        }

        .dataTables_wrapper select,
        .dataTables_wrapper input {
            height: 28px;
            font-size: 12px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form runat="server">

        <asp:ScriptManager runat="server" />

        <div class="container-fluid">

            <!-- Breadcrumb -->

            <ol class="breadcrumb">
                <li><a href="STA-DASHBOARD.aspx">Home</a></li>
                <li class="active">Edit Counter Signature Application</li>
            </ol>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <!-- FILTER PANEL -->

                    <div class="panel panel-default filter-panel">
                        <div class="panel-body">

                            <h4 class="page-title">Edit Counter Signature Application</h4>

                            <div class="row">

                                <div class="col-md-4">

                                    <div class="form-group">
                                        <label>State</label>

                                        <asp:DropDownList
                                            ID="ddlDesState"
                                            runat="server"
                                            CssClass="form-control"
                                            AppendDataBoundItems="true">

                                            <asp:ListItem Text="All" Value="0" />

                                        </asp:DropDownList>

                                    </div>
                                </div>


                                <div class="col-md-4">

                                    <div class="form-group">
                                        <label>Status</label>

                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">

                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem>Issued</asp:ListItem>
                                            <asp:ListItem>Renewed</asp:ListItem>
                                            <asp:ListItem>Replaced</asp:ListItem>
                                            <asp:ListItem>Transfered</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>
                                </div>


                                <div class="col-md-4">

                                    <div class="form-group">
                                        <label>Permit Type</label>

                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">

                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem>Permanent</asp:ListItem>
                                            <asp:ListItem>Temporary</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>

                                </div>

                            </div>

                            <div class="text-right">

                                <asp:Button
                                    ID="btnRun"
                                    runat="server"
                                    Text="Run"
                                    CssClass="btn btn-success"
                                    OnClick="btnRun_Click" />

                            </div>

                        </div>
                    </div>


                    <!-- TABLE PANEL -->

                    <div class="panel panel-default"
                        runat="server"
                        id="tblISPermit"
                        visible="false">

                        <div class="panel-body">

                            <div class="table-responsive">

                                <table id="sample_1"
                                    class="table table-bordered table-hover">

                                    <thead>

                                        <tr>

                                            <th class="nowrap">Application No.</th>
                                            <th class="nowrap">Permit Details</th>
                                            <th class="nowrap">Vehicle Details</th>
                                            <th class="nowrap">Owner Details</th>
                                            <th class="nowrap">Validity</th>
                                            <th class="route-col">Route</th>
                                            <th class="nowrap">Status</th>
                                            <th class="nowrap action-col">Action</th>

                                        </tr>

                                    </thead>

                                    <tbody id="tbdInterStatePermitList" runat="server"></tbody>

                                </table>

                            </div>

                        </div>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

    </form>

</asp:Content>


<asp:Content ID="Content3"
    ContentPlaceHolderID="ContentPlaceHolder2"
    runat="Server">

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script src="assets/bootstrap/js/bootstrap.min.js"></script>

    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>

    <script src="https://cdn.datatables.net/1.13.6/js/dataTables.bootstrap.min.js"></script>

    <script>

    function initDataTable() {

                 if ($.fn.DataTable.isDataTable('#sample_1')) {
                     $('#sample_1').DataTable().destroy();
                 }

                 $('#sample_1').DataTable({

                     pageLength: 5,
                     lengthMenu: [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                     autoWidth: false,
                     ordering: true,

                     scrollX: true,
                     scrollY: "55vh",
                     scrollCollapse: true

                 });

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
                     "Edit-Form-CS.aspx?q=" + encodeURIComponent(id),
                     "_blank"
                 );

             }

             function MasterView(id) {

                 window.open(
                     "STA-COUNTER-SIGNATURE-REPORT-VIEW.aspx?q=" + encodeURIComponent(id),
                     "_blank"
                 );

             }

    </script>

</asp:Content>
