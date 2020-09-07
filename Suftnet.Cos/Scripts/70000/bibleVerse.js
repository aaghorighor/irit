
var bibleVerse = {

    create: function () {
       
        $("#btnSubmit").bind("click", function (event) {

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

            if ($('#PushNotification').is(':checked')) {
                $("#PushNotification").val(true);
            } else {
                $("#PushNotification").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add                                          

                            _dataTables.bibleVerse.draw();
                            break;
                        case 2: //// update  

                            _dataTables.bibleVerse.draw();

                            break;
                        default: ;
                    }
                
                    $("#Active").attr("checked", false);
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl")); 
                    iuHelper.resetForm("#form");
                });
        });

        suftnet_Settings.ClearErrorMessages("#form");
    },
  
    edit: function (obj) {

        var dataobject = _dataTables.bibleVerse.row($(obj).parents('tr')).data();
        
        $("#Verse").val(dataobject.Verse);
        $("#CreatedDT").val(dataobject.CreatedOn);
        $("#Description").val(dataobject.Description);
        $("#Active").attr("checked", dataobject.Active);
        $("#QueryString").val(dataobject.QueryId);
               
        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
    },
    delete: function (obj) {

        var dataobject = _dataTables.bibleVerse.row($(obj).parents('tr')).data();

        if (dataobject !== null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblBibleVerse").then(
                function (data) {
                    _dataTables.bibleVerse.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    pageInit: function () {

        $("#CreatedDT").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });       

        bibleVerse.create();
        bibleVerse.load();
    },
    load: function () {
        _dataTables.bibleVerse = $('#tblBibleVerse').DataTable({
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
                { "data": "CreatedOn", "width": "10%" },
                { "data": "Verse", "width": "10%" },
                { "data": "Description", "width": "60%" },
                {
                    "data": "Active", render: function (data, type, row) {
                        return data == true ? "Yes" : "No";

                    }, "width": "5%"
                },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "width": "15%",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=bibleVerse.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="bibleVerse.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3] },            
                { className: "text-left", "targets": [0, 1, 2, 3] }],
            destroy: true
        });
    }
}