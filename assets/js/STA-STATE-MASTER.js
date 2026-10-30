function fnEdit(ID)
{
    window.location.href = "STA-STATE-MASTER-NEW.aspx?q=" + encodeURIComponent(ID)
}
function fnDelete(key) {
    $('input[id*=txtDelKey]').val(key);
   // $('#ctl00_ContentPlaceHolder1_txtDelKey').val(key);
    $("#delete").modal();
}
var operCount = 1;
function AddRow() {
    operCount++;
    var add = "";
    add += '<tr id="trVia_' + operCount + '">';
    add += '<td class="col-md-2"> Upto (in Kms)</td>';
    add += '<td class="col-md-3" id="tdUpto_' + operCount + '">';
    add += '<input type="text" maxlength="4"  class="number form-control" id="txtUpto_' + operCount + '"/>';
    add += '</td>';
    add += '<td class="col-md-2"> No. of Round Trips</td>';
    add += '<td class="col-md-3" id="tdTrip_' + operCount + '">';
    add += '<input type="text" maxlength="1"  class="number form-control" id="txtTrip_' + operCount + '"/>';
    add += '</td>';
    add += '<td class="col-md-2" id="tdoperation_' + operCount + '">';
    add += '<button type="button" class="btn green" onclick="javascript:AddRow();" ><i class="fa fa-plus"></i></button> <button type="button" class="btn red" onclick="javascript:RemoveRow(' + operCount + ');" ><i class="fa fa-minus"></i></button>';
    add += '</td>';
    add += '</tr>';

    $("#tdbViaRoute").append(add);




}
function RemoveRow(removeNum) {
    $('#trVia_' + removeNum).remove();
}
function fnStateValidate() {
    $('.form-control').css('border-color', '#c2cad8');
    var sateName = $('input[id*=txtStateName]').val().trim();
    var sateAbbr = $('input[id*=txtStateAbbr]').val().trim();
    var status = $('select[id*=ddlStatus]').val();
    if (sateName == "" || sateName == null) {
        alert("Please enter state name");
        $('input[id*=txtStateName]').css('border-color', 'red').focus();
        return false;
    }
    
    if (sateName.length < 2) {
        alert("Please enter at least two characters in the state name field");
        $('input[id*=txtStateName]').css('border-color', 'red').focus();
        return false;
    }
    if (sateAbbr == "" || sateAbbr == null) {
        alert("Please enter state abbreviation");
        $('input[id*=txtStateAbbr]').css('border-color', 'red').focus();
        return false;
    }
    if (sateAbbr.length < 2) {
        alert("Please enter two characters in the state abbreviation field");
        $('input[id*=txtStateAbbr]').css('border-color', 'red').focus();
        return false;
    }
    //if (status == "0" || status == null || status == "") {
    //    alert("Please select state status");
    //    return false;
    //}
    return true;
}
function SaveState() {
    if (fnStateValidate())
    {

        var obj = new Object();
      
        obj.STATE_MSTR_LDR = new Array();
        var condition = true;
        var arrInput = new Array();
        var arrInput1 = new Array();
        $('#tdbViaRoute tr').each(function () {
            var Childid = $(this).attr('id').split("_");
            arrInput.push($('#txtUpto_' + Childid[1]).val().trim());
            arrInput1.push($('#txtTrip_' + Childid[1]).val().trim());

            if ($('#txtUpto_' + Childid[1]).val().trim() == "") {
                condition = false;
                alert("Please enter Upto Km");
                return false;

            }
            if ($('#txtTrip_'+ Childid[1]).val().trim() == "") {
                condition = false;
                alert("Please enter Number of Round Trip");
                return false;

            }

            else {
                if ($('#txtUpto_' + Childid[1]).val().trim() != "" && $('#txtTrip_' + Childid[1]).val().trim() != "") {
                    var Upto = $('#txtUpto_' + Childid[1]).val().trim();
                    var Trip = $('#txtTrip_' + Childid[1]).val().trim();
                    obj.STATE_MSTR_LDR.push({ KM_UPTO: Upto, NUMBER_OF_ROUND_TRIP: Trip });
                }
            }

        });

       for (var i = 0; i < arrInput.length - 1; i++) {
            if (arrInput[i + 1] <= arrInput[i]) {
                alert("Please select KM in ascending order");
                condition = false;
                return false;

            }
        }
        for (var i = 0; i < arrInput1.length - 1; i++) {
            if (arrInput1[i + 1] >= arrInput1[i]) {
                alert("Please select Number of Round Trips in descending order");
                condition = false;
                return false;

            }
        }


        if (condition == false) {
            return false;

        }
        else {


            //viacity.push(obj);
            var xxx = JSON.stringify(obj);
            $('input[id*=txtStatejson]').val(xxx);
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

        var data = JSON.parse($('input[id*=txtStatejson]').val());
        rowNumber = 1;
         operCount = data[0].STATE_MSTR_LDR.length;
        var incval = 1;
        for (i = 0; i < data[0].STATE_MSTR_LDR.length; i++) {

            if (i==0) {
                $('#txtUpto_1').val(data[0].STATE_MSTR_LDR[0].KM_UPTO);
                $('#txtTrip_1').val(data[0].STATE_MSTR_LDR[i].NUMBER_OF_ROUND_TRIP);

            }
            else {
                incval++
                var add1 = "";


                add1 += '<tr id="trVia_' + incval + '">';
                add1 += '<td class="col-md-2"> Upto (in Kms)</td>';
                add1 += '<td class="col-md-3" id="tdUpto_' + incval + '">';
                add1 += '<input type="text" maxlength="4"  class="number form-control" id="txtUpto_' + incval + '"/>';
                add1 += '</td>';
                add1 += '<td class="col-md-2"> No. of Round Trips</td>';
                add1 += '<td class="col-md-3" id="tdTrip_' + incval + '">';
                add1 += '<input type="text" maxlength="1"  class="number form-control" id="txtTrip_' + incval + '"/>';
                add1 += '</td>';
                add1 += '<td class="col-md-2" id="tdoperation_' + incval + '">';
                add1 += '<button type="button" class="btn green" onclick="javascript:AddRow();" ><i class="fa fa-plus"></i></button> <button type="button" class="btn red" onclick="javascript:RemoveRow(' + incval + ');" ><i class="fa fa-minus"></i></button>';
                add1 += '</td>';
                add1 += '</tr>';

                $("#tdbViaRoute").append(add1);
                $('#txtUpto_' + incval).val(data[0].STATE_MSTR_LDR[i].KM_UPTO);
                $('#txtTrip_' + incval).val(data[0].STATE_MSTR_LDR[i].NUMBER_OF_ROUND_TRIP);

            }
        }
     






    });
}