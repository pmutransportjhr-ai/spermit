function fnGetRTO(){
jQuery(document).ready(function ()
{
    fnGetRTOList();
})
};
var arrRTO = new Array();
function fnGetRTOList()
{
    var webUrl = "api/RTA_PERMIT.ashx?caller=GetRTOList";
    $.ajax({
        type: 'GET',
        url: webUrl,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: false,
        success: function (data) {
            $('#ddlRTO_1').empty();
            $('#ddlRTO_1').append(
                $("<option></option>").val(0).html("--Please Select RTO--")
            );
            arrRTO = new Array();
            
            for (var x = 0; x < data.Table.length; x++) {
                var obj = new Object();
                obj.RTO_CODE = data.Table[x].RTO_CODE;
                obj.RTO_NAME = data.Table[x].RTO_NAME;
                arrRTO.push(obj);
                $('#ddlRTO_1').append(
                    $("<option></option>").val(data.Table[x].RTO_CODE).html(data.Table[x].RTO_NAME));
            }
            
        },
        error: function (response) {
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnEdit(ID) {
    window.location.href = "RTA_RESION_UPDATE.aspx?q=" + encodeURIComponent(ID)
}
function fnLegacyEdit(ID) {
    window.location.href = "RTA-ROUTE-MASTER-LEGACY.aspx?q=" + encodeURIComponent(ID)
}
function fnUpdateVacancies(ID, Vac, rn) {

    //$('div[id*=Pannel1]').css("display", "block");
    $('input[id*=txtUpdateVacancies]').val(Vac);
    $('input[id*=txtRouteNo]').val(rn);
    $('input[id*=HiddenRouteNo]').val(rn);
    $('input[id*=hdUv]').val(ID);
    $("#VacancyRoute").modal();

}
function fnbtnUpdate() {
    //$("#update").modal();
    if ($('input[id*=txtUpdateVacancies]').val().trim() == null || $('input[id*=txtUpdateVacancies]').val().trim() == "") {
        alert("Please enter Number of Update Vacancy");
        return false;
    }
    if ($('input[id*=txtRouteNo]').val().trim() == null || $('input[id*=txtRouteNo]').val().trim() == "") {
        alert("Please enter Route number");
        return false;
    }
    return true;
}
function fnDelete(key) {
    $('input[id*=txtDelKey]').val(key);
    $("#delete").modal();
}
var operRTOCount = 1;
function AddRTORow() {
    operRTOCount++;
    var add = "";
    add += '<tr id="trRTO_' + operRTOCount + '">';
    add += '<td class="col-md-5" id="tdViaRTO_' + operRTOCount + '">';
    add += '<select class="form-control" id="ddlRTO_' + operRTOCount + '"></select>';
    add += '</td>';
    add += '<td class="col-md-3" id="tdViaRTODistance_' + operRTOCount + '">';
    add += '<input type="text" maxlength="7"  class="numberWithdot form-control" id="txtRTODistance_' + operRTOCount + '"/>';
    add += '</td>';
    add += '<td class="col-md-3" id="tdRTOoperation_' + operRTOCount + '">';
    add += '<button type="button" class="btn green" onclick="javascript:AddRTORow(); return false;" ><i class="fa fa-plus"></i></button> <button type="button" class="btn red" onclick="javascript:RemoveRTORow(' + operRTOCount + '); return false;" ><i class="fa fa-minus"></i></button>';
    add += '</td>';
    add += '</tr>';

    $("#tdbViaRTO").append(add);
    $('#ddlRTO_' + operRTOCount).empty();
    $('#ddlRTO_' + operRTOCount).append($("<option></option>").val(0).html("--Please Select RTO--"));

    for (var i = 0; i < arrRTO.length; i++) {
        $('#ddlRTO_' + operRTOCount).append($("<option></option>").val(arrRTO[i].RTO_CODE).html(arrRTO[i].RTO_NAME));
    }



}
function RemoveRTORow(removeNum) {
    $('#trRTO_' + removeNum).remove();
}
var operCount = 1;
function AddRow() {
    operCount++;
    var add = "";
    add += '<tr id="trVia_' + operCount + '">';
    add += '<td class="col-md-3"> Via</td>';
    add += '<td class="col-md-5" id="tdViaCity_' + operCount + '">';
    add += '<input type="text" maxlength="30" class="regtxt form-control" id="txtCity_' + operCount + '"/>';
    add += '</td>';
    add += '<td class="col-md-3" id="tdoperation_' + operCount + '">';
    add += '<button type="button" class="btn green" onclick="javascript:AddRow(); return false;" ><i class="fa fa-plus"></i></button> <button type="button" class="btn red" onclick="javascript:RemoveRow(' + operCount + '); return false;" ><i class="fa fa-minus"></i></button>';
    add += '</td>';
    add += '</tr>';

    $("#tdbViaRoute").append(add);




}
function RemoveRow(removeNum) {
    $('#trVia_' + removeNum).remove();
}
function fnRouteValidate() {
    var sourceStation = $('select[id*=ddlSource]').val();
    var destinationCity = $('select[id*=ddlDestination]').val();
    //var routeFor = $('select[id*=ddlRouteFor]').val();
    var totalDistance = $('input[id*=txtTotalDistance]').val().trim();
    var vacancies = $('input[id*=txtVacancies]').val().trim();
    var MaxTrips = $('input[id*=txtNumberTrips]').val().trim();
    var RouteNo = $('input[id*=txtRouteNo]').val().trim();

    var status = "Active";//$('select[id*=ddlStatus]').val();

   if (sourceStation == "0" || sourceStation == null || sourceStation == "") {
        alert("Please select source station");
        return false;
    }
    if (destinationCity == "0" || destinationCity == null || destinationCity == "") {
        alert("Please select destination");
        return false;
    }
    //if (routeFor == "0" || routeFor == null || routeFor == "") {
    //    alert("Please select route for");
    //    return false;
    //}
    if (sourceStation==destinationCity) {
        alert("source station and destination station could not be same");
        return false;
    }
    
    if (totalDistance == "" || totalDistance == null) {
        alert("Please enter total distance");
        return false;
    }
     if (vacancies == "" || vacancies == null) {
        alert("Please enter state name");
        return false;
     }
     if (MaxTrips == "" || MaxTrips == null || MaxTrips == "0") {
         alert("Please enter Maximum Number of Trips");
         return false;
     }
     if (parseInt(MaxTrips)>10) {
         alert("Please enter Maximum Number of Trips between 1 to 10");
         return false;
     }
     if (RouteNo == "" || RouteNo == null) {
         alert("Please enter route number");
         return false;
     }
    if (status == "0" || status == null || status == "") {
        alert("Please select status");
        return false;
    }
 
    return true;
}
function SaveRoute() {
    if (fnRouteValidate()) {
        var sourceStation = $('select[id*=ddlSource]').val();
        var destinationCity = $('select[id*=ddlDestination]').val();
        //var routeFor = $('select[id*=ddlRouteFor]').val();
        var totalDistance = $('input[id*=txtTotalDistance]').val().trim();
        var vacancies = $('input[id*=txtVacancies]').val().trim();
        var MaxTrips = $('input[id*=txtNumberTrips]').val().trim();
        var routeNo = $('input[id*=txtRouteNo]').val().trim();

        var status = "Active";//$('select[id*=ddlStatus]').val();
        var arrInput = new Array();
        var arrInput1 = new Array();
        var obj = new Object();
    
        obj.ST_FROM = sourceStation;
        obj.ST_TO = destinationCity;
        //obj.STATE_CODE = routeFor;
        obj.TOTAL_DISTANCE = totalDistance;
        obj.NO_OF_VACANCIES = vacancies;
        obj.MAX_NUMBER_OF_TRIPS = MaxTrips;
        obj.STATUS = status;
        obj.ROUTE_NUMBER = routeNo;

        obj.ROUTE_MSTR_RTO = new Array();
        obj.ROUTE_MSTR_LDR = new Array();
        var condition = true;
        $('#tdbViaRTO  tr').each(function ()
        {
            var id = $(this).attr('id').split("_");
            arrInput.push($('#ddlRTO_' + id[1]).val());
            arrInput1.push($('#txtRTODistance_' + id[1]).val().trim());

            //alert(id[1]);
            if ($('#ddlRTO_' + id[1]).val() == "0") {
                condition = false;
                alert("Please select RTO");
                return false;

            }
           else if ($('#txtRTODistance_' + id[1]).val().trim() == "") {
                condition = false;
                alert("Please enter covered distance  in selected RTO");
                return false;

            }

            

            else {
                if ($('#ddlRTO_' + id[1]).val() != "0" || $('#txtRTODistance_' + id[1]).val().trim() != "") {
                    // alert("Childid " + Childid[1])
                    var RTO = $('#ddlRTO_' + id[1]).val();
                    var RTODistance = $('#txtRTODistance_' + id[1]).val().trim();
                    // alert("city " + city)
                    obj.ROUTE_MSTR_RTO.push({ RTO_CODE: RTO, DISTANCE_COVERED: RTODistance, SEQUENCE: id[1] });
                }
            }

        });
     
        var length = 0;
        obj.ROUTE_MSTR_LDR.push({ VIA_CITY: $('select[id*=ddlSource]').find('option:selected').text(), SEQUENCE: 1 });
        $('#tdbViaRoute  tr').each(function () {
            
            var Childid = $(this).attr('id').split("_");
            var Childid1 = parseInt(Childid[1]) + 1;
            length = Childid1 + 1;
            //alert(parentid[1]);

            //if ($('#txtCity_1').val().trim() == "") {
            //    condition = false;
            //   // alert("Please enter via city name");
            //    return false;

            //}

           // else {
                if ($('#txtCity_' + Childid[1]).val().trim() != "") {
                    // alert("Childid " + Childid[1])
                    var city = $('#txtCity_' + Childid[1]).val().trim();
                    // alert("city " + city)
                    obj.ROUTE_MSTR_LDR.push({ VIA_CITY: city, SEQUENCE: Childid1 });
                    
               // }
            }

        });
        obj.ROUTE_MSTR_LDR.push({ VIA_CITY: $('select[id*=ddlDestination]').find('option:selected').text(), SEQUENCE: length });

         var sorted_arr = arrInput.sort();
         var sorted_arr1 = arrInput1.sort();
         for (var i = 0; i < arrInput.length - 1; i++) {
             if (sorted_arr[i + 1] == sorted_arr[i]) {
                 alert("Please select unique RTO");
                 condition = false;
             }
         }
         for (var i = 0; i < arrInput1.length - 1; i++) {
             if (sorted_arr1[i + 1] == sorted_arr1[i]) {
                 alert("RTO covered distance could not be same.");
                 condition = false;
             }
         }
   
        if (condition == false) {
            return false;

        }
        else {


            //viacity.push(obj);
            var xxx = JSON.stringify(obj);
            $('input[id*=txtroutjson]').val(xxx);
            return true;
        }


    }
    else {

        return false;
    }
}
function fnEdit1() {
    
    jQuery(document).ready(function () {
        fnGetRTOList();
        var obj = new Object();

        var data = JSON.parse($('input[id*=txtroutjson]').val());
        $('select[id*=ddlSource]').val(data[0].ST_FROM).change();
        $('select[id*=ddlDestination]').val(data[0].ST_TO).change();
        //$('select[id*=ddlRouteFor]').val(data[0].STATE_CODE).change();
        $('input[id*=txtTotalDistance]').val(data[0].TOTAL_DISTANCE);
        $('input[id*=txtVacancies]').val(data[0].NO_OF_VACANCIES);
        $('input[id*=txtNumberTrips]').val(data[0].MAX_NUMBER_OF_TRIPS);
        //$('select[id*=ddlStatus]').val(data[0].STATUS).change();
        $('input[id*=txtRouteNo]').val(data[0].ROUTE_NUMBER);
        var incval = 1;
        var cntRTO = 1;
        operRTOCount = data[0].ROUTE_MSTR_RTO.length;
        for (var k = 0; k < data[0].ROUTE_MSTR_RTO.length; k++) {
            if (data[0].ROUTE_MSTR_RTO[k].SEQUENCE == "1") {
                
                $('#ddlRTO_1').val(data[0].ROUTE_MSTR_RTO[0].RTO_CODE);
                
                $('#txtRTODistance_1').val(data[0].ROUTE_MSTR_RTO[0].DISTANCE_COVERED);
            }
            else {

                cntRTO++
                var add2 = "";


                add2 += '<tr id="trRTO_' + cntRTO + '">';
                add2 += '<td class="col-md-5" id="tdViaRTO_' + cntRTO + '">';
                add2 += '<select class="form-control" id="ddlRTO_' + cntRTO + '"></select>';
                add2 += '</td>';
                add2 += '<td class="col-md-3" id="tdViaRTODistance_' + cntRTO + '">';
                add2 += '<input type="text" maxlength="7"  class="numberWithdot form-control" id="txtRTODistance_' + cntRTO + '"/>';
                add2 += '</td>';
                add2 += '<td class="col-md-3" id="tdRTOoperation_' + cntRTO + '">';
                add2 += '<button type="button" class="btn green" onclick="javascript:AddRTORow(); return false;" ><i class="fa fa-plus"></i></button> <button type="button" class="btn red" onclick="javascript:RemoveRTORow(' + cntRTO + '); return false;" ><i class="fa fa-minus"></i></button>';
                add2 += '</td>';
                add2 += '</tr>';

                $("#tdbViaRTO").append(add2);
                $('#ddlRTO_' + cntRTO).empty();
                $('#ddlRTO_' + cntRTO).append($("<option></option>").val(0).html("--Please Select RTO--"));

                for (var i = 0; i < arrRTO.length; i++) {
                    $('#ddlRTO_' + cntRTO).append($("<option></option>").val(arrRTO[i].RTO_CODE).html(arrRTO[i].RTO_NAME));
                }
                $('#ddlRTO_' + cntRTO).val(data[0].ROUTE_MSTR_RTO[k].RTO_CODE);
                $('#txtRTODistance_' + cntRTO).val(data[0].ROUTE_MSTR_RTO[k].DISTANCE_COVERED);
            }
        }
        operCount = data[0].ROUTE_MSTR_LDR.length;
        for (var j = 1; j < data[0].ROUTE_MSTR_LDR.length-1; j++) {
            if (data[0].ROUTE_MSTR_LDR[j].SEQUENCE == "2")
            {
               
                $('#txtCity_1').val(data[0].ROUTE_MSTR_LDR[1].VIA_CITY);
            }
            else
                {

                incval++
                var add1 = "";


                add1 += '<tr id="trVia_' + incval + '">';
                add1 += '<td class="col-md-3"> Via</td>';
                add1 += '<td class="col-md-5" id="tdViaCity_' + incval + '">';
                add1 += '<input type="text" maxlength="30" class="regtxt form-control" id="txtCity_' + incval + '"/>';
                add1 += '</td>';
                add1 += '<td class="col-md-3" id="tdoperation_' + incval + '">';
                add1 += '<button type="button" class="btn green" onclick="javascript:AddRow(); return false;" ><i class="fa fa-plus"></i></button> <button type="button" class="btn red" onclick="javascript:RemoveRow(' + incval + '); return false;" ><i class="fa fa-minus"></i></button>';
                add1 += '</td>';
                add1 += '</tr>';

                $("#tdbViaRoute").append(add1);
                $('#txtCity_' + incval).val(data[0].ROUTE_MSTR_LDR[j].VIA_CITY);
            }
            }

       


       




    });
    
   
}