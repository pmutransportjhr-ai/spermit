<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-STATE-MASTER-NEW.aspx.cs" Inherits="STA_STATE_MASTER_NEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script src="assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="assets/js/STA-STATE-MASTER.js" type="text/javascript"></script>
   
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
                            <span>STATE</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">State Master Add/Edit</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                        <!-- BEGIN FORM-->
                                        <form id="formState" runat="server" action="#" class="form-horizontal" autocomplete="off">
                                             <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                        
                                                <asp:HiddenField ID="hdnID" runat ="server" />
                                            <div class="form-body">

                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">State Name <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtStateName" MaxLength="30" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="txtStateCode" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                                                        <asp:HiddenField ID="txtStatejson" runat="server" />
                                                        <%--<input id="txtStateName" class="form-control" type="text" data-tabindex="2" />
                                                                <input class="form-control" id="txtStateKey" type="text" style="display: none;" />--%>
                                                    </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">State Abbreviation <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtStateAbbr" MaxLength="2" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                        
                                                    </div>
                                                    <div class="col-md-3"></div>

                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Home State</label>
                                                    <div class="col-md-5">
                                                       <asp:CheckBox ID="StateFlag" AutoPostBack="true"  OnCheckedChanged="StateFlag_CheckedChanged" runat="server" />
                                                               
                                                        <%--<input type="checkbox" id="StateFlag" />--%>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                               <%-- <div class="form-group">
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
                                                <div id="divTrips" runat="server">
                                                <div class="form-group last">

                                                    <div class="col-md-2"></div>

                                                    <div class="col-md-7">
                                                        
                                                                        <div style="border: 1px solid #60aee4; margin: 5px 0; padding: 5px;">
                                                                            

                                                                            <table class="table">

                                                                                <tbody id="tdbViaRoute">
                                                                                    <tr id="trVia_1">
                                                                                        <td class="col-md-2">Upto (in Kms)<span class="required" aria-required="true">*</span></td>

                                                                                        <td class="col-md-3" id="tdUpto_1">
                                                                                            <input type="text" maxlength="4" class="number form-control" id="txtUpto_1" />

                                                                                        </td>
                                                                                        <td class="col-md-2">No. of Round Trips<span class="required" aria-required="true">*</span></td>

                                                                                        <td class="col-md-3" id="tdTrip_1">
                                                                                            <input type="text" maxlength="1" class="number form-control" id="txtTrip_1" />

                                                                                        </td>
                                                                                        <td class="col-md-2" id="td1operation_1">

                                                                                            <button type="button" class="btn green" onclick="javascript:AddRow(); ">
                                                                                                <i class="fa fa-plus"></i>
                                                                                            </button>

                                                                                        </td>

                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </div>
                                                                   <%-- </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>--%>

                                                    </div>
                                                    <div class="col-md-2"></div>
                                                </div>
                                                    </div>

                                            </div>
                                                                 
                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-4">
                                                        <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Save" OnClientClick="return SaveState()" OnClick="btnSave_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Cancel" OnClick="btnCancel_Click" />

                                                    </div>
                                                    <div class="col-md-6"></div>
                                                </div>
                                            </div>
                                                                </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="StateFlag" />
                                                                <asp:PostBackTrigger ControlID ="btnSave" />
                                                            </Triggers>
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
    <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="assets/js/STA-STATE-MASTER.js" type="text/javascript"></script>

</asp:Content>

