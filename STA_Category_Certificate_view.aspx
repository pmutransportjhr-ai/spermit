<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="STA_Category_Certificate_view.aspx.cs" Inherits="STA_Category_Certificate_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <input id="txtScreen" type="text" value="STA-VEHICLE-REPLACE-APPLICATION-VIEW.aspx" style="display: none;" />

   <form id="Form1" runat="server" autocomplete="off">
        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />

        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="STA-DASHBOARD.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>Category Certificate View</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="portlet light portlet-fit">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="icon-settings font-red"></i>
                                                        <span class="caption-subject font-red sbold uppercase">Category Certificate View</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                     <div class="row">
                                            <div class="col-md-12 col-sm-12" style="text-align: right">
                                                <%--<asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="Print" OnClientClick="return printDiv('IssuedReceiptDiv')"  />--%>
                                                <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv(IssuedReceiptDiv)">Print</button>

                                            </div>

                                        </div>
                                    
                                                    <%--    <br />--%>


                                                     <div id="IssuedReceiptDiv">
                                                   <%-- <div class="row">
                                                        <div class="col-md-12 col-sm-12" style="text-align: right">
                                                            <b style="font-size: large">Permit Type : </b>
                                                            <asp:Label ID="lblPermitType" runat="server"></asp:Label>



                                                        </div>
                                                        <div class="col-md-6 col-sm-6">
                                                            <b style="font-size: large">Application No :</b>
                                                            <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-md-6 col-sm-6" style="text-align: right">
                                                            <b style="font-size: large">Application Status : </b>
                                                            <asp:Label ID="lblApplicationStatus" runat="server"></asp:Label>



                                                        </div>--%>
                                                        <br />

                                                        <div class="col-md-6 col-sm-6">
                                                           
                                                        </div>
                                                       <%-- <div class="col-md-6 col-sm-6" style="text-align: right">
                                                            <b style="font-size: large">Permit No : </b>
                                                            <asp:Label ID="lblPermitNo" runat="server"></asp:Label>--%>



                                                       <%-- </div>--%>
                                                        <br />
                                                        <br />

                                                        



                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12">
                                                            <h4><b><u>Category report</u></b></h4>
                                                        </div>
                                                        <div class="col-md-12 col-sm-12">

                                                            <table class="table table-striped table-bordered table-hover">
                                                                <tr>

                                                                    <td><b>Registration No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblRegNumber" runat="server"></asp:Label></td>
                                                                    <td><b>Application Number:</b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblapplicationnumber" runat="server"></asp:Label></td></tr>
                                                                <tr>
                                                                    <td><b>Mobile Number:</b></td>
                                                                    <td><asp:Label ID="lblmobilenumber" runat="server"></asp:Label></td>
                                                                     <td><b>Seating Capacity:</b></td>
                                                                    <td><asp:Label ID="lblseatingcapacity" runat="server"></asp:Label></td></tr>
                                                                <tr>
                                                                     <td><b>Sleeper Capacity:</b></td>
                                                                    <td><asp:Label ID="lblsleeper" runat="server"></asp:Label></td>
                                                                     <td><b>Vehicle Type:</b></td>
                                                                    <td><asp:Label ID="lblcategory" runat="server"></asp:Label></td>
                                                                </tr>
                                                             
                                                               
                                                            </table>



                                                        </div>
                                                    </div>

                                                   
                                                       


                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12">
                                                            <h4><b><u>Uploaded Documents</u></b></h4>
                                                        </div>

                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    <td><b>Category Certificate</b></td>
                                                                    <td>
                                                                        <div id="divcategoryfile" runat="server">Not Uploaded</div>
                                                                    </td>
                                                                   
                                                                </tr>
                                                               
                                                                
                                                            </table>

                                                        </div>
                                                    </div>
                                                    

                                                </div>
                                                    </div>



                                                <div class="row">
                                                    <br />
                                                    <br />
                                                    <div class="col-md-12" style="text-align: center">
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Close" OnClientClick="javascript:window.close();return false;" />

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
    
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
</asp:Content>

