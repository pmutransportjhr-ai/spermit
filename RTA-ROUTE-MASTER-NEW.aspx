<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-ROUTE-MASTER-NEW.aspx.cs" Inherits="RTA_ROUTE_MASTER_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />

    <style>
        .table > tbody > tr > td {
            padding-left: 0px !important;
            padding-right: 10px !important;
            border-top: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/js/RTA-ROUTE-MASTER.js" type="text/javascript"></script>
    <div class="page-container">
        <div class="page-content-wrapper">
            <div class="page-content">
                <div class="container">
                    <ul class="page-breadcrumb breadcrumb">
                        <li>
                            <a href="RTA-DASHBOARD.aspx">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            Master
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Route</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">Route Master Add/Edit</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                        <!-- BEGIN FORM-->
                                        <form id="formState" runat="server" action="#" class="form-horizontal" autocomplete="off">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                            <div class="form-body">
                                                <div class="form-group">
                                                    <div class="col-md-2">
                                                        <asp:HiddenField ID="ID" runat="server" />
                                                    </div>
                                                    <label class="col-md-2 control-label">Source Station <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:UpdatePanel ID="homecitypanel" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlSource" AutoPostBack="false" AppendDataBoundItems="true" CssClass="form-control select2" runat="server">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlSource" />
                                                            </Triggers>

                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                               
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Destination Station <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:UpdatePanel ID="citypanel" runat="server">
                                                            <ContentTemplate>
                                                                <asp:DropDownList ID="ddlDestination" AutoPostBack="false" AppendDataBoundItems="true"  CssClass="form-control select2" runat="server">
                                                                </asp:DropDownList>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlDestination" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <%--<div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    
                                                    <label class="col-md-2 control-label">Route For<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:UpdatePanel ID="countrypanel" runat="server">
                                                       <ContentTemplate >
                                                         <asp:DropDownList ID="ddlRouteFor" AppendDataBoundItems="true" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                          <asp:AsyncPostBackTrigger ControlID ="ddlRouteFor" />
                                                         </Triggers>
                                                     </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>--%>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Total Distance <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtTotalDistance" MaxLength="7" CssClass="numberWithdot form-control" runat="server"></asp:TextBox>
                                                        <%--<asp:TextBox ID="txtRouteId" CssClass="form-control" runat="server" Visible="false" Text="0"></asp:TextBox>--%>
                                                        <asp:HiddenField ID="txtCityCode" runat="server" Value="0" />
                                                        <asp:HiddenField ID="txtRouteId" runat="server" Value="0" />
                                                        <asp:HiddenField ID="txtroutjson" runat="server" />

                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                               
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">No. of Vacancies <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtVacancies" MaxLength="3" CssClass="number form-control" runat="server"></asp:TextBox>


                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                  <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Maximum Number of Trips <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtNumberTrips" MaxLength="2" CssClass="number form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                 <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Route Number <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtRouteNo" MaxLength="80" CssClass="regtxtnumber form-control"  runat="server"></asp:TextBox>
                                </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <%--<div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Status <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">--select status--</asp:ListItem>
                                                            <asp:ListItem Value="Active">
                                                Active
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="Inactive">
                                                Inactive
                                                            </asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>--%>
                                                 <div class="form-group">

                                                    <div class="col-md-2"></div>

                                                    <div class="col-md-7">
                                                        
                                                           
                                                                        <div style="border: 1px solid #60aee4; margin: 5px 0; padding: 5px;">
                                                                            
                                                                            <div class="col-md-5" style="text-align:center;font-weight:bold">RTO <span class="required" aria-required="true">*</span></div>
                                                                            <div class="col-md-5" style="text-align:center;font-weight:bold">Distance Covered in RTO <span class="required" aria-required="true">*</span></div>
                                                                            <div class="col-md-2"></div>
                                                                            <table class="table">

                                                                                <tbody id="tdbViaRTO">
                                                                                    <tr id="trRTO_1">
                                                                                        

                                                                                        <td class="col-md-5" id="tdViaRTO_1">
                                                                                             <select class="form-control" id="ddlRTO_1"></select>
                                         

                                                                                        </td>
                                                                                        <td class="col-md-3" id="tdViaRTODistance_1">
                                                                                             <input type="text" maxlength="7" class="numberWithdot form-control" id="txtRTODistance_1" />
                                         

                                                                                        </td>
                                                                                        <td class="col-md-3" id="tdRTOoperation_1">

                                                                                            <button type="button" class="btn green" onclick="javascript:AddRTORow(); return false;">
                                                                                                <i class="fa fa-plus"></i>
                                                                                            </button>

                                                                                        </td>

                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                   

                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                               
                                                <div class="form-group">

                                                    <div class="col-md-2"></div>

                                                    <div class="col-md-7">
                                                        <div style="border: 1px solid #60aee4; margin: 5px 0; padding: 5px;">
                                                                            

                                                                            <table class="table">

                                                                                <tbody id="tdbViaRoute">
                                                                                    <tr id="trVia_1">
                                                                                        <td class="col-md-3">Via </td>

                                                                                        <td class="col-md-5" id="tdViaCity_1">
                                                                                            <input type="text" maxlength="30" class="regtxt form-control" id="txtCity_1" />

                                                                                        </td>
                                                                                        <td class="col-md-3" id="tdoperation_1">

                                                                                            <button type="button" class="btn green" onclick="javascript:AddRow(); return false;">
                                                                                                <i class="fa fa-plus"></i>
                                                                                            </button>

                                                                                        </td>

                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>

                                                    </div>
                                                    <div class="col-md-2"></div>
                                                </div>

                                               


                                            </div>
                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-4">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Save" OnClick="btnSave_Click" OnClientClick="return SaveRoute()" />
                                                        <%--<button type="button" runat="server" id="btnSave" class="btn green" onclick="javascript:SaveRoute();">Save</button>--%>
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Cancel" OnClick="btnCancel_Click" />

                                                    </div>
                                                    <div class="col-md-6"></div>
                                                </div>
                                            </div>
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
    <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="assets/js/RTA-ROUTE-MASTER.js" type="text/javascript"></script>
</asp:Content>

