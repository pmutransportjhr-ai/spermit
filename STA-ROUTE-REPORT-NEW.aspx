<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="MasterPage.master" CodeFile="STA-ROUTE-REPORT-NEW.aspx.cs" Inherits="STA_ROUTE_REPORT_NEW" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

     <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-ROUTE-REPORT-NEW.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="15" />

    <form runat="server" autocomplete="off">
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
                                <span>Applicant Merit Permit</span>
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
                                                <span class="caption-subject font-red sbold uppercase">Applicant Merit Permit</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                                        <asp:HiddenField ID="hdnID" runat="server" />
                                                        <asp:HiddenField ID="hdnType" runat="server" />
                                                        <asp:HiddenField ID="hdnTypeOptionID" runat="server" />
                                            <div class="table-toolbar">
                                                    
                                            </div>
                                            <div id="tblISPermit" runat="server">
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                       <th>Sr.No</th>
                                                        <th>Name & Address</th>
                                                        <th>Vehicle Detail</th>
                                                        <th>Manf. Year</th>
                                                        <th>AC (2 Marks)</th>
                                                        <th>Delux (2 Marks)</th>
                                                        <th>SC/ST (3 Marks)</th>
                                                        <th>Ex-Army (3 Marks)</th>
                                                        <th>OBC (1 Mark)</th>
                                                        <th>Minority (1 Mark)</th>
                                                        <th>Gender (1 Mark)</th>
                                                        <th>PG/Graduation (3 Marks)</th>
                                                        <th>Intermediate (2 Marks)</th>
                                                        <th>Matric (1 Mark)</th>
                                                        <th>Heavy Motor Licence (2 Marks)</th>
                                                        <th>Handicapped (2 Marks)</th>
                                                        <th>Marks obtained</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tbdInterStatePermitList">
                                                        <div id="FillSTAPermitApplication" runat="server"></div>
                                                </tbody>
                                            </table>
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

        <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title"><b>Are you sure, you want to publish this Application?</b></h4>
                        <br />
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <div class="col-md-4"><span><b>Objection Date</b><span class="required">*</span></span></div>
                            <div class="col-md-8"><asp:TextBox ID="txtObjectionDt" CssClass="regreadonly form-control date-picker" data-date-format="dd/mm/yyyy" runat="server"></asp:TextBox></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="txtDelKey" runat="Server" Value="0" />
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Cancel</button>
                        <%--<asp:Button ID="btnPublish" runat="server" CssClass="btn red" OnClientClick="javascript:return fnValidateObjection();" Text="YES" OnClick="btnPublish_Click" />--%>
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
    <script src="assets/pages/scripts/table-datatables-scroller.js" type="text/javascript"></script>

     <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="assets/global/plugins/moment.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>

</asp:Content>