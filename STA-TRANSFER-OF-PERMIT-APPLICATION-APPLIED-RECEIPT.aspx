<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA-TRANSFER-OF-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx.cs" Inherits="STA_TRANSFER_OF_PERMIT_APPLICATION_APPLIED_RECEIPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form runat="server" autocomplete="off">
        <asp:HiddenField ID="hdnID" runat ="server" />
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
                                <span id="PageHeading" runat="server"></span>
                            </li>
                            <li> > 
                                Receipt
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                     <div class="row">
                                            <div class="col-md-12 col-sm-12" style="text-align: right">
                                                <%--<asp:Button ID="btnPrint" CssClass="btn btn-primary" runat="server" Text="Print" OnClientClick="return printDiv('IssuedReceiptDiv')"  />--%>
                                                <button id="btnPrint" type="button" class="btn btn-primary" style="background-color: #1d943b !important;" onclick="printDiv('IssuedReceiptDiv')">Print</button>
                                            </div>
                                        </div>
                                        <div id="IssuedReceiptDiv">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="portlet light portlet-fit">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        <i class="icon-settings font-red"></i>
                                                        <span class="caption-subject font-red sbold uppercase" id="PageHeading1" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                    <asp:HiddenField ID="EncryptedParameter" runat="server" />
                                                   
                                                    <div class="row">
                                                         <div class="col-md-12 col-sm-12" style="text-align:center">
                                                              <asp:PlaceHolder ID="plBarCode" runat="server" /></div>
                                                        
                                                        <div class="col-md-6 col-sm-6">
                                                                <b style="font-size:large">Application No :</b>
                                                                    <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                            </div>
                                                                        <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                    <b  style="font-size:large">Permit Type : </b>
                                                                        <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                   
                                                               
                                                               
                                                            </div>
                                                        <br />
                                                        
                                                        <div class="col-md-6 col-sm-6">
                                                              <%--  <b style="font-size:large">Old Registration No :</b>
                                                                    <asp:Label ID="lblOldRegNo" runat="server"></asp:Label>--%>
                                                            </div>
                                                                        <div class="col-md-6 col-sm-6" style="text-align:right">
                                                                   
                                                                   <b style="font-size:large">Permit No :</b>
                                                                    <asp:Label ID="lblPermitNumber" runat="server"></asp:Label>
                                                               
                                                               
                                                            </div><br /><br />
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
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Vehicle Details</u></b></h4></div>
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
                                                                   <td><b>Relation With Vehicle Owner</b></td>
                                                                    <td><asp:Label ID="lblRelation" runat="server"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><b>
                                                                        Is Vehicle owner Alive:</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblAliveorDead" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                            </div>
                                                    </div>
                                                    
                                                  
                                                    
                                                    
                                                      <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Uploaded Documents</b></u></h4></div>
                                                    
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
                                                                    <td><b>Inasurance Certificate Valid Upto </b></td>
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
                                                                    <td><b>Permit Certificate</b></td>
                                                                    <td>
                                                                        <div id="divPermit" runat="server">Not Uploaded</div>
                                                                                               </td>
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

                                                                  <tr>
                                                                   <td><b>Jharkhand Tax Token Valid upto</b></td>
                                                                    <td>
                                                                        <asp:Label ID="lbljhtTaxToken" runat="server"></asp:Label>
                                                                      
                                                                        
                                                                    </td>
                                                                    
                                                                     <td><b>Jharkhand Tax Token Certificate</b></td>
                                                                    <td>
                                                                        <div id="jhtTaxTokencertificate" runat="server">NA</div>
                                                                      
                                                                        
                                                                    </td>
                                                                </tr>

                                                                  <tr>
                                                                   <td><b>Affidavit regarding transfer of ownership of permit </b></td>
                                                                    <td>
                                                                       <div id="divAffidavit" runat="server">NA</div>
                                                                    </td>
                                                                     <td><b>Death Certificate</b></td>
                                                                    <td>
                                                                        <div id="DeathCertificate" runat="server">NA</div>
                                                                    </td>
                                                                </tr>

                                                              </table>
                                                               
                                                    </div>
                                                          <br />
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Fee Details</b></u></h4></div>
                                                        <div class="col-md-3 col-sm-3"><b>Fees :</b></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblPermitFees" runat="server"></asp:Label></div>
                                                        <div class="col-md-3 col-sm-3"><b>Payment Status:</b></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblPaymentStatus" runat="server"></asp:Label></div>
                                                  </div><br />
                                                    <div class="row">
                                                          <div class="col-md-12 col-sm-12" style="font-size:14px;">
                                                        <b>(The State/Reginoal Transport Authority)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <span>JHARKHAND</span></b>
                                                    </div>
                                                         </div>

                                                    
                                                </div>
                                            </div>
                                    </div>
                            </div>

                                                               
                                                    </div>
                                                            <div class="row" id="PaymentDiv" runat="server">
                                                        <div class="col-md-4 col-sm-4"></div>
                                                        <div class="col-md-4 col-sm-4">
                                                            <div style="text-align: center">
                                                                <%--<asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="btn green" Text="Make Payement" /></div>--%>
                                                                <input type="button" class="btn green" value="Make Payment" onclick="javascript: ShoePaymentConfirmModal();" />

                                                        </div>
                                                        <div class="col-md-4 col-sm-4"></div>
                                                    </div>
                                                </div>
                                                <div class="modal fade" id="confirm" tabindex="-1" role="dialog" aria-hidden="true">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                                                <h4 class="modal-title"><b>Are you sure, you want to pay the Amount?</b></h4>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn dark btn-outline" data-dismiss="modal">NO</button>
                                                                <asp:Button ID="btnYes" runat="server" CssClass="btn green" Text="YES" OnClick="btnYes_Click" />
                                                            </div>
                                                        </div>
                                                        <!-- /.modal-content -->
                                                    </div>
                                                    <!-- /.modal-dialog -->
                                                </div>
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
            width = width + 50;;
            document.body.innerHTML = "<html><head><title></title></head><body>" + printContents + "</body>";
            //"<html><head><title></title></head><body><img width='" + width + "' height='" + height + "' style='z-index:-1;position:absolute' src='assets/img/Untitled.png' alt ='image' />" + printContents + "</body>";


            window.print();
            document.body.innerHTML = originalContents;


        }
    </script>

</asp:Content>

