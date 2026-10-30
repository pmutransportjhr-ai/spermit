<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-FEE-MASTER-NEW.aspx.cs" Inherits="RTA_FEE_MASTER_NEW" %>

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
                                            <span class="caption-subject font-red sbold uppercase">Fee Master Add/Edit</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                        <!-- BEGIN FORM-->
                                        <form id="formState" runat="server" action="#" class="form-horizontal" autocomplete="off">
                                           <asp:HiddenField ID="hdnID" runat="server" />
                                             <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                            <div class="form-body">
                                                
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    
                                                    <label class="col-md-2 control-label">Fee/Tax For<span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:UpdatePanel ID="countrypanel" runat="server">
                                                       <ContentTemplate >
                                                         <asp:DropDownList ID="ddlFeeFor" OnSelectedIndexChanged="ddlFeeFor_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                          <asp:AsyncPostBackTrigger ControlID ="ddlFeeFor" />
                                                         </Triggers>
                                                     </asp:UpdatePanel>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                  <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Fee/Tax Option <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                       <ContentTemplate >
                                                         <asp:DropDownList ID="ddlFeeOption" OnSelectedIndexChanged="ddlFeeOption_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true"  CssClass="form-control select2" runat="server"></asp:DropDownList>
                                                       </ContentTemplate>
                                                        <Triggers>
                                                          <asp:AsyncPostBackTrigger ControlID ="ddlFeeOption" />
                                                         </Triggers>
                                                     </asp:UpdatePanel>
                                                      
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Fee/Tax Service <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                       <ContentTemplate >
                                                                <asp:DropDownList AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFeeService_SelectedIndexChanged" AutoPostBack="true"   ID="ddlFeeService" CssClass="form-control select2"  runat="server"> 
                                                                     
                                                                </asp:DropDownList>
                                                                    </ContentTemplate>
                                                        <Triggers>
                                                          <asp:AsyncPostBackTrigger ControlID ="ddlFeeService" />
                                                         </Triggers>
                                                     </asp:UpdatePanel>                                         
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                  <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Fee Type <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                       <ContentTemplate >
                                                                <asp:DropDownList AppendDataBoundItems="true" ID="ddlFeeType" CssClass="form-control select2"  runat="server"> 
                                                                </asp:DropDownList>
                                                           </ContentTemplate>
                                                        <Triggers>
                                                          <asp:AsyncPostBackTrigger ControlID ="ddlFeeType" />
                                                         </Triggers>
                                                     </asp:UpdatePanel>     
                                                                                                             
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                                
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Fee/ Tax <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:TextBox ID="txtFee" MaxLength="6" CssClass="number form-control" runat="server"></asp:TextBox>


                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                               <%-- <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Status <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control select2" runat="server">
                                                            <asp:ListItem Value="0">--select status--</asp:ListItem>
                                                            <asp:ListItem Value="Active">Active</asp:ListItem>
                                                            <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                 <div class="col-md-3"></div>
                                                    </div>--%>
                                               
                                                <div class="form-group">
                                                    <div class="col-md-2"></div>
                                                    <label class="col-md-2 control-label">Upload Document(PDF Only) <span class="required" aria-required="true">*</span></label>
                                                    <div class="col-md-5">
                                                        <asp:FileUpload ID="pdfupload" accept="application/pdf, .pdf"  runat="server"  class=" btn-file"/>
                                                        <%=strUploadedFile %>
                                                        <asp:label id="lblFileUpload" Visible ="false"  runat="server" text=""></asp:label>                                                           
                                                  
                                                       <br />
                                                 
                                                       
                                                         
                                                    </div>
                                                    
                                                    <div class="col-md-3">
                                                    <input type ="hidden" id="txtUploadFileEdit" runat="server" value="" />
</div>
                                                    
                                                    
                                                </div>
                                         
                                            </div>
                                            <div class="form-actions">
                                                <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-4">
                                                         <asp:Button ID="btnSave" runat="server" CssClass="btn blue" Text="Save" OnClick="btnSave_Click" OnClientClick="return fnFeeValidate()" />
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
     <script src="assets/js/RTA-FEE-MASTER.js" type="text/javascript"></script>
   
</asp:Content>

