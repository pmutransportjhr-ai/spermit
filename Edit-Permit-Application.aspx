<%@ Page Title=""
    Language="C#"
    MasterPageFile="MasterPage.master"
    AutoEventWireup="true"
    CodeFile="Edit-Permit-Application.aspx.cs"
    Inherits="Edit_Permit_Application" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <link href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background: #f7f9fb;
            font-size: 13px;
            overflow-x: hidden;
        }

        .page-title {
            font-weight: 700;
            color: #0B6B3A;
            font-size: 18px;
        }

        .panel {
            border-radius: 8px;
            box-shadow: 0 5px 15px rgba(0,0,0,.1);
        }

        .form-label {
            font-weight: 600;
            font-size: 12px;
        }

        .table th, .table td {
            font-size: 12px;
            vertical-align: middle;
            white-space: nowrap;
            padding: 8px;
        }

        .main-data {
            font-weight: 600;
            display: block;
        }

        .secondary-data {
            font-size: 11px;
            color: #777;
            display: block;
        }

        .route-col {
            white-space: normal !important;
            word-break: break-word;
            min-width: 350px;
            max-width: 450px;
        }

        .action-col {
            width: 150px;
        }

        #sample_1 {
            min-width: 1200px;
        }
    </style>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <input id="txtScreen" type="text" value="Edit-Permit-Application.aspx" style="display: none;" />
    <input id="txtColCnt" type="text" value="9" style="display: none;" />

    <form runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />

        <div class="container-fluid">

            <ol class="breadcrumb">
                <li><a href="STA-DASHBOARD.aspx">Home</a></li>
                <li class="active">Edit Permit Application</li>
            </ol>


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="panel panel-default">

                        <div class="panel-body">

                            <h4 class="page-title">Edit Permit Application</h4>

                            <div class="row">

                                <div class="col-md-4">

                                    <div class="form-group">

                                        <label class="form-label">State</label>

                                        <asp:DropDownList
                                            ID="ddlDesState"
                                            runat="server"
                                            CssClass="form-control"
                                            AppendDataBoundItems="true" />

                                    </div>

                                </div>


                                <div class="col-md-4">

                                    <div class="form-group">

                                        <label class="form-label">Status</label>

                                        <asp:DropDownList
                                            ID="ddlStatus"
                                            runat="server"
                                            CssClass="form-control">

                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem>Issued</asp:ListItem>
                                            <asp:ListItem>Renewed</asp:ListItem>
                                            <asp:ListItem>Replaced</asp:ListItem>
                                            <asp:ListItem>Transfered</asp:ListItem>
                                            <asp:ListItem>Surrender</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>

                                </div>


                                <div class="col-md-4">

                                    <div class="form-group">

                                        <label class="form-label">Permit Type</label>

                                        <asp:DropDownList
                                            ID="ddlType"
                                            runat="server"
                                            CssClass="form-control">

                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem>Permanent</asp:ListItem>
                                            <asp:ListItem>Temporary</asp:ListItem>

                                        </asp:DropDownList>

                                    </div>

                                </div>

                            </div>


                            <div class="row">

                                <div class="col-md-12 text-right">

                                    <asp:Button
                                        ID="btnRun"
                                        runat="server"
                                        Text="Run"
                                        CssClass="btn btn-success"
                                        OnClick="btnRun_Click"
                                        OnClientClick="return fnValidateReport();" />

                                </div>

                            </div>

                        </div>
                    </div>


                    <div id="tblISPermit"
                        runat="server"
                        visible="false"
                        class="panel panel-default">

                        <div class="panel-body">

                            <div class="table-responsive">

                                <table class="table table-bordered table-hover"
                                    id="sample_1"
                                    style="width: 100%">

                                    <thead>

                                        <tr>

                                            <th>Application Details</th>
                                            <th>Permit Details</th>
                                            <th>Vehicle Details</th>
                                            <th>Owner Details</th>
                                            <th class="route-col">Route</th>
                                            <th>Validity</th>
                                            <th>Status</th>
                                            <th class="action-col">Action</th>

                                        </tr>

                                    </thead>

                                    <tbody id="divrenewalDetail" runat="server"></tbody>

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

             /* Run on first page load */

             $(document).ready(function () {

                 if ($('#sample_1 tbody tr').length > 0) {
                     initDataTable();
                 }

             });

             /* Run after UpdatePanel postback */

             Sys.WebForms.PageRequestManager
                 .getInstance()
                 .add_endRequest(function () {

                     if ($('#sample_1 tbody tr').length > 0) {
                         initDataTable();
                     }

                 });

             function fnValidateReport() {
                 return true;
             }

             function MasterEdit(id) {
                 window.open("Edit-Form-Permit.aspx?q=" + encodeURIComponent(id), "_blank");
             }

             function MasterView(id) {
                 window.open("STA-PERMIT-APPLICATION-VIEW.aspx?q=" + encodeURIComponent(id), "_blank");
             }

    </script>

</asp:Content>
