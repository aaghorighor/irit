
var subTopic = {

    create: function ()
    {        
        $("#btnSubmit").bind("click", function (event) {

            event.preventDefault();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }                      

            $("#Description").val(tinymce.activeEditor.getContent());

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1:                                        

                            suftnet_grid.addSubTopic(data.objrow);
                            break;
                        case 2: 

                            suftnet_grid.updateSubTopic(data.objrow);

                            $("#MainCollapsible").accordion({ collapsible: true });
                            $("#MainCollapsible").accordion("activate", 1);

                            break;
                        default: ;
                    }

                });
           
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

            js.confirm($('#deleteUrl').attr('data-deleteUrl'), { Id: $(this).attr("deleteid") }, $(this).attr("deleteid"), "#tdSubTopic").then(
                function (data) {

                });
        });
                
        $(document).on("click", "#Edit", function (e) {

            e.preventDefault();

            js.ajaxGet($('#editUrl').attr('data-editUrl'), { Id: $(this).attr("editid") }).then(
                function (data) {
                    var dataobject = data.dataobject;
                                       
                    $("#Id").val(dataobject.Id);
                         
                    tinymce.get('Description').setContent(dataobject.Description);
                                                     
                    $("#ImageUrl").val(dataobject.ImageUrl);
                    $("#IndexNo").val(dataobject.IndexNo);
                    $("#Title").val(dataobject.Title);
                                    
                    $("#MainCollapsible").accordion({ collapsible: true });
                    $("#MainCollapsible").accordion("activate", 0);                    
                });

        });
   
        suftnet_Settings.TableInit('#tdSubTopic', [0, 1, 2, 3, 4,5]);
    }    
   
}