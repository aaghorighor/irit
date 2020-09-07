
var order = {

    create: function ()
    {
        $("#Time").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            buttonImageOnly: true
        });

        $("#CreatedDt").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });


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

                            tableViewModel.update(data.dataobject.orderId);
                                                     
                            break;
                        case 2: //// update
                          
                            break;
                        default: ;
                    }   

                    iuHelper.resetForm("#form");                  
                });

        });

        suftnet_Settings.ClearErrorMessages("#form");      
    },

    edit: function (obj) {

        var dataobject = _dataTables.order.row($(obj).parents('tr')).data();
        
    },
    delete: function (obj) {

        var dataobject = _dataTables.order.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.Id }, dataobject.Id, "#tblOrder").then(
                function (data) {
                    _dataTables.order.row($(obj).parents('tr')).remove().draw();
                });
        }
    },
    view: function (obj) {

        var dataobject = _dataTables.order.row($(obj).parents('tr')).data();
        window.location.href = $("#cartUrl").attr("data-cartUrl") + "/" + dataobject.Id;
    },
    pageInit: function () {      
        order.load();
    },
    load: function () {
        _dataTables.order = $('#tblOrder').DataTable({
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
                { "data": "CreatedBy", "defaultContent": "<i>-</i>" },
                { "data": "OrderType", "defaultContent": "<i>-</i>" },
                { "data": "Guest", "defaultContent": "<i>-</i>" },
                { "data": "Time", "defaultContent": "<i>-</i>" },
                { "data": "Table", "defaultContent": "<i>-</i>" },
                { "data": "ExpectedGuest", "defaultContent": "<i>-</i>" },
                { "data": "Status", "defaultContent": "<i>-</i>" },
                {
                    "data": "GrandTotal", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                },
                {
                    "data": "Payment", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                },
                {
                    "data": "Balance", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                },               
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=order.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="order.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="order.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"View Order Details\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4, 5, 6, 7,8,9,10,11] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11] }],
            destroy: true
        });
    }
}