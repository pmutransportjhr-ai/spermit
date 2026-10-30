<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STA-CHANGE-PASSWORD.aspx.cs" Inherits="JHT_STA_CHANGE_PASSWORD" %>

<!DOCTYPE html>
<html lang="en">
    <head runat="server">
        <meta charset="utf-8" />
        <meta http-equiv="Pragma" content="no-cache">
        <title>Change Password | STA</title>
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
        <link href="assets/pages/css/login.min.css" rel="stylesheet" type="text/css" />
        <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        
       <%-- <script src="assets/pages/scripts/crypto-js/core-min.js"></script>
        <script src="assets/pages/scripts/crypto-js/enc-utf16-min.js"></script>
        <script src="assets/pages/scripts/crypto-js/enc-base64-min.js"></script>
        <script src="assets/pages/scripts/crypto-js/aes.js"></script>--%>
        <script src="assets/pages/scripts/sha512.js" type="text/javascript"></script>
        <script src="assets/js/STA-CHANGE-PASSWORD.js" type="text/javascript"></script>


        <!-- END PAGE LEVEL STYLES -->
        <!-- BEGIN THEME LAYOUT STYLES -->
        <!-- END THEME LAYOUT STYLES -->
      
          
           
   
        <script type="text/javascript">
            function returnHashCode() {
                var PASSWORD = $('#txtoldPassword').val();
                var salt = '<%=Session["salt"]%>';
                var hash = hex_sha512(hex_sha512(PASSWORD) + salt);
                document.getElementById('hfpwdold').value = hash;
                document.getElementById('txtoldPassword').value = hash;
                
            }
            function GenerateNewPasswordHashcode()
            {
                //debugger;
                //alert("Here 2");
                var PASSWORD = $('#txtPassword').val();
                var salt = '<%=Session["salt"]%>';
                var hash = hex_sha512(hex_sha512(PASSWORD) + salt);
                document.getElementById('txtPassword').value = hash;
                document.getElementById('txtCPassword').value = hash;
                document.getElementById('hfpwd').value = hash;
            }
            
        </script>
    </head>
    <!-- END HEAD -->
<body class="page-container-bg-solid page-header-fixed" style="background-color: #364150 !important;">
 <%--<form  method="post" runat="server"   autocomplete="off">--%>
           
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
                                    <span class="caption-subject font-red-sunglo bold uppercase">Change Password</span>
                                    <%--<span class="caption-helper">form actions on top...</span>--%>
                                </div>

                            </div>
                            <div class="portlet-body form">
                                <!-- BEGIN FORM-->
                                <form runat="server" action="#" id="frmlogin" class="form-horizontal">
                              
       
                   
                    <div class="portlet-body">
                        <div class="row">
                            <div class="col-md-12 col-sm-12">
                                <input id="txtUserKey" type="text" runat="server" style="display:none;" />
                                <input id="txtoldPasswordhide" type="text" runat="server" style="display:none;" />
                                <input type="text" style="display:none;" id="hfpwd" runat="server" />
                                <input type="text" style="display:none;" id="hfpwdold" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-sm-5"> <label for="form_control"><b>Old Password</b></label>
 

                            </div>
                            <div class="col-md-7 col-sm-7">
                                <div class="form-group">
                                   
                                <input class="form-control"  id="txtoldPassword"  runat="server" type="password" data-tabindex="1" />
                               
                     
                                           
                                </div>
                            </div>
                           
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-sm-5"> <label for="form_control"><b>New Password</b></label></div>
                            <div class="col-md-7 col-sm-7">
                                <div class="form-group">
                                   
                                   <input class="form-control"  runat="server" id="txtPassword"  type="password" data-tabindex="2" />
                                           
                                </div>
                            </div>
                           
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-sm-5"> <label for="form_control"><b>Confirm Password</b></label></div>
                            <div class="col-md-7 col-sm-7">
                                <div class="form-group">
                                   
                                    <input class="form-control" id="txtCPassword"  runat="server"  type="password" data-tabindex="3" />
                                    
                                </div>
                            </div>
                            
                        </div>
                        <div class="row">
                            <div class="col-md-5"></div>
                            <div class="col-md-7">
                                <div class="form-group">
                                   <asp:Button ID="btnSave" CssClass="btn green" TabIndex="4" runat="server" OnClientClick="return fnSaveUser();"  Text="Save" OnClick="btnSave_Click" />
                                   <button type="button" class="btn btn-outline dark" data-tabindex="5" onclick="javascript:fnCloseModal1();">Close</button>&nbsp;
                            
                                     
                                </div>
                            </div>
                          
                        </div>
                        <br /><br /><br /><br />
                    </div>
               
        
    
    

                                    <!-- END FORM-->

                                </form>




                            </div>
                           

                        </div>
                         <div class="col-md-12">
                                <div style="text-align: center; color: white">All Rights Reserved by Government of Jharkhand, India.  </div>
                            </div>
                    </div>
                    <!-- END LOGIN FORM -->
                    <div class="col-md-3">
                    </div>
                </div>
            </div>
        </div>
    </div>       
           
            <!-- BEGIN CONTENT BODY -->
            <!-- BEGIN PAGE CONTENT BODY -->
             

        
        <div id="loadingmessage" class="modal fade" tabindex="-1" data-backdrop="static" style="top: 40%; background-color: transparent;border:none;-webkit-box-shadow:none;box-shadow:none;" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" style="left:40%;width:200px;border:3px solid #999">
                <div class="modal-body" style="padding: 0px 0px 0px 10px;">
                    <img id="loading" src="assets/global/img/loading-spinner-blue.gif" style="padding: 10px 0 10px 10px; z-index: 10600" alt="loading.........." /><span style="font-size:16px"><b> Please Wait.....</b></span>
                </div>
            </div>
        </div>
    </div>

       
     <%--   <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
     --%>   <script src="assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="assets/global/scripts/app.min.js" type="text/javascript"></script>
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
 <%--</form>    --%>    
</body>
</html>