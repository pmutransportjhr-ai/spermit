<%@ Page Language="C#" MasterPageFile="MasterPage.master"
    AutoEventWireup="true"
    CodeFile="Edit-Form-CS.aspx.cs"
    Inherits="Edit_Form_CS" %>

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
        }

            .step-item.personal.active {
                background: #0d6efd;
                color: #fff;
            }

            .step-item.vehicle.active {
                background: #6c757d;
                color: #fff;
            }

            .step-item.route.active {
                background: #212529;
                color: #fff;
            }

            .step-item.other.active {
                background: #ffc107;
                color: #000;
            }

            .step-item.tax.active {
                background: #6c757d;
                color: #fff;
            }


        .readonly {
            background: #f8f9fa;
        }

        .table-scroll {
            width: 100%;
            overflow-x: auto;
        }

        .action-bar {
            position: sticky;
            bottom: 15px;
            display: flex;
            justify-content: flex-end;
            gap: 10px;
            padding: 10px;
            background: rgba(255,255,255,.95);
            border-top: 1px solid #dee2e6;
            z-index: 10;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

        <asp:HiddenField ID="hdnID" runat="server" />

        <!-- ================= HEADER ================= -->
        <nav class="breadcrumb mb-3">
            <a class="breadcrumb-item" href="STA-DASHBOARD.aspx">Home</a>
            <span class="breadcrumb-item active">Counter Signature Application</span>
        </nav>

        <h5 class="fw-bold mb-3">Counter Signature Application</h5>

        <!-- ================= STEP BAR ================= -->
        <div class="step-bar">
            <div class="step-item personal active">Personal</div>
            <div class="step-item vehicle">Vehicle</div>
            <div class="step-item route">Route</div>
            <div class="step-item other">Other</div>
            <div class="step-item tax">Tax Details</div>
        </div>


        <!-- ================= APPLICATION INFO ================= -->
        <div class="card bg-info-subtle mb-4">
            <div class="card-body row">
                <div class="col-md-6">
                    <b>Application No :</b>
                    <asp:Label ID="lblApplicationNo" runat="server" />
                </div>
                <div class="col-md-6 text-end">
                    <b>Status :</b>
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

        <!-- ================= STEP 1 : PERSONAL ================= -->
        <div class="step active">
            <div class="card mb-4">
                <div class="card-header fw-bold">Personal Information</div>
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
                        <asp:TextBox ID="txtPresentAddress" runat="server" CssClass="form-control" TextMode="MultiLine" />
                    </div>
                    <div class="col-md-6">
                        <label>Permanent Address</label>
                        <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control" TextMode="MultiLine" />
                    </div>
                </div>
            </div>
        </div>

        <!-- ================= STEP 2 : VEHICLE ================= -->
        <div class="step">
            <div class="card mb-4">
                <div class="card-header fw-bold">Vehicle Details</div>
                <div class="card-body table-responsive">
                    <table class="table table-bordered">

                        <tr>
                            <th>Registration No</th>
                            <td>
                                <asp:TextBox ID="VORegNumber" runat="server" CssClass="form-control" />
                            </td>
                            <th>Permit No</th>
                            <td>
                                <asp:TextBox ID="VOPermitNo" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                        <tr>
                            <th>Counter Signature No</th>
                            <td>
                                <asp:TextBox ID="txtCounterSignatureNumber" runat="server" CssClass="form-control" />
                            </td>
                            <th>Sleeper Capacity</th>
                            <td>
                                <asp:TextBox ID="txtSleeperCapacity" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                        <tr>
                            <th>Permit Valid From</th>
                            <td>
                                <asp:TextBox ID="VoPermitFrom" runat="server" CssClass="form-control" />
                            </td>
                            <th>Permit Valid Upto</th>
                            <td>
                                <asp:TextBox ID="VoPermitUpto" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                        <tr>
                            <th>Maker Description</th>
                            <td>
                                <asp:TextBox ID="lblMakerDesc" runat="server" CssClass="form-control" />
                            </td>
                            <th>Maker Model</th>
                            <td>
                                <asp:TextBox ID="lblMakermodel" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                        <tr>
                            <th>Vehicle Type</th>
                            <td>
                                <asp:TextBox ID="VType" runat="server" CssClass="form-control" />
                            </td>
                            <th>Chassis No</th>
                            <td>
                                <asp:TextBox ID="VChassisNumber" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                        <tr>
                            <th>Engine No</th>
                            <td>
                                <asp:TextBox ID="VEngineNumber" runat="server" CssClass="form-control" />
                            </td>
                            <th>Seating Capacity</th>
                            <td>
                                <asp:TextBox ID="VSeatCapacity" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                        <tr>
                            <th>ULW</th>
                            <td>
                                <asp:TextBox ID="VGVW" runat="server" CssClass="form-control" />
                            </td>
                            <th>RLW</th>
                            <td>
                                <asp:TextBox ID="VPayLoad" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                        <tr>
                            <th>Manufacturing Date</th>
                            <td>
                                <asp:TextBox ID="VManufacturingON" runat="server" CssClass="form-control" />
                            </td>
                            <th>Nature of Goods</th>
                            <td>
                                <asp:TextBox ID="VNatureOfGoods" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                        <tr>
                            <th>Insurance Valid Upto</th>
                            <td>
                                <asp:TextBox ID="txtICValidUpto" runat="server" CssClass="form-control" />
                            </td>
                            <th>Pollution Valid Upto</th>
                            <td>
                                <asp:TextBox ID="txtPCValidUpto" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                        <tr>
                            <th>Fitness Valid Upto</th>
                            <td>
                                <asp:TextBox ID="txtFCValidUpto" runat="server" CssClass="form-control" />
                            </td>
                            <th>Tax Paid Upto</th>
                            <td>
                                <asp:TextBox ID="txtTPValidUpto" runat="server" CssClass="form-control" />
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
        </div>

        <!-- ================= STEP 3 : ROUTE ================= -->
        <div class="step">
            <div class="card mb-4">
                <div class="card-header fw-bold">Route Details</div>
                <div class="card-body">
                    <b>Issued By :</b>
                    <asp:Label ID="lblIssuedBy" runat="server" CssClass="form-control mb-2" />
                    <br />
                    <b>Route :</b>
                    <asp:Label ID="RouteHDRInfo" runat="server" CssClass="form-control mb-2" />
                    <br />
                    Time Format : 24 Hours
                        <div class="table-scroll mt-2">
                            <div id="RouteTimeInfo" runat="server"></div>
                        </div>
                </div>
            </div>
        </div>

        <!-- ================= STEP 4 : OTHER ================= -->
        <div class="step">
            <div class="card mb-4">
                <div class="card-header fw-bold">Other Details</div>
                <div class="card-body table-responsive">
                    <table class="table table-bordered">
                        <tr>
                            <th>Mobile No</th>
                            <td>
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" /></td>
                            <th>Email</th>
                            <td>
                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" /></td>
                        </tr>
                        <tr>
                            <th>Recommendation No</th>
                            <td>
                                <asp:TextBox ID="txtRecommendationNo" runat="server" CssClass="form-control" /></td>
                            <th>Recommendation Date</th>
                            <td>
                                <asp:TextBox ID="txtRecommendationDate" runat="server" CssClass="form-control" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>


        <!-- ================= STEP 5 : TAX DETAILS ================= -->

        <div class="step">
            <div class="card mb-4">
                <div class="card-header fw-bold">Tax Details</div>
                <div class="card-body table-responsive">

                    <table class="table table-bordered align-middle">

                        <tr>
                            <th>Tax Paid Amount</th>
                            <td>
                                <asp:TextBox ID="txttaxpayAmount"
                                    CssClass="form-control"
                                    runat="server"
                                    MaxLength="10">
                                </asp:TextBox>
                            </td>

                            <th>Tax Period</th>
                            <td>
                                <asp:DropDownList ID="ddltaxperiod"
                                    runat="server"
                                    CssClass="form-select">
                                    <asp:ListItem Value="q">Quarterly</asp:ListItem>
                                    <asp:ListItem Value="h">Half Yearly</asp:ListItem>
                                    <asp:ListItem Value="y">Yearly</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <th>Tax Paid From</th>
                            <td>
                                <div class="input-group">
                                    <input type="text"
                                        id="txtTAXpaidfrom"
                                        class="form-control"
                                        runat="server" />
                                    <button class="btn btn-outline-secondary" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </div>
                            </td>

                            <th>Tax Paid Upto</th>
                            <td>
                                <div class="input-group">
                                    <input type="text"
                                        id="txtTAXpaidupto"
                                        class="form-control"
                                        runat="server" />
                                    <button class="btn btn-outline-secondary" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </div>
                            </td>
                        </tr>

                        <tr>
                            <th>Tax Reference No <span class="text-danger">*</span>
                            </th>
                            <td>
                                <asp:TextBox ID="txttaxRefNo"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>
                            </td>

                            <th>Tax Paid On <span class="text-danger">*</span>
                            </th>
                            <td>
                                <div class="input-group">
                                    <input type="text"
                                        id="txttaxRefDated"
                                        class="form-control"
                                        runat="server" />
                                    <button class="btn btn-outline-secondary" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </div>
                            </td>
                        </tr>

                    </table>

                </div>
            </div>
        </div>

        <!-- ================= ACTION BAR ================= -->
        <div class="action-bar">
            <button type="button" class="btn btn-secondary" onclick="prevStep()">Previous</button>
            <button type="button" class="btn btn-primary" onclick="nextStep()">Next</button>
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success"
                Text="Update" OnClick="btnUpdate_Click" Style="display: none" />
        </div>

    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        let step = 0;
        const steps = document.querySelectorAll('.step');
        const stepItems = document.querySelectorAll('.step-item');

        const btnUpdate = document.getElementById("<%= btnUpdate.ClientID %>");

        function showStep(i) {
            steps.forEach(s => s.classList.remove('active'));
            stepItems.forEach(s => s.classList.remove('active'));

            steps[i].classList.add('active');
            stepItems[i].classList.add('active');

            btnUpdate.style.display = (i === steps.length - 1) ? "inline-block" : "none";
        }

        function nextStep() { if (step < steps.length - 1) showStep(++step); }
        function prevStep() { if (step > 0) showStep(--step); }

        showStep(step);

    </script>
</asp:Content>
