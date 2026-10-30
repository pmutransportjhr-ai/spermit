function fnRTAGCRenewalValidate() {
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
function fnRGCPApplicationValidate() {

    var oldRegnumber = $('span[id*=lblExistRegNumber]').text();
    var newRegnumber = $('span[id*=VORegNumber]').text();
    if (oldRegnumber != newRegnumber) {
        alert("Registration Number is diffrent. You can not Renew the Permit");
        return false;
    }
    if ($('span[id*=lblRenewalPermitFees]').text() == null || $('span[id*=lblRenewalPermitFees]').text() == "") {
        alert("Renewal of Permit Fees is not defined. So you can not apply for Renewal of Permit right now");
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
    if (RC != "") {
        var fsize2 = $('input[id*=uploadRC]')[0].files[0].size;
        if (fsize2 > 2097152) {
            alert("Please upload copy of RC maximum 2 MB file.");
            return false;
        }

    }
    var Insurance = $('input[id*=uploadInsur]').val();
    if (Insurance != "") {
        var fsize2 = $('input[id*=uploadInsur]')[0].files[0].size;
        if (fsize2 > 2097152) {
            alert("Please upload copy of Insurance maximum 2 MB file.");
            return false;
        }
    }
    var Pollution = $('input[id*=uploadPollution]').val();
    if (Pollution != "") {
        var fsize2 = $('input[id*=uploadPollution]')[0].files[0].size;
        if (fsize2 > 2097152) {
            alert("Please upload copy of Pollution maximum 2 MB file.");
            return false;
        }
    }
    var Fitness = $('input[id*=uploadFitness]').val();
    if (Fitness != "") {

        var fsize2 = $('input[id*=uploadFitness]')[0].files[0].size;
        if (fsize2 > 2097152) {
            alert("Please upload copy of Fitness maximum 2 MB file.");
            return false;
        }


    }
    var permit = $('input[id*=uploadPermit]').val();
    if (permit == "")
    {

        alert("Please upload copy of Permit.");
            return false;
       


    }
    if (permit != "") {

        var fsize2 = $('input[id*=uploadPermit]')[0].files[0].size;
        if (fsize2 > 2097152) {
            alert("Please upload copy of Permit maximum 2 MB file.");
            return false;
        }


    }
    return true;
}


$('input[type=file]').on('change', function () {
    var file = this.files[0];
    fileUploadName = file.name;
    var filesize = parseInt(this.files[0].size / 1024);

    var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
    if (fileextension.toString().toUpperCase != ".PDF") {
        $(this).val('');

        alert("Plesae Upload Only PDF File");

        return false;

    }
    else if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
        $(this).val('');
        alert("Size of file must be greater than 0 MB and less than 2 MB");

        return false;

    }
});