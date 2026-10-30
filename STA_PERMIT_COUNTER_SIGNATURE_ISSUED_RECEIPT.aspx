<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA_PERMIT_COUNTER_SIGNATURE_ISSUED_RECEIPT.aspx.cs" Inherits="STA_PERMIT_COUNTER_SIGNATURE_ISSUED_RECEIPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
         <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
   <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="PermitHomePage.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>Counter Signature Issued Receipt
                            <i class="fa fa-circle"></i>
                            </li>
                            
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">


                                    <div class="portlet light">
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12" style="text-align: right">
                                                <%--<asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="Print" OnClientClick="return printDiv('IssuedReceiptDiv')"  />--%>
                                                <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv('IssuedReceiptDiv')">Print</button>
                                            </div>
                                        </div>
                                        <div id="IssuedReceiptDiv">
                                           <div class="portlet-body">
                                                        
                                                           <div class="row" >
                                                               <div class="col-md-12">
                                                                    <p style="text-align:center; font-size:large;">
                                                                        Government of Jharkhand
                                                                        <br /><b>Office of Transport Commissioner
                                                                            <br />State Transport Authority, Jharkhand</b>
                                                                   <br />F.F.P. Building,HEC Campus,Dhurwa, Ranchi
                                                                   <br /><b>FORM P.C.S.</b>
                                                                   <br />[See rule 73 (7) (ix)]
                                                                   <br /><b><i>Countersignature of the permit</i></b></p>
                                                               </div>
                                                           </div>
                                                           <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                               <div class="col-md-7">
                                                                    No.&nbsp;<b><label style="font-weight:bold" id="lblAppNumber" runat="server"></label></b>
                                                               </div>
                                                               <div class="col-md-3">
                                                                  Ranchi,dated.&nbsp;<b><label style="font-weight:bold" id="lblAppDt" runat="server"></label></b>
                                                               </div>
                                                                <div class="col-md-1">
                                                                 
                                                               </div>
                                                           </div>
                                                             <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                               <div class="col-md-7">
                                                                   
                                                               </div>
                                                               <div class="col-md-3">
                                                                  Countersignature No.&nbsp;<label style="font-weight:bold" id="lblAppNumber1" runat="server"></label>
                                                               </div>
                                                                <div class="col-md-1">
                                                                 
                                                               </div>
                                                           </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                               <div class="col-md-10">
                                                                    <p>The State Transport Authority, Jharkhand,Ranchi</p>
                                                               </div>
<div class="col-md-1">
                                                                 
                                                               </div>
                                                               
                                                           </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-11">
                                                                    <p>1. Full name of the holder:&nbsp;<label style="font-weight:bold" id="lblName" runat="server"></label> </p>
                                                                    <p>2. Full Residential Address:&nbsp;<label style="font-weight:bold" id="lblResAdd" runat="server"></label> </p>
                                                                    <p>3. Full Business Address :&nbsp;<label style="font-weight:bold" id="lblBuAdd" runat="server"></label> </p>
                                                                    
                                                               </div>
                                                           </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-11">
                                                                    <p>&nbsp;&nbsp;&nbsp;&nbsp; Mobile Number:&nbsp;<label style="font-weight:bold" id="lblMobileNumber" runat="server"></label> </p>
                                                                    
                                                               </div>
                                                           </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-7">
                                                                    <p>4. Primary Permit No :&nbsp;<label style="font-weight:bold" id="lblPermitNo" runat="server"></label> </p>
                                                                    </div>
                                                                <div class="col-md-3">
                                                                    <p>Valid upto:&nbsp;<label style="font-weight:bold" id="lblValiUpto" runat="server"></label> </p>
                                                                    </div>
                                                                <div class="col-md-1">
                                                                 
                                                               </div>
                                                            </div>
                                                             <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-7">
                                                                    <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Issued by :&nbsp;<label style="font-weight:bold" id="lblIssuedBY" runat="server"></label> </p>
                                                                    </div>
                                                                <div class="col-md-4">
                                                                    
                                                                    </div>
                                                            </div>
                                                             <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                    <p>5. Registration Mark of the State Carriage/Contact Carriage/Private/Service Vehicle/Goods Carriage :&nbsp;<label style="font-weight:bold" id="lblRegNo" runat="server"></label> 
                                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;Seating capacity including (Driver/Conductor/Attendant) :&nbsp;<label style="font-weight:bold" id="lblSeatCapacity" runat="server"></label>&nbsp;<b>in all</b>
                                                                    </p>
                                                                   </div>
                                                                <div class="col-md-1">
                                                                     
                                                                 </div>
                                                            </div>
                                                             <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                      <p>6. The route of the routes or the area of areas for which the permit is countersigned :&nbsp;<label style="font-weight:bold" id="lblRoute" runat="server"></label> </p>
                                                                    <p>7. Validity of countersignature from :&nbsp;<label style="font-weight:bold" id="lblValidityfrom" runat="server"></label> To  &nbsp;<label style="font-weight:bold" id="lblValidityto" runat="server"></label></p>
                                                                    <p>8. Nature of goods to be carried, if any : &nbsp;<label style="font-weight:bold" id="lblNatureofGoods" runat="server"></label></p>
                                                               
                                                                    </div>
                                                                <div class="col-md-1">
                                                                     
                                                                 </div>
                                                            </div>
                                                             <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                    <p>9. Conditions:</p>
                                                                   
                                                                </div>
                                                                <div class="col-md-1">
                                                                 </div>
                                                                 </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                               
                                                                <div class="col-md-10">
                                                                    <ol type="a">
                                                                        <li>This Countersignature is subject to the conditions laid down in section 84 of the act and in rule (76) of rule of the Bihar Motor Vehicle Rules.</li>
                                                                        <li>This counter Signature is also subject to the condition attached to the primary/permit.</li>
                                                                        <li>This counter signature is subject to the Payment to tax of Jharkhand State. </li>
                                                                    </ol>
                                                                        
                                                                 </div>
                                                                 <div class="col-md-1">
                                                                  
                                                                   
                                                                </div>
                                                            </div>
                                                              <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                    <p>10. Timing:</p>
                                                                   
                                                                </div>
                                                                <div class="col-md-1">
                                                                 </div>
                                                                 </div>
                                                             <div class="row" >
                                                               <div class="col-md-2">
                                                                 
                                                               </div>
                                                              
                                                                
                                                                <div class="col-md-9">
                                                                    <div id="RouteTimeInfo" runat="server"></div>
                                                                 </div>
                                                                 <div class="col-md-1">
                                                                    
                                                                   
                                                                </div>
                                                                 </div>
                                                            <div class="row" >
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                              
                                                                <div class="col-md-10">
                                                                     <p>11. The Road Tax of Jharkhand is paid From :&nbsp;<label style="font-weight:bold" id="lblTaxDetails" runat="server"></label> </p>
                                                                    <p>12. Approval of Timing :&nbsp;<label style="font-weight:bold" id="lblApprovalTiming" runat="server"></label> </p>
                                                                   
                                                                </div>
                                                               <div class="col-md-1">
                                                                 
                                                               </div>
                                                                 </div>
                                                                 <div class="row">
                                                                     <div class="col-md-1">
                                                                 
                                                               </div>
                                                    <div class="col-md-11">
                                                        Date : 
                                                
                                                    </div>
                                                                     <div class="col-md-1">
                                                                 
                                                               </div>
                                                    <div class="col-md-11">
                                                        Place : 
                                                
                                                    </div>
                                                                     <div class="col-md-1">
                                                                 
                                                               </div>
                                                    <div class="col-md-5">
                                                        Seal : 
                                                
                                                    </div>
                                                    <div class="col-md-5" style="text-align: center">
                                                        Signature and Designation of
                                                        <br />
                                                        authorised signatory
                                                
                                                    </div>
                                                                     <div class="col-md-1">
                                                                 
                                                               </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                                 
                                                               </div>
                                                    <div class="col-md-11">
                                                        The State/Reginoal Transport Authority<br />
                                                        .............................................State/Region.
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
            //var height = $(document).height();
            //height = height + 100;
            //var width = $(document).width();
            //width = width + 50;;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";

            window.print();
            document.body.innerHTML = originalContents;


        }
    </script>
 
</asp:Content>

