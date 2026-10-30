



function fnAppSearchValidate() {
    //var type = $('select[id*=ddlApplicationFor]').val();
    //var option = $('select[id*=ddlOptions]').val();

    if ($('input[id*=txtApplicationNumber]').val() == null || $('input[id*=txtApplicationNumber]').val() == "") {
        alert("Please enter Application Number");
        return false;
    }
    //if (type == "0" || type == null || type == "") {
    //    alert("Please select type");
    //    return false;
    //}
    //if (option == "0" || option == null || option == "") {
    //    alert("Please select option");
    //    return false;
    //}
    $('#loadingmessage').modal('show');
    return true;
    
}
function fnMobileValidate() {
    if ($('input[id*=txtMobileNumber]').val().trim() == null || $('input[id*=txtMobileNumber]').val().trim() == "") {
        alert("Please enter Mobile Number");
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