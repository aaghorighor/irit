
var smallGroupMapping = {

    create: function () {
        $("#btnSaveChanges").click(function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
            function (data) {
                switch (data.flag) {
                    case 1: //// add
                                                
                        _dataTables.smallGroupMapping.ajax.reload();
                        break;
                    case 2: //// update

                        break;
                    default:;
                }

                $("#Id").val(0);
                iuHelper.resetForm("#form");

            });

        });

        suftnet_Settings.ClearErrorMessages("#form");
    },
   
    delete: function (obj) {

        var dataobject = _dataTables.smallGroupMapping.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblSmallGroupMapping").then(
                function (data) {
                    _dataTables.smallGroupMapping.row($(obj).parents('tr')).remove().draw(true);
                });
        }

    },
    load: function () {

        _dataTables.smallGroupMapping = $('#tblSmallGroupMapping').DataTable({
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
                { "data": "FirstName", "autowidth": true },
                { "data": "LastName", "autowidth": true },
                { "data": "Role", "autowidth": true },
                {
                    "data": null,
                    "orderable": false,
                    "defaultContent": '<a style=margin:10px; href="#" onclick="smallGroupMapping.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3] },
                { className: "text-left", "targets": [0, 1, 2, 3] },
                { className: "text-center", "targets": [4] }],
            destroy: true
        });
    }   
}