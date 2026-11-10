<%@ Page Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="MISCELLANEOUS_PAYMENT.aspx.cs" Inherits="MISCELLANEOUS_PAYMENT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .misc-card { background:#fff; border-radius:12px; padding:25px; box-shadow:0 4px 20px rgba(0,0,0,.08); margin:20px auto; max-width:900px; }
        .misc-title { font-size:21px; font-weight:600; color:#e7505a; margin-bottom:20px; }
        .misc-details { background:#f8fafc; border:1px solid #e5e9ef; border-radius:8px; padding:16px; margin:18px 0; }
        .misc-details .detail-label { color:#687386; font-size:12px; text-transform:uppercase; margin-bottom:4px; }
        .misc-details .detail-value { color:#202938; font-weight:600; margin-bottom:14px; }
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

                            <div class="row">
                                <div class="col-md-8">
                                    <label for="txtApplicationNumber">Application Number</label>
                                    <input type="text" runat="server" id="txtApplicationNumber" class="form-control"
                                        maxlength="120" placeholder="Enter application number" />
                                </div>
                                <div class="col-md-4 misc-actions">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success"
                                        Text="Fetch Application" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-default"
                                        Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
                                </div>
                            </div>

                            <asp:Panel ID="pnlPayment" runat="server" Visible="false">
                                <div class="misc-details">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="detail-label">Applicant Name</div>
                                            <div class="detail-value"><asp:Label ID="lblApplicantName" runat="server" /></div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="detail-label">Application Number</div>
                                            <div class="detail-value"><asp:Label ID="lblFetchedApplicationNumber" runat="server" /></div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="detail-label">Authority</div>
                                            <div class="detail-value"><asp:Label ID="lblAuthority" runat="server" /></div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="detail-label">Application Status</div>
                                            <div class="detail-value"><asp:Label ID="lblApplicationStatus" runat="server" /></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-5">
                                        <label for="txtAmount">Miscellaneous Amount (₹)</label>
                                        <input type="number" runat="server" id="txtAmount" class="form-control"
                                            min="0.01" max="99999999.99" step="0.01" placeholder="Enter amount" />
                                    </div>
                                </div>

                                <div class="text-center misc-actions">
                                    <asp:Button ID="btnMiscPay" runat="server" CssClass="btn btn-danger btn-lg"
                                        Text="Proceed to JEGRAS Payment" OnClick="btnMiscPay_Click" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
