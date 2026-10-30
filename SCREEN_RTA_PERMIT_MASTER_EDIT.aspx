<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="SCREEN_RTA_PERMIT_MASTER_EDIT.aspx.cs" Inherits="SCREEN_RTA_PERMIT_MASTER_EDIT" %>

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
     <input id="txtScreen" type="text" value="SCREEN_RTA_PERMIT_MASTER_EDIT.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="6" />

     <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">
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
                                      
                                              <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                            <asp:UpdatePanel ID="statepanel" runat="server">
                                                <ContentTemplate>     
                                          
                                       <div class="row">
                                            
                                            
                                    <asp:Panel CssClass="row" ID="Panel1" runat="server">

                                        <div class ="row">
                                            <div class ="col-md-3">

                                            </div>
                                            <div class ="col-md-2">
                                               <b>Permit Type</b> 
                                            </div>
                                            <div class ="col-md-3">
                                                <asp:DropDownList ID="ddlPermitType" runat="server" CssClass="form-control">
                                                   <asp:ListItem Value="1" >Temporary</asp:ListItem>
                                                    <asp:ListItem Value="2" >Permanent</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class ="col-md-4">

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
                                                            <a href="javascript:;" class="collapse"></a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body">
                                                        <div class="row" style="align-content: center">
                                                            <div class="col-md-12 col-sm-12 col-xs-12" style="align-content: center">
                                                                <table class="table table-striped table-bordered table-hover">

                                                                    <tr>
                                                                        <td><b>Name : <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VOName" MaxLength="40" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                                            <%-- <asp:Label ID="VOName" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                        <td><b>C/O :  <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VOCareOf" MaxLength="40" CssClass="regtxt form-control" runat="server"></asp:TextBox>
                                                                            <%--<asp:Label ID="VOCareOf" runat="server"></asp:Label>--%>

                                                                        </td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Present Address: <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VOPresentAddress" TextMode="MultiLine" Columns="5" CssClass="address form-control" runat="server"></asp:TextBox>
                                                                            <%--<asp:Label ID="VOPresentAddress" runat="server"></asp:Label>--%>
                                                                        </td>
                                                                        <td><b>Permanent Address : <span style="color: red">*</span></b></td>
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
                                                            <a href="javascript:;" class="collapse"></a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body">
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <tr>


                                                                        <td><b>Vehicle Registration No : <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VORegNumber" CssClass="form-control" runat="server" ></asp:TextBox>
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
                                                                        <td><b>Vehicle Class Description: <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VType" CssClass="regtxt form-control" runat="server" ></asp:TextBox>
                                                                            <%--<asp:Label ID="VType" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                        <td><b>CS Status: <span style="color:red">*</span></b></td>
                                                                            <td> 
                                                                                <asp:DropDownList ID="ddlCSStatus" runat="server" class="form-control select2">
                                                                                    <asp:ListItem Value="Not Verified">Not Verified</asp:ListItem>
                                                                                    <asp:ListItem Value="Verified">Verified</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                      
                                                                            </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td><b>Chassis Number : <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VChassisNumber" MaxLength="30" CssClass="regtxtnumbersomespl form-control" runat="server" ></asp:TextBox>
                                                                            <%--<asp:Label ID="VChassisNumber" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                        <td><b>Engine Number  : <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VEngineNumber" MaxLength="60" CssClass="regtxtnumbersomespl form-control" runat="server" ></asp:TextBox>
                                                                            <%--<asp:Label ID="VEngineNumber" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Maker Description :<span style="color: red">*</span> </b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VMakerDesc" CMaxLength="60" CssClass="regtxtsomespl form-control" runat="server" ></asp:TextBox>
                                                                            <%--<asp:Label ID="VMakerDesc" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                        <td><b>Maker Model No :<span style="color: red">*</span> </b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VMakerModel" MaxLength="60" CssClass="regtxtnumbersomespl form-control" runat="server" ></asp:TextBox>
                                                                            <%--<asp:Label ID="VMakerModel" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Fuel Description : <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VFuelDesc" MaxLength="6" CssClass="regtxt form-control" runat="server" ></asp:TextBox>
                                                                            <%--<asp:Label ID="VFuelDesc" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                        <td><b>Color :<span style="color: red">*</span> </b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VColor" MaxLength="30" CssClass="regtxt form-control" runat="server" ></asp:TextBox>
                                                                            <%--<asp:Label ID="VColor" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Fitness Valid Upto: <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                <input type="text" id="VFitness" class="form-control"  readonly="true" runat="server" />
                                                                                <span class="input-group-btn">
                                                                                    <button id="btn1" class="btn default" type="button"  runat="server">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </button>
                                                                                </span>
                                                                            </div>
                                                                            <%--<asp:Label ID="VFitness" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                        <td><b>Tax Paid UpTo:<span style="color: red">*</span> </b></td>
                                                                        <td>
                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                <input type="text" id="VTax"  class="form-control" readonly="true" runat="server" />
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

                                                                        <td><b>U. L. W. as per RC: <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VULW"  CssClass="number form-control" runat="server" ></asp:TextBox>
                                                                            <%--<asp:Label ID="VULW" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                        <td><b>R. L. W. as per RC: <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VRLW"  CssClass="number form-control" runat="server"></asp:TextBox>
                                                                            <%--<asp:Label ID="VRLW" runat="server"></asp:Label>--%>

                                                                        </td>

                                                                    </tr>
                                                                    <tr>


                                                                        <td><b>Seating Capacity: <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="VSeatCapacity" MaxLength="3" CssClass="number form-control" runat="server" ></asp:TextBox>
                                                                            <%--<asp:Label ID="VSeatCapacity" runat="server"></asp:Label>--%>

                                                                        </td>
                                                                        <td><b>Manufacturing Date: <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <div id='container'>
                                                                                <input id="VManufacturingON" type="text" runat="server" class="form-control" />
                                                                            </div>
                                                                        </td>

                                                                    </tr>

                                                                    <tr>
                                                                        <td><b>Insurance Certificate Valid Upto <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                <input type="text" id="txtICValidUpto" class="form-control" readonly="true" runat="server" />
                                                                                <span class="input-group-btn">
                                                                                    <button class="btn default" type="button">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </button>
                                                                                </span>
                                                                            </div>
                                                                        </td>
                                                                        <td><b>Pollution Certificate Valid Upto <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            <div class="input-group input-medium date date-picker" data-date-format="dd/mm/yyyy" >
                                                                                <input type="text" id="txtPCValidUpto" class="form-control" readonly="true" runat="server" />
                                                                                <span class="input-group-btn">
                                                                                    <button class="btn default" type="button">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </button>
                                                                                </span>
                                                                            </div>
                                                                        </td>


                                                                    </tr>

                                                                </table>

                                                                <div class="col-md-12">
                                                                    <asp:HiddenField ID="hdnApp_ID" runat="server"></asp:HiddenField>
                                                                    <asp:HiddenField ID="HDRegisteredat" runat="server"></asp:HiddenField>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="portlet box green">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            Route Details <span style="color: red">*</span>
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                        </div>
                                                    </div>

                                                    <div class="portlet-body">
                                                        <br />
                                                        <div class="row" id="RouteHDRInfo" runat="server">

                                                        </div>
                                                        <asp:HiddenField ID="txtroutjson" runat="server" />
                                                        
                                                                    
                                                                    <asp:HiddenField ID="txtNoRoundTrip" runat="server" />
                                                        <br />

                                                        Time Format: 24 Hours
                                                        <br />
                                                        <div id="RouteTimeInfo" runat="server"></div>

                                                    </div>
                                                </div>
                                                <div class="portlet box green">
                                                    <div class="portlet-title">
                                                        <div class="caption">
                                                            Other Details
                                                        </div>
                                                        <div class="tools">
                                                            <a href="javascript:;" class="collapse"></a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body">
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-xs-12">
                                                                <table class="table table-striped table-bordered table-hover">
                                                                    <tr>
                                                                        <td><b>Mobile No : </b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMobileNo" MaxLength="10" CssClass="number form-control" runat="server"></asp:TextBox></td>
                                                                        <td><b>Email ID : </b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox></td>

                                                                    </tr>
                                                                    <tr>


                                                                        <td><b>Gender : </b></td>
                                                                        <td>
                                                                            <div class="radio-list" style="padding-left: 25px">
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="gender" id="txtMale" value="Male" checked/>
                                                                                    Male
																		
                                                                                </label>
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="gender" id="txtFemale" value="Female" />
                                                                                    Female
																		
                                                                                </label>
                                                                            </div>
                                                                        </td>
                                                                        <td><b>Qualification : </b></td>
                                                                        <td>
                                                                            <select id="ddlQualification" name="ddlQualification" onchange="javascript:fnQualification();" class="form-control select2" runat="server">

                                                                           
                                                                                <option value="Master">Master</option>
                                                                                <option value="Bachelor">Bachelor</option>
                                                                                <option value="Intermediate">Intermediate</option>
                                                                                <option value="Matriculation">Matriculation</option>
                                                                                <option value="Illiterate">Illiterate</option>

                                                                            </select></td>
                                                                    </tr>

                                                                    <tr>


                                                                        <td><b>Category of Applicant : </b></td>
                                                                        <td>
                                                                            <select id="ddlCategory" name="ddlCategory" onchange="javascript:fnCategory();" class="form-control select2" runat="server">

                                                                              
                                                                                <option value="Gen">Gen</option>
                                                                                <option value="SC">SC</option>
                                                                                <option value="ST">ST</option>
                                                                                <option value="OBC">OBC</option>

                                                                            </select></td>
                                                                        <td><b>Ex-Army category : </b></td>
                                                                        <td>
                                                                            <div class="radio-list" style="padding-left: 25px">
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="ExArmy" onchange="javascript:fnExArmy();" id="txtExArmyY" value="Yes"  />
                                                                                    Yes 
																		
                                                                                </label>
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="ExArmy" onchange="javascript:fnExArmy();" id="txtExArmyN" value="No" checked/>
                                                                                    No
																		
                                                                                </label>
                                                                                &nbsp;
                                                
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>



                                                                        <td><b>Minority : </b></td>
                                                                        <td>
                                                                            <div class="radio-list" style="padding-left: 25px">
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="Minority" id="txtMinorityY" value="Yes" />
                                                                                    Yes 
																		
                                                                                </label>
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="Minority" id="txtMinorityN" value="No" checked />
                                                                                    No
																		
                                                                                </label>
                                                                                &nbsp;
                                                
                                                                            </div>
                                                                        </td>
                                                                        <td><b>Handicapped : </b></td>
                                                                        <td>
                                                                            <div class="radio-list" style="padding-left: 25px">
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="Handicapped" onchange="javascript:fnHandicapped();" id="txtHandicappedY" value="Yes" />
                                                                                    Yes 
																		
                                                                                </label>
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="Handicapped" onchange="javascript:fnHandicapped();" id="txtHandicappedN" value="No" checked />
                                                                                    No
																		
                                                                                </label>
                                                                                &nbsp;
                                                
                                                                            </div>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>


                                                                        <td><b>Heavy Motor Licence  : </b></td>
                                                                        <td>
                                                                            <div class="radio-list" style="padding-left: 25px">
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="HML" onchange="javascript:fnHML();" id="txtHMLY" value="Yes" />
                                                                                    Yes 
																		
                                                                                </label>
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="HML" onchange="javascript:fnHML();" id="txtHMLN" value="No" checked />
                                                                                    No
																		
                                                                                </label>
                                                                                &nbsp;
                                                
                                                                            </div>
                                                                        </td>
                                                                        <td><b>Bus Type (Sleeper) :</b></td>
                                                                        <td>
                                                                            <div class="radio-list" style="padding-left: 25px">
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="Sleeper" id="txtSleeperY" value="Yes" />
                                                                                    Yes 
																		
                                                                                </label>
                                                                                <label class="radio-inline">


                                                                                    <input type="radio" runat="server" name="Sleeper" id="txtSleeperN" value="No" checked />
                                                                                    No
																		
                                                                                </label>
                                                                                &nbsp;
                                                
                                                                            </div>
                                                                        </td>

                                                                    </tr>
                                                                    <tr>


                                                                        <td><b>Bus Type (AC) : </b></td>
                                                                        <td>
                                                                            <div class="radio-list" style="padding-left: 25px">
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="AC" id="txtACY" value="Yes" />
                                                                                    Yes 
																		
                                                                                </label>


                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="AC" id="txtACN" value="No" checked />
                                                                                    No
																		
                                                                                </label>
                                                                                &nbsp;
                                                
                                                                            </div>
                                                                        </td>
                                                                        <td><b>Bus Type (Deluxe) : </b></td>
                                                                        <td>
                                                                            <div class="radio-list" style="padding-left: 25px">
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="Deluxe" id="txtDeluxeY" value="Yes" />
                                                                                    Yes 
																		
                                                                                </label>
                                                                                <label class="radio-inline">


                                                                                    <input type="radio" runat="server" name="Deluxe" id="txtDeluxeN" value="No"  checked/>
                                                                                    No
																		
                                                                                </label>
                                                                                &nbsp;
                                                
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Identity Proof : </b></td>
                                                                        <td>
                                                                            <div class="radio-list" style="padding-left: 25px">
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="IdentityProof"  id="txtIdentityProofV" value="Voter Id" checked />
                                                                                   Voter Id 
																		
                                                                                </label>
                                                                                <label class="radio-inline">
                                                                                    <input type="radio" runat="server" name="IdentityProof"  id="txtIdentityProofP" value="Pan Card" />
                                                                                    Pan Card
																		
                                                                                </label>
                                                                                &nbsp;
                                                  <label class="radio-inline">
                                                      <input type="radio" runat="server" name="IdentityProof"  id="txtIdentityProofA" value="Aadhaar Card" />
                                                      Aadhaar Card
																		
                                                  </label>
                                                                                &nbsp;
                                                                            </div>
                                                                        </td>

                                                                        <td><b>Voter Id/ Pan Card/ Aadhaar Card No.</b></td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtIdentityNo" CssClass="regtxtnumber form-control" runat="server"></asp:TextBox></td>



                                                                    </tr>
                                                                </table>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 col-sm-12" style="text-align: center">
                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn green" Text="Submit" OnClick="btnSave_Click" OnClientClick="return SaveApplication()" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Close" OnClientClick="Javascript: window.close(); return false;" />
                                                
                                                </div>


                                            </div>
                                            </div>
                                       
                                                   
                                 
                                              </asp:Panel>
   </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID ="btnSave" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        
                                          


                                              

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
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
    
    <script src="assets/js/SCREEN_PERMIT_REPORT.js" type="text/javascript"></script>
    <script src="assets/js/monthpicker.min.js"></script>
    <script src="assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js" type="text/javascript"></script>
    <script src="assets/js/form-input-mask1.js" type="text/javascript"></script>
 
     <script type="text/javascript">
         $(document).ready(function () {
           
             var getdate = new Date();
        
             var  yearstart  = parseInt(getdate.getFullYear()) - parseInt(12);
             var monthsStart = parseInt(getdate.getMonth()) + parseInt(1);
             var startdate = monthsStart + "/" + yearstart;
             var endyear = getdate.getFullYear();
             var monthEnd = parseInt(getdate.getMonth() );
             var endDate = monthEnd + "/" + endyear;
             $('input[id*=VManufacturingON]').Monthpicker({ minYear: yearstart, maxYear: endyear, minValue: startdate , maxValue: endDate  })
             Monthpicker.Init();
          
         });

         function fnApplicationValidate() {
             $('.form-control').css('border-color', '#c2cad8');
             var VOName = $('input[id*=VOName]').val().trim();
             if (VOName == "" || VOName == null) {
                 $('input[id*=VOName]').css('border-color', 'red').focus();
                 alert("Please enter Name");
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
             var VORegNumber = $('input[id*=VORegNumber]').val().trim();
             if (VORegNumber == "" || VORegNumber == null) {
                 alert("Please enter Vehicle Registration Number");
                 $('input[id*=VORegNumber]').css('border-color', 'red').focus();
                 return false;
             }
             var permitNo = $('input[id*=txtpermitNo]').val().trim();
             if (permitNo == "" || permitNo == null) {
                 alert("Please enter valid permit number");
                 $('input[id*=txtpermitNo]').css('border-color', 'red').focus();
                 return false;
             }
             var permitFrom = $('input[id*=txtpermitFrom]').val().trim();
             if (permitFrom == "" || permitFrom == null) {
                 alert("Please select permit valid from date");
                 $('input[id*=txtpermitFrom]').css('border-color', 'red').focus();
                 return false;
             }
             var permitTo = $('input[id*=txtpermitTo]').val().trim();
             if (permitTo == "" || permitTo == null) {
                 alert("Please select permit valid to date");
                 $('input[id*=txtpermitTo]').css('border-color', 'red').focus();
                 return false;
             }




             var VType = $('input[id*=VType]').val().trim();
             if (VType == "" || VType == null) {
                 $('input[id*=VType]').css('border-color', 'red').focus();
                 alert("Please enter Vehicle class description");
                 return false;
             }
             if (VType.toUpperCase().indexOf("BUS") === -1) {
                 alert("Vehicle class Should be BUS");
                 $('input[id*=VType]').css('border-color', 'red').focus();
                 return false;
             }
             var VChassisNumber = $('input[id*=VChassisNumber]').val().trim();
             if (VChassisNumber == "" || VChassisNumber == null) {
                 $('input[id*=VChassisNumber]').css('border-color', 'red').focus();
                 alert("Please enter Vehicle chasis number");
                 return false;
             }
             var VEngineNumber = $('input[id*=VEngineNumber]').val().trim();
             if (VEngineNumber == "" || VEngineNumber == null) {
                 $('input[id*=VEngineNumber]').css('border-color', 'red').focus();
                 alert("Please enter Vehicle Engine Number");
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
                 $('input[id*=VFitness]').css('border-color', 'red').focus();
                 alert("Please enter fitness valid upto date");
                 return false;
             }
             var VTax = $('input[id*=VTax]').val().trim();
             if (VTax == "" || VTax == null) {
                 $('input[id*=VTax]').css('border-color', 'red').focus();
                 alert("Please enter tax valid upto date");
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
             //if (VULW.length < 2) {
             //    alert("Enter atleast 2 chars in the U. L. W. as per RC textbox");
             //    $('input[id*=VULW]').css('border-color', 'red').focus();
             //    return false;
             //}

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
             //if (VRLW.length < 2) {
             //    alert("Enter atleast 2 chars in the R. L. W. as per RC textbox");
             //    $('input[id*=VRLW]').css('border-color', 'red').focus();
             //    return false;
             //}
             var VSeatCapacity = $('input[id*=VSeatCapacity]').val().trim();
             if (VSeatCapacity == "" || VSeatCapacity == null) {
                 alert("Please enter seat capacity");
                 $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
                 return false;
             }
             if (VSeatCapacity < parseInt(7)) {
                 alert("Minimum 7 Seat capacity required");
                 $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
                 return false;
             }
             var VManufacturingON = $('input[id*=VManufacturingON]').val().trim();
             if (VManufacturingON == "" || VManufacturingON == null) {
                 $('input[id*=VManufacturingON]').css('border-color', 'red').focus();
                 alert("Please enter Manufacturing date");
                 return false;
             }
             

             var MobileNo = $('input[id*=txtMobileNo]').val().trim();
             //if (MobileNo == "" || MobileNo == null) {
             //    $('input[id*=txtMobileNo]').css('border-color', 'red').focus();
             //    alert("Please enter user mobile no");
             //    return false;
             //}
             if (MobileNo != "") {
                 if (!validateMob(MobileNo)) {
                     $('input[id*=txtMobileNo]').css('border-color', 'red').focus();
                     alert("Invalid Mobile Number");
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
             if ($("select[name*='ddlQualification']").val() == "0" || $("select[name*='ddlQualification']").val() == null || $("select[name*='ddlQualification']").val() == "") {
                 alert("Please select qualification");
                 return false;
             }
             if ($("select[name*='ddlCategory']").val() == "0" || $("select[name*='ddlCategory']").val() == null || $("select[name*='ddlCategory']").val() == "") {
                 alert("Please select Category");
                 return false;
             }
             if ($("input[name*='ExArmy']:checked").length <= 0) {
                 alert("Please select ex-army category option");
                 return false;
             }
             if ($("input[name*='Minority']:checked").length <= 0) {
                 alert("Please select minority option");
                 return false;
             }
             if ($("input[name*='Handicapped']:checked").length <= 0) {
                 alert("Please select handicapped option");
                 return false;
             }
             if ($("input[name*='HML']:checked").length <= 0) {
                 alert("Please select heavy motor licence option");
                 return false;
             }
             if ($("input[name*='IdentityProof']:checked").length <= 0) {
                 alert("Please select Identity Proof option");
                 return false;
             }
             if ($("input[name*='AC']:checked").length <= 0) {
                 alert("Please select bus type (AC) option");
                 return false;
             }
             if ($("input[name*='Deluxe']:checked").length <= 0) {
                 alert("Please select bus type (Deluxe) option");
                 return false;
             }
             if ($("input[name*='Sleeper']:checked").length <= 0) {
                 alert("Please select bus type (Sleeper) option");
                 return false;
             }



             if ($('input[id*=txtICValidUpto]').val() == null || $('input[id*=txtICValidUpto]').val() == "") {
                 alert("Please select Insurance Certificate Validity Date");
                 return false;
             }

             if ($('input[id*=txtPCValidUpto]').val() == null || $('input[id*=txtPCValidUpto]').val() == "") {
                 alert("Please select Pollution Certificate Validity Date");
                 return false;
             }

             return true;
         }
         function timeFunction(id) {
             var objArray = $('#' + id).val().split(':');
             if (parseInt(objArray[0]) >= parseInt('0') && parseInt(objArray[0]) <= parseInt('23')) {
                 if (parseInt(objArray[1]) >= parseInt('0') && parseInt(objArray[1]) <= parseInt('59')) {

                 }
                 else {
                     $('#' + id).val('');
                 }
             }
             else {
                 $('#' + id).val('');
             }


         }

         function SaveApplication() {
             if (fnApplicationValidate()) {
                
                 var PERMIT_APPLICATION_LDRs = new Array();
                 var condition = true;
                 var NoRoundTrip = $('input[id*=txtNoRoundTrip]').val();

                 for (j = 1; j <= NoRoundTrip; j++) {
                     var x1 = 1;
                     var length1 = $('#tbdRouteInfo_' + j).find('tr').length;
                     var length = $('#tbdRouteInfo_' + j + ' tr:last').attr('id').split("_");
                     $('#tbdRouteInfo_' + j + '  tr').each(function () {
                         //debugger;
                         var parentid = $(this).attr('id').split("_");
                         //alert(parentid);
                         if (x1 == 1) {
                             if ($('#txtUPDepRoute1_1').val().trim() == "") {
                                 condition = false;
                                 alert("Please enter Departure Time of UP for " + $('#lblUPRoute1_' + parentid[1]).text());
                                 return false;

                             }
                             else if ($('#txtDownDepRoute1_1').val().trim() == "") {
                                 condition = false;
                                 alert("Please enter Departure Time of Down for " + $('#lblDownRoute1_' + parentid[1]).text());
                                 return false;

                             }
                             else {
                                 var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                                 var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                                 var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();
                                 var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();
                                 PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: UPDepTime.toString(), MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                                 PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: DownDepTime.toString(), MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });

                             }

                         }
                         else if (j * length1 == parentid[1]) {
                             if ($('#txtUPArrRoute1_' + length1).val().trim() == "") {
                                 condition = false;
                                 alert("Please enter Arrival Time of UP for " + $('#lblUPRoute1_' + parentid[1]).text());
                                 return false;

                             }
                             else if ($('#txtDownArrRoute1_' + length1).val().trim() == "") {
                                 condition = false;
                                 alert("Please enter Arrival Time of Down for " + $('#lblDownRoute1_' + parentid[1]).text());
                                 return false;

                             }


                             else {
                                 var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                                 var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                                 var UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();
                                 var DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();


                                 PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime.toString(), DEPARTURE_TIMING: "", MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                                 PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime.toString(), DEPARTURE_TIMING: "", MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });


                             }
                         }
                         else {
                             if (x1 != 1) {


                                 var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                                 var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                                 var UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();
                                 var DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();
                                 var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();
                                 var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();
                                 

                                 PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime.toString(), DEPARTURE_TIMING: UPDepTime.toString(), MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                                 PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime.toString(), DEPARTURE_TIMING: DownDepTime.toString(), MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });




                             }
                         }
                         x1++;

                     });

                 }

                 if (condition == false) {
                     return false;

                 }
                 else {
                     //var RouteHdr = $("input[name='routeHdr']:checked").val();

                     var xxx = JSON.stringify({ ROUTE_DETAIL_ID: 0, PERMIT_APPLICATION_LDRs: PERMIT_APPLICATION_LDRs });

                     $('input[id*=txtroutjson]').val(xxx);
                     $('#loadingmessage').modal('show');
                     return true;
                 }


             }
             else {

                 return false;
             }
         }

         </script>
   
</asp:Content>

