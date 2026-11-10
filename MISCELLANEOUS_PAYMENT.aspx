<%@ Page Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="MISCELLANEOUS_PAYMENT.aspx.cs" Inherits="MISCELLANEOUS_PAYMENT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .misc-card { background:#fff; border-radius:12px; padding:25px; box-shadow:0 4px 20px rgba(0,0,0,.08); margin:20px auto; max-width:1000px; }
        .misc-title { font-size:21px; font-weight:600; color:#e7505a; margin-bottom:20px; }
        .misc-step { border-bottom:1px solid #eee; padding-bottom:18px; margin-bottom:18px; }
        .misc-step:last-child { border-bottom:0; }
        .misc-actions { margin-top:22px; }
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
                        <div class="misc-card">
                            <div class="misc-title">Miscellaneous Payment</div>
                            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="alert alert-warning" />
                            <div class="misc-step">
                                <h4>1. Select Authority</h4>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Authority</label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlAuthority" runat="server" CssClass="form-control"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlAuthority_SelectedIndexChanged" />
                                    </div>
                                </div>
                            </div>
                            <div class="misc-step">
                                <h4>2. Verify Application</h4>
                                <div class="row">
                                    <div class="col-md-7">
                                        <label>Application Number</label>
                                        <input type="text" runat="server" class="form-control" id="txtApplicationNumber" placeholder="Enter application number" />
                                    </div>
                                    <div class="col-md-5 misc-actions">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Check Application" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-default" Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
                                    </div>
                                </div>
                                <asp:HiddenField ID="HFTableName" runat="server" />
                                <asp:HiddenField ID="HFPermitNo" runat="server" />
                                <div id="DivMobileValidate" runat="server" visible="false" class="row" style="margin-top:18px;">
                                    <div class="col-md-6">
                                        <label>Registered Mobile Number</label>
                                        <input type="text" runat="server" class="form-control" id="txtMobileNumber" maxlength="10" placeholder="Enter registered mobile number" />
                                    </div>
                                    <div class="col-md-4 misc-actions">
                                        <asp:Button ID="btnOTP" runat="server" CssClass="btn btn-primary" Text="Send OTP" OnClick="btnOTP_Click" />
                                    </div>
                                </div>
                                <div id="DivOTPMatch" runat="server" visible="false" class="row" style="margin-top:18px;">
                                    <div class="col-md-6">
                                        <label>Enter OTP</label>
                                        <input type="text" runat="server" class="form-control" id="txtOTP" maxlength="6" placeholder="Enter OTP" />
                                    </div>
                                    <div class="col-md-4 misc-actions">
                                        <asp:Button ID="btnVerify" runat="server" CssClass="btn btn-success" Text="Verify OTP" OnClick="btnVerify_Click" />
                                    </div>
                                </div>
                            </div>
                            <div id="DivMiscPayment" runat="server" visible="false" class="misc-step">
                                <h4>3. Payment Details</h4>
                                <div class="row">
                                    <div class="col-md-6 form-group">
                                        <label>Payment Reason</label>
                                        <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged" />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:PlaceHolder ID="phDynamicFields" runat="server" />
                                </div>
                                <div class="text-center misc-actions">
                                    <asp:Button ID="btnMiscPay" runat="server" CssClass="btn btn-danger btn-lg"
                                        Text="Proceed to JEGRAS Payment" OnClick="btnMiscPay_Click" />
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
</asp:Content>