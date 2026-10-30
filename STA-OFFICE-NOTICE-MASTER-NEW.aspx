<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-OFFICE-NOTICE-MASTER-NEW.aspx.cs" Inherits="STA_OFFICE_NOTICE_MASTER_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
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
                            <span>Office Notice</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">Office Notice Add/Edit</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                        <!-- BEGIN FORM-->
                                        <form id="formCity" runat="server" action="#" class="form-horizontal" autocomplete="off">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
                                            <asp:HiddenField ID="hdnID" runat ="server" />
                                              <asp:UpdatePanel ID="UPpanel" runat="server">
                                                <contenttemplate>

                            
                                            <asp:Panel CssClass="row" ID="Panel1" runat="server">
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Notice Details <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtNoticeDetails" MaxLength="250" TextMode="MultiLine" CssClass="regtxtnumber form-control" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtNoticeCode" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                                        
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group last">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Start and End Date <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                              <div class="input-group input-large date-picker input-daterange" data-date="10/11/2012" data-date-format="dd/mm/yyyy">
                                                                        <asp:TextBox ID="txtstdt" Width="210px" CssClass="regreadonly form-control" runat="server"></asp:TextBox>
                                                                        <span class="input-group-addon">To </span>
                                                                        <asp:TextBox ID="txtenddt" Width="210px" CssClass="regreadonly form-control" runat="server"></asp:TextBox>
                                                              </div>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group last">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Notice Copy<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                    <table class="table table-striped table-bordered table-hover">
                                                                           <tr>
                                                                            
                                                                                <td>
                                                                                    <asp:FileUpload runat="server" accept="application/pdf, .pdf"   id="uploadNotice" ></asp:FileUpload></td>
                                                                           </tr>
                                                    </table>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group last" id="dvdownload" runat="server">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Notice Copy File Download</label>
                                                    <div class="col-md-5" id="dvlinkdownload" runat="server">
                                                    </div>
                                                    <div class="col-md-3">
                                                        
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-4">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Save" OnClientClick="return fnNoticeValidate()" OnClick="btnSave_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Cancel" OnClick="btnCancel_Click1" />

                                                    </div>
                                                    <div class="col-md-6"></div>
                                                </div>
                                            </div>
                                            </asp:Panel>
                                                </contenttemplate>
                                                <triggers>
                                                    <asp:PostBackTrigger ControlID ="btnSave" />
                                                </triggers>

                                            </asp:UpdatePanel>

                                        </form>
                                        <!-- END FORM-->
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
     <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="assets/global/plugins/moment.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <script src="assets/js/STA-OFFICE-NOTICE-MASTER.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>

   


</asp:Content>

