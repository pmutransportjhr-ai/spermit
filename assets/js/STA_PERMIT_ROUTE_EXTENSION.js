function fnSTAValidate() {
    if ($('input[id*=txtExistRegNo]').val().trim() == null || $('input[id*=txtExistRegNo]').val().trim() == "") {
        alert("Please enter Registration Number");
        $('input[id*=txtExistRegNo]').focus();
        return false;
    }
    if ($('input[id*=txtExistPermitNo]').val().trim() == null || $('input[id*=txtExistPermitNo]').val().trim() == "") {
        alert("Please enter Permit Number");
        $('input[id*=txtExistPermitNo]').focus();
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;

}
function fnMobileValidate() {
    if ($('input[id*=txtMobileNumber]').val().trim() == null || $('input[id*=txtMobileNumber]').val().trim() == "") {
        alert("Please enter mobile number");
        $('input[id*=txtMobileNumber]').focus();
        return false;
    }
    if (!validateMob($('input[id*=txtMobileNumber]').val().trim())) {
        alert("Invalid Mobile Number");
        $('input[id*=txtMobileNumber]').focus();
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;

}
function fnOTPValidate() {
    if ($('input[id*=txtOTP]').val().trim() == null || $('input[id*=txtOTP]').val().trim() == "") {
        alert("Please enter OTP");
        $('input[id*=txtOTP]').focus();
        return false;
    }

    $('#loadingmessage').modal('show');
    return true;

}
function fileuploadValidate() {
   
   





    
}
function fnApplicationValidate()
{
    $('.form-control').css('border-color', '#c2cad8');
    if ($('span[id*=lblRenewalPermitFees]').text() == null || $('span[id*=lblRenewalPermitFees]').text() == "") {
        alert("Permit Route Extension Fees is not defined. So you can not apply for Permit Route Extension right now");
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
    if (EmailID != "") {

        if (!IsEmail(EmailID)) {
            alert("Invalid Email ID");
            $('input[id*=txtEmailId]').css('border-color', 'red').focus();
            return false;
        }
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
        alert("Please upload  Speed Government Certificate");
        return false;
    }
    if (Speed != "") {
        var fileUploadName = $('input[id*=uploadSpeed]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadSpeed]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadSpeed]').val('');

            alert("Plesae upload only PDF file for Speed Government Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadSpeed]').val('');
            alert("Speed Government Certificate file must be greater than 0 MB and less than 2 MB");

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
    var Permitcopy = $('input[id*=uploadPermit]').val();
    if (Permitcopy == "") {
        alert("Please upload  Permit Certificate");
        return false;
    }
    if (Permitcopy != "") {

        var fileUploadName = $('input[id*=uploadPermit]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadPermit]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadPermit]').val('');

            alert("Plesae upload only PDF file for Permit Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadPermit]').val('');
            alert("Permit Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    var CS = $('input[id*=uploadCS]').val();
    if (CS == "") {
        alert("Please upload  Counter Signature Certificate");
        return false;
    }
    if (CS != "") {

        var fileUploadName = $('input[id*=uploadCS]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadCS]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadCS]').val('');

            alert("Plesae upload only PDF file for Counter Signature Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadCS]').val('');
            alert("Counter Signature Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
        var PERMIT_APPLICATION_LDRs = new Array();
        var condition = true;
        var NoRoundTrip = $('input[id*=HFNoOftrips]').val();
        for (var j = 1; j <= NoRoundTrip; j++)
        {
            var length = $('#tbdRouteInfo_' + j + ' tr:last').attr('id').split("_");
            var m = length[1] - 1;
            if ($('#lblUPRoute1' + '_1').val().trim() != "" || $('#lblDownRoute1_' + length[1]).val().trim() != "") {
                if ($('#lblUPRoute1_1').val().trim() == "") {
                    condition = false;
                    alert("Please enter first station Name of UP");
                    return false;

                }
                if ($('#txtUPDepRoute1_1').val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Departure Time of UP for " + $('#lblUPRoute1_1').val());
                    return false;

                }
                if ($('#txtUPArrRoute1_2').val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Arrival Time  of UP for " + $('#lblUPRoute1_2').text());
                    return false;

                }
                if ($('#txtUPDepRoute1_2').val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Departure Time  of UP for " + $('#lblUPRoute1_2').text());
                    return false;

                }
                if ($('#txtDownArrRoute1_' + m).val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Arrival Time  of Down for " + $('#lblDownRoute1_' + m).text());
                    return false;

                }
                if ($('#txtDownDepRoute1_' + m).val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Departure Time of Down for " + $('#lblDownRoute1_' + m).text());
                    return false;

                }
                
                if ($('#lblDownRoute1_' + length[1]).val().trim() == "") {
                    condition = false; 
                    alert("Please enter last station Name of Down");
                    return false;

                }
                if ($('#lblUPRoute1' + '_1').val().trim() != $('#lblDownRoute1_' + length[1]).val().trim()) {
                    condition = false;
                    alert("First station name of UP is not same as last station name of Down");
                    return false;
                }
                if ($('#txtDownArrRoute1_' + length[1]).val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Arrival Time of Down for " + $('#lblDownRoute1_' + length[1]).val());
                    return false;

                }

            }
            if ($('#lblUPRoute1_' + length[1]).val().trim() != "" || $('#lblDownRoute1_1').val().trim() != "")
            {
                if ($('#lblDownRoute1_1').val().trim() == "") {
                    condition = false;
                    alert("Please enter First station Name of Down");
                    return false;

                }
                if ($('#txtDownDepRoute1_1').val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Departure Time  of Down for " + $('#lblUPRoute1_1').val());
                    return false;

                }
                if ($('#txtUPArrRoute1_' + length[1]).val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Arrival Time of UP for " + $('#lblUPRoute1_' + length[1]).val());
                    return false;

                }
                if ($('#txtUPArrRoute1_' + m).val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Arrival Time of UP for " + $('#lblUPRoute1_' + m).text());
                    return false;

                }
                if ($('#txtUPDepRoute1_' + m).val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Departure Time  of UP for " + $('#lblUPRoute1_' + m).text());
                    return false;

                }
                if ($('#txtDownArrRoute1_2').val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Arrival Time  of Down for " + $('#lblDownRoute1_2').text());
                    return false;

                }
                if ($('#txtDownDepRoute1_2').val().trim() == "" ) {
                    condition = false;
                    alert("Please enter Departure Time  of Down for " + $('#lblDownRoute1_2').text());
                    return false;

                }
                if ($('#lblUPRoute1_' + length[1]).val().trim() == "") {
                    condition = false;
                    alert("Please enter last station Name of UP");
                    return false;

                }
               
                if ($('#lblDownRoute1_1').val().trim() != $('#lblUPRoute1_' + length[1]).val().trim()) {
                    condition = false;
                    alert("Last station name of UP is not same as first station name of Down");
                    return false;
                }
               

            }




            $('#tbdRouteInfo_' + j + '  tr').each(function ()
            {
                //debugger;
                var parentid = $(this).attr('id').split("_");
                
                if (parentid[1] == 1) 
                {
                    

                    var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).val();
                    var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).val();
                    var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();
                    var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();
                       
                    PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: UPDepTime.toString(), MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                    PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: DownDepTime.toString(), MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });

                       
                    
                }
               

               if (parentid[1] == 2)
                {
                    

                    if ($('#txtUPDepRoute1_2').val().trim() == "") {
                        condition = false;
                        alert("Please enter Departure Time of UP for " + $('#lblUPRoute1_' + parentid[1]).text());
                        return false;

                    }
                    else if ($('#txtDownDepRoute1_2').val().trim() == "") {
                        condition = false;
                        alert("Please enter Departure Time of Down for " + $('#lblDownRoute1_' + parentid[1]).text());
                        return false;

                    }
                    

                }
              if (parentid[1] == m) 
              {
                    if ($('#txtUPArrRoute1_' + m).val().trim() == "" ) {
                        condition = false;
                        alert("Please enter Arrival Time of UP for " + $('#lblUPRoute1_' + parentid[1]).text());
                        return false;

                    }
                    else if ($('#txtDownArrRoute1_' + m).val().trim() == "") {
                        condition = false;
                        alert("Please enter Arrival Time of Down for " + $('#lblDownRoute1_' + parentid[1]).text());
                        return false;

                    }
                

                 
               }
               if (parentid[1] == length[1]) {
                   

                   var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).val();
                   var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).val();
                   var UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();
                   var DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();

                   
                    

                   PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime.toString(), DEPARTURE_TIMING: "", MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                   PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime.toString(), DEPARTURE_TIMING: "", MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });


                    
                }
                else 
                {
                   if (parentid[1] != 1)
                   {
                        
                        
                       var UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();
                       var DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();
                       var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();
                       var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();

                           
                        var  UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                        var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                        
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
    
        if (condition == false) {
            return false;

        }
        else {
            //var RouteHdr = $("input[name='routeHdr']:checked").val();

            var xxx = JSON.stringify({PERMIT_APPLICATION_LDRs: PERMIT_APPLICATION_LDRs });
             $('input[id*=txtroutjson]').val(xxx);
             $('#loadingmessage').modal('show');
            return true;
        }


    
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