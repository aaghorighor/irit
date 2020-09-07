
var fellowshipSchedule = {

    create: function () {       

        $("#btnSaveChanges").click(function (event) {

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

                        _dataTables.fellowshipSchedule.ajax.reload();
                        break;
                    case 2: //// update

                        _dataTables.fellowshipSchedule.ajax.reload();
                        break;
                    default:;
                }

                $("#Id").val(0);
                $("#Active").attr("checked", false);

                iuHelper.resetForm("#form");
                $("#form").attr("action", $("#createUrl").attr("data-createUrl"));   
            });

        });

        suftnet_Settings.ClearErrorMessages("#form");
    },    
    edit: function (obj) {

        var dataobject = _dataTables.fellowshipSchedule.row($(obj).parents('tr')).data();

        $("#StartTime").val(dataobject.StartTime);
        $("#EndTime").val(dataobject.EndTime);
        $("#QueryString").val(dataobject.QueryId);
        $("#WeekDayId").val(dataobject.WeekDayId);
        $("#CycleId").val(dataobject.CycleId);
        $("#Active").attr("checked", dataobject.Active);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
      
    }, pageInit: function () {

        $("#StartTime").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "time.png",
            buttonText: "Open datepicker",
            buttonImageOnly: true
        });

        $("#EndTime").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "time.png",
            buttonText: "Open datepicker",
            buttonImageOnly: true
        });

        fellowshipSchedule.create();
        fellowshipSchedule.load();
    },
    delete: function (obj) {

        var dataobject = _dataTables.fellowshipSchedule.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblFellowshipSchedule").then(
                function (data) {
                    _dataTables.fellowshipSchedule.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    load: function () {
        _dataTables.fellowshipSchedule = $('#tblFellowshipSchedule').DataTable({
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
                { "data": "WeekDay", "defaultContent": "<i>-</i>" },
                { "data": "Cycle", "defaultContent": "<i>-</i>" },
                { "data": "StartTime", "defaultContent": "<i>-</i>" },
                { "data": "EndTime", "defaultContent": "<i>-</i>" },                
                { "data": "Active", "defaultContent": "<i>-</i>" },
                {
                    "data": null,
                    "orderable": false,
                    "defaultContent": '<a style=margin:10px; href="#" onclick=fellowshipSchedule.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>'+
                        '<a style=margin:10px; href="#" onclick="fellowshipSchedule.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4, 5] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5] },
                { className: "text-center", "targets": [6] }],
            destroy: true
        });
    }
}