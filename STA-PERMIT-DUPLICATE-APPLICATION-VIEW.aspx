<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-PERMIT-DUPLICATE-APPLICATION-VIEW.aspx.cs" Inherits="STA_PERMIT_DUPLICATE_APPLICATION_VIEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-PERMIT-DUPLICATE-APPLICATION-VIEW.aspx" style="display: none;" />
    <%--<input type="text" id="txtColCnt" style="display: none;" value="8" />--%>

    <form runat="server" autocomplete="off">
        <asp:HiddenField ID="hdnID" runat ="server" />
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
                                <span>Duplicate of Permit Application</span>
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
                                                        <span class="caption-subject font-red sbold uppercase">Duplicate of Permit Application</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                   <div class="row">
                                            <div class="col-md-12 col-sm-12" style="text-align: right">
                                                <%--<asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="Print" OnClientClick="return printDiv('IssuedReceiptDiv')"  />--%>
                                                <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv(IssuedReceiptDiv)">Print</button>

                                            </div>

                                        </div>
                                        <div id="IssuedReceiptDiv">
                                                    <div class="row">
                                                       <div class="col-md-6 col-sm-6">
                                                                <b style="font-size:large">Application No :</b>
                                                                    <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                            </div>
                                                                        <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                    <b  style="font-size:large">Application Status Type : </b>
                                                                        <asp:Label ID="lblApplicationStatus" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                            </div><br />
                                                        <div class="col-md-6 col-sm-6">
                                                                <b style="font-size:large">Permit No :</b>
                                                                    <asp:Label ID="lblPermitNo" runat="server"></asp:Label>
                                                            </div>
                                                                        <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                    <b  style="font-size:large">Permit Type : </b>
                                                                        <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                            </div><br /><br />
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                
                                                               <tr>
                                                                    <td><b>Name : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOName" runat="server"></asp:Label></td>
                                                                     <td><b>Registration No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VORegNumber" runat="server"></asp:Label></td>

                                                                </tr>
                                                                <tr>
                                                               <td><b>Mobile No: </b></td>
                                                                    <td><asp:Label ID="lblMobileNo" runat="server"></asp:Label></td>
                                                                 <td><b>Email ID: </b></td>
                                                                   <td><asp:Label ID="lblEmailId"  runat="server"></asp:Label></td>
                                                                               
                                                                   
                                                                </tr>
                                                               

                                                            </table>

                                                        </div>
                                                    </div>
                                                   
                                                   
                                                    
                                               
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Duplicate of Permit Fee Details</b></u></h4></div>
                                                        <div class="col-md-3 col-sm-3"><b>Duplicate of Permit Fees :</b></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblPermitFees" runat="server"></asp:Label></div>
                                                        <div class="col-md-3 col-sm-3"><b>Payment Status:</b></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblPaymentStatus" runat="server"></asp:Label></div>
                                                    </div>
                                                  
                                                    
                                                    
                                                      <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Uploaded Documents</u></b></h4>
                                                    </div>

                                                    <div class="col-md-12 col-sm-12">
                                                        <table class="table table-striped table-bordered table-hover">
                                                            <tr>
                                                                <td><b>Registration Certificate</b></td>
                                                                <td>
                                                                    <div id="divrcfile" runat="server">Not Uploaded</div>
                                                                </td>
                                                                 <td><b>Speed Government Certificate  </b></td>
                                                                    <td>
                                                                        <div id="divspeedfile" runat="server">Not Uploaded</div>
                                                                          
                                                                   </td>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Insurance Certificate</b></td>
                                                                <td>
                                                                    <div id="divinsurFile" runat="server">Not Uploaded</div>


                                                                </td>
                                                                <td><b>Insurance Certificate Valid Upto </b></td>
                                                                <td>
                                                                    <asp:Label ID="lbICValidUpto" runat="server"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Pollution Certificate</b></td>
                                                                <td>
                                                                    <div id="divrollution" runat="server">Not Uploaded</div>

                                                                </td>
                                                                <td><b>Pollution Certificate Valid Upto </b></td>
                                                                <td>
                                                                    <asp:Label ID="lbPCValidUpto" runat="server"></asp:Label>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><b>Fitness Certificate</b></td>
                                                                <td>
                                                                    <div id="divfitness" runat="server">Not Uploaded</div>


                                                                </td>
                                                                <td><b>Fitness  Certificate Valid Upto </b></td>
                                                                <td>
                                                                    <asp:Label ID="lbFCValidUpto" runat="server"></asp:Label>

                                                                </td>

                                                            </tr>
                                                             <tr>
                                                                <td><b>Permit Certificate</b></td>
                                                                <td>
                                                                    <div id="divPermit" runat="server">Not Uploaded</div>


                                                                </td>
                                                                <td><b>Counter Signature Certificate </b></td>
                                                                <td>
                                                                  <div id="divCS" runat="server">Not Uploaded</div>

                                                                </td>

                                                            </tr>
                                                               
                                                             
                                                                
                                                        </table>

                                                    </div>
                                                </div><br />
                                                     <div class="row" id="RejComments" runat="server" visible="false">
                                                    <div class="col-md-3 col-sm-3"><b>Rejected Comment :</b></div>
                                                    <div class="col-md-9 col-sm-9">
                                                        <asp:Label ID="lblRejectedComments" runat="server"></asp:Label>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                                    <div class="row">
                                                        <br /><br />
                                                    <div class="col-md-12" style="text-align:center">
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Close" OnClientClick="javascript:window.close();return false;"  />

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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
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

