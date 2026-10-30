<%@ Page Title="" Language="C#" MasterPageFile="~/RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA_RESION_CS_ENTRY.aspx.cs" Inherits="RTA_RESION_CS_ENTRY" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            <a href="mobile_update.aspx">RTA CS UPDATE</a>
                        </li>
                       
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">RTA CS UPDATE</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body" style="height:500px;">
                                        <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
                                            <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label class="control-label">Reg. No:</label><span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtRegistration" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Permit No.:</label><span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtPermit" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="btn-success" OnClick="btnGo_Click"/>
                                                        </div>
                                                    </div>    
                                                <hr />
                                                    <br />
                                                </div>
                                            <div id="divdetail" runat="server" class="col-md-12">
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label class="control-label">APP ID : </label>
                                                            <span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtAppID" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Application No. : </label>
                                                            <span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtApplication" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label class="control-label">RTO. : </label>
                                                            <span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlRTO" runat="server" CssClass="form-control">
                                                                                        <asp:ListItem Value="0">--Select RTO--</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Ranchi</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Hazaribagh</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Palamu</asp:ListItem>
                                                                                        <asp:ListItem Value="4">Dumka</asp:ListItem>
                                                                                        <asp:ListItem Value="5">Kolhan</asp:ListItem>
                                                                                   </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Status : </label>
                                                            <span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-2">
                                                            <label class="control-label">CS Status : </label>
                                                            <span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtCSStatus" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Approved On: </label>
                                                            <span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        
                                                        <div class="col-md-2">
                                                            <label class="control-label">Modified by: </label>
                                                            <span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtModifiedby" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-2">
                                                            <label class="control-label">Modified on: </label>
                                                            <span style="color: red">*</span>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:TextBox ID="txtmodifiedon" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-10"></div>
                                                        <div class="col-md-2">
                                                            <asp:Button Text="Save" CssClass="btn btn-primary" ID="btnSave"  runat="server" OnClick="btnSave_Click" />
                                                        </div>
                                                    </div>
                                                    
                                                    
                                                
                                                
                                            </div>
                                       </form>
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
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        <%--function fnValidate() {
            if ($(<%=txtVReg.ClientID%>).val() == "" || $(<%=txtVReg.ClientID%>).val() == null) {
                alert("Please Enter Valid Vehicel Registration Number");
                return false;
            }
            return true;

        }--%>






    </script>
</asp:Content>



