
var attendance = {

    create : function()
    {
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

                        _dataTables.attendance.draw();
                        break;
                    case 2: //// update

                        _dataTables.attendance.draw();
                                              
                        break;
                    default:;
                }

                $("#Id").val(0);
                iuHelper.resetForm("#form");
                $("#form").attr("action", $("#createUrl").attr("data-createUrl"));       
                                        
            });

        });

        suftnet_Settings.ClearErrorMessages("#form");       
    },

    edit: function (obj) {

        var dataobject = _dataTables.attendance.row($(obj).parents('tr')).data();

        $("#EventTypeId").val(dataobject.EventTypeId);
        $("#count").val(dataobject.count);
        $("#Note").val(dataobject.Note);
        $("#QueryString").val(dataobject.QueryId);
        $("#CreatedDT").val(dataobject.CreatedOn);     

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

    },
    delete: function (obj) {

        var dataobject = _dataTables.attendance.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblAttendance").then(
                function (data) {
                    _dataTables.attendance.row($(obj).parents('tr')).remove().draw();
                });
        }
    },   
    view: function (obj) {

        var dataobject = _dataTables.attendance.row($(obj).parents('tr')).data();
        window.location.href = $("#mappingUrl").attr("data-mappingUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.EventType) + "/" + dataobject.QueryId;       
    },
    pageInit: function () {

        $("#CreatedDT").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        attendance.create();
        attendance.load();
    },
    load: function () {
        _dataTables.attendance = $('#tblAttendance').DataTable({
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
                { "data": "EventType", "defaultContent": "<i>-</i>" },
                { "data": "count", "defaultContent": "<i>-</i>" },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=attendance.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="attendance.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="attendance.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"View Attendance count\" /></a>'                       
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