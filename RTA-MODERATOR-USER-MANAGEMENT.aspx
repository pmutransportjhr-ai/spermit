<%@ Page Title="RTA Moderator - User Management" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-MODERATOR-USER-MANAGEMENT.aspx.cs" Inherits="RTA_MODERATOR_USER_MANAGEMENT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .filter-box {
            background-color: #f7f9fa;
            border: 1px solid #e7ecf1;
            padding: 15px;
            margin-bottom: 20px;
            border-radius: 4px;
        }
        .action-btn {
            margin-right: 3px;
            margin-bottom: 3px;
        }
        .badge-active {
            background-color: #26C281;
            color: #fff;
            padding: 4px 8px;
            border-radius: 3px;
            font-weight: 600;
        }
        .badge-inactive {
            background-color: #E7505A;
            color: #fff;
            padding: 4px 8px;
            border-radius: 3px;
            font-weight: 600;
        }
        .modal-header-primary {
            background-color: #3598dc;
            color: #ffffff;
        }
        .modal-header-primary .modal-title {
            color: #ffffff;
            font-weight: 600;
        }
        .modal-header-warning {
            background-color: #f3c200;
            color: #ffffff;
        }
        .modal-header-warning .modal-title {
            color: #ffffff;
            font-weight: 600;
        }
        /* Pagination Styles */
        .pagination-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 12px 0;
            flex-wrap: wrap;
        }
        .pagination-info {
            color: #555;
            font-size: 13px;
            font-weight: 600;
        }
        .pagination-controls {
            display: flex;
            align-items: center;
            gap: 3px;
        }
        .pagination-controls .btn {
            min-width: 34px;
            padding: 5px 10px;
            font-size: 12px;
        }
        .pagination-controls .btn.active-page {
            background-color: #3598dc;
            color: #fff;
            border-color: #3598dc;
            font-weight: bold;
        }
        .pagination-controls .btn.page-ellipsis {
            border: none;
            background: none;
            cursor: default;
            padding: 5px 4px;
            min-width: auto;
        }
        .no-records-row td {
            text-align: center;
            padding: 30px;
            color: #888;
            font-size: 14px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <!-- BEGIN BREADCRUMBS -->
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="RTA-DASHBOARD.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="#">RTA Moderator</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>User Management</span>
                            </li>
                        </ul>
                        <!-- END BREADCRUMBS -->

                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-user font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">RTA Moderator - User Management</span>
                                            </div>
                                        </div>

                                        <div class="portlet-body">
                                            <!-- SEARCH AND FILTER TOOLBAR -->
                                            <div class="filter-box">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <div class="form-group" style="margin-bottom:0;">
                                                            <label class="control-label bold">Search Keyword:</label>
                                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Placeholder="Search ID, Name, Login ID, Email, Mobile..."></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group" style="margin-bottom:0;">
                                                            <label class="control-label bold">Status:</label>
                                                            <asp:DropDownList ID="ddlStatusFilter" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="ALL" Text="-- All Statuses --"></asp:ListItem>
                                                                <asp:ListItem Value="Active" Text="Active"></asp:ListItem>
                                                                <asp:ListItem Value="Inactive" Text="Inactive"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group" style="margin-bottom:0;">
                                                            <label class="control-label bold">RTO Office:</label>
                                                            <asp:DropDownList ID="ddlRTOFilter" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2" style="padding-top: 25px;">
                                                        <asp:Button ID="btnFilter" runat="server" CssClass="btn green" Text="Search" OnClick="btnFilter_Click" />
                                                        <asp:Button ID="btnResetFilter" runat="server" CssClass="btn dark btn-outline" Text="Reset" OnClick="btnResetFilter_Click" />
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- USER LIST GRID TABLE - EXACTLY 8 COLUMNS -->
                                            <div class="table-responsive">
                                                <table class="table table-striped table-hover table-bordered" id="tblUserList">
                                                    <thead>
                                                        <tr class="uppercase">
                                                            <th>Employee ID</th>
                                                            <th>Employee Name</th>
                                                            <th>Login ID</th>
                                                            <th>Email</th>
                                                            <th>Mobile</th>
                                                            <th>User Role</th>
                                                            <th>Status</th>
                                                            <th style="min-width: 220px;">Actions</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rptUsers" runat="server" OnItemCommand="rptUsers_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%# Eval("EMPLOYEE_ID") %></td>
                                                                    <td><strong><%# Eval("EMPLOYEE_NAME") %></strong></td>
                                                                    <td><%# Eval("LOGIN_ID") %></td>
                                                                    <td><%# Eval("EMAIL_ID") %></td>
                                                                    <td><%# Eval("MOBILE_NUMBER") %></td>
                                                                    <td><span class="label label-sm label-info"><%# Eval("USER_ROLE") %></span></td>
                                                                    <td>
                                                                        <span class='<%# Convert.ToString(Eval("STATUS")) == "Active" ? "badge-active" : "badge-inactive" %>'>
                                                                            <%# Eval("STATUS") %>
                                                                        </span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="btn btn-xs blue action-btn" CommandName="EditUser" CommandArgument='<%# Eval("EMPLOYEE_ID") %>'>
                                                                            <i class="fa fa-pencil"></i> Edit
                                                                        </asp:LinkButton>
                                                                        
                                                                        <asp:LinkButton ID="lnkPassword" runat="server" CssClass="btn btn-xs yellow-gold action-btn" CommandName="ChangePwd" CommandArgument='<%# Eval("EMPLOYEE_ID") + "|" + Eval("EMPLOYEE_NAME") + "|" + Eval("LOGIN_ID") %>'>
                                                                            <i class="fa fa-key"></i> Password
                                                                        </asp:LinkButton>

                                                                        <asp:LinkButton ID="lnkStatus" runat="server" 
                                                                            CssClass='<%# Convert.ToString(Eval("STATUS")) == "Active" ? "btn btn-xs red action-btn" : "btn btn-xs green action-btn" %>'
                                                                            CommandName="ToggleStatus" 
                                                                            CommandArgument='<%# Eval("EMPLOYEE_ID") + "|" + Eval("STATUS") + "|" + Eval("EMPLOYEE_NAME") %>'>
                                                                            <i class='<%# Convert.ToString(Eval("STATUS")) == "Active" ? "fa fa-times" : "fa fa-check" %>'></i> 
                                                                            <%# Convert.ToString(Eval("STATUS")) == "Active" ? "Deactivate" : "Activate" %>
                                                                        </asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>

                                            <!-- PAGINATION -->
                                            <asp:HiddenField ID="hdnCurrentPage" runat="server" Value="1" />
                                            <asp:HiddenField ID="hdnTotalRecords" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPageSize" runat="server" Value="10" />

                                            <div class="pagination-container">
                                                <div class="pagination-info">
                                                    <asp:Label ID="lblPaginationInfo" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="pagination-controls">
                                                    <asp:LinkButton ID="btnFirst" runat="server" CssClass="btn btn-sm default" OnClick="btnFirst_Click" ToolTip="First Page">
                                                        <i class="fa fa-angle-double-left"></i> First
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnPrev" runat="server" CssClass="btn btn-sm default" OnClick="btnPrev_Click" ToolTip="Previous Page">
                                                        <i class="fa fa-angle-left"></i> Previous
                                                    </asp:LinkButton>
                                                    <asp:Repeater ID="rptPager" runat="server" OnItemCommand="rptPager_ItemCommand">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkPage" runat="server"
                                                                CssClass='<%# Convert.ToBoolean(Eval("IsCurrentPage")) ? "btn btn-sm active-page" : (Convert.ToBoolean(Eval("IsEllipsis")) ? "btn btn-sm page-ellipsis" : "btn btn-sm default") %>'
                                                                CommandName="GoToPage"
                                                                CommandArgument='<%# Eval("PageNumber") %>'
                                                                Enabled='<%# !Convert.ToBoolean(Eval("IsEllipsis")) %>'
                                                                Text='<%# Eval("DisplayText") %>'>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:LinkButton ID="btnNext" runat="server" CssClass="btn btn-sm default" OnClick="btnNext_Click" ToolTip="Next Page">
                                                        Next <i class="fa fa-angle-right"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnLast" runat="server" CssClass="btn btn-sm default" OnClick="btnLast_Click" ToolTip="Last Page">
                                                        Last <i class="fa fa-angle-double-right"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- ================================================================= -->
        <!-- EDIT USER MODAL                                                   -->
        <!-- ================================================================= -->
        <div class="modal fade" id="editModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header modal-header-primary">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><i class="fa fa-edit"></i> Edit User Information</h4>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hdnEditEmpId" runat="server" />
                        
                        <div class="row">
                            <div class="col-md-12">
                                <h4 class="form-section font-blue bold"><i class="fa fa-user"></i> User Account Details</h4>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label bold">Employee ID:</label>
                                    <asp:TextBox ID="txtEditEmpId" runat="server" CssClass="form-control" ReadOnly="true" BackColor="#eee"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label bold">Login ID:</label>
                                    <asp:TextBox ID="txtEditLoginId" runat="server" CssClass="form-control" ReadOnly="true" BackColor="#eee"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label bold">User Role:</label>
                                    <asp:TextBox ID="txtEditUserRole" runat="server" CssClass="form-control" ReadOnly="true" BackColor="#eee"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label bold">Employee Name <span class="font-red">*</span>:</label>
                                    <asp:TextBox ID="txtEditEmpName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label bold">Email Address <span class="font-red">*</span>:</label>
                                    <asp:TextBox ID="txtEditEmail" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label bold">Mobile Number <span class="font-red">*</span>:</label>
                                    <asp:TextBox ID="txtEditMobile" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label bold">Account Status <span class="font-red">*</span>:</label>
                                    <asp:DropDownList ID="ddlEditStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="Active" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="Inactive" Text="Inactive"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <h4 class="form-section font-blue bold"><i class="fa fa-building"></i> Official Organization Info</h4>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label bold">Department:</label>
                                    <asp:DropDownList ID="ddlEditDept" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label bold">Designation:</label>
                                    <asp:DropDownList ID="ddlEditDesg" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label bold">RTO Office:</label>
                                    <asp:DropDownList ID="ddlEditRTO" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label bold">User Level:</label>
                                    <asp:DropDownList ID="ddlEditUserLevel" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="USER" Text="User"></asp:ListItem>
                                        <asp:ListItem Value="OFFICER" Text="Officer"></asp:ListItem>
                                        <asp:ListItem Value="MODERATOR" Text="Moderator"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnSaveEdit" runat="server" CssClass="btn green" Text="Save Changes" OnClick="btnSaveEdit_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- ================================================================= -->
        <!-- CHANGE PASSWORD MODAL                                             -->
        <!-- ================================================================= -->
        <div class="modal fade" id="passwordModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header modal-header-warning">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"><i class="fa fa-key"></i> Change User Password</h4>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hdnPwdEmpId" runat="server" />
                        <div class="alert alert-info" style="margin-bottom:15px;">
                            <div style="margin-bottom:5px;">
                                <strong>User:</strong> <asp:Label ID="lblPwdEmpName" runat="server"></asp:Label>
                            </div>
                            <div>
                                <strong>Login ID:</strong> <asp:Label ID="lblPwdLoginId" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label bold">New Password <span class="font-red">*</span>:</label>
                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" MaxLength="50"></asp:TextBox>
                            <span class="help-block">Must be at least 6 characters long.</span>
                        </div>
                        <div class="form-group">
                            <label class="control-label bold">Confirm New Password <span class="font-red">*</span>:</label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnSavePassword" runat="server" CssClass="btn yellow-gold" Text="Change Password" OnClick="btnSavePassword_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- ================================================================= -->
        <!-- STATUS TOGGLE CONFIRMATION MODAL                                  -->
        <!-- ================================================================= -->
        <div class="modal fade" id="statusModal" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title bold"><i class="fa fa-question-circle"></i> Confirm Status Update</h4>
                    </div>
                    <div class="modal-body">
                        <asp:HiddenField ID="hdnStatusEmpId" runat="server" />
                        <asp:HiddenField ID="hdnTargetStatus" runat="server" />
                        <p class="font-lg" style="margin:10px 0;">
                            <asp:Label ID="lblStatusConfirmPrompt" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">No, Cancel</button>
                        <asp:Button ID="btnConfirmStatus" runat="server" CssClass="btn blue" Text="Yes, Confirm" OnClick="btnConfirmStatus_Click" />
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript">
        function showEditModal() {
            $('#editModal').modal('show');
        }
        function hideEditModal() {
            $('#editModal').modal('hide');
        }
        function showPasswordModal() {
            $('#passwordModal').modal('show');
        }
        function hidePasswordModal() {
            $('#passwordModal').modal('hide');
        }
        function showStatusModal() {
            $('#statusModal').modal('show');
        }
        function hideStatusModal() {
            $('#statusModal').modal('hide');
        }
    </script>
</asp:Content>
