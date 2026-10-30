
/* ==================================================
   GLOBAL CUSTOM DATE
================================================== */

function enableCustomDate() {


    let custom =
        document.getElementById(
            '<%= rdoCustom.ClientID %>'
        );


    let from =
        document.getElementById(
            '<%= txtFromDate.ClientID %>'
        );


    let to =
        document.getElementById(
            '<%= txtToDate.ClientID %>'
        );



    if (!custom || !from || !to)
        return;



    from.disabled =
        !custom.checked;


    to.disabled =
        !custom.checked;

}



document.addEventListener(
    "DOMContentLoaded",
    function () {


        document
            .querySelectorAll(
                "input[type=radio]"
            )
            .forEach(function (r) {


                r.addEventListener(
                    "change",
                    enableCustomDate
                );


            });


    });

