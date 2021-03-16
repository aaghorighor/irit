
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

            if ($('#IsFlatRate').is(':checked')) {
                $("#IsFlatRate").val(true);
            } else {
                $("#IsFlatRate").val(false);
            }                           

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
              function (data) {

              });
        });

        suftnet_Settings.ClearErrorMessages("#form");
    },
    adminPageInit: function () {       
       
        setting.listener();
        setting.load();
    },
    listener: function () {

        $("#btnSubmit").click(function (e) {

            e.preventDefault();

            if (!suftnet_validation.isValid("form")) {
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

            if ($('#IsFlatRate').is(':checked')) {
                $("#IsFlatRate").val(true);
            } else {
                $("#IsFlatRate").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {

                });
        });

        $(document).on("change", "#CurrencyId", function () {
            $("#CurrencyCode").val($(this).find('option:selected').attr("code"));
        });

        suftnet_upload.init1($("#uploadBackgroundUrl").attr("data-uploadBackgroundUrl"));
        suftnet_upload.init3($("#uploadLogoUrl").attr("data-uploadLogoUrl"));
    },
    load: function () {

        js.ajaxGet($("#settingsUrl").attr("data-settingsUrl")).then(
            function (data) {
                              
                var model = data.dataobject;
               
                $("#CurrencyId").val(model.CurrencyId);                           
                $("#CurrencyCode").val(model.CurrencyCode); 
                $("#LogoUrl").val(model.LogoUrl);   
                $("#BackgroundUrl").val(model.BackgroundUrl); 

                $("#logoImagePlaceHolder")
                    .on('load', function () {
                        $(this).removeClass("displayNone");
                        $("#logoImageErrorPlaceHolder").addClass("displayNone");
                    })
                    .on('error', function () {
                        $("#logoImageErrorPlaceHolder").attr("src", $("#blankImagePlaceHolderUrl").attr("data-blankImagePlaceHolderUrl"));
                        $("#logoImageErrorPlaceHolder").removeClass("displayNone");
                        $(this).addClass("displayNone");

                    }).attr("src", $("#logoImagePlaceHolderUrl").attr("data-logoImagePlaceHolderUrl") + model.LogoUrl);

                
                $("#backgroundImagePlaceHolder")
                    .on('load', function () {
                        $(this).removeClass("displayNone");
                        $("#backgroundImageErrorPlaceHolder").addClass("displayNone");
                    })
                    .on('error', function () {
                        $("#backgroundImageErrorPlaceHolder").attr("src", $("#blankImagePlaceHolderUrl").attr("data-blankImagePlaceHolderUrl"));
                        $("#backgroundImageErrorPlaceHolder").removeClass("displayNone");
                        $(this).addClass("displayNone");

                    }).attr("src", $("#backgroundImagePlaceHolderUrl").attr("data-backgroundImagePlaceHolderUrl") + model.BackgroundUrl);

                                                             
                $("#Latitude").val(model.Latitude);
                $("#Longitude").val(model.Longitude);
                $("#CompleteAddress").val(model.CompleteAddress);
                $("#Town").val(model.Town);
                $("#County").val(model.County); 
                $("#PostCode").val(model.PostCode);
                $("#Country").val(model.Country);
                $("#AddressId").val(model.AddressId);         
                $("#StatusId").val(model.StatusId.toUpperCase());
                $("#AppCode").val(model.AppCode);
                $("#Name").val(model.Name);               
                $("#Telephone").val(model.Telephone);
                $("#Mobile").val(model.Mobile);
                $("#DeliveryCost").val(model.DeliveryCost);
                $("#TaxRate").val(model.TaxRate);
                $("#DiscountRate").val(model.DiscountRate);
                $("#Email").val(model.Email);
                $("#WebsiteUrl").val(model.WebsiteUrl);
                $("#Description").val(model.Description);

                if (model.IsFlatRate === true) {
                    $("#IsFlatRate").attr("checked", true);
                } else {
                    $("#IsFlatRate").attr("checked", false);
                }
              
                $("#DeliveryRate").val(model.DeliveryRate);
                $("#DeliveryUnitId").val(model.DeliveryUnitId);
                $("#DeliveryLimitNote").val(model.DeliveryLimitNote);
                $("#FlatRate").val(model.FlatRate);
              
                $("#StripePublishableKey").val(model.StripePublishableKey);
                $("#StripeSecretKey").val(model.StripeSecretKey);

                if (model.Publish === true) {
                    $("#Publish").attr("checked", true);
                } else {
                    $("#Publish").attr("checked", false);
                }                                                                
                                     
            }).catch(function (e) {
                console.log(e);
            });
    },
    loadAdmin: function () {

        $("#btnSaveChanges").click(function (e) {

            e.preventDefault();

            if (!suftnet_validation.isValid("form")) { //// Call validation routine
                return false;
            }
            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {

                });

        });

        js.ajaxGet($("#loadUrl").attr("data-loadUrl")).then(
            function (data) {

                var model = data.dataobject;
            
                $("#Id").val(model.Id);
                $("#Server").val(model.Server);
                $("#Email").val(model.Email);
                $("#ServerEmail").val(model.ServerEmail);
                $("#UserName").val(model.UserName);
                $("#Password").val(model.Password);
                $("#Port").val(model.Port);

                $("#DateTimeFormat").val(model.DateTimeFormat);
                $("#CurrencyId").val(model.CurrencyId);

                $("#Company").val(model.Company);
                $("#Telephone").val(model.Telephone);
                $("#Mobile").val(model.Mobile);
                $("#Description").val(model.Description);
                $("#AppCode").val(model.AppCode);
                $("#AddressId").val(model.AddressId);
                $("#AddressLine1").val(model.AddressLine1);
                $("#AddressLine2").val(model.AddressLine2);
                $("#AddressLine3").val(model.AddressLine3);
                $("#PostCode").val(model.PostCode);
                $("#Country").val(model.Country);
                $("#TaxRate").val(model.TaxRate);

            }).catch(function (e) {
                console.log(e);
            });
    }
}