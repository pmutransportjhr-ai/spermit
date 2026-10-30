<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="Application_Withdrawal.aspx.cs" Inherits="Application_Withdrawal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    --%>
    <link href="assets/global/plugins/jstree/dist/themes/default/style.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .side-menu {
            /*position: fixed;
  width: 300px;
  height: 100%;
  background-color: #f8f8f8;
  border-right: 1px solid #e7e7e7;*/
        }

            .side-menu .navbar {
                border: none;
            }

            .side-menu .navbar-header {
                width: 100%;
                border-bottom: 1px solid #e7e7e7;
            }

            .side-menu .navbar-cav .active a {
                /*background-color: transparent;*/
                margin-right: -1px;
                border-right: 5px solid #e7e7e7;
            }

            .side-menu .navbar-cav li {
                display: block;
                width: 100%;
                border-bottom: 1px solid #e7e7e7;
                /*background-color: #FFF;*/
            }

                .side-menu .navbar-cav li a {
                    padding: 15px;
                }

                    .side-menu .navbar-cav li a .glyphicon {
                        padding-right: 10px;
                    }

            .side-menu #dropdown {
                border: 0;
                margin-bottom: 0;
                border-radius: 0;
                /*background-color: #FFF;*/
                box-shadow: none;
            }

                .side-menu #dropdown .caret {
                    float: right;
                    margin: 9px 5px 0;
                }

                .side-menu #dropdown .indicator {
                    float: right;
                }

                .side-menu #dropdown > a {
                    border-bottom: 1px solid #e7e7e7;
                }

                .side-menu #dropdown .panel-body {
                    padding: 0;
                    /*background-color: #f3f3f3;*/
                }

                    .side-menu #dropdown .panel-body .navbar-cav {
                        width: 100%;
                    }

                        .side-menu #dropdown .panel-body .navbar-cav li {
                            padding-left: 15px;
                            border-bottom: 1px solid #e7e7e7;
                        }

                            .side-menu #dropdown .panel-body .navbar-cav li:last-child {
                                border-bottom: none;
                            }

                    .side-menu #dropdown .panel-body .panel > a {
                        margin-left: -20px;
                        padding-left: 35px;
                    }

                    .side-menu #dropdown .panel-body .panel-body {
                        margin-left: -15px;
                    }

                        .side-menu #dropdown .panel-body .panel-body li {
                            padding-left: 30px;
                        }

                            .side-menu #dropdown .panel-body .panel-body li:last-child {
                                border-bottom: 1px solid #e7e7e7;
                            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <asp:ScriptManager ID="ss" runat="server"></asp:ScriptManager>
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li><a href="PermitHomePage.aspx">Home</a> </li>
                            <li>WITHDRAW APPLICATION</li>

                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    Application Number: 
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblApplicationNumber" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-2">
                                    Registration Number: 
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblRegistration" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>
                            <br />
                            <asp:HiddenField ID="hdnAppID" runat="server" />
                            <asp:HiddenField ID="hdnTable" runat="server" />
                            <div class="row">
                                <div class="col-md-6"></div>
                                <div class="col-md-2">
                                    <asp:Button Text="Withdraw"
                                        CssClass="btn btn-primary"
                                        ID="btnWithdraw"
                                        runat="server"
                                        OnClientClick="showWithdrawPopup(); return false;" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button Text="Cancel" CssClass="btn btn-danger" runat="server" ID="btnCancel" OnClick="btnCancel_Click" />
                                </div>

                                <!-- Withdraw Confirmation Modal -->

                                <div id="withdrawModal" class="modal fade" tabindex="-1" role="dialog">
                                    <div class="modal-dialog modal-md">
                                        <div class="modal-content">

                                            <div class="modal-header" style="background: #d9534f; color: white;">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title">Confirm Withdrawal</h4>
                                            </div>

                                            <div class="modal-body">

                                                <p style="font-size: 16px">
                                                    Application Number :
        <b>
            <asp:Label ID="lblAppPopup" runat="server"></asp:Label></b>

                                                    <br />
                                                    <br />

                                                    Registration Number :
        <b>
            <asp:Label ID="lblRegPopup" runat="server"></asp:Label></b>

                                                    <br />
                                                    <br />

                                                    <span style="color: red; font-weight: bold;">Are you sure you want to withdraw this application?
                                                    </span>

                                                </p>

                                                <hr />

                                                <div class="checkbox">
                                                    <label style="font-size: 15px;">
                                                        <input type="checkbox" id="chkConsent" onclick="toggleWithdrawButton()" />
                                                        I confirm that I want to withdraw this application.
                                                    </label>
                                                </div>

                                            </div>

                                            <div class="modal-footer">

                                                <asp:Button
                                                    ID="btnConfirmWithdraw"
                                                    runat="server"
                                                    CssClass="btn btn-danger"
                                                    Text="Yes, Withdraw"
                                                    Enabled="false"
                                                    OnClick="btnWithdraw_Click" />

                                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                                    Cancel
                                                </button>

                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/plugins/jstree/dist/jstree.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/ui-tree.min.js" type="text/javascript"></script>
    <script>

        function showWithdrawPopup() {
            $('#withdrawModal').modal('show');

            $('#chkConsent').prop('checked', false);
            $('#<%= btnConfirmWithdraw.ClientID %>').prop('disabled', true);

            $('#<%= lblAppPopup.ClientID %>').text($('#<%= lblApplicationNumber.ClientID %>').text());
            $('#<%= lblRegPopup.ClientID %>').text($('#<%= lblRegistration.ClientID %>').text());
        }

        function toggleWithdrawButton() {

            if ($('#chkConsent').is(':checked')) {
                $('#<%= btnConfirmWithdraw.ClientID %>').prop('disabled', false);
            }
            else {
                $('#<%= btnConfirmWithdraw.ClientID %>').prop('disabled', true);
            }

        }

    </script>

</asp:Content>

