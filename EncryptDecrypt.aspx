<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EncryptDecrypt.aspx.cs" Inherits="_EncryptDecrypt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <script type = "text/javascript">
            var counter = 1;
            function DisableButton() {
                document.getElementById("<%=Button1.ClientID %>").disabled = true;
            }
            function fnOnLoad() {
                document.getElementById("txtClientConuter").value = counter;
            }
            function fnValidate() {
                counter++;
                document.getElementById("txtClientConuter").value = counter;
            }
            window.onload = fnOnLoad;
            window.onbeforeunload = DisableButton;
        </script>
        <script type = "text/javascript">
            function DisableButtons() {
                var inputs = document.getElementsByTagName("INPUT");
                for (var i in inputs) {
                    if (inputs[i].type == "button" || inputs[i].type == "submit") {
                        inputs[i].disabled = true;
                    }
                }
            }
            // window.onbeforeunload = DisableButtons; //uncomment to use this script.
        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:TextBox ID="txtClientConuter" runat="server" />
            <asp:TextBox ID="txtCounter" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="fnValidate();" OnClick = "Button1_Clicked" />
            <br /><br /><br />

            <table>
                <tr>
                    <td>String</td>
                    <td><asp:TextBox ID="txtString" runat="server" /></td>
                </tr>
                <tr>
                    <td>Encrypted</td>
                    <td><asp:TextBox ID="txtEString" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="btnEncrypt" Text="Encrypt" runat="server" OnClick="btnEncrypt_onClick" /></td>
                </tr>

                <tr>
                    <td>Encrypted</td>
                    <td><asp:TextBox ID="txtEnString" runat="server" /></td>
                </tr>
                <tr>
                    <td>String</td>
                    <td><asp:TextBox ID="txtDString" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="2"><asp:Button ID="btnDecrypt" Text="Decrypt" runat="server" OnClick="btnDecrypt_onClick" /></td>
                </tr>
            </table>

        </form>
    </body>
</html>