var checkout = {

    init: function () {
       
        checkout.pageEvent();      
    },     

    pageEvent : function()
    {
        $(document).on("click", "#btnSubmit", function (e)
        {
            e.preventDefault();
            e.stopImmediatePropagation();

            var $form = $("#form");              

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
        });       

        $(document).on("blur", "#Email", function (e)
        {
            e.preventDefault();

            var email = $(this).val();
            var errorString = email.substring(0, email.length - 4);

            js.ajaxGet($("#checkEmailUrl").attr("data-checkEmailUrl"),
                { email: $(this).val() }).then(
                    function (data) {
                        if (data.ok == true) {
                            $("<span for=Email' class='error'></span>")
                                .html(errorString).appendTo($("input#Email").parent());
                        }
                        else {
                            $("<span for=Email' class=''></span>")
                                .html().appendTo($("input#Email").parent());
                        }

                    });
        });

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

