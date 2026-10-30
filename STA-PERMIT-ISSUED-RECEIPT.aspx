<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-PERMIT-ISSUED-RECEIPT.aspx.cs" Inherits="STA_PERMIT_ISSUED_RECEIPT" %>

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
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="PermitHomePage.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>Stage Carriage Permit - Inter State
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
                                            <div class="col-md-2" style="text-align: left">
                                             <asp:PlaceHolder ID="plBarCode" runat="server" /></div>
                                            
                                            <div class="portlet-title" style="border-bottom: 0px solid #eee;">
                                               
                                                <div class="col-md-10" style="text-align: center">
                                                    <div class="caption caption-md">
                                                        <span class="caption-subject font-black bold" style="font-size: 30px;">GOVERNMENT OF JHARKHAND</span>
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
                                                
                                                 <table style="width:100%">
                                                     <tr>
                                                    <td style="width:65%">
                                                       
                                                        Pari-Ayu-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        ............................../STA   
                                                    </td>
                                                    <td style="width:35%">
                                                        Permit No.-
                                                        <asp:label font-bold="true" id="lblPermitNo" runat="server" text=""></asp:label>
                                                   
                                                    </td></tr>
                                                </table><br />
                                                <%--<div class="row">
                                                    <div class="col-md-8">
                                                       The State Transport Authoritiy
                                                <br />
                                                           Jharkhand, Ranchi
                                                    </div>
                                                    <br />
                                                    <div class="col-md-4"> </div>
                                                </div>--%>
                                                <ol>
                                                    <li>Full name of Permit holder:
                                                        <asp:label font-bold="true" id="lblOwnerName" runat="server" text=""></asp:label> &nbsp;&nbsp;<asp:Label ID="lbTPOwnerName" runat="server" Text=""></asp:Label>

                                                    </li>
                                                    <li>Full residential address:
                                                        <asp:label font-bold="true" id="lblPermanentAddress" runat="server" text=""></asp:label>

                                                    </li>
                                                    <li>Full business address:
                                                        <asp:label font-bold="true" id="lblPresentAddress" runat="server" text=""></asp:label>

                                                    </li>
                                                    <li>The route or routes for which the permit is valid: 
                                                <asp:label font-bold="true" id="lblRoute" runat="server" text=""></asp:label> &nbsp;&nbsp;<asp:Label ID="lbRERoute" runat="server" Text=""></asp:Label>
                                                    </li>
                                                    <li>Total number of stage carriage covered by this permit: 
                                                <asp:label font-bold="true" id="lblTotalPermit" runat="server" text="One"></asp:label>
                                                    </li>
                                                    <li>Number of vehicles out of such total number to be kept in reserve to maintain the carriage and provide for special occasions: 
                                                <asp:label font-bold="true" id="lblVSoccasions" runat="server" text=""></asp:label>
                                                    </li>
                                                    <li>Maximum and minimum daily trips on each route: 
                                                <asp:label font-bold="true" id="lblMaximumTrip" runat="server" text=""></asp:label>
                                                    </li>
                                                    <li>Maximum and minimum rate of fares: 
                                                <asp:label font-bold="true" id="lblMaximumRate" runat="server" text="As per Goverment Rate"></asp:label>
                                                    </li>
                                                    <li>The stage carriage may be used as a goods carriage within the area or areas and subject to conditions here in under spacified
                                                <asp:label font-bold="true" id="lblStageCarrige" runat="server" text=""></asp:label>
                                                    </li>
                                                    <li>The reserve stage carriage may be used as a contract carriage within the area or areas and subject to conditions specified in P.Co.S. Permit No.
                                                <asp:label font-bold="true" id="lblStageCarrige1" runat="server" text=""></asp:label>
                                                    </li>
                                                    <li>Validity of permit: From&nbsp;&nbsp; 
                                                    <asp:label font-bold="true" id="lblPermitFrom" runat="server" text=""></asp:label>
                                                        &nbsp;&nbsp;
                                                    To&nbsp;&nbsp;
                                                <asp:label font-bold="true" id="lblPermitTo" runat="server" text=""></asp:label>

                                                    </li>
                                                    <li>
                                                        <u><b>Particulars of registration of each stage carriage</b></u>
                                                        <ol type="a">
                                                            <li>Registration mark:
                                                        <asp:label font-bold="true" id="lblRegistrationNo" runat="server" text=""></asp:label>&nbsp;&nbsp;<asp:Label ID="lblRPRegNo" runat="server" Text=""></asp:Label>
                                                            </li>
                                                            <li>Make:
                                                        <asp:label font-bold="true" id="lblMake" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>Model:
                                                        <asp:label font-bold="true" id="lblModel" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>Chasis No:
                                                        <asp:label font-bold="true" id="lblChasisNumber" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>Engine No:
                                                        <asp:label font-bold="true" id="lblEngineNumber" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>Seating Capacity (including driver/ conductor):
                                                        <asp:label font-bold="true" id="lblCapacity" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>Number of standees allowed
                                                        <asp:label font-bold="true" id="lblSeatAllowed" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>
                                                                SLD Serial No. 
                                                            </li>
                                                            <!--<li> Registration mark </li>
                                                        <li> Make </li>
                                                        <li> Model </li>
                                                        <li> Chasis No. </li>
                                                        <li> Engine No. </li>
                                                        <li> Seating Capacity (including driver/ conductor) </li>
                                                        <li> Number of standees allowed </li>-->
                                                        </ol>
                                                    </li>
                                                    <li>Hire Purchase/lease/hypothecation agreement with in respect of M.V. Nos.
                                                <asp:label font-bold="true" id="lblHire" runat="server" text=""></asp:label>
                                                    </li>
                                                    <li>
                                                        <u><b>Conditions</b></u> :-
                                                <ol style="list-style: lower-alpha">
                                                    <li>This permit is subject to the conditions laid down in section 84 of the Act, and in sub rule (1) and (6) of Rule 76 of the Bihar (now Jharkhand) Motor Vechicles Rules, 1992
                                                    </li>
                                                    <li>This Permit is also subject to conditions that-
                                                        <ol style="list-style: lower-roman">
                                                            <li>the approved fare-tables and time-tables of the routes shall be exhibited in the vehicles and on the parking places/stands and halts specified in the application for the permit.
                                                            </li>
                                                            <li>the arrangements specified in the application for the permit, for housing, maintenance and repair of the vehicles, and for storage and safe custody of luggage and goods will be maintained.
                                                            </li>
                                                            <li>the records and returns shall be submitted to the Transport Authority as specified by it.
                                                            </li>
                                                        </ol>
                                                    </li>
                                                    <li>This permit is further subject to other conditions attached to the permit.
                                                    </li>
                                                    <li>The State Transport Authority may, after giving notice of not less then one month, may
                                                        <ol style="list-style: lower-roman">
                                                            <li>vary the conditions of the permit.
                                                            </li>
                                                            <li>attach to the permit further conditions.
                                                            </li>
                                                            
                                                        </ol>
                                                    </li>
                                                    <li>This permit shall be valid in this state also only after counter signature of reciprocal state.
                                                            </li>
                                                            <li>The covered vehicle must have updated Tax, CF, Insurance & SLD along with Permit.
                                                            </li>
                                                    <li><strong>Every vehicle owner of stage and contract carriage will keep reserved two seats of the first row for the handicapped persons. It will be mentioned near the reserved seats.</strong></li>
                                                </ol>
                                                    </li>
                                                </ol>
                                                
                                                  <asp:Label ID="lblSpecial" runat="server" Visible="false">
                                                    <b>NOTE: </b> Additional vehicle JH02AL1408 is allowed to operate the service vide chairman STA order dated 31/08/2018.
                                                </asp:Label>
                                                <table style="width:100%">
                                                     <tr>
                                                    <td style="width:65%">
                                                         Date :  
                                                        <br/>Place : <b>Ranchi</b> 
                                                        <br/>Seal : 
                                                
                                                   </td>
                                                    <td style="width:35%; text-align:center">
                                                        Signature and Designation of authorised signatory
                                                        <br />The State Transport Authority
                                                        <br />Jharkhand, Ranchi.
                                                    </td></tr>
                                                </table>
                                                   

                                                
                                                <div class="row">
                                                    <div class="col-md-12" style="text-align: center">
                                                        <span class="caption-subject font-black bold" style="font-size: 30px">EXTENSION</span>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Under the provisions of rule 78, the area of validity of this permit is further extended in the following contiguous regions subject to the conditions set out below:- 
                                                
                                                    </div>
                                                </div>
                                                <br />
                                                <table style="width:100%">
                                                    <tr>
                                                    <td style="width:33%; text-align:center">Region</td>
                                                    <td style="width:33%; text-align:center">Route/Area</td>
                                                    <td style="width:33%; text-align:center">Conditions</td>
                                                        </tr>
                                                </table>
                                                <br />
                                               <table style="width:100%">
                                                     <tr>
                                                    <td style="width:65%">
                                                         Date :  
                                                        <br/>Place : <b>Ranchi</b> 
                                                        <br/>Seal : 
                                                
                                                   </td>
                                                   <td style="width:35%; text-align:center">
                                                        Signature and Designation of authorised signatory
                                                        <br />The State Transport Authority
                                                        <br />Jharkhand, Ranchi.
                                                    </td></tr>
                                                </table>

                                                <div class="row">
                                                    <div class="col-md-12" style="text-align: center">
                                                        <span class="caption-subject font-black bold" style="font-size: 30px">COUNTER SIGNATURE</span>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The permit is hereby countersigned with effect from signature and designation of ............... and  the counter signature No. ............... has been issued.
                                                
                                                    </div>
                                                </div>
                                                <br />
                                               <table style="width:100%">
                                                     <tr>
                                                    <td style="width:65%">
                                                         Date :  
                                                        <br/>Place : 
                                                        <br/>Seal : 
                                                
                                                   </td>
                                                    <td style="width:35%; text-align:center">
                                                        Signature and Designation of authorised signatory
                                                        <br />The State Transport Authority
                                                        <br />.................
                                                    </td></tr>
                                                </table>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12" style="text-align: center">
                                                        <span class="caption-subject font-black bold" style="font-size: 30px">RENEWAL</span>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This permit is herby renewed up to <asp:Label ID="lblRENUpto" runat="server" Text="..............."></asp:Label> with effect from <asp:Label ID="lblRENFrom" runat="server" Text="..............."></asp:Label> 
                                                
                                                    </div>
                                                </div>
                                                <br />
                                            <table style="width:100%">
                                                     <tr>
                                                    <td style="width:65%">
                                                         Date :  
                                                        <br/>Place : <b>Ranchi</b> 
                                                        <br/>Seal : 
                                                
                                                   </td>
                                                    <td style="width:35%; text-align:center">
                                                        Signature and Designation of authorised signatory
                                                        <br />The State Transport Authority
                                                        <br />Jharkhand, Ranchi.
                                                    </td></tr>
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
            <%--</div>--%>
     
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

