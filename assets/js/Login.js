
$('#frmlogin').submit(function (event) {
    // prevent default browser behaviour
    event.preventDefault();
});
jQuery(document).ready(function () {
    var contrl = false;
    var shift = false;

     $('#txtLogin').val("");
     $('#txtPassword').val("");
     $('#ddlType').val('0');
     Captcha();
     $('#imgCaptcha').bind("keypress", function (e) { e.preventDefault(); }).bind("mousedown", function (e) { e.preventDefault(); });
     $('#txtCaptcha').bind("contextmenu", function (e) { e.preventDefault(); });
    
     $("#refCaptchta").click(function () {
         Captcha();
     });

});
function validateFP() {
    var LOGIN_ID = $('#txtLogin').val();
    var type = $('#ddlType').val();
    if (LOGIN_ID == "" || LOGIN_ID == null) {
        alert("Please Enter Email ID");
        Captcha();
        return false;
    }
    if (!IsEmail($("#txtLogin").val())) {
        alert("Invalid email address.");
        $('#txtLogin').css('border-color', 'red').focus();
        Captcha();
        return false;
    }
    if (type == "0" || type == null || type == "") {
        alert("Please select type");
        Captcha();
        return false;
    }

   var captcha = removeSpaces($('#txtCaptcha').val());

    if (captcha == "0" || captcha == null || captcha == "") {
        alert("Please enter captcha code");
        Captcha();
        return false;
    }
    else
    {
        if (captcha != removeSpaces($('#hdnCap').val())) {
            alert("Please enter valid captcha code");
            Captcha();
            return false;

        }

    }
  


    return true;
}
function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}
function fnRPValidate() {
    $('.form-control').css('border-color', '#c2cad8');

    if ($('#txtRNewPassword').val().trim() == null || $('#txtRNewPassword').val().trim() == "") {
        alert("Please Enter New Password");
        $('#txtRNewPassword').css('border-color', 'red').focus();
        return false;
    }
    if (!Ispassword($("#txtRNewPassword").val())) {
        alert("Minimum 8 and Maximum 20 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character i.e. @$!%*#?");
        $('#txtRNewPassword').css('border-color', 'red').focus();
        return false;
    }
    if ($('#txtRNewPassword').val().trim() != $('#txtRConfirmPassword').val().trim()) {
        alert("Password Not Match");
        $('#txtRConfirmPassword').css('border-color', 'red').focus();
        return false;
    }
    returnHashCode();

    return true;
}
//function validatelogin()
//{
//    var LOGIN_ID = $('#txtLogin').val();
//    var PASSWORD = $('#txtPassword').val();
//    var type = $('#ddlType').val();
//    if (LOGIN_ID == "" || LOGIN_ID == null) {
//        alert("Please Enter Login ID");
//        return false;
//    }
//    if (PASSWORD == "" || PASSWORD == null) {
//        alert("Please Enter Password");
//        return false;
//    }
//    if (type == "0" || type == null || type == "") {
//        alert("Please select type");
//        return false;
//    }
  
//    debugger;
//    var captcha = removeSpaces($('#txtCaptcha').val());
    
//    if (captcha == "0" || captcha == null || captcha == "") {
//        alert("Please enter captcha code");
//        return false;
//    }
//    else
//    {
//        if (captcha != removeSpaces($('#hdnCap').val()))
//        {
//            alert("Please enter valid captcha code");
//            return false;

//        }

//    }

//    var salt = '<%=Session["salt"]%>';
//    var hash = hex_sha512(hex_sha512(value) + salt);
//    alert("Hash "+hash);
//    document.getElementById('txtPassword').value = hash;
//    document.getElementById('<%=hfpwd.ClientID%>').value = hash;


//    return true;
//}
$('body').on('keypress', ".CopyValidate", function (event) {


});
$('body').on('paste', ".CopyValidate", function (e) {
    e.preventDefault();
    alert("You cannot paste text into this textbox!");
    return false;
});

  
    function Captcha() {

        var alphanum = new Array('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0');
        var a = alphanum[Math.floor(Math.random() * alphanum.length)];
        var b = alphanum[Math.floor(Math.random() * alphanum.length)];
        var c = alphanum[Math.floor(Math.random() * alphanum.length)];
        var d = alphanum[Math.floor(Math.random() * alphanum.length)];
        var e = alphanum[Math.floor(Math.random() * alphanum.length)];
    
        var code = a +' '+b+' '+c+' '+d+' '+ e;
        $('#imgCaptcha').val(code);
        $('#hdnCap').val(code);

    }
    function removeSpaces(string) {
        return string.split(' ').join('');
    }



