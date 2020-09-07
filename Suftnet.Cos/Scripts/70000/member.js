var _dataTables = {};

var member = {
   
    create : function()
    {       
        $("#btnSaveChanges").bind("click", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }          
          
            if ($('#IsEmail').is(':checked')) {
                               
                $("#IsEmail").val(true);
            } else {
                $("#IsEmail").val(false);
            }

            if ($('#IsSms').is(':checked')) {
                $("#IsSms").val(true);
            } else {
                $("#IsSms").val(false);
            }

            if ($('#CreateUser').is(':checked')) {
                $("#CreateUser").val(true);
            } else {
                $("#CreateUser").val(false);
            }  
          
            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {
                        case 1: //// add                                          

                            _dataTables.member.draw();
                            break;
                        case 2: //// update 
                            _dataTables.member.draw();                      

                            break;
                        default: ;
                    }

                    $("#IsEmail").attr('checked', false);
                    $("#IsSms").attr('checked', false);

                    $("#Id").val(0);

                    $("#TempUrl").attr('src', "/Content/Photo/Blank.jpg");
                    $("#form").attr("action", $("#createUrl").attr("data-createUrl"));

                    iuHelper.resetForm("#form");
                                    
                    if (data.flag == 2) {
                        suftnet.tab(1);  
                    }                   
            });
        });

        suftnet_Settings.ClearErrorMessages("#form");       
    },
    edit: function (obj)
    {
        var dataobject = _dataTables.member.row($(obj).parents('tr')).data();

        $("#Id").val(dataobject.Id);
        $("#GenderId").val(dataobject.GenderId);

        $("#FirstName").val(dataobject.FirstName);
        $("#LastName").val(dataobject.LastName);

        $("#Email").val(dataobject.Email);
        $("#Mobile").val(dataobject.Mobile);

        if (dataobject.DateOfBirthDT != null) {
            $("#DateOfBirth").val(dataobject.DateOfBirthDT);
        }

        $("#IsEmail").attr("checked", dataobject.IsEmail);
        $("#IsSms").attr("checked", dataobject.IsSms);

        $("#MemberTypeId").val(dataobject.MemberTypeId);
        $("#StatusId").val(dataobject.StatusId);
        $("#QueryId").val(dataobject.QueryId);
        $("#JoinDate").val(dataobject.MembershipDT);

        $("#AddressId").val(dataobject.AddressId);
        $("#AddressLine1").val(dataobject.AddressLine1);
        $("#AddressLine2").val(dataobject.AddressLine2);
        $("#AddressLine3").val(dataobject.AddressLine3);

        $("#PostCode").val(dataobject.PostCode);
        $("#Country").val(dataobject.Country);

        var imageUrl = "/Content/Photo/Member/216X196/";

        if (dataobject.FileName != null && dataobject.FileName.length != 0) {
            $("#FileName").val(dataobject.FileName);
            $("#TempUrl").attr('src', imageUrl + dataobject.FileName);
        };
       
        $("#form").attr("action", $("#editUrl").attr("data-editUrl"));

        suftnet.tab(0);       
    },   
    delete: function (obj) {
                
        var dataobject = _dataTables.member.row($(obj).parents('tr')).data();
                                          
        if (dataobject != null) {
            js.dyconfirm($("#deleteUrl").attr("data-deleteUrl"), { Id: dataobject.QueryId }, dataobject.QueryId, "#tblMember").then(
                function (data) {                                    
                    _dataTables.member.row($(obj).parents('tr')).remove().draw();                    
                });
        }       

    },
    pageInit: function () {

        $(".forms-date-txt").mask("99/99/9999");

        $("#JoinDate").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        $(document).on("blur", "#Mobile", function () {

            if ($(this).val() != "") {
                $("#Mobile").addClass("validate[required]");
            } else {

                $("#Mobile").removeClass("validate[required]");
            }
        });

        $(document).on("change", "#IsSms", function () {

            if ($(this).is(':checked')) {

                if ($("#Mobile").val() == "") {
                    $("#Mobile").addClass("validate[required]");
                }
            } else {
                $("#Mobile").removeClass("validate[required]");
            }
        });

        $(document).on("blur", "#Email", function () {

            if ($(this).val() != "") {
                $("#Email").addClass("validate[required]").addClass("validate[custom[email]]");
            } else {

                $("#Email").removeClass("validate[required]").removeClass("validate[custom[email]]");
            }
        });

        $(document).on("change", "#IsEmail", function () {

            if ($(this).is(':checked')) {

                if ($("#Email").val() == "") {
                    $("#Email").addClass("validate[required]").addClass("validate[custom[email]]");
                }
            } else {
                $("#Email").removeClass("validate[required]").removeClass("validate[custom[email]]");
            }
        });

        $(document).on("click", "#btnClose", function () {

            $("#IsEmail").attr('checked', false);
            $("#IsSms").attr('checked', false);

            $("#Id").val(0);

            $("#TempUrl").attr('src', "/Content/Photo/Blank.jpg");

            iuHelper.resetForm("#form");
            suftnet_Settings.ClearErrorMessages("#form"); 
            suftnet.tab(1);  
        });  

        $(document).on("click", "#EditLink", function () {
            window.location.href = $("#editMemberUrl").attr("data-editMemberUrl") + "/" + $(this).attr("EditId");
        });  

        $("#form").attr("action", $("#createUrl").attr("data-createUrl"));

        member.create();
        member.load();

    },
    load: function ()
    {          
           _dataTables.member = $('#tblMember').DataTable({
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
                { "data": "Id", "visible": false, "defaultContent": "<i>-</i>" },
                { "data": "FirstName", "defaultContent": "<i>-</i>"  },
                { "data": "LastName", "defaultContent": "<i>-</i>"  },
                { "data": "Email", "defaultContent": "<i>-</i>"  },
                { "data": "Mobile", "defaultContent": "<i>-</i>"  },    
                { "data": "Status", "defaultContent": "<i>-</i>"  },  
                {
                    "data": null,
                    "orderable": false,    
                    className: "align-center",
                    "defaultContent": '<a style=margin:10px; href="#" onclick=member.edit(this)><img src=' + suftnet_grid.iconUrl + 'edit.png\ alt=\"Edit this row\" /></a><a style=margin:10px; href="#" onclick="member.delete(this)"><img src=' + suftnet_grid.iconUrl + 'delete.png\ alt=\"Delete this row\" /></a>'
                }
            ],            
            columnDefs: [
            { "targets":[] , "visible": false, "searchable": false },
            { "orderable": false, "targets": [0, 1, 2, 3, 4, 5] },
            { className: "text-left", "targets": [0,1,2,3,4,5] }],    
            destroy: true
        });
    }
}