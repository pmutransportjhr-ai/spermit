<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA_PERMIT_OBJECTION_RECEIPT.aspx.cs" Inherits="STA_PERMIT_OBJECTION_RECEIPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <form runat="server" autocomplete="off">
                                
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
                            <li> > 
                                Receipt
                            </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">

                                            <div class="col-md-12 col-sm-12" style="text-align: right">
                                                <%--<asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="Print" OnClientClick="return printDiv('IssuedReceiptDiv')"  />--%>
                                                <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv('IssuedReceiptDiv')">Print</button>
                                            </div>
                                        </div>
                                        <div id="IssuedReceiptDiv">
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
                                                
                                                
                                                    <div class="row">
                                                       
                                                        <div class="col-md-3 col-sm-3">
                                                     
                                                        <label for="form_control"><b>Objector's Name: </b></label>
                                                            <asp:HiddenField ID="EncryptedParameter" runat="server" />
                                        
                                                               </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblObjectorName" runat="server"></asp:Label>
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
                                                                <label for="form_control"><b>Application Number: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">

                                                                <asp:Label ID="lblObjectionNo" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                                <label for="form_control"><b>Amount to be Pay: </b></label>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                        </div>


                                                    </div>
                                                    <div class="row">

                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control"><b>Objection On Registration Number: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblOnRegistrationNo" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">

                                                            <div class="form-group">
                                                                <label for="form_control"><b>Objection On Application Number: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblOnRefrenceNo" runat="server"></asp:Label>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">


                                                        <div class="col-md-3 col-sm-3">

                                                            <div class="form-group">
                                                                <label for="form_control"><b>Objector's Reg. No. : </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblObjectorRegNo" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control"><b>Objector's Permit No. : </b></label>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblObjectorPermitNo" runat="server"></asp:Label>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">

                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control"><b>Objection Type :</b></label>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblObjType" runat="server"></asp:Label>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">

                                                            <div class="form-group">
                                                                <label for="form_control"><b>Objection Remarks : </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblObjRemarks" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                         </div>
                                            </div>
                                    </div>
                            </div></div>
                                                    <div class="row" id="PaymentDiv" runat="server">
                                                        <div class="col-md-12 col-sm-12" style="text-align: center">
                                                                    <%--<asp:Button ID="btnSave" runat="server" OnClientClick="javascript: ShoePaymentConfirmModal();" CssClass="btn green" Text="Make Payement" /></div>--%>
                                                             <input type="button" class="btn green" value="Make Payment" onclick="javascript: ShoePaymentConfirmModal();" />

                                                       
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
                                             
            </div></div>
        </div>
    </div></div>
                                            <%--OnClick="btnSave_Click" OnClientClick="return MakePayment()"--%>
                                        </form>

                                    

                               

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript">
        function printDiv(divId) {
            //document.getElementById(divId).style.backgroundImage = "url('assets/img/Untitled.png')";
            //document.body.style.backgroundImage = "url('assets/img/Untitled.png')";
            var printContents = $('#IssuedReceiptDiv').html(); //document.getElementById(divId).innerHTML;
            var originalContents = document.body.innerHTML;
            var height = $(document).height();
            height = height + 100;
            var width = $(document).width();
            width = width + 50;;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            //"<html><head><title></title></head><body><img width='" + width + "' height='" + height + "' style='z-index:-1;position:absolute' src='assets/img/Untitled.png' alt ='image' />" + printContents + "</body>";

            window.print();
            document.body.innerHTML = originalContents;


        }
    </script>
</asp:Content>

