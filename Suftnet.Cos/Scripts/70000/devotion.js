var devotion = {

    create: function () {

        $("#btnSubmit").bind("click", function (e) {

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

            $("#Description").val(tinymce.activeEditor.getContent());

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {

                    switch (data.flag) {
                        case 1: //// add                                          

                            _dataTables.devotion.draw();
                            break;
                        case 2: //// update  

                            _dataTables.devotion.draw();
                            suftnet.tab(1);

                            break;
                        default: ;
                    }

                    $("#Active").attr('checked', false);
                    $("#Id").val(0);
                    iuHelper.resetForm("#form");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl")); 

                });

        });
        suftnet_Settings.ClearErrorMessages("#form");
    },

    edit: function (obj) {

        var dataobject = _dataTables.devotion.row($(obj).parents('tr')).data();

        $("#Title").val(dataobject.Title);
        $("#ShortDescription").val(dataobject.ShortDescription);
        $("#QueryString").val(dataobject.QueryId);
        tinymce.get('Description').setContent(dataobject.Description);
        $("#Active").attr("checked", dataobject.Active);
        $("#ImageUrl").val(dataobject.ImageUrl);
        $("#Author").val(dataobject.Author);
        $("#CreatedDT").val(dataobject.CreatedOn);

        suftnet.tab(0);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
    },
    delete: function (obj) {

        var dataobject = _dataTables.devotion.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblDevotion").then(
                function (data) {
                    _dataTables.devotion.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    pageInit: function () {

        $("#CreatedDT").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        suftnet_upload.init($("#uploadUrl").attr("data-uploadUrl"));

        devotion.create();
        devotion.load();
    },
    load: function () {
        _dataTables.devotion = $('#tblDevotion').DataTable({
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
                { "data": "Author", "defaultContent": "<i>-</i>" },
                { "data": "Title", "defaultContent": "<i>-</i>" },
                {
                    "data": "Active", render: function (data, type, row) {
                        return data == true ? "Yes" : "No";

                    }, "defaultContent": "<i>-</i>"
                },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=devotion.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin: 10px; href="#" onclick="devotion.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3] },
                { className: "text-left", "targets": [0, 1, 2, 3] }],
            destroy: true
        });
    }
}