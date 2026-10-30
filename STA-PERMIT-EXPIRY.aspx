<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="STA-PERMIT-EXPIRY.aspx.cs" Inherits="STA_PERMIT_EXPIRY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-PERMIT-EXPIRY.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="7" />

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
                                <span>Permit Expired Report</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">permit Expired Report</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="row">
                                                <div class="col-md-4">Sate</div>
                                                <div class="col-md-4"></div>
                                                <div class="col-md-4"></div>
                                                </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlStateList" runat="server" class="form-control"></asp:DropDownList>
                                                        
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
                                                                <th>State</th>
                                                                <th>Vehicle Number</th>
                                                                <th>Permit Number</th>
                                                                <th>Permit Holder Name</th>
                                                                <th>Validity</th>
                                                                <th>Route </th>
                                                                
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
    <script type="text/javascript">
        $(document).ready(function () {
            
            $('#btRun').on('click', function () {
                var state_code = $('#ContentPlaceHolder1_ddlStateList').val();
           
                var objFilterData = new Object();
                objFilterData.state_code = state_code;
             
                $('#loadingmessage').modal('show');
                $.ajax({
                    type: "POST",
                    url: "STA-PERMIT-EXPIRY.aspx/GetFilteredList",
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

