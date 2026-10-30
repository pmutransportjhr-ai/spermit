<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DASHBOARD.aspx.cs" Inherits="DASHBOARD" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Transport Department, Government of Jharkhand</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <link href="assets/css/dashboard.css" rel="stylesheet" />
    <link href="assets/css/dashboard-responsive.css" rel="stylesheet" />
    <link rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css">
</head>

<body>
    <form runat="server">

        <div class="top-wrapper">
            <!-- ================= HEADER ================= -->

            <header class="dashboard-header">

                <div class="container-fluid">

                    <div class="row align-items-center">

                        <!-- Logo -->

                        <div class="col-lg-2 col-md-3 text-center text-lg-start">

                            <img src="assets/img/logo.png"
                                class="header-logo"
                                alt="Jharkhand Logo" />

                        </div>

                        <!-- Title -->

                        <div class="col-lg-8 col-md-6 text-center">

                            <h2 class="header-title">Government of Jharkhand
                            </h2>

                            <h4 class="header-subtitle">Transport Department
                            </h4>

                            <p class="header-system">
                                Online State Permit System
                            </p>

                        </div>

                    </div>

                </div>

            </header>

            <div>
                <!-- BEGIN HEADER MENU -->
                <!--==================== DASHBOARD NAVIGATION ====================-->

                <nav class="navbar navbar-expand-lg dashboard-navbar shadow-sm">

                    <div class="container-fluid">

                        <button class="navbar-toggler"
                            type="button"
                            data-bs-toggle="collapse"
                            data-bs-target="#dashboardMenu">

                            <span class="navbar-toggler-icon"></span>

                        </button>

                        <div class="collapse navbar-collapse"
                            id="dashboardMenu">

                            <ul class="navbar-nav mx-auto">

                                <li class="nav-item">

                                    <a class="nav-link active"
                                        href="PermitHomepage.aspx">

                                        <i class="fa fa-home me-2"></i>

                                        Home

                                    </a>

                                </li>

                                <li class="nav-item">

                                    <a class="nav-link"
                                        href="#pendencySection">

                                        <i class="fa fa-clock me-2"></i>

                                        Pendency

                                    </a>

                                </li>

                                <li class="nav-item">

                                    <a class="nav-link"
                                        href="RouteMonitoring.aspx">

                                        <i class="fa fa-road me-2"></i>

                                        Route Monitoring

                                    </a>

                                </li>

                                <li class="nav-item">

                                    <a class="nav-link"
                                        href="#analyticsSection">

                                        <i class="fa fa-chart-column me-2"></i>

                                        Permit Analytics

                                    </a>

                                </li>

                               <!-- <li class="nav-item">

                                    <a class="nav-link"
                                        href="#revenueSection">

                                        <i class="fa fa-indian-rupee-sign me-2"></i>

                                        Revenue Overview

                                    </a>

                                </li> -->

                            </ul>

                            <div>

                                <a href="Login.aspx"
                                    class="btn btn-success px-4">

                                    <i class="fa fa-sign-in-alt me-2"></i>

                                    Login

                                </a>

                            </div>

                        </div>

                    </div>

                </nav>
                <!-- END HEADER MENU -->
            </div>

        </div>



        <div class="container-fluid p-4">

            <!-- ================= GLOBAL FILTER ================= -->
            <div class="card shadow-sm mb-4">

                <div class="card-body">

                    <div class="d-flex flex-wrap align-items-center gap-3">

                        <!-- TITLE -->
                        <div class="fw-bold text-success mr-2">
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


                <div class="card-header fw-bold d-flex justify-content-between">

                    <span>Pendency Count</span>


                    <asp:Button
                        ID="btnDownloadPendency"
                        runat="server"
                        Text="Download Excel"
                        CssClass="btn btn-success btn-sm"
                        OnClick="btnDownloadPendency_Click" />

                </div>


                <div class="card-body">

                    <div class="row g-4">


                        <!-- RTA PERMIT -->
                        <div class="col-lg-4 col-md-6">

                            <div class="card pendency-card border-start border-danger border-4">

                                <div class="card-body">

                                    <h6 class="text-danger fw-bold">RTA Permit
                                    </h6>

                                    <asp:Repeater
                                        ID="rptRTAPendency"
                                        runat="server">

                                        <ItemTemplate>

                                            <div class="pendency-row">

                                                <span>
                                                    <%# Eval("LOCATION") %>
                                                    <br />
                                                    <small>
                                                        <%# Eval("SERVICE") %>
                                                    </small>
                                                </span>


                                                <b class="count-badge">
                                                    <%# Eval("PENDING_COUNT") %>
                                                </b>

                                            </div>


                                        </ItemTemplate>


                                    </asp:Repeater>

                                </div>

                            </div>

                        </div>




                        <!-- RTA CS -->
                        <div class="col-lg-4 col-md-6">

                            <div class="card pendency-card border-start border-warning border-4">

                                <div class="card-body">

                                    <h6 class="text-warning fw-bold">RTA Counter Signature
                                    </h6>


                                    <asp:Repeater
                                        ID="rptRTACSPendency"
                                        runat="server">

                                        <ItemTemplate>

                                            <div class="pendency-row">

                                                <span>
                                                    <%# Eval("LOCATION") %>
                                                    <br />
                                                    <small>
                                                        <%# Eval("SERVICE") %>
                                                    </small>
                                                </span>


                                                <b class="count-badge">
                                                    <%# Eval("PENDING_COUNT") %>
                                                </b>


                                            </div>

                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>

                            </div>

                        </div>





                        <!-- SCHOOL BUS -->
                        <div class="col-lg-4 col-md-6">

                            <div class="card pendency-card border-start border-info border-4">

                                <div class="card-body">

                                    <h6 class="text-info fw-bold">School Bus
                                    </h6>


                                    <asp:Repeater
                                        ID="rptSchoolBusPendency"
                                        runat="server">

                                        <ItemTemplate>

                                            <div class="pendency-row">

                                                <span>
                                                    <%# Eval("LOCATION") %>
                                                    <br />
                                                    <small>
                                                        <%# Eval("SERVICE") %>
                                                    </small>
                                                </span>


                                                <b class="count-badge">
                                                    <%# Eval("PENDING_COUNT") %>
                                                </b>

                                            </div>

                                        </ItemTemplate>

                                    </asp:Repeater>


                                </div>

                            </div>

                        </div>





                        <!-- STA PERMIT -->

                        <div class="col-lg-6 col-md-6">

                            <div class="card pendency-card border-start border-primary border-4">

                                <div class="card-body">


                                    <h6 class="text-primary fw-bold">STA Permit
                                    </h6>


                                    <asp:Repeater
                                        ID="rptSTAPendency"
                                        runat="server">

                                        <ItemTemplate>

                                            <div class="pendency-row">

                                                <span>
                                                    <%# Eval("SERVICE") %>
                                                </span>


                                                <b class="count-badge">
                                                    <%# Eval("PENDING_COUNT") %>
                                                </b>

                                            </div>


                                        </ItemTemplate>

                                    </asp:Repeater>

                                </div>

                            </div>

                        </div>





                        <!-- STA CS -->

                        <div class="col-lg-6 col-md-6">

                            <div class="card pendency-card border-start border-success border-4">

                                <div class="card-body">


                                    <h6 class="text-success fw-bold">STA Counter Signature
                                    </h6>


                                    <asp:Repeater
                                        ID="rptSTACSPendency"
                                        runat="server">

                                        <ItemTemplate>


                                            <div class="pendency-row">

                                                <span>
                                                    <%# Eval("SERVICE") %>
                                                </span>


                                                <b class="count-badge">
                                                    <%# Eval("PENDING_COUNT") %>
                                                </b>


                                            </div>


                                        </ItemTemplate>

                                    </asp:Repeater>


                                </div>

                            </div>

                        </div>


                    </div>

                </div>

            </div>






            <!-- ================= CHART SECTION ================= -->
            <div class="card mt-4 shadow-sm"
                id="analyticsSection">

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

            <asp:HiddenField ID="hdBarLabel" runat="server" />
            <asp:HiddenField ID="hdBarValue" runat="server" />

            <asp:HiddenField ID="hdLineLabel" runat="server" />
            <asp:HiddenField ID="hdLineValue" runat="server" />

            <asp:HiddenField ID="hdPieLabel" runat="server" />
            <asp:HiddenField ID="hdPieValue" runat="server" />

            <asp:HiddenField ID="hdDonutLabel" runat="server" />
            <asp:HiddenField ID="hdDonutValue" runat="server" />


            <!-- ================= REVENUE ================= -->

            <!-- <div class="card mt-4 shadow-sm"
                id="revenueSection">

                <div class="card-header fw-bold d-flex justify-content-between">

                    <span>Revenue Overview</span>


                    <div class="d-flex gap-2">


                        <asp:TextBox
                            ID="txtRevFrom"
                            runat="server"
                            TextMode="Date"
                            CssClass="form-control form-control-sm" />


                        <asp:TextBox
                            ID="txtRevTo"
                            runat="server"
                            TextMode="Date"
                            CssClass="form-control form-control-sm" />



                        <asp:DropDownList
                            ID="ddlRevenueType"
                            runat="server"
                            CssClass="form-select form-select-sm">

                            <asp:ListItem Value="">
                                All
                            </asp:ListItem>

                            <asp:ListItem>
                                RTA Permit
                            </asp:ListItem>

                            <asp:ListItem>
                                STA Permit
                            </asp:ListItem>

                            <asp:ListItem>
                                    RTA CS
                            </asp:ListItem>

                            <asp:ListItem>
                                    STA CS
                            </asp:ListItem>


                        </asp:DropDownList>



                        <asp:Button
                            ID="btnRevenueFilter"
                            runat="server"
                            Text="Apply"
                            CssClass="btn btn-success btn-sm"
                            OnClick="btnRevenueFilter_Click" />


                        <asp:Button
                            ID="btnRevenueExcel"
                            runat="server"
                            Text="Excel"
                            CssClass="btn btn-primary btn-sm"
                            OnClick="btnRevenueExcel_Click" />


                    </div>

                </div>



                <div class="card-body">


                    <h3 class="text-success text-center">₹
                        <asp:Label
                            ID="lblTotalRevenue"
                            runat="server" />

                    </h3>



                    <div class="row g-4">


                        <asp:Repeater
                            ID="rptRevenue"
                            runat="server">


                            <ItemTemplate>


                                <div class="col-md-3">

                                    <div class="revenue-box border-start border-success border-4">


                                        <h6>
                                            <%# Eval("CATEGORY") %>
                                        </h6>


                                        <h5>₹ <%# Eval("TOTAL_AMOUNT") %>

                                        </h5>


                                        <small>Transactions:
                                            <%# Eval("TOTAL_TRANSACTION") %>

                                        </small>


                                    </div>

                                </div>


                            </ItemTemplate>


                        </asp:Repeater>


                    </div>


                    <div class="mt-4">

                        <canvas id="revenueChart"></canvas>

                    </div>


                    <asp:HiddenField
                        ID="hdRevenueLabels"
                        runat="server" />


                    <asp:HiddenField
                        ID="hdRevenueValues"
                        runat="server" />


                </div>


            </div>


        </div>  -->
            <!-- container-fluid -->
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="assets/js/dashboard/menu.js"></script>
    <script src="assets/js/dashboard/filters.js"></script>
    <script>
        window.dashboardIds = {

            hdBarLabel: '<%= hdBarLabel.ClientID %>',
            hdBarValue: '<%= hdBarValue.ClientID %>',

            hdLineLabel: '<%= hdLineLabel.ClientID %>',
            hdLineValue: '<%= hdLineValue.ClientID %>',

            hdPieLabel: '<%= hdPieLabel.ClientID %>',
            hdPieValue: '<%= hdPieValue.ClientID %>',

            hdDonutLabel: '<%= hdDonutLabel.ClientID %>',
            hdDonutValue: '<%= hdDonutValue.ClientID %>',

            //hdRevenueLabels: '<%= hdRevenueLabels.ClientID %>',
            //hdRevenueValues: '<%= hdRevenueValues.ClientID %>'

        };
    </script>
    <script src="assets/js/dashboard/charts.js"></script>
    <script src="assets/js/dashboard/dashboard.js"></script>



</body>
</html>
