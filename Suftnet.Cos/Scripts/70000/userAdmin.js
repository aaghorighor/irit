var _dataTables = {};

var userAdmin = {

    create: function () {       

        $("#btnSaveChanges").click(function (e) {

            e.preventDefault();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Active').is(':checked')) {
                $("#Active").val(true);
            } else {
                $("#Active").val(false);
            }
        
            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                  function (data) {
                      switch (data.flag) {
                          case 1: //// add
                              _dataTables.userAdmin.draw();
                              break;
                          case 2: //// update
                            
                              _dataTables.userAdmin.draw();

                              $("#MainCollapsible").accordion({ collapsible: true });
                              $("#MainCollapsible").accordion("activate", 1);

                              break;
                          default:;
                      }

                    $("#UserId").val("");                 
                    $("#Active").attr("checked", false);  
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));
                    iuHelper.resetForm("#form");
                  });
        });

        $("#form").attr("action", $("#createUrl").attr("data-createUrl"));
        suftnet_Settings.ClearErrorMessages("#form");
    },
   
    edit: function (obj) {

        var dataobject = _dataTables.userAdmin.row($(obj).parents('tr')).data();

        $("#FirstName").val(dataobject.FirstName);
        $("#LastName").val(dataobject.LastName);
        $("#Email").val(dataobject.Email);
        $("#AreaId").val(dataobject.AreaId);

        if (dataobject.ImageUrl != null) {
            $("#ImageUrl").val(dataobject.ImageUrl);
            $("#TempUrl").attr('src', $("#imageUrl").attr("data-imageUrl") + dataobject.ImageUrl);
        }

        $("#Active").attr("checked", dataobject.Active);
        $("#UserId").val(dataobject.UserId);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));
          
        $("#MainCollapsible").accordion({ collapsible: true });
        $("#MainCollapsible").accordion("activate", 0);                    

    },
    delete: function (obj) {

        var dataobject = _dataTables.userAdmin.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.UserId }, dataobject.UserId, "#tblUser").then(
                function (data) {
                    _dataTables.userAdmin.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    view: function (obj) {

        var dataobject = _dataTables.userAdmin.row($(obj).parents('tr')).data();
        window.location.href = $("#permissionUrl").attr("data-permissionUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.FullName) + "/" + dataobject.UserId;
    },
    resetPassword: function (obj) {

        var dataobject = _dataTables.userAdmin.row($(obj).parents('tr')).data();

        iuHelper.resetForm("#formResetPassword");

        $("#Id").val(dataobject.UserId);
        $("#resetPasswordDialog").dialog("open");
    },
    pageInit: function () {

        $("#btnSaveResetPassword").bind("click", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("formResetPassword")) {
                return false;
            }

            js.ajaxPost($("#formResetPassword").attr("action"), $("#formResetPassword").serialize()).then(
                function (data) {
               
                    setTimeout("showSuccess('Success',5000);", 1000);      
                }                  
            );

            iuHelper.resetForm("#formResetPassword");
            $("#resetPasswordDialog").dialog("close");
        });

        $("#btnClose").bind("click", function (event) {

            iuHelper.resetForm("#form");
            $("#MainCollapsible").accordion({ collapsible: true });
            $("#MainCollapsible").accordion("activate", 1);
        });               

        $("#btnCloseResetPassword").bind("click", function (event) {
            iuHelper.resetForm("#formResetPassword");  
            $("#resetPasswordDialog").dialog("close");
        });

        $("#resetPasswordDialog").dialog({
            autoOpen: false, width: 350, height: 300, modal: false, title: 'Reset Password'});
              
        userAdmin.create();
        userAdmin.load();

    },
    tenantPageInit: function () {

        $("#btnSaveResetPassword").bind("click", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("formResetPassword")) {
                return false;
            }

            js.ajaxPost($("#formResetPassword").attr("action"), $("#formResetPassword").serialize()).then(
                function (data) {

                    setTimeout("showSuccess('Success',5000);", 1000);
                }
            );

            iuHelper.resetForm("#formResetPassword");
            $("#resetPasswordDialog").dialog("close");
        });

        $("#btnClose").bind("click", function (event) {

            iuHelper.resetForm("#form");
            $("#MainCollapsible").accordion({ collapsible: true });
            $("#MainCollapsible").accordion("activate", 1);
        });

        $("#btnCloseResetPassword").bind("click", function (event) {
            iuHelper.resetForm("#formResetPassword");
            $("#resetPasswordDialog").dialog("close");
        });

        $("#resetPasswordDialog").dialog({
            autoOpen: false, width: 350, height: 300, modal: false, title: 'Reset Password'
        });

        userAdmin.create();
        userAdmin.loadTenant();

    },
    load: function () {
        _dataTables.userAdmin = $('#tblUser').DataTable({
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
                { "data": "FirstName", "defaultContent": "<i>-</i>" },
                { "data": "LastName", "defaultContent": "<i>-</i>" },
                { "data": "Email", "defaultContent": "<i>-</i>" },
                { "data": "Area", "defaultContent": "<i>-</i>" },
                { "data": "Active", "defaultContent": "<i>-</i>" },              
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=userAdmin.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>'+
                        '<a style=margin:10px; href="#" onclick="userAdmin.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="userAdmin.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"Give userPermission\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="userAdmin.resetPassword(this)"><img src=' + suftnet_grid.iconUrl + 'user.png\ alt=\"Reset User Password\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4] }],
            destroy: true
        });
    },
    loadTenant: function () {
        _dataTables.userAdmin = $('#tblUser').DataTable({
            "serverSide": true,
            "searching": true,
            "lengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "pageLength": 5,
            "pagingType": "full_numbers",
            "ajax": {
                "url": $("#loadTenantUrl").attr("data-loadTenantUrl"),
                "type": "GET",
                "datatype": "json"   
            },
            "columns": [
                { "data": "FirstName", "defaultContent": "<i>-</i>" },
                { "data": "LastName", "defaultContent": "<i>-</i>" },
                { "data": "Email", "defaultContent": "<i>-</i>" },
                { "data": "Area", "defaultContent": "<i>-</i>" },
                { "data": "Active", "defaultContent": "<i>-</i>" },
                {
                    "data": null,
                    "orderable": false,
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=userAdmin.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="userAdmin.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="userAdmin.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"Give userPermission\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="userAdmin.resetPassword(this)"><img src=' + suftnet_grid.iconUrl + 'user.png\ alt=\"Reset User Password\" /></a>'
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