jQuery(document).ready(function () {

      

})

   
    
    function fnView(ID)
    {
       $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/STA_PERMIT.ashx?caller=GetCSDetailsByState&q=" + encodeURIComponent(ID),
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
        window.open("STA-COUNTER-SIGNATURE-REPORT-VIEW.aspx?q=" + encodeURIComponent(q));
    }
    
    function MasterEditSTA(q) {
        window.open("SCREEN_STA_CS_EDIT.aspx?q=" + encodeURIComponent(q));
    }
   
   
   