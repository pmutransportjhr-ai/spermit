jQuery(document).ready(function () {

      

})

   
    function fnApplyPermit(ID) {
        window.location.href = "STA-PERMIT-NEW.aspx?q=" + encodeURIComponent(ID)
    }
    function fnShowRoute(ID)
    {
        $('#loadingmessage').modal('show');
        $.ajax({
            type: "POST",
            url: "api/STA_PERMIT.ashx?caller=GetSTARouteDetails1&q=" + encodeURIComponent(ID),
            data:"",
            cache: false,
            async: false,
            success: function (data) {

                $('#loadingmessage').modal('hide');
                if (data != "No Data Found") {
                    var statusf1 = data.split('~');
                    if (statusf1[0] == "Success") {

                    
                        $('div[id*=RouteTimeInfo]').html(data.split(/~(.+)?/)[1].toString());
                        ComponentsDateTimePickers.init();
                        $('#Panel1').show();
                        $('input[id*=hdnID]').val(ID);
                        var NoRoundTrip = $('#txtNoRoundTrip').val();

                        for(j= 1; j<=NoRoundTrip; j++)
                        {
                            var length = $('#tbdRouteInfo_' + j + ' tr:last').attr('id').split("_");
                            $('#tbdRouteInfo_' + j + '  tr').each(function () {
                                var parentid = $(this).attr('id').split("_");

                                if ($('#txtUPArrRoute' + j + '_' + parentid[1]).attr("placeholder", "_")) 
                                {
                                    $('#txtUPArrRoute' + j + '_' + parentid[1]).inputmask("99:99", { placeholder: "0" })
                                }

                                if ($('#txtDownArrRoute' + j + '_' + parentid[1]).attr("placeholder", "_")) 
                                    {
                                    $('#txtDownArrRoute' + j + '_' + parentid[1]).inputmask("99:99", { placeholder: "0" })
                                }
                       
                                if ($('#txtUPDepRoute' + j + '_' + parentid[1]).attr("placeholder", "_")) {
                                    $('#txtUPDepRoute' + j + '_' + parentid[1]).inputmask("99:99", { placeholder: "0" })

                                };
                                
                                if ($('#txtDownDepRoute' + j + '_' + parentid[1]).attr("placeholder", "_")) {

                                    $('#txtDownDepRoute' + j + '_' + parentid[1]).inputmask("99:99", { placeholder: "0" });
                                }
                               

                            });
                        }


                    }
                    else
                    {
                        $('div[id*=RouteTimeInfo]').html("");
                        $('div[id*=Panel1]').hide();
                    }

                }

            },
            error: function (response) {
                $('#loadingmessage').modal('hide');
                alert(response.status + ' ' + response.statusText);
            }
        });
    }

    function timeFunction(id)
    {
        var objArray = $('#' + id).val().split(':');
        if (parseInt(objArray[0]) >= parseInt('0') && parseInt(objArray[0]) <= parseInt('23')) {
            if (parseInt(objArray[1]) >= parseInt('0') && parseInt(objArray[1]) <= parseInt('59')) {
               
            }
            else
            {
                $('#' + id).val('');
            }
        }
        else
        {
            $('#' + id).val('');
        }
        

    }
    function SaveApplication() {
        if (fnApplicationValidate())
        {
        
            var PERMIT_APPLICATION_LDRs = new Array();
            var condition = true;
            var NoRoundTrip = $('#txtNoRoundTrip').val();

            for(j= 1; j<=NoRoundTrip; j++)
            {
                var length = $('#tbdRouteInfo_' + j + ' tr:last').attr('id').split("_");
                $('#tbdRouteInfo_' + j + '  tr').each(function () {
                    //debugger;
                    var parentid = $(this).attr('id').split("_");
                    //alert(parentid);
                    if (parentid[1] == 1) {
                        if ($('#txtUPDepRoute1_1').val().trim() == "" ) {
                            condition = false;
                            alert("Please enter Departure Time of UP for " + $('#lblUPRoute1_' + parentid[1]).text());
                            return false;

                        }
                        else if ($('#txtDownDepRoute1_1').val().trim() == "") {
                            condition = false;
                            alert("Please enter Departure Time of Down for " + $('#lblDownRoute1_' + parentid[1]).text());
                            return false;

                        }
                        else {
                            var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                            var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                            var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();
                            var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();
                            PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: UPDepTime.toString(), MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                            PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: "", DEPARTURE_TIMING: DownDepTime.toString(), MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });

                        }

                    }
                    else if (parentid[1] == length[1]) {
                        if ($('#txtUPArrRoute1_' + length[1]).val().trim() == "") {
                            condition = false;
                            alert("Please enter Arrival Time of UP for " + $('#lblUPRoute1_' + parentid[1]).text());
                            return false;

                        }
                        else if ($('#txtDownArrRoute1_' + length[1]).val().trim() == "") {
                            condition = false;
                            alert("Please enter Arrival Time of Down for " + $('#lblDownRoute1_' + parentid[1]).text());
                            return false;

                        }


                        else {
                            var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                            var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                            var UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();
                            var DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();
                            //alert(UPCity);
                            //alert(DownCity);
                            //alert(UPArrTime);
                            //alert(DownArrTime);

                            PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime.toString(), DEPARTURE_TIMING: "", MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                            PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime.toString(), DEPARTURE_TIMING: "", MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });


                        }
                    }
                    else {
                        if (parentid[1] != 1) {
                       

                            var UPCity = $('#lblUPRoute' + j + '_' + parentid[1]).text();
                            var DownCity = $('#lblDownRoute' + j + '_' + parentid[1]).text();
                            var UPArrTime = $('#txtUPArrRoute' + j + '_' + parentid[1]).val().trim();
                            var DownArrTime = $('#txtDownArrRoute' + j + '_' + parentid[1]).val().trim();
                            var UPDepTime = $('#txtUPDepRoute' + j + '_' + parentid[1]).val().trim();
                            var DownDepTime = $('#txtDownDepRoute' + j + '_' + parentid[1]).val().trim();
                            //var UPArrTime = $('#txtUPArrRoute_' + parentid[1]).val().trim();
                            //var DownArrTime = $('#txtDownArrRoute_' + parentid[1]).val().trim();
                            //var UPDepTime = $('#txtUPDepRoute_' + parentid[1]).val().trim();
                            //var DownDepTime = $('#txtDownDepRoute_' + parentid[1]).val().trim();
                            //alert(UPCity);
                            //alert(DownCity);
                            //alert(UPArrTime);
                            //alert(DownArrTime);
                            //alert(UPDepTime);
                            //alert(DownDepTime);

                            PERMIT_APPLICATION_LDRs.push({ VIA_CITY: UPCity, ARRIVAL_TIMING: UPArrTime.toString(), DEPARTURE_TIMING: UPDepTime.toString(), MODE: "UP", ROUTE_DETAIL_ID: j, ID: parentid[1] });
                            PERMIT_APPLICATION_LDRs.push({ VIA_CITY: DownCity, ARRIVAL_TIMING: DownArrTime.toString(), DEPARTURE_TIMING: DownDepTime.toString(), MODE: "DOWN", ROUTE_DETAIL_ID: j, ID: parentid[1] });




                        }
                    }

                });

            }
       

            if (condition == false)
            {
                return false;

            }
            else
            {
                //var RouteHdr = $("input[name='routeHdr']:checked").val();

                var xxx = JSON.stringify({ ROUTE_DETAIL_ID: 0, PERMIT_APPLICATION_LDRs: PERMIT_APPLICATION_LDRs });

                $('input[id*=txtroutjson]').val(xxx);
                $('#loadingmessage').modal('show');
                return true;
            }


        }
        else
        {

            return false;
        }
    }
   
    function fnClearPermitNew()
    {
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
        $('input[id*=VSeatCapacity]').val('');
        $('input[id*=VManufacturingON]').val('');
        $('input[id*=txtMobileNo]').val('');
        $('input[id*=txtEmailId]').val('');
        $('input[id*=txtICValidUpto]').val('');
        $('input[id*=txtFCValidUpto]').val('');
        $('input[id*=txtPCValidUpto]').val('');
        $('input:radio[name*="gender"]').prop('checked', true);
        $("input[id*=txtMale]").parent('span').addClass('checked');
        $("input[id*=txtFemale]").parent('span').removeClass('checked');
        
        $("select[name*='ddlQualification']").val('0').change();
        $("select[name*='ddlCategory']").val('0').change();

        $('input:radio[name*="ExArmy"]').prop('checked', true);
        $("input[id*=txtExArmyY]").parent('span').addClass('checked');
        $("input[id*=txtExArmyN]").parent('span').removeClass('checked');

        $('input:radio[name*="IdentityProof"]').prop('checked', true);
        $("input[id*=txtIdentityProofV]").parent('span').addClass('checked');
        $("input[id*=txtIdentityProofP]").parent('span').removeClass('checked');
        $("input[id*=txtIdentityProofA]").parent('span').removeClass('checked');

        $('input:radio[name*="Minority"]').prop('checked', true);
        $("input[id*=txtMinorityY]").parent('span').removeClass('checked');
        $("input[id*=txtMinorityN]").parent('span').addClass('checked');

        $('input:radio[name*="Handicapped"]').prop('checked', true);
        $("input[id*=txtHandicappedY]").parent('span').removeClass('checked');
        $("input[id*=txtHandicappedN]").parent('span').addClass('checked');

        $('input:radio[name*="HML"]').prop('checked', true);
        $("input[id*=txtHMLY]").parent('span').removeClass('checked');
        $("input[id*=txtHMLN]").parent('span').addClass('checked');

        $('input:radio[name*="AC"]').prop('checked', true);
        $("input[id*=txtACY]").parent('span').removeClass('checked');
        $("input[id*=txtACN]").parent('span').addClass('checked');

        $('input:radio[name*="Deluxe"]').prop('checked', true);
        $("input[id*=txtDeluxeY]").parent('span').removeClass('checked');
        $("input[id*=txtDeluxeN]").parent('span').addClass('checked');

        $('input:radio[name*="Sleeper"]').prop('checked', true);
        $("input[id*=txtSleeperY]").parent('span').removeClass('checked');
        $("input[id*=txtSleeperN]").parent('span').addClass('checked');
    
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
        var VORegNumber = $('input[id*=VORegNumber]').val().trim();
        if (VORegNumber == "" || VORegNumber == null) {
            alert("Please enter Vehicle Registration Number");
            $('input[id*=VORegNumber]').css('border-color', 'red').focus();
            return false;
        }
        var permitNo = $('input[id*=txtpermitNo]').val().trim();
        if (permitNo == "" || permitNo == null) {
            alert("Please enter valid permit number");
            $('input[id*=txtpermitNo]').css('border-color', 'red').focus();
            return false;
        }
        var permitFrom = $('input[id*=txtpermitFrom]').val().trim();
        if (permitFrom == "" || permitFrom == null) {
            alert("Please select permit valid from date");
            $('input[id*=txtpermitFrom]').css('border-color', 'red').focus();
            return false;
        }
        var permitTo = $('input[id*=txtpermitTo]').val().trim();
        if (permitTo == "" || permitTo == null) {
            alert("Please select permit valid to date");
            $('input[id*=txtpermitTo]').css('border-color', 'red').focus();
            return false;
        }
        


        var VType = $('input[id*=VType]').val().trim();
        if (VType == "" || VType == null) {
            alert("Please enter Vehicle class description");
            $('input[id*=VType]').css('border-color', 'red').focus();
            return false;
        }
        if (VType.toUpperCase().indexOf("BUS")===-1)
        {
            alert("Vehicle class Should be BUS");
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
        if ($('input[id*=VMakerDesc]').val().trim().length !="") {
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
        if ($('input[id*=VFuelDesc]').val().trim().length != "") {

            if ($('input[id*=VFuelDesc]').val().trim().length < 3) {
                alert("Enter atleast 3 chars in the fuel description textbox");
                $('input[id*=VFuelDesc]').css('border-color', 'red').focus();
                return false;
            }
        }
        if ($('input[id*=VColor]').val().trim().length != "") {


            if ($('input[id*=VColor]').val().trim().length < 3) {
                alert("Enter atleast 3 chars in the color textbox");
                $('input[id*=VColor]').css('border-color', 'red').focus();
                return false;
            }
        }
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
        if (VULW <= parseInt(0)) {
            alert("Please enter valid data  in the U. L. W. as per RC textbox");
            $('input[id*=VULW]').css('border-color', 'red').focus();
      
            return false;
        }
        if (VULW.length < 2) {
            alert("Enter atleast 2 chars in the U. L. W. as per RC textbox");
            $('input[id*=VULW]').css('border-color', 'red').focus();
            return false;
        }

        var VRLW = $('input[id*=VRLW]').val().trim();
        if (VRLW == "" || VRLW == null) {
            alert("Please enter R. L. W. as per RC");
            $('input[id*=VRLW]').css('border-color', 'red').focus();
            return false;
        }
        if (VRLW <= parseInt(0)) {
            alert("Please enter valid data  in the R. L. W. as per RC textbox");
            $('input[id*=VRLW]').css('border-color', 'red').focus();

            return false;
        }
        if (VRLW.length < 2) {
            alert("Enter atleast 2 chars in the R. L. W. as per RC textbox");
            $('input[id*=VRLW]').css('border-color', 'red').focus();
            return false;
        }
        var VSeatCapacity = $('input[id*=VSeatCapacity]').val().trim();
        if (VSeatCapacity == "" || VSeatCapacity == null) {
            alert("Please enter seat capacity");
            $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
            return false;
        }
        if (VSeatCapacity < parseInt(32)) {
            alert("Minimum 32 Seat capacity required");
            $('input[id*=VSeatCapacity]').css('border-color', 'red').focus();
            return false;
        }
        var VManufacturingON = $('input[id*=VManufacturingON]').val().trim();
        if (VManufacturingON == "" || VManufacturingON == null) {
            alert("Please enter Manufacturing date");
            $('input[id*=VManufacturingON]').css('border-color', 'red').focus();
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
        if (EmailID != "" ) {

            if (!IsEmail(EmailID)) {
                alert("Invalid Email ID");
                $('input[id*=txtEmailId]').css('border-color', 'red').focus();
                return false;
            }
        }
        
        
        if ($('input[id*=txtICValidUpto]').val() == null || $('input[id*=txtICValidUpto]').val() == "") {
            alert("Please select Insurance Certificate Validity Date");
            return false;
        }
        
        if ($('input[id*=txtPCValidUpto]').val() == null || $('input[id*=txtPCValidUpto]').val() == "") {
            alert("Please select Pollution Certificate Validity Date");
            return false;
        }
        

        
        
        return true;
    }



