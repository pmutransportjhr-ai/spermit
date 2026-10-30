<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-PERMIT-DURATION-MASTER-NEW.aspx.cs" Inherits="RTA_PERMIT_DURATION_MASTER_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />

    <style>
        .table > tbody > tr > td {
            padding-left: 0px !important;
            padding-right: 10px !important;
            border-top: none !important;
        }
    </style>
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
                            <span>Permit Duration</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">Permit Duration Add/Edit</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                        <!-- BEGIN FORM-->
                                        <form id="formState" runat="server" action="#" class="form-horizontal" autocomplete="off">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <div class="col-md-2"><asp:HiddenField ID="ID" runat="server" /></div>
                                                    <label class="col-md-2 control-label">Select Permit Type <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                                <asp:DropDownList AppendDataBoundItems="true" ID="ddlPermitType" CssClass="form-control select2"  runat="server"> 
                                                            
                                                                </asp:DropDownList>
                                                                                                             
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Permit Duration<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtPermitDuration" MaxLength="1" CssClass="number form-control" runat="server"></asp:TextBox>
                                                      
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                                                                
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Select Year/ Month <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlYM" CssClass="form-control select2" runat="server">
                                                            <asp:ListItem Value="0">--Please select --</asp:ListItem>
                                                            <asp:ListItem Value="Years">Years</asp:ListItem>
                                                            <asp:ListItem Value="Months">Months</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                 <div class="col-md-3"></div>
                                                </div>
                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-4">
                                                         <asp:Button ID="btnSave" runat="server" CssClass="btn blue" Text="Save" OnClick="btnSave_Click" OnClientClick="return fnDurationValidate()" />
                                                       <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Cancel" OnClick="btnCancel_Click" />
                                                    </div>
                                                    <div class="col-md-6"></div>
                                                </div>
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
     <script src="assets/js/RTA-PERMIT-DURATION-MASTER.js" type="text/javascript"></script>
   
</asp:Content>

