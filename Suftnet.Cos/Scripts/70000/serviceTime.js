
var serviceTime = {

    create: function () {       

        $("#btnSubmit").on("click", function (event) {

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

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add

                            _dataTables.serviceTime.draw();
                            break;
                        case 2: //// update

                            _dataTables.serviceTime.draw();
                            break;
                        default: ;
                    }

                    $("#Id").val(0);
                    $("#Active").attr("checked", false);
                    iuHelper.resetForm("#form");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl")); 

                });
        });
        suftnet_Settings.ClearErrorMessages("#form");
    },

    edit: function (obj) {

        var dataobject = _dataTables.serviceTime.row($(obj).parents('tr')).data();

        $("#ServiceTypeId").val(dataobject.ServiceTypeId);
        $("#ServiceGroupId").val(dataobject.ServiceGroupId);
        $("#Description").val(dataobject.Description);
        $("#QueryString").val(dataobject.QueryId);
        $("#EndTime").val(dataobject.EndTime);
        $("#StartTime").val(dataobject.StartTime);
        $("#Index").val(dataobject.Index);
        $("#Active").attr("checked", dataobject.Active);
        $("#CreatedDT").val(dataobject.CreatedOn);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

    },
    delete: function (obj) {

        var dataobject = _dataTables.serviceTime.row($(obj).parents('tr')).data();

        if (dataobject !== null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblServiceTime").then(
                function (data) {
                    _dataTables.serviceTime.row($(obj).parents('tr')).remove().draw();
                });
        }
    },    
    view: function (obj) {

        var dataobject = _dataTables.serviceTime.row($(obj).parents('tr')).data();  
        window.location.href = $("#viewUrl").attr("data-viewUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.ServiceType) + "/" + dataobject.QueryId;
    },
    pageInit: function () {

        $("#StartTime").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "time.png",
            buttonText: "Open datepicker",
            buttonImageOnly: true
        });

        $("#EndTime").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "time.png",
            buttonText: "Open datepicker",
            buttonImageOnly: true
        });

        serviceTime.create();
        serviceTime.load();
    },
    load: function () {
        _dataTables.serviceTime = $('#tblServiceTime').DataTable({
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
                { "data": "Index", "defaultContent": "<i>-</i>" },
                { "data": "ServiceType", "defaultContent": "<i>-</i>" },
                { "data": "EndTime", "defaultContent": "<i>-</i>" },
                { "data": "StartTime", "defaultContent": "<i>-</i>" },
                {
                    "data": "Active", render: function (data, type, row) {
                        return data == true ? "Yes" : "No";

                    }, "defaultContent": "<i>-</i>"
                },              
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=serviceTime.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="serviceTime.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="serviceTime.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"View Service TimeLine\" /></a>'                         
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2,3,4] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4] }],
            destroy: true
        });
    }
}