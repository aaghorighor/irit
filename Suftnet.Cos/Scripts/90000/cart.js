var menuViewModel = null;
var cartViewModel = null;
var categoryViewModel = null;
var addonViewModel = null;

var cart = {

    init: function () {

        cart.ui();
        cart.ko();      
        cart.listener();
        cart.load();
    },
    ko: function () {

        categoryViewModel = new CategoryViewModel();
        ko.applyBindings(categoryViewModel, document.getElementById("categoryContainer"));   

        menuViewModel = new MenuViewModel();
        ko.applyBindings(menuViewModel, document.getElementById("menuContainer"));

        cartViewModel = new CartViewModel();
        ko.applyBindings(cartViewModel, document.getElementById("cartContainer"));
        ko.applyBindings(cartViewModel, document.getElementById("cartSummaryContainer"));
        ko.applyBindings(cartViewModel, document.getElementById("paymenyContainer"));

        addonViewModel = new AddonViewModel();
        ko.applyBindings(addonViewModel, document.getElementById("addonContainer"));

    },
    ui: function ()
    {
        setTimeout(function () {
            $("#MainCollapsible").show();
        }, 0);

        $('#mixedSlider').multislider({
            duration: 0,
            interval: 0
        });

        $('#cartContainer').perfectScrollbar({ suppressScrollX: true });
        $('#_itemcontainer').perfectScrollbar({ suppressScrollX: true });

        $("#addonDialog").dialog({ autoOpen: false, width: 450, height: 435, modal: false, title: 'Add On' });
        $("#paymentDialog").dialog({ autoOpen: false, width: 853, height: 590, modal: false, title: 'Tender' });
    },
    listener: function () {

        $(document).on("click","#btnOpenTender", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();

            cartViewModel.getSelectedStatus($("#orderStatusId").attr("data-orderStatusId"),
                $("#orderTypeId").attr("data-orderTypeId"));

            $("#paymentDialog").dialog("open");

        });

        $(document).on("click", "#btnCloseTender", function (event) {

            event.preventDefault();
            event.stopImmediatePropagation();          

            $("#paymentDialog").dialog("close");

        });  

        $(document).on("change", "#DiscountId", function (event) {      
            event.preventDefault();
            event.stopImmediatePropagation();
            
            var option = $('option:selected', this).attr('rate');
            cartViewModel.computeDiscount(0);

            if (option != undefined) {
                cartViewModel.computeDiscount(option);
            } 

        });

        $(document).on("change", "#TaxId", function (event) {       
            event.preventDefault();
            event.stopImmediatePropagation();

            var option = $('option:selected', this).attr('rate');          
            cartViewModel.computeTax(0);

            if (option != undefined) {
                cartViewModel.computeTax(option);
            }
                       
        });

        $(document).on("click", "#TaxId", function (event) {
            event.preventDefault();
            event.stopImmediatePropagation();

            var option = $('option:selected', this).attr('rate');
            cartViewModel.computeTax(0);

            if (option != undefined) {
                cartViewModel.computeTax(option);
            }

        });
    },

    load: function ()
    {
        loadCategory();
        loadMenu();
        loadCart();
    }
}