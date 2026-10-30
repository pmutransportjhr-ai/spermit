function fnEdit(ID) {
    window.location.href = "STA-ROUTE-MASTER-NEW.aspx?q=" + encodeURIComponent(ID)
}
function fnLegacyEdit(ID) {
    window.location.href = "STA-ROUTE-MASTER-LEGACY.aspx?q=" + encodeURIComponent(ID)
}
function fnUpdateVacancies(ID,Vac,rn) {
    
                   // $('div[id*=Pannel1]').css("display", "block");
    $('input[id*=txtUpdateVacancies]').val(Vac);
    $('input[id*=txtRouteNo]').val(rn);
    
                    $('input[id*=HiddenRouteNo]').val(rn);
                    $('input[id*=hdUv]').val(ID);
                    $("#VacancyRoute").modal();
    
}
function fnDelete(key) {
    $('input[id*=txtDelKey]').val(key);
    $("#delete").modal();
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
    add += '<button type="button" class="btn green" onclick="javascript:AddRow();" ><i class="fa fa-plus"></i></button> <button type="button" class="btn red" onclick="javascript:RemoveRow(' + operCount + ');" ><i class="fa fa-minus"></i></button>';
    add += '</td>';
    add += '</tr>';

    $("#tdbViaRoute").append(add);




}
function RemoveRow(removeNum) {
    $('#trVia_' + removeNum).remove();
}
function fnRouteValidate() {

    var sourceStation = $('select[id*=ddlSource]').val();
    var desState = $('select[id*=ddlDesState]').val();
    var destinationCity = $('select[id*=ddlDestination]').val();
    var totalDistance = $('input[id*=txtTotalDistance]').val().trim();
    var homeDistance = $('input[id*=txtHomeDistance]').val().trim();
    var vacancies = $('input[id*=txtVacancies]').val().trim();
    var viaCity = $('#txtCity_1').val().trim();
    var RouteNo = $('input[id*=txtRouteNo]').val().trim();

    var status = $('select[id*=ddlStatus]').val();

    if (sourceStation == "0" || sourceStation == null || sourceStation == "") {
        alert("Please select source station");
        return false;
    }
    if (desState == "0" || desState == null || desState == "") {
        alert("Please select destination state");
        return false;
    }
    if (destinationCity == "0" || destinationCity == null || destinationCity == "") {
        alert("Please select destination");
        return false;
    }
    if (totalDistance == "" || totalDistance == null) {
        alert("Please enter total distance");
        return false;
    }
    if (homeDistance == "" || homeDistance == null) {
        alert("Please enter distance in home state");
        return false;
    }
    if (vacancies == "" || vacancies == null) {
        alert("Please enter no. of Vacancies");
        return false;
    }
    if (RouteNo == "" || RouteNo == null) {
        alert("Please enter route number");
        return false;
    }
    //if (status == "0" || status == null || status == "") {
    //    alert("Please select state status");
    //    return false;
    //}
    

    return true;
}
function SaveRoute() {
    if (fnRouteValidate()) {
        var sourceStation = $('select[id*=ddlSource]').val();
        var desState = $('select[id*=ddlDesState]').val();
        var destinationCity = $('select[id*=ddlDestination]').val();
        var totalDistance = $('input[id*=txtTotalDistance]').val().trim();
        var homeDistance = $('input[id*=txtHomeDistance]').val().trim();
        var vacancies = $('input[id*=txtVacancies]').val().trim();
        var routeNo = $('input[id*=txtRouteNo]').val().trim();

        var status = "Active" //$('select[id*=ddlStatus]').val();

        var viacity = new Array();
        var obj = new Object();
        //obj.ROUTE_ID = $('input[id*=txtRouteId]').val();

        obj.ST_FROM = sourceStation;
        obj.ST_TO = destinationCity;
        obj.TOTAL_DISTANCE = totalDistance;
        obj.DISTANCE_IN_HOME_STATE = homeDistance;
        obj.NO_OF_VACANCIES = vacancies;
        obj.ROUTE_NUMBER = routeNo;

        obj.STATUS = status;
        obj.ROUTE_MSTR_LDR = new Array();
        var condition = true;
        var length = 0;
        obj.ROUTE_MSTR_LDR.push({ VIA_CITY: $('select[id*=ddlSource]').find('option:selected').text(), SEQUENCE: 1 });
                $('#tdbViaRoute tr').each(function () {
                    var Childid = $(this).attr('id').split("_");
                    var Childid1 = parseInt(Childid[1]) + 1;
                    length = Childid1 +1;
                 
                    if ($('#txtCity_1').val().trim() == "") {
                        condition = false;
                        alert("Please enter via city name");
                        return false;

                    }

                    else
                    {
                        if ($('#txtCity_' + Childid[1]).val().trim() != "")
                        {
                            var city = $('#txtCity_' + Childid[1]).val().trim();
                            obj.ROUTE_MSTR_LDR.push({ VIA_CITY: city, SEQUENCE: Childid1});
                        }
                    }

                });

                obj.ROUTE_MSTR_LDR.push({ VIA_CITY: $('select[id*=ddlDestination]').find('option:selected').text(), SEQUENCE: length });

           

    
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
        var obj = new Object();

        var data = JSON.parse($('input[id*=txtroutjson]').val());
        rowNumber = 1;
        $('select[id*=ddlSource]').val(data[0].ST_FROM).change();
        $('select[id*=ddlDesState]').val(data[0].STATE_CODE).change();
        $('input[id*=txtCityCode]').val(data[0].ST_TO);
        $('input[id*=txtTotalDistance]').val(data[0].TOTAL_DISTANCE);
        $('input[id*=txtHomeDistance]').val(data[0].DISTANCE_IN_HOME_STATE);
        $('input[id*=txtVacancies]').val(data[0].NO_OF_VACANCIES);
        $('input[id*=txtRouteNo]').val(data[0].ROUTE_NUMBER);
        //$('select[id*=ddlStatus]').val(data[0].STATUS).change();
        operCount = data[0].ROUTE_MSTR_LDR.length;
        var incval = 1;
        for (i = 1; i < data[0].ROUTE_MSTR_LDR.length -1; i++)
        {

           if (data[0].ROUTE_MSTR_LDR[i].SEQUENCE == "2")
            {
                $('#txtCity_1').val(data[0].ROUTE_MSTR_LDR[i].VIA_CITY);

                
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
                add1 += '<button type="button" class="btn green" onclick="javascript:AddRow();" ><i class="fa fa-plus"></i></button> <button type="button" class="btn red" onclick="javascript:RemoveRow('+ incval + ');" ><i class="fa fa-minus"></i></button>';
                add1 += '</td>';
                add1 += '</tr>';

                $("#tdbViaRoute").append(add1);
                $('#txtCity_' + incval).val(data[0].ROUTE_MSTR_LDR[i].VIA_CITY);

            }

        }

       




    });
}