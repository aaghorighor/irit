var event = {
  
    create: function ()
    {
        $("#StartDate").datepicker({
            showOn: "button",
            minDate: 0,
            maxDate: "+60D",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true          
        });

        $("#EndDate").datepicker({
            showOn: "button",
            minDate: 0,
            maxDate: "+60D",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        $("#StartTime").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "time.png",
            buttonText: "Open datepicker",
            buttonImageOnly: true,
            onSelect: function (selected)
            {
                $("#EndTime").val(selected);
            }
        });

        $("#EndTime").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "time.png",
            buttonText: "Open datepicker",
            buttonImageOnly: true,
            onSelect: function (selected) {
                var time = $("#StartTime").val();

                if (time > selected)
                {
                    $("#EndTime").val(time);
                }              
            }
        });       

        $("#IsRegistration").on("change", function (e) {

            if($(this).is(':checked'))
            {
                $("#isDisclaimer").show();
            } else {
                $("#isDisclaimer").hide();
            }
        });

        $("#btnClose").bind("click", function (e) {
            window.location.href = $('#eventUrl').attr('data-eventUrl');
        });

        $("#IsRegistration").on("change", function (e) {

            if ($(this).is(':checked')) {
                $("#isDisclaimer").show();
            } else {
                $("#isDisclaimer").hide();
            }
        });

        $("#btnSubmit").bind("click", function (e)
        {
            e.preventDefault();
            e.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Publish').is(':checked')) {
                $("#Publish").val(true);
            } else {
                $("#Publish").val(false);
            }

            if ($('#IsRegistration').is(':checked')) {
                $("#IsRegistration").val(true);
            } else {
                $("#IsRegistration").val(false);
            }

            if ($('#PushNotification').is(':checked')) {
                $("#PushNotification").val(true);
            } else {
                $("#PushNotification").val(false);
            }

            if ($('#IsSlider').is(':checked')) {
                $("#IsSlider").val(true);
            } else {
                $("#IsSlider").val(false);
            }

            if ($('#IsDisclaimer').is(':checked')) {
                $("#IsDisclaimer").val(true);
            } else {
                $("#IsDisclaimer").val(false);
            }                      

            js.ajaxPost($('#createUrl').attr('data-createUrl'), $("#form").serialize()).then(
              function (data) {       
                  window.location.href = $("#editUrl").attr("data-editUrl") + "/" + data.Id
              });           
        });
                             
        suftnet_Settings.ClearErrorMessages("#form");
        suftnet_upload.init($('#uploadUrl').attr('data-uploadUrl'));
    }
}