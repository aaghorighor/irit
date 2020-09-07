var editEvent = {
  
    pageInit: function ()
    {      
        editEvent.edit();

        js.ajaxGet($('#editUrl').attr('data-editUrl')).then(
            function (data) {
                             
                var dataobject = data.dataobject;

            $("#EventTypeId").val(dataobject.EventTypeId);
            $("#Title").val(dataobject.Title);
            $("#Note").val(dataobject.Note);
            $("#QueryString").val(dataobject.QueryId);
            $("#StatusId").val(dataobject.StatusId);
            $("#VenueId").val(dataobject.VenueId);
            $("#Publish").attr("checked", dataobject.Publish);       
            $("#StartDate").val(dataobject.StartDt);
            $("#EndDate").val(dataobject.EndDt);
            $("#StartTime").val(dataobject.StartTime);
            $("#EndTime").val(dataobject.EndTime);
            $("#ImageUrl").val(dataobject.ImageUrl);           
            $("#IsRegistration").attr("checked", dataobject.IsRegistration);
            $("#IsDisclaimer").attr("checked", dataobject.IsDisclaimer);
            $("#IsSlider").attr("checked", dataobject.IsSlider);

            if ($("#IsRegistration").is(':checked')) {
                $("#isDisclaimer").show();
            } else {
                $("#isDisclaimer").hide();
            }

        });     
    },
    edit: function ()
    { 
        $("#StartDate").datepicker({
            showOn: "button",           
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true            
        });

        $("#EndDate").datepicker({
            showOn: "button",           
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
            onSelect: function (selected) {
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

                if (time > selected) {
                    $("#EndTime").val(time);
                }
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

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
              function (data) {
               
              });           
        });

        $("#btnDelete").bind("click", function (e) {

            if ($(this).attr("data-eventId") != null) {

                var param = {
                    Id: $(this).attr("data-eventId")                  
                };

                $.confirm.show({
                    "message": "Do you want to delete this event?",
                    "type": "danger",
                    "yes": function () {
                                               
                        js.ajaxPost($('#deleteUrl').attr('data-deleteUrl'), param).then(
                        function (data) {

                            window.location.href = $('#eventUrl').attr('data-eventUrl');                          
                        });
                    },
                    "no": function () {
                        
                    }
                });
            }

        });

        suftnet_upload.init($('#uploadUrl').attr('data-uploadUrl'));
        suftnet_Settings.ClearErrorMessages("#form");       
    }

}