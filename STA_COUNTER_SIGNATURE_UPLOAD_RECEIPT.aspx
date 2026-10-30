<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA_COUNTER_SIGNATURE_UPLOAD_RECEIPT.aspx.cs" Inherits="STA_COUNTER_SIGNATURE_UPLOAD_RECEIPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<form id="FormID1" enctype="multipart/form-data" runat="server" class="form-horizontal" autocomplete="off" method="post">
    <asp:Label ID="HiddenField1" runat="server" Visible="false" />
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering ="true" />
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
                            <span>Counter Signature Upload Receipt</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">Counter Signature Upload Receipt</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <div class="row">
                                            <div class ="col-md-6">
                                             <span class="form-fit">Application Number:</span>
                                                </div>
                                            <div class ="col-md-6">
                                                <asp:Label ID="lblApplicationNumber" runat="server"></asp:Label>    
                                            </div>
                                        </div>
                                        <br />
                                       
                                         <div class="row">
                                            <div class ="col-md-6">
                                             <span class="form-fit">Registration Number:</span>
                                            </div>
                                            <div class ="col-md-6">
                                                <asp:Label ID="lblRegistrationNo" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <br />
                                         <div class="row">
                                            <div class ="col-md-6">
                                              <span class="form-fit">Permit Number:</span>
                                            </div>
                                            <div class ="col-md-6">
                                                <asp:Label ID="lblPermitNo" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                       <br />
                                        <table class="table responsive">
                                            <tr>
                                                <td>Counter Signature Copy :</td>
                                                <td> <div id="csCopy" runat="server"></div></td>
                                                <td>Approved Time Table Copy :</td>
                                                <td><div id="ApprovedTimeTableCopy" runat="server"></div></td>
                                            </tr>
                                             <tr>
                                                <td>Registration Certificate :</td>
                                                <td><div id="dvRC" runat="server"></div></td>
                                                <td>Speed Governer Certificate :</td>
                                                <td><div id="dvSpeed" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td>Insureance Certificate :</td>
                                                <td><div id="dvInssurance" runat="server"></div></td>
                                                <td>Pollution Certificate :</td>
                                                <td><div id="dvPollution" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td>Fitness Certificate :</td>
                                                <td><div id="dvFitness" runat="server"></div></td>
                                                <td>Permit Copy :</td>
                                                <td><div id="dvPermit" runat="server"></div></td>
                                            </tr>
                                            
                                            <tr>
                                                <td>Temporary Time Table :</td>
                                                <td><div id="dvTempoTimeTable" runat="server"></div></td>
                                                <td>Jharkhand Tax Paid Upto Certificate</td>
                                                <td><div id="dvJHTTaxPaidUptoCertificate" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td>Insurance Certificate Valid Upto :</td>
                                                <td><asp:Label ID ="lblICValidity" runat="server"></asp:Label></td>
                                                <td>Pollution Certificate Valid Upto :</td>
                                                <td><asp:Label ID="lblPollutionValidity" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Jharkhand Tax Token :</td>
                                                <td><asp:Label ID ="lblJharkhandTaxToken" runat="server"></asp:Label></td>
                                            
                                                <td>Fitness Certificate Valid Upto :</td>
                                                <td><asp:Label ID ="lblFitness" runat="server"></asp:Label></td></tr>
                                        </table>
                                          <asp:HiddenField ID="EncryptedParameter" runat="server"></asp:HiddenField>
                                          <asp:HiddenField ID="hdnAppID" runat="server"></asp:HiddenField>

                                    </div>

                                </div>
                            </div>
                        </div>
                        
    <div class="row" id="PaymentDiv" runat="server" visible="true">
                                                                <div class="col-md-4 col-sm-4"></div>
                                                        <div class="col-md-4 col-sm-4">
                                                            <div style="text-align: center">
                                                                <%--<asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="btn green" Text="Make Payement" /></div>--%>
                                                                <asp:Button ID="btnWithDraw" Text="Withdraw Application" CssClass =" btn btn-warning" runat="server" OnClick="btnWithDraw_Click" />
                                                                <asp:Button ID="btnClose" Text ="Close" CssClass ="= btn btn-info" OnClick="btnClose_Click" runat="server" />

                                                        </div>
                                                        <div class="col-md-4 col-sm-4"></div>
                                                    </div>
                                                </div>
    <div class="modal fade" id="confirm" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                                                <h4 class="modal-title"><b>Are you sure, you want to pay the Amount?</b></h4>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn dark btn-outline" data-dismiss="modal">NO</button>
                                                                <asp:Button ID="btnYes" runat="server" CssClass="btn green" Text="YES" OnClick="btnYes_Click" />
                                                            </div>
                                                        </div>
                                                        <!-- /.modal-content -->
                                                    </div>
                                                    <!-- /.modal-dialog -->
                                                </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/plugins/moment.min.js" type="text/javascript"></script>
</asp:Content>

