function fnPayCancel() {
	parent.history.back();
}

function getParamValuesByName(querystring) {
    querystring = querystring.replace('&', 'and');
    name = querystring.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
	var regexS = "[\\?&]" + name + "=([^&#]*)";
	var regex = new RegExp(regexS);
	var results = regex.exec(window.location.search);
	
	if (results == null) {
		return "";
	}
	else {
		return decodeURIComponent(results[1].replace(/\+/g, " "));
	}
}

$(function () {
	$("#btnPost").bind("click", function () {
		var $form = $("<form/>").attr("id", "data_form").attr("action", "PaymentResponse.aspx").attr("method", "post");
		$("body").append($form);
		
		//Append the values to be send
		AddParameter($form, "responseparam", $('input[id*=responseparam]').val());
		
		//Send the Form
		$form[0].submit();
	});
});

function AddParameter(form, name, value) {
	var $input = $("<input />").attr("type", "hidden").attr("name", name).attr("value", value);
	form.append($input);
}

jQuery(document).ready(function () {
	$('input[id*=hdnenc_val]').val(getParamValuesByName('q'));
	//alert(getParamValuesByName('q'));
	
	$('#btnPay').on('click', function (event) {
		var APP_ID = $('input[id*=APP_ID').val();
		var APPLICATION_SERVICE = $('input[id*=APPLICATION_SERVICE').val();
		
		$.ajax({
                    type: "POST",
                    url: "api/payInitiate.ashx?q="+encodeURIComponent($('input[id*=hdnenc_val]').val()),
                    data: "",
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "Success") {
                            alert("Please Try Again Later...");
                            window.location.href = "PermitHomepage.aspx";
                            event.preventDefault();
                             return false;
                        }
                        else {
                              return true;
                            }

                        },
                    error: function (data) {
                        alert("Please Try Again Later...");
                        event.preventDefault();
                        return false;
                    }
                });

            });
});