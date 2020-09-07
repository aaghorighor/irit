var _dataTables = {};

var venue = {

    create : function()
    {        
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

            if ($('#InternalVenue').is(':checked')) {
                $("#InternalVenue").val(true);
            } else {
                $("#InternalVenue").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
               function (data) {
                   switch (data.flag) {
                       case 1: //// add                                          

                           _dataTables.venue.draw();
                           break;
                       case 2: //// update  

                           $("#venueDialog").dialog("close");
                           _dataTables.venue.draw();
                       
                           break;
                       default:;
                   }

                    $("#Active").attr('checked', false);
                    $("#Id").val(0);

                    iuHelper.resetForm("#form");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));   
               });
        });

        suftnet_Settings.ClearErrorMessages("#form");
    },  
    edit: function (obj) {

        var dataobject = _dataTables.venue.row($(obj).parents('tr')).data();
                
        $("#Active").attr("checked", dataobject.Active);
        $("#InternalVenue").attr("checked", dataobject.InternalVenue);
        $("#QueryString").val(dataobject.QueryId);
        $("#Phone").val(dataobject.Phone);
        $("#Email").val(dataobject.Email);
        $("#Company").val(dataobject.Company);
        $("#AddressId").val(dataobject.AddressId);
        $("#CompleteAddress").val(dataobject.CompleteAddress);
        $("#County").val(dataobject.County);
        $("#Town").val(dataobject.Town);
        $("#PostCode").val(dataobject.PostCode);
        $("#Country").val(dataobject.Country);
     
        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
        $("#venueDialog").dialog("open");
    },
    delete: function (obj) {

        var dataobject = _dataTables.venue.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblVenue").then(
                function (data) {
                    _dataTables.venue.row($(obj).parents('tr')).remove().draw();
                });
        }
    },
    pageInit: function () {

        $("#btnClose").on("click", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            $("#Id").val(0);

            iuHelper.resetForm("#form");

            $("#venueDialog").dialog("close");
        });

        $("#btnVenue").click(function () {

            $("#venueDialog").dialog("open");
        });

        $("#InternalVenue").bind("click", function () {
            if ($(this).is(':checked')) {

                $(".externalAddress").hide();

            } else {
                $(".externalAddress").show();
            }
        });

        $("#venueDialog").dialog({
            autoOpen: false, width: 790, height: 640, modal: false, title: 'Venue', show: { effect: 'fade', duration: 1000 },
            hide: { effect: 'fade', duration: 1000 }
        });

        venue.create();
        venue.load();
    },
    load: function () {
        _dataTables.venue = $('#tblVenue').DataTable({
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
                { "data": "Company", "defaultContent": "<i>-</i>" },
                { "data": "Email", "defaultContent": "<i>-</i>" },
                { "data": "Phone", "defaultContent": "<i>-</i>" },
                { "data": "FullAddress", "defaultContent": "<i>-</i>" },
                { "data": "Active", "defaultContent": "<i>-</i>" },             
                {
                    "data": null,
                    "orderable": false,
                    "defaultContent": '<a style=margin:10px; href="#" onclick=venue.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a><a style=margin:10px; href="#" onclick="venue.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4] },
                { className: "text-center", "targets": [5] }],
            destroy: true
        });
    }
}