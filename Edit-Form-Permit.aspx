<%@ Page Language="C#" MasterPageFile="MasterPage.master"
    AutoEventWireup="true"
    CodeFile="Edit-Form-Permit.aspx.cs"
    Inherits="Edit_Form_Permit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .step {
            display: none;
        }

            .step.active {
                display: block;
            }

        .step-bar {
            display: flex;
            gap: 8px;
            margin-bottom: 15px;
            flex-wrap: wrap;
        }

        .step-item {
            padding: 6px 14px;
            border-radius: 20px;
            background: #e9ecef;
            font-size: 13px;
            font-weight: 600;
            color: #000;
        }

            /* Step specific colors */
            .step-item.personal.active {
                background: #0d6efd; /* primary */
                color: #fff;
            }

            .step-item.vehicle.active {
                background: #6c757d; /* secondary */
                color: #fff;
            }

            .step-item.route.active {
                background: #212529; /* dark */
                color: #fff;
            }

            .step-item.other.active {
                background: #ffc107; /* warning */
                color: #000;
            }


        .readonly {
            background: #f8f9fa;
        }

        .table-scroll {
            width: 100%;
            overflow-x: auto;
            -webkit-overflow-scrolling: touch; /* smooth mobile scroll */
        }

            .table-scroll table {
                min-width: 800px; /* forces horizontal scroll on mobile */
            }

        .action-bar {
            position: sticky;
            bottom: 15px;
            display: flex;
            justify-content: flex-end;
            gap: 10px;
            padding: 10px;
            background: rgba(255, 255, 255, 0.95);
            border-top: 1px solid #dee2e6;
            z-index: 10;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

        <asp:HiddenField ID="hdnID" runat="server" />

        <!-- ================= BREADCRUMB ================= -->
        <nav class="breadcrumb mb-3">
            <a class="breadcrumb-item" href="STA-DASHBOARD.aspx">Home</a>
            <span class="breadcrumb-item active">Permit Application</span>
        </nav>

        <h5 class="fw-bold mb-3">Permit Application</h5>

        <!-- ================= STEP BAR ================= -->
        <div class="step-bar">
            <div class="step-item personal active">Personal</div>
            <div class="step-item vehicle">Vehicle</div>
            <div class="step-item route">Route</div>
            <div class="step-item other">Other</div>
        </div>


        <!-- ================= APPLICATION DETAILS (ALWAYS VISIBLE) ================= -->
        <div class="card border-1 bg-info-subtle bg-gradient mb-4">
            <div class="card-body  row">
                <div class="col-md-6">
                    <b>Application No :</b>
                    <asp:Label ID="lblApplicationNo" runat="server" />
                </div>
                <div class="col-md-6 text-end">
                    <b>Application Status :</b>
                    <asp:Label ID="lblApplicationStatus" runat="server" />
                </div>
                <div class="col-md-6">
                    <b>Permit Type :</b>
                    <asp:Label ID="lblPermitType" runat="server" />
                </div>
                <div class="col-md-6 text-end">
                    <b>Permit No :</b>
                    <asp:Label ID="lblPermitNo" runat="server" />
                </div>
            </div>
        </div>

        <!-- ================= STEP 1 : PERSONAL (EDITABLE) ================= -->
        <div class="step active">

            <div class="card border-primary-subtle border-1 mb-4">
                <div class="card-header fw-bold text-bg-primary-subtle">Personal Information</div>
                <div class="card-body row g-3">

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

                </div>
            </div>
        </div>

        <!-- ================= STEP 2 : VEHICLE (VIEW) ================= -->
        <div class="step">
            <div class="card border-secondary-subtle border-1 mb-4">
                <div class="card-header fw-bold text-bg-secondary-subtle">Vehicle Details</div>
                <div class="card-body table-responsive">
                    <table class="table table-bordered">
                        <tr>
                            <th>Registration No.</th>
                            <td>
                                <asp:TextBox ID="VORegNumber" runat="server" CssClass="form-control" />
                            </td>
                            <th>Vehicle Class Description</th>
                            <td>
                                <asp:TextBox ID="VType" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>Permit Valid From</th>
                            <td>
                                <asp:TextBox ID="txtPermitFrom" runat="server" CssClass="form-control" />
                            </td>
                            <th>Permit Valid Upto</th>
                            <td>
                                <asp:TextBox ID="txtPermitTo" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>CS Status</th>
                            <td>
                                <asp:DropDownList ID="ddlCSStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Not Verified">Not Verified</asp:ListItem>
                                    <asp:ListItem Value="Verified">Verified</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <th>Chassis No.</th>
                            <td>
                                <asp:TextBox ID="VChassisNumber" runat="server" CssClass="form-control" />
                            </td>
                            <th>Engine No.</th>
                            <td>
                                <asp:TextBox ID="VEngineNumber" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>Maker Description</th>
                            <td>
                                <asp:TextBox ID="lblMakerDesc" runat="server" CssClass="form-control" />
                            </td>
                            <th>Model Model No.</th>
                            <td>
                                <asp:TextBox ID="lblMakermodel" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>Fuel Description</th>
                            <td>
                                <asp:TextBox ID="lblFuelDesc" runat="server" CssClass="form-control" />
                            </td>
                            <th>Color</th>
                            <td>
                                <asp:TextBox ID="lblVcolor" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>Fitness Valid Upto</th>
                            <td>

                                <asp:TextBox ID="VFitness" runat="server" />


                            </td>

                            <th>Tax Paid Upto</th>
                            <td>
                                <asp:TextBox ID="VTax" runat="server" />

                            </td>
                        </tr>

                        <tr>
                            <th>ULW as per RC</th>
                            <td>
                                <asp:TextBox ID="VULW" runat="server" CssClass="form-control" />
                            </td>
                            <th>RLW as per RC</th>
                            <td>
                                <asp:TextBox ID="VRLW" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>Seating Capacity</th>
                            <td>
                                <asp:TextBox ID="VSeatCapacity" runat="server" CssClass="form-control" />
                            </td>
                            <th>Manufacturing Date</th>
                            <td>
                                <asp:TextBox ID="VManufacturingON" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                        <tr>
                            <th>Insurance Certificate Valid Upto</th>
                            <td>
                                <asp:TextBox ID="txtICValidUpto" runat="server" CssClass="form-control" />
                            </td>

                            <th>Pollution Certificate Valid Upto</th>
                            <td>
                                <asp:TextBox ID="txtPCValidUpto" runat="server" CssClass="form-control" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <!-- ================= STEP 3 : ROUTE ================= -->
        <div class="step">
            <div class="card border-dark-subtle border-1 mb-4">
                <div class="card-header fw-bold text-bg-dark-subtle">
                    Route Details
                </div>

                <div class="card-body">
                    <div class="fw-semibold mb-2">
                        <asp:Label ID="RouteHDRInfo" runat="server" />
                    </div>

                    <div class="mb-2">Time Format : 24 Hours</div>

                    <!-- 🔽 SCROLL WRAPPER -->
                    <div class="table-scroll">
                        <div id="RouteTimeInfo" runat="server"></div>
                    </div>
                </div>
            </div>
        </div>


        <!-- ================= STEP 4 : OTHER ================= -->
        <div class="step">
            <div class="card border-warning-subtle border-1 mb-4">
                <div class="card-header text-bg-warning-subtle fw-bold">
                    Other Details
                </div>

                <div class="card-body table-responsive">
                    <table class="table table-bordered">

                        <tr>
                            <th>Mobile</th>
                            <td>
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="10" />
                            </td>
                            <th>Email</th>
                            <td>
                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                        <tr>
                            <th>Gender</th>
                            <td>
                                <label class="me-3">
                                    <asp:RadioButton ID="txtMale" runat="server" GroupName="Gender" Text=" Male" />
                                </label>
                                <label>
                                    <asp:RadioButton ID="txtFemale" runat="server" GroupName="Gender" Text=" Female" />
                                </label>
                            </td>

                            <th>Qualification</th>
                            <td>
                                <asp:DropDownList ID="ddlQualification" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">-- Select --</asp:ListItem>
                                    <asp:ListItem>Master</asp:ListItem>
                                    <asp:ListItem>Bachelor</asp:ListItem>
                                    <asp:ListItem>Intermediate</asp:ListItem>
                                    <asp:ListItem>Matriculation</asp:ListItem>
                                    <asp:ListItem>Illiterate</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <th>Category of Applicant</th>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">-- Select --</asp:ListItem>
                                    <asp:ListItem Value="Gen">Gen</asp:ListItem>
                                    <asp:ListItem Value="SC">SC</asp:ListItem>
                                    <asp:ListItem Value="ST">ST</asp:ListItem>
                                    <asp:ListItem Value="OBC">OBC</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <th>Ex-Army</th>
                            <td>
                                <asp:RadioButton ID="txtExArmyY" runat="server" GroupName="ExArmy" Text=" Yes" />
                                <asp:RadioButton ID="txtExArmyN" runat="server" GroupName="ExArmy" Text=" No" />
                            </td>
                        </tr>

                        <tr>
                            <th>Minority</th>
                            <td>
                                <asp:RadioButton ID="txtMinorityY" runat="server" GroupName="Minority" Text=" Yes" />
                                <asp:RadioButton ID="txtMinorityN" runat="server" GroupName="Minority" Text=" No" />
                            </td>

                            <th>Handicapped</th>
                            <td>
                                <asp:RadioButton ID="txtHandicappedY" runat="server" GroupName="Handicapped" Text=" Yes" />
                                <asp:RadioButton ID="txtHandicappedN" runat="server" GroupName="Handicapped" Text=" No" />
                            </td>
                        </tr>

                        <tr>
                            <th>Heavy Motor Licence</th>
                            <td>
                                <asp:RadioButton ID="txtHMLY" runat="server" GroupName="HML" Text=" Yes" />
                                <asp:RadioButton ID="txtHMLN" runat="server" GroupName="HML" Text=" No" />
                            </td>

                            <th>Identity Proof</th>
                            <td>
                                <asp:RadioButton ID="txtIdentityProofV" runat="server" GroupName="IDProof" Text=" Voter Id" />
                                <asp:RadioButton ID="txtIdentityProofP" runat="server" GroupName="IDProof" Text=" Pan Card" />
                                <asp:RadioButton ID="txtIdentityProofA" runat="server" GroupName="IDProof" Text=" Aadhaar" />
                            </td>
                        </tr>

                        <tr>
                            <th>Identity Proof No.</th>
                            <td>
                                <asp:TextBox ID="txtIdentityNo" runat="server" CssClass="form-control" />
                            </td>
                            <td colspan="2"></td>
                        </tr>

                        <tr>
                            <th>Bus Type (AC)</th>
                            <td>
                                <asp:RadioButton ID="txtACY" runat="server" GroupName="AC" Text=" Yes" />
                                <asp:RadioButton ID="txtACN" runat="server" GroupName="AC" Text=" No" />
                            </td>

                            <th>Bus Type (Deluxe)</th>
                            <td>
                                <asp:RadioButton ID="txtDeluxeY" runat="server" GroupName="Deluxe" Text=" Yes" />
                                <asp:RadioButton ID="txtDeluxeN" runat="server" GroupName="Deluxe" Text=" No" />
                            </td>
                        </tr>

                        <tr>
                            <th>Bus Type (Sleeper)</th>
                            <td>
                                <asp:RadioButton ID="txtSleeperY" runat="server" GroupName="Sleeper" Text=" Yes" />
                                <asp:RadioButton ID="txtSleeperN" runat="server" GroupName="Sleeper" Text=" No" />
                            </td>
                            <td colspan="2"></td>
                        </tr>

                    </table>
                </div>
            </div>
        </div>

        <div class="border-primary-subtle border-2">
            <div class="action-bar">

                <button type="button" id="btnPrev" class="btn btn-secondary" onclick="prevStep()">Previous</button>
                <button type="button" id="btnNext" class="btn btn-primary" onclick="nextStep()">Next</button>

                <asp:Button ID="btnUpdate" runat="server"
                    Text="Update"
                    CssClass="btn btn-success"
                    OnClick="btnUpdate_Click"
                    Style="display: none;" />

            </div>
        </div>


    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <script>
        let step = 0;
        const steps = document.querySelectorAll('.step');
        const stepItems = document.querySelectorAll('.step-item');

        const btnNext = document.getElementById("btnNext");
        const btnPrev = document.getElementById("btnPrev");
        const btnUpdate = document.getElementById("<%= btnUpdate.ClientID %>");

        function showStep(i) {
            steps.forEach(s => s.classList.remove('active'));
            stepItems.forEach(s => s.classList.remove('active'));

            steps[i].classList.add('active');
            stepItems[i].classList.add('active');

            // Button visibility logic
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



        // Initialize
        showStep(step);
    </script>

</asp:Content>
