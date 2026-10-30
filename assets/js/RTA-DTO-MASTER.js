function fnEdit(ID)
{
    window.location.href = "RTA-DTO-MASTER-NEW.aspx?q=" + encodeURIComponent(ID)
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
    var RToCode = $('select[id*=ddlRTOCode]').val();
    if ($('input[id*=txtDTOCode]').val().trim() == "" || $('input[id*=txtDTOCode]').val().trim() == null) {
        alert("Please enter DTO code");
        return false;
    }
    if ($('input[id*=txtDTOName]').val().trim() == "" || $('input[id*=txtDTOName]').val().trim() == null) {
        alert("Please enter DTO name");
        return false;
    }
    if (RToCode == "0" || RToCode == null || RToCode == "") {
        alert("Please select RTO");
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
    
    //if (status == "0" || status == null || status == "") {
    //    alert("Please select status");
    //    return false;
    //}
    return true;
}
