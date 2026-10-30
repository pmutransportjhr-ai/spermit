<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="VehicleDetailsPublic.aspx.cs"
    Inherits="VehicleDetailsPublic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta charset="utf-8" />

    <meta
        name="viewport"
        content="width=device-width, initial-scale=1" />

    <title>Vehicle Details | Government of Jharkhand
    </title>

    <!-- Bootstrap -->

    <link
        href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
        rel="stylesheet" />

    <!-- Font Awesome -->

    <link
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css"
        rel="stylesheet" />

    <!-- Dashboard -->

    <link
        href="assets/css/dashboard.css"
        rel="stylesheet" />

    <!-- Vehicle Details -->

    <link
        href="assets/css/vehicle-details.css"
        rel="stylesheet" />

</head>

<body>

    <form
        id="form1"
        runat="server">


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

                                <li class="nav-item">

                                    <a class="nav-link"
                                        href="#revenueSection">

                                        <i class="fa fa-indian-rupee-sign me-2"></i>

                                        Revenue Overview

                                    </a>

                                </li>

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



        <br />

        <asp:HyperLink
            ID="lnkBack"
            runat="server"
            NavigateUrl="~/RouteMonitoring.aspx"
            CssClass="btn btn-outline-secondary">
    <i class="fa fa-arrow-left me-2"></i>
    Back
        </asp:HyperLink>

        <br />

        <!--====================================================
        VEHICLE SUMMARY
=====================================================-->

        <br />

        <div class="summary-section mb-4">

            <div class="section-header">

                <div class="d-flex justify-content-between align-items-center">

                    <div>

                        <h4>

                            <i class="fa-solid fa-car-side me-2"></i>

                            Vehicle Summary

                        </h4>

                        <small>Latest Vahan Information
                        </small>

                    </div>

                    <span class="badge bg-success fs-6">Vehicle Details

                    </span>

                </div>

            </div>

            <div class="card-body">

                <div class="row g-4">

                    <!-- Registration -->

                    <div class="col-xl-3 col-md-6">

                        <div class="kpi-card total">

                            <div class="kpi-icon">

                                <i class="fa fa-id-card"></i>

                            </div>

                            <div class="kpi-content">

                                <div class="kpi-title">
                                    Registration

                                </div>

                                <div class="kpi-value fs-5">

                                    <asp:Label
                                        ID="lblRegistration"
                                        runat="server"
                                        Text="--" />

                                </div>

                            </div>

                        </div>

                    </div>

                    <!-- Owner -->

                    <div class="col-xl-3 col-md-6">

                        <div class="kpi-card success">

                            <div class="kpi-icon">

                                <i class="fa fa-user"></i>

                            </div>

                            <div class="kpi-content">

                                <div class="kpi-title">
                                    Owner

                                </div>

                                <div class="kpi-value fs-5">

                                    <asp:Label
                                        ID="lblOwnerName"
                                        runat="server"
                                        Text="--" />

                                </div>

                            </div>

                        </div>

                    </div>

                    <!-- Vehicle Class -->

                    <div class="col-xl-3 col-md-6">

                        <div class="kpi-card warning">

                            <div class="kpi-icon">

                                <i class="fa fa-bus"></i>

                            </div>

                            <div class="kpi-content">

                                <div class="kpi-title">
                                    Vehicle Class

                                </div>

                                <div class="kpi-value fs-5">

                                    <asp:Label
                                        ID="lblVehicleClass"
                                        runat="server"
                                        Text="--" />

                                </div>

                            </div>

                        </div>

                    </div>

                    <!-- Status -->

                    <div class="col-xl-3 col-md-6">

                        <div class="kpi-card danger">

                            <div class="kpi-icon">

                                <i class="fa fa-circle-check"></i>

                            </div>

                            <div class="kpi-content">

                                <div class="kpi-title">
                                    Status

                                </div>

                                <div class="kpi-value fs-5">

                                    <asp:Label
                                        ID="lblStatus"
                                        runat="server"
                                        Text="--" />

                                </div>

                            </div>

                        </div>

                    </div>

                    <!-- Permit -->

                    <div class="col-xl-3 col-md-6">

                        <div class="kpi-card info">

                            <div class="kpi-icon">

                                <i class="fa fa-file-lines"></i>

                            </div>

                            <div class="kpi-content">

                                <div class="kpi-title">
                                    Permit No

                                </div>

                                <div class="kpi-value fs-5">

                                    <asp:Label
                                        ID="lblPermitNo"
                                        runat="server"
                                        Text="--" />

                                </div>

                            </div>

                        </div>

                    </div>

                    <!-- Fuel -->

                    <div class="col-xl-3 col-md-6">

                        <div class="kpi-card purple">

                            <div class="kpi-icon">

                                <i class="fa fa-gas-pump"></i>

                            </div>

                            <div class="kpi-content">

                                <div class="kpi-title">
                                    Fuel

                                </div>

                                <div class="kpi-value fs-5">

                                    <asp:Label
                                        ID="lblFuel"
                                        runat="server"
                                        Text="--" />

                                </div>

                            </div>

                        </div>

                    </div>

                    <!-- Registration Date -->

                    <div class="col-xl-3 col-md-6">

                        <div class="kpi-card total">

                            <div class="kpi-icon">

                                <i class="fa fa-calendar"></i>

                            </div>

                            <div class="kpi-content">

                                <div class="kpi-title">
                                    Registered On

                                </div>

                                <div class="kpi-value fs-5">

                                    <asp:Label
                                        ID="lblRegistrationDate"
                                        runat="server"
                                        Text="--" />

                                </div>

                            </div>

                        </div>

                    </div>

                    <!-- Fitness -->

                    <div class="col-xl-3 col-md-6">

                        <div class="kpi-card success">

                            <div class="kpi-icon">

                                <i class="fa fa-shield-heart"></i>

                            </div>

                            <div class="kpi-content">

                                <div class="kpi-title">
                                    Fitness Valid

                                </div>

                                <div class="kpi-value fs-5">

                                    <asp:Label
                                        ID="lblFitnessValid"
                                        runat="server"
                                        Text="--" />

                                </div>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

        <!--====================================================
        OWNER & VEHICLE INFORMATION
=====================================================-->

        <div class="row g-4 mb-4">

            <!--==========================================
            OWNER INFORMATION
    ===========================================-->

            <div class="col-xl-6">

                <div class="details-card h-100">

                    <div class="section-header">

                        <h4>

                            <i class="fa-solid fa-user text-success me-2"></i>

                            Owner Information

                        </h4>

                    </div>

                    <div class="card-body">

                        <table class="table table-borderless details-table">

                            <tr>
                                <th>Owner Name</th>
                                <td>
                                    <asp:Label ID="lblOwner" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Father's Name</th>
                                <td>
                                    <asp:Label ID="lblFatherName" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Present Address</th>
                                <td>
                                    <asp:Label ID="lblPresentAddress" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Permanent Address</th>
                                <td>
                                    <asp:Label ID="lblPermanentAddress" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Registered At</th>
                                <td>
                                    <asp:Label ID="lblRegisteredAt" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Status As On</th>
                                <td>
                                    <asp:Label ID="lblStatusAsOn" runat="server" Text="--" />
                                </td>
                            </tr>

                        </table>

                    </div>

                </div>

            </div>

            <!--==========================================
            VEHICLE INFORMATION
    ===========================================-->

            <div class="col-xl-6">

                <div class="details-card h-100">

                    <div class="section-header">

                        <h4>

                            <i class="fa-solid fa-car text-primary me-2"></i>

                            Vehicle Information

                        </h4>

                    </div>

                    <div class="card-body">

                        <table class="table table-borderless details-table">

                            <tr>
                                <th>Vehicle Class</th>
                                <td>
                                    <asp:Label ID="lblVehicleClassDetail" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Category</th>
                                <td>
                                    <asp:Label ID="lblCategory" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Fuel Type</th>
                                <td>
                                    <asp:Label ID="lblFuelType" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Colour</th>
                                <td>
                                    <asp:Label ID="lblColour" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Maker</th>
                                <td>
                                    <asp:Label ID="lblMaker" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Maker Model</th>
                                <td>
                                    <asp:Label ID="lblMakerModel" runat="server" Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Manufacturing Year</th>
                                <td>
                                    <asp:Label ID="lblManufacturingYear" runat="server" Text="--" />
                                </td>
                            </tr>

                        </table>

                    </div>

                </div>

            </div>

        </div>

        <!--====================================================
        PERMIT / VALIDITY / TECHNICAL
=====================================================-->

        <div class="row g-4 mb-4">

            <!--======================================
            PERMIT INFORMATION
    =======================================-->

            <div class="col-xl-4">

                <div class="details-card h-100">

                    <div class="section-header">

                        <h4>

                            <i class="fa-solid fa-file-contract text-success me-2"></i>

                            Permit Information

                        </h4>

                    </div>

                    <div class="card-body">

                        <table class="table table-borderless details-table">

                            <tr>
                                <th>Permit Number</th>
                                <td>
                                    <asp:Label
                                        ID="lblPermitNumber"
                                        runat="server"
                                        Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Status</th>
                                <td>
                                    <asp:Label
                                        ID="lblApplicationStatus"
                                        runat="server"
                                        Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Category</th>
                                <td>
                                    <asp:Label
                                        ID="lblPermitCategory"
                                        runat="server"
                                        Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Registered At</th>
                                <td>
                                    <asp:Label
                                        ID="lblOffice"
                                        runat="server"
                                        Text="--" />
                                </td>
                            </tr>

                            <tr>
                                <th>Vehicle Class</th>
                                <td>
                                    <asp:Label
                                        ID="lblPermitVehicleClass"
                                        runat="server"
                                        Text="--" />
                                </td>
                            </tr>

                        </table>

                    </div>

                </div>

            </div>

            <!--======================================
            VALIDITY INFORMATION
    =======================================-->

            <div class="col-xl-4">

                <div class="details-card h-100">

                    <div class="section-header">

                        <h4>

                            <i class="fa-solid fa-calendar-check text-primary me-2"></i>

                            Validity Information

                        </h4>

                    </div>

                    <div class="card-body">

                        <table class="table table-borderless details-table">

                            <tr>
                                <th>Fitness Valid</th>
                                <td>

                                    <asp:Label
                                        ID="lblFitness"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>Insurance Valid</th>
                                <td>

                                    <asp:Label
                                        ID="lblInsurance"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>Tax Paid Upto</th>
                                <td>

                                    <asp:Label
                                        ID="lblTax"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>National Permit</th>
                                <td>

                                    <asp:Label
                                        ID="lblNationalPermit"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>Policy Number</th>
                                <td>

                                    <asp:Label
                                        ID="lblPolicyNo"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>Insurance Company</th>
                                <td>

                                    <asp:Label
                                        ID="lblInsuranceCompany"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                        </table>

                    </div>

                </div>

            </div>

            <!--======================================
            TECHNICAL INFORMATION
    =======================================-->

            <div class="col-xl-4">

                <div class="details-card h-100">

                    <div class="section-header">

                        <h4>

                            <i class="fa-solid fa-gears text-warning me-2"></i>

                            Technical Information

                        </h4>

                    </div>

                    <div class="card-body">

                        <table class="table table-borderless details-table">

                            <tr>
                                <th>Engine No</th>
                                <td>

                                    <asp:Label
                                        ID="lblEngineNo"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>Chassis No</th>
                                <td>

                                    <asp:Label
                                        ID="lblChassisNo"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>ULW</th>
                                <td>

                                    <asp:Label
                                        ID="lblULW"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>RLW</th>
                                <td>

                                    <asp:Label
                                        ID="lblRLW"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>Seating Capacity</th>
                                <td>

                                    <asp:Label
                                        ID="lblSeatCapacity"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>Emission Norm</th>
                                <td>

                                    <asp:Label
                                        ID="lblEmissionNorm"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                            <tr>
                                <th>Financer</th>
                                <td>

                                    <asp:Label
                                        ID="lblFinancer"
                                        runat="server"
                                        Text="--" />

                                </td>
                            </tr>

                        </table>

                    </div>

                </div>

            </div>

        </div>

        <!--====================================================
                ROUTE INFORMATION
=====================================================-->

        <div class="route-section mb-4">

            <div class="section-header">

                <div class="d-flex justify-content-between align-items-center">

                    <div>

                        <h4>

                            <i class="fa-solid fa-route me-2"></i>

                            Route Information

                        </h4>

                        <small>Route allotted to this permit

                        </small>

                    </div>

                    <span class="badge bg-success fs-6">Active Route

                    </span>

                </div>

            </div>

            <div class="card-body">

                <div class="row g-4">

                    <!-- Route Number -->

                    <div class="col-xl-3 col-md-6">

                        <div class="route-card">

                            <div class="route-icon bg-primary">

                                <i class="fa fa-hashtag"></i>

                            </div>

                            <div class="route-title">
                                Route Number

                            </div>

                            <div class="route-value">

                                <asp:Label
                                    ID="lblRouteNumber"
                                    runat="server"
                                    Text="--" />

                            </div>

                        </div>

                    </div>

                    <!-- Route Name -->

                    <div class="col-xl-3 col-md-6">

                        <div class="route-card">

                            <div class="route-icon bg-success">

                                <i class="fa fa-road"></i>

                            </div>

                            <div class="route-title">
                                Route Name

                            </div>

                            <div class="route-value">

                                <asp:Label
                                    ID="lblRouteName"
                                    runat="server"
                                    Text="--" />

                            </div>

                        </div>

                    </div>

                    <!-- Distance -->

                    <div class="col-xl-3 col-md-6">

                        <div class="route-card">

                            <div class="route-icon bg-warning">

                                <i class="fa fa-ruler-horizontal"></i>

                            </div>

                            <div class="route-title">
                                Distance

                            </div>

                            <div class="route-value">

                                <asp:Label
                                    ID="lblRouteDistance"
                                    runat="server"
                                    Text="--" />

                            </div>

                        </div>

                    </div>

                    <!-- Authority -->

                    <div class="col-xl-3 col-md-6">

                        <div class="route-card">

                            <div class="route-icon bg-danger">

                                <i class="fa fa-building"></i>

                            </div>

                            <div class="route-title">
                                Permit Authority

                            </div>

                            <div class="route-value">

                                <asp:Label
                                    ID="lblAuthority"
                                    runat="server"
                                    Text="--" />

                            </div>

                        </div>

                    </div>

                </div>

                <hr class="my-4" />

                <!-- Route Via -->

                <div class="route-path-card">

                    <div class="route-path-title">

                        <i class="fa-solid fa-location-dot me-2"></i>

                        Route Via

                    </div>

                    <div class="route-path-text">

                        <asp:Label
                            ID="lblRouteVia"
                            runat="server"
                            Text="--" />

                    </div>

                </div>

            </div>

        </div>

        <!--====================================================
                ROUTE SCHEDULE
=====================================================-->

        <div class="route-schedule-section mb-4">

            <div class="section-header">

                <div class="d-flex justify-content-between align-items-center">

                    <div>

                        <h4>

                            <i class="fa-solid fa-route me-2"></i>

                            Route Schedule

                        </h4>

                        <small>Up Journey & Down Journey Timings

                        </small>

                    </div>

                    <span class="badge bg-primary fs-6">Live Route

                    </span>

                </div>

            </div>

            <div class="card-body">

                <div class="row g-4">

                    <!--=========================================
                    UP JOURNEY
            ==========================================-->

                    <div class="col-lg-6">

                        <div class="journey-card">

                            <div class="journey-header bg-success">

                                <i class="fa-solid fa-arrow-up me-2"></i>

                                UP JOURNEY

                            </div>

                            <div class="table-responsive">

                                <asp:GridView
                                    ID="gvUpJourney"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    CssClass="table table-hover journey-table"
                                    GridLines="None"
                                    EmptyDataText="No UP Journey Found">

                                    <Columns>

                                        <asp:BoundField
                                            HeaderText="Seq"
                                            DataField="SEQUENCE" />

                                        <asp:BoundField
                                            HeaderText="Via City"
                                            DataField="VIA_CITY" />

                                        <asp:BoundField
                                            HeaderText="Arrival"
                                            DataField="ARRIVAL_TIMING" />

                                        <asp:BoundField
                                            HeaderText="Departure"
                                            DataField="DEPARTURE_TIMING" />

                                    </Columns>

                                </asp:GridView>

                            </div>

                        </div>

                    </div>

                    <!--=========================================
                    DOWN JOURNEY
            ==========================================-->

                    <div class="col-lg-6">

                        <div class="journey-card">

                            <div class="journey-header bg-danger">

                                <i class="fa-solid fa-arrow-down me-2"></i>

                                DOWN JOURNEY

                            </div>

                            <div class="table-responsive">

                                <asp:GridView
                                    ID="gvDownJourney"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    CssClass="table table-hover journey-table"
                                    GridLines="None"
                                    EmptyDataText="No DOWN Journey Found">

                                    <Columns>

                                        <asp:BoundField
                                            HeaderText="Seq"
                                            DataField="SEQUENCE" />

                                        <asp:BoundField
                                            HeaderText="Via City"
                                            DataField="VIA_CITY" />

                                        <asp:BoundField
                                            HeaderText="Arrival"
                                            DataField="ARRIVAL_TIMING" />

                                        <asp:BoundField
                                            HeaderText="Departure"
                                            DataField="DEPARTURE_TIMING" />

                                    </Columns>

                                </asp:GridView>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

        <!--====================================================
            SYSTEM INFORMATION
=====================================================-->

        <div class="row g-4 mb-5">

            <!--======================================
            RESPONSE STATUS
    =======================================-->

            <div class="col-lg-4">

                <div class="details-card h-100">

                    <div class="section-header">

                        <h4>

                            <i class="fa-solid fa-circle-info me-2"></i>

                            Response Status

                        </h4>

                    </div>

                    <div class="card-body">

                        <table class="table table-borderless details-table">

                            <tr>

                                <th>API Status</th>

                                <td>

                                    <span class="badge bg-success">

                                        <asp:Label
                                            ID="lblApiStatus"
                                            runat="server"
                                            Text="Ready" />

                                    </span>

                                </td>

                            </tr>

                            <tr>

                                <th>Response Time</th>

                                <td>

                                    <asp:Label
                                        ID="lblResponseTime"
                                        runat="server"
                                        Text="--" />

                                </td>

                            </tr>

                            <tr>

                                <th>Last Updated</th>

                                <td>

                                    <asp:Label
                                        ID="lblLastUpdated"
                                        runat="server"
                                        Text="--" />

                                </td>

                            </tr>

                        </table>

                    </div>

                </div>

            </div>

            <!--======================================
            SEARCH INFORMATION
    =======================================-->

            <div class="col-lg-4">

                <div class="details-card h-100">

                    <div class="section-header">

                        <h4>

                            <i class="fa-solid fa-magnifying-glass me-2"></i>

                            Search Information

                        </h4>

                    </div>

                    <div class="card-body">

                        <table class="table table-borderless details-table">

                            <tr>

                                <th>Search Vehicle</th>

                                <td>

                                    <asp:Label
                                        ID="lblSearchVehicle"
                                        runat="server"
                                        Text="--" />

                                </td>

                            </tr>

                            <tr>

                                <th>Search Date</th>

                                <td>

                                    <asp:Label
                                        ID="lblSearchDate"
                                        runat="server"
                                        Text="--" />

                                </td>

                            </tr>

                            <tr>

                                <th>Search Time</th>

                                <td>

                                    <asp:Label
                                        ID="lblSearchTime"
                                        runat="server"
                                        Text="--" />

                                </td>

                            </tr>

                        </table>

                    </div>

                </div>

            </div>

            <!--======================================
            SYSTEM INFORMATION
    =======================================-->

            <div class="col-lg-4">

                <div class="details-card h-100">

                    <div class="section-header">

                        <h4>

                            <i class="fa-solid fa-server me-2"></i>

                            System Information

                        </h4>

                    </div>

                    <div class="card-body">

                        <table class="table table-borderless details-table">

                            <tr>

                                <th>Department</th>

                                <td>Transport Department

                                </td>

                            </tr>

                            <tr>

                                <th>Application</th>

                                <td>Online State Permit Management System

                                </td>

                            </tr>

                            <tr>

                                <th>State</th>

                                <td>Government of Jharkhand

                                </td>

                            </tr>

                        </table>

                    </div>

                </div>

            </div>

        </div>

        <!--====================================
        LOADING
=====================================-->

        <div
            id="loadingOverlay"
            class="loading-overlay"
            style="display: none;">

            <div class="loading-card">

                <div
                    class="spinner-border text-success mb-3"
                    role="status">
                </div>

                <h5>Fetching Vehicle Details...

                </h5>

                <p>
                    Please wait while the system retrieves
            the latest Vahan information.

                </p>

            </div>

        </div>

        <footer class="page-footer">

            <div class="container-fluid">

                <div class="row">

                    <div class="col-lg-6">
                        © 2026 Government of Jharkhand

                    </div>

                    <div class="col-lg-6 text-lg-end">
                        Transport Department |
                Vehicle Information Dashboard

                    </div>

                </div>

            </div>

        </footer>

        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

        <script>

            function showLoading() {
                document.getElementById("loadingOverlay").style.display = "flex";
            }

            function hideLoading() {
                document.getElementById("loadingOverlay").style.display = "none";
            }

        </script>

    </form>

</body>

</html>
