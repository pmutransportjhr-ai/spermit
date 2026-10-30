function fnEdit(q)
{
    window.location = "RTA-OFFICE-NOTICE-MASTER-NEW.aspx?q="+q;
}
function fnNoticeValidate()
{
    var NOticeDetails = $('textarea[id*=txtNoticeDetails]').val().trim();
    var Start_Date = $('input[id*=txtstdt]').val();
    var End_Date = $('input[id*=txtenddt]').val();
    if (NOticeDetails == "" || NOticeDetails == null) {
        alert("Please enter Notice Details");
        return false;
    }
    if (NOticeDetails.length<2) {
        alert("Please enter at least two characters in the notice details field");
        return false;
    }
    if (Start_Date == null || Start_Date == "") {
        alert("Please Enter Start Date");
        return false;
    }
    if (End_Date == null || End_Date == "") {
        alert("Please Enter End Date");
        return false;
    }
    var FileUpload = $('input[type=file]').val();
    if ($('div[id*=dvlinkdownload]').html() == "" || $('div[id*=dvlinkdownload]').html() == undefined) {
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

    else if (parseInt(filesize) <= 0 || parseInt(filesize) > 25600) {
        $(this).val('');
        alert("Size of file must be greater than 0 MB and less than 25 MB");

        return false;

    }
});
function fnDelete(key)
{
    $('input[id*=txtDelKey]').val(key);
    $("#delete").modal();
}