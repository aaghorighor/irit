var checkout = {

    init: function () {
            
        $(".cc-number").payment('formatCardNumber');
        $(".cc-exp").payment('formatCardExpiry');
        $(".cc-cvc").payment('formatCardCVC');     

        checkout.pageEvent();      
    },

    isCard: function ()
    {
        var valid = true;          

        if (!$.payment.validateCardNumber($(".cc-number").val())) {        

            $("<span for=cc-number class='error'></span>")
                .html("Invalid Card Number").appendTo($("input.cc-number").parent());
                       
            valid = false;
        } else {
            $("<span for=cc-number class=''></span>")
                .html("").appendTo($("input.cc-number").parent());          
        }

        var cardType = $.payment.cardType($(".cc-number").val());

        if (!$.payment.validateCardCVC($(".cc-cvc").val(), cardType)) {

            $("<span for=cc-cvc class='error'></span>")
                .html("Invalid CVC").appendTo($("input.cc-cvc").parent());

            valid = false;
        } else {

            $("<span for=cc-cvc class=''></span>")
                .html("").appendTo($("input.cc-cvc").parent());        
        }      

        if (!$.payment.validateCardExpiry($(".cc-exp").payment('cardExpiryVal'))) {

            $("<span for=cc-exp class='error'></span>")
                .html("Invalid Expiry Date").appendTo($("input.cc-exp").parent());

            valid = false;
        } else {

            $("<span for=cc-exp class=''></span>")
                .html("").appendTo($("input.cc-exp").parent());
          
        } 
     
        return valid;
    },    

    pageEvent : function()
    {
        $(document).on("click", "#btnSubmit", function (e)
        {
            e.preventDefault();
            e.stopImmediatePropagation();

            var $form = $("#form");
                               
            if (!checkout.isCard()) {
                return false;
            }           

            if (!$form.valid())
            {               
                return false;
            }
          
            $.preloader.start({
                position: 'center',
                modal: true,
                src: $("#spriteUrl").attr("data-spriteUrl")
            });                       
                               
            $form.find('button').prop('disabled', true);        

            var expiration = $(".cc-exp").payment('cardExpiryVal');
            Stripe.card.createToken({
                number: $(".cc-number").val(),
                cvc: $(".cc-cvc").val(),
                exp_month: (expiration.month || 0),
                exp_year: (expiration.year || 0)
            }, handler);

        });

        handler = function (status, response)
        {
            var $form = $("#form");
           
            if (response.error) {
                               
                $("<span for=alert class='error'></span>")
                    .html(response.error.message).appendTo($("span.alert").parent());
                              
                $.preloader.stop();

            } else {             
              
               $form.append($('<input type="hidden" name="StripeToken" />').val(response.id));
               js.post($("#form").attr("action"), $("#form").serialize()).then(
               function (data)
                   {
                       $.preloader.stop();

                       if (data.ok) {
                           window.location.href = $("#confirmationUrl").attr("data-confirmationUrl");
                       }
                       else {        

                           $form.append($('<input type="hidden" name="StripeToken" />').val(""));

                           $("#bottom-wizard").find("button").removeAttr("disabled");
                           $("#bottom-wizard").find("button:eq(2)").attr("disabled", false);
                           $("#bottom-wizard").find("button:eq(1)").attr("disabled", "");
                           $("#bottom-wizard").find("button:eq(0)").attr("disabled", false);

                           if (data.isValid) {

                               $("#validationError .modal-dialog .modal-content .modal-body #errorList").html("");

                               var sel = $('<ul>').appendTo('#validationError .modal-dialog .modal-content .modal-body #errorList');
                               $.each(data.errors, function (i, value) {
                                   sel.append($("<li class=\"list-group-itemlist-group-item-danger\">" + value.Error + "</li>"));

                                   $("<span for='" + value.PropertyName + "' class='error'></span>")
                                       .html(value.Error).appendTo($("input#" + value.PropertyName).parent());
                               });

                               $('#validationError').modal('show');
                               return;
                           }

                           if (data.isApplication) {
                               $('#applicationError').modal('show');
                           }                                               
                       }                
               });
            }           
        };

        $(document).on("blur", "#Email", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            js.ajaxGet($("#checkEmailUrl").attr("data-checkEmailUrl"),
                {
                    email: $(this).val()
                }).then(
                    function (data) {
                        if (data.ok == false) {

                            $("span[for*='Email']").removeClass('error').text("");
                            $("input#Email").removeClass('error').text("");
                            $("input#Email").addClass('valid');
                        }
                        else {

                            $("input#Email").removeClass('valid');
                            $("input#Email").addClass('error');
                            $("<span for=Email class='error'></span>")
                                .html("Email already in used").appendTo($("input#Email").parent());
                        }
                });
           });                   
    }

}

