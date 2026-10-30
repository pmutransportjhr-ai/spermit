jQuery(document).ready(function () {

    if ($('input[id*=MFSERVER]').val() != 'YES') {
        fnManufactDate();
    }
})

function fnApplyPermit(ID) {
    
    $('input[id*=routeID]').val(ID);
    $('div[id*=vehicleRegistration]').show();
    $('input[id*=txtVehicleNumber]').val('');
    $('span[id*=lblMSg]').text('');
    
}
function fnShowRoute(ID)
{
    var RouteAction = $('select[id*=ddlRouteAction]').val();
    if (RouteAction == "0" || RouteAction == "" || RouteAction == null)
    {
        $('div[id*=RouteTimeInfo]').html("");
    }
   
    else
        {
    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/RTA_PERMIT.ashx?caller=GetRTARouteDetails1&q=" + encodeURIComponent(ID) + "&q1=" + RouteAction,
        data:"",
        cache: false,
        async: false,
        success: function (data) {

            $('#loadingmessage').modal('hide');
            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {
                    
                    $('input[id*=NoRoundTrip]').val(RouteAction);
                    $('div[id*=RouteTimeInfo]').html(data.split(/~(.+)?/)[1].toString());
                    ComponentsDateTimePickers.init();
                    var NoRoundTrip = $('#txtNoRoundTrip').val();

                    for (j = 1; j <= NoRoundTrip; j++) {
                        var length = $('#tbdRouteInfo_' + j + ' tr:last').attr('id').split("_");
                        $('#tbdRouteInfo_' + j + '  tr').each(function () {
                            var parentid = $(this).attr('id').split("_");

                            if ($('#txtUPArrRoute' + j + '_' + parentid[1]).attr("placeholder", "_")) {
                                $('#txtUPArrRoute' + j + '_' + parentid[1]).inputmask("99:99", { placeholder: "0" })
                            }

                            if ($('#txtDownArrRoute' + j + '_' + parentid[1]).attr("placeholder", "_")) {
                                $('#txtDownArrRoute' + j + '_' + parentid[1]).inputmask("99:99", { placeholder: "0" })
                            }

                            if ($('#txtUPDepRoute' + j + '_' + parentid[1]).attr("placeholder", "_")) {
                                $('#txtUPDepRoute' + j + '_' + parentid[1]).inputmask("99:99", { placeholder: "0" })

                            };

                            if ($('#txtDownDepRoute' + j + '_' + parentid[1]).attr("placeholder", "_")) {

                                $('#txtDownDepRoute' + j + '_' + parentid[1]).inputmask("99:99", { placeholder: "0" });
                            }


                        });
                    }
                }
                else
                {
                    $('div[id*=RouteTimeInfo]').html("");
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
function SaveApplication() {
    if (fnApplicationValidate())
    {
        
        var PERMIT_APPLICATION_LDRs = new Array();
        var condition = true;
        var NoRoundTrip = $('#txtNoRoundTrip').val();

        for(j= 1; j<=NoRoundTrip; j++)
        {
            var length = $('#tbdRouteInfo_' + j + ' tr:last').attr('id').split("_");
            $('#tbdRouteInfo_' + j + '  tr').each(function () {
                //debugger;
                var parentid = $(this).attr('id').split("_");
                //alert(parentid);
                if (parentid[1] == 1) {
                    if ($('#txtUPDepRoute' + j + '_1').val().trim() == "") {
                        condition = false;
                        alert("Please enter Departure Time of UP for " + $('#lblUPRoute' + j + '_' + parentid[1]).text());
                        return false;

                    }
                    else if ($('#txtDownDepRoute' + j + '_1').val().trim() == "") {
                        condition = false;
                        alert("Please enter Departure Time of Down for " + $('#lblDownRoute' + j + '_' + parentid[1]).text());
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
                else if (parentid[1] == length[1]) {
                    if ($('#txtUPArrRoute1_' + length[1]).val().trim() == "") {
                        condition = false;
                        alert("Please enter Arrival Time of UP for " + $('#lblUPRoute1_' + parentid[1]).text());
                        return false;

                    }
                    else if ($('#txtDownArrRoute1_' + length[1]).val().trim() == "") {
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
                    if (parentid[1] != 1) {
                        var UPArrTime = "----";
                        var DownArrTime = "----";
                        var UPDepTime = "----";
                        var DownDepTime = "----";
                        if ($('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim() == "0:00") {
                            UPArrTime = "----";

                        }
                        else {
                            UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();

                        }
                        if ($('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim() == "0:00") {
                            DownArrTime = "----";

                        }
                        else {
                            DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();

                        }
                        if ($('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim() == "0:00") {
                            UPDepTime = "----";

                        }
                        else {
                            UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();

                        }
                        if ($('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim() == "0:00") {
                            DownDepTime = "----";

                        }
                        else {
                            DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();

                        }
                        var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                        var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                        

                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime.toString(), DEPARTURE_TIMING: UPDepTime.toString(), MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime.toString(), DEPARTURE_TIMING: DownDepTime.toString(), MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });




                    }
                }

            });

        }
       

        if (condition == false)
        {
            return false;

        }
        else
        {
            //var RouteHdr = $("input[name='routeHdr']:checked").val();

            var xxx = JSON.stringify({ ROUTE_DETAIL_ID: 0, PERMIT_APPLICATION_LDRs: PERMIT_APPLICATION_LDRs });

            $('input[id*=txtroutjson]').val(xxx);
            $('#loadingmessage').modal('show');
            return true;
        }


    }
    else
    {

        return false;
    }
}
function fnSearchValidate() {
    if ($('input[id*=txtVehicleNumber]').val() == null || $('input[id*=txtVehicleNumber]').val() == "") {
        alert("Please enter Registration Number");
        $('input[id*=txtVehicleNumber]').focus();
        return false;
    }
    var x = $('input[id*=txtVehicleNumber]').val();
    if (x.lenght < 4) {
        alert("Please enter Valid Jharkhand Vehicle Registration Number ");
        $('input[id*=txtVehicleNumber]').focus();
        return false;
    }
    //if (x.substring(0, 4).toUpperCase() == 'JH01' || x.substring(0, 4).toUpperCase() == 'JH02' ||
    //    x.substring(0, 4).toUpperCase() == 'JH03' || x.substring(0, 4).toUpperCase() == 'JH04' ||
    //    x.substring(0, 4).toUpperCase() == 'JH05' || x.substring(0, 4).toUpperCase() == 'JH06' ||
    //    x.substring(0, 4).toUpperCase() == 'JH07' || x.substring(0, 4).toUpperCase() == 'JH08' ||
    //    x.substring(0, 4).toUpperCase() == 'JH09' || x.substring(0, 4).toUpperCase() == 'JH10' ||
    //    x.substring(0, 4).toUpperCase() == 'JH11' || x.substring(0, 4).toUpperCase() == 'JH12' ||
    //    x.substring(0, 4).toUpperCase() == 'JH13' || x.substring(0, 4).toUpperCase() == 'JH14' ||
    //    x.substring(0, 4).toUpperCase() == 'JH15' || x.substring(0, 4).toUpperCase() == 'JH16' ||
    //    x.substring(0, 4).toUpperCase() == 'JH17' || x.substring(0, 4).toUpperCase() == 'JH18' ||
    //    x.substring(0, 4).toUpperCase() == 'JH19' || x.substring(0, 4).toUpperCase() == 'JH20' ||
    //    x.substring(0, 4).toUpperCase() == 'JH21' || x.substring(0, 4).toUpperCase() == 'JH22' ||
    //    x.substring(0, 4).toUpperCase() == 'JH23' || x.substring(0, 4).toUpperCase() == 'JH24'

    //    ) {

    //}
    //else {
    //    alert("Please enter Valid Jharkhand Vehicle Registration Number ");
    //    $('input[id*=txtVehicleNumber]').focus();
    //    return false;
    //}
    $('#loadingmessage').modal('show');
    return true;
    
}
function fnClearPermitNew()
{
    $('input[id*=VOName]').val('');
    $('input[id*=VOCareOf]').val('');
    $('input[id*=VOPresentAddress]').val('');
    $('input[id*=VOPermanentAddress]').val('');
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
    $('input[id*=VSeatCapacity]').val('');
    $('input[id*=VManufacturingON]').val('');
    $('input[id*=txtMobileNo]').val('');
    $('input[id*=txtEmailId]').val('');
    $('input[id*=txtICValidUpto]').val('');
    $('input[id*=txtFCValidUpto]').val('');
    $('input[id*=txtPCValidUpto]').val('');
    $('input:radio[name*="gender"]').prop('checked', false);
    $("input[id*=txtMale]").parent('span').removeClass('checked');
    $("input[id*=txtFemale]").parent('span').removeClass('checked');
    $('input[id*=txtIdentityNo]').val('');
    $("select[name*='ddlQualification']").val('0').change();
    $("select[name*='ddlCategory']").val('0').change();

    $('input:radio[name*="ExArmy"]').prop('checked', false);
    $("input[id*=txtExArmyY]").parent('span').removeClass('checked');
    $("input[id*=txtExArmyN]").parent('span').removeClass('checked');

    $('input:radio[name*="IdentityProof"]').prop('checked', false);
    $("input[id*=txtIdentityProofV]").parent('span').removeClass('checked');
    $("input[id*=txtIdentityProofP]").parent('span').removeClass('checked');
    $("input[id*=txtIdentityProofA]").parent('span').removeClass('checked');

    $('input:radio[name*="Minority"]').prop('checked', false);
    $("input[id*=txtMinorityY]").parent('span').removeClass('checked');
    $("input[id*=txtMinorityN]").parent('span').removeClass('checked');

    $('input:radio[name*="Handicapped"]').prop('checked', false);
    $("input[id*=txtHandicappedY]").parent('span').removeClass('checked');
    $("input[id*=txtHandicappedN]").parent('span').removeClass('checked');

    $('input:radio[name*="HML"]').prop('checked', false);
    $("input[id*=txtHMLY]").parent('span').removeClass('checked');
    $("input[id*=txtHMLN]").parent('span').removeClass('checked');

    $('input:radio[name*="AC"]').prop('checked', false);
    $("input[id*=txtACY]").parent('span').removeClass('checked');
    $("input[id*=txtACN]").parent('span').removeClass('checked');

    $('input:radio[name*="Deluxe"]').prop('checked', false);
    $("input[id*=txtDeluxeY]").parent('span').removeClass('checked');
    $("input[id*=txtDeluxeN]").parent('span').removeClass('checked');

    $('input:radio[name*="Sleeper"]').prop('checked', false);
    $("input[id*=txtSleeperY]").parent('span').removeClass('checked');
    $("input[id*=txtSleeperN]").parent('span').removeClass('checked');
    
}
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
    if (VType.toUpperCase().indexOf("BUS")===-1)
    {
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
        if ($('input[id*=VMakerDesc]').val().trim().length < 1) {
            alert("Enter atleast 3 chars in the maker description textbox");
            $('input[id*=VMakerDesc]').css('border-color', 'red').focus();
            return false;
        }
    }
    if ($('input[id*=VMakerModel]').val().trim().length != "") {

        if ($('input[id*=VMakerModel]').val().trim().length < 1) {
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
    //if ($('input[id*=VColor]').val().trim().length != "") {


    //    if ($('input[id*=VColor]').val().trim().length < 3) {
    //        alert("Enter atleast 3 chars in the color textbox");
    //        $('input[id*=VColor]').css('border-color', 'red').focus();
    //        return false;
    //    }
    //}

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
    //if (VULW <= parseInt(0)) {
    //    alert("Please enter valid data  in the U. L. W. as per RC textbox");
    //    $('input[id*=VULW]').css('border-color', 'red').focus();

    //    return false;
    //}
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
    //if (VRLW <= parseInt(0)) {
    //    alert("Please enter valid data  in the R. L. W. as per RC textbox");
    //    $('input[id*=VRLW]').css('border-color', 'red').focus();

    //    return false;
    //}
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
    var RouteTrip = $('select[id*=ddlRouteAction]').val();

    
    if (RouteTrip == "" || RouteTrip == "0" || RouteTrip == null) {
        alert("Please select number of round trips");
        return false;
    }

    if ($('div[id*=RouteTimeInfo]').html() == null || $('div[id*=RouteTimeInfo]').html() == "") {
        alert("Please select Route Details");
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
    if (EmailID != "" ) {

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
function fnManufactDate() {

    var getdate = new Date();
    var yearstart = parseInt(getdate.getFullYear()) - parseInt(12);
    var monthsStart = parseInt(getdate.getMonth()) + parseInt(1);
    var startdate = monthsStart + "/" + yearstart;
    var endyear = getdate.getFullYear();
    var monthEnd = parseInt(getdate.getMonth());
    var endDate = monthEnd + "/" + endyear;
    $('input[id*=VManufacturingON]').Monthpicker({ minYear: yearstart, maxYear: endyear, minValue: startdate, maxValue: endDate })
    //Monthpicker.Init();


}