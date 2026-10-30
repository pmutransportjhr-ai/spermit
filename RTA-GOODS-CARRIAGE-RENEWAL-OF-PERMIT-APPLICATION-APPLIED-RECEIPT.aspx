<%@ Page Title="" Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="RTA-GOODS-CARRIAGE-RENEWAL-OF-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx.cs" Inherits="RTA_GOODS_CARRIAGE_RENEWAL_OF_PERMIT_APPLICATION_APPLIED_RECEIPT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="RTA-GOODS-CARRIAGE-RENEWAL-OF-PERMIT-APPLICATION-APPLIED-RECEIPT.aspx" style="display: none;" />
    <%--<input type="text" id="txtColCnt" style="display: none;" value="8" />--%>

    <form runat="server" autocomplete="off">
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
                                <span>RTA Good's Carriage Renewal of Permit Application- Receipt</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                            <div class="col-md-12 col-sm-12" style="text-align: right">
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
                                                        <span class="caption-subject font-red sbold uppercase">RTA Good's Carriage Renewal of Permit Application- Receipt</span>
                                                    </div>
                                                </div>
                                                <div class="portlet-body">
                                                   
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12">
                                                            <asp:HiddenField ID="ID" runat="server" />
                                                             <asp:HiddenField ID="EncryptedParameter" runat="server" />
                                                        </div>
                                                        <div class="col-md-6 col-sm-6"><b style="font-size:large">RTO :</b>
                                                                              <asp:Label ID="lblRTO" runat="server"></asp:Label></div>
                                                            
                                                        
                                                                        <div class="col-md-6 col-sm-6">
                                                                    <b  style="font-size:large">Permit Type : </b>
                                                                        <asp:Label ID="lblPermitType" runat="server"></asp:Label>
                                                                   </div><br /><br />    
                                                               <div class="col-md-6 col-sm-6">
                                                                <b style="font-size:large">Application No :</b>
                                                                    <asp:Label ID="lblApplicationNo" runat="server"></asp:Label>
                                                            </div>
                                                                            <div class="col-md-6 col-sm-6">
                                                                <b style="font-size:large">Permit No :</b>
                                                                    <asp:Label ID="lblPermitNo" runat="server"></asp:Label>
                                                            </div>
                                                               
                                                            <br /><br />
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
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Vehicle Details</b></u></h4></div>
                                                        <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    
                                                                    <td><b>Registration No : </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VORegNumber" runat="server"></asp:Label></td>
                                                                   <td></td>
                                                                    <td></td>
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
                                                                    <td><b>Tax Paid UpTo: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VTax" runat="server"></asp:Label></td>
                                                                    <td><b>Fitness Valid Upto: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VFitness" runat="server"></asp:Label></td>

                                                                </tr>
                                                                <tr>

                                                                    <td><b>Vehicle Class Type:</b></td>
                                                                    <td>
                                                                        <asp:Label ID="VType" runat="server"></asp:Label></td>
                                                                    <td><b>Insurance Company: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VInsuranceCmp" runat="server"></asp:Label></td>

                                                                </tr>
                                                                <tr>
                                                                    <td><b>Insurance Policy No: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VInsuranceNo" runat="server"></asp:Label></td>

                                                                    <td><b>Insurance Upto: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VInsuranceUpto" runat="server"></asp:Label></td>
                                                                </tr>
                                                            </table>

                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Other Details</b></u></h4></div>
                                                    
                                                        <table class="table table-striped table-bordered table-hover">
                                                               <tr>
                                                                                 <td><b>Unload weight(Kgs)</b></td>
                                                                                <td>  <asp:Label ID="lblUnloadWeight" runat="server"></asp:Label></td>
                                                                                <td><b>Gross Vehicle weight(Kgs)</b></td>
                                                                                <td>  <asp:Label ID="lblGrossvehicleWeight" runat="server"></asp:Label></td>
                                                                               
                                                                                
                                                                            </tr>   
                                                                <tr>
                                                                                 <td><b>Pay load(Kgs)</b></td>
                                                                                <td>  <asp:Label ID="lblPayLoad" runat="server"></asp:Label></td>
                                                                                <td><b>Hire purchaser /lease /hypothecation agreement with</b></td>
                                                                                <td>  <asp:Label ID="lblAgreementWith" runat="server"></asp:Label></td>
                                                                               
                                                                                
                                                                            </tr>                                                          
 <tr>
                                                                    <td><b>Mobile No: </b></td>
                                                                    <td>
                                                                        <asp:Label ID="VOMobileNo" runat="server"></asp:Label></td>
                                                                    <td></td>
                                                                    <td> </td>

                                                                </tr>
                                                            </table>
                                                    </div><br />
                                                    
                                                    
                                                    <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Permit Fee Details</b></u></h4></div>
                                                        <div class="col-md-3 col-sm-3"><b>Permit Fees :</b></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblPermitFees" runat="server"></asp:Label></div>
                                                        <div class="col-md-3 col-sm-3"><b>Payment Status:</b></div>
                                                        <div class="col-md-3 col-sm-3">
                                                            <asp:Label ID="lblPaymentStatus" runat="server"></asp:Label></div>
                                                    </div>
                                                  
                                                    
                                                    
                                                      <div class="row">
                                                        <div class="col-md-12 col-sm-12"><h4><b><u>Uploaded Documents</b></u></h4></div>
                                                    
                                                           <div class="col-md-12 col-sm-12">
                                                            <table class="table table-striped table-bordered table-hover">
                                                                <tr>
                                                                    <td><b>Registration Certificate</b></td>
                                                                    <td>
                                                                          <div id="divrcfile" runat="server">Not Uploaded</div>
                                                                   
                                                                    <td><b>Insurance Certificate</b></td>
                                                                    <td>
                                                                        <div id="divinsurFile" runat="server">Not Uploaded</div>

                                                                      
                                                                          
                                                                    
                                                                    </tr>
                                                                <tr>
                                                                    <td><b>Pollution Certificate</b></td>
                                                                    <td>
                                                                        <div id="divrollution" runat="server">Not Uploaded</div>
                                                                      
                                                                        
                                                                  
                                                                    <td><b>Fitness Certificate</b></td>
                                                                    <td>
                                                                        <div id="divfitness" runat="server">Not Uploaded</div>
                                                                      
                                                                        
                                                                    <td>

                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td><b>Permit Copy</b></td>
                                                                    <td>
                                                                        <div id="divPermit" runat="server">Not Uploaded</div>



                                                                        <td></td>
                                                                        <td>



                                                                            <td>
                                                                </tr>
                                                          
                                                              </table>
                                                               </div>
                                                          <br /><br />
                                                          <div class="col-md-6 col-sm-6" style="font-size:14px;">
                                                        <b>(The State/Reginoal Transport Authority)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <span>JHARKHAND</span></b>
                                                    </div>
                                                          <div class="col-md-6 col-sm-6" style="text-align:right">
                                                              <asp:PlaceHolder ID="plBarCode" runat="server" /></div>
                                                        </div>

                                                    
                                                </div>
                                            </div>
                                    </div>
                            </div>

                                                               </div>
                                                            <div class="row">
                                                        <div class="col-md-4 col-sm-4"></div>
                                                        <div class="col-md-4 col-sm-4">
                                                            <div style="text-align: center">
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
    <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-buttons.js" type="text/javascript"></script>
    <script type="text/javascript">
        function printDiv(divId) {

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

