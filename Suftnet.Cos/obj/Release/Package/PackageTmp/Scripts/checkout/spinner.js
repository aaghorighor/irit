
var spinner = {

    init: function() {

        $(document).on("click", "#myDashboard",
            function () {

                $.preloader.start({
                    position: 'center',
                    modal: true,
                    src: $("#spriteUrl").attr("data-spriteUrl")
                });

            });    
    }
}