


function fnObjectionGoValidate() {
    if ($('input[id*=txtObjRegNo]').val() == null || $('input[id*=txtObjRegNo]').val() == "") {
        alert("Please enter Registration Number");
        return false;
    }
    if ($('input[id*=txtObjPermitNo]').val() == null || $('input[id*=txtObjPermitNo]').val() == "") {
        alert("Please enter Permit Number");
        return false;
    }
   
    
    $('#loadingmessage').modal('show');
    return true;

}
function fnObjectionValidate() {
    $('.form-control').css('border-color', '#c2cad8');
    //if ($('input[id*=txtObjectorName]').val() == null || $('input[id*=txtObjectorName]').val() == "") {
    //    alert("Please enter Objector's Name");
    //    return false;
    //}
    if ($('select[id*=ddlObjType]').val() == "0" || $('select[id*=ddlObjType]').val() == null || $('select[id*=ddlObjType]').val() == "") {
        alert("Please select Objection Type");
        return false;
    }
    if ($('input[id*=txtObjRegNo]').val() == null || $('input[id*=txtObjRegNo]').val() == "") {
        $('input[id*=txtObjRegNo]').css('border-color', 'red').focus();
        alert("Please enter Registration Number");
        return false;
    }
    if ($('input[id*=txtObjPermitNo]').val() == null || $('input[id*=txtObjPermitNo]').val() == "") {
        $('input[id*=txtObjPermitNo]').css('border-color', 'red').focus();
              alert("Please enter Permit Number");
        return false;
    }
    if ($('textarea[id*=txtObjectionRemarks]').val() == null || $('textarea[id*=txtObjectionRemarks]').val() == "") {
        $('input[id*=txtObjectionRemarks]').css('border-color', 'red').focus();
        alert("Please enter Remarks");
        return false;
    }
    if ($('span[id*=lblObjectionFees]').text() == null || $('span[id*=lblObjectionFees]').text() == "") {
        alert("Application Fees is not defined. So you can not apply for Objection right now");

        return false;
    }

    $('#loadingmessage').modal('show');
    return true;

}