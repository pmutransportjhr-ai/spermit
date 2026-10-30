<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SCREEN_STA_COUNTER_SIGNATURE_APPLICATION.aspx.cs" Inherits="SCREEN_STA_COUNTER_SIGNATURE_APPLICATION" %>

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
         <asp:HiddenField ID="hdnID" runat="server" />
                                            <input type="text" id="hdnType" runat ="server" style="display:none" />
                                            <input type="text" id="hdnTypeOptionID" runat="server" style="display:none"  />
                                            <input type="text" id="txtroutjson" runat="server"  style="display:none" />
                                            <asp:HiddenField ID="txtVehicleJson" runat="server" />
                                            <%--<asp:HiddenField ID="txtNoRoundTrip" runat="server" />--%>                                    



                                          
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
                                            <div class="form-group">
                                                          <div class="col-md-3"></div>
                                                        <div class="col-md-2"><b>Permit Type</b></div>
                                                        <div class="col-md-2">
                                                            <asp:DropDownList ID="ddlPermitType" CssClass="form-control select2" runat="server" >
                                                                 <asp:ListItem Value ="2" Text ="Permanent" ></asp:ListItem> 
                                                                <asp:ListItem Value ="1" Text ="Temporary" ></asp:ListItem>
                                                           
                                                            </asp:DropDownList>
                                                         </div>
                                                      
                                                            <div class="col-md-5"></div> 
                                                        
                                                        <%--<div class="col-md-1"></div>--%>
                                                    
                                                   </div>     
                                                        
                                            <br />
                                              

                                            <asp:UpdatePanel ID="UPpanel" runat="server" Visible="true">
                                                <contenttemplate>
                                                    

                                    
                                            
                                            
                                           
                                           
                                       
<div class="row">
                                   
                                                   
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
                                                                                     <asp:TextBox ID="VOCareOf" CssClass="regtxt form-control" runat="server"></asp:TextBox> 
                                                                                    <%--<asp:Label ID="VOCareOf" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                                 <td><b>Present Address: <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VOPresentAddress" TextMode="MultiLine" Columns="5"  CssClass="form-control" runat="server"></asp:TextBox>   
                                                                                    <%--<asp:Label ID="VOPresentAddress" runat="server"></asp:Label>--%>
                                                                                     </td>
                                                                                <td><b> Permanent Address : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VOPermanentAddress" TextMode="MultiLine" Columns="5"  CssClass=" form-control" runat="server"></asp:TextBox>  
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
                                                                                
                                                                                
                                                                                <td><b>Vehicle Registration No : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VORegNumber" CssClass="form-control" runat="server" ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VORegNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                <td>
                                                                                     <b>Permit No :</b>  <span style="color:red">*</span>
                                                                
                                                                    
                                                                
                                                               
                                                                                </td>
                                                                              <td><asp:TextBox ID="txtPermitNo" MaxLength="20" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                            </tr>
                                                                               <tr>
                                                                                
                                                                                <td>
                                                                                    <b>Counter Signature No: <span style="color:red">*</span></b>
                                                                                </td>
                                                                                <td >
                                                                                    <asp:TextBox ID="txtCounterSignatureNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                                                                </td>
                                                                                <td></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                     
                                                                <b>Validity of Permit from : <span style="color:red">*</span></b> </td>
                                                                                    <td colspan="3">
                                                                
                                                                    <div class="input-group input-large date-picker input-daterange"  data-date-format="dd/mm/yyyy">
                                                                        <input type="text" id="txtvalidityfrom" width="210px" readonly="true" class="regreadonly form-control" runat="server" />
                                                                        <span class="input-group-addon">To </span>
                                                                        <input type="text" id="txtvalidityto" width="210px" readonly="true" class="regreadonly form-control" runat="server" />
                                                                    </div>
                                                                  
                                                               
                                                               

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
                                                                                    <asp:TextBox ID="VMakerModel" MaxLength="60"  CssClass="regtxtnumbersomespl form-control" runat="server"  ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VMakerModel" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                           <tr>
                                                                                <td><b>Type of Vehicle : <span style="color:red">*</span></b></td>
                                                                                  <td> 
                                                                                      <asp:TextBox ID="VType" CssClass="regtxt form-control" runat="server"  ></asp:TextBox>
                                                                                      <%--<asp:Label ID="VType" runat="server"></asp:Label>--%>

                                                                                  </td>
                                                                               <td><b>Chassis Number : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VChassisNumber" MaxLength="30" CssClass="regtxtnumbersomespl form-control" runat="server"  ></asp:TextBox>  
                                                                                    <%--<asp:Label ID="VChassisNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                           </tr>
                                                                            <tr>
                                                                                
                                                                                <td><b>  Engine Number  : <span style="color:red">*</span></b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VEngineNumber" MaxLength="60"  CssClass="regtxtnumbersomespl form-control" runat="server"  ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VEngineNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                                 <td><b>Seating Capacity (including driver/conductor/attendant): <span style="color:red">*</span></b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VSeatCapacity" MaxLength="2"  CssClass="number form-control" runat="server"  ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VSeatCapacity" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                            </tr>
                                                                             <tr>
 <td><b>U.L.W as per RC<span style="color:red">*</span></b></td>
                                                                                <td>  
                                                                                    <asp:TextBox ID="txtULW"  CssClass="number form-control" runat="server"  MaxLength="5" ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VULW" runat="server"></asp:Label>--%>

                                                                                </td>         
                                                                               
<td><b>R.L.W as per RC<span style="color:red">*</span></b></td>
                                                                                <td>  
                                                                                    <asp:TextBox ID="txtRLW"  CssClass="number form-control" runat="server"   MaxLength="5" ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VULW" runat="server"></asp:Label>--%>

                                                                                </td>   

                                                                                 </tr>

                                                                            
                                                                            
                                                                            <tr>
                                                                               
                                                                                
                                                                               
                                                                               <td><b>Manufacturing Date: <span style="color:red">*</span></b></td>
                                                                               <td>
                                                                                  <%-- <div class="input-group input-medium date date-picker" data-date-format="dd-M-yyyy" data-date-start-date="-12y" data-date-end-date="-1d">
                                                                <input type="text" id="VManufacturingON"  class="form-control" readonly="true" runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button id="btn3" class="btn default" type="button" runat="server">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div>--%>
                                                                                   <div id='container'>
                                                                                  <input id="VManufacturingON" type="text" runat="server" class="form-control" />
                                                          </div> 






                                                                                    <%--<asp:Label ID="VManufacturingON" runat="server"></asp:Label>--%>

                                                                               </td>
                                                                                   <td><b>Nature of goods to be carried, if any</b></td>                                                              
                                                                                <td> <asp:TextBox ID="txtNatureOfGoods"  MaxLength="60" CssClass="regtxt form-control" runat="server"  ></asp:TextBox>
                                                                                  </td>
                                                                               </tr>
                                                                               <tr>

                                                                                        <td><b>Insurance Certificate Valid Upto</b> <span style="color: red">*</span></td>
                                                                                        <td>
                                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                                <input type="text" id="txtICValidUpto" class="form-control" runat="server" readonly="true" />
                                                                                                <span class="input-group-btn">
                                                                                                    <button class="btn default" type="button">
                                                                                                        <i class="fa fa-calendar"></i>
                                                                                                    </button>
                                                                                                </span>
                                                                                            </div>
                                                                                        </td>


                                                                                        <td><b>Pollution Certificate Valid Upto </b><span style="color: red">*</span></td>
                                                                                        <td>
                                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                                <input type="text" id="txtPCValidUpto" class="form-control" runat="server" readonly="true" />
                                                                                                <span class="input-group-btn">
                                                                                                    <button class="btn default" type="button">
                                                                                                        <i class="fa fa-calendar"></i>
                                                                                                    </button>
                                                                                                </span>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr>

                                                                                        <td><b>Fitness Certificate Valid Upto</b> <span style="color: red">*</span></td>
                                                                                        <td>
                                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                                <input type="text" id="txtFCValidUpto" class="form-control" runat="server" readonly="true" />
                                                                                                <span class="input-group-btn">
                                                                                                    <button class="btn default" type="button">
                                                                                                        <i class="fa fa-calendar"></i>
                                                                                                    </button>
                                                                                                </span>
                                                                                            </div>
                                                                                        </td>


                                                                                        <td><b>Tax Paid Upto</b> <span style="color: red">*</span></td>
                                                                                        <td>
                                                                                             <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                                <input type="text" id="txtTPValidUpto" class="form-control" runat="server"  readonly="true" />
                                                                                                <span class="input-group-btn">
                                                                                                    <button class="btn default" type="button">
                                                                                                        <i class="fa fa-calendar"></i>
                                                                                                    </button>
                                                                                                </span>
                                                                                            </div>


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
                                                                    Route Details <span style="color:red">*</span>
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"> </a>
                                                                </div>
                                                            </div>
                                                          
                                                            <div class="portlet-body">
                                                            <br />
                                                                <div class="row">
                                                                    <div class="col-md-3"><b>Issued by : <span style="color:red">*</span></b></div>
                                                                    <div class="col-md-3"> <asp:DropDownList ID="ddlStateIssuedBy" AutoPostBack="true" OnSelectedIndexChanged="ddlStateIssuedBy_SelectedIndexChanged" CssClass="form-control select2" runat="server" AppendDataBoundItems="true"></asp:DropDownList></div>
                                                                    <div class="col-md-6"></div>
                                                                </div><br />
                                                                
                                                                  <div id="RouteHDRInfo" runat="server" style="height: 150px !important; overflow-y: auto !important; overflow-x: auto !important; padding:10px 0;" >
                                                                    
                                                                </div>
                                                                <br />
                                                                
                                                                Time Format: 24 Hours <br />
                                                                <div id="RouteTimeInfo" runat="server"></div>
                                                               
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
                                                                                <td> <asp:TextBox ID="txtMobileNo" CssClass="number form-control" runat="server" MaxLength ="10"></asp:TextBox></td>
                                                                                <td><b>Email ID : </b></td>
                                                                                <td><asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                
                                                                            </tr>
                                                                           
                                                                            <tr>
                                                                                <td>
                                                                                    <b>Recommendation Letter No: <span style="color:red">*</span> </b>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID ="txtRecommendationNo" runat="server" CssClass="form-control" ></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <b>Recommendation Date:<span style="color:red">*</span> </b>
                                                                                </td>
                                                                                <td>
                                                                                     <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                                <input type="text" id="txtRecommendationNoDated" class="form-control" runat="server"  readonly="true"/>
                                                                                                <span class="input-group-btn">
                                                                                                    <button class="btn default" type="button">
                                                                                                        <i class="fa fa-calendar"></i>
                                                                                                    </button>
                                                                                                </span>
                                                                                            </div>
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
                                                                    TAX Detail
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
                                                                                 <td><b>Tax Paid Amount: <span style="color:red">*</span></b></td>
                                                                                <td> <asp:TextBox ID="txttaxpayAmount" CssClass="number form-control" runat="server" MaxLength ="10"></asp:TextBox></td>
                                                                              <td><b>Tax Period : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddltaxperiod"  runat="server"  CssClass="form-control select2" AutoPostBack="false">
                                                               
                                                                    <asp:ListItem Value="q">Quarterly</asp:ListItem>
                                                                    <asp:ListItem Value="h">Half Yearly</asp:ListItem>
                                                                    <asp:ListItem Value="y">Yearly</asp:ListItem>
                                                                </asp:DropDownList>



                                                                                </td>
                                                                                  
                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b>Fee Tax Paid From :<span style="color:red">*</span> </b></td>
                                                                                <td>
                                                                                     <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                                                <input type="text" id="txtTAXpaidfrom" class="form-control" runat="server" readonly="true" />
                                                                                                <span class="input-group-btn">
                                                                                                    <button class="btn default" type="button">
                                                                                                        <i class="fa fa-calendar"></i>
                                                                                                    </button>
                                                                                                </span>
                                                                                            </div>
                                                                                    
                                                                                   </td>
                                                                                 <td><b>Fee Tax Paid Upto : <span style="color:red">*</span></b></td>
                                                                                <td> 
                                                                                     <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                                <input type="text" id="txtTAXpaidupto" class="form-control" runat="server"  readonly="true"/>
                                                                                                <span class="input-group-btn">
                                                                                                    <button class="btn default" type="button">
                                                                                                        <i class="fa fa-calendar"></i>
                                                                                                    </button>
                                                                                                </span>
                                                                                            </div>
                                                                                    
                                                                                    </td>
                                                                                
                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <b>Tax Reference No: <span style="color:red">*</span> </b>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID ="txttaxRefNo" runat="server" CssClass="form-control" ></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <b>Tax Paid on:<span style="color:red">*</span> </b>
                                                                                </td>
                                                                                <td>
                                                                                     <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                                <input type="text" id="txttaxRefDated" class="form-control" runat="server"  readonly="true"/>
                                                                                                <span class="input-group-btn">
                                                                                                    <button class="btn default" type="button">
                                                                                                        <i class="fa fa-calendar"></i>
                                                                                                    </button>
                                                                                                </span>
                                                                                            </div>
                                                                                </td>
                                                                            </tr>
                                                                           
                                                                        </table>
                                                                      
                                                                    </div>
                                                                </div>
 
                                                        </div></div>
                                              
                                                                                               
                                                       </div><br />
                                                            <div class="col-md-12 col-sm-12" style="text-align:center">
                                                                <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Submit" OnClick="btnSave_Click" OnClientClick="return SaveApplication()" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnReset" CssClass="btn default" runat="server" Text="Reset" OnClientClick="return fnClearPermitNew()"  OnClick="btnReset_Click" />

                                                                
                                                                
                                                            </div>
                                                                </div>
                                                        
                                                    

                                            </contenttemplate>
                                                <triggers>
                                                   <asp:AsyncPostBackTrigger ControlID="ddlStateIssuedBy" />
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
    <script src="assets/js/SCREEN_STA_COUNTER_SIGNATURE_APPLICATION.js" type="text/javascript"></script>
     <script src="assets/js/monthpicker.min.js"></script>
    <script src="assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js" type="text/javascript"></script>
    <script src="assets/js/form-input-mask1.js" type="text/javascript"></script>
 
    
</asp:Content>

