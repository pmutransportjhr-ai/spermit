function fnSaveUser() {
    if (fnValidate()) {
        return true;
    }
   return false;
}
function fnValidate() {
    var encryptedpassword1 = $('#txtoldPassword').val();
    

    if ($('#txtoldPassword').val() == "" || $('#txtoldPassword').val() == null) {
        alert("Please Enter Old Password");
        return false;
    }
    
    //var oldpsdEncrypted = returnHashCode();
   
    //if (encryptedpassword1 != oldpsdEncrypted) {
    //    alert("Password does not matched with Old password");
    //    return false;
    //    $('#txtoldPassword').focus();
    //}
    if ($('#txtPassword').val().trim() == "" || $('#txtPassword').val().trim() == null) {
        alert("Please enter New password");
        return false;
    }
    if (!Ispassword($('#txtPassword').val().trim())) {
        alert("Minimum 8 and Maximum 20 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character i.e. @$!%*#?");
        return false;
    }
    if ($('#txtCPassword').val() == "" || $('#txtCPassword').val() == null) {
        alert("Please enter confirm password");
        return false;
    }
    if ($('#txtPassword').val() != $('#txtCPassword').val()) {
        alert("Password does not matched with confirm password");
        return false;
    }
    returnHashCode();
    GenerateNewPasswordHashcode();
    return true;
}
function fnCloseModal1() {
    window.location.href = ("STA-DASHBOARD.aspx");
}


function Ispassword(password) {
    var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,20}$/;
    if (!regex.test(password)) {
        return false;
    } else {
        return true;
    }
}
