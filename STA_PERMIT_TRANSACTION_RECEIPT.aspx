<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA_PERMIT_TRANSACTION_RECEIPT.aspx.cs" Inherits="STA_PERMIT_TRANSACTION_RECEIPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        @font-face {
            font-family: kruti dev;
            src: url( 'assets/Fonts/Shusha.ttf' ) format("truetype");
        }

        .hinditext {
            font-family: kruti dev;
            font-size: 18px;
        }
    </style>
    <style type="text/css">
        .fbold {
            font-weight: 600;
            font-family: "Open Sans",sans-serif;
            font-size: large;
        }

            .fbold li {
                padding: 10px 0;
            }

        .screenbg {
            background-image: url('assets/img/Untitled.png');
        }

        img.screenbg {
            pointer-events: none;
            position: absolute;
            /*width: 100%;
    height:100%;*/
            z-index: -1;
        }
    </style>
    <%--<link rel=\"stylesheet\" href=\"style.css\" type=\"text/css\" media=\"print\"/>--%>
    <%--<link rel="stylesheet"  href="assets/css/screen.css" type="text/css" media="screen, print" />
<link rel="stylesheet"  href="assets/css/print.css" type="text/css" media="print" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="PermitHomePage.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>STA Permit Transaction Receipt
                            <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>Receipt</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div id="IssuedReceiptDiv">
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                         <asp:PlaceHolder ID="plBarCode" runat="server" /></div>
                                    </div>
                                </div>



                            <div class="row">
                                <div style="text-align:center"><b>Your Application ( Application ID: <label id="lblapnumber" runat="server"></label>) is successfully submited for further proceeding and verification.<br />Status Can be checked through Application ID/Number</b></div>
                            </div>
                            <div class="row">
                                    <div class="col-md-6"><b>Service: </b> <label id="servicename" runat="server"></label></div>
                                    <div class="col-md-6"><b>Date: </b> <label id="trndate" runat="server"></label></div>
                                </div><br />
                            <div class="row">
                                      
                                 <div class="col-md-6"><b>Transaction Number: </b> <label id="trnno" runat="server"></label></div>
                                <div class="col-md-6"><b>GRN: <label id="txtGRN" runat="server"></label>  </b></div>
                                </div><br />
                            <div class="row">
                                     <div class="col-md-6"><b>Amount: </b> <label id="Amount" runat="server"></label></div>
                                   <div class="col-md-6"><b>Payment Status: </b> Success</div>
                                    
                                </div>
                                </div>
                            </div><br /><br />
                             <div class="row">
                                 
                                     <div class="col-md-12" style="text-align:center">
                                         <button id="btnPrint" type="button" class="btn-green" onclick="printDiv('IssuedReceiptDiv')">Print</button>
                                         &nbsp;&nbsp;&nbsp;&nbsp;<a class='btn btn-default' href='PermitHomePage.aspx' >OK</a>
                                         
                                
                             </div>
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

    <%--<script src="assets/js/STA_PERMIT.js" type="text/javascript"></script>--%>
</asp:Content>

