function fnViewRenewal(ID) {
    window.open("RTA-COUNTER-SIGNATURE-RENEWAL-VIEW.aspx?q=" + encodeURIComponent(ID),"_blank")
}

function fnRenewalStatusList() {

    $('#loadingmessage').modal('show');
    return true;
}
function fnRegComments(Comments) {
    $('p[id*=Commentsdetail]').html(Comments);
    $("#RegComments").modal();
}

function fnRegDetail(ID) {
    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/RTA_PERMIT.ashx?caller=GetCSRegDetails&q=" + encodeURIComponent(ID),
        data: "",
        cache: false,
        async: false,
        success: function (data) {
            $('#loadingmessage').modal('hide');
            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {


                    $('div[id*=DivHistory]').html(data.split(/~(.+)?/)[1].toString());
                    $("#RegDetail").modal();

                }
                else {
                    $('div[id*=DivHistory]').html("");
                    $("#RegDetail").modal();
                }

            }

        },
        error: function (response) {
            $('#loadingmessage').modal('hide');
            alert(response.status + ' ' + response.statusText);
        }
    });
}

function fnApproverValidateAction() {
    var Finalstatus = $('select[id*=ddlApproverAction]').val();
    if (Finalstatus == "" || Finalstatus == "0" || Finalstatus == null) {
        alert("Please select action");
        return false;
    }


    var CommentsReject = $('textarea[id*=txtApproverComments]').val();
    if (CommentsReject == "" || CommentsReject == null) {
        alert("Please Enter Comments");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;
}

function fnValidateAction() {
    var Finalstatus = $('select[id*=ddlRenewalAction]').val();
    if (Finalstatus == "" || Finalstatus == "0" || Finalstatus == null) {
        alert("Please select action");
        return false;
    }
    //if (Finalstatus == "Rejected") {
    var CommentsReject = $('textarea[id*=txtCommentsReject]').val();
        if (CommentsReject == "" || CommentsReject == null) {
            alert("Please Enter Comments");
            return false;
        }
    //}
    $('#loadingmessage').modal('show');
    return true;
}



function fnAction(key, status, USER_LABEL, AST_VERIFY_COMMENTS, SO_VERIFY_COMMENTS, RTC_VERIFY_COMMENTS) {
    $('select[id*=ddlPermitAction]').val(0);

    $('textarea[id*=txtCommentsReject]').val("");
    $('input[id*=txtDelKey]').val(key);

    if (USER_LABEL == "SO") {
        $('label[id*=lblAstComments]').text(AST_VERIFY_COMMENTS);
        $('div[id*=dvAstComments]').show();

    }
    if (USER_LABEL == "RTA") {
        $('label[id*=lblAstComments]').text(AST_VERIFY_COMMENTS);
        $('div[id*=dvAstComments]').show();

        $('label[id*=lblSOComments]').text(SO_VERIFY_COMMENTS);
        $('div[id*=dvSOComments]').show();


    }

    $('input[id*=txtPermitStatus]').val(status);
    $("#delete").modal();
}


function fnApproveAction(key, AST_VERIFY_COMMENTS, SO_VERIFY_COMMENTS, RTA_VERIFY_COMMENTS) {
    $('select[id*=ddlApproverAction]').val(0);

    $('label[id*=lblAstComments]').text(AST_VERIFY_COMMENTS);
    $('label[id*=lblSOComments]').text(SO_VERIFY_COMMENTS);
    $('label[id*=lblRTAComments]').text(RTA_VERIFY_COMMENTS);

    $('textarea[id*=txtApproverComments]').val("");
    $('input[id*=txtDelKey]').val(key);
    $("#delete").modal();
}

function fnSaveSTAApprove() {


    if (fnValidateSTAMeeting()) {
        var data = "A~";
        $('.checkSTAMeetingApproved').each(function () {
            if ($(this).prop('checked') == true) {
                data += $(this).closest('tr').find('td:eq(0)').attr('id') + "~";

            }
        });
        data += 'R~';
        $('.checkSTAMeetingRejected').each(function () {
            if ($(this).prop('checked') == true) {
                data += $(this).closest('tr').find('td:eq(0)').attr('id') + "~";

            }
        });
        $('input[id*=staData]').val('0');
        $('input[id*=staData]').val(data.substring(0, data.length - 1));
        $('#loadingmessage').modal('show');
    }
    else
        return false;

}
function fnRejMarkedView() {
    var PermitType = $('select[id*=ddlType]').val();

    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/RTA_PERMIT.ashx?caller=CSGetRTARejMeetingMarked_PermitRenewal",
        data: { PermitType: PermitType },
        cache: false,
        async: false,
        success: function (data) {
            $('#loadingmessage').modal('hide');
            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {
                    $('#staRejmeetingTblBody').html('');
                    $('#staRejmeetingTblBody').append(data.split(/~(.+)?/)[1].toString());
                    $('#JTCMarkedRejected').modal('show');

                }
                else {
                    alert('No Data Found');
                    $('#staRejmeetingTblBody').html('');
                }

            }
            else {
                alert('No Data Found');
                $('#staRejmeetingTblBody').html('');

            }

        },
        error: function (response) {
            $('div[id*=stameetingTblBody]').html("");
            $('#STAMeetingModal').modal('hide');
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnMarkedView() {

    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/RTA_PERMIT.ashx?caller=CSGetRTAMeetingMarked_PermitRenewal",
        data: "",
        cache: false,
        async: false,
        success: function (data) {

            $('#loadingmessage').modal('hide');

            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {

                    $('#jtcAppRej').html('');


                    $('#jtcAppRej').append(statusf1[1].toString());

                    $('#JTCApproveReject').modal('show');

                }

                else {
                    alert('No Data Found');
                    $('#jtcAppRej').html('');
                    $('#JTCApproveReject').modal('hide');
                }



            }
            else {
                alert('No Data Found');
                $('#jtcAppRej').html('');
                $('#JTCApproveReject').modal('hide');
            }

        },
        error: function (response) {
            $('#loadingmessage').modal('hide');
            $('div[id*=jtcAppRej]').html("");
            $('#JTCApproveReject').modal('hide');
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function checkSTAMeetingApproved(appid) {

    if ($("td[id='" + appid + "']").closest('tr').find('.checkSTAMeetingApproved').prop('checked') == true) {
        $("td[id='" + appid + "']").closest('tr').find('.checkSTAMeetingRejected').attr('checked', false);
        $("td[id='" + appid + "']").closest('tr').find('.checkApplicationHold').attr('checked', false);

    }



}
function checkSTAMeetingRejected(appid) {

    if ($("td[id='" + appid + "']").closest('tr').find('.checkSTAMeetingRejected').prop('checked') == true) {
        $("td[id='" + appid + "']").closest('tr').find('.checkSTAMeetingApproved').attr('checked', false);
        $("td[id='" + appid + "']").closest('tr').find('.checkApplicationHold').attr('checked', false);
    }

}
function checkSTAMeetingHold(appid) {

    if ($("td[id='" + appid + "']").closest('tr').find('.checkApplicationHold').prop('checked') == true) {
        $("td[id='" + appid + "']").closest('tr').find('.checkSTAMeetingRejected').attr('checked', false);
        $("td[id='" + appid + "']").closest('tr').find('.checkSTAMeetingApproved').attr('checked', false);
    }

}
function fnValidateSTAMeeting() {


    var Meetingcount = 0;
    $('.checkSTAMeetingApproved').each(function () {
        if ($(this).prop('checked') == true) {

            Meetingcount += 1;
        }
    });
    $('.checkSTAMeetingRejected').each(function () {
        if ($(this).prop('checked') == true) {

            Meetingcount += 1;
        }
    });

    if (Meetingcount == 0) { alert('Please select at least one Approve/Reject Application or Close'); return false; }

    var fileuploadvalue = $('input[id*=momFileUpload]').val();
    var txtComments = $('textarea[id*=txtFinalComments]').val();
    if (fileuploadvalue == '' || fileuploadvalue == null) {
        alert("Please upload Minutes of STA Meeting");
        return false;
    }
    var fileUploadName = $('input[id*=momFileUpload]')[0].files[0].name;

    var fileextension = fileUploadName.substring(fileUploadName.lastIndexOf('.'), fileUploadName.length);

    if (fileextension.toString().toUpperCase() != ".PDF") {
        $('input[id*=momFileUpload]').val('');

        alert("Plesae upload only PDF file");

        return false;

    }

    if (txtComments == '' || txtComments == null) {
        alert("Please enter comments");
        return false;
    }




    return true;


}

function fnSaveSTAMeetingAction() {

    var data = 'A~';
    var Meetingcount = 0;
    var rejectCount = 0;
    $('.checkSTAMeetingRejected').each(function () {
        if ($(this).prop('checked') == true) {
            rejectCount += 1;
            Meetingcount += 1;
        }
    });
    if (Number(rejectCount) > Number(0)) {
        $('#divrejectionComment').show();
    }
    else
        $('#divrejectionComment').hide();

    $('.checkSTAMeetingApproved').each(function () {
        if ($(this).prop('checked') == true) {
            data += $(this).closest('tr').find('td:eq(0)').attr('id') + "~";
            Meetingcount += 1;
        }
    });
    data += 'R~';
    $('.checkSTAMeetingRejected').each(function () {
        if ($(this).prop('checked') == true) {
            data += $(this).closest('tr').find('td:eq(0)').attr('id') + "~";

        }
    });

    if (Meetingcount == 0) { alert('Please select at least one Meeting/Reject Application or Close'); return false; }
    if (rejectCount > 0) {
        if (validateRejcetComments()) {
            $('input[id*=staData]').val('0');
            $('input[id*=staData]').val(data.substring(0, data.length - 1));
            return true;

        }
        else {
            return false;
        }
    }
    else {
        $('input[id*=staData]').val('0');
        $('input[id*=staData]').val(data.substring(0, data.length - 1));

    }
    $('#loadingmessage').modal('show');
    return true;
}
function validateRejcetComments() {

    var rejectionComment = $('textarea[id*=txtRejectionComments]').val();
    if (rejectionComment == '' || rejectionComment == null) {
        alert('Please Enter Rejection Comments');
        return false;

    }
    return true;

}

function clearcheckbox() {
    $('#staRejmeetingTblBody').html('');
    $('#jtcAppRej').html('');
    $('textarea[id*=txtRejectionComments]').val('');
    $('input[type=file]').val('');
    $('textarea[id*=txtFinalComments]').val('');
}

$(document).ready(function () { clearcheckbox(); });
