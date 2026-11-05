# Spermit Miscellaneous Payment - JEGRAS Implementation

## Scope and isolation

This feature is implemented on feature/miscellaneous-egras-payment. It does not change the existing SP_PAYMENT_TRANSACTIONS_HISTORY_AED, PaymentRequest.aspx, PaymentResponse.aspx, or ClientPaymentStatus.aspx flow. New pages use a separate transaction table and new stored procedures.

The public navigation now links to MISCELLANEOUS_PAYMENT.aspx and MISC_PAYMENT_STATUS.aspx.

## SQL migration

Run App_Data/Migrations/002_MiscellaneousPayment.sql against the Spermit JHT_LIVE database. It creates:
- MISC_PAYMENT_AUTHORITY_MASTER
- MISC_PAYMENT_REASON_MASTER
- MISC_PAYMENT_REASON_FIELD_MASTER
- MISC_PAYMENT_TRANSACTION
- SP_MISC_PAYMENT_REASON_AED
- SP_MISC_PAYMENT_TRANSACTION_AED

No existing payment table or stored procedure is altered.

### Treasury mapping

The migration seeds the following values from patterns currently present in the application source:

| Authority | Department | Receipt head | Treasury | IFMS office |
|---|---|---|---|---|
| STA | JHTD | 004100102040101 | PRJ | PRJSES026 |
| RTA | JHTD | 004100102030101 | PRJ | PRJSES026 |

Finance must confirm the receipt-head, treasury, and office mapping for both authorities before production use. These are source-code-derived defaults, not a Finance approval.

## Finance security configuration

Set the deployment's JEGRAS_SECURITY_CODE application setting to the actual Finance-issued value. It is intentionally blank in the committed web.config; the application refuses to initiate payment while it is blank.

The JEGRAS encryption/decryption key follows the existing PayGetWayDTO implementation (key1234) so this feature uses the same wire format as the current portal. Do not change the existing DTO as part of this feature.

## Reasons and dynamic fields

The reason catalogue is deliberately not seeded with guessed RTA/STA fees. Insert only reasons approved by the Transport Department.

Example SQL to create a variable-amount STA reason and an optional remarks field:

    DECLARE @ReasonId INT;
    INSERT dbo.MISC_PAYMENT_REASON_MASTER
        (AUTHORITY_CODE, REASON_CODE, REASON_NAME, AMOUNT_MODE, FIXED_AMOUNT, IS_ACTIVE, DISPLAY_ORDER)
    VALUES
        ('STA', 'STA_COUNTER_SIGNATURE_TAX_PAY', N'STA Counter Signature Tax Pay', 'VARIABLE', NULL, 1, 1);
    SET @ReasonId = CONVERT(INT, SCOPE_IDENTITY());
    INSERT dbo.MISC_PAYMENT_REASON_FIELD_MASTER
        (REASON_ID, FIELD_KEY, FIELD_LABEL, FIELD_TYPE, IS_REQUIRED, FIELD_OPTIONS, DISPLAY_ORDER, IS_ACTIVE)
    VALUES
        (@ReasonId, 'REFERENCE_DETAILS', N'Reference / Remarks', 'TEXTAREA', 0, NULL, 1, 1);

Example dropdown field:

    INSERT dbo.MISC_PAYMENT_REASON_FIELD_MASTER
        (REASON_ID, FIELD_KEY, FIELD_LABEL, FIELD_TYPE, IS_REQUIRED, FIELD_OPTIONS, DISPLAY_ORDER, IS_ACTIVE)
    VALUES
        (@ReasonId, 'PAYMENT_PERIOD', N'Payment Period', 'DROPDOWN', 1, N'Current|Previous|Other', 2, 1);

Supported field types are TEXT, NUMBER, DATE, DROPDOWN (pipe-delimited options), and TEXTAREA. For a fixed-amount reason, set AMOUNT_MODE='FIXED' and FIXED_AMOUNT to a positive amount. For a variable amount, use AMOUNT_MODE='VARIABLE' and leave FIXED_AMOUNT null.

Use SP_MISC_PAYMENT_REASON_AED with SAVE_REASON or SAVE_FIELD for controlled catalogue maintenance. The public payment page only reads active reasons and fields.

## Payment lifecycle

1. Applicant opens the public Miscellaneous Payment link and selects STA or RTA.
2. Applicant enters the application number. Existing SP_VIEW_APPLICATION_STATUS is used in read/verification modes only; no existing procedure is modified.
3. Applicant verifies the registered mobile OTP. Application ID, owner name, application number, and STA/RTA type are taken from the verified application result, not from browser-supplied hidden values.
4. Reason list is loaded from MISC_PAYMENT_REASON_MASTER for the selected authority. Dynamic fields are loaded from MISC_PAYMENT_REASON_FIELD_MASTER.
5. Server validates the active reason, fixed/variable amount, required fields, and selected authority against the application type.
6. Server generates a unique DEPTTRANID, creates an INITIATED row through SP_MISC_PAYMENT_TRANSACTION_AED, and builds the JEGRAS request using PayGetWayDTO.MakeEncryptedData. OPERATION_FOR carries the selected reason name (for example, STA Counter Signature Tax Pay), matching the existing payment-history convention; the stable reason ID and authority are also retained in the new table and ADDINFO3.
7. MISC_PAYMENT_REQUEST.aspx decrypts and validates the request, atomically changes the row from INITIATED to REQUESTED to prevent a second browser submission, then posts requestparam to https://finance.jharkhand.gov.in/jegras/payment.aspx.
8. JEGRAS returns responseparam. MISC_PAYMENT_RESPONSE.aspx decrypts the 22-field response, validates DEPTTRANID, application, amount, operation, reason, department and receipt head, then saves status, message, reference, mode, transaction date/amount, challan URL, GRN, CIN and treasury metadata through the new transaction procedure.
9. Applicant can open MISC_PAYMENT_STATUS.aspx, re-verify the registered mobile OTP, and then view the new table's history. The page can call JEGRAS SBIePayDoubleVerification for REQUESTED, BOOKED or FAIL records. Verification results are saved through UPDATE_VERIFICATION mode.

## Critical Finance gateway dependency

The Finance/eGRAS team must register or approve this callback URL:

https://<production-host>/MISC_PAYMENT_RESPONSE.aspx

The repository cannot change the response URL registered against the department's JEGRAS merchant configuration. Until Finance confirms that JEGRAS will post responseparam to this new endpoint, do not enable the new payment link for production transactions. Do not point the existing callback at this page unless Finance confirms callback routing and that all existing payment types remain unaffected.

## Test checklist

- [ ] Apply SQL migration in test database.
- [ ] Confirm STA and RTA receipt-head/treasury/office values with Finance.
- [ ] Set JEGRAS_SECURITY_CODE in test deployment.
- [ ] Insert department-approved STA and RTA reason catalogue and dynamic fields.
- [ ] Confirm application lookup returns APP_ID, OWNER_NAME, APPLICATION_NUMBER, and TABLE_TYPE for supported STA/RTA application types.
- [ ] Verify authority mismatch is blocked.
- [ ] Verify required dynamic fields, numeric/date field validation, and fixed amount validation.
- [ ] Verify payment-status history is hidden until the registered-mobile OTP succeeds.
- [ ] Confirm a pending row exists before redirecting to JEGRAS.
- [ ] Confirm Finance callback reaches MISC_PAYMENT_RESPONSE.aspx.
- [ ] Test SUCCESS, FAIL, BOOKED, malformed response, duplicate response, and browser-close scenarios.
- [ ] Verify double verification updates only the miscellaneous transaction table.
- [ ] Confirm existing application payments still use their original pages and stored procedures.

## Reference UI flow

SBI Collect exposes a staged payment experience: Select Payee, Enter Payment Details, Verify Payment Details, Complete Payment, and Print Receipt. Spermit follows the same staged interaction pattern but uses Spermit STA/RTA master data and JEGRAS. The exact STA/RTA reason catalogue must be supplied and approved by the department; it is not inferred from unrelated SBI Collect payees. The supplied payment-history example is retained as the model for OPERATION_FOR: it stores the selected reason name, while the new transaction table separately stores REASON_ID, AUTHORITY_CODE, and the eGRAS ADDINFO3 value in TR_TYPE.
