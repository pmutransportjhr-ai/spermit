function fnEdit(ID)
{
    window.location.href = "RTA-DEPARTMENT-MASTER-NEW.aspx?q=" + encodeURIComponent(ID)
}
function fnDelete(key) {
    $('input[id*=txtDelKey]').val(key);
   // $('#ctl00_ContentPlaceHolder1_txtDelKey').val(key);
    $("#delete").modal();
}
function fnDepartmentValidate() {

    var DepartmentName = $('input[id*=txtDepartmentName]').val().trim();
    if (DepartmentName == "" || DepartmentName == null) {
        alert("Please enter department name");
        return false;
    }
    if (DepartmentName.length < 2) {
        alert("Please enter at least two characters in the department name field");
        return false;
    }
    return true;
}
