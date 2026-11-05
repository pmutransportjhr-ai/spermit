<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MISC_PAYMENT_REQUEST.aspx.cs" Inherits="MISC_PAYMENT_REQUEST" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Redirecting to JEGRAS</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <form id="gatewayForm" runat="server" method="post" action="https://finance.jharkhand.gov.in/jegras/payment.aspx">
        <input type="hidden" id="requestparam" name="requestparam" runat="server" />
        <p>Redirecting securely to Jharkhand eGRAS. Please do not refresh this page.</p>
    </form>
    <div id="errorPanel" runat="server" visible="false">
        <h3>Unable to start payment</h3>
        <p id="errorMessage" runat="server"></p>
        <a href="MISCELLANEOUS_PAYMENT.aspx">Return to Miscellaneous Payment</a>
    </div>
</body>
</html>