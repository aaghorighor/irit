
var schedule = {

    create: function () {

        $("#StartTime").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "time.png",
            buttonText: "Open datepicker",
            buttonImageOnly: true
        });

        $("#EndTime").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "time.png",
            buttonText: "Open datepicker",
            buttonImageOnly: true
        });

        $("#btnSubmit").bind("click", function (e) {

            e.preventDefault();

            if (!suftnet_validation.isValid("schedule-form")) {
                return false;
            }

            if ($('#Active').is(':checked')) {
                $("#Active").val(true);
            } else {
                $("#Active").val(false);
            }

            js.ajaxPost($("#schedule-form").attr("action"), $("#schedule-form").serialize()).then(
           function (data) {

               switch (data.flag) {
                   case 1: //// add                                          

                       suftnet_grid.addSchedule(data.objrow);
                       break;
                   case 2: //// update  

                       suftnet_grid.updateSchedule(data.objrow);

                       suftnet.Tab(1);

                       break;
                   default:;
               }

               $("#Active").attr('checked', false)

               $("#Id").val(0);

               iuHelper.resetForm("#schedule-form");

           });

        });
        suftnet_Settings.ClearErrorMessages("#schedule-form");

    },

    load : function()
    {
        $(document).on("click", "#view", function () {
            window.location.href = $("#notificationUrl").attr("data-notificationUrl") + "/" + $(this).attr("scheduleId");
        });

        $(document).on("click", "#Delete", function (e) {
            e.preventDefault();

            js.confirm($("#deleteUrl").attr("data-deleteUrl"), { Id: $(this).attr("DeleteId") }, $(this).attr("DeleteId"), "#tdSchedule").then(
            function (data) {

            });
        });

        $(document).on("click", "#EditLink", function (e) {

            e.preventDefault();

            js.ajaxGet($("#getUrl").attr("data-getUrl"), { Id: $(this).attr("EditId") }).then(
              function (data) {
                  var dataobject = data.dataobject;

                  $("#Title").val(dataobject.Title);
                  $("#Description").val(dataobject.Description);
                  $("#Id").val(dataobject.Id);
                  $("#FrequencyId").val(dataobject.FrequencyId);
                  $("#PeriodId").val(dataobject.PeriodId);
                  $("#StartTime").val(dataobject.StartTime);
                  $("#EndTime").val(dataobject.EndTime);
                  $("#GroupId").val(dataobject.GroupId);
                  $("#VenueId").val(dataobject.VenueId);
                  $("#Active").attr("checked", dataobject.Active);

                  suftnet.Tab(0);
              });
        });

        suftnet_Settings.TableInit('#tdSchedule', [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]);
    }
}