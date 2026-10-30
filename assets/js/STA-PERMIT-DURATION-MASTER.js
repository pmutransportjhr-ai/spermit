
function fnEdit(ID) {
    window.location.href = "STA-PERMIT-DURATION-MASTER-NEW.aspx?q=" + encodeURIComponent(ID)
}

function fnDelete(key) {
    $('input[id*=txtDelKey]').val(key);
    $("#delete").modal();
}
function fnDurationValidate() {

    var permitType = $('select[id*=ddlPermitType]').val();
    var permitDuration = $('input[id*=txtPermitDuration]').val().trim();
    var durationType = $('select[id*=ddlYM]').val();

    if (permitDuration == "" || permitDuration == null) {
        alert("Please enter permit duration");
        return false;
    }
    
    if (permitType == "0" || permitType == "" || permitType == null) {
        alert("Please select permit Type");
        return false;
    }



    if (durationType == "0" || durationType == null || durationType == "") {
        alert("Please select years/months");
        return false;
    }

    return true;
}


jQuery(document).ready(function () {
    $('input[id*=txtPermitDuration]').keypress(function () {
        $(this).attr('maxlength', '2');
       
    });
})

