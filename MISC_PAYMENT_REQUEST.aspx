<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MISC_PAYMENT_REQUEST.aspx.cs" Inherits="MISC_PAYMENT_REQUEST" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Redirecting to JEGRAS</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
</head>
<body>
    <% if (IsValidRequest) { %>
        <p>Redirecting securely to Jharkhand eGRAS. Please do not refresh this page.</p>
        <form id="gatewayForm" method="post" action="https://finance.jharkhand.gov.in/jegras/payment.aspx">
            <input type="hidden" name="requestparam" value="<%= Server.HtmlAttributeEncode(RequestPayload) %>" />
            <noscript><button type="submit">Continue to JEGRAS</button></noscript>
        </form>
        <script type="text/javascript">
            window.setTimeout(function () {
                document.getElementById('gatewayForm').submit();
            }, 100);
        </script>
    <% } else { %>
        <h3>Unable to start payment</h3>
        <p><%= Server.HtmlEncode(ErrorMessage) %></p>
        <a href="MISCELLANEOUS_PAYMENT.aspx">Return to Miscellaneous Payment</a>
    <% } %>
</body>
</html>