
var reservationViewModel = null;
var cartViewModel = null;
var categoryViewModel = null;
var productViewModel = null;
var tableViewModel = null;
var menuAddonModel = null;
var orderTable;

var jsInit = function ()
{
    settings.orderUrl = $("#createOrderDetail").attr("data-createOrderDetailUrl");
    settings.ProductUrl = $("#getMenuByCategoryId").attr("data-getMenuByCategoryIdUrl");
    settings.productDetailUrl = $("#getMenuByMenuId").attr("data-getMenuByMenuIdUrl");
    settings.OrderDetailUrl = $("#getOrderDetailById").attr("data-getOrderDetailByIdUrl");
    settings.categoryUrl = $("#GetCategoryById").attr("data-getCategoryByCategoryIdUrl");
    settings.testPrintUrl = $("#testPrint").attr("data-testPrintUrl");
    settings.openCashDrawerUrl = $("#openCashDrawer").attr("data-openCashDrawerUrl");
    settings.reserveUrl = $("#getOrderByReservation").attr("data-getOrderByReservationUrl");
    settings.poleDisplayUrl = $("#pending").attr("data-pendingUrl");
    settings.totalPoleDisplayUrl = $("#complete").attr("data-completeUrl");
    settings.clearPoleDisplayUrl = $("#clear").attr("data-clearUrl");
    settings.initProductUrl = $("#getMenus").attr("data-getMenusUrl");
    settings.contentUrl = $("#imageUrl").attr("data-imageUrl");
    settings.deleteUrl = $("#delete").attr("data-deleteUrl");
    settings.changeStatusUrl = $("#changeStatus").attr("data-changeStatusUrl");
    settings.commonUrl = $("#getCommonById").attr("data-getCommonByIdUrl");
    settings.deliveryUrl = $("#getOrderByDelivery").attr("data-getOrderByDeliveryUrl");
    settings.changeDeliveryStatusUrl = $("#changeDeliveryStatus").attr("data-changeDeliveryStatusUrl");
    settings.tableUrl = $("#getTableById").attr("data-getTableByIdUrl");

    ///..................................................................ko binding...............................
   
    reservationViewModel = new ReservationViewModel();
    ko.applyBindings(reservationViewModel, document.getElementById("reservationContainer"));

    cartViewModel = new CartViewModel();
    ko.applyBindings(cartViewModel, document.getElementById("cartContainer"));
    ko.applyBindings(cartViewModel, document.getElementById("orderSummaryContainer"));
    ko.applyBindings(cartViewModel, document.getElementById("paymenyContainer"));
   
    categoryViewModel = new CategoryViewModel();
    ko.applyBindings(categoryViewModel, document.getElementById("categoryContainer"));
    InitCategory(settings.categoryUrl);

    productViewModel = new ProductViewModel();
    ko.applyBindings(productViewModel, document.getElementById("productContainer"));
    InitProduct();    
   
    tableViewModel = new TableViewModel();
    ko.applyBindings(tableViewModel, document.getElementById("tableContainers"));
    InitTable(settings.tableUrl);

    menuAddonModel = new MenuAddonModel();
    ko.applyBindings(menuAddonModel, document.getElementById("menuAddonContainer"));

    LoadReserveOrder(); //// load reserved orders
   
    ///...................................................................Js binding ..............................
               
    setTimeout(function () {
      
        $("#MainCollapsible").show();     
      
        $('#mixedSlider').multislider({
            duration: 0,
            interval: 0
        });

        Settings();       
              
    }, 0);     
 
    $("#btnPaymentCloseDialog").click(function (event)
    {
        event.preventDefault();
               
        $("#PaymentDialog").dialog("close");       
    });

    $("#btnNewReservation").click(function (e)
    {
        e.preventDefault();      
            
        $("#StatusId").val(OrderStatus.Reservation);
        $("#OrderTypeId").val(OrderType.Reservation);     
      
        $("#reservationOrderDialog").dialog("open");
    });
      
    $(".barandtakeway").click(function (e)
    {
        e.preventDefault();     
      
        $("#TableId").val($(this).attr("tableId"));
        $("#StatusId").val(OrderStatus.Completed);
        $("#OrderTypeId").val($(this).attr("orderType"));

        cartViewModel.orderType($("#OrderTypeId").val());

        $("#orderNo").text(Suftnet_Utility.getUniqueName());
        $("#tableNo").text($(this).attr("title"));

        cartViewModel.getSelectedStatus(OrderStatus.Completed);

        $("#MainCollapsible").accordion("activate", 1);

    });   

    $("#btnback").click(function () {

        cartViewModel.reset(); // clear ko         
        settings.reset(); // reset all global variable
        settings.orderStatusId = 0;                   
        cartViewModel.orderType(0);

        $("#orderNo").text("");
        $("#categoryName").text("");       

        iuHelper.resetForm("#paymentform");

        $("#PaymentDialog").dialog("close");
        $("#MenuAddonDialog").dialog("close");

        $("#MainCollapsible").accordion("activate", 0);
    });

    $("#btnDrawer").click(function (event) {

        event.preventDefault();

        if ($("#IpAddress").val() != '')
        {
            var param = {
                ipAddress: $("#IpAddress").val()
            };

            js.ajaxPost(settings.openCashDrawerUrl, param).then(
             function (data) {

             });
        }

    });

    $("#DiscountId").change(function (event)
    {
        event.preventDefault();

        if ($(this).val() > 0)
        {
            var param = {
                Id: $(this).val()
            };

            js.ajaxGet(settings.commonUrl, param).then(
                function (data) {
                    cartViewModel.computeDiscount(data.dataobject.code);
                });
        }
       
    });

    $("#TaxId").change(function (event)
    {
        event.preventDefault();

        if ($(this).val() > 0) {

            var param = {
                Id: $(this).val()
            };

            js.ajaxGet(settings.commonUrl, param).then(
                function (data) {
                    cartViewModel.computeTax(data.dataobject.code);
                });
        }
    });

    $("#btnPayment").click(function (event) {

        event.preventDefault();
        event.stopImmediatePropagation();
              
        $("#PaymentDialog").dialog("open");
       
    });
   
    var _option = {
        active: true,
        collapsible: true,
        autoHeight: false,
        animated: "bounceslide"
    };

    $("#MainCollapsible").accordion(_option);
    $("#MainCollapsible").accordion("activate", 0);

    $('#cartContainer').perfectScrollbar({ suppressScrollX: true });
    $('#_itemcontainer').perfectScrollbar({ suppressScrollX: true });

    $("#orderDialog").dialog({ autoOpen: false, width: 400, height: 350, modal: false, title: 'Order' });
    $("#reservationOrderDialog").dialog({ autoOpen: false, width: 400, height: 480, modal: false, title: 'Reservation Order' });    
    $("#PaymentDialog").dialog({ autoOpen: false, width: 853, height: 630, modal: false, title: 'Tender' });
    $("#MenuAddonDialog").dialog({ autoOpen: false, width: 450, height: 585, modal: false, title: 'Menu Options' });
    $("#PrinterSettingDialog").dialog({ autoOpen: false, width: 450, height: 290, modal: false, title: 'Settings' });
}

var Settings = function ()
{
    LoadSettings();

    $("#btnSaveSettings").click(function ()
    {
        SaveSettings();
        setTimeout("showSuccess('Success',5000);", 1000);
    });

    $("#btnTest").click(function () {

        if ($("#IpAddress").val() == '')
        {
            setTimeout("showSuccess('Success',5000);", 1000);
            return false;
        }

        var param = {
            ipAddress: $("#IpAddress").val()
        };

        js.ajaxPost(settings.testPrintUrl, param).then(
         function (data) {

         });
    });

    $(document).on("click", "#printerSetting", function () {
        $("#PrinterSettingDialog").dialog("open");
    });

    $("#btnCloseSettings").click(function () {
        $("#PrinterSettingDialog").dialog("close");
    });


    function SaveSettings() {

        localStorage.setItem('IpAddress', $("#IpAddress").val());
    }

    function LoadSettings() {
        $("#IpAddress").val(localStorage.getItem('IpAddress'));
    }
}
