<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA_TRANSFER_OF_PERMIT.aspx.cs" Inherits="STA_TRANSFER_OF_PERMIT" %>

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
    <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
        <asp:HiddenField ID="hdnTypeID" runat="server" />
        <asp:HiddenField ID="hdnTypeOptionID" runat="server" />
        
        <asp:Label ID="hdnOldApplicationNo" runat="server" Visible="false"></asp:Label>

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
                                                            <asp:Button ID="btnGO" runat="server" CssClass="btn green" OnClick="btnGO_Click" OnClientClick="return fnSTAReplacementValidate()" Text="GO" />
                                                            &nbsp;&nbsp;<asp:Button ID="btnExistClear" runat="server" CssClass="btn default" OnClientClick="return fnClearPermitNew()" OnClick="btnExistClear_Click" Text="Clear" />
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
                                                                <div class="col-md-12 col-sm-12 col-xs-12">
                                                                    <table class="table table-striped table-bordered table-hover">
                                                                        <tr>
                                                                            <td><b>Permit Type : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                                <asp:Label ID="lbPermitId" runat="server" Visible="false"></asp:Label>
                                                                            </td>
                                                                            <td></td>
                                                                            <td></td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Permit Number : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblPermitNo" runat="server"></asp:Label>

                                                                            </td>
                                                                            <td><b>Valid Up to: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblValidUpto" runat="server"></asp:Label>

                                                                            </td>

                                                                        </tr>



                                                                    </table>

                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12">
                                                                    <h4><b><u>Personal Information</b></u></h4>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12">
                                                                    <table class="table table-striped table-bordered table-hover">

                                                                        <tr>
                                                                            <td><b>Name : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelName" runat="server"></asp:Label></td>
                                                                            <td><b>C/O :  </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelCareOf" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Present Address: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelPresentAddress" runat="server"></asp:Label><span id=""></span> </td>
                                                                            <td><b>Permanent Address : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelPermanentAddress" runat="server"></asp:Label></td>

                                                                        </tr>


                                                                    </table>

                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12">
                                                                    <h4><b><u>Vehicle Details</u></b></h4>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12">

                                                                    <table class="table table-striped table-bordered table-hover">
                                                                        <tr>

                                                                            <td><b>Vehicle Registration No : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelRegNumber" runat="server"></asp:Label></td>
                                                                            <td><b>Vehicle Class Description:</b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelType" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Chassis Number : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelChassisNumber" runat="server"></asp:Label></td>
                                                                            <td><b>Engine Number  : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelEngineNumber" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Maker Description : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelMakerDesc" runat="server"></asp:Label></td>
                                                                            <td><b>Maker Model No : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelMakermodel" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Fuel Description :  </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelFuelDesc" runat="server"></asp:Label></td>
                                                                            <td><b>Color : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelcolor" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Fitness Valid Upto: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelFitness" runat="server"></asp:Label></td>

                                                                            <td><b>Tax Paid UpTo: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelTax" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>

                                                                            <td><b>U. L. W. as per RC: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelULW" runat="server"></asp:Label></td>
                                                                            <td><b>R. L. W. as per RC: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelRLW" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                        <tr>


                                                                            <td><b>Seating Capacity: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelSeatCapacity" runat="server"></asp:Label></td>
                                                                            <td><b>Manufacturing Date:</b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelManufacturingON" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                    </table>



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
                                                            <div id="RouteTimeInfo" runat="server"></div>
                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12">
                                                                    <h4><b><u>Other Details</u></b></h4>
                                                                </div>
                                                                <div class="col-md-12 col-sm-12">
                                                                    <table class="table table-striped table-bordered table-hover">
                                                                        <tr>
                                                                            <td><b>Mobile No: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelMobileNo" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td><b>Email ID: </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelEmailId" runat="server"></asp:Label>
                                                                            </td>


                                                                        </tr>
                                                                        <tr>

                                                                            <td><b>Gender : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelGender" runat="server"></asp:Label></td>
                                                                            <td><b>Qualification : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelQualification" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>

                                                                            <td><b>Category of Applicant : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelCategory" runat="server"></asp:Label></td>
                                                                            <td><b>Ex-Army category :</b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelExArmy" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>


                                                                            <td><b>Minority :  </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelMinority" runat="server"></asp:Label></td>
                                                                            <td><b>Handicapped :</b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelHandicapped" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                        <tr>

                                                                            <td><b>Heavy Motor License : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelHML" runat="server"></asp:Label></td>
                                                                            <td><b>Identity Proof : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelIdentityProof" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>

                                                                            <td><b>Bus Type (AC) :</b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelAC" runat="server"></asp:Label></td>
                                                                            <td><b>Bus Type (Deluxe) : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelDeluxe" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Bus Type (Sleeper) :</b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelSleeper" runat="server"></asp:Label></td>
                                                                            <td><b>Voter Id/ Pan Card/ Aadhaar Card No. :</b></td>
                                                                            <td>
                                                                                <asp:Label ID="labelIDNo" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>




                                                            <br />





                                                            <div class="row">

                                                                <div class="col-md-4 col-sm-4">
                                                                    <asp:HiddenField ID="txtVehicleJson" runat="server" />
                                                                    <label for="form_control"><b>Re-Enter your Vehicle Registration Number:  <span style="color: red">*</span></b></label>

                                                                </div>
                                                                <div class="col-md-4 col-sm-4">

                                                                    <asp:TextBox ID="txtVehicleNumber" CssClass="regtxtnumber form-control" runat="server"></asp:TextBox>


                                                                </div>
                                                                <div class="col-md-1 col-sm-1"></div>
                                                                <div class="col-md-3 col-sm-3">


                                                                    <asp:Button ToolTip="Search" ID="btnSearch" runat="server" CssClass="btn green" Text="Search" OnClientClick="return fnSearchValidate()" OnClick="btnSearch_Click" />
                                                                    &nbsp;<asp:Button ToolTip="Clear" ID="btnClear" runat="server" CssClass="btn default" OnClientClick="return fnClearPermitNew()" Text="Clear" OnClick="btnClear_Click" />


                                                                </div>

                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <br />




                                                    <div class="portlet-body">
                                                        <div class="row">
                                                        <div class="col-md-12 col-sm-12">
                                                            <asp:Label ID="lblMSg" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                                        </div></div>
                                                        <br />
                                                        <asp:Panel CssClass="row" ID="Panel2" runat="server" Visible="false">

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
                                                                                            <asp:TextBox ID="VOCareOf" MaxLength="40" CssClass="regtxt form-control" runat="server"></asp:TextBox>
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


                                                                                <td><b>Gender : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <div class="radio-list" style="padding-left: 25px">
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="gender" id="txtMale" value="Male" />
                                                                                            Male
																		
                                                                                        </label>
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="gender" id="txtFemale" value="Female" />
                                                                                            Female
																		
                                                                                        </label>
                                                                                    </div>
                                                                                </td>
                                                                                <td><b>Qualification : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <select id="ddlQualification" name="ddlQualification" onchange="javascript:fnQualification();" class="form-control select2" runat="server">

                                                                                        <option value="0">--select qualification--</option>
                                                                                        <option value="Master">Master</option>
                                                                                        <option value="Bachelor">Bachelor</option>
                                                                                        <option value="Intermediate">Intermediate</option>
                                                                                        <option value="Matriculation">Matriculation</option>
                                                                                        <option value="Illiterate">Illiterate</option>

                                                                                    </select></td>
                                                                            </tr>

                                                                            <tr>


                                                                                <td><b>Category of Applicant : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <select id="ddlCategory" name="ddlCategory" onchange="javascript:fnCategory();" class="form-control select2" runat="server">

                                                                                        <option value="0">--select category--</option>
                                                                                        <option value="Gen">Gen</option>
                                                                                        <option value="SC">SC</option>
                                                                                        <option value="ST">ST</option>
                                                                                        <option value="OBC">OBC</option>

                                                                                    </select></td>
                                                                                <td><b>Ex-Army category : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <div class="radio-list" style="padding-left: 25px">
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="ExArmy" onchange="javascript:fnExArmy();" id="txtExArmyY" value="Yes" />
                                                                                            Yes 
																		
                                                                                        </label>
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="ExArmy" onchange="javascript:fnExArmy();" id="txtExArmyN" value="No" />
                                                                                            No
																		
                                                                                        </label>
                                                                                        &nbsp;
                                                
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>



                                                                                <td><b>Minority : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <div class="radio-list" style="padding-left: 25px">
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="Minority" id="txtMinorityY" value="Yes" />
                                                                                            Yes 
																		
                                                                                        </label>
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="Minority" id="txtMinorityN" value="No" />
                                                                                            No
																		
                                                                                        </label>
                                                                                        &nbsp;
                                                
                                                                                    </div>
                                                                                </td>
                                                                                <td><b>Handicapped : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <div class="radio-list" style="padding-left: 25px">
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="Handicapped" onchange="javascript:fnHandicapped();" id="txtHandicappedY" value="Yes" />
                                                                                            Yes 
																		
                                                                                        </label>
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="Handicapped" onchange="javascript:fnHandicapped();" id="txtHandicappedN" value="No" />
                                                                                            No
																		
                                                                                        </label>
                                                                                        &nbsp;
                                                
                                                                                    </div>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>


                                                                                <td><b>Heavy Motor License  : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <div class="radio-list" style="padding-left: 25px">
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="HML" onchange="javascript:fnHML();" id="txtHMLY" value="Yes" />
                                                                                            Yes 
																		
                                                                                        </label>
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="HML" onchange="javascript:fnHML();" id="txtHMLN" value="No" />
                                                                                            No
																		
                                                                                        </label>
                                                                                        &nbsp;
                                                
                                                                                    </div>
                                                                                </td>
                                                                                <td><b>Identity Proof : <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <div class="radio-list" style="padding-left: 25px">
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="IdentityProof" onchange="javascript:fnIdentityProof();" id="txtIdentityProofV" value="Voter Id" />
                                                                                            Voter Id 
																		
                                                                                        </label>
                                                                                        <label class="radio-inline">
                                                                                            <input type="radio" runat="server" name="IdentityProof" onchange="javascript:fnIdentityProof();" id="txtIdentityProofP" value="Pan Card" />
                                                                                            Pan Card
																		
                                                                                        </label>
                                                                                        &nbsp;
                                                  <label class="radio-inline">
                                                      <input type="radio" runat="server" name="IdentityProof" onchange="javascript:fnIdentityProof();" id="txtIdentityProofA" value="Aadhaar Card" />
                                                      Aadhaar Card
																		
                                                  </label>
                                                                                        &nbsp;
                                                                                    </div>
                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>
                                                                                    Owner of Vehicle is Alive:
                                                                                    </b>
                                                                                </td>
                                                                                <td>
                                                                                    <select id="ddlAlive" class="form-control" runat="server" >
                                                                                        <option selected ="selected" value="Yes" >Yes</option>
                                                                                        <option value="No" >No</option>
                                                                                    </select>
                                                                                </td>
                                                                                <td><b>
                                                                                   Relation with permit holder:<span style="color: red">*</span>
                                                                                    </b>
                                                                                </td>
                                                                                <td>
                                                                                    <select id="ddlRelation" class="form-control" runat="server" >
                                                                                        <option selected ="selected" value="0" >--Please Select--</option>
                                                                                        <option value="Father" >Father</option>
                                                                                         <option value="Mother" >Mother</option>
                                                                                         <option value="Son" >Son</option>
                                                                                         <option value="Daughter" >Daughter</option>
                                                                                         <option value="Other" >Other</option>

                                                                                       
                                                                                    </select>
                                                                                    <br />
                                                                                     <input id="txtRealtion"  runat="server" class="form-control" style="display:none" />
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
                                                                                <td>Jharkhand Tax Token Certificate <span style="color: red">*</span></td>
                                                                                <td>
                                                                                    <asp:FileUpload runat="server" ID="uploadJhtTaxToken" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                                <td>Jharkhand Tax Token Valid Upto <span style="color: red">*</span></td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" data-date-start-date="+0d">
                                                                                        <input type="text" id="txtuploadJhtTaxToken" class="form-control" readonly="true" runat="server" />
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
                                                                                <td>Affidavit Certificate <span style="color: red">*</span></td>
                                                                                <td><asp:FileUpload runat="server" ID="uploadAffidavit" accept="application/pdf, .pdf"></asp:FileUpload></td>
                                                                            </tr>
                                                                            <tr id="trDeath" style="display:none;">
                                                                                <td>
                                                                                    Death Certificate of Vehicle Owner<span style="color: red">*</span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:FileUpload runat="server" ID="uploadDeathCertificate" accept="application/pdf, .pdf"></asp:FileUpload>
                                                                                </td>
                                                                            </tr>

                                                                        </table>
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div id="divQualification" runat="server" style="display: none">
                                                                        <div class="col-md-3 col-sm-3">Qualification Certificate <span style="color: red">*</span></div>
                                                                        <div class="col-md-9 col-sm-9">
                                                                            <asp:FileUpload runat="server" ID="uploadQualification" accept="application/pdf, .pdf"></asp:FileUpload>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div id="divSCST" runat="server" style="display: none">
                                                                        <div class="col-md-3 col-sm-3">Category Certificate <span style="color: red">*</span></div>
                                                                        <div class="col-md-9 col-sm-9">
                                                                            <asp:FileUpload runat="server" ID="uploadSCST" accept="application/pdf, .pdf"></asp:FileUpload>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div id="divEXArmy" runat="server" style="display: none">
                                                                        <div class="col-md-3 col-sm-3">Ex-Army Certificate <span style="color: red">*</span></div>
                                                                        <div class="col-md-9 col-sm-9">

                                                                            <asp:FileUpload runat="server" ID="uploadEXArmy" accept="application/pdf, .pdf"></asp:FileUpload>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div id="divHandicaped" runat="server" style="display: none">
                                                                        <div class="col-md-3 col-sm-3">Handicapped Certificate <span style="color: red">*</span></div>
                                                                        <div class="col-md-9 col-sm-9">
                                                                            <asp:FileUpload runat="server" ID="uploadHandicaped" accept="application/pdf, .pdf"></asp:FileUpload>
                                                                        </div>
                                                                    </div>


                                                                </div>
                                                                <br />
                                                                <div class="row">
                                                                    <div id="divHML" runat="server" style="display: none">
                                                                        <div class="col-md-3 col-sm-3">Heavy Motor License  <span style="color: red">*</span></div>
                                                                        <div class="col-md-9 col-sm-9">
                                                                            <asp:FileUpload runat="server" ID="uploadHML" accept="application/pdf, .pdf"></asp:FileUpload>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <br />

                                                                <div class="row">
                                                                    <div id="divIdentityProof" runat="server" style="display: none">

                                                                        <div class="col-md-3 col-sm-3">Voter Id/ Pan Card/ Aadhaar Card No.  <span style="color: red">*</span></div>
                                                                        <div class="col-md-3 col-sm-3">
                                                                            <asp:TextBox ID="txtIdentityNo" CssClass="regtxtnumber form-control" runat="server"></asp:TextBox></div>
                                                                        <div class="col-md-3 col-sm-3">Attach File <span style="color: red">*</span></div>
                                                                        <div class="col-md-3 col-sm-3">

                                                                            <asp:FileUpload runat="server" ID="uploadIdentityProof" accept="application/pdf, .pdf"></asp:FileUpload>
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="portlet box green">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                Transfer of Permit Fee Details
                                                            </div>
                                                            <div class="tools">
                                                                <a href="javascript:;" class="collapse"></a>
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body">
                                                            <div class="row">
                                                                <div class="col-md-3 col-sm-3"><b>Transfer of Permit Fees :</b></div>
                                                                <div class="col-md-3 col-sm-3">
                                                                    <asp:Label ID="lblReplacevehicleFees" runat="server"></asp:Label></div>
                                                                <div class="col-md-3 col-sm-3"></div>
                                                                <div class="col-md-3 col-sm-3"></div>
                                                            </div>


                                                        </div>
                                                    </div>

                                                                <div class="row">
                                                                    <div class="col-md-12 col-sm-12">

                                                                        <label for="modalAgree"><a style="color: red" href="javascript:fnTermsCondition('ptfs');"><b>Terms & Conditions</b></a> (By procedding you are agree with the Terms and condition)</label>
                                                                    </div>
                                                                </div><br /><br />


                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12">
                                                            <div style="text-align: center">
                                                                <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Proceed" OnClick="btnSave_Click" OnClientClick="return fnRVApplicationValidate()" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnreset" runat="server" CssClass="btn red" Text="Reset" OnClientClick="return fnClearPermitNew()" OnClick="btnreset_Click" />
                                                            </div>

                                                        </div>

                                                    </div>

                                                    </div>
                                              </asp:Panel>
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
    <script src="assets/js/STA_TRANSFER_OF_PERMIT.js" type="text/javascript"></script>
    <script>

        $('input[id*=txtExistRegNo]').on('blur', function () {
            var xx = $('input[id*=txtExistRegNo]').val();
            STAServices(xx, 'txtExistPermitNo');
        });



    </script>
</asp:Content>

