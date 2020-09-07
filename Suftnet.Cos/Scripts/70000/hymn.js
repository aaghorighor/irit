var hymnTable;

var hymn = {

    create : function()
    {       
        $(document).on("click", "#SaveChanges", function (e) {

            e.preventDefault();   
            
            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Publish').is(':checked')) {
                $("#Publish").val(true);
            } else {
                $("#Publish").val(false);
            }

            var lyrics = tinymce.activeEditor.getContent();

            $("#Lyrics").val(lyrics);
          
            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add                                          

                            hymnTable.DataTable().fnDraw();

                            break;
                        case 2: //// update 

                            hymnTable.fnUpdate(data.objrow.Number, parseInt($("#rowindex").val()), 1, false, false);
                            hymnTable.fnUpdate(data.objrow.Title, parseInt($("#rowindex").val()), 2, false, false);
                            hymnTable.fnUpdate(data.objrow.Publish == true ? "Yes" : "No", parseInt($("#rowindex").val()), 3, false, false);                                            

                            break;
                        default: ;
                    }
                 
                    $("#Publish").attr('checked', false);
                    $("#Id").val(0);

                    iuHelper.resetForm("#form");                   
                });
        });
    },
   
    load : function()
    {
        $(document).on("click", "#delete", function (e) {

            e.preventDefault();

            var tr = $(this).closest('tr');
            var rowIndex = tr.index();

            $("#rowindex").val(rowIndex);

            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: $(this).attr("deleteId") }, $(this).attr("deleteId"), "#tdHymn").then(
                function (data) {
                    hymnTable.fnDeleteRow($("#rowindex").val(), null, true);
                });
        });

        $(document).on("click", "#edit", function (e) {
            e.preventDefault();

            var tr = $(this).closest('tr');
            var rowIndex = tr.index();

            $("#rowindex").val(rowIndex);

            js.ajaxGet($("#getUrl").attr("data-getUrl"), { Id: $(this).attr("editId") }).then(
                function (data) {

                    var dataobject = data.dataobject;

                    tinymce.get('Lyrics').setContent(dataobject.Lyrics);

                    $("#Title").val(dataobject.Title);
                    $("#Number").val(dataobject.Number);                   
                    $('#Id').val(dataobject.Id);                  
                    $("#Publish").attr("checked", dataobject.Publish);                

                });
        });

        suftnet_dynamicTable.initHymn($("#viewUrl").attr("data-viewUrl"));
    }    
}