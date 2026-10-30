<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="SCREEN_STA_CS_EDIT.aspx.cs" Inherits="SCREEN_STA_CS_EDIT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/monthpicker.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form runat="server" autocomplete="off">
        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />



        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="STA-DASHBOARD.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>Counter Signature</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="portlet light portlet-fit">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="icon-settings font-red"></i>
                                                        <span class="caption-subject font-red sbold uppercase">Counter Signature Edit</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12" style="text-align: right">
                                                            <%--<asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="Print" OnClientClick="return printDiv('IssuedReceiptDiv')"  />--%>
                                                            <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv(IssuedReceiptDiv)">Print</button>

                                                        </div>

                                                    </div>
                                                    <div id="IssuedReceiptDiv">
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                            </div>
                                                            <div class="col-md-2">
                                                                <b>Permit Type</b>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:DropDownList ID="ddlPermitType" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="1">Temporary</asp:ListItem>
                                                                    <asp:ListItem Value="2">Permanent</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-4">
                                                            </div>



                                                        </div>
                                                        <br />
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Personal Information
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row" style="align-content: center">
                                                                    <asp:HiddenField ID="txtroutjson" runat="server" />
                                                                    <asp:HiddenField ID="txtNoRoundTrip" runat="server" />

                                                                    <div class="col-md-12 col-sm-12 col-xs-12" style="align-content: center">
                                                                        <table class="table table-striped table-bordered table-hover">

                                                                            <tr>
                                                                                <td><b>Name : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VOName" MaxLength="40" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                                                    <%-- <asp:Label ID="VOName" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>C/O :  <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VOCareOf" MaxLength="40" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VOCareOf" runat="server"></asp:Label>--%>

                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Present Address: <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox TextMode="MultiLine" Columns="5" ID="VOPresentAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VOPresentAddress" runat="server"></asp:Label>--%>
                                                                                </td>
                                                                                <td><b>Permanent Address : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox TextMode="MultiLine" Columns="5" ID="VOPermanentAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    <%-- <asp:Label ID="VOPermanentAddress" runat="server"></asp:Label>--%>

                                                                                </td>

                                                                            </tr>


                                                                        </table>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Vehicle Details
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <tr>


                                                                                <td><b>Vehicle Registration No : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VORegNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VORegNumber" runat="server"></asp:Label>--%>
                                                                                </td>
                                                                                <td>
                                                                                    <b>Permit No :</b>  <span style="color: red">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtPermitNo" MaxLength="20" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <b>Counter Signature No: <span style="color: red">*</span></b>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtCounterSignatureNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <b>Validity of Permit from : <span style="color: red">*</span></b> </td>
                                                                                <td colspan="3">

                                                                                    <div class="input-group input-large date-picker input-daterange" data-date-format="dd/mm/yyyy">
                                                                                        <input type="text" id="txtvalidityfrom" width="210px" readonly="true" class="regreadonly form-control" runat="server"></input>
                                                                                        <span class="input-group-addon">To </span>
                                                                                        <input type="text" id="txtvalidityto" width="210px" readonly="true" class="regreadonly form-control" runat="server"></input>
                                                                                    </div>
                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Maker Description : </b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VMakerDesc" MaxLength="60" CssClass="regtxtsomespl form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VMakerDesc" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>Maker Model No : </b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VMakerModel" MaxLength="60" CssClass="regtxtnumbersomespl form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VMakerModel" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Type of Vehicle : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VType" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VType" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>Chassis Number : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VChassisNumber" MaxLength="30" CssClass="regtxtnumbersomespl form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VChassisNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>

                                                                                <td><b>Engine Number  : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VEngineNumber" MaxLength="60" CssClass="regtxtnumbersomespl form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VEngineNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>Seating Capacity (including driver/conductor/attendant): <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VSeatCapacity" MaxLength="2" CssClass="number form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VSeatCapacity" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Sleeper Capacity: <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtsleeper" MaxLength="2" CssClass="number form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VSeatCapacity" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>U.L.W as per RC:<span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtULW" CssClass="number form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VULW" runat="server"></asp:Label>--%>

                                                                                </td>

                                                                                <td><b>R.L.W as per RC<span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRLW" CssClass="number form-control" runat="server"></asp:TextBox>
                                                                                    <%--<asp:Label ID="VULW" runat="server"></asp:Label>--%>

                                                                                </td>

                                                                            </tr>



                                                                            <tr>



                                                                                <td><b>Manufacturing Date: <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <%-- <div class="input-group input-medium date date-picker" data-date-format="dd-M-yyyy" data-date-start-date="-12y" data-date-end-date="-1d">
                                                                <input type="text" id="VManufacturingON"  class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button id="btn3" class="btn default" type="button" runat="server">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div>--%>
                                                                                    <div id='container'>
                                                                                        <input id="VManufacturingON" type="text" runat="server" class="form-control" />
                                                                                    </div>






                                                                                    <%--<asp:Label ID="VManufacturingON" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>Nature of goods to be carried, if any</b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtNatureOfGoods" MaxLength="60" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>

                                                                                <td><b>Insurance Certificate Valid Upto</b> <span style="color: red">*</span></td>
                                                                                <td>
                                                                                    <%--<div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">--%>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                        <input type="text" id="txtICValidUpto" class="form-control" runat="server" readonly="true" />
                                                                                        <span class="input-group-btn">
                                                                                            <button class="btn default" type="button">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </button>
                                                                                        </span>
                                                                                    </div>
                                                                                </td>


                                                                                <td><b>Pollution Certificate Valid Upto </b><span style="color: red">*</span></td>
                                                                                <td>
                                                                                    <%--<div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">--%>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                        <input type="text" id="txtPCValidUpto" class="form-control" runat="server" readonly="true" />
                                                                                        <span class="input-group-btn">
                                                                                            <button class="btn default" type="button">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </button>
                                                                                        </span>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>

                                                                                <td><b>Fitness Certificate Valid Upto</b> <span style="color: red">*</span></td>
                                                                                <td>
                                                                                    <%--<div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">--%>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                        <input type="text" id="txtFCValidUpto" class="form-control" runat="server" readonly="true" />
                                                                                        <span class="input-group-btn">
                                                                                            <button class="btn default" type="button">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </button>
                                                                                        </span>
                                                                                    </div>
                                                                                </td>


                                                                                <td><b>Tax Paid Upto</b> <span style="color: red">*</span></td>
                                                                                <td>

                                                                                    <%--<div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">--%>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                        <input type="text" id="txtTPValidUpto" class="form-control" runat="server" readonly="true" />
                                                                                        <span class="input-group-btn">
                                                                                            <button class="btn default" type="button">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </button>
                                                                                        </span>
                                                                                    </div>


                                                                                </td>
                                                                            </tr>



                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12">
                                                                <h4><b><u>Route Details</u></b></h4>
                                                            </div>

                                                            <asp:Label CssClass="col-md-12 col-sm-12 control-label" Font-Bold="true" ID="RouteHDRInfo" runat="server" />
                                                        </div>
                                                        <br />
                                                        Time Format: 24 Hours
                                                        <br />
                                                        <div id="RouteTimeInfo" runat="server">
                                                        </div>
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Other Details
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <tr>
                                                                                <td><b>Mobile No : <span style="color: red"></span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtMobileNo" CssClass="number form-control" runat="server" MaxLength="10"></asp:TextBox></td>
                                                                                <td><b>Email ID : </b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox></td>

                                                                            </tr>

                                                                            <tr>
                                                                                <td>
                                                                                    <b>Recommendation Letter No: <span style="color: red">*</span> </b>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRecommendationNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <b>Recommendation Date:<span style="color: red">*</span> </b>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                        <input type="text" id="txtRecommendationNoDated" class="form-control" runat="server" readonly="true" />
                                                                                        <span class="input-group-btn">
                                                                                            <button class="btn default" type="button">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </button>
                                                                                        </span>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>


                                                                        </table>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    TAX Detail
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"></a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <tr>
                                                                                <td><b>Tax Paid Amount: <span style="color: red"></span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txttaxpayAmount" CssClass="number form-control" runat="server" MaxLength="10"></asp:TextBox></td>
                                                                                <td><b>Tax Period : </b></td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddltaxperiod" runat="server" CssClass="form-control select2" AutoPostBack="false">

                                                                                        <asp:ListItem Value="q">Quarterly</asp:ListItem>
                                                                                        <asp:ListItem Value="h">Half Yearly</asp:ListItem>
                                                                                        <asp:ListItem Value="y">Yearly</asp:ListItem>
                                                                                    </asp:DropDownList>



                                                                                </td>


                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Fee Tax Paid From : </b></td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                        <input type="text" id="txtTAXpaidfrom" class="form-control" runat="server" readonly="true" />
                                                                                        <span class="input-group-btn">
                                                                                            <button class="btn default" type="button">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </button>
                                                                                        </span>
                                                                                    </div>

                                                                                </td>
                                                                                <td><b>Fee Tax Paid Upto : <span style="color: red"></span></b></td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                        <input type="text" id="txtTAXpaidupto" class="form-control" runat="server" readonly="true" />
                                                                                        <span class="input-group-btn">
                                                                                            <button class="btn default" type="button">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </button>
                                                                                        </span>
                                                                                    </div>

                                                                                </td>


                                                                            </tr>

                                                                            <tr>
                                                                                <td>
                                                                                    <b>Tax Reference No: <span style="color: red">*</span> </b>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txttaxRefNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <b>Tax Paid on:<span style="color: red">*</span> </b>
                                                                                </td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                        <input type="text" id="txttaxRefDated" class="form-control" runat="server" readonly="true" />
                                                                                        <span class="input-group-btn">
                                                                                            <button class="btn default" type="button">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </button>
                                                                                        </span>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>



                                                                        </table>

                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <br />
                                                            <br />
                                                            <div class="col-md-12" style="text-align: center">
                                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn  btn-success" Text="Update " OnClientClick="return SaveApplication();" OnClick="btnSubmit_Click" />
                                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Close" OnClientClick="Javascript: window.close(); return false;" />

                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
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
    <script src="assets/global/plugins/moment.min.js" type="text/javascript"></script>

    <script src="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>

    <script src="assets/js/monthpicker.min.js"></script>
    <script src="assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js" type="text/javascript"></script>
    <script src="assets/js/form-input-mask1.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            var getdate = new Date();
            var yearstart = parseInt(getdate.getFullYear()) - parseInt(12);
            var monthsStart = parseInt(getdate.getMonth()) + parseInt(1);
            var startdate = monthsStart + "/" + yearstart;
            var endyear = getdate.getFullYear();
            var monthEnd = parseInt(getdate.getMonth());
            var endDate = monthEnd + "/" + endyear;
            $('input[id*=VManufacturingON]').Monthpicker({ minYear: yearstart, maxYear: endyear, minValue: startdate, maxValue: endDate })


        });
        function fnApplicationValidate() {
            $('.form-control').css('border-color', '#c2cad8');
            var VOName = $('input[id*=VOName]').val().trim();
            if (VOName == "" || VOName == null) {
                alert("Please enter Name");
                $('input[id*=VOName]').css('border-color', 'red').focus();
                return false;
            }
            if (VOName.length < 3) {
                alert("Enter atleast 3 chars in the name textbox");
                $('input[id*=VOName]').css('border-color', 'red').focus();
                return false;
            }
            var VOCareOf = $('input[id*=VOCareOf]').val().trim();
            if (VOCareOf == "" || VOCareOf == null) {
                alert("Please enter C/O Name");
                $('input[id*=VOCareOf]').css('border-color', 'red').focus();
                return false;
            }
            if (VOCareOf.length < 3) {
                alert("Enter atleast 3 chars in the c/o name textbox");
                $('input[id*=VOCareOf]').css('border-color', 'red').focus();
                return false;
            }
            var VOPresentAddress = $('textarea[id*=VOPresentAddress]').val().trim();
            if (VOPresentAddress == "" || VOPresentAddress == null) {
                alert("Please enter present Address");
                $('textarea[id*=VOPresentAddress]').css('border-color', 'red').focus();
                return false;
            }
            var VOPermanentAddress = $('textarea[id*=VOPermanentAddress]').val().trim();
            if (VOPermanentAddress == "" || VOPermanentAddress == null) {
                alert("Please enter Permanent Address");
                $('textarea[id*=VOPermanentAddress]').css('border-color', 'red').focus();
                return false;
            }

            var VORegistration = $('input[id*=VORegNumber]').val().trim();
            if (VORegistration == "" || VORegistration == null) {
                alert("Please enter Registration Number");
                $('input[id*=VORegNumber]').css('border-color', 'red').focus();
                return false;
            }
            var VOPermit = $('input[id*=txtPermitNo]').val().trim();
            if (VOPermit == "" || VOPermit == null) {
                alert("Please enter Permit Number");
                $('input[id*=txtPermitNo]').css('border-color', 'red').focus();
                return false;
            }


            var validityfrom = $('input[id*=txtvalidityfrom]').val().trim();
            if (validityfrom == "" || validityfrom == null) {
                alert("Please enter Permit Validity From");
                $('input[id*=txtvalidityfrom]').css('border-color', 'red').focus();
                return false;
            }
            var validityto = $('input[id*=txtvalidityto]').val().trim();
            if (validityto == "" || validityto == null) {
                alert("Please enter Permit Validity Upto");
                $('input[id*=txtvalidityto]').css('border-color', 'red').focus();
                return false;
            }
            var VMakerDesc = $('input[id*=VMakerDesc]').val().trim();
            if (VMakerDesc == "" || VMakerDesc == null) {
                alert("Please enter Maker Description");
                $('input[id*=VMakerDesc]').css('border-color', 'red').focus();
                return false;
            }


            var VMakerModel = $('input[id*=VMakerModel]').val().trim();
            if (VMakerModel == "" || VMakerModel == null) {
                alert("Please enter Vehicle Model");
                $('input[id*=VMakerModel]').css('border-color', 'red').focus();
                return false;
            }


            var VType = $('input[id*=VType]').val().trim();
            if (VType == "" || VType == null) {
                alert("Please enter Vehicle class description");
                $('input[id*=VType]').css('border-color', 'red').focus();
                return false;
            }
            //if (VType.toUpperCase().indexOf("BUS") === -1) {
            //    alert("Vehicle class Should be BUS");
            //    $('input[id*=VType]').css('border-color', 'red').focus();
            //    return false;
            //}
            var VChassisNumber = $('input[id*=VChassisNumber]').val().trim();
            if (VChassisNumber == "" || VChassisNumber == null) {
                alert("Please enter Vehicle chassis number");
                $('input[id*=VChassisNumber]').css('border-color', 'red').focus();
                return false;
            }
            var VEngineNumber = $('input[id*=VEngineNumber]').val().trim();
            if (VEngineNumber == "" || VEngineNumber == null) {
                alert("Please enter Vehicle Engine Number");
                $('input[id*=VEngineNumber]').css('border-color', 'red').focus();
                return false;
            }

            var VSeatCapacity = $('input[id*=VSeatCapacity]').val().trim();
            if (VSeatCapacity == "" || VSeatCapacity == null) {
                alert("Please enter seat capacity");
                $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
                return false;
            }
            if (parseInt(VSeatCapacity) < parseInt(7)) {
                alert("Minimum 7 Seat and Maximum 74 seat capacity required");
                $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
                return false;
            }
            if (parseInt(VSeatCapacity) > parseInt(74)) {

                alert("Minimum 7 Seat and Maximum 74 seat capacity required");
                $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
                return false;
            }
            var ulw = $('input[id*=txtULW]').val().trim();
            if (ulw == "" || ulw == null) {
                alert("Please enter U.L.W value as per RC");
                $('input[id*=txtULW]').css('border-color', 'red').focus();
                return false;
            }
            if (ulw <= parseInt(0)) {
                alert("Please enter valid data  in the U.L.W value as per RC");
                $('input[id*=txtULW]').css('border-color', 'red').focus();

                return false;
            }
            var rlw = $('input[id*=txtRLW]').val().trim();
            if (rlw == "" || rlw == null) {
                alert("Please enter R.L.W value as per RC");
                $('input[id*=txtRLW]').css('border-color', 'red').focus();
                return false;
            }
            if (rlw <= parseInt(0)) {
                alert("Please enter valid data  in the R.L.W value as per RC");
                $('input[id*=txtRLW]').css('border-color', 'red').focus();

                return false;
            }



            var VManufacturingON = $('input[id*=VManufacturingON]').val().trim();
            if (VManufacturingON == "" || VManufacturingON == null) {
                alert("Please enter Manufacturing date");
                $('input[id*=VManufacturingON]').css('border-color', 'red').focus();
                return false;
            }
            if ($('input[id*=txtICValidUpto]').val() == null || $('input[id*=txtICValidUpto]').val() == "") {
                alert("Please select Insurance Certificate Validity Date");
                $('input[id*=txtICValidUpto]').css('border-color', 'red').focus();
                return false;
            }


            if ($('input[id*=txtPCValidUpto]').val() == null || $('input[id*=txtPCValidUpto]').val() == "") {
                alert("Please select Pollution Certificate Validity Date");
                $('input[id*=txtPCValidUpto]').css('border-color', 'red').focus();
                return false;
            }



            if ($('input[id*=txtFCValidUpto]').val() == null || $('input[id*=txtFCValidUpto]').val() == "") {
                alert("Please select Fitness Certificate Validity Date");
                $('input[id*=txtFCValidUpto]').css('border-color', 'red').focus();
                return false;
            }
            if ($('input[id*=txtTPValidUpto]').val() == null || $('input[id*=txtTPValidUpto]').val() == "") {
                alert("Please select Tax Paid Up to Date");
                $('input[id*=txtTPValidUpto]').css('border-color', 'red').focus();
                return false;
            }
            if ($('div[id*=RouteTimeInfo]').html() == null || $('div[id*=RouteTimeInfo]').html() == "") {
                alert("Route time table is not displaying. So you can not apply for counter signature right now");
                return false;
            }

            var MobileNo = $('input[id*=txtMobileNo]').val().trim();
            //if (MobileNo == "" || MobileNo == null) {
            //    alert("Please enter user mobile no");
            //    $('input[id*=txtMobileNo]').css('border-color', 'red').focus();
            //    return false;
            //}
            if (MobileNo != "") {
                if (!validateMob(MobileNo)) {
                    alert("Invalid Mobile Number");
                    $('input[id*=txtMobileNo]').css('border-color', 'red').focus();
                    return false;
                }
            }
            var EmailID = $('input[id*=txtEmailId]').val().trim();
            if (EmailID != "") {

                if (!IsEmail(EmailID)) {
                    alert("Invalid Email ID");
                    $('input[id*=txtEmailId]').css('border-color', 'red').focus();
                    return false;
                }
            }
            var txttaxpayAmount = $('input[id*=txttaxpayAmount]').val().trim();
            if (txttaxpayAmount == "" || txttaxpayAmount == null) {
                alert("Please enter Tax Pay Amount");
                $('input[id*=txttaxpayAmount]').css('border-color', 'red').focus();
                return false;
            }
            var txtTAXpaidfrom = $('input[id*=txtTAXpaidfrom]').val().trim();
            if (txtTAXpaidfrom == "" || txtTAXpaidfrom == null) {
                alert("Please select Fee Tax Paid from");
                $('input[id*=txtTAXpaidfrom]').css('border-color', 'red').focus();
                return false;
            }
            var txtTAXpaidupto = $('input[id*=txtTAXpaidupto]').val().trim();
            if (txtTAXpaidupto == "" || txtTAXpaidupto == null) {
                alert("Please select Fee Tax Paid Upto");
                $('input[id*=txtTAXpaidupto]').css('border-color', 'red').focus();
                return false;
            }

            var taxRefNo = $('input[id*=txttaxRefNo]').val();
            if (taxRefNo == "" || taxRefNo == null) {
                alert("Please enter Tax Ref. No");
                $('input[id*=txttaxRefNo]').css('border-color', 'red').focus();
                return false;
            }

            var taxRefNoDated = $('input[id*=txttaxRefDated]').val();
            if (taxRefNoDated == "" || taxRefNoDated == null) {
                alert("Please enter Tax Ref. Paid on Date");
                $('input[id*=txttaxRefDated]').css('border-color', 'red').focus();
                return false;
            }






            return true;
        }
        function timeFunction(id) {
            var objArray = $('#' + id).val().split(':');
            if (parseInt(objArray[0]) >= parseInt('0') && parseInt(objArray[0]) <= parseInt('23')) {
                if (parseInt(objArray[1]) >= parseInt('0') && parseInt(objArray[1]) <= parseInt('59')) {

                }
                else {
                    $('#' + id).val('');
                }
            }
            else {
                $('#' + id).val('');
            }


        }



        function SaveApplication() {

            if (fnApplicationValidate()) {
                var PERMIT_APPLICATION_LDRs = new Array();
                var condition = true;
                var NoRoundTrip = $('input[id*=txtNoRoundTrip]').val();
                // alert("No of TRIPS"+NoRoundTrip);
                for (j = 1; j <= NoRoundTrip; j++) {

                    var UPCity = $('#lblUPRoute' + j + '_' + j).text();
                    var DownCity = $('#lblDownRoute' + j + '_' + j).text();

                    var DownDepTime = '';

                    var UPArrTime = '';
                    var UPDepTime = '';


                    if (j == NoRoundTrip) {
                        UPDepTime = '----';
                        DownDepTime = '----';
                    } else {
                        UPDepTime = $('#txtUPDepRoute' + j + '_' + j).val().trim();
                        DownDepTime = $('#txtDownDepRoute' + j + '_' + j).val().trim();
                    }
                    if (j == 1) {
                        UPArrTime = '----';
                        DownArrTime = '----';
                    } else {
                        UPArrTime = $('#txtUPArrRoute' + j + '_' + j).val().trim();
                        DownArrTime = $('#txtDownArrRoute' + j + '_' + j).val().trim();
                    }
                    PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime, DEPARTURE_TIMING: UPDepTime.toString(), MODE: "UP", ROUTE_DETAIL_ID: j, ID: j });
                    PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime, DEPARTURE_TIMING: DownDepTime.toString(), MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: j });




                }





                var xxx = JSON.stringify({ ROUTE_DETAIL_ID: 0, PERMIT_APPLICATION_LDRs: PERMIT_APPLICATION_LDRs });

                $('input[id*=txtroutjson]').val(xxx);
                $('#loadingmessage').modal('show');
                return true;



            }
            else {

                return false;
            }
        }
    </script>
    <script type="text/javascript">


        function printDiv(divId) {

            $('input[id*=btnSubmit]').hide();
            $('input[id*=btnCancel]').hide();


            //document.getElementById(divId).style.backgroundImage = "url('assets/img/Untitled.png')";
            //document.body.style.backgroundImage = "url('assets/img/Untitled.png')";

            var printContents = $('#IssuedReceiptDiv').html(); //document.getElementById(divId).innerHTML;
            var originalContents = document.body.innerHTML;
            var height = $(document).height();
            height = height + 100;
            var width = $(document).width();
            width = width + 50;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;


            $('input[id*=btnSubmit]').show();
            $('input[id*=btnCancel]').show();





        }
    </script>





</asp:Content>

