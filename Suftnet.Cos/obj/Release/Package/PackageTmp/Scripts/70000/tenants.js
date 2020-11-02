var tenants = {
   
    view: function (obj) {

        var dataobject = _dataTables.tenants.row($(obj).parents('tr')).data();

        console.log(dataobject);

        window.location.href = $("#viewUrl").attr("data-viewUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.Name) + "/" + dataobject.Id;
    },
    user: function (obj) {

        var dataobject = _dataTables.tenants.row($(obj).parents('tr')).data();
        window.location.href = $("#userUrl").attr("data-userUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.Name) + "/" + dataobject.Id;
    },
    pageInit: function () {
       
        tenants.load();
    },
    load: function () {
        _dataTables.tenants = $('#tblTenant').DataTable({
            "serverSide": true,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 10,
            "pagingType": "full_numbers",
            "ajax": {
                url: $("#loadUrl").attr("data-loadUrl"),
                type: 'POST'
            },
            "columns": [
                { "data": "CreatedOn", "defaultContent": "<i>-</i>" },
                { "data": "ExpireDate", "defaultContent": "<i>-</i>" },
                { "data": "CustomerStripeId", "defaultContent": "<i>-</i>" },
                { "data": "Name", "defaultContent": "<i>-</i>" },
                { "data": "Email", "defaultContent": "<i>-</i>" },
                { "data": "Mobile", "defaultContent": "<i>-</i>" },              
                { "data": "Status", "defaultContent": "<i>-</i>" },                
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a title="View this row" style="margin:10px;" href="#" onclick=tenants.view(this)><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"View this row\" /></a>' +
                        '<a title="View Use in this row" style="margin:10px;" href="#" onclick="tenants.user(this)"><img src=' + suftnet_grid.iconUrl + 'user.png\ alt=\"View Use in this row\" /></a>'                       
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3,4,5,6,7] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4, 5, 6, 7]}],
            destroy: true
        });

        _dataTables.tenants.on("draw", function () {
            $('a').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}