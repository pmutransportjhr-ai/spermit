$(document).ready(function () {
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
                   
                  // fnManufactDate();


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
                    if ($('#txtUPDepRoute1_1').val().trim() == "") {
                        condition = false;
                        alert("Please enter Arrival  Time for " + $('#lblUPRoute1_' + parentid[1]).text());
                        return false;

                    }
                    else if ($('#txtDownDepRoute1_1').val().trim() == "") {
                        condition = false;
                        alert("Please enter Departure Time for " + $('#lblDownRoute1_' + parentid[1]).text());
                        return false;

                    }
                    else {
                        var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                        var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                        var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();
                        var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();
                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: UPDepTime.toString(), MODE: "UP", ROUTE_DETAIL_ID: j,ID: parentid[1]});
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
                       
                       
                        var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                        var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                        var UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();
                        var DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();
                        var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();
                        var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();

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
function fnSearchValidate() {

    if ($('input[id*=txtVehicleNumber]').val() == null || $('input[id*=txtVehicleNumber]').val() == "") {
        alert("Please enter Vehicle Registration Number");
        $('input[id*=txtVehicleNumber]').focus();
        return false;
    }
    var x = $('input[id*=txtVehicleNumber]').val();
    if (x.lenght < 4) {
        alert("Please enter Valid Vehicle Registration Number ");
        $('input[id*=txtVehicleNumber]').focus();
        return false;
    }
    
    $('#loadingmessage').modal('show');
    return true;

}

function fnClearPermitNew()
{
    $('select[id*=VOName]').val('');
    $('input[id*=VOCareOf]').val('');
    $('textarea[id*=VOPresentAddress]').val('');
    $('textarea[id*=VOPermanentAddress]').val('');
    $('input[id*=VORegNumber]').val('');
    $('input[id*=txtPermitNo]').val('');
    $('input[id*=txtvalidityfrom]').val('');
    $('input[id*=txtvalidityto]').val('');
    $('input[id*=VMakerDesc]').val('');
    $('input[id*=VMakerModel]').val('');
    $('input[id*=VType]').val('');
    $('input[id*=VChassisNumber]').val('');
    $('input[id*=VEngineNumber]').val('');
    $('input[id*=VSeatCapacity]').val('');
    $('input[id*=txtULW]').val('');
   
    $('input[id*=txtRLW]').val('');
    $('input[id*=VManufacturingON]').val('');
    $('input[id*=txtNatureOfGoods]').val('');
    $('input[id*=txtMobileNo]').val('');
    $('input[id*=txtEmailId]').val('');
    $('input[id*=txtICValidUpto]').val('');
    $('input[id*=txtFCValidUpto]').val('');
    $('input[id*=txtPCValidUpto]').val('');
    $('input[id*=txtTPValidUpto]').val('');
    $('input[id*=txttaxpayAmount]').val('');
    $('input[id*=txtTAXpaidfrom]').val('');
    $('input[id*=txtTAXpaidupto]').val('');


    $('input[id*=txtCounterSignatureNumber]').val('');
    $('input[id*=txtRecommendationNo]').val('');
    $('input[id*=txtRecommendationNoDated]').val('');
    $('input[id*=txttaxRefNo]').val('');
    $('input[id*=txttaxRefDated]').val('');

    
    
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

    if ($('input[id*=txtCounterSignatureNumber]').val() == "" || $('input[id*=txtCounterSignatureNumber]').val() == null)
    {
        alert("Please enter Counter Signature Number");
        $('input[id*=txtCounterSignatureNumber]').css('border-color', 'red').focus();
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

    var VMakerDesc = $('input[id*=VMakerDesc]').val().trim();
    if (VMakerDesc == "" || VMakerDesc == null) {
        alert("Please enter Maker Description");
        $('input[id*=VMakerDesc]').css('border-color', 'red').focus();
        return false;
    }


    var VMakerModel =  $('input[id*=VMakerModel]').val().trim();
    if (VMakerModel == "" || VMakerModel == null) {
        alert("Please enter Vehicle Model");
        $('input[id*=VMakerModel]').css('border-color', 'red').focus();
        return false;
    }

       var VType = $('input[id*=VType]').val().trim();
    if (VType == "" || VType == null) {
        alert("Please enter Vehicle class description");
        $('input[id*=VType]').css('border-color', 'red').focus();
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
        alert("Please enter U.L.W as per RC value");
        $('input[id*=txtULW]').css('border-color', 'red').focus();
        return false;
    }
    if (ulw <= parseInt(0)) {
        alert("Please enter valid data  in the U.L.W as per RC textbox");
        $('input[id*=txtULW]').css('border-color', 'red').focus();

        return false;
    }
    //if (VGVW.length < 2) {
    //    alert("Enter atleast 2 chars in the G.V.W.  textbox");
    //    $('input[id*=VGVW]').css('border-color', 'red').focus();
    //    return false;
    //}
    var rlw = $('input[id*=txtRLW]').val().trim();
    if (rlw == "" || rlw == null) {
        alert("Please enter R.L.W as per RC value");
        $('input[id*=txtRLW]').css('border-color', 'red').focus();
        return false;
    }
    if (rlw <= parseInt(0)) {
        alert("Please enter valid data  in the R.L.W as per RC textbox");
        $('input[id*=txtRLW]').css('border-color', 'red').focus();

        return false;
    }
    //if (PayLoad.length < 2) {
    //    alert("Enter atleast 2 chars in the Pay Load textbox");
    //    $('input[id*=txtPayLoad]').css('border-color', 'red').focus();
    //    return false;
    //}
    var VManufacturingON = $('input[id*=VManufacturingON]').val().trim();
    if (VManufacturingON == "" || VManufacturingON == null) {
        alert("Please enter Manufacturing date");
        $('input[id*=VManufacturingON]').css('border-color', 'red').focus();
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
    if ($('input[id*=txtTPValidUpto]').val() == null || $('input[id*=txtTPValidUpto]').val() == "") {
        alert("Please select Tax Paid Up to Date");
        $('input[id*=txtTPValidUpto]').css('border-color', 'red').focus();
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
    if (EmailID != "" ) {

        if (!IsEmail(EmailID)) {
            alert("Invalid Email ID");
            $('input[id*=txtEmailId]').css('border-color', 'red').focus();
            return false;
        }
    }
    var txttaxpayAmount = $('input[id*=txttaxpayAmount]').val().trim();
    if (txttaxpayAmount == "" || txttaxpayAmount == null) {
        alert("Please enter Tax Pay Amount");
        $('input[id*=txttaxpayAmount]').css('border-color', 'red').focus();
        return false;
    }
    var txtTAXpaidfrom = $('input[id*=txtTAXpaidfrom]').val().trim();
    if (txtTAXpaidfrom == "" || txtTAXpaidfrom == null) {
        alert("Please select Fee Tax Paid from");
        $('input[id*=txtTAXpaidfrom]').css('border-color', 'red').focus();
        return false;
    }
    var txtTAXpaidupto = $('input[id*=txtTAXpaidupto]').val().trim();
    if (txtTAXpaidupto == "" || txtTAXpaidupto == null) {
        alert("Please select Fee Tax Paid Upto");
        $('input[id*=txtTAXpaidupto]').css('border-color', 'red').focus();
        return false;
    }

    var taxRefNo = $('input[id*=txttaxRefNo]').val();
    if (taxRefNo == "" || taxRefNo == null)
    {
        alert("Please enter Tax Ref. No");
        $('input[id*=txttaxRefNo]').css('border-color', 'red').focus();
        return false;
    }

    var taxRefNoDated = $('input[id*=txttaxRefDated]').val();
    if (taxRefNoDated == "" || taxRefNoDated == null) {
        alert("Please enter Tax Ref. Paid on Date");
        $('input[id*=txttaxRefDated]').css('border-color', 'red').focus();
        return false;
    }

   
    return true;
}



