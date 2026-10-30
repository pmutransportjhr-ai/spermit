<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-RTO-MASTER-NEW.aspx.cs" Inherits="RTA_RTO_MASTER_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                            <span>RTO</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">RTO Add/Edit</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                        <!-- BEGIN FORM-->
                                        <form id="formRTO" runat="server" action="#" class="form-horizontal" autocomplete="off">
                                            <asp:HiddenField ID="IDXVAL" runat="server" />
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <%--<label class="col-md-2 control-label">RTO CODE <span class="required" aria-required="true">*</span></label>--%>
                                                    <div class="col-md-5">
                                                          <%-- <asp:TextBox ID="txtRTOCode" CssClass="regtxtnumber form-control" runat="server"></asp:TextBox>--%>
                                                            <asp:HiddenField ID="txtRTOCode" runat="server" />
                                                      
                                                      </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">RTO Name <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtRTOName" MaxLength="60" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                     </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                               <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Phone No <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtPhoneNo" MaxLength="11" CssClass="number form-control" runat="server"></asp:TextBox>
                                                     </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Email Id <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtEmailId" MaxLength="80" CssClass="form-control" runat="server"></asp:TextBox>
                                                     </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Address <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="address form-control" runat="server"></asp:TextBox>
                                                     </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                                 <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">TreasCode <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtTREASCODE" MaxLength="20"  CssClass="regtxtnumber form-control" runat="server"></asp:TextBox>
                                                     </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                                 <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">IFMS Office Code <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtIFMSOFFICECODE" MaxLength="20" CssClass="regtxtnumber form-control" runat="server"></asp:TextBox>
                                                     </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                                <%--<div class="form-group last">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Status <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">--select status--</asp:ListItem>
                                                            <asp:ListItem Value="Active">
                                                Active
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="Inactive">
                                                Inactive
                                                            </asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>--%>


                                            </div>
                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-4">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Save" OnClientClick="return fnRTOValidate()" OnClick="btnSave_Click" />
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
    <script src="assets/js/RTA-RTO-MASTER.js" type="text/javascript"></script>

</asp:Content>



