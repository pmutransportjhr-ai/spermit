<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STATimeTable.aspx.cs" Inherits="STATimeTable" %>

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
                                                <div class="row">
                                                    <div class="col-md-8">
                                                       
                                                           
                                                    </div>
                                                    <div class="col-md-4">Ranchi, dated: - <asp:label font-bold="true" id="lblDate" runat="server" text=""></asp:label>
                                                        
                                                    </div>
                                                </div><br />
                                                 <div class="row">
                                                    <div class="col-md-3"><b>Permit No. : </b></div>
                                                    <div class="col-md-3"><asp:label  id="lblPermitNumber" runat="server" text=""></asp:label></div>
                                                    
                                                    <div class="col-md-3"><b>Vehicle No. : </b></div>
                                                    <div class="col-md-3"><asp:label  id="lblRegNo" runat="server" text=""></asp:label></div>
                                                </div><br />
                                                <div class="row">
                                                    <div class="col-md-3"><b>Name of Permit Holder : </b></div>
                                                    <div class="col-md-9"><asp:label  id="lblName" runat="server" text=""></asp:label></div>
                                                    </div><br />
                                                <div class="row">
                                                    <div class="col-md-3"><b>Route : </b></div>
                                                    <div class="col-md-9"><asp:label  id="lblRoute" runat="server" text=""></asp:label></div>
                                                    </div><br />
                                               
                                                
                                                <div class="row">
                                                    <div class="col-md-12" style="text-align:center">
                                                        <b><asp:label font-bold="true" id="lbltext" runat="server" text=""></asp:label></b>
                                                
                                                    </div></div><br /><br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                      <div id="RouteTimeinfo" runat="server"></div>
                                                    </div></div>
                                                <div class="row">
                                                    <div class="col-md-9"></div>
                                                    <div class="col-md-3" style="text-align:center;font-weight:bold">
                                                         Secreatery
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

