<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-PERMIT-ROUTE-UPDATE.aspx.cs" Inherits="STA_PERMIT_ROUTE_UPDATE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <form runat="server" autocomplete="off">
            <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />



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
                                <span>Permit Route Update</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="portlet light portlet-fit">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="icon-settings font-red"></i>
                                                        <span class="caption-subject font-red sbold uppercase">Permit Route Update</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                           
                                                
                                                   
                                                   
                                                        <div class="row" style="align-content: center">
                                                            <div class="col-md-12 col-sm-12 col-xs-12" style="align-content: center">
                                                                <table class="table table-striped table-bordered table-hover">

                                                                    <tr>
                                                                        <td><b>Name : </b></td>
                                                                        <td>
                                                                            
                                                                             <asp:Label ID="VOName" runat="server"></asp:Label>

                                                                        </td>
                                                                        <td><b>Vehicle Registration No : </b></td>
                                                                        <td>
                                                                            
                                                                            <asp:Label ID="VORegNumber" runat="server"></asp:Label>

                                                                        </td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td><b>Application No :  </b></td>
                                                                        <td>
                                                                             <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td><b>Application Status :  <span style="color: red">*</span></b></td>
                                                                        <td>
                                                                            
                                                                             <asp:Label ID="lblApplicationStatus" runat="server"></asp:Label>

                                                                        </td>

                                                                    </tr>

                                                                    <tr>


                                                                       
                                                                        <td><b>Permit No :</b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblPermitNo" runat="server"></asp:Label>

                                                                        </td>
                                                                        <td><b>Permit Type :</b></td>
                                                                        <td>
                                                                            <asp:Label ID="lblPermitType" runat="server"></asp:Label>

                                                                        </td>
                                                                    </tr>
                                                                </table>

                                                            </div>
                                                        </div><br />
                                                   
                                                
                                                

                                               

                                                    
                                                         <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Route Details</u></b></h4>
                                                    </div>

                                                    <asp:Label CssClass="col-md-12 col-sm-12 control-label" Font-Bold="true" ID="RouteHDRInfo" runat="server" />
                                                </div>
                                                 <br />
                                                        <asp:HiddenField ID="hdnID" runat="server" />
                                                        <asp:HiddenField ID="hdnType" runat="server" />
                                                        <asp:HiddenField ID="txtroutjson" runat="server" />
                                                        
                                                                    
                                                                    <asp:HiddenField ID="txtNoRoundTrip" runat="server" />
                                                        <br />

                                                        Time Format: 24 Hours
                                                        <br />
                                                        <div id="RouteTimeInfo" runat="server"></div>

                                                     <div class="row">
                                                        <br /><br />
                                                    <div class="col-md-12" style="text-align:center">
                                                         <asp:Button ID="btnSubmit" runat="server" CssClass="btn  btn-success" Text="Submit " OnClientClick="return SaveApplication();" OnClick="btnYes_Click"  />
                                                         <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Cancel" OnClick="btnCancel_Click" />

                                                    </div>
                                                    
                                                </div>
                                                </div>
                                            </div>
                                    </div>
                            </div>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

       <script src="assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js" type="text/javascript"></script>
     <script src="assets/js/form-input-mask1.js" type="text/javascript"></script>
 
    <script type="text/javascript">
        
        
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

                                //alert(UPCity);
                                //alert(DownCity);
                                //alert(UPArrTime);
                                //alert(DownArrTime);
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
    </script>
   
</asp:Content>

