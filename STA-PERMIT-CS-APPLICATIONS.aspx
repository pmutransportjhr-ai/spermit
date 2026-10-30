<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-PERMIT-CS-APPLICATIONS.aspx.cs" Inherits="STA_PERMIT_CS_APPLICATIONS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

     <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />
    <style>
        .borderColor
        {
            border-color : red;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-PERMIT-CS-APPLICATIONS.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="21" />

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
                                <span>STA Time Table Extension</span>
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
                                                        <span class="caption-subject font-red sbold uppercase">STA Time Table Extension</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                    <div class="table-toolbar">
                                                        <div class="row">

                                                            <label class="col-md-3 control-label">State<span class="required" aria-required="true">*</span></label>
                                                            <label class="col-md-3 control-label">Status<span class="required" aria-required="true">*</span></label>
                                                            <%-- <label class="col-md-3 control-label">Type<span class="required" aria-required="true">*</span></label>--%>
                                                            <div class="col-md-3"></div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-3">
                                                                <asp:DropDownList ID="ddlDesState" AppendDataBoundItems="true" CssClass="form-control select2" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server">

                                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                                    <asp:ListItem Value="Applied">
                                                Applied
                                                                    </asp:ListItem>

                                                                    <asp:ListItem Value="Verified">
                                                Verified
                                                                    </asp:ListItem>

                                                                    <asp:ListItem Value="Approved">
                                                Approved
                                                                    </asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <div class="btn-group-devided">
                                                                    <asp:Button ID="btnRun" OnClick="btnRun_Click" OnClientClick="javascript:return fnPermitStatusList();" runat="server" Text="Run" CssClass="btn green" />



                                                                </div>
                                                            </div>
                                                            <div class="col-md-3"></div>


                                                        </div>
                                                    </div>
                                                    <br />

                                                    <div id="tblISPermit" runat="server" visible="true">
                                                        <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                            <thead>
                                                                <tr>
                                                                    <th>Application Number</th>
                                                                    <th>Registration Number</th>
                                                                    <th>Permit Number</th>
                                                                    <th>State</th>
                                                                    <th>Status</th>
                                                                    <th>Pendency </th>
                                                                    <th>Payament Status</th>
                                                                    <th>View</th>
                                                                    <th>Action</th>
                                                                    


                                                                </tr>
                                                            </thead>
                                                            <tbody id="tbdInterStatePermitList">
                                                                <div id="FillSTAPermitApplication" runat="server">
                                                                </div>
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
                        <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                        <h4 class="modal-title"><b>Are you sure, you want to take Action?</b></h4>

                                        <br />
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-4"><span><b>Action</b><span class="required" style="color: red">*</span></span></div>
                                            <div class="col-md-8">
                                                <select id="ddlPermitAction" name="ddlPermitAction"  class="form-control" runat="server">

                                                    <option value="0">--select action--</option>
                                                    <option value="Rejected">Rejected</option>
                                                    <option value="Verified">Verified</option>

                                                </select>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <b>Comments/ Remarks</b><span class="required" style="color: red">*</span>
                                                    
                                                <asp:TextBox ID="txtCommentsCS" runat="server" CssClass ="form-control" TextMode="MultiLine"></asp:TextBox>

                                            </div>
                                        </div>
                                       

                                    </div>
                                    <div class="modal-footer">
                                        <asp:HiddenField ID="txtDelKey" runat="server" />
                                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Cancel</button>
                                        <asp:Button ID="btnAction" runat="server" CssClass="btn red" OnClientClick="javascript:return fnValidateAction();" Text="YES" OnClick="btnAction_Click" />
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>
                         <div class="modal fade" id="modelApprove" tabindex="-1" role="dialog" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                        <h4 class="modal-title"><b>Are you sure, you want to Approve ?</b></h4>

                                        <br />
                                    </div>
                                    <div class="modal-body">
                                        </div>
                                    <div class="modal-footer">
                                        <asp:HiddenField ID="hdnAppApproveID" runat="server" />
                                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Cancel</button>
                                        <asp:Button ID="Button1" runat="server" CssClass="btn red" OnClientClick="javascript:return fnApprovedCS();" Text="YES"  />
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>
                         <div class="modal fade" id="showDetail" tabindex="-1" role="dialog" aria-hidden="true">
                            <div class="modal-dialog ">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                        <h4 class="modal-title"><b>Application Summary</b></h4>

                                        <br />
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-6"><span><b>Application Submit On</b></span></div>
                                            <div class="col-md-6"><span id="applicationSubmitOn"></span></div>

                                        </div>
                                        <br />
                                            <table class="table responsive" >
                                                <tr>
                                                    <td>Counter Signature</td>
                                                    <td><div id="csFileDiv"></div></td>
                                                    <td>Approved Time Table</td><td>  <div id="ttFileDiv"></div></td>
                                                </tr>
                                                <tr>
                                                    <td>Vehicle RC</td>
                                                    <td><div id="rcDiv"></div></td>
                                                    <td>Speed Governor Certificate</td>
                                                    <td><div id="speedGover"></div></td>
                                                </tr>
                                                <tr>
                                                    <td>Insurance Certificate</td>
                                                    <td><div id="dvInsurr"></div></td>
                                                    <td>Pollution Certificate</td>
                                                    <td><div id="dvPollution"></div></td>
                                                </tr>
                                                <tr>
                                                    <td>Fitness Certificate</td>
                                                    <td><div id="dvFitnessCertificate"></div></td>
                                                    <td>Permit Certificate</td>
                                                    <td><div id="dvPermitCertificate"></div></td>
                                                </tr>
                                                <tr>
                                                    <td>Tempo Time Table</td>
                                                    <td><div id="dvTempoTimeTable"></div></td>
                                                    <td>Jharkhand Tax Paid Certificate</td>
                                                    <td><div id="dvjhttaxCopy"></div></td>
                                                </tr>
                                                <tr>
                                                    <td>Insurance Certificate Validity</td>
                                                    <td><span id="InsurranceCertificateValidity"></span></td>
                                                    <td>Pollution Certificate Validity</td>
                                                    <td><span id="PollutionCertificateValidity"></span></td>
                                                </tr>
                                                 <tr>
                                                    <td>Fitness Certificate Validity</td>
                                                    <td><span id="FitnessCertificateValidity"></span></td>
                                                    <td>Jharkhand Tax Token Validity</td>
                                                    <td><span id="JharkhandTaxTokenValidity"></span></td>
                                                </tr>
                                                
                                            </table>
                                        
                                        <br />
                                         <div class="row">
                                          <div class="col-md-12">
                                              Assistant Comments (Dated: <asp:Label ID="lblAssistantDates" runat="server"></asp:Label>)
                                              <asp:TextBox TextMode="MultiLine" runat="server" ID="txtAssistantComments" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                          </div>
                                        </div>
                                        <br />
                                           <div class="row">
                                          <div class="col-md-12">
                                              SO Comments (Dated: <asp:Label ID="lblSODates" runat="server"></asp:Label>)
                                              <asp:TextBox TextMode="MultiLine" runat="server" ID="lblSOCommets" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                          </div>
                                        </div>
                                         <br />
                                           <div class="row">
                                          <div class="col-md-12">
                                              JTC Comments (Dated: <asp:Label ID="lblJTCDates" runat="server"></asp:Label>)
                                              <asp:TextBox TextMode="MultiLine" runat="server" ID="lblJTCCommets" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                          </div>
                                        </div>

                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Cancel</button>
                                       
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
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
        function printObjetionTblDiv(divId) {
            var toPrint;
            toPrint = window.open();
            toPrint.document.write(document.getElementById(divId).innerHTML);
            toPrint.print();
            toPrint.close();

        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            clear();
        });
        

        function fnAction(app_id) {
            $('#<%=txtDelKey.ClientID%>').val(app_id);
            $('#delete').modal('show');
        }

        function fnApprove(App_id) {

            $('#<%=hdnAppApproveID.ClientID%>').val(App_id);
            $('#modelApprove').modal('show');
        }
        function fnApprovedCS() {
            var appID =   $('#<%=hdnAppApproveID.ClientID%>').val();
            $('#loadingmessage').modal('show');


            $.ajax({
                type: "POST",
                url: "api/CS.ashx?caller=CSApprove&q=" + encodeURIComponent(appID),
                data: { APP_ID: appID },
                cache: false,
                async: false,
                success: function (data) {
                    alert("Successfully submitted.");
                    location.href = "STA-DASHBOARD.ASPX";
                    
                },
                error: function (response) {
                    $('#loadingmessage').modal('hide');
                    alert(response.status + ' ' + response.statusText);
                }
            });
        }


       
        function fnView(App_id) {
            
            $('#loadingmessage').modal('show');
            $.ajax({
                type: "POST",
                url: "api/CS.ashx?caller=GetCSDetail&q=" + encodeURIComponent(App_id),
                data: "",
                cache: false,
                async: false,
                success: function (data) {
                    $('#loadingmessage').modal('hide');
                    if (data != "ERROR") {
                        
                        clear();
                        $('#ttFileDiv').html('<a href="download.aspx?path=STAUploadedFiles&filename=' + data[0].APPROVED_TIME_TABLE_FILE + '" target = "_blank" >click to download</a>');
                        $('#csFileDiv').html('<a href="download.aspx?path=STAUploadedFiles&filename=' + data[0].CS_FILE + '" target = "_blank" >click to download</a>');
                        $('#rcDiv').html('<a href="download.aspx?path=STAUploadedFiles&filename=' + data[0].RC + '" target = "_blank" >click to download</a>');
                        $('#speedGover').html('<a href="download.aspx?path=STAUploadedFiles&filename=' + data[0].SPEED_GOVERNER + '" target = "_blank" >click to download</a>');
                        $('#dvInsurr').html('<a href="download.aspx?path=STAUploadedFiles&filename=' + data[0].INSURRANCE + '" target = "_blank" >click to download</a>');
                        $('#dvPollution').html('<a href="download.aspx?path=STAUploadedFiles&filename=' + data[0].POLLUTION_CERTIFICATE + '" target = "_blank" >click to download</a>');
                        $('#dvFitnessCertificate').html('<a href="download.aspx?path=STAUploadedFiles&filename=' + data[0].FITNESS + '" target = "_blank" >click to download</a>');
                        $('#dvPermitCertificate').html('<a href="download.aspx?path=STAUploadedFiles&filename=' + data[0].PERMIT_CERTIFICATE + '" target = "_blank" >click to download</a>');
                        $('#dvTempoTimeTable').html('<a href="download.aspx?path=STAUploadedFiles&filename=' + data[0].TEMPO_TIME_TABLE + '" target = "_blank" >click to download</a>');
                        $('#dvjhttaxCopy').html('<a href="download.aspx?path=STAUploadedFiles&filename=' + data[0].JHT_TAX_CERTIFICATE + '" target = "_blank" >click to download</a>');
                        $('#InsurranceCertificateValidity').text(data[0].INSURRANCE_VALIDITY);
                        $('#PollutionCertificateValidity').text(data[0].POLLUTION_VALIDITY);
                        $('#FitnessCertificateValidity').text(data[0].FITNESS_VALIDITY);
                        $('#JharkhandTaxTokenValidity').text(data[0].JHARKHAND_TAX_TOKEN);
                   


                         if (data[0].ASSISTANT_COMMENTS_DATE != null)
                             $('#<%=lblAssistantDates.ClientID%>').text(data[0].ASSISTANT_COMMENTS_DATE);
                         else
                             $('#<%=lblAssistantDates.ClientID%>').text("Pending");

                        if (data[0].ASSISTANT_COMMENTS != null)
                            $('#<%=txtAssistantComments.ClientID%>').val(data[0].ASSISTANT_COMMENTS);
                        else
                            $('#<%=txtAssistantComments.ClientID%>').val("Pending");

                        if (data[0].SO_COMMENTS_DATE != null)
                            $('#<%=lblSODates.ClientID%>').text(data[0].SO_COMMENTS_DATE);
                        else
                            $('#<%=lblSODates.ClientID%>').text("Pending");

                        if (data[0].SO_COMMENTS != null)
                            $('#<%=lblSOCommets.ClientID%>').val(data[0].SO_COMMENTS);
                        else
                            $('#<%=lblSOCommets.ClientID%>').val("Pending");

                        if (data[0].JTC_COMMENTS_DATE != null)
                            $('#<%=lblJTCDates.ClientID%>').text(data[0].JTC_COMMENTS_DATE);
                        else
                            $('#<%=lblJTCDates.ClientID%>').text("Pending");
                        if (data[0].JTC_COMMENTS != null)
                            $('#<%=lblJTCCommets.ClientID%>').val(data[0].JTC_COMMENTS);
                        else
                            $('#<%=lblJTCCommets.ClientID%>').val("Pending");
                       
                            $('#applicationStatus').text(data[0].APPLICATION_STATUS);

                        $('#applicationSubmitOn').text(data[0].SUBMIT_DATE);

                        
                    }

                },
                error: function (response) {
                    $('#loadingmessage').modal('hide');
                    alert(response.status + ' ' + response.statusText);
                }
            });



            $('#showDetail').modal('show');
        }

        function clear() {

            $('#ttFileDiv').html('');
            $('#csFileDiv').html('');
            $('#<%=lblAssistantDates.ClientID%>').text('');
            $('#<%=txtAssistantComments.ClientID%>').val('');
            $('#<%=lblSODates.ClientID%>').text('');
            $('#<%=lblSOCommets.ClientID%>').val('');
            $('#<%=lblJTCDates.ClientID%>').text('');
            $('#<%=lblJTCCommets.ClientID%>').val('');
            $('#applicationSubmitOn').text('');
            $('#<%=ddlPermitAction.ClientID%>').val();
            $('#<%=txtCommentsCS.ClientID%>').val();
        }

        function fnValidateAction()
        {
           var ddlSelected = $('#<%=ddlPermitAction.ClientID%>').val();
            var txtComments = $('#<%=txtCommentsCS.ClientID%>').val();
            if(ddlSelected == '0')
            {
                alert('Please select Action');
                return false;
            }
            if(txtComments == '')
            {

                alert('Please Enter Comments');
                return false;
            }
            
            return true;




        }
    </script>
</asp:Content>


