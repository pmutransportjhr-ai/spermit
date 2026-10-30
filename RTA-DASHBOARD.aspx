<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RTA-DASHBOARD.aspx.cs" MasterPageFile="RTAMasterPage.master" Inherits="RTA_DASHBOARD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="page-container">
        <div class="page-content-wrapper">
            <div class="page-content">
                <div class="container">
                    <ul class="page-breadcrumb breadcrumb">
                        <li>
                            <a href="RTA-DASHBOARD.aspx">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                       
                        <li>
                            <span>Dashboard</span>
                        </li>
                    </ul>
                    <div class="page-content-inner">
                        <div id="dvTopBand" runat="server" class="row widget-row">
                            <div class="col-md-4">
                                <div id="RTAdiv" class="widget-thumb widget-bg-color-white margin-bottom-20 " style="height: 130px; border: 1px solid #e31838 !important; padding: 0px !important;">
                                    <h4 class="widget-thumb-heading" style="font-size: 12px; background-color: #e31838; color: white; padding: 10px;">RTA (Yearly)</h4>
                                    <div class="widget-thumb-wrap" style="padding-left: 10px; padding-right: 10px;">
                                         <div class="widget-thumb-body">
                                            <span class="widget-thumb-subtitle" style="text-transform: capitalize;">
                                                Permit Issued 
                                                <span  id="RTAIssued" runat="server" class="widget-thumb-body-stat" data-counter="counterup" data-value="7,644" style="float: right; font-size: 13px; cursor: pointer;">0</span>
                                            </span>
                                        </div>
                                        <div class="widget-thumb-body">
                                            <span class="widget-thumb-subtitle" style="text-transform: capitalize;">
                                                Pending Applications 
                                                <span  id="RTAPen" runat="server" class="widget-thumb-body-stat" data-counter="counterup" data-value="7,644" style="float: right; font-size: 13px; cursor: pointer;">0</span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div id="Goodsdiv" class="widget-thumb widget-bg-color-white margin-bottom-20 " style="height: 130px; border: 1px solid #006884 !important; padding: 0px !important;">
                                    <h4 class="widget-thumb-heading" style="font-size: 12px; background-color: #006884; color: white; padding: 10px;">Goods carriage (Yearly)</h4>
                                    <div class="widget-thumb-wrap" style="padding-left: 10px; padding-right: 10px;">
                                           <div class="widget-thumb-body">
                                            <span class="widget-thumb-subtitle" style="text-transform: capitalize;">
                                                Permit Issued 
                                                <span  id="GoodsIssued" runat="server" class="widget-thumb-body-stat" data-counter="counterup" data-value="7,644" style="float: right; font-size: 13px; cursor: pointer;">0</span>
                                            </span>
                                        </div>
                                        <div class="widget-thumb-body">
                                            <span class="widget-thumb-subtitle" style="text-transform: capitalize;font-size:12px;">
                                               Pending Applications 
                                                <span id="GoodsPen" runat="server" class="widget-thumb-body-stat" data-counter="counterup" data-value="7,644" style="float: right; font-size: 13px; cursor: pointer;">0</span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div id="Contractdiv" class="widget-thumb widget-bg-color-white margin-bottom-20 " style="height: 130px; border: 1px solid #ee7936 !important; padding: 0px !important;">
                                    <h4 class="widget-thumb-heading" style="font-size: 12px; background-color: #ee7936; color: white; padding: 10px;">Contract (Yearly) </h4>
                                    <div class="widget-thumb-wrap" style="padding-left: 10px; padding-right: 10px;">
                                          <div class="widget-thumb-body">
                                            <span class="widget-thumb-subtitle" style="text-transform: capitalize;">
                                                 Permit Issued 
                                                <span  id="ContractIssued" runat="server" class="widget-thumb-body-stat" data-counter="counterup" data-value="7,644" style="float: right; font-size: 13px; cursor: pointer;">0</span>
                                            </span>
                                        </div>
                                        <div class="widget-thumb-body">
                                            <span class="widget-thumb-subtitle" style="text-transform: capitalize;">
                                              Pending Applications 
                                                <span  id="ContractPen" runat="server" class="widget-thumb-body-stat" data-counter="counterup" data-value="7,644" style="float: right; font-size: 13px; cursor: pointer;">0</span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div id="Autodiv" class="widget-thumb widget-bg-color-white margin-bottom-20 " style="height: 130px; border: 1px solid #de5b8d !important; padding: 0px !important;">
                                    <h4 class="widget-thumb-heading" style="font-size: 12px; background-color: #de5b8d; color: white; padding: 10px;">Auto (Yearly) </h4>
                                    <div class="widget-thumb-wrap" style="padding-left: 10px; padding-right: 10px;">
                                         <div class="widget-thumb-body">
                                            <span class="widget-thumb-subtitle" style="text-transform: capitalize;">
                                                Permit Issued 
                                                <span   id="AutoIssued" runat="server" class="widget-thumb-body-stat" data-counter="counterup" data-value="7,644" style="float: right; font-size: 13px; cursor: pointer;">0</span>
                                            </span>
                                        </div>
                                        <div class="widget-thumb-body">
                                            <span class="widget-thumb-subtitle" style="text-transform: capitalize;">
                                               Pending Applications 
                                                <span  id="AutoPen" runat="server" class="widget-thumb-body-stat" data-counter="counterup" data-value="7,644" style="float: right; font-size: 13px; cursor: pointer;">0</span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div id="Schooldiv" class="widget-thumb widget-bg-color-white margin-bottom-20 " style="height: 130px; border: 1px solid #ffd800 !important; padding: 0px !important;">
                                    <h4 class="widget-thumb-heading" style="font-size: 12px; background-color: #ffd800; color: white; padding: 10px;">School Bus (Yearly)</h4>
                                    <div class="widget-thumb-wrap" style="padding-left: 10px; padding-right: 10px;">
                                         <div class="widget-thumb-body">
                                            <span class="widget-thumb-subtitle" style="text-transform: capitalize;">
                                                Permit Issued 
                                                <span   id="SchoolIssued" runat="server" class="widget-thumb-body-stat" data-counter="counterup" data-value="7,644" style="float: right; font-size: 13px; cursor: pointer;">0</span>
                                            </span>
                                        </div>
                                        <div class="widget-thumb-body">
                                            <span class="widget-thumb-subtitle" style="text-transform: capitalize;">
                                               Pending Applications 
                                                <span id="SchoolPen" runat="server" class="widget-thumb-body-stat" data-counter="counterup" data-value="7,644" style="float: right; font-size: 13px; cursor: pointer;">0</span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4"></div>
                        </div> 
                        <%--<div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">Dashboard</span>
                                        </div>
                                    </div>
<div class="portlet-body form">
                                                
                                            </div>
                                   
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
