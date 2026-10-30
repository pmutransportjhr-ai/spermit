<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="RTA_PERMIT_RECEIPT.aspx.cs" Inherits="RTA_PERMIT_RECEIPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-container">
        <div class="page-content-wrapper">
            <div class="page-content">
                <div class="container">
                    <ul class="page-breadcrumb breadcrumb">
                        <li>
                            <a href="PermitHomePage.aspx">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>Stage Carriage Permit  - Regional
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Receipt</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">Stage Carriage Permit  - Regional (Receipt)</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                        <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
                                            <div class="form-group" id="ReceiptDiv" runat="server">
                                                <asp:HiddenField ID="EncryptedParameter" runat="server" />
                                                <div class="col-md-1"></div>
                                                <div class="col-md-10">
                                                    <div class="row">
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control"><b>Application Number: </b></label>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">

                                                                <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">

                                                            <div class="form-group">
                                                                <label for="form_control"><b>Enrollment On: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblEnrollmentOn" runat="server"></asp:Label>
                                                            </div>
                                                        </div>


                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-3 col-sm-3">

                                                            <div class="form-group">
                                                                <label for="form_control"><b>Owner Name: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblOwnerName" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control"><b>Registration Number: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblRegistrationNo" runat="server"></asp:Label>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">

                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control"><b>C/O: </b></label>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblCareOf" runat="server"></asp:Label>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">

                                                            <div class="form-group">
                                                                <label for="form_control"><b>Owner Permanent Address: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control"><b>Chasis Number: </b></label>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblChasisNumber" runat="server"></asp:Label>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">

                                                            <div class="form-group">
                                                                <label for="form_control"><b>Engine Number: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblEngineNumber" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control"><b>Permit Fees: </b></label>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblPermitFee" runat="server"></asp:Label>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4"></div>
                                                        <div class="col-md-4 col-sm-4">
                                                            <div style="text-align: center">
                                                                <%--<asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="btn green" Text="Make Payement" /></div>--%>
                                                                <input type="button" class="btn green" value="Make Payment" onclick="javascript: ShoePaymentConfirmModal();" />

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
                                                <div class="col-md-1"></div>
                                             </div>

                                            <%--OnClick="btnSave_Click" OnClientClick="return MakePayment()"--%>
                                        </form>

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    
</asp:Content>

