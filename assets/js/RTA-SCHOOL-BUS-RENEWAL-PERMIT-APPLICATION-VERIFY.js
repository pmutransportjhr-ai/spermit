function fnViewRenewal(ID) {
    window.open("RTA-SCHOOL-BUS-PERMIT-RENEWAL-APPLICATION-VIEW.aspx?q=" + encodeURIComponent(ID), "_blank");
    
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
        url: "api/RTA_PERMIT.ashx?caller=GetRegDetailsSchoolBus&q=" + encodeURIComponent(ID),
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
