<%@ Page Title="" Language="C#" MasterPageFile="~/RTAMasterPage.master" AutoEventWireup="true" CodeFile="SCREEN_RTA_SCHOOL_BUS_PERMIT_APPLICATION_EDIT.aspx.cs" Inherits="SCREEN_RTA_SCHOOL_BUS_PERMIT_APPLICATION_EDIT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/clockface/css/clockface.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/monthpicker.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="FormID1" enctype="multipart/form-data" runat="server" class="form-horizontal" autocomplete="off" method="post">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
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
                            <span id="PageHeading" runat="server"></span>
                           
                        </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light">
                                        <div class="portlet-title">
                                            <div class="caption caption-md">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase" id="PageHeading1" runat="server"></span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">

                                            <asp:UpdatePanel ID="UPpanel" runat="server">
                                                <contenttemplate>
                                                    

                                    <div class="row">
                                            
                                             <div class="col-md-1 col-sm-1">
                                            <asp:HiddenField ID="hdnID" runat="server" />
                                            <asp:HiddenField ID="hdnType" runat ="server" />
                                            <asp:HiddenField ID="hdnTypeOptionID" runat="server" />
                                            <asp:HiddenField ID="txtroutjson" runat="server" />
                                            <asp:HiddenField ID="txtVehicleJson" runat="server" />

<asp:HiddenField ID="MFSERVER" runat="server" />

                                             </div>
                                         <div class="col-md-3 col-sm-3"></div>
                                         <div class="col-md-2 col-sm-2">
                                                <div class="form-group">
                                                    <label for="form_control"><b>Select Permit Type<span style="color:red">*</span></b>                                                       
                                                    </label>
                                                </div>
                                            </div>
                                        &nbsp;
                                             <div class="col-md-2 col-sm-2">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlPermitType" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="1">Permanent</asp:ListItem> 
                                                        <asp:ListItem Value="2">Temporary</asp:ListItem> 
                                                    </asp:DropDownList>
                                                    </div>

                                        </div>
                                              
                                       </div>
                                        <br/>
                                       
                                   
                                                    <asp:Panel CssClass="row" ID="Panel1" runat="server" Visible="true">
                                      <div class="portlet-body">
                                          <div class="col-md-12 col-sm-12">
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Personal Information
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"> </a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row" style="align-content:center">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12" style="align-content:center">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                            
                                                                            <tr>
                                                                                 <td><b> Name : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VOName"  MaxLength="40" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                                                   <%-- <asp:Label ID="VOName" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>C/O :  <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                     <asp:TextBox ID="VOCareOf" MaxLength="40"  CssClass="regtxt form-control" runat="server"></asp:TextBox> 
                                                                                    <%--<asp:Label ID="VOCareOf" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                                 <td><b>Present Address: <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VOPresentAddress" TextMode="MultiLine" Columns="5"  CssClass="address form-control" runat="server"></asp:TextBox>   
                                                                                    <%--<asp:Label ID="VOPresentAddress" runat="server"></asp:Label>--%>
                                                                                     </td>
                                                                                <td><b> Permanent Address : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VOPermanentAddress"  TextMode="MultiLine" Columns="5" CssClass="address form-control" runat="server"></asp:TextBox>  
                                                                                   <%-- <asp:Label ID="VOPermanentAddress" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                
                                                                            </tr>
                                                                            
                                                                            
                                                                        </table>
                                                                      
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Vehicle Details
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"> </a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-md-5 col-sm-5 col-xs-5">

                                                                     </div>
                                                                    <div class="col-md-7 col-sm-7 col-xs-7">

                                                                     </div>
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                           <tr>
                                                                                
                                                                               
                                                                               
                                                                                 <td><b>RTO Name: <span style="color:red">*</span></b></td>
                                                                                  <td> 
                                                                                      <asp:TextBox ID="txtRTOName" CssClass="regtxt form-control" runat="server"  Enabled="false"  ReadOnly="true"></asp:TextBox>
                                                                               

                                                                                  </td>
                                                                                <td></td>
                                                                               <td><asp:HiddenField ID="txtRTOCode" runat="server" /></td>
                                                                              
                                                                            </tr>
                                                                            
                                                                             <tr>
                                                                                
                                                                                
                                                                                <td><b>Vehicle Registration No : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VORegNumber" CssClass="form-control" runat="server" ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VORegNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                 <td><b>Permit No <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtpermitNo" CssClass="form-control" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                                                            <%--<asp:Label ID="VType" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>

                                                                        <td><b>Permit Valid From</b> <span style="color: red">*</span></td>
                                                                        <td>
                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                <input type="text" id="txtpermitFrom" class="form-control" runat="server" readonly="readonly" />
                                                                                <span class="input-group-btn">
                                                                                    <button class="btn default" type="button">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </button>
                                                                                </span>
                                                                            </div>
                                                                        </td>


                                                                        <td><b>Permit Valid Upto</b> <span style="color: red">*</span></td>
                                                                        <td>
                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                <input type="text" id="txtpermitTo" class="form-control" runat="server" readonly="readonly" />
                                                                                <span class="input-group-btn">
                                                                                    <button class="btn default" type="button">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </button>
                                                                                </span>
                                                                            </div>
                                                                        </td>

                                                                        </tr>
                                                                            <tr>
                                                                                 <td><b>Vehicle Class Description: <span style="color:red">*</span></b></td>
                                                                                  <td> 
                                                                                        <asp:TextBox ID="VType" CssClass="regtxt form-control" runat="server"  ></asp:TextBox>
                                                                                      <%--<asp:Label ID="VType" runat="server"></asp:Label>--%>

                                                                                  </td>
                                                                              
                                                                            </tr>
                                                                           
                                                                            <tr>
                                                                                <td><b>Chassis Number : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VChassisNumber" MaxLength="30" CssClass="regtxtnumbersomespl form-control" runat="server" ></asp:TextBox>  
                                                                                    <%--<asp:Label ID="VChassisNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>  Engine Number  : <span style="color:red">*</span></b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VEngineNumber" MaxLength="60"  CssClass="regtxtnumbersomespl form-control" runat="server" ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VEngineNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td><b>Maker Description : <span style="color:red">*</span></b></td>
                                                                                <td>  
                                                                                   <asp:TextBox ID="VMakerDesc" MaxLength="60" CssClass="regtxtsomespl form-control" runat="server" ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VMakerDesc" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>  Maker Model No : <span style="color:red">*</span> </b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VMakerModel"  MaxLength="60"  CssClass="regtxtnumbersomespl form-control" runat="server"  ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VMakerModel" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td><b>Fuel Description : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VFuelDesc" MaxLength="6" CssClass="regtxt form-control" runat="server"  ></asp:TextBox>  
                                                                                    <%--<asp:Label ID="VFuelDesc" runat="server"></asp:Label>--%>

                                                                          </td>
                                                                                <td><b>  Color : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VColor"  MaxLength="30" CssClass="regtxt form-control" runat="server" ></asp:TextBox> 
                                                                                    <%--<asp:Label ID="VColor" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b> Fitness Valid Upto: <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                <input type="text" id="VFitness" class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button id="btn1" class="btn default" type="button" runat="server">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div>  <%--<asp:Label ID="VFitness" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>Tax Paid UpTo: <span style="color:red">*</span> </b></td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                <input type="text" id="VTax" class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button id="btn2" class="btn default" type="button" runat="server">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div>
                                                                                    <%--<asp:Label ID="VTax" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                
                                                                            </tr>
                                                                            <tr>

                                                                                <td><b>U. L. W. as per RC: <span style="color:red">*</span></b></td>
                                                                                <td>  
                                                                                    <asp:TextBox ID="VULW"   CssClass="number form-control" runat="server" ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VULW" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>R. L. W. as per RC: <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VRLW"  CssClass="number form-control" runat="server"  ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VRLW" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                               
                                                                                
                                                                                <td><b>Seating Capacity: <span style="color:red">*</span></b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VSeatCapacity" MaxLength="3" CssClass="number form-control" runat="server"  ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VSeatCapacity" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                               <td><b>Manufacturing Date: <span style="color:red">*</span></b></td>
                                                                               <td>
                                                                                    <div id='container'>
                                                                                    <input id="VManufacturingON" type="text" runat="server" class="form-control" />
                                                                                  </div>  
                                                                               </td>
                                                                                                                                                           
                                                                               </tr>
                                                                              <tr>
                                                                                   <td><b>Insurance Certificate Valid Upto</b> <span style="color:red">*</span></td>
                                                                               <td><div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                <input type="text" id="txtICValidUpto" class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div></td>
                                                                                <td><b>Pollution Certificate Valid Upto</b> <span style="color:red">*</span></td>
                                                                               <td><div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                <input type="text" id="txtPCValidUpto" class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div></td>    
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Fitness Certificate Valid Upto</b> <span style="color:red">*</span></td>
                                                                               <td><div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                <input type="text" id="txtFCValidUpto" class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div></td> 
                                                                            </tr>
                                                                        </table>
                                                                       
                                                                                <div class="col-md-12"> <asp:HiddenField ID="HDRegisteredat" runat="server"></asp:HiddenField></div>
                                                                                </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        
                                                        
                                                        <div class="portlet box green">
                                                            <div class="portlet-title">
                                                                <div class="caption">
                                                                    Other Details
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"> </a>
                                                                </div>
                                                            </div>
                                                            <div class="portlet-body">
                                                                <div class="row">
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                          
                                                                              <tr>
                                                                                 <td><b>School Location : <span style="color:red">*</span></b></td>
                                                                                <td> <asp:TextBox ID="txtFrom" CssClass="regtxt form-control" runat="server"></asp:TextBox></td>
                                                                                <td><%--<b>To (Station) : <span style="color:red">*</span></b>--%></td>
                                                                                <td><%--<asp:TextBox ID="txtTo" CssClass="form-control" runat="server"></asp:TextBox>--%></td>
                                                                                
                                                                            </tr>
                                                                            


                                                                              <tr>
                                                                                 <td><b>Mobile No : </b></td>
                                                                                <td> <asp:TextBox ID="txtMobileNo" MaxLength="10" CssClass="number form-control" runat="server"></asp:TextBox></td>
                                                                                <td><b>Email ID : </b></td>
                                                                                <td><asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                
                                                                            </tr>
                                                                             <tr>
                                                                                 <td><b> Gender : <span style="color:red">*</span></b></td>
                                                                                <td> <div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="gender" id="txtMale" value="Male" />
                                                        Male
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="gender" id="txtFemale" value="Female" />
                                                        Female
																		
                                                    </label>
                                                </div></td>
                                                                                <td></td>
                                                                                <td></td>
                                                                                
                                                                            </tr>
                                                                          
                                                                        </table>
                                                                      
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                       </div><br />
                                                            <div class="col-md-12 col-sm-12" style="text-align:center">
                                                                <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Update" OnClick="btnSave_Click" OnClientClick="return fnApplicationValidate()" />
                                                                
                                                       </div>
                                                          </div>
                                                            </div>
                                                         </div>
                                          </asp:Panel>

                                            </contenttemplate>
                                                <triggers>
                                                <asp:PostBackTrigger ControlID ="btnSave" />
                                                </triggers>
                                                </asp:UpdatePanel>
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
    <script src="assets/global/plugins/moment.min.js" type="text/javascript"></script>

    <script src="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>
    <script src="assets/js/SCREEN_RTA_SCHOOL_BUS_PERMIT_APPLICATION.js" type="text/javascript"></script>
    <script src="assets/js/monthpicker.min.js"></script>

</asp:Content>

