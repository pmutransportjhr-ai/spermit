<%@ Page Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="MISCELLANEOUS_PAYMENT.aspx.cs" Inherits="MISCELLANEOUS_PAYMENT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />



    <style type="text/css">

        .card-box {
            background: #fff;
            border-radius: 12px;
            padding: 25px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.08);

            margin-top: 20px;
        }

        .title {
            font-size: 20px;
            font-weight: 600;
            margin-bottom: 20px;
            color: #e7505a;
        }

        .btn-modern {
            border-radius: 25px;
            padding: 8px 20px;
        }

        .center-box {
            max-width: 900px;
            margin: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" runat="server" class="form-horizontal" autocomplete="off">



        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">

                        <ul class="page-breadcrumb breadcrumb">
                            <li><a href="PermitHomePage.aspx">Home</a><i class="fa fa-circle"></i></li>
                            <li><span>Miscellaneous Payment</span></li>
                        </ul>

                        <div class="page-content-inner center-box">

                            <div class="card-box">

                                <div class="title">Miscellaneous Payment</div>

                                <!-- Application Number -->
                                <div class="row">
                                    <div class="col-md-8">
                                        <label><b>Application Number</b></label>
                                        <input type="text" runat="server" class="form-control" id="txtApplicationNumber" placeholder="Enter Application Number" />
                                    </div>

                                    <div class="col-md-4" style="margin-top: 25px;">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-modern"
                                            Text="Check Status" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-default btn-modern"
                                            Text="Clear" OnClick="btnClear_Click" />
                                    </div>
                                </div>

                                <br />

                                <!-- Message -->
                                <asp:Label ID="lblMessage" Visible="false" CssClass="alert alert-warning" runat="server"></asp:Label>

                                <asp:HiddenField ID="HFTableName" runat="server" />
                                <asp:HiddenField ID="HFPermitNo" runat="server" />

                                <!-- Mobile -->
                                <div id="DivMobileValidate" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Mobile Number</label>
                                            <input type="text" runat="server" class="form-control" id="txtMobileNumber" placeholder="Enter Mobile Number" />
                                        </div>
                                        <div class="col-md-6" style="margin-top: 25px;">
                                            <asp:Button ID="btnOTP" runat="server" CssClass="btn btn-primary btn-modern"
                                                Text="Send OTP" OnClick="btnOTP_Click" />
                                        </div>
                                    </div>
                                </div>

                                <br />

                                <!-- OTP -->
                                <div id="DivOTPMatch" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Enter OTP</label>
                                            <input type="text" runat="server" class="form-control" id="txtOTP" placeholder="Enter OTP" />
                                        </div>
                                        <div class="col-md-6" style="margin-top: 25px;">
                                            <asp:Button ID="btnVerify" runat="server" CssClass="btn btn-success btn-modern"
                                                Text="Verify OTP" OnClick="btnVerify_Click" />
                                        </div>
                                    </div>
                                </div>

                                <br />

                                <!-- ✅ MISC PAYMENT SECTION -->
                                <div id="DivMiscPayment" runat="server" visible="false">

                                    <hr />

                                    <div class="title">Enter Payment Details</div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>Amount (₹)</label>
                                            <input type="number" runat="server" id="txtAmount" class="form-control" placeholder="Enter Amount" />
                                        </div>

                                        <div class="col-md-4">
                                            <label>Reason</label>
                                            <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                                <asp:ListItem>Late Fee</asp:ListItem>
                                                <asp:ListItem>Penalty</asp:ListItem>
                                                <asp:ListItem>Correction Charges</asp:ListItem>
                                                <asp:ListItem Value="OTHER">Other</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-4" id="divOtherReason" style="display: none;">
                                            <label>Other Reason</label>
                                            <input type="text" runat="server" id="txtOtherReason" class="form-control" placeholder="Enter Reason" />
                                        </div>
                                    </div>

                                    <br />

                                    <div class="text-center">
                                        <asp:Button ID="btnMiscPay" runat="server"
                                            CssClass="btn btn-danger btn-modern"
                                            Text="Proceed to Payment"
                                            OnClick="btnMiscPay_Click" />
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
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="assets/js/ViewApplicationStatus.js" type="text/javascript"></script>

    <script>
    $('#<%=ddlReason.ClientID%>').change(function () {
        if ($(this).val() === "OTHER") {
            $('#divOtherReason').show();
        } else {
            $('#divOtherReason').hide();
        }
    });
    </script>
</asp:Content>
