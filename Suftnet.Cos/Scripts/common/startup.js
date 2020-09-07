
var suftnet_Settings = function () {

    var dateTimeFormat = null;
    var icon = "";
  
    function tableInit(tableName, column)
    {
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
    }

    function Init()
    {          
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

        //$("#InvoiceDialog").dialog("option", "minWidth", 200);

        //$("#InvoiceDialog").dialog({
        //    modal: true
        //});
       
    }
   
    function setCurrencySymbol(value)
    {
        suftnet_grid.currency = value;
    }

    function setDateTimeFormat(value) {
        suftnet_Settings.dateTimeFormat = value;        
    }

    function setCalendarIcon(value) {     

        suftnet_Settings.icon = value;       
    }

    function validatorListener() {
        suftnet_validation.EventListener();
    }

    function clearFormErrorMessages(form) {
        suftnet_validation.clearFormErrorMessages(form);
    }

    function clearErrorMessages(form) {
        suftnet_validation.clearErrorMessages(form);
    }

    return {
       
        SetCurrencySymbol: setCurrencySymbol,
        SetDateTimeFormat: setDateTimeFormat,
        ValidatorListner: validatorListener,
        ClearErrorMessages: clearErrorMessages,
        ClearFormErrorMessages: clearFormErrorMessages,   
        SetCalendarIcon: setCalendarIcon,
        Init: Init,
        TableInit: tableInit,       
    }

}();





