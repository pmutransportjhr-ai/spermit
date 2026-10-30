<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rtaRevenue.aspx.cs" Inherits="rtaRevenue" %>

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
    <input id="txtScreen" type="text" value="rtaRevenue.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="4" />

    <form runat="server" autocomplete="off">
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
                                <span>RTA Revenue Report</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">RTA Revenue Report</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="row">
                                                <div class="col-md-4">Date Range</div>
                                                <div class="col-md-4"></div>
                                                <div class="col-md-4"></div>
                                                </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                   <div class="input-group input-large date-picker input-daterange" data-date="10/11/2012" data-date-format="dd/mm/yyyy">
                                                                        <asp:TextBox ID="txtFrom" Width="210px" CssClass="regreadonly form-control" runat="server"></asp:TextBox>
                                                                        <span class="input-group-addon">To </span>
                                                                        <asp:TextBox ID="txtTo" Width="210px" CssClass="regreadonly form-control" runat="server"></asp:TextBox>
                                                                    </div>
                                                        
                                                </div>  
                                              
                                                <div class="col-md-4"><input type="button" class="btn btn-info" value="Run" id="btRun" /> </div>  
                                                  <div class="col-md-4">
                                                   
                                                </div>
                                            </div>
                                            <br /><br /><br />

                                            <div class="table-toolbar">
                                                    <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                        <thead>
                                                            <tr>
                                                                <th>RTA Name</th>
                                                                <th>Amount</th>
                                                                <th>Month</th>
                                                                <th>Year</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tbdInterStatePermitList" runat="server">
                                                               
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

    <script type="text/javascript">
        $(document).ready(function () {
            
            $('#btRun').on('click', function () {
                var fromdate = $('input[id*=txtFrom]').val().split('/')[2] + $('input[id*=txtFrom]').val().split('/')[1] + $('input[id*=txtFrom]').val().split('/')[0];
                var todate = $('input[id*=txtTo]').val().split('/')[2] + $('input[id*=txtTo]').val().split('/')[1] + $('input[id*=txtTo]').val().split('/')[0];
           
                var objFilterData = new Object();
                objFilterData.start_date = fromdate;
                objFilterData.todate = todate;
                $('#loadingmessage').modal('show');
                $.ajax({
                    type: "POST",
                    url: "rtaRevenue.aspx/GetFilteredList",
                    data: JSON.stringify({ objFilterData: objFilterData }),
                    cache: false,
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $('#loadingmessage').modal('hide');
                        if (data.d != "") {
                            var table = $('#sample_1').DataTable();
                            table.destroy();

                            $('#ContentPlaceHolder1_tbdInterStatePermitList').html("");
                            $('#ContentPlaceHolder1_tbdInterStatePermitList').html(data.d);
                            TableDatatablesScroller.init();
                                                     
                        }
                        else {
                            alert("Data Not Found.");
                        }
                    },
                    error: function (response) {
                        $('#loadingmessage').modal('hide');
                        alert(response.status + ' ' + response.statusText);
                    }
                });
            });
        });
    </script>
</asp:Content>

