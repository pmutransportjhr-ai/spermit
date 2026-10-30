function fnObjectionStatusList() {

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
function fnView(ID) {
    window.open("RTA-PERMIT-OBJECTION-VIEW.aspx?q=" + encodeURIComponent(ID),"_blank")
}



function fnAction(key, status, USER_LABEL, AST_VERIFY_COMMENTS, SO_VERIFY_COMMENTS, JTC_VERIFY_COMMENTS) {
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

    $("#delete").modal();
}
function fnApproveAction(key, AST_VERIFY_COMMENTS, SO_VERIFY_COMMENTS, JTC_VERIFY_COMMENTS) {
    $('select[id*=ddlApproverAction]').val(0);
    $('textarea[id*=txtApproverComments]').val("");

    $('label[id*=lblAstComments]').text(AST_VERIFY_COMMENTS);
    $('label[id*=lblSOComments]').text(SO_VERIFY_COMMENTS);
    $('label[id*=lblJTCComments]').text(JTC_VERIFY_COMMENTS);

    $('input[id*=txtDelKey]').val(key);
    $("#delete").modal();
}
function fnValidateAction()
{
    var Finalstatus = $('select[id*=ddlObjectionAction]').val();
    if (Finalstatus == "" || Finalstatus == "0" || Finalstatus == null) {
        alert("Please select action");
        return false;
    }
        var CommentsReject = $('textarea[id*=txtCommentsReject]').val();
        if (CommentsReject == "" || CommentsReject == null) {
            alert("Please enter comments");
            return false;
        }
     $('#loadingmessage').modal('show');
    return true;
}



