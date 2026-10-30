<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-PERMIT-ISSUED-RECEIPT-GLOBAL.aspx.cs" Inherits="STA_PERMIT_ISSUED_RECEIPT_GLOBAL" %>

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
                                            <div class="portlet-title" style="border-bottom: 0px solid #eee;">

                                                <div class="col-md-12 col-sm-12" style="text-align: center">
                                                    <div class="caption caption-md">
                                                        <span class="caption-subject font-black " style="font-size: 16px">Government of Jharkhand</span>
                                                        <br />
                                                        <span class="caption-subject font-black bold" style="font-size: 20px">OFFICE OF STATE TRANSPORT COMMISIONER</span>
                                                        <br />
                                                        <span class="caption-subject font-black bold" style="font-size: 20px;">STATE TRANSPORT AUTHORITY</span>
                                                        <br />
                                                        <span class="caption-subject font-black" style="font-size: 16px;">F.F.P. Building, HEC Campus, Dhurwa, Ranchi</span>
                                                    </div>
                                                </div>
                                            </div><br />
                                            <div class="portlet-body">
<asp:Panel ID="PanelReplacement" runat="server" Visible="false">
<div class="row">
                                                    <div class="col-md-8">
                                                        No. Pari-Ayu-536/<asp:label font-bold="true" id="lblRepYear" runat="server" text=""></asp:label>,STA <asp:label font-bold="true" id="lblRepRef" runat="server" text=""></asp:label>
                                                
                                                           
                                                    </div>
                                                    <br />
                                                    <div class="col-md-4">Ranchi, dated- <asp:label font-bold="true" id="lblRepDate" runat="server" text=""></asp:label>
                                                        
                                                    </div>
                                                </div><br />
<div class="row">
                                                   
                                                    <div class="col-md-12">
                                                       From,
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Secreatery
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State Transport Authority
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Jharkhand, Ranchi.
                                                
                                                    </div></div><br />
<div class="row">
                                                    <div class="col-md-12">
                                                       To,
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Secreatery
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State Transport Authority
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Jharkhand, Ranchi.
                                                
                                                    </div></div><br /><br />
<div class="row">
                                                    <div class="col-md-12">
                                                        <b>Sub: </b><b>Sub: </b><asp:label Font-Underline="true" font-bold="true" id="lblRepSubject" runat="server" text=""></asp:label>
                                                        <br /><br />
                                                        Sir,
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; I have to say that a <asp:label font-bold="true" id="lblRepPermitType1" runat="server" text=""></asp:label> state carriage permit <asp:label font-bold="true" id="lblRepPermitNo1" runat="server" text=""></asp:label> in respect
                                                        of vehicle no. <asp:label font-bold="true" id="lblRepOldRegNo1" runat="server" text=""></asp:label> of <asp:label font-bold="true" id="lblRepOldModel1" runat="server" text=""></asp:label> model is replaced by Vehicle No.-<asp:label font-bold="true" id="lblRepRegNo1" runat="server" text=""></asp:label> of <asp:label font-bold="true" id="lblRepModel1" runat="server" text=""></asp:label>
                                                         model and accordingly the permit alongwith the necessary documents are sent for necessary correction in your record.

                                                
                                                    </div></div>
</asp:Panel>
<asp:Panel ID="PanelRenewal" runat="server" Visible="false">
<div class="row">
                                                    <div class="col-md-8">
                                                        No. Pari-Ayu-536/<asp:label font-bold="true" id="lblRenYear" runat="server" text=""></asp:label>,STA <asp:label font-bold="true" id="lblRenRef" runat="server" text=""></asp:label>
                                                
                                                           
                                                    </div>
                                                    <br />
                                                    <div class="col-md-4">Ranchi, dated- <asp:label font-bold="true" id="lblRenDate" runat="server" text=""></asp:label>
                                                        
                                                    </div>
                                                </div><br />
<div class="row">
                                                   
                                                    <div class="col-md-12">
                                                       From,
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Secreatery
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State Transport Authority
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Jharkhand, Ranchi.
                                                
                                                    </div></div><br />
<div class="row">
                                                    <div class="col-md-12">
                                                       To,
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Secreatery
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State Transport Authority
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Jharkhand, Ranchi.
                                                
                                                    </div></div><br /><br />
<div class="row">
                                                    <div class="col-md-12">
                                                        <b>Sub: </b><asp:label Font-Underline="true" font-bold="true" id="lblRenSubject" runat="server" text=""></asp:label>
                                                        <br /><br />
                                                        Sir,
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This Authority had issued a <asp:label font-bold="true" id="lblRenPermitType1" runat="server" text=""></asp:label> state carriage permit <asp:label font-bold="true" id="lblRenPermitNo1" runat="server" text=""></asp:label> in favour of
                                                        <asp:label font-bold="true" id="lblRenOwnerName" runat="server" text=""></asp:label> for the route <asp:label font-bold="true" id="lblRenRoute1" runat="server" text=""></asp:label> on vehicle No.-<asp:label font-bold="true" id="lblRenRegNo" runat="server" text=""></asp:label> (<asp:label font-bold="true" id="lblRenModel" runat="server" text=""></asp:label> model)
                                                         valid upto <asp:label font-bold="true" id="lblRenOldValidUpto" runat="server" text=""></asp:label>. Now the said permit has been renewed by this Authority for a further period of <asp:label font-bold="true" id="lblRenDuration" runat="server" text=""></asp:label> i.e. till <asp:label font-bold="true" id="lblRenValidUpto" runat="server" text=""></asp:label>.
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Hence, you are requested to countersign the aforesaid permit and make necessary correction in your records. 
                                                        The permit holder is advised to approach you for the needful.
                                                
                                                    </div></div>
</asp:Panel>
<asp:Panel ID="PanelRouteExtend" runat="server" Visible="false">
<div class="row">
                                                    <div class="col-md-8">
                                                        No. Pari-Ayu-536/<asp:label font-bold="true" id="lblREYear" runat="server" text=""></asp:label>,STA <asp:label font-bold="true" id="lblRERef" runat="server" text=""></asp:label>
                                                
                                                           
                                                    </div>
                                                    <br />
                                                    <div class="col-md-4">Ranchi, dated- <asp:label font-bold="true" id="lblREDate" runat="server" text=""></asp:label>
                                                        
                                                    </div>
                                                </div><br />
<div class="row">
                                                   
                                                    <div class="col-md-12">
                                                       From,
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Secreatery
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State Transport Authority
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Jharkhand, Ranchi.
                                                
                                                    </div></div><br />
<div class="row">
                                                    <div class="col-md-12">
                                                       To,
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The Secreatery
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; State Transport Authority
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Jharkhand, Ranchi.
                                                
                                                    </div></div><br /><br />
<div class="row">
                                                    <div class="col-md-12">
                                                        <b>Sub: </b><asp:label Font-Underline="true" font-bold="true" id="lblRESubject" runat="server" text=""></asp:label>
                                                        <br /><br />
                                                        Sir,
                                                        <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; I am directed to say that the Inter State Route <asp:label font-bold="true" id="lblRERoute1" runat="server" text=""></asp:label> has been extended from  <asp:label font-bold="true" id="lblREStation1" runat="server" text=""></asp:label> on demand of permit holder. 
                                                        This portion extended in the state of Jharkhand through order of Secretary, STA, Jharkhand, Ranchi dated <asp:label font-bold="true" id="lblREApprovedDate" runat="server" text=""></asp:label>. After this extension of route, the route will be known as Inter State route <asp:label font-bold="true" id="lblRERoute2" runat="server" text=""></asp:label> instead of <asp:label font-bold="true" id="lblREStation2" runat="server" text=""></asp:label>.
                                                        
                                                        <br /><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; It is therefore requested that necessary amendment and correction may kindly be made in your official record as well as in the countersignature slip. 
                                                        
                                                
                                                    </div></div>
</asp:Panel>
<br /><br />
<div class="row">
<div class="col-md-9"></div>
<div class="col-md-3" style="text-align:center">
Yours faithfully
<br /> Sd/-
<br />Secreatery
<br />State Transport Authority
<br />Jharkhand, Ranchi.
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
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;






        }
    </script>
    
    <%--<script src="assets/js/STA_PERMIT.js" type="text/javascript"></script>--%>
</asp:Content>

