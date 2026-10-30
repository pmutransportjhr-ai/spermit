jQuery(document).ready(function () {
    if ($('input[id*=MFSERVER]').val() != 'YES') {
        fnManufactDate();
    }

})
function fnManufactDate() {

    var getdate = new Date();
    var yearstart = parseInt(getdate.getFullYear()) - parseInt(12);
    var monthsStart = parseInt(getdate.getMonth()) + parseInt(1);
    var startdate = monthsStart + "/" + yearstart;
    var endyear = getdate.getFullYear();
    var monthEnd = parseInt(getdate.getMonth());
    var endDate = monthEnd + "/" + endyear;
    $('input[id*=VManufacturingON]').Monthpicker({ minYear: yearstart, maxYear: endyear, minValue: startdate, maxValue: endDate })
  //  Monthpicker.Init();


}

function fnSTAReplacementValidate() {
    if ($('input[id*=txtExistRegNo]').val() == null || $('input[id*=txtExistRegNo]').val() == "") {
        alert("Please enter Registration Number");
        $('input[id*=txtExistRegNo]').focus();
        return false;
    }
    if ($('input[id*=txtExistPermitNo]').val() == null || $('input[id*=txtExistPermitNo]').val() == "") {
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
function fnSearchValidate() {
    if ($('input[id*=txtVehicleNumber]').val() == null || $('input[id*=txtVehicleNumber]').val() == "") {
        alert("Please enter Replacement Vehicle Registration Number");
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
function fnClearPermitNew()
{
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
    $('input[id*=VSeatCapacity]').val('');
    $('input[id*=VManufacturingON]').val('');
    $('input[id*=txtMobileNo]').val('');
    $('input[id*=txtEmailId]').val('');
    $('input[id*=txtICValidUpto]').val('');
    $('input[id*=txtFCValidUpto]').val('');
    $('input[id*=txtPCValidUpto]').val('');
    $('input[id*=txtIdentityNo]').val('');
    $('input:radio[name*="gender"]').prop('checked', false);
    $("input[id*=txtMale]").parent('span').removeClass('checked');
    $("input[id*=txtFemale]").parent('span').removeClass('checked');

   
}
function fnRVApplicationValidate() {
    $('.form-control').css('border-color', '#c2cad8');
    var oldOwnername = $('span[id*=lblExistName]').text();
    var newOwnername = $('span[id*=VOName]').text();
   
    //if (oldOwnername != newOwnername) {
    //    alert("Owner name is diffrent. You can not replace the vehicle");
    //    return false;
    //}
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
    if (VULW.length < 2) {
        alert("Enter atleast 2 chars in the U. L. W. as per RC textbox");
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
    if (VRLW.length < 2) {
        alert("Enter atleast 2 chars in the R. L. W. as per RC textbox");
        $('input[id*=VRLW]').css('border-color', 'red').focus();
        return false;
    }
    var VSeatCapacity = $('input[id*=VSeatCapacity]').val().trim();
    if (VSeatCapacity == "" || VSeatCapacity == null) {
        alert("Please enter seat capacity");
        $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
        return false;
    }
    var VManufacturingON = $('input[id*=VManufacturingON]').val().trim();
    if (VManufacturingON == "" || VManufacturingON == null) {
        alert("Please enter Manufacturing date");
        $('input[id*=VManufacturingON]').css('border-color', 'red').focus();
        return false;
    }
    if ($('span[id*=lblReplacevehicleFees]').text() == null || $('span[id*=lblReplacevehicleFees]').text() == "") {
        alert("Replacement of Vehicle Fees is not defined. So you can not apply for Replacement of Vehicle right now");
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
    if ($("input[name*='gender']:checked").length <= 0) {
        alert("Please select gender option");
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

   
    var Permitcopy = $('input[id*=uploadPermit]').val();
    if (Permitcopy == "") {
        alert("Please upload  Permit Certificate File");
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
    
    if ($('span[id*=lblReplacevehicleFees]').text() == null || $('span[id*=lblReplacevehicleFees]').text() == "") {
        alert("Replacement of Vehicle Fees is not defined. So you can not apply for Replacement of Vehicle right now");
        return false;
    }

    
    return true;
}
