<%@ Page Title="" Language="C#" MasterPageFile="~/RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-GOODS-PERMIT-ISSUED-RECEIPT-GLOBAL.aspx.cs" Inherits="RTA_GOODS_PERMIT_ISSUED_RECEIPT_GLOBAL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style type="text/css">
        .fbold {
            font-weight: 600;
            font-family: "Open Sans",sans-serif;
            font-size: large;
        }

            .fbold li {
                padding: 10px 0;
            }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="RTA-DASHBOARD.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>Goods Carriage Permit - Regional
                            <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>Receipt</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">


                                    <div class="portlet light">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12" style="text-align: right">
                                                <%--<asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="Print" OnClientClick="return printDiv('IssuedReceiptDiv')"  />--%>
                                                <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv(IssuedReceiptDiv)">Print</button>

                                            </div>

                                        </div>
                                        <div id="IssuedReceiptDiv">
                                            <div class="portlet-title" style="border-bottom: 0px solid #eee;">

                                                <div class="col-md-12 col-sm-12" style="text-align: center">
                                                    <div class="caption caption-md">
                                                        <span class="caption-subject font-black bold" style="font-size: 30px">GOVERNMENT OF JHARKHAND</span>
                                                        <br />
                                                        <span class="caption-subject font-black bold" style="font-size: 20px">STAGE CARRIAGE PERMIT</span>
                                                        <br />
                                                        <span class="caption-subject font-black" style="font-size: 16px; font-weight: 500;">Form P.St. S.</span>
                                                        <br />
                                                        <span class="caption-subject font-black" style="font-size: 16px; font-weight: 500;">[See rule 73 (7)(l)]</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="portlet-body fbold">
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        <b>The State/Regional Transport Authoritiy
                                                <br />
                                                            ........................................................Region</b>
                                                    </div>
                                                    <br />
                                                    <div class="col-md-4">Permit No.-
                                                        <asp:label font-bold="true" id="lblPermitNo" runat="server" text=""></asp:label>
                                                    </div>
                                                </div>
                                                

                                                </div>
                                                
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12" style="text-align: center">
                                                        <span class="caption-subject font-black bold" style="font-size: 30px"><label id="lblHdr" runat="server"></label></span>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h3><b><label id="lblmsg" runat="server"></label></b></h3> <%--This permit is herby renewed up to ............... with effect from ............... --%>
                                                
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        Date : 
                                                
                                                    </div>
                                                    <div class="col-md-12">
                                                        Place : 
                                                
                                                    </div>
                                                    <div class="col-md-12">
                                                        Seal : 
                                                
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        The State/Reginoal Transport Authority<br />
                                                        .............................................State/Region.
                                                    </div>
                                                    <div class="col-md-6"></div>
                                                    <div class="col-md-6" style="text-align: center">
                                                        Signature and Designation of
                                                        <br />
                                                        authorised signatory
                                                
                                                    </div>
                                                </div>
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
    <script type="text/javascript">


        function printDiv(divId) {
            //document.getElementById(divId).style.backgroundImage = "url('assets/img/Untitled.png')";
            //document.body.style.backgroundImage = "url('assets/img/Untitled.png')";

            var printContents = $('#IssuedReceiptDiv').html(); //document.getElementById(divId).innerHTML;
            var originalContents = document.body.innerHTML;
            var height = $(document).height();
            height = height + 100;
            var width = $(document).width();
            width = width + 50;
            document.body.innerHTML = "<html><head><title></title></head><body><img width='" + width + "' height='" + height + "' style='z-index:-1;position:absolute' src='assets/img/log-watermark.png' alt ='image' />" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;






        }
    </script>
    
    <%--<script src="assets/js/STA_PERMIT.js" type="text/javascript"></script>--%>
</asp:Content>

