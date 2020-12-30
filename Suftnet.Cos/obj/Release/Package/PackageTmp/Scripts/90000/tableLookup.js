var _dataTables = {};

var tables = {

    select: function (obj) {

        var dataobject = _dataTables.tables.row($(obj).parents('tr')).data();  
                      
        var $option = $("<option />");
        $option.attr("value", dataobject.Id).text(dataobject.Number);
        $("#TableId").empty();
        $("#TableId").append($option);

        $("#TableNumber").val(dataobject.Number);                  
        $("#tableDialog").dialog("close");     
    },
    pageInit: function () {

        $(document).on("click", "#TableId", function (event) {

            event.preventDefault();
            tables.load();

            $("#tableDialog").dialog("open");
        });

        $("#btnClose").click(function () {
            $("#tableDialog").dialog("close");
        });

        $("#tableDialog").dialog({ autoOpen: false, width: 500, height: 600, modal: false, title: 'Table Finder' });    
    },
    load: function ()
    {          
        _dataTables.tables = $('#tblTables').DataTable({
            "serverSide": false,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 5,
            "pagingType": "full_numbers",
            "ajax": {
                url: $("#loadTableUrl").attr("data-loadTableUrl"),
                "type": "GET",
                "datatype": "json"
            },
            "columns": [        
                { "data": "Number", "defaultContent": "<i>-</i>"  },
                { "data": "Size", "defaultContent": "<i>-</i>"  },                 
                { "data": "Active", "defaultContent": "<i>-</i>"  },  
                {
                    "data": null,
                    "orderable": false,           
                    "defaultContent": '<a class="etip" title="Select this Table" style="margin:10px;" href="#" onclick=tables.select(this)><img src=' + suftnet_grid.iconUrl + 'add.png\ alt=\"Select this Table\" /></a>'
                }
            ],            
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2] },
                { className: "text-left", "targets": [0, 1, 2] }],
            destroy: true
           });

        _dataTables.tables.on("draw", function () {
                       
            $('.etip').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}