<%@ Page Language="C#" MasterPageFile="JHTRANSPORT.master" AutoEventWireup="true" CodeFile="PaymentRequest.aspx.cs" Inherits="PaymentRequest" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input id="txtScreen" type="text" value="PaymentRequest.aspx" style="display: none;" />
    
    <%--<form runat="server" id="eGrassClient">--%>
<form name="eGrassClient1" method="post" runat="server">
	                                       <input type="hidden" id="APP_ID" runat="server" />
                                       <input type="hidden" id="APPLICATION_SERVICE" runat="server" />
</form>
   <form name="eGrassClient" method="post" action="https://finance.jharkhand.gov.in/jegras/payment.aspx">
    
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
                                <span>Tax Payment</span>
                            </li>
                        </ul>
                        <div class="page-content-inner">
                                   <div class="row">
                                     <input type="hidden" name="requestparam" id="hdnenc_val" />

                                     
                                      
                                       <br />
                                       <br />
                                       <br />
                                       <div class="form-group">
                                           <div class="col-md-5"></div>
                                           <div class="col-md-1"><%--<input  class="btn green" type="button" name="submit" value="Pay" />--%>
                                               <button class="btn green" name="submit" id="btnPay">Pay</button>
                                           </div>
                                           <div class="col-md-1">
                                               <a  id="btnPayCancel"  href="javascript:fnPayCancel();" class="btn red">Cancel</a>
                                               </div>
                                           <div class="col-md-5"></div>
                                           
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
    
    <script src="assets/js/Payment.js" type="text/javascript"></script>


</asp:Content>


