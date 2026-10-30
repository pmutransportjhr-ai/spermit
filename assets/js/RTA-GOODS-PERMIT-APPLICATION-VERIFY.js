function fnView(ID) {
    window.open("RTA-GOODS-PERMIT-APPLICATION-VIEW.aspx?q=" + encodeURIComponent(ID),"_blank")
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
        url: "api/RTA_PERMIT.ashx?caller=GetRegDetailsGoods&q=" + encodeURIComponent(ID),
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