
var topic = {

    create: function () {
        $("#btnSubmit").bind("click", function (event) {

            event.preventDefault();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Publish').is(':checked')) {
                $("#Publish").val(true);
            } else {
                $("#Publish").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1:

                            suftnet_grid.addTopic(data.objrow);
                            break;
                        case 2:

                            suftnet_grid.updateTopic(data.objrow);

                            $("#MainCollapsible").accordion({ collapsible: true });
                            $("#MainCollapsible").accordion("activate", 1);

                            break;
                        default: ;
                    }

                });

            $("#Publish").attr('checked', false);
            $("#Id").val(0);
            $("#ImageUrl").val("");

            iuHelper.resetForm("#form");
        });

        suftnet_Settings.ClearErrorMessages("#form");
    },

    load: function () {

        $(document).on("click", "#clearImage", function (e) {
            e.preventDefault();
            $("#ImageUrl").val("");
        });

        $(document).on("click", "#Delete", function (e) {
            e.preventDefault();

            js.confirm($('#deleteUrl').attr('data-deleteUrl'), { Id: $(this).attr("deleteid") }, $(this).attr("deleteid"), "#tdTopic").then(
                function (data) {

                });
        });

        $(document).on("click", "#Edit", function (e) {

            e.preventDefault();

            js.ajaxGet($('#editUrl').attr('data-editUrl'), { Id: $(this).attr("editid") }).then(
                function (data) {
                    var dataobject = data.dataobject;

                    $("#Id").val(dataobject.Id);

                    $("#Description").val(dataobject.Description);
                    $("#TopicId").val(dataobject.TopicId);
                    $("#ImageUrl").val(dataobject.ImageUrl);
                    $("#IndexNo").val(dataobject.IndexNo);
                    $("#VideoId").val(dataobject.VideoId);
                    $("#VideoUrl").val(dataobject.VideoUrl);

                    $("#Publish").attr("checked", dataobject.Publish);

                    $("#MainCollapsible").accordion({ collapsible: true });
                    $("#MainCollapsible").accordion("activate", 0);
                });

        });

        suftnet_Settings.TableInit('#tdTopic', [0, 1, 2, 3, 4, 5, 6]);
    }

}