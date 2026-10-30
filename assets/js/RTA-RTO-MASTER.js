function fnEdit(ID)
{
    window.location.href = "RTA-RTO-MASTER-NEW.aspx?q=" + encodeURIComponent(ID)
}
function fnDelete(key) {
    $('input[id*=txtDelKey]').val(key);
   // $('#ctl00_ContentPlaceHolder1_txtDelKey').val(key);
    $("#delete").modal();
}
function fnRTOValidate() {

    var MobileNo = $('input[id*=txtPhoneNo]').val().trim();
    var EmailID = $('input[id*=txtEmailId]').val().trim();
    var status = $('select[id*=ddlStatus]').val();
    if ($('input[id*=txtRTOCode]').val().trim() == "" || $('input[id*=txtRTOCode]').val().trim() == null) {
        alert("Please enter RTO code");
        return false;
    }
    if ($('input[id*=txtRTOName]').val().trim() == "" || $('input[id*=txtRTOName]').val().trim() == null) {
        alert("Please enter RTO name");
        return false;
    }
    if (MobileNo == "" || MobileNo == null) {
        alert("Please enter phone number");
        return false;
    }
    if (!validateMob(MobileNo)) {
        alert("Invalid Phone Number");
        return false;
    }
    if (EmailID == "" || EmailID == null) {
        alert("Please enter Email Id");
        return false;
    }
    if (!IsEmail(EmailID)) {
        alert("Invalid Email Id");

        return false;
    }
    if ($('textarea[id*=txtAddress]').val().trim() == "" || $('textarea[id*=txtAddress]').val().trim() == null) {
        alert("Please enter RTO Address");
        return false;
    }
    if ($('input[id*=txtTREASCODE]').val().trim() == "" || $('input[id*=txtTREASCODE]').val().trim() == null) {
        alert("Please enter TreasCode");
        return false;
    }
    if ($('input[id*=txtIFMSOFFICECODE]').val().trim() == "" || $('input[id*=txtIFMSOFFICECODE]').val().trim() == null) {
        alert("Please enter IFMS Office Code");
        return false;
    }
    //if (status == "0" || status == null || status == "") {
    //    alert("Please select status");
    //    return false;
    //}
    return true;
}
