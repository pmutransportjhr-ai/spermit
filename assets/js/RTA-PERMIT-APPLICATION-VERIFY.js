function fnView(ID) {
    window.open("RTA-PERMIT-APPLICATION-VIEW.aspx?q=" + encodeURIComponent(ID),"_blank")
}

function fnCloseComments() {
    $("#RegComments").modal('hide');
}
function fnRegComments(Comments)
{
    $('p[id*=Commentsdetail]').html(Comments);
    $("#RegComments").modal();
}

function fnRegDetail(ID) {
    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/RTA_PERMIT.ashx?caller=GetRegDetails&q=" + encodeURIComponent(ID),
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
function fnPermitStatusList() {

    $('#loadingmessage').modal('show');
    return true;
}

function fnApproveAction(key, AST_VERIFY_COMMENTS, SO_VERIFY_COMMENTS, JTC_VERIFY_COMMENTS)
{
    $('select[id*=ddlApproverAction]').val(0);
    $('textarea[id*=txtApproverComments]').val("");

    $('label[id*=lblAstComments]').text(AST_VERIFY_COMMENTS);
    $('label[id*=lblSOComments]').text(SO_VERIFY_COMMENTS);
    $('label[id*=lblJTCComments]').text(JTC_VERIFY_COMMENTS);

    $('input[id*=txtDelKey]').val(key);
    $("#delete").modal();
}
function fnPermitAction()
{
    
    var status = $('select[id*=ddlPermitAction]').val();
    if (status == "Published")
    {
        $('div[id*=DivActionPublish]').css("display", "block");
        $('div[id*=DivActionReject]').css("display", "none");
        ComponentsDateTimePickers.init();
    }
    else if (status == "Rejected" || status == "Verified") {
        $('div[id*=DivActionReject]').css("display", "block");
        $('div[id*=DivActionPublish]').css("display", "none");

    }
    else
    {
        $('div[id*=DivActionPublish]').css("display", "none");
        $('div[id*=DivActionReject]').css("display", "none");
    }

}
function fnAction(key, status, USER_LABEL, AST_VERIFY_COMMENTS, SO_VERIFY_COMMENTS, JTC_VERIFY_COMMENTS) {
    $('select[id*=ddlPermitAction]').val(0);
    $('input[id*=txtObjectionDt]').val("");
    $('textarea[id*=txtCommentsReject]').val("");

    if (USER_LABEL == "ASSISTANT") {
        $('select[id*=ddlPermitAction]').find('option[value=Published]').remove();
    }



    if (USER_LABEL == "SO") {
        $('label[id*=lblAstComments]').text(AST_VERIFY_COMMENTS);
        $('div[id*=dvAstComments]').show();

        $('select[id*=ddlPermitAction]').find('option[value=Verified]').remove();

    }
    if (USER_LABEL == "RTA") {
        $('label[id*=lblAstComments]').text(AST_VERIFY_COMMENTS);
        $('div[id*=dvAstComments]').show();

        $('label[id*=lblSOComments]').text(SO_VERIFY_COMMENTS);
        $('div[id*=dvSOComments]').show();

        $('select[id*=ddlPermitAction]').find('option[value=Published]').remove();

    }

    //if (USER_LABEL == "SO") {
    //    $('label[id*=lblAstComments]').text(AST_VERIFY_COMMENTS);
    //    $('div[id*=dvAstComments]').show();

    //}
    //if (USER_LABEL == "RTA") {
    //    $('label[id*=lblAstComments]').text(AST_VERIFY_COMMENTS);
    //    $('div[id*=dvAstComments]').show();

    //    $('label[id*=lblSOComments]').text(SO_VERIFY_COMMENTS);
    //    $('div[id*=dvSOComments]').show();


    //}

    $('input[id*=txtDelKey]').val(key);
    $('input[id*=txtPermitStatus]').val(status);
    $("#delete").modal();
}
function fnPermissionAction(key) {
    $('textarea[id*=txtPermissionComments]').val("");
    $('input[id*=HFAPPID1]').val(key);
    $("#permission").modal();
}
function fnCSAction(key) {
    $('select[id*=ddlCSAction]').val(0);
    $('textarea[id*=txtCSComments]').val("");
    $('input[id*=HFCSId]').val(key);
    $("#CSAction").modal();
}
function fnPermissionValidateAction() {
    var Commentspermission = $('textarea[id*=txtPermissionComments]').val();
        if (Commentspermission == "" || Commentspermission == null) {
            alert("Please Enter Comments");
            return false;
        }
   
    $('#loadingmessage').modal('show');
    return true;
}

function fnValidateAction()
{
    var status = $('input[id*=txtPermitStatus]').val();
    var Finalstatus = $('select[id*=ddlPermitAction]').val();
    if (Finalstatus == "" || Finalstatus == "0" || Finalstatus == null) {
        alert("Please select action");
        return false;
    }
    if (Finalstatus=="Published")
    {
        var ObjectDate = $('input[id*=txtObjectionDt]').val();
        if (ObjectDate == "" || ObjectDate == null)
        {
            alert("Please enter objection till date");
            return false;
        }
    }
    
    if (Finalstatus == "Rejected" || Finalstatus == "Verified")

    {
        var CommentsReject = $('textarea[id*=txtCommentsReject]').val();
        if (CommentsReject == "" || CommentsReject == null) {
            alert("Please Enter Comments");
            return false;
        }
    }
    $('#loadingmessage').modal('show');
    return true;
}
function fnCSValidateAction() {
    var Finalstatus = $('select[id*=ddlCSAction]').val();
    if (Finalstatus == "" || Finalstatus == "0" || Finalstatus == null) {
        alert("Please select action");
        return false;
    }


    var Comments = $('textarea[id*=txtCSComments]').val();
    if (Comments == "" || Comments == null) {
        alert("Please Enter Comments");
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;
}
function fnApproverValidateAction() {
    var Finalstatus = $('select[id*=ddlApproverAction]').val();
    if (Finalstatus == "" || Finalstatus == "0" || Finalstatus == null) {
        alert("Please select action");
        return false;
    }
  

        var CommentsReject = $('textarea[id*=txtApproverComments]').val();
        if (CommentsReject == "" || CommentsReject == null)
        {
            alert("Please Enter Comments");
            return false;
       }
    $('#loadingmessage').modal('show');
    return true;
}


function fnSaveRTAApprove() {


    if (fnValidateRTAMeeting()) {
        var data = "A~";
        $('.checkRTAMeetingApproved').each(function () {
            if ($(this).prop('checked') == true) {
                data += $(this).closest('tr').find('td:eq(0)').attr('id') + "~";

            }
        });
        data += 'R~';
        $('.checkRTAMeetingRejected').each(function () {
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
        url: "api/RTA_PERMIT.ashx?caller=GetRTARejMeetingMarked",
        data: { PermitType: PermitType },
        cache: false,
        async: false,
        success: function (data) {
            $('#loadingmessage').modal('hide');
            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {
                    $('#rtaRejmeetingTblBody').html('');
                    $('#rtaRejmeetingTblBody').append(data.split(/~(.+)?/)[1].toString());
                    $('#RTAMarkedRejected').modal('show');

                }
                else {
                    alert('No Data Found');
                    $('#rtaRejmeetingTblBody').html('');
                }

            }
            else {
                alert('No Data Found');
                $('#rtaRejmeetingTblBody').html('');

            }

        },
        error: function (response) {
            $('div[id*=rtameetingTblBody]').html("");
            $('#RTAMeetingModal').modal('hide');
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnMarkedView() {

    $('#loadingmessage').modal('show');
    $.ajax({
        type: "POST",
        url: "api/RTA_PERMIT.ashx?caller=GetRTAMeetingMarked",
        data: "",
        cache: false,
        async: false,
        success: function (data) {

            $('#loadingmessage').modal('hide');

            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {

                    $('#RTAAppRej').html('');


                    $('#RTAAppRej').append(statusf1[1].toString());

                    $('#RTAApproveReject').modal('show');

                }

                else {
                    alert('No Data Found');
                    $('#RTAAppRej').html('');
                    $('#RTAApproveReject').modal('hide');
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
function checkRTAMeetingApproved(appid) {

    if ($("td[id='" + appid + "']").closest('tr').find('.checkRTAMeetingApproved').prop('checked') == true) {
        $("td[id='" + appid + "']").closest('tr').find('.checkRTAMeetingRejected').attr('checked', false);
        $("td[id='" + appid + "']").closest('tr').find('.checkApplicationHold').attr('checked', false);

    }



}
function checkRTAMeetingRejected(appid) {

    if ($("td[id='" + appid + "']").closest('tr').find('.checkRTAMeetingRejected').prop('checked') == true) {
        $("td[id='" + appid + "']").closest('tr').find('.checkRTAMeetingApproved').attr('checked', false);
        $("td[id='" + appid + "']").closest('tr').find('.checkApplicationHold').attr('checked', false);
    }

}
function checkRTAMeetingHold(appid) {

    if ($("td[id='" + appid + "']").closest('tr').find('.checkApplicationHold').prop('checked') == true) {
        $("td[id='" + appid + "']").closest('tr').find('.checkRTAMeetingRejected').attr('checked', false);
        $("td[id='" + appid + "']").closest('tr').find('.checkRTAMeetingApproved').attr('checked', false);
    }

}
function fnValidateRTAMeeting() {


    var Meetingcount = 0;
    $('.checkRTAMeetingApproved').each(function () {
        if ($(this).prop('checked') == true) {

            Meetingcount += 1;
        }
    });
    $('.checkRTAMeetingRejected').each(function () {
        if ($(this).prop('checked') == true) {

            Meetingcount += 1;
        }
    });

    if (Meetingcount == 0) { alert('Please select at least one Approve/Reject Application or Close'); return false; }

    var fileuploadvalue = $('input[id*=momFileUpload]').val();
    var txtComments = $('textarea[id*=txtFinalComments]').val();
    if (fileuploadvalue == '' || fileuploadvalue == null) {
        alert("Please upload MOM of RTA Meeting");
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
function fnObjectionModalView(regNo) {


    $.ajax({
        type: "POST",
        url: "api/RTA_PERMIT.ashx?caller=GetObjectionDetailbyReg&q=" + encodeURIComponent(regNo),
        data: "",
        cache: false,
        async: false,
        success: function (data) {
            if (data != "No Data Found") {
                var statusf1 = data.split('~');
                if (statusf1[0] == "Success") {

                    $('#rtameetingObjetionTblBody').html('');
                    $('#rtameetingObjetionTblBody').append(data.split(/~(.+)?/)[1].toString());
                    $('#ObjectioModal').modal('show');
                }
                else {
                    alert('No Data Found');
                    $('#rtameetingObjetionTblBody').html('');
                    $('#ObjectioModal').modal('hide');
                }
            }
            else {
                alert('No Data Found');
                $('#rtameetingObjetionTblBody').html('');
                $('#ObjectioModal').modal('hide');
            }

        },
        error: function (response) {
            $('#rtameetingObjetionTblBody').html('');
            $('#ObjectioModal').modal('hide');
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function fnSaveRTAMeetingAction() {

    var data = 'A~';
    var Meetingcount = 0;
    var rejectCount = 0;
    $('.checkRTAMeetingRejected').each(function () {
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

    $('.checkRTAMeetingApproved').each(function () {
        if ($(this).prop('checked') == true) {
            data += $(this).closest('tr').find('td:eq(0)').attr('id') + "~";
            Meetingcount += 1;
        }
    });
    data += 'R~';
    $('.checkRTAMeetingRejected').each(function () {
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
    $('#rtaRejmeetingTblBody').html('');
    $('#rtaAppRej').html('');
    $('textarea[id*=txtRejectionComments]').val('');
    $('input[type=file]').val('');
    $('textarea[id*=txtFinalComments]').val('');
}

$(document).ready(function () { clearcheckbox(); });