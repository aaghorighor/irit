var _dataTables = {};

var order = {

    create: function () {

        $(document).on("click", "#btnSaveChanges", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            if (!suftnet_validation.isValid("orderform")) {
                return false;
            }

            if ($('#ChangeTable').is(':checked')) {
                $("#ChangeTable").val(true);
            } else {
                $("#ChangeTable").val(false);
            }

            js.ajaxPost($("#orderform").attr("action"), $("#orderform").serialize()).then(
                function (data) {
                    switch (data.flag) {

                        case 1: //// add                  
                            _dataTables.order.draw();
                            break;
                        case 2: //// update  
                            _dataTables.order.draw();
                            break;

                        default: ;
                    }

                    $("#dineInDialog").dialog("close");

                }).catch(function (error) {
                    console.log(error);
                });
        });     

    },
    edit: function (obj)
    {
        iuHelper.resetForm("#orderform");

        var dataobject = _dataTables.order.row($(obj).parents('tr')).data();     

        $("#Id").val(dataobject.Id);
        $("#TableId").val(dataobject.TableId);
        $("#FirstName").val(dataobject.FirstName);
        $("#LastName").val(dataobject.LastName);       
        $("#ExpectedGuest").val(dataobject.ExpectedGuest);
        $("#Time").val(dataobject.Time);       
        $("#StatusId").val(dataobject.StatusId);

        $("#dineInDialog").dialog("open");
        
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
    listener: function () {

        $(document).on("click", "#btnCloseOrder", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            iuHelper.resetForm("#orderform");

            $("#dineInDialog").dialog("close");
        });

    },
    pageInit: function () {       

        $("#dineInDialog").dialog({ autoOpen: false, width: 580, height: 480, modal: false, title: 'Dine In' });

        order.load();
        order.listener();
        order.create();
    },
    load: function ()
    {          
        _dataTables.order = $('#tblOrder').DataTable({
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
                { "data": "Id", "visible": false, "defaultContent": "<i>-</i>" },
                { "data": "UpdatedOn", "visible": true, "defaultContent": "<i>-</i>" },
                { "data": "Time", "defaultContent": "<i>-</i>" },
                { "data": "Table", "defaultContent": "<i>-</i>" },  
                { "data": "FullName", "defaultContent": "<i>-</i>" },   
                { "data": "Status", "defaultContent": "<i>-</i>"  },              
                {
                    "data": "Total", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "  },    
                {
                    "data": "TotalTax", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                }, 
                {
                    "data": "TotalDiscount", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                }, 
                {
                    "data": "GrandTotal", render: function (data, type, row) {
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
                    "defaultContent": '<a style=margin:10px; href="#" onclick=order.view(this)><img src=' + suftnet_grid.iconUrl + 'basket.png\ alt=\"View this Order\" /></a>'+
                                      '<a style=margin:10px; href="#" onclick="order.edit(this)"><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                                      '<a style=margin:10px; href="#" onclick="order.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'                }
            ],            
            columnDefs: [
            { "targets":[] , "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5, 6, 7,8,9,10,11] }],    
            destroy: true
        });
    }
}