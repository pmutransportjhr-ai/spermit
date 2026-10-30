function fnEdit(ID) {
    window.location.href = "STA-USER-SETUP-NEW.aspx?q=" + encodeURIComponent(ID)
}
function fnDelete(key) {
    $('input[id*=txtDelKey]').val(key);
    $("#delete").modal();
}
function fnUserValidate() {
    
    var Username = $('input[id*=txtName]').val().trim();
    var loginId = $('input[id*=txtLoginId]').val().trim();
    var Pasword = $('input[id*=txtPassword]').val().trim();
    var ConfirmPassword = $('input[id*=txtConfirmPassword]').val().trim();
    var Department = $('select[id*=ddlDepartment]').val();
    var Designation = $('select[id*=ddlDesignation]').val();
    var MobileNo = $('input[id*=txtMobileNo]').val().trim();
    var EmailID = $('input[id*=txtEmailId]').val().trim();
    var SecurityQuestion = $('select[id*=ddlSecurityQuestion]').val();
    var SecurityAnswer = $('input[id*=txtSecurityAnswer]').val().trim();
    var Status = $('select[id*=ddlStatus]').val();
    var UserLevel = $('select[id*=ddlULevel]').val();

    if (Username == "" || Username == null) {
        alert("Please enter user name");
        return false;
    }
    if (loginId == "" || loginId == null) {
        alert("Please enter login Id");
        return false;
    }
    if (Pasword == "" || Pasword == null) {
        alert("Please enter pasword");
        return false;
    }
   
    if ($('input[id*=hdnID]').val().trim() == "0")
    {
    if (!Ispassword(Pasword)) {
        alert("Minimum 8 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character i.e. @$!%*#?");
        return false;
    }
    }
    if (ConfirmPassword == "" || ConfirmPassword == null) {
        alert("Please enter confirm pasword");
        return false;
    }
    if (Pasword != ConfirmPassword) {
        alert("Password not match");
        $('input[id*=txtPassword]').val('');
        $('input[id*=txtConfirmPassword]').val('');
        return false;
    }

    
   
    if (Department == "0" || Department == "" || Department == null) {
        alert("Please select user department");
        return false;
    }

    if (Designation == "0" || Designation == "" || Designation == null) {
        alert("Please select user designation");
        return false;
    }
    if (MobileNo == "" || MobileNo == null) {
        alert("Please enter user mobile no");
        return false;
    }
    if (!validateMob(MobileNo)) {
        alert("Invalid Mobile Number");
        return false;
    }
    if (EmailID == "" || EmailID == null) {
        alert("Please enter Email Id");
        return false;
    }
    if (!IsEmail(EmailID)) {
        alert("Invalid email address.");

        return false;
    }
    if (UserLevel == 0 || UserLevel == null || UserLevel == "")
    {
        alert("Please select User Level");
        return false;
    }
    if (SecurityQuestion == "0" || SecurityQuestion == null || SecurityQuestion == "") {
        alert("Please select security question");
        return false;
    }
    if (SecurityAnswer == "" || SecurityAnswer == null) {
        alert("Please enter answer");
        return false;
    }

    if (Status == "0" || Status == null || Status == "") {
        alert("Please select user status");
        return false;
    }
    //if ($('input[type="checkbox"]:checked').length==0) {
    //    alert("Please select atleast one menu");
    //    return false;
    //}
   
    setpsd();
    return true;
}
//jQuery(document).ready(function () {
    
//    //$('input[id*=txtLoginId]').val('');
//    //$('input[id*=txtPassword]').val('');
   
//    $('input[type="checkbox"]').click(function () {
//        if ($(this).prop("checked") == true) {

//            var objReadId = $(this).closest('tr').find($("input[name^='Read']")).attr('id');


//            $('#' + objReadId).parent('span').addClass('checked');

//        }
//        else if ($(this).prop("checked") == false) {
//            var objWriteID = $(this).closest('tr').find($("input[name^='Read']")).attr('id');
//            var fg = objWriteID.replace('Read', 'Write')
//            $('#' + fg).parent('span').removeClass('checked');

//        }

//    });
//})
function MenuItems() {
    if (fnUserValidate())
    {
       
       // var Menuaccess = new Array();

       // $('#tdbMenuAccess tr.tblparent').each(function () {
       //     var Rowid = $(this).attr('id').split('_');
       //     var ActRowID = Rowid[1];
       //     if ($('#Read' + ActRowID).parent('span').hasClass('checked') == true || $('#Write' + ActRowID).parent('span').hasClass('checked') == true) {
       //         var READACCESS = null;
       //         var WRITEACCESS = null;
       //         if ($('#Read' + ActRowID).parent('span').hasClass('checked') == true) {
       //             READACCESS = 'Y';
       //         }
       //         else if ($('#Read' + ActRowID).parent('span').hasClass('checked') == false) {
       //             READACCESS = 'N';
       //         }

       //         if ($('#Write' + ActRowID).parent('span').hasClass('checked') == true) {
       //             WRITEACCESS = 'Y';
       //         }
       //         else if ($('#Write' + ActRowID).parent('span').hasClass('checked') == false) {
       //             WRITEACCESS = 'N';
       //         }
       //         if (READACCESS != null || WRITEACCESS != null) {
       //             Menuaccess.push({ MENU_ID: ActRowID, READ_ACCESS: READACCESS, WRITE_ACCESS: WRITEACCESS });
       //         }

       //     }


       // });
       // var xxx = JSON.stringify({ MenuaccessM: Menuaccess });
       
       //// document.getElementById('<%=hdnMenuItems.ClientID%>').value = JSON.stringify({ MenuaccessM: Menuaccess });
       // $('input[id*=hdnMenuItems]').val(xxx);
    }
    else {

        return false;
    }
}
//function fnEdit1() {
//    jQuery(document).ready(function () {


//        var data = $('input[id*=hdnJsonData]').val();
//        var newData = JSON.parse(data);
//        for (var i = 0; i < newData.length ; i++) {
//            if (newData[i].READ_ACCESS == 'Y') {
//                var ReadID = $('#' + newData[i].MENU_ID).closest('tr').find("input[id^='Read']").attr('id');
//                if ($('#' + ReadID).parent('span').hasClass('checked') == false) {
//                    $('#' + ReadID).attr('checked', 'checked');

//                }

//            }
//            if (newData[i].WRITE_ACCESS == 'Y') {
//                var WriteID = $('#' + newData[i].MENU_ID).closest('tr').find("input[id^='Write']").attr('id');
//                if ($('#' + WriteID).parent('span').hasClass('checked') == false) {
//                    $('#' + WriteID).attr('checked', 'checked');

//                }
//            }


//        }



//    });
//}