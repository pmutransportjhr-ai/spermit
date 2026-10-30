
function fnSearchValidate() {
    if ($('input[id*=txtPermitNumber]').val() == null || $('input[id*=txtPermitNumber]').val() == "") {
        alert("Please enter Permit Number");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;
    
}
function fnMobileValidate() {
    if ($('input[id*=txtMobileNumber]').val() == null || $('input[id*=txtMobileNumber]').val() == "") {
        alert("Please enter Mobile Number");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;

}
function fnOTPValidate() {
    if ($('input[id*=txtVeifyOTP]').val() == null || $('input[id*=txtVeifyOTP]').val() == "") {
        alert("Please enter OTP Number");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;

}


