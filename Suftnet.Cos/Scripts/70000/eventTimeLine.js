
var eventTimeLine = {

    create : function(startDate, endDate)
    {
        $("#btnSubmit").bind("click", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
             function (data) {
                 switch (data.flag) {
                     case 1: 

                         _dataTables.eventTimeLine.draw();
                         break;
                     case 2: 

                         _dataTables.eventTimeLine.draw();
                         break;
                     default:;
                 }

                 $("#Id").val(0);
                 iuHelper.resetForm("#form");

             });
        });
        
        suftnet_Settings.ClearErrorMessages("#form");
    },
   
    edit: function (obj) {

        var dataobject = _dataTables.eventTimeLine.row($(obj).parents('tr')).data();

        $("#TimeLineTypeId").val(dataobject.TimeLineTypeId);
        $("#Description").val(dataobject.Description);
        $("#QueryString").val(dataobject.QueryId);
        $("#MemberExternalId").val(dataobject.QueryId_2);
        $("#EndTime").val(dataobject.EndTime);
        $("#StartTime").val(dataobject.StartTime);
        $("#StartDate").val(dataobject.StartDt);
        $("#Member").val(dataobject.FullName);
        
        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
      
    },
    delete: function (obj) {

        var dataobject = _dataTables.eventTimeLine.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblEventTimeLine").then(
                function (data) {
                    _dataTables.eventTimeLine.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    pageInit: function () {

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

        $("#StartDate").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        eventTimeLine.create();
        eventTimeLine.load();
    },
    load: function () {
        _dataTables.eventTimeLine = $('#tblEventTimeLine').DataTable({
            "serverSide": true,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 5,
            "pagingType": "full_numbers",
            "ajax": {
                url: $("#loadUrl").attr("data-loadUrl"),
                type: 'GET'
            },
            "columns": [
                { "data": "CreatedOn", "defaultContent": "<i>-</i>" },
                { "data": "TimeLineType", "defaultContent": "<i>-</i>" },
                { "data": "StartTime", "defaultContent": "<i>-</i>" },
                { "data": "EndTime", "defaultContent": "<i>-</i>" },
                { "data": "FullName", "defaultContent": "<i>-</i>" },               
                {
                    "data": null,
                    "orderable": false,
                    "defaultContent":'<a style=margin:10px; href="#" onclick=eventTimeLine.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a><a style=margin:10px; href="#" onclick="eventTimeLine.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3,4] },
                { className: "text-left", "targets": [0, 1, 2, 3,4] },
                { className: "text-center", "targets": [5] }],
            destroy: true
        });
    }
}