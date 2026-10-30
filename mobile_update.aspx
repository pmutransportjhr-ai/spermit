<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="MasterPage.master" CodeFile="mobile_update.aspx.cs" Inherits="mobile_update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-container">
        <div class="page-content-wrapper">
            <div class="page-content">
                <div class="container">
                    <ul class="page-breadcrumb breadcrumb">
                        <li>
                            <a href="PermitHomePage.aspx">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="mobile_update.aspx">MOBILE UPDATE</a>
                        </li>
                    </ul>

                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">MOBILE UPDATE</span>
                                        </div>
                                    </div>

                                    <div class="portlet-body" style="height:500px;">
                                        <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">

                                            <div class="col-md-12">

                                                <!-- LEFT SIDE -->
                                                <div class="col-md-6">

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label class="control-label">Select STA/RTA :</label><span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:DropDownList ID="ddlTA" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="STA">STA PERMIT</asp:ListItem>
                                                                <asp:ListItem Value="CS">STA COUNTERSIGNATURE</asp:ListItem>
                                                                <asp:ListItem Value="RTA">RTA PERMIT</asp:ListItem>
                                                                <asp:ListItem Value="SB">SCHOOL BUS PERMIT</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label class="control-label">Vehicle Reg. No. :</label><span style="color:red">*</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtRegNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label class="control-label">Permit No. :</label><span style="color:red">*</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtPermitNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label class="control-label">Engine No. :</label><span style="color:red">*</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtEngineNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <label class="control-label">Chasis No. :</label><span style="color:red">*</span>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:TextBox ID="txtChasisNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <br />

                                                    <div class="row">
                                                        <div class="col-md-4"></div>
                                                        <div class="col-md-6">
                                                            <asp:Button Text="Validate" CssClass="btn btn-primary" ID="btnValidate" runat="server" OnClick="btnValidate_Click" />
                                                        </div>
                                                    </div>

                                                    <br />

                                                    <div class="row" id="roeHide" runat="server" visible="false">
                                                        <div class="col-md-3"><asp:Label ID="lblEngineNo" runat="server"></asp:Label></div>
                                                        <div class="col-md-3"><asp:Label ID="lblChasisNo" runat="server"></asp:Label></div>
                                                        <div class="col-md-3"><asp:Label ID="lblAppNo" runat="server"></asp:Label></div>
                                                        <div class="col-md-3"><asp:Label ID="lblMobileNo" runat="server"></asp:Label></div>
                                                    </div>

                                                </div>

                                                <!-- RIGHT SIDE (OTP Removed) -->
                                                <div class="col-md-6">
                                                    <div id="divMob" runat="server">
                                                        <div class="row">
                                                            <div class="col-md-3" style="padding-right:3px;">
                                                                <label class="control-label">New Mobile No.:</label><span style="color:red">*</span>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtNewMobile" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:Button Text="UPDATE" CssClass="btn btn-primary" ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" />
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Button Text="Cancel" CssClass="btn btn-danger" runat="server" ID="btnCancel" OnClick="btnCancel_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                        </form>
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
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
</asp:Content>
