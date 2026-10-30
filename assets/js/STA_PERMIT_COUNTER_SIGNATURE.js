function intializePicker()
{
    ComponentsDateTimePickers.init();
}
function chkkk()
{
    alert("Here");
    var q = $('input[id*=txtqueryString]').val();
    var m = $('input[id*=txtSuccessmsg]').val();
    if (q == "" || q == null) {
        if (q == "Error") {
            alert(m);

        }
        if (q != "Error") {

            alert(m);
            window.location = "STA_PERMIT_COUNTER_SIGNATURE_RECEIPT.aspx?q=" + HttpUtility.UrlEncode(q);
        }
    }
}

jQuery(document).ready(function () {
          
    //$('input[id*=btnSave]').click(function () {
        //var q = $('input[id*=txtqueryString]').val();
        //var m = $('input[id*=txtSuccessmsg]').val();
        //if (q != "0") {
        //    if (q == "Error") {
        //        alert(m);
        //    }
        //    if (q != "Error") {
        //        alert(m);
        //        alert("QueryString"+q);
        //        window.location = "STA_INTER_STATE_PERMIT_COUNTER_SIGNATURE_RECEIPT.aspx?q=" + q;
        //    }
        //}

  //  }
  //);
   


    ComponentsDateTimePickers.init();
    ////$('input[id*=txtPermitValidUpto]').timepicker().parent;
    ////$('input[id*=txtPermitValidUpto]').parent('.input-group').on('click', '.input-group-btn', function (e) {
    ////    e.preventDefault();
    ////    $(this).parent('.input-group').find('.timepicker').timepicker('showWidget');
    ////});
    //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

    //function EndRequestHandler(sender, args) {
    //    $('input[id*=txtPermitValidUpto]').DateTimePicker({
    //        dateFormat: "yyyy-MM-dd",
    //        timeFormat: "hh:mm AA",
    //        dateTimeFormat: "yyyy-MM-dd hh:mm:ss AA"
    //    });
    //}
});
function fnApplyPermit(ID) {
    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/STA_PERMIT.ashx?caller=GetRouteDetailsBYID&q=" + encodeURIComponent(ID),
        data: "",
        cache: false,
        async: false,
        success: function (data) {

            $('#loadingmessage').modal('hide');
            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {
                    $('div[id*=RouteHDRInfo]').html(data.split(/~(.+)?/)[1].toString());
                }
                else {
                    $('div[id*=RouteHDRInfo]').html("");
                }

            }

        },
        error: function (response) {
            $('#loadingmessage').modal('hide');
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnShowRoute(ID) {
    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/STA_PERMIT.ashx?caller=GetSTARouteDetails&q=" + encodeURIComponent(ID),
        data: "",
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
                else {
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
    if (fnApplicationValidate()) {

        var PERMIT_APPLICATION_LDRs = new Array();
        var condition = true;
        var length = $('#tbdRouteInfo tr:last').attr('id').split("_");
        $('#tbdRouteInfo tr').each(function () {
            //debugger;
            var parentid = $(this).attr('id').split("_");
            //alert(parentid);
            if (parentid[1] == 1)
            {
                if ($('#txtUPDepRoute_1').val().trim() == "" || $('#txtDownDepRoute_1').val().trim() == "")
                {
                    condition = false;
                    alert("Please enter Departure Time");
                    return false;

                }
                else {
                    var UPCity = $('#lblUPRoute_' + parentid[1]).text();
                    var DownCity = $('#lblDownRoute_' + parentid[1]).text();
                    var UPDepTime = $('#txtUPDepRoute_' + parentid[1]).val().trim();
                    var DownDepTime = $('#txtDownDepRoute_' + parentid[1]).val().trim();
                    PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: UPDepTime, MODE: "UP" });
                    PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: DownDepTime, MODE: "DOWN" });
                    //alert(UPCity);
                    //alert(DownCity);
                    //alert(UPDepTime);
                    //alert(DownDepTime);

                }

            }
            else if (parentid[1] == length[1])
            {
                if ($('#txtUPArrRoute_' + length[1]).val().trim() == "" || $('#txtDownArrRoute_' + length[1]).val().trim() == "") {
                    condition = false;
                    alert("Please enter Departure Time");
                    return false;

                }
                else
                {
                    var UPCity = $('#lblUPRoute_' + parentid[1]).text();
                    var DownCity = $('#lblDownRoute_' + parentid[1]).text();
                    var UPArrTime = $('#txtUPArrRoute_' + parentid[1]).val().trim();
                    var DownArrTime = $('#txtDownArrRoute_' + parentid[1]).val().trim();
                    //alert(UPCity);
                    //alert(DownCity);
                    //alert(UPArrTime);
                    //alert(DownArrTime);

                    PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime, DEPARTURE_TIMING: "", MODE: "UP" });
                    PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime, DEPARTURE_TIMING: "", MODE: "DOWN" });


                }
            }
            else
            {
                if (parentid[1] != 1)
                {
                    if ($('#txtUPArrRoute_' + parentid[1]).val().trim() == "" || $('#txtDownArrRoute_' + parentid[1]).val().trim() == "" || $('#txtUPDepRoute_' + parentid[1]).val().trim() == "" || $('#txtDownDepRoute_' + parentid[1]).val().trim() == "") {
                        condition = false;
                        alert("Please enter Departure Time");
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

                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime, DEPARTURE_TIMING: UPDepTime, MODE: "UP" });
                        PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime, DEPARTURE_TIMING: DownDepTime, MODE: "DOWN" });



                    }
                }
            }

        });
        if (condition == false) {
            $('#loadingmessage').modal('hide');
            return false;

        }
        else
        {
           // var RouteHdr = $("input[name='routeHdr']:checked").val();
            var Route = $("input[name='routehsddr']:checked").val();

            var xxx = JSON.stringify({ ROUTE_ID: Route, ROUTE_DETAIL_ID: 0, INTER_STATE_COUNTER_SIGNATURE_APPLICATION_LDRs: PERMIT_APPLICATION_LDRs });

            $('input[id*=txtroutjson]').val(xxx);
            return true;
        }


    }
    else {

        $('#loadingmessage').modal('hide');
        return false;
    }
}
function fnApplicationValidate() {
    
    if ($('input[id*=txtOwnerName]').val() == null || $('input[id*=txtOwnerName]').val() == "")
    {
        alert("Please Enter the Applicant Holder Name");
        return false;
    }
    if ($('textarea[id*=txtRAddress]').val() == null || $('textarea[id*=txtRAddress]').val() == "") {
        alert("Please Enter the Residential Address");
        return false;
    }
    if ($('textarea[id*=txtBUAddress]').val() == null || $('textarea[id*=txtBUAddress]').val() == "") {
        alert("Please Enter the Business Address");
        return false;
    }
    var MobileNumber = $('input[id*=txtMobileNumber]').val();
    if (MobileNumber == null || MobileNumber == "") {
        alert("Please Enter the Mobile Number");
        return false;
    }
    
    if (MobileNumber != "" || MobileNumber != null) {
        if (validateMob(MobileNumber) == false) {
            alert("Please Enter Valid Mobile Number");
            return false;
        }
    }
    if ($('input[id*=txtPermitNo]').val() == null || $('input[id*=txtPermitNo]').val() == "") {
        alert("Please Enter the Primary Permit Number");
        return false;
    }
    if ($('input[id*=txtPermitValidUpto]').val() == null || $('input[id*=txtPermitValidUpto]').val() == "") {
        alert("Please Enter the Permit Valid Upto");
        return false;
    }
    if ($('input[id*=txtPermitValidUpto]').val() != "") {

        _currentDate = GetTodayDate();

        var datebreakup = $('input[id*=txtPermitValidUpto]').val().split('/');
        var enteredDate = datebreakup[2] + "-" + datebreakup[1] + "-" + datebreakup[0];

        if (new Date(enteredDate) < new Date(_currentDate)) {
            alert("Please select valid date for Valid Upto");
            $('input[id*=txtPermitValidUpto]').val('');
            return false;
        }

    }
    if ($('select[id*=ddlStateIssuedBy]').val() == null || $('select[id*=ddlStateIssuedBy]').val() == "") {
        alert("Please Select State from Where the Permit issued");
        return false;
    }
    if ($('input[id*=txtCarriageNo]').val() == null || $('input[id*=txtCarriageNo]').val() == "") {
        alert("Please Enter Vehicle Registration Number");
        return false;
    }
    if ($('input[id*=txtMenufecturingDt]').val() == null || $('input[id*=txtMenufecturingDt]').val() == "") {
        alert("Please Enter Vehicle Menufecturing Date");
        return false;
    }
    if ($('input[id*=txtseatCapacity]').val() == null || $('input[id*=txtseatCapacity]').val() == "") {
        alert("Please Enter Vehicle Seat Capacity");
        return false;
    }
    if ($("input[name='routehsddr']:checked").val() == null || $("input[name='routehsddr']:checked").val() == "") {
        alert("Please select Route");
        return false;
    }
    if ($('input[id*=txtvalidityfrom]').val() == null || $('input[id*=txtvalidityfrom]').val() == "") {
        alert("Please Enter Counter Signature Validity From");
        return false;
    }
    if ($('input[id*=txtvalidityto]').val() == null || $('input[id*=txtvalidityto]').val() == "") {
        alert("Please Enter Counter Signature Validity Up To");
        return false;
    }
    
    if ($("input[name='routeHdr']:checked").val() == null || $("input[name='routeHdr']:checked").val() == "") {
        alert("Please select Route Details");
        return false;
    }
    if ($('div[id*=RouteTimeInfo]').html() == null || $('div[id*=RouteTimeInfo]').html() == "") {
        alert("Route time table is not displaying. So you can not apply for counter signature application right now");
        return false;
    }
    
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
    var Insurance = $('input[id*=uploadINSURANCE]').val();
    if (Insurance != "") {
        var fileUploadName = $('input[id*=uploadINSURANCE]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadINSURANCE]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadINSURANCE]').val('');

            alert("Plesae upload only PDF file for Insurance Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadINSURANCE]').val('');
            alert("Insurance Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }
    }
    var Pollution = $('input[id*=uploadPOLLUTION]').val();
    if (Pollution != "") {
        var fileUploadName = $('input[id*=uploadPOLLUTION]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadPOLLUTION]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadPOLLUTION]').val('');

            alert("Plesae upload only PDF file for Pollution Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadPOLLUTION]').val('');
            alert("Pollution Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }
    }
    var Fitness = $('input[id*=uploadFITNESS]').val();
    if (Fitness != "") {

        var fileUploadName = $('input[id*=uploadFITNESS]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadFITNESS]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadFITNESS]').val('');

            alert("Plesae upload only PDF file for Fitness Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadFITNESS]').val('');
            alert("Fitness Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    var PermitVal = $('input[id*=uploadPERMIT]').val();
    if (PermitVal != "") {

        var fileUploadName = $('input[id*=uploadPERMIT]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadPERMIT]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadPERMIT]').val('');

            alert("Plesae upload only PDF file for Permit Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadPERMIT]').val('');
            alert("Permit Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    else
    {
        alert("Please select Permit Certificate File");
        return false;
    }
   
    $('#loadingmessage').modal('show');
    return true;
}
function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth() + parseInt(1); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    if (MM < 10)
        MM = '0' + MM;
    if (dd < 10)
        dd = '0' + dd;
    var xxx = yyyy + "-" + MM + "-" + dd;
    return xxx;
}

