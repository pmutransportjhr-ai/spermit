<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="SCREEN_STA_PERMIT_MASTER.aspx.cs" Inherits="SCREEN_STA_PERMIT_MASTER" %>

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
                                    <div class="portlet-body form">
                                        <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
                                            <asp:HiddenField ID="hdnTypeID" runat="server" />
                                            <asp:HiddenField ID="hdnTypeOptionID" runat="server" />
                                            <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                            <asp:UpdatePanel ID="statepanel" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-2"><b>Permit Type</b></div>
                                                        <div class="col-md-3">
                                                            <asp:DropDownList ID="ddlPermitType"  CssClass="form-control select2" runat="server">
                                                             <asp:ListItem Value ="1" Text ="Temporary"></asp:ListItem>
                                                            <asp:ListItem Value ="2" Text ="Permanent"></asp:ListItem>
                                                            </asp:DropDownList>
                                                         </div>
                                                        <div class="col-md-2" style="text-align:right"><b>State <span class="required" aria-required="true">*</span></b></div>
                                                        <div class="col-md-3">
                                                       <asp:DropDownList ID="ddlDesState" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDesState_SelectedIndexChanged" CssClass="form-control select2" runat="server">
                                                            </asp:DropDownList>

                                                             
                                                        </div>
                                                        <div class="col-md-1"></div>
                                                   
                                                        </div>
                                                    <br /><br /><br />
                                                    <div style="text-align: center; font-weight: bold">
                                                        <asp:Label ID="Message" runat="server" Visible="false"> No Route Found</asp:Label></div>
                                                    <div class="form-group" id="RouteDiv" runat="server" visible="false">
                                                        <div class="col-md-1"></div>
                                                        <div class="col-md-10">
                                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                                <thead>
                                                                    <tr>
                                                                        <th rowspan="2">Sr No. </th>
                                                                        <th rowspan="2">Route Name </th>
                                                                        <th rowspan="2">Sanctioned Permit </th>
                                                                        <th colspan="2" style="text-align:center">Issued Permit </th>
                                                                        <th rowspan="2">Balance Permit </th>
                                                                        <th rowspan="2">Select Route </th>

                                                                    </tr>
                                                                    <tr>
                                                                        
                                                                        <th>Permanent</th>
                                                                        <th>Temporary</th>
                                                                      
                                                                        

                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tbdAPPlyRouteList">
                                                                    <div id ="FillAPPlyRouteList"  runat="server"></div>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                        <div class="col-md-1"></div>
                                                    </div>

                                                      <div id="Panel1" style ="display:none">
                                        <div class="row">
                                            
                                             <div class="col-md-1 col-sm-1">
                                            <asp:HiddenField ID="hdnID" runat="server" />
                                            <asp:HiddenField ID="hdnType" runat ="server" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <asp:HiddenField ID="txtroutjson" runat="server" />
                                            <asp:HiddenField ID="txtVehicleJson" runat="server" />
                                             </div>
                                     
                                                <div class="col-md-1 col-sm-1"></div>
                                            <div class="col-md-3 col-sm-3">
                                                <div class="form-group">
                                                    
                                               </div>
                                            </div><br /><br />
                                            <div class="col-md-12 col-sm-12">
                                          <asp:Label ID="lblMSg" Visible="false" Font-Bold="true" runat="server"></asp:Label>
                                              </div>
                                       </div>
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
                                                                                     <asp:TextBox ID="VOCareOf" MaxLength="40"  CssClass="regtxt form-control" runat="server"></asp:TextBox> 
                                                                                    <%--<asp:Label ID="VOCareOf" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                                 <td><b>Present Address: <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox TextMode="MultiLine" Columns="5" ID="VOPresentAddress" CssClass="address form-control" runat="server"></asp:TextBox>   
                                                                                    <%--<asp:Label ID="VOPresentAddress" runat="server"></asp:Label>--%>
                                                                                     </td>
                                                                                <td><b> Permanent Address : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox TextMode="MultiLine" Columns="5"  ID="VOPermanentAddress" CssClass="address form-control" runat="server"></asp:TextBox>  
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
                                                                                
                                                                                
                                                                                <td><b> Registration No : <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VORegNumber" CssClass="form-control" runat="server" ></asp:TextBox>
                                                                                    <%--<asp:Label ID="VORegNumber" runat="server"></asp:Label>--%>

                                                                                </td>
                                                                              
                                                                                 <td><b>Permit No <span style="color:red">*</span></b></td>
                                                                                  <td> 
                                                                                      <asp:TextBox ID="txtpermitNo" CssClass="form-control" runat="server"  ></asp:TextBox>
                                                                                      <%--<asp:Label ID="VType" runat="server"></asp:Label>--%>

                                                                                  </td>
                                                                            </tr>
                                                                            <tr>
                                                                            
                                                                           <td><b>Permit Valid From</b> <span style="color:red">*</span></td>
                                                                               <td><div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                <input type="text" id="txtpermitFrom" class="form-control"  runat="server" readonly="readonly" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div></td>    
                                                                           
                                                                          
                                                                           <td><b>Permit  Valid Upto</b> <span style="color:red">*</span></td>
                                                                               <td><div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                <input type="text" id="txtpermitTo" class="form-control"  runat="server" readonly="readonly" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div></td>    
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
                                                                                   

                                                                                </td>
                                                                                <td><b>  Engine Number  : <span style="color:red">*</span></b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VEngineNumber" MaxLength="60"  CssClass="regtxtnumbersomespl form-control" runat="server"  ></asp:TextBox>
                                                                                  

                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td><b>Maker Description : </b></td>
                                                                                <td>  
                                                                                    <asp:TextBox ID="VMakerDesc" MaxLength="60" CssClass="regtxtsomespl form-control" runat="server"  ></asp:TextBox>
                                                                                   

                                                                                </td>
                                                                                <td><b>  Maker Model No : </b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VMakerModel" MaxLength="60"  CssClass="regtxtnumbersomespl form-control" runat="server"  ></asp:TextBox>
                                                                                 
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                <td><b>Fuel Description : </b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VFuelDesc" MaxLength="6" CssClass="regtxt form-control" runat="server"  ></asp:TextBox>  
                                                                                  
                                                                                </td>
                                                                                <td><b>  Color : </b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VColor" MaxLength="30" CssClass="regtxt form-control" runat="server"  ></asp:TextBox> 
                                                                                   

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td><b> Fitness Valid Upto: </b> <span style="color:red">*</span></td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd-M-yyyy">
                                                                <input type="text" id="VFitness" class="form-control" runat="server" readonly="readonly" />
                                                                <span class="input-group-btn">
                                                                    <button id="btn1" class="btn default" type="button"  runat="server">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div>  

                                                                                </td>
                                                                                <td><b>Tax Paid UpTo: </b> <span style="color:red">*</span></td>
                                                                                <td>
                                                                                    <div class="input-group input-medium date date-picker" data-date-format="dd-M-yyyy">
                                                                <input type="text" id="VTax"  class="form-control"  runat="server" readonly="readonly" />
                                                                <span class="input-group-btn">
                                                                    <button id="btn2" class="btn default" type="button"  runat="server">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div>
                                                                                   

                                                                                </td>
                                                                                
                                                                            </tr>
                                                                            <tr>

                                                                                <td><b>U. L. W. as per RC: <span style="color:red">*</span></b></td>
                                                                                <td>  
                                                                                    <asp:TextBox ID="VULW" MaxLength="5" CssClass="number form-control" runat="server"  ></asp:TextBox>
                                                                                   

                                                                                </td>
                                                                                <td><b>R. L. W. as per RC: <span style="color:red">*</span></b></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="VRLW" MaxLength="5" CssClass="number form-control" runat="server"  ></asp:TextBox> 
                                                                                   

                                                                                </td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                               
                                                                                
                                                                                <td><b>Seating Capacity: <span style="color:red">*</span></b></td>
                                                                                <td> 
                                                                                    <asp:TextBox ID="VSeatCapacity" MaxLength="3" CssClass="number form-control" runat="server"  ></asp:TextBox>
                                                                                  

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
                                                                <input type="text" id="txtICValidUpto" class="form-control" readonly="readonly"  runat="server" />
                                                                <span class="input-group-btn">
                                                                    <button class="btn default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div></td>    
                                                                           
                                                                          
                                                                           <td><b>Pollution Certificate Valid Upto </b><span style="color:red">*</span></td>
                                                                               <td><div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy">
                                                                <input type="text" id="txtPCValidUpto" readonly="readonly" class="form-control"  runat="server" />
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
                                                                    Route Details <span style="color:red">*</span>
                                                                </div>
                                                                <div class="tools">
                                                                    <a href="javascript:;" class="collapse"> </a>
                                                                </div>
                                                            </div>
                                                          
                                                            <div class="portlet-body">
                                                            <br />
                                                                <div class="row" id="RouteHDRInfo" runat="server">
                                                                    
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
                                                                                 <td><b>Mobile No :</b></td>
                                                                                <td> <asp:TextBox ID="txtMobileNo" MaxLength="10" CssClass="number form-control" runat="server"></asp:TextBox></td>
                                                                                <td><b>Email ID : </b></td>
                                                                                <td><asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                                                
                                                                            </tr>
                                                                            <tr>
                                                                                
                                                                                
                                                                                <td><b> Gender : </b></td>
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="gender" id="txtMale" value="Male" checked />
                                                        Male
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="gender" id="txtFemale" value="Female" />
                                                        Female
																		
                                                    </label>
                                                </div></td>
                                                                               <td><b> Qualification : </b></td>
                                                                                <td><select id="ddlQualification" name="ddlQualification" class="form-control select2" runat="server">

                                                            <option value="Master">Master</option>
                                                            <option value="Bachelor">Bachelor</option>
                                                                                    <option value="Intermediate">Intermediate</option>
                                                                                    <option value="Matriculation">Matriculation</option>
                                                                                    <option value="Illiterate">Illiterate</option>
                                                                                    
                                                        </select></td>
                                                                            </tr>
                                                                           
                                                                            <tr>
                                                                                
                                                                                
                                                                                <td><b> Category of Applicant : <span style="color:red">*</span></b></td>
                                                                                <td><select id="ddlCategory" name="ddlCategory"  class="form-control select2" runat="server">

                                                            <option value="Gen">Gen</option>
                                                            <option value="SC">SC</option>
                                                            <option value="ST">ST</option>
                                                            <option value="OBC">OBC</option>                      
                                                                                    
                                                        </select></td>
                                                                               <td><b> Ex-Army category : </b></td>
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="ExArmy"  id="txtExArmyY" value="Yes" />
                                                        Yes 
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="ExArmy"  id="txtExArmyN" value="No" checked />
                                                        No
																		
                                                    </label>&nbsp;
                                                
                                                </div></td>
                                                                            </tr>
                                                                            <tr>
                                                                                
                                                                                
                                                                                
                                                                               <td><b> Minority : </b></td>
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="Minority"  id="txtMinorityY" value="Yes"  />
                                                        Yes 
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="Minority" id="txtMinorityN" value="No" checked />
                                                        No
																		
                                                    </label>&nbsp;
                                                
                                                </div></td>
                                                                                <td><b> Handicapped : </b></td>
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="Handicapped"   id="txtHandicappedY" value="Yes" />
                                                        Yes 
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="Handicapped"  id="txtHandicappedN" value="No" checked />
                                                        No
																		
                                                    </label>&nbsp;
                                                
                                                </div></td>
                                                                            </tr>
                                                                            
                                                                             <tr>
                                                                                
                                                                                
                                                                                <td><b> Heavy Motor Licence  :</b></td>
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="HML"  id="txtHMLY" value="Yes" />
                                                        Yes 
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="HML"  id="txtHMLN" value="No"  checked/>
                                                        No
																		
                                                    </label>&nbsp;
                                                
                                                </div></td>
                                                                             <td><b> Bus Type (AC) : </b></td>
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="AC" id="txtACY" value="Yes" />
                                                        Yes 
																		
                                                    </label>
                                                                                    
                                                        
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="AC" id="txtACN" value="No" checked />
                                                        No
																		
                                                    </label>&nbsp;
                                                
                                                </div></td> 
                                                                            </tr>
                                                                             <tr>
                                                                                
                                                                                
                                                                               
                                                                               <td><b> Bus Type (Deluxe) : </b></td>
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="Deluxe" id="txtDeluxeY" value="Yes" />
                                                        Yes 
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        
                                                        
                                                        <input type="radio" runat="server" name="Deluxe" id="txtDeluxeN" value="No"  checked/>
                                                        No
																		
                                                    </label>&nbsp;
                                                
                                                </div></td>
                                                                            
                                                                                <td><b> Bus Type (Sleeper) : </b></td>
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="Sleeper" id="txtSleeperY" value="Yes" />
                                                        Yes 
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        
                                                        
                                                        <input type="radio" runat="server" name="Sleeper" id="txtSleeperN" value="No" checked />
                                                        No
																		
                                                    </label>&nbsp;
                                                
                                                </div></td>
                                                                                <td></td>
                                                                            </tr>
                                                                            <tr>
                                                                                 <td><b> Identity Proof : </b></td>
                                                                                <td><div class="radio-list" style="padding-left:25px">
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="IdentityProof"  id="txtIdentityProofV" value="Voter Id" checked />
                                                        Voter Id 
																		
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input type="radio" runat="server" name="IdentityProof" id="txtIdentityProofP" value="Pan Card" />
                                                        Pan Card
																		
                                                    </label>&nbsp;
                                                  <label class="radio-inline">
                                                        <input type="radio" runat="server" name="IdentityProof" id="txtIdentityProofA" value="Aadhaar Card" />
                                                        Aadhaar Card
																		
                                                    </label>&nbsp;
                                                </div></td> 

                                                                                  <td><b>Voter Id/ Pan Card/ Aadhaar Card No.  </b></td>
                                                                              <td><asp:TextBox ID="txtIdentityNo" CssClass="regtxtnumber form-control" runat="server"></asp:TextBox></td>
                                                                            
                                                                            </tr>
                                                                        </table>
                                                                      
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                   
                                               
                                                       </div><br />
                                                            <div class="col-md-12 col-sm-12" style="text-align:center">
                                                                <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Proceed" OnClick="btnSave_Click" OnClientClick="return SaveApplication()" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnReset" CssClass="btn default" runat="server" Text="Reset" OnClientClick="return fnClearPermitNew()"  OnClick="btnReset_Click" />

                                                                
                                                                
                                                            </div>
                                                                

                                                            </div>    

                                                    </div>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlDesState" EventName="SelectedIndexChanged" />
                                                    <asp:PostBackTrigger ControlID ="btnSave" />
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
    <script src="assets/global/plugins/moment.min.js" type="text/javascript"></script>

    <script src="assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/global/plugins/clockface/js/clockface.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
    <script src="assets/js/SCREEN_STA_PERMIT.js" type="text/javascript"></script>
    <script src="assets/js/monthpicker.min.js"></script>
     <script src="assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js" type="text/javascript"></script>
     <script src="assets/pages/scripts/form-input-mask.js" type="text/javascript"></script>
       
</asp:Content>

