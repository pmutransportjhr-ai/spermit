      <%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="QR_STA_CS_RECEIPT.aspx.cs" 
    Inherits="QR_STA_CS_RECEIPT" %>

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
                            <li>STA Permit Counter Signature Transaction Receipt
                            <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>Receipt</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">


                                    <div class="portlet light" style="background-image: url('assets/img/Untitled.png')">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12" style="text-align: right">
                                                <%--<asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="Print" OnClientClick="return printDiv('IssuedReceiptDiv')"  />--%>
                                                <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv('IssuedReceiptDiv')">Print</button>
                                            </div>
                                        </div>
                                        <div id="IssuedReceiptDiv">
                                            <div class="portlet-title" style="border-bottom: 0px solid #eee;">
                                              
                                            </div>
                                            <div class="portlet-body fbold">
                                                 <div class="row">
                                    <div class="col-md-12 text-left">
                                         <asp:PlaceHolder ID="plBarCode" runat="server" /></div>
                                    </div>
                                                <div class="row">
                                                    <div class="col-md-4" style="text-align: center;">
                                                       <%-- <p class="hinditext">JaarKMD sarkar</p>--%>
                                                    </div>
                                                    <div class="col-md-4" style="text-align: center;">
                                                        <p class="hinditext">JaarKMD sarkar</p>
                                                        <table style="width: 100%">
                                                           
                                                            <tr style="width: 100%">
                                                                <td colspan="2" style="text-align: center;">(<label class="hinditext"> Vara 5</label>(1,2)&nbsp;<label class="hinditext">AaOr inayama 6 </label>
                                                                    (1)
                                                                    <label class="hinditext">doKo</label>)</td>
                                                            </tr>
                                                            <tr style="width: 100%">
                                                                <td colspan="2" style="text-align: center;">
                                                                    <label class="hinditext">kr yaa AitirM@t kr kI rsaId</label></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="col-md-4" style="text-align: center;">
                                                        <%--<p class="hinditext">JaarKMD sarkar</p>--%>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                     
                                                    <div class="col-md-8">
                                                        
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                    <label class="hinditext">EaI</label>&nbsp;<label class="bold" id="lblApplicantName" style="font-size:14px" runat="server"></label>&nbsp;
                                                                <label class="hinditext">sao  ]nakI</label>
                                                                    <label class="hinditext">maaoTrgaaDI ko ijasakI roijasT`IkRt saMKa </label>
                                                                    <label  class="bold" id="lblRegNo" style="font-size:14px" runat="server"></label>
                                                                    <label class="hinditext">hO</label>
                                                                    <label class="hinditext">kr ko $p mao</label>&nbsp;<label  class="bold" id="lblTrID" runat="server">
                                                                     </label>&nbsp;<label class="hinditext">$</label>&nbsp;<label  class="bold" id="lblTaxAmount" style="font-size:14px;" runat="server"></label>/-&nbsp;
                                                               Online
                                                                <label class="hinditext">ko Wara p`aPt</label>
                                                                    <label class="hinditext">ikyao.</label>
                                                                
                                                                <table style="width: 100%">
                                                                    <tr style="width: 100%">
                                                                        <td style="width: 20%">
                                                                            <label class="small">Transaction Number: </label><label id="lblTransNumber" runat="server"> </label>
                                                                        </td>
                                                                        <td style="width: 20%"></td>
                                                                        
                                                                    </tr>
                                                                    <tr style="width: 100%">
                                                                        <td style="width: 20%">
                                                                            <label class="small">GRN:</label><label id="lblgrnNumber" runat="server"> </label>

                                                                        </td>
                                                                        <td style="width: 20%"></td>
                                                                        
                                                                    </tr>
                                                                </table>
                                                                <p>
                                                                    <label class="hinditext">tarIK</label> : <label id="lblDated" runat="server"></label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">sqaana</label>: Jharkhand
                                                                </p>
                                                                <p>
                                                                    <label class="small" >Tax Paid Period: </label><label id="lblTaxperiod" runat="server"></label>
                                                                </p>
                                                              <%--  <br />--%>
                                                                <%--<p>
                                                                    <label style="font-weight: bold;" class="hinditext">hstaxar</label>
                                                                </p>--%>


                                                            </div>
                                                        </div>
                                                    </div>
                                                    </div>
                                                <div class="row">
                                                    
                                                    <div class="col-md-8">
                                                        <b>
                                                        <span style="text-align: center">
                                                            <label class="hinditext">kr p`tIk</label>(<label class="hinditext">TO@sa Taokna</label>)</span>
                                                        <span style="text-align: center">(<label class="hinditext">JaarKMD maaoTrgaaDI kraraopna AiVinayama 2001 ko AVIna</label>)</span>
                                                            </b>
                                                       <p>
                                                        <span style="text-align: left">
                                                            <label class="hinditext">maok  </label>: <label id="lblMake" runat="server"></label></span>  </p>
                                                        <p> <span style="text-align: left">
                                                            <label class="hinditext">[Mijana saMKa </label>: <label id="lblEngineNumber" runat="server"></label></span>  </p>
                                                        <p> <span style="text-align: left">
                                                            <label class="hinditext">caoisasa saMKa  </label>: <label id="lblChaiseNo" runat="server"></label></span>  </p>
                                                        
                                                         <p><span style="text-align: left">
                                                            <label class="hinditext">rija saMKa  </label>: <label id="lblRegisNo" runat="server"></label></span>  </p>
                                                         <p><span style="text-align: left">
                                                            <label class="hinditext">samaaiPt kI tarIK  </label>: <label id="lblPaidUpto" runat="server"></label></span>  </p>
                                                         <p><span style="text-align: left">
                                                            <label class="hinditext">kraraopna AiVkarI  </label>: <label id="Label13" runat="server"></label></span>  </p>
                                                        <p> <span style="text-align: left">
                                                            <label class="hinditext">ijalaa  </label>: Jharkhand</span>  </p>
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
            document.body.innerHTML = "<html><head><title></title></head><body><img width='" + width + "' height='" + height + "' style='z-index:-1;position:absolute' src='assets/img/Untitled.png' alt ='image' />" + printContents + "</body>";

            window.print();
            document.body.innerHTML = originalContents;


        }
    </script>
    <%--<script src="assets/js/STA_PERMIT.js" type="text/javascript"></script>--%>
</asp:Content>

