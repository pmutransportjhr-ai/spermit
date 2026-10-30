<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="RTA_AUTO_RICKSHAW_REPLACE_TRANSACTION_RECEIPT.aspx.cs" Inherits="RTA_AUTO_RICKSHAW_REPLACE_TRANSACTION_RECEIPT" %>

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
                            <li>RTA Vehicle (Auto Rickshaw) Replacement Transaction Receipt
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

                                               <%-- <div class="col-md-12 col-sm-12" style="text-align: center">
                                                    <div class="caption caption-md">
                                                        <span class="caption-subject font-black bold" style="font-size: 30px">GOVERNMENT OF JHARKHAND</span>
                                                        <br />
                                                        <span class="caption-subject font-black bold" style="font-size: 20px">Counter Signature Payment Receipt</span>
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </div>
                                                </div>--%>
                                            </div>
                                            <div class="portlet-body fbold">
                                                <div class="row">
                                                    <div class="col-md-4" style="text-align: center;">
                                                        <p class="hinditext">JaarKMD sarkar</p>
                                                    </div>
                                                    <div class="col-md-4" style="text-align: center;">
                                                        <p class="hinditext">JaarKMD sarkar</p>
                                                    </div>
                                                    <div class="col-md-4" style="text-align: center;">
                                                        <p class="hinditext">JaarKMD sarkar</p>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <table style="width: 100%">
                                                            <tr style="width: 100%">
                                                                <td style="width: 60%">
                                                                    <p>
                                                                        <label class="hinditext">AnausaUcaI</label><label id="lblAnusuchi" runat="server"></label>&nbsp&nbsp;<label id="lblBillNo" runat="server"></label>
                                                                    </p>
                                                                </td>
                                                                <td style="width: 40%; text-align: right;">
                                                                    <p class="hinditext">kayaa-laya p`it</p>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 100%">
                                                                <td style="width: 60%">
                                                                    <p>
                                                                        <label class="hinditext">farma saM</label>&nbsp;<label style="font-size:14px" id="lblApplicationNumber" runat="server"></label>
                                                                    </p>
                                                                </td>
                                                                <td style="width: 40%; text-align: right;">
                                                                    <p>
                                                                        <label class="hinditext">farma saM [</label>/<label class="hinditext">saI</label>
                                                                    </p>
                                                                </td>
                                                            </tr>
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
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <p>
                                                                    <label class="hinditext">EaI</label>&nbsp;<label id="lblApplicantName" style="font-size:14px" runat="server"></label>&nbsp;<label class="hinditext">sao  ]nakI</label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">maaoTrgaaDI ko ijasakI roijasT`IkRt saMKa </label>
                                                                    <label id="lblRegNo" style="font-size:14px" runat="server"></label>
                                                                    <label class="hinditext">hO</label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">kr ko $p mao</label>&nbsp;<label id="lblTrID" runat="server"></label>&nbsp;<label class="hinditext">$</label>&nbsp;<label id="lblTaxAmount" style="font-size:14px;" runat="server"></label>/-&nbsp;<label class="hinditext">pO</label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">nakd </label>
                                                                    /<label class="hinditext">baOnk </label>
                                                                    /<label class="hinditext">D\aFT</label>/<label class="hinditext">caalaana</label>/<label class="hinditext">gaor</label><label>--</label><label class="hinditext">nyaaiyak sTamp ko Wara p`aPt</label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">ikyao.</label>
                                                                </p>
                                                                <table style="width: 100%">
                                                                    <tr style="width: 100%">
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">Caalaana saMKyaa </label>
                                                                        </td>
                                                                        <td style="width: 20%"></td>
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">id0 </label>
                                                                        </td>
                                                                        <td style="width: 20%"></td>
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">baoMk</label></td>
                                                                    </tr>
                                                                    <tr style="width: 100%">
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">baOnk D\aFT</label></td>
                                                                        <td style="width: 20%"></td>
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">id0 </label>
                                                                        </td>
                                                                        <td style="width: 20%"></td>
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">baoMk</label></td>
                                                                    </tr>
                                                                </table>
                                                                <p>
                                                                    <label class="hinditext">tarIK</label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">sqaana</label>
                                                                </p>
                                                                <br />
                                                                <p>
                                                                    <label style="font-weight: bold;" class="hinditext">hstaxar</label>
                                                                </p>


                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <table style="width: 100%">
                                                            <tr style="width: 100%">
                                                                <td style="width: 60%">
                                                                    <p>
                                                                        <label class="hinditext">AnausaUcaI</label><label id="Label1" runat="server"></label>&nbsp&nbsp;<label id="lblApplicationnm" runat="server"></label>
                                                                    </p>
                                                                </td>
                                                                <td style="width: 40%; text-align: right;">
                                                                    <p class="hinditext">baahna maalaIk p`it</p>
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 100%">
                                                                <td style="width: 60%">
                                                                    <p>
                                                                        <label class="hinditext">farma saM</label>&nbsp;<label style="font-size:14px" id="lblApplicationn" runat="server"></label>
                                                                    </p>
                                                                </td>
                                                                <td style="width: 40%; text-align: right;">
                                                                    <p>
                                                                        <label class="hinditext">farma saM [</label>/<label class="hinditext">saI</label>
                                                                    </p>
                                                                </td>
                                                            </tr>
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
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <p>
                                                                    <label class="hinditext">EaI</label>&nbsp;<label id="lblApplicantName2" style="font-size:14px" runat="server"></label>&nbsp;<label class="hinditext">sao  ]nakI</label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">maaoTrgaaDI ko ijasakI roijasT`IkRt saMKa </label>
                                                                    <label id="lblRegNo2" style="font-size:14px;" runat="server"></label>
                                                                    <label class="hinditext">hO</label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">kr ko $p mao</label>&nbsp;<label class="hinditext">$</label>&nbsp;<label id="lblTaxAmount3" style="font-size:14px" runat="server"></label>/-&nbsp;<label id="Label7" runat="server"></label>&nbsp;<label class="hinditext">pO</label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">nakd </label>
                                                                    /<label class="hinditext">baOnk </label>
                                                                    /<label class="hinditext">D\aFT</label>/<label class="hinditext">caalaana</label>/<label class="hinditext">gaor</label><label>--</label><label class="hinditext">nyaaiyak sTamp ko Wara p`aPt</label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">ikyao.</label>
                                                                </p>
                                                                <table style="width: 100%">
                                                                    <tr style="width: 100%">
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">Caalaana saMKyaa </label>
                                                                        </td>
                                                                        <td style="width: 20%"></td>
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">id0 </label>
                                                                        </td>
                                                                        <td style="width: 20%"></td>
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">baoMk</label></td>
                                                                    </tr>
                                                                    <tr style="width: 100%">
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">baOnk D\aFT</label></td>
                                                                        <td style="width: 20%"></td>
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">id0 </label>
                                                                        </td>
                                                                        <td style="width: 20%"></td>
                                                                        <td style="width: 20%">
                                                                            <label class="hinditext">baoMk</label></td>
                                                                    </tr>
                                                                </table>
                                                                <p>
                                                                    <label class="hinditext">tarIK</label>
                                                                </p>
                                                                <p>
                                                                    <label class="hinditext">sqaana</label>
                                                                </p>
                                                                <br />
                                                                <p>
                                                                    <label style="font-weight: bold;" class="hinditext">hstaxar</label>
                                                                </p>


                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <p style="text-align: center">
                                                            <label class="hinditext">Anau0</label><label id="lblAnusuchiNo1" runat="server"></label>&nbsp;&nbsp;<label class="hinditext">farma0</label>&nbsp;<label id="lblFormNo1" style="font-size:14px;" runat="server"></label></p>
                                                        <p style="text-align: center">
                                                            <label class="hinditext">kr p`tIk</label>(<label class="hinditext">TO@sa Taokna</label>)</p>
                                                        <p style="text-align: center">(<label class="hinditext">JaarKMD maaoTrgaaDI kraraopna AiVinayama 2001 ko AVIna</label>)</p>
                                                        <p style="text-align: left">
                                                            <label class="hinditext">k``ma saMKa</label><label id="lblKramikNo" runat="server"></label></p>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <p style="text-align: left">
                                                            <label class="hinditext">maok</label><label id="Label8" runat="server"></label></p>
                                                        <p style="text-align: left">
                                                            <label class="hinditext">[Mijana saMKa</label><label id="Label9" runat="server"></label></p>
                                                        <p style="text-align: left">
                                                            <label class="hinditext">Caoisasa saMKa</label><label id="Label10" runat="server"></label></p>
                                                        <p style="text-align: left">
                                                            <label class="hinditext">rija saMKa</label><label id="Label11" runat="server"></label></p>
                                                        <p style="text-align: left">
                                                            <label class="hinditext">samaaiPt kI tarIK</label><label id="Label12" runat="server"></label></p>
                                                        <p style="text-align: left">
                                                            <label class="hinditext">kraraopna AiVkarI</label><label id="Label13" runat="server"></label></p>
                                                        <p style="text-align: left">
                                                            <label class="hinditext">ijalaa</label><label id="Label14" runat="server"></label></p>
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

