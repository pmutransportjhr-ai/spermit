<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-USER-SETUP-NEW.aspx.cs" Inherits="RTA_USER_SETUP_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
      <script src="assets/pages/scripts/sha512.js" type="text/javascript"></script>
    <script src="assets/js/RTA-USER-MASTER.js" type="text/javascript"></script>
    <script type="text/javascript">
        function setpsd() {
        
            var Pasword = document.getElementById('<%=txtPassword.ClientID%>').value.trim();
            var salt = '<%=Session["salt"]%>';
            var hash = hex_sha512(hex_sha512(Pasword) + salt);
            document.getElementById('<%=txtPassword.ClientID%>').value = hash;
            document.getElementById('<%=txtConfirmPassword.ClientID%>').value = hash;
            document.getElementById('<%=hfpwd.ClientID%>').value = hash;
        
        }

    </script>
    

    <div class="page-container">
        <div class="page-content-wrapper">
            <div class="page-content">
                <div class="container">
                    <ul class="page-breadcrumb breadcrumb">
                        <li>
                            <a href="RTA-DASHBOARD.aspx">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="#">User Setup</a>
                            <i class="fa fa-circle"></i>
                        </li>

                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">User Master Add/Edit</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                        <!-- BEGIN FORM-->
                                        <form id="formCity" runat="server" action="#" class="form-horizontal" autocomplete="off">

                                            <div class="form-body">
                                                <div class="form-group">
                                                    <div class="col-md-2"><asp:HiddenField ID="ID" runat="server" /></div>
                                                    <label class="col-md-2 control-label">RTO <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList AppendDataBoundItems="true" ID="ddlRTO" CssClass="form-control select2" runat="server">
                                                        </asp:DropDownList>
                                                       

                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Name <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:HiddenField ID="txtEmployeeID" Value="0" runat="server" /><asp:HiddenField ID="hfpwd" runat="server" />
                                                        <asp:TextBox ID="txtName" MaxLength="60" runat="server" CssClass="regtxt form-control"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Login Id<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtLoginId"  autocomplete="off" MaxLength="60" runat="server" CssClass="regtxtnumber form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Password<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtPassword"  autocomplete="off" MaxLength="20" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Confirm Password<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtConfirmPassword" MaxLength="20" TextMode="Password"  runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Designation<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList AppendDataBoundItems="true" ID="ddlDesignation" CssClass="form-control select2" runat="server">
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Department<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList AppendDataBoundItems="true" ID="ddlDepartment" CssClass="form-control select2" runat="server">
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Mobile No<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="number form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Email Id<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                  <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">User Level<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <%--<asp:TextBox ID="txtUserlevel" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                        
                                                        <asp:DropDownList ID="ddlULevel" runat="server" CssClass="form-control select2">
                                                            <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                            <asp:ListItem Value="ASSISTANT">ASSISTANT</asp:ListItem>
                                                            <asp:ListItem Value="SO">SO</asp:ListItem>
                                                            <asp:ListItem Value="RTA">RTA</asp:ListItem>
                                                            <asp:ListItem Value="APPROVER">APPROVER</asp:ListItem>
                                                        </asp:DropDownList>
                                                   
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Security Question<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlSecurityQuestion" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">--select security question--</asp:ListItem>
                                                            <asp:ListItem Value="What is the name of your favorite pet?">What is the name of your favorite pet?</asp:ListItem>
                                                            <asp:ListItem Value="Who is your favorite actor, musician, or artist?">Who is your favorite actor, musician, or artist?</asp:ListItem>
                                                            <asp:ListItem Value="In what city were you born?">In what city were you born?</asp:ListItem>
                                                            <asp:ListItem Value="What is the name of your first school?">What is the name of your first school?</asp:ListItem>
                                                            <asp:ListItem Value="What is your mother's maiden name?">What is your mother's maiden name?</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Answer<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtSecurityAnswer" MaxLength="100" runat="server" CssClass="address form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group last">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Status <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">--select status--</asp:ListItem>
                                                            <asp:ListItem Value="Active">Active</asp:ListItem>
                                                            <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                            </div>
                                            <%--<div class="page-content-inner">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="portlet light portlet-fit">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    <i class="icon-settings font-red"></i>
                                                                    <span class="caption-subject font-red sbold uppercase">Menu Access</span>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body form">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-3"></div>
                                                    <div class="col-md-6">
                                                        <div class="portlet-body table-both-scroll">
                                                            <table class="table table-striped table-bordered table-hover order-column" id="sample_4">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Menu Item</th>
                                                                        <th>Read Access</th>
                                                                        <th>Write Access</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tdbMenuAccess">


                                                                    <%=menuTable %>
                                                                </tbody>
                                                            </table>

                                                        </div>

                                                    </div>

                                                </div>
                                                <div class="col-md-3"></div>
                                            </div>--%>

                                            <asp:HiddenField ID="hdnMenuItems" runat="server" />
                                            <asp:HiddenField ID="hdnJsonData" runat="server" />
                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-4">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Save" OnClientClick="return MenuItems()" OnClick="btnSave_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Cancel" OnClick="btnCancel_Click1" />

                                                    </div>
                                                    <div class="col-md-6"></div>
                                                </div>
                                            </div>
                                        </form>
                                        <!-- END FORM-->
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    
    
</asp:Content>

