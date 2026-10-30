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
     var SLD = $('input[id*=uploadSpeed]').val();
     if (SLD != "") {
         var fileUploadName = $('input[id*=uploadSpeed]')[0].files[0].name;
         var filesize = parseInt($('input[id*=uploadSpeed]')[0].files[0].size / 1024);
         var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
         if (fileextension.toString().toUpperCase() != ".PDF") {
             $('input[id*=uploadSpeed]').val('');
             alert("Plesae upload only PDF file for SLD Certificate");
             return false;
         }
         if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
             $('input[id*=uploadSpeed]').val('');
             alert("SLD Certificate file must be greater than 0 MB and less than 2 MB");
             return false;
         }
     }

     var PERMIT = $('input[id*=uploadPermit]').val();
     if (PERMIT != "")
     {
         var fileUploadName = $('input[id*=uploadPermit]')[0].files[0].name;
         var filesize = parseInt($('input[id*=uploadPermit]')[0].files[0].size / 1024);
         var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
         if (fileextension.toString().toUpperCase() != ".PDF")
         {
             $('input[id*=uploadPermit]').val('');
             alert("Plesae upload only PDF file for PERMIT Certificate");
             return false;
         }
         if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
             $('input[id*=uploadPermit]').val('');
             alert("PERMIT Certificate file must be greater than 0 MB and less than 2 MB");
             return false;
         }
     }

     var TIMING = $('input[id*=uploadTimetable]').val();
     if (TIMING != "") {
         var fileUploadName = $('input[id*=uploadTimetable]')[0].files[0].name;
         var filesize = parseInt($('input[id*=uploadTimetable]')[0].files[0].size / 1024);
         var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
         if (fileextension.toString().toUpperCase() != ".PDF") {
             $('input[id*=uploadTimetable]').val('');
             alert("Plesae upload only PDF file for Time Table Certificate");
             return false;
         }
         if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
             $('input[id*=uploadTimetable]').val('');
             alert("Time Table Certificate file must be greater than 0 MB and less than 2 MB");
             return false;
         }
     }
     var LETTER = $('input[id*=uploadLetter]').val();
     if (LETTER != "") {
         var fileUploadName = $('input[id*=uploadLetter]')[0].files[0].name;
         var filesize = parseInt($('input[id*=uploadLetter]')[0].files[0].size / 1024);
         var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
         if (fileextension.toString().toUpperCase() != ".PDF") {
             $('input[id*=uploadLetter]').val('');
             alert("Plesae upload only PDF file for Letter Certificate");
             return false;
         }
         if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
             $('input[id*=uploadLetter]').val('');
             alert("Letter Certificate file must be greater than 0 MB and less than 2 MB");
             return false;
         }
     }

     var TAX = $('input[id*=uploadTax]').val();
     if (TAX != "") {
         var fileUploadName = $('input[id*=uploadTax]')[0].files[0].name;
         var filesize = parseInt($('input[id*=uploadTax]')[0].files[0].size / 1024);
         var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
         if (fileextension.toString().toUpperCase() != ".PDF") {
             $('input[id*=uploadTax]').val('');
             alert("Plesae upload only PDF file for TAX Certificate");
             return false;
         }
         if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
             $('input[id*=uploadTax]').val('');
             alert("TAX Certificate file must be greater than 0 MB and less than 2 MB");
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
      
       var condition = true;
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