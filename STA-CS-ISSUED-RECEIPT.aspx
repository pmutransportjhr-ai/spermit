<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master"
    AutoEventWireup="true" CodeFile="STA-CS-ISSUED-RECEIPT.aspx.cs"
    Inherits="STA_CS_ISSUED_RECEIPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        body {
            font-family: "Open Sans", Arial;
            font-size: 14px;
        }

        .print-bar {
            margin-bottom: 8px;
            text-align: left;
        }

        .receipt-card {
            border: 1px solid #000;
            padding: 18px 20px;
            background: #fff;
            /* A4 size on screen */
            width: 794px; /* 210mm */
            min-height: 1123px; /* 297mm */

            margin: 0 auto;
            box-sizing: border-box;
        }


        .header {
            text-align: center;
            line-height: 1.4;
        }

            .header b {
                display: block;
            }

        .title {
            font-size: 18px;
            font-weight: bold;
            text-decoration: underline;
            margin-top: 10px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        td {
            padding: 4px 2px;
            vertical-align: top;
        }

        ul {
            margin: 4px 0 0 18px;
        }

        @media print {
            .print-bar {
                display: none;
            }

            @page {
                margin: 15mm;
            }
        }

        /* HEADER STYLING */
        .gov-header {
            text-align: center;
            margin-top: 10px;
            margin-bottom: 15px;
        }

        /* MAIN TITLE */
        .gov-main {
            font-size: 22px;
            font-weight: bold;
            color: #0B6B3A;
            letter-spacing: 0.5px;
        }

        /* SUB TITLE */
        .gov-sub {
            font-size: 18px;
            font-weight: 600;
            color: #444;
            margin-top: 2px;
        }

        /* RECEIPT TITLE */
        .receipt-title {
            display: inline-block;
            margin-top: 8px;
            padding: 6px 18px;
            font-size: 15px;
            font-weight: bold;
            color: #444;
            border-radius: 20px;
            letter-spacing: 0.5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">

        <!-- PRINT BUTTON OUTSIDE CARD -->
        <div class="print-bar">
            <button type="button" class="btn btn-success btn-sm"
                onclick="printDiv('IssuedReceiptDiv')">
                Print</button>
        </div>

        <div class="receipt-card" id="IssuedReceiptDiv">



            <!-- HEADER -->
            <table style="width: 100%; margin-top: 10px;">
                <tr>
                    <td style="width: 20%;"></td>

                    <!-- CENTER HEADING -->
                    <td style="width: 60%; text-align: center;">
                        <div class="header text-center">

                            <img src='<%= ResolveUrl("~/assets/img/logo.png") %>' class="logo" />
                            <div class="gov-header">

                                <div class="gov-main">Government of Jharkhand</div>

                                <div class="gov-sub">Transport Department</div>

                                <div class="receipt-title">
                                    <b>OFFICE OF STATE TRANSPORT COMMISIONER</b><br />
                                    <b>FORM P.C.S.</b><br />
                                    <span>[ See rule 73 (7) (ix) ]</span><br />
                                    <br />
                                    <div style="font-size: 18px; font-weight: bold; text-decoration: underline;">
                                        COUNTERSIGNATURE OF THE PERMIT
                                    </div>
                                </div>

                            </div>

                        </div>

                    </td>

                    <!-- QR RIGHT -->
                    <td style="width: 20%; text-align: right; vertical-align: top;">
                        <asp:PlaceHolder ID="plBarCode" runat="server" />
                    </td>
                </tr>
            </table>


            <!-- META -->
            <table style="width: 100%; margin-top: 10px;">
                <tr>
                    <!-- LEFT COLUMN -->
                    <td style="width: 60%;">Pari-Ayu-&nbsp;............................../STA
                    </td>

                    <!-- RIGHT COLUMN (FIXED START) -->
                    <td style="width: 40%;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 150px; white-space: nowrap;">Ranchi, dated :-
                                </td>
                                <td>
                                    <b>
                                        <asp:Label ID="lblDate" runat="server" /></b>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table style="width: 100%; margin-top: 4px;">
                <tr>
                    <!-- LEFT COLUMN -->
                    <td style="width: 60%; font-weight: bold;">The State Transport Authority Ranchi, Jharkhand
                    </td>

                    <!-- RIGHT COLUMN (SAME FIXED START) -->
                    <td style="width: 40%;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 150px; white-space: nowrap;">Countersignature no :-
                                </td>
                                <td style="white-space: nowrap;">
                                    <!-- .............................. -->
                                    <asp:Label ID="csno" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>




            <!-- DETAILS -->
            <table>
                <tr>
                    <td style="width: 4%;"><b>1.</b></td>
                    <td style="width: 28%;">Full Name</td>
                    <td style="width: 4%;">:</td>
                    <td><b>
                        <asp:Label ID="lblName" runat="server" /></b></td>
                </tr>

                <tr>
                    <td><b>2.</b></td>
                    <td>Full Residential Address</td>
                    <td>:</td>
                    <td><b>
                        <asp:Label ID="lblresidentialaddr" runat="server" /></b></td>
                </tr>

                <tr>
                    <td><b>3.</b></td>
                    <td>Full Business Address</td>
                    <td>:</td>
                    <td><b>
                        <asp:Label ID="lblbusinessaddress" runat="server" /></b></td>
                </tr>

                <tr>
                    <td><b>4.</b></td>
                    <td>Primary Permit No</td>
                    <td>:</td>
                    <td>
                        <b>
                            <asp:Label ID="lblprimaryPermitno" runat="server" /></b>

                    </td>
                </tr>
                <tr>
                    <!-- Empty number column -->
                    <td></td>

                    <!-- Content aligned with other labels -->
                    <td colspan="3">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: left; width: 60%;">Issued by :
                    <b>
                        <asp:Label ID="lblissuedstate" runat="server" />
                    </b>
                                </td>

                                <td style="text-align: left; width: 40%;">Valid Upto :
                    <b>
                        <asp:Label ID="lblValidupto" runat="server" />
                    </b>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td valign="top"><b>5.</b></td>
                    <td colspan="3">Registration mark of the State Carriage :
        <b>
            <asp:Label ID="lblRegNo" runat="server" /></b>
                        &nbsp;Model&nbsp;
        <b>
            <asp:Label ID="lblModel" runat="server" /></b>

                        <!-- ONLY DB VALUE, ONLY WHEN REPLACED -->
                        <span runat="server" id="trReplace" visible="false">&nbsp;<b><asp:Label ID="lblReplace" runat="server" /></b>
                        </span>

                        &nbsp;Seating capacity :&nbsp;
        <b>
            <asp:Label ID="lblSeatCapacity" runat="server" /></b>
                        in all
                    </td>
                </tr>




                <tr>
                    <td><b>6.</b></td>
                    <td colspan="3">The route of the routes or the area of areas for which the permit is countersigned :
                <b>
                    <asp:Label ID="lblRouteDertailswithvia" runat="server" /></b>
                    </td>
                </tr>

                <tr>
                    <td><b>7.</b></td>
                    <td colspan="3">Validity of countersignature from :
                <b>
                    <asp:Label ID="lblValidtyFrom" runat="server" /></b>
                        To
                <b>
                    <asp:Label ID="lblValidityto" runat="server" /></b>
                    </td>
                </tr>

                <tr>
                    <td><b>8.</b></td>
                    <td colspan="3">Nature of the goods to be carried, if any :
                <b>Passenger's personal Luggage only</b>
                    </td>
                </tr>

                <tr>
                    <td valign="top"><b>9.</b></td>
                    <td colspan="3">Conditions :
                <ul>
                    <li>This countersignature is subject to Section 84 of the Act.</li>
                    <li>Subject to conditions attached to the primary permit.</li>
                    <li>Subject to payment of Jharkhand State tax.</li>
                    <li>Vehicle must be fitted with Speed Governor as per MoRTH.</li>
                </ul>
                    </td>
                </tr>

                <tr>
                    <td><b>10.</b></td>
                    <td colspan="3">The road tax of Jharkhand is paid from
                <b>
                    <asp:Label ID="lblTaxFrom" runat="server" /></b>
                        to
                <b>
                    <asp:Label ID="lblTaxto" runat="server" /></b>
                        (
                <b>
                    <asp:Label ID="lblAmount" runat="server" /></b>
                        Ref No.
                <b>
                    <asp:Label ID="lblReferenceno" runat="server" /></b>
                        dated
                <b>
                    <asp:Label ID="lblTransactiondt" runat="server" /></b>
                        )
                    </td>
                </tr>

                <tr>
                    <td><b>11.</b></td>
                    <td colspan="3">Timing :
                <b>Countersignature Time Table attached herewith.
                   <!-- <asp:Label ID="lblSourceStation" runat="server" />
                    <asp:Label ID="lblSourDepTime" runat="server" />
                    <asp:Label ID="lblDestinationStation" runat="server" />
                    <asp:Label ID="lblDestDepTime" runat="server" /> -->
                </b>
                    </td>
                </tr>
            </table>

            <!-- SIGNATURE -->
            <table>
                <tr>
                    <td style="width: 65%;"></td>
                    <td style="width: 35%; text-align: center; font-weight: bold;">Signature & Designation of<br />
                        Authorized Signatory
                    </td>
                </tr>
            </table>

            <!-- RENEWAL SECTION (RESTORED) -->
            <div style="text-align: center; margin-top: 10px;">
                <b><u>Renewal</u></b>
            </div>

            <div style="margin-top: 6px;">
                This Permit is hereby renewed upto ........ with effect from ........ and the Countersignature No. ........
            </div>

            <table>
                <tr>
                    <td style="width: 65%;">Date : ...............<br />
                        Place : ...............<br />
                        Seal : ...............<br />
                        The State / Regional Transport Authority<br />
                        Jharkhand ............ State / Region
                    </td>
                </tr>
            </table>



        </div>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script>
        function printDiv(id) {
            var content = document.getElementById(id).innerHTML;
            var old = document.body.innerHTML;
            document.body.innerHTML = content;
            window.print();
            document.body.innerHTML = old;
        }
    </script>
</asp:Content>
