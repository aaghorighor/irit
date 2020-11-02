
var backOffice_dashboard = {

    init: function () {
        
        if ($("#userName").attr("data-userName") === "demo@synevde.com") {
            backoffice_tour.init();
        } else {
            learn_backoffice.init();
        }

        js.ajaxGet($("#backOfficeDashboardUrl").attr("data-backOfficeDashboardUrl")).then(
            function (data) {

                var model = data.summary;
                             
            });          
    }
}

var adminOffice_dashboard = {

    init: function () {      

        js.ajaxGet($("#adminOfficeDashboardUrl").attr("data-adminOfficeDashboardUrl")).then(
            function (data) {

                var objectdata = data.summary;

                if (objectdata != null) {

                    $("#customer").text(objectdata.Tenants);
                    $("#paid").text(objectdata.Paid);
                    $("#trials").text(objectdata.Trials);
                    $("#expired").text(objectdata.Expired);
                    $("#cancelled").text(objectdata.Cancelled);
                    $("#mobile").text(objectdata.Mobile);
                    $("#web").text(objectdata.Web);
                    $("#total").text(objectdata.Members);
                };                              
            });
    }
}