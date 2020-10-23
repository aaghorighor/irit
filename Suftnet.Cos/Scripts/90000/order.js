var _dataTables = {};

var order = {

    create: function () {

        $(document).on("click", "#btnSaveChanges", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            if (!suftnet_validation.isValid("editform")) {
                return false;
            }
                       
            var params = {
                Id: $("#editId").val(),  
                tableId: $("#editTableId").val(),               
                ExpectedGuest: $("#editExpectedGuest").val(),
                ChangeTable: false,
                Time: $("#editTime").val(),                
                StatusId: $("#editStatusId").val(),
            }

            if ($('#ChangeTable').is(':checked')) {
                params.ChangeTable =true;
            } else {
                params.ChangeTable = false;
            }

            console.log(params);
            console.log($("#editform").attr("action"));

            js.ajaxPost($("#editform").attr("action"), params).then(
                function (data) {
                    switch (data.flag) {

                        case 1: //// add                  
                            
                            break;
                        case 2: //// update  
                          
                            break;

                        default:

                            break;
                    }

                    _dataTables.order.draw();
                    $("#dineInDialog").dialog("close");

                }).catch(function (error) {
                    console.log(error);
                });
        });     

    },
    edit: function (obj)
    {
        iuHelper.resetForm("#editform");

        var dataobject = _dataTables.order.row($(obj).parents('tr')).data(); 
     
        $("#editId").val(dataobject.Id);
        $("#editTableId").val(dataobject.TableId);         
        $("#ExpectedGuest").val(dataobject.ExpectedGuest);
        $("#editTime").val(dataobject.Time);       
        $("#editStatusId").val(dataobject.StatusId);

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
        window.location.href = $("#cartUrl").attr("data-cartUrl") + "/" + dataobject.Id + "/" + dataobject.OrderTypeId + "/" + dataobject.OrderType;
    },
    listener: function () {

        $(document).on("click", "#btnCloseOrder", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            iuHelper.resetForm("#editform");

            $("#dineInDialog").dialog("close");
        });

    },
    pageInit: function () {       
       
        order.load();
        order.listener();
        order.create();

        $("#dineInDialog").dialog({ autoOpen: false, width: 580, height: 400, modal: false, title: 'Edit' });
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
                url: $("#fetchUrl").attr("data-fetchUrl"),
                type: 'POST'
            },
            "columns": [
                { "data": "Id", "visible": false, "defaultContent": "<i>-</i>" },
                { "data": "UpdatedOn", "visible": true, "defaultContent": "<i>-</i>" },
                { "data": "Time", "defaultContent": "<i>-</i>" },
                { "data": "Table", "defaultContent": "<i>-</i>" },                  
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
                { "orderable": false, "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5, 6, 7,8,9,10] }],    
            destroy: true
        });
    }
}