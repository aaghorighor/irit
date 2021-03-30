var _dataTables = {};

var userAdmin = {

    create: function () {

        $(document).on("click", "#btnSaveChanges", function (e) {

            e.preventDefault();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Active').is(':checked')) {
                $("#Active").val(true);
            } else {
                $("#Active").val(false);
            }

            if ($('#ChangePassword').is(':checked')) {
                $("#ChangePassword").val(true);
            } else {
                $("#ChangePassword").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add
                            _dataTables.userAdmin.ajax.reload();
                            break;
                        case 2: //// update

                            _dataTables.userAdmin.ajax.reload();

                            suftnet.tab(1);

                            break;
                        default: ;
                    }

                    $("#UserId").val("");
                    $("#Active").attr("checked", false);
                    $("#ChangePassword").attr("checked", false);
                    $("#Password").removeClass("validate[required],maxSize[20],minSize[6]]");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));

                    iuHelper.resetForm("#form");
                });
        });
    },
    adminCreate: function () {

        $(document).on("click", "#btnSubmit", function (e) {

            e.preventDefault();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Active').is(':checked')) {
                $("#Active").val(true);
            } else {
                $("#Active").val(false);
            }

            if ($('#ChangePassword').is(':checked')) {
                $("#ChangePassword").val(true);
            } else {
                $("#ChangePassword").val(false);
            }         

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add
                            _dataTables.userAdmin.ajax.reload();
                            break;
                        case 2: //// update

                            _dataTables.userAdmin.ajax.reload();

                            suftnet.tab(1);

                            break;
                        default: ;
                    }

                    $("#UserId").val("");
                    $("#Active").attr("checked", false);
                    $("#ChangePassword").attr("checked", false);
                    $("#Password").removeClass("validate[required],maxSize[20],minSize[6]]");

                    iuHelper.resetForm("#form");
                });

            $("#form").attr("action", $("#createUrl").attr("data-createUrl"));
        });
    },

    edit: function (obj) {

        var dataobject = _dataTables.userAdmin.row($(obj).parents('tr')).data();

        $("#FirstName").val(dataobject.FirstName);
        $("#LastName").val(dataobject.LastName);
        $("#Email").val(dataobject.Email);
        $("#AreaId").val(dataobject.AreaId);

        $("#profileImage")
            .on('load', function () {
                $("#ImageUrl").val(dataobject.ImageUrl);
            })
            .on('error', function () {
                $("#ImageUrl").val("");
                $("#profileImage").attr('src', $("#defaultImageUrl").attr("data-defaultImageUrl"));

            }).attr("src", $("#profileImageUrl").attr("data-profileImageUrl") + dataobject.ImageUrl);

        $("#Active").attr("checked", dataobject.Active);
        $("#UserId").val(dataobject.UserId);

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

        suftnet.tab(0);

    },
    delete: function (obj) {

        var dataobject = _dataTables.userAdmin.row($(obj).parents('tr')).data();

        js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.UserId }, dataobject.UserId, "#tblUser").then(
            function (data) {
                _dataTables.userAdmin.row($(obj).parents('tr')).remove().draw();
            });

    },
    pageInit: function () {

        $(document).on("change", "#ChangePassword", function (e) {

            if ($(this).is(":checked")) {
                $("#Password").addClass("validate[required],maxSize[20],minSize[6]]");
            } else {
                $("#Password").removeClass("validate[required],maxSize[20],minSize[6]]");
            }
        });

        $("#btnClose").bind("click", function (event) {

            iuHelper.resetForm("#form");
            suftnet.tab(1);
        });

        userAdmin.adminCreate();
        userAdmin.load();

        $("#form").attr("action", $("#createUrl").attr("data-createUrl"));

    },
    view: function (obj) {

        var dataobject = _dataTables.userAdmin.row($(obj).parents('tr')).data();
        window.location.href = $("#permissionUrl").attr("data-permissionUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.FullName) + "/" + dataobject.UserId;
    },
    tenantPageInit: function () {

        suftnet.tab(0);

        $(document).on("change", "#ChangePassword", function (e) {

            if ($(this).is(":checked")) {
                $("#Password").addClass("validate[required],maxSize[20],minSize[6]]");
            } else {
                $("#Password").removeClass("validate[required],maxSize[20],minSize[6]]");
            }
        });

        $(document).on("click", "#btnClose", function (event) {
            iuHelper.resetForm("#form");
            suftnet.tab(1);
        });

        userAdmin.adminCreate();
        userAdmin.loadTenant();

        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

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
                    "defaultContent": '<a class="tooltip" title="Edit this row" style=margin:10px; href="#" onclick=userAdmin.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a class="tooltip" title="Delete this row" style=margin:10px; href="#" onclick="userAdmin.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4] }],
            destroy: true
        });

        _dataTables.userAdmin.on("draw", function () {
            $('.tooltip').tipsy({ fade: true, gravity: 'e', live: true });
        });
    },
    loadTenant: function () {

        _dataTables.userAdmin = $('#tblUser').DataTable({
            "serverSide": false,
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
                    "defaultContent": '<a class="tooltip" title="Edit this row" style=margin:10px; href="#" onclick=userAdmin.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>' +
                        '<a class="tooltip" title="Delete this row" style=margin:10px; href="#" onclick="userAdmin.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a class="tooltip" title="Add Permissions to this User" style=margin:10px; href="#" onclick="userAdmin.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"Add Permission  to this User \" /></a>'
                }
            ],
            columnDefs: [
                { "targets": [], "visible": false, "searchable": false },
                { "orderable": false, "targets": [0, 1, 2, 3, 4] },
                { className: "text-left", "targets": [0, 1, 2, 3, 4] }],
            destroy: true
        });

        _dataTables.userAdmin.on("draw", function () {
            $('.tooltip').tipsy({ fade: true, gravity: 'e', live: true });
        });
    }
}