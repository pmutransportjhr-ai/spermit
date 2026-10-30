<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA_TIME_TABLE_EXTENSION_RECEIPT.aspx.cs" Inherits="STA_TIME_TABLE_EXTENSION_RECEIPT" %>

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
                                            <span class="caption-subject font-red sbold uppercase" id="PageHeading1" runat="server">Time table Extension receipt</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                                
                                                    <div class="row">
                                                        <div class="col-md-12 text-center">
                                                             <asp:PlaceHolder ID="plBarCode" runat="server" /></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                       
                                                        <div class="col-md-3 col-sm-3">
                                                     
                                                        <label for="form_control"><b>Application Number: </b></label>
                                                           
                                        
                                                               </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblApplicationNumber" runat="server"></asp:Label>
                                                        </div>
                                                         <div class="col-md-3 col-sm-3">

                                                            <div class="form-group">
                                                                <label for="form_control"><b>Registration Number: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <asp:Label ID="lblRegistrationNumber" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control"><b>Permit Number: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">

                                                                <asp:Label ID="lblPermitNumber" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                                <label for="form_control"><b>Amount Paid: </b></label>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                        </div>


                                                    </div>
                                                    <div class="row">

                                                        <div class="col-md-3 col-sm-3">
                                                            <div class="form-group">
                                                                <label for="form_control"><b>Payment Transaction Ref. Number: </b></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3 col-sm-3">
                                                           <asp:Label ID="lblPaymentTransRefNo" runat="server"></asp:Label>
                                                        </div>
                                                        </div>
                                                       
                                                  
                                         </div>
                                            </div>
                                    </div>
                            </div></div>
                                                    
                                            
                                             
            </div></div>
        </div>
    </div></div>
                                          
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

