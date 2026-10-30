<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-TIME-TABLE.aspx.cs" Inherits="STA_TIME_TABLE" %>

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
                                <a href="PermitHomePage.aspx">Home</a>
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
                                            <div class="portlet-title" style="border-bottom: 0px solid #eee;">
                                                 <div class="col-md-2 col-sm-2" style="text-align: center">
                                                       <asp:PlaceHolder ID="plBarCode" runat="server" /></div>
                                                 </div>
                                                <div class="col-md-10 col-sm-10" style="text-align: center">
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
                                           <br />
                                            <div class="portlet-body">
                                                <table style="width:100%">
                                                     <tr>
                                                    <td style="width:65%">
                                                       
                                                        Pari-Ayu-&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        ............................../STA   
                                                    </td>
                                                    <td style="width:35%">Ranchi, dated: - <asp:label font-bold="true" id="lblDate" runat="server" text=""></asp:label>
                                                        
                                                    </td></tr>
                                                </table>
                                                 
                                                <table style="width:100%">
                                                    <tr>
                                                    <td style="width:28%"><b>Name of Permit Holder</b></td>
                                                    <td style="width:4%"><b>:</b></td>
                                                    <td style="width:68%"><asp:label  id="lblName" runat="server" text=""></asp:label></td>
                                                    </tr>
                                                    <tr>
                                                    <td style="width:28%"><b>Route</b></td>
                                                    <td style="width:4%"><b>:</b></td>
                                                    <td style="width:68%"><asp:label  id="lblRoute" runat="server" text=""></asp:label></td>
                                                   </tr><tr>
                                                    <td style="width:28%"><b>Permit No.</b></td>
                                                   <td style="width:4%"><b>:</b></td>
                                                    <td style="width:68%"><asp:label  id="lblPermitNumber" runat="server" text=""></asp:label></td>
                                                     </tr><tr>
                                                    <td style="width:28%"><b>Vehicle No.</b></td>
                                                   <td style="width:4%"><b>:</b></td>
                                                    <td style="width:68%"><asp:label  id="lblRegNo" runat="server" text=""></asp:label></td>
                                                </tr>
                                                        </table><br />
                                                
                                                <div class="row">
                                                    <div class="col-md-12" style="text-align:center">
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
                                                         Secretary
                                                        <br />State Transport Authority
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
            document.body.innerHTML = "<html><body>" + printContents + "</body></html>";
            window.print();
            document.body.innerHTML = originalContents;






        }
    </script>
    
    <%--<script src="assets/js/STA_PERMIT.js" type="text/javascript"></script>--%>
</asp:Content>

