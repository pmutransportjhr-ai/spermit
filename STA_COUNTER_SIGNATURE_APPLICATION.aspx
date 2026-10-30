<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA_COUNTER_SIGNATURE_APPLICATION.aspx.cs" Inherits="STA_COUNTER_SIGNATURE_APPLICATION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/monthpicker.css" rel="stylesheet" />
    <style>
        .action-buttons .btn {
            min-width: 140px;
            margin: 5px;
            border-radius: 4px;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="FormID1" enctype="multipart/form-data" runat="server" class="form-horizontal" autocomplete="off" method="post">


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <asp:HiddenField ID="hdnID" runat="server" />
        <input type="text" id="hdnType" runat="server" style="display: none" />
        <input type="text" id="hdnTypeOptionID" runat="server" style="display: none" />
        <input type="text" id="txtroutjson" runat="server" style="display: none" />
        <asp:HiddenField ID="txtVehicleJson" runat="server" />

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
                                <span id="PageHeading" runat="server"></span>

                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light">
                                        <div class="portlet-title">
                                            <div class="caption caption-md">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase" id="PageHeading1" runat="server"></span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">

                                            <br />
                                            <br />

                                            <asp:UpdatePanel ID="UPpanel" runat="server">
                                                <ContentTemplate>

                                                    <!-- ================= MOBILE VERIFICATION ================= -->

                                                    <div class="row">

                                                        <div class="col-md-12">

                                                            <div class="portlet box green">

                                                                <div class="portlet-title">

                                                                    <div class="caption">
                                                                        <i class="fa fa-mobile"></i>
                                                                        Mobile Verification
                                                                    </div>

                                                                    <div class="tools">
                                                                        <a href="javascript:;" class="collapse"></a>
                                                                    </div>

                                                                </div>

                                                                <div class="portlet-body">

                                                                    <div class="row">

                                                                        <!-- MOBILE -->
                                                                        <div class="col-md-4">

                                                                            <label>
                                                                                <b>Mobile Number :</b>
                                                                            </label>

                                                                            <asp:TextBox ID="txtMobileOtp"
                                                                                runat="server"
                                                                                CssClass="form-control number"
                                                                                MaxLength="10"
                                                                                placeholder="Enter Mobile Number">
                                                                            </asp:TextBox>

                                                                        </div>

                                                                        <!-- SEND OTP -->
                                                                        <div class="col-md-2" style="margin-top: 25px;">

                                                                            <asp:Button ID="btnSendOtp"
                                                                                runat="server"
                                                                                CssClass="btn blue btn-block"
                                                                                Text="Send OTP"
                                                                                OnClick="btnSendOtp_Click"
                                                                                CausesValidation="false" />

                                                                        </div>

                                                                        <!-- OTP -->
                                                                        <div class="col-md-4">

                                                                            <label>
                                                                                <b>Enter OTP :</b>
                                                                            </label>

                                                                            <asp:TextBox ID="txtOtp"
                                                                                runat="server"
                                                                                CssClass="form-control"
                                                                                MaxLength="6"
                                                                                placeholder="Enter OTP">
                                                                            </asp:TextBox>

                                                                        </div>

                                                                        <!-- VERIFY -->
                                                                        <div class="col-md-2" style="margin-top: 25px;">

                                                                            <asp:Button ID="btnVerifyOtp"
                                                                                runat="server"
                                                                                CssClass="btn green btn-block"
                                                                                Text="Verify OTP"
                                                                                OnClick="btnVerifyOtp_Click"
                                                                                CausesValidation="false" />

                                                                        </div>

                                                                    </div>

                                                                    <hr />

                                                                    <div class="row">

                                                                        <div class="col-md-12 text-center">

                                                                            <asp:Label ID="lblOtpStatus"
                                                                                runat="server"
                                                                                Font-Bold="true"
                                                                                Font-Size="Medium">
                                                                            </asp:Label>

                                                                        </div>

                                                                    </div>

                                                                    <asp:HiddenField ID="hdnOtpVerified"
                                                                        runat="server"
                                                                        Value="false" />

                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>

                                                    <asp:Panel ID="pnlMainForm"
                                                        runat="server"
                                                        Visible="false">

                                                        <div class="row">
                                                            <div class="col-md-12">

                                                                <div class="row">



                                                                    <div class="col-md-12 col-sm-12">
                                                                        <asp:Label ID="lblMSg" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <br />

                                                                <div class="row">


                                                                    <div class="col-md-12 col-sm-12">
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
                                                                                                    <asp:TextBox ID="VOCareOf" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                                                                    <%--<asp:Label ID="VOCareOf" runat="server"></asp:Label>--%>

                                                                                                </td>

                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td><b>Present Address: <span style="color: red">*</span></b></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="VOPresentAddress" TextMode="MultiLine" Columns="5" CssClass="address form-control" runat="server"></asp:TextBox>
                                                                                                    <%--<asp:Label ID="VOPresentAddress" runat="server"></asp:Label>--%>
                                                                                                </td>
                                                                                                <td><b>Permanent Address : <span style="color: red">*</span></b></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="VOPermanentAddress" TextMode="MultiLine" Columns="5" CssClass="address form-control" runat="server"></asp:TextBox>
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
                                                                                                <td><b>Type of Vehicle: </b></td>
                                                                                                <td>
                                                                                                    <asp:DropDownList ID="ddlCategory"
                                                                                                        runat="server"
                                                                                                        CssClass="form-control select2">

                                                                                                        <asp:ListItem Value="0">--select category--</asp:ListItem>
                                                                                                        <asp:ListItem Value="Ordinary">Ordinary</asp:ListItem>
                                                                                                        <asp:ListItem Value="Semi Deluxe">Semi Deluxe</asp:ListItem>
                                                                                                        <asp:ListItem Value="Deluxe">Deluxe</asp:ListItem>
                                                                                                        <asp:ListItem Value="Volvo">Volvo</asp:ListItem>

                                                                                                    </asp:DropDownList>
                                                                                                </td>



                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td><b>Sleeper Capacity: <span style="color: red">*</span></b></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtSleeperCapacity" runat="server" MaxLength="2" CssClass="number form-control"></asp:TextBox>

                                                                                                </td>

                                                                                                <td><b>Seating Capacity (including driver/conductor/attendant): <span style="color: red">*</span></b></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="VSeatCapacity" MaxLength="3" CssClass="number form-control" runat="server"></asp:TextBox>
                                                                                                    <%--<asp:Label ID="VSeatCapacity" runat="server"></asp:Label>--%>

                                                                                                </td>

                                                                                            </tr>

                                                                                            <tr>

                                                                                                <td><b>Engine Number  : <span style="color: red">*</span></b></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="VEngineNumber" MaxLength="60" CssClass="regtxtnumbersomespl form-control" runat="server"></asp:TextBox>
                                                                                                    <%--<asp:Label ID="VEngineNumber" runat="server"></asp:Label>--%>

                                                                                                </td>

                                                                                                <td><b>Chassis Number : <span style="color: red">*</span></b></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="VChassisNumber" MaxLength="30" CssClass="regtxtnumbersomespl form-control" runat="server"></asp:TextBox>
                                                                                                    <%--<asp:Label ID="VChassisNumber" runat="server"></asp:Label>--%>

                                                                                                </td>

                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td><b>U.L.W as per RC<span style="color: red">*</span></b></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtULW" MaxLength="5" CssClass="number form-control" runat="server"></asp:TextBox>
                                                                                                    <%--<asp:Label ID="VULW" runat="server"></asp:Label>--%>

                                                                                                </td>

                                                                                                <td><b>R.L.W as per RC<span style="color: red">*</span></b></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtRLW" MaxLength="5" CssClass="number form-control" runat="server"></asp:TextBox>
                                                                                                    <%--<asp:Label ID="VULW" runat="server"></asp:Label>--%>

                                                                                                </td>

                                                                                            </tr>



                                                                                            <tr>



                                                                                                <td><b>Manufacturing Date: <span style="color: red">*</span></b></td>
                                                                                                <td>

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


                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="portlet box green">
                                                                            <div class="portlet-title">
                                                                                <div class="caption">
                                                                                    Route Details <span style="color: red">*</span>
                                                                                </div>
                                                                                <div class="tools">
                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                </div>
                                                                            </div>

                                                                            <div class="portlet-body">
                                                                                <br />
                                                                                <div class="row">
                                                                                    <div class="col-md-3"><b>Issued by : <span style="color: red">*</span></b></div>
                                                                                    <div class="col-md-3">
                                                                                        <asp:DropDownList ID="ddlStateIssuedBy" AutoPostBack="true" OnSelectedIndexChanged="ddlStateIssuedBy_SelectedIndexChanged" CssClass="form-control select2" runat="server" AppendDataBoundItems="true"></asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-md-6"></div>
                                                                                </div>
                                                                                <br />

                                                                                <div id="RouteHDRInfo" runat="server" style="height: 150px !important; overflow-y: auto !important; overflow-x: auto !important; padding: 10px 0;">
                                                                                </div>
                                                                                <br />

                                                                                Time Format: 24 Hours
                   
                                                                                <div id="RouteTimeInfo" runat="server"></div>

                                                                            </div>
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
                                                                                                <td><b>Mobile No : <span style="color: red">*</span></b></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtMobileNo" CssClass="number form-control" runat="server"></asp:TextBox></td>
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
                                                                                    Application Fee Details
                                                                                </div>
                                                                                <div class="tools">
                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                </div>
                                                                            </div>
                                                                            <div class="portlet-body">
                                                                                <div class="row">
                                                                                    <div class="col-md-3 col-sm-3"><b>Application Fees :</b></div>
                                                                                    <div class="col-md-3 col-sm-3">
                                                                                        <asp:Label ID="lblApplicationFees" Text="0" runat="server"></asp:Label>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-3"></div>
                                                                                    <div class="col-md-3 col-sm-3"></div>
                                                                                </div>


                                                                            </div>
                                                                        </div>
                                                                        <div class="portlet box green">
                                                                            <div class="portlet-title">
                                                                                <div class="caption">
                                                                                    Upload Documents
                                                                                </div>
                                                                                <div class="tools">
                                                                                    <a href="javascript:;" class="collapse"></a>
                                                                                </div>
                                                                            </div>

                                                                            <div class="portlet-body">
                                                                                <br />

                                                                                <div id="uploadDocuments">
                                                                                    <div class="row">
                                                                                        <div class="col-md-12 col-sm-12 col-xs-12">
                                                                                            <table class="table table-striped table-bordered table-hover">
                                                                                                <tr>
                                                                                                    <td>Registration Certificate (RC)  <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <asp:FileUpload runat="server" ID="uploadRC" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                                                    <td>Speed Governor Certificate <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <asp:FileUpload runat="server" ID="uploadSpeed" accept="application/pdf, .pdf"></asp:FileUpload></td>

                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <td>Insurance Certificate <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <asp:FileUpload runat="server" ID="uploadInsur" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                                                    <td>Insurance Certificate Valid Upto <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">
                                                                                                            <input type="text" id="txtICValidUpto" class="form-control" readonly="true" runat="server" />
                                                                                                            <span class="input-group-btn">
                                                                                                                <button class="btn default" type="button">
                                                                                                                    <i class="fa fa-calendar"></i>
                                                                                                                </button>
                                                                                                            </span>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <td>Pollution Certificate <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <asp:FileUpload runat="server" ID="uploadPollution" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                                                    <td>Pollution Certificate Valid Upto <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">
                                                                                                            <input type="text" id="txtPCValidUpto" class="form-control" readonly="true" runat="server" />
                                                                                                            <span class="input-group-btn">
                                                                                                                <button class="btn default" type="button">
                                                                                                                    <i class="fa fa-calendar"></i>
                                                                                                                </button>
                                                                                                            </span>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <td>Fitness Certificate <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <asp:FileUpload runat="server" ID="uploadFitness" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                                                    <td>Fitness Certificate Valid Upto <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">
                                                                                                            <input type="text" id="txtFCValidUpto" class="form-control" readonly="true" runat="server" />
                                                                                                            <span class="input-group-btn">
                                                                                                                <button class="btn default" type="button">
                                                                                                                    <i class="fa fa-calendar"></i>
                                                                                                                </button>
                                                                                                            </span>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>Tax paid upto Certificate <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <asp:FileUpload runat="server" ID="uploadTax" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                                                    <td>Tax paid upto <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">
                                                                                                            <input type="text" id="txtTPValidUpto" class="form-control" readonly="true" runat="server" />
                                                                                                            <span class="input-group-btn">
                                                                                                                <button class="btn default" type="button">
                                                                                                                    <i class="fa fa-calendar"></i>
                                                                                                                </button>
                                                                                                            </span>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <td>Permit Certificate <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <asp:FileUpload runat="server" ID="uploadpermit" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                                                    <td>TimeTable Copy <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <asp:FileUpload runat="server" ID="uploadtimetable" accept="application/pdf, .pdf"></asp:FileUpload></td>

                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <td>Recommendation Letter <span style="color: red">*</span></td>
                                                                                                    <td>
                                                                                                        <asp:FileUpload runat="server" ID="recommendationUpload" accept="application/pdf, .pdf"></asp:FileUpload></td>

                                                                                                </tr>


                                                                                            </table>
                                                                                            <br />
                                                                                        </div>
                                                                                    </div>



                                                                                </div>

                                                                            </div>






                                                                        </div>

                                                                    </div>
                                                                    <br />
                                                                    <!-- STATUS -->
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <asp:Label ID="Label1" runat="server"
                                                                                Font-Bold="true">
                                                                            </asp:Label>
                                                                        </div>
                                                                    </div>

                                                                    <!-- IMPORTANT -->
                                                                    <asp:HiddenField ID="HiddenField1" runat="server"
                                                                        Value="false" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                        </br>
                                                         <div class="row">
                                                             <div class="col-md-12 col-sm-12">

                                                                 <label for="modalAgree">
                                                                     <a style="color: red" href="javascript:fnTermsCondition('csapp');">
                                                                         <b>Terms & Conditions</b></a> (By procedding you are agree with the Terms and condition)</label>
                                                             </div>
                                                         </div>

                                                        <br />


                                                        <div class="row" style="margin-top: 30px; margin-bottom: 25px;">
                                                            <div class="col-md-12 text-center">

                                                                <asp:Button ID="btnSave"
                                                                    runat="server"
                                                                    CssClass="btn btn-success btn-lg"
                                                                    Text="Proceed"
                                                                    OnClick="btnSave_Click"
                                                                    OnClientClick="return SaveApplication()" />

                                                                &nbsp;&nbsp;

                                                                <asp:Button ID="btnReset"
                                                                    runat="server"
                                                                    CssClass="btn btn-default btn-lg"
                                                                    Text="Reset"
                                                                    OnClick="btnReset_Click"
                                                                    OnClientClick="return fnClearPermitNew()" />

                                                            </div>
                                                        </div>

                                                    </asp:Panel>


                                                </ContentTemplate>
                                                <Triggers>

                                                    <asp:AsyncPostBackTrigger
                                                        ControlID="ddlStateIssuedBy"
                                                        EventName="SelectedIndexChanged" />

                                                    <asp:PostBackTrigger
                                                        ControlID="btnSave" />

                                                </Triggers>

                                            </asp:UpdatePanel>

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
    <script src="assets/global/plugins/moment.min.js" type="text/javascript"></script>

    <script src="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>
    <script src="assets/js/STA_COUNTER_SIGNATURE_APPLICATION.js" type="text/javascript"></script>
    <script src="assets/js/monthpicker.min.js"></script>
    <script src="assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js" type="text/javascript"></script>
    <script src="assets/js/form-input-mask1.js" type="text/javascript"></script>

  
    <script id="p2e7qa">
        document.addEventListener("DOMContentLoaded", function () {
            var mobile = document.getElementById('<%= txtMobileNo.ClientID %>');
            var otpFlag = document.getElementById('<%= hdnOtpVerified.ClientID %>');

            mobile.addEventListener("input", function () {
                otpFlag.value = "false";
            });
        });
    </script>

    <script>
        Sys.Application.add_load(function () {

            var mainMobile = document.getElementById('<%= txtMobileNo.ClientID %>');
            var otpMobile = document.getElementById('<%= txtMobileOtp.ClientID %>');
            var otpFlag = document.getElementById('<%= hdnOtpVerified.ClientID %>');

            if (mainMobile != null) {

                mainMobile.oninput = function () {

                    var value = mainMobile.value.replace(/\D/g, '');

                    if (value.length === 10) {
                        otpMobile.value = value;   // ✅ autopopulate instantly
                    } else {
                        otpMobile.value = "";
                        otpFlag.value = "false";
                    }
                };
            }
        });
    </script>


</asp:Content>

