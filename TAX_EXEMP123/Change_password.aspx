<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Change_password.aspx.cs" Inherits="Change_password" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta charset="utf-8" />
        <title>Transport Department, Government of Jharkhand</title>
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta content="width=device-width, initial-scale=1" name="viewport" />
        <meta content="" name="description" />
        <meta content="" name="author" />
        <!-- BEGIN GLOBAL MANDATORY STYLES -->
        <link href="~/assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <link href="~/assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
        <link href="~/assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="~/assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
        <link href="~/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
        <!-- END GLOBAL MANDATORY STYLES -->
        <!-- BEGIN THEME GLOBAL STYLES -->
        <link href="~/assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
        <link href="~/assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
        <!-- END THEME GLOBAL STYLES -->
        <!-- BEGIN THEME LAYOUT STYLES -->
        <link href="~/assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
        <link href="~/assets/layouts/layout4/css/themes/light.min.css" rel="stylesheet" type="text/css" id="style_color" />
        <link href="~/assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />
        <!-- END THEME LAYOUT STYLES -->
        <style type="text/css">
            .page-header.navbar .page-logo .logo-default {
             margin: 3px 10px 0 !important;
        }
        .form-horizontal .control-label {
            text-align: left !important;
        }
        .title-center {text-align: center;}
        .title-center1{font-size: 24px;font-weight: bold;color: #54A624;}
        .title-center2{font-size: 22px;font-weight: bold;color: #006EBD;}
        .title-center3{font-size: 14px;font-weight: bold;color: #54A624;}
   
        </style>
        <!-- BEGIN CORE PLUGINS -->
        <script src="~/assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="~/assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="~/assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="~/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
        <script src="~/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="~/assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="~/assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
        <script src="~/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="~/assets/global/scripts/app.min.js" type="text/javascript"></script>

       
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN THEME LAYOUT SCRIPTS -->
        <script src="~/assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
        <script src="~/assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
        <script src="~/assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
        <!-- END THEME LAYOUT SCRIPTS -->
        <script src="~/assets/pages/scripts/sha512.js" type="text/javascript"></script>

        <script type="text/javascript">
            function validatelogin() {
                var LOGIN_ID = $('#txtUsername').val();
                var PASSWORD = $('#txtPassword').val();
                var type = $('#ddlRole').val();
                if (LOGIN_ID == "" || LOGIN_ID == null) {
                    alert("Please Enter Login ID");
                    //Captcha();

                    return false;
                }
                if (PASSWORD == "" || PASSWORD == null) {
                    alert("Please Enter Password");

                    //Captcha();

                    return false;
                }
                if (type == "0" || type == null || type == "") {
                    alert("Please select type");

                    //Captcha();

                    return false;
                }

                // debugger;
                //var captcha = removeSpaces($('#txtCaptcha').val());

                //if (captcha == "0" || captcha == null || captcha == "") {
                //    alert("Please enter captcha code");

                //    Captcha();

                //    return false;
                //}
                //else {
                //    if (captcha != removeSpaces($('#hdnCap').val())) {
                //        alert("Please enter valid captcha code");

                //        Captcha();

                //        return false;

                //    }

                //}
                // debugger;
                // alert("Here");
                 <%--var salt = '<%=Session["salt"]%>';
                 var hash = hex_sha512(hex_sha512(PASSWORD) + salt);
                 // alert("Hash " + hash);
                 document.getElementById('txtPassword').value = hash;
                 document.getElementById('<%=hfpwd.ClientID%>').value = hash;--%>


                //return true;
            }
         </script>
    </head>
    <body class="page-container-bg-solid page-header-fixed" style="background-color: #e7ecf1 !important;">
        <script src="~/assets/js/Login.js" type="text/javascript"></script>
        <form id="form1" runat="server">
            <div class="page-header navbar navbar-fixed-top" style="height: 100px !important;">
                <div class="page-header-inner ">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="page-logo">
                                <a href="#"><img src="../assets/img/logo.png" alt="logo" class="logo-default" /></a>
                            </div>
                        </div>
                        <div class="col-md-8 title-center">
                            <span class="title-center1">Government of Jharkhand</span>
                            <br /><span class="title-center2">Transport Department</span>
                            <br /><span class="title-center3">Online State Permit System</span>
                        </div>
                        <div class="col-md-2 title-center">
                            <asp:Button ID="lkbLogout" runat="server" OnClick="lkbLogout_Click" Text="Logout" CssClass="btn btn-circle-left btn-success" style="margin-top: 60px;"></asp:Button>
                            <asp:Button ID="btnPassword" runat="server" OnClick="btnPassword_Click" Text="Change Password" CssClass="btn btn-circle-left btn-success" style="margin-top: 60px;"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        
            <div class="page-container">
                <div class="page-content-wrapper">
                    <div class="page-content" style="padding-left:0; margin-left:0;">
                        <div class="row" style="padding-top:40px">
                            <div class="col-md-12">
                                <div class="portlet light bordered">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-equalizer font-red-sunglo"></i>
                                            <span class="caption-subject font-red-sunglo bold uppercase">Change Password</span>
                                        </div>
                                    </div>
                                <div class="form-body">
                                    <h2 style="text-align: center;font-size: 15px;color: red;"><asp:Label ID="lblError" runat="server"></asp:Label></h2>
                                        <div class="form-group  last">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Old Password</label>
                                                <div class="col-md-9">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                                        <asp:TextBox ID="txtOldPass" runat="server"  autocomplete="off" TabIndex="1" CssClass="regtxt form-control CopyValidate" TextMode="Password"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">New Password</label>
                                            <div class="col-md-9">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                                    <asp:TextBox ID="txtNewPass" runat="server"  autocomplete="off" TabIndex="1" CssClass="regtxt form-control CopyValidate" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Confirm Password</label>
                                            <div class="col-md-9">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                                    <asp:TextBox ID="txtConfPass" runat="server"  autocomplete="off" CssClass="regtxt form-control CopyValidate" TabIndex="1" TextMode="Password"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        
                                        </div>
                                        
                                    </div>

                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <%--<button type="submit"  class="btn green">Login</button>--%>
                                                <asp:Button ID="btnSave" TabIndex="1" CssClass="btn green" runat="server" Text="Change Password" OnClientClick="return validatelogin();" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <!-- END FORM-->

                                </form>




                            </div>
        </div></div></div></div></div>
    </form>
    
</body>
</html>
