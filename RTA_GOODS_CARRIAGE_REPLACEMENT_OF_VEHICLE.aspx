<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="RTA_GOODS_CARRIAGE_REPLACEMENT_OF_VEHICLE.aspx.cs" Inherits="RTA_GOODS_CARRIAGE_REPLACEMENT_OF_VEHICLE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
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
                                <span>Goods Carriage Replacement of Vehicle</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">Goods Carriage Replacement of Vehicle</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <asp:UpdatePanel ID="UPpanel" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label"><b>Reg. No. : </b></label><span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtExistRegNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <label class="control-label"><b>Permit No. : </b></label><span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <asp:TextBox ID="txtExistPermitNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:Button ID="btnGO" runat="server" CssClass="btn green" OnClick="btnGO_Click" OnClientClick="return fnSTAReplacementValidate()" Text="GO" />
                                                            &nbsp;&nbsp;<asp:Button ID="btnExistClear" runat="server" CssClass="btn default" OnClick="btnExistClear_Click" Text="Clear" />
                                                            &nbsp;<asp:Button ToolTip="Cancel" ID="btnCancel" runat="server" CssClass="btn red" Text="Cancel" OnClick="btnCancel_Click" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <asp:Label CssClass="col-md-12" Font-Bold="True" ID="Message" runat="server" Visible="False"></asp:Label>
                                                    </div>
                                                    <br />
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
                                                                            <td><b>Permit Number : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblPermitNo" runat="server"></asp:Label>

                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Name : </b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblExistName" runat="server"></asp:Label></td>
                                                                            <td><b><b>C/O : </b></b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblVOCareOf" runat="server"></asp:Label></td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td><b>Present Address :</b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblVOPresentAddress" runat="server"></asp:Label><span id=""></span> </td>
                                                                            <td><b>Permanent Address :</b></td>
                                                                            <td>
                                                                                <asp:Label ID="lblVOPermanentAddress" runat="server"></asp:Label></td>

                                                                        </tr>


                                                                    </table>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Vehicle Details
                                                                </div>
                                                                <%--<div class="tools">
                                                                    <a href="javascript:;" class="expand"> </a>
                                                                </div>--%>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <tr>


                                                                                <td><b>Registration No :</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblVORegNumber" runat="server"></asp:Label></td>
                                                                                <td></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Chassis Number :</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblVChassisNumber" runat="server"></asp:Label></td>
                                                                                <td><b>Engine Number :</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblVEngineNumber" runat="server"></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Tax Paid UpTo :</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblVTax" runat="server"></asp:Label></td>
                                                                                <td><b>Fitness Valid Upto :</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblVFitness" runat="server"></asp:Label></td>

                                                                            </tr>
                                                                            <tr>

                                                                                <td><b>Vehicle Class Type :</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblVType" runat="server"></asp:Label></td>
                                                                                <td><b>Insurance Company :</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblVInsuranceCmp" runat="server"></asp:Label></td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Insurance Policy No :</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblVInsuranceNo" runat="server"></asp:Label></td>

                                                                                <td><b>Insurance Upto :</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblVInsuranceUpto" runat="server"></asp:Label></td>
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
                                                                <%--<div class="tools">
                                                                    <a href="javascript:;" class="expand"> </a>
                                                                </div>--%>
                                                            </div>

                                                            <div class="portlet-body">
                                                                <br />
                                                                <div class="row">
                                                                    <div class="col-md-12 col-sm-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <tr>
                                                                                <td><b>Mobile No: </b></td>

                                                                                <td>
                                                                                    <asp:Label ID="VOMobileNo" runat="server"></asp:Label><span id=""></span> </td>
                                                                                <td><b>RTO Name: </b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lbRTOName" runat="server"></asp:Label>
                                                                                    <asp:HiddenField ID="lblRTOCode" runat="server"></asp:HiddenField>
                                                                                </td>

                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Unload weight(Kgs)</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblUnloadWeight" runat="server"></asp:Label></td>
                                                                                <td><b>Gross Vehicle weight(Kgs)</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblGrossvehicleWeight" runat="server"></asp:Label></td>


                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Pay load(Kgs)</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblPayLoad" runat="server"></asp:Label></td>
                                                                                <td><b>Hire purchaser /lease /hypothecation agreement with</b></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAgreementWith" runat="server"></asp:Label></td>


                                                                            </tr>

                                                                        </table>
                                                                    </div>


                                                                </div>
                                                                <br />

                                                            </div>
                                                        </div>
                                                        <br />





                                                        <div class="row">

                                                            <div class="col-md-1 col-sm-1">
                                                                <asp:HiddenField ID="txtVehicleJson" runat="server" />
                                                            </div>
                                                            <div class="col-md-3 col-sm-3">
                                                                <div class="form-group">
                                                                    <label for="form_control"><b>Enter Registration Number: </b></label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4 col-sm-4">
                                                                <div class="form-group">
                                                                    <asp:TextBox ID="txtVehicleNumber" CssClass="regtxtnumber form-control" runat="server"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                            <div class="col-md-1 col-sm-1"></div>
                                                            <div class="col-md-3 col-sm-3">
                                                                <div class="form-group">

                                                                    <asp:Button ToolTip="Search" ID="btnSearch" runat="server" CssClass="btn green" Text="Search" OnClientClick="return fnSearchValidate()" OnClick="btnSearch_Click" />
                                                                    &nbsp;<asp:Button ToolTip="Clear" ID="btnClear" runat="server" CssClass="btn default" Text="Clear" OnClick="btnClear_Click" />

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
                                                                            <a href="javascript:;" class="expand"></a>
                                                                        </div>
                                                                    </div>
                                                                    <div class="portlet-body portlet-collapsed">
                                                                        <div class="row">
                                                                            <div class="col-md-12 col-sm-12 col-xs-12">
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
                                                                    </div>
                                                                </div>
                                                                <div class="portlet box green">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">
                                                                            Vehicle Details
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a href="javascript:;" class="expand"></a>
                                                                        </div>
                                                                    </div>
                                                                    <div class="portlet-body portlet-collapsed">
                                                                        <div class="row">
                                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                                <table class="table table-striped table-bordered table-hover">
                                                                                    <tr>


                                                                                        <td><b>Registration No : </b></td>
                                                                                        <td>
                                                                                            <asp:Label ID="VORegNumber" runat="server"></asp:Label></td>
                                                                                        <td></td>
                                                                                        <td></td>
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
                                                                                        <td><b>Tax Paid UpTo: </b></td>
                                                                                        <td>
                                                                                            <asp:Label ID="VTax" runat="server"></asp:Label></td>
                                                                                        <td><b>Fitness Valid Upto: </b></td>
                                                                                        <td>
                                                                                            <asp:Label ID="VFitness" runat="server"></asp:Label></td>

                                                                                    </tr>
                                                                                    <tr>

                                                                                        <td><b>Vehicle Class Type:</b></td>
                                                                                        <td>
                                                                                            <asp:Label ID="VType" runat="server"></asp:Label></td>
                                                                                        <td><b>Insurance Company: </b></td>
                                                                                        <td>
                                                                                            <asp:Label ID="VInsuranceCmp" runat="server"></asp:Label></td>

                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td><b>Insurance Policy No: </b></td>
                                                                                        <td>
                                                                                            <asp:Label ID="VInsuranceNo" runat="server"></asp:Label></td>

                                                                                        <td><b>Insurance Upto: </b></td>
                                                                                        <td>
                                                                                            <asp:Label ID="VInsuranceUpto" runat="server"></asp:Label></td>
                                                                                    </tr>
                                                                                </table>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Other Information
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="expand"> </a>
                                                                </div>
                                                            </div>
                                                          
                                                            <div class="portlet-body  portlet-collapsed">
                                                            <br />
                                                                <table class="table table-striped table-bordered table-hover" style="align-content:center">

                                                                    <tr>
                                                                        <td><b>
                                                                            <label>Unload weight(Kgs): <span class="required" aria-required="true">*</span></label></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtUnloadWeight" CssClass="numberWithdot form-control" runat="server"></asp:TextBox></td>
                                                                        <td><b>
                                                                            <label>Gross Vehicle weight(Kgs): <span class="required" aria-required="true">*</span></label></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtGrossvehicleWeight" CssClass="numberWithdot form-control" runat="server"></asp:TextBox></td>


                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>
                                                                            <label>Pay load(Kgs): <span class="required" aria-required="true">*</span></label></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPayLoad" CssClass="numberWithdot form-control" runat="server"></asp:TextBox></td>
                                                                        <td><b>
                                                                            <label>Hire purchaser /lease /hypothecation agreement with:  <span class="required" aria-required="true">*</span></label></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAgreementWith" CssClass="regtxt form-control" runat="server"></asp:TextBox></td>


                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>
                                                                            <label>Enter Mobile No <span class="required" aria-required="true">*</span></label></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMobileNo" CssClass="number form-control" runat="server"></asp:TextBox></td>
                                                                        <td></td>
                                                                        <td></td>

                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                                <div class="portlet box green">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">
                                                                            Upload Documents
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a href="javascript:;" class="expand"></a>
                                                                        </div>
                                                                    </div>

                                                                    <div class="portlet-body portlet-collapsed">
                                                                        <br />
                                                                        <div class="row" id="Div3" runat="server">
                                                                        </div>
                                                                        <div id="Div4" runat="server"></div>
                                                                        <div id="uploadDocuments">
                                                                            <div class="row">
                                                                                <div class="col-md-6 col-sm-6 col-xs-6">
                                                                                    <table class="table table-striped table-bordered table-hover">
                                                                                        <tr>
                                                                                            <td>Registration Certificate (R/C)  </td>
                                                                                            <td>
                                                                                                <asp:FileUpload runat="server" accept="application/pdf, .pdf" ID="uploadRC"></asp:FileUpload></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Insurance Certificate</td>
                                                                                            <td>
                                                                                                <asp:FileUpload runat="server" accept="application/pdf, .pdf" ID="uploadInsur"></asp:FileUpload></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Pollution Certificate </td>
                                                                                            <td>
                                                                                                <asp:FileUpload runat="server" accept="application/pdf, .pdf" ID="uploadPollution"></asp:FileUpload></td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>Fitness Certificate</td>
                                                                                            <td>
                                                                                                <asp:FileUpload runat="server" accept="application/pdf, .pdf" ID="uploadFitness"></asp:FileUpload></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>Permit Certificate</td>
                                                                                            <td>
                                                                                                <asp:FileUpload runat="server" accept="application/pdf, .pdf" ID="uploadPermit"></asp:FileUpload></td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </div>
                                                                            </div>


                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                <div class="portlet box green">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">
                                                                            Replacement of Vehicle Fee Details
                                                                        </div>
                                                                        <div class="tools">
                                                                            <a href="javascript:;" class="expand"></a>
                                                                        </div>
                                                                    </div>
                                                                    <div class="portlet-body portlet-collapsed">
                                                                        <div class="row">
                                                                            <div class="col-md-3 col-sm-3"><b>Replacement of Vehicle Fees :</b></div>
                                                                            <div class="col-md-3 col-sm-3">
                                                                                <asp:Label ID="lblReplacevehicleFees" runat="server"></asp:Label>
                                                                            </div>
                                                                            <div class="col-md-3 col-sm-3"></div>
                                                                            <div class="col-md-3 col-sm-3"></div>
                                                                        </div>


                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-4 col-sm-4"></div>
                                                                    <div class="col-md-4 col-sm-4">
                                                                        <div style="text-align: center">
                                                                            <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Save Application" OnClick="btnSave_Click" OnClientClick="return fnRVApplicationValidate()" />
                                                                        </div>

                                                                    </div>
                                                                    <div class="col-md-4 col-sm-4">
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
    <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="assets/js/RTA_GOODS_CARRIAGE_REPLACEMENT_OF_VEHICLE.js" type="text/javascript"></script>
</asp:Content>

