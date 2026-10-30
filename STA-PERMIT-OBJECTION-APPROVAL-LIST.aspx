<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-PERMIT-OBJECTION-APPROVAL-LIST.aspx.cs" Inherits="STA_PERMIT_OBJECTION_APPROVAL_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-PERMIT-OBJECTION-APPROVAL-LIST.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="12" />

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
                                <span>STA Permit Objection Application - Inter State </span>
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
                                                <span class="caption-subject font-red sbold uppercase">STA Permit Objection Application - Inter State </span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="table-toolbar">
                                                <div class="row">
                                                    
                                                    <label class="col-md-3 control-label">State<span class="required" aria-required="true">*</span></label>
                                                    <label class="col-md-3 control-label">Status<span class="required" aria-required="true">*</span></label>
                                                    <label class="col-md-3 control-label">Objection Type<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-3"></div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-3"><asp:DropDownList ID="ddlDesState" AppendDataBoundItems="true" CssClass="form-control select2" runat="server">
                                                            </asp:DropDownList></div>
        <div class="col-md-3"><asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">All</asp:ListItem>
            <asp:ListItem Value="Applied">
                                                Applied
                                                            </asp:ListItem>
                                                             <asp:ListItem Value="Verified">
                                                Verified
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="Closed">
                                                Closed
                                                            </asp:ListItem>
                                                        </asp:DropDownList></div>
                                                        <div class="col-md-3"><asp:DropDownList ID="ddlType" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">All</asp:ListItem>
                                                             <asp:ListItem Value="Timing Related">
                                                Timing Related
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="Other">
                                                Other
                                                            </asp:ListItem>
                                                           
                                                        </asp:DropDownList></div>
                                                         <div class="col-md-3">
                                                             <div class="btn-group-devided">
                                                                 <asp:Button ID="btnRun"  OnClick="btnRun_Click" runat="server" OnClientClick="javascript:return fnObjectionStatusList();" Text="Run" CssClass="btn green" />
                                                            

                                                        </div>
                                                         </div>
                                                        
                                                    
                                                </div><br /><br />
                                            </div>
                                            <div id="tblISPermit" runat="server" visible="false">
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>Objector's Name</th>
                                                        <th>Objector's Reg. No.</th>
                                                        <th>Objector's Permit No.</th>
                                                        <th>Objector's Contact No.</th>
                                                        <th>Objection on Reg. No.</th>
                                                        <th>Objection on Application No.</th>
                                                        <th>Objectio till</th>
                                                        <th>Fee Status</th>
                                                        <th>Objection Type</th>
                                                        <th>Application No.</th>
                                                        <th>Application Status</th>
                                                        <th>Objection On</th>
                                                        <th>View</th>
                                                        
                                                        

                                                    </tr>
                                                </thead>
                                                <tbody id="tbdInterStatePermitList">
                                                    <div id="FillObjectionDetail" runat="server"></div>
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
    <script src="assets/js/STA-PERMIT-OBJECTION-VERIFY.js" type="text/javascript"></script>
    

</asp:Content>

