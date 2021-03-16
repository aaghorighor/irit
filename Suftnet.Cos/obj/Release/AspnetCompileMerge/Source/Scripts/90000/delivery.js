var _dataTables = {};

var delivery = {

    create: function () {

        $(document).on("click", "#btnSubmit", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }
                       
            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {

                        case 1: //// add                  
                            _dataTables.delivery.draw();
                            break;
                        case 2: //// update  
                            _dataTables.delivery.draw();
                            break;

                        default: ;
                    }

                    $("#deliveryDialog").dialog("close");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));   

                }).catch(function (error) {
                    console.log(error);
                });
        });

        $("#form").attr("action", $("#createUrl").attr("data-createUrl"));   

    },
    edit: function (obj)
    {
        var dataobject = _dataTables.delivery.row($(obj).parents('tr')).data();     

        iuHelper.resetForm("#form");
        
        $("#Id").val(dataobject.Id);       
        $("#FirstName").val(dataobject.FirstName);
        $("#LastName").val(dataobject.LastName);
        $("#Email").val(dataobject.Email)
        $("#Mobile").val(dataobject.Mobile)
        $("#Note").val(dataobject.Note);
        $("#Time").val(dataobject.Time);
        $("#StatusId").val(dataobject.StatusId);

        $("#DeliveryId").val(dataobject.DeliveryId);
        $("#Latitude").val(dataobject.Latitude);
        $("#Logitude").val(dataobject.Logitude);
        $("#AddressLine").val(dataobject.AddressLine);
        $("#Duration").val(dataobject.Duration);
        $("#Distance").val(dataobject.Distance);
        $("#DeliveryCost").val(dataobject.DeliveryCost);
     
        $("#StatusId").append('<option value="E4E6975E-4881-459D-BB2D-2AD841FBA835">Ready</option>');
        $("#StatusId").append('<option value="85616F94-1826-43B1-ACFF-819B37F028E4">Processing</option>');
        $("#StatusId").append('<option value="58EE00D9-D449-4EBF-B4E8-769F51FE7EFE">Completed</option>');
        $("#StatusId").append('<option value="12BC2434-5CD8-42A2-8345-C1A3ECBC8E3B">Cancel</option>');
               
        $("#StatusId").val(dataobject.StatusId.toUpperCase());

        if (dataobject.StatusId.toUpperCase() === constants.orderStatus.completed) {
            $('#btnSubmit').addClass('disabled');
        }

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));   
        $("#deliveryDialog").dialog("open");

        suftnet.tab(1);
        
    },   
    delete: function (obj) {
                
        var dataobject = _dataTables.delivery.row($(obj).parents('tr')).data();
                                          
        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.Id }, dataobject.Id, "#tblDelivery").then(
                function (data) {                                    
                    _dataTables.delivery.row($(obj).parents('tr')).remove().draw();                    
                });
        }       

    },
    view: function (obj) {

        var dataobject = _dataTables.delivery.row($(obj).parents('tr')).data();
        window.location.href = $("#cartUrl").attr("data-cartUrl") + "/" + dataobject.Id + "/" + dataobject.OrderTypeId + "/" + dataobject.OrderType + "/" + dataobject.StatusId + "/" + dataobject.DeliveryCost;
    },
    pageInit: function () {      

        $("#Time").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open time picker",
            buttonImageOnly: true
        });

        $("#StartDt").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });       

        $(document).on("click", "#btnOpen", function () {

            iuHelper.resetForm("#form");           
            $("#deliveryDialog").dialog("open");

            suftnet.tab(0);
        });      

        $(document).on("click", "#btnClose", function () {
        
            $("#deliveryDialog").dialog("close");
        }); 

        delivery.create();    
        delivery.load();     

        $("#deliveryDialog").dialog({ autoOpen: false, width: 580, height: 800, modal: false, title: 'Delivery Order' });
    },
    load: function ()
    {          
        _dataTables.delivery = $('#tblDelivery').DataTable({
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
                { "data": "FullName", "defaultContent": "<i>-</i>" },
                { "data": "Mobile", "defaultContent": "<i>-</i>" },               
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
                    "data": "Payment", render: function (data, type, row) {
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
                    "defaultContent": '<a class="tooltip" title="View Menu to this Order" style=margin:10px; href="#" onclick=delivery.view(this)><img src=' + suftnet_grid.iconUrl + 'basket.png\ alt=\"View Menu to this Order\" /></a>'+                                      
                                      '<a class="tooltip" title="Delete this row" style=margin:10px; href="#" onclick="delivery.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'                }
            ],            
            columnDefs: [
            { "targets":[] , "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5, 6, 7,8,9] }],    
            destroy: true
        });

        _dataTables.delivery.on("draw", function () {
            $('.tooltip').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}