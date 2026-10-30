<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STAPermitReceipt.aspx.cs" Inherits="STAPermitReceipt" %>

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
                                <a href="PermitHomePage.aspx">Home</a>
                                <i class="fa fa-circle"></i>
                            </li>
                            <li>
                                <span>Permit Receipt</span>
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
                                                        <span class="caption-subject font-red sbold uppercase">Permit Receipt</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                   <div id="IssuedReceiptDiv">
                                                    <div class="row">
                                                         <div class="col-md-6 col-sm-6">
                                                                    <b  style="font-size:large">Permit Type : </b>
                                                                        <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                         </div>
                                                        <div class="col-md-6 col-sm-6">
                                                                    </div> </div>
                                                     <div class="row">
                                                        <div class="col-md-6 col-sm-6">
                                                                <b style="font-size:large">Permit No :</b>
                                                                    <asp:Label ID="lblPermitNo" runat="server"></asp:Label>
                                                            </div>
                                                                        <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                    <b  style="font-size:large">Status : </b>
                                                                        <asp:Label ID="lblApplicationStatus" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                         </div></div>
                                                     <div class="row">
                                                        <div class="col-md-6 col-sm-6">
                                                                <b style="font-size:large">Valid From :</b>
                                                                    <asp:Label ID="lblValidFrom" runat="server"></asp:Label>
                                                            </div>
                                                                        <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                    <b  style="font-size:large">Valid Upto : </b>
                                                                        <asp:Label ID="lblValidUpto" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                         </div></div><br />
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
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Vehicle Details</u></b></h4>
                                                    </div>
                                                    <div class="col-md-12 col-sm-12">

                                                         <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    
                                                                    <td><b>Registration No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VORegNumber" runat="server"></asp:Label></td>
                                                                   <td><b>Vehicle Class Description:</b></td>
                                                                    <td>
                                                                        <asp:Label ID="VType" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Chassis Number : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VChassisNumber" runat="server"></asp:Label></td>
                                                                    <td><b>Engine Number  : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VEngineNumber" runat="server"></asp:Label></td>
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
                                                                    <td><b>Fuel Description :  </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblFuelDesc" runat="server"></asp:Label></td>
                                                                    <td><b>Color : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblVcolor" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Fitness Valid Upto: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VFitness" runat="server"></asp:Label></td>

                                                                  <td><b>Tax Paid UpTo: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VTax" runat="server"></asp:Label></td>
                                                                  </tr>
                                                                <tr>

                                                                                <td><b>U. L. W. as per RC: </b></td>
                                                                                <td>  <asp:Label ID="VULW" runat="server"></asp:Label></td>
                                                                                <td><b>R. L. W. as per RC: </b></td>
                                                                                <td> <asp:Label ID="VRLW" runat="server"></asp:Label></td>
                                                                               
                                                                            </tr>
                                                                            <tr>
                                                                               
                                                                                
                                                                                <td><b>Seating Capacity: </b></td>
                                                                                <td> <asp:Label ID="VSeatCapacity" runat="server"></asp:Label></td>
                                                                               <td><b>Manufacturing Date:</b></td>
                                                                               <td> <asp:Label ID="VManufacturingON" runat="server"></asp:Label></td>
                                                                                                                                                           
                                                                               </tr>
                                                            </table>
                                                       
                                                        

                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Route Details</u></b></h4>
                                                    </div>

                                                    <asp:Label CssClass="col-md-12 col-sm-12 control-label" Font-Bold="true" ID="RouteHDRInfo" runat="server" />
                                                </div>
                                                 <br />
                                                                Time Format: 24 Hours <br />
                                                <div id="RouteTimeInfo" runat="server"></div>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Other Details</u></b></h4>
                                                    </div>
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
                                                                    
                                                                    <td><b>Gender : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblGender" runat="server"></asp:Label></td>
                                                                   <td><b>Qualification : </b></td>
                                                                    <td><asp:Label ID="lblQualification" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    
                                                                    <td><b>Category of Applicant : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblCategory" runat="server"></asp:Label></td>
                                                                   <td><b>Ex-Army category :</b></td>
                                                                    <td><asp:Label ID="lblExArmy" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    
                                                                    
                                                                   <td><b>Minority :  </b></td>
                                                                    <td><asp:Label ID="lblMinority" runat="server"></asp:Label></td>
                                                                       <td><b>Handicapped :</b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblHandicapped" runat="server"></asp:Label></td>
                                                                
                                                                </tr>
                                                                <tr>
                                                                    
                                                                    <td><b>Heavy Motor Licence : </b></td>
                                                                    <td><asp:Label ID="lblHML" runat="server"></asp:Label></td>
                                                                    <td><b> Identity Proof : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblIdentityProof" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    
                                                                    <td><b>Bus Type (AC) :</b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblAC" runat="server"></asp:Label></td>
                                                                   <td><b>Bus Type (Deluxe) : </b></td>
                                                                    <td><asp:Label ID="lblDeluxe" runat="server"></asp:Label></td>
                                                                </tr>
                                                            <tr>
                                                                    
                                                                    <td><b>Bus Type (Sleeper) :</b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblSleeper" runat="server"></asp:Label></td>
                                                                   <td></td>
                                                                    <td></td>
                                                                </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                               
                                               
                                                

                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12">
                                                        <h4><b><u>Uploaded Documents</u></b></h4>
                                                    </div>

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
                                                                    <td><b>Qualification Certificate</b></td>
                                                                    <td>
                                                                        <div id="divQualification" runat="server">NA</div>
                                                                      
                                                                        
                                                                    </td>
                                                                    <td><b>Category Certificate</b></td>
                                                                    <td>
                                                                        <div id="divSCST" runat="server">NA</div>
                                                                      
                                                                        
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Ex-Army Certificate</b></td>
                                                                    <td>
                                                                        <div id="divExArmy" runat="server">NA</div>
                                                                      
                                                                        
                                                                    </td>
                                                                     <td><b>Handicapped Certificate</b></td>
                                                                    <td>
                                                                        <div id="divHandicapped" runat="server">NA</div>
                                                                      
                                                                        
                                                                    </td>
                                                                   
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Heavy Motor Licence</b></td>
                                                                    <td>
                                                                        <div id="divHML" runat="server">NA</div>
                                                                      
                                                                        
                                                                    </td>
                                                                    <td><b>Counter Signature Certificate</b></td>
                                                                    <td><div id="divCS" runat="server">NA</div></td>
                                                                </tr>
                                                                <tr>
                                                                   <td><b>Voter Id/ Pan Card/ Aadhaar Card No.</b></td>
                                                                    <td>
                                                                        <asp:Label ID="lblIDNo" runat="server"></asp:Label>
                                                                      
                                                                        
                                                                    </td>
                                                                    
                                                                     <td><b>Identity Proof Certificate</b></td>
                                                                    <td>
                                                                        <div id="divIdentityProof" runat="server">NA</div>
                                                                      
                                                                        
                                                                    </td>
                                                                </tr>
                                                        </table>

                                                    </div>
                                                </div>
                                                    </div>
                                                    <div class="row">
                                                        <br /><br />
                                                    <div class="col-md-12" style="text-align:center">
                                                              <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv(IssuedReceiptDiv)">Print</button>


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

