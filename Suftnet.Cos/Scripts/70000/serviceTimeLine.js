
var serviceTimeLine = {

    create: function () {       

        $("#btnSubmit").bind("click", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            var description = tinymce.activeEditor.getContent();
            $("#Description").val(description);

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add

                            _dataTables.serviceTimeLine.ajax.reload();
                            break;
                        case 2: //// update

                            _dataTables.serviceTimeLine.ajax.reload();
                            break;
                        default: ;
                    }

                    $("#Id").val(0);

                    iuHelper.resetForm("#form");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl")); 

                });
        });
        suftnet_Settings.ClearErrorMessages("#form");
    },
  
    edit: function (obj) {

        var dataobject = _dataTables.serviceTimeLine.row($(obj).parents('tr')).data();

        $("#ServiceTypeId").val(dataobject.ServiceTypeId);
        tinymce.get('Description').setContent(dataobject.Description);
        $("#QueryString").val(dataobject.QueryId);
        $("#MemberExternalId").val(dataobject.QueryId_2);
        $("#EndTime").val(dataobject.EndTime);
        $("#StartTime").val(dataobject.StartTime);
        $("#Member").val(dataobject.FullName);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

    },
    delete: function (obj) {

        var dataobject = _dataTables.serviceTimeLine.row($(obj).parents('tr')).data();

        if (dataobject !== null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblServiceTimeLine").then(
                function (data) {
                    _dataTables.serviceTimeLine.row($(obj).parents('tr')).remove().draw();
                });
        }
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

        serviceTimeLine.create();
        serviceTimeLine.load();
    },
    load: function () {
        _dataTables.serviceTimeLine = $('#tblServiceTimeLine').DataTable({
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
                { "data": "ServiceType", "defaultContent": "<i>-</i>" },
                { "data": "StartTime", "defaultContent": "<i>-</i>" },
                { "data": "EndTime", "defaultContent": "<i>-</i>" },
                { "data": "FullName", "defaultContent": "<i>-</i>" },               
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=serviceTimeLine.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="serviceTimeLine.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'                      
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