
var welcome = {

    startup : function(){
        $(function () {
            $('#startup').bazeModal({
                onOpen: function () {

                },
                onClose: function ()
                {
                    js.ajaxPost($("#updateStartUpUrl").attr("data-updateStartUpUrl")).then(
                    function (data) {

                    });
                }
            });

            $('#startup').trigger("click");
        });
    }
}