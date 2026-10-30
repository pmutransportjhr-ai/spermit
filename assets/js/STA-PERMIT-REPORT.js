function fnValidateReport()
{
    if($('select[id*=ddlDesState]').val()==""||$('select[id*=ddlDesState]').val()==null)
    {
        alert("Please Select State");
        return false;

    }
    if ($('select[id*=ddlStatus]').val() == "" || $('select[id*=ddlStatus]').val() == null) {
        alert("Please Select Status");
        return false;

    }
    if ($('select[id*=ddlType]').val() == "" || $('select[id*=ddlType]').val() == null) {
        alert("Please Select Type");
        return false;

    }
    if ($('input[id*=txtFrom]').val() == "" || $('input[id*=txtFrom]').val() == null) {
        alert("Please Enter From Date");
        return false;

    }
    if ($('input[id*=txtTo]').val() == "" || $('input[id*=txtTo]').val() == null) {
        alert("Please Enter To Date");
        return false;

    }

    return true;
}
function MasterView(q) {
    window.open("STA-PERMIT-REPORT-VIEW.aspx?q=" + encodeURIComponent(q));
}
function showIssuedReceipt(q)
{
    window.open("STA-PERMIT-ISSUED-RECEIPT.aspx?q=" + encodeURIComponent(q));
}
function showIssuedReceiptGlobal(q) {
   window.open("STA-PERMIT-ISSUED-RECEIPT-GLOBAL.aspx?q=" + encodeURIComponent(q));
}
function showTimetable(q)
{
    window.open("STA-TIME-TABLE.aspx?q=" + encodeURIComponent(q));
}
function showForwardingLetter(q)
{
    window.open("STA-FORWARDING-LETTER.aspx?q=" + encodeURIComponent(q));
}
