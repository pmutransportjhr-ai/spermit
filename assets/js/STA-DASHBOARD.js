function fnViewRevenue(PERIOD) {
    $('input[id*=txtroutjson]')
    if ($('span[id*=' + PERIOD + 'Revenue').text() == "0.00" || $('span[id*=' + PERIOD + 'Revenue').text() == "0" || $('span[id*=' + PERIOD + 'Revenue').text() == "") {
        alert("Sorry Nothing to Display")
        return false;
    }
    else {
        //window.location.href = "RTA-AUTO-RICKSHAW-PERMIT-DUPLICATE-APPLICATION-VIEW.aspx?q=" + encodeURIComponent(ID)
        $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/STA_DASHBOARD.ashx?caller=GetRevenueDetails&PERIOD=" + encodeURIComponent(PERIOD),
            data: "",
            cache: false,
            async: false,
            success: function (data) {

                $('#loadingmessage').modal('hide');
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "Success") {

                        $('#pnlperiodldrsub').text("Revenue Details");
                        $('div[id*=rptpnlLdrsub]').html(data.split(/~(.+)?/)[1].toString());
                        $('#stack3').modal();
                        //ComponentsDateTimePickers.init();
                    }
                    else {
                        $('div[id*=rptpnlLdrsub]').html("");
                    }

                }

            },
            error: function (response) {
                $('#loadingmessage').modal('hide');
                alert(response.status + ' ' + response.statusText);
            }
        });
    }
}
function fnViewPTIssued(PERIOD)
{
  
    if ($('span[id*=' + PERIOD + 'PTIssued').text() == "0.00" || $('span[id*=' + PERIOD + 'PTIssued').text() == "0" || $('span[id*=' + PERIOD + 'PTIssued').text() == "") {
        alert("Sorry Nothing to Display")
        return false;
    }
    else {
        $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/STA_DASHBOARD.ashx?caller=GetPermitDetails&PERIOD=" + encodeURIComponent(PERIOD),
            data: "",
            cache: false,
            async: false,
            success: function (data) {

                $('#loadingmessage').modal('hide');
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "Success") {

                        $('#pnlperiodldrsub').text("Permit Details");
                        $('div[id*=rptpnlLdrsub]').html(data.split(/~(.+)?/)[1].toString());
                        $('#stack3').modal();
                        //ComponentsDateTimePickers.init();
                    }
                    else {
                        $('div[id*=rptpnlLdrsub]').html("");
                    }

                }

            },
            error: function (response) {
                $('#loadingmessage').modal('hide');
                alert(response.status + ' ' + response.statusText);
            }
        });
    }
}
function fnViewPTApp(PERIOD)
{
    //$('#tbdRouteInfo_' + j + '  tr')
    
    if ($('span[id*=' + PERIOD + 'PTApp').text() == "0.00" || $('span[id*=' + PERIOD + 'PTApp').text() == "0" || $('span[id*=' + PERIOD + 'PTApp').text() == "") {
        alert("Sorry Nothing to Display")
        return false;
    }
    else {
        $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/STA_DASHBOARD.ashx?caller=GetPendingAppDetails&PERIOD=" + encodeURIComponent(PERIOD),
            data: "",
            cache: false,
            async: false,
            success: function (data) {

                $('#loadingmessage').modal('hide');
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "Success") {

                        $('#pnlperiodldrsub').text("Pending Application Details");
                        $('div[id*=rptpnlLdrsub]').html(data.split(/~(.+)?/)[1].toString());
                        $('#stack3').modal();
                        //ComponentsDateTimePickers.init();
                    }
                    else {
                        $('div[id*=rptpnlLdrsub]').html("");
                    }

                }

            },
            error: function (response) {
                $('#loadingmessage').modal('hide');
                alert(response.status + ' ' + response.statusText);
            }
        });
    }
}