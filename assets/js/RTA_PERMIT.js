

function fnApplyPermit(ID) {
    window.location.href = "RTA_PERMIT_NEW.aspx?q=" + encodeURIComponent(ID)
}
function fnShowRoute(ID)
{
    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/RTA_PERMIT.ashx?caller=GetRTARouteDetails&q=" + encodeURIComponent(ID),
        data:"",
        cache: false,
        async: false,
        success: function (data) {

            $('#loadingmessage').modal('hide');
            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {

                    
                    $('div[id*=RouteTimeInfo]').html(data.split(/~(.+)?/)[1].toString());
                    ComponentsDateTimePickers.init();
                }
                else
                {
                    $('div[id*=RouteTimeInfo]').html("");
                }

            }

        },
        error: function (response) {
            $('#loadingmessage').modal('hide');
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function SaveApplication() {
    if (fnApplicationValidate())
    {
        
        var PERMIT_APPLICATION_LDRs = new Array();
        var condition = true;
        var length = $('#tbdRouteInfo tr:last').attr('id').split("_");
        $('#tbdRouteInfo tr').each(function () {
            //debugger;
            var parentid = $(this).attr('id').split("_");
            //alert(parentid);
            if (parentid[1] == 1)
            {
                if ($('#txtUPDepRoute_1').val().trim() == "" || $('#txtDownDepRoute_1').val().trim() == "" || $('#txtUPDepRoute_1').val().trim() == "0:00" || $('#txtDownDepRoute_1').val().trim() == "0:00") {
                    condition = false;
                    alert("Please enter Arrival/Departure Time");
                    return false;

                }
                else
                {
                    var UPCity = $('#lblUPRoute_' + parentid[1]).text();
                    var DownCity = $('#lblDownRoute_' + parentid[1]).text();
                    var UPDepTime = $('#txtUPDepRoute_' + parentid[1]).val().trim();
                    var DownDepTime = $('#txtDownDepRoute_' + parentid[1]).val().trim();
                    PERMIT_APPLICATION_LDRs.push({VIA_CITY: UPCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: UPDepTime, MODE: "UP" });
                    PERMIT_APPLICATION_LDRs.push({VIA_CITY: DownCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: DownDepTime, MODE: "DOWN" });
                    //alert(UPCity);
                    //alert(DownCity);
                    //alert(UPDepTime);
                    //alert(DownDepTime);

                }

            }
          else if (parentid[1] == length[1]) {
              if ($('#txtUPArrRoute_' + length[1]).val().trim() == "" || $('#txtDownArrRoute_' + length[1]).val().trim() == "" || $('#txtUPArrRoute_' + length[1]).val().trim() == "0:00" || $('#txtDownArrRoute_' + length[1]).val().trim() == "0:00") {
                    condition = false;
                    alert("Please enter Arrival/Departure Time");
                    return false;

                }
                else {
                    var UPCity = $('#lblUPRoute_' + parentid[1]).text();
                    var DownCity = $('#lblDownRoute_' + parentid[1]).text();
                    var UPArrTime = $('#txtUPArrRoute_' + parentid[1]).val().trim();
                    var DownArrTime = $('#txtDownArrRoute_' + parentid[1]).val().trim();
                    //alert(UPCity);
                    //alert(DownCity);
                    //alert(UPArrTime);
                    //alert(DownArrTime);
                   
                    PERMIT_APPLICATION_LDRs.push({VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime, DEPARTURE_TIMING: "", MODE: "UP" });
                    PERMIT_APPLICATION_LDRs.push({VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime, DEPARTURE_TIMING: "", MODE: "DOWN" });


                }
            }
            else
                {
            if (parentid[1] != 1) 
            {
                if ($('#txtUPArrRoute_' + parentid[1]).val().trim() == "" || $('#txtDownArrRoute_' + parentid[1]).val().trim() == "" || $('#txtUPDepRoute_' + parentid[1]).val().trim() == "" || $('#txtDownDepRoute_' + parentid[1]).val().trim() == ""
                    || $('#txtUPArrRoute_' + parentid[1]).val().trim() == "0:00" || $('#txtDownArrRoute_' + parentid[1]).val().trim() == "0:00" || $('#txtUPDepRoute_' + parentid[1]).val().trim() == "0:00" || $('#txtDownDepRoute_' + parentid[1]).val().trim() == "0:00"
                    ) {
                    condition = false;
                    alert("Please enter Arrival/Departure Time");
                    return false;


                }
                else {
                    var UPCity = $('#lblUPRoute_' + parentid[1]).text();
                    var DownCity = $('#lblDownRoute_' + parentid[1]).text();
                    var UPArrTime = $('#txtUPArrRoute_' + parentid[1]).val().trim();
                    var DownArrTime = $('#txtDownArrRoute_' + parentid[1]).val().trim();
                    var UPDepTime = $('#txtUPDepRoute_' + parentid[1]).val().trim();
                    var DownDepTime = $('#txtDownDepRoute_' + parentid[1]).val().trim();
                    //alert(UPCity);
                    //alert(DownCity);
                    //alert(UPArrTime);
                    //alert(DownArrTime);
                    //alert(UPDepTime);
                    //alert(DownDepTime);

                    PERMIT_APPLICATION_LDRs.push({VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime, DEPARTURE_TIMING: UPDepTime, MODE: "UP" });
                    PERMIT_APPLICATION_LDRs.push({VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime, DEPARTURE_TIMING: DownDepTime, MODE: "DOWN" });



                }
            }
            }

        });
        if (condition == false)
        {
            return false;

        }
        else
        {
            var RouteHdr = $("input[name='routeHdr']:checked").val();

            var xxx = JSON.stringify({ ROUTE_DETAIL_ID: RouteHdr, PERMIT_APPLICATION_LDRs: PERMIT_APPLICATION_LDRs });

            $('input[id*=txtroutjson]').val(xxx);
            $('#loadingmessage').modal('show');
            return true;
        }


    }
    else
    {

        return false;
    }
}
function fnSearchValidate() {
    if ($('input[id*=txtVehicleNumber]').val() == null || $('input[id*=txtVehicleNumber]').val() == "") {
        alert("Please enter Registration Number");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;
    
}
function fnApplicationValidate() {
    
    if ($('div[id*=RouteTimeInfo]').html() == null || $('div[id*=RouteTimeInfo]').html() == "") {
        alert("Please select Route Details");
        return false;
    }
    if ($('span[id*=lblPermitFees]').text() == null || $('span[id*=lblPermitFees]').text() == "") {
        alert("Permit Fees is not defined. So you can not apply for permit right now");
        return false;
    }
    var MobileNo = $('input[id*=txtMobileNo]').val().trim();
    if (MobileNo == "" || MobileNo == null) {
        alert("Please enter user mobile no");
        return false;
    }
    if (!validateMob(MobileNo)) {
        alert("Invalid Mobile Number");
        return false;
    }

    var RC = $('input[id*=uploadRC]').val();
    if (RC != "")
    {
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
    return true;
}

    



//$('input[type=file]').on('change', function () {
//    var file = this.files[0];
//    fileUploadName = file.name;
//    var filesize = parseInt(this.files[0].size / 1024);

//    var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
//    if (fileextension.toString().toUpperCase != ".PDF") {
//        $(this).val('');

//        alert("Plesae Upload Only PDF File");

//        return false;

//    }
//    else if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
//        $(this).val('');
//        alert("Size of file must be greater than 0 MB and less than 2 MB");

//        return false;

//    }
//});