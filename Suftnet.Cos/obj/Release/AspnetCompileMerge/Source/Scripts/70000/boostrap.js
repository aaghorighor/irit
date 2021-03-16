
var boostrap = {

    init: function () {
                
        $('.etip, .nwtip,button, .tooltip, #file_upload').tipsy(); 
        suftnet_Settings.setCurrencySymbol($("#currencySymbol").text());           
        suftnet_Settings.tabs();
    }
}