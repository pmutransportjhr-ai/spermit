<%@ Page Title="" Language="C#"  MasterPageFile="~/JHTRANSPORT.master" AutoEventWireup="true" CodeFile="QR_CS_VIEW.aspx.cs" Inherits="QR_CS_VIEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style type="text/css" media="print">
    @media print
      {
         @page {
           margin-top: 10px;
           margin-bottom: 10px;
         }
         
      } 
        .fbold {
            /*font-weight: 600;*/
            font-family: "Open Sans",sans-serif;
            font-size: 14px !important;
        }

            .fbold li {
                padding: 5px 0;
            }
            
           
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12" style="text-align: right">
                                             
                                            </div>
                                        </div>
                                        <div id="IssuedReceiptDiv">
                                             <div class="col-md-2" style="text-align: left">
                                        <asp:PlaceHolder ID="plBarCode" runat="server" /></div>
                                        <div class="portlet-title" style="border-bottom: 0px solid #eee;"><div class="col-md-10" style="text-align: center">
                                                    <div class="caption caption-md">
                                                        <span class="caption-subject font-black " style="font-size: 14px">Government of Jharkhand</span>
                                                        <br>
                                                        <span class="caption-subject font-black bold" style="font-size: 16px">OFFICE OF STATE TRANSPORT COMMISIONER</span>
                                                        <br>
                                                        <span class="caption-subject font-black" style="font-size: 14px;">F.F.P. Building, HEC Campus, Dhurwa, Ranchi</span>
                                                        <br>
                                                        <span class="caption-subject font-black bold" style="font-size: 16px;">FORM P.C.S.</span>
                                                        <br>
                                                        <span class="caption-subject font-black" style="font-size: 12px;">[ See rule 73 (7) (ix) ]</span>
                                                        <br>
                                                        <span class="caption-subject font-black bold" style="font-size: 18px;"><u>Countersignature of the Permit</u></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="portlet-body">
                                                <table style="width:100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width:65%">Pari-Ayu-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;............................../STA</td>
                                                            <td style="width:35%">Ranchi, dated: - <span id="date" style="font-weight:bold;"></span></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table style="width:100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width:65%"><span class="caption-subject font-black bold" style="font-size: 16px;">The State Transport Authority Ranchi, Jharkhand</td>
                                                            <td style="width:35%">Countersignature no : - <span><asp:Label id="csno" runat="server" Text="lblCSno" style="font-weight:bold;"></asp:Label></span></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table style="width:100%">
                                                    <tbody>
                                                        <tr>
                                                            <td style="width:4%"><b>1.</b></td>
                                                            <td style="width:28%">Full Name</td>
                                                            <td style="width:4%"><b>:</b></td>
                                                            <td style="width:64%"><span><b><asp:Label ID="lblName" runat="server" Text="lblName"></asp:Label></b></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:4%"><b>2.</b></td>
                                                            <td style="width:28%">Full Residential Address</td>
                                                            <td style="width:4%"><b>:</b></td>
                                                            <td style="width:64%"><span><b><asp:Label ID="lblresidentialaddr" runat="server" Text="lblresidentialaddr"></asp:Label></b></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:4%"><b>3.</b></td>
                                                            <td style="width:28%">Full Business Address</td>
                                                            <td style="width:4%"><b>:</b></td>
                                                            <td style="width:64%"><span><b><asp:Label ID="lblbusinessaddress" runat="server" Text="lblbusinessaddress"></asp:Label></b></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:4%"><b>4.</b></td>
                                                            <td style="width:28%">Primary Permit No</td>
                                                            <td style="width:4%"><b>:</b></td>
                                                            <td style="width:64%"><span><b><asp:Label ID="lblprimaryPermitno" runat="server" Text="lblprimaryPermitno"></asp:Label></b></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Issued by :&nbsp;<span><b><asp:Label ID="lblissuedstate" runat="server" Text="lblissuedstate"></asp:Label></b></span>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                Valid Upto : &nbsp; <span><b><asp:Label ID="lblValidupto" runat="server" Text="lblValidupto"></asp:Label></b></span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:4%" valign="top"><b>5.</b></td>
                                                            <td colspan="3">Registration mark of the State Carriage/Contact Carriage/Private/Service Vehicle/Goods Carriage :&nbsp;
                                                                <span>
                                                                    <b><asp:Label ID="lblRegNo" runat="server" Text="lblRegNo"></asp:Label></b>
                                                                    &nbsp;Model&nbsp;<b><asp:Label ID="lblModel" runat="server" Text="lblModel"></asp:Label></b>
                                                                    &nbsp;Seating capacity including (Driver/Conductor/Attendant)<b>&nbsp;:&nbsp;<asp:Label ID="lblSeatCapacity" runat="server" Text="lblSeatCapacity"></asp:Label> &nbsp; in all</b> 
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:4%" valign="top"><b>6.</b></td>
                                                            <td colspan="3">The route of the routes or the area of areas for which the permit is countersigned :&nbsp;
                                                                <span>
                                                                    <b><asp:Label ID="lblRouteDertailswithvia" runat="server" Text="lblRouteDertailswithvia"></asp:Label></b>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:4%"><b>7.</b></td>
                                                            <td colspan="3">Validity of countersignature from :&nbsp;
                                                                <span>
                                                                    <b>
                                                                        <asp:Label ID="lblValidtyFrom" runat="server" Text="lblValidtyFrom"></asp:Label>
                                                                        &nbsp;&nbsp;To&nbsp;&nbsp; <asp:Label ID="lblValidityto" runat="server" Text="lblValidityto"></asp:Label>
                                                                    </b>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:4%"><b>8.</b></td>
                                                            <td colspan="3">Nature of the goods to be carried, if any :&nbsp;
                                                                <span><b>Passenger's personal Luggage only</b></span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:4%" valign="top"><b>9.</b></td>
                                                            <td colspan="3">Conditions :&nbsp;
                                                                <span>
                                                                    <ul>
                                                                        <li>This countersignature is subject to the conditions laid down in section 84 of the act and in rule (76) of rule of the Bihar Motor Vehicle Rules.</li>
                                                                        <li>This countersignature is also subject to the conditions attached to the primary/permit.</li>
                                                                        <li>This countersignature is subject to the payment to tax of Jharkhand State.</li>
                                                                        <li>Vehicle must be fitted with Speed Governor as per the guideline issued by MoRTH..</li>
                                                                    </ul>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                       <%-- <tr>
                                                            <td style="width:4%" valign="top"><b>10.</b></td>
                                                            <td colspan="3">Timing &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <span>
                                                                    <b>
                                                                        <asp:Label ID="lblSourceStation" runat="server" Text="lblSourceStation"></asp:Label>&nbsp;&nbsp;
                                                                        Dep. - &nbsp;&nbsp;<asp:Label ID="lblSourDepTime" runat="server" Text="lblSourDepTime"></asp:Label>,
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDestinationStation" runat="server" Text="lblDestinationStation"></asp:Label>&nbsp;&nbsp;
                                                                        Dep. - &nbsp;&nbsp;<asp:Label ID="lblDestDepTime" runat="server" Text="lblDestDepTime"></asp:Label>
                                                                    </b>
                                                                </span>
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td style="width:4%" valign="top"><b>10.</b></td>
                                                            <td colspan="3">The road tax of Jharkhand is paid from :<span><b><asp:Label ID="lblTaxFrom" runat="server" Text="lblTaxFrom"></asp:Label></b>&nbsp;&nbsp;
                                                                        to&nbsp;&nbsp;<b><asp:Label ID="lblTaxto" runat="server" Text="lblTaxto"></asp:Label></b>&nbsp;&nbsp;
                                                                        (&nbsp;<b><asp:Label ID="lblAmount" runat="server" Text="lblAmount"></asp:Label></b>&nbsp;&nbsp;
                                                                        Ref. No. - &nbsp;<b><asp:Label ID="lblReferenceno" runat="server" Text="lblReferenceno"></asp:Label></b>&nbsp;&nbsp;
                                                                        dated. &nbsp;<b><asp:Label ID="lblTransactiondt" runat="server" Text="lblTransactionDate"></asp:Label></b>)
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date :&nbsp;<span><b><asp:Label ID="Label1" runat="server" Text="..............."></asp:Label></b></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Place :&nbsp;<span><b><asp:Label ID="lblPlace" runat="server" Text="Ranchi"></asp:Label></b></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:4%"><b>11.</b></td>
                                                            <td colspan="3">Approval of Timing</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                
                                               <table style="width:100%">
                                                   <tbody><tr>
                                                       <td style="width:65%"></td>
                                                       <td style="width:35%; text-align:center; font-weight:bold"> 
                                                            Signature & Designation of
                                                            <br>Authorized Signatory
                                                       </td>
                                                       </tr>
                                                    </tbody>
                                               </table>
                                                <table style="width:100%">
                                                   <tbody><tr><td style="width:4%; font-size:12px;" valign="top">Seal</td>
                                                       <td style="width:40%">The State Regional Transport Authority
                                                            <br>Jharkhand............State/Region</td>
                                                       <td style="width:56%"> 
                                                            
                                                       </td>
                                                       </tr>
                                                    </tbody>
                                               </table>
                                                <div class="row">
                                                    <div class="col-md-12" style="text-align:center">
                                                        <b><u><span><asp:Label ID="Label4" runat="server" Text="Renewal" style="font-size:18px;"></asp:Label></span></u></b>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This Permit is hereby renewed upto ........ with effect from ......... and the Countersignature No.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;          
                                                        <%--<span id="ContentPlaceHolder1_label4"><b><br />PRADEEP KUMAR, RAGHU NANADAN PRASAD, SANKET PURI MAIN ROAD HAZARIBAGH</b></span> --%>
                                                </div></div>
                                                <table style="width:100%">
                                                    <tr>
                                                        <td style="width:65%">
                                                            Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:...............<br />
                                                            Place&nbsp;&nbsp;&nbsp;&nbsp;:...............<br />
                                                            Seal&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:...............<br />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The State/Regional Transport Authority
                                                            <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jharkhand............State/Region
                                                        </td>
                                                        </tr>
                                                </table>
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
            document.body.innerHTML = "<html><body>" + printContents + "</body></html>";
            window.print();
            document.body.innerHTML = originalContents;






        }
    </script>
    
    <%--<script src="assets/js/STA_PERMIT.js" type="text/javascript"></script>--%>
</asp:Content>

