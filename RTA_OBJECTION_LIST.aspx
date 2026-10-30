<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RTA_OBJECTION_LIST.aspx.cs" MasterPageFile="JHTRANSPORT.master" Inherits="RTA_OBJECTION_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
    <asp:HiddenField ID="hdnTypeID" runat="server" />
    <asp:HiddenField ID ="hdnTypeOptionID" runat="server" />                                   
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

                        <li>
                            <span>List of Published Applications (RTA)</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">List of Published Applications (RTA)</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                            
                                                   
                                                    <div class="row">
                                                        <asp:Label CssClass="col-md-12" Font-Bold="True"  ID="Message" runat="server">Please click on Registration Number to raise an objection..</asp:Label>
                                                    </div><br />
                                                    
                                                        <div class="row">
                                                        <div class="col-md-12">
                                                            <table class="table table-striped table-hover table-bordered" id="RouteTable">
                                                                <thead>
                                                                    <tr>
                                                                        <th>S. No. </th>
                                                                        <th>Reg. No. </th>
                                                                        <th>Application Number </th>
                                                                        <th>Owner Name</th>
                                                                        <th>Permit Type </th>
                                                                        <th>Route Desc</th>
                                                                        <th>Objection till date</th>
                                                                        <th>Total Objection</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tbdObjectionList">

                                                                    <%=FillObjectionList %>
                                                                </tbody>
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
    </div>
</form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    
</asp:Content>
