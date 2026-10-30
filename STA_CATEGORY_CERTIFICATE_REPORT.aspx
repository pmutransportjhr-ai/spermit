<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="STA_CATEGORY_CERTIFICATE_REPORT.aspx.cs" Inherits="STA_CATEGORY_CERTIFICATE_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
      <input id="txtScreen" type="text" value="STA_CATEGORY_CERTIFICATE_REPORT.aspx" style="display: none;" />
    <input type="text" id="txtColCnt" style="display: none;" value="14" />

    <form id="Form1" runat="server" autocomplete="off">
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
                                <span>Category Report </span>
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
                                                <span class="caption-subject font-red sbold uppercase">Category Report</span>
                                            </div>
                                        </div>
                                       
                                                        
                                                    
                                                </div><br /><br />
                                        <%--<div class="portlet-body">
                                            <div class="table-toolbar">
                                               
                                                  
                                                    
                                                </div><br /><br />--%>
                                            <%--</div>
                                            
                                                 <div class="col-md-2"></div>
                                                 </div>
                                            <br /><br />
                                         
                                            <br /><br />--%>
                                      <%--      <div id="tblISPermit" runat="server" visible="false">--%>
                                            <table class="table table-striped table-hover table-bordered" id="sample_1">
                                                <thead>
                                                    <tr>
                                                        <th>Application No.</th>
                                                       
                                                        <th>Registration No.</th>
                                                        <th>Mobile Number</th>
                                                        <th>Seating Capacity</th>
                                                        <th>Sleeper Capacity</th>
                                                        <th>Vehicle Type</th>
                                                       
                                                     <%--   <th>Name</th>
                                                        <th>Remarks</th>
                                                        <th>Other</th>--%>
                                                       <th>View</th>
                                                   


                                                    </tr>
                                                </thead>
                                              <tbody id ="STA_CATEGORY_CERTIFICATE_REPORT">
                                                  <%=FillInterStatePermitList %>
                                              
                                              </tbody>                                            
                                            </table>
                                         <%--   </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
    


    </ContentTemplate>
        </asp:UpdatePanel>
                        </div>
                           
                    <%--<div class="modal-footer">
                        <asp:HiddenField ID="txtDelKey" runat="Server" Value="0" />
                        <button type="button" class="btn dark btn-outline" data-dismiss="modal">Cancel</button>
                        <asp:Button ID="btnAction" runat="server" CssClass="btn red" OnClientClick="javascript:return fnValidateAction();" Text="YES" OnClick="btnAction_Click" />
                    </div>--%>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
   
   

   
   </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <script src="assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/datatables.js" type="text/javascript"></script>
    <script src="assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="assets/pages/scripts/table-datatables-scroller.js" type="text/javascript"></script>
   <%--<script src="assets/js/STA_COUNTER_SIGNATURE_TAX_PAY.js" type="text/javascript"></script>--%>
       <script src="assets/js/CategoryCertificate.js" type="text/javascript"></script>
     <script src="assets/pages/scripts/components-date-time-pickers.js" type="text/javascript"></script>

    <script type="text/javascript">
        function printObjetionTblDiv(divId) {
            //document.getElementById(divId).style.backgroundImage = "url('assets/img/Untitled.png')";
            //document.body.style.backgroundImage = "url('assets/img/Untitled.png')";
            //var printContents = document.getElementById(divId).innerHTML;//$('#stameetingObjetionTblBody').html(); 
            //var originalContents = document.body.innerHTML;            
            //document.body.innerHTML = printContents;
            //window.print();

            var toPrint;
            toPrint = window.open();
            toPrint.document.write(document.getElementById(divId).innerHTML);
            toPrint.print();
            toPrint.close();

        }


    </script>
</asp:Content>

