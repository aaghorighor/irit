var prayerRequest = {

    create: function () {
        $("#btnSubmit").bind("click", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add

                            _dataTables.prayerRequest.draw();
                            break;
                        case 2: //// update                                                       

                            break;
                        default: ;
                    }

                    $("#Id").val(0);
                    iuHelper.resetForm("#form");
                });
        });

        suftnet_Settings.ClearErrorMessages("#form");
    },   
    delete: function (obj) {

        var dataobject = _dataTables.prayerRequest.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblPrayerRequest").then(
                function (data) {
                    _dataTables.prayerRequest.row($(obj).parents('tr')).remove().draw(true);
                });
        }

    },
    load: function () {

        _dataTables.prayerRequest = $('#tblPrayerRequest').DataTable({
            "serverSide": true,
            "ordering": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pagingType": "full_numbers",
            "ajax": {
                url: $("#loadUrl").attr("data-loadUrl"),
                type: 'POST'
            },
            columns: [
                { "data": "CreatedOn", "width": "10%" },
                { "data": "FullName", "width": "20%"},
                { "data": "Message", "width": "60%"},            
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "width": "10%",
                    "defaultContent": '<a style=margin:10px; href="#" onclick="prayerRequest.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
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