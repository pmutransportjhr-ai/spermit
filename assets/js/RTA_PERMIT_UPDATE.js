
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
    var Speed = $('input[id*=uploadSpeed]').val();
    if (Speed != "") {
        var fileUploadName = $('input[id*=uploadSpeed]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadSpeed]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadSpeed]').val('');

            alert("Plesae upload only PDF file for Speed Governor Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadSpeed]').val('');
            alert("Speed Governor Certificate file must be greater than 0 MB and less than 2 MB");

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
    if (fileuploadValidate())
    {

        $('div[id*=confirm]').modal();
       
    }
}
function SaveApplication() {
   
      
       var PERMIT_APPLICATION_LDRs = new Array();
        var condition = true;
        var NoRoundTrip = $('input[id*=HFNoOftrips]').val();

            
        for (j = 1; j <= NoRoundTrip; j++)
        {
            var length = $('#tbdRouteInfo_' + j + ' tr:last').attr('id').split("_");
            $('#tbdRouteInfo_' + j + '  tr').each(function () {
                //debugger;
                var parentid = $(this).attr('id').split("_");
                //alert(parentid);
                if (parentid[1] == 1) {
                    if ($('#txtUPDepRoute' + j + '_1').val().trim() == "") {
                        condition = false;
                        alert("Please enter Arrival  Time for " + $('#lblUPRoute' + j + '_' + parentid[1]).text());
                        return false;

                    }
                    else if ($('#txtDownDepRoute' + j + '_1').val().trim() == "") {
                        condition = false;
                        alert("Please enter Departure Time for " + $('#lblDownRoute' + j + '_' + parentid[1]).text());
                        return false;

                    }
                    else {
                        var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                        var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                        var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();
                        var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();
                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: UPDepTime.toString(), MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: DownDepTime.toString(), MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });

                    }

                }
                else if (parentid[1] == length[1]) {
                    if ($('#txtUPArrRoute' + j + '_' + length[1]).val().trim() == "") {
                        condition = false;
                        alert("Please enter Arrival Time for " + $('#lblUPRoute' + j + '_' + parentid[1]).text());
                        return false;

                    }
                    else if ($('#txtDownArrRoute' + j + '_' + length[1]).val().trim() == "") {
                        condition = false;
                        alert("Please enter Departure  Time for " + $('#lblDownRoute' + j + '_' + parentid[1]).text());
                        return false;

                    }


                    else {
                        var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                        var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                        var UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();
                        var DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();
                       

                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime.toString(), DEPARTURE_TIMING: "", MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime.toString(), DEPARTURE_TIMING: "", MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });


                    }
                }
                else {
                    if (parentid[1] != 1) {
                      
                       
                        var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                        var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                        var UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();
                        var DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();
                        var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();
                        var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();


                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime.toString(), DEPARTURE_TIMING: UPDepTime.toString(), MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime.toString(), DEPARTURE_TIMING: DownDepTime.toString(), MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });




                    }
                }

            });

        }
        if (condition == false) {
            return false;

        }
        else {
            //var RouteHdr = $("input[name='routeHdr']:checked").val();

            var xxx = JSON.stringify({PERMIT_APPLICATION_LDRs: PERMIT_APPLICATION_LDRs });
             $('input[id*=txtroutjson]').val(xxx);
             $('#loadingmessage').modal('show');
            return true;
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