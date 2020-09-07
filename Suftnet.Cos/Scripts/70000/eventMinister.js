
var eventMinister = {

    create: function () {
        $("#__btnOpen").click(function () {         

            $("#__createMinisterDialog").dialog("open");
        });

        $("#__btnClose").click(function () {

            $("#__createMinisterDialog").dialog("close");
        });

        $("#__btnSubmit").bind("click", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            var params =
             {
                 FirstName: $("#__FirstName").val(),
                 LastName: $("#__LastName").val(),
                 Email: $("#__Email").val(),
                 Phone: $("#__Phone").val(),               
                 RoleTypeId: $("#__RoleTypeId").val(),
                 Note: $("#__Note").val(),
                 Title: $("#__Title").val(),
                 EventId: $("#__EventId").val(),
                 memberId: $("#__memberId").text()
             }

            js.ajaxPost($("#form").attr("action"), params).then(
            function (data) {
                suftnet_grid.addEventMinister(data.objrow);
                iuHelper.resetForm("#form");
            });

            $("#__memberId").text("");
        });

        $("#__report").on("click", function (e) {

            e.preventDefault();

            if ($("#__EventId").val() > 0) {

                $("#ifInvoice").attr("src", $("#reportUrl").attr("data-reportUrl") + "?ReportTypeId=5181&EventId=" + $("#__EventId").val() + "&CurrencySymbol=" + $("#Currency").val());
                $("#InvoiceDialog").dialog("open");
            }
        });
    },
    load: function ()
    {      
        js.ajaxGet($("#getAllMinisterUrl").attr("data-getAllMinisterUrl")).then(
        function (data) {
                     
            suftnet_grid.refreshEventMinister(data.dataobject);
        });      

        $(document).on("click", "#Delete", function (e) {

            e.preventDefault();

            js.confirm($("#deleteUrl").attr("data-deleteUrl"), { Id: $(this).attr("DeleteId") }, $(this).attr("DeleteId"), "#__tdMinister").then(
             function (data) {

             });
        });

        suftnet_Settings.TableInit('#__tdMinister', [0, 3, 4, 5]);
    },
    lookup: function ()
    {
        $("#__btnload").click(function () {

            $("#__loadMinisterDialog").dialog("open");
        });

        $(document).on("click", "#selectMinister", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            var param =
             {
                 Id: $(this).attr("ministerId")
             }

            js.ajaxGet($("#getMinisterUrl").attr("data-getMinisterUrl"), param).then(
                  function (data) {

                      create(data);
                  });
        });

        var create = function (data) {

            $("#__FirstName").val(data.dataobject.FirstName),
            $("#__LastName").val(data.dataobject.LastName),
            $("#__Email").val(data.dataobject.Email),
            $("#__Phone").val(data.dataobject.Phone),
            $("#__RoleTypeId").val(data.dataobject.RoleTypeId),
            $("#__Note").val(data.dataobject.Note),
            $("#__Title").val(data.dataobject.Title)
            $("#__memberId").text(data.dataobject.MemberId)
                      
            $("#__loadMinisterDialog").dialog("close");
        };

        $("#__loadMinisterDialog").dialog({ autoOpen: false, width: 1000, height: 600, modal: false, title: 'Ministers' });
        $("#__createMinisterDialog").dialog({ autoOpen: false, width: 800, height: 630, modal: false, title: 'Add Minister' });

        suftnet_dynamicTable.initMinisterFinder($("#smallGroupUrl").attr("data-smallGroupUrl"));      
      
    }
    
}