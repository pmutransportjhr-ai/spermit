<%@ Page Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="MISC_PAYMENT_STATUS.aspx.cs" Inherits="MISC_PAYMENT_STATUS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>.misc-status{max-width:1100px;margin:25px auto;background:#fff;padding:25px;border-radius:10px;box-shadow:0 3px 18px #0001}</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" runat="server" class="form-horizontal">
        <div class="container"><div class="misc-status">
            <h3>Miscellaneous Payment Status</h3>
            <div class="row">
                <div class="col-md-7"><asp:TextBox ID="txtApplicationNumber" runat="server" CssClass="form-control" MaxLength="120" placeholder="Application Number" /></div>
                <div class="col-md-3"><asp:Button ID="btnSearch" runat="server" Text="Find Application" CssClass="btn btn-primary" OnClick="btnSearch_Click" /></div>
            </div>
            <asp:HiddenField ID="HFTableName" runat="server" />
            <asp:HiddenField ID="HFPermitNo" runat="server" />
            <div id="DivMobileValidate" runat="server" visible="false" class="row" style="margin-top:18px;">
                <div class="col-md-6">
                    <label>Registered Mobile Number</label>
                    <input type="text" runat="server" id="txtMobileNumber" class="form-control" maxlength="10" placeholder="Registered mobile number" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnOTP" runat="server" Text="Send OTP" CssClass="btn btn-primary" OnClick="btnOTP_Click" />
                </div>
            </div>
            <div id="DivOTPMatch" runat="server" visible="false" class="row" style="margin-top:18px;">
                <div class="col-md-6">
                    <label>Enter OTP</label>
                    <input type="text" runat="server" id="txtOTP" class="form-control" maxlength="6" placeholder="Enter OTP" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnVerifyOTP" runat="server" Text="Verify OTP" CssClass="btn btn-success" OnClick="btnVerifyOTP_Click" />
                </div>
            </div>
            <br />
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="alert alert-info" />
            <asp:GridView ID="gvTransactions" runat="server" Visible="false" AutoGenerateColumns="false" CssClass="table table-bordered table-striped"
                OnRowCommand="gvTransactions_RowCommand" EmptyDataText="No transactions found.">
                <Columns>
                    <asp:BoundField DataField="DEPTTRANID" HeaderText="Transaction ID" />
                    <asp:BoundField DataField="AUTHORITY_CODE" HeaderText="Authority" />
                    <asp:BoundField DataField="REASON_NAME" HeaderText="Reason" />
                    <asp:BoundField DataField="AMOUNT" HeaderText="Amount" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="TRN_STATUS" HeaderText="Status" />
                    <asp:BoundField DataField="GRN" HeaderText="GRN" />
                    <asp:BoundField DataField="TRN_REF_NO" HeaderText="Bank Reference" />
                    <asp:BoundField DataField="CREATED_ON" HeaderText="Initiated On" DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkVerify" runat="server" Text="Verify with JEGRAS" CommandName="VERIFY"
                                CommandArgument='<%# Eval("DEPTTRANID") %>'
                                Visible='<%# Convert.ToString(Eval("TRN_STATUS")) != "SUCCESS" %>'
                                OnClientClick="return confirm('Check the latest payment status with JEGRAS?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Challan">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkChallan" runat="server" Text="Open" Target="_blank"
                                NavigateUrl='<%# Eval("CHALLAN_URL") %>'
                                Visible='<%# !String.IsNullOrEmpty(Convert.ToString(Eval("CHALLAN_URL"))) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <p><a href="MISCELLANEOUS_PAYMENT.aspx">Make another miscellaneous payment</a></p>
        </div></div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server"></asp:Content>