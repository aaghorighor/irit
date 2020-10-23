
var category = {

    create : function()
    {
        $("#btnSubmit").bind("click", function (e) {
            e.preventDefault();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Status').is(':checked')) {
                $("#Status").val(true);
            } else {
                $("#Status").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                  function (data) {

                      switch (data.flag) {
                          case 1: //// add    

                              _dataTables.category.ajax.reload();
                              break;
                          case 2: //// update

                              _dataTables.category.ajax.reload();

                              break;
                          default:;
                      };

                    $("#Status").attr('checked', false);
                 
                    iuHelper.resetForm("#form");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));  
                  });
        });

        suftnet_Settings.ClearErrorMessages("#form");      
    },
  
    edit: function (obj) {

        var dataobject = _dataTables.category.row($(obj).parents('tr')).data();

        $("#Name").val(dataobject.Name);      
        $("#IndexNo").val(dataobject.IndexNo);       
        $("#Description").val(dataobject.Description);  
        $("#Id").val(dataobject.Id);     
        $("#ImageUrl").val(dataobject.ImageUrl);  
        $("#Status").attr('checked', dataobject.Status);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
      
    },
    delete: function (obj) {

        var dataobject = _dataTables.category.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.Id }, dataobject.Id, "#tblCategory").then(
                function (data) {
                    _dataTables.category.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    pageInit: function () {

        suftnet_upload.init($("#uploadUrl").attr("data-uploadUrl")); 

        category.create();
        category.load();       
    },
    load: function () {
        _dataTables.category = $('#tblCategory').DataTable({        
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
                {
                    "data": "ImageUrl", render: function (data, type, row) {

                        if (data == null) {                          
                            return "<img src=" + $("#defaultImagePath").attr("data-defaultImagePath") + " width=\"32\" height=\"32\" alt=\"avatar\" />";
                        } else {                                                     
                            return "<img src=" + $("#imagePath").attr("data-imagePath") + data + " width=\"32\" height=\"32\" alt=\"avatar\" />";
                        }                       

                    }, "defaultContent": "<i>-</i > "
                },              
                { "data": "IndexNo", "defaultContent": "<i>-</i>" },
                { "data": "Name", "defaultContent": "<i>-</i>" },     
                {
                    "data": "Status", render: function (data, type, row) {
                        return data == true ? "Yes" : "No";

                    }, "defaultContent": "<i>-</i > "
                },               
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=category.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>'+
                        '<a style=margin:10px; href="#" onclick="category.delete(this)" > <img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3,4] },
                { className: "text-left", "targets": [0, 1, 2, 3,4] }],
            destroy: true
        });
    }
}