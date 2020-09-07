var checkout = {

    init: function () {
       
        checkout.pageEvent();      
    },     

    pageEvent : function()
    {
        $("#btnSubmit").on("click", function (e)
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

                        if (data.isValid != undefined) {

                            $("#validationError .modal-dialog .modal-content .modal-body #errorList").html("");

                            var sel = $('<ul>').appendTo('#validationError .modal-dialog .modal-content .modal-body #errorList');
                            $.each(data.errors, function (i, value) {
                                sel.append($("<li class=\"list-group-itemlist-group-item-danger\">" + value.Error + "</li>"));

                                $("<span for='" + value.PropertyName + "' class='error'></span>")
                                    .html(value.Error).appendTo($("input#" + value.PropertyName).parent());
                            });
                        }

                        $("#bottom-wizard").find("button").removeAttr("disabled");
                        $("#bottom-wizard").find("button:eq(2)").attr("disabled", false);
                        $("#bottom-wizard").find("button:eq(1)").attr("disabled", "");
                        $("#bottom-wizard").find("button:eq(0)").attr("disabled", false);

                        $('#validationError').modal('show');                           
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

        $(document).on("blur", "#ConfirmPassword", function (e) {
            e.preventDefault();

            if ($("#Password").val() != $("#ConfirmPassword").val()) {

                $("<span for= ConfirmPassword class='error'></span>")
                    .html("Passsword not match Confirm Password").appendTo($("input#ConfirmPassword").parent());     
                $(".forward").attr("disabled", true);

            } else {

                $("<span for=ConfirmPassword class=''></span>")
                    .html("").appendTo($("input#ConfirmPassword").parent());     
                $(".forward").attr("disabled", false);
            }
        });                  

        $(document).on("mouseenter", ".forward", function () {

            if ($("#Password").val() != $("#ConfirmPassword").val()) {
                               
                $("<span for= ConfirmPassword class='error'></span>")
                    .html("Passsword not match Confirm Password").appendTo($("input#ConfirmPassword").parent());

                $(".forward").attr("disabled", true);
            } else {

                $("<span for=ConfirmPassword class=''></span>")
                    .html("").appendTo($("input#ConfirmPassword").parent());
                $(".forward").attr("disabled", false);
            }
        });

        $(document).on("mouseenter", "#ConfirmPassword", function () {
                        
            $("<span for=ConfirmPassword class=''></span>")
                .html("").appendTo($("input#ConfirmPassword").parent());
            $(".forward").attr("disabled", false);

        });              
           
    }

}

