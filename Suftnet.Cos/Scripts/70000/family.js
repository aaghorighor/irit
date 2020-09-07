
family = {

    step : function()
    {
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
                $("#Mobile").addClass("personnelrequired");
            } else {

                $("#Mobile").removeClass("personnelrequired").removeClass("error");
            }
        });

        $(document).on("change", "#IsSms", function () {

            if ($(this).is(':checked')) {

                if ($("#Mobile").val() == "") {
                    $("#Mobile").addClass("personnelrequired");
                }
            } else {
                $("#Mobile").removeClass("personnelrequired").removeClass("error");
            }
        });

        $(document).on("blur", "#Email", function () {

            if ($(this).val() != "") {
                $("#Email").addClass("personnelrequired").addClass("emailrequired");
            } else {

                $("#Email").removeClass("personnelrequired").removeClass("emailrequired").removeClass("error");
            }
        });

        $(document).on("change", "#IsEmail", function () {

            if ($(this).is(':checked')) {

                if ($("#Email").val() == "") {
                    $("#Email").addClass("personnelrequired").addClass("emailrequired");
                }
            } else {
                $("#Email").removeClass("personnelrequired").removeClass("emailrequired").removeClass("error");
            }
        });
               
        $("#btnSavePersonnel").click(function (e) {

            e.preventDefault();

            var isErrorFree = true;

            $('input.personnelrequired, textarea.personnelrequired, select.personnelrequired').each(function () {
                if (suftnet_validation.IsValid(this) == false) {
                    isErrorFree = false;
                };
            });

            $('input.daterequired').each(function () {
                if (suftnet_validation.IsDate(this, suftnet_Settings.dateTimeFormat) == false) {
                    isErrorFree = false;
                };
            });

            if (isErrorFree) {
                $("#MainCollapsibleLeft").accordion("activate", 1);
            }

        });
    },

    create: function () {

        $("#btnSaveChanges").bind("click", function (event) {

            event.preventDefault();

            var isErrorFree = true;

            $('input.personnelrequired, textarea.personnelrequired, select.personnelrequired').each(function () {
                if (suftnet_validation.IsValid(this) == false) {
                    isErrorFree = false;
                    $("#MainCollapsibleLeft").accordion("activate", 0);
                };
            });

            $('input.addressrequired, textarea.addressrequired, select.addressrequired').each(function () {
                if (suftnet_validation.IsValid(this) == false) {
                    isErrorFree = false;
                    $("#MainCollapsibleLeft").accordion("activate", 1);
                };
            });

            $('input.emailrequired').each(function () {
                if (suftnet_validation.IsEmail(this) == false) {
                    isErrorFree = false;
                };
            });

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

            if (isErrorFree) {

                js.ajaxPost($("#member-form").attr("action"), $("#member-form").serialize()).then(
                 function (data) {
                     switch (data.flag) {
                         case 1: //// add                                          

                             suftnet_grid.addFamily(data.objrow);

                             break;
                         case 2: //// update                               

                             suftnet_grid.updateFamily(data.objrow);

                             suftnet.Tab(1);

                             break;
                         default:;
                     }
                  
                     $("#IsEmail").attr('checked', false);
                     $("#IsSms").attr('checked', false);

                     $("#Id").val(0);

                     $("#TempUrl").attr('src', "/Content/Photo/Blank.jpg");

                     iuHelper.resetForm("#member-form");

                     $("#MainCollapsibleLeft").accordion("activate", 0);
                 });
            }
        });

        $("#btnPrevious").click(function () {

            $("#MainCollapsibleLeft").accordion("activate", 0);
        });
    },

    load: function () {

        $(document).on("click", "#Delete", function (e) {
            e.preventDefault();

            js.confirm($("#deleteUrl").attr("data-deleteUrl"), { Id: $(this).attr("DeleteId") }, $(this).attr("DeleteId"), "#tdfamily").then(
            function (data) {

            });
        });

        $(document).on("click", "#EditLink", function (e) {

            e.preventDefault();

            js.ajaxGet($("#getUrl").attr("data-getUrl"), { Id: $(this).attr("EditId") }).then(
             function (data) {

                 var dataobject = data.dataobject;

                 $("#TitleId").val(dataobject.TitleId);
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

                 $("#BaptizeId").val(dataobject.BaptizeId);
                 $("#MaritalStatusId").val(dataobject.MaritalStatusId);
                 $("#MemberTypeId").val(dataobject.MemberTypeId);
                 $("#ProfessionId").val(dataobject.ProfessionId);
                 $("#StatusId").val(dataobject.StatusId);

                 $("#JoinDate").val(dataobject.MembershipDT);

                 $("#AddressId").val(dataobject.AddressId);
                 $("#AddressLine1").val(dataobject.AddressLine1);
                 $("#AddressLine2").val(dataobject.AddressLine2);
                 $("#AddressLine3").val(dataobject.AddressLine3);

                 $("#PostCode").val(dataobject.PostCode);
                 $("#Country").val(dataobject.Country);

                 var imageUrl = "/Content/Photo/Member/216X196/";

                 if (dataobject.FileName != null) {
                     $("#FileName").val(dataobject.FileName);
                     $("#TempUrl").attr('src', imageUrl + dataobject.FileName);
                 }

                 suftnet.Tab(0);

             });
        });

        suftnet_Settings.TableInit('#tdfamily', [0, 1, 2, 3, 4, 5, 6]);
    }

}