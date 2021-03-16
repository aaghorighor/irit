
var support = {

    init :  function() {

        support.pageInit();
        support.pageEvent();
    },

    pageInit : function()
    {
        $(".faq-right").load($("#supportUrl").attr("data-supportUrl"), { Id: 1 });
    },

    pageEvent : function()
    {
        $(document).on("click", "li a", function () {

            $(".faq-left ul li").removeClass("active");
            $(this).parent().addClass("active");

            $(".faq-right").load($("#supportUrl").attr("data-supportUrl"), { Id: $(this).attr("data-supportTypeId") });
        });

    }
}