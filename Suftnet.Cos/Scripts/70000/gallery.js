
var gallery = {

    create: function () {
        $(document).on("click", "#btnSubmit", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }           

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
             function (data) {
                 switch (data.flag) {

                     case 1: //// add                                          

                         _dataTables.gallery.ajax.reload();
                         break;
                     case 2: //// update  
                   
                         break;

                     default:;
                 }

                 $("#Id").val(0);                               
             });
        });

        suftnet_Settings.ClearErrorMessages("#form");
       
    },
    pageInit: function () {
        suftnet_upload.init($("#uploadUrl").attr("data-uploadUrl"));
        gallery.load();
    },   
    delete: function (obj) {

        var dataobject = _dataTables.gallery.row($(obj).parents('tr')).data();

        if (dataobject !== null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblGallery").then(
                function (data) {
                    _dataTables.gallery.row($(obj).parents('tr')).remove().draw(true);
                });
        }

    },
    load: function () {

        _dataTables.gallery = $('#tblGallery').DataTable({
            "serverSide": false,
            "ordering": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pagingType": "full_numbers",
            "ajax": {
                "url": $("#loadUrl").attr("data-loadUrl"),
                "type": "GET",
                "datatype": "json"
            },
            columns: [
                { "data": "CreatedOn", "autowidth": true },
                { "data": "Album", "autowidth": true },              
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick="gallery.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2] },
                { className: "text-left", "targets": [0, 1,2] }],
            destroy: true
        });
    }    
}