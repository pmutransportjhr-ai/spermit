<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-COUNTER-REPORT-EDIT-VIEW.aspx.cs" Inherits="STA_COUNTER_REPORT_EDIT_VIEW" %>

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
                                    <span>STA Counter Signature Report</span>
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
                                                            <span class="caption-subject font-red sbold uppercase">STA Counter Signature Report</span>
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
                                                                <div class="col-md-6 col-sm-6">
                                                                    <b  style="font-size:large">Permit Type : </b>
                                                                    <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                 <b  style="font-size:large">Status : </b>
                                                                 <asp:Label ID="lblApplicationStatus" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <br /><br />
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12"><h4><b><u>Personal Information</b></u></h4></div>
                                                            <div class="col-md-12 col-sm-12">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    <td><b>Name : </b></td>
                                                                    <td><asp:TextBox ID="VOName" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                    <td><b>C/O :  </b></td>
                                                                    <td><asp:TextBox ID="VOCareOf" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Present Address: </b></td>
                                                                    <td><asp:TextBox ID="VOPresentAddress" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                    <td><b>Permanent Address : </b></td>
                                                                    <td><asp:TextBox ID="VOPermanentAddress" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12"><h4><b><u>Vehicle Details</u></b></h4></div>
                                                            <div class="col-md-12 col-sm-12">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <tr>
                                                                        <td><b>Registration No : </b></td>
                                                                        <td><asp:TextBox ID="VORegNumber" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                        <td><b>Permit No : </b></td>
                                                                        <td><asp:Label ID="VOPermitNo" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Counter Signature No :</b></td>
                                                                        <td><asp:TextBox ID="VOCSNo" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Permit Validity From : </b></td>
                                                                        <td><asp:Label ID="VoPermitFrom" runat="server"></asp:Label></td>
                                                                        <td><b>Permit Validity Upto : </b></td>
                                                                        <td><asp:Label ID="VoPermitUpto" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Maker Description : </b></td>
                                                                        <td><asp:TextBox ID="lblMakerDesc" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                        <td><b>Maker Model No : </b></td>
                                                                        <td><asp:TextBox ID="lblMakermodel" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Type of Vehicle :</b></td>
                                                                        <td><asp:TextBox ID="VType" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                        <td><b>Chassis Number : </b></td>
                                                                        <td><asp:TextBox ID="VChassisNumber" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Engine Number  : </b></td>
                                                                        <td><asp:TextBox ID="VEngineNumber" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                        <td><b>Seating Capacity : </b></td>
                                                                        <td><asp:TextBox ID="VSeatCapacity" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Sleeper Capacity : </b></td>
                                                                        <td><asp:TextBox ID="vsleepercapacity" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>U.L.W as per RC : </b></td>
                                                                        <td><asp:TextBox ID="txtULW" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                        <td><b>R.L.W as per RC : </b></td>
                                                                        <td><asp:TextBox ID="txtRLW" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Manufacturing Date :</b></td>
                                                                        <td>
                                                                            <div id='container'>
                                                                                  <input id="VManufacturingON" type="text" runat="server" class="form-control" readonly="true" />
                                                                            </div> 
                                                                        </td>
                                                                        <td><b>Nature of goods to be carried, if any :</b></td>
                                                                        <td> <asp:Label ID="VNatureOfGoods" runat="server"></asp:Label></td>                                                                         
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12"><h4><b><u>Route Details</u></b></h4></div>
                                                        </div> <br />
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-3"><b>Issued by : </b></div>
                                                            <div class="col-md-9 col-sm-9"><asp:Label  ID="lblIssuedBy" runat="server" /></div>
                                                        </div> <br />
                                                        <div class="row">
                                                            <div class="col-md-3 col-sm-3"><b>Route : </b></div>
                                                            <div class="col-md-9 col-sm-9"><asp:Label  ID="RouteHDRInfo" runat="server" /></div>
                                                        </div> <br /> Time Format: 24 Hours <br />
                                                        <div id="RouteTimeInfo" runat="server"></div>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12"><h4><b><u>Other Details</u></b></h4></div>
                                                            <div class="col-md-12 col-sm-12">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <tr>
                                                                        <td><b>Mobile No: </b></td>
                                                                        <td><asp:Label ID="VOMobileNo" runat="server"></asp:Label></td>
                                                                        <td><b>Email ID: </b></td>
                                                                        <td><asp:Label ID="VOEmailID" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Recommendation Letter No:</b></td>
                                                                        <td><asp:Label ID="lblRecommendationNo" runat="server"></asp:Label></td>
                                                                        <td><b>Recommendation Date:</b></td>
                                                                        <td><asp:Label ID="lblRecommendationDate" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12"><h4><b><u>Tax Details</u></b></h4></div>
                                                            <div class="col-md-12 col-sm-12">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <tr>
                                                                        <td><b>Amount: </b></td>
                                                                        <td><asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                        <td><b>Pay Mode: </b></td>
                                                                        <td><asp:DropDownList ID="ddlPayMode" runat="server" CssClass="form-control">
                                                                                <asp:ListItem Value="q">Quartarly</asp:ListItem>
                                                                                <asp:ListItem Value="h">Half Yearly</asp:ListItem>
                                                                                <asp:ListItem Value="y">Yearly</asp:ListItem>
                                                                            </asp:DropDownList></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>From:</b></td>
                                                                        <td>
                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                <input type="text" id="txtFromDate" class="form-control" runat="server" readonly="true" />
                                                                                <span class="input-group-btn">
                                                                                    <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                                                </span>
                                                                            </div>
                                                                        </td>
                                                                        <td><b>To:</b></td>
                                                                        <td>
                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                <input type="text" id="txtToDate" class="form-control" runat="server" readonly="true" />
                                                                                <span class="input-group-btn">
                                                                                    <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                                                </span>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Ref. No:</b></td>
                                                                        <td><asp:TextBox ID="txtPayRef" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                                        <td><b>Paid On:</b></td>
                                                                        <td>
                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                <input type="text" id="txtPaidOn" class="form-control" runat="server" readonly="true" />
                                                                                <span class="input-group-btn">
                                                                                    <button class="btn default" type="button"><i class="fa fa-calendar"></i></button>
                                                                                </span>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                        <br /><br />
                                                        <div class="col-md-12" style="text-align:center">
                                                            <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Update" OnClick="btnSave_Click" />
                                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Close" OnClientClick="javascript:window.close();return false;"  />
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
    <script src="assets/js/SCREEN_STA_COUNTER_SIGNATURE_APPLICATION.js" type="text/javascript"></script>
     <script src="assets/js/monthpicker.min.js"></script>
    <script src="assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js" type="text/javascript"></script>
    <script src="assets/js/form-input-mask1.js" type="text/javascript"></script>

</asp:Content>


