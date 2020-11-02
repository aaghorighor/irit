var _tableViewModel;

var tableOder = {

    pageInit: function () {

        tableOder.initKo();
        tableOder.load();    
        tableOder.listener();

        $("#orderDialog").dialog({ autoOpen: false, width: 400, height: 310, modal: false, title: 'Create' });
    },
    load: function () {

        js.ajaxGet($("#loadUrl").attr("data-loadUrl")).then(function (data) {

            $.each(data.dataobject, function (i, value) {
                var item = new tableItem(value);
                _tableViewModel.addTable(item);
            });

        });  

    },
    initKo: function () {

        _tableViewModel = new tableViewModel();
        ko.applyBindings(_tableViewModel, document.getElementById("tableContainers"));
    },
    listener: function () {

        $("#Time").timepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open Picker",
            buttonImageOnly: true
        });

        $("#StartDt").datepicker({
            showOn: "button",
            buttonImage: suftnet_Settings.icon + "calendar.png",
            buttonText: "Open Date Picker",
            dateFormat: suftnet_Settings.dateTimeFormat,
            buttonImageOnly: true
        });

        $(document).on("click", "#btnClose", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();
     
            iuHelper.resetForm("#form");

            $("#orderDialog").dialog("close");
        });

        $(document).on("click", "#btnSubmit", function (e) {

            e.preventDefault();
            e.stopImmediatePropagation();

            if (!suftnet_validation.isValid("form")) {
                return false;
            }

            js.ajaxPost($("#form").attr("action"), $("#form").serialize()).then(
                function (data) {
                    switch (data.flag) {

                        case 1: //// add                  

                            if (_tableViewModel == null || undefined) {
                                console.log(data.orderId);
                            }
                                                   
                            _tableViewModel.update(data.order);               
                                                 
                            break;
                        case 2: //// update  
                         
                            break;

                        default: ;
                    }  

                    $("#Id").val("c7a037e8-3045-4996-9e02-1794e08622c6");
                    $("#TableId").val("c7a037e8-3045-4996-9e02-1794e08622c6");

                    iuHelper.resetForm("#form");
                    $("#orderDialog").dialog("close");

                }).catch(function (err) {
                    console.log(err);
                });
            
        });
    }
}