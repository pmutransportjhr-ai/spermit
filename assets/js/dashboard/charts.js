/* ==========================================
   COMMON CHART BUILDER
========================================== */

function createChart(config) {

    const canvas = document.getElementById(config.canvasId);

    if (!canvas)
        return null;

    const labels = getArray(config.labelId);
    const values = getArray(config.valueId);

    if (!labels.length || !values.length)
        return null;

    if (config.instance)
        config.instance.destroy();

    return new Chart(canvas, {

        type: config.type,

        data: {

            labels: labels,

            datasets: [{

                label: config.label || "",

                data: values,

                tension: config.tension || 0,

                fill: config.fill || false

            }]

        },

        options: {

            responsive: true,

            maintainAspectRatio: false,

            plugins: {

                legend: {

                    display: true,

                    position: "top"

                }

            }

        }

    });

}

/* ==========================================
   BAR
========================================== */

function loadBarChart() {

    barChart = createChart({

        canvasId: "barChart",

        type: "bar",

        labelId: dashboardIds.hdBarLabel,

        valueId: dashboardIds.hdBarValue,

        label: "RTO Wise Pendency",

        instance: barChart

    });

}

/* ==========================================
   LINE
========================================== */

function loadLineChart() {

    lineChart = createChart({

        canvasId: "lineChart",

        type: "line",

        labelId: dashboardIds.hdLineLabel,

        valueId: dashboardIds.hdLineValue,

        label: "Monthly Applications",

        tension: 0.4,

        instance: lineChart

    });

}

/* ==========================================
   PIE
========================================== */

function loadPieChart() {

    pieChart = createChart({

        canvasId: "pieChart",

        type: "pie",

        labelId: dashboardIds.hdPieLabel,

        valueId: dashboardIds.hdPieValue,

        instance: pieChart

    });

}

/* ==========================================
   DONUT
========================================== */

function loadDonutChart() {

    donutChart = createChart({

        canvasId: "donutChart",

        type: "doughnut",

        labelId: dashboardIds.hdDonutLabel,

        valueId: dashboardIds.hdDonutValue,

        instance: donutChart

    });

}

/* ==========================================
   REVENUE
========================================== */

//function loadRevenueChart() {

//    revenueChart = createChart({

//        canvasId: "revenueChart",

//        type: "bar",

//        labelId: dashboardIds.hdRevenueLabels,

//        valueId: dashboardIds.hdRevenueValues,

//        label: "Revenue",

//        instance: revenueChart

//    });

//}

/* ==========================================
   INIT ALL CHARTS
========================================== */

function initCharts() {

    loadBarChart();

    loadLineChart();

    loadPieChart();

    loadDonutChart();

    //loadRevenueChart();

}