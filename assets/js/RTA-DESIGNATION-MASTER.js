function fnEdit(ID) {
    
    window.location.href = "RTA-DESIGNATION-MASTER-NEW.aspx?q=" + encodeURIComponent(ID)
}
function fnDelete(key) {
    $('input[id*=txtDelKey]').val(key);
    // $('#ctl00_ContentPlaceHolder1_txtDelKey').val(key);
    $("#delete").modal();
}
function fnDesignationValidate() {
   
    var DESG_NAME = $('input[id*=txtDESG_NAME]').val().trim();
    if (DESG_NAME == "" || DESG_NAME == null) {
        alert("Please enter designation name");
        return false;
    }
    if (DESG_NAME.length < 2) {
        alert("Please enter at least two characters in the designation name field");
        return false;
    }
    return true;
}
