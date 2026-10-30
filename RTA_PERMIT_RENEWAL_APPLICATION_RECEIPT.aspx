<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="RTA_PERMIT_RENEWAL_APPLICATION_RECEIPT.aspx.cs" Inherits="RTA_PERMIT_RENEWAL_APPLICATION_RECEIPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
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
                                                   <asp:HiddenField ID="EncryptedParameter" runat="server" />
                                                    <div class="row">
                                                         <div class="col-md-12 col-sm-12" style="text-align:center" >
                                                              <asp:PlaceHolder ID="plBarCode" runat="server" /></div>
                                                        </div>
                                                    <div class="row">
                                                        <div class="col-md-6 col-sm-6">
                                                                <b style="font-size:large">Application No :</b>
                                                                    <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                            </div>
                                                         <div class="col-md-6 col-sm-6"  style="text-align:right">
                                                                <b style="font-size:large">Application Status :</b>
                                                                    <asp:Label ID="lblApplicationStatus" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    <div class="row">
                                                                        <div class="col-md-6 col-sm-6">
                                                                    <b  style="font-size:large">Permit Type : </b>
                                                                        <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                            </div>
                                                        <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                    <b  style="font-size:large">Permit No : </b>
                                                                        <asp:Label ID="LblPermitNo" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                            </div>
                                                        </div></br>
                                                    <div class="row">
                                                        <div class="col-md-6 col-sm-6">
                                                           
                                                                   <b>Name : </b>
                                                                    
                                                                        <asp:Label ID="VOName" runat="server"></asp:Label>
                                                            </div>
                                                     <div class="col-md-6 col-sm-6">
                                                                     <b>Registration No : </b>
                                                                    
                                                                        <asp:Label ID="VORegNumber" runat="server"></asp:Label>

                                                               

                                                        </div>
                                                   </div>
                                                     <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Fee Details</b></u></h4></div>
                                                        <div class="col-md-12 col-sm-12"><b>Fees : </b>
                                                            <asp:Label ID="lblPermitFees" runat="server"></asp:Label></div>
                                                        <div class="col-md-12 col-sm-12"><b>Payment Status: </b> 
                                                            <asp:Label ID="lblPaymentStatus" runat="server"></asp:Label></div>
                                                     </div><br /><br />
                                                     <div class="row">
                                                          <div class="col-md-12 col-sm-12" style="font-size:14px;">
                                                        <b>(The State/Reginoal Transport Authority)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <span>JHARKHAND</span></b>
                                                    </div>
                                                         
                                                    </div>
                                                  
                                                    
                                                    
                                                     
                                                        </div>
                                                           
                                                         

                                                    
                                                </div>
                                            </div>
                                    
                            </div>

                                                     </div>          
                                                  

                                                      <div class="row" id="PaymentDiv" runat="server">
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

