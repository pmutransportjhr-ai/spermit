<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-COUNTER-SIGNATURE-DUPLICATE-APPLICATION-VIEW.aspx.cs" Inherits="RTA_COUNTER_SIGNATURE_DUPLICATE_APPLICATION_VIEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="RTA-COUNTER-SIGNATURE-DUPLICATE-APPLICATION-VIEW.aspx" style="display: none;" />
    <%--<input type="text" id="txtColCnt" style="display: none;" value="8" />--%>

    <form runat="server" autocomplete="off">
        <asp:HiddenField ID="hdnID" runat ="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />



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
                                <span>Duplicate of Counter Signature Application</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="portlet light portlet-fit">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="icon-settings font-red"></i>
                                                        <span class="caption-subject font-red sbold uppercase">Duplicate of Counter Signature Application</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                   
                                                    <div class="row">
                                                       
                                                        
                                                       
                                                        <div class="col-md-12 col-sm-12">
                                                            <b style="font-size: large">Permit Type : </b>
                                                            <asp:Label ID="lblPermitType" runat="server"></asp:Label>

                                                        </div>
                                                       <br />

                                                              <div class="col-md-6 col-sm-6">
                                                            <b style="font-size: large">Application No :</b>
                                                            <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-md-6 col-sm-6" style="text-align: right">
                                                            <b style="font-size: large">Application Status : </b>
                                                            <asp:Label ID="lblApplicationStatus" runat="server"></asp:Label>



                                                        </div>                  
                                                       
                                                    </div>

                                                   

                                                <div class="row">
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                
                                                                <tr>
                                                                    <td><b>Name : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOName" runat="server"></asp:Label></td>
                                                                    <td><b>RTO Name (Counter Sigmature) : </b></td>
                                                                    <td><asp:Label ID="lblCSRTOName" runat="server"></asp:Label>
                                                                        </td>

                                                                </tr>
                                                                <tr>
                                                                    
                                                                    <td><b>Registration No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VORegNumber" runat="server"></asp:Label></td>
                                                                     <td><b>Permit No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOPermitNo" runat="server"></asp:Label></td>
                                                                 
                                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Permit Validity From : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VoPermitFrom" runat="server"></asp:Label></td>
                                                                     <td><b>Permit Validity Upto : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VoPermitUpto" runat="server"></asp:Label></td>
                                                                 

                                                                </tr>
                                                                <tr>
                                                                    <td><b>Mobile No: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOMobileNo" runat="server"></asp:Label></td>
                                                                    <td><b>Email ID: </b></td>
                                                                    <td> <asp:Label ID="VOEmailID" runat="server"></asp:Label></td>

                                                                </tr>
                                                               

                                                            </table>

                                                        </div>
                                                    </div><br />
                                                    
                                                    
                                                      <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Uploaded Documents</u></b></h4></div>
                                                    
                                                           <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                
                                                                <tr>
                                                                    <td><b>Permit Certificate</b></td>
                                                                    <td>
                                                                        <div id="divPermit" runat="server">Not Uploaded</div>
                                                                      
                                                                        
                                                                    </td>
                                                                     <td></td>
                                                                    <td>
                                                                        
                                                                        
                                                                    </td>
                                                                   
                                                                    
                                                                </tr>
                                                               
                                                              </table>
                                                               
                                                    </div>
                                                       
                                                  
                                                          
                                                              
                                                        </div>

                                                    <div class="row">
                                                        <br /><br />
                                                    <div class="col-md-12" style="text-align:center">
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Back" OnClick="btnCancel_Click" />

                                                    </div>
                                                    
                                                </div>
                                                </div>
                                            </div>
                                    </div>
                            </div>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
    

</asp:Content>

