var _dataTables = {};

var asset = {

    create : function()
    {
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

                         _dataTables.asset.draw();
                         break;
                     case 2: //// update  

                         $("#assetsDialog").dialog("close");
                         _dataTables.asset.draw();
                     
                         break;

                     default:;
                 }

                 $("#Id").val(0);

                 iuHelper.resetForm("#form");
                 $("#form").attr("action", $("#createUrl").attr("data-createUrl"));                 
             });
        });

        suftnet_Settings.ClearErrorMessages("#form");
    },

    edit: function (obj) {

        var dataobject = _dataTables.asset.row($(obj).parents('tr')).data();

        $("#Name").val(dataobject.Name);
        $("#AssetTypeId").val(dataobject.AssetTypeId);
        $("#Cost").val(dataobject.Cost);
        $("#Reference").val(dataobject.Reference);
        $("#Description").val(dataobject.Description);
        $("#QueryString").val(dataobject.QueryId);
        $("#CreatedDT").val(dataobject.CreatedOn);
        $("#StatusId").val(dataobject.StatusId);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
        $("#assetsDialog").dialog("open");
    },
    delete: function (obj) {

        var dataobject = _dataTables.asset.row($(obj).parents('tr')).data();
        
        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblAsset").then(
                function (data) {
                    _dataTables.asset.row($(obj).parents('tr')).remove().draw();
                });
        }       

    },
    pageInit: function () {

        $("#btnClose").on("click", function (event) {

            event.stopImmediatePropagation();

            $("#Id").val(0);

            iuHelper.resetForm("#form");

            $("#assetsDialog").dialog("close");
        });

        $("#btnAsset").click(function () {

            $("#assetsDialog").dialog("open");

        });

        $("#CreatedDT").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        $("#assetsDialog").dialog({
            autoOpen: false, width: 490, height: 650, modal: false, title: 'Assets', show: { effect: 'fade', duration: 1000 },
            hide: { effect: 'fade', duration: 1000 }
        });

        asset.create();
        asset.load();
    },
    load: function () {
        _dataTables.asset = $('#tblAsset').DataTable({
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
                { "data": "Reference", "defaultContent": "<i>-</i>" },
                { "data": "AssetType", "defaultContent": "<i>-</i>" },
                { "data": "Name", "defaultContent": "<i>-</i>" },
                {
                    "data": "Cost", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i>"
                },

                { "data": "Status", "defaultContent": "<i>-</i>" },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=asset.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a><a style=margin:10px; href="#" onclick="asset.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4, 5] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5] }],
            destroy: true
        });
    }
}