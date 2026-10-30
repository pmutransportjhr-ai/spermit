<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DASHBOARD.aspx.cs" Inherits="DASHBOARD" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Transport Department, Government of Jharkhand</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <style>
        /* ================= GLOBAL ================= */
        body {
            background: #f4f6f9;
        }

        /* ================= HEADER ================= */
        .page-logo img {
            height: 60px;
        }

        .title-center {
            text-align: center;
        }

        .title-center1 {
            font-size: 22px;
            font-weight: bold;
            color: #54A624;
        }

        .title-center2 {
            font-size: 20px;
            font-weight: bold;
            color: #006EBD;
        }

        /* ================= LOGIN BUTTON ================= */
        .login-btn {
            background: linear-gradient(45deg,#006EBD,#54A624);
            border: none;
            font-weight: bold;
            padding: 8px 20px;
            border-radius: 25px;
            color: #fff !important;
        }

            .login-btn:hover {
                transform: scale(1.05);
            }

        /* ================= NAVBAR ================= */
        .custom-navbar {
            background: linear-gradient(90deg,#4e7d1f,#6aa52f);
            padding: 10px 0;
        }

        .navbar {
            overflow: visible !important;
        }

        /* CENTER MENU */
        .navbar-nav {
            gap: 25px;
        }

            .navbar-nav .nav-link {
                color: #fff !important;
                font-weight: 600;
                font-size: 15px;
                position: relative;
            }

                /* HOVER */
                .navbar-nav .nav-link:hover {
                    color: #ffd700 !important;
                }

                /* UNDERLINE */
                .navbar-nav .nav-link::after {
                    content: '';
                    display: block;
                    height: 2px;
                    width: 0;
                    background: #ffd700;
                    transition: 0.3s;
                }

                .navbar-nav .nav-link:hover::after {
                    width: 100%;
                }

        /* ================= DROPDOWN ================= */
        .dropdown-menu {
            background: #5b8e2d !important;
            border: none;
            border-radius: 8px;
            padding: 5px 0;
            min-width: 240px;
            z-index: 9999;
            box-shadow: 0 8px 20px rgba(0,0,0,.2);
        }

        /* ITEMS */
        .dropdown-item {
            color: #fff !important;
            padding: 10px 16px;
            background: transparent !important;
            font-size: 14px;
        }

            /* HOVER FIX (IMPORTANT) */
            .dropdown-item:hover,
            .dropdown-item:focus {
                background: #6fa83a !important;
                color: #ffd700 !important;
            }

        /* ================= MULTI LEVEL ================= */
        .dropdown-submenu {
            position: relative;
        }

            /* LEVEL 2 & 3 */
            .dropdown-submenu > .dropdown-menu {
                top: 0;
                left: 100%;
                margin-left: 0.2rem;
                display: none;
                background: #5b8e2d;
            }

            /* SHOW */
            .dropdown-submenu.show > .dropdown-menu {
                display: block;
            }

        /* ARROW */
        .submenu-toggle::after {
            content: "▶";
            float: right;
            font-size: 12px;
        }

        /* ================= CARDS ================= */
        .card-hover {
            transition: .3s;
            cursor: pointer;
        }

            .card-hover:hover {
                transform: translateY(-4px);
                box-shadow: 0 10px 25px rgba(0,0,0,.15);
            }

        /* ================= SERVICE ================= */
        .service-box {
            background: linear-gradient(135deg,#f8f9fa,#ffffff);
            border-radius: 12px;
            padding: 15px;
            font-weight: 600;
            box-shadow: 0 4px 10px rgba(0,0,0,.08);
            transition: .3s;
        }

            .service-box:hover {
                transform: translateY(-4px);
                box-shadow: 0 8px 20px rgba(0,0,0,.15);
            }

            .service-box b {
                display: block;
                font-size: 20px;
                margin-top: 5px;
                color: #4e7d1f;
            }

        /* ================= DISTRICT ================= */
        .district-item {
            cursor: pointer;
            transition: 0.2s;
        }

            .district-item:hover {
                background: #e9f5e3;
                color: #4e7d1f;
                font-weight: 600;
            }

            .district-item.active {
                background: #4e7d1f;
                color: #fff;
            }

        /* ================= PENDENCY ================= */
        .pendency-value {
            font-size: 18px;
            font-weight: bold;
            margin-top: 5px;
        }

        /* ================= CHART ================= */
        canvas {
            height: 220px !important;
        }

        /* ================= RESPONSIVE ================= */
        @media (max-width: 768px) {
            .title-center1 {
                font-size: 16px;
            }

            .title-center2 {
                font-size: 14px;
            }

            .navbar-nav {
                gap: 10px;
            }
        }

        .pendency-value {
            font-size: 18px;
            font-weight: 700;
        }

        .chart-box {
            background: #fff;
            padding: 15px;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0,0,0,.08);
        }

        canvas {
            height: 250px !important;
        }

        .revenue-box {
            background: #fff;
            border-radius: 10px;
            padding: 15px;
            text-align: center;
            box-shadow: 0 4px 12px rgba(0,0,0,.08);
            transition: .3s;
        }

            .revenue-box:hover {
                transform: translateY(-4px);
                box-shadow: 0 8px 20px rgba(0,0,0,.15);
            }

            .revenue-box h6 {
                font-weight: 600;
                color: #555;
            }

            .revenue-box h5 {
                font-weight: bold;
                color: #4e7d1f;
            }

        .gobal-filter .form-check {
            margin-bottom: 0;
        }

        .gobal-filter .form-check-label {
            cursor: pointer;
            font-weight: 500;
        }

        .gobal-filter .form-control {
            min-width: 140px;
        }

        .gobal-filter .btn {
            min-width: 90px;
        }

        /* ================= SUMMARY CARDS ================= */

        .summary-row {
            align-items: stretch;
        }

        .summary-card {
            height: 100%;
            border-left-width: 4px !important;
            border-radius: 14px;
            transition: .3s;
            box-shadow: 0 4px 14px rgba(0,0,0,.08);
        }

            .summary-card:hover {
                transform: translateY(-5px);
                box-shadow: 0 10px 24px rgba(0,0,0,.15);
            }

        .summary-title {
            font-size: 18px;
            font-weight: 700;
            margin-bottom: 18px;
        }

        .summary-card .list-group-item {
            border: none;
            padding: 12px 0;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .summary-card .badge {
            font-size: 13px;
            padding: 7px 10px;
        }

        /* MOBILE */
        @media (max-width: 991px) {

            .summary-card {
                margin-bottom: 10px;
            }
        }

        .chart-box {
            height: 350px;
            position: relative;
        }

            .chart-box canvas {
                width: 100% !important;
                height: 100% !important;
            }
    </style>

</head>

<body>
    <form runat="server">

        <div class="page-header">
            <!-- BEGIN HEADER TOP -->
            <div class="page-header-top">
                <div class="container">
                    <div class="row">
                        <div class="col-md-2">
                            <!-- BEGIN LOGO -->
                            <div class="page-logo">
                                <a href="#">
                                    <img src="assets/img/logo.png" alt="logo" class="logo-default" />
                                </a>

                            </div>
                        </div>
                        <div class="col-md-8 title-center">
                            <!-- END LOGO -->
                            <!-- BEGIN RESPONSIVE MENU TOGGLER -->

                            <!-- END RESPONSIVE MENU TOGGLER -->
                            <!-- BEGIN TOP NAVIGATION MENU -->
                            <span class="title-center1">Government of Jharkhand,</span>
                            <span class="title-center2">Transport Department</span>
                            <br />
                            <span class="title-center2">Online State Permit System</span>


                            <!-- END TOP NAVIGATION MENU -->
                        </div>
                        <!--  <div class="col-md-2">
                    <a href="http://localhost:52716/Login.aspx">
                        <img src="assets/img/login.png" style="width: 225px; height: 80px;" />
                    </a>

                </div> -->
                        <div class="col-md-2 text-right" style="padding-top: 20px">

                            <a href="Login.aspx" class="btn btn-primary btn-lg login-btn">
                                <i class="fa fa-sign-in"></i>Login
                            </a>

                        </div>
                    </div>
                </div>
            </div>
            <!-- END HEADER TOP -->
            <br />

            <!-- BEGIN HEADER MENU -->
            <nav class="navbar navbar-expand-lg navbar-dark custom-navbar shadow-sm">
                <div class="container-fluid">

                    <!-- LEFT EMPTY (for spacing) -->
                    <div class="d-none d-lg-block" style="width: 150px;"></div>

                    <!-- CENTER MENU -->
                    <div class="collapse navbar-collapse justify-content-center" id="mainNavbar">
                        <ul id="ulMenu" runat="server" class="navbar-nav"></ul>
                    </div>

                    <!-- RIGHT LOGIN -->
                    <div class="d-none d-lg-block" style="width: 150px;"></div>

                </div>
            </nav>
            <!-- END HEADER MENU -->
        </div>

        <div class="container-fluid p-4">

            <!-- ================= GLOBAL FILTER ================= -->
            <div class="card shadow-sm mb-4">

                <div class="card-body">

                    <div class="d-flex flex-wrap align-items-center gap-3">

                        <!-- TITLE -->
                        <div class="fw-bold text-success me-2">
                            Global Filters
                        </div>

                        <!-- 15 DAYS -->
                        <div class="form-check">

                            <asp:RadioButton
                                ID="rdo15Days"
                                runat="server"
                                GroupName="DATEFILTER"
                                Checked="true"
                                CssClass="form-check-input" />

                            <label class="form-check-label ms-1" for="rdo15Days">
                                15 Days
                            </label>

                        </div>

                        <!-- 30 DAYS -->
                        <div class="form-check">

                            <asp:RadioButton
                                ID="rdo30Days"
                                runat="server"
                                GroupName="DATEFILTER"
                                CssClass="form-check-input" />

                            <label class="form-check-label ms-1" for="rdo30Days">
                                30 Days
                            </label>

                        </div>

                        <!-- 3 MONTHS -->
                        <div class="form-check">

                            <asp:RadioButton
                                ID="rdo3Months"
                                runat="server"
                                GroupName="DATEFILTER"
                                CssClass="form-check-input" />

                            <label class="form-check-label ms-1" for="rdo3Months">
                                3 Months
                            </label>

                        </div>

                        <!-- 1 YEAR -->
                        <div class="form-check">

                            <asp:RadioButton
                                ID="rdo1Year"
                                runat="server"
                                GroupName="DATEFILTER"
                                CssClass="form-check-input" />

                            <label class="form-check-label ms-1" for="rdo1Year">
                                1 Year
                            </label>

                        </div>

                        <!-- CUSTOM -->
                        <div class="form-check">

                            <asp:RadioButton
                                ID="rdoCustom"
                                runat="server"
                                GroupName="DATEFILTER"
                                CssClass="form-check-input"
                                onclick="enableCustomDate()" />

                            <label class="form-check-label ms-1 fw-semibold text-primary"
                                for="rdoCustom">
                                Custom
                            </label>

                        </div>

                        <!-- DATE RANGE -->
                        <div class="d-flex align-items-center gap-2">

                            <asp:TextBox
                                ID="txtFromDate"
                                runat="server"
                                TextMode="Date"
                                CssClass="form-control form-control-sm"
                                Style="width: 170px;"
                                Enabled="false">
                            </asp:TextBox>

                            <span class="text-muted">to</span>

                            <asp:TextBox
                                ID="txtToDate"
                                runat="server"
                                TextMode="Date"
                                CssClass="form-control form-control-sm"
                                Style="width: 170px;"
                                Enabled="false">
                            </asp:TextBox>

                        </div>

                        <!-- APPLY -->
                        <asp:Button
                            ID="btnApplyFilter"
                            runat="server"
                            Text="Apply"
                            CssClass="btn btn-success btn-sm px-4"
                            OnClick="btnApplyFilter_Click" />

                        <!-- RESET -->
                        <asp:Button
                            ID="btnResetFilter"
                            runat="server"
                            Text="Reset"
                            CssClass="btn btn-outline-secondary btn-sm"
                            OnClick="btnResetFilter_Click" />

                    </div>

                </div>

            </div>

            <!-- ================= TOP SUMMARY ================= -->
            <div class="row g-4">

                <!-- ================= RTA PERMIT ================= -->
                <div class="col-lg col-md-6">

                    <div class="card card-hover border-start border-danger border-4 h-100">

                        <div class="card-body">

                            <h6 class="fw-bold text-danger">RTA Permit
                            </h6>

                            <ul class="list-group list-group-flush">

                                <asp:Repeater ID="rptRTAPermit" runat="server">

                                    <ItemTemplate>

                                        <li class="list-group-item district-item"
                                            data-type="RTA Permit"
                                            data-district='<%# Eval("RTO_NAME") %>'>

                                            <%# Eval("RTO_NAME") %>

                                            <span class="badge bg-danger float-end">
                                                <%# Eval("TOTAL_COUNT") %>
                                            </span>

                                        </li>

                                    </ItemTemplate>

                                </asp:Repeater>

                            </ul>

                        </div>

                    </div>

                </div>

                <!-- ================= RTA CS ================= -->
                <div class="col-lg col-md-6">

                    <div class="card card-hover border-start border-warning border-4 h-100">

                        <div class="card-body">

                            <h6 class="fw-bold text-warning">RTA CS
                            </h6>

                            <ul class="list-group list-group-flush">

                                <asp:Repeater ID="rptRTACS" runat="server">

                                    <ItemTemplate>

                                        <li class="list-group-item district-item"
                                            data-type="RTA CS"
                                            data-district='<%# Eval("RTO_NAME") %>'>

                                            <%# Eval("RTO_NAME") %>

                                            <span class="badge bg-warning float-end">
                                                <%# Eval("TOTAL_COUNT") %>
                                            </span>

                                        </li>

                                    </ItemTemplate>

                                </asp:Repeater>

                            </ul>

                        </div>

                    </div>

                </div>

                <!-- ================= SCHOOL BUS ================= -->
                <div class="col-lg col-md-6">

                    <div class="card card-hover border-start border-info border-4 h-100">

                        <div class="card-body">

                            <h6 class="fw-bold text-info">School Bus Permit
                            </h6>

                            <ul class="list-group list-group-flush">

                                <asp:Repeater ID="rptSchoolBus" runat="server">

                                    <ItemTemplate>

                                        <li class="list-group-item district-item"
                                            data-type="School Bus Permit"
                                            data-district='<%# Eval("RTO_NAME") %>'>

                                            <%# Eval("RTO_NAME") %>

                                            <span class="badge bg-info float-end">
                                                <%# Eval("TOTAL_COUNT") %>
                                            </span>

                                        </li>

                                    </ItemTemplate>

                                </asp:Repeater>

                            </ul>

                        </div>

                    </div>

                </div>

                <!-- ================= STA PERMIT ================= -->
                <div class="col-lg col-md-6">

                    <div class="card card-hover border-start border-primary border-4 h-100">

                        <div class="card-body">

                            <h6 class="fw-bold text-primary">STA Permit
                            </h6>

                            <ul class="list-group list-group-flush">

                                <asp:Repeater ID="rptSTAPermit" runat="server">

                                    <ItemTemplate>

                                        <li class="list-group-item district-item"
                                            data-type="STA Permit"
                                            data-district='<%# Eval("RTO_NAME") %>'>

                                            <%# Eval("RTO_NAME") %>

                                            <span class="badge bg-primary float-end">
                                                <%# Eval("TOTAL_COUNT") %>
                                            </span>

                                        </li>

                                    </ItemTemplate>

                                </asp:Repeater>

                            </ul>

                        </div>

                    </div>

                </div>

                <!-- ================= STA CS ================= -->
                <div class="col-lg col-md-6">

                    <div class="card card-hover border-start border-success border-4 h-100">

                        <div class="card-body">

                            <h6 class="fw-bold text-success">STA CS
                            </h6>

                            <ul class="list-group list-group-flush">

                                <asp:Repeater ID="rptSTACS" runat="server">

                                    <ItemTemplate>

                                        <li class="list-group-item district-item"
                                            data-type="STA CS"
                                            data-district='<%# Eval("RTO_NAME") %>'>

                                            <%# Eval("RTO_NAME") %>

                                            <span class="badge bg-success float-end">
                                                <%# Eval("TOTAL_COUNT") %>
                                            </span>

                                        </li>

                                    </ItemTemplate>

                                </asp:Repeater>

                            </ul>

                        </div>

                    </div>

                </div>

            </div>

            <!-- ================= SERVICE WISE ================= -->
            <div class="card mt-4 shadow-sm" id="serviceSection" style="display: none;">

                <div class="card-header fw-bold d-flex justify-content-between align-items-center">
                    <span id="serviceTitle">Service Wise Count</span>

                    <!-- Optional filter -->
                    <span class="badge bg-success" id="serviceBadge">All</span>
                </div>

                <div class="card-body">

                    <!-- EMPTY STATE -->
                    <div id="serviceEmpty" class="text-center text-muted py-4">
                        Select a district to view service data
                    </div>

                    <!-- DATA -->
                    <div class="row g-3 text-center d-none" id="serviceContainer"></div>

                </div>

            </div>

            <!-- ================= PENDENCY ================= -->
            <div class="card mt-4 shadow-sm" id="pendencySection">

                <div class="card-header fw-bold">
                    Pendency Count
                </div>

                <div class="card-body">
                    <div class="row g-4" id="pendencyContainer">

                        <!-- RTA PERMIT -->
                        <div class="col-lg col-md-4 col-sm-6">
                            <div class="card border-start border-danger border-4 h-100">
                                <div class="card-body text-center">
                                    <h6 class="fw-bold mb-3 text-danger">RTA Permit</h6>

                                    <div class="row">
                                        <div class="col-4">
                                            <span class="small text-muted">Applied</span>
                                            <div class="pendency-value text-danger">120</div>
                                        </div>
                                        <div class="col-4">
                                            <span class="small text-muted">Verified</span>
                                            <div class="pendency-value text-warning">80</div>
                                        </div>
                                        <div class="col-4">
                                            <span class="small text-muted">Approved</span>
                                            <div class="pendency-value text-success">60</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- RTA CS -->
                        <div class="col-lg col-md-4 col-sm-6">
                            <div class="card border-start border-warning border-4 h-100">
                                <div class="card-body text-center">
                                    <h6 class="fw-bold mb-3 text-warning">RTA CS</h6>

                                    <div class="row">
                                        <div class="col-4">
                                            <span class="small text-muted">Applied</span>
                                            <div class="pendency-value text-danger">120</div>
                                        </div>
                                        <div class="col-4">
                                            <span class="small text-muted">Verified</span>
                                            <div class="pendency-value text-warning">80</div>
                                        </div>
                                        <div class="col-4">
                                            <span class="small text-muted">Approved</span>
                                            <div class="pendency-value text-success">60</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- 🔥 NEW: RTA SCHOOL BUS -->
                        <div class="col-lg col-md-4 col-sm-6">
                            <div class="card border-start border-info border-4 h-100">
                                <div class="card-body text-center">
                                    <h6 class="fw-bold mb-3 text-info">School Bus Permit</h6>

                                    <div class="row">
                                        <div class="col-4">
                                            <span class="small text-muted">Applied</span>
                                            <div class="pendency-value text-danger">90</div>
                                        </div>
                                        <div class="col-4">
                                            <span class="small text-muted">Verified</span>
                                            <div class="pendency-value text-warning">50</div>
                                        </div>
                                        <div class="col-4">
                                            <span class="small text-muted">Approved</span>
                                            <div class="pendency-value text-success">30</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- STA PERMIT -->
                        <div class="col-lg col-md-4 col-sm-6">
                            <div class="card border-start border-primary border-4 h-100">
                                <div class="card-body text-center">
                                    <h6 class="fw-bold mb-3 text-primary">STA Permit</h6>

                                    <div class="row">
                                        <div class="col-4">
                                            <span class="small text-muted">Applied</span>
                                            <div class="pendency-value text-danger">120</div>
                                        </div>
                                        <div class="col-4">
                                            <span class="small text-muted">Verified</span>
                                            <div class="pendency-value text-warning">80</div>
                                        </div>
                                        <div class="col-4">
                                            <span class="small text-muted">Approved</span>
                                            <div class="pendency-value text-success">60</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- STA CS -->
                        <div class="col-lg col-md-4 col-sm-6">
                            <div class="card border-start border-success border-4 h-100">
                                <div class="card-body text-center">
                                    <h6 class="fw-bold mb-3 text-success">STA CS</h6>

                                    <div class="row">
                                        <div class="col-4">
                                            <span class="small text-muted">Applied</span>
                                            <div class="pendency-value text-danger">120</div>
                                        </div>
                                        <div class="col-4">
                                            <span class="small text-muted">Verified</span>
                                            <div class="pendency-value text-warning">80</div>
                                        </div>
                                        <div class="col-4">
                                            <span class="small text-muted">Approved</span>
                                            <div class="pendency-value text-success">60</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>


            <!-- ================= CHART SECTION ================= -->
            <div class="card mt-4 shadow-sm">

                <!-- HEADER WITH FILTERS -->
                <div class="card-header fw-bold d-flex flex-wrap gap-2 justify-content-between align-items-center">

                    <span>Permit Analytics</span>



                </div>

                <!-- CHARTS -->
                <div class="card-body">

                    <div class="row g-4">

                        <!-- BAR CHART -->
                        <div class="col-md-6">
                            <div class="chart-box">
                                <canvas id="barChart"></canvas>
                            </div>
                        </div>

                        <!-- LINE CHART -->
                        <div class="col-md-6">
                            <div class="chart-box">
                                <canvas id="lineChart"></canvas>
                            </div>
                        </div>

                        <!-- PIE CHART -->
                        <div class="col-md-6">
                            <div class="chart-box">
                                <canvas id="pieChart"></canvas>
                            </div>
                        </div>

                        <!-- DOUGHNUT -->
                        <div class="col-md-6">
                            <div class="chart-box">
                                <canvas id="donutChart"></canvas>
                            </div>
                        </div>

                    </div>

                </div>

            </div>

            <!-- ================= REVENUE ================= -->
            <div class="card mt-4 shadow-sm" id="revenueSection">

                <!-- HEADER WITH FILTER -->
                <div class="card-header fw-bold d-flex justify-content-between align-items-center flex-wrap">

                    <span>Revenue Overview</span>

                    <div class="d-flex gap-2">

                        <input type="date" id="revFrom" class="form-control form-control-sm">
                        <input type="date" id="revTo" class="form-control form-control-sm">

                        <select id="revType" class="form-select form-select-sm">
                            <option value="">All</option>
                            <option>RTA Permit</option>
                            <option>STA Permit</option>
                            <option>RTA CS</option>
                            <option>STA CS</option>
                        </select>

                        <button class="btn btn-success btn-sm" onclick="updateRevenue()">Apply</button>

                    </div>

                </div>

                <div class="card-body">

                    <!-- TOTAL -->
                    <div class="text-center mb-4">
                        <h6 class="text-muted">Total Revenue</h6>
                        <h3 class="text-success fw-bold" id="totalRevenue">₹ 3,30,000</h3>
                    </div>

                    <!-- CARDS -->
                    <div class="row g-4" id="revenueContainer">

                        <div class="col-md-3">
                            <div class="revenue-box border-start border-danger border-4">
                                <h6>RTA Permit</h6>
                                <h5>₹ 1,20,000</h5>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="revenue-box border-start border-primary border-4">
                                <h6>STA Permit</h6>
                                <h5>₹ 90,000</h5>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="revenue-box border-start border-warning border-4">
                                <h6>RTA CS</h6>
                                <h5>₹ 70,000</h5>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="revenue-box border-start border-success border-4">
                                <h6>STA CS</h6>
                                <h5>₹ 50,000</h5>
                            </div>
                        </div>

                    </div>

                    <!-- OPTIONAL CHART -->
                    <div class="mt-4">
                        <canvas id="revenueChart"></canvas>
                    </div>

                </div>

            </div>

        </div>

    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <script>

        let barChart, lineChart, pieChart, donutChart, revenueChart;

        /* ========================= CHART ========================= */


        function createCharts(labels, data) {

            // DESTROY OLD CHARTS
            if (barChart) barChart.destroy();
            if (lineChart) lineChart.destroy();
            if (pieChart) pieChart.destroy();
            if (donutChart) donutChart.destroy();

            /* BAR CHART */
            barChart = new Chart(document.getElementById("barChart"), {

                type: 'bar',

                data: {
                    labels: labels,
                    datasets: [{
                        label: "Applications",
                        data: data,
                        backgroundColor: [
                            "#dc3545",
                            "#ffc107",
                            "#0dcaf0",
                            "#0d6efd",
                            "#198754",
                            "#6f42c1"
                        ],
                        borderRadius: 6
                    }]
                },

                options: {
                    responsive: true,
                    maintainAspectRatio: false
                }
            });

            /* LINE CHART */
            lineChart = new Chart(document.getElementById("lineChart"), {

                type: 'line',

                data: {
                    labels: labels,
                    datasets: [{
                        label: "Trend",
                        data: data,
                        borderColor: "#198754",
                        backgroundColor: "rgba(25,135,84,0.1)",
                        tension: 0.4,
                        fill: true
                    }]
                },

                options: {
                    responsive: true,
                    maintainAspectRatio: false
                }
            });

            /* PIE CHART */
            pieChart = new Chart(document.getElementById("pieChart"), {

                type: 'pie',

                data: {
                    labels: labels,
                    datasets: [{
                        data: data,
                        backgroundColor: [
                            "#dc3545",
                            "#ffc107",
                            "#0dcaf0",
                            "#0d6efd",
                            "#198754"
                        ]
                    }]
                },

                options: {
                    responsive: true,
                    maintainAspectRatio: false
                }
            });

            /* DOUGHNUT */
            donutChart = new Chart(document.getElementById("donutChart"), {

                type: 'doughnut',

                data: {
                    labels: labels,
                    datasets: [{
                        data: data,
                        backgroundColor: [
                            "#198754",
                            "#0d6efd",
                            "#ffc107",
                            "#dc3545",
                            "#0dcaf0"
                        ]
                    }]
                },

                options: {
                    responsive: true,
                    maintainAspectRatio: false
                }
            });
        }

        /* ========================= SERVICE DATA ========================= */

        const serviceData = {
            "RTA Permit": {
                "Ranchi": [
                    { name: "Permanent", value: 120 },
                    { name: "Temporary", value: 80 },
                    { name: "New Permit", value: 60 },
                    { name: "Renewal", value: 40 },
                    { name: "Replacement", value: 30 },
                    { name: "Transfer", value: 20 }
                ]
            }
        };

        /* ========================= PENDENCY DATA ========================= */

        const pendencyData = {
            "RTA Permit": {
                "Ranchi": { applied: 120, verified: 80, approved: 60 }
            }
        };

        /* ========================= REVENUE ========================= */

        function updateRevenue() {

            let data = {
                labels: ["RTA Permit", "STA Permit", "RTA CS", "STA CS"],
                values: [120000, 90000, 70000, 50000]
            };

            let total = data.values.reduce((a, b) => a + b, 0);
            document.getElementById("totalRevenue").innerText = "₹ " + total.toLocaleString();

            if (revenueChart) revenueChart.destroy();

            revenueChart = new Chart(document.getElementById("revenueChart"), {
                type: 'bar',
                data: {
                    labels: data.labels,
                    datasets: [{
                        label: "Revenue",
                        data: data.values,
                        backgroundColor: "#4e7d1f"
                    }]
                }
            });
        }

        /* ========================= SERVICE LOAD ========================= */

        function loadService(type, district) {

            let services = serviceData[type] && serviceData[type][district];

            let container = document.getElementById("serviceContainer");
            let title = document.getElementById("serviceTitle");

            container.innerHTML = "";

            if (!services) return;

            title.innerText = type + " - " + district;

            services.forEach(function (srv) {
                container.innerHTML += `
            <div class="col-lg col-md-4 col-6">
                <div class="service-box">
                    ${srv.name}
                    <b>${srv.value}</b>
                </div>
            </div>
        `;
            });

            document.getElementById("serviceSection").style.display = "block";
        }

        /* ========================= PENDENCY UPDATE ========================= */

        function updatePendency(type, district) {

            let data = pendencyData[type] && pendencyData[type][district];
            if (!data) return;

            document.querySelectorAll("#pendencyContainer .card").forEach(function (card) {

                let title = card.querySelector("h6").innerText;

                if (title.includes(type.split(" ")[0])) {

                    let cols = card.querySelectorAll(".pendency-value");

                    cols[0].innerText = data.applied;
                    cols[1].innerText = data.verified;
                    cols[2].innerText = data.approved;
                }
            });
        }



        /* ========================= CHART UPDATE ========================= */

        function updateCharts(type = "", district = "") {

            let labels = [];
            let data = [];

            // DEFAULT DATA
            if (!type) {

                labels = ["Jan", "Feb", "Mar", "Apr", "May", "Jun"];
                data = [50, 80, 120, 90, 140, 110];

            }
            else {

                // DISTRICT BASED DATA
                labels = [
                    district + " Applied",
                    district + " Verified",
                    district + " Approved",
                    district + " Pending"
                ];

                data = [120, 90, 60, 30];
            }

            createCharts(labels, data);
        }

        /* ========================= MENU SUBMENU ========================= */

        document.addEventListener("DOMContentLoaded", function () {

            document.querySelectorAll(".submenu-toggle").forEach(function (el) {

                el.addEventListener("click", function (e) {
                    e.preventDefault();
                    e.stopPropagation();

                    let parent = this.closest(".dropdown-submenu");

                    document.querySelectorAll(".dropdown-submenu").forEach(function (menu) {
                        if (menu !== parent) menu.classList.remove("show");
                    });

                    parent.classList.toggle("show");
                });

            });

        });

        /* ========================= MAIN CLICK FLOW ========================= */

        document.querySelectorAll(".district-item").forEach(function (item) {

            item.addEventListener("click", function () {

                let type = this.getAttribute("data-type");
                let district = this.getAttribute("data-district");

                // ACTIVE UI
                document.querySelectorAll(".district-item").forEach(el => el.classList.remove("active"));
                this.classList.add("active");

                // 🔥 FULL DASHBOARD UPDATE
                loadService(type, district);
                updatePendency(type, district);
                updateCharts(type, district);
                updateRevenue();
            });

        });

        /* ========================= INIT ========================= */

        window.addEventListener("load", function () {

            updateRevenue();
            updateCharts();

        });

        /* ================= GLOBAL FILTER ================= */

        document.addEventListener("DOMContentLoaded", function () {

            const customRadio = document.getElementById("customDate");

            const fromDate = document.getElementById("fromDate");
            const toDate = document.getElementById("toDate");

            const normalFilters = document.querySelectorAll(".date-radio");

            // CUSTOM CLICK
            customRadio.addEventListener("change", function () {

                if (this.checked) {

                    // ENABLE DATE
                    fromDate.disabled = false;
                    toDate.disabled = false;

                    fromDate.focus();

                    // UNCHECK NORMAL FILTERS
                    normalFilters.forEach(function (radio) {
                        radio.checked = false;
                    });
                }
            });

            // NORMAL FILTER CLICK
            normalFilters.forEach(function (radio) {

                radio.addEventListener("change", function () {

                    if (this.checked) {

                        // DISABLE DATE RANGE
                        fromDate.disabled = true;
                        toDate.disabled = true;

                        fromDate.value = "";
                        toDate.value = "";

                        // REMOVE CUSTOM
                        customRadio.checked = false;
                    }
                });

            });

        });

        /* ================= APPLY FILTER ================= */

        function applyGlobalFilter() {

            let selected =
                document.querySelector('input[name="dateFilter"]:checked');

            if (!selected) {
                alert("Please select a filter");
                return;
            }

            let value = selected.value;

            let fromDate = document.getElementById("fromDate").value;
            let toDate = document.getElementById("toDate").value;

            console.log("Filter:", value);
            console.log("From:", fromDate);
            console.log("To:", toDate);

            updateCharts();
            updateRevenue();
        }

        /* ================= RESET ================= */

        function resetGlobalFilter() {

            document.getElementById("days15").checked = true;

            document.getElementById("customDate").checked = false;

            document.getElementById("fromDate").disabled = true;
            document.getElementById("toDate").disabled = true;

            document.getElementById("fromDate").value = "";
            document.getElementById("toDate").value = "";

            updateCharts();
            updateRevenue();
        }

    </script>

    <script>

        function enableCustomDate() {

            let custom =
                document.getElementById('<%= rdoCustom.ClientID %>');

            let from =
                document.getElementById('<%= txtFromDate.ClientID %>');

            let to =
                document.getElementById('<%= txtToDate.ClientID %>');

            if (custom.checked) {

                from.disabled = false;
                to.disabled = false;
            }
        }

        window.onload = function () {

            let radios = document.querySelectorAll(
                "input[type=radio]"
            );

            radios.forEach(function (r) {

                r.addEventListener("change", function () {

                    let custom =
                        document.getElementById('<%= rdoCustom.ClientID %>');

                    let from =
                        document.getElementById('<%= txtFromDate.ClientID %>');

                    let to =
                        document.getElementById('<%= txtToDate.ClientID %>');

                    if (!custom.checked) {

                        from.disabled = true;
                        to.disabled = true;

                        from.value = "";
                        to.value = "";
                    }
                });

            });

        };

    </script>

</body>
</html>
