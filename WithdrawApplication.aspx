<%@ Page Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="WithdrawApplication.aspx.cs" Inherits="WithdrawApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" />

    <style>
        .page-card {
            background: #fff;
            border-radius: 6px;
            padding: 25px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }

        .page-title {
            font-size: 22px;
            font-weight: 600;
            color: #c62828;
        }

        .form-label {
            font-weight: 600;
        }

        .section-divider {
            border-top: 1px solid #eee;
            margin: 25px 0;
        }
    </style>

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
                                <span>Withdraw Application</span>
                            </li>
                        </ul>


                        <div class="row">

                            <div class="col-md-12">

                                <div class="page-card">

                                    <div class="text-left">
                                        <i class="icon-settings font-red"></i>
                                        <span class="page-title">Withdraw Application</span>
                                    </div>

                                    <br />

                                    <asp:HiddenField ID="EncryptedParameter" runat="server" />



                                    <!-- Application Number -->

                                    <div class="row form-group">

                                        <div class="col-md-5">

                                            <label class="form-label">Application Number</label>

                                            <input type="text"
                                                runat="server"
                                                class="form-control"
                                                id="txtApplicationNumber"
                                                placeholder="Enter Application Number" />

                                        </div>

                                        <div class="col-md-7" style="padding-top: 25px;">

                                            <asp:Button
                                                ID="btnSearch"
                                                runat="server"
                                                CssClass="btn green"
                                                Text="Show Status"
                                                ToolTip="Search"
                                                OnClientClick="return fnAppSearchValidate()"
                                                OnClick="btnSearch_Click" />

                                            &nbsp;

                                            <asp:Button
                                                ID="btnClear"
                                                runat="server"
                                                CssClass="btn default"
                                                Text="Clear"
                                                ToolTip="Clear"
                                                OnClick="btnClear_Click" />

                                            &nbsp;

                                            <asp:Button
                                                ID="btnKnowApplication"
                                                runat="server"
                                                CssClass="btn blue"
                                                Text="Know Your Application Number"
                                                ToolTip="Know your Application number"
                                                OnClick="Unnamed_Click" />

                                        </div>

                                    </div>


                                    <div class="section-divider"></div>



                                    <!-- Message -->

                                    <div class="row">

                                        <div class="col-md-12 text-center">

                                            <asp:Label
                                                ID="lblMessage"
                                                runat="server"
                                                Visible="false"
                                                Font-Bold="true" />

                                            <asp:HiddenField ID="HFTableName" runat="server" />
                                            <asp:HiddenField ID="HFPermitNo" runat="server" />

                                        </div>

                                    </div>



                                    <!-- Mobile Validation -->

                                    <div id="DivMobileValidate" runat="server" visible="false">

                                        <div class="section-divider"></div>

                                        <div class="row form-group">

                                            <div class="col-md-6">

                                                <label class="form-label">Enter Mobile Number</label>

                                                <input type="text"
                                                    runat="server"
                                                    class="form-control number"
                                                    id="txtMobileNumber"
                                                    placeholder="Enter Mobile Number" />

                                            </div>

                                            <div class="col-md-6" style="padding-top: 25px;">

                                                <asp:Button
                                                    ID="btnOTP"
                                                    runat="server"
                                                    CssClass="btn btn-success"
                                                    Text="Send OTP"
                                                    ToolTip="Send OTP"
                                                    OnClick="btnOTP_Click"
                                                    OnClientClick="return fnMobileValidate()" />

                                            </div>

                                        </div>

                                    </div>



                                    <!-- OTP Verification -->

                                    <div id="DivOTPMatch" runat="server" visible="false">

                                        <div class="section-divider"></div>

                                        <div class="row form-group">

                                            <div class="col-md-6">

                                                <label class="form-label">Enter OTP</label>

                                                <input type="text"
                                                    runat="server"
                                                    class="form-control number"
                                                    id="txtOTP"
                                                    placeholder="Enter OTP" />

                                            </div>

                                            <div class="col-md-6" style="padding-top: 25px;">

                                                <asp:Button
                                                    ID="btnVerify"
                                                    runat="server"
                                                    CssClass="btn btn-primary"
                                                    Text="Verify"
                                                    ToolTip="Verify"
                                                    OnClick="btnVerify_Click"
                                                    OnClientClick="return fnOTPValidate()" />

                                            </div>

                                        </div>

                                    </div>



                                    <!-- Application Status Result -->

                                    <div id="DivList" runat="server" visible="false">

                                        <div class="section-divider"></div>

                                        <div class="row">

                                            <div class="col-md-12">

                                                <p id="tbdCityList">

                                                    <%=FillList %>
                                                </p>

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

    <script src="assets/global/scripts/datatable.js"></script>
    <script src="assets/global/plugins/datatables/datatables.js"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="assets/global/plugins/select2/js/select2.full.min.js"></script>
    <script src="assets/pages/scripts/components-select2.min.js"></script>
    <script src="assets/js/ViewApplicationStatus.js"></script>

</asp:Content>
