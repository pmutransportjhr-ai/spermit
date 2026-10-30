

function fileuploadValidate()
{
    var RC = $('input[id*=uploadRC]').val();
    if (RC != "") {
        var fileUploadName = $('input[id*=uploadRC]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadRC]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadRC]').val('');

            alert("Plesae upload only PDF file for Registration Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadRC]').val('');
            alert("Registration Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }

    }
    if ($('input[id*=txtICValidUpto]').val() == null || $('input[id*=txtICValidUpto]').val() == "") {
        alert("Please select Insurance Certificate Validity Date");
        return false;
    }
    if ($('input[id*=txtPCValidUpto]').val() == null || $('input[id*=txtPCValidUpto]').val() == "") {
        alert("Please select Pollution Certificate Validity Date");
        return false;
    }
    if ($('input[id*=txtFCValidUpto]').val() == null || $('input[id*=txtFCValidUpto]').val() == "") {
        alert("Please select Fitness Certificate Validity Date");
        return false;
    }
    var Insurance = $('input[id*=uploadInsur]').val();
    if (Insurance != "") {
        var fileUploadName = $('input[id*=uploadInsur]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadInsur]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadInsur]').val('');

            alert("Plesae upload only PDF file for Insurance Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadInsur]').val('');
            alert("Insurance Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }
    }
    var Pollution = $('input[id*=uploadPollution]').val();
     if (Pollution != "") {
        var fileUploadName = $('input[id*=uploadPollution]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadPollution]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadPollution]').val('');

            alert("Plesae upload only PDF file for Pollution Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadPollution]').val('');
            alert("Pollution Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }
    }
    var Fitness = $('input[id*=uploadFitness]').val();
     if (Fitness != "") {

        var fileUploadName = $('input[id*=uploadFitness]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadFitness]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadFitness]').val('');

            alert("Plesae upload only PDF file for Fitness Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadFitness]').val('');
            alert("Fitness Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
        
    

            
       
       
        return true;

    }

function ShowUpdateConfirmModal()
{
    $('div[id*=confirm]').modal();
}
function SaveApplication() {
   if (fileuploadValidate) {
      
       
        if (condition == false) {
            return false;

        }
        else {
            //var RouteHdr = $("input[name='routeHdr']:checked").val();

            return true;
        }


    }
}
function timeFunction(id) {
    var objArray = $('#' + id).val().split(':');
    if (parseInt(objArray[0]) >= parseInt('0') && parseInt(objArray[0]) <= parseInt('23')) {
        if (parseInt(objArray[1]) >= parseInt('0') && parseInt(objArray[1]) <= parseInt('59')) {

        }
        else {
            $('#' + id).val('');
        }
    }
    else {
        $('#' + id).val('');
    }


}