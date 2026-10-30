<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="JHTRANSPORT.master"
    CodeFile="STA_CS_ALL_TAX_VERIFY.aspx.cs" Inherits="STA_CS_ALL_TAX_VERIFY" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

        <title>Generating Receipt...</title>

        <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" rel="stylesheet" />

        <style>
            body {
                background: #f7f9fb;
            }

            .loader-box {
                text-align: center;
                margin-top: 120px;
            }

            .loader-box h3 {
                margin-top: 20px;
                color: #0B6B3A;
                font-weight: 600;
            }

            .loader-box p {
                color: #777;
                margin-top: 10px;
            }

            .spinner {
                width: 60px;
                height: 60px;
                margin: 0 auto;
                border: 6px solid #e5e5e5;
                border-top: 6px solid #0B6B3A;
                border-radius: 50%;
                animation: spin 1s linear infinite;
            }

            @keyframes spin {
                100% {
                    transform: rotate(360deg);
                }
            }

            .error-box {
                display: none;
                max-width: 450px;
                margin: 100px auto;
            }
        </style>

    </asp:Content>


    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <form runat="server">

            <asp:ScriptManager runat="server" />

            <!-- LOADER -->

            <div id="loaderBox" class="container">
                <div class="loader-box">

                    <div class="spinner"></div>

                    <h3>Generating Receipt...</h3>

                    <p>
                        Please wait while we retrieve your payment details.
                    </p>

                </div>
            </div>


            <!-- ERROR -->

            <div id="errorBox" class="alert alert-danger error-box text-center">

                <strong>Unable to generate receipt.</strong>

                <br />

                <span id="errorMessage">
                    Please try again.
                </span>

                <br /><br />

                <button type="button" class="btn btn-default" onclick="history.back();">
                    Go Back
                </button>

            </div>


            <!-- ERROR MODAL -->

            <div class="modal fade" id="errorModal" tabindex="-1">

                <div class="modal-dialog modal-sm">

                    <div class="modal-content">

                        <div class="modal-header" style="background:#d9534f;color:#fff;">

                            <h4 class="modal-title">
                                Error
                            </h4>

                        </div>

                        <div class="modal-body text-center">
                        </div>

                        <div class="modal-footer">

                            <button type="button" class="btn btn-default" data-dismiss="modal">

                                OK

                            </button>

                        </div>

                    </div>

                </div>

            </div>

        </form>

    </asp:Content>


    <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

    </asp:Content>