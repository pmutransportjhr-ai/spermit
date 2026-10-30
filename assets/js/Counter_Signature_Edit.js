function fnValidateReport() {
    if ($('select[id*=ddlDesState]').val() == "" || $('select[id*=ddlDesState]').val() == null) {
        alert("Please Select State");
        return false;

    }
    if ($('select[id*=ddlStatus]').val() == "" || $('select[id*=ddlStatus]').val() == null) {
        alert("Please Select Status");
        return false;

    }
    if ($('select[id*=ddlType]').val() == "" || $('select[id*=ddlType]').val() == null) {
        alert("Please Select Type");
        return false;

    }
    //if ($('input[id*=txtFrom]').val() == "" || $('input[id*=txtFrom]').val() == null) {
    //    alert("Please Enter From Date");
    //    return false;

    //}
    //if ($('input[id*=txtTo]').val() == "" || $('input[id*=txtTo]').val() == null) {
    //    alert("Please Enter To Date");
    //    return false;

    //}

    return true;
}
function fnPayDetail(ID) {
    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/STA_PERMIT.ashx?caller=GetPayDetails&q=" + encodeURIComponent(ID),
        data: "",
        cache: false,
        async: false,
        success: function (data) {
            $('#loadingmessage').modal('hide');
            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {


                    $('div[id*=Tbody1]').html(data.split(/~(.+)?/)[1].toString());
                    $("#PaymentDetail").modal();

                }
                else {
                    $('div[id*=Tbody1]').html("");
                    $("#PaymentDetail").modal();
                }

            }
            else
            {
                alert(data);
            }

        },
        error: function (response) {
            $('#loadingmessage').modal('hide');
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function MasterView(q) {
    window.open("STA-COUNTER-REPORT-EDIT-VIEW.aspx?q=" + encodeURIComponent(q), "_blank");
}



function showTimetable(q) {
    window.open("NEW_CS_EDIT.aspx?q=" + encodeURIComponent(q));
}