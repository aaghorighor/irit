
var mobileSlider = {

    create : function()
    {       
        $(document).on("click","#btnSubmit", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            if ($('#Publish').is(':checked')) {
                $("#Publish").val(true);
            } else {
                $("#Publish").val(false);
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
             function (data) {
                 switch (data.flag) {

                     case 1: //// add                                          

                         suftnet_grid.addMobileSlider(data.objrow);
                         break;
                     case 2: //// update  

                         suftnet_grid.updateMobileSlider(data.objrow);
                         break;

                     default:;
                 }

                 $("#Id").val(0);
                 $("#Publish").attr("checked", false);

                 iuHelper.resetForm("#form");
                
             });
        });

        suftnet_Settings.ClearErrorMessages("#form");
    },

    load: function () {

        $(document).on("click", "#Delete", function (e) {
            e.preventDefault();

            js.confirm($("#deleteUrl").attr("data-deleteUrl"), { Id: $(this).attr("DeleteId") }, $(this).attr("DeleteId"), "#tdMobileSlider").then(
             function (data) {

             });
        });

        $(document).on("click", "#EditLink", function (e) {

            e.preventDefault();

            js.ajaxGet($("#getUrl").attr("data-getUrl"), { Id: $(this).attr("EditId") }).then(
              function (data) {

                  var dataobject = data.dataobject;

                  $("#Publish").attr("checked", dataobject.Publish);
                  $("#Id").val(dataobject.Id);
                  $("#Title").val(dataobject.Title);
                  $("#ImageUrl").val(dataobject.ImageUrl);
                  $("#Description").val(dataobject.Description);                 

              });
        });

        suftnet_Settings.TableInit('#tdMobileSlider', [0, 1,2,3,4]);
    }
}