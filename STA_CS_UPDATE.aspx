<%@ Page Title="" Language="C#" MasterPageFile="~/JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA_CS_UPDATE.aspx.cs" Inherits="STA_CS_UPDATE" %>

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

        <asp:Label ID="HiddenField1" runat="server" Visible="false" />
        <asp:Label ID="HiddenField2" runat="server" Visible="false" />
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
                                <span>Counter Signature Update</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">Counter Signature Update</span>
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
                                                            <asp:Button ID="btnGO" runat="server" CssClass="btn green" OnClick="btnGO_Click" OnClientClick="return fnSTAReplacementValidate" Text="GO" />
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
                                                            </div>

                                                            <div class="col-md-12 col-sm-12">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <tr>
                                                                        <td><b>Name : </b></td>
                                                                        <td>
                                                                            <asp:Label ID="VOName" runat="server"></asp:Label></td>
                                                                        <td><b>Vehicle Reg. No. : </b></td>
                                                                        <td>
                                                                            <asp:Label ID="VORegNo" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Valid From: </b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblValidFrom" runat="server"></asp:Label></td>
                                                                        <td><b>Valid Up to: </b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblValidUpto" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Chassis No: </b></td>
                                                                        <td>
                                                                            <asp:Label ID="labelChassisNumber" runat="server"></asp:Label></td>
                                                                        <td><b>Engine No: </b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblEngineno" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Maker : </b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblMaker" runat="server"></asp:Label></td>
                                                                        <td><b>Model: </b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblModel" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Seating Capacity: </b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblseatingcapacity" runat="server" Text=""></asp:Label></td>
                                                                        <td><b>Manufacturing on: </b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblManufactringDate" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Sleeper Capacity: <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtSleeperCapacity" runat="server" MaxLength="2" CssClass="number form-control" AutoPostBack="true" OnTextChanged="txtSleeperCapacity_TextChanged" required></asp:TextBox></td>


                                                                        <td><b>Vehicle Type: </b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblcategory" runat="server" Text=""></asp:Label>


                                                                            <select id="ddlCategory" name="ddlCategory" onchange="javascript:fnCategory();" class="form-control select2" runat="server">

                                                                                <option value="0">--select category--</option>
                                                                                <option value="Ordinary">Ordinary</option>
                                                                                <option value="Semi Deluxe">Semi Deluxe</option>
                                                                                <option value="Deluxe">Deluxe</option>
                                                                                <option value="Volvo">Volvo</option>

                                                                            </select></td>

                                                                        <td>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlCategory" InitialValue="0" runat="server" ErrorMessage="Please select category"></asp:RequiredFieldValidator>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Last Payment From : </b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblPayFrom" runat="server"></asp:Label></td>
                                                                        <td><b>Last Payment Upto : </b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblPayUpto" runat="server"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Category Certificate</b>  <span style="color: red"></span></td>
                                                                        <td>
                                                                            <asp:FileUpload runat="server" ID="uploadCC" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4 col-sm-4"></div>
                                                            <div class="col-md-4 col-sm-4">
                                                                <div style="text-align: center">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Update" OnClick="btnSave_Click" />
                                                                    <asp:Button ID="btnReset" runat="server" CssClass="btn green" Text="Cancel" OnClick="btnReset_Click" />
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
    <script src="assets/js/CategoryCertificate.js" type="text/javascript"></script>
    <script type="text/javascript">


        $('#ContentPlaceHolder1_txtExistRegNo').on('blur', function () {
            $('#loadingmessage').modal('show');
            var registrationNumber = $(this).val();
            var regNumber = new Object();
            regNumber.REG = registrationNumber;

            $.ajax({
                type: "POST",
                url: "STA_COUNTER_SIGNATURE_TAX_PAY.aspx/GetCSPermitnumber",
                cache: false,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ objRegNumber: regNumber }),
                success: function (data) {
                    if (data == null) {
                        $('#ContentPlaceHolder1_txtExistPermitNo').val('');
                        $('#ContentPlaceHolder1_txtExistRegNo').val('');
                    }
                    else {
                        $('#ContentPlaceHolder1_txtExistPermitNo').val(data.d);
                    }
                    $('#loadingmessage').modal('hide');

                },
                error: function (response) {
                    $('#loadingmessage').modal('hide');
                    alert(response.status + ' ' + response.statusText);
                }
            });


        });

    </script>


</asp:Content>

