var _dataTables = {};

var reservation = {
      
    edit: function (obj)
    {
        var dataobject = _dataTables.reservation.row($(obj).parents('tr')).data();   

        iuHelper.resetForm("#form");

        $("#Id").val(dataobject.Id);
        $("#TableId").val(dataobject.TableId);
        $("#FirstName").val(dataobject.FirstName);
        $("#LastName").val(dataobject.LastName);
        $("#Email").val(dataobject.Email);
        $("#Mobile").val(dataobject.Mobile);
        $("#ExpectedGuest").val(dataobject.ExpectedGuest);
        $("#Time").val(dataobject.Time);
        $("#StartDt").val(dataobject.StartOn);
        $("#Note").val(dataobject.Note);
        $("#StatusId").val(dataobject.StatusId);

        $("#StatusId").append('<option value="58EE00D9-D449-4EBF-B4E8-769F51FE7EFE">Completed</option>');
        $("#StatusId").append('<option value="12BC2434-5CD8-42A2-8345-C1A3ECBC8E3B">Cancel</option>');

        $("#reservationDialog").dialog("open");
        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
        
    },   
    delete: function (obj) {
                
        var dataobject = _dataTables.reservation.row($(obj).parents('tr')).data();
                                          
        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.Id }, dataobject.Id, "#tblReservation").then(
                function (data) {                                    
                    _dataTables.reservation.row($(obj).parents('tr')).remove().draw();                    
                });
        }       

    },
    view: function (obj) {

        var dataobject = _dataTables.reservation.row($(obj).parents('tr')).data();
        window.location.href = $("#cartUrl").attr("data-cartUrl") + "/" + dataobject.Id + "/" + dataobject.OrderTypeId + "/" + dataobject.OrderType + "/" + dataobject.StatusId + "/" + "0";
    },
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
                            _dataTables.reservation.draw();
                            break;
                        case 2: //// update  
                            _dataTables.reservation.draw();
                            break;

                        default: ;
                    }

                    $("#reservationDialog").dialog("close");

                }).catch(function (error) {
                    console.log(error);
                });
        });  

        $("#form").attr("action", $("#createUrl").attr("data-createUrl"));

    },
    listener: function () {

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

        $(document).on("click", "#btnClose", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            iuHelper.resetForm("#form");

            $("#reservationDialog").dialog("close");
        });

        $(document).on("click", "#btnOpen", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            iuHelper.resetForm("#form");

            $("#Id").val("c7a037e8-3045-4996-9e02-1794e08622c6");
            $("#TableId").val("c7a037e8-3045-4996-9e02-1794e08622c6");

            $("#reservationDialog").dialog("open");
        });
       
    },
    pageInit: function () {       

        $("#reservationDialog").dialog({ autoOpen: false, width: 580, height: 700, modal: false, title: 'Reservation Order' });

        reservation.create();
        reservation.listener();
        reservation.load();      
    },
    load: function ()
    {          
        _dataTables.reservation = $('#tblReservation').DataTable({
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
                { "data": "StartOn", "visible": true, "defaultContent": "<i>-</i>" },
                { "data": "Time", "defaultContent": "<i>-</i>" },
                { "data": "Table", "defaultContent": "<i>-</i>" },
                { "data": "FullName", "defaultContent": "<i>-</i>" },               
                { "data": "ExpectedGuest", "defaultContent": "<i>-</i>" },
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
                    "defaultContent": '<a style=margin:10px; href="#" onclick=reservation.view(this)><img src=' + suftnet_grid.iconUrl + 'basket.png\ alt=\"View this Reservation\" /></a>'+
                                      '<a style=margin:10px; href="#" onclick="reservation.edit(this)"><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                                      '<a style=margin:10px; href="#" onclick="reservation.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'                }
            ],            
            columnDefs: [
            { "targets":[] , "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12] }],    
            destroy: true
        });

        _dataTables.reservation.on("draw", function () {
            $('a').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}