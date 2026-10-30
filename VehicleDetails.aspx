<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="VehicleDetails.aspx.cs" Inherits="VehicleDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input type="text" id="txtColCnt" style="display: none;" value="2" />

    <form runat="server" autocomplete="off">
        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="STA-DASHBOARD.aspx">HOME</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <a href="#">Vehicle Detail</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>Vahaan Service</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light portlet-fit">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="icon-settings font-red"></i>
                                                <span class="caption-subject font-red sbold uppercase">Latest Vehicle Details </span>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="form-body">
                                                <div class="col-md-4">
                                                    <span class="bold">Enter Registration Number:</span>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtRegistrationNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="regularExpression" runat="server" ControlToValidate="txtRegistrationNumber"
                                                        ValidationExpression="^[a-zA-Z]{2}[0-9]{2}[a-zA-Z]{1,2}[0-9]*$" Font-Bold="true" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="Please Enter Valid Registration Number"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:Button ID="btnGetDetail" runat="server"  Text="Submit"
                                                        CssClass="btn btn-info" OnClick="btnGetDetail_Click" />                         
                                                </div>

                                         
                                            <div runat="server" id="divTable" visible="false">
                                            <div class="table-toolbar">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="btn-group-devided">

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>Parameters</th>
                                                        <th>Values</th>

                                                    </tr>
                                                </thead>
                                                <tbody id="tbdCityList">
                                                  <div id="FillCityList"  runat="server"></div>
                                                </tbody>
                                            </table>
                                                </div>
                                                   </div>
                                        </div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
<script src="assets/pages/scripts/table-datatables-buttonsReg.js" type="text/javascript"></script>
  


</asp:Content>

