
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

    self.totalDiscount = ko.observable(0);
    self.discountRate = ko.observable(0);
    self.deliveryCost = ko.observable(0);

    self.taxRate = ko.observable(0);
    self.totalTax = ko.observable(0);
        
    self.items = ko.observableArray([]);

    self.isReceiptPrinter = ko.observable(false),
    self.isKitchenPrinter = ko.observable(false),

    self.selectedOptionValue = ko.observable();
    self.selectedPaymentMethod = ko.observable();

    self.optionValues = ko.observableArray([new Status("Completed", constants.orderStatus.completed), new Status("Occupied", constants.orderStatus.occupied)]);
    self.paymentMethods = ko.observableArray([new PaymentMethod("Card", constants.paymentMethod.card), new PaymentMethod("Cash", constants.paymentMethod.cash)]);
             
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

        if (self.deliveryCost() > 0) {
            total = parseFloat(total) + parseFloat(self.deliveryCost());
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

    self.displayDeliveryCost = ko.computed(function () {
        return suftnet_grid.formatCurrency(self.deliveryCost());
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
      
    self.add = function (item) {
       
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
       
    self.getSelectedStatus = function(statusId, orderTypeId)
    {                                
        switch (orderTypeId)
        {
            case constants.orderType.reservation: 

                self.optionValues.removeAll();

                self.optionValues.push(new Status("Reserved", constants.orderStatus.reserved));  
                self.optionValues.push(new Status("Cancelled", constants.orderStatus.cancelled));

                break;

            case constants.orderType.dineIn: 

                self.optionValues.removeAll();

                self.optionValues.push(new Status("Occupied", constants.orderStatus.occupied));  
                self.optionValues.push(new Status("Processing", constants.orderStatus.processing));             
                self.optionValues.push(new Status("Completed", constants.orderStatus.completed));  
                self.optionValues.push(new Status("Cancelled", constants.orderStatus.cancelled));

                break;

            case constants.orderType.delivery:

                self.optionValues.removeAll();

                self.optionValues.push(new Status("Pending", constants.orderStatus.pending));
                self.optionValues.push(new Status("Processing", constants.orderStatus.processing));
                self.optionValues.push(new Status("Ready", constants.orderStatus.ready));
                self.optionValues.push(new Status("Dispatched", constants.orderStatus.dispatched));           
                self.optionValues.push(new Status("Transit", constants.orderStatus.transit));
                self.optionValues.push(new Status("Delivered", constants.orderStatus.delivered));
                self.optionValues.push(new Status("Cancelled", constants.orderStatus.cancelled));

                break;                       
          
            default:
                break;

        }   
    

        self.selectedOptionValue(self.optionValues.getStatusByStatusId(statusId));
        self.orderStatus(statusId);
    } 

    self.remove = function (item) {
        var index = self.items.indexOf(item);
        self.items.splice(index, 1);
    }

    self.edit = function (cart)
    {      
        menuViewModel.find(cart.id(), cart.optionIds, self.items.indexOf(cart));
    }      

    self.save = function ()
    {
        var amountPaid = 0;
        var basket = [];

        $.map(self.items(), function (data)
        {
            var item = {
                MenuId: data.id(),
                Name: data.name(),
                IsProcessed: data.isProcessed(),
                Price: data.sellingprice,
                Quantity: data.quantity()
            }     

            basket.push(item)
        });
       
        if (parseFloat(self.amountPaid()) > parseFloat(self.balance()))
        {
            var amountPaid = parseFloat(self.balance());

            self.amountPaid(amountPaid.toFixed(2));
        }
                                              
        var orderSummary = {         
            OrderId: $("#orderId").attr("data-orderId"),
            OrderTypeId: $("#orderTypeId").attr("data-orderTypeId"),
            Note: $("#Note").val(),
            PaymentMethodId: self.selectedPaymentMethod(),
            AmountPaid: self.amountPaid(),                 
            DiscountRate: self.discountRate(),
            TotalTax: self.totalTax(),
            TaxRate: self.taxRate(),
            Balance: self.balance(),
            DeliveryCost: $("#deliveryCost").attr("data-deliveryCost"),
            OrderStatusId: self.selectedOptionValue().OrderStatusId,
            orderedItems: basket,
            TotalDiscount: self.totalDiscount()
        }

        js.ajaxPost($("#createUrl").attr("data-createUrl"),
            { entityToCreate: orderSummary
            }).then(
            function (data) {                                                               
               cartViewModel.reset(); 
               $("#paymentDialog").dialog("close");

                    setTimeout(function () {
                        window.location.href = $("#dineInUrl").attr("data-dineInUrl");
                    }, 2500);             
            
            }
        );
    };
};

function CreateCart(menuId, data) {
                      
    var self = this;
    self.parent = null;
    self.index = ko.computed(function () {
        var index = cartViewModel.items.indexOf(self);
        return index;
    });

    self.id = ko.observable(menuId);
    self.name = ko.observable(data.ItemName);
    self.isProcessed = ko.observable(data.IsProcessed);
    self.quantity = ko.observable(data.Quantity);
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

function loadCart()
{     
    var param = {
        Id: $("#orderId").attr("data-orderId")
    };  
        
    js.ajaxGet($("#loadUrl").attr("data-loadUrl"), param).then(function (data) {
              
        cartViewModel.reset();           

        if (data.dataobject.Baskets != undefined || data.dataobject.Baskets != null) {

            if (data.dataobject.Baskets.length > 0)
            {
                cartViewModel.orderType(data.dataobject.Order.OrderTypeId);
                cartViewModel.orderId(data.dataobject.Order.Id);
                cartViewModel.paid(data.dataobject.Order.Paid);

                cartViewModel.totalDiscount(data.dataobject.Order.TotalDiscount);
                cartViewModel.discountRate(data.dataobject.Order.DiscountRate);
                cartViewModel.deliveryCost(data.dataobject.Order.DeliveryCost);

                cartViewModel.totalTax(data.dataobject.Order.TotalTax);
                cartViewModel.taxRate(data.dataobject.Order.TaxRate);

                cartViewModel.getSelectedStatus(data.dataobject.Order.StatusId, data.dataobject.Order.OrderTypeId);
            
                $.each(data.dataobject.Baskets, function (i, value) {
                    var item = new CreateCart(value.MenuId, value);
                    cartViewModel.add(item);
                });
            }   
        }           

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



