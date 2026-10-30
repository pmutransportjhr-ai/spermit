<%@ Page Title="" Language="C#" MasterPageFile="~/RTAMasterPage.master" AutoEventWireup="true" CodeFile="SCREEN_RTA_GOODS_PERMIT_APPLICATION_EDIT.aspx.cs" Inherits="SCREEN_RTA_GOODS_PERMIT_APPLICATION_EDIT" %>

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
                                                <span class="caption-subject font-red sbold uppercase"  id="PageHeading1" runat="server"></span>
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
                                                 <asp:HiddenField ID="oldRegnumber" runat="server" />
                                                 <asp:HiddenField ID="MFSERVER" runat="server" />
                                             </div>

                                         <div class="col-md-2 col-sm-2">
                                                <div class="form-group">
                                                    <label for="form_control"><b>Select Permit Type<span style="color:red">*</span></b>                                                       
                                                    </label>
                                                </div>
                                            </div>
                                        &nbsp;
                                        <div class="col-md-2 col-sm-2">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlPermitType" runat="server" CssClass="form-control" onchange="fnddlChange();">
                                                        <asp:ListItem Value="1">Permanent</asp:ListItem> 
                                                        <asp:ListItem Value="2">Temporary</asp:ListItem> 
                                                    </asp:DropDownList>
                                                    </div>

                                        </div>
                                           <div class="col-md-1 col-sm-1"></div>
                                      
                                          <asp:Label ID="lblMSg" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                              </div><br /><br />
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
                                                                                    <asp:TextBox ID="VOName" MaxLength="40" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                                                   <%-- <asp:Label ID="VOName" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>C/O :  <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                     <asp:TextBox ID="VOCareOf" MaxLength="40" CssClass="regtxt form-control" runat="server"></asp:TextBox> 
                                                                                    <%--<asp:Label ID="VOCareOf" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                                 <td><b>Present Address: <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VOPresentAddress" TextMode="MultiLine" Columns="5" CssClass="address form-control" runat="server"></asp:TextBox>   
                                                                                    <%--<asp:Label ID="VOPresentAddress" runat="server"></asp:Label>--%>
                                                                                     </td>
                                                                                <td><b> Permanent Address : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VOPermanentAddress" TextMode="MultiLine" Columns="5" CssClass="address form-control" runat="server"></asp:TextBox>  
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
                                                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                                                        <table class="table table-striped table-bordered table-hover">
                                                                          
                                                                             <tr>
                                                                                
                                                                                <td><b> Vehicle Registration No : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VORegNumber" CssClass="regtxtnumber form-control" runat="server" onblur="fnregNoBlur();" ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VORegNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                 <td><b>Permit No <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtpermitNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                                            <%--<asp:Label ID="VType" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>

                                                                        <td><b>Permit Valid From</b> <span style="color: red">*</span></td>
                                                                        <td>
                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                <input type="text" id="txtpermitFrom" class="form-control" runat="server"  />
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
                                                                                <input type="text" id="txtpermitTo" class="form-control" runat="server" />
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
                                                                                    <asp:TextBox ID="VChassisNumber" MaxLength="30" CssClass="regtxtnumbersomespl form-control" runat="server"  ></asp:TextBox>  
                                                                                    <%--<asp:Label ID="VChassisNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>  Engine Number  : <span style="color:red">*</span></b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VEngineNumber" MaxLength="30" CssClass="regtxtnumbersomespl form-control" runat="server"  ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VEngineNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td><b>Maker Description : </b></td>
                                                                                <td>  
                                                                                    <asp:TextBox ID="VMakerDesc" MaxLength="60" CssClass="regtxtsomespl form-control" runat="server"  ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VMakerDesc" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>  Maker Model No : </b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VMakerModel" MaxLength="60" CssClass="regtxtnumbersomespl form-control" runat="server"  ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VMakerModel" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td><b>Fuel Description : </b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VFuelDesc" MaxLength="6" CssClass="regtxt form-control" runat="server"  ></asp:TextBox>  
                                                                                    <%--<asp:Label ID="VFuelDesc" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>  Color : </b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VColor" MaxLength="30" CssClass="regtxt form-control" runat="server"  ></asp:TextBox> 
                                                                                    <%--<asp:Label ID="VColor" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b> Fitness Valid Upto: <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                <input type="text" id="VFitness" class="form-control"  readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button id="btn1" class="btn default" type="button"  runat="server">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div>  <%--<asp:Label ID="VFitness" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>Tax Paid UpTo: <span style="color: red">*</span></b></td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                <input type="text" id="VTax" class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button id="btn2" class="btn default" type="button"  runat="server">
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
                                                                                    <asp:TextBox ID="VULW"  CssClass="number form-control" runat="server" ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VULW" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td><b>R. L. W. as per RC: <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VRLW"  CssClass="number form-control" runat="server"  ></asp:TextBox> 
                                                                                    <%--<asp:Label ID="VRLW" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                               
                                                                                
                                                                                <td><b>The Nature of goods proposed to be carried: <span style="color:red">*</span></b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VNatureofGoods" MaxLength="60" CssClass="regtxt form-control" runat="server"  ></asp:TextBox>
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
                                                                                <td><b>The Rates to be charged by the applicant for carriage of goods: <span style="color:red">*</span></b></td>
                                                                                <td><asp:TextBox ID="txtRates" MaxLength="10" CssClass="numberWithdot form-control" runat="server" ></asp:TextBox></td>
                                                                                <td><b>Capacity: <span style="color:red">*</span></b></td>
                                                                                <td><asp:TextBox ID="txtVCapacity" MaxLength="5" CssClass="number form-control" runat="server" ></asp:TextBox></td>
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
                                                                    Goods Permit Apply Information
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
                                                                                 <td colspan ="2"><b>Self</b></td>
                                                                                     <td colspan="2"><b>Other RTO </b></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2"> 
                                                                                      <input type="checkbox" value="SELF" id="SelfCheckBox"  onclick="selfCheck1();" runat="server"/> SELF

                                                                                  </td>
                                                                                 <td colspan="2">
                                                                                        <div id="RTORouteDiv" runat="server"></div>
                                                                                    </td>
                                                                              

                                                                            </tr>
                                                                           
                                                                           
                                                                             <tr>
                                                                                <td colspan="2"><b> Whole State</b></td>
                                                                                
                                                                                
                                                                                <td colspan="2"><b> Other States</b></td>
                                                                                
                                                                            </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <div class='col-md-12 col-sm-12'>
                                                                                    <input type="checkbox" value="JHARKHAND" id="WholeStateCheckbox" runat="server" onclick="fnWholeState1();"/> JHARKHAND
                                                                                            </div>
                                                                                            
                                                                                    </td>
                                                                               
                                                                                    <td colspan="4">
                                                                                        <div id="otherStateDiv" runat="server"></div>
                                                                                    </td>
                                                                                </tr>
                                                                        </table>
                                                                       
                                                                                <div class="col-md-12"> <asp:HiddenField ID="RTOCode" runat="server"></asp:HiddenField>
                                                                                    
                                                                                      <input type="text" id="otherRTOCode" runat="server" style="display:none" />
                                                                                    <input type="text" id="hdnStateCode" runat="server" style="display:none" />
                                                                                    <input type="text" id="hdnminValidity" runat="server" style="display:none" />
                                                                                    
                                                                                </div>
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
                                                                                 <td><b>Mobile No : </b></td>
                                                                                <td> <asp:TextBox ID="txtMobileNo" CssClass="number form-control" runat="server"></asp:TextBox></td>
                                                                                <td><b>Email ID : </b></td>
                                                                                <td><asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                
                                                                                
                                                                                <td><b> Gender : <span style="color:red">*</span></b></td>
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="gender" id="txtMale" value="Male" />
                                                        Male
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="gender" id="txtFemale" value="Female" />
                                                        Female
																		
                                                    </label>
                                                </div></td>
                                                                               
                                                                            </tr>
                                                                        </table>
                                                                      
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                   
                                                       </div><br />
                                                            <div class="col-md-12 col-sm-12" style="text-align:center">
                                                                <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Update" OnClick="btnSave_Click" OnClientClick="return SaveApplication()" />
                                                            

                                                                
                                                                
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
   
     <script src="assets/js/monthpicker.min.js"></script>
    <script type="text/javascript">

        jQuery(document).ready(function () {


            $('input[id*=oldRegnumber]').val($('input[id*=VORegNumber]').val());

            if ($('input[id*=MFSERVER]').val() != 'YES') {
                fnManufactDate();
            }
            if ($('select[id*=ddlPermitType]').val() == "1")
            {
                $('.StateHDR').attr("disabled", true);
            }

            $('select[id*=ddlPermitType]').on('change', function () {

                if($(this).val() == "1")
                {
                    $('.StateHDR').attr("disabled", true);
                }


            });



        });
        function fnregNoBlur() {
            var oldRegNumber = $('input[id*=oldRegnumber]').val().trim();
            var regNumber = $('input[id*=VORegNumber]').val().trim();
            if(oldRegNumber != regNumber )
            {
                $('#loadingmessage').modal('show');
                $.ajax({
                    type: "POST",
                    url: "api/Screen_RTA.ashx?caller=GetScreenRTO&q="+regNumber+"~"+$('select[id*=ddlPermitType]').val(),
                    data: "",
                    cache: false,
                    async: false,
                    success: function (data) {
                        $('#loadingmessage').modal('hide');
                        if (data != "No Data Found") {
                            var statusf1 = data.split('~~');
                            if (statusf1[0] == "Success") {
                                $('div[id*=otherStateDiv').html("");
                                $('div[id*=otherStateDiv').html(statusf1[1]);
                                $('div[id*=RTORouteDiv').html("");
                                $('div[id*=RTORouteDiv').html(statusf1[2]);
                                $('input[id*=RTOCode').val(statusf1[3]);

                                $('.StateHDR').prop('checked', false);
                                $('.StateHDR').parent('span').removeClass('checked');
                                $('input[id*=WholeStateCheckbox]').prop('checked', false);
                                $('input[id*=WholeStateCheckbox]').parent('span').removeClass('checked');
                                $('input[id*=SelfCheckBox]').prop('checked', false);
                                $('input[id*=SelfCheckBox]').parent('span').removeClass('checked');
                                $('.rtoHdr').prop('checked', false);
                                $('.rtoHdr').parent('span').removeClass('checked');
                                $('input[id*=otherRTOCode]').val('');
                            }
                            else {
                                $('div[id*=otherStateDiv').html("");

                                $('div[id*=RTORouteDiv').html("");
                             
                            }

                        }

                    },
                    error: function (response) {
                        $('#loadingmessage').modal('hide');
                        alert(response.status + ' ' + response.statusText);
                    }
                });




            }



        }

        
        function selfCheck1() {
            $('input[id*=WholeStateCheckbox]').prop('checked', false);
            $('input[id*=WholeStateCheckbox]').parent('span').removeClass('checked');

        }
        function fnWholeState1() {
            $('.rtoHdr').each(function () {
                this.checked = false;
                $(this).parent('span').removeClass('checked');
            });
            $('.StateHDR').each(function () {
                this.checked = false;
                $(this).parent('span').removeClass('checked');
            });

            $('input[id*=SelfCheckBox]').prop('checked', false);
            $('input[id*=SelfCheckBox]').parent('span').removeClass('checked');
            $('input[id*=otherRTOCode]').val('');
        }
        function otherSate1() {
       
            $('.rtoHdr').each(function () {
                this.checked = false;
                $(this).parent('span').removeClass('checked');
            });

            $('input[id*=SelfCheckBox]').prop('checked', false);
            $('input[id*=SelfCheckBox]').parent('span').removeClass('checked');
            //  $('input[id*=WholeStateCheckbox]').prop('checked', false);

            //alert($(".StateHDR:checkbox:checked").length);

            var lenght = $(".StateHDR:checkbox:checked").length;
            if (lenght > 2) {
                alert('Please Select only Two States');
                $('.StateHDR').each(function () {
                    this.checked = false;
                    $(this).parent('span').removeClass('checked');
                });

            }


        }
        function otherRTOCode1(ID) {
             $('.StateHDR').each(function () {
                this.checked = false;
                $(this).parent('span').removeClass('checked');
            });

            // $('input[id*=SelfCheckBox]').prop('checked', false);
            $('input[id*=WholeStateCheckbox]').prop('checked', false);
            $('input[id*=WholeStateCheckbox]').parent('span').removeClass('checked');
             
            $('input[id*=otherRTOCode]').val(ID);
            
        }
        function fnddlChange() {
            if ($('select[id*=ddlPermitType]').val() == "1") {
                $('.StateHDR').attr("disabled", true);

                $('.StateHDR').prop('checked', false);
                $('.StateHDR').parent('span').removeClass('checked');
                $('input[id*=WholeStateCheckbox]').prop('checked', false);
                $('input[id*=WholeStateCheckbox]').parent('span').removeClass('checked');
                $('input[id*=SelfCheckBox]').prop('checked', false);
                $('input[id*=SelfCheckBox]').parent('span').removeClass('checked');
                $('.rtoHdr').prop('checked', false);
                $('.rtoHdr').parent('span').removeClass('checked');
                $('input[id*=otherRTOCode]').val('');
               
            }
            else {
                $('.StateHDR').attr("disabled", false);
                $('.StateHDR').prop('checked', false);
                $('.StateHDR').parent('span').removeClass('checked');
                $('input[id*=WholeStateCheckbox]').prop('checked', false);
                $('input[id*=WholeStateCheckbox]').parent('span').removeClass('checked');
                $('input[id*=SelfCheckBox]').prop('checked', false);
                $('input[id*=SelfCheckBox]').parent('span').removeClass('checked');
                $('.rtoHdr').prop('checked', false);
                $('.rtoHdr').parent('span').removeClass('checked');
                $('input[id*=otherRTOCode]').val('');
            }
        }
        function fnManufactDate() {

            var getdate = new Date();
            var yearstart = parseInt(getdate.getFullYear()) - parseInt(12);
            var monthsStart = parseInt(getdate.getMonth()) + parseInt(1);
            var startdate = monthsStart + "/" + yearstart;
            var endyear = getdate.getFullYear();
            var monthEnd = parseInt(getdate.getMonth());
            var endDate = monthEnd + "/" + endyear;
            $('input[id*=VManufacturingON]').Monthpicker({ minYear: yearstart, maxYear: endyear, minValue: startdate, maxValue: endDate })

        }
        function SaveApplication() {

            $("input[class='rtoHdr']:checked").each(function () {
                checked.push(($(this).val()));
                $('#otherRTOCode').val(($(this).val()));
            });

            var checkedstate = new Array();
            $("input[class='StateHDR']:checked").each(function () {
                checkedstate.push(($(this).val()));
            })
            var stateCodeValidity = "S";
            if (checkedstate.length > Number(2)) {
                alert("Please Select Only Two States");
                $("input[class='StateHDR']:checked").each(function () {
                    this.checked = false;
                })
                return false;

            }
            for (var j = 0; j < checkedstate.length; j++) {
                stateCodeValidity += checkedstate[j] + "-";


            }

            $('input[id*=hdnStateCode]').val(stateCodeValidity);

            if (fnApplicationValidate()) {
                return true;
            }

            else {

                return false;
            }
        }
        function fnSearchValidate() {
            if ($('input[id*=txtVehicleNumber]').val() == null || $('input[id*=txtVehicleNumber]').val() == "") {
                alert("Please enter Vehicle Registration Number");
                $('input[id*=txtVehicleNumber]').focus();
                return false;
            }
            var x = $('input[id*=txtVehicleNumber]').val();
            if (x.lenght < 4) {
                alert("Please enter Valid Jharkhand Vehicle Registration Number ");
                $('input[id*=txtVehicleNumber]').focus();
                return false;
            }
            if (x.substring(0, 4).toUpperCase() == 'JH01' || x.substring(0, 4).toUpperCase() == 'JH02' ||
                x.substring(0, 4).toUpperCase() == 'JH03' || x.substring(0, 4).toUpperCase() == 'JH04' ||
                x.substring(0, 4).toUpperCase() == 'JH05' || x.substring(0, 4).toUpperCase() == 'JH06' ||
                x.substring(0, 4).toUpperCase() == 'JH07' || x.substring(0, 4).toUpperCase() == 'JH08' ||
                x.substring(0, 4).toUpperCase() == 'JH09' || x.substring(0, 4).toUpperCase() == 'JH10' ||
                x.substring(0, 4).toUpperCase() == 'JH11' || x.substring(0, 4).toUpperCase() == 'JH12' ||
                x.substring(0, 4).toUpperCase() == 'JH13' || x.substring(0, 4).toUpperCase() == 'JH14' ||
                x.substring(0, 4).toUpperCase() == 'JH15' || x.substring(0, 4).toUpperCase() == 'JH16' ||
                x.substring(0, 4).toUpperCase() == 'JH17' || x.substring(0, 4).toUpperCase() == 'JH18' ||
                x.substring(0, 4).toUpperCase() == 'JH19' || x.substring(0, 4).toUpperCase() == 'JH20' ||
                x.substring(0, 4).toUpperCase() == 'JH21' || x.substring(0, 4).toUpperCase() == 'JH22' ||
                x.substring(0, 4).toUpperCase() == 'JH23' || x.substring(0, 4).toUpperCase() == 'JH24'

                ) {

            }
            else {
                alert("Please enter Valid Jharkhand Vehicle Registration Number ");
                $('input[id*=txtVehicleNumber]').focus();
                return false;
            }
            $('#loadingmessage').modal('show');
            return true;

        }
        function fnClearPermitNew() {
            $('input[id*=VOName]').val('');
            $('input[id*=VOCareOf]').val('');
            $('textarea[id*=VOPresentAddress]').val('');
            $('textarea[id*=VOPermanentAddress]').val('');
            $('input[id*=VType]').val('');
            $('input[id*=VChassisNumber]').val('');
            $('input[id*=VEngineNumber]').val('');
            $('input[id*=VMakerDesc]').val('');
            $('input[id*=VMakerModel]').val('');
            $('input[id*=VFuelDesc]').val('');
            $('input[id*=VColor]').val('');
            $('input[id*=VFitness]').val('');
            $('input[id*=VTax]').val('');
            $('input[id*=VULW]').val('');
            $('input[id*=VRLW]').val('');
            $('input[id*=VNatureofGoods]').val('');
            $('input[id*=VManufacturingON]').val('');
            $('input[id*=txtRates]').val('');
            $('input[id*=txtVCapacity]').val('');
            $('input[id*=txtMobileNo]').val('');
            $('input[id*=txtEmailId]').val('');
            $('input[id*=txtICValidUpto]').val('');
            $('input[id*=txtFCValidUpto]').val('');
            $('input[id*=txtPCValidUpto]').val('');
            //$('input:radio[name*="gender"]').prop('checked', false);
            //$("input[id*=txtMale]").parent('span').removeClass('checked');
            //$("input[id*=txtFemale]").parent('span').removeClass('checked');
            $('input[id*=txtpermitNo]').val('');
            $('input[id*=txtpermitFrom]').val('');
            $('input[id*=txtpermitTo]').val('');
            $('input[id*=txtVehicleNumber]').val('');
            $('input[id*=VORegNumber]').val('');



        }
        function fnApplicationValidate() {
        
            $('.form-control').css('border-color', '#c2cad8');

            var VOName = $('input[id*=VOName]').val().trim();
            if (VOName == "" || VOName == null) {
                alert("Please enter Name");
                $('input[id*=VOName]').css('border-color', 'red').focus();
                return false;
            }
            if (VOName.length < 3) {
                alert("Enter atleast 3 chars in the name textbox");
                $('input[id*=VOName]').css('border-color', 'red').focus();
                return false;
            }
            var VOCareOf = $('input[id*=VOCareOf]').val().trim();
            if (VOCareOf == "" || VOCareOf == null) {
                alert("Please enter C/O Name");
                $('input[id*=VOCareOf]').css('border-color', 'red').focus();
                return false;
            }
            if (VOCareOf.length < 3) {
                alert("Enter atleast 3 chars in the c/o name textbox");
                $('input[id*=VOCareOf]').css('border-color', 'red').focus();
                return false;
            }
            var VOPresentAddress = $('textarea[id*=VOPresentAddress]').val().trim();
            if (VOPresentAddress == "" || VOPresentAddress == null) {
                alert("Please enter present Address");
                $('textarea[id*=VOPresentAddress]').css('border-color', 'red').focus();
                return false;
            }
            var VOPermanentAddress = $('textarea[id*=VOPermanentAddress]').val().trim();
            if (VOPermanentAddress == "" || VOPermanentAddress == null) {
                alert("Please enter Permanent Address");
                $('textarea[id*=VOPermanentAddress]').css('border-color', 'red').focus();
                return false;
            }
            if ($('input[id*=SelfCheckBox]').prop('checked') == false && $('input[id*=WholeStateCheckbox]').prop('checked') == false) {
                alert("Please select at least One CheckBox");
                return false;

            }
            var x = $('input[id*=VORegNumber]').val();
            if (x.lenght < 4) {
                alert("Please enter Valid Jharkhand Vehicle Registration Number ");
                $('input[id*=VORegNumber]').focus();
                return false;
            }
            if (x.substring(0, 4).toUpperCase() == 'JH01' || x.substring(0, 4).toUpperCase() == 'JH02' ||
               x.substring(0, 4).toUpperCase() == 'JH03' || x.substring(0, 4).toUpperCase() == 'JH04' ||
               x.substring(0, 4).toUpperCase() == 'JH05' || x.substring(0, 4).toUpperCase() == 'JH06' ||
               x.substring(0, 4).toUpperCase() == 'JH07' || x.substring(0, 4).toUpperCase() == 'JH08' ||
               x.substring(0, 4).toUpperCase() == 'JH09' || x.substring(0, 4).toUpperCase() == 'JH10' ||
               x.substring(0, 4).toUpperCase() == 'JH11' || x.substring(0, 4).toUpperCase() == 'JH12' ||
               x.substring(0, 4).toUpperCase() == 'JH13' || x.substring(0, 4).toUpperCase() == 'JH14' ||
               x.substring(0, 4).toUpperCase() == 'JH15' || x.substring(0, 4).toUpperCase() == 'JH16' ||
               x.substring(0, 4).toUpperCase() == 'JH17' || x.substring(0, 4).toUpperCase() == 'JH18' ||
               x.substring(0, 4).toUpperCase() == 'JH19' || x.substring(0, 4).toUpperCase() == 'JH20' ||
               x.substring(0, 4).toUpperCase() == 'JH21' || x.substring(0, 4).toUpperCase() == 'JH22' ||
               x.substring(0, 4).toUpperCase() == 'JH23' || x.substring(0, 4).toUpperCase() == 'JH24'

               ) {

            }
            else {
                alert("Please enter Valid Jharkhand Vehicle Registration Number ");
                $('input[id*=VORegNumber]').focus();
                return false;
            }
            var permitNo = $('input[id*=txtpermitNo]').val().trim();
            if (permitNo == "" || permitNo == null) {
                alert("Please enter permit number");
                $('input[id*=txtpermitNo]').css('border-color', 'red').focus();
                return false;
            }


            var permitFrom = $('input[id*=txtpermitFrom]').val().trim();
            if (permitFrom == "" || permitFrom == null) {
                alert("Please select permit valid from date ");
                $('input[id*=txtpermitFrom]').css('border-color', 'red').focus();
                return false;
            }
            var permitTo = $('input[id*=txtpermitTo]').val().trim();
            if (permitTo == "" || permitTo == null) {
                alert("Please select permit valid upto date");
                $('input[id*=txtpermitTo]').css('border-color', 'red').focus();
                return false;
            }



            var VType = $('input[id*=VType]').val().trim();
            if (VType == "" || VType == null) {
                alert("Please enter Vehicle class description");
                $('input[id*=VType]').css('border-color', 'red').focus();
                return false;
            }

            var VChassisNumber = $('input[id*=VChassisNumber]').val().trim();
            if (VChassisNumber == "" || VChassisNumber == null) {
                alert("Please enter Vehicle chassis number");
                $('input[id*=VChassisNumber]').css('border-color', 'red').focus();
                return false;
            }
            var VEngineNumber = $('input[id*=VEngineNumber]').val().trim();
            if (VEngineNumber == "" || VEngineNumber == null) {
                alert("Please enter Vehicle Engine Number");
                $('input[id*=VEngineNumber]').css('border-color', 'red').focus();
                return false;
            }

            if ($('input[id*=VMakerDesc]').val().trim().length != "") {
                if ($('input[id*=VMakerDesc]').val().trim().length < 3) {
                    alert("Enter atleast 3 chars in the maker description textbox");
                    $('input[id*=VMakerDesc]').css('border-color', 'red').focus();
                    return false;
                }
            }
            if ($('input[id*=VMakerModel]').val().trim().length != "") {

                if ($('input[id*=VMakerModel]').val().trim().length < 3) {
                    alert("Enter atleast 3 chars in the maker model no. textbox");
                    $('input[id*=VMakerModel]').css('border-color', 'red').focus();
                    return false;
                }
            }
            if ($('input[id*=VFuelDesc]').val().trim().length != "") {

                if ($('input[id*=VFuelDesc]').val().trim().length < 3) {
                    alert("Enter atleast 3 chars in the fuel description textbox");
                    $('input[id*=VFuelDesc]').css('border-color', 'red').focus();
                    return false;
                }
            }
            if ($('input[id*=VColor]').val().trim().length != "") {


                if ($('input[id*=VColor]').val().trim().length < 3) {
                    alert("Enter atleast 3 chars in the color textbox");
                    $('input[id*=VColor]').css('border-color', 'red').focus();
                    return false;
                }
            }
            var VFitness = $('input[id*=VFitness]').val().trim();
            if (VFitness == "" || VFitness == null) {
                alert("Please enter fitness valid upto date");
                $('input[id*=VFitness]').css('border-color', 'red').focus();
                return false;
            }
            var VTax = $('input[id*=VTax]').val().trim();
            if (VTax == "" || VTax == null) {
                alert("Please enter tax valid upto date");
                $('input[id*=VTax]').css('border-color', 'red').focus();
                return false;
            }
            var VULW = $('input[id*=VULW]').val().trim();
            if (VULW == "" || VULW == null) {
                alert("Please enter U. L. W. as per RC");
                $('input[id*=VULW]').css('border-color', 'red').focus();
                return false;
            }
            if (VULW <= parseInt(0)) {
                alert("Please enter valid data  in the U. L. W. as per RC textbox");
                $('input[id*=VULW]').css('border-color', 'red').focus();

                return false;
            }

            var VRLW = $('input[id*=VRLW]').val().trim();
            if (VRLW == "" || VRLW == null) {
                alert("Please enter R. L. W. as per RC");
                $('input[id*=VRLW]').css('border-color', 'red').focus();
                return false;
            }
            if (VRLW <= parseInt(0)) {
                alert("Please enter valid data  in the R. L. W. as per RC textbox");
                $('input[id*=VRLW]').css('border-color', 'red').focus();

                return false;
            }

            var VNatureofGoods = $('input[id*=VNatureofGoods]').val().trim();
            if (VNatureofGoods == "" || VNatureofGoods == null) {
                alert("Please enter Nature of goods proposed to be carried");
                $('input[id*=VNatureofGoods]').css('border-color', 'red').focus();
                return false;
            }

            var VManufacturingON = $('input[id*=VManufacturingON]').val().trim();
            if (VManufacturingON == "" || VManufacturingON == null) {
                alert("Please enter Manufacturing date");
                $('input[id*=VManufacturingON]').css('border-color', 'red').focus();
                return false;
            }
            var VRates = $('input[id*=txtRates]').val().trim();
            if (VRates == "" || VRates == null) {
                alert("Please enter Rates to be charged by the applicant for carriage of goods");
                $('input[id*=txtRates]').css('border-color', 'red').focus();
                return false;
            }
            var VCapacity = $('input[id*=txtVCapacity]').val().trim();
            if (VCapacity == "" || VCapacity == null) {
                alert("Please enter Capacity");
                $('input[id*=txtVCapacity]').css('border-color', 'red').focus();
                return false;
            }
            if ($('input[id*=txtICValidUpto]').val() == null || $('input[id*=txtICValidUpto]').val() == "") {

                alert("Please select Insurance Certificate Validity Date");
                $('input[id*=txtICValidUpto]').css('border-color', 'red').focus();
                return false;
            }

            if ($('input[id*=txtPCValidUpto]').val() == null || $('input[id*=txtPCValidUpto]').val() == "") {
                alert("Please select Pollution Certificate Validity Date");
                $('input[id*=txtPCValidUpto]').css('border-color', 'red').focus();
                return false;
            }

            if ($('input[id*=txtFCValidUpto]').val() == null || $('input[id*=txtFCValidUpto]').val() == "") {
                alert("Please select Fitness Certificate Validity Date");
                $('input[id*=txtFCValidUpto]').css('border-color', 'red').focus();
                return false;
            }

            var MobileNo = $('input[id*=txtMobileNo]').val().trim();
            //if (MobileNo == "" || MobileNo == null) {
            //    alert("Please enter user mobile no");
            //    $('input[id*=txtMobileNo]').css('border-color', 'red').focus();
            //    return false;
            //}
            if (MobileNo != "") {
                if (!validateMob(MobileNo)) {
                    alert("Invalid Mobile Number");
                    $('input[id*=txtMobileNo]').css('border-color', 'red').focus();
                    return false;
                }
            }
            var EmailID = $('input[id*=txtEmailId]').val().trim();
            if (EmailID != "") {

                if (!IsEmail(EmailID)) {
                    alert("Invalid Email ID");
                    $('input[id*=txtEmailId]').css('border-color', 'red').focus();
                    return false;
                }
            }
            if ($("input[name*='gender']:checked").length <= 0) {
                alert("Please select gender option");
                return false;
            }


           


            return true;
        }





    </script>

</asp:Content>

