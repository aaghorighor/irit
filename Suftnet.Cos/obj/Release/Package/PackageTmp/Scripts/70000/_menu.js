
var _menu = {

    create: function () {
        $("#btnSaveChanges").click(function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Active').is(':checked')) {
                $("#Active").val(true);
            } else {
                $("#Active").val(false);
            }

            if ($('#IsKitchen').is(':checked')) {
                $("#IsKitchen").val(true);
            } else {
                $("#IsKitchen").val(false);
            }        

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
            function (data) {
                switch (data.flag) {
                        case 1: //// add

                        _dataTables._menu.draw();
                            break;
                        case 2: //// update

                        _dataTables._menu.draw();
                        suftnet.tab(1); 

                            break;
                        default:;
                    }

              
                    $("#Active").attr("checked", false);
                    $("#IsKitchen").attr("checked", false);
                    $("#imageSrc").attr('src', "/content/photo/blank.jpg");                     
                                  
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));    
                    iuHelper.resetForm("#form");
            });

        });

        suftnet_Settings.ClearErrorMessages("#form");   
    },
  
    edit: function (obj) {

        var dataobject = _dataTables._menu.row($(obj).parents('tr')).data();

        $("#Name").val(dataobject.Name);
        $('#Id').val(dataobject.Id);
        $("#UnitId").val(dataobject.UnitId);      
        $("#CategoryId").val(dataobject.CategoryId);
        $("#Active").attr("checked", dataobject.Active);
        $("#IsKitchen").attr("checked", dataobject.IsKitchen);
        $("#Price").val(dataobject.Price);

        if (dataobject.CutOff != null) {
            $("#CutOff").val(dataobject.CutOff);
        } else {
            $("#CutOff").val(5);
        }

        if (dataobject.ImageUrl != null) {
            $("#ImageUrl").val(dataobject.ImageUrl);
            $("#imageSrc").attr('src', $("#imagePathUrl").attr("data-imagePathUrl") + dataobject.ImageUrl);
        }

        $("#SubStractId").val(dataobject.SubStractId);
        $("#Quantity").val(dataobject.Quantity);
        $("#Description").val(dataobject.Description);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

        suftnet.tab(0); 

    },
    delete: function (obj) {

        var dataobject = _dataTables._menu.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.Id }, dataobject.Id, "#tblMenu").then(
                function (data) {
                    _dataTables._menu.row($(obj).parents('tr')).remove().draw();
                });
        }
    },   
    view: function (obj) {

        var dataobject = _dataTables._menu.row($(obj).parents('tr')).data();
        window.location.href = $("#addonUrl").attr("data-addonUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.Name) + "/" + dataobject.Id;
    },
    pageInit: function () {
               
        _menu.create();
        _menu.load();
         suftnet_upload.init($("#uploadUrl").attr("data-uploadUrl")); 
    },
    load: function () {
        _dataTables._menu = $('#tblMenu').DataTable({
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
                { "data": "Category", "defaultContent": "<i>-</i>" },
                { "data": "Name", "defaultContent": "<i>-</i>" },
                { "data": "Unit", "defaultContent": "<i>-</i>" },
                { "data": "Quantity", "defaultContent": "<i>-</i>" },
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
                    "defaultContent": '<a title="Edit this Menu" style=margin:10px; href="#" onclick=_menu.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a title="Delete this Menu" style=margin:10px; href="#" onclick="_menu.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a title="View Addons for this Menu" style=margin:10px; href="#" onclick="_menu.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"View Addon\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2,3,4,5,6,7] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5, 6, 7] }],
            destroy: true
        });

        _dataTables._menu.on("draw", function () {
            $('a').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}