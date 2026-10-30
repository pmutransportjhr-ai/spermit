<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="JHTRANSPORT.master"
    CodeFile="STA_PERMIT_COUNTER_SIGNATURE_TRANSACTION_RECEIPT_CLNT_NEW.aspx.cs"
    Inherits="STA_PERMIT_COUNTER_SIGNATURE_TRANSACTION_RECEIPT_CLNT_NEW" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

        <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" rel="stylesheet" />

        <style>
            body {
                background: #f5f5f5;
                color: #222;
                font-family: Arial, Helvetica, sans-serif;
            }

            /* RECEIPT */
            .receipt-container {
                position: relative;
                width: 850px;
                margin: 25px auto;
                padding: 30px 40px;
                box-sizing: border-box;

                background: #fff url('<%= ResolveUrl("~/assets/img/Untitled.png") %>') repeat center;

                border: 1px solid #ccc;
                border-top: 3px solid #2f6f3e;
            }

            .receipt-container>* {
                position: relative;
                z-index: 2;
            }

            /* HEADER */
            .header {
                text-align: center;
                margin-bottom: 20px;
            }

            .header img.logo {
                width: 65px;
                height: auto;
                margin-bottom: 5px;
            }

            .gov-main {
                font-size: 21px;
                font-weight: bold;
                color: #222;
            }

            .gov-sub {
                font-size: 15px;
                color: #555;
                margin-top: 3px;
            }

            .receipt-title {
                display: inline-block;
                margin-top: 10px;
                padding: 6px 18px;

                font-size: 15px;
                font-weight: bold;
                color: #2f6f3e;

                border: 1px solid #2f6f3e;
                border-radius: 2px;
            }

            /* QR */
            .qr-box {
                position: absolute;
                right: 40px;
                top: 30px;
                background: #fff;
                padding: 3px;
                border: 1px solid #ddd;
            }

            .qr-box img {
                display: block;
            }

            /* TABLE */
            .receipt-table {
                width: 100%;
                margin-top: 10px;
                margin-bottom: 18px;

                border-collapse: collapse;
                font-size: 13px;

                background: #fff;
            }

            .receipt-table th {
                width: 22%;
                padding: 9px 10px;

                background: #f7f7f7;
                color: #333;

                font-weight: bold;
                text-align: left;

                border: 1px solid #ccc;
            }

            .receipt-table td {
                padding: 9px 10px;

                background: #fff;
                color: #222;

                border: 1px solid #ccc;
            }

            /* SECTION */
            .section-title {
                margin-top: 18px;
                margin-bottom: 8px;
                padding-bottom: 5px;

                border-bottom: 1px solid #ccc;

                font-size: 15px;
                font-weight: bold;
                color: #2f6f3e;
            }

            /* FOOTER */
            .receipt-footer {
                margin-top: 20px;
                padding-top: 10px;

                border-top: 1px solid #ccc;

                text-align: center;
                color: #555;
                font-size: 12px;
            }

            /* PRINT BUTTON */
            .no-print {
                width: 850px;
                margin: 15px auto;
                text-align: right;
            }

            .no-print .btn {
                background: #2f6f3e;
                border: 1px solid #2f6f3e;
                color: #fff;

                padding: 5px 14px;
                border-radius: 2px;
                font-size: 12px;
            }

            /* HINDI */
            @font-face {
                font-family: kruti-dev;
                src: url('<%= ResolveUrl("~/assets/Fonts/Shusha.ttf") %>') format("truetype");
            }

            .hinditext {
                font-family: kruti-dev;
                font-size: 18px;
            }

            /* PRINT */
            @media print {

                @page {
                    size: A4;
                    margin: 10mm;
                }

                body {
                    background: #fff;
                }

                .no-print {
                    display: none !important;
                }

                .receipt-container {
                    width: 100%;
                    margin: 0;
                    padding: 25px 30px;

                    border: 1px solid #ccc;
                    border-top: 3px solid #2f6f3e;

                    box-shadow: none;
                }

                .receipt-table {
                    font-size: 12px;
                }

                .receipt-table th,
                .receipt-table td {
                    padding: 7px;
                }
            }
        </style>

    </asp:Content>


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <form runat="server">

            <!-- BUTTONS -->
            <div class="no-print" style="width: 850px; margin: 20px auto; text-align: right;">
                <!-- <button type="button" class="btn btn-primary btn-sm" onclick="setLang('en')">English</button>
            <button type="button" class="btn btn-warning btn-sm" onclick="setLang('hi')">हिंदी</button> -->


                <button type="button" class="btn btn-success btn-sm" onclick="printDiv()">Print</button>
            </div>

            <div class="receipt-container">

                <!-- HEADER -->
                <div class="header text-center">

                    <img src='<%= ResolveUrl("~/assets/img/logo.png") %>' class="logo" />
                    <div class="gov-header">

                        <div class="gov-main">Government of Jharkhand</div>

                        <div class="gov-sub">Transport Department</div>

                        <div class="receipt-title">Tax Payment Receipt</div>

                    </div>

                </div>

                <!-- QR -->
                <div class="qr-box">
                    <asp:PlaceHolder ID="plBarCode" runat="server" />
                </div>

                <!-- MAIN DETAILS -->
                <table class="table table-bordered receipt-table">

                    <tr>
                        <th>
                            <span class="en">Applicant Name</span>
                            <span class="hi">आवेदक का नाम</span>
                        </th>
                        <td id="lblApplicantName" runat="server"></td>

                        <th>Registration No</th>
                        <td id="lblRegNo" runat="server"></td>
                    </tr>

                    <tr>
                        <th>Transaction No</th>
                        <td id="lblTransNumber" runat="server"></td>

                        <th>GRN</th>
                        <td id="lblgrnNumber" runat="server"></td>
                    </tr>

                    <tr>
                        <th>Transaction Date</th>
                        <td id="lblDated" runat="server"></td>

                        <th>Tax Amount</th>
                        <td>₹ <span id="lblTaxAmount" runat="server"></span></td>
                    </tr>

                    <tr>
                        <th>Tax Period</th>
                        <td colspan="3" id="lblTaxperiod" runat="server"></td>
                    </tr>

                </table>

                <!-- VEHICLE DETAILS -->
                <div class="section-title">Vehicle Details</div>

                <table class="table table-bordered receipt-table">

                    <tr>
                        <th>Make</th>
                        <td id="lblMake" runat="server"></td>

                        <th>Engine No</th>
                        <td id="lblEngineNumber" runat="server"></td>
                    </tr>

                    <tr>
                        <th>Chassis No</th>
                        <td id="lblChaiseNo" runat="server"></td>

                        <th>Registration</th>
                        <td id="lblRegisNo" runat="server"></td>
                    </tr>

                    <tr>
                        <th>Valid Upto</th>
                        <td id="lblPaidUpto" runat="server"></td>

                        <th>State</th>
                        <td>Jharkhand</td>
                    </tr>

                </table>

                <!-- FOOTER -->
                <div class="section-title"></div>

                <div class="receipt-footer">
                    <b>
                        <span class="en">
                            This is a computer generated receipt
                        </span>
                        <span class="hi">
                            यह कंप्यूटर जनित रसीद है
                        </span>
                    </b>
                </div>

            </div>

        </form>

    </asp:Content>


    <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

        <script>
            function setLang(lang) {
                if (lang === 'hi') {
                    $('.en').css('display', 'none');
                    $('.hi').css('display', 'inline');
                } else {
                    $('.hi').css('display', 'none');
                    $('.en').css('display', 'inline');
                }
            }

            $(document).ready(function () {
                setLang('en');
            });

            function printDiv() {
                var printContents = document.querySelector('.receipt-container').innerHTML;
                var originalContents = document.body.innerHTML;

                var height = document.body.scrollHeight;
                var width = document.body.scrollWidth;

                document.body.innerHTML =
                    "<html><body>" +
                    "<img src='assets/img/Untitled.png' style='position:absolute; z-index:-1; width:100%; height:100%; opacity:0.2; repeat center;' />" +
                    printContents +
                    "</body></html>";

                window.print();
                document.body.innerHTML = originalContents;
            }
        </script>

    </asp:Content>