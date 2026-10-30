<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-ROUTE-MASTER.aspx.cs" Inherits="STA_ROUTE_MASTER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="STA-ROUTE-MASTER.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="6" />

    <form runat="server">
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
                                <span>ROUTE</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">Route Master</span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            
                                             <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                            <asp:UpdatePanel ID="statepanel" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-2" style="text-align:right"><b>State <span class="required" aria-required="true">*</span></b></div>
                                                        <div class="col-md-3">
                                                       <asp:DropDownList ID="ddlDesState" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDesState_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                                            </asp:DropDownList>


                                                        </div>
                                                        <div class="col-md-5"></div>
                                                    </div>
                                                    <br />
                                                    <div style="text-align: center; font-weight: bold">
                                                        <asp:Label ID="Message" runat="server" Visible="false"> No Route Found</asp:Label></div>
                                                    <div class="form-group" id="RouteDiv" runat="server" visible="false">
<div class="table-toolbar">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="btn-group-devided">

                                                            <button id="btnAdd" runat="server" class="btn green" onserverclick="btnAdd_Click">
                                                                Add New <i class="fa fa-plus"></i>
                                                            </button>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>Route Number</th>                                                        
                                                        <th>Source Station</th>
                                                        <th>Destination Station</th>
                                                        <th>Total Distance(KM)</th>
                                                        <th>Distance in Home State(KM)</th>
                                                        <th>Number of Vacancies</th>
                                                        <%--<th>Active / Inactive</th>--%>
                                                        <th>Update Vacancy and Route Name</th>
                                                        <th>Edit</th>
                                                        <th>Delete</th>

                                                    </tr>
                                                </thead>
                                                <tbody id="tbdRouteList" runat="server">
                                                    <%--<tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>--%>
                                                    <%=StrContainer %>
                                                </tbody>
                                            </table>
                                                         </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlDesState" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <br /><br />
                                            <%--<div id="Pannel1" runat="server" style="display:none">
                                                <div class="row">
                                                 <div class="col-md-3 col-sm-3">
                                                <div class="form-group">
                                                    <label for="form_control"><b>Update No. of Vacancies: </b></label>
                                                </div>
                                            </div>
                                            <div class="col-md-4 col-sm-4">
                                                <div class="form-group">
                                                    <asp:HiddenField ID="hdUv" runat="server" />
                                                    <asp:TextBox ID="txtUpdateVacancies" CssClass="number form-control"  runat="server"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                                    <div class="col-md-5 col-sm-5">
                                                <div class="form-group">
                                                    <a  id="btnUpdate"  href="javascript:fnbtnUpdate();" class="btn red">Update</a>
                                                    
                                                </div>
                                            </div>
                                                    </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="VacancyRoute" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title"><b>Update Vacancy and Route Number</b></h4>
                        <br />
                    </div>
                    <div class="modal-body">
                           <div class="row">
                            <div class="col-md-4"><span><b>Update No. of Vacancies:</b><span class="required">*</span></span></div>
                            <div class="col-md-8">
                                    <asp:TextBox ID="txtUpdateVacancies" CssClass="number form-control"  runat="server"></asp:TextBox>
                                </div>
                               </div><br /><br />
                         <div class="row">
                                      
                           
                            <div class="col-md-4"><span><b>Update Route Number:</b><span class="required">*</span></span></div>
                            <div class="col-md-8">
                                
                                <asp:TextBox ID="txtRouteNo" MaxLength="80" CssClass="regtxtnumber form-control"  runat="server"></asp:TextBox>
                                </div>
                           
                  

                              
                            
                </div>
                           
                              
                            
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="hdUv" runat="server" />
                        <asp:HiddenField ID="HiddenRouteNo" runat="server" />
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnUpdateYes" runat="server" OnClientClick="javascript:return fnbtnUpdate();" CssClass="btn red" Text="Update" OnClick="btnUpdateYes_Click"/>
                        <%--<a  id="btnUpdate"  href="javascript:fnbtnUpdate();" class="btn red">Update</a>--%>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
        <%--<div class="modal fade" id="update" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title"><b>Are you sure, you want to update the no. of vacany?</b></h4>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">NO</button>
                        <asp:Button ID="btnUpdateYes" runat="server" CssClass="btn red" Text="YES" OnClick="btnUpdateYes_Click"/>
                    </div>
                </div>
               
            </div>
           
        </div>--%>
        <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                        <h4 class="modal-title"><b>Are you sure, you want to delete this Route?</b></h4>
                    </div>
                    <div class="modal-footer">
                        <asp:HiddenField ID="txtDelKey" runat="Server" Value="0" />
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">NO</button>
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn red" Text="YES" OnClick="btnDelete_Click" />
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
    <script src="assets/js/STA-ROUTE-MASTER.js" type="text/javascript"></script>

</asp:Content>

