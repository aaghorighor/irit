var _dataTables = {};

var eventTable = {

    edit: function (obj) {

        var dataobject = _dataTables.events.row($(obj).parents('tr')).data();     
        window.location.href = $("#editUrl").attr("data-editUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.Title) + "/" + dataobject.QueryId;
    },
    pageInit : function()
    {
        eventTable.load();
    },
    load: function () {
        _dataTables.events = $('#tblEvent').DataTable({
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
                { "data": "Title", "defaultContent": "<i>-</i>" },
                { "data": "StartLongDt", "defaultContent": "<i>-</i>" },
                { "data": "EndLongDt", "defaultContent": "<i>-</i>" },
                { "data": "Venue", "defaultContent": "<i>-</i>" },
                { "data": "Status", "defaultContent": "<i>-</i>" },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=eventTable.edit(this)>'+
                        '<img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4, 5,6] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5,6] }],
            destroy: true
        });
    }
}