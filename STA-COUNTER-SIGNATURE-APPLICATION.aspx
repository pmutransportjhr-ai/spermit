<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-COUNTER-SIGNATURE-APPLICATION.aspx.cs" Inherits="STA_COUNTER_SIGNATURE_APPLICATION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-COUNTER SIGNATURE-APPLICATION.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="8" />

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
                                <span>STA Inter State Counter Signature Application</span>
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
                                                <span class="caption-subject font-red sbold uppercase">STA Inter State Counter Signature Application</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="table-toolbar">
                                                <div class="row">
                                                    
                                                    <label class="col-md-3 control-label">State<span class="required" aria-required="true">*</span></label>
                                                    <label class="col-md-3 control-label">Status<span class="required" aria-required="true">*</span></label>
                                                    <label class="col-md-3 control-label">Type<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-3"></div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-3"><asp:DropDownList ID="ddlDesState" AppendDataBoundItems="true" CssClass="form-control select2" runat="server">
                                                            </asp:DropDownList></div>
        <div class="col-md-3"><asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="Verified">
                                                Verified
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="Issued">
                                                Issued
                                                            </asp:ListItem>
                                                        </asp:DropDownList></div>
                                                        <div class="col-md-3"><asp:DropDownList ID="ddlType" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                             <asp:ListItem Value="Permanent">
                                                Permanent
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="Temporary">
                                                Temporary
                                                            </asp:ListItem>
                                                           
                                                        </asp:DropDownList></div>
                                                         <div class="col-md-3">
                                                             <div class="btn-group-devided">
                                                                 <asp:Button ID="btnRun" OnClientClick="return fnCounterSignatureStatusList()" OnClick="btnRun_Click" runat="server" Text="Run" CssClass="btn green" />
                                                            

                                                        </div>
                                                         </div>
                                                        
                                                    
                                                </div><br /><br />
                                            </div>
                                            <div id="tblISPermit" runat="server" visible="false">
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>Name</th>
                                                        <th>Registration No.</th>
                                                        <th>Permit Type</th>
                                                        <th>Route</th>
                                                        <th>Application No.</th>
                                                        <th>Application Status</th>
                                                        <th>Payment Status</th>
                                                        <th>Objection till</th>
                                                        <th>Reference No.</th>
                                                        <th>Valid Upto</th>
                                                        <th>Edit</th>
                                                        

                                                    </tr>
                                                </thead>
                                                <tbody id="tbdInterStatePermitList">
                                                    <div id="FillInterStateCounterSignatureList" runat="server"></div>
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
        

    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-scroller.js" type="text/javascript"></script>
    <script src="assets/js/STA-COUNTER-SIGNATURE-APPLICATION.js" type="text/javascript"></script>


</asp:Content>

