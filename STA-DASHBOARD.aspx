<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="STA-DASHBOARD.aspx.cs"
    MasterPageFile="MasterPage.master"
    Inherits="STA_DASHBOARD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        /* ================= DASHBOARD CARDS ================= */
        .stat-card {
            background: var(--bg-card);
            border-radius: 16px;
            box-shadow: 0 10px 25px rgba(0,0,0,.08); /* Adjusted shadow */
            transition: transform .3s ease, box-shadow .3s ease;
            height: 100%;
        }

            .stat-card:hover {
                transform: translateY(-4px);
                box-shadow: 0 18px 40px rgba(0,0,0,.15);
            }

        .stat-header {
            padding: 14px 18px; /* Slightly increased padding */
            font-weight: 700;
            color: #fff;
            font-size: 18px;
            border-radius: 16px 16px 0 0;
            text-shadow: 0 1px 2px rgba(0,0,0,0.2); /* Subtle text shadow for depth */
        }

        .stat-body {
            padding: 16px;
            font-size: 15px;
        }

        .stat-row {
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px; /* Increased margin */
            align-items: center;
        }

        .stat-value {
            font-weight: 700;
            font-size: 18px;
            cursor: pointer;
            color: var(--brand-green-dark); /* Using dark green for important values */
        }

        /* header colors - KEPT for Old Design Consistency */
        .bg-m {
            background: #e31838; /* Red/Monthly */
        }

        .bg-q {
            background: #006884; /* Teal/Quarterly */
        }

        .bg-h {
            background: #ee7936; /* Orange/Half-Yearly */
        }

        .bg-y {
            background: #de5b8d; /* Pink/Yearly */
        }

        .section-title {
            text-align: center;
            font-weight: 700;
            font-size: 20px;
            margin-bottom: 15px;
            color: var(--text-dark);
        }

        /* table wrapper */
        .table-card {
            background: var(--bg-card);
            border-radius: 12px; /* Smoother border radius */
            box-shadow: 0 5px 15px rgba(0,0,0,.05);
            padding: 16px;
        }
        
        /* Table Styling */
        .table {
            border-radius: 10px;
            overflow: hidden; /* Important for border-radius on tables */
            border-collapse: separate !important;
            border-spacing: 0;
        }
        
        .table thead th {
            font-weight: 600;
            color: var(--text-dark);
        }
        
        .table-bordered {
            border: 1px solid #dee2e6;
        }
        
        .table-hover tbody tr:hover {
            background-color: var(--link-hover-bg) !important;
        }

        /* pagination buttons */
        .pager {
            display: flex;
            justify-content: space-between;
            margin-top: 20px;
        }
        
        .pager .btn-outline-secondary {
            border-color: var(--brand-green-dark);
            color: var(--brand-green-dark);
            transition: .2s;
        }
        
        .pager .btn-outline-secondary:hover:not(:disabled) {
            background-color: var(--brand-green-dark);
            color: #fff;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- Breadcrumb -->
    <nav class="mb-4">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="STA-DASHBOARD.aspx">Home</a></li>
            <li class="breadcrumb-item active">Dashboard</li>
        </ol>
    </nav>

    <!-- ================= TOP CARDS ================= -->
    <div class="row g-4 mb-5" id="dvTopBand" runat="server">

        <div class="col-xl-3 col-md-6">
            <div class="stat-card">
                <div class="stat-header bg-m">Monthly</div>
                <div class="stat-body">
                    <div class="stat-row">Total Revenue <span id="MRevenue" runat="server" onclick="fnViewRevenue('M')" class="stat-value">0</span></div>
                    <div class="stat-row">Permit Issued <span id="MPTIssued" runat="server" onclick="fnViewPTIssued('M')" class="stat-value">0</span></div>
                    <div class="stat-row">Pending Applications <span id="MPTApp" runat="server" onclick="fnViewPTApp('M')" class="stat-value">0</span></div>
                </div>
            </div>
        </div>

        <div class="col-xl-3 col-md-6">
            <div class="stat-card">
                <div class="stat-header bg-q">Quarterly</div>
                <div class="stat-body">
                    <div class="stat-row">Total Revenue <span id="QRevenue" runat="server" onclick="fnViewRevenue('Q')" class="stat-value">0</span></div>
                    <div class="stat-row">Permit Issued <span id="QPTIssued" runat="server" onclick="fnViewPTIssued('Q')" class="stat-value">0</span></div>
                    <div class="stat-row">Pending Applications <span id="QPTApp" runat="server" onclick="fnViewPTApp('Q')" class="stat-value">0</span></div>
                </div>
            </div>
        </div>

        <div class="col-xl-3 col-md-6">
            <div class="stat-card">
                <div class="stat-header bg-h">Half Yearly</div>
                <div class="stat-body">
                    <div class="stat-row">Total Revenue <span id="HRevenue" runat="server" onclick="fnViewRevenue('H')" class="stat-value">0</span></div>
                    <div class="stat-row">Permit Issued <span id="HPTIssued" runat="server" onclick="fnViewPTIssued('H')" class="stat-value">0</span></div>
                    <div class="stat-row">Pending Applications <span id="HPTApp" runat="server" onclick="fnViewPTApp('H')" class="stat-value">0</span></div>
                </div>
            </div>
        </div>

        <div class="col-xl-3 col-md-6">
            <div class="stat-card">
                <div class="stat-header bg-y">Yearly</div>
                <div class="stat-body">
                    <div class="stat-row">Total Revenue <span id="YRevenue" runat="server" onclick="fnViewRevenue('Y')" class="stat-value">0</span></div>
                    <div class="stat-row">Permit Issued <span id="YPTIssued" runat="server" onclick="fnViewPTIssued('Y')" class="stat-value">0</span></div>
                    <div class="stat-row">Pending Applications <span id="YPTApp" runat="server" onclick="fnViewPTApp('Y')" class="stat-value">0</span></div>
                </div>
            </div>
        </div>

    </div>

    <!-- ================= TABLE HEADERS ================= -->
    <div class="row mb-3" id="dvMainTableContentHeader" runat="server">
        <div class="col-md-6 section-title">List of Applications to be verify</div>
        <div class="col-md-6 section-title">List of Applications to be approve</div>
    </div>

    <!-- ================= TABLES ================= -->
    <div class="row" id="dvMainTableContent" runat="server">

        <!-- VERIFY -->
        <div class="col-md-6">
            <asp:Panel ID="dvVerifySection" runat="server" Visible="false">
                <div class="table-card">
                    <table class="table table-bordered table-hover align-middle">
                        <thead class="table-light">
                            <tr>
                                <th>Application No.</th>
                                <th>Permit Type</th>
                                <th>Name</th>
                                <th>Registration No.</th>
                                <th>Status</th>
                                <th>Service</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <div id="FillSTAPermitApplication" runat="server"></div>
                        </tbody>
                    </table>

                    <div class="pager">
                        <asp:Button ID="btnVerifyPrev" runat="server" Text="Previous"
                            CssClass="btn btn-outline-secondary btn-sm"
                            OnClick="btnVerifyPrev_Click" />
                        <asp:Button ID="btnVerifyNext" runat="server" Text="Next"
                            CssClass="btn btn-outline-secondary btn-sm"
                            OnClick="btnVerifyNext_Click" />
                    </div>

                </div>
            </asp:Panel>
        </div>

        <!-- APPROVE -->
        <div class="col-md-6">
            <asp:Panel ID="dvApproveSection" runat="server" Visible="false">
                <div class="table-card">
                    <table class="table table-bordered table-hover align-middle">
                        <thead class="table-light">
                            <tr>
                                <th>Application No.</th>
                                <th>Permit No.</th>
                                <th>Permit Type</th>
                                <th>Name</th>
                                <th>Registration No.</th>
                                <th>Status</th>
                                <th>Service</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <div id="FillSTAPermitApplication1" runat="server"></div>
                        </tbody>
                    </table>

                    <div class="pager">
                        <asp:Button ID="btnApprovePrev" runat="server" Text="Previous"
                            CssClass="btn btn-outline-secondary btn-sm"
                            OnClick="btnApprovePrev_Click" />
                        <asp:Button ID="btnApproveNext" runat="server" Text="Next"
                            CssClass="btn btn-outline-secondary btn-sm"
                            OnClick="btnApproveNext_Click" />
                    </div>

                </div>
            </asp:Panel>
        </div>

    </div>

</asp:Content>
