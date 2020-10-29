	/*  Wizard */
	jQuery(function ($) {
		"use strict";
		$('form#wrapped').attr('action', '#');
		$("#wizard_container").wizard({
			stepsWrapper: "#wrapped",
			submit: ".submit",
			beforeSelect: function (event, state)
			{	
				if (state.stepIndex === 1) {

					if ($("#Email").hasClass("error")) {
						return false;
					}

					if ($("#Password").val() != $("#ConfirmPassword").val()) {

						$("<span for= ConfirmPassword class='error'></span>")
							.html("Passsword not match Confirm Password").appendTo($("input#ConfirmPassword").parent());
						return false;
					} else {
						$("<span for=ConfirmPassword class=''></span>")
							.html("").appendTo($("input#ConfirmPassword").parent());
					}
				}

				if (!state.isMovingForward)
					return true;
				var inputs = $(this).wizard('state').step.find(':input');
				return !inputs.length || !!inputs.valid();
			}
		}).validate({
			errorPlacement: function (error, element) {
				if (element.is(':radio') || element.is(':checkbox')) {
					error.insertBefore(element.next());
				} else {
					error.insertAfter(element);
				}
			}
		});
		//  progress bar
		$("#progressbar").progressbar();
		$("#wizard_container").wizard({
            afterSelect: function (event, state) {

				$("#progressbar").progressbar("value", state.percentComplete);
				$("#location").text("(" + state.stepsComplete + "/" + state.stepsPossible + ")");
			}
		});
		
	});