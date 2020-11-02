var mobilePermission = {

    create: function () {
        $("#btnSaveChanges").on("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {

                        case 1: //// add                                          

                            _dataTables.mobilePermission.ajax.reload();

                            break;
                        case 2: //// update  

                            _dataTables.mobilePermission.ajax.reload();
                            break;

                        default: ;
                    }

                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));
                    iuHelper.resetForm("#form");
                });
        });
    },

    edit: function (obj) {

        var dataobject = _dataTables.mobilePermission.row($(obj).parents('tr')).data();

        $("#Id").val(dataobject.Id);
        $("#PermissionId").val(dataobject.PermissionId);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

    },
    pageInit: function () {
        mobilePermission.create();
        mobilePermission.load();
    },
    delete: function (obj) {

        var dataobject = _dataTables.mobilePermission.row($(obj).parents('tr')).data();

        js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.Id }, dataobject.Id, "#tblMobilePermission").then(
            function (data) {
                _dataTables.mobilePermission.row($(obj).parents('tr')).remove().draw();
            });

    },
    load: function () {
        _dataTables.mobilePermission = $('#tblMobilePermission').DataTable({
            "serverSide": false,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 10,
            "pagingType": "full_numbers",
            "ajax": {
                "url": $("#loadUrl").attr("data-loadUrl"),
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "CreatedOn", "defaultContent": "<i>-</i>" },
                { "data": "CreatedBy", "defaultContent": "<i>-</i>" },
                { "data": "Description", "defaultContent": "<i>-</i>" },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a title="Delete this row" style="margin:10px;" href="#" onclick="mobilePermission.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2] },
                { className: "text-left", "targets": [0, 1, 2] }],
            destroy: true
        });

        _dataTables.mobilePermission.on("draw", function () {
            $('a').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}