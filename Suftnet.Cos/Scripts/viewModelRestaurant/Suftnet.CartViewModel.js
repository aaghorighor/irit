
var CartViewModel = function () {
    var self = this;
 
    self.orderId = ko.observable();
    self.orderStatus = ko.observable();
    self.orderType = ko.observable();

    self.id = ko.observable();
    self.amountPaid = ko.observable();
    self.options = ko.observable();

    self.grandTotal = ko.observable();
    self.total = ko.observable();
    self.balance = ko.observable();   
 
    self.paid = ko.observable(0);

    self.totalDiscount = ko.observable();
    self.discountRate = ko.observable();

    self.taxRate = ko.observable();
    self.totalTax = ko.observable();
        
    self.items = ko.observableArray([]);

    self.isReceiptPrinter = ko.observable(false),
    self.isKitchenPrinter = ko.observable(false),

    self.selectedOptionValue = ko.observable();
    selectedPaymentMethod = ko.observable();

    self.optionValues = ko.observableArray([new Status("Completed", OrderStatus.Completed), new Status("Dine In", OrderStatus.Occupied), new Status("Pending", OrderStatus.Reservation)]);
    self.paymentMethods = ko.observableArray([new PaymentMethod("Card", paymentMethod.CreditCard), new PaymentMethod("Cash", paymentMethod.Cash)]);
             
    self.reset = function ()
    {
        self.amountPaid('');     
        self.isReceiptPrinter(false);
        self.isKitchenPrinter(false);
        self.items.removeAll();           
      
        self.discountRate(0);
        self.totalDiscount(0);

        self.taxRate(0);
        self.totalTax(0);
        self.paid(0);
    },
      
    mykeyboard = function (data, event)
    {              
        if (event.target.value == "11") {

            if (self.amountPaid() > 0)
            {
                var amount = self.amountPaid();
                var res = amount.toString().substring(0, amount.length - 1);
                self.amountPaid(res)
            }

            return false;
        }

        if (event.target.value == "12") {

            if (self.amountPaid() > 0) {

                self.amountPaid('');                    
            }

            return false;
        }

        if (self.amountPaid() == undefined)
        {           
            amount = parseInt(event.target.innerText);           
        } else {
            amount =self.amountPaid() +  event.target.innerText;
        }

        self.amountPaid(amount);              
    };

    clear = function () {
        self.amountPaid('');
    };

    self.setOrderId = function (orderId) {
        self.orderId(orderId);
    };
 
    self.computeGrandTotal = ko.computed(function ()
    {
        var total = self.total();
       
        if (self.totalDiscount() > 0) {
            total = parseFloat(total) - parseFloat(self.totalDiscount());
        }

        if (self.totalTax() > 0) {
            total = parseFloat(total) + parseFloat(self.totalTax());
        }       

        self.grandTotal(total);

        return suftnet_grid.formatCurrency(total);
    });     
  
    self.computeTotal = ko.computed(function () {
        var total = 0;

        $.each(self.items(), function () {
            total += this.lineTotal()
        });       

        self.total(total);       

        return suftnet_grid.formatCurrency(total);
    });
        
    self.computeBalance = ko.computed(function ()
    {
        var grandTotal = self.grandTotal();
        var balance = 0;
                                             
        if (grandTotal > 0)
        {
            if (self.paid() > 0)
            {               
                balance = grandTotal - (parseFloat(self.paid()));
            } else {
               
                balance = grandTotal;
            }                   
        }        

        self.balance(balance);    
              
        return suftnet_grid.formatCurrency(balance);
    });
        
    self.displayTax = ko.computed(function ()
    {
        return suftnet_grid.formatCurrency(self.totalTax());
    });

    self.displayAmountPaid = ko.computed(function ()
    {
        return suftnet_grid.formatCurrency(self.paid());
    });

    self.computeTax = function (rate)
    {       
        var total = self.total();
        var taxRate = 0;
        var totalTax = 0;
              
        if (!isNaN(rate)) {
            taxRate = rate;
        }       
      
        totalTax = parseFloat(total) * (parseFloat(taxRate) / 100);
  
        if (totalTax > 0)
        {           
            self.totalTax(totalTax);
            self.taxRate(rate);           

        } else {

            self.taxRate(0);
            self.totalTax(0);
        } 

        return totalTax;
    };

    self.displayDiscount = ko.computed(function ()
    {
        return suftnet_grid.formatCurrency(self.totalDiscount());
    });

    self.computeDiscount = function (rate)
    {
        var total = self.grandTotal();
        var discountRate = 0;
        var totalDiscount = 0;            
                        
        if (!isNaN(rate))
        {
            discountRate = rate;
        }

        totalDiscount = parseFloat(total) * (parseFloat(rate) / 100);
      
        if (totalDiscount > 0)
        {
            self.totalDiscount(totalDiscount);
            self.discountRate(rate);
        } else {

            self.discountRate(0);
            self.totalDiscount(0);
        }      

        return suftnet_grid.formatCurrency(totalDiscount);
    };

    self.payment = ko.computed(function () {

        if (self.amountPaid() == '') {
            return '';
        }

        if (self.amountPaid() > 0) {
            return suftnet_grid.formatCurrency(parseFloat(self.amountPaid()));
        }

        return '';
    });

    self.change = ko.computed(function () {
       
        var balance = self.balance();

        var amountPaid = self.amountPaid();

        if (balance < 0|| balance == 0) {
            return '';
        }
             
        if (amountPaid == '') {
            return '';
        }       
                     
        if (parseFloat(amountPaid) > parseFloat(balance))
        {           
            var change = parseFloat(self.amountPaid()) -parseFloat(balance);

            return suftnet_grid.formatCurrency(change);
        }
       
        return '';
    });

    self.totalItems = ko.computed(function ()
    {
        var total = 0;
        $.each(self.items(), function () {
            total += 1
        });

        return total;
    });
      
    self.addItem = function (item) {
       
        self.items.push(item);      

    }.bind(self);
      
    self.addPaymentMethod = function (data) {
        var item = {
            PaymentMethodId: data.ID,
            Title: data.Title,
        }

        self.paymentMethods.push(item);
    }.bind(self);

    self.optionValues.getStatusByStatusId = function (statusId) {
        var ret;
        this().forEach(function (status) {
            if (status.OrderStatusId == statusId) {
                ret = status;
            }
        });

        return ret;
    };
       
    self.getSelectedStatus = function(statusId)
    {       
        self.optionValues.removeAll();       

        switch (statusId)
        {
            case OrderStatus.Reservation: //// Reservation
               
                self.optionValues.push(new Status("Pending", OrderStatus.Reservation));              

                break;

            case OrderStatus.Occupied: //// Dine In
            case OrderStatus.Processing:

                self.optionValues.push(new Status("Dine In", OrderStatus.Occupied));
                //self.optionValues.push(new Status("Processing", OrderStatus.Processing));
                self.optionValues.push(new Status("Completed", OrderStatus.Completed));

                break;

            case OrderStatus.Completed: //// Completed
                              
                self.optionValues.push(new Status("Completed", OrderStatus.Completed));

                break;          

        }       

        self.selectedOptionValue(self.optionValues.getStatusByStatusId(statusId));

        self.orderStatus(statusId);
    }

    self.getDeliveryStatus = function (statusId) {
        self.optionValues.removeAll();

        switch (statusId) {
            case OrderStatus.Reservation: //// Reservation

                self.optionValues.push(new Status("Pending", OrderStatus.Reservation));

                break;

            case OrderStatus.Occupied: //// Dine In
            case OrderStatus.Processing:

                self.optionValues.push(new Status("Dine In", OrderStatus.Occupied));
                //self.optionValues.push(new Status("Processing", OrderStatus.Processing));
                self.optionValues.push(new Status("Completed", OrderStatus.Completed));

                break;

            case OrderStatus.Completed: //// Completed

                self.optionValues.push(new Status("Completed", OrderStatus.Completed));

                break;

                self.selectedOptionValue(self.optionValues.getStatusByStatusId(statusId));
                self.orderStatus(statusId);
        }
    }

    self.removeItem = function (item) {
        var index = self.items.indexOf(item);
        self.items.splice(index, 1);
    }

    self.editItem = function (cart)
    {
        productViewModel.findProduct(cart.id(), cart.optionIds, self.items.indexOf(cart));
    }      

    self.save = function ()
    {
        var amountPaid = 0;
        var basket = [];
        var itemsToSave = $.map(self.items(), function (data)
        {
            var item = {
                MenuId: data.id(),
                Name: data.name(),
                IsProcessed: data.isProcessed(),
                Price: data.sellingprice
            }

            basket.push(item)
        });
       
        if (parseFloat(self.amountPaid()) > parseFloat(self.balance()))
        {
            var amountPaid = parseFloat(self.balance());

            self.amountPaid(amountPaid.toFixed(2));
        }
                                  
        var orderSummary = {         
            OrderId: self.orderId(),
            PaymentMethodId: selectedPaymentMethod(),
            AmountPaid: self.amountPaid(),      
            OrderStatusId: self.selectedOptionValue().OrderStatusId,
            orderedItems: basket,
            IpAddress: $("#IpAddress").val(),
            TableId: $("#TableId").val(),
            OrderTypeId: $("#OrderTypeId").val(),
            Note: $("#Note").val(),
            TotalDiscount: self.totalDiscount(),
            DiscountRate: self.discountRate(),
            TotalTax: self.totalTax(),
            TaxRate: self.taxRate(),
            Balance: self.balance(),
            DeliveryCost: 0,
            IsKitchenReceipt: self.isKitchenPrinter(),
            IsPrintReceipt: self.isReceiptPrinter()
        }

        js.ajaxPost(settings.orderUrl,
            {
                orderedItems: basket,
                entityToCreate: orderSummary

            }).then(
            function (data) {
                                                               
                if (data.receipt.Order == OrderStatus.Completed) {

                    tableViewModel.remove(data.receipt.Order);

                } else {

                    tableViewModel.update(data.receipt.Order);
                }
                                              
                $("#tdSalesOrder").DataTable().fnDraw();

                $("#btnPaymentCloseDialog").trigger("click");
                $("#btnback").trigger("click");                          

                $("#MainCollapsible").accordion("activate", 0);
            }
        );
    };
};

function CartModel(productId, data) {
               
    var self = this;
    self.parent = null;
    self.index = ko.computed(function () {
        var index = cartViewModel.items.indexOf(self);
        return index;
    });

    self.id = ko.observable(productId);
    self.name = ko.observable(data.ItemName);
    self.isProcessed = ko.observable(data.IsProcessed);
    self.quantity = ko.observable(1);
    self.sellingprice = ko.observable(data.Price);
       
    if (data.OptionIds != null)
    {
        self.optionIds = data.OptionIds;
    } else {
        self.optionIds = ko.observable(0);
    }
    
    self.price = ko.observable(suftnet_grid.formatCurrency(self.sellingprice()));          
  
    self.lineTotal = ko.computed(function () {
        var total = (self.sellingprice() * self.quantity());
        return total;
    });   
    
};

function getOrderDetails(orderId)
{  
    var param = {
        Id: orderId
    };
        
    js.ajaxGet(settings.OrderDetailUrl, param).then(function (data) {

        cartViewModel.reset();       

        if (data.dataobject.Order != null)
        {
            $("#OrderTypeId").val(data.dataobject.Order.OrderTypeId);

            cartViewModel.orderType(data.dataobject.Order.OrderTypeId);
            cartViewModel.orderId(data.dataobject.Order.Id);
            cartViewModel.paid(data.dataobject.Order.Payment);

            cartViewModel.totalDiscount(data.dataobject.Order.TotalDiscount);
            cartViewModel.discountRate(data.dataobject.Order.Discount);

            cartViewModel.totalTax(data.dataobject.Order.TotalTax);
            cartViewModel.taxRate(data.dataobject.Order.Tax);

            if (data.dataobject.Order.OrderTypeId != OrderType.Delivery)
            {
                cartViewModel.getSelectedStatus(data.dataobject.Order.StatusId);
            }
        }

        $.each(data.dataobject.OrderDetail, function (i, value)
        {
            var item = new CartModel(value.MenuId, value);
            cartViewModel.addItem(item);
        });

        $("#MainCollapsible").accordion("activate", 1);

    }).catch(function (error) {

        console.log(error);
    });;
}

function Status(text, value) {
    this.OrderStatusId = value;
    this.Title = text;
};

function PaymentMethod(text, value) {
    this.PaymentMethodId = value;
    this.Title = text;
};



