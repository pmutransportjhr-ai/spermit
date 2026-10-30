let barChart = null;
let lineChart = null;
let pieChart = null;
let donutChart = null;
//let revenueChart = null;

/* ==========================================
   COMMON HELPERS
========================================== */

function getArray(id) {

    const element = document.getElementById(id);

    if (!element || !element.value)
        return [];

    return element.value.split(",");

}

function getValue(id) {

    const element = document.getElementById(id);

    return element ? element.value : "";

}

/* ==========================================
   PAGE INIT
========================================== */

document.addEventListener("DOMContentLoaded", function () {

    if (typeof initDistrictSelection === "function")
        initDistrictSelection();

    if (typeof bindFilterEvents === "function")
        bindFilterEvents();

    initCharts();

});

function showRouteLoader() {

    document.getElementById("routeLoading").style.display = "flex";

}

function hideRouteLoader() {

    document.getElementById("routeLoading").style.display = "none";

}