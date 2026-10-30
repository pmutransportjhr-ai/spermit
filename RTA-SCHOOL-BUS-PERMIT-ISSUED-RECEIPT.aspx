<%@ Page Title="" Language="C#" MasterPageFile="~/RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-SCHOOL-BUS-PERMIT-ISSUED-RECEIPT.aspx.cs" Inherits="RTA_SCHOOL_BUS_PERMIT_ISSUED_RECEIPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style type="text/css">
        .fbold {
            font-weight: 200;
            font-family: "Open Sans",sans-serif;
            font-size: small;
        }

            .fbold li {
                padding: 3px 0;
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
                            <li>RTA School Bus Permit
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
                                        <div id="IssuedReceiptDiv" style="width:100%">
                                            <div class="col-md-2" style="text-align: left">
                                             <asp:PlaceHolder ID="plBarCode" runat="server" /></div>
                                            
                                            <div class="portlet-title" style="border-bottom: 0px solid #eee;">
                                               
                                                <div class="col-md-10" style="text-align: center">
                                                    <div class="caption caption-md">
                                                        <span class="caption-subject font-black bold" style="font-size: 30px">GOVERNMENT OF JHARKHAND</span>
                                                      <%--  <br />
                                                        <span class="caption-subject font-black bold" style="font-size: 20px">STAGE CARRIAGE PERMIT</span>--%>
                                                        <br />
                                                        <span class="caption-subject font-black" style="font-size: 16px; font-weight: 500;">Form P.Co.P.</span>
                                                        <br />
                                                        <span class="caption-subject font-black" style="font-size: 16px; font-weight: 500;">[See rule 73(7)(iii)]</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="portlet-body fbold">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <b>The State/Regional Transport Authoritiy     <br />                                          
                                                          <u> <asp:Label ID="lblRegion1" runat="server" ></asp:Label></u>&nbsp; Region</b>
                                                    </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4"></div>
                                                    <div class="col-md-4" style="text-align: right">Permit No.-
                                                        <asp:label font-bold="true" id="lblPermitNo" runat="server" text=""></asp:label>
                                                    </div>
                                                        <div class="col-md-2"></div>
                                                    
                                                </div>
                                                <ol>
                                                    <li>Full name of the holder:
                                                        <asp:label font-bold="true" id="lblOwnerName" runat="server" text=""></asp:label>

                                                    </li>
                                                    <li>Full residential address:
                                                        <asp:label font-bold="true" id="lblPermanentAddress" runat="server" text=""></asp:label>

                                                    </li>
                                                    <li>Full business address:
                                                        <asp:label font-bold="true" id="lblPresentAddress" runat="server" text=""></asp:label>

                                                    </li>
                                                    <li>Registration mark: 
                                                         <asp:label font-bold="true" id="lblRegistrationNo" runat="server" text=""></asp:label>
                                              <%--  <asp:label font-bold="true" id="lblRoute" runat="server" text=""></asp:label>--%>
                                                    </li>
                                                    <%--<li>Total number of stage carriages covered by this permit: 
                                                <asp:label font-bold="true" id="lblTotalPermit" runat="server" text="One"></asp:label>
                                                    </li>--%>
                                                    <%--<li>Number of vehicles out of such total number to be kept in reserve to maintain the carriage and provide for special occasions: 
                                                <asp:label font-bold="true" id="lblVSoccasions" runat="server" text=""></asp:label>
                                                    </li>
                                                    <li>Maximum and minimum daily trips on each route: 
                                                <asp:label font-bold="true" id="lblMaximumTrip" runat="server" text="UP or Down"></asp:label>
                                                    </li>
                                                    <li>Maximum and minimum rate of fares: 
                                                <asp:label font-bold="true" id="lblMaximumRate" runat="server" text="As per Goverment Rate"></asp:label>
                                                    </li>
                                                    <li>The stage carriage may be used as a goods carriage within the areas or areas and subject to conditions here in under spacified
                                                <asp:label font-bold="true" id="lblStageCarrige" runat="server" text=""></asp:label>
                                                    </li>
                                                    <li>The reserve stage carriage may be used as a contract carriage within the area or areas and subject to condition specified in P.Co.S. Permit No.
                                                <asp:label font-bold="true" id="lblStageCarrige1" runat="server" text=""></asp:label>
                                                    </li--%>
                                                  
                                                    <li>
                                                        <u><b>Particulars of Vehicle</b></u>
                                                        <ol type="a">
                                                            <li>Date of first Registration : 
                                                      <asp:label font-bold="true" id="lblRegistSrationNo" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>Make:
                                                        <asp:label font-bold="true" id="lblMake" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>Model:
                                                        <asp:label font-bold="true" id="lblModel" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>Type of Vehicle:
                                                        <asp:label font-bold="true" id="lblVehicleType" runat="server" text=""></asp:label>
                                                            </li>
                                                            
                                                            <li>Chasis No:
                                                        <asp:label font-bold="true" id="lblChasisNumber" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>Engine No:
                                                        <asp:label font-bold="true" id="lblEngineNumber" runat="server" text=""></asp:label>
                                                            </li>
                                                            <li>Seating Capacity (in ALL):
                                                        <asp:label font-bold="true" id="lblCapacity" runat="server" text=""></asp:label>
                                                            </li>
                                                            <%--<li>Number of standees allowed
                                                        <asp:label font-bold="true" id="lblSeatAllowed" runat="server" text="No"></asp:label>
                                                            </li>--%>
                                                            <!--<li> Registration mark </li>
                                                        <li> Make </li>
                                                        <li> Model </li>
                                                        <li> Chasis No. </li>
                                                        <li> Engine No. </li>
                                                        <li> Seating Capacity (including driver/ conductor) </li>
                                                        <li> Number of standees allowed </li>-->
                                                        </ol>
                                                    </li>

                                                      <li>Validity of permit: From&nbsp;&nbsp; 
                                                    <asp:label font-bold="true" id="lblPermitFrom" runat="server" text=""></asp:label>
                                                        &nbsp;&nbsp;
                                                    To&nbsp;&nbsp;
                                                <asp:label font-bold="true" id="lblPermitTo" runat="server" text=""></asp:label>

                                                    </li>

                                                    <li>Hire Purchase/lease/hypothecation agreement with :- <b>AS PER R/C</b>
                                                <asp:label font-bold="true" id="lblHire" runat="server" text=""></asp:label>
                                                    </li>
                                                    <li>
                                                        Area for which the permit valid:
                                                        <asp:label font-bold="true" id="lblPermitArea" runat="server" text=""></asp:label>
                                                    </li>
                                                    <li>
                                                    <u><b>Conditions</b></u> :-
                                                <ol style="list-style: lower-alpha">
                                                    <li>The permit is subject to the conditions laid down in section 84 of the Act, 
                                                        and in sub rule (1) and (6) of the rule 86 of the Central Motor Vechicles Rule, 1989
                                                    </li>
                                                     <li>This permit is subject to other conditions attached of the permit.
                                                    </li>
                                               <%--     <li>This Permit is also subject to conditions that-
                                                        <ol style="list-style: lower-roman">
                                                            <li>the approved fare-tables and time-table of the riutes shall be exhibited in the vehicles and on the parking places/stands and hait specified in the application for the permit.
                                                            </li>
                                                            <li>the arrangements specified in the application for the permit, for housing, maintenance an repairs of the vehicles, and for storage and safe custody of luggage and goods will be maintained.
                                                            </li>
                                                            <li>the records and returns shall be submitted to the Transport Authority as specified by it.
                                                            </li>
                                                        </ol>
                                                    </li>
                                                    <li>This permit is further subject to other conditions attached to the permit.
                                                    </li>
                                                    <li>The State/Regional Transport may, after giving notice of not less then one month may
                                                        <ol style="list-style: lower-roman">
                                                            <li>vary the conditions of the permit.
                                                            </li>
                                                            <li>attach to the permit further conditions.
                                                            </li>
                                                        </ol>
                                                    </li>--%>
                                                </ol>
                                                    </li>
                                                </ol>
                                           
                                               
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        Date : 
                                                
                                                    </div>
                                                    <div class="col-md-12">
                                                        Place : 
                                                
                                                    </div>
                                                    <div class="col-md-6">
                                                        Seal : 
                                                
                                                    </div>
                                                    <div class="col-md-6" style="text-align: right">
                                                       Signature and Designation of
                                                      
                                                        authorised signatory
                                                
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12" style="font-size:14px;font-weight:bold">
                                                        <b>The State/Reginoal Transport Authority<br />
                                                        <u><asp:Label ID="lblRegion2" runat="server" ></asp:Label></u>&nbsp; Region</b>
                                                    </div>
                                                     <div class="col-md-6 col-sm-6" style="text-align:right"></div>
                                                        </div>

                                                </div>
                                              <%--  <div class="row">
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
                                                <div class="row" style="text-align: center">
                                                    <div class="col-md-4">Region</div>
                                                    <div class="col-md-4">Route/Area</div>
                                                    <div class="col-md-4">Conditions</div>
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

                                             --%>
                                          
                                                <div class="row">
                                                    <div class="col-md-12" style="text-align: center">
                                                        <span class="caption-subject font-black bold" style="font-size: 30px">RENEWAL</span>
                                                    </div>
                                                </div>
                                          
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;This permit is herby renewed up to ............... with effect from ............... 
                                                
                                                    </div>
                                                </div>
                                          
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
                                                       <b> The State/Reginoal Transport Authority<br />
                                                      <u> <asp:Label ID="lblRegion3" runat="server"></asp:Label></u>&nbsp; Region
                                                           </b>
                                                    </div>
                                                    <div class="col-md-6"></div>
                                                    <div class="col-md-6" style="text-align: right">
                                                        Signature and Designation of
                                               
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
            var width = $(document).width() + 10;
            width = width + 50;
            document.body.innerHTML = "<html><head><title></title></head><body><img width='" + width + "' height='" + height + "' style='z-index:-1;position:absolute' src='assets/img/log-watermark.png' alt ='image' />" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;






        }
    </script>
    
    <%--<script src="assets/js/STA_PERMIT.js" type="text/javascript"></script>--%>
</asp:Content>

