jQuery(document).ready(function () {

    fnExArmy();
    fnIdentityProof();
    fnHandicapped();
    fnHML();
    fnQualification();
    fnCategory();
    if ($('input[id*=MFSERVER]').val() != 'YES') {
        fnManufactDate();
    }
   


});
function fnQualification() {
    var Value = $('select[id*=ddlQualification]').val();
    if (Value == "0" || Value == "Illiterate") {
        $('div[id*=divQualification]').css('display', 'none');
    }

    else {
        $('div[id*=divQualification]').css('display', 'block');
    }
}
function fnCategory() {
    var Value = $('select[id*=ddlCategory]').val();
    if (Value == "0" || Value == "Gen") {
        $('div[id*=divSCST]').css('display', 'none');

    }

    else {
        $('div[id*=divSCST]').css('display', 'block');

    }
}
function fnExArmy() {
    var ExArmy = $("input[name*='ExArmy']:checked").val();
    if (ExArmy == 'Yes') {
        $('div[id*=divEXArmy]').css('display', 'block');

    }

    else {
        $('div[id*=divEXArmy]').css('display', 'none');

    }
}
function fnIdentityProof() {
    var IdentityProof = $("input[name*='IdentityProof']:checked").val();
    if ($("input[name*='IdentityProof']:checked").length > 0) {
        $('div[id*=divIdentityProof]').css('display', 'block');

    }

    else {
        $('div[id*=divIdentityProof]').css('display', 'none');

    }
}
function fnHandicapped() {
    var Handicapped = $("input[name*='Handicapped']:checked").val();
    if (Handicapped == 'Yes') {
        $('div[id*=divHandicaped]').css('display', 'block');

    }

    else {
        $('div[id*=divHandicaped]').css('display', 'none');

    }
}
function fnHML() {
    var HML = $("input[name*='HML']:checked").val();
    if (HML == 'Yes') {
        $('div[id*=divHML]').css('display', 'block');

    }

    else {
        $('div[id*=divHML]').css('display', 'none');

    }
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
        alert("Please enter Replacement Registration Number");
        $('input[id*=txtVehicleNumber]').focus();
        return false;
    }
    else
    {
        var registrationNumber = $('input[id*="txtVehicleNumber"]').val();
        var registrationNumberExp = '^[a-zA-Z]{2}[0-9]{2}[a-zA-Z]{1,2}[0-9]{1,}$';
        var regtxt = new RegExp(registrationNumberExp);
        if (regtxt.test(registrationNumber) == false) {
            alert('Please enter valid registration number.');
            $('input[id*="txtVehicleNumber"]').val('');
            return false;
        }
      
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

    $('input:radio[name*="SLEEPER"]').prop('checked', false);
    $("input[id*=txtSleeperY]").parent('span').removeClass('checked');
    $("input[id*=txtSleeperN]").parent('span').removeClass('checked');

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
        $('input[id*=VOCareOf]').css('border-color', 'red').focus();
        alert("Please enter C/O Name");
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
    if (VType.toUpperCase().indexOf("BUS") === -1) {
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


        if ($('input[id*=VColor]').val().trim().length < 0) {
            alert("Enter atleast 2 chars in the color textbox");
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
    if (VSeatCapacity < parseInt(32)) {
        alert("Minimum 32 Seat capacity required");
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
    } if ($("input[name*='Deluxe']:checked").length <= 0) {
        alert("Please select bus type (Deluxe) option");
        return false;
    }
    if ($("input[name*='SLEEPER']:checked").length <= 0) {
        alert("Please select bus type (Sleeper) option");
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

    if ($("select[name*='ddlQualification']").val() == 'Master' || $("select[name*='ddlQualification']").val() == 'Bachelor'
        || $("select[name*='ddlQualification']").val() == 'Intermediate' || $("select[name*='ddlQualification']").val() == 'Matriculation'
        ) {
        var Qualification = $('input[id*=uploadQualification]').val();
        if (Qualification == "") {
            alert("Please upload  Qualification Certificate");
            return false;
        }
        if (Qualification != "") {

            var fileUploadName = $('input[id*=uploadQualification]')[0].files[0].name;
            var filesize = parseInt($('input[id*=uploadQualification]')[0].files[0].size / 1024);
            var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
            if (fileextension.toString().toUpperCase() != ".PDF") {
                $('input[id*=uploadQualification]').val('');

                alert("Plesae upload only PDF file for Qualification Certificate");

                return false;

            }

            if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
                $('input[id*=uploadQualification]').val('');
                alert("Qualification Certificate file must be greater than 0 MB and less than 2 MB");

                return false;

            }


        }

    }
    if ($("select[name*='ddlCategory']").val() == 'SC' || $("select[name*='ddlCategory']").val() == 'ST') {
        var SCST = $('input[id*=uploadSCST]').val();
        if (SCST == "") {
            alert("Please upload  Category Certificate");
            return false;
        }
        if (SCST != "") {

            var fileUploadName = $('input[id*=uploadSCST]')[0].files[0].name;
            var filesize = parseInt($('input[id*=uploadSCST]')[0].files[0].size / 1024);
            var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
            if (fileextension.toString().toUpperCase() != ".PDF") {
                $('input[id*=uploadSCST]').val('');

                alert("Plesae upload only PDF file for Category Certificate");

                return false;

            }

            if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
                $('input[id*=uploadSCST]').val('');
                alert("Category Certificate file must be greater than 0 MB and less than 2 MB");

                return false;

            }


        }

    }
    if ($("input[name*='ExArmy']:checked").val() == 'Yes') {
        var EXArmy = $('input[id*=uploadEXArmy]').val();
        if (EXArmy == "") {
            alert("Please upload  Ex-Army Certificate");
            return false;
        }
        if (EXArmy != "") {

            var fileUploadName = $('input[id*=uploadEXArmy]')[0].files[0].name;
            var filesize = parseInt($('input[id*=uploadEXArmy]')[0].files[0].size / 1024);
            var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
            if (fileextension.toString().toUpperCase() != ".PDF") {
                $('input[id*=uploadEXArmy]').val('');

                alert("Plesae upload only PDF file for Ex-Army  Certificate");

                return false;

            }

            if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
                $('input[id*=uploadEXArmy]').val('');
                alert("Ex-Army Certificate file must be greater than 0 MB and less than 2 MB");

                return false;

            }


        }
    }



    if ($("input[name*='Handicapped']:checked").val() == 'Yes') {

        var Handicaped = $('input[id*=uploadHandicaped]').val();
        if (Handicaped == "") {
            alert("Please upload  Handicapped Certificate");
            return false;
        }
        if (Handicaped != "") {

            var fileUploadName = $('input[id*=uploadHandicaped]')[0].files[0].name;
            var filesize = parseInt($('input[id*=uploadHandicaped]')[0].files[0].size / 1024);
            var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
            if (fileextension.toString().toUpperCase() != ".PDF") {
                $('input[id*=uploadHandicaped]').val('');

                alert("Plesae upload only PDF file for Handicapped  Certificate");

                return false;

            }

            if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
                $('input[id*=uploadHandicaped]').val('');
                alert("Handicapped Certificate file must be greater than 0 MB and less than 2 MB");

                return false;

            }


        }
    }
    if ($("input[name*='HML']:checked").val() == 'Yes') {

        var HML = $('input[id*=uploadHML]').val();
        if (HML == "") {
            alert("Please upload  Heavy Motor Licence");
            return false;
        }
        if (HML != "") {

            var fileUploadName = $('input[id*=uploadHML]')[0].files[0].name;
            var filesize = parseInt($('input[id*=uploadHML]')[0].files[0].size / 1024);
            var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
            if (fileextension.toString().toUpperCase() != ".PDF") {
                $('input[id*=uploadHML]').val('');

                alert("Plesae upload only PDF file for Heavy Motor Licence");

                return false;

            }

            if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
                $('input[id*=uploadHML]').val('');
                alert("Heavy Motor Licence file must be greater than 0 MB and less than 2 MB");

                return false;

            }


        }
    }
    var IdentityNo = $('input[id*=txtIdentityNo]').val().trim();
    if (IdentityNo == "" || IdentityNo == null) {
        alert("Please enter Identity Proof Number");
        $('input[id*=txtIdentityNo]').css('border-color', 'red').focus();
        return false;
    }
    var IdentityProof = $('input[id*=uploadIdentityProof]').val();
    if (IdentityProof == "") {
        alert("Please upload  Identity Proof");
        return false;
    }
    if (IdentityProof != "") {

        var fileUploadName = $('input[id*=uploadIdentityProof]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadIdentityProof]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadIdentityProof]').val('');

            alert("Plesae upload only PDF file for Identity Proof");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadIdentityProof]').val('');
            alert("Identity Proof file must be greater than 0 MB and less than 2 MB");

            return false;

        }


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


    /////////////////////////////////////// Old Vehicle  Certificate Validation ////////////////////////////////////////////////////

    /// Registration Certificate 
    var registrationNumber_old = $('input[id*=RegistrationCertificate_old]').val();
    if (registrationNumber_old == "") {
        alert("Please upload  Registration Certificate File of Old Vehicle");
        return false;
    }
    if (registrationNumber_old != "") {
        var fileUploadName = $('input[id*=RegistrationCertificate_old]')[0].files[0].name;
        var filesize = parseInt($('input[id*=RegistrationCertificate_old]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=RegistrationCertificate_old]').val('');
            alert("Plesae upload only PDF file of Old Vehicle.");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=RegistrationCertificate_old]').val('');
            alert("Registration Certificate file must be greater than 0 MB and less than 2 MB of Old Vehicle");
            return false;
        }
    }

    /// SpeedGovernorCertificate_old Certificate 
    var SpeedGovernorCertificate_old = $('input[id*=SpeedGovernorCertificate_old]').val();
    if (SpeedGovernorCertificate_old == "") {
        alert("Please upload  Speed Governer Certificate File");
        return false;
    }
    if (SpeedGovernorCertificate_old != "") {
        var fileUploadName = $('input[id*=SpeedGovernorCertificate_old]')[0].files[0].name;
        var filesize = parseInt($('input[id*=SpeedGovernorCertificate_old]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=SpeedGovernorCertificate_old]').val('');
            alert("Plesae upload Speed Governer Certificate only in PDF file of Old Vehicle.");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=SpeedGovernorCertificate_old]').val('');
            alert("Speed Governer file must be greater than 0 MB and less than 2 MB of Old Vehicle");
            return false;
        }
    }
    /// InsuranceCertificate_old Certificate 
    var InsuranceCertificate_old = $('input[id*=InsuranceCertificate_old]').val();
    if (InsuranceCertificate_old == "") {
        alert("Please upload  Insurance Certificate File of Old Vehicle");
        return false;
    }
    if (InsuranceCertificate_old != "") {
        var fileUploadName = $('input[id*=InsuranceCertificate_old]')[0].files[0].name;
        var filesize = parseInt($('input[id*=InsuranceCertificate_old]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=InsuranceCertificate_old]').val('');
            alert("Plesae upload Speed Governer Certificate only in PDF file of Old Vehicle.");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=InsuranceCertificate_old]').val('');
            alert("Speed Governer file must be greater than 0 MB and less than 2 MB of Old Vehicle");
            return false;
        }
    }
    /// PollutionCertificate_old Certificate 
    var PollutionCertificate_old = $('input[id*=PollutionCertificate_old]').val();
    if (PollutionCertificate_old == "") {
        alert("Please upload  Pollution Certificate File of Old Vehicle");
        return false;
    }
    if (PollutionCertificate_old != "") {
        var fileUploadName = $('input[id*=PollutionCertificate_old]')[0].files[0].name;
        var filesize = parseInt($('input[id*=PollutionCertificate_old]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=PollutionCertificate_old]').val('');
            alert("Plesae upload Pollution Certificate only in PDF file of Old Vehicle.");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=PollutionCertificate_old]').val('');
            alert("Pollution file must be greater than 0 MB and less than 2 MB of Old Vehicle");
            return false;
        }
    }
    /// fitnessCertificate_old Certificate 
    var fitnessCertificate_old = $('input[id*=fitnessCertificate_old]').val();
    if (fitnessCertificate_old == "") {
        alert("Please upload  Fitness Certificate File of Old Vehicle");
        return false;
    }
    if (fitnessCertificate_old != "") {
        var fileUploadName = $('input[id*=fitnessCertificate_old]')[0].files[0].name;
        var filesize = parseInt($('input[id*=fitnessCertificate_old]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=fitnessCertificate_old]').val('');
            alert("Plesae upload Fitness Certificate only in PDF file of Old Vehicle.");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=fitnessCertificate_old]').val('');
            alert("Fitness file must be greater than 0 MB and less than 2 MB of Old Vehicle");
            return false;
        }
    }
    /// permitCertificate_old Certificate 
    var permitCertificate_old = $('input[id*=permitCertificate_old]').val();
    if (permitCertificate_old == "") {
        alert("Please upload  Permit Certificate File of Old Vehicle");
        return false;
    }
    if (permitCertificate_old != "") {
        var fileUploadName = $('input[id*=permitCertificate_old]')[0].files[0].name;
        var filesize = parseInt($('input[id*=permitCertificate_old]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=permitCertificate_old]').val('');
            alert("Plesae upload Permit Certificate only in PDF file of Old Vehicle.");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=permitCertificate_old]').val('');
            alert("Permit file must be greater than 0 MB and less than 2 MB of Old Vehicle");
            return false;
        }
    }
    /// permitCertificate_old Certificate 
    var jhTaxToken_certificate_old = $('input[id*=jhTaxToken_certificate_old]').val();
    if (jhTaxToken_certificate_old == "") {
        alert("Please upload Jharkhand Tax Token Certificate File of Old Vehicle");
        return false;
    }
    if (jhTaxToken_certificate_old != "") {
        var fileUploadName = $('input[id*=jhTaxToken_certificate_old]')[0].files[0].name;
        var filesize = parseInt($('input[id*=jhTaxToken_certificate_old]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=jhTaxToken_certificate_old]').val('');
            alert("Plesae upload Jharkhand Tax Token Certificate only in PDF file of Old Vehicle.");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=jhTaxToken_certificate_old]').val('');
            alert("Jharkhand Tax Token file must be greater than 0 MB and less than 2 MB of Old Vehicle");
            return false;
        }
    }

    /// sldCertificate_old Certificate 
    var sldCertificate_old = $('input[id*=sldCertificate_old]').val();
    if (sldCertificate_old == "") {
        alert("Please upload SLD Certificate File of Old Vehicle");
        return false;
    }
    if (sldCertificate_old != "") {
        var fileUploadName = $('input[id*=sldCertificate_old]')[0].files[0].name;
        var filesize = parseInt($('input[id*=sldCertificate_old]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=sldCertificate_old]').val('');
            alert("Plesae upload SLD Certificate only in PDF file of Old Vehicle.");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=sldCertificate_old]').val('');
            alert("SLD file must be greater than 0 MB and less than 2 MB of Old Vehicle");
            return false;
        }
    }

    var UploadNeibhouring = $('input[id*=UploadNeibhouring]').val();
    if (UploadNeibhouring == "") {
        alert("Please upload Neibhouring State Tax.");
        return false;
    }
    if (UploadNeibhouring != "") {
        var fileUploadName = $('input[id*=UploadNeibhouring]')[0].files[0].name;
        var filesize = parseInt($('input[id*=UploadNeibhouring]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=UploadNeibhouring]').val('');
            alert("Plesae upload Neibhouring State Tax only in PDF file of Old Vehicle.");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=UploadNeibhouring]').val('');
            alert("Neibhouring State Tax file must be greater than 0 MB and less than 2 MB of Old Vehicle");
            return false;
        }
    }




    var txtInsuranceCertificate_validUpto = $('input[id*=txtInsuranceCertificate_validUpto]').val().trim();
    if (txtInsuranceCertificate_validUpto == "" || txtInsuranceCertificate_validUpto == null) {
        alert("Please enter Insurance Certificate valid upto date for Old Vehicle");
        $('input[id*=txtInsuranceCertificate_validUpto]').css('border-color', 'red').focus();
        return false;
    }

    var txtPollutionCertificate_validupto = $('input[id*=txtPollutionCertificate_validupto]').val().trim();
    if (txtPollutionCertificate_validupto == "" || txtPollutionCertificate_validupto == null) {
        alert("Please enter Pollution Certificate valid upto date for Old Vehicle");
        $('input[id*=txtPollutionCertificate_validupto]').css('border-color', 'red').focus();
        return false;
    }
    var txtFitnessCertificateValidUpto_old = $('input[id*=txtFitnessCertificateValidUpto_old]').val().trim();
    if (txtFitnessCertificateValidUpto_old == "" || txtFitnessCertificateValidUpto_old == null) {
        alert("Please enter Fitness Certificate valid upto date for Old Vehicle");
        $('input[id*=txtFitnessCertificateValidUpto_old]').css('border-color', 'red').focus();
        return false;
    }
    var txtPaidUpto_Old = $('input[id*=txtPaidUpto_Old]').val().trim();
    if (txtPaidUpto_Old == "" || txtPaidUpto_Old == null) {
        alert("Please enter Jharkhand Tax Paid up to Date for Old Vehicle");
        $('input[id*=txtPaidUpto_Old]').css('border-color', 'red').focus();
        return false;
    }
    return true;
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
    Monthpicker.Init();


}