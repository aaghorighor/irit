var ProductViewModel = function () {
    var self = this;
    self.products = ko.observableArray([]);

    self.clear = function () {

        self.products.removeAll();

    }.bind(self);

    self.getProduct = function(productId, price, name, OptionIds)
    {                             
        for (var i = 0; i < self.products().length; i++)
        {           
            if (self.products()[i].ProductId() === productId) 
            {
                var param = {
                    ItemName: name,
                    OptionIds :OptionIds,
                    Price: price,
                    ProductId: productId                                
                };             

                CreateOrder(productId, param);

                break;
            }
        };       
    };

    self.findProduct = function (productId, optionIds, index)
    {      
        if (optionIds.length <= 0)
        {
            return false;
        }

        var options = optionIds.split(",");

        if (cartViewModel.orderStatus() == OrderStatus.Completed) {
                      
            if (cartViewModel.orderType() != OrderType.Bar && cartViewModel.orderType() != OrderType.Takeway) {
                return false;
            }
        }
               
        for (var i = 0; i < self.products().length; i++)
        {
            if (self.products()[i].ProductId() === productId)
            {
                var product = self.products()[i];

                if (product.IsMenuAddons())
                {
                    menuAddonModel.clear();
                    menuAddonModel.ProductName(product.Title());
                    menuAddonModel.ProductId(product.ProductId());
                    menuAddonModel.SellingPrice(product.Price());
                 
                    $.each(product.MenuAddons(), function (i, addOn)
                    {                       
                        $.each(options, function (i, option)
                        {
                            if (addOn.Id == option)
                            {                                                           
                                addOn.selected = true;
                            }                          
                        });

                        menuAddonModel.addMenuAddon(new MenuAddOnItem(addOn));
                        addOn.selected = false;
                                                           
                    });

                    settings.optionModel = 1; // edit model
                    settings.index = index; // cart index

                    $("#MenuAddonDialog").dialog("open");
                }                

                break;
            }
        };
    };

    self.addProduct = function (item) {

        self.products.push(item);

    }.bind(self);

    self.addToOrder = function (item)
    {       
        if (cartViewModel.orderStatus() == OrderStatus.Completed) {
                       
            if (cartViewModel.orderType() != OrderType.Bar && cartViewModel.orderType() != OrderType.Takeway) {
                return false;
            }
        }
      
        var param = {
            ItemName: item.Title(),
            Price: item.Price(),
            ProductId: item.ProductId()
                    
        };
           
        if (item.IsMenuAddons())
        {
            menuAddonModel.clear();
            menuAddonModel.ProductName(item.Title());
            menuAddonModel.ProductId(item.ProductId());
            menuAddonModel.SellingPrice(item.Price());

            $.each(item.MenuAddons(), function (i, value)
            {            
                var createAddon = new MenuAddOnItem(value);
                    menuAddonModel.addMenuAddon(createAddon);
            });          

            $("#MenuAddonDialog").dialog("open");

        } else {
           
            CreateOrder(item.ProductId(), param);           
        }  
    };    
}

function CreateOrder(productId, param)
{   
    var cartModel = new CartModel(productId, param);
    cartViewModel.addItem(cartModel);
}

function ProductModel(data) {
                    
    var self = this;
    
    self.index = ko.computed(function () {
        var index = productViewModel.products.indexOf(self);
        return index;
    });
        
    if (data.AddonDto.length > 0)
    {
        self.IsMenuAddons = ko.observable(true);
        self.MenuAddons = ko.observableArray(data.AddonDto);
    } else {
        self.IsMenuAddons = ko.observable(false);
        self.MenuAddons = ko.observableArray([]);
    }
    
    self.Title = ko.observable(data.Name);
    self.Description = ko.observable(data.Description);
    self.Price = ko.observable(data.Price);
    self.Category = ko.observable(data.Category);
    self.ProductId = ko.observable(data.Id);
       
    self.sellingPrice = ko.computed(function () {
        return suftnet_grid.formatCurrency(self.Price());
    });   
}

function InitProduct() {
    
    js.ajaxGet(settings.initProductUrl).then(function (data)
    {     
        $.each(data.dataobject, function (i, value) {          
                    
            productViewModel.clear();

            $.each(data.dataobject, function (i, value) {
                var item = new ProductModel(value);                               
                productViewModel.addProduct(item);
            });
        });

    });
}



