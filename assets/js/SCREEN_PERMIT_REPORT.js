jQuery(document).ready(function () {

      

})
  
    function fnView(ID)
    {
       $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/STA_PERMIT.ashx?caller=GetDetailsByState&q=" + encodeURIComponent(ID),
            data: "",
            cache: false,
            async: false,
            success: function (data) {

                $('#loadingmessage').modal('hide');
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "Success") {
                        $('#sample50 tbody').html(data.split(/~(.+)?/)[1].toString());
                      
                    }
                    else {
                        $('#sample50').html('');

                    }
                    }
                   

                },

            error: function (response) {
                $('#loadingmessage').modal('hide');
                alert(response.status + ' ' + response.statusText);
            }
        });

        $('#showDetails').modal();
        $('#loadingmessage').modal('hide');

    }
    function MasterView(q) {
        window.open("STA-PERMIT-REPORT-VIEW.aspx?q=" + encodeURIComponent(q));
    }
    function MasterEditSTA(q) {
        window.open("SCREEN_STA_PERMIT_REPORT_EDIT.aspx?q=" + encodeURIComponent(q));
    }
    function MasterEditRTA(q) {
        window.open("SCREEN_RTA_PERMIT_MASTER_EDIT.aspx?q=" + encodeURIComponent(q));
    }
    function fnViewRTA(ID) {
        $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/RTA_PERMIT.ashx?caller=GetDetailsByRTO&q=" + encodeURIComponent(ID),
            data: "",
            cache: false,
            async: false,
            success: function (data) {

                $('#loadingmessage').modal('hide');
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "Success") {


                        $('#sample50').html(data.split(/~(.+)?/)[1].toString());
                        // TableDatatablesButtons.init();

                    }
                    else {
                        $('#sample50').html('');

                    }

                }
                else {
                    $('#sample50').html('');

                }

            },

            error: function (response) {
                $('#loadingmessage').modal('hide');
                alert(response.status + ' ' + response.statusText);
            }
        });

        $('#showDetails').modal();
        $('#loadingmessage').modal('hide');

    }
    function MasterViewRTA(q) {
        window.open("RTA-PERMIT-REPORT-VIEW.aspx?q=" + encodeURIComponent(q));
    }
    function fnViewRTAGoods(ID) {
        $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/RTA_PERMIT.ashx?caller=GetGoodsDetailsByRTO&q=" + encodeURIComponent(ID),
            data: "",
            cache: false,
            async: false,
            success: function (data) {

                $('#loadingmessage').modal('hide');
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "Success") {


                        $('#sample50').html(data.split(/~(.+)?/)[1].toString());
                        // TableDatatablesButtons.init();

                    }
                    else {
                        $('#sample50').html('');

                    }

                }
                else {
                    $('#sample50').html('');

                }

            },

            error: function (response) {
                $('#loadingmessage').modal('hide');
                alert(response.status + ' ' + response.statusText);
            }
        });

        $('#showDetails').modal();
        $('#loadingmessage').modal('hide');

    }
    function MasterViewRTAGoods(q) {
        window.open("RTA-GOODS-PERMIT-REPORT-VIEW.aspx?q=" + encodeURIComponent(q));
    }
    function MasterEditRTAGoods(q) {
        window.open("SCREEN_RTA_GOODS_PERMIT_APPLICATION_EDIT.aspx?q=" + encodeURIComponent(q));
    }

    function fnViewRTAContract(ID) {
        $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/RTA_PERMIT.ashx?caller=GetContractDetailsByRTO&q=" + encodeURIComponent(ID),
            data: "",
            cache: false,
            async: false,
            success: function (data) {

                $('#loadingmessage').modal('hide');
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "Success") {


                        $('#sample50').html(data.split(/~(.+)?/)[1].toString());
                        // TableDatatablesButtons.init();

                    }
                    else {
                        $('#sample50').html('');

                    }

                }
                else {
                    $('#sample50').html('');

                }

            },

            error: function (response) {
                $('#loadingmessage').modal('hide');
                alert(response.status + ' ' + response.statusText);
            }
        });

        $('#showDetails').modal();
        $('#loadingmessage').modal('hide');

    }
    function MasterViewRTAContract(q) {
        window.open("RTA-CONTRACT-PERMIT-REPORT-VIEW.aspx?q=" + encodeURIComponent(q));
    }
    function MasterEditRTAContract(q) {
        window.open("SCREEN_RTA_CONTRACT_PERMIT_APPLICATION_EDIT.aspx?q=" + encodeURIComponent(q));
    }
    function fnViewRTAAuto(ID) {
        $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/RTA_PERMIT.ashx?caller=GetAutoDetailsByRTO&q=" + encodeURIComponent(ID),
            data: "",
            cache: false,
            async: false,
            success: function (data) {

                $('#loadingmessage').modal('hide');
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "Success") {


                        $('#sample50').html(data.split(/~(.+)?/)[1].toString());
                        // TableDatatablesButtons.init();

                    }
                    else {
                        $('#sample50').html('');

                    }

                }
                else {
                    $('#sample50').html('');

                }

            },

            error: function (response) {
                $('#loadingmessage').modal('hide');
                alert(response.status + ' ' + response.statusText);
            }
        });

        $('#showDetails').modal();
        $('#loadingmessage').modal('hide');

    }
    function MasterViewRTAAuto(q) {
        window.open("RTA-AUTO-PERMIT-REPORT-VIEW.aspx?q=" + encodeURIComponent(q));
    }
    function MasterEditRTAAuto(q) {
        window.open("SCREEN_RTA_AUTO_PERMIT_APPLICATION_EDIT.aspx?q=" + encodeURIComponent(q));
    }
    function fnViewRTASchool(ID) {
        $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/RTA_PERMIT.ashx?caller=GetSchoolDetailsByRTO&q=" + encodeURIComponent(ID),
            data: "",
            cache: false,
            async: false,
            success: function (data) {

                $('#loadingmessage').modal('hide');
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "Success") {
                      $('#sample50').html(data.split(/~(.+)?/)[1].toString());
                        // TableDatatablesButtons.init();

                    }
                    else {
                        $('#sample50').html('');
                    }

                }
                else {
                    $('#sample50').html('');

                }

            },

            error: function (response) {
                $('#loadingmessage').modal('hide');
                alert(response.status + ' ' + response.statusText);
            }
        });

        $('#showDetails').modal();
        $('#loadingmessage').modal('hide');

    }
    function MasterViewRTASchool(q) {
        window.open("RTA-SCHOOL-BUS-PERMIT-REPORT-VIEW.aspx?q=" + encodeURIComponent(q));
    }
    function MasterEditRTASchool(q) {
        window.open("SCREEN_RTA_SCHOOL_BUS_PERMIT_APPLICATION_EDIT.aspx?q=" + encodeURIComponent(q));
    }