<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="STA-COUNTER-SIGNATURE-REPORT-VIEW.aspx.cs" Inherits="STA_COUNTER_SIGNATURE_REPORT_VIEW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <form runat="server" autocomplete="off">
    <asp:HiddenField ID="hdnID" runat="server" />
            <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />



        <div class="page-container">
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="container">
                        <ul class="page-breadcrumb breadcrumb">
                            <li>
                                <a href="STA-DASHBOARD.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>STA Counter Signature Report</span>
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
                                                        <span class="caption-subject font-red sbold uppercase">STA Counter Signature Report</span>
                                                    </div>
                                                </div>
                                     <div class="portlet-body">
                                                   
                                                  <div class="row">
                                            <div class="col-md-12 col-sm-12" style="text-align: right">
                                                <%--<asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="Print" OnClientClick="return printDiv('IssuedReceiptDiv')"  />--%>
                                                <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv(IssuedReceiptDiv)">Print</button>

                                            </div>

                                        </div>
                                        <div id="IssuedReceiptDiv">
                                                        <div class="row">
                                                         <div class="col-md-6 col-sm-6">
                                                                    <b  style="font-size:large">Permit Type : </b>
                                                                        <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                         </div>
                                                         <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                    <b  style="font-size:large">Status : </b>
                                                                        <asp:Label ID="lblApplicationStatus" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                         </div> </div>
                                                     
                                                        <br /><br />
                                                        <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Personal Information</b></u></h4></div>
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                
                                                                <tr>
                                                                    <td><b>Name : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOName" runat="server"></asp:Label></td>
                                                                    <td><b>C/O :  </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOCareOf" runat="server"></asp:Label></td>

                                                                </tr>
                                                                <tr>
                                                                    <td><b>Present Address: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOPresentAddress" runat="server"></asp:Label><span id=""></span> </td>
                                                                    <td><b>Permanent Address : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOPermanentAddress" runat="server"></asp:Label></td>

                                                                </tr>

                                                               

                                                            </table>

                                                        </div>
                                                   
                                                   
                                                    
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Vehicle Details</u></b></h4></div>
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    
                                                                    <td><b>Registration No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VORegNumber" runat="server"></asp:Label></td>
                                                                     <td><b>Permit No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOPermitNo" runat="server"></asp:Label></td>
                                                                 
                                                                   
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Counter Signature No :</b></td>
                                                                    <td><asp:Label ID="VOCSNo" runat="server"></asp:Label></td>
                                                                    <td></td>
                                                                    <td></td>
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
                                                                    <td><b>Maker Description : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblMakerDesc" runat="server"></asp:Label></td>
                                                                    <td><b>Maker Model No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblMakermodel" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Type of Vehicle :</b></td>
                                                                    <td>
                                                                        <asp:Label ID="VType" runat="server"></asp:Label></td>
                                                                    <td><b>Chassis Number : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VChassisNumber" runat="server"></asp:Label></td>
                                                                   
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td><b>Engine Number  : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VEngineNumber" runat="server"></asp:Label></td>
                                                                    <td><b>Seating Capacity : </b></td>
                                                                                <td> <asp:Label ID="VSeatCapacity" runat="server"></asp:Label></td>
                                                                               
                                                                </tr>
                                                                <tr> <td><b>Sleeper Capacity : </b></td>
                                                                                <td> <asp:Label ID="vsleepercapacity" runat="server"></asp:Label></td>
                                                                               </tr>
                                                               
                                                                <tr>

                                                                                <td><b>U.L.W as per RC : </b></td>
                                                                                <td>  <asp:Label ID="txtULW" runat="server"></asp:Label></td>
                                                                                <td><b>R.L.W as per RC : </b></td>
                                                                                <td> <asp:Label ID="txtRLW" runat="server"></asp:Label></td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                               
                                                                                
                                                                                <td><b>Manufacturing Date :</b></td>
                                                                               <td> <asp:Label ID="VManufacturingON" runat="server"></asp:Label></td>
                                                                                  <td><b>Nature of goods to be carried, if any :</b></td>
                                                                               <td> <asp:Label ID="VNatureOfGoods" runat="server"></asp:Label></td>                                                                         
                                                                               </tr>
                                                            </table>

                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Route Details</u></b></h4></div>
                                                        </div> <br />
                                                        <div class="row">
                                                         <div class="col-md-3 col-sm-3"><b>Issued by : </b></div>
                                                        <div class="col-md-9 col-sm-9"><asp:Label  ID="lblIssuedBy" runat="server" /></div>
                                                       </div> <br />
                                                        <div class="row">
                                                        <div class="col-md-3 col-sm-3"><b>Route : </b></div>
                                                        <div class="col-md-9 col-sm-9"><asp:Label  ID="RouteHDRInfo" runat="server" /></div>
                                                    </div> <br />
                                                                Time Format: 24 Hours <br />
                                                    <div id="RouteTimeInfo" runat="server"></div>
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Other Details</u></b></h4></div>
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                 <tr>
                                                                    <td><b>Mobile No: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOMobileNo" runat="server"></asp:Label></td>
                                                                    <td><b>Email ID: </b></td>
                                                                    <td> <asp:Label ID="VOEmailID" runat="server"></asp:Label></td>

                                                                </tr>
                                                                <tr>
                                                                    <td><b>Recommendation Letter No:</b></td>
                                                                    <td><asp:Label ID="lblRecommendationNo" runat="server"></asp:Label></td>

                                                                    
                                                                    <td><b>Recommendation Date:</b></td>
                                                                    <td><asp:Label ID="lblRecommendationDate" runat="server"></asp:Label></td>
                                                                </tr>
                                                               
                                                                </table>
                                                            </div>
                                                    </div>
                                                    
                                                    
                                                    
                                                      <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Uploaded Documents</u></b></h4></div>
                                                    
                                                           <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    <td><b>Registration Certificate</b></td>
                                                                    <td>
                                                                          <div id="divrcfile" runat="server">Not Uploaded</div>
                                                                   </td>
                                                                    <td><b>Speed Government Certificate  </b></td>
                                                                    <td>
                                                                        <div id="divspeedfile" runat="server">Not Uploaded</div>
                                                                          
                                                                   </td>
                                                                    </tr>
                                                                <tr>
                                                                    <td><b>Insurance Certificate</b></td>
                                                                    <td>
                                                                        <div id="divinsurFile" runat="server">Not Uploaded</div>

                                                                      
                                                                       </td>   
                                                                    <td><b>Insurance Certificate Valid Upto </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lbICValidUpto" runat="server"></asp:Label>
                                                                          
                                                                   </td>
                                                                    </tr>
                                                                <tr>
                                                                    <td><b>Pollution Certificate</b></td>
                                                                    <td>
                                                                        <div id="divrollution" runat="server">Not Uploaded</div>
                                                                      
                                                                        </td>
                                                                  <td><b>Pollution Certificate Valid Upto </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lbPCValidUpto" runat="server"></asp:Label>
                                                                          
                                                                   </td>
                                                                    </tr>
                                                                <tr>
                                                                    <td><b>Fitness Certificate</b></td>
                                                                    <td>
                                                                        <div id="divfitness" runat="server">Not Uploaded</div>
                                                                      
                                                                        
                                                                    </td>
                                                                    <td><b>Fitness  Certificate Valid Upto </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lbFCValidUpto" runat="server"></asp:Label>
                                                                          
                                                                   </td>
                                                                    
                                                                </tr>
                                                          <tr>
                                                                    <td><b>Tax paid upto Certificate</b></td>
                                                                    <td>
                                                                        <div id="divTax" runat="server">Not Uploaded</div>
                                                                      
                                                                        
                                                                    </td>
                                                                    <td><b>Tax paid upto</b></td>
                                                                    <td>
                                                                        <asp:Label ID="lbTPValidUpto" runat="server"></asp:Label>
                                                                      
                                                                        
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Permit Certificate</b></td>
                                                                    <td>
                                                                        <div id="divPermit" runat="server">Not Uploaded</div>
                                                                      
                                                                        
                                                                    </td>
                                                                     <td><b>Time Table Copy</b></td>
                                                                    <td>
                                                                        <div id="divTimeTable" runat="server">Not Uploaded</div>
                                                                        
                                                                    </td>
                                                                   
                                                                    
                                                                </tr>
                                                                <tr>
                                                                  <td><b>Recommendation Letter</b></td>
                                                                  <td><div id="divRecommendation" runat="server">Not Uploaded</div></td>
                                                                  <td><b>MOM File</b></td>
                                                                  <td><div id="divMOM" runat="server">Not Uploaded</div></td>
                                                              </tr>
                                                              </table>
                                                               
                                                    </div>
                                                       
                                                  
                                                          
                                                              
                                                        </div></div>

                                                    <div class="row">
                                                        <br /><br />
                                                    <div class="col-md-12" style="text-align:center">
                                                      <asp:Button ID="btnCancel" runat="server" CssClass="btn default" Text="Close" OnClientClick="javascript:window.close();return false;"  />

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

