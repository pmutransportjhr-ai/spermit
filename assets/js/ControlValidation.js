
function fnTermsCondition(service) {
    
    $('#loadingmessage').modal('show');
    $('#termsConditions').html('');
    switch (service) {
        ////////////////////////// STA Permit  ////////////////////
        case 'pafs': // pafs  ----------  1. STA Permit Application service
            $.get("Terms/sta_permit.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;
        case 'prfs': // prfs ------------ 2. STA Replacement of Vehicle
            $.get("Terms/sta_vehicle_replacement.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;
        case 'prnewfs': // prnewfs ------------ 3. STA Renewal of Permit
            $.get("Terms/sta_permit_renewal.html", function (data) {

                async: false;
                $('#termsConditions').append(data);
            });
            break;
        case 'pdfs': // pdfs ------------ 4. STA Duplicate of permit
            $.get("Terms/sta_permit_duplicate.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;
        case 'psfs': // psfs ------------ 5. STA Surrender of Permit
            $.get("Terms/sta_permit_surrender.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;
        case 'ptfs': // prfs ------------ 6. STA Transfer for permit
            $.get("Terms/sta_permit_transfer.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;
        case 'perfs': // prfs ------------ 7. extension of route application
            $.get("Terms/sta_permit_extension_of_route.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;

            ////////////////////////// Counter Signature ////////////////////
        case 'csrp': // csrp  ----------  1. Counter signature Replacement of Vehicle
            $.get("Terms/cs_replacement_of_vehicle.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;

        case 'csrenew': // csrenew  ---------- 2.  Counter signature renewal of application
            $.get("Terms/cs_renewal_of_permit.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;

        case 'csdp': // csdp  ---------- 3. Counter Signature Duplicate application
            $.get("Terms/cs_duplicate.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;

        case 'csapp': // csapp  ---------- 4.  Counter Signature application
            $.get("Terms/cs_apply.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;
            
        case 'cssapp': // cssapp ------ 5. counter signature surrender application
            $.get("Terms/cs_surrender.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;
        case 'cstapp': // cstapp ------ 6. counter signature transfer  application
            $.get("Terms/cs_transfer.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
            });
            break;
        case 'stpermitObj': // stpermitObj ------ 7. counter signature transfer  application
           
            $.get("Terms/cs_sta_permit_obj.html", function (data) {
                async: false;
                $('#termsConditions').append(data);
                
            });
            break;

        default:
            alert('Please refresh the page');
            
    } 
    $('#loadingmessage').modal('hide')
    $('#TermAndCondition').modal('show').delay(10000);
}

jQuery(document).ready(function () { $("#Loader").fadeOut("slow"); });
jQuery(window).on('load', function () { $("#Loader").fadeOut("slow"); });
jQuery(document).ready(function () {
$('body').on('keypress', ".regtxt", function (event) {
        // if (event.keyCode != 46 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode !== 9 && event.keyCode !== 13) {
            // var regex = new RegExp("^[A-Za-z \b]+$");
            // //var code = event.charCode === 0 ? event.which : event.charCode;
            // //var key = String.fromCharCode(code);
            // var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
            // if (!regex.test(key) || (key == 46) || (key == 37) || (key == 39) || (key == 40) || (key === 9) || (key === 13)) {
                // event.preventDefault(); 
                // alert("You can write only alphabets into this textbox!");
                // return false;
            // }
        // }
		
		var charCode = (event.which) ? event.which : event.keyCode
		if (
        (charCode != 8) &&
		(charCode != 9) &&
		(charCode != 32) &&
        (charCode < 65 || charCode > 90) &&
        (charCode < 97 || charCode > 122)) {
        alert("You can write only alphabets into this textbox!");
        return false;
    }
		
});
$('body').on('keypress', ".regtxtsomespl", function (event) {
    if (event.keyCode != 46 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode !== 9 && event.keyCode !== 13) {
        var regex = new RegExp("^[-0-9A-Za-z. \b]+$");
        //var code = event.charCode === 0 ? event.which : event.charCode;
        //var key = String.fromCharCode(code);
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key) || (key == 46) || (key == 37) || (key == 39) || (key == 40) || (key === 9) || (key === 13)) {
            event.preventDefault();
            alert("You can write only alphanumeric and some special characters should be allowed like dot and - value into this textbox!");
            return false;
        }
    }
});
$(".regreadonly").focus(function () {
        if ($(this).val())
            $(this).blur().css('background-color', 'gray');
});

$('body').on('keypress', ".regreadonly", function (event) {
    
    
        return false;
    
    //if (event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode !== 9 && event.keyCode !== 13) {
    //    var regex = new RegExp("^[]+$");
    //    //var code = event.charCode === 0 ? event.which : event.charCode;
    //    //var key = String.fromCharCode(code);
    //    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    //    if (!regex.test(key) ||  (key == 37) || (key == 39) || (key == 40) || (key === 9) || (key === 13)) {
    //        event.preventDefault();
    //        alert("You can not write into this textbox!");
    //        return false;
    //    }
    //}
});
$('body').on('keypress', ".address", function (event) {
    
    var charCode = (event.which) ? event.which : event.keyCode
    keychar = String.fromCharCode(charCode);
    //if (keychar == "'" || keychar == "`" || keychar == "!" || keychar == "@" || keychar == "#" || keychar == "$" || keychar == "%" || keychar == "^" || keychar == "&" || keychar == "*" || keychar == "(" || keychar == ")" || keychar == "-" || keychar == "_" || keychar == "+" || keychar == "=" || keychar == "/" || keychar == "~" || keychar == "<" || keychar == ">" || keychar == "," || keychar == ";" || keychar == ":" || keychar == "|" || keychar == "?" || keychar == "{" || keychar == "}" || keychar == "[" || keychar == "]" || keychar == "¬" || keychar == "£" || keychar == '"' || keychar == "\\") {
    if (charCode===13 || keychar == "'" || keychar == "`" || keychar == "@" || keychar == "$" || keychar == "^" || keychar == "*" || keychar == "=" || keychar == "~" || keychar == "<" || keychar == ">") {
    return false;
    }
    

});
$('body').on('keypress', ".number", function (event) {
    //if (event.keyCode != 46 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode !== 9 && event.keyCode !== 13) {
    //    var regex = new RegExp("^[0-9\b]+$");
    //    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    //    if (!regex.test(key) || (key == 46) || (key == 37) || (key == 39) || (key == 40) || (key === 9) || (key === 13)) {
    //        event.preventDefault();
    //        alert("You can write only digit into this textbox!");
    //        return false;
    //    }
    //}
    var charCode = (event.which) ? event.which : event.keyCode

    if (
        //(charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
        //(charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
        (charCode != 8) &&
		(charCode != 9) &&
        (charCode < 48 || charCode > 57)) {
        alert("You can write only digit into this textbox!");
        return false;
    }
    
});

$('body').on('keypress', ".regtxtnumber", function (event) {
   
    if (event.keyCode != 46 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode !== 9 && event.keyCode !== 13) {
        var regex = new RegExp("^[0-9A-Za-z \b]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key) || (key == 46) || (key == 37) || (key == 39) || (key == 40) || (key === 9) || (key === 13)) {
            event.preventDefault();
            alert("You can write only alphanumeric value into this textbox!");
            return false;
        }
    }
});

$('body').on('keypress', ".regtxtnumbersomespl", function (event) {
    if (event.keyCode != 46 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode !== 9 && event.keyCode !== 13) {
        var regex = new RegExp("^[-0-9A-Za-z-. \b]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key) || (key == 46) || (key == 37) || (key == 39) || (key == 40) || (key === 9) || (key === 13)) {
            event.preventDefault();
            alert("You can write only alphanumeric and some special characters should be allowed like dot and - value into this textbox!");
            return false;
        }
    }
});
$('body').on('keypress', ".numberWithdot", function (event) {
   
    var charCode = (event.which) ? event.which : event.keyCode

    if (
        //(charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
        (charCode != 8) &&
	    (charCode != 9) &&
        (charCode != 46 || $(this).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
        (charCode < 48 || charCode > 57)) {
        alert("You can write only decimal value into this textbox!");
        return false;
    }

    //if (event.keyCode != 46 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 40 && event.keyCode !== 9 && event.keyCode !== 13) {
    //    var regex = new RegExp("^[0-9.\b]+$");
    //    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    //    if (!regex.test(key) || (key == 46) || (key == 37) || (key == 39) || (key == 40) || (key === 9) || (key === 13)) {
    //        event.preventDefault();
    //        alert("You can write only decimal value into this textbox!");
    //        return false;
    //    }
    //}
});
$('body').on('keypress', ".CopyValidate", function (event) {

    
});
$('body').on('paste', ".regtxt,.address,.regtxtsomespl,.regtxtnumbersomespl,.regreadonly,.number,.regtxtnumber,.numberWithdot,.CopyValidate", function (e) {
    e.preventDefault();
    alert("You cannot paste text into this textbox!");
    return false;
});



});
function validateMob(mob) {
    var filter = /^[0-9]{10,11}$/;
    if (filter.test(mob)) {
        return true;
    }
    else {
        return false;
    }
}
function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}
function Ispassword(password) {
    var regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,20}$/;
    if (!regex.test(password)) {
        return false;
    } else {
        return true;
    }
}
function ShoePaymentConfirmModal() {
  $('#confirm').modal('show');
}

function STAServices(registrationNo, txtPermitID) {
    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/GetPermitNo.ashx?caller=GetSTAPermitNumber&q=" + encodeURIComponent(registrationNo),
        data: "",
        cache: false,
        async: false,
        success: function (data) {
            $('#loadingmessage').modal('hide');
            if (data.split('~')[0] != "FAIL") {
                var statusf1 = data.split('~')[1];
                $('input[id*="' + txtPermitID + '"]').val(statusf1);

            }

        },
        error: function (response) {
            $('#loadingmessage').modal('hide');
            alert(response.status + ' ' + response.statusText);
        }
    });
}