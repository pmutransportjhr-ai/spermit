/// <reference path="RTA-PERMIT-APPLICATION-VERIFY.js" />
function fnSTACSUploadValidate() {
    if ($('input[id*=txtExistRegNo]').val().trim() == null || $('input[id*=txtExistRegNo]').val().trim() == "") {
        alert("Please enter Registration Number");
        return false;
    }
    if ($('input[id*=txtExistPermitNo]').val().trim() == null || $('input[id*=txtExistPermitNo]').val().trim() == "") {
        alert("Please enter Permit Number");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;
    
}
function fnMobileValidate() {
    if ($('input[id*=txtMobileNumber]').val().trim() == null || $('input[id*=txtMobileNumber]').val().trim() == "") {
        alert("Please enter mobile number");
        return false;
    }
    if (!validateMob($('input[id*=txtMobileNumber]').val().trim())) {
        alert("Invalid Mobile Number");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;

}
function fnOTPValidate() {
    if ($('input[id*=txtOTP]').val().trim() == null || $('input[id*=txtOTP]').val().trim() == "") {
        alert("Please enter OTP");
        return false;
    }

    $('#loadingmessage').modal('show');
    return true;

}

function fnCSApplicationValidate() {

    
    
    var CS = $('input[id*=uploadCS]').val();
    if (CS == "") {
        alert("Please upload  Counter Signature Certificate");
        return false;
    }
    if (CS != "") {

        var fileUploadName = $('input[id*=uploadCS]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadCS]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadCS]').val('');

            alert("Plesae upload only PDF file for Counter Signature Certificate");

            return false;

        }

        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadCS]').val('');
            alert("Counter Signature Certificate file must be greater than 0 MB and less than 2 MB");

            return false;

        }


    }
    var timeTable = $('input[id*=uploadApprovedTT]').val();
    if (timeTable == "") {
        alert("Please upload approved time table copy. ");
        return false;
    }
    if (timeTable != "") {

        var fileUploadName = $('input[id*=uploadApprovedTT]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadApprovedTT]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadApprovedTT]').val('');
            alert("Please upload only PDF file for approved time table.");
            return false;

        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadApprovedTT]').val('');
            alert("Approved Time Table file must be greater than 0 MB and less than 2 MB");
            return false;
        }
    }

    var RC = $('input[id*=uploadRC]').val();
    if (RC == "") {
        alert("Please upload Vehicle Registration Certificate");
        return false;
    }
    if (RC != "") {
        var fileUploadName = $('input[id*=uploadRC]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadRC]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadRC]').val('');
            alert("Plesae upload only PDF file for Vehicle Registration Certificate");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadRC]').val('');
            alert("Vehicle Registration Certificate file must be greater than 0 MB and less than 2 MB");
            return false;
        }
    }

    var SpeedGovrn = $('input[id*=uploadSpeed]').val();
    if (SpeedGovrn == "") {
        alert("Please upload Speed Governor Certificate.");
        return false;
    }
    if (SpeedGovrn != "") {
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
    var InsuranceCertificate = $('input[id*=uploadInsur]').val();
    if (InsuranceCertificate == "") {
        alert("Please upload Insurance Certificate");
        return false;
    }
    if (InsuranceCertificate != "") {
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
    var PollutionCertificate = $('input[id*=uploadPollution]').val();
    if (PollutionCertificate == "") {
        alert("Please upload Pollution Certificate");
        return false;
    }
    if (PollutionCertificate != "") {
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
    var FitnessCertificate = $('input[id*=uploadFitness]').val();
    if (FitnessCertificate == "") {
        alert("Please upload Fitness Certificate");
        return false;
    }
    if (FitnessCertificate != "") {
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
    var PermitCertificate = $('input[id*=uploadPermit]').val();
    if (PermitCertificate == "") {
        alert("Please upload Permit Certificate");
        return false;
    }
    if (PermitCertificate != "") {
        var fileUploadName = $('input[id*=uploadPermit]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadPermit]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadPermit]').val('');
            alert("Plesae upload only PDF file for Permit Certificate");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadPermit]').val('');
            alert("Permit Certificate file must be greater than 0 MB and less than 2 MB");
            return false;
        }
    }
    var TemporaryTimeCertificate = $('input[id*=uploadTempoTT]').val();
    if (TemporaryTimeCertificate == "") {
        alert("Please upload Temporary Time Table Copy");
        return false;
    }
    if (TemporaryTimeCertificate != "") {
        var fileUploadName = $('input[id*=uploadTempoTT]')[0].files[0].name;
        var filesize = parseInt($('input[id*=uploadTempoTT]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=uploadTempoTT]').val('');
            alert("Plesae upload only PDF file for Temporary Time Table Copy");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=uploadTempoTT]').val('');
            alert("Temporary Time Table Copy must be greater than 0 MB and less than 2 MB");
            return false;
        }
    }

    var jhTaxPaidUptoCopy = $('input[id*=jhTaxPaidUptoCopy]').val();
    if (jhTaxPaidUptoCopy == "") {
        alert("Please upload Copy Jharkhan Tax Paid Upto Certificate");
        return false;
    }
    if (jhTaxPaidUptoCopy != "") {
        var fileUploadName = $('input[id*=jhTaxPaidUptoCopy]')[0].files[0].name;
        var filesize = parseInt($('input[id*=jhTaxPaidUptoCopy]')[0].files[0].size / 1024);
        var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);
        if (fileextension.toString().toUpperCase() != ".PDF") {
            $('input[id*=jhTaxPaidUptoCopy]').val('');
            alert("Plesae upload only PDF file for Jharkhan Tax Paid Upto Certificate");
            return false;
        }
        if (parseInt(filesize) <= 0 || parseInt(filesize) > 2048) {
            $('input[id*=jhTaxPaidUptoCopy]').val('');
            alert("Jharkhan Tax Paid Upto Certificate Copy must be greater than 0 MB and less than 2 MB");
            return false;
        }
    }



    var txtICValidUpto = $('input[id*=txtICValidUpto').val();
    var txtPCValidUpto = $('input[id*=txtPCValidUpto').val();
    var txtFCValidUpto = $('input[id*=txtFCValidUpto').val();
    var txtJharkhandTaxToken = $('input[id*=txtJharkhandTaxToken').val();
  
    if (txtICValidUpto == '') { alert('Please select Insurance Certificate Vaid Upto Date. '); return false;}
    if (txtPCValidUpto == '') { alert('Please select Pollution Certificate Vaid Upto Date. '); return false; }
    if (txtFCValidUpto == '') { alert('Please select Fitness Certificate Vaid Upto Date. '); return false; }
    if (txtJharkhandTaxToken == '') { alert('Please select Jharkhand Tax Token Vaid Upto Date. '); return false; }
  
    $('#loadingmessage').modal('show');
    return true;
}

function fnOnBlur() {
    $('#ContentPlaceHolder1_txtExistRegNo').on('blur', function () {
        var registrationNumber = $('#ContentPlaceHolder1_txtExistRegNo').val().trim();
        
        
        $.ajax({
            type: "POST",
            url: "api/GetPermitNo.ashx?caller=GetSTAPermitNumber&q=" + registrationNumber,
            data: {},
            cache: false,
            async: false,
            success: function (data) {
        
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "SUCCESS") {
                        $('#ContentPlaceHolder1_txtExistPermitNo').val(statusf1[1]);
                    }
                }
                else {
                    alert('No Data Found');
                    
                }
            },
            error: function (response) {

                alert(response.status + ' ' + response.statusText);
            }
        });
    });
}
