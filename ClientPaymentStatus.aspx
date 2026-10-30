<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="JHTRANSPORT.master" CodeFile="ClientPaymentStatus.aspx.cs" Inherits="ClientPaymentStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .errorClass {
            border-bottom-color: red;
        }

        .payment-history-container {
            margin-top: 25px;
        }

        .payment-history-table {
            width: 100%;
            margin-top: 15px;
            border-collapse: collapse;
        }

            .payment-history-table th,
            .payment-history-table td {
                padding: 10px;
                border: 1px solid #ddd;
                text-align: left;
                vertical-align: middle;
            }

            .payment-history-table th {
                background-color: #f5f5f5;
                font-weight: bold;
            }

            .payment-history-table tr.selected-payment {
                background-color: #eaf5ff;
            }

        .payment-history-message {
            margin-top: 12px;
            display: block;
        }

        .payment-history-actions {
            margin-top: 15px;
        }

        .payment-history-radio {
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                            <a href="ClientPaymentStatus.aspx">Check Payment Status</a>
                        </li>
                    </ul>

                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="portlet light portlet-fit">

                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">Check Payment Status
                                            </span>
                                        </div>
                                    </div>

                                    <div class="portlet-body">

                                        <form runat="server">

                                            <asp:ScriptManager ID="ScriptManager1"
                                                runat="server"
                                                ScriptMode="Release" />

                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <contenttemplate>

                                                    <input type="hidden"
                                                        name="requestparam"
                                                        id="hdnenc_val" />

                                                    <asp:HiddenField
                                                        ID="lblencyValue"
                                                        runat="server" />

                                                    <!-- Application Number -->
                                                    <div class="row">

                                                        <div class="col-md-2">
                                                            <b>Application Number</b>
                                                            <span style="color: red">*</span>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <input type="text"
                                                                id="txtApplicationNo"
                                                                class="form-control"
                                                                maxlength="150"
                                                                autocomplete="off" />

                                                            <span style="color: red; font-size: smaller; display: none;"
                                                                id="errorSpan">Please enter your Application Number
                                                            </span>
                                                        </div>

                                                        <div class="col-md-2">
                                                            <button type="button"
                                                                id="btnGetPaymentHistory"
                                                                class="btn btn-info"
                                                                onclick="getPaymentHistory();">
                                                                Get Payment History
                                                            </button>
                                                        </div>

                                                    </div>

                                                    <br />

                                                    <!-- Payment History -->
                                                    <div class="row payment-history-container"
                                                        id="paymentHistoryContainer"
                                                        style="display: none;">

                                                        <div class="col-md-12">

                                                            <h4>Payment History</h4>

                                                            <p>
                                                                Select the payment transaction for which you want
                                                                to check the payment status.
                                                            </p>

                                                            <div class="table-responsive">

                                                                <table class="table table-bordered table-striped payment-history-table">

                                                                    <thead>
                                                                        <tr>
                                                                            <th style="width: 60px;">Select</th>
                                                                            <th>Payment Date</th>
                                                                            <th>Amount</th>
                                                                            <th>Transaction Reference</th>
                                                                            <th>Status</th>
                                                                        </tr>
                                                                    </thead>

                                                                    <tbody id="paymentHistoryBody">
                                                                    </tbody>

                                                                </table>

                                                            </div>

                                                            <div class="payment-history-actions">

                                                                <button type="button"
                                                                    id="btnCheckSelectedPayment"
                                                                    class="btn btn-success"
                                                                    onclick="checkSelectedPayment();"
                                                                    disabled="disabled">
                                                                    Check Payment Status
                                                                </button>

                                                            </div>

                                                        </div>

                                                    </div>

                                                    <!-- Payment Status Message -->
                                                    <div class="row">
                                                        <div class="col-md-12">

                                                            <span>
                                                                <label id="lblMessage"
                                                                    style="display: none;">
                                                                </label>
                                                            </span>

                                                        </div>
                                                    </div>

                                                    <br />

                                                    <!-- Existing Submit Button -->
                                                    <div class="row">
                                                        <div class="col-md-12">

                                                            <asp:Button
                                                                ID="btnSubmit"
                                                                Text="Submit"
                                                                runat="server"
                                                                OnClick="btnSubmit_Click"
                                                                CssClass="btn btn-success"
                                                                Style="display: none;" />

                                                        </div>
                                                    </div>

                                                </contenttemplate>
                                            </asp:UpdatePanel>

                                        </form>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3"
    ContentPlaceHolderID="ContentPlaceHolder2"
    runat="Server">

    <script type="text/javascript">

        var selectedPaymentId = null;

        // Show the loading modal when Submit is clicked.
        $(document).on('click', 'input[id*=btnSubmit]', function () {
            $("#loadingmessage").modal("show");
        });

        // Retrieve payment history for the entered application number.
        function getPaymentHistory() {

            var applicationNo = $.trim($('#txtApplicationNo').val());

            if (applicationNo === "") {
                $('#txtApplicationNo').addClass('errorClass');
                $('#errorSpan').show();

                $('#paymentHistoryContainer').hide();
                $('#paymentHistoryBody').empty();
                $('#lblMessage').hide();

                $('input[id*=btnSubmit]').hide();

                return false;
            }

            $('#txtApplicationNo').removeClass('errorClass');
            $('#errorSpan').hide();

            $('#lblMessage').hide();
            $('input[id*=btnSubmit]').hide();

            $('#paymentHistoryBody').empty();
            $('#paymentHistoryContainer').hide();

            selectedPaymentId = null;

            $('#btnCheckSelectedPayment').prop('disabled', true);
            $('#btnGetPaymentHistory').prop('disabled', true);

            $("#loadingmessage").modal("show");

            $.ajax({
                type: "POST",
                url: "api/doubleVerification.ashx",
                data: {
                    caller: "GetPaymentHistory",
                    q: applicationNo
                },
                dataType: "json",
                cache: false,

                success: function (response, textStatus, xhr) {
                    console.log("HTTP status:", xhr.status);
                    console.log("Content-Type:", xhr.getResponseHeader("Content-Type"));
                    console.log("Raw response:", response);

                    // jQuery has already parsed the response because dataType is "json".
                    var records = response;

                    if (!Array.isArray(records)) {
                        console.error("Unexpected payment-history response:", records);
                        alert((records && records.error) || "Unexpected payment-history response.");
                        return;
                    }

                    console.log("Parsed payment records:", records);
                    renderPaymentHistory(records);
                },

                error: function (xhr, textStatus, errorThrown) {
                    console.error("HTTP status:", xhr.status);
                    console.error("Status:", textStatus);
                    console.error("Error:", errorThrown);
                    console.error("Response:", xhr.responseText);

                    alert("Payment history request failed. Check F12 > Console.");
                },

                complete: function () {
                    hideLoadingModal();
                    $("#btnGetPaymentHistory").prop("disabled", false);
                }
            });

            return false;
        }


        // Render payment-history records returned by GetPaymentHistory.
        function renderPaymentHistory(records) {
            var $body = $('#paymentHistoryBody');
            $body.empty();
            selectedPaymentId = null;
            $('#btnCheckSelectedPayment').prop('disabled', true);

            if (!records || records.length === 0) {
                $('#paymentHistoryContainer').hide();
                $('#lblMessage').show().text('No payment history found for this application number.');
                return;
            }

            $.each(records, function (index, record) {
                var id = record.ID;
                var status = record.TRN_STATUS || '';
                var dateText = formatPaymentDate(record.TRANSACTION_ON);
                var amount = record.TXN_AMOUNT == null || record.TXN_AMOUNT === ''
                    ? '—' : record.TXN_AMOUNT;
                var reference = record.TRN_REF_NO || '—';

                var $row = $('<tr>');
                var $radio = $('<input>', {
                    type: 'radio',
                    name: 'selectedPayment',
                    class: 'payment-history-radio',
                    value: id,
                    'aria-label': 'Select payment transaction ' + id
                });

                var $selectCell = $('<td>').append($radio);
                var $dateCell = $('<td>').text(dateText || '—');
                var $amountCell = $('<td>').text(amount);
                var $referenceCell = $('<td>').text(reference);
                var $statusCell = $('<td>').text(status);

                $row.append($selectCell, $dateCell, $amountCell, $referenceCell, $statusCell);
                $body.append($row);
            });

            $('#paymentHistoryContainer').show();
            $('#lblMessage').hide();
        }

        // Keep the selected ID synchronized with the radio button selection.
        $(document).on('change', 'input[name="selectedPayment"]', function () {
            selectedPaymentId = $(this).val();
            $('#paymentHistoryBody tr').removeClass('selected-payment');
            $(this).closest('tr').addClass('selected-payment');
            $('#btnCheckSelectedPayment').prop('disabled', false);
            $('input[id*=btnSubmit]').hide();
        });

        // Check the status of the selected payment transaction.
        function checkSelectedPayment() {

            if (
                selectedPaymentId === null ||
                selectedPaymentId === undefined ||
                selectedPaymentId === ""
            ) {
                alert('Please select a payment transaction.');
                return false;
            }

            var applicationNo = $.trim($('#txtApplicationNo').val());

            if (applicationNo === "") {

                $('#txtApplicationNo').addClass('errorClass');
                $('#errorSpan').show();

                return false;
            }

            $('#txtApplicationNo').removeClass('errorClass');
            $('#errorSpan').hide();

            $('#lblMessage').hide();
            $('input[id*=btnSubmit]').hide();

            $('#btnCheckSelectedPayment').prop('disabled', true);

            $("#loadingmessage").modal("show");

            $.ajax({

                type: "POST",
                url: "api/doubleVerification.ashx",

                // Send both the application number and selected payment ID.
                data: {
                    caller: "GetPaymentStatus",
                    q: applicationNo,
                    selectedPaymentId: selectedPaymentId
                },

                cache: false,

                success: function (data) {

                    handlePaymentStatusResponse(data);

                },

                error: function (xhr) {

                    $('#lblMessage')
                        .show()
                        .text(
                            'Unable to check payment status. Please try again later.'
                        );

                    console.error(
                        'GetPaymentStatus error:',
                        xhr.status,
                        xhr.responseText
                    );

                },

                complete: function () {

                    hideLoadingModal();

                    $('#btnCheckSelectedPayment').prop('disabled', false);

                }

            });

            return false;
        }


        // Process the response from GetPaymentStatus.
        function handlePaymentStatusResponse(data) {

            if (typeof data !== 'string') {
                data = String(data || '');
            }

            var parts = data.split('~');

            var resp = parts[0] || '';
            var respMsg = parts[1] || '';

            if (resp !== "Success") {

                $('#lblMessage')
                    .show()
                    .text(
                        'Unable to retrieve payment status. Please try again later.'
                    );

                $('input[id*=btnSubmit]').hide();

                return;
            }

            switch (respMsg) {

                // The handler returns:
                // Success~Submitted~<encrypted transaction data>
                case "Submitted":

                    $('input[id*=btnSubmit]').hide();

                    $('#lblMessage')
                        .show()
                        .text(
                            'Payment has already been submitted successfully. ' +
                            'No further verification is required.'
                        );

                    break;


                // Payment is still within the two-hour waiting period.
                case "2 Hrs.":

                    $('#lblMessage')
                        .show()
                        .text(
                            'Please verify your payment after 2 hours.'
                        );

                    $('input[id*=btnSubmit]').hide();

                    break;


                // Retained for compatibility if the handler returns Failed.
                case "Failed":

                    $('#lblMessage')
                        .show()
                        .text(
                            'Payment transaction failed. Please click on ' +
                            'Track Application Status on the Home Page and try again.'
                        );

                    $('input[id*=btnSubmit]').hide();

                    break;


                // The handler returns:
                // Success~encry~<encrypted transaction data>
                // for a transaction that can be sent to the existing Submit flow.
                case "encry":

                    var encryData = parts[2] || '';

                    if (encryData === '') {

                        $('#lblMessage')
                            .show()
                            .text(
                                'Transaction details are missing. Please try again.'
                            );

                        $('input[id*=btnSubmit]').hide();

                        break;
                    }

                    // Populate the existing ASP.NET HiddenField.
                    $('input[id*=lblencyValue]').val(encryData);

                    $('#lblMessage')
                        .show()
                        .text(
                            'Please click Submit to verify the selected payment.'
                        );

                    $('input[id*=btnSubmit]').show();

                    break;


                // Display an explanatory message returned by the handler.
                case "exp":

                    var expData = parts.slice(2).join('~');

                    $('#lblMessage')
                        .show()
                        .text(
                            expData || 'Unable to process this payment transaction.'
                        );

                    $('input[id*=btnSubmit]').hide();

                    break;


                case "Not Found":

                    $('#lblMessage')
                        .show()
                        .text(
                            'Application number or selected payment was not found.'
                        );

                    $('input[id*=btnSubmit]').hide();

                    break;


                default:

                    $('#lblMessage')
                        .show()
                        .text(
                            'Unable to determine payment status. Please try again later.'
                        );

                    $('input[id*=btnSubmit]').hide();

                    break;

            }

        }


        // Format the transaction date.
        function formatPaymentDate(value) {

            if (!value) {
                return '';
            }

            // JavaScriptSerializer serializes .NET DateTime values as /Date(milliseconds)/.
            var dotNetDate = /Date\\((-?\\d+)\\)/.exec(String(value));
            var date = dotNetDate
                ? new Date(parseInt(dotNetDate[1], 10))
                : new Date(value);

            if (isNaN(date.getTime())) {
                return String(value);
            }

            return date.toLocaleString();

        }


        // Hide the loading modal and remove its backdrop.
        function hideLoadingModal() {

            $("#loadingmessage").modal("hide");

            $('.modal-backdrop').remove();

            $('body').removeClass('modal-open');

        }


        // Reset the payment history when the application number changes.
        $('#txtApplicationNo').on('input', function () {

            $(this).removeClass('errorClass');

            $('#errorSpan').hide();

            $('#paymentHistoryContainer').hide();
            $('#paymentHistoryBody').empty();

            $('#lblMessage').hide();

            $('input[id*=btnSubmit]').hide();

            $('#btnCheckSelectedPayment').prop('disabled', true);

            selectedPaymentId = null;

        });

    </script>

</asp:Content>
