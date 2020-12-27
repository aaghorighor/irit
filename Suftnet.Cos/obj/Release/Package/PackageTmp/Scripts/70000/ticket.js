
var ticket = {

    create : function()
    {
        $("#CreatedDT").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        $("#btnSubmit").bind("click", function (event) {

            event.preventDefault();

            if (!suftnet_validation.isValid("ticketform")) {
                return false;
            }

            js.ajaxPost($("#ticketform").attr("action"), $("#ticketform").serialize()).then(
               function (data) {

                   switch (data.flag) {
                       case 1: //// add                                          

                           suftnet_grid.addTicketExtension(data.objrow);
                           break;
                       case 2: //// update 

                           suftnet_grid.updateTicket(data.objrow);

                           suftnet.Tab(1);

                           break;
                       default:;
                   }

                   $("#ticketform #Id").val(0);

                   iuHelper.resetForm("#ticketform");
               });
        });

        suftnet_Settings.ClearErrorMessages("#ticketform");
    },

    load : function()
    {

        $(document).on("click", "#TicketThreadLink", function (e) {

            e.preventDefault();

            $("#TicketId").val($(this).attr("TicketId"));
            $("#ticketId").text("Ticket No :" + $(this).attr("TicketId"));

            js.ajaxGet($("#getAllUrl").attr("data-getAllUrl"), { Id: $(this).attr("ticketid") }).then(
               function (data) {

                   suftnet_grid.refreshFrontOfficeTicketThread(data.dataobject);
               });

            suftnet.Tab(2);
        });

        $(document).on("click", "#EditLink", function (e) {
            e.preventDefault();

            js.ajaxGet($("#getUrl").attr("data-getUrl"), { Id: $(this).attr("EditId") }).then(
             function (data) {
                 var dataobject = data.dataobject;

                 $("#Subject").val(dataobject.Subject);
                 $("#DepartmentId").val(dataobject.DepartmentId);
                 $("#PriorityId").val(dataobject.PriorityId);
                 $("#StatusId").val(dataobject.StatusId);
                 $("#Id").val(dataobject.Id);
                 $("#Note").val(dataobject.Note);
                 $("#CreatedDT").val(dataobject.CreatedOn);

                 suftnet.Tab(0);

             });
        });

        suftnet_Settings.TableInit('#tdTicket', [0, 6]);
    }
}

var ticketThread = {

    create : function()
    {
        $("#ThreadCreatedDT").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open datepicker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        $("#btnback").bind("click", function (event) {

            $("#TicketId").val(0);
            $("#ticketId").text('');

            suftnet.Tab(1);

        });

        $("#btnSaveChanges").bind("click", function (event) {

            event.preventDefault();

            if (!suftnet_validation.isValid("ticketThreadform")) {
                return false;
            }

            js.ajaxPost($("#ticketThreadform").attr("action"), $("#ticketThreadform").serialize()).then(
               function (data) {
                   switch (data.flag) {
                       case 1: //// add                                          

                           suftnet_grid.addFrontOfficeTicketThreadExtension(data.objrow);
                           break;
                       case 2: //// update 

                           break;
                       default:;
                   }

                   $("#ticketThreadform #Id").val(0);

                   iuHelper.resetForm("#ticketThreadform");

               });
        });

        suftnet_Settings.ClearErrorMessages("#ticketThreadform");

    }
}