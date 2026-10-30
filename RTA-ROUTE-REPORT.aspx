<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-ROUTE-REPORT.aspx.cs" Inherits="RTA_ROUTE_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <input id="txtScreen" type="text" value="RTA-ROUTE-REPORT.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="5" />
    <div class="page-container">
        <div class="page-content-wrapper">
            <div class="page-content">
                <div class="container">
                    <ul class="page-breadcrumb breadcrumb">
                        <li>
                            <a href="RTA-DASHBOARD.ASPX">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>

                        <li>
                            <span>Applicant Merit Permit</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">Applicant Merit Permit</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                         <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
                                            <asp:HiddenField ID="hdnTypeID" runat="server" />
                                            <asp:HiddenField ID="hdnTypeOptionID" runat="server" />
                                                      
                                                    <div style="text-align: center; font-weight: bold">
                                                        <asp:Label ID="Message" runat="server" Visible="false"> No Route Found</asp:Label></div>
                                                    <div id="RouteDiv" runat="server" visible="false">
                                                       
                                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                                
                                                                    <thead>
                                                                    <tr>
                                                                        <th rowspan="2">Sr No. </th>
                                                                        <th rowspan="2">Route Name </th>
                                                                        <th rowspan="2">Sanctioned Permit </th>
                                                                        <th colspan="2" style="text-align:center">Issued Permit </th>
                                                                        <th rowspan="2">Balance Permit </th>
                                                                        <th rowspan="2">Select Route </th>

                                                                    </tr>
                                                                    <tr>
                                                                        
                                                                        <th>Permanent</th>
                                                                        <th>Temporary</th>
                                                                      
                                                                        

                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tbdAPPlyRouteList">
                                                                    <div id ="FillAPPlyRouteList"  runat="server"></div>
                                                                </tbody>
                                                            </table>
                                                        
                                            </div>

                                        </form>

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="assets/js/RTA_ROUTE_REPORT.js" type="text/javascript"></script>
</asp:Content>

