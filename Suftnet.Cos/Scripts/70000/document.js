
var _document = {

    create : function()
    {
        $("#btnSubmit").on("click", function (event) {

            event.preventDefault();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Active').is(':checked')) {
                $("#Active").val(true);
            } else {
                $("#Active").val(false);
            }

            $("#Contents").val(tinymce.activeEditor.getContent());

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
             function (data) {
                 switch (data.flag) {
                     case 1: //// add

                         suftnet_grid.addEditor(data.objrow);
                         break;
                     case 2: //// update

                         suftnet_grid.updateEditor(data.objrow);

                         suftnet.Tab(1);

                         break;
                     default:;
                 }

                 $("#Active").attr('checked', false)

                 $("#Id").val(0);

                 iuHelper.resetForm("#form");
             });
        });

        suftnet_upload.init($("#uploadUrl").attr("data-uploadUrl"));

        suftnet_Settings.ClearErrorMessages("#form");
    },

    load : function()
    {
        $(document).on("click", "#DownloadFileLink", function () {

            window.location.href = $("#downloadUrl").attr("data-downloadUrl") + "/" + $(this).attr("documentId")
        });

        $(document).on("click", "#Delete", function (e) {
            e.preventDefault();

            js.confirm($("#deleteUrl").attr("data-deleteUrl"), { Id: $(this).attr("DeleteId") }, $(this).attr("DeleteId"), "#tdeditor").then(
            function (data) {

            });
        });

        $(document).on("click", "#EditLink", function (e) {
            e.preventDefault();

            js.ajaxGet($("#getUrl").attr("data-getUrl"), { Id: $(this).attr("EditId") }).then(
              function (data) {
                  var dataobject = data.dataobject;

                  $("#Title").val(dataobject.Title);
                  $('#Id').val(dataobject.ID);

                  tinymce.get('Contents').setContent(dataobject.Contents);

                  $("#ContentTypeId").val(dataobject.ContentTypeId);
                  $("#CreatedDT").val(dataobject.CreatedOn);
                  $("#Active").attr("checked", dataobject.Active);
                  $("#ImageUrl").val(dataobject.ImageUrl);

                  suftnet.Tab(0);

              });
        });

        suftnet_Settings.TableInit('#tdeditor', [0, 5]);
    }
}