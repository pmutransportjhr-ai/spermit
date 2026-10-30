<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="True" CodeFile="RTA_GOODS_PERMIT_APPLICATION_RECEIPT.aspx.cs" Inherits="RTA_GOODS_CARRIAGE_PERMIT_ISSUED_RECEIPT" %>

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
    <form ID="FormID1" runat="server" class="form-horizontal" autocomplete="off">
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="PermitHomePage.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>RTA Goods Carriage Permit
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
                                                <button ID="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv(IssuedReceiptDiv)">Print</button>

                                            </div>

                                        </div>
                                        <div ID="IssuedReceiptDiv">
                                            <div class="portlet-title" style="border-bottom: 0px solid #eee;">

                                                <div class="col-md-12 col-sm-12" style="text-align: center">
                                                    <div class="caption caption-md">
                                                        <span class="caption-subject font-black bold" style="font-size: 20px">FORM P. Pu. C.</span>
                                                        <br />
                                                        <span class="caption-subject font-black" style="font-size: 16px; font-weight: 500;">[See rule 73 (7)(V)]</span>
                                                        <br />
                                                        <span class="caption-subject font-black" style="font-size: 16px; font-weight: 500;">Goods Carriage Permit</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="portlet-body fbold">
                                                <div class="row">
                                                    <div class="col-md-8">
                                                        <b>The Regional Transport Authoritiy<br />
                                                            <asp:Label Font-Bold="True" ID="lblRTOName" runat="server" Text=""></asp:Label>

                                                        </b>
                                                    </div>
                                                    <br />
                                                    <div class="col-md-4">Permit No. :-
                                                        <asp:Label Font-Bold="True" ID="lblPermitNo" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <ol>
                                                    <li>Full Name of holder :-
                                                        <asp:Label Font-Bold="True" ID="lblOwnerName" runat="server" Text=""></asp:Label>

                                                    </li>
                                                    <li>Full Residential Address :-
                                                        <asp:Label Font-Bold="True" ID="lblPermanentAddress" runat="server" Text=""></asp:Label>

                                                    </li>
                                                    <li>Full Business Address :-
                                                        <asp:Label Font-Bold="True" ID="lblPresentAddress" runat="server" Text=""></asp:Label>

                                                    </li>
                                                    <li>Registration mark of the goods carriage :- 
                                                <asp:Label Font-Bold="True" ID="lblRegistrationNo" runat="server" Text=""></asp:Label>
                                                            </li>
                                                    <li>Make :-
                                                        <asp:Label Font-Bold="True" ID="lblMake" runat="server" Text=""></asp:Label>
                                                             &nbsp;&nbsp;&nbsp;&nbsp;Model :-
                                                        <asp:Label Font-Bold="True" ID="lblModel" runat="server" Text=""></asp:Label>
                                                             &nbsp;&nbsp;&nbsp;&nbsp;Date of first registration:-
                                                     </li>
                                                         <li>Chasis No. :-
                                                        <asp:Label Font-Bold="True" ID="lblChasisNumber" runat="server" Text=""></asp:Label>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;Engine No. :-
                                                        <asp:Label Font-Bold="True" ID="lblEngineNumber" runat="server" Text=""></asp:Label>
                                                            </li>
                                                       <li>Types of vehicle :- 
                                                        <asp:Label Font-Bold="True" ID="lblTypesOfVehichle" runat="server" Text=""></asp:Label>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;Unload weight-
                                                        <asp:Label Font-Bold="True" ID="lblUnloadWeight" runat="server" Text=""></asp:Label> <b>Kgs.</b>
                                                            </li>
                                                    <li>Gross Vehicle weight :- 
                                                        <asp:Label Font-Bold="True" ID="lblGrossVehicleWeight" runat="server" Text=""></asp:Label> <b>Kgs.</b>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;Pay load :-
                                                        <asp:Label Font-Bold="True" ID="lblPayLoad" runat="server" Text=""></asp:Label> <b>Kgs.</b>
                                                            </li>
                                                    <li>Hire purchaser /lease /hypothecation agreement with : - 
                                                        <asp:Label Font-Bold="True" ID="lblAgreementwith" runat="server" Text=""></asp:Label>
                                                            
                                                            </li>
                                                    <li>Area for which the permit valid :- <b>AREA OF JHARKHAND.</b></li>
                                                    
                                                    <li>Validity of permit :- from&nbsp;&nbsp; 
                                                    <asp:Label Font-Bold="True" ID="lblPermitFrom" runat="server" Text=""></asp:Label>
                                                        &nbsp;&nbsp;
                                                    to&nbsp;&nbsp;
                                                <asp:Label Font-Bold="True" ID="lblPermitTo" runat="server" Text=""></asp:Label>

                                                    </li>
                                                    <li>Nature of goods to be carried a public carrier :- <b>UNRESTRICTED GENERAL GOODS.</b></li>
                                                    <li>Nature of goods to be carried for or in connection with trade or business on by the holder-</li>
                                                    <li>
                                                        <i><b>CONDITION</b></i> :-
                                                <ol style="list-style: lower-alpha">
                                                    <li>This permit is subject to the conditions laid down in section 84 of the Act, and in sub rule [1] and [6] of Rule 96 of the Central Motor Vechicles Rules 1989.
                                                    </li>
                                                    <li>This Permit is subject to other conditions attached of the permit.
                                                        
                                                    </li>
                                                   
                                                </ol>
                                                    </li>
                                                </ol>
                                                <br />
                                                <br />
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
                                                    <div class="col-md-6" style="text-align: center">
                                                        <i>Signature and Designation of
                                                        <br />
                                                        authorised signatory</i>
                                                
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        The Reginoal Transport Authority<br />
                                                        Ranchi (Jharkhand)
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
            width = width + 50;
            document.body.innerHTML = "<html><head><title></title></head><body><img width='" + width + "' height='" + height + "' style='z-index:-1;position:absolute' src='assets/img/log-watermark.png' alt ='image' />" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;






        }
    </script>
    
    <%--<script src="assets/js/STA_PERMIT.js" type="text/javascript"></script>--%>
</asp:Content>

