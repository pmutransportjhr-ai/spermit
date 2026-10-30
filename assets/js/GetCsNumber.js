/// <reference path="../global/plugins/jquery.min.js" />
$(document).ready(function () { 
        $('input[id*="txtExistRegNo"]').on('blur', function () {
      CounterSignatureServices($(this).val());
   
        });
});

function CounterSignatureServices(registrationNo) {
    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/GetPermitNo.ashx?caller=GetCSPermitNumber&q=" + encodeURIComponent(registrationNo),
        data: "",
        cache: false,
        async: false,
        success: function (data) {
            $('#loadingmessage').modal('hide');
            if (data.split('~')[0] != "FAIL") {
                var statusf1 = data.split('~')[1];
                $('input[id*="txtExistPermitNo"]').val(statusf1);

            }

        },
        error: function (response) {
            $('#loadingmessage').modal('hide');
            alert(response.status + ' ' + response.statusText);
        }
    });
}