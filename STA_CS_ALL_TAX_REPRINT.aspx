<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="JHTRANSPORT.master"
    CodeFile="STA_CS_ALL_TAX_REPRINT.aspx.cs"
    Inherits="STA_CS_ALL_TAX_REPRINT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <!-- Bootstrap 3 + DataTables -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" rel="stylesheet" />
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

        .panel {
            border-radius: 8px;
            box-shadow: 0 5px 15px rgba(0,0,0,.15);
        }

        .filter-panel .panel-body {
            padding: 12px 16px;
        }

        .form-control {
            height: 30px;
            font-size: 12px;
        }

        #sample_1 th, #sample_1 td {
            font-size: 12px;
            padding: 6px 8px;
            vertical-align: middle;
        }

        .nowrap {
            white-space: nowrap;
        }

        .route-col {
            white-space: normal !important;
            word-break: break-word;
            min-width: 350px;
            max-width: 450px;
        }

        .action-col {
            width: 140px;
        }

        #sample_1 {
            min-width: 1200px;
        }

        .notice-box {
            display: flex;
            gap: 10px;
            background: #fff3cd;
            border-left: 5px solid #ff9800;
            padding: 12px 15px;
            margin-bottom: 15px;
            border-radius: 6px;
            color: #856404;
            font-size: 14px;
        }

        .notice-icon {
            font-size: 20px;
            color: #ff9800;
        }

        .help-link {
            color: #0d47a1;
            font-weight: 600;
            text-decoration: underline;
        }

            .help-link:hover {
                color: #1565c0;
                text-decoration: none;
            }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form runat="server">
        <asp:ScriptManager runat="server" />

        <div class="container-fluid">

            <!-- BREADCRUMB -->
            <ol class="breadcrumb">
                <li><a href="PermitHomePage.aspx">Home</a></li>
                <li class="active">STA COUNTER SIGNATURE ALL TAX TOKEN REPRINT</li>
            </ol>

            <!-- HELP SECTION -->
            <div class="alert alert-info" style="margin-bottom: 10px;">
                <strong><i class="glyphicon glyphicon-info-sign"></i>Need Help?</strong>
                To download your last payment receipt, 
                <a href="STA_COUNTERSIG_REPRINT.aspx"><b>click here</b></a>.
                        </div>

            <!-- NOTICE SECTION -->
            <div class="alert alert-warning">
                <strong><i class="glyphicon glyphicon-warning-sign"></i>Note:</strong>
                Receipts are available only for transactions on or after 
                    <b>31 March 2026</b>. 
                    For older transactions, only transaction status can be viewed.
            </div>

            <asp:UpdatePanel runat="server">


                <ContentTemplate>

                    <!-- FILTER PANEL -->
                    <div class="panel panel-default filter-panel">

                        <div class="panel-body">

                            <h4 class="page-title">STA COUNTER SIGNATURE ALL TAX TOKEN REPRINT</h4>

                            <div class="row">
                                <div class="col-md-4">
                                    <label>Enter Application Number</label>
                                    <asp:TextBox
                                        ID="txtApplicationNo"
                                        runat="server"
                                        CssClass="form-control" />
                                </div>
                            </div>

                            <div class="row" style="margin-top: 10px;">
                                <div class="col-md-12 text-right">
                                    <asp:Button
                                        ID="btnRun"
                                        runat="server"
                                        Text="Run"
                                        CssClass="btn btn-success"
                                        OnClick="btnRun_Click" />
                                </div>
                            </div>

                        </div>
                    </div>

                    <!-- TABLE PANEL -->
                    <div class="panel panel-default" id="tblISPermit" runat="server" visible="false">
                        <div class="panel-body">

                            <table id="sample_1"
                                class="table table-bordered table-striped table-hover">

                                <thead>
                                    <tr>
                                        <th class="nowrap">Application No</th>
                                        <th class="nowrap">Transaction Type</th>
                                        <th class="nowrap">Amount (₹)</th>
                                        <th class="nowrap">Transaction Date</th>
                                        <th class="nowrap">Status</th>
                                        <th class="route-col">GRN / Ref Details</th>
                                        <th class="nowrap action-col">Action</th>
                                    </tr>
                                </thead>

                                <tbody id="csTransactionList" runat="server"></tbody>

                            </table>

                        </div>
                    </div>

                </ContentTemplate>
                
            </asp:UpdatePanel>

        </div>

        <!-- MODAL (Bootstrap 3) -->
        <div class="modal fade" id="errorModal" tabindex="-1">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">

                    <div class="modal-header" style="background: #d9534f; color: #fff;">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Transaction Verification</h4>
                    </div>

                    <div class="modal-body text-center"></div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            OK
                        </button>
                    </div>

                </div>
            </div>
        </div>

    </form>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <!-- Scripts -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.13.6/js/dataTables.bootstrap.min.js"></script>
    <script>
        function openVerify(url) {
            var win = window.open(url, '_blank');
        }
    </script>
    <script>
        function initDataTable() {
            if ($.fn.DataTable.isDataTable('#sample_1')) {
                $('#sample_1').DataTable().destroy();
            }

            $('#sample_1').DataTable({
                pageLength: 5,
                lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
                ordering: true,
                scrollX: true,
                scrollY: "55vh",
                scrollCollapse: true
            });
        }

        // Rebind after UpdatePanel refresh
        Sys.WebForms.PageRequestManager.getInstance()
            .add_endRequest(function () {
                if ($('#sample_1').length) {
                    initDataTable();
                }
            });

    </script>

</asp:Content>
