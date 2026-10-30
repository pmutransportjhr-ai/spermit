
function fnEdit(ID) {
    window.location.href = "RTA-TRANSFER-OF-APPLICATION-VERIFY.aspx?q=" + encodeURIComponent(ID)
}
function fnEditApprove(ID) {
    window.location.href = "RTA-VEHICLE-REPLACE-APPLICATION-APPROVAL.aspx?q=" + encodeURIComponent(ID)
}
function fnApplicationStatusValidation() {
    var applicationStatus = $('select[id*=ddlApplicationStatus]').val();

    var ObjectionDate = $('input[id*=txtObjectionDate]').val();

    if (applicationStatus == "" || applicationStatus == "0" || applicationStatus == null) {
        alert("Please select application status");
        return false;
    }

    return true;

}
function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth() + parseInt(1); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    if (MM < 10)
        MM = '0' + MM;
    if (dd < 10)
        dd = '0' + dd;
    var xxx = yyyy + "-" + MM + "-" + dd;
    return xxx;
}
$('select[id*=ddlApplicationStatus]').on('change', function () {

    var objId = $(this).val();
    if (objId == "Rejected") {
        $('input[id*=txtObjectionDate]').val('');
        $('input[id*=txtObjectionDate]').attr('Readonly', true);
    }
    else if (objId == "Verified")
        $('input[id*=txtObjectionDate]').attr('Readonly', false);
});
$('input[id*=txtObjectionDate]').on('mousedown', function () {

    if ($(this).is('[readonly]')) {
        alert('Not Allowed');
        $(this).val('');
        return false;
    }

});
function fnLoading() {

    $('#loadingmessage').modal('show');
    return true;
}
