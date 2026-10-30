<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeFile="RouteMonitoring.aspx.cs"
    Inherits="RouteMonitoring" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <meta charset="utf-8" />

    <meta
        name="viewport"
        content="width=device-width, initial-scale=1" />

    <title>Transport Department, Government of Jharkhand</title>



    <link
        href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
        rel="stylesheet" />



    <link
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css"
        rel="stylesheet" />

    <link
        href="assets/css/dashboard.css"
        rel="stylesheet" />

    <link
        href="assets/css/route-monitoring.css"
        rel="stylesheet" />

    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0/dist/css/select2.min.css" rel="stylesheet" />
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





        <div class="container-fluid py-4">



            <div class="hero-card mb-4">

                <div class="row align-items-center">



                    <div class="col-lg-8">

                        <nav class="hero-breadcrumb mb-2">

                            <i class="fa fa-home text-success"></i>

                            Home

                <span class="mx-2">/</span>

                            Dashboard

                <span class="mx-2">/</span>

                            <strong>Route Monitoring</strong>

                        </nav>

                        <h2 class="hero-title">

                            <i class="fa-solid fa-road text-success me-2"></i>

                            Route & Vehicle Monitoring

                        </h2>

                        <p class="hero-subtitle">
                            Monitor all operational permit routes, search vehicles,
                analyse permit status and generate reports for
                STA Permit, STA Counter Signature and
                RTA Permit routes.

                        </p>

                    </div>



                    <div class="col-lg-4">

                        <div class="row g-3">


                            <div class="col-6">

                                <div class="hero-mini-card">

                                    <div class="hero-mini-title">
                                        Total Routes

                                    </div>

                                    <div class="hero-mini-value">

                                        <asp:Label
                                            ID="lblTotalRoutes"
                                            runat="server"
                                            Text="0" />

                                    </div>

                                </div>

                            </div>



                            <div class="col-6">

                                <div class="hero-mini-card">

                                    <div class="hero-mini-title">
                                        Last Refresh

                                    </div>

                                    <div class="hero-mini-time">

                                        <asp:Label
                                            ID="lblLastRefresh"
                                            runat="server"
                                            Text="--" />

                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>

                </div>



                <hr class="my-4" />

                <div class="row align-items-center">

                    <div class="col-lg-6">

                        <span class="text-muted">

                            <i class="fa-solid fa-circle-info me-2 text-success"></i>

                            Select an Authority and Route to load all vehicles
                operating on that route.

                        </span>

                    </div>

                    <div class="col-lg-6 text-end">

                        <asp:Button
                            ID="btnRefreshRoutes"
                            runat="server"
                            CssClass="btn btn-outline-success me-2"
                            Text="Refresh"
                            OnClick="btnRefreshRoutes_Click" />

                        <asp:Button
                            ID="btnExportRoutes"
                            runat="server"
                            CssClass="btn btn-success"
                            Text="Export Excel"
                            OnClick="btnExportRoutes_Click" />

                    </div>

                </div>

            </div>

            <!--====================================================
                    ROUTE FILTER
=====================================================-->

            <div class="filter-card mb-4">

                <div class="card-header-custom">

                    <div class="d-flex justify-content-between align-items-center">

                        <div>

                            <h4>

                                <i class="fa-solid fa-filter me-2"></i>

                                Route Selection

                            </h4>

                            <small>Select Authority and Route to load vehicle details.

                            </small>

                        </div>

                        <span class="badge bg-success fs-6">Step 1

                        </span>

                    </div>

                </div>

                <div class="card-body">

                    <div class="row g-4">



                        <div class="col-xl-3 col-lg-4">

                            <label class="form-label">
                                Authority

                            </label>

                            <asp:DropDownList
                                ID="ddlAuthority"
                                runat="server"
                                CssClass="form-select"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlAuthority_SelectedIndexChanged">

                                <asp:ListItem Value="">

                        -- Select Authority --

                                </asp:ListItem>

                                <asp:ListItem Value="STA_PERMIT">

                        STA Permit

                                </asp:ListItem>

                                <asp:ListItem Value="STA_CS">

                        STA Counter Signature

                                </asp:ListItem>

                                <asp:ListItem Value="RTA_PERMIT">

                        RTA Permit

                                </asp:ListItem>

                            </asp:DropDownList>

                        </div>



                        <div class="col-xl-7 col-lg-8">

                            <label class="form-label">
                                Route

                            </label>
                            <div class="route-wrapper">
                                <asp:DropDownList
                                    ID="ddlRoute"
                                    runat="server"
                                    CssClass="form-select route-dropdown"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged">

                                    <asp:ListItem Value="">

-- Select Route --

                                    </asp:ListItem>

                                </asp:DropDownList>
                            </div>


                            <small class="text-muted">Long route names will automatically wrap.

                            </small>

                        </div>



                        <div class="col-xl-2 d-grid">

                            <label class="form-label text-white">
                                Action

                            </label>

                            <asp:Button
                                ID="btnLoadRoute"
                                runat="server"
                                Text="Load Details"
                                CssClass="btn btn-success btn-lg"
                                OnClick="btnLoadRoute_Click" />

                        </div>

                    </div>

                    <hr class="my-4" />



                    <div class="row">

                        <div class="col-lg-9">

                            <div class="filter-info">

                                <i class="fa-solid fa-circle-info text-primary me-2"></i>

                                After selecting a route, click

                    <strong>Load Details</strong>

                                to display the vehicles, permit information,
                    statistics and route summary.

                            </div>

                        </div>

                        <div class="col-lg-3 text-end">

                            <span class="badge bg-primary">Step 2

                            </span>

                        </div>

                    </div>

                </div>

            </div>

            <!--====================================================
                    ROUTE OVERVIEW
=====================================================-->

            <div class="route-overview-card mb-4">

                <div class="section-header">

                    <div class="d-flex justify-content-between align-items-center">

                        <div>

                            <h4>

                                <i class="fa-solid fa-map-location-dot me-2"></i>

                                Route Overview

                            </h4>

                            <small>Complete information about the selected route.

                            </small>

                        </div>

                        <span class="badge bg-primary fs-6">Step 3

                        </span>

                    </div>

                </div>

                <div class="card-body">

                    <!--==================================================
                    SUMMARY INFORMATION
        ===================================================-->

                    <div class="row g-4 mb-4">



                        <div class="col-xl-3 col-md-6">

                            <div class="route-info-box">

                                <div class="route-info-icon bg-success">

                                    <i class="fa-solid fa-building"></i>

                                </div>

                                <div class="route-info-title">
                                    Authority

                                </div>

                                <div class="route-info-value">

                                    <asp:Label
                                        ID="lblAuthorityName"
                                        runat="server"
                                        Text="-" />

                                </div>

                            </div>

                        </div>



                        <div class="col-xl-3 col-md-6">

                            <div class="route-info-box">

                                <div class="route-info-icon bg-primary">

                                    <i class="fa-solid fa-road"></i>

                                </div>

                                <div class="route-info-title">
                                    Route Number

                                </div>

                                <div class="route-info-value">

                                    <asp:Label
                                        ID="lblRouteNumber"
                                        runat="server"
                                        Text="-" />

                                </div>

                            </div>

                        </div>



                        <div class="col-xl-3 col-md-6">

                            <div class="route-info-box">

                                <div class="route-info-icon bg-warning">

                                    <i class="fa-solid fa-route"></i>

                                </div>

                                <div class="route-info-title">
                                    Distance

                                </div>

                                <div class="route-info-value">

                                    <asp:Label
                                        ID="lblDistance"
                                        runat="server"
                                        Text="-" />

                                    KM

                                </div>

                            </div>

                        </div>



                        <div class="col-xl-3 col-md-6">

                            <div class="route-info-box">

                                <div class="route-info-icon bg-danger">

                                    <i class="fa-solid fa-bus"></i>

                                </div>

                                <div class="route-info-title">
                                    Vacancies

                                </div>

                                <div class="route-info-value">

                                    <asp:Label
                                        ID="lblVacancy"
                                        runat="server"
                                        Text="0" />

                                </div>

                            </div>

                        </div>

                    </div>

                    <!--==================================================
                    ROUTE DESCRIPTION
        ===================================================-->

                    <div class="route-path-card">

                        <div class="route-path-title">

                            <i class="fa-solid fa-location-dot text-success me-2"></i>

                            Route Description

                        </div>

                        <div class="route-path-text">

                            <asp:Label
                                ID="lblRouteVia"
                                runat="server"
                                Text="Select a route to view the complete route description." />

                        </div>

                    </div>

                </div>

            </div>

            <!--====================================================
                VEHICLE STATISTICS
=====================================================-->

            <div class="summary-section mb-4">

                <div class="section-header">

                    <div class="d-flex justify-content-between align-items-center">

                        <div>

                            <h4>

                                <i class="fa-solid fa-chart-line me-2"></i>

                                Route Statistics

                            </h4>

                            <small>Live statistics for the selected route.

                            </small>

                        </div>

                        <span class="badge bg-success fs-6">Step 4

                        </span>

                    </div>

                </div>

                <div class="card-body">

                    <div class="row g-4">



                        <div class="col-xl-4 col-md-6">

                            <div class="kpi-card total">

                                <div class="kpi-icon">

                                    <i class="fa-solid fa-bus"></i>

                                </div>

                                <div class="kpi-content">

                                    <div class="kpi-title">
                                        Total Vehicles

                                    </div>

                                    <div class="kpi-value">

                                        <asp:Label
                                            ID="lblTotalVehicle"
                                            runat="server"
                                            Text="0" />

                                    </div>

                                </div>

                            </div>

                        </div>



                        <div class="col-xl-4 col-md-6">

                            <div class="kpi-card success">

                                <div class="kpi-icon">

                                    <i class="fa-solid fa-circle-check"></i>

                                </div>

                                <div class="kpi-content">

                                    <div class="kpi-title">
                                        Issued Permits

                                    </div>

                                    <div class="kpi-value">

                                        <asp:Label
                                            ID="lblRunning"
                                            runat="server"
                                            Text="0" />

                                    </div>

                                </div>

                            </div>

                        </div>



                        <div class="col-xl-4 col-md-6">

                            <div class="kpi-card danger">

                                <div class="kpi-icon">

                                    <i class="fa-solid fa-calendar-xmark"></i>

                                </div>

                                <div class="kpi-content">

                                    <div class="kpi-title">
                                        Expired Permits

                                    </div>

                                    <div class="kpi-value">

                                        <asp:Label
                                            ID="lblExpired"
                                            runat="server"
                                            Text="0" />

                                    </div>

                                </div>

                            </div>

                        </div>



                        <div class="col-xl-4 col-md-6">

                            <div class="kpi-card warning">

                                <div class="kpi-icon">

                                    <i class="fa-solid fa-file-signature"></i>

                                </div>

                                <div class="kpi-content">

                                    <div class="kpi-title">
                                        Counter Signature

                                    </div>

                                    <div class="kpi-value">

                                        <asp:Label
                                            ID="lblCounterSignature"
                                            runat="server"
                                            Text="0" />

                                    </div>

                                </div>

                            </div>

                        </div>



                        <div class="col-xl-4 col-md-6">

                            <div class="kpi-card info">

                                <div class="kpi-icon">

                                    <i class="fa-solid fa-road-circle-check"></i>

                                </div>

                                <div class="kpi-content">

                                    <div class="kpi-title">
                                        Active Vehicles

                                    </div>

                                    <div class="kpi-value">

                                        <asp:Label
                                            ID="lblActiveVehicle"
                                            runat="server"
                                            Text="0" />

                                    </div>

                                </div>

                            </div>

                        </div>



                        <div class="col-xl-4 col-md-6">

                            <div class="kpi-card purple">

                                <div class="kpi-icon">

                                    <i class="fa-solid fa-clock"></i>

                                </div>

                                <div class="kpi-content">

                                    <div class="kpi-title">
                                        Last Updated

                                    </div>

                                    <div class="kpi-value fs-5">

                                        <asp:Label
                                            ID="lblGridRefresh"
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
                VEHICLE SECTION
=====================================================-->

            <div class="vehicle-section mb-4">

                <!--==========================================
                HEADER
    ===========================================-->

                <div class="section-header">

                    <div class="d-flex justify-content-between align-items-center">

                        <div>

                            <h4>

                                <i class="fa-solid fa-bus me-2"></i>

                                Vehicle Details

                            </h4>

                            <small>Search vehicles by Registration Number or Permit Number.

                            </small>

                        </div>

                        <span class="badge bg-primary fs-6">Step 5

                        </span>

                    </div>

                </div>

                <!--==========================================
                TOOLBAR
    ===========================================-->

                <div class="vehicle-toolbar">

                    <div class="row g-3 align-items-end">



                        <div class="col-lg-3">

                            <label class="form-label">
                                Registration Number

                            </label>

                            <asp:TextBox
                                ID="txtVehicleNo"
                                runat="server"
                                CssClass="form-control"
                                placeholder="e.g.JH01AB1234" />

                        </div>



                        <div class="col-lg-3">

                            <label class="form-label">
                                Permit Number

                            </label>

                            <asp:TextBox
                                ID="txtPermitNo"
                                runat="server"
                                CssClass="form-control"
                                placeholder="Permit Number" />

                        </div>



                        <div class="col-lg-2">

                            <label class="form-label">
                                Records

                            </label>

                            <asp:DropDownList
                                ID="ddlPageSize"
                                runat="server"
                                CssClass="form-select"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">

                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem Selected="True">20</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>

                            </asp:DropDownList>

                        </div>



                        <div class="col-lg-4 text-end">

                            <asp:Button
                                ID="btnSearchVehicle"
                                runat="server"
                                Text="Search"
                                CssClass="btn btn-success"
                                OnClick="btnSearchVehicle_Click" />

                            <asp:Button
                                ID="btnResetVehicle"
                                runat="server"
                                Text="Reset"
                                CssClass="btn btn-outline-secondary"
                                OnClick="btnResetVehicle_Click" />

                            <asp:Button
                                ID="btnExportExcel"
                                runat="server"
                                Text="Excel"
                                CssClass="btn btn-primary"
                                OnClick="btnExportRoutes_Click" />

                        </div>

                    </div>

                </div>

                <!--==========================================
            RECORD INFORMATION
    ===========================================-->

                <div class="vehicle-toolbar-footer">

                    <div class="row">

                        <div class="col-md-6">

                            <div class="record-info">
                                Showing

                    <strong>

                        <asp:Label
                            ID="lblShowingFrom"
                            runat="server"
                            Text="0" />

                    </strong>

                                -

                    <strong>

                        <asp:Label
                            ID="lblShowingTo"
                            runat="server"
                            Text="0" />

                    </strong>

                                of

                    <strong>

                        <asp:Label
                            ID="lblTotalRecords"
                            runat="server"
                            Text="0" />

                    </strong>

                                Records

                            </div>

                        </div>

                        <div class="col-md-6 text-end">

                            <div class="record-time">
                                Last Refresh :

                    <asp:Label
                        ID="lblLastRefreshGrid"
                        runat="server"
                        Text="--" />

                            </div>

                        </div>

                    </div>

                </div>

                <!--====================================================
                    VEHICLE GRID
=====================================================-->

                <div class="vehicle-grid">

                    <asp:GridView
                        ID="GridView1"
                        runat="server"
                        AutoGenerateColumns="False"
                        AllowPaging="True"
                        PageSize="20"
                        DataKeyNames="APP_ID"
                        OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowCommand="GridView1_RowCommand">

                        <Columns>



                            <asp:TemplateField HeaderText="#">

                                <ItemStyle Width="60px" HorizontalAlign="Center" />

                                <ItemTemplate>

                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>

                            </asp:TemplateField>



                            <asp:BoundField
                                HeaderText="Registration No"
                                DataField="REGISTRATION_NUMBER" />



                            <asp:BoundField
                                HeaderText="Permit Number"
                                DataField="PERMIT_NUMBER" />



                            <asp:BoundField
                                HeaderText="Owner Name"
                                DataField="OWNER_NAME" />


                            <asp:TemplateField HeaderText="UP Journeys">

                                <ItemStyle Width="260px" />

                                <ItemTemplate>

                                    <div class="journey-list up">

                                        <asp:Literal
                                            ID="litUpJourney"
                                            runat="server"
                                            Text='<%# Eval("UP_JOURNEYS") %>'>
                                        </asp:Literal>

                                    </div>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="DOWN Journeys">

                                <ItemStyle Width="260px" />

                                <ItemTemplate>

                                    <div class="journey-list down">

                                        <asp:Literal
                                            ID="litDownJourney"
                                            runat="server"
                                            Text='<%# Eval("DOWN_JOURNEYS") %>'>
                                        </asp:Literal>

                                    </div>

                                </ItemTemplate>

                            </asp:TemplateField>


                            <asp:BoundField
                                HeaderText="Permit Valid"
                                DataField="PERMIT_VALID_UPTO"
                                DataFormatString="{0:dd-MMM-yyyy}" />



                            <asp:TemplateField HeaderText="Status">

                                <ItemStyle HorizontalAlign="Center" />

                                <ItemTemplate>

                                    <span class='<%# Eval("APPLICATION_STATUS").ToString()=="Issued"
                                        ? "badge bg-success"
                                        : "badge bg-warning text-dark" %>'>

                                        <%# Eval("APPLICATION_STATUS") %>

                                    </span>

                                </ItemTemplate>

                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Action">

                                <ItemStyle Width="90px" HorizontalAlign="Center" />

                                <ItemTemplate>

                                    <asp:LinkButton
                                        ID="btnView"
                                        runat="server"
                                        CssClass="btn btn-sm btn-outline-primary"
                                        CommandName="View"
                                        CommandArgument='<%# Eval("APP_ID") %>'>

                        <i class="fa fa-eye"></i>

                                    </asp:LinkButton>

                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                        <PagerStyle CssClass="d-none" />

                    </asp:GridView>

                </div>

                <!--====================================================
                    GRID FOOTER
=====================================================-->

                <div class="grid-footer">

                    <div class="row align-items-center">



                        <div class="col-lg-4">

                            <div class="footer-records">
                                Total Records :

                <strong>

                    <asp:Label
                        ID="lblTotalRecordsFooter"
                        runat="server"
                        Text="0" />

                </strong>

                            </div>

                        </div>



                        <div class="col-lg-4">

                            <div class="pagination-wrapper">

                                <asp:Button
                                    ID="btnPrevious"
                                    runat="server"
                                    CssClass="btn btn-outline-success"
                                    Text="Previous"
                                    OnClick="btnPrevious_Click" />

                                <span class="page-number">Page

                    <asp:Label
                        ID="lblCurrentPage"
                        runat="server"
                        Text="1" />

                                    of

                    <asp:Label
                        ID="lblTotalPages"
                        runat="server"
                        Text="1" />

                                </span>

                                <asp:Button
                                    ID="btnNext"
                                    runat="server"
                                    CssClass="btn btn-outline-success"
                                    Text="Next"
                                    OnClick="btnNext_Click" />

                            </div>

                        </div>



                        <div class="col-lg-4 text-end">

                            <div class="last-update">
                                Last Updated :

                <asp:Label
                    ID="Label1"
                    runat="server"
                    Text="--" />

                            </div>

                        </div>

                    </div>

                </div>

            </div>

            <!--====================================================
                    LOADING PANEL
=====================================================-->

            <div
                id="loadingPanel"
                class="loading-overlay"
                style="display: none;">

                <div class="loading-card">

                    <div class="spinner-border text-success mb-3">
                    </div>

                    <h5>Loading Route Details...

                    </h5>

                    <div class="text-muted">
                        Please wait...

                    </div>

                </div>

            </div>

            <!--====================================================
                    FOOTER
=====================================================-->

            <footer class="page-footer">

                <div class="container-fluid">

                    <div class="row">

                        <div class="col-lg-6">
                            ©

                <%= DateTime.Now.Year %>

                Government of Jharkhand

                | Transport Department

                        </div>

                        <div class="col-lg-6 text-end">
                            Online State Permit Management System

                        </div>

                    </div>

                </div>

            </footer>

        </div>

    </form>



    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>



    <script src="https://cdn.jsdelivr.net/npm/jquery@3.7.1/dist/jquery.min.js"></script>

    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0/dist/js/select2.min.js"></script>

    <script>

        function ShowLoader() {

            document.getElementById("loadingPanel").style.display = "flex";

        }

        function HideLoader() {

            document.getElementById("loadingPanel").style.display = "none";

        }

        $(function () {



            $('#<%= ddlRoute.ClientID %>').select2({
                width: '100%',
                placeholder: '🔍 Search or Select Route',
                allowClear: true
            });

        });

    </script>

</body>

</html>
