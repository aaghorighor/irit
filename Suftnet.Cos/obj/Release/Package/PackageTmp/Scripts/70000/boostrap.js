
var boostrap = {

    init: function () {
                
        $('a, button, .tooltip, #file_upload').tipsy(); 
        suftnet_Settings.setCurrencySymbol($("#currencySymbol").text());           
        suftnet_Settings.tabs();
    }
}