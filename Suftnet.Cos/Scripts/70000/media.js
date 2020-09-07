
var media = {

    create: function () {
        $("#btnSubmit").bind("click", function (e) {

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

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add

                            suftnet_grid.addMedia(data.objrow);
                            break;
                        case 2: //// update

                            suftnet_grid.updateMedia(data.objrow);
                            break;
                        default: ;
                    }

                    $("#Id").val(0);
                    $("#Publish").attr("checked", false);

                    iuHelper.resetForm("#form");

                });
        });
        suftnet_Settings.ClearErrorMessages("#form");
    },

    load: function () {
        $(document).on("click", "#Delete", function (e) {
            e.preventDefault();

            js.confirm($("#deleteUrl").attr("data-deleteUrl"), { Id: $(this).attr("DeleteId") }, $(this).attr("DeleteId"), "#tdMedia").then(
                function (data) {

                });
        });

        $(document).on("click", "#EditLink", function (e) {
            e.preventDefault();

            js.ajaxGet($("#deleteUrl").attr("data-deleteUrl"), { Id: $(this).attr("EditId") }).then(
                function (data) {

                    var dataobject = data.dataobject;

                    $("#Title").val(dataobject.Title);
                    $('#Id').val(dataobject.Id);
                    $("#MediaTypeId").val(dataobject.MediaTypeId);
                    $("#FormatTypeId").val(dataobject.FormatTypeId);
                    $("#CreatedDT").val(dataobject.CreatedOn);
                    $("#MedialUrl").val(dataobject.MedialUrl);
                    $("#Description").val(dataobject.Description);
                    $("#ImageUrl").val(dataobject.ImageUrl);
                    $("#Speaker").val(dataobject.Speaker);
                    $("#Publish").attr("checked", dataobject.Publish);

                    $("#MainCollapsible").accordion({ collapsible: true });
                    $("#MainCollapsible").accordion("activate", 0);

                });
        });

        suftnet_Settings.TableInit('#tdMedia', [0, 1, 4, 5, 6]);   
    }
}