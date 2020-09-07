
var prayerPoint = {

    create: function ()
    {      
        $("#btnSubmit").bind("click", function (event) {
            event.preventDefault();
            event.stopImmediatePropagation();

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
                                                        
                            _dataTables.prayerPoint.ajax.reload();
                            break;
                        case 2: //// update  
                                                     
                            _dataTables.prayerPoint.ajax.reload();

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

        var dataobject = _dataTables.prayerPoint.row($(obj).parents('tr')).data();

        $("#SequencyId").val(dataobject.SequencyId);
        $("#Description").val(dataobject.Description);
        $("#Active").attr("checked", dataobject.Active);
        $("#QueryString").val(dataobject.QueryId);
       
        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

    },
    delete: function (obj) {

        var dataobject = _dataTables.prayerPoint.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblPrayerPoint").then(
                function (data) {
                    _dataTables.prayerPoint.row($(obj).parents('tr')).remove().draw();
                });
        }

    },  
    pageInit: function ()
    {
        prayerPoint.create();
        prayerPoint.load();
    },
    load: function () {
        _dataTables.prayerPoint = $('#tblPrayerPoint').DataTable({
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
                { "data": "CreatedOn", "width": "10%" },
                { "data": "SequencyId", "width": "10%" },
                { "data": "Description", "width": "50%" },
                {
                    "data": "Active", render: function (data, type, row) {
                        return data == true ? "Yes" : "No";

                    }, "width": "10%"
                },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "width": "20%",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=prayerPoint.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="prayerPoint.delete(this)" ><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' 
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