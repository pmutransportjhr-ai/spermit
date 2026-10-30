<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true"
    CodeFile="STA-PERMIT-NEW.aspx.cs" Inherits="STA_PERMIT_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css"
        rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/monthpicker.css" rel="stylesheet" />



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="FormID1" enctype="multipart/form-data" runat="server" class="form-horizontal" autocomplete="off"
        method="post">


        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
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
                                                <span class="caption-subject font-red sbold uppercase"
                                                    id="PageHeading1" runat="server"></span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">

                                            <asp:UpdatePanel ID="UPpanel" runat="server">
                                                <contenttemplate>


                                                    <div class="row">

                                                        <div class="col-md-1 col-sm-1">
                                                            <asp:HiddenField ID="hdnID" runat="server" />
                                                            <asp:HiddenField ID="hdnType" runat="server" />
                                                            <asp:HiddenField ID="hdnTypeOptionID" runat="server" />
                                                            <asp:HiddenField ID="txtroutjson" runat="server" />
                                                            <asp:HiddenField ID="txtVehicleJson" runat="server" />
                                                            <asp:HiddenField ID="MFSERVER" runat="server" />
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control">
                                                                    <b>Enter Vehicle
                                                                            Registration Number: <span
                                                                                style="color: red">*</span></b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4">
                                                            <div class="form-group">
                                                                <asp:TextBox ID="txtVehicleNumber" MaxLength="12"
                                                                    CssClass="regtxtnumber form-control"
                                                                    runat="server">
                                                                </asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-1 col-sm-1"></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">

                                                                <asp:Button ToolTip="Search" ID="btnSearch"
                                                                    runat="server" CssClass="btn green"
                                                                    Text="Search"
                                                                    OnClientClick="return fnSearchValidate()"
                                                                    OnClick="btnSearch_Click" />
                                                                &nbsp;
                                                                    <asp:Button ToolTip="Clear" ID="btnClear"
                                                                        runat="server" CssClass="btn default"
                                                                        Text="Clear"
                                                                        OnClientClick="return fnClearPermitNew()"
                                                                        OnClick="btnClear_Click" />
                                                                &nbsp;
                                                                    <asp:Button ToolTip="Cancel" ID="btnCancel"
                                                                        runat="server" CssClass="btn red" Text="Cancel"
                                                                        OnClick="btnCancel_Click" />
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <div class="col-md-12 col-sm-12">
                                                            <asp:Label ID="lblMSg" Visible="false" Font-Bold="true"
                                                                runat="server"></asp:Label>
                                                        </div>
                                                        <br />
                                                        <br />
                                                    </div>
                                                    <br />


                                                    <asp:Panel CssClass="row" ID="Panel1" runat="server"
                                                        Visible="false">
                                                        <div class="portlet-body">
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
                                                                        <div class="row"
                                                                            style="align-content: center">
                                                                            <div class="col-md-12 col-sm-12 col-xs-12"
                                                                                style="align-content: center">
                                                                                <table
                                                                                    class="table table-striped table-bordered table-hover">

                                                                                    <tr>
                                                                                        <td><b>Name : <span
                                                                                            style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="VOName"
                                                                                                MaxLength="40"
                                                                                                CssClass="regtxt form-control"
                                                                                                runat="server">
                                                                                            </asp:TextBox>
                                                                                            <%-- <asp:Label
                                                                                                    ID="VOName"
                                                                                                    runat="server">
                                                                                                    </asp:Label>--%>

                                                                                        </td>
                                                                                        <td><b>C/O : <span
                                                                                            style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox
                                                                                                ID="VOCareOf"
                                                                                                MaxLength="40"
                                                                                                CssClass="regtxt form-control"
                                                                                                runat="server">
                                                                                            </asp:TextBox>
                                                                                            <%--<asp:Label
                                                                                                    ID="VOCareOf"
                                                                                                    runat="server">
                                                                                                    </asp:Label>--%>

                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td><b>Present Address:
                                                                                                    <span
                                                                                                        style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox
                                                                                                TextMode="MultiLine"
                                                                                                Columns="5"
                                                                                                ID="VOPresentAddress"
                                                                                                CssClass="address form-control"
                                                                                                runat="server">
                                                                                            </asp:TextBox>
                                                                                            <%--<asp:Label
                                                                                                    ID="VOPresentAddress"
                                                                                                    runat="server">
                                                                                                    </asp:Label>--%>
                                                                                        </td>
                                                                                        <td><b>Permanent Address :
                                                                                                    <span
                                                                                                        style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox
                                                                                                TextMode="MultiLine"
                                                                                                Columns="5"
                                                                                                ID="VOPermanentAddress"
                                                                                                CssClass="address form-control"
                                                                                                runat="server">
                                                                                            </asp:TextBox>
                                                                                            <%-- <asp:Label
                                                                                                    ID="VOPermanentAddress"
                                                                                                    runat="server">
                                                                                                    </asp:Label>--%>

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
                                                                            <div
                                                                                class="col-md-12 col-sm-12 col-xs-12">
                                                                                <table
                                                                                    class="table table-striped table-bordered table-hover">
                                                                                    <tr>


                                                                                        <td><b>Registration No :
                                                                                                    <span
                                                                                                        style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox
                                                                                                ID="VORegNumber"
                                                                                                CssClass="form-control"
                                                                                                runat="server"
                                                                                                Enabled="false">
                                                                                            </asp:TextBox>
                                                                                            <%--<asp:Label
                                                                                                    ID="VORegNumber"
                                                                                                    runat="server">
                                                                                                    </asp:Label>--%>

                                                                                        </td>
                                                                                        <td><b>Vehicle Class
                                                                                                    Description: <span
                                                                                                        style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="VType"
                                                                                                CssClass="regtxt form-control"
                                                                                                runat="server"
                                                                                                ReadOnly="true">
                                                                                            </asp:TextBox>
                                                                                            <%--<asp:Label
                                                                                                    ID="VType"
                                                                                                    runat="server">
                                                                                                    </asp:Label>--%>

                                                                                        </td>

                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td><b>Chassis Number :
                                                                                                    <span
                                                                                                        style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox
                                                                                                ID="VChassisNumber"
                                                                                                MaxLength="30"
                                                                                                CssClass="regtxtnumbersomespl form-control"
                                                                                                runat="server"
                                                                                                ReadOnly="true">
                                                                                            </asp:TextBox>


                                                                                        </td>
                                                                                        <td><b>Engine Number :
                                                                                                    <span
                                                                                                        style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox
                                                                                                ID="VEngineNumber"
                                                                                                MaxLength="60"
                                                                                                CssClass="regtxtnumbersomespl form-control"
                                                                                                runat="server"
                                                                                                ReadOnly="true">
                                                                                            </asp:TextBox>


                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td><b>Maker Description :
                                                                                        </b></td>
                                                                                        <td>
                                                                                            <asp:TextBox
                                                                                                ID="VMakerDesc"
                                                                                                MaxLength="60"
                                                                                                CssClass="regtxtsomespl form-control"
                                                                                                runat="server"
                                                                                                ReadOnly="true">
                                                                                            </asp:TextBox>


                                                                                        </td>
                                                                                        <td><b>Maker Model No :
                                                                                        </b></td>
                                                                                        <td>
                                                                                            <asp:TextBox
                                                                                                ID="VMakerModel"
                                                                                                MaxLength="60"
                                                                                                CssClass="regtxtnumbersomespl form-control"
                                                                                                runat="server"
                                                                                                ReadOnly="true">
                                                                                            </asp:TextBox>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td><b>Fuel Description :
                                                                                        </b></td>
                                                                                        <td>
                                                                                            <asp:TextBox
                                                                                                ID="VFuelDesc"
                                                                                                MaxLength="6"
                                                                                                CssClass="regtxt form-control"
                                                                                                runat="server"
                                                                                                ReadOnly="true">
                                                                                            </asp:TextBox>

                                                                                        </td>
                                                                                        <td><b>Color : </b></td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="VColor"
                                                                                                MaxLength="30"
                                                                                                CssClass="regtxt form-control"
                                                                                                runat="server"
                                                                                                ReadOnly="true">
                                                                                            </asp:TextBox>


                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td><b>Fitness Valid Upto:
                                                                                        </b><span
                                                                                            style="color: red">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="input-group input-medium date date-picker"
                                                                                                data-date-format="dd/mm/yyyy"
                                                                                                data-date-start-date="+0d">
                                                                                                <input type="text"
                                                                                                    id="VFitness"
                                                                                                    class="form-control"
                                                                                                    disabled="disabled"
                                                                                                    readonly="true"
                                                                                                    runat="server" />
                                                                                                <span
                                                                                                    class="input-group-btn">
                                                                                                    <button
                                                                                                        id="btn1"
                                                                                                        class="btn default"
                                                                                                        type="button"
                                                                                                        disabled="disabled"
                                                                                                        runat="server">
                                                                                                        <i
                                                                                                            class="fa fa-calendar"></i>
                                                                                                    </button>
                                                                                                </span>
                                                                                            </div>

                                                                                        </td>
                                                                                        <td><b>Tax Paid UpTo: </b>
                                                                                            <span
                                                                                                style="color: red">*</span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="input-group input-medium date date-picker"
                                                                                                data-date-format="dd/mm/yyyy"
                                                                                                data-date-start-date="+0d">
                                                                                                <input type="text"
                                                                                                    id="VTax"
                                                                                                    disabled="disabled"
                                                                                                    class="form-control"
                                                                                                    readonly="true"
                                                                                                    runat="server" />
                                                                                                <span
                                                                                                    class="input-group-btn">
                                                                                                    <button
                                                                                                        id="btn2"
                                                                                                        class="btn default"
                                                                                                        type="button"
                                                                                                        disabled="disabled"
                                                                                                        runat="server">
                                                                                                        <i
                                                                                                            class="fa fa-calendar"></i>
                                                                                                    </button>
                                                                                                </span>
                                                                                            </div>


                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>

                                                                                        <td><b>U. L. W. as per RC:
                                                                                                    <span
                                                                                                        style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="VULW"
                                                                                                MaxLength="5"
                                                                                                CssClass="number form-control"
                                                                                                runat="server"
                                                                                                ReadOnly="true">
                                                                                            </asp:TextBox>


                                                                                        </td>
                                                                                        <td><b>R. L. W. as per RC:
                                                                                                    <span
                                                                                                        style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="VRLW"
                                                                                                MaxLength="5"
                                                                                                CssClass="number form-control"
                                                                                                runat="server"
                                                                                                ReadOnly="true">
                                                                                            </asp:TextBox>


                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>


                                                                                        <td><b>Seating Capacity:
                                                                                                    <span
                                                                                                        style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox
                                                                                                ID="VSeatCapacity"
                                                                                                MaxLength="3"
                                                                                                CssClass="number form-control"
                                                                                                runat="server"
                                                                                                ReadOnly="true">
                                                                                            </asp:TextBox>


                                                                                        </td>
                                                                                        <td><b>Manufacturing Date:
                                                                                                    <span
                                                                                                        style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%--<div
                                                                                                    class="input-group input-medium date date-picker"
                                                                                                    data-date-format="dd-M-yyyy"
                                                                                                    data-date-start-date="-12y"
                                                                                                    data-date-end-date="-1d">
                                                                                                    <input type="text"
                                                                                                        id="VManufacturingON"
                                                                                                        disabled="disabled"
                                                                                                        class="form-control"
                                                                                                        readonly="true"
                                                                                                        runat="server" />
                                                                                                    <span
                                                                                                        class="input-group-btn">
                                                                                                        <button
                                                                                                            id="btn3"
                                                                                                            class="btn default"
                                                                                                            type="button"
                                                                                                            disabled="disabled"
                                                                                                            runat="server">
                                                                                                            <i
                                                                                                                class="fa fa-calendar"></i>
                                                                                                        </button>
                                                                                                    </span>
                                                                                </div>--%>
                                                                                            <div id='container'>
                                                                                                <input id="VManufacturingON"
                                                                                                    type="text" runat="server"
                                                                                                    class="form-control" />
                                                                                            </div>

                                                                                        </td>


                                                                                    </tr>
                                                                                </table>

                                                                                <div class="col-md-12">
                                                                                    <asp:HiddenField ID="HDRegisteredat"
                                                                                        runat="server">
                                                                                    </asp:HiddenField>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="portlet box green">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">
                                                                            Route Details <span
                                                                                style="color: red">*</span>
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a href="javascript:;" class="collapse"></a>
                                                                        </div>
                                                                    </div>

                                                                    <div class="portlet-body">
                                                                        <br />
                                                                        <div class="row">
                                                                            <div class="col-md-10"></div>
                                                                            <div class="col-md-2">

                                                                                <a href="javascript:fnExistsReg();"
                                                                                    id="RouteReg">Existing Vehicles</a>


                                                                            </div>
                                                                        </div>
                                                                        <div class="row" id="RouteHDRInfo"
                                                                            runat="server">
                                                                        </div>
                                                                        <br />

                                                                        Time Format: 24 Hours
                                                                        <br />
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
                                                                                <table
                                                                                    class="table table-striped table-bordered table-hover">
                                                                                    <tr>
                                                                                        <td><b>Mobile No : <span
                                                                                            style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtMobileNo"
                                                                                                MaxLength="10"
                                                                                                CssClass="number form-control"
                                                                                                oninput="this.value=this.value.replace(/[^0-9]/g,'')"
                                                                                                runat="server">
                                                                                            </asp:TextBox>
                                                                                        </td>
                                                                                        <td><b>Email ID : </b></td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtEmailId"
                                                                                                CssClass="form-control"
                                                                                                runat="server">
                                                                                            </asp:TextBox>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr>


                                                                                        <td><b>Gender : <span
                                                                                            style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="radio-list"
                                                                                                style="padding-left: 25px">
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="gender"
                                                                                                        id="txtMale"
                                                                                                        value="Male" />
                                                                                                    Male

                                                                                                </label>
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="gender"
                                                                                                        id="txtFemale"
                                                                                                        value="Female" />
                                                                                                    Female

                                                                                                </label>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td><b>Qualification : <span
                                                                                            style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <select
                                                                                                id="ddlQualification"
                                                                                                name="ddlQualification"
                                                                                                onchange="javascript:fnQualification();"
                                                                                                class="form-control select2"
                                                                                                runat="server">

                                                                                                <option value="0">--select
                                                                                                    qualification--
                                                                                                </option>
                                                                                                <option value="Master">Master</option>
                                                                                                <option
                                                                                                    value="Bachelor">Bachelor</option>
                                                                                                <option
                                                                                                    value="Intermediate">Intermediate
                                                                                                </option>
                                                                                                <option
                                                                                                    value="Matriculation">Matriculation
                                                                                                </option>
                                                                                                <option
                                                                                                    value="Illiterate">Illiterate</option>

                                                                                            </select></td>
                                                                                    </tr>

                                                                                    <tr>


                                                                                        <td><b>Category of Applicant :
                                                                                                <span
                                                                                                    style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <select id="ddlCategory"
                                                                                                name="ddlCategory"
                                                                                                onchange="javascript:fnCategory();"
                                                                                                class="form-control select2"
                                                                                                runat="server">

                                                                                                <option value="0">--select category--
                                                                                                </option>
                                                                                                <option value="Gen">Gen
                                                                                                </option>
                                                                                                <option value="SC">SC
                                                                                                </option>
                                                                                                <option value="ST">ST
                                                                                                </option>
                                                                                                <option value="OBC">OBC
                                                                                                </option>

                                                                                            </select></td>
                                                                                        <td><b>Ex-Army category : <span
                                                                                            style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="radio-list"
                                                                                                style="padding-left: 25px">
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="ExArmy"
                                                                                                        onchange="javascript:fnExArmy();"
                                                                                                        id="txtExArmyY"
                                                                                                        value="Yes" />
                                                                                                    Yes

                                                                                                </label>
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="ExArmy"
                                                                                                        onchange="javascript:fnExArmy();"
                                                                                                        id="txtExArmyN"
                                                                                                        value="No" />
                                                                                                    No

                                                                                                </label>
                                                                                                &nbsp;

                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>



                                                                                        <td><b>Minority : <span
                                                                                            style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="radio-list"
                                                                                                style="padding-left: 25px">
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="Minority"
                                                                                                        id="txtMinorityY"
                                                                                                        value="Yes" />
                                                                                                    Yes

                                                                                                </label>
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="Minority"
                                                                                                        id="txtMinorityN"
                                                                                                        value="No" />
                                                                                                    No

                                                                                                </label>
                                                                                                &nbsp;

                                                                                            </div>
                                                                                        </td>
                                                                                        <td><b>Handicapped : <span
                                                                                            style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="radio-list"
                                                                                                style="padding-left: 25px">
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="Handicapped"
                                                                                                        onchange="javascript:fnHandicapped();"
                                                                                                        id="txtHandicappedY"
                                                                                                        value="Yes" />
                                                                                                    Yes

                                                                                                </label>
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="Handicapped"
                                                                                                        onchange="javascript:fnHandicapped();"
                                                                                                        id="txtHandicappedN"
                                                                                                        value="No" />
                                                                                                    No

                                                                                                </label>
                                                                                                &nbsp;

                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>


                                                                                        <td><b>Heavy Motor Licence :
                                                                                                <span
                                                                                                    style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="radio-list"
                                                                                                style="padding-left: 25px">
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="HML"
                                                                                                        onchange="javascript:fnHML();"
                                                                                                        id="txtHMLY"
                                                                                                        value="Yes" />
                                                                                                    Yes

                                                                                                </label>
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="HML"
                                                                                                        onchange="javascript:fnHML();"
                                                                                                        id="txtHMLN"
                                                                                                        value="No" />
                                                                                                    No

                                                                                                </label>
                                                                                                &nbsp;

                                                                                            </div>
                                                                                        </td>
                                                                                        <td><b>Identity Proof : <span
                                                                                            style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="radio-list"
                                                                                                style="padding-left: 25px">
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="IdentityProof"
                                                                                                        onchange="javascript:fnIdentityProof();"
                                                                                                        id="txtIdentityProofV"
                                                                                                        value="Voter Id" />
                                                                                                    Voter Id

                                                                                                </label>
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="IdentityProof"
                                                                                                        onchange="javascript:fnIdentityProof();"
                                                                                                        id="txtIdentityProofP"
                                                                                                        value="Pan Card" />
                                                                                                    Pan Card

                                                                                                </label>
                                                                                                &nbsp;
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="IdentityProof"
                                                                                                        onchange="javascript:fnIdentityProof();"
                                                                                                        id="txtIdentityProofA"
                                                                                                        value="Aadhaar Card" />
                                                                                                    Aadhaar Card

                                                                                                </label>
                                                                                                &nbsp;
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>


                                                                                        <td><b>Bus Type (AC) : <span
                                                                                            style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="radio-list"
                                                                                                style="padding-left: 25px">
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="AC"
                                                                                                        id="txtACY"
                                                                                                        value="Yes" />
                                                                                                    Yes

                                                                                                </label>


                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="AC"
                                                                                                        id="txtACN"
                                                                                                        value="No" />
                                                                                                    No

                                                                                                </label>
                                                                                                &nbsp;

                                                                                            </div>
                                                                                        </td>
                                                                                        <td><b>Bus Type (Deluxe) :
                                                                                                <span
                                                                                                    style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="radio-list"
                                                                                                style="padding-left: 25px">
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="Deluxe"
                                                                                                        id="txtDeluxeY"
                                                                                                        value="Yes" />
                                                                                                    Yes

                                                                                                </label>
                                                                                                <label
                                                                                                    class="radio-inline">


                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="Deluxe"
                                                                                                        id="txtDeluxeN"
                                                                                                        value="No" />
                                                                                                    No

                                                                                                </label>
                                                                                                &nbsp;

                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td><b>Bus Type (Sleeper) :
                                                                                                <span
                                                                                                    style="color: red">*</span></b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="radio-list"
                                                                                                style="padding-left: 25px">
                                                                                                <label
                                                                                                    class="radio-inline">
                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="Sleeper"
                                                                                                        id="txtSleeperY"
                                                                                                        value="Yes" />
                                                                                                    Yes

                                                                                                </label>
                                                                                                <label
                                                                                                    class="radio-inline">


                                                                                                    <input type="radio"
                                                                                                        runat="server"
                                                                                                        name="Sleeper"
                                                                                                        id="txtSleeperN"
                                                                                                        value="No" />
                                                                                                    No

                                                                                                </label>
                                                                                                &nbsp;

                                                                                            </div>
                                                                                        </td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                </table>

                                                                            </div>
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
                                                                                <div
                                                                                    class="col-md-12 col-sm-12 col-xs-12">
                                                                                    <table
                                                                                        class="table table-striped table-bordered table-hover">
                                                                                        <tr>
                                                                                            <td>Registration Certificate
                                                                                                (RC) <span
                                                                                                    style="color: red">*</span>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:FileUpload
                                                                                                    runat="server"
                                                                                                    ID="uploadRC"
                                                                                                    accept="application/pdf, .pdf">
                                                                                                </asp:FileUpload>
                                                                                            </td>
                                                                                            <td>Speed Governor
                                                                                                Certificate <span
                                                                                                    style="color: red">*</span>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:FileUpload
                                                                                                    runat="server"
                                                                                                    ID="uploadSpeed"
                                                                                                    accept="application/pdf, .pdf">
                                                                                                </asp:FileUpload>
                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Insurance Certificate
                                                                                                <span
                                                                                                    style="color: red">*</span>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:FileUpload
                                                                                                    runat="server"
                                                                                                    ID="uploadInsur"
                                                                                                    accept="application/pdf, .pdf">
                                                                                                </asp:FileUpload>
                                                                                            </td>
                                                                                            <td>Insurance Certificate
                                                                                                Valid Upto <span
                                                                                                    style="color: red">*</span>
                                                                                            </td>
                                                                                            <td>
                                                                                                <div class="input-group input-medium date date-picker"
                                                                                                    data-date-format="dd/mm/yyyy"
                                                                                                    data-date-start-date="+0d">
                                                                                                    <input type="text"
                                                                                                        id="txtICValidUpto"
                                                                                                        class="form-control"
                                                                                                        readonly="true"
                                                                                                        runat="server" />
                                                                                                    <span
                                                                                                        class="input-group-btn">
                                                                                                        <button
                                                                                                            class="btn default"
                                                                                                            type="button">
                                                                                                            <i
                                                                                                                class="fa fa-calendar"></i>
                                                                                                        </button>
                                                                                                    </span>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Pollution Certificate
                                                                                                <span
                                                                                                    style="color: red">*</span>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:FileUpload
                                                                                                    runat="server"
                                                                                                    ID="uploadPollution"
                                                                                                    accept="application/pdf, .pdf">
                                                                                                </asp:FileUpload>
                                                                                            </td>
                                                                                            <td>Pollution Certificate
                                                                                                Valid Upto <span
                                                                                                    style="color: red">*</span>
                                                                                            </td>
                                                                                            <td>
                                                                                                <div class="input-group input-medium date date-picker"
                                                                                                    data-date-format="dd/mm/yyyy"
                                                                                                    data-date-start-date="+0d">
                                                                                                    <input type="text"
                                                                                                        id="txtPCValidUpto"
                                                                                                        class="form-control"
                                                                                                        readonly="true"
                                                                                                        runat="server" />
                                                                                                    <span
                                                                                                        class="input-group-btn">
                                                                                                        <button
                                                                                                            class="btn default"
                                                                                                            type="button">
                                                                                                            <i
                                                                                                                class="fa fa-calendar"></i>
                                                                                                        </button>
                                                                                                    </span>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Fitness Certificate
                                                                                                <span
                                                                                                    style="color: red">*</span>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:FileUpload
                                                                                                    runat="server"
                                                                                                    ID="uploadFitness"
                                                                                                    accept="application/pdf, .pdf">
                                                                                                </asp:FileUpload>
                                                                                            </td>
                                                                                            <td>Fitness Certificate
                                                                                                Valid Upto <span
                                                                                                    style="color: red">*</span>
                                                                                            </td>
                                                                                            <td>
                                                                                                <div class="input-group input-medium date date-picker"
                                                                                                    data-date-format="dd/mm/yyyy"
                                                                                                    data-date-start-date="+0d">
                                                                                                    <input type="text"
                                                                                                        id="txtFCValidUpto"
                                                                                                        class="form-control"
                                                                                                        readonly="true"
                                                                                                        runat="server" />
                                                                                                    <span
                                                                                                        class="input-group-btn">
                                                                                                        <button
                                                                                                            class="btn default"
                                                                                                            type="button">
                                                                                                            <i
                                                                                                                class="fa fa-calendar"></i>
                                                                                                        </button>
                                                                                                    </span>
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                    <br />
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div id="divQualification"
                                                                                    runat="server" style="display: none">
                                                                                    <div class="col-md-3 col-sm-3">
                                                                                        Qualification Certificate <span
                                                                                            style="color: red">*</span>
                                                                                    </div>
                                                                                    <div class="col-md-9 col-sm-9">
                                                                                        <asp:FileUpload runat="server"
                                                                                            ID="uploadQualification"
                                                                                            accept="application/pdf, .pdf">
                                                                                        </asp:FileUpload>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div id="divSCST" runat="server"
                                                                                    style="display: none">
                                                                                    <div class="col-md-3 col-sm-3">
                                                                                        Category Certificate <span
                                                                                            style="color: red">*</span>
                                                                                    </div>
                                                                                    <div class="col-md-9 col-sm-9">
                                                                                        <asp:FileUpload runat="server"
                                                                                            ID="uploadSCST"
                                                                                            accept="application/pdf, .pdf">
                                                                                        </asp:FileUpload>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div id="divEXArmy" runat="server"
                                                                                    style="display: none">
                                                                                    <div class="col-md-3 col-sm-3">
                                                                                        Ex-Army Certificate <span
                                                                                            style="color: red">*</span>
                                                                                    </div>
                                                                                    <div class="col-md-9 col-sm-9">

                                                                                        <asp:FileUpload runat="server"
                                                                                            ID="uploadEXArmy"
                                                                                            accept="application/pdf, .pdf">
                                                                                        </asp:FileUpload>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div id="divHandicaped" runat="server"
                                                                                    style="display: none">
                                                                                    <div class="col-md-3 col-sm-3">
                                                                                        Handicapped Certificate <span
                                                                                            style="color: red">*</span>
                                                                                    </div>
                                                                                    <div class="col-md-9 col-sm-9">
                                                                                        <asp:FileUpload runat="server"
                                                                                            ID="uploadHandicaped"
                                                                                            accept="application/pdf, .pdf">
                                                                                        </asp:FileUpload>
                                                                                    </div>
                                                                                </div>


                                                                            </div>
                                                                            <br />
                                                                            <div class="row">
                                                                                <div id="divHML" runat="server"
                                                                                    style="display: none">
                                                                                    <div class="col-md-3 col-sm-3">
                                                                                        Heavy
                                                                                        Motor License <span
                                                                                            style="color: red">*</span>
                                                                                    </div>
                                                                                    <div class="col-md-9 col-sm-9">
                                                                                        <asp:FileUpload runat="server"
                                                                                            ID="uploadHML"
                                                                                            accept="application/pdf, .pdf">
                                                                                        </asp:FileUpload>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <br />

                                                                            <div class="row">
                                                                                <div id="divIdentityProof"
                                                                                    runat="server" style="display: none">

                                                                                    <div class="col-md-3 col-sm-3">
                                                                                        Voter
                                                                                        Id/ Pan Card/ Aadhaar Card No.
                                                                                        <span style="color: red">*</span>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-3">
                                                                                        <asp:TextBox ID="txtIdentityNo"
                                                                                            CssClass="regtxtnumber form-control"
                                                                                            runat="server">
                                                                                        </asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-3">
                                                                                        Voter
                                                                                        Id/ Pan Card/ Aadhaar Card File
                                                                                        <span style="color: red">*</span>
                                                                                    </div>
                                                                                    <div class="col-md-3 col-sm-3">

                                                                                        <asp:FileUpload runat="server"
                                                                                            ID="uploadIdentityProof"
                                                                                            accept="application/pdf, .pdf">
                                                                                        </asp:FileUpload>
                                                                                    </div>
                                                                                </div>
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
                                                                            <div class="col-md-3 col-sm-3">
                                                                                <b>Application Fees :</b>
                                                                            </div>
                                                                            <div class="col-md-3 col-sm-3">
                                                                                <asp:Label ID="lblPermitFees"
                                                                                    runat="server"></asp:Label>
                                                                            </div>
                                                                            <div class="col-md-3 col-sm-3"></div>
                                                                            <div class="col-md-3 col-sm-3"></div>
                                                                        </div>


                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <div class="col-md-12 col-sm-12">
                                                                <div class="portlet box green">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">
                                                                            Mobile Verification
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a href="javascript:;" class="collapse"></a>
                                                                        </div>
                                                                    </div>

                                                                    <div class="portlet-body">

                                                                        <!-- MOBILE NUMBER (AUTO FILLED + READONLY) -->
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                                <b>Mobile Number :</b>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtMobileOtp"
                                                                                    runat="server"
                                                                                    CssClass="form-control"
                                                                                    ReadOnly="true">
                                                                                </asp:TextBox>
                                                                            </div>

                                                                            <!-- SEND OTP -->
                                                                            <div class="col-md-3">
                                                                                <asp:Button ID="btnSendOtp" runat="server"
                                                                                    CssClass="btn blue"
                                                                                    Text="Send OTP"
                                                                                    OnClick="btnSendOtp_Click" />
                                                                            </div>
                                                                        </div>

                                                                        <br />

                                                                        <!-- OTP INPUT -->
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                                <b>Enter OTP :</b>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <asp:TextBox ID="txtOtp" runat="server"
                                                                                    CssClass="form-control"
                                                                                    MaxLength="6">
                                                                                </asp:TextBox>
                                                                            </div>

                                                                            <!-- VERIFY BUTTON -->
                                                                            <div class="col-md-3">
                                                                                <asp:Button ID="btnVerifyOtp" runat="server"
                                                                                    CssClass="btn green"
                                                                                    Text="Verify OTP"
                                                                                    OnClick="btnVerifyOtp_Click" />
                                                                            </div>
                                                                        </div>

                                                                        <br />

                                                                        <!-- RESEND + TIMER -->
                                                                        <div class="row">
                                                                            <div class="col-md-3"></div>

                                                                            <div class="col-md-3">
                                                                                <asp:Button ID="btnResendOtp" runat="server"
                                                                                    CssClass="btn yellow"
                                                                                    Text="Resend OTP"
                                                                                    OnClick="btnSendOtp_Click"
                                                                                    Style="display: none;" />

                                                                                <span id="otpTimer"
                                                                                    style="margin-left: 10px; font-weight: bold; color: red;"></span>
                                                                            </div>
                                                                        </div>

                                                                        <br />

                                                                        <!-- STATUS -->
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <asp:Label ID="lblOtpStatus" runat="server"
                                                                                    Font-Bold="true"></asp:Label>
                                                                            </div>
                                                                        </div>

                                                                        <!-- IMPORTANT -->
                                                                        <asp:HiddenField ID="hdnOtpVerified" runat="server"
                                                                            Value="false" />

                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-12 col-sm-12">

                                                                <label for="modalAgree">
                                                                    <a style="color: red"
                                                                        href="javascript:fnTermsCondition('pafs');"><b>Terms
                                                                            & Conditions</b></a> (By procedding you are
                                                                    agree with the Terms and condition)</label>
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <div class="col-md-12 col-sm-12" style="text-align: center">
                                                                <asp:Button ID="btnSave" runat="server"
                                                                    CssClass="btn green" Text="Proceed" Enabled="false"
                                                                    OnClick="btnSave_Click"
                                                                    OnClientClick="return SaveApplication()" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnReset" CssClass="btn default"
                                                                    runat="server" Text="Reset"
                                                                    OnClientClick="return fnClearPermitNew()"
                                                                    OnClick="btnReset_Click" />

                                                            </div>


                                                        </div>
                                        </div>
                                    </div>
                                    <div id="ApplicantonRoute" class="modal fade" tabindex="-1" data-backdrop="static"
                                        data-keyboard="false">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h4 class="modal-title"><b>Existing Vehicles On Selected Route</b>
                                                    </h4>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="scroller" data-height="400px" data-always-visible="1"
                                                        data-rail-visible1="1">
                                                        <div id="RouteApplicants" runat="server"
                                                            style="overflow: scroll">
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" data-dismiss="modal"
                                                        class="btn black">
                                                        Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="RegRouteDetail" class="modal fade" tabindex="-1" data-backdrop="static"
                                        data-keyboard="false">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h4 class="modal-title"><b>Route Detail Of Selected Registration
                                                            Number : <span id="regNumber"></span></b></h4>
                                                </div>

                                                <div class="modal-body">
                                                    <div class="scroller" data-height="400px" data-always-visible="1"
                                                        data-rail-visible1="1">
                                                        <div id="divRouteRegDetail" runat="server"
                                                            style="overflow: scroll">
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" data-dismiss="modal"
                                                        class="btn black">
                                                        Close</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </asp:Panel>

                                                </ContentTemplate>
                                                <triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlQualification" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCategory" />
                                                    <asp:PostBackTrigger ControlID="btnSave" />
                                                </triggers>

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
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js"
        type="text/javascript"></script>
    <script src="assets/global/plugins/moment.min.js" type="text/javascript"></script>

    <script src="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"
        type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"
        type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"
        type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"
        type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>
    <script src="assets/js/STA_PERMIT.js" type="text/javascript"></script>
    <script src="assets/js/monthpicker.min.js"></script>
    <script src="assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js"
        type="text/javascript"></script>
    <script src="assets/js/form-input-mask1.js" type="text/javascript"></script>

    <script>
        if (hdnOtpVerified != "true") {
            $('#btnSave').prop('disabled', true);
        }
    </script>
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
    <script>
        var timerInterval;
        var timeLeft = 30;

        function startOtpTimer() {

            timeLeft = 30;

            var resendBtn = document.getElementById('<%= btnResendOtp.ClientID %>');
            var timerLabel = document.getElementById('otpTimer');

            resendBtn.style.display = "none";

            timerInterval = setInterval(function () {

                timerLabel.innerHTML = "Resend in " + timeLeft + " sec";

                timeLeft--;

                if (timeLeft < 0) {
                    clearInterval(timerInterval);
                    timerLabel.innerHTML = "";
                    resendBtn.style.display = "inline-block";
                }

            }, 1000);
        }
    </script>
</asp:Content>
