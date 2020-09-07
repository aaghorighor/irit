var giving = {

    create : function()
    {      
        $("#btnSaveChanges").bind("click", function (event) {

            event.preventDefault();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#GiftAid').is(':checked')) {
                $("#GiftAid").val(true);
            } else {
                $("#GiftAid").val(false);
            }
           
            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add                                          

                            _dataTables.giving.draw();
                            break;
                        case 2: //// update  

                            _dataTables.giving.draw();
                            break;
                        default:;
                    }

                    $("#Id").val(0);
                    $("#GiftAid").attr('checked', false);
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));   
                    iuHelper.resetForm("#form");

                });            
        });

        suftnet_Settings.ClearErrorMessages("#form");
    },
   
    edit: function (obj) {

        var dataobject = _dataTables.giving.row($(obj).parents('tr')).data();

        $("#IncomeTypeId").val(dataobject.IncomeTypeId);
        $("#Amount").val(dataobject.Amount);
        $("#Note").val(dataobject.Note);
        $("#Id").val(dataobject.Id);
        $("#GiftAid").attr('checked', dataobject.GiftAid)
        $("#MemberReference").val(dataobject.MemberReference);
        $("#CreatedDT").val(dataobject.CreatedOn);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

    },
    delete: function (obj) {

        var dataobject = _dataTables.giving.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblGiving").then(
                function (data) {
                    _dataTables.giving.row($(obj).parents('tr')).remove().draw();
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
              
        giving.create();
        giving.load();
    },
    load: function () {
           _dataTables.giving = $('#tblGiving').DataTable({
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
                { "data": "StripeReference", "defaultContent": "<i>-</i>" },
                { "data": "MemberReference", "defaultContent": "<i>-</i>" },
                { "data": "IncomeType", "defaultContent": "<i>-</i > " },
                { "data": "GiftAid", "defaultContent": "<i>-</i > " },
                {
                    "data": "Amount", render: function (data, type, row)
                    {
                        return suftnet_grid.formatCurrency(data);

                    }, "defaultContent": "<i>-</i > "
                },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=giving.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="giving.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'                
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2,3,4,5] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5] }],
            destroy: true
        });
    }
}