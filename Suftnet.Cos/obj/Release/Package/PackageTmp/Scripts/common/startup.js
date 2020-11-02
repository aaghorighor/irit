
var suftnet_Settings = {

    dateTimeFormat: $("#defaultDateTimeFormat").attr("data-defaultDateTimeFormat"),
    icon: '\/Content/zice-OneChurch/images/icon/color_18/',

    TableInit: function (tableName, column) {
        $(tableName).dataTable({
            "aLengthMenu": [[5, 10, 25, 50], [5, 10, 25, 50]],
            "iDisplayLength": 5,
            "bAutoWidth": false,
            "aaSorting": [[0, "desc"]],
            "aoColumnDefs": [
                {
                    'bSortable': false,
                    'aTargets': column
                }],
            "sPaginationType": "full_numbers"
        });
    },

    tabs: function () {
        $("ul.tabs li").fadeIn(400);
        $("ul.tabs li:first").addClass("active").fadeIn(400);
        $(".tab_content:first").fadeIn();
        $("ul.tabs li").on('click', function () {
            $("ul.tabs li").removeClass("active");
            $(this).addClass("active");
            var activeTab = $(this).find("a").attr("href");
            $('.tab_content').fadeOut();
            $(activeTab).delay(400).fadeIn();

            return false;
        });

    },

    setCurrencySymbol: function (value) {
        suftnet_grid.currency = value;
    },

    validatorListener: function () {
        suftnet_validation.EventListener();
    },

    ClearFormErrorMessages: function (form) {
        suftnet_validation.clearFormErrorMessages(form);
    },

    ClearErrorMessages: function (form) {
        suftnet_validation.clearErrorMessages(form);
    }
};





