
var setting = {

    init: function ()
    {       
        setting.load();

        $(document).on("change", "#CurrencyId", function () {
            $("#CurrencyCode").val($(this).find('option:selected').attr("code"));
        });

        $("#btnSaveChanges").click(function (e) {

            e.preventDefault();

            if (!suftnet_validation.isValid("form"))
            { 
                return false;
            };

            if ($('#PushNotification').is(':checked')) {
                $("#PushNotification").val(true);
            } else {
                $("#PushNotification").val(false);
            }

            if ($('#Publish').is(':checked')) {
                $("#Publish").val(true);
            } else {
                $("#Publish").val(false);
            }

            //$("#Description").val(tinymce.activeEditor.getContent());          

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
              function (data) {

              });
        });

        suftnet_Settings.ClearErrorMessages("#form");
    },
    load: function () {

        js.ajaxGet($("#settingsUrl").attr("data-settingsUrl")).then(
            function (data) {
                              
                var model = data.dataobject;
               
                $("#CurrencyId").val(model.CurrencyId);                           
                $("#CurrencyCode").val(model.CurrencyCode); 
                $("#LogoUrl").val(model.LogoUrl);                
                $("#BackgroundUrl").val(model.BackgroundUrl);  
                                                             
                $("#Latitude").val(model.Latitude);
                $("#Longitude").val(model.Longitude);
                $("#CompleteAddress").val(model.CompleteAddress);
                $("#Town").val(model.Town);
                $("#County").val(model.County); 
                $("#PostCode").val(model.PostCode);
                $("#Country").val(model.Country);
                $("#AddressId").val(model.AddressId);                              
                               
                $("#Name").val(model.Name);               
                $("#Telephone").val(model.Telephone);
                $("#Mobile").val(model.Mobile);
                $("#Email").val(model.Email);
                $("#WebsiteUrl").val(model.WebsiteUrl);
                $("#Description").val(model.Description);
              
                $("#StripePublishableKey").val(model.StripePublishableKey);
                $("#StripeSecretKey").val(model.StripeSecretKey);

                if (model.Publish === true) {
                    $("#Publish").attr("checked", true);
                } else {
                    $("#Publish").attr("checked", false);
                }

                //if (model.Description != null) {
                //    if (tinymce != undefined) {
                //        tinymce.get('Description').setContent(model.Description); 
                //    }
                //}                                 
                                     
            }).catch(function (e) {
                console.log(e);
            });
    }
}