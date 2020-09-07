
var myProfile = {

    create : function(imageUrl)
    {
        $('#JoinDate').mask('99/99/9999');

        if (imageUrl != "")
        {           
            $("#TempUrl").attr('src', "/Content/Photo/Member/216X196/" + imageUrl);
        }

        suftnet_upload.init($("#uploadUrl").attr("data-uploadUrl"));
        myProfile.pageEvent();      
    },

    pageEvent : function()
    {
        $("#form-myProfile").submit(function ()
        {
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
         
        });

        $(document).on("click", "a.deleteFamily", function (e)
        {
            e.preventDefault();

            var deleteUrl = $(this).attr("deleteFamilyUrl");
                      
            $.confirm.show(
             {
                "message": "Do you want to remove this member?",
                "type": "danger",
                "yes": function ()
                {
                    js.ajaxPost(deleteUrl).then(
                     function (data)
                     {
                         window.location.reload();
                     });
                },
                "no": function ()
                {
                    
                }
            });            
        });

        $(document).on("click", "button#delete", function (e)
        {
            e.preventDefault();

            var deleteUrl = $(this).attr("deleteUrl");

            $.confirm.show(
             {
                 "message": "This will remove your record and member of your family",
                 "type": "danger",
                 "yes": function ()
                 {
                     js.ajaxPost(deleteUrl).then(
                      function (data)
                      {
                          window.location.reload();
                      });
                 },
                 "no": function () {

                 }
             });
        });

        $("#form-changePassword").submit(function ()
        {
            js.ajaxPost($("#form-changePassword").attr("action"), $("#form-changePassword").serialize()).then(
                      function (data)
                      {                        
                          $("#_success").removeClass("hide");
                      });
            
            return false;
        });
    }    
}