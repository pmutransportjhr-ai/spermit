<%@ Page Title="Counter Signature Tax Payment for Other State"
    Language="C#"
    MasterPageFile="JHTRANSPORT.master"
    AutoEventWireup="true"
    CodeFile="STA_COUNTER_SIGNATURE_TAX_PAY.aspx.cs"
    Inherits="STA_COUNTER_SIGNATURE_TAX_PAY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="assets/global/plugins/datatables/datatables.min.css"
        rel="stylesheet" type="text/css" />

    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css"
        rel="stylesheet" type="text/css" />

    <style type="text/css">
        .verification-wrapper {
            max-width: 900px;
            margin: 0 auto;
        }

        .verification-card {
            border: 1px solid #e5e5e5;
            border-radius: 8px;
            background: #fff;
            box-shadow: 0 3px 15px rgba(0,0,0,.08);
            margin-bottom: 20px;
        }

        .verification-header {
            padding: 20px 25px;
            border-bottom: 1px solid #eee;
        }

            .verification-header h3 {
                margin: 0;
                font-weight: 600;
            }

            .verification-header p {
                margin: 7px 0 0;
                color: #777;
            }

        .verification-body {
            padding: 25px;
        }

        .verification-step {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
        }

        .step-number {
            width: 34px;
            height: 34px;
            line-height: 34px;
            text-align: center;
            border-radius: 50%;
            background: #36c6d3;
            color: #fff;
            font-weight: bold;
            margin-right: 12px;
        }

        .step-title {
            font-size: 17px;
            font-weight: 600;
        }

        .field-label {
            font-weight: 600;
            margin-bottom: 7px;
            display: block;
        }

        .required {
            color: #e7505a;
        }

        .input-help {
            color: #999;
            font-size: 12px;
            margin-top: 5px;
        }

        .details-card {
            background: #f8fafb;
            border: 1px solid #e6e9ec;
            border-radius: 7px;
            padding: 20px;
            margin-top: 20px;
        }

        .details-title {
            font-size: 17px;
            font-weight: 600;
            margin-bottom: 15px;
        }

        .detail-item {
            padding: 12px 15px;
            border-bottom: 1px solid #e8ebed;
        }

            .detail-item:last-child {
                border-bottom: none;
            }

        .detail-label {
            color: #777;
            font-size: 13px;
            display: block;
            margin-bottom: 3px;
        }

        .detail-value {
            font-size: 16px;
            font-weight: 600;
            color: #333;
        }

        .masked-mobile {
            letter-spacing: 1px;
        }

        .consent-box {
            margin-top: 20px;
            padding: 15px 18px;
            background: #fff;
            border: 1px solid #ddd;
            border-radius: 6px;
        }

            .consent-box label {
                font-weight: 500;
                margin-left: 7px;
                cursor: pointer;
            }

        .verification-success {
            padding: 18px;
            background: #f1f9f3;
            border: 1px solid #b8dfc1;
            border-radius: 6px;
            color: #27733b;
            margin-bottom: 20px;
        }

        .action-row {
            margin-top: 25px;
            text-align: center;
        }

        .btn-main {
            min-width: 170px;
        }

        @media(max-width:767px) {

            .verification-body {
                padding: 18px;
            }

            .action-row .btn {
                width: 100%;
                margin-bottom: 8px;
            }

            .detail-value {
                font-size: 15px;
            }
        }

        /* ============================================================
   APPLICATION NUMBER
   ============================================================ */

        .application-number-item {
            background: #f8fbfc;
            border: 1px solid #dfe8eb;
            border-radius: 8px;
            padding: 15px 18px;
            min-height: 82px;
        }

            .application-number-item .detail-label {
                display: block;
                color: #6b7785;
                font-size: 13px;
                font-weight: 600;
                margin-bottom: 7px;
            }

                .application-number-item .detail-label i {
                    margin-right: 6px;
                    color: #36c6d3;
                }

        .application-number-value {
            display: block;
            color: #1f2d3d;
            font-size: 20px;
            font-weight: 700;
            letter-spacing: .5px;
        }


        /* ============================================================
   APPLICATION REFERENCE NOTE
   ============================================================ */

        .application-reference-note {
            display: flex;
            align-items: center;
            min-height: 82px;
            padding: 14px 17px;
            background: #fffaf0;
            border: 1px solid #f0dfb5;
            border-radius: 8px;
        }

        .reference-icon {
            width: 38px;
            height: 38px;
            min-width: 38px;
            border-radius: 50%;
            background: #fff1c7;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-right: 12px;
        }

            .reference-icon i {
                color: #c58a00;
                font-size: 17px;
            }

        .reference-content {
            display: flex;
            flex-direction: column;
            line-height: 1.5;
        }

            .reference-content strong {
                color: #6b4d00;
                font-size: 14px;
                font-weight: 700;
                margin-bottom: 2px;
            }

            .reference-content span {
                color: #806f48;
                font-size: 12px;
            }


        /* ============================================================
   MOBILE
   ============================================================ */

        @media (max-width: 767px) {

            .application-number-item,
            .application-reference-note {
                margin-bottom: 12px;
            }

            .application-number-value {
                font-size: 18px;
            }
        }
    </style>

</asp:Content>


<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">

    <form id="FormID1"
        enctype="multipart/form-data"
        runat="server"
        autocomplete="off"
        method="post">

        <asp:Label ID="HiddenField1"
            runat="server"
            Visible="false" />

        <asp:Label ID="HiddenField2"
            runat="server"
            Visible="false" />

        <asp:Label ID="HiddenField3"
            runat="server"
            Visible="false" />

        <asp:ScriptManager ID="ScriptManager1"
            runat="server"
            EnablePartialRendering="true" />

        <div class="page-container">

            <div class="page-content-wrapper">

                <div class="page-content">

                    <div class="container">

                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="PermitHomePage.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>

                            <li>
                                <span>Counter Signature Tax Payment
                                </span>
                            </li>
                        </ul>


                        <div class="page-content-inner">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="verification-wrapper">

                                        <!-- PAGE HEADER -->
                                        <div class="verification-card">

                                            <div class="verification-header">

                                                <h3>
                                                    <i class="fa fa-shield"></i>
                                                    Vehicle Verification
                                                </h3>

                                                <p>
                                                    Verify your vehicle details before proceeding
                                                with Counter Signature Tax Payment.
                                                </p>

                                            </div>

                                        </div>


                                        <asp:UpdatePanel ID="UPpanel"
                                            runat="server">

                                            <ContentTemplate>


                                                <!-- MESSAGE -->
                                                <asp:Label
                                                    ID="Message"
                                                    runat="server"
                                                    Visible="false"
                                                    CssClass="alert alert-danger"
                                                    Style="display: block;">
                                                </asp:Label>


                                                <!-- STEP 1 -->
                                                <div class="verification-card"
                                                    id="VerificationInputCard"
                                                    runat="server">

                                                    <div class="verification-body">

                                                        <div class="verification-step">

                                                            <div class="step-number">
                                                                1
                                                            </div>

                                                            <div class="step-title">
                                                                Enter Vehicle Details
                                                            </div>

                                                        </div>


                                                        <div class="row">

                                                            <!-- REGISTRATION -->
                                                            <div class="col-md-6">

                                                                <div class="form-group">

                                                                    <label class="field-label">
                                                                        Vehicle Registration Number
                                                                    <span class="required">*</span>
                                                                    </label>

                                                                    <asp:TextBox
                                                                        ID="txtExistRegNo"
                                                                        runat="server"
                                                                        CssClass="form-control"
                                                                        MaxLength="20"
                                                                        placeholder="Enter Vehicle Registration Number">
                                                                    </asp:TextBox>

                                                                </div>

                                                            </div>


                                                            <!-- CHASSIS -->
                                                            <div class="col-md-6">

                                                                <div class="form-group">

                                                                    <label class="field-label">
                                                                        Chassis Number - Last 5 Characters
                                                                    <span class="required">*</span>
                                                                    </label>

                                                                    <asp:TextBox
                                                                        ID="txtChassisLast5"
                                                                        runat="server"
                                                                        CssClass="form-control"
                                                                        MaxLength="5"
                                                                        placeholder="Enter last 5 characters">
                                                                    </asp:TextBox>

                                                                    <div class="input-help">
                                                                        Enter only the last 5 characters
                                                                    of the vehicle chassis number.
                                                                    </div>

                                                                </div>

                                                            </div>

                                                        </div>


                                                        <div class="action-row">

                                                            <asp:Button
                                                                ID="btnFetchDetails"
                                                                runat="server"
                                                                CssClass="btn green btn-main"
                                                                Text="Fetch Vehicle Details"
                                                                OnClick="btnFetchDetails_Click"
                                                                OnClientClick="return validateVehicleSearch();" />

                                                            <asp:Button
                                                                ID="btnExistClear"
                                                                runat="server"
                                                                CssClass="btn default btn-main"
                                                                Text="Clear"
                                                                OnClick="btnExistClear_Click" />

                                                            <asp:Button
                                                                ID="btnCancel"
                                                                runat="server"
                                                                CssClass="btn red btn-main"
                                                                Text="Cancel"
                                                                OnClick="btnCancel_Click" />

                                                        </div>

                                                    </div>

                                                </div>


                                                <!-- STEP 2 -->
                                                <asp:Panel
                                                    ID="VerificationDetailsPanel"
                                                    runat="server"
                                                    Visible="false">

                                                    <div class="verification-card">

                                                        <div class="verification-body">

                                                            <div class="verification-step">

                                                                <div class="step-number">
                                                                    2
                                                                </div>

                                                                <div class="step-title">
                                                                    Confirm Vehicle Details
                                                                </div>

                                                            </div>


                                                            <div class="details-card">

                                                                <div class="details-title">
                                                                    <i class="fa fa-car"></i>
                                                                    Vehicle Details Found
                                                                </div>

                                                                <div class="row">

                                                                    <!-- APPLICATION NUMBER -->
                                                                    <div class="col-md-6">
                                                                        <div class="detail-item application-number-item">

                                                                            <span class="detail-label">
                                                                                <i class="fa fa-file-text-o"></i>
                                                                                Application Number
                                                                            </span>

                                                                            <span class="detail-value application-number-value">
                                                                                <asp:Label
                                                                                    ID="lblApplicationNumber"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>
                                                                    </div>

                                                                    <!-- REFERENCE NOTE -->
                                                                    <div class="col-md-6">
                                                                        <div class="application-reference-note">

                                                                            <div class="reference-icon">
                                                                                <i class="fa fa-info-circle"></i>
                                                                            </div>

                                                                            <div class="reference-content">
                                                                                <strong>Keep this Application Number</strong>
                                                                                <span>for future reference and payment-related queries.</span>
                                                                            </div>

                                                                        </div>
                                                                    </div>

                                                                </div>


                                                                <div class="row">

                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Vehicle Registration Number
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblVerifyRegNo"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>


                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Permit Number
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblVerifyPermitNo"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>


                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Owner Name
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblVerifyOwnerName"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>


                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Registered Mobile Number
                                                                            </span>

                                                                            <span class="detail-value masked-mobile">
                                                                                <asp:Label
                                                                                    ID="lblVerifyMobile"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>

                                                                </div>

                                                            </div>


                                                            <!-- CONSENT -->
                                                            <div class="consent-box">

                                                                <asp:CheckBox
                                                                    ID="chkConsent"
                                                                    runat="server"
                                                                    Text="I confirm that the above vehicle, permit and owner details are correct and I consent to proceed with the Counter Signature Tax Payment."
                                                                    onclick="toggleVerifyButton();" />

                                                            </div>


                                                            <div class="action-row">

                                                                <asp:Button
                                                                    ID="btnVerify"
                                                                    runat="server"
                                                                    CssClass="btn green btn-main"
                                                                    Text="Verify & Continue"
                                                                    Enabled="false"
                                                                    OnClick="btnVerify_Click"
                                                                    OnClientClick="return validateConsent();" />

                                                                <asp:Button
                                                                    ID="btnBack"
                                                                    runat="server"
                                                                    CssClass="btn default btn-main"
                                                                    Text="Change Details"
                                                                    OnClick="btnBack_Click" />

                                                            </div>

                                                        </div>

                                                    </div>

                                                </asp:Panel>


                                                <!-- STEP 3 -->
                                                <asp:Panel
                                                    ID="Panel1"
                                                    runat="server"
                                                    Visible="false">

                                                    <div class="verification-card">

                                                        <div class="verification-body">

                                                            <div class="verification-step">

                                                                <div class="step-number">
                                                                    3
                                                                </div>

                                                                <div class="step-title">
                                                                    Verification Successful
                                                                </div>

                                                            </div>


                                                            <div class="verification-success">

                                                                <i class="fa fa-check-circle"></i>

                                                                <strong>Vehicle verification completed successfully.
                                                                </strong>

                                                                <br />

                                                                You can now proceed with the tax payment.

                                                            </div>


                                                            <div class="details-card">

                                                                <div class="details-title">
                                                                    Permit Details
                                                                </div>


                                                                <div class="row">

                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Owner Name
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="VOName"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>




                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Permit Number
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblPermitNo"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>





                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Vehicle Registration Number
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="VORegNo"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>


                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Seating Capacity
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblSeatingCap"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>

                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Sleeper Capacity
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblsleepr"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>


                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Vehicle Type
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblvehicletype"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>


                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Valid From
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblValidFrom"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>


                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Valid Up To
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblValidUpto"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>


                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Last Payment From
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblPayFrom"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>


                                                                    <div class="col-md-6">

                                                                        <div class="detail-item">

                                                                            <span class="detail-label">Last Payment Up To
                                                                            </span>

                                                                            <span class="detail-value">
                                                                                <asp:Label
                                                                                    ID="lblPayUpto"
                                                                                    runat="server">
                                                                                </asp:Label>
                                                                            </span>

                                                                        </div>

                                                                    </div>

                                                                </div>

                                                            </div>


                                                            <div class="action-row">

                                                                <asp:Button
                                                                    ID="btnSave"
                                                                    runat="server"
                                                                    CssClass="btn green btn-main"
                                                                    Text="Make Payment"
                                                                    OnClick="btnSave_Click" />

                                                                <asp:Button
                                                                    ID="btnReset"
                                                                    runat="server"
                                                                    CssClass="btn default btn-main"
                                                                    Text="Start Again"
                                                                    OnClick="btnReset_Click" />

                                                            </div>

                                                        </div>

                                                    </div>

                                                </asp:Panel>


                                            </ContentTemplate>

                                            <Triggers>

                                                <asp:PostBackTrigger
                                                    ControlID="btnSave" />

                                            </Triggers>

                                        </asp:UpdatePanel>

                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    </form>

</asp:Content>


<asp:Content ID="Content3"
    ContentPlaceHolderID="ContentPlaceHolder2"
    runat="Server">

    <script type="text/javascript">
        function validateVehicleSearch() {

            var reg =
                $('#<%= txtExistRegNo.ClientID %>').val().trim();

            var chassis =
                $('#<%= txtChassisLast5.ClientID %>').val().trim();

            if (reg === '') {

                alert('Please enter Vehicle Registration Number.');

                $('#<%= txtExistRegNo.ClientID %>').focus();

                return false;
            }

            if (chassis === '') {

                alert('Please enter last 5 characters of Chassis Number.');

                $('#<%= txtChassisLast5.ClientID %>').focus();

                return false;
            }

            if (chassis.length !== 5) {

                alert('Please enter exactly last 5 characters of Chassis Number.');

                $('#<%= txtChassisLast5.ClientID %>').focus();

                return false;
            }

            $('#loadingmessage').modal('show');

            return true;
        }


        function toggleVerifyButton() {

            var checked =
                $('#<%= chkConsent.ClientID %> input:checkbox').is(':checked');

                                            if (!checked) {

                                                checked =
                                                    $('#<%= chkConsent.ClientID %>').is(':checked');

                                            }

                                            $('#<%= btnVerify.ClientID %>').prop('disabled', !checked);
        }


        function validateConsent() {

            var checked =
                $('#<%= chkConsent.ClientID %>').is(':checked');

            if (!checked) {

                alert(
                    'Please confirm that the fetched vehicle details are correct.'
                );

                return false;
            }

            $('#loadingmessage').modal('show');

            return true;
        }


        $(document).ready(function () {

            $('#<%= txtChassisLast5.ClientID %>').on('input', function () {

                                                this.value =
                                                    this.value.toUpperCase().replace(/[^A-Z0-9]/g, '');

                                            });

                                            toggleVerifyButton();

                                        });

    </script>

</asp:Content>

