
var collapsible = {

    open: function() {

        setTimeout(function () {
            $("#MainCollapsible").show();
        }, 0);

        var _opt = {
            active: false,
            collapsible: false,
            autoHeight: true,
            animated: "bounceslide"
        };

        $("#MainCollapsible").accordion(_opt);
        $("#MainCollapsible").accordion("option", "active", 0);
        $("#MainCollapsibleLeft").accordion(_opt);
        $("#MainCollapsibleLeft").accordion("option", "active", 0);
        $("#MainCollapsibleRight").accordion(_opt);
        $("#MainCollapsibleRight").accordion("option", "active", 0);
    },

    close: function () {
        setTimeout(function () {
            $("#MainCollapsible").show();
        }, 0);

        var _opt = {
            active: true,
            collapsible: false,
            autoHeight: true,
            animated: "bounceslide"
        };

        //$("#MainCollapsible").accordion();

        //$("#MainCollapsible").accordion(_opt);
        //$("#MainCollapsible").accordion("option", "active", 1);
        //$("#MainCollapsibleLeft").accordion();
        //$("#MainCollapsibleLeft").accordion("option", "active", 1);
        //$("#MainCollapsibleRight").accordion();
        //$("#MainCollapsibleRight").accordion("option", "active", 1);

    }
}