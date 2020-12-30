
var addon = {

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

                              _dataTables.addon.ajax.reload();
                              break;
                          case 2: //// update

                              _dataTables.addon.ajax.reload();

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

        var dataobject = _dataTables.addon.row($(obj).parents('tr')).data();

        $("#Name").val(dataobject.Name);                
        $("#Id").val(dataobject.Id);     
        $("#Active").attr('checked', dataobject.Active);
        $("#Price").val(dataobject.Price);  
        $("#AddonTypeId").val(dataobject.AddonTypeId);  

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
      
    },
    delete: function (obj) {

        var dataobject = _dataTables.addon.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.Id }, dataobject.Id, "#tblAddon").then(
                function (data) {
                    _dataTables.addon.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    pageInit: function () {
                    
        addon.create();
        addon.load();
    },
    load: function () {
        _dataTables.addon = $('#tblAddon').DataTable({        
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
                { "data": "AddonType", "defaultContent": "<i>-</i>" },  
                { "data": "Name", "defaultContent": "<i>-</i>" },        
                {
                    "data": "Price", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                },
                {
                    "data": "Active", render: function (data, type, row) {
                        return data == true ? "YES" : "NO";

                    }, "defaultContent": "<i>-</i > "
                },  
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a class="tooltip" title="Edit this row" style=margin:10px; href="#" onclick=addon.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>'+
                        '<a class="tooltip" title="Delete this row" style=margin:10px; href="#" onclick="addon.delete(this)" > <img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3,4,5] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5] }],
            destroy: true
        });

        _dataTables.addon.on("draw", function () {
            $('.tooltip').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}