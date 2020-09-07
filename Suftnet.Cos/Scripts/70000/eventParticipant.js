
var participant = {

    create: function ()
    {
        $("#btnSubmit").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }
           
            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {

                        case 1: //// add                                          

                            _dataTables.participant.draw();
                            break;
                        case 2: //// update  
                                                        
                            break;

                        default: ;
                    }

                    $("#Id").val(0);
                    iuHelper.resetForm("#form");               
                });
            suftnet_Settings.ClearErrorMessages("#form");
        });
       
    },
    pageInit: function () {

        $("#btnOpen").click(function () {

            $("#participantDialog").dialog("open");
        });

        $("#btnClose").click(function () {

            $("#participantDialog").dialog("close");
        });

        $("#report").on("click", function (e) {

            e.preventDefault();

            $("#ifParticipan").attr("src", $("#reportUrl").attr("data-reportUrl") + "?ReportTypeId=5180&QueryString=" + $(this).attr("queryString"));
            $("#participantDialog").dialog("open");
        });

        $("#participantDialog").dialog({ autoOpen: false, width: 1030, height: 750, modal: false, title: 'Report' });

        participant.create();
        participant.load();
    },
    delete: function (obj) {

        var dataobject = _dataTables.participant.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblParticipant").then(
                function (data) {
                    _dataTables.participant.row($(obj).parents('tr')).remove().draw(true);
                });
        }

    },
    load: function () {

        _dataTables.participant = $('#tblParticipant').DataTable({
            "serverSide": true,
            "ordering": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pagingType": "full_numbers",
            "ajax": {
                "url": $("#loadUrl").attr("data-loadUrl"),
                "type": "GET",
                "datatype": "json"
            },
            columns: [
                { "data": "CreatedOn", "autowidth": true },
                { "data": "FirstName", "autowidth": true },
                { "data": "LastName", "autowidth": true },
                { "data": "Email", "autowidth": true },
                { "data": "Phone", "autowidth": true },
                {
                    "data": null,
                    "orderable": false,
                    "defaultContent": '<a style=margin:10px; href="#" onclick="participant.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3,4] },
                { className: "text-left", "targets": [0, 1, 2, 3,4] },
                { className: "text-center", "targets": [5] }],
            destroy: true
        });
    }    

}