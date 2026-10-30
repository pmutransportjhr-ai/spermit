<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="utf-8" />
        <title>Transport Department, Government of Jharkhand</title>
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta content="width=device-width, initial-scale=1" name="viewport" />
        <meta content="" name="description" />
        <meta content="" name="author" />
        <!-- BEGIN GLOBAL MANDATORY STYLES -->
       <link href="assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
        <!-- END GLOBAL MANDATORY STYLES -->
        <!-- BEGIN THEME GLOBAL STYLES -->
        <link href="assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
        <link href="assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />
        <!-- END THEME GLOBAL STYLES -->
        <!-- BEGIN THEME LAYOUT STYLES -->
        <link href="assets/layouts/layout4/css/layout.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/layouts/layout4/css/themes/light.min.css" rel="stylesheet" type="text/css" id="style_color" />
        <link href="assets/layouts/layout4/css/custom.min.css" rel="stylesheet" type="text/css" />
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
        <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
   
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN THEME LAYOUT SCRIPTS -->
        <script src="assets/layouts/layout4/scripts/layout.min.js" type="text/javascript"></script>
        <script src="assets/layouts/layout4/scripts/demo.min.js" type="text/javascript"></script>
        <script src="assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
        <!-- END THEME LAYOUT SCRIPTS -->
            
          
   
</head>
<body class="page-container-bg-solid page-header-fixed" style="background-color: #364150 !important;">
    <script src="assets/js/Login.js" type="text/javascript"></script>

    <!-- BEGIN HEADER -->
    <div class="page-header navbar navbar-fixed-top" style="height: 100px !important;">
        <!-- BEGIN HEADER INNER -->
        <div class="page-header-inner ">
            <!-- BEGIN LOGO -->
             <div class="row">
                <div class="col-md-2">
                    <div class="page-logo">
                        <a href="#">
                            <img src="assets/img/logo.png" alt="logo" class="logo-default" />
                        </a>

                    </div>
                </div>
                <!-- END LOGO -->
                <div class="col-md-10 title-center">

                    <span class="title-center1">Government of Jharkhand</span>
                    <br /><span class="title-center2">Transport Department</span>
                    <br /><span class="title-center3">Online State Permit System</span>

                </div>
            </div>


        </div>
    </div>



    <div class="page-container">
        <div class="page-content-wrapper">
            <!-- BEGIN CONTENT BODY -->

            <div class="page-content">
                <!-- BEGIN LOGIN FORM -->
                
                <div class="row" style="padding-top:40px">
                   
                    <div class="col-md-9">
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="icon-equalizer font-red-sunglo"></i>
                                    <span class="caption-subject font-red-sunglo bold uppercase">Forgot Password</span>
                                    <%--<span class="caption-helper">form actions on top...</span>--%>
                                </div>

                            </div>
                            <div class="portlet-body form">
                                <!-- BEGIN FORM-->
                                <form runat="server" action="#" id="frmlogin" class="form-horizontal">
                                    <div class="form-body">

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Email Id</label>
                                            <div class="col-md-9">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                                    <%--<input runat="server" type="text" class="regtxt form-control" id="txtLogin" placeholder="UserName" />--%>
                                                    <asp:TextBox ID="txtLogin" autocomplete="off" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group  last">
                                            <label class="col-md-3 control-label">Type</label>
                                            <div class="col-md-9">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                                    <asp:DropDownList ID="ddlType" CssClass="form-control select2" runat="server" TabIndex="2">

                                                        <asp:ListItem Value="0">--select type--</asp:ListItem>
                                                        <asp:ListItem Value="STA">
                                                STA
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="RTA">
                                                RTA
                                                        </asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">

                                           
                                            <label class="col-md-3 control-label">Captcha code</label>
                                            <div class="col-md-7">
                                                <%--<input type="text" readonly="readonly" class="form-control" id="imgCaptcha" style="background-color: lightgray; color: steelblue; font-size: medium; text-decoration: line-through; font-weight: 400" />--%>
                                                <input  runat="server" type="text" id="imgCaptcha"  onkeydown = "return (event.keyCode!=13);" style="height:60px;width:100%; font-size:20px; background-image:url('assets/pages/img/background/CreateCaptcha.gif');padding:10px 0; text-align:center; cursor:none; border:none; font-weight:bold; font-family:Modern" readonly="readonly" />
                                            </div>
                                            <div class="col-md-2"  style="padding-top:20px;">
                                                
                                                 <a id="refCaptchta"><img src="assets/pages/img/login/refresh.png" /></a>
                                            </div>
                                            
                                        </div>



                                        <div class="form-group  last">
                                            <input id="hdnCap" type="hidden" />
                                            <label class="col-md-3 control-label">Enter captcha code</label>
                                            <div class="col-md-9">

                                                <input type="text" autocomplete="off" id="txtCaptcha" class="form-control" tabindex="3" />
                                            </div>


                                            <%--<asp:DropDownList ID="DropDownList1" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">--select type--</asp:ListItem>
                                                            <asp:ListItem Value="STA">
                                                STA
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="RTA">
                                                RTA
                                                            </asp:ListItem>
                                                        </asp:DropDownList>
                                            --%>
                                        </div>
                                        
                                    </div>

                                    <div class="form-actions">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <%--<button type="submit"  class="btn green">Login</button>--%>
                                                <asp:Button ID="btnSubmit" CssClass="btn green" runat="server" TabIndex="4" Text="Submit" OnClientClick="return validateFP();" OnClick="btnSubmit_Click" />
                                                <asp:Button ID="btnCancel" CssClass="btn default" runat="server" TabIndex="5" Text="Cancel" OnClick="btnCancel_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <!-- END FORM-->

                                </form>




                            </div>
                           

                        </div>
                         <div class="col-md-12">
                                <div style="text-align: center; color: white">All Rights Reserved by Government of Jharkhand, India.</div>
                            </div>
                    </div>
                    <!-- END LOGIN FORM -->
                    <div class="col-md-3">
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
