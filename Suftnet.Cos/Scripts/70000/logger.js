var mobileLogger = {

    view: function (obj) {

        var dataobject = _dataTables.mobileLogger.row($(obj).parents('tr')).data();
     
    },    
    pageInit: function () {

        $(document).on("click", "#clear", function (e) {

            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), null, "#tblMobileLogger").then(
                function (data) {
                    _dataTables.mobileLogger.draw();
                });

        });
       
        mobileLogger.load();
    },
    load: function () {
        _dataTables.mobileLogger = $('#tblMobileLogger').DataTable({
            "serverSide": true,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 10,
            "pagingType": "full_numbers",
            "ajax": {
                url: $("#loadUrl").attr("data-loadUrl"),
                type: 'POST'
            },
            "columns": [
                { "data": "REPORT_ID", "defaultContent": "<i>-</i>" },
                { "data": "CreatedOn", "defaultContent": "<i>-</i>" },             
                { "data": "PACKAGE_NAME", "defaultContent": "<i>-</i>" },
                { "data": "STACK_TRACE", "defaultContent": "<i>-</i>" },
                { "data": "APP_VERSION_CODE", "defaultContent": "<i>-</i>" },
                { "data": "ANDROID_VERSION", "defaultContent": "<i>-</i>" },
                { "data": "AVAILABLE_MEM_SIZE", "defaultContent": "<i>-</i>" },                         
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a title="View this row" style="margin:10px;" href="#" onclick=mobileLogger.view(this)><img src=' + suftnet_grid.iconUrl + 'document.png\ alt=\"View this row\" /></a>' 
                       
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3,4,5,6,7] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5, 6, 7]}],
            destroy: true
        });

        _dataTables.mobileLogger.on("draw", function () {
            $('a').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}

var logger = {
       
    view: function (obj) {

        var dataobject = _dataTables.logger.row($(obj).parents('tr')).data();

    },
    pageInit: function () {

        $(document).on("click", "#clear", function (e) {

            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), null, "#tblLogger").then(
                function (data) {
                    _dataTables.logger.draw();
                });
        });

        logger.load();
    },
    load: function () {
        _dataTables.logger = $('#tblLogger').DataTable({
            "serverSide": true,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 10,
            "pagingType": "full_numbers",
            "ajax": {
                url: $("#loadUrl").attr("data-loadUrl"),
                type: 'POST'
            },
            "columns": [
                { "data": "Id", "defaultContent": "<i>-</i>" },
                { "data": "CreatedOn", "defaultContent": "<i>-</i>" },
                { "data": "CreatedBy", "defaultContent": "<i>-</i>" },
                { "data": "Description", "defaultContent": "<i>-</i>" },               
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a title="View this row" style="margin:10px;" href="#" onclick=logger.view(this)><img src=' + suftnet_grid.iconUrl + 'document.png\ alt=\"View this row\" /></a>'

                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3,4] },
                { className: "text-left", "targets": [0, 1, 2, 3,4] }],
            destroy: true
        });

        _dataTables.logger.on("draw", function () {
            $('a').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}