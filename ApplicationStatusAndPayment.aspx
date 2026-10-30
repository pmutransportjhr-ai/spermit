<%@ Page Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="ApplicationStatusAndPayment.aspx.cs" Inherits="MakePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="Form1" runat="server" class="form-horizontal" autocomplete="off">

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
                                <span>View Application Status </span>
                            </li>
                        </ul>
                        <div class="page-content-inner">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">View Application Status</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body form">
                                            <asp:HiddenField ID="EncryptedParameter" runat="server"></asp:HiddenField>
                                            <div class="portlet-body">
                                                <div class="row">
                                                    <div class="col-md-1"></div>
                                                    <div class="col-md-6">

                                                        <label><b>Application Number</b></label>

                                                    </div>
                                                    <%--<div class="col-md-3">

                                                        <label><b>Type</b></label>

                                                    </div>
                                                    <div class="col-md-2">

                                                        <label><b>Option</b></label>

                                                    </div>--%>
                                                    <div class="col-md-5"></div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1"></div>



                                                    <div class="col-md-6">

                                                        <input type="text" runat="server" class="form-control" id="txtApplicationNumber" placeholder="Application Number" />

                                                    </div>
                                                    <%--<div class="col-md-3">

                                                        <asp:DropDownList ID="ddlApplicationFor" AppendDataBoundItems="true" CssClass="form-control select2" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">

                                                        <asp:DropDownList ID="ddlOptions" CssClass="form-control select2" runat="server">
                                                            <asp:ListItem Value="0">--select Option--</asp:ListItem>
                                                            <asp:ListItem Value="Permit">Permit</asp:ListItem>
                                                            <asp:ListItem Value="Counter Signature">Counter Signature</asp:ListItem>
                                                            <asp:ListItem Value="Objections">Objections</asp:ListItem>
                                                            <asp:ListItem Value="Replacement of Vehicle">Replacement of Vehicle</asp:ListItem>
                                                            <asp:ListItem Value="Transfer of Permit">Transfer of Permit</asp:ListItem>
                                                            <asp:ListItem Value="Renewal of Permit">Renewal of Permit</asp:ListItem>
                                                            <asp:ListItem Value="Duplicate Permit">Duplicate Permit</asp:ListItem>
                                                            <asp:ListItem Value="Surrender of Permit">Surrender of Permit</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>--%>
                                                    <div class="col-md-5">


                                                        <asp:Button ToolTip="Search" ID="btnSearch" runat="server" CssClass="btn green" Text="Show Status" OnClientClick="return fnAppSearchValidate()" OnClick="btnSearch_Click" />
                                                        &nbsp;<asp:Button ToolTip="Clear" ID="btnClear" runat="server" CssClass="btn green" Text="Clear" OnClick="btnClear_Click" />


                                                        &nbsp;<asp:Button ToolTip="Know your Application number"
                                                            runat="server"
                                                            CssClass="btn btn-info"
                                                            OnClick="Unnamed_Click"
                                                            Text="Know Your Aplication Number" />


                                                    </div>




                                                </div>
                                                <br />
                                            </div>
                                            <div class="row">

                                                <div class="col-md-1"></div>
                                                <div class="col-md-10">
                                                    <asp:Label ID="lblMessage" Visible="False" Font-Bold="true" runat="server" Text="Label"></asp:Label>
                                                    <asp:HiddenField ID="HFTableName" runat="server" />
                                                    <asp:HiddenField ID="HFRegNo" runat="server" />
                                                </div>
                                                <div class="col-md-1"></div>
                                            </div>
                                            <br />
                                            <div id="DivMobileValidate" runat="server" visible="false">

                                                <div class="row">
                                                    <div class="col-md-1"></div>
                                                    <div class="col-md-3">Enter Your Mobile Number </div>
                                                    <div class="col-md-3">
                                                        <input type="text" runat="server" class="number form-control" id="txtMobileNumber" placeholder="Mobile Number" />
                                                    </div>
                                                    <div class="col-md-4">


                                                        <asp:Button ToolTip="Send OTP" ID="btnOTP" runat="server" CssClass="btn green" Text="Send OTP" OnClick="btnOTP_Click" OnClientClick="return fnMobileValidate()" />



                                                    </div>
                                                    <div class="col-md-1"></div>
                                                </div>
                                                <br />
                                            </div>

                                            <div id="DivOTPMatch" runat="server" visible="false">

                                                <div class="row">
                                                    <div class="col-md-1"></div>
                                                    <div class="col-md-3">OTP: </div>
                                                    <div class="col-md-3">
                                                        <input type="text" runat="server" class="number form-control" id="txtOTP" placeholder="------" />
                                                    </div>
                                                    <div class="col-md-4">


                                                        <asp:Button ToolTip="Verify" ID="btnVerify" runat="server" CssClass="btn green" Text="Verify" OnClick="btnVerify_Click" OnClientClick="return fnOTPValidate()" />



                                                    </div>
                                                    <div class="col-md-1"></div>
                                                </div>
                                            </div>

                                            <div id="DivList" runat="server" visible="false">

                                                <br />
                                                <div class="row">
                                                    <div class="col-md-1"></div>
                                                    <div class="col-md-10">

                                                        <p id="tbdCityList">
                                                            <%=FillList %>
                                                        </p>

                                                    </div>
                                                    <div class="col-md-1"></div>
                                                </div>
                                            </div>
                                            <br />


                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="assets/js/ViewApplicationStatus.js" type="text/javascript"></script>
</asp:Content>