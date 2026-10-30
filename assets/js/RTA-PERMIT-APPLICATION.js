
function fnEdit(ID) {
    window.location.href = "RTA-PERMIT-APPLICATION-APPROVAL.aspx?q=" + encodeURIComponent(ID)
}

function fnPermitStatusList() {

    $('#loadingmessage').modal('show');
    return true;
}

