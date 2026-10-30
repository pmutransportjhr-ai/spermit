<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordReset.aspx.cs" Inherits="PasswordReset" %>

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
.title-center {
            text-align: center;
        }

        .title-center1 {
            font-size: 24px;
            font-weight: bold;
            color: #54A624;
        }

        .title-center2 {
            font-size: 22px;
            font-weight: bold;
            color: #006EBD;
        }

        .title-center3 {
            font-size: 14px;
            font-weight: bold;
            color: #54A624;
        }
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
     <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
       <script src="assets/pages/scripts/sha512.js" type="text/javascript"></script>
      <script src="assets/js/Login.js" type="text/javascript"></script>
    <script type="text/javascript">

        function returnHashCode() {
            var PASSWORD = $('#txtRNewPassword').val();
            var salt = '<%=Session["salt"]%>';
                var hash = hex_sha512(hex_sha512(PASSWORD) + salt);
                document.getElementById('txtRNewPassword').value = hash;
               

            }
    </script>
    <!-- BEGIN HEADER -->
    <div class="page-header navbar navbar-fixed-top" style="height: 100px !important;">
        <!-- BEGIN HEADER INNER -->
        <div class="page-header-inner ">
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
                
                <div class="row" style="padding-top: 100px">
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-6">
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="icon-equalizer font-red-sunglo"></i>
                                    <span class="caption-subject font-red-sunglo bold uppercase">Password Reset</span>
                                    <%--<span class="caption-helper">form actions on top...</span>--%>
                                </div>

                            </div>
                            <div class="portlet-body form">
                                <!-- BEGIN FORM-->
                                <form runat="server" action="#" id="frmlogin" class="form-horizontal">
                                   <asp:Panel ID="PanelResetPassword" runat="server">
       
                   
                    <div class="portlet-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12">  <asp:TextBox runat="server" ID="txtApplicantEmail" Style="display: none" />
                                 <asp:TextBox runat="server" ID="txtApplicantPassCode" Style="display: none" /></div>
                            <div class="col-md-5 col-sm-5"> <label for="form_control"><b>New Password</b></label>
                                
                            </div>
                            <div class="col-md-7 col-sm-7">
                                <div class="form-group">
                                   
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtRNewPassword" TextMode="Password" />
                               
                                           
                                </div>
                            </div>
                           
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-sm-5"> <label for="form_control"><b>Confirm Password</b></label></div>
                            <div class="col-md-7 col-sm-7">
                                <div class="form-group">
                                   
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtRConfirmPassword" TextMode="Password" />
                                    
                                </div>
                            </div>
                            
                        </div>
                        <div class="row">
                            <div class="col-md-5"></div>
                            <div class="col-md-7">
                                <div class="form-group">
                                    <asp:Button ID="btnRPassword" CssClass="btn green" runat="server" Text="Submit" OnClientClick="return fnRPValidate();" OnClick="btnRPassword_Click"  />
                                    <asp:Button ID="btnCancel" CssClass="btn default" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                    
                                </div>
                            </div>
                          
                        </div>
                        <br /><br /><br /><br />
                    </div>
               
        </asp:Panel>
    
    <asp:Panel ID="Panel2" runat="server" Visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light portlet-fit ">
                    
                    <div class="portlet-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12">
                                <ul>
                                    <li>The Link has expired and can not be used to Reset your password now</li>
                                    <li>The Reason is that Link has been already accessed once</li>
                                </ul>
                                
                                
                            </div>
                            <div class="col-md-12 col-sm-12" style="text-align:center" ><a href="Login.aspx" class="btn btn-sm blue"> Click here to go home page &nbsp;&nbsp;<i class="fa fa-arrow-right"></i></a></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

                                    <!-- END FORM-->

                                </form>




                            </div>
                           

                        </div>
                         <div class="col-md-12">
                                <div style="text-align: center; color: white">All Rights Reserved by Government of Jharkhand, India. </div>
                            </div>
                    </div>
                    <!-- END LOGIN FORM -->

                </div>
            </div>
        </div>
    </div>

</body>
</html>
