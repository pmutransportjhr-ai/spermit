<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="JHTRANSPORT.master" CodeFile="QR_VIEW.aspx.cs" Inherits="QR_VIEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

    <style type="text/css">


        /* ===============================
           QR VIEW CLEAN LOOK
        ================================ */

        .portlet.light {
            border: 1px solid #e5e5e5;
        }

        .portlet-body {
            padding: 20px;
        }

        .caption-subject {
            letter-spacing: .5px;
        }


        /* labels */
        #lblTimeTableValidiyty,
        #lblRoute {
            display: block;
            margin: 8px 0;
            font-size: 15px;
        }


        /* table clean */
        .table {
            width: 100%;
            margin-bottom: 20px;
        }


            .table td,
            .table th {
                padding: 10px !important;
                vertical-align: middle !important;
            }

        /* ===============================
           MOBILE VIEW
        ================================ */

        @media(max-width:768px) {

            body {
                overflow-x: hidden;
            }


            .page-container,
            .page-content-wrapper,
            .page-content,
            .container {
                width: 100% !important;
                padding-left: 5px !important;
                padding-right: 5px !important;
            }


            .page-content-inner {
                margin: 0 !important;
            }


            .portlet {
                margin: 5px 0 !important;
            }


            .portlet-body {
                padding: 10px !important;
            }


            .caption-subject {
                font-size: 14px !important;
            }


            /* =========================
       PERMIT DETAILS TABLE
       SAME LINE MOBILE VIEW
    ========================== */


            #dvNoticeDetails {
                overflow-x: hidden !important;
            }


                #dvNoticeDetails table {
                    width: 100% !important;
                    table-layout: fixed;
                    border: 1px solid #ddd;
                }


                #dvNoticeDetails tbody {
                    width: 100%;
                }


                #dvNoticeDetails tr {
                    display: flex;
                    flex-wrap: wrap;
                    width: 100%;
                }


                #dvNoticeDetails td {
                    display: block;
                    width: 50% !important;
                    box-sizing: border-box;
                    text-align: left !important;
                    padding: 8px !important;
                    font-size: 13px;
                    border: 1px solid #eee !important;
                    word-break: break-word;
                }


                    #dvNoticeDetails td b {
                        font-size: 13px;
                    }


            /*
      Result:
      Permit Number | Value
      Valid Upto    | Value

      No horizontal scroll
    */


            /* =========================
       ROUTE LABELS
    ========================== */


            #lblTimeTableValidiyty,
            #lblRoute {
                font-size: 13px;
                padding: 0 5px;
                line-height: 20px;
            }




            /* =========================
       ROUTE TIME TABLE
       ONLY THIS WILL SCROLL
    ========================== */


            #RouteTimeInfo {
                width: 100%;
                overflow-x: auto;
            }


                #RouteTimeInfo table {
                    min-width: 650px;
                }


                #RouteTimeInfo th,
                #RouteTimeInfo td {
                    font-size: 12px;
                    padding: 7px !important;
                    white-space: nowrap;
                }
        }





            /* ===============================
       SMALL PHONE
    ================================ */


        @media(max-width:480px) {


            .breadcrumb {
                font-size: 12px;
            }


            .caption-subject {
                font-size: 13px !important;
            }



            #dvNoticeDetails td {
                width: 50% !important;
                font-size: 12px;
            }


                #dvNoticeDetails td b {
                    font-size: 12px;
                }


            .table td {
                font-size: 12px;
            }
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


                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">QR View</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                        <form id="FormID1" runat="server" class="form-horizontal" autocomplete="off">

                                            <div class="row">

                                                <div class="col-sm-12"
                                                    id="dvNoticeDetails"
                                                    runat="server">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 text-center uppercase">
                                                    <b>
                                                        <asp:Label ID="lblTimeTableValidiyty" runat="server"></asp:Label></b>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 text-center uppercase">
                                                    <b>
                                                        <asp:Label ID="lblRoute" runat="server"></asp:Label></b>
                                                </div>

                                            </div>
                                            <div class="table-responsive">
                                                <div id="RouteTimeInfo" runat="server"></div>
                                            </div>


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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>

</asp:Content>
