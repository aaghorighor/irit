﻿
var commonType = {

    create : function()
    {
        $("#btnSubmit").bind("click", function (e) {
            e.preventDefault();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Active').is(':checked')) {
                $("#Active").val(true);
            } else {
                $("#Active").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                  function (data) {

                      switch (data.flag) {
                          case 1: //// add    

                              _dataTables.commonType.ajax.reload();
                              break;
                          case 2: //// update

                              _dataTables.commonType.ajax.reload();

                              break;
                          default:;
                      };

                      $("#Active").attr('checked', false);
                 
                    iuHelper.resetForm("#form");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));  
                  });
        });

        suftnet_Settings.ClearErrorMessages("#form");
    },
  
    edit: function (obj) {

        var dataobject = _dataTables.commonType.row($(obj).parents('tr')).data();

        $("#Name").val(dataobject.Name);      
        $("#IndexNo").val(dataobject.IndexNo);        
        $("#Id").val(dataobject.Id);     
        $("#Active").attr('checked', dataobject.Active);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
      
    },
    delete: function (obj) {

        var dataobject = _dataTables.commonType.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.Id }, dataobject.Id, "#tblCommonType").then(
                function (data) {
                    _dataTables.commonType.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    pageInit: function () {
                    
        commonType.create();
        commonType.load();
    },
    load: function () {
        _dataTables.commonType = $('#tblCommonType').DataTable({        
            "serverSide": false,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],           
            "ordering": true,
            "pagingType": "full_numbers",
            "ajax": {
                "url": $("#loadUrl").attr("data-loadUrl"),
                "type": "GET",
                "datatype": "json"    
            },
            "columns": [
                { "data": "CreatedOn", "defaultContent": "<i>-</i>" },
                { "data": "IndexNo", "defaultContent": "<i>-</i>" },
                { "data": "Name", "defaultContent": "<i>-</i>" },        
                { "data": "Active", "defaultContent": "<i>-</i>" },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a class="tooltip" title="Edit this row" style=margin:10px; href="#" onclick=commonType.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>'+
                        '<a class="tooltip" title="Delete this row" style=margin:10px; href="#" onclick="commonType.delete(this)" > <img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3] },
                { className: "text-left", "targets": [0, 1, 2, 3] }],
            destroy: true
        });

        _dataTables.commonType.on("draw", function () {
            $('.tooltip').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}