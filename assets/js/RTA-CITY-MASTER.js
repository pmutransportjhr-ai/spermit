
function fnEdit(ID) {
    window.location.href = "RTA-CITY-MASTER-NEW.aspx?q=" + encodeURIComponent(ID)
}

function fnDelete(key) {
    $('input[id*=txtDelKey]').val(key);
    $("#delete").modal();
}
function fnCityValidate() {

    
    var cityName = $('input[id*=txtCityName]').val().trim();
    //var status = $('select[id*=ddlStatus]').val();
    
    if (cityName == "" || cityName == null) {
        alert("Please enter city name");
        return false;
    }
    if (cityName.length < 2) {
        alert("Please enter at least two characters in the city name field");
        return false;
    }
    //if (status == "0" || status == null || status == "") {
    //    alert("Please select city status");
    //    return false;
    //}
    return true;
}