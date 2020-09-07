
var table = {

    create: function () {       

        $("#btnSubmit").bind("click", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }
                       
            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add

                            _dataTables.table.ajax.reload();
                            break;
                        case 2: //// update

                            _dataTables.table.ajax.reload();
                            break;
                        default: ;
                    }
                
                    $("#Active").attr('checked', false);
                    iuHelper.resetForm("#form");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl")); 

                });
        });
        suftnet_Settings.ClearErrorMessages("#form");
    },
  
    edit: function (obj) {

        var dataobject = _dataTables.table.row($(obj).parents('tr')).data();

        $("#Number").val(dataobject.Number);    
        $("#Size").val(dataobject.Size);
        $("#Id").val(dataobject.Id);
        $("#Active").attr('checked', dataobject.Active);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

    },
    delete: function (obj) {

        var dataobject = _dataTables.table.row($(obj).parents('tr')).data();

        if (dataobject !== null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.Id }, dataobject.Id, "#tblTable").then(
                function (data) {
                    _dataTables.table.row($(obj).parents('tr')).remove().draw();
                });
        }
    },    
    pageInit: function () {
               
        table.create();
        table.load();
    },
    load: function () {
        _dataTables.table = $('#tblTable').DataTable({
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
                { "data": "Number", "defaultContent": "<i>-</i>" },
                { "data": "Size", "defaultContent": "<i>-</i>" },
                {
                    "data": "Active", render: function (data, type, row) {
                        return data == true ? "Yes" : "No";
                    }, "defaultContent": "<i>-</i>"
                },                            
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=table.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="table.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'                      
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4] }],
            destroy: true
        });
    }
}