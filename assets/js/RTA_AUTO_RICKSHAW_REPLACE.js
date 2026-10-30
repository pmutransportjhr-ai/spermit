function fnSTAReplacementValidate() {
    if ($('input[id*=txtExistRegNo]').val() == null || $('input[id*=txtExistRegNo]').val() == "") {
        alert("Please enter Registration Number");
        return false;
    }
    if ($('input[id*=txtExistPermitNo]').val() == null || $('input[id*=txtExistPermitNo]').val() == "") {
        alert("Please enter Permit Number");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;
    
}

function fnSearchValidate() {
    if ($('input[id*=txtVehicleNumber]').val() == null || $('input[id*=txtVehicleNumber]').val() == "") {
        alert("Please enter Registration Number");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;

}
function fnRVApplicationValidate() {

    var oldOwnername = $('span[id*=lblExistName]').text();
    var newOwnername = $('span[id*=VOName]').text();
   
    //if (oldOwnername != newOwnername) {
    //    alert("Owner name is diffrent. You can not replace the vehicle");
    //    return false;
    //}
    if ($('span[id*=lblReplacevehicleFees]').text() == null || $('span[id*=lblReplacevehicleFees]').text() == "") {
        alert("Replacement of Vehicle Fees is not defined. So you can not apply for Replacement of Vehicle right now");
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
    var seating_capacity = $('input[id*=txtSeatingCapacity]').val().trim();
    if (seating_capacity == "" || seating_capacity == null) {
        alert("Please enter seating capacity of vehicle");
        return false;
    }

    var agreement_with = $('input[id*=txtAgreementWith]').val().trim();
    if (agreement_with == "" || agreement_with == null) {
        alert("Please enter vehicle agreement details");
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

    var permit = $('input[id*=uploadPermit]').val();
    if (permit == "") {

        alert("Please upload Permit file");
        return false;



    }
    if (permit != "") {

        var fileUploadName = $('input[id*=uploadPermit]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadPermit]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadPermit]').val('');

            alert("Plesae upload only PDF file for Permit");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadPermit]').val('');
            alert("Permit file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    var AgreementCopy = $('input[id*=uploadAgreement]').val();
    if (AgreementCopy == "") {

        alert("Please upload Agreement.");
        return false;



    }
    if (AgreementCopy != "") {

        var fileUploadName = $('input[id*=uploadAgreement]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadAgreement]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadAgreement]').val('');

            alert("Plesae upload only PDF file for Agreement");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadAgreement]').val('');
            alert("Agreement file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    return true;
}

