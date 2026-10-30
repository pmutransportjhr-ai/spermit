function fnEdit(ID) {
    window.location.href = "RTA-FEE-MASTER-NEW.aspx?q=" + encodeURIComponent(ID)
}

function fnDelete(key) {
    $('input[id*=txtDelKey]').val(key);
    $("#delete").modal();
}
function fnFeeValidate() {
    var feefor = $('select[id*=ddlFeeFor]').val();
    var feeOption = $('select[id*=ddlFeeOption]').val();
    var feeService = $('select[id*=ddlFeeService]').val();
    var feeType = $('select[id*=ddlFeeType]').val();
    var fee = $('input[id*=txtFee]').val();
    var status = $('select[id*=ddlStatus]').val();
    var FileUpload = $('input[type=file]').val();

    
    if (feefor == "0" || feefor == null || feefor == "") {
        alert("Please select Fee for");
        return false;
    }
    if (feeOption == "0" || feeOption == null || feeOption == "") {
        alert("Please select Fee option");
        return false;
    }
    if (feeService == "0" || feeService == null || feeService == "") {
        alert("Please select Fee service");
        return false;
    }
    if (feeType == "0" || feeType == null || feeType == "") {
        alert("Please select Fee type");
        return false;
    }
    if (fee == null || fee == "") {
        alert("Please Enter Fee/Tax");
        return false;
    }
    if (fee <= parseInt(0)) {
        alert("Please enter valid Fee/Tax");
      
        return false;
    }
    //if (status == "0" || status == null || status == "") {
    //    alert("Please select status");
    //    return false;
    //}
    if ($('input[id*=txtUploadFileEdit]').val() == "") {
        if (FileUpload == "") {
            alert("Please select File to upload");
            return false;
        }
    }


    return true;
}

$('input[type=file]').on('change', function () {
    var file = this.files[0];
    fileUploadName = file.name;
    var filesize = parseInt(this.files[0].size / 1024);

    var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length).toString().toLowerCase();
    if (fileextension != ".pdf") {
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
