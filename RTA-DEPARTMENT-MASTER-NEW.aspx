<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-DEPARTMENT-MASTER-NEW.aspx.cs" Inherits="RTA_DEPARTMENT_MASTER_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                            <a href="#">Master</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Department</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">Department Add/Edit</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                        <!-- BEGIN FORM-->
                                        <form id="formState" runat="server" action="#" class="form-horizontal" autocomplete="off">
                                            <div class="form-body">

                                                <div class="form-group">
                                                    <div class="col-md-2"><asp:HiddenField ID="ID" runat="server" /></div>
                                                    <label class="col-md-2 control-label">Department Name <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtDepartmentName" MaxLength="30" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtDepartmentId" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                                        <%--<input id="txtStateName" class="form-control" type="text" data-tabindex="2" />
                                                                <input class="form-control" id="txtStateKey" type="text" style="display: none;" />--%>
                                                    </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                               
                                               


                                            </div>
                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-4">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Save" OnClientClick="return fnDepartmentValidate()" OnClick="btnSave_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Cancel" OnClick="btnCancel_Click" />

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
     <script src="assets/js/RTA-DEPARTMENT-MASTER.js" type="text/javascript"></script>

</asp:Content>

