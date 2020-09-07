var notification = {
       
    load : function()
    {
        $(document).on("click", "#Delete", function (e) {
            e.preventDefault();

            js.confirm($("#deleteUrl").attr("data-deleteUrl"), { Id: $(this).attr("DeleteId") }, $(this).attr("DeleteId"), "#tdNotification").then(
            function (data) {

            });
        });

        $(document).on("click", "#EditLink", function (e) {
            e.preventDefault();

            js.ajaxGet($("#getUrl").attr("data-getUrl"), { Id: $(this).attr("EditId") }).then(
              function (data) {
                  var dataobject = data.dataobject;

                  var tinymce_editor_id = 'Body';
                                   
                  $("#StatusId").val(dataobject.StatusId);              
                  $("#MessageTypeId").val(dataobject.MessageTypeId);
                  $('#Id').val(dataobject.Id);

                  tinymce.get(tinymce_editor_id).setContent(dataobject.Body);

                  $("#MessageTypeId").trigger("change");

                  suftnet.Tab(0);
              });
        });

        suftnet_Settings.TableInit('#tdNotification', [0, 1, 2, 3, 4, 5, 6]);
    },

    create : function()
    {      
        $("#btnSubmit").click(function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Active').is(':checked')) {
                $("#Active").val(true);
            } else {
                $("#Active").val(false);
            }

            $("#Body").val(tinymce.activeEditor.getContent());

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
             function (data) {
                 switch (data.flag) {
                     case 1: //// add
                                                  
                         suftnet_grid.addNotification(data.objrow);

                         break;
                     case 2: //// update

                         suftnet_grid.updateNotification(data.objrow);

                         suftnet.Tab(1);

                         break;
                     default:;
                 }

                 $("#Id").val(0);                
            
                 iuHelper.resetForm("#form");
             });

        });

        suftnet_Settings.ClearErrorMessages("#form");       
    }  

}