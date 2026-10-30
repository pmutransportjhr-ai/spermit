<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RTA_VERIFY.aspx.cs" Inherits="RTA_VERIFY" %>


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
            <!-- BEGIN HEADER -->
            <div class="page-header navbar navbar-fixed-top" style="height: 100px !important;">
                <!-- BEGIN HEADER INNER -->
                <div class="page-header-inner ">
                    <!-- BEGIN LOGO -->
                    <div class="row">
                        <div class="col-md-2">
                            <div class="page-logo">
                                <a href="#"><img src="../assets/img/logo.png" alt="logo" class="logo-default" /></a>
                            </div>
                        </div>
                            <!-- END LOGO -->
                        <div class="col-md-8 title-center">
                            <span class="title-center1">Government of Jharkhand</span>
                            <br /><span class="title-center2">Transport Department</span>
                            <br /><span class="title-center3">Online State Permit System</span>
                        </div>
                        <div class="col-md-2 title-center">
                            <table>
                                <tr>
                                    <td><asp:Button ID="lkbLogout" runat="server" OnClick="lkbLogout_Click" Text="Logout" CssClass="btn btn-circle-left btn-success" style="margin-top: 10px;"></asp:Button></td>
                                </tr>
                                <tr>
                                    <td><asp:Button ID="btnPassword" runat="server" OnClick="btnPassword_Click" Text="Change Password" CssClass="btn btn-circle-left btn-success" style="margin-top: 10px;"></asp:Button></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        
            <div class="page-container">
        <div class="page-content-wrapper">
            <!-- BEGIN CONTENT BODY -->

            <div class="page-content" style="padding-left:0; margin-left:0;">
                <!-- BEGIN LOGIN FORM -->
                
                <div class="row" style="padding-top:40px">
                   
                    <div class="col-md-12">
                        <div class="portlet light bordered">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class="icon-equalizer font-red-sunglo"></i>
                                    <span class="caption-subject font-red-sunglo bold uppercase">Verify for Tax excemption (RTA)</span>
                                    <%--<span class="caption-helper">form actions on top...</span>--%>
                                </div>

                            </div>
            
                <div id="dvGrid" style="padding: 10px; width: 100%">
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                    <div class="r3_counter_box">
                        <div class="stats">
                            <table>
                                <tr>
                                    <td><asp:TextBox ID="txtSearch" runat="server" class="form-control" placeholder="Enter Registration No." CssClass="form-control" autofill="off"></asp:TextBox> </td>
                                    <td>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-success" ToolTip="Search"  OnClick="btnSearch_Click"/> </td>
                                    <td colspan="6">&nbsp;&nbsp;&nbsp;<asp:Button ID="btnVerify" runat="server" Text="Verified Details" class="btn btn-success" ToolTip="Verify Details" style="margin-left:900px;" OnClick="btnVerify_Click" /> </td>
                                </tr>
                            </table>
                        </div>                     
                    </div>
                </div>
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
                            <asp:Panel ID="panelGrid" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" onselectedindexchanged="GridView1_SelectedIndexChanged"
                        PageSize="25" AllowPaging="true" OnPageIndexChanging="OnPaging" EmptyDataText="No records has been added." >
                        <Columns>
                            <asp:TemplateField HeaderText="SNo">
                                <ItemTemplate>
                                    <span><%#Container.DataItemIndex + 1%></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="rc_no" HeaderText="Reg. No." />
                            <asp:BoundField DataField="chasis_no" HeaderText="Chasis" />
                            <asp:BoundField DataField="owner" HeaderText="Owner" />
                            <asp:BoundField DataField="mobile_no" HeaderText="Mobile" />
                            <asp:BoundField DataField="rto_no" HeaderText="DTO" />
                            <asp:BoundField DataField="vehicle_class" HeaderText="Class" />
                            <asp:BoundField DataField="category" HeaderText="Category" />
                            <asp:BoundField DataField="permit_no" HeaderText="Permit No." />
                            <asp:BoundField DataField="p_valid_upto" HeaderText="Permit Validity" />
                            <asp:BoundField DataField="rc_tax_upto" HeaderText="Tax Upto" />
                            <asp:BoundField DataField="period_from" HeaderText="Period To" />
                            <asp:BoundField DataField="period_to" HeaderText="Period To" />
                            <asp:BoundField DataField="remarks" HeaderText="Remarks" />
                            <asp:BoundField DataField="status" HeaderText="Status" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton Text="Select" ID="lnkSelect" runat="server" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                
            </asp:Panel>
        
            <asp:Panel ID="panelUpdate" runat="server">
                <div>
                    <div class="row">
                        <div class="col-md-12 col-sm-12">
                            <table class="table table-striped table-bordered table-hover">
                                <tr>
                                    <td><b><asp:Label Text="Reg. No" runat="server" /></b></td>
                                    <td><asp:TextBox ID="txtrc_no" runat="server" CssClass="form-control" /></td>
                                    <td><b><asp:Label Text="Chasis" runat="server"/></b></td>
                                    <td><asp:TextBox ID="txtChasis" runat="server" CssClass="form-control"/></td>
                                    <td><b><asp:Label Text="Mobile No" runat="server" Visible="false"/></b></td>
                                    <td><asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" Visible="false"/></td>
                                </tr>
                                <tr>
                                    <td><b><asp:Label Text="Status" runat="server" /></b></td>
                                    <td>
                                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value="" />
                                        <asp:ListItem Text="Verify" Value="Verify" />
                                        <asp:ListItem Text="Reject" Value="Reject" />
                                        </asp:DropDownList>
                                    </td>
                                    <td><asp:Label Text="Remarks" runat="server" /></td>
                                    <td><asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"/></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-success" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-success" OnClick="btnBack_Click"/>
                                    </td>
                                </tr>
                                </table>
                            </div>
                        
                    </div>
            </div>
            
        </asp:Panel>

                <asp:Panel ID="panelVerify" runat="server">
                    <asp:GridView ID="GridVerify" runat="server" AutoGenerateColumns="false" 
                        PageSize="25" AllowPaging="true" OnPageIndexChanging="OnPaging1" EmptyDataText="No records has been added." >
                        <Columns>
                            <asp:TemplateField HeaderText="SNo">
                                <ItemTemplate>
                                    <span><%#Container.DataItemIndex + 1%></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="rc_no" HeaderText="Reg. No." />
                            <asp:BoundField DataField="chasis_no" HeaderText="Chasis" />
                            <asp:BoundField DataField="owner" HeaderText="Owner" />
                            <asp:BoundField DataField="mobile_no" HeaderText="Mobile" />
                            <asp:BoundField DataField="rto_no" HeaderText="DTO" />
                            <asp:BoundField DataField="vehicle_class" HeaderText="Class" />
                            <asp:BoundField DataField="category" HeaderText="Category" />
                            <asp:BoundField DataField="permit_no" HeaderText="Permit No." />
                            <asp:BoundField DataField="p_valid_upto" HeaderText="Permit Validity" />
                            <asp:BoundField DataField="rc_tax_upto" HeaderText="Tax Upto" />
                            <asp:BoundField DataField="period_from" HeaderText="Period To" />
                            <asp:BoundField DataField="period_to" HeaderText="Period To" />
                            <asp:BoundField DataField="remarks" HeaderText="Remarks" />
                            <asp:BoundField DataField="status" HeaderText="Status" />
                        </Columns>
                    </asp:GridView>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <%--<asp:Button ID="btnDownload" runat="server" CssClass="btn btn-success" Text="Downlaod" OnClick="Excel_Click"/>--%>
                
            </asp:Panel>

        </div></div></div></div></div>
    </form>
    
</body>
</html>
