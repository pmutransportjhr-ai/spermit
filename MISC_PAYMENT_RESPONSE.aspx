<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MISC_PAYMENT_RESPONSE.aspx.cs" Inherits="MISC_PAYMENT_RESPONSE" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Miscellaneous Payment Result</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style>body{font-family:Arial,sans-serif;background:#f5f7fa;margin:0;padding:35px}.panel{max-width:680px;margin:40px auto;background:#fff;padding:28px;border-radius:10px;box-shadow:0 3px 18px #0001}a{display:inline-block;margin-top:20px}</style>
</head>
<body>
    <div class="panel">
        <h2>Miscellaneous Payment</h2>
        <p><%= Server.HtmlEncode(ResultMessage) %></p>
        <% if (!string.IsNullOrWhiteSpace(ReceiptNumber)) { %>
            <p><strong>GRN:</strong> <%= Server.HtmlEncode(ReceiptNumber) %></p>
        <% } %>
        <a href="MISC_PAYMENT_STATUS.aspx">Check payment status</a>
        <br /><a href="PermitHomePage.aspx">Return to Home</a>
    </div>
</body>
</html>