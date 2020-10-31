var _dataTables = {};

var user = {

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

            if ($('#ChangePassword').is(':checked')) {
                $("#ChangePassword").val(true);
            } else {
                $("#ChangePassword").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                  function (data) {
                      switch (data.flag) {
                          case 1: //// add

                              _dataTables.user.draw();
                              break;
                          case 2: //// update
                            
                              _dataTables.user.draw();
                              suftnet.tab(1);

                              break;
                          default:;
                      }

                    $("#Id").val("");                 
                    $("#Active").attr("checked", false);
                    $("#ChangePassword").attr("checked", false);
                    $("#__changePassword").addClass("hide");
                    $("#Password").removeClass("validate[required],maxSize[20],minSize[6]]");               
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));

                    iuHelper.resetForm("#form");
                  });
        });

        $("#__changePassword").addClass("hide");
        $("#form").attr("action", $("#createUrl").attr("data-createUrl"));

        suftnet_Settings.ClearErrorMessages("#form");
    },
   
    edit: function (obj) {

        var dataobject = _dataTables.user.row($(obj).parents('tr')).data();

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

            }).attr("src", $("#imageUrl").attr("data-imageUrl") + dataobject.ImageUrl);

        $("#Active").attr("checked", dataobject.Active);
        $("#Id").val(dataobject.UserId);
        $("#__changePassword").removeClass("hide");
        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

        suftnet.tab(0);
    },
    delete: function (obj) {

        var dataobject = _dataTables.user.row($(obj).parents('tr')).data();

        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.UserId }, dataobject.UserId, "#tblUser").then(
                function (data) {
                    _dataTables.user.row($(obj).parents('tr')).remove().draw();
                });
        }

    },
    view: function (obj) {

        var dataobject = _dataTables.user.row($(obj).parents('tr')).data();
        window.location.href = $("#permissionUrl").attr("data-permissionUrl") + "/" + Suftnet_Utility.seoUrl(dataobject.FullName) + "/" + dataobject.UserId;
    },    
    pageInit: function () {

        suftnet_upload.init($("#uploadUrl").attr("data-uploadUrl"));

        $(document).on("change", "#ChangePassword", function (e) {

            if ($(this).is(":checked")) {
                $("#Password").addClass("validate[required],maxSize[20],minSize[6]]");
            } else {
                $("#Password").removeClass("validate[required],maxSize[20],minSize[6]]");
            }
        });

        $(document).on("click", "#btnClose", function (event) {
            iuHelper.resetForm("#form");
            $("#__changePassword").addClass("hide");
            $("#ChangePassword").attr("checked", false);
            $("#form").attr("action", $("#createUrl").attr("data-createUrl"));
            suftnet.tab(1);
        });
      
        user.create();
        user.load();
    },
    load: function () {
           _dataTables.user = $('#tblUser').DataTable({
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
                    "defaultContent": '<a style=margin:10px; href="#" onclick=user.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a>'+
                        '<a style=margin:10px; href="#" onclick="user.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>' +
                        '<a style=margin:10px; href="#" onclick="user.view(this)"><img src=' + suftnet_grid.iconUrl + 'folder.png\ alt=\"Give userPermission\" /></a>'                     
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