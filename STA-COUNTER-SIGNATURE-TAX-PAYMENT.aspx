<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA-COUNTER-SIGNATURE-TAX-PAYMENT.aspx.cs" Inherits="STA_COUNTER_SIGNATURE_TAX_PAYMENT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-COUNTER-SIGNATURE-TAX-PAYMENT.aspx" style="display: none;" />
    <%--<input type="text" id="txtColCnt" style="display: none;" value="8" />--%>

    <form runat="server" autocomplete="off">
        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:HiddenField ID="tblType" runat="server" />
            
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />



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
                                <span>STA Inter State Counter Signature Tax Payment</span>
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
                                                        <span class="caption-subject font-red sbold uppercase">STA Inter State Counter Signature Tax Payment</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:HiddenField ID="lblMenufecturingDt" runat="server" />
                                                            <asp:HiddenField ID="lblQuterlyTaxAmount" runat="server" />
                                                            <asp:HiddenField ID="AppIDAXAX" runat="server" />
                                                            <asp:HiddenField ID="EncryptedParameter" runat="server" />
                                                            <asp:HiddenField ID="txtAPP_ID" runat="server" />
                                                            </div>
                                                        </div>
                                                    <div class="row">
                                                        <div class="col-md-3"><b>Application Number :</b></div>
                                                            <div class="col-md-9"><asp:Label Font-Weight="True" ID="lblApplicationNumber" runat="server"></asp:Label></div>

                                                            </div><br />
                                                            <div class="row">
                                                        <div class="col-md-3"><b>Applicant Name :</b></div>
                                                            <div class="col-md-9"><asp:Label Font-Weight="True" ID="lblApplicantName" runat="server"></asp:Label>
                                                            </div></div><br />
                                                            <div class="row">
                                                        <div class="col-md-3"><b>Registration Number :</b></div>
                                                            <div class="col-md-9"><asp:Label Font-Weight="True" ID="txtRegNo" runat="server"></asp:Label>
                                                            </div></div><br />
                                                            <div class="row">
                                                        <div class="col-md-3"><b>Tax Period :</b></div>
                                                            <div class="col-md-3"><asp:DropDownList ID="ddlTaxPeriod"  runat="server" OnSelectedIndexChanged="ddlTaxPeriod_SelectedIndexChanged" CssClass="form-control select2" AutoPostBack="true">
                                                                    <asp:ListItem Value="0">--Select Period--</asp:ListItem>
                                                                    <asp:ListItem Value="q">Quarterly</asp:ListItem>
                                                                <%--    <asp:ListItem Value="h">Half Yearly</asp:ListItem>--%>
                                                                    <asp:ListItem Value="y">Yearly</asp:ListItem>
                                                                </asp:DropDownList>

                                                            </div>
                                                                <div class="col-md-6"></div>
                                                            </div><br />
                                                            <div class="row">
                                                        <div class="col-md-3"><b>Actual Tax Amount :</b></div>
                                                            <div class="col-md-9"><asp:Label Font-Weight="True" ID="Label1" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label3" runat="server" Font-Weight="True"></asp:Label>
                                                                </div></div><br />
                                                            <div class="row"><div class="col-md-3"><b>Fine :</b></div>
                                                            <div class="col-md-9"><asp:Label Font-Weight="True" ID="Label2" runat="server"></asp:Label></div></div><br />

                                                            <div class="row">
                                                        <div class="col-md-3"><b>Total Tax Amount to be Pay:</b></div>
                                                            <div class="col-md-9"><asp:Label Font-Weight="True" ID="TaxAmount" runat="server"></asp:Label></div>
                                                        </div>
                                                        
                                                    

                                                    <div class="row">
                                                        <br />
                                                        <br />
                                                        <div class="col-md-12" style="text-align:center">
                                                            <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Pay" OnClick="btnSave_Click" />
                                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Back" OnClick="btnCancel_Click" />
                                                        </div>
                                                        
                                                    </div>


                                                </div>
                                            </div>
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
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
  

</asp:Content>

