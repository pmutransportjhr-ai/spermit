<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-PERMIT-OBJECTION-VIEW.aspx.cs" Inherits="STA_PERMIT_OBJECTION_VIEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-PERMIT-OBJECTION-VIEW.aspx" style="display: none;" />
    <%--<input type="text" id="txtColCnt" style="display: none;" value="8" />--%>
    
    <form runat="server" autocomplete="off">
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
                                <span>STA Permit Objection Application</span>
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
                                                        <span class="caption-subject font-red sbold uppercase">STA Permit Objection Application</span>
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
                                                        <div class="col-md-6 col-sm-6"><b style="font-size:large">Application No :</b>
                                                                    <asp:Label ID="lblApplicationNo" runat="server"></asp:Label></div>
                                                        <div class="col-md-6 col-sm-6" style="text-align:right"><b style="font-size:large">Application Status :</b>
                                                                    <asp:Label ID="lblApplicationStatus" runat="server"></asp:Label></div><br />
                                                        <div class="col-md-6 col-sm-6">
                                                                    <b  style="font-size:large">Objection on Register Number : </b>
                                                                        <asp:Label ID="lblObjRegNumber" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                         </div>
                                                        <div class="col-md-6 col-sm-6" style="text-align:right">
                                                            <b style="font-size:large">Objection on Application Number :</b>
                                                                    <asp:Label ID="lblObjAppNumber" runat="server"></asp:Label>
                                                                
                                                            </div>
                                                                        <br />
                                                        
                                                        <br /><br />
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Objector's Information</b></u></h4></div>
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                
                                                                <tr>
                                                                <td><b>Name : </b></td>
                                                                <td>
                                                                    <asp:Label ID="VOName" runat="server"></asp:Label></td>
                                                                <td><b>Mobile No. :  </b></td>
                                                                <td>
                                                                    <asp:Label ID="lblMobileNumber" runat="server"></asp:Label></td>

                                                            </tr>
                                                           <tr>
                                                                <td><b>Register No. : </b></td>
                                                                <td>
                                                                    <asp:Label ID="lbRegNo" runat="server"></asp:Label></td>
                                                                <td><b>Permit No. :  </b></td>
                                                                <td>
                                                                    <asp:Label ID="lblPermitNo" runat="server"></asp:Label></td>

                                                            </tr>
                                                           
                                                        </table>

                                                    </div>
                                                </div>

                                               
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Route Details (On which Objection Raised)</u></b></h4>
                                                    </div>

                                                    <asp:Label CssClass="col-md-12 col-sm-12 control-label" Font-Bold="true" ID="RouteHDRInfo" runat="server" />
                                                </div>
                                                 <br />
                                                                Time Format: 24 Hours <br />
                                                <div id="RouteTimeInfo" runat="server"></div>

                                                    <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>objector Route Details</u></b></h4>
                                                    </div>

                                                    <asp:Label CssClass="col-md-12 col-sm-12 control-label" Font-Bold="true" ID="RouteHDRInfo1" runat="server" />
                                                </div>
                                                 <br />
                                                                Time Format: 24 Hours <br />
                                                <div id="RouteTimeInfo1" runat="server"></div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Other Details</u></b></h4>
                                                    </div>
                                                </div>
                                               
                                                

                                                    <div class="row">
                                                    <div class="col-md-3 col-sm-3"><b>Objection Type :</b></div>
                                                    <div class="col-md-9 col-sm-9">
                                                        <asp:Label ID="lblObjectionType" runat="server"></asp:Label>
                                                    </div></div><br />
                                                        <div class="row">
                                                    <div class="col-md-3 col-sm-3"><b>Objector Remarks:</b></div>
                                                    <div class="col-md-9 col-sm-9">
                                                        <asp:Label ID="lblObjectorRemarks" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                                    <div class="row">
                                                        <br /><br />
                                                    <div class="col-md-12" style="text-align:center">
                                                         <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Close" OnClientClick="Javascript: window.close(); return false;"  />

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

