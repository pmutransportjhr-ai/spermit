<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Transport Department, Government of Jharkhand</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/css/components.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout4/css/themes/light.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body { background-color: #364150 !important; }
        .page-header.navbar .page-logo .logo-default { margin: 3px 10px 0 !important; }
        .form-horizontal .control-label { text-align: left !important; }
        .title-center { text-align: center; }
        .title-center1 { font-size: 24px; font-weight: bold; color: #54A624; }
        .title-center2 { font-size: 22px; font-weight: bold; color: #006EBD; }
        .title-center3 { font-size: 14px; font-weight: bold; color: #54A624; }
        .field-value { padding-top: 7px; overflow-wrap: anywhere; }
        .message { display: block; margin-bottom: 15px; }
    </style>
    <script type="text/javascript">
        function validateResetPassword() {
            var password = document.getElementById('<%= txtNewPassword.ClientID %>').value;
            var confirmPassword = document.getElementById('<%= txtConfirmPassword.ClientID %>').value;
            var pattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).{8,20}$/;

            if (!password) {
                alert('Please enter the new password.');
                return false;
            }
            if (!pattern.test(password)) {
                alert('Password must be 8-20 characters and contain uppercase, lowercase, number and special character.');
                return false;
            }
            if (password !== confirmPassword) {
                alert('New password and confirm password do not match.');
                return false;
            }
            return confirm('Are you sure you want to reset this user password?');
        }
    </script>
</head>
<body class="page-container-bg-solid page-header-fixed">
    <div class="page-header navbar navbar-fixed-top" style="height:100px !important;">
        <div class="page-header-inner">
            <div class="row">
                <div class="col-md-2">
                    <div class="page-logo">
                        <a href="#"><img src="assets/img/logo.png" alt="logo" class="logo-default" /></a>
                    </div>
                </div>
                <div class="col-md-10 title-center">
                    <span class="title-center1">Government of Jharkhand</span><br />
                    <span class="title-center2">Transport Department</span><br />
                    <span class="title-center3">Online State Permit System</span>
                </div>
            </div>
        </div>
    </div>

    <form id="form1" runat="server">
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content" style="padding-top:130px;">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-8">
                            <div class="portlet light bordered">
                                <div class="portlet-title">
                                    <div class="caption">
                                        <i class="fa fa-lock font-red-sunglo"></i>
                                        <span class="caption-subject font-red-sunglo bold uppercase">Admin – Reset User Password</span>
                                    </div>
                                </div>
                                <div class="portlet-body form">
                                    <div class="form-horizontal">
                                        <div class="form-body">
                                            <asp:Label ID="lblMessage" runat="server" CssClass="message" EnableViewState="false" />
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">User Type</label>
                                                <div class="col-md-9">
                                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="">-- Select STA / RTA --</asp:ListItem>
                                                        <asp:ListItem Value="STA">STA</asp:ListItem>
                                                        <asp:ListItem Value="RTA">RTA</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Search User</label>
                                                <div class="col-md-9">
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" MaxLength="200"
                                                        autocomplete="off" placeholder="Enter username, email address or mobile number" />
                                                    <span class="help-block">Enter an exact username, email address or mobile number.</span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search User" CssClass="btn blue"
                                                        OnClick="btnSearch_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn default"
                                                        CausesValidation="false" OnClick="btnCancel_Click" />
                                                </div>
                                            </div>

                                            <asp:Panel ID="pnlUserDetails" runat="server" Visible="false">
                                                <hr />
                                                <h4>User Details</h4>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Employee Name</label>
                                                    <div class="col-md-9 field-value"><asp:Label ID="lblEmployeeName" runat="server" /></div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Username</label>
                                                    <div class="col-md-9 field-value"><asp:Label ID="lblLoginId" runat="server" /></div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Email</label>
                                                    <div class="col-md-9 field-value"><asp:Label ID="lblEmail" runat="server" /></div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Mobile</label>
                                                    <div class="col-md-9 field-value"><asp:Label ID="lblMobile" runat="server" /></div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Status</label>
                                                    <div class="col-md-9 field-value"><asp:Label ID="lblStatus" runat="server" /></div>
                                                </div>
                                                <hr />
                                                <h4>Set New Password</h4>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">New Password</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control"
                                                            MaxLength="20" autocomplete="new-password" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Confirm New Password</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"
                                                            MaxLength="20" autocomplete="new-password" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-offset-3 col-md-9">
                                                        <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CssClass="btn green"
                                                            OnClientClick="return validateResetPassword();" OnClick="btnResetPassword_Click" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="text-center" style="color:white;">All Rights Reserved by Government of Jharkhand, India.</div>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
</body>
</html>
