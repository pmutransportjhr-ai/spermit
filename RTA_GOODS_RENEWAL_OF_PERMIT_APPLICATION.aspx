<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="RTA_GOODS_RENEWAL_OF_PERMIT_APPLICATION.aspx.cs" Inherits="RTA_GOODS_RENEWAL_OF_PERMIT_APPLICATION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="FormID1" enctype="multipart/form-data" runat="server" class="form-horizontal" autocomplete="off" method="post">
        <asp:HiddenField ID="hdnTypeID" runat="server" />
        <asp:HiddenField ID="hdnTypeOptionID" runat="server" />
        <asp:Label ID="hdnApplicationNo" runat ="server" CssClass="display:none" ></asp:Label> 
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
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase" id="PageHeading1" runat="server"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <asp:UpdatePanel ID="UPpanel" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Vehicle Reg. No. : </label>
                                                            <span style="color: red">*</span></div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtExistRegNo" CssClass="form-control" runat="server"></asp:TextBox></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Permit No. : </label>
                                                            <span style="color: red">*</span></div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtExistPermitNo" CssClass="form-control" runat="server"></asp:TextBox></div>
                                                        <div class="col-md-4">
                                                            <asp:Button ID="btnGO" runat="server" CssClass="btn green" OnClick="btnGO_Click" OnClientClick="return fnRenewalValidate()" Text="GO" />
                                                            &nbsp;&nbsp;<asp:Button ID="btnExistClear" runat="server" CssClass="btn default" OnClick="btnExistClear_Click" Text="Clear" />
                                                            &nbsp;<asp:Button ToolTip="Cancel" ID="btnCancel" runat="server" CssClass="btn red" Text="Cancel" OnClick="btnCancel_Click" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <asp:Label CssClass="col-md-12" Font-Bold="True" ID="Message" runat="server" Visible="False"></asp:Label>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div id="DivMobileValidate" runat="server" visible="false">

                                                        <div class="row">
                                                            <div class="col-md-3">Enter Your Mobile Number </div>
                                                            <div class="col-md-3">
                                                                <input type="text" runat="server" class="number form-control" id="txtMobileNumber" placeholder="Mobile Number" />
                                                            </div>
                                                            <div class="col-md-6">


                                                                <asp:Button ToolTip="Send OTP" ID="btnOTP" runat="server" CssClass="btn green" Text="Send OTP" OnClick="btnOTP_Click" OnClientClick="return fnMobileValidate()" />



                                                            </div>
                                                        </div>
                                                        <br />
                                                    </div>

                                                    <div id="DivOTPMatch" runat="server" visible="false">

                                                        <div class="row">
                                                            <div class="col-md-3">OTP: </div>
                                                            <div class="col-md-3">
                                                                <input type="text" runat="server" class="number form-control" id="txtOTP" placeholder="------" />
                                                            </div>
                                                            <div class="col-md-6">


                                                                <asp:Button ToolTip="Verify" ID="btnVerify" runat="server" CssClass="btn green" Text="Verify" OnClick="btnVerify_Click" OnClientClick="return fnOTPValidate()" />



                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:Panel CssClass="row" ID="Panel1" runat="server" Visible="false">
                                                        <div class="portlet-body">
                                                            <div class="row">

                                                                <div class="col-md-6 col-sm-6">
                                                                    <b style="font-size: large">Permit Type : </b>
                                                                    <asp:Label ID="lblPermitType" runat="server"></asp:Label>



                                                                </div>
                                                                <div class="col-md-6 col-sm-6" style="text-align: right">
                                                                    <b style="font-size: large">Permit No :</b>
                                                                    <asp:Label ID="lblPermitNo" runat="server"></asp:Label>
                                                                </div>
                                                                <br />
                                                                <div class="col-md-6 col-sm-6">

                                                                    <b style="font-size: large">Valid From: </b>
                                                                    <asp:Label ID="lblValidFrom" runat="server"></asp:Label>



                                                                </div>
                                                                <div class="col-md-6 col-sm-6" style="text-align: right">
                                                                    <b style="font-size: large">Valid Up to: </b>
                                                                    <asp:Label ID="lblValidUpto" runat="server"></asp:Label>



                                                                </div>
                                                                <br />
                                                                <br />
                                                                <div class="col-md-12 col-sm-12">
                                                                    <h4><b><u>Personal Information</b></u></h4>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12">
                                                                    <table class="table table-striped table-bordered table-hover">

                                                                        <tr>
                                                                            <td><b>Name : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VOName" runat="server"></asp:Label></td>
                                                                            <td><b>C/O :  </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VOCareOf" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Present Address: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VOPresentAddress" runat="server"></asp:Label><span id=""></span> </td>
                                                                            <td><b>Permanent Address : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VOPermanentAddress" runat="server"></asp:Label></td>

                                                                        </tr>


                                                                    </table>

                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12">

                                                                    <input type="text" id="lblRTOCode" runat="server" style="display: none" />
                                                                     <input type="text" id="lblPermitSelf" runat="server" style="display:none" />
                                                        <input type="text" id="lblAnotherRTOCode" runat="server" style="display:none" />
                                                        <input type="text" id="lblWholeJH" runat="server" style="display:none" />
                                                        <input type="text" id="lblStateFirst" runat="server" style="display:none" />
                                                        <input type="text" id="lblStateSecond" runat="server" style="display:none" />
                                                        <input type="text" id="lblValidityMonth" runat="server" style="display:none" />
                                                        
                                                                    <h4><b><u>Vehicle Details</u></b></h4>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12">

                                                                    <table class="table table-striped table-bordered table-hover">
                                                                        <tr>






                                                                            <td><b>RTO Name: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblRTOName" runat="server"></asp:Label>


                                                                            </td>
                                                                            <td><b>Area of Permit: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblAreaofPermit" runat="server"></asp:Label></td>

                                                                        </tr>



                                                                        <tr>

                                                                            <td><b>Registration No : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VORegNumber" runat="server"></asp:Label></td>
                                                                            <td><b>Vehicle Class Description:</b></td>
                                                                            <td>
                                                                                <asp:Label ID="VType" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Chassis Number : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VChassisNumber" runat="server"></asp:Label></td>
                                                                            <td><b>Engine Number  : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VEngineNumber" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Maker Description : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblMakerDesc" runat="server"></asp:Label></td>
                                                                            <td><b>Maker Model No : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblMakermodel" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Fuel Description :  </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblFuelDesc" runat="server"></asp:Label></td>
                                                                            <td><b>Color : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblVcolor" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Fitness Valid Upto: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VFitness" runat="server"></asp:Label></td>

                                                                            <td><b>Tax Paid UpTo: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VTax" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>

                                                                            <td><b>U. L. W. as per RC: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VULW" runat="server"></asp:Label></td>
                                                                            <td><b>R. L. W. as per RC: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VRLW" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>The Nature of goods proposed to be carried:</b></td>
                                                                            <td>
                                                                                <asp:Label ID="VNatureofGoods" runat="server"></asp:Label>
                                                                                <%--<asp:Label ID="VSeatCapacity" runat="server"></asp:Label>--%>

                                                                            </td>


                                                                            <td><b>Manufacturing Date:</b></td>
                                                                            <td>
                                                                                <asp:Label ID="VManufacturingON" runat="server"></asp:Label></td>

                                                                        </tr>


                                                                        <tr>



                                                                            <td><b>The Rates to be charged by the applicant for carriage of goods:</b></td>
                                                                            <td>
                                                                                <asp:Label ID="txtRates" runat="server"></asp:Label></td>
                                                                            <td><b>Capacity: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="VCapacity" runat="server"></asp:Label></td>
                                                                    </table>



                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12">
                                                                    <h4><b><u>Other Details</u></b></h4>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12">
                                                                    <table class="table table-striped table-bordered table-hover">

                                                                        <tr>
                                                                            <td><b>Mobile No: <span style="color: red">*</span></b></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtMobileNo" CssClass="number form-control" runat="server"></asp:TextBox></td>
                                                                            <td><b>Email ID: </b></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox></td>


                                                                        </tr>
                                                                         <tr>
                                                                    <td><b>Gender: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                    </table>
                                                                </div>
                                                            </div>


                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12">
                                                                    <h4><b><u>Uploaded Documents</u></b></h4>
                                                                </div>

                                                                <div class="col-md-12 col-sm-12">
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
                                                                            <td>Permit Certificate <span style="color: red">*</span></td>
                                                                            <td>
                                                                                <asp:FileUpload runat="server" ID="uploadPermit" accept="application/pdf, .pdf"></asp:FileUpload></td>

                                                                        </tr>
                                                                    </table>

                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12">
                                                                    <h4><b><u>Renewal of Permit Fee Details</u></b></h4>
                                                                </div>
                                                                <div class="col-md-3 col-sm-3"><b>Renewal of Permit Fees : <span style="color: red">*</span></b></div>
                                                                <div class="col-md-9 col-sm-9">
                                                                    <asp:Label ID="lblRenewalPermitFees" runat="server"></asp:Label></div>

                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-4 col-sm-4"></div>
                                                                <div class="col-md-4 col-sm-4">
                                                                    <div style="text-align: center">
                                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Proceed" OnClick="btnSave_Click" OnClientClick="return fnRPApplicationValidate()" />
                                                                        <asp:Button ID="btnReset" runat="server" CssClass="btn green" Text="Reset" OnClick="btnReset_Click" />

                                                                    </div>

                                                                </div>
                                                                <div class="col-md-4 col-sm-4">
                                                                </div>

                                                            </div>
                                                            <br />
                                                    </asp:Panel>
                                                    <br />





                                                    </div>
                                          

                                           

                                       
                                        
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSave" />
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
    <script src="assets/js/RTA_GOODS_RENEWAL_OF_PERMIT_APPLICATION.js" type="text/javascript"></script>
</asp:Content>

