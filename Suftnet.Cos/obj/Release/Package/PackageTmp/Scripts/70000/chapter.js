
var chapter = {

    create: function ()
    {
        chapter.change();

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

                            suftnet_grid.addChapter(data.objrow);
                            break;
                        case 2: 

                            suftnet_grid.updateChapter(data.objrow);

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
        suftnet_upload.init($('#uploadUrl').attr('data-uploadUrl'));
    },

    load: function () {
        $(document).on("click", "#Delete", function (e) {
            e.preventDefault();

            js.confirm($('#deleteUrl').attr('data-deleteUrl'), { Id: $(this).attr("deleteid") }, $(this).attr("deleteid"), "#tdChapter").then(
                function (data) {

                });
        });

        $(document).on("click", "#View", function () {
            window.location.href = $("#viewUrl").attr("data-viewUrl") + "/" + $(this).attr("topic") + "/" + $(this).attr("viewId");
        });

        $(document).on("click", "#Edit", function (e) {

            e.preventDefault();

            js.ajaxGet($('#editUrl').attr('data-editUrl'), { Id: $(this).attr("editid") }).then(
                function (data) {
                    var dataobject = data.dataobject;
                                       
                    $("#Id").val(dataobject.Id);                         
                
                    $("#TopicId").val(dataobject.TopicId);
                    $("#SectionId").val(dataobject.SectionId);
                    $("#ImageUrl").val(dataobject.ImageUrl);
                    $("#Publish").attr("checked", dataobject.Publish);

                    $("#MainCollapsible").accordion({ collapsible: true });
                    $("#MainCollapsible").accordion("activate", 0);

                    chapter.edit(dataobject.SectionId, dataobject.SubSectionId);
                });

        });
   
        suftnet_Settings.TableInit('#tdChapter', [0, 1, 2, 3, 4,5,6]);
    },
    change: function () {

        $(document).on("change", "#SectionId", function (e) {
            e.preventDefault();
           
            js.ajaxGet($("#getUrl").attr("data-getUrl"), { Id: $(this).val() }).then(
                function (data) {     
                    chapter.dropdown(data.dataobject, "#SubSectionId", 0);
                });
        });
    },
    edit: function (sectionId, subSectionId) {
               
        js.ajaxGet($("#getUrl").attr("data-getUrl"), { Id: sectionId }).then(
            function (data) {               
                chapter.dropdown(data.dataobject, "#SubSectionId", subSectionId);
            });
    },
    dropdown: function (data, element, Id)
    {
        $(element).empty();
        $(element).append('<option value="">-- SELECT --</option>');

        $(data).each(function () {
            var $option = $("<option />");
            $option.attr("value", this.Id).text(this.Title);
            $(element).append($option);
        });

        if (Id != null) {
            $(element).val(Id);
        }
    }
}