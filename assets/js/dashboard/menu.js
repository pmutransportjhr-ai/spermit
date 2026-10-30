/* ==================================================
       MENU DROPDOWN
    ================================================== */

document.addEventListener(
    "DOMContentLoaded",
    function () {


        document
            .querySelectorAll(".submenu-toggle")
            .forEach(function (el) {


                el.addEventListener(
                    "click",
                    function (e) {


                        e.preventDefault();

                        e.stopPropagation();


                        let parent =
                            this.closest(
                                ".dropdown-submenu"
                            );


                        document
                            .querySelectorAll(".dropdown-submenu")
                            .forEach(function (menu) {


                                if (menu !== parent)

                                    menu.classList.remove("show");


                            });


                        parent.classList.toggle("show");


                    });


            });

    });



/* ==================================================
   DISTRICT CARD ACTIVE
================================================== */

document.addEventListener(
    "DOMContentLoaded",
    function () {


        document
            .querySelectorAll(".district-item")
            .forEach(function (item) {


                item.addEventListener(
                    "click",
                    function () {


                        document
                            .querySelectorAll(".district-item")
                            .forEach(x =>
                                x.classList.remove("active")
                            );


                        this.classList.add("active");


                    });


            });


    });

