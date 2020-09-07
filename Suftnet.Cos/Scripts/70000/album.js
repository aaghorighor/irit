
var album = {

    create: function () {
        $(document).on("click", "#btnSubmit", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

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

                         _dataTables.album.draw();
                         break;
                     case 2: //// update  

                         _dataTables.album.draw();
                         break;

                     default:;
                 }

                 $("#Id").val(0);
                 $("#Publish").attr("checked", false);
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl")); 
                 iuHelper.resetForm("#form");

             });
        });

        suftnet_Settings.ClearErrorMessages("#form");     
    },

    view: function (obj) {

        var dataobject = _dataTables.album.row($(obj).parents('tr')).data();
        window.location.href = $("#viewUrl").attr("data-viewUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.Title) + "/" + dataobject.QueryId;
    },
    edit: function (obj) {

        var dataobject = _dataTables.album.row($(obj).parents('tr')).data();
               
        $("#Description").val(dataobject.Description);
        $("#QueryString").val(dataobject.QueryId);
        $("#Title").val(dataobject.Title);
        $("#ImageUrl").val(dataobject.ImageUrl);
        $("#Publish").attr("checked", dataobject.Publish);  

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
    },
    delete: function (obj) {

        var dataobject = _dataTables.album.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblAlbum").then(
                function (data) {
                    _dataTables.album.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    pageInit: function () {

        suftnet_upload.init($("#uploadUrl").attr("data-uploadUrl"));              
        album.create();
        album.load();
    },
    load: function () {
        _dataTables.album = $('#tblAlbum').DataTable({
            "serverSide": true,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 5,
            "pagingType": "full_numbers",
            "ajax": {
                url: $("#loadUrl").attr("data-loadUrl"),
                type: 'POST'
            },
            "columns": [
                { "data": "CreatedOn", "defaultContent": "<i>-</i>" },
                { "data": "Title", "defaultContent": "<i>-</i>" },              
                {
                    "data": "Publish", render: function (data, type, row) {
                        return data == true ? "Yes" : "No";

                    }, "defaultContent": "<i>-</i>"
                },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=album.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="album.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="album.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"View this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2] },
                { className: "text-left", "targets": [0, 1, 2] }],
            destroy: true
        });
    }
}