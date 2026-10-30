<%@ Page Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="STAGE-CARRIAGE-EDIT-FORM.aspx.cs" Inherits="STAGE_CARRIAGE_EDIT_FORM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Hide inactive steps */
        .step {
            display: none;
        }

            .step.active {
                display: block;
            }

        /* Modern Step Indicator */
        .step-bar {
            display: flex;
            gap: 10px;
            margin-bottom: 20px;
            border-bottom: 2px solid #ddd;
            padding-bottom: 10px;
        }

        .step-item {
            padding: 8px 20px;
            border-radius: 4px;
            background: #f8f9fa;
            color: #666;
            font-weight: 600;
            cursor: pointer;
            transition: 0.3s;
            border: 1px solid #dee2e6;
        }

            .step-item.active {
                background: #007bff;
                color: #fff;
                border-color: #007bff;
            }

        /* Form Spacing */
        .row > div {
            margin-bottom: 15px;
        }

        label {
            font-weight: 600;
            color: #333;
            margin-bottom: 5px;
        }

        /* Panel Styling */
        .panel-info {
            border-color: #bee5eb;
        }

            .panel-info .panel-body {
                background-color: #f8ffff;
            }

        /* Action Bar - Removing the 'Fixed' overlap issue */
        .form-actions {
            background: #fff;
            padding: 20px;
            border-top: 1px solid #dee2e6;
            margin-top: 30px;
            display: flex;
            justify-content: flex-end;
            gap: 10px;
        }

        /* Fixed footer handling */
        body {
            min-height: 100vh;
            display: flex;
            flex-direction: column;
        }

        .main-content {
            flex: 1;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid main-content" style="padding: 20px;">
        <form runat="server">

            <asp:HiddenField ID="hdnID" runat="server" />

            <!-- ================= BREADCRUMB ================= -->
            <nav class="breadcrumb mb-3">
                <a class="breadcrumb-item" href="RTA-DASHBOARD.aspx">Home</a>
                <span class="breadcrumb-item active">Stage Carrige Edit Form</span>
            </nav>

            <h5 class="fw-bold mb-3">RTA Stage Carrige Edit</h5>

            <!-- ================= STEP BAR ================= -->
            <div class="step-bar">
                <span class="step-item active">Personal</span>
                <span class="step-item">Vehicle</span>
                <span class="step-item">Route</span>
                <span class="step-item">Other</span>
            </div>


            <!-- ================= APPLICATION DETAILS (ALWAYS VISIBLE) ================= -->

            <div class="panel panel-info">
                <div class="panel-body">

                    <div class="row">
                        <div class="col-md-6">
                            <b>Application No :</b>
                            <asp:Label ID="lblApplicationNo" runat="server" />
                        </div>

                        <div class="col-md-6 text-right">
                            <b>Status :</b>
                            <asp:Label ID="lblApplicationStatus" runat="server" />
                        </div>

                        <div class="col-md-6">
                            <b>Permit Type :</b>
                            <asp:Label ID="lblPermitType" runat="server" />
                        </div>

                        <div class="col-md-6 text-right">
                            <b>Permit No :</b>
                            <asp:Label ID="lblPermitNo" runat="server" />
                        </div>
                    </div>

                </div>
            </div>

            <!-- ================= STEP 1 : PERSONAL ================= -->
            <div class="step active">
                <div class="panel panel-default">
                    <div class="panel-heading"><b>Personal Information</b></div>

                    <div class="panel-body">
                        <div class="row">

                            <div class="col-md-6">
                                <label>Name</label>
                                <asp:TextBox ID="txtOwnerName" runat="server" CssClass="form-control" />
                            </div>

                            <div class="col-md-6">
                                <label>C/O</label>
                                <asp:TextBox ID="txtCareOf" runat="server" CssClass="form-control" />
                            </div>

                            <div class="col-md-6">
                                <label>Present Address</label>
                                <asp:TextBox ID="txtPresentAddress" runat="server" TextMode="MultiLine" CssClass="form-control" />
                            </div>

                            <div class="col-md-6">
                                <label>Permanent Address</label>
                                <asp:TextBox ID="txtPermanentAddress" runat="server" TextMode="MultiLine" CssClass="form-control" />
                            </div>

                            <div class="col-md-12 text-right">
                                <button type="button" class="btn btn-default" onclick="copyAddress()">
                                    Copy Present → Permanent
                                </button>
                            </div>

                        </div>
                    </div>
                </div>
            </div>


            <!-- ================= STEP 2 : VEHICLE ================= -->
            <div class="step">

                <div class="panel panel-default">
                    <div class="panel-heading"><b>Vehicle Details</b></div>

                    <div class="panel-body">
                        <div class="row">

                            <!-- Registration -->
                            <div class="col-md-6">
                                <label>Registration No</label>
                                <asp:TextBox ID="VORegNumber" runat="server" CssClass="form-control" />
                            </div>

                            <!-- Vehicle Class -->
                            <div class="col-md-6">
                                <label>Vehicle Class</label>
                                <asp:TextBox ID="VType" runat="server" CssClass="form-control" />
                            </div>

                            <!-- Permit Dates -->
                            <div class="col-md-6">
                                <label>Permit From</label>
                                <asp:TextBox ID="txtPermitFrom" runat="server" TextMode="Date" CssClass="form-control" />
                            </div>

                            <div class="col-md-6">
                                <label>Permit Upto</label>
                                <asp:TextBox ID="txtPermitTo" runat="server" TextMode="Date" CssClass="form-control" />
                            </div>

                            <!-- Engine / Chassis -->
                            <div class="col-md-6">
                                <label>Chassis No</label>
                                <asp:TextBox ID="VChassisNumber" runat="server" CssClass="form-control" />
                            </div>

                            <div class="col-md-6">
                                <label>Engine No</label>
                                <asp:TextBox ID="VEngineNumber" runat="server" CssClass="form-control" />
                            </div>

                            <!-- Maker -->
                            <div class="col-md-6">
                                <label>Manufacturer</label>
                                <asp:TextBox ID="lblMakerDesc" runat="server" CssClass="form-control" />
                            </div>

                            <!-- Model -->
                            <div class="col-md-6">
                                <label>Model</label>
                                <asp:TextBox ID="lblMakermodel" runat="server" CssClass="form-control" />
                            </div>

                            <!-- Fuel -->
                            <div class="col-md-6">
                                <label>Fuel Type</label>
                                <asp:TextBox ID="lblFuelDesc" runat="server" CssClass="form-control" />
                            </div>

                            <!-- Color -->
                            <div class="col-md-6">
                                <label>Vehicle Color</label>
                                <asp:TextBox ID="lblVcolor" runat="server" CssClass="form-control" />
                            </div>

                            <!-- Validity -->
                            <div class="col-md-4">
                                <label>Fitness Valid Upto</label>
                                <asp:TextBox ID="VFitness" runat="server" TextMode="Date" CssClass="form-control" />
                            </div>

                            <div class="col-md-4">
                                <label>Tax Paid Upto</label>
                                <asp:TextBox ID="VTax" runat="server" TextMode="Date" CssClass="form-control" />
                            </div>

                            <div class="col-md-4">
                                <label>Seating Capacity</label>
                                <asp:TextBox ID="VSeatCapacity" runat="server" CssClass="form-control" />
                            </div>

                            <!-- Weight -->
                            <div class="col-md-6">
                                <label>ULW</label>
                                <asp:TextBox ID="VULW" runat="server" CssClass="form-control" />
                            </div>

                            <div class="col-md-6">
                                <label>RLW</label>
                                <asp:TextBox ID="VRLW" runat="server" CssClass="form-control" />
                            </div>

                        </div>
                    </div>
                </div>

            </div>

            <!-- ================= STEP 3 : ROUTE ================= -->
            <div class="step">
                <div class="panel panel-default">

                    <div class="panel-heading">
                        <b>Route Details</b>
                    </div>

                    <div class="panel-body">

                        <div class="well well-sm">
                            <asp:Label ID="RouteHDRInfo" runat="server" />
                        </div>

                        <p class="text-muted">Time Format: 24 Hours</p>

                        <div class="route-scroll">
                            <div id="RouteTimeInfo" runat="server"></div>
                        </div>

                    </div>
                </div>
            </div>


            <!-- ================= STEP 4 : OTHER ================= -->
            <div class="step">

                <div class="panel panel-default">
                    <div class="panel-heading"><b>Other Details</b></div>

                    <div class="panel-body">
                        <div class="row">

                            <!-- Mobile -->
                            <div class="col-md-6">
                                <label>Mobile Number</label>
                                <asp:TextBox ID="txtMobileNo" runat="server"
                                    CssClass="form-control" MaxLength="10" />
                            </div>

                            <!-- Email -->
                            <div class="col-md-6">
                                <label>Email</label>
                                <asp:TextBox ID="txtEmailId" runat="server"
                                    CssClass="form-control" />
                            </div>

                            <!-- Gender -->
                            <div class="col-md-6">
                                <label>Gender</label><br />
                                <asp:RadioButton ID="txtMale" runat="server" GroupName="Gender" Text=" Male" />
                                <asp:RadioButton ID="txtFemale" runat="server" GroupName="Gender" Text=" Female" />
                            </div>

                            <!-- Qualification -->
                            <div class="col-md-6">
                                <label>Qualification</label>
                                <asp:DropDownList ID="ddlQualification" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">-- Select --</asp:ListItem>
                                    <asp:ListItem>Master</asp:ListItem>
                                    <asp:ListItem>Bachelor</asp:ListItem>
                                    <asp:ListItem>Intermediate</asp:ListItem>
                                    <asp:ListItem>Matriculation</asp:ListItem>
                                    <asp:ListItem>Illiterate</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <!-- Category -->
                            <div class="col-md-6">
                                <label>Category</label>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">-- Select --</asp:ListItem>
                                    <asp:ListItem Value="Gen">Gen</asp:ListItem>
                                    <asp:ListItem Value="SC">SC</asp:ListItem>
                                    <asp:ListItem Value="ST">ST</asp:ListItem>
                                    <asp:ListItem Value="OBC">OBC</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <!-- Identity -->
                            <div class="col-md-6">
                                <label>Identity Proof No</label>
                                <asp:TextBox ID="txtIdentityNo" runat="server"
                                    CssClass="form-control" />
                            </div>

                            <!-- EX ARMY -->
                            <div class="col-md-6">
                                <label>Ex-Army</label><br />
                                <asp:RadioButton ID="txtExArmyY" runat="server" GroupName="ExArmy" Text=" Yes" />
                                <asp:RadioButton ID="txtExArmyN" runat="server" GroupName="ExArmy" Text=" No" />
                            </div>

                            <!-- MINORITY -->
                            <div class="col-md-6">
                                <label>Minority</label><br />
                                <asp:RadioButton ID="txtMinorityY" runat="server" GroupName="Minority" Text=" Yes" />
                                <asp:RadioButton ID="txtMinorityN" runat="server" GroupName="Minority" Text=" No" />
                            </div>

                            <!-- HANDICAPPED -->
                            <div class="col-md-6">
                                <label>Handicapped</label><br />
                                <asp:RadioButton ID="txtHandicappedY" runat="server" GroupName="Handicapped" Text=" Yes" />
                                <asp:RadioButton ID="txtHandicappedN" runat="server" GroupName="Handicapped" Text=" No" />
                            </div>

                            <!-- HML -->
                            <div class="col-md-6">
                                <label>Heavy Motor Licence</label><br />
                                <asp:RadioButton ID="txtHMLY" runat="server" GroupName="HML" Text=" Yes" />
                                <asp:RadioButton ID="txtHMLN" runat="server" GroupName="HML" Text=" No" />
                            </div>

                            <!-- AC -->
                            <div class="col-md-6">
                                <label>Air Conditioned</label><br />
                                <asp:RadioButton ID="txtACY" runat="server" GroupName="AC" Text=" Yes" />
                                <asp:RadioButton ID="txtACN" runat="server" GroupName="AC" Text=" No" />
                            </div>

                            <!-- DELUXE -->
                            <div class="col-md-6">
                                <label>Deluxe</label><br />
                                <asp:RadioButton ID="txtDeluxeY" runat="server" GroupName="Deluxe" Text=" Yes" />
                                <asp:RadioButton ID="txtDeluxeN" runat="server" GroupName="Deluxe" Text=" No" />
                            </div>

                            <!-- SLEEPER -->
                            <div class="col-md-6">
                                <label>Sleeper</label><br />
                                <asp:RadioButton ID="txtSleeperY" runat="server" GroupName="Sleeper" Text=" Yes" />
                                <asp:RadioButton ID="txtSleeperN" runat="server" GroupName="Sleeper" Text=" No" />
                            </div>

                        </div>
                    </div>
                </div>

            </div>

            <!-- ================= ACTION BAR ================= -->
            <div class="well text-right" style="margin-bottom: 70px;">

                <button type="button" id="btnPrev" class="btn btn-default" onclick="prevStep()">
                    ← Previous
                </button>

                <button type="button" id="btnNext" class="btn btn-primary" onclick="nextStep()">
                    Next →
                </button>

                <asp:Button ID="btnUpdate" runat="server"
                    Text="Update"
                    CssClass="btn btn-success"
                    OnClick="btnUpdate_Click"
                    Style="display: none;" />

            </div>

        </form>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <script>
        function copyAddress() {
            var present = document.getElementById('<%= txtPresentAddress.ClientID %>').value;
            document.getElementById('<%= txtPermanentAddress.ClientID %>').value = present;
        }
    </script>


    <script>
        var step = 0;
        var steps;
        var stepItems;
        var btnNext;
        var btnPrev;
        var btnUpdate;

        $(document).ready(function () {

            steps = document.querySelectorAll('.step');
            stepItems = document.querySelectorAll('.step-item');

            btnNext = document.getElementById("btnNext");
            btnPrev = document.getElementById("btnPrev");
            btnUpdate = document.getElementById("<%= btnUpdate.ClientID %>");

            showStep(step);
        });

        function showStep(i) {

            for (var s = 0; s < steps.length; s++) {
                steps[s].classList.remove('active');
                stepItems[s].classList.remove('active');
            }

            steps[i].classList.add('active');
            stepItems[i].classList.add('active');

            btnPrev.style.display = (i === 0) ? "none" : "inline-block";

            if (i === steps.length - 1) {
                btnNext.style.display = "none";
                btnUpdate.style.display = "inline-block";
            } else {
                btnNext.style.display = "inline-block";
                btnUpdate.style.display = "none";
            }
        }

        function nextStep() {
            if (step < steps.length - 1) {
                step++;
                showStep(step);
            }
        }

        function prevStep() {
            if (step > 0) {
                step--;
                showStep(step);
            }
        }
    </script>

</asp:Content>
