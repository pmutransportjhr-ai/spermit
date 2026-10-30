<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" ViewStateEncryptionMode="Always" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">

<head runat="server">
    <meta charset="utf-8" />
    <title>Departmental Login | Transport Department, Government of Jharkhand</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Google Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="anonymous" />
    <link href="https://fonts.googleapis.com/css2?family=Plus+Jakarta+Sans:wght@400;500;600;700;800&display=swap" rel="stylesheet" />

    <!-- BOOTSTRAP 5 -->
    <link href="assets/bootstrap-5.3.8/css/bootstrap.min.css" rel="stylesheet" />

    <!-- FontAwesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />

    <style>
        :root {
            --primary-green: #5c9429;
            --dark-green: #2d6a4f;
            --deep-green: #1b4332;
            --accent-blue: #0070c0;
            --text-dark: #1e293b;
            --text-muted: #64748b;
            --card-bg: #ffffff;
            --bg-light: #f1f5f9;
        }

        body {
            background: #f8fafc;
            font-family: "Plus Jakarta Sans", "Segoe UI", sans-serif;
            color: var(--text-dark);
            min-height: 100vh;
            display: flex;
            flex-direction: column;
            margin: 0;
        }

        /* ================= GOV HEADER ================= */
        .gov-top-bar {
            background: linear-gradient(135deg, #1b4332 0%, #2d6a4f 55%, #0070c0 100%);
            color: #ffffff;
            padding: 10px 0;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
        }

        .gov-logo-img {
            height: 52px;
            width: auto;
            filter: drop-shadow(0 2px 4px rgba(0,0,0,0.15));
        }

        .gov-title-main {
            font-size: 20px;
            font-weight: 800;
            letter-spacing: 0.3px;
            line-height: 1.2;
            color: #ffffff;
        }

        .gov-title-sub {
            font-size: 14px;
            font-weight: 600;
            color: #e2e8f0;
        }

        .gov-title-tag {
            font-size: 12px;
            font-weight: 500;
            color: #cbd5e1;
        }

        .header-action-btn {
            background: rgba(255, 255, 255, 0.15);
            color: #ffffff !important;
            border: 1px solid rgba(255, 255, 255, 0.3);
            border-radius: 8px;
            padding: 6px 14px;
            font-size: 13px;
            font-weight: 600;
            text-decoration: none;
            transition: all 0.2s ease;
        }

        .header-action-btn:hover {
            background: rgba(255, 255, 255, 0.3);
            color: #ffffff !important;
            transform: translateY(-1px);
        }

        /* ================= MAIN CONTAINER ================= */
        .login-section {
            flex: 1;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 40px 20px;
            background: radial-gradient(circle at 50% 0%, #e2e8f0 0%, #f8fafc 70%);
        }

        .login-card {
            width: 100%;
            max-width: 1020px;
            background: #ffffff;
            border-radius: 20px;
            overflow: hidden;
            box-shadow: 0 20px 50px rgba(15, 23, 42, 0.1);
            display: flex;
            border: 1px solid #e2e8f0;
        }

        /* LEFT BRANDING PANEL */
        .login-left-panel {
            flex: 1.1;
            background: linear-gradient(135deg, #1b4332 0%, #2d6a4f 60%, #0070c0 100%);
            color: #ffffff;
            padding: 48px 40px;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            position: relative;
            overflow: hidden;
        }

        .login-left-panel::before {
            content: "";
            position: absolute;
            top: -50px;
            right: -50px;
            width: 200px;
            height: 200px;
            border-radius: 50%;
            background: rgba(255, 255, 255, 0.05);
            pointer-events: none;
        }

        .login-left-panel::after {
            content: "";
            position: absolute;
            bottom: -80px;
            left: -80px;
            width: 300px;
            height: 300px;
            border-radius: 50%;
            background: rgba(255, 255, 255, 0.04);
            pointer-events: none;
        }

        .branding-emblem-wrapper {
            text-align: center;
            margin-bottom: 30px;
        }

        .branding-emblem {
            width: 100px;
            height: 100px;
            background: rgba(255, 255, 255, 0.12);
            border: 2px solid rgba(255, 255, 255, 0.25);
            border-radius: 50%;
            padding: 12px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
            backdrop-filter: blur(8px);
            margin: 0 auto 16px;
        }

        .branding-emblem img {
            width: 100%;
            height: 100%;
            object-fit: contain;
        }

        .portal-badge {
            display: inline-block;
            background: rgba(255, 255, 255, 0.18);
            border: 1px solid rgba(255, 255, 255, 0.3);
            color: #ffffff;
            font-size: 11px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
            padding: 5px 14px;
            border-radius: 20px;
            margin-bottom: 12px;
        }

        .left-title {
            font-size: 22px;
            font-weight: 800;
            line-height: 1.3;
            margin-bottom: 8px;
        }

        .left-desc {
            font-size: 13.5px;
            color: #cbd5e1;
            line-height: 1.6;
        }

        .feature-list {
            list-style: none;
            padding: 0;
            margin: 24px 0;
        }

        .feature-list li {
            font-size: 13.5px;
            margin-bottom: 12px;
            display: flex;
            align-items: center;
            gap: 12px;
            color: #f1f5f9;
        }

        .feature-list li i {
            width: 26px;
            height: 26px;
            border-radius: 50%;
            background: rgba(255, 255, 255, 0.15);
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 12px;
            color: #86efac;
        }

        .left-footer-note {
            font-size: 12px;
            color: #94a3b8;
            border-top: 1px solid rgba(255, 255, 255, 0.12);
            padding-top: 16px;
        }

        /* RIGHT FORM PANEL */
        .login-right-panel {
            flex: 1;
            padding: 44px 44px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            background: #ffffff;
        }

        .form-header {
            margin-bottom: 28px;
        }

        .form-header h2 {
            font-size: 22px;
            font-weight: 800;
            color: var(--text-dark);
            margin-bottom: 4px;
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .form-header h2 i {
            color: var(--primary-green);
        }

        .form-header p {
            font-size: 13.5px;
            color: var(--text-muted);
            margin: 0;
        }

        .form-label-custom {
            font-size: 13px;
            font-weight: 700;
            color: #334155;
            margin-bottom: 6px;
            display: block;
        }

        .input-group-custom {
            position: relative;
            margin-bottom: 18px;
        }

        .input-group-custom .input-icon {
            position: absolute;
            left: 14px;
            top: 50%;
            transform: translateY(-50%);
            color: #94a3b8;
            font-size: 15px;
            z-index: 5;
        }

        .form-control-custom {
            width: 100%;
            padding: 11px 14px 11px 42px;
            font-size: 14px;
            border: 1.5px solid #cbd5e1;
            border-radius: 10px;
            color: var(--text-dark);
            background: #ffffff;
            transition: all 0.2s ease;
        }

        .form-control-custom:focus {
            outline: none;
            border-color: var(--primary-green);
            box-shadow: 0 0 0 4px rgba(92, 148, 41, 0.12);
        }

        .form-select-custom {
            padding-left: 42px !important;
            appearance: none;
            background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'%3e%3cpath fill='none' stroke='%3c64748b' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='m2 5 6 6 6-6'/%3e%3c/svg%3e");
            background-repeat: no-repeat;
            background-position: right 14px center;
            background-size: 12px 12px;
        }

        .pwd-toggle-btn {
            position: absolute;
            right: 14px;
            top: 50%;
            transform: translateY(-50%);
            color: #94a3b8;
            cursor: pointer;
            z-index: 5;
            padding: 4px;
            transition: color 0.2s ease;
        }

        .pwd-toggle-btn:hover {
            color: var(--primary-green);
        }

        /* CAPTCHA STYLING */
        .captcha-row {
            display: flex;
            align-items: center;
            gap: 12px;
            margin-bottom: 18px;
        }

        .captcha-display-box {
            flex: 1;
            background: #f0fdf4 !important;
            border: 2px dashed #86efac !important;
            color: #166534 !important;
            font-family: 'Courier New', Courier, monospace !important;
            font-size: 22px !important;
            font-weight: 800 !important;
            letter-spacing: 6px !important;
            text-align: center !important;
            border-radius: 10px !important;
            height: 48px !important;
            box-shadow: inset 0 2px 4px rgba(0,0,0,0.03);
            user-select: none;
        }

        .captcha-refresh-btn {
            width: 48px;
            height: 48px;
            background: #f1f5f9;
            border: 1.5px solid #cbd5e1;
            border-radius: 10px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            color: #475569;
            cursor: pointer;
            transition: all 0.2s ease;
        }

        .captcha-refresh-btn:hover {
            background: #e2e8f0;
            color: var(--primary-green);
            transform: rotate(45deg);
        }

        /* BUTTONS */
        .btn-gov-primary {
            width: 100%;
            background: linear-gradient(135deg, #5c9429 0%, #46741d 100%);
            color: #ffffff;
            border: none;
            padding: 12px 20px;
            font-size: 15px;
            font-weight: 700;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(92, 148, 41, 0.3);
            transition: all 0.2s ease;
            cursor: pointer;
        }

        .btn-gov-primary:hover {
            background: linear-gradient(135deg, #46741d 0%, #355815 100%);
            box-shadow: 0 6px 16px rgba(92, 148, 41, 0.4);
            transform: translateY(-1px);
            color: #ffffff;
        }

        .action-row {
            display: flex;
            gap: 10px;
            margin-top: 14px;
        }

        .btn-gov-outline {
            flex: 1;
            background: #ffffff;
            border: 1.5px solid #cbd5e1;
            color: #475569;
            padding: 9px 14px;
            font-size: 13px;
            font-weight: 600;
            border-radius: 8px;
            transition: all 0.2s ease;
            cursor: pointer;
            text-align: center;
        }

        .btn-gov-outline:hover {
            border-color: var(--primary-green);
            color: var(--primary-green);
            background: #f8fafc;
        }

        .btn-gov-danger {
            border-color: #fca5a5 !important;
            color: #dc2626 !important;
        }

        .btn-gov-danger:hover {
            background: #fef2f2 !important;
            border-color: #dc2626 !important;
        }

        .security-warning-note {
            margin-top: 24px;
            padding: 10px 14px;
            background: #f8fafc;
            border-radius: 8px;
            border-left: 3px solid var(--primary-green);
            font-size: 11.5px;
            color: var(--text-muted);
            line-height: 1.5;
        }

        /* ================= FOOTER ================= */
        .gov-footer {
            background: #1e293b;
            color: #94a3b8;
            font-size: 13px;
            padding: 16px 20px;
            text-align: center;
            border-top: 3px solid var(--primary-green);
        }

        .gov-footer a {
            color: #cbd5e1;
            text-decoration: none;
        }

        .gov-footer a:hover {
            color: #ffffff;
        }

        /* ================= DARK MODE ================= */
        body.dark {
            background: #0f172a;
            color: #e2e8f0;
        }

        body.dark .login-section {
            background: radial-gradient(circle at 50% 0%, #1e293b 0%, #0f172a 70%);
        }

        body.dark .login-card {
            background: #1e293b;
            border-color: #334155;
        }

        body.dark .login-right-panel {
            background: #1e293b;
            color: #e2e8f0;
        }

        body.dark .form-header h2 {
            color: #ffffff;
        }

        body.dark .form-header p {
            color: #94a3b8;
        }

        body.dark .form-label-custom {
            color: #cbd5e1;
        }

        body.dark .form-control-custom {
            background: #0f172a;
            color: #ffffff;
            border-color: #334155;
        }

        body.dark .form-control-custom:focus {
            border-color: var(--primary-green);
            box-shadow: 0 0 0 4px rgba(92, 148, 41, 0.2);
        }

        body.dark .btn-gov-outline {
            background: #0f172a;
            border-color: #334155;
            color: #cbd5e1;
        }

        body.dark .security-warning-note {
            background: #0f172a;
            color: #94a3b8;
        }

        @media (max-width: 880px) {
            .login-card {
                flex-direction: column;
                max-width: 480px;
            }
            .login-left-panel {
                padding: 32px 24px;
            }
            .login-right-panel {
                padding: 32px 24px;
            }
        }
    </style>

    <script src="assets/global/plugins/jquery.min.js"></script>
    <script src="assets/pages/scripts/sha512.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            generateCaptcha();

            // Disable copy, paste, cut, drag, drop
            $('input').on('copy paste cut drag drop', function (e) {
                e.preventDefault();
            });

            // Disable right click on inputs
            $('input').on('contextmenu', function (e) {
                e.preventDefault();
            });

            // Disable Ctrl+C, Ctrl+V, Ctrl+X for inputs
            $('input').on('keydown', function (e) {
                if (e.ctrlKey && (e.key === 'c' || e.key === 'v' || e.key === 'x')) {
                    e.preventDefault();
                }
            });
        });

        // Generate Captcha
        function generateCaptcha() {
            var alpha = [
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                '1', '2', '3', '4', '5', '6', '7', '8', '9'
            ];

            var code = "";
            for (var i = 0; i < 6; i++) {
                code += alpha[Math.floor(Math.random() * alpha.length)];
            }

            $('#<%= imgCaptcha.ClientID %>').val(code);
            $('#<%= hdnCap.ClientID %>').val(code);
        }

        // Refresh Captcha
        $(document).on("click", "#refCaptchta", function () {
            generateCaptcha();
        });

        function validatelogin() {
            var LOGIN_ID = $('#<%= txtLogin.ClientID %>').val();
            var PASSWORD = $('#<%= txtPassword.ClientID %>').val();
            var type = $('#<%= ddlType.ClientID %>').val();
            var captcha = $('#txtCaptcha').val();

            if (!LOGIN_ID) { alert("Enter Login ID"); return false; }
            if (!PASSWORD) { alert("Enter Password"); return false; }
            if (type === "0") { alert("Select User Type"); return false; }
            if (!captcha) { alert("Enter Captcha"); return false; }

            var actual = $('#<%= hdnCap.ClientID %>').val();

            if (captcha.trim() !== actual.trim()) {
                alert("Invalid Captcha");
                generateCaptcha();
                return false;
            }

            // Hashing
            var salt = '<%= Session["salt"] %>';
            var hash = hex_sha512(hex_sha512(PASSWORD) + salt);

            document.getElementById('<%= txtPassword.ClientID %>').value = hash;
            document.getElementById('<%= hfpwd.ClientID %>').value = hash;

            return true;
        }

        function togglePassword() {
            var pwd = document.getElementById('<%= txtPassword.ClientID %>');
            var icon = document.getElementById('eyeIcon');

            if (pwd.type === "password") {
                pwd.type = "text";
                icon.classList.remove("fa-eye");
                icon.classList.add("fa-eye-slash");
            } else {
                pwd.type = "password";
                icon.classList.remove("fa-eye-slash");
                icon.classList.add("fa-eye");
            }
        }
    </script>
</head>

<body>

    <!-- ================= GOV HEADER ================= -->
    <header class="gov-top-bar">
        <div class="container-fluid px-4 d-flex justify-content-between align-items-center">
            <div class="d-flex align-items-center gap-3">
                <img src="assets/img/logo.png" class="gov-logo-img" alt="Government of Jharkhand Emblem" />
                <div>
                    <div class="gov-title-main">Government of Jharkhand</div>
                    <div class="gov-title-sub">Transport Department</div>
                    <div class="gov-title-tag">Online State Permit System</div>
                </div>
            </div>

            <div class="d-flex align-items-center gap-2">
                <a href="PermitHomePage.aspx" class="header-action-btn">
                    <i class="fa fa-home me-1"></i> Public Portal
                </a>
                <button id="darkToggle" class="btn btn-sm btn-outline-light ms-1" style="border-radius: 8px; padding: 6px 12px;">🌙</button>
            </div>
        </div>
    </header>

    <!-- ================= MAIN LOGIN CONTENT ================= -->
    <main class="login-section">
        <div class="login-card">

            <!-- LEFT BRANDING & SECURITY showcase -->
            <div class="login-left-panel">
                <div>
                    <div class="branding-emblem-wrapper">
                        <div class="branding-emblem">
                            <img src="assets/img/logo.png" alt="Jharkhand Emblem" />
                        </div>
                        <span class="portal-badge">STA & RTA Official Portal</span>
                        <div class="left-title">Transport Department</div>
                        <div class="left-desc">State Transport Authority & Regional Transport Authority Authorized Personnel Login</div>
                    </div>

                    <ul class="feature-list">
                        <li>
                            <i class="fa fa-user-shield"></i>
                            <span>Authorized Departmental Personnel Access</span>
                        </li>
                        <li>
                            <i class="fa fa-route"></i>
                            <span>State & Inter-State Permit Management</span>
                        </li>
                        <li>
                            <i class="fa fa-file-signature"></i>
                            <span>Official Permit Verification & Issuance</span>
                        </li>
                        <li>
                            <i class="fa fa-building-columns"></i>
                            <span>Jharkhand e-Governance Portal System</span>
                        </li>
                    </ul>
                </div>

                <div class="left-footer-note">
                    <div class="fw-bold text-white mb-1"><i class="fa fa-headset me-1 text-success"></i> Departmental Helpdesk</div>
                    <div>Helpline: +91-651-2400200 | Email: support.transport@jharkhand.gov.in</div>
                </div>
            </div>

            <!-- RIGHT LOGIN FORM PANEL -->
            <div class="login-right-panel">
                <div class="form-header">
                    <h2><i class="fa fa-user-shield"></i> Departmental Login</h2>
                    <p>Enter your credentials to access the transport portal</p>
                </div>

                <form runat="server" autocomplete="off">

                    <!-- USER TYPE -->
                    <label class="form-label-custom">Select User Type <span class="text-danger">*</span></label>
                    <div class="input-group-custom">
                        <i class="fa fa-id-card input-icon"></i>
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control-custom form-select-custom">
                            <asp:ListItem Value="0">-- Select User Type (STA / RTA) --</asp:ListItem>
                            <asp:ListItem Value="STA">STA (State Transport Authority)</asp:ListItem>
                            <asp:ListItem Value="RTA">RTA (Regional Transport Authority)</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <!-- LOGIN ID -->
                    <label class="form-label-custom">Login ID / Username <span class="text-danger">*</span></label>
                    <div class="input-group-custom">
                        <i class="fa fa-user input-icon"></i>
                        <asp:TextBox ID="txtLogin" runat="server" CssClass="form-control-custom" placeholder="Enter Login ID"></asp:TextBox>
                        <asp:HiddenField ID="hfpwd" runat="server" />
                    </div>

                    <!-- PASSWORD -->
                    <label class="form-label-custom">Password <span class="text-danger">*</span></label>
                    <div class="input-group-custom">
                        <i class="fa fa-lock input-icon"></i>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control-custom" placeholder="Enter Password"></asp:TextBox>
                        <i id="eyeIcon" class="fa fa-eye pwd-toggle-btn" title="Toggle Password Visibility" onclick="togglePassword();"></i>
                    </div>

                    <!-- CAPTCHA DISPLAY -->
                    <label class="form-label-custom">Security Captcha</label>
                    <div class="captcha-row">
                        <asp:TextBox ID="imgCaptcha" runat="server" ReadOnly="true" CssClass="captcha-display-box" />
                        <div id="refCaptchta" class="captcha-refresh-btn" title="Refresh Captcha Code">
                            <i class="fa fa-arrows-rotate fs-5"></i>
                        </div>
                    </div>

                    <asp:HiddenField ID="hdnCap" runat="server" />

                    <!-- ENTER CAPTCHA -->
                    <label class="form-label-custom">Enter Captcha Code <span class="text-danger">*</span></label>
                    <div class="input-group-custom">
                        <i class="fa fa-font input-icon"></i>
                        <input id="txtCaptcha" class="form-control-custom" placeholder="Enter the 6-character code above" autocomplete="off" />
                    </div>

                    <!-- PRIMARY LOGIN BUTTON -->
                    <div class="mt-2">
                        <asp:Button ID="btnLogin" runat="server"
                            CssClass="btn-gov-primary"
                            Text="🔒  Login to Portal"
                            OnClientClick="return validatelogin();"
                            OnClick="btnLogin_Click1" />
                    </div>

                    <!-- SECONDARY ACTIONS -->
                    <div class="action-row">
                        <asp:Button ID="btnFP" runat="server"
                            CssClass="btn-gov-outline btn-gov-danger"
                            Text="Forgot Password?"
                            OnClick="btnFP_Click" />

                        <asp:Button ID="btnBack" runat="server"
                            CssClass="btn-gov-outline"
                            Text="← Back to Home"
                            OnClick="btnBack_Click" />
                    </div>

                    <!-- LEGAL NOTICE -->
                    <div class="security-warning-note">
                        <i class="fa fa-circle-info me-1 text-primary"></i>
                        <strong>Notice:</strong> This portal is for authorized Government Personnel only. Unauthorized access attempts are monitored and logged under the IT Act, 2000.
                    </div>

                </form>
            </div>
        </div>
    </main>

    <!-- ================= FOOTER ================= -->
    <footer class="gov-footer">
        <div>All Rights Reserved &copy; Government of Jharkhand, Transport Department.</div>
        <div class="mt-1 style-small opacity-75">Designed &amp; Developed for State Transport Authority (STA) &amp; Regional Transport Authority (RTA) Systems.</div>
    </footer>

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            const toggle = document.getElementById('darkToggle');
            if (!toggle) return;

            toggle.addEventListener('click', function () {
                document.body.classList.toggle('dark');
                toggle.innerHTML = document.body.classList.contains('dark') ? '☀️' : '🌙';
            });
        });
    </script>
</body>
</html>
