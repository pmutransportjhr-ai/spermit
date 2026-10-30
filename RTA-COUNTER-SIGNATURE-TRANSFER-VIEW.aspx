<%@ Page Title="" Language="C#" MasterPageFile="RTAMasterPage.master" AutoEventWireup="true" CodeFile="RTA-COUNTER-SIGNATURE-TRANSFER-VIEW.aspx.cs" Inherits="RTA_COUNTER_SIGNATURE_TRANSFER_VIEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="RTA-COUNTER-SIGNATURE-TRANSFER-VIEW.aspx" style="display: none;" />
    <form runat="server" autocomplete="off">
        <asp:HiddenField ID="hdnID" runat="server" />
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
                                <span>Transfer of Counter Signature Application</span>
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
                                                        <span class="caption-subject font-red sbold uppercase">Transfer of Counter Signature Application</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                     <div class="row">
                                            <div class="col-md-12 col-sm-12" style="text-align: right">
                                               
                                                <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv(IssuedReceiptDiv)">Print</button>

                                            </div>

                                        </div> <div id="IssuedReceiptDiv">
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12">
                                                            <b style="font-size: large">Permit Type : </b>
                                                            <asp:Label ID="lblPermitType" runat="server"></asp:Label>



                                                        </div>
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
                                                                      <td><b>MOM File</b></td>
                                                                    <td>
                                                                        <div id="divMOM" runat="server">Not Uploaded</div>
                                                                      
                                                                        
                                                                    </td>
                                                                   
                                                                   
                                                                    
                                                                </tr>
                                                               
                                                              </table>
                                                               
                                                    </div>
                                                       
                                                  
                                                          
                                                              
                                                        </div></div>

                                                    <div class="row">
                                                        <br /><br />
                                                    <div class="col-md-12" style="text-align:center">
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Close" OnClientClick="Javascript: window.close(); return false;"  />

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
    <script type="text/javascript">


        function printDiv(divId) {
            //document.getElementById(divId).style.backgroundImage = "url('assets/img/Untitled.png')";
            //document.body.style.backgroundImage = "url('assets/img/Untitled.png')";

            var printContents = $('#IssuedReceiptDiv').html(); //document.getElementById(divId).innerHTML;
            var originalContents = document.body.innerHTML;
            var height = $(document).height();
            height = height + 100;
            var width = $(document).width();
            width = width + 50;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            window.print();
            document.body.innerHTML = originalContents;






        }
    </script>  

</asp:Content>

