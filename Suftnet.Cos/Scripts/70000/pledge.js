
var campaign = {

    create: function () {                      

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

                        _dataTables.campaign.draw();

                        break;
                    case 2: //// update  

                        _dataTables.campaign.draw();

                        $("#createPledgeDialog").dialog("close");

                        break;
                    default:;
                }

                $("#Id").val(0);
                $("#form").attr("action", $("#createUrl").attr("data-createUrl"));  
                iuHelper.resetForm("#form");
            });

        });

        suftnet_Settings.ClearErrorMessages("#form");       
    },
   
    edit: function (obj) {

        var dataobject = _dataTables.campaign.row($(obj).parents('tr')).data();

        $("#Title").val(dataobject.Title);
        $("#CreatedDT").val(dataobject.CreatedOn);
        $("#StatusId").val(dataobject.StatusId);
        $("#Note").val(dataobject.Note);
        $("#Expected").val(dataobject.Expected);
        $("#Donated").val(dataobject.Donated);
        $("#Donated").val(dataobject.Donated);
        $("#Remaining").val(dataobject.Remaining);
        $("#ImageUrl").val(dataobject.ImageUrl);  
        $("#QueryString").val(dataobject.QueryId);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
        $("#createPledgeDialog").dialog("open");
    },
    view: function (obj) {

        var dataobject = _dataTables.campaign.row($(obj).parents('tr')).data();
        window.location.href = $("#viewUrl").attr("data-viewUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.Title) + "/" + dataobject.QueryId;
    },
    delete: function (obj) {

        var dataobject = _dataTables.campaign.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblCampaign").then(
                function (data) {
                    _dataTables.campaign.row($(obj).parents('tr')).remove().draw();
                });
        }
    },
    pageInit: function () {

        $("#btnClose").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            $("#Id").val(0);

            suftnet_Settings.ClearErrorMessages("#form");
            $("#createPledgeDialog").dialog("close");
        });
        $("#btnOpen").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            suftnet_Settings.ClearErrorMessages("#form");
            $("#createPledgeDialog").dialog("open");
        });
        $("#CreatedDT").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        campaign.create();
        campaign.load();

        $("#createPledgeDialog").dialog({
            autoOpen: false, width: 720, height: 750, modal: false, title: 'Campaign', show: { effect: 'fade', duration: 1000 },
            hide: { effect: 'fade', duration: 1000 }
        });

        suftnet_upload.init($("#uploadUrl").attr("data-uploadUrl"));    
    },
    load: function () {
        _dataTables.campaign = $('#tblCampaign').DataTable({
            "serverSide": true,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 5,
            "pagingType": "full_numbers",
            "info": false,
            "ajax": {
                url: $("#loadUrl").attr("data-loadUrl"),
                type: 'POST'
            },
            "columns": [
                { "data": "CreatedOn", "defaultContent": "<i>-</i>" },
                { "data": "Title", "defaultContent": "<i>-</i>" },
                {
                    "data": "Expected", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                },
                {
                    "data": "Donated", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                },
                {
                    "data": "Remaning", render: function (data, type, row) {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                },     
                { "data": "Status", "defaultContent": "<i>-</i>" },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "width": "15%",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=campaign.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="campaign.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="campaign.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"View givers\" /></a>' 
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4,5] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4,5] }],
            destroy: true
        });
    }
}

