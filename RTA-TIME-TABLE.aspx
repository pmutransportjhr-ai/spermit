<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-TIME-TABLE.aspx.cs" Inherits="RTA_TIME_TABLE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style type="text/css" media="print">
        @media print
      {
         @page {
           margin-top: 10px;
           margin-bottom: 0;
         }
         
      } 
       
             table{border-collapse: separate;
    border-spacing: 0 0.5em;
    font-size:14px;

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
                                <a href="RTA-DASHBOARD.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>Time Table
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
                                                        <span class="caption-subject font-black " style="font-size: 16px">Government of Jharkhand</span>
                                                        <br />
                                                        <span class="caption-subject font-black bold" style="font-size: 20px"><asp:label id="lblregion" runat="server" text=""></asp:label></span>
                                                        <br />                                                       
                                                        <span class="caption-subject font-black" style="font-size: 16px;"><asp:label id="lblAddr" runat="server" text=""></asp:label></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="portlet-body">
                                               <table style="width:100%">
                                                     <tr>
                                                    <td style="width:65%">
                                                       
                                                        RTA -&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        ............................. 
                                                    </td>
                                                    <td style="width:35%">dated: - <asp:label font-bold="true" id="lblDate" runat="server" text=""></asp:label>
                                                        
                                                    </td></tr>
                                                </table>
                                               <table style="width:100%">
                                                    <tr>
                                                    <td style="width:28%">Name of Permit Holder</td>
                                                    <td style="width:4%"><b>:</b></td>
                                                    <td style="width:68%"><asp:label Font-Bold="true" id="lblName" runat="server" text=""></asp:label></td>
                                                    </tr>
                                                    <tr>
                                                    <td style="width:28%">Route</td>
                                                    <td style="width:4%"><b>:</b></td>
                                                    <td style="width:68%"><asp:label  Font-Bold="true" id="lblRoute" runat="server" text=""></asp:label></td>
                                                   </tr><tr>
                                                    <td style="width:28%">Permit No.</td>
                                                   <td style="width:4%"><b>:</b></td>
                                                    <td style="width:68%"><asp:label  Font-Bold="true"  id="lblPermitNumber" runat="server" text=""></asp:label></td>
                                                     </tr><tr>
                                                    <td style="width:28%">Vehicle No.</td>
                                                   <td style="width:4%"><b>:</b></td>
                                                    <td style="width:68%"><asp:label  Font-Bold="true" id="lblRegNo" runat="server" text=""></asp:label></td>
                                                </tr>
                                                        </table><br />
                                                        </div>
                                                
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <b><asp:label font-bold="true" id="lbltext" runat="server" text=""></asp:label></b>
                                                
                                                    </div></div><br /><br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                      <div id="RouteTimeinfo" runat="server"></div>
                                                    </div></div>
                                                <table style="width:100%">
                                                     <tr>
                                                    <td style="width:65%">
                                                        
                                                   </td>
                                                    <td style="width:35%; text-align:center; font-weight:bold"> 
                                                         <br /><br />Secretary                                                        
                                                        <br /><asp:label id="lblregion1" runat="server" text=""></asp:label>



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
            //width = width + 50;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;






        }
    </script>
    
    <%--<script src="assets/js/STA_PERMIT.js" type="text/javascript"></script>--%>
</asp:Content>

