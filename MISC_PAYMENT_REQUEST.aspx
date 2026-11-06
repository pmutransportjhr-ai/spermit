<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MISC_PAYMENT_REQUEST.aspx.cs" Inherits="MISC_PAYMENT_REQUEST" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Redirecting to JEGRAS</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <asp:Panel ID="pnlValidRequest" runat="server" Visible="false">
        <p>Redirecting securely to Jharkhand eGRAS. Please do not refresh this page.</p>
        <form id="gatewayForm" method="post" action="https://finance.jharkhand.gov.in/jegras/payment.aspx">
            <asp:Literal ID="litRequestInput" runat="server" />
            <noscript><button type="submit">Continue to JEGRAS</button></noscript>
        </form>
        <script type="text/javascript">
            window.setTimeout(function () {
                document.getElementById('gatewayForm').submit();
            }, 100);
        </script>
    </asp:Panel>
    <asp:Panel ID="pnlInvalidRequest" runat="server" Visible="false">
        <h3>Unable to start payment</h3>
        <p><asp:Literal ID="litErrorMessage" runat="server" /></p>
        <a href="MISCELLANEOUS_PAYMENT.aspx">Return to Miscellaneous Payment</a>
    </asp:Panel>
</body>
</html>