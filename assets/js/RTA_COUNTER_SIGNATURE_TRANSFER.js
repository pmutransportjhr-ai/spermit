function fnValidate() {
    if ($('input[id*=txtExistRegNo]').val().trim() == null || $('input[id*=txtExistRegNo]').val().trim() == "") {
        alert("Please enter Registration Number");
        return false;
    }
    if ($('input[id*=txtExistPermitNo]').val().trim() == null || $('input[id*=txtExistPermitNo]').val().trim() == "") {
        alert("Please enter Permit Number");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;

}
function fnMobileValidate() {
    if ($('input[id*=txtMobileNumber]').val().trim() == null || $('input[id*=txtMobileNumber]').val().trim() == "") {
        alert("Please enter mobile number");
        return false;
    }
    if (!validateMob($('input[id*=txtMobileNumber]').val().trim())) {
        alert("Invalid Mobile Number");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;

}
function fnOTPValidate() {
    if ($('input[id*=txtOTP]').val().trim() == null || $('input[id*=txtOTP]').val().trim() == "") {
        alert("Please enter OTP");
        return false;
    }

    $('#loadingmessage').modal('show');
    return true;

}


function fnApplicationValidate() {


    var RTOIssuedBy = $('select[id*=ddlCSRTO]').val();
    if (RTOIssuedBy == "0" || RTOIssuedBy == "" || RTOIssuedBy == null) {
        alert("Please select RTO");
        return false;
    }
    if ($('span[id*=lblReplacevehicleFees]').text() == null || $('span[id*=lblReplacevehicleFees]').text() == "") {
        alert("Transfer of Counter Signature Fees is not defined. So you can not apply for Transfer of Counter Signature right now");
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
    return true;
}
