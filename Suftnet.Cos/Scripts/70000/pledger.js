var givers = {

    create: function () {
      
        $("#btnSubmit").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
             function (data) {
                 switch (data.flag) {
                     case 1: //// add                                          

                         _dataTables.givers.ajax.reload();
                         break;
                     case 2: //// update  

                         _dataTables.givers.ajax.reload();

                         break;
                     default:;
                 }

                    $("#Id").val(0);
                    $("#Active").attr("checked", false);
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));  
                    iuHelper.resetForm("#form");       
             });

            suftnet_Settings.ClearErrorMessages("#form");
        });
    },
   
    edit: function (obj) {

        var dataobject = _dataTables.givers.row($(obj).parents('tr')).data();

        $("#FirstName").val(dataobject.FirstName);      
        $("#LastName").val(dataobject.LastName);
        $("#Mobile").val(dataobject.Mobile);
        $("#Email").val(dataobject.Email);
        $("#Amount").val(dataobject.Amount);
        $("#QueryString").val(dataobject.QueryId);
              
        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));     
    },    
    delete: function (obj) {

        var dataobject = _dataTables.givers.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblGivers").then(
                function (data) {
                    _dataTables.givers.row($(obj).parents('tr')).remove().draw();
                });
        }
    },
    pageInit: function () {
                
        givers.create();
        givers.load();      
    },
    load: function () {
        _dataTables.givers = $('#tblGivers').DataTable({
            "serverSide": false,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 5,
            "pagingType": "full_numbers",
            "info": false,
            "ajax": {
                "url": $("#loadUrl").attr("data-loadUrl"),
                "type": "GET",
                "datatype": "json" 
            },
            "columns": [
                { "data": "CreatedOn", "defaultContent": "<i>-</i>" },
                { "data": "FirstName", "defaultContent": "<i>-</i>" },
                { "data": "LastName", "defaultContent": "<i>-</i>" },
                { "data": "Email", "defaultContent": "<i>-</i>" },
                { "data": "Mobile", "defaultContent": "<i>-</i>" },              
                {
                    "data": "Amount", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=givers.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="givers.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'                    
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [ 1, 2,4,5] },
                { className: "text-left", "targets": [ 1, 2,4,5] }],
            destroy: true
        });
    }
}