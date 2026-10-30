<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="JHTRANSPORT.master" CodeFile="APPLICATION_NUMBER.aspx.cs" Inherits="APPLICATION_NUMBER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <style>
        .red{
            color:red;
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
                            <a href="PermitHomePage.aspx">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        
                         <li>
                            <a href="APPLICATION_NUMBER.aspx">Know your applicaion number</a>
                        </li>
                       
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">application number </span>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
                                            <asp:ScriptManager ID="sc" runat="server"></asp:ScriptManager>
                                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                            <div class="row">
                                                <div class ="col-md-2"><b>Applied For</b><span class="red ">*</span></div>
                                                
                                                <div class ="col-md-4">
                                                    <asp:DropDownList ID="ddlTA" runat="server" CssClass="form-control" 
                                                        OnSelectedIndexChanged="ddlTA_SelectedIndexChanged" AutoPostBack="true">
                                                         <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="STA">STA</asp:ListItem>
                                                        <asp:ListItem Value="RTA">RTA</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class ="col-md-2"><b>Service</b><span class="red ">*</span></div>
                                                <div class ="col-md-4">
                                                    <asp:DropDownList ID="ddlservice" runat="server" CssClass="form-control">
                                                     
                                                    </asp:DropDownList>
                                                </div>
                                                    

                                                </div>
                                            </ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="ddlTA" />
                           </Triggers>
                           </asp:UpdatePanel>
                                            <br />  
                                            <div class="row" id="divRTAService" style="display:none;">
                                                <div class="col-md-6"></div>
                                                <div class ="col-md-2"><b>Select Type</b><span class="red ">*</span></div>
                                                <div class ="col-md-4">
                                                    <asp:DropDownList ID="ddlServiceType" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Application">Permit Application</asp:ListItem>
                                                        <asp:ListItem Value="Renewal">Renewal</asp:ListItem>
                                                        <asp:ListItem Value="Transfer">Transfer</asp:ListItem>
                                                        <asp:ListItem Value="Duplicate">Duplicate</asp:ListItem>
                                                        <asp:ListItem Value="Replacement">Replacement</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                              <br />  
                                            <div class="row">
                                                <div class ="col-md-2"><b>Registration No</b><span class="red">*</span></div>
                                                <div class ="col-md-4">
                                                    <asp:TextBox ID="txtVReg" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <b>Mobile Number</b>
                                                    <span class="red">*</span>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtMobileNumber" CssClass="form-control number" runat="server" MaxLength="10"></asp:TextBox>
                                                    
                                                </div>
                                          </div>
                                               <br /> 
                                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate> 
                                            <div class="row">
                                                      <div class ="col-md-8"></div>
                                                 <div class ="col-md-1">
                                                    <asp:Button ID="btnSubmit" runat ="server" CssClass="form-control btn-success" 
                                                        Text ="Submit" OnClientClick="return fnValidate();" OnClick="btnSubmit_Click" />
                                                </div>
                                            </div>
                                         <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                </div>
                                            </div>
                                 </ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                           </Triggers>
                           </asp:UpdatePanel>
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

        $('body').on('change', '<%#ddlTA.ClientID%>', function () {

            var ddlTA = $('select[id*=ddlTA]').val();
            
            if (ddlTA == 'RTA') {
                $('#divRTAService').show();

            }
            else {
                $('#divRTAService').hide();
            }

        });
        function fnValidate() {
            var ddlTA = $('select[id*=ddlTA]').val();
            var ddlservice = $('select[id*=ddlservice]').val();
            var txtMobileNumber = $('input[id*=txtMobileNumber]').val();
            var vregNo = $('input[id*=txtVReg ]').val();
            if (ddlTA == '0' || ddlTA == null)
            {
                alert("Please select STA / RTA");
                return false;
            }

            if (ddlservice == '0' || ddlservice == null) {
                alert("Please select service");
                return false;
            }

            if (ddlTA == 'RTA')
            {
                if($('select[id*=ddlServiceType]').val() == "0")
                {
                    alert('Please select RTA service type');
                    return false;
                }
            }

            if (vregNo == "" || vregNo == null) {
                alert("Please enter registration number");
                return false;
            }
            if (txtMobileNumber == '') {
                alert("Please enter registered mobile number");
                return false;
            }
            return true;

        }






    </script>
</asp:Content>