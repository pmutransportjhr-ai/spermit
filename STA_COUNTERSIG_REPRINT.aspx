<%--<%@ Page Title="" Language="C#" MasterPageFile="~/JHTRANSPORT.master" AutoEventWireup="true" CodeFile="STA_COUNTERSIG_REPRINT.aspx.cs" Inherits="STA_COUNTERSIG_REPRINT" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="JHTRANSPORT.master" CodeFile="STA_COUNTERSIG_REPRINT.aspx.cs" Inherits="STA_COUNTERSIG_REPRINT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .errorClass {
            border-bottom-color:red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                            <a href="STA_COUNTERSIG_REPRINT.aspx">STA COUNTER SIGNATURE TAX TOKEN REPRINT</a>
                        </li>
                       
                    </ul>
                    <div class="page-content-inner">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="portlet light portlet-fit">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            <i class="icon-settings font-red"></i>
                                            <span class="caption-subject font-red sbold uppercase">STA COUNTER SIGNATURE TAX TOKEN REPRINT</span>
                                        </div>
                                    </div>
                                    <div class="portlet-body">
                                          <form id="Form1" runat="server">
                                              <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" />
                                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>

                                                  <input type="hidden" name="requestparam" id="hdnenc_val" />
                                                 <asp:HiddenField ID="lblencyValue" runat="server"></asp:HiddenField>
                                                    
                                                <div class="row">
                                                <div class ="col-md-2"><b>Application Number</b><span style="color:red">*</span></div>
                                                <div class ="col-md-3">
                                                   <input type="text" id="txtApplicationNo" class="form-control" />
                                                    <span style="color:red;font-size:smaller;display:none" id="errorSpan">Please enter your Application Number</span>
                                                </div>
                                                <div class ="col-md-1">
                                                    <a onclick="javascript:fnValidate()" class="btn btn-info">Go</a>

                                                </div>
                                                <div class="col-md-2"> 
                                                    <asp:Button ID="btnSubmit" Text ="Submit" runat="server" OnClick="btnSubmit_Click"  cssClass="btn btn-success" style="display:none" />
                                                    
                                                </div>

                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <span>
                                                            <label id="lblMessage" style="display:none"></label>
                                                        </span>
                                                    </div>

                                                </div>
                                                     </ContentTemplate>
        </asp:UpdatePanel>
                                       </form>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript">

        $('input[id*=btnSubmit]').on('click', function () {
            $("#loadingmessage").modal("show");
        });


        function fnValidate() {
           // alert('fn call');
            $("#loadingmessage").modal("show");
            var applicationNo = $('input[id*=txtApplicationNo]').val();

            if (applicationNo == "" || applicationNo == null) {
                $('input[id*=txtApplicationNo]').addClass('errorClass');
                $('#errorSpan').show();
                $("#loadingmessage").modal("hide");
                return false;
            }
            else {
                $('input[id*=txtApplicationNo]').removeClass('errorClass');
                $('#errorSpan').hide();

                $.ajax({
                    type: "POST",
                    url: "api/doubleVerification.ashx?q=" + encodeURIComponent($('input[id*=txtApplicationNo]').val()) + "&caller=GetPaymentStatus",
                    data: "",
                    cache: false,
                    async: false,
                    success: function (data) {

                        var resp = data.split('~')[0];
                        var respMsg = data.split('~')[1];
                        if (resp == "Success") {
                            switch (respMsg) {
                                case "Submitted":
                                    var txnamount = data.split('~')[2];
                                    var txnRefNo = data.split('~')[3];
                                    var encryData = data.split('~')[2];
                                    $('input[id*=lblencyValue]').val(encryData);
                                    $('#lblMessage').show();
                                    if (txnRefNo && txnRefNo !== "undefined") {
                                        $('#lblMessage').text("Payment of Amount: " + txnamount + " has been submitted Successfully with Transaction Ref. No: " + txnRefNo);
                                    } else {
                                        $('#lblMessage').text("Payment details submitted successfully. Please click Submit button to Verify.");
                                    }
                                    $('input[id*=btnSubmit]').show();
                                    break;
                                case "2 Hrs.":
                                    $('#lblMessage').show();
                                    $('#lblMessage').text("Please Verify your payment after 2 hrs.");
                                    $('input[id*=btnSubmit]').hide();
                                    break;
                                case "Failed":
                                    $('#lblMessage').show();
                                    $('#lblMessage').text("Payment transcation failed please click on Track Application Status on Home Page and try again.");
                                    $('input[id*=btnSubmit]').hide();
                                    break;
                                case "encry":
                                    var encryData = data.split('~')[2];
                                    $('input[id*=lblencyValue]').val(encryData);
                                    $('#lblMessage').show().text('Please click submit button to Verify.');
                                    $('input[id*=btnSubmit]').show();

                                    break;
                                case "exp":
                                    var expData = data.split('~')[2];
                                    $('#lblMessage').show();
                                    $('#lblMessage').text(expData);
                                    $('input[id*=btnSubmit]').hide();
                                    break;
                                case "Not Found":
                                    $('#lblMessage').show();
                                    $('#lblMessage').text('Application number not found.');
                                    $('input[id*=btnSubmit]').hide();
                                    break;
                                default:
                                    alert('Try again later...');
                                    $('#lblMessage').hide();
                                    $('input[id*=btnSubmit]').hide();
                                    break;
                            }
                        }
                        hideLoadingModal();
                    },
                    error: function (response) {
                        hideLoadingModal();
                        alert(response.status + ' ' + response.statusText);

                    }
                });
            }
            hideLoadingModal();
        }

        function hideLoadingModal() {
            $("#loadingmessage").modal("hide");
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
        }
        $('input[id*=txtApplicationNo]').on('focus', function () {
            $('input[id*=txtApplicationNo]').removeClass('errorClass');
            $('#errorSpan').hide();
        });



    </script>
</asp:Content>
