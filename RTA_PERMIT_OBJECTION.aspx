<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="RTA_PERMIT_OBJECTION.aspx.cs" Inherits="RTA_PERMIT_OBJECTION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
    <asp:HiddenField ID="hdnID" runat="server" />
    <asp:HiddenField ID="hdnTableType" runat="server" />
    <asp:HiddenField ID ="hdnTypeOptionID" runat="server" />
    <asp:HiddenField ID="hdnPermitTypeID" runat="server" />
<asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                         <div class="page-container">
        <div class="page-content-wrapper">
            <div class="page-content">
                <div class="container">
                    <ul class="page-breadcrumb breadcrumb">
                        <li>
                            <a href="PermitHomePage.aspx">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>

                        <li>
                            <span id="PageHeading" runat="server"></span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase" id="PageHeading1" runat="server"></span>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                            
                                                   
                                                    <div class="row">
                                                        <asp:Label CssClass="col-md-12" Font-Bold="True"  ID="Message" runat="server" Visible="False"></asp:Label>
                                                    </div><br />
                                                    
                                                        
                                                <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            <tr>
                                                                                 <td><b>Permit Type : </b></td>
                                                                                <td>  <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lbPermitId" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td><b>Application Number : </b></td>
                                                                                <td><asp:Label ID="lblAppNo" runat="server"></asp:Label>
                                                                                    <asp:Label ID="lblObjTill" runat="server" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblRTOCode" runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                 <td><b> Name : </b></td>
                                                                                <td>
                                                                                    <asp:Label ID="VOName" runat="server"></asp:Label></td>
                                                                                <td><b>C/O : </b></td>
                                                                                <td> <asp:Label ID="VOCareOf" runat="server"></asp:Label></td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                                 <td><b>Vehicle Registration No : </b></td><td><asp:Label ID="VORegNumber" runat="server"></asp:Label></td>
                                                                                 <td></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            
                                                                            
                                                                        </table>
                                                                      
                                                                    </div>
                                                                </div>
                                                            </div>
                                        
                                       
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Route Details
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="expand"> </a>
                                                                </div>
                                                            </div>
                                                          
                                                            <div class="portlet-body portlet-collapsed">
                                                            <br />
                                                                <div class="row">
                                                                <asp:Label CssClass="col-md-12" ID="RouteHDRInfo" runat="server"></asp:Label>
                                                                    
                                                                </div><br />
                                                                <div id="RouteTimeInfo" runat="server"></div>
                                                               
                                                            </div>
                                                        </div>
                                            <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption" >
                                                                    Fee Details
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="expand"> </a>
                                                                </div>
                                                            </div>
                                                          
                                                            <div class="portlet-body portlet-collapsed">
                                                             <div class="row">
                                                                    <div class="col-md-3 col-sm-3"><b>Objction Fees :</b></div>
                                                                    <div class="col-md-3 col-sm-3"><asp:Label ID="lblObjectionFees" runat="server"></asp:Label></div>
                                                                    <div class="col-md-3 col-sm-3"></div>
                                                                    <div class="col-md-3 col-sm-3"></div>
                                                            </div>
                                              </div>
                                                </div>
                                        <div class="portlet box green">
                                            <div class="portlet-title">
                                                <div class="caption">
                                                  Raise  Objection
                                                </div>
                                                <%-- <div class="tools">
                                                                    <a href="javascript:;" class="expand"> </a>
                                                                </div>--%>
                                            </div>

                                            <div class="portlet-body">
                                               
                                                <div class="row">
                                                    <div class="col-md-2">
                                                         Type : 
                                                         <asp:DropDownList ID="objector_type" runat="server">
                                                             <asp:ListItem Selected="True" Value="RTA">RTA Permit</asp:ListItem>
                                                             <asp:ListItem Value="STA">STA Permit</asp:ListItem>
                                                             <asp:ListItem Value="CS">Conter Signature</asp:ListItem>
                                                         </asp:DropDownList>
                                                      </div>
                                                    <div class="col-md-2">
                                                        <label class="control-label">Objector's Vehicle Reg. No. :</label><span style="color: red">*</span></div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtObjRegNo" CssClass="form-control" runat="server"></asp:TextBox></div>
                                                    <div class="col-md-2">
                                                        <label class="control-label">Permit No. :</label><span style="color: red">*</span></div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtObjPermitNo" CssClass="form-control" runat="server"></asp:TextBox></div>
                                                    <div class="col-md-2">
                                                        <asp:Button ID="btnGO" runat="server" CssClass="btn green" OnClick="btnGO_Click" OnClientClick="return fnObjectionGoValidate()" Text="GO" />
                                                        &nbsp;&nbsp;<asp:Button ID="btnclear" runat="server" CssClass="btn default" OnClick="btnclear_Click" Text="Clear" />
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lblObjectorMsg" Font-Bold="true" Visible="false" runat="server"></asp:Label></div>
                                                </div>
                                                <div id="divObjSubmit" runat="server" visible="false">
                                                     <div class="row">                                    
                                                                                                             

                                                    <div class="col-md-2">
                                                        <label class="control-label">Objection Type:</label><span style="color: red">*</span></div>
                                                    <div class="col-md-3">
                                                        <asp:DropDownList ID="ddlObjType" CssClass="form-control select2" runat="server">

                                                            <asp:ListItem Value="0">select</asp:ListItem>
                                                            <asp:ListItem Value="TIMING RELATED">
                                                                                                                TIMING RELATED
                                                            </asp:ListItem>
                                                            <asp:ListItem Value="OTHER">
                                                OTHER
                                                            </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-7">
                                                         <asp:Label ID="lblObjectorName" Font-Bold="true" Visible="false" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <br />
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Objector's Remarks</label><span style="color: red">*</span></div>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtObjectionRemarks" CssClass="form-control" TextMode="Multiline" runat="server"></asp:TextBox>
                                                            <br />
                                                            <asp:Label ID="lblObjectorMobileNo" Visible="false" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                    </div>
                                                    <br />
                                                     <div class="row">
                                                    <div class="col-md-4"></div>
                                                    <div class="col-md-4">
                                                        <div style="text-align: center">
                                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" OnClientClick="return fnObjectionValidate()" CssClass="btn green" Text="Submit" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
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
    <script src="assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
    <script src="assets/js/RTA_PERMIT_OBJECTION.js" type="text/javascript"></script>
</asp:Content>

