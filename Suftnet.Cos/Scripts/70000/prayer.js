
var prayer = {

    create: function ()
    {
        $("#btnSubmit").bind("click", function (event) {

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

                            _dataTables.prayer.draw();
                            break;
                        case 2: //// update  

                            _dataTables.prayer.draw();

                            break;
                        default: ;
                    }
                   
                    $("#Active").attr("checked", false);
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl")); 
                    iuHelper.resetForm("#form");
                });

        });

        suftnet_Settings.ClearErrorMessages("#form");
    },
    edit: function (obj) {

        var dataobject = _dataTables.prayer.row($(obj).parents('tr')).data();

        $("#Title").val(dataobject.Title);
        $("#Description").val(dataobject.Description);
        $("#Active").attr("checked", dataobject.Active);
        $("#Title").val(dataobject.Title);
        $("#QueryString").val(dataobject.QueryId);
        $("#PrayerTypeId").val(dataobject.PrayerTypeId);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
     
    },
    delete: function (obj) {

        var dataobject = _dataTables.prayer.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblPrayer").then(
                function (data) {
                    _dataTables.prayer.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    view: function (obj) {

        var dataobject = _dataTables.prayer.row($(obj).parents('tr')).data();
        window.location.href = $("#viewUrl").attr("data-viewUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.Title) + "/" + dataobject.QueryId;
    },
    pageInit: function () {
       
        prayer.create();
        prayer.load();
    },
    load: function () {
        _dataTables.prayer = $('#tblPrayer').DataTable({
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
                { "data": "CreatedOn", "width": "10%" },
                { "data": "PrayerType", "width": "10%" },
                { "data": "Title", "width": "55%" },       
                {
                    "data": "Active", render: function (data, type, row) {
                        return data == true ? "Yes" : "No";

                    }, "width": "5%"
                },              
                {
                    "data": null,
                    "orderable": false,
                    "width": "20%",
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=prayer.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>'+
                        '<a style=margin:10px; href="#" onclick="prayer.delete(this)" ><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="prayer.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"View this row\" /></a>'
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