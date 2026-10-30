<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master"
    AutoEventWireup="true" CodeFile="PermitHomePage.aspx.cs"
    Inherits="PermitHomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        /* ================= HERO SLIDER & NOTICE BOARD ================= */
        .hero-section-card {
            background: #ffffff;
            border-radius: var(--radius-lg);
            overflow: hidden;
            border: 1px solid var(--border-color);
            box-shadow: var(--shadow-sm);
        }

        .hero-slider {
            height: 400px;
            position: relative;
            overflow: hidden;
            border-radius: var(--radius-lg);
        }

        .hero-slider .carousel-inner,
        .hero-slider .carousel-item {
            height: 400px;
        }

        .hero-slider img {
            width: 100%;
            height: 400px;
            object-fit: cover;
        }

        .hero-slider .carousel-caption {
            position: absolute;
            right: 30px;
            left: auto;
            bottom: 30px;
            background: rgba(27, 67, 50, 0.88);
            backdrop-filter: blur(4px);
            padding: 14px 22px;
            border-radius: var(--radius-md);
            max-width: 450px;
            text-align: right;
            border-right: 5px solid var(--emerald-glow);
            box-shadow: var(--shadow-md);
        }

        .hero-slider .carousel-caption h3 {
            font-size: 20px;
            font-weight: 700;
            color: #ffffff;
            margin: 0;
            line-height: 1.3;
        }

        .hero-slider .carousel-caption p {
            font-size: 13px;
            color: #e8f5e9;
            margin: 4px 0 0;
        }

        /* NOTICE BOARD */
        .notice-card {
            background: #ffffff;
            border-radius: var(--radius-lg);
            border: 1px solid var(--border-color);
            box-shadow: var(--shadow-sm);
            height: 400px;
            display: flex;
            flex-direction: column;
            overflow: hidden;
        }

        .notice-card-header {
            background: linear-gradient(135deg, var(--primary-green), var(--mid-green));
            color: #ffffff;
            padding: 14px 20px;
            font-weight: 700;
            font-size: 16px;
            display: flex;
            align-items: center;
            justify-content: space-between;
        }

        .notice-container {
            height: 290px;
            overflow: hidden;
            position: relative;
            padding: 15px;
            background: #fafdfa;
        }

        .notice-card-footer {
            padding: 10px 15px;
            background: #ffffff;
            border-top: 1px solid var(--border-color);
            text-align: right;
        }

        /* PROCESS FLOW */
        .process-section-title {
            font-size: 20px;
            font-weight: 800;
            color: var(--primary-green);
            margin-bottom: 20px;
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .process-flow-container {
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 10px;
            flex-wrap: wrap;
        }

        .process-step-item {
            flex: 1;
            min-width: 140px;
            background: #ffffff;
            border: 1px solid var(--border-color);
            border-radius: var(--radius-md);
            padding: 18px 12px;
            text-align: center;
            box-shadow: var(--shadow-sm);
            transition: var(--transition-fast);
        }

        .process-step-item:hover {
            transform: translateY(-4px);
            border-color: var(--emerald-glow);
            box-shadow: var(--shadow-md);
        }

        .process-step-icon {
            width: 48px;
            height: 48px;
            border-radius: 50%;
            background: var(--mint-green);
            color: var(--brand-green);
            display: inline-flex;
            align-items: center;
            justify-content: center;
            font-size: 20px;
            margin-bottom: 10px;
            transition: var(--transition-fast);
        }

        .process-step-item:hover .process-step-icon {
            background: var(--brand-green);
            color: #ffffff;
        }

        .process-step-label {
            font-weight: 700;
            color: var(--primary-green);
            font-size: 14px;
            margin: 0;
        }

        .process-arrow-divider {
            color: var(--emerald-glow);
            font-size: 18px;
            padding: 0 4px;
        }

        /* MOBILE ADAPTATION */
        @media (max-width: 768px) {
            .hero-slider, .hero-slider .carousel-inner, .hero-slider .carousel-item {
                height: 220px !important;
            }
            .hero-slider img {
                height: 220px !important;
            }
            .hero-slider .carousel-caption {
                right: 10px;
                left: 10px;
                bottom: 10px;
                max-width: none;
                padding: 8px 12px;
                text-align: center;
            }
            .hero-slider .carousel-caption h3 {
                font-size: 14px;
            }
            .notice-card {
                height: 280px;
                margin-top: 15px;
            }
            .process-flow-container {
                flex-direction: column;
            }
            .process-arrow-divider {
                transform: rotate(90deg);
                margin: 6px 0;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-4">

        <!-- QUICK INFO STATS BANNER -->
        <div class="row g-3 mb-4">
            <div class="col-lg-3 col-md-6">
                <div class="card p-3 border-0 shadow-sm rounded-4 bg-white h-100 d-flex flex-row align-items-center gap-3 border-start border-4 border-success">
                    <div class="rounded-circle p-3 text-white" style="background:#5c9429; width:48px; height:48px; display:flex; align-items:center; justify-content:center;">
                        <i class="fa fa-globe fs-5"></i>
                    </div>
                    <div>
                        <h6 class="text-uppercase text-muted small fw-bold mb-0">Online Services</h6>
                        <span class="fs-6 fw-bold text-dark">24/7 Digital Access</span>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-6">
                <div class="card p-3 border-0 shadow-sm rounded-4 bg-white h-100 d-flex flex-row align-items-center gap-3 border-start border-4 border-primary">
                    <div class="rounded-circle p-3 text-white" style="background:#0070c0; width:48px; height:48px; display:flex; align-items:center; justify-content:center;">
                        <i class="fa fa-credit-card fs-5"></i>
                    </div>
                    <div>
                        <h6 class="text-uppercase text-muted small fw-bold mb-0">Instant Payment</h6>
                        <span class="fs-6 fw-bold text-dark">Secure E-Payment</span>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-6">
                <div class="card p-3 border-0 shadow-sm rounded-4 bg-white h-100 d-flex flex-row align-items-center gap-3 border-start border-4 border-info">
                    <div class="rounded-circle p-3 text-white" style="background:#00bcd4; width:48px; height:48px; display:flex; align-items:center; justify-content:center;">
                        <i class="fa fa-qrcode fs-5"></i>
                    </div>
                    <div>
                        <h6 class="text-uppercase text-muted small fw-bold mb-0">QR Verification</h6>
                        <span class="fs-6 fw-bold text-dark">Digital Verification</span>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-6">
                <div class="card p-3 border-0 shadow-sm rounded-4 bg-white h-100 d-flex flex-row align-items-center gap-3 border-start border-4 border-warning">
                    <div class="rounded-circle p-3 text-white" style="background:#d97706; width:48px; height:48px; display:flex; align-items:center; justify-content:center;">
                        <i class="fa fa-check-circle fs-5"></i>
                    </div>
                    <div>
                        <h6 class="text-uppercase text-muted small fw-bold mb-0">Permit Status</h6>
                        <span class="fs-6 fw-bold text-dark">Real-Time Tracking</span>
                    </div>
                </div>
            </div>
        </div>

        <!-- HERO SLIDER + LATEST NOTICE BOARD -->
        <div class="row g-4 mb-4">
            <div class="col-lg-8">
                <div id="heroCarousel" class="carousel slide hero-slider shadow-sm rounded-4 overflow-hidden" data-bs-ride="carousel" data-bs-interval="4000">
                    <!-- Indicators -->
                    <div class="carousel-indicators">
                        <button type="button" data-bs-target="#heroCarousel" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                        <button type="button" data-bs-target="#heroCarousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
                        <button type="button" data-bs-target="#heroCarousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
                        <button type="button" data-bs-target="#heroCarousel" data-bs-slide-to="3" aria-label="Slide 4"></button>
                    </div>

                    <div class="carousel-inner">
                        <!-- Slide 1 -->
                        <div class="carousel-item active">
                            <img src="assets/img/slider/slider_1.jpg" class="d-block w-100" alt="Online State Permit Portal" />
                            <div class="carousel-caption" style="background: rgba(92, 148, 41, 0.92); border-right: 5px solid #0070c0;">
                                <h3>Online State Permit Portal</h3>
                                <p>Transport Department, Government of Jharkhand</p>
                            </div>
                        </div>

                        <!-- Slide 2 -->
                        <div class="carousel-item">
                            <img src="assets/img/slider/slider_3.jpg" class="d-block w-100" alt="Online Permit Application Process" />
                            <div class="carousel-caption" style="background: rgba(0, 112, 192, 0.92); border-right: 5px solid #5c9429;">
                                <h3>Online Permit Application Process</h3>
                                <p>Apply → Pay Fee → Verification → Approval → Permit Issued</p>
                            </div>
                        </div>

                        <!-- Slide 3 -->
                        <div class="carousel-item">
                            <img src="assets/img/slider/slider_4.jpg" class="d-block w-100" alt="Choose Your Route" />
                            <div class="carousel-caption" style="background: rgba(92, 148, 41, 0.92); border-right: 5px solid #00bcd4;">
                                <h3>Choose Your Route</h3>
                                <p>Inter-State & Intra-State Bus Route Selection</p>
                            </div>
                        </div>

                        <!-- Slide 4 -->
                        <div class="carousel-item">
                            <img src="assets/img/slider/slider_5.jpg" class="d-block w-100" alt="Integrated Transport Route Network" />
                            <div class="carousel-caption" style="background: rgba(0, 112, 192, 0.92); border-right: 5px solid #5c9429;">
                                <h3>Integrated Transport Network</h3>
                                <p>Jharkhand Connected with Neighboring States</p>
                            </div>
                        </div>
                    </div>

                    <!-- Controls -->
                    <button class="carousel-control-prev" type="button" data-bs-target="#heroCarousel" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Previous</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#heroCarousel" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Next</span>
                    </button>
                </div>
            </div>

            <!-- NOTICE BOARD -->
            <div class="col-lg-4">
                <div class="notice-card rounded-4 border-0 shadow-sm">
                    <div class="notice-card-header">
                        <span><i class="fa fa-bullhorn me-2 text-danger"></i> Latest Office Notices</span>
                    </div>
                    <div class="notice-container" id="noticeContainer">
                        <div id="noticeScroll">
                            <div id="dvNoticeDetails" runat="server"></div>
                        </div>
                    </div>
                    <div class="notice-card-footer">
                        <a href="OFFICE-NOTICE.aspx" class="btn btn-sm btn-success px-3 rounded-pill">
                            See All <i class="fa fa-arrow-right ms-1"></i>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <!-- ONLINE PERMIT PROCESS FLOW -->
        <div class="row mb-4">
            <div class="col-12">
                <div class="card p-4 border-0 shadow-sm rounded-4 bg-white">
                    <h4 class="process-section-title" style="color: #5c9429;">
                        <i class="fa fa-cogs me-2"></i> Online Permit Workflow
                    </h4>
                    
                    <div class="process-flow-container">
                        <div class="process-step-item">
                            <div class="process-step-icon" style="background:#f2f8eb; color:#5c9429;"><i class="fa fa-pencil-square-o"></i></div>
                            <p class="process-step-label">1. Apply Online</p>
                        </div>
                        <div class="process-arrow-divider"><i class="fa fa-angle-right"></i></div>

                        <div class="process-step-item">
                            <div class="process-step-icon" style="background:#e8f4fc; color:#0070c0;"><i class="fa fa-credit-card"></i></div>
                            <p class="process-step-label">2. Pay Fee</p>
                        </div>
                        <div class="process-arrow-divider"><i class="fa fa-angle-right"></i></div>

                        <div class="process-step-item">
                            <div class="process-step-icon" style="background:#e8f8fa; color:#00bcd4;"><i class="fa fa-search"></i></div>
                            <p class="process-step-label">3. Verification</p>
                        </div>
                        <div class="process-arrow-divider"><i class="fa fa-angle-right"></i></div>

                        <div class="process-step-item">
                            <div class="process-step-icon" style="background:#fef6eb; color:#d97706;"><i class="fa fa-check-circle-o"></i></div>
                            <p class="process-step-label">4. Approval</p>
                        </div>
                        <div class="process-arrow-divider"><i class="fa fa-angle-right"></i></div>

                        <div class="process-step-item">
                            <div class="process-step-icon" style="background:#f2f8eb; color:#5c9429;"><i class="fa fa-print"></i></div>
                            <p class="process-step-label">5. Permit Issued</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- QUICK SERVICES -->
        <div class="row mb-3">
            <div class="col-12 mb-3">
                <h4 class="process-section-title" style="color: #5c9429;">
                    <i class="fa fa-th-large me-2"></i> Public Services & Quick Portals
                </h4>
            </div>

            <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
                <a href="ApplicationStatusAndPayment.aspx" class="text-decoration-none">
                    <div class="service-card-modern">
                        <div class="service-card-icon"><i class="fa fa-search"></i></div>
                        <h5 class="service-card-title">Track Application</h5>
                    </div>
                </a>
            </div>

            <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
                <a href="ClientPaymentStatus.aspx" class="text-decoration-none">
                    <div class="service-card-modern">
                        <div class="service-card-icon" style="background:#e8f4fc; color:#0070c0;"><i class="fa fa-credit-card"></i></div>
                        <h5 class="service-card-title" style="color:#0070c0;">Payment Status</h5>
                    </div>
                </a>
            </div>

            <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
                <a href="APPLICATION_NUMBER.aspx" class="text-decoration-none">
                    <div class="service-card-modern">
                        <div class="service-card-icon" style="background:#e8f8fa; color:#00bcd4;"><i class="fa fa-id-card-o"></i></div>
                        <h5 class="service-card-title" style="color:#00bcd4;">Know App No.</h5>
                    </div>
                </a>
            </div>

            <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
                <a href="OFFICE-NOTICE.aspx" class="text-decoration-none">
                    <div class="service-card-modern">
                        <div class="service-card-icon" style="background:#fef6eb; color:#d97706;"><i class="fa fa-bullhorn"></i></div>
                        <h5 class="service-card-title" style="color:#d97706;">Notice Board</h5>
                    </div>
                </a>
            </div>

            <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
                <a href="Third-Party-View.aspx" class="text-decoration-none">
                    <div class="service-card-modern">
                        <div class="service-card-icon"><i class="fa fa-eye"></i></div>
                        <h5 class="service-card-title">Third Party View</h5>
                    </div>
                </a>
            </div>

            <div class="col-lg-2 col-md-4 col-sm-6 mb-3">
                <a href="WithdrawApplication.aspx" class="text-decoration-none">
                    <div class="service-card-modern">
                        <div class="service-card-icon" style="background:#fef2f2; color:#dc2626;"><i class="fa fa-undo"></i></div>
                        <h5 class="service-card-title" style="color:#dc2626;">Withdraw App</h5>
                    </div>
                </a>
            </div>
        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            // Initialize Bootstrap 5 Carousel
            var heroCarouselElement = document.querySelector('#heroCarousel');
            if (heroCarouselElement && typeof bootstrap !== 'undefined' && bootstrap.Carousel) {
                new bootstrap.Carousel(heroCarouselElement, {
                    interval: 4000,
                    ride: 'carousel',
                    pause: 'hover'
                });
            }
        });

        /* NOTICE SCROLL FUNCTIONALITY */
        var noticeInterval;

        function startNoticeScroll() {
            var container = document.getElementById("noticeContainer");
            var content = document.getElementById("noticeScroll");

            if (!container || !content) return;

            content.innerHTML += content.innerHTML;

            var speed = 1;
            var height = content.scrollHeight / 2;

            function scrollNotice() {
                if (container.scrollTop >= height) {
                    container.scrollTop = 0;
                } else {
                    container.scrollTop += speed;
                }
            }

            noticeInterval = setInterval(scrollNotice, 30);

            container.addEventListener("mouseenter", function () {
                clearInterval(noticeInterval);
            });

            container.addEventListener("mouseleave", function () {
                noticeInterval = setInterval(scrollNotice, 30);
            });
        }

        window.onload = function () {
            startNoticeScroll();
        };

        /* AUTO REFRESH NOTICE */
        setInterval(function () {
            $("#dvNoticeDetails").load("PermitHomePage.aspx #dvNoticeDetails");
        }, 60000);
    </script>

</asp:Content>
