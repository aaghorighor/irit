
var smallGroup = {

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

            if ($('#IsRegister').is(':checked')) {
                $("#IsRegister").val(true);
            } else {
                $("#IsRegister").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
            function (data) {
                switch (data.flag) {
                    case 1: //// add

                        _dataTables.smallGroup.draw();
                        break;
                    case 2: //// update

                        _dataTables.smallGroup.draw();
                        break;
                    default:;
                }

                $("#Id").val(0);
                $("#IsRegister").attr("checked", false);
                $("#Active").attr("checked", false);

                    iuHelper.resetForm("#form");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));  
            });

        });

        suftnet_Settings.ClearErrorMessages("#form");
    },
       
    edit: function (obj) {

        var dataobject = _dataTables.smallGroup.row($(obj).parents('tr')).data();
               
        $("#Title").val(dataobject.Title);
        $("#Description").val(dataobject.Description);
        $("#QueryString").val(dataobject.QueryId);
        $("#IsRegister").attr("checked", dataobject.IsRegister);
        $("#Active").attr("checked", dataobject.Active);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

    },
    delete: function (obj) {

        var dataobject = _dataTables.smallGroup.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblSmallGroup").then(
                function (data) {
                    _dataTables.smallGroup.row($(obj).parents('tr')).remove().draw();
                });
        }
    },
    viewSchedule: function (obj) {

        var dataobject = _dataTables.smallGroup.row($(obj).parents('tr')).data();
        window.location.href = $("#scheduleUrl").attr("data-scheduleUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.Title) + "/" + dataobject.QueryId;
    },
    view: function (obj) {

        var dataobject = _dataTables.smallGroup.row($(obj).parents('tr')).data();
        window.location.href = $("#mappingUrl").attr("data-mappingUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.Title) + "/" + dataobject.QueryId;
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

        smallGroup.create();
        smallGroup.load();
    },
    load: function () {
        _dataTables.smallGroup = $('#tblSmallGroup').DataTable({
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
                { "data": "Title", "defaultContent": "<i>-</i>" },               
                { "data": "Active", "defaultContent": "<i>-</i>" },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center" ,
                    "defaultContent": '<a style=margin:10px; href="#" onclick=smallGroup.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="smallGroup.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="smallGroup.view(this)"><img src=' + suftnet_grid.iconUrl + 'user.png\ alt=\"View SmallGroup Member\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="smallGroup.viewSchedule(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"View SmallGroup Schedule\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2] },
                { className: "text-left", "targets": [0, 1, 2] }],             
            destroy: true
        });
    }
}