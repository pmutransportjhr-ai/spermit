<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="JHTRANSPORT.master" CodeFile="THIRD-PARTY-VIEW.aspx.cs" Inherits="THIRD_PARTY_VIEW" %>

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
                            <a href="#"></a>
                            <i class="fa fa-circle"></i>
                        </li>
                         <li>
                            <a href="THIRD-PARTY-VIEW.aspx">Third Party View</a>
                        </li>
                       
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">Third Party View</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
                                            <div class="row">
                                                
                                                <div class ="col-md-2"><b>Select STA/RTA</b><span class="red ">*</span></div>
                                                <div class ="col-md-2">
                                                    <asp:DropDownList ID="ddlTA" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="STA">STA</asp:ListItem>
                                                        <asp:ListItem Value="RTA">RTA</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class ="col-md-3"><b>Vehicle Registration No</b><span class="red">*</span></div>
                                                <div class ="col-md-2">
                                                    <asp:TextBox ID="txtVReg" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class ="col-md-1">
                                                    <asp:Button ID="btnSubmit" runat ="server" CssClass="form-control btn-success" Text ="Go" OnClientClick="return fnValidate();" OnClick="btnSubmit_Click" />

                                                </div>

                                                </div>
                                            <div class="row">

                                                <div class="col-md-12" id="dvNoticeDetails" runat="server" style="text-align:center;">
                                                    
                                                   
                                                </div>
                                            </div>
                                           <div id="RouteTimeInfo" runat="server"></div>


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

        function fnValidate() {
            if ($(<%=txtVReg.ClientID%>).val() == "" || $(<%=txtVReg.ClientID%>).val() == null) {
                alert("Please Enter Valid Vehicel Registration Number");
                return false;
            }
            return true;

        }






    </script>
</asp:Content>