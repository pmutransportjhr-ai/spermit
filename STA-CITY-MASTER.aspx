<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-CITY-MASTER.aspx.cs" Inherits="STA_CITY_MASTER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-CITY-MASTER.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="2" />

    <form runat="server" autocomplete="off">
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="STA-DASHBOARD.aspx">HOME</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="#">MASTER</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>CITY</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">City Master</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="table-toolbar">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="btn-group-devided">

                                                            <button id="btnAdd" runat="server" class="btn green" onserverclick="btnAdd_Click">
                                                                Add New <i class="fa fa-plus"></i>
                                                            </button>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>State</th>
                                                        <th>City</th>
                                                       <%-- <th>Active / Inactive</th>--%>
                                                        <th>Edit</th>
                                                        <th>Delete</th>

                                                    </tr>
                                                </thead>
                                                <tbody id="tbdCityList">
                                                  <div id="FillCityList"  runat="server"></div>
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
        <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title"><b>Are you sure, you want to delete this City?</b></h4>
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="txtDelKey" runat="Server" Value="0" />
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">NO</button>
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn red" Text="YES" OnClick="btnDelete_Click" />
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
    <script src="assets/js/STA-CITY-MASTER.js" type="text/javascript"></script>


</asp:Content>

