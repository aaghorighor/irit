
var permission = {

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

                            _dataTables.permission.ajax.reload();
                            break;
                        case 2: //// update  

                            _dataTables.permission.ajax.reload();
                            break;

                        default: ;
                    }

                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));
                    iuHelper.resetForm("#form");
                });
        });
    },
    edit: function (obj) {

        var dataobject = _dataTables.permission.row($(obj).parents('tr')).data();

        $("#Id").val(dataobject.Id);
        $("#ViewId").val(dataobject.ViewId);
        $("#Create").val(dataobject.Create);
        $("#Edit").val(dataobject.Edit);
        $("#Remove").val(dataobject.Remove);
        $("#Get").val(dataobject.Get);
        $("#GetAll").val(dataobject.GetAll);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

    },
    pageInit: function () {
        permission.create();
        permission.load();
    },
    delete: function (obj) {

        var dataobject = _dataTables.permission.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.Id }, dataobject.Id, "#tblPermission").then(
                function (data) {
                    _dataTables.permission.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    load: function () {
        _dataTables.permission = $('#tblPermission').DataTable({
            "serverSide": false,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 5,
            "pagingType": "full_numbers",
            "ajax": {
                "url": $("#loadUrl").attr("data-loadUrl"),
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "CreatedOn", "defaultContent": "<i>-</i>" },
                { "data": "View", "defaultContent": "<i>-</i>" },
                {
                    "data": "Create", render: function (data, type, row) {
                        return data == view.enable ? "Yes" : "No";
                    }, "defaultContent": "<i>-</i>"
                },
                {
                    "data": "Edit", render: function (data, type, row) {
                        return data == view.enable ? "Yes" : "No";
                    }, "defaultContent": "<i>-</i>"
                },
                {
                    "data": "Remove", render: function (data, type, row) {
                        return data == view.enable ? "Yes" : "No";
                    }, "defaultContent": "<i>-</i>"
                },
                {
                    "data": "Get", render: function (data, type, row) {
                        return data == view.enable ? "Yes" : "No";
                    }, "defaultContent": "<i>-</i>"
                },
                {
                    "data": "GetAll", render: function (data, type, row) {
                        return data == view.enable ? "Yes" : "No";
                    }, "defaultContent": "<i>-</i>"
                },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a title="Edit this row" style="margin:10px;" href="#" onclick=permission.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a title="Delete this row" style="margin:10px;" href="#" onclick="permission.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4, 5, 6] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5, 6] }],
            destroy: true
        });

        _dataTables.permission.on("draw", function () {
            $('a').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}