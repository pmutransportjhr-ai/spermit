<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="JHTRANSPORT.master" CodeFile="OFFICE-NOTICE.aspx.cs"
    Inherits="JHT_OFFICE_NOTICE" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
        <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet"
            type="text/css" />
        <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet"
            type="text/css" />
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
                                <a href="#">Notice Borad</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="OFFICE-NOTICE.aspx">Office Notice</a>
                            </li>

                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">Office
                                                    Notice</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <form id="FormID1" runat="server" class="form-horizontal"
                                                autocomplete="off">
                                                <div class="form-group">
                                                    <div class="col-md-4" style="text-align:center;font-weight:bold;">
                                                        Serivce:
                                                    </div>
                                                    <div class="col-md-4" style="text-align:center;font-weight:bold;">
                                                        <asp:DropDownList ID="ddlservice" runat="server"
                                                            CssClass="form-control"
                                                            OnSelectedIndexChanged="ddlservice_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                            <asp:ListItem Value="0" Selected="True" Text="All">
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="STA" Text="STA"></asp:ListItem>
                                                            <asp:ListItem Value="RTA" Text="RTA"></asp:ListItem>

                                                        </asp:DropDownList>
                                                        <%--RTA_OBJECTION_LIST.aspx--%>
                                                    </div>
                                                    <div class="col-md-4"></div>
                                                </div>
                                                <div class="row">

                                                    <div class="col-md-12" id="dvNoticeDetails" runat="server"
                                                        style="text-align:center;">


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
        <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js"
            type="text/javascript"></script>
        <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"
            type="text/javascript"></script>

    </asp:Content>