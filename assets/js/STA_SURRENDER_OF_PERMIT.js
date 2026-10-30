/// <reference path="RTA-PERMIT-APPLICATION-VERIFY.js" />
function fnSTARenewalValidate() {
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
function fnRPApplicationValidate() {

    $('.form-control').css('border-color', '#c2cad8');
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
    var Othercopy = $('input[id*=uploadOther]').val();
    if (Othercopy == "") {
        alert("Please upload  other State Tax Token");
        return false;
    }
    if (Othercopy != "") {

        var fileUploadName = $('input[id*=uploadOther]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadOther]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadOther]').val('');

            alert("Plesae upload only PDF file for Other");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadOther]').val('');
            alert("Other file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    
    var jhttaxtoken = $('input[id*=uploadeJhtTaxToken]').val();
    if (jhttaxtoken == "") {
        alert("Please upload  Jharkhand State Tax Token");
        return false;
    }
    if (jhttaxtoken != "") {

        var fileUploadName = $('input[id*=uploadeJhtTaxToken]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadeJhtTaxToken]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadeJhtTaxToken]').val('');

            alert("Plesae upload only PDF file for Jharkhand State Tax Token");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadeJhtTaxToken]').val('');
            alert("Jharkhand State Tax Token file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    var vehicleFitness = $('input[id*=vehicleFitness]').val();
    if (vehicleFitness == "") {
        alert("Please upload  vehicle Fitness certificate");
        return false;
    }
    if (vehicleFitness != "") {

        var fileUploadName = $('input[id*=vehicleFitness]')[0].files[0].name;
        var filesize = parseInt($('input[id*=vehicleFitness]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=vehicleFitness]').val('');

            alert("Plesae upload only PDF file for vehicle Fitness certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=vehicleFitness]').val('');
            alert("vehicle Fitness certificate must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }

    $('#loadingmessage').modal('show');
    return true;
}
