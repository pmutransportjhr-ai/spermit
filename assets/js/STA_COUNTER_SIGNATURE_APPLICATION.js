$(document).ready(function () {
 $('input[id*=VORegNumber]').on('blur', function () {
        var registrationNumber = $(this).val();
        var registrationNumberExp = '^[a-zA-Z]{2}[0-9]{2}[a-zA-Z]{1,2}[0-9]{3,}$';
        var regtxt = new RegExp(registrationNumberExp);
        if (regtxt.test(registrationNumber) == false) {
            alert('Please enter valid registration number.');
            $('input[id*=VORegNumber]').val('');
        }
    });

    fnManufactDate();
});
function fnManufactDate()
{
    
    var getdate = new Date();
    var yearstart = parseInt(getdate.getFullYear()) - parseInt(12);
    var monthsStart = parseInt(getdate.getMonth()) + parseInt(1);
    var startdate = monthsStart + "/" + yearstart;
    var endyear = getdate.getFullYear();
    var monthEnd = parseInt(getdate.getMonth());
    var endDate = monthEnd + "/" + endyear;
    $('input[id*=VManufacturingON]').Monthpicker({ minYear: yearstart, maxYear: endyear, minValue: startdate, maxValue: endDate })
    Monthpicker.Init();
    

}

function fnShowRoute(ID)
{
    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/STA_PERMIT.ashx?caller=GetSTARouteDetailsCS&q=" + encodeURIComponent(ID),
        data:"",
        cache: false,
        async: false,
        success: function (data) {

            $('#loadingmessage').modal('hide');
            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {

                    
                    $('div[id*=RouteTimeInfo]').html(data.split(/~(.+)?/)[1].toString());
                    ComponentsDateTimePickers.init();
                    
                    $(".mask_route").inputmask("99:99", {
                        placeholder: "0",

                    });
                   
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
                    if ($('#txtUPDepRoute1_1').val().trim() == "" || $('#txtUPDepRoute1_1').val().trim() == "0:00") {
                        condition = false;
                        alert("Please enter Arrival  Time for " + $('#lblUPRoute1_' + parentid[1]).text());
                        return false;

                    }
                    else if ($('#txtDownDepRoute1_1').val().trim() == "" || $('#txtDownDepRoute1_1').val().trim() == "0:00") {
                        condition = false;
                        alert("Please enter Departure Time for " + $('#lblDownRoute1_' + parentid[1]).text());
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
                        alert("Please enter Arrival Time for " + $('#lblUPRoute1_' + parentid[1]).text());
                        return false;

                    }
                    else if ($('#txtDownArrRoute1_' + length[1]).val().trim() == "") {
                        condition = false;
                        alert("Please enter Departure  Time for " + $('#lblDownRoute1_' + parentid[1]).text());
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
                       
                        var UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();                       
                        var DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();                       
                        var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();                       
                        var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();                        
                        var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                        var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                        //var UPArrTime = $('#txtUPArrRoute_' + parentid[1]).val().trim();
                        //var DownArrTime = $('#txtDownArrRoute_' + parentid[1]).val().trim();
                        //var UPDepTime = $('#txtUPDepRoute_' + parentid[1]).val().trim();
                        //var DownDepTime = $('#txtDownDepRoute_' + parentid[1]).val().trim();
                        //alert(UPCity);
                        //alert(DownCity);
                        //alert(UPArrTime);
                        //alert(DownArrTime);
                        //alert(UPDepTime);
                        //alert(DownDepTime);

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
            var RouteHdr = $("input[name='routeHdr']:checked").val();
            var xxx = JSON.stringify({ ROUTE_DETAIL_ID: RouteHdr, PERMIT_APPLICATION_LDRs: PERMIT_APPLICATION_LDRs });
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

function fnClearPermitNew()
{
    $('input[id*=VOName]').val('');
    $('input[id*=VOCareOf]').val('');
    $('textarea[id*=VOPresentAddress]').val('');
    $('textarea[id*=VOPermanentAddress]').val('');
    $('input[id*=VORegNumber]').val('');
    $('input[id*=txtPermitNo]').val('');
    $('input[id*=txtvalidityfrom]').val('');
    $('input[id*=txtvalidityto]').val('');
    $('input[id*=VMakerDesc]').val('');
    $('input[id*=VMakerModel]').val('');
    $('select[id*=ddlCategory]').val('0');
    $('input[id*=VChassisNumber]').val('');
    $('input[id*=VEngineNumber]').val('');
    $('input[id*=VSeatCapacity]').val('');
    $('input[id*=VGVW]').val('');
    $('input[id*=txtPayLoad]').val('');
    $('input[id*=VManufacturingON]').val('');
    $('input[id*=txtNatureOfGoods]').val('');
    $('input[id*=txtMobileNo]').val('');
    $('input[id*=txtEmailId]').val('');
    $('input[id*=txtICValidUpto]').val('');
    $('input[id*=txtFCValidUpto]').val('');
    $('input[id*=txtPCValidUpto]').val('');
    $('input[id*=txtTPValidUpto]').val('');



    $('input[id*=txtRecommendationNo]').val('');
    $('input[id*=txtRecommendationNoDated').val('');
    $('input[id*=recommendationUpload').val('');
    $('input[id*=txtULW').val('');
    $('input[id*=txtRLW').val('');

    
    
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
    
    var VORegistration = $('input[id*=VORegNumber]').val().trim();
    if (VORegistration == "" || VORegistration == null) {
        alert("Please enter Registration Number");
        $('input[id*=VORegNumber]').css('border-color', 'red').focus();
        return false;
    }
    var VOPermit = $('input[id*=txtPermitNo]').val().trim();
    if (VOPermit == "" || VOPermit == null) {
        alert("Please enter Permit Number");
        $('input[id*=txtPermitNo]').css('border-color', 'red').focus();
        return false;
    }
    var validityfrom = $('input[id*=txtvalidityfrom]').val().trim();
    if (validityfrom == "" || validityfrom == null) {
        alert("Please enter Permit Validity From");
        $('input[id*=txtvalidityfrom]').css('border-color', 'red').focus();
        return false;
    }
    var validityto = $('input[id*=txtvalidityto]').val().trim();
    if (validityto == "" || validityto == null) {
        alert("Please enter Permit Validity Upto");
        $('input[id*=txtvalidityto]').css('border-color', 'red').focus();
        return false;
    }
    var VType = $('select[id*=ddlCategory]').val();

    if (VType == "0" || VType == "" || VType == null) {
        alert("Please select Vehicle Type");
        $('select[id*=ddlCategory]').css('border-color', 'red').focus();
        return false;
    }
    //if (VType.toUpperCase().indexOf("BUS")===-1)
    //{
    //    alert("Vehicle class Should be BUS");
    //    $('input[id*=VType]').css('border-color', 'red').focus();
    //    return false;
    //}
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
   
    var VSeatCapacity = $('input[id*=VSeatCapacity]').val().trim();
    if (VSeatCapacity == "" || VSeatCapacity == null) {
        alert("Please enter seat capacity");
        $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
        return false;
    }
    if (parseInt(VSeatCapacity) < parseInt(7)) {
        alert("Minimum 7 Seat and Maximum 74 seat capacity required");
        $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
        return false;
    }
    if (parseInt(VSeatCapacity) > parseInt(74)) {

        alert("Minimum 7 Seat and Maximum 74 seat capacity required");
        $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
        return false;
    }
    var ulw = $('input[id*=txtULW]').val().trim();
    if (ulw == "" || ulw == null) {
        alert("Please enter U.L.W value as per RC ");
        $('input[id*=txtULW]').css('border-color', 'red').focus();
        return false;
    }
    
   
    var rlw = $('input[id*=txtRLW]').val().trim();
    if (rlw == "" || rlw == null) {
        alert("Please enter R.L.W value as per RC");
        $('input[id*=txtRLW]').css('border-color', 'red').focus();
        return false;
    }
   
    var VManufacturingON = $('input[id*=VManufacturingON]').val().trim();
    if (VManufacturingON == "" || VManufacturingON == null) {
        alert("Please enter Manufacturing date");
        $('input[id*=VManufacturingON]').css('border-color', 'red').focus();
        return false;
    }
    var StateIssuedBy = $('select[id*=ddlStateIssuedBy]').val();
    if (StateIssuedBy == "0" || StateIssuedBy == "" || StateIssuedBy == null) {
        alert("Please select State");
        return false;
    }
    if ($('div[id*=RouteTimeInfo]').html() == null || $('div[id*=RouteTimeInfo]').html() == "") {
        alert("Route time table is not displaying. So you can not apply for counter signature right now");
        return false;
    }
   
    var MobileNo = $('input[id*=txtMobileNo]').val().trim();
    if (MobileNo == "" || MobileNo == null) {
        alert("Please enter user mobile no");
        $('input[id*=txtMobileNo]').css('border-color', 'red').focus();
        return false;
    }
    if (!validateMob(MobileNo)) {
        alert("Invalid Mobile Number");
        $('input[id*=txtMobileNo]').css('border-color', 'red').focus();
        return false;
    }
    var EmailID = $('input[id*=txtEmailId]').val().trim();
    if (EmailID != "" ) {

        if (!IsEmail(EmailID)) {
            alert("Invalid Email ID");
            $('input[id*=txtEmailId]').css('border-color', 'red').focus();
            return false;
        }
    }
    var RecommendationNo = $('input[id*=txtRecommendationNo]').val().trim();
    if (RecommendationNo == "" || RecommendationNo == null) {
        alert("Please enter Recommendation Number");
        $('input[id*=txtRecommendationNo]').css('border-color', 'red').focus();
        return false;
    }
    if ($('input[id*=txtRecommendationNoDated]').val() == null || $('input[id*=txtRecommendationNoDated]').val() == "") {
        alert("Please select Recommendation Date");
        return false;
    }
    var RC = $('input[id*=uploadRC]').val();
    if (RC == "") {
        alert("Please upload  Registration Certificate");
        return false;
    }
    if (RC != "") {
        var fileUploadName = $('input[id*=uploadRC]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadRC]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadRC]').val('');

            alert("Plesae upload only PDF file for Registration Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadRC]').val('');
            alert("Registration Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }

    }
    var Speed = $('input[id*=uploadSpeed]').val();
    if (Speed == "") {
        alert("Please upload  Speed Governor Certificate");
        return false;
    }
    if (Speed != "") {
        var fileUploadName = $('input[id*=uploadSpeed]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadSpeed]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadSpeed]').val('');

            alert("Plesae upload only PDF file for Speed Governor Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadSpeed]').val('');
            alert("Speed Governor Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }

    }
   
    var Insurance = $('input[id*=uploadInsur]').val();
    if (Insurance == "") {
        alert("Please upload  Insurance Certificate");
        return false;
    }
    if (Insurance != "") {
        var fileUploadName = $('input[id*=uploadInsur]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadInsur]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadInsur]').val('');

            alert("Plesae upload only PDF file for Insurance Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadInsur]').val('');
            alert("Insurance Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }
    }
    if ($('input[id*=txtICValidUpto]').val() == null || $('input[id*=txtICValidUpto]').val() == "") {
        alert("Please select Insurance Certificate Validity Date");
        return false;
    }
    var Pollution = $('input[id*=uploadPollution]').val();
    if (Pollution == "") {
        alert("Please upload  Pollution Certificate");
        return false;
    }
    if (Pollution != "") {
        var fileUploadName = $('input[id*=uploadPollution]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadPollution]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadPollution]').val('');

            alert("Plesae upload only PDF file for Pollution Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadPollution]').val('');
            alert("Pollution Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }
    }
    if ($('input[id*=txtPCValidUpto]').val() == null || $('input[id*=txtPCValidUpto]').val() == "") {
        alert("Please select Pollution Certificate Validity Date");
        return false;
    }
    var Fitness = $('input[id*=uploadFitness]').val();
    if (Fitness == "") {
        alert("Please upload  Fitness Certificate");
        return false;
    }
    if (Fitness != "") {

        var fileUploadName = $('input[id*=uploadFitness]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadFitness]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadFitness]').val('');

            alert("Plesae upload only PDF file for Fitness Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadFitness]').val('');
            alert("Fitness Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    if ($('input[id*=txtFCValidUpto]').val() == null || $('input[id*=txtFCValidUpto]').val() == "") {
        alert("Please select Fitness Certificate Validity Date");
        return false;
    }

    var Tax = $('input[id*=uploadTax]').val();
    if (Tax == "") {
        alert("Please upload  Tax Paid Upto Certificate");
        return false;
    }
    if (Tax != "") {

        var fileUploadName = $('input[id*=uploadTax]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadTax]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadTax]').val('');

            alert("Plesae upload only PDF file for Tax Paid Upto Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadTax]').val('');
            alert("Tax Paid Upto Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    if ($('input[id*=txtTPValidUpto]').val() == null || $('input[id*=txtTPValidUpto]').val() == "") {
        alert("Please select Tax Paid Upto Certificate Validity Date");
        return false;
    }
    var Permit = $('input[id*=uploadpermit]').val();
    if (Permit == "") {
        alert("Please upload  Permit Certificate");
        return false;
    }
    if (Permit != "") {
        var fileUploadName = $('input[id*=uploadpermit]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadpermit]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadpermit]').val('');

            alert("Plesae upload only PDF file for Permit Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadpermit]').val('');
            alert("Permit Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }

    }
    var timetable = $('input[id*=uploadtimetable]').val();
    if (timetable == "") {
        alert("Please upload  Time Table Certificate");
        return false;
    }
    if (timetable != "") {
        var fileUploadName = $('input[id*=uploadtimetable]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadtimetable]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadtimetable]').val('');

            alert("Plesae upload only PDF file for Time Table Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadtimetable]').val('');
            alert("Time Table Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }

    }
    var recommendation = $('input[id*=recommendationUpload]').val();
    if (recommendation == "") {
        alert("Please upload  Recommendation Letter");
        return false;
    }
    if (recommendation != "") {
        var fileUploadName = $('input[id*=recommendationUpload]')[0].files[0].name;
        var filesize = parseInt($('input[id*=recommendationUpload]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=recommendationUpload]').val('');

            alert("Plesae upload only PDF file for Recommendation Letter");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=recommendationUpload]').val('');
            alert("Recommendation Letter must be greater than 0 MB and less than 2 MB");

            return false;

        }

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