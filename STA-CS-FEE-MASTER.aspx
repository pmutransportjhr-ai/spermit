<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-CS-FEE-MASTER.aspx.cs" Inherits="STA_CS_FEE_MASTER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-CS-FEE-MASTER.aspx" style="display: none;" />
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
                                <a href="#">Master</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>Fee/Tax</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">STA Time Table Extension (only for Jharkand permit holders)</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="table-toolbar">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="btn-group-devided">

                                                            <a id="btnAdd" class="btn green" >
                                                                Add New <i class="fa fa-plus"></i>
                                                            </a>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>Fee For</th>
                                                        <th>Fee</th>
                                                        <th>Uploaded Document</th>
                                                                              
                                                        <th>Edit</th>
                                                                </tr>
                                                </thead>
                                                <tbody id="tbdFeeList">
                                                    <%=FillFeeList %>
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
                        <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-blue sbold uppercase">STA Time Table Extension (only for Jharkand permit holders)</span>
                                            </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-2">Fees For <span style="color:red">*</span></div>
                            <div class="col-md-8"><asp:DropDownList ID="ddlFeesFor" runat="server" CssClass ="form-control">
                                <asp:ListItem Value ="0" >--- Please select ---</asp:ListItem>
                                <asp:ListItem Value ="1" >Fees for temporary permit</asp:ListItem>
                                <asp:ListItem Value ="2" >Fees for permanent permit</asp:ListItem>
                                </asp:DropDownList>
                               
                            </div>
                            <div class="col-md-1"></div>

                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-2">Fees <span style="color:red">*</span></div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtFees" runat="server" CssClass ="form-control number"></asp:TextBox>
                             
                            </div>
                            <div class="col-md-1"></div>

                        </div>
                         <br />
                        <div class="row">
                            <div class="col-md-1"></div>
                            <div class="col-md-2">Fees Document </div>
                            <div class="col-md-8">

                               <asp:FileUpload ID="pdfupload" runat="server"  />
                            </div>
                            <div class="col-md-1"></div>

                        </div>
                        <br />
                        <div class="row" style="display:none" id="rowUploadedFile">
                            <div class="col-md-1"></div>
                            <div class="col-md-2">Uploaded Document </div>
                            <div class="col-md-8">
                                <a href='#' target="_blank" id="uploadedFile" > click to download file </a>
                              
                            </div>
                            <div class="col-md-1"></div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="txtModeKey" runat="Server" Value="0" />
                        <asp:HiddenField ID="txtID" runat="Server" Value="0" />

                        <%--<asp:TextBox ID="txtDelKey" CssClass="form-control" runat="server" Text="0"></asp:TextBox>   --%>

                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn red" Text="Submit" OnClick="btnDelete_Click" OnClientClick ="return fnValidate();" />
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnAdd').on('click', function () {
                $('#delete').modal();

            });

            <%--$('#<%=btnDelete.ClientID%>').on('click', function () {
                if(validate())
                {

                }

            });--%>


        });
        function fnValidate()
        {
            var feesFor = $('#<%=ddlFeesFor.ClientID%>').val();
            var fees = $('#<%=txtFees.ClientID%>').val();
            var feesDocument = $('#<%=pdfupload.ClientID%>').val();

            if (feesFor == '0' || feesFor == null) {
                alert("Please select fees for");
                return false;
            }
            if(fees == '' || fees ==  null)
            {
                alert("Please enter fees");
                return false;
            }
            if ($('#<%=txtModeKey.ClientID%>') == "0") {
                if (feesDocument == '' || feesDocument == null) {
                    alert("Please select fees document");
                    return false;
                }
                if (feesDocument != "") {

                    var fileUploadName = $('input[id*=pdfupload]')[0].files[0].name;
                    var filesize = parseInt($('input[id*=pdfupload]')[0].files[0].size / 1024);
                    var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
                    if (fileextension.toString().toUpperCase() != ".PDF") {
                        $('input[id*=pdfupload]').val('');
                        alert("Plesae upload only PDF file for Counter Signature fees Certificate");
                        return false;
                    }
                    if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
                        $('input[id*=pdfupload]').val('');
                        alert("Counter Signature fees Certificate file must be greater than 0 MB and less than 2 MB");

                        return false;
                    }
                }
            }
        }
        function fnEdit(ID) {
            $('#<%=txtModeKey.ClientID%>').val('Edit');
            $('#<%=txtID.ClientID%>').val(ID);
            $('#loadingmessage').modal('show');
            var objFileData = new Object();
            objFileData.ID = ID;
            objFileData.FeeFor = "";
            objFileData.FeeDocument = "";
            objFileData.Fees = "";
            $.ajax({
                type: "POST",
                url: "STA-CS-FEE-MASTER.aspx/GetDetails",
                data: JSON.stringify({ objFileData: objFileData }),
                cache: false,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#loadingmessage').modal('hide');
                                 
                    $('#<%=ddlFeesFor.ClientID%>').val(data.d.FeeFor);
                    $('#<%=txtFees.ClientID%>').val(data.d.Fees);

                    if (data.d.FeeDocument != "JHT/STAUploadedFiles/") {
                        $('#rowUploadedFile').show();
                        $('#uploadedFile').attr('href', data.d.FeeDocument);
                    }
                    else
                    {
                        $('#rowUploadedFile').hide();
                    }



                    $("#delete").modal();
                      

                   

                },
                error: function (response) {
                    $('#loadingmessage').modal('hide');
                    alert(response.status + ' ' + response.statusText);
                }
            });
        }

    </script>



</asp:Content>

