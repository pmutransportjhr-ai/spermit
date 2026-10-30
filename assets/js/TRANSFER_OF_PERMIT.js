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
   
    if ($('span[id*=lblReplacevehicleFees]').text() == null || $('span[id*=lblReplacevehicleFees]').text() == "") {
        alert("Transfer of Permit Fees is not defined. So you can not apply for Replacement of Vehicle right now");
        return false;
    }
   
    var hv1 = $('input[id$=HiddenField1]').val();
    var hv2 = $('input[id$=HiddenField2]').val();
    if (hv1 == '3' || hv1 == '4' && hv2 == '21' || hv2 == '29') {
        if ($('input[id*=txtUnloadWeight]').val() == null || $('input[id*=txtUnloadWeight]').val() == "") {
            alert("Please enter Unload Weight");
            return false;
        }
        if ($('input[id*=txtGrossvehicleWeight]').val() == null || $('input[id*=txtGrossvehicleWeight]').val() == "") {
            alert("Please enter Gross Vehicle weight");
            return false;
        }
        if ($('input[id*=txtPayLoad]').val() == null || $('input[id*=txtPayLoad]').val() == "") {
            alert("Please enter Pay load");
            return false;
        }
        if ($('input[id*=txtAgreementWith]').val() == null || $('input[id*=txtAgreementWith]').val() == "") {
            alert("Please enter Hire purchaser /lease /hypothecation agreement with");
            return false;
        }
    }

    var Reason = $('input[id*=txtReason]').val();// $('input[id*=txtReason]').val().trim();
    if (Reason == "" || Reason == null) {
        alert("Please enter reason for permit transfer");
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

    if ($('input[type=checkbox]').is(':checked')) {
        var DateVal = $('input[id*=txtDate]').val().trim();
        if (DateVal == "" || DateVal == null) {
            alert("Please select deceased death date");
            return false;
        }
        var RelationVal = $('input[id*=txtRelation]').val().trim();
        if (RelationVal == "" || RelationVal == null) {
            alert("Please enter relation with deceased permit holder");
            return false;
        }



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
    if (permit == "")
    {

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
    var NoObjCertificate = $('input[id*=uploadNO]').val();
    if (NoObjCertificate == "") {

        alert("Please upload No Objection Certificate copy");
        return false;



    }
    if (NoObjCertificate != "") {

        var fileUploadName = $('input[id*=uploadNO]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadNO]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadNO]').val('');

            alert("Plesae upload only PDF file for No Objection");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadNO]').val('');
            alert("No Objection Certifcate copy must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    return true;
}
function fnApplyPermit() {

    
    if ($('input[type=checkbox]').is(':checked')) {
        
        $('input[id*=txtDate]').attr('readonly', false);
        $('input[id*=txtRelation]').attr('readonly', false);

    }
    else {
        $('input[id*=txtDate]').val('');
        $('input[id*=txtRelation]').val('');
        $('input[id*=txtDate]').attr('readonly', true);
        $('input[id*=txtRelation]').attr('readonly', true);
    }
}

