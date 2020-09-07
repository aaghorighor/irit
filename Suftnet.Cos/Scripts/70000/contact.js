
var contact = {

    create: function () {        

        $("#btnSaveChanges").on("click", function (event) {
            event.preventDefault();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Active').is(':checked')) {
                $("#Active").val(true);
            } else {
                $("#Active").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {

                    switch (data.flag) {
                        case 1: //// add

                            _dataTables.contact.ajax.reload();
                            break;
                        case 2: //// update

                            _dataTables.contact.ajax.reload();

                            break;
                        default: ;
                    }

                    $("#Id").val(0);
                    $("#Active").attr("checked", false);
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl")); 
                    iuHelper.resetForm("#form");

                });
        });
        suftnet_Settings.ClearErrorMessages("#form");
    },  
    edit: function (obj) {

        var dataobject = _dataTables.contact.row($(obj).parents('tr')).data();

        $("#RoleId").val(dataobject.RoleId);        
        $("#SequencyId").val(dataobject.SequencyId);
        $("#Active").attr("checked", dataobject.Active);
        $("#QueryString").val(dataobject.QueryId);
        $("#MemberExternalId").val(dataobject.QueryId_2);
        $("#Member").val(dataobject.FullName);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
    },
    delete: function (obj) {

        var dataobject = _dataTables.contact.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblContactus").then(
                function (data) {
                    _dataTables.contact.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    pageInit: function () {
       
        contact.create();
        contact.load();
    },
    load: function () {
        _dataTables.contact = $('#tblContactus').DataTable({
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
                { "data": "SequencyId", "defaultContent": "<i>-</i>" },
                { "data": "FirstName", "defaultContent": "<i>-</i>" },
                { "data": "LastName", "defaultContent": "<i>-</i>" },
                { "data": "Role", "defaultContent": "<i>-</i>" },
                {
                    "data": "Active", render: function (data, type, row) {
                        return data == true ? "Yes" : "No";

                    }, "defaultContent": "<i>-</i>"
                },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=contact.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="contact.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 2,3,4,5,3,6] },
                { className: "text-left", "targets": [0, 2, 3, 4, 5, 3, 6] }],
            destroy: true
        });
    }
}