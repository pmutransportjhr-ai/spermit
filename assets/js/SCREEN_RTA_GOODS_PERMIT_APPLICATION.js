

jQuery(document).ready(function () {
    if ($('input[id*=MFSERVER]').val() != 'YES') {
        fnManufactDate();
    }

    $('select[id*=ddlPermitType]').on('change', function () {
    
        $('div[id*=onPanel1]').hide();

        fnClearPermitNew();

        


    });



})
function selfCheck() {

    //$('.rtoHdr').each(function(){
    //    this.checked = false; 
    //});
    //$('.StateHDR').each(function(){ 
    //    this.checked = false; 
    //});
   
    $('input[id*=WholeStateCheckbox]').prop('checked', false);

}

function fnWholeState() {
    $('.rtoHdr').each(function () {
        this.checked = false;
    });
    $('.StateHDR').each(function () {
        this.checked = false;
    });

    $('input[id*=SelfCheckBox]').prop('checked', false);
    $('input[id*=otherRTOCode]').val('');
}

function otherSate() {
    $('.rtoHdr').each(function () {
        this.checked = false;
    });

    $('input[id*=SelfCheckBox]').prop('checked', false);
    //  $('input[id*=WholeStateCheckbox]').prop('checked', false);

    //alert($(".StateHDR:checkbox:checked").length);

    var lenght = $(".StateHDR:checkbox:checked").length;
    if (lenght > 2) {
        alert('Please Select only Two States');
        $('.StateHDR').each(function () {
            this.checked = false;
        });

    }


}


function otherRTOCode(ID) {

    $('.StateHDR').each(function () {
        this.checked = false;
    });

    // $('input[id*=SelfCheckBox]').prop('checked', false);
    $('input[id*=WholeStateCheckbox]').prop('checked', false);
    $('input[id*=otherRTOCode]').val(ID);

}


function fnddlChange()
{
    $('div[id*=Panel1]').hide();

    fnClearPermitNew();

}


function fnManufactDate() {

    var getdate = new Date();
    var yearstart = parseInt(getdate.getFullYear()) - parseInt(12);
    var monthsStart = parseInt(getdate.getMonth()) + parseInt(1);
    var startdate = monthsStart + "/" + yearstart;
    var endyear = getdate.getFullYear();
    var monthEnd = parseInt(getdate.getMonth());
    var endDate = monthEnd + "/" + endyear;
    $('input[id*=VManufacturingON]').Monthpicker({ minYear: yearstart, maxYear: endyear, minValue: startdate, maxValue: endDate })
  
}
function SaveApplication() {
     
    $("input[class='rtoHdr']:checked").each(function () {
      
        $('#otherRTOCode').val(($(this).val()));
    });
     
    var checkedstate = new Array();
    $("input[class='StateHDR']:checked").each(function () {
        checkedstate.push(($(this).val()));
    })
    var stateCodeValidity ="S";
    if (checkedstate.length > Number(2))
    {
        alert("Please Select Only Two States");
        $("input[class='StateHDR']:checked").each(function () {
            this.checked = false;
        })
        return false;

    }
    for (var j = 0; j < checkedstate.length; j++)
    {
        stateCodeValidity += checkedstate[j] + "-";
        

    }
    
    $('input[id*=hdnStateCode]').val(stateCodeValidity);
     
    if (fnApplicationValidate()) {
        return true;
    }

    else {

        return false;
    }
}
function fnSearchValidate() {
    if ($('input[id*=txtVehicleNumber]').val() == null || $('input[id*=txtVehicleNumber]').val() == "") {
        alert("Please enter Vehicle Registration Number");
        $('input[id*=txtVehicleNumber]').focus();
        return false;
    }
    var x = $('input[id*=txtVehicleNumber]').val();
    if (x.lenght<4) {
        alert("Please enter Valid Jharkhand Vehicle Registration Number ");
        $('input[id*=txtVehicleNumber]').focus();
        return false;
    }
    if (x.substring(0, 4).toUpperCase() == 'JH01' || x.substring(0, 4).toUpperCase() == 'JH02' ||
        x.substring(0, 4).toUpperCase() == 'JH03' || x.substring(0, 4).toUpperCase() == 'JH04' ||
        x.substring(0, 4).toUpperCase() == 'JH05' || x.substring(0, 4).toUpperCase() == 'JH06' ||
        x.substring(0, 4).toUpperCase() == 'JH07' || x.substring(0, 4).toUpperCase() == 'JH08' ||
        x.substring(0, 4).toUpperCase() == 'JH09' || x.substring(0, 4).toUpperCase() == 'JH10' ||
        x.substring(0, 4).toUpperCase() == 'JH11' || x.substring(0, 4).toUpperCase() == 'JH12' ||
        x.substring(0, 4).toUpperCase() == 'JH13' || x.substring(0, 4).toUpperCase() == 'JH14' ||
        x.substring(0, 4).toUpperCase() == 'JH15' || x.substring(0, 4).toUpperCase() == 'JH16' ||
        x.substring(0, 4).toUpperCase() == 'JH17' || x.substring(0, 4).toUpperCase() == 'JH18' ||
        x.substring(0, 4).toUpperCase() == 'JH19' || x.substring(0, 4).toUpperCase() == 'JH20' ||
        x.substring(0, 4).toUpperCase() == 'JH21' || x.substring(0, 4).toUpperCase() == 'JH22' ||
        x.substring(0, 4).toUpperCase() == 'JH23' || x.substring(0, 4).toUpperCase() == 'JH24'

        ) {
        
    }
    else
    {
        alert("Please enter Valid Jharkhand Vehicle Registration Number ");
        $('input[id*=txtVehicleNumber]').focus();
        return false;
    }
    $('#loadingmessage').modal('show');
    return true;

}
function fnClearPermitNew() {
    $('input[id*=VOName]').val('');
    $('input[id*=VOCareOf]').val('');
    $('textarea[id*=VOPresentAddress]').val('');
    $('textarea[id*=VOPermanentAddress]').val('');
    $('input[id*=VType]').val('');
    $('input[id*=VChassisNumber]').val('');
    $('input[id*=VEngineNumber]').val('');
    $('input[id*=VMakerDesc]').val('');
    $('input[id*=VMakerModel]').val('');
    $('input[id*=VFuelDesc]').val('');
    $('input[id*=VColor]').val('');
    $('input[id*=VFitness]').val('');
    $('input[id*=VTax]').val('');
    $('input[id*=VULW]').val('');
    $('input[id*=VRLW]').val('');
    $('input[id*=VNatureofGoods]').val('');
    $('input[id*=VManufacturingON]').val('');
    $('input[id*=txtRates]').val('');
    $('input[id*=txtVCapacity]').val('');
    $('input[id*=txtMobileNo]').val('');
    $('input[id*=txtEmailId]').val('');
    $('input[id*=txtICValidUpto]').val('');
    $('input[id*=txtFCValidUpto]').val('');
    $('input[id*=txtPCValidUpto]').val('');
    //$('input:radio[name*="gender"]').prop('checked', false);
    //$("input[id*=txtMale]").parent('span').removeClass('checked');
    //$("input[id*=txtFemale]").parent('span').removeClass('checked');
    $('input[id*=txtpermitNo]').val('');
    $('input[id*=txtpermitFrom]').val('');
    $('input[id*=txtpermitTo]').val('');
    $('input[id*=txtVehicleNumber]').val('');
    $('input[id*=VORegNumber]').val('');
   
    $('input[id*=otherRTOCode]').val('')
   
}
function fnApplicationValidate() {
  
    $('.form-control').css('border-color', '#c2cad8');

    var VOName = $('input[id*=VOName]').val().trim();
    if (VOName == "" || VOName == null) {
        alert("Please enter Name");
        $('input[id*=VOName]').css('border-color', 'red').focus();
        return false;
    }
    if (VOName.length < 3) {
        alert("Enter atleast 3 chars in the name textbox");
        $('input[id*=VOName]').css('border-color', 'red').focus();
        return false;
    }
    var VOCareOf = $('input[id*=VOCareOf]').val().trim();
    if (VOCareOf == "" || VOCareOf == null) {
        alert("Please enter C/O Name");
        $('input[id*=VOCareOf]').css('border-color', 'red').focus();
        return false;
    }
    if (VOCareOf.length < 3) {
        alert("Enter atleast 3 chars in the c/o name textbox");
        $('input[id*=VOCareOf]').css('border-color', 'red').focus();
        return false;
    }
    var VOPresentAddress = $('textarea[id*=VOPresentAddress]').val().trim();
    if (VOPresentAddress == "" || VOPresentAddress == null) {
        alert("Please enter present Address");
        $('textarea[id*=VOPresentAddress]').css('border-color', 'red').focus();
        return false;
    }
    var VOPermanentAddress = $('textarea[id*=VOPermanentAddress]').val().trim();
    if (VOPermanentAddress == "" || VOPermanentAddress == null) {
        alert("Please enter Permanent Address");
        $('textarea[id*=VOPermanentAddress]').css('border-color', 'red').focus();
        return false;
    }
    if ($('input[id*=SelfCheckBox]').prop('checked') == false && $('input[id*=WholeStateCheckbox]').prop('checked') == false) {
        alert("Please select at least One CheckBox");
        return false;

    }
    var x = $('input[id*=VORegNumber]').val();
    if (x.lenght < 4) {
        alert("Please enter Valid Jharkhand Vehicle Registration Number ");
        $('input[id*=VORegNumber]').focus();
        return false;
    }
    if (x.substring(0, 4).toUpperCase() == 'JH01' || x.substring(0, 4).toUpperCase() == 'JH02' ||
        x.substring(0, 4).toUpperCase() == 'JH03' || x.substring(0, 4).toUpperCase() == 'JH04' ||
        x.substring(0, 4).toUpperCase() == 'JH05' || x.substring(0, 4).toUpperCase() == 'JH06' ||
        x.substring(0, 4).toUpperCase() == 'JH07' || x.substring(0, 4).toUpperCase() == 'JH08' ||
        x.substring(0, 4).toUpperCase() == 'JH09' || x.substring(0, 4).toUpperCase() == 'JH10' ||
        x.substring(0, 4).toUpperCase() == 'JH11' || x.substring(0, 4).toUpperCase() == 'JH12' ||
        x.substring(0, 4).toUpperCase() == 'JH13' || x.substring(0, 4).toUpperCase() == 'JH14' ||
        x.substring(0, 4).toUpperCase() == 'JH15' || x.substring(0, 4).toUpperCase() == 'JH16' ||
        x.substring(0, 4).toUpperCase() == 'JH17' || x.substring(0, 4).toUpperCase() == 'JH18' ||
        x.substring(0, 4).toUpperCase() == 'JH19' || x.substring(0, 4).toUpperCase() == 'JH20' ||
        x.substring(0, 4).toUpperCase() == 'JH21' || x.substring(0, 4).toUpperCase() == 'JH22' ||
        x.substring(0, 4).toUpperCase() == 'JH23' || x.substring(0, 4).toUpperCase() == 'JH24'

        ) {

    }
    else {
        alert("Please enter Valid Jharkhand Vehicle Registration Number ");
        $('input[id*=VORegNumber]').focus();
        return false;
    }
    var permitNo = $('input[id*=txtpermitNo]').val().trim();
    if (permitNo == "" || permitNo == null) {
        alert("Please enter permit number");
        $('input[id*=txtpermitNo]').css('border-color', 'red').focus();
        return false;
    }


    var permitFrom = $('input[id*=txtpermitFrom]').val().trim();
    if (permitFrom == "" || permitFrom == null) {
        alert("Please select permit valid from date ");
        $('input[id*=txtpermitFrom]').css('border-color', 'red').focus();
        return false;
    }
    var permitTo = $('input[id*=txtpermitTo]').val().trim();
    if (permitTo == "" || permitTo == null) {
        alert("Please select permit valid upto date");
        $('input[id*=txtpermitTo]').css('border-color', 'red').focus();
        return false;
    }



    var VType = $('input[id*=VType]').val().trim();
    if (VType == "" || VType == null) {
        alert("Please enter Vehicle class description");
        $('input[id*=VType]').css('border-color', 'red').focus();
        return false;
    }

    var VChassisNumber = $('input[id*=VChassisNumber]').val().trim();
    if (VChassisNumber == "" || VChassisNumber == null) {
        alert("Please enter Vehicle chassis number");
        $('input[id*=VChassisNumber]').css('border-color', 'red').focus();
        return false;
    }
    var VEngineNumber = $('input[id*=VEngineNumber]').val().trim();
    if (VEngineNumber == "" || VEngineNumber == null) {
        alert("Please enter Vehicle Engine Number");
        $('input[id*=VEngineNumber]').css('border-color', 'red').focus();
        return false;
    }
   
    if ($('input[id*=VMakerDesc]').val().trim().length != "") {
        if ($('input[id*=VMakerDesc]').val().trim().length < 3) {
            alert("Enter atleast 3 chars in the maker description textbox");
            $('input[id*=VMakerDesc]').css('border-color', 'red').focus();
            return false;
        }
    }
    if ($('input[id*=VMakerModel]').val().trim().length != "") {

        if ($('input[id*=VMakerModel]').val().trim().length < 3) {
            alert("Enter atleast 3 chars in the maker model no. textbox");
            $('input[id*=VMakerModel]').css('border-color', 'red').focus();
            return false;
        }
    }
    // if ($('input[id*=VFuelDesc]').val().trim().length != "") {

        // if ($('input[id*=VFuelDesc]').val().trim().length < 3) {
            // alert("Enter atleast 3 chars in the fuel description textbox");
            // $('input[id*=VFuelDesc]').css('border-color', 'red').focus();
            // return false;
        // }
    // }
    // if ($('input[id*=VColor]').val().trim().length != "") {


        // if ($('input[id*=VColor]').val().trim().length < 3) {
            // alert("Enter atleast 3 chars in the color textbox");
            // $('input[id*=VColor]').css('border-color', 'red').focus();
            // return false;
        // }
    // }
    var VFitness = $('input[id*=VFitness]').val().trim();
    if (VFitness == "" || VFitness == null) {
        alert("Please enter fitness valid upto date");
        $('input[id*=VFitness]').css('border-color', 'red').focus();
        return false;
    }
    var VTax = $('input[id*=VTax]').val().trim();
    if (VTax == "" || VTax == null) {
        alert("Please enter tax valid upto date");
        $('input[id*=VTax]').css('border-color', 'red').focus();
        return false;
    }
    var VULW = $('input[id*=VULW]').val().trim();
    if (VULW == "" || VULW == null) {
        alert("Please enter U. L. W. as per RC");
        $('input[id*=VULW]').css('border-color', 'red').focus();
        return false;
    }
    // if (VULW <= parseInt(0)) {
        // alert("Please enter valid data  in the U. L. W. as per RC textbox");
        // $('input[id*=VULW]').css('border-color', 'red').focus();

        // return false;
    // }
   
    var VRLW = $('input[id*=VRLW]').val().trim();
    if (VRLW == "" || VRLW == null) {
        alert("Please enter R. L. W. as per RC");
        $('input[id*=VRLW]').css('border-color', 'red').focus();
        return false;
    }
    // if (VRLW <= parseInt(0)) {
        // alert("Please enter valid data  in the R. L. W. as per RC textbox");
        // $('input[id*=VRLW]').css('border-color', 'red').focus();

        // return false;
    // }
    
    var VNatureofGoods = $('input[id*=VNatureofGoods]').val().trim();
    if (VNatureofGoods == "" || VNatureofGoods == null) {
        alert("Please enter Nature of goods proposed to be carried");
        $('input[id*=VNatureofGoods]').css('border-color', 'red').focus();
        return false;
    }
   
    var VManufacturingON = $('input[id*=VManufacturingON]').val().trim();
    if (VManufacturingON == "" || VManufacturingON == null) {
        alert("Please enter Manufacturing date");
        $('input[id*=VManufacturingON]').css('border-color', 'red').focus();
        return false;
    }
    var VRates = $('input[id*=txtRates]').val().trim();
    if (VRates == "" || VRates == null) {
        alert("Please enter Rates to be charged by the applicant for carriage of goods");
        $('input[id*=txtRates]').css('border-color', 'red').focus();
        return false;
    }
    var VCapacity = $('input[id*=txtVCapacity]').val().trim();
    if (VCapacity == "" || VCapacity == null) {
        alert("Please enter Capacity");
        $('input[id*=txtVCapacity]').css('border-color', 'red').focus();
        return false;
    }
    if ($('input[id*=txtICValidUpto]').val() == null || $('input[id*=txtICValidUpto]').val() == "") {

        alert("Please select Insurance Certificate Validity Date");
        $('input[id*=txtICValidUpto]').css('border-color', 'red').focus();
        return false;
    }

    if ($('input[id*=txtPCValidUpto]').val() == null || $('input[id*=txtPCValidUpto]').val() == "") {
        alert("Please select Pollution Certificate Validity Date");
        $('input[id*=txtPCValidUpto]').css('border-color', 'red').focus();
        return false;
    }

    if ($('input[id*=txtFCValidUpto]').val() == null || $('input[id*=txtFCValidUpto]').val() == "") {
        alert("Please select Fitness Certificate Validity Date");
        $('input[id*=txtFCValidUpto]').css('border-color', 'red').focus();
        return false;
    }
    var MobileNo = $('input[id*=txtMobileNo]').val().trim();
    //if (MobileNo == "" || MobileNo == null) {
    //    alert("Please enter user mobile no");
    //    $('input[id*=txtMobileNo]').css('border-color', 'red').focus();
    //    return false;
    //}
    if (MobileNo != "") {
        if (!validateMob(MobileNo)) {
            alert("Invalid Mobile Number");
            $('input[id*=txtMobileNo]').css('border-color', 'red').focus();
            return false;
        }
    }
    var EmailID = $('input[id*=txtEmailId]').val().trim();
    if (EmailID != "") {

        if (!IsEmail(EmailID)) {
            alert("Invalid Email ID");
            $('input[id*=txtEmailId]').css('border-color', 'red').focus();
            return false;
        }
    }
    if ($("input[name*='gender']:checked").length <= 0) {
        alert("Please select gender option");
        return false;
    }
  
   
    
    

    return true;
}



