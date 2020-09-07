
$('.cc-number').payment('formatCardNumber');
$('.cc-exp').payment('formatCardExpiry');
$('.cc-cvc').payment('formatCardCVC');

$.fn.toggleInputError = function (erred) {
    this.parent('.form-group').toggleClass('has-error', erred);
    return false;
};

$("#btnUpgradePlan").click(function (e)
{
    e.preventDefault();
    e.stopImmediatePropagation();

    js.ajaxPost(upgradeUrl, { planId: $("#PlanId").val(), planName: $("#PlanName").val() }).then(
         function (data) {
             if (data.flag == 1) {
                 window.location.href = confirmationUrl + "/" + data.plan;
             }
         }).catch(function (error) {
             console.log(error);
         });
});

$('#form').submit(function (e) {

    e.preventDefault();
    e.stopImmediatePropagation();   	

	if (!suftnet_Payment.isCardValid()) {
		return false;
	}
    	
    var $form = $(this);

    $form.find('button').prop('disabled', true);

    expiration = $('.cc-exp').payment('cardExpiryVal');
    Stripe.card.createToken({
        number: $('.cc-number').val(),
        cvc: $('.cc-cvc').val(),
        exp_month: (expiration.month || 0),
        exp_year: (expiration.year || 0)
    }, stripeResponseHandler);

});

stripeResponseHandler = function (status, response)
{   
    var $form = $("#form");

	if (response.error) {

        $form.find('.payment-errors').text(response.error.message);
        $form.find('.payment-errors').closest('.row').show();
        $form.find('.payment-errors').toggle($('.payment-errors').text.length > 0);

        $form.find('button').prop('disabled', false);

	} else {

        $form.find('.payment-errors').closest('.row').hide();
        $form.find('.payment-errors').text("");

		// token contains id, last4, and card type
		var token = response.id;
	    // Insert the token into the form so it gets submitted to the server		

		$form.append($('<input type="hidden" name="stripeToken" />').val(token));
	    // and re-submit      	

		var __requestVerificationToken = $('input[name="__RequestVerificationToken"]').val();		

		js.ajaxPost($("#form").attr("action"), $("#form").serialize(), null, __requestVerificationToken).then(
		 function (data) {

		     if (data.flag == 1)
		     {
		         window.location.href = confirmationUrl + "/" + data.plan;
		     }
		 });
	}
};

var suftnet_Payment = {

	isCardValid : function()
	{   		
   		var isValid = false;

   		if (!$.payment.validateCardNumber($('.cc-number').val())) {
   			$('.cc-number').toggleInputError(!$.payment.validateCardNumber($('.cc-number').val()));
   		} else {

   			$('.cc-number').toggleInputError(!$.payment.validateCardNumber($('.cc-number').val()));
   			isValid = true;
   		}

   		if (!$.payment.validateCardExpiry($('.cc-exp').payment('cardExpiryVal'))) {
   			$('.cc-exp').toggleInputError(!$.payment.validateCardExpiry($('.cc-exp').payment('cardExpiryVal')));
   		} else {
   			$('.cc-exp').toggleInputError(!$.payment.validateCardExpiry($('.cc-exp').payment('cardExpiryVal')));
   			isValid = true;
   		}

   		if (!$.payment.validateCardCVC($('.cc-cvc').val(), cardType)) {
   			var cardType = $.payment.cardType($('.cc-number').val());
   			$('.cc-cvc').toggleInputError(!$.payment.validateCardCVC($('.cc-cvc').val(), cardType));
   		} else {
   			var cardType = $.payment.cardType($('.cc-number').val());
   			$('.cc-cvc').toggleInputError(!$.payment.validateCardCVC($('.cc-cvc').val(), cardType));
   			isValid = true;
   		}

   		return isValid;
	},

	isEditCardValid : function()
	{
 		var isValid = false;

 		if (!$.payment.validateCardExpiry($('.cc-exp').payment('cardExpiryVal'))) {
 			$('.cc-exp').toggleInputError(!$.payment.validateCardExpiry($('.cc-exp').payment('cardExpiryVal')));
 		} else {
 			$('.cc-exp').toggleInputError(!$.payment.validateCardExpiry($('.cc-exp').payment('cardExpiryVal')));
 			isValid = true;
 		}

 		return isValid;
	}
}