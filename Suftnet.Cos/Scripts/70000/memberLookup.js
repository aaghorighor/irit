var _dataTables = {};

var memberLookup = {

    view: function (obj) {

        var dataobject = _dataTables.memberLookup.row($(obj).parents('tr')).data();
        var names = dataobject.FirstName + " " + dataobject.LastName;
              
        $("#MemberExternalId").val(dataobject.QueryId);
        $("#Member").val(names);
        $("#MemberReference").val(names);

        console.log($("#MemberExternalId").val());

        $("#memberDialog").dialog("close");
    },
    pageInit: function () {

        $("#btnMemberFinder").bind("click", function () {

            memberLookup.load();

            $("#memberDialog").dialog("open");
        });

        $("#btnClose").click(function () {

            $("#memberDialog").dialog("close");
        });

        $("#memberDialog").dialog({ autoOpen: false, width: 1050, height: 600, modal: false, title: 'Member Finder' });    
    },
    load: function ()
    {          
           _dataTables.memberLookup = $('#tblMemberLookup').DataTable({
            "serverSide": true,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 5,
            "pagingType": "full_numbers",
            "ajax": {
                url: $("#loadMemberUrl").attr("data-loadMemberUrl"),
                type: 'POST'
            },
            "columns": [        
                { "data": "FirstName", "defaultContent": "<i>-</i>"  },
                { "data": "LastName", "defaultContent": "<i>-</i>"  },
                { "data": "Email", "defaultContent": "<i>-</i>"  },
                { "data": "Mobile", "defaultContent": "<i>-</i>"  },    
                { "data": "Status", "defaultContent": "<i>-</i>"  },  
                {
                    "data": null,
                    "orderable": false,           
                    "defaultContent": '<a style=margin:10px; href="#" onclick=memberLookup.view(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"View this row\" /></a>'
                }
            ],            
            columnDefs: [
            { "targets":[] , "visible": false, "searchable": false },
            { "orderable": false, "targets": [0, 1, 2, 3, 4] },
            { className: "text-left", "targets": [0,1,2,3,4] },
            { className: "text-center", "targets": [4] } ],    
            destroy: true
        });
    }
}