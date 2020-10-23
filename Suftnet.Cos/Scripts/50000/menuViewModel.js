var MenuViewModel = function () {
    var self = this;
    self.menues = ko.observableArray([]);

    self.clear = function () {

        self.menues.removeAll();

    }.bind(self);

    self.get = function(menuId, price, name, OptionIds)
    {                             
        for (var i = 0; i < self.menues().length; i++)
        {           
            if (self.menues()[i].MenuId() === menuId) 
            {
                var param = {
                    ItemName: name,
                    OptionIds :OptionIds,
                    Price: price,
                    MenuId: menuId                                
                };             

                CreateOrder(menuId, param);

                break;
            }
        };       
    };

    self.find = function (menuId, optionIds, index)
    {      
        if (optionIds.length <= 0)
        {
            return false;
        }

        var options = optionIds.split(",");

        if (cartViewModel.orderStatus() == constants.orderStatus.completed) {

            if (cartViewModel.orderType() != constants.orderType.bar && cartViewModel.orderType() != constants.orderType.takeAway) {
                return false;
            }
        }
               
        for (var i = 0; i < self.menues().length; i++)
        {
            if (self.menues()[i].MenuId() === menuId)
            {
                var menu = self.menues()[i];

                if (menu.IsMenuAddons())
                {
                    addonViewModel.clear();
                    addonViewModel.Name(menu.Title());
                    addonViewModel.MenuId(menu.MenuId());
                    addonViewModel.SellingPrice(menu.Price());
                 
                    $.each(menu.MenuAddons(), function (i, addOn)
                    {                       
                        $.each(options, function (i, option)
                        {
                            if (addOn.Id == option)
                            {                                                           
                                addOn.selected = true;
                            }                          
                        });

                        menuAddonModel.add(new CreateAddOn(addOn));
                        addOn.selected = false;                                                           
                    });

                    constants.addOn.optionModel = 1; // edit model
                    constants.addOn.index = index; // cart index

                    $("#addonDialog").dialog("open");
                }                

                break;
            }
        };
    };

    self.add = function (item) {

        self.menues.push(item);

    }.bind(self);

    self.addToOrder = function (item)
    {       
        if (cartViewModel.orderStatus() == constants.orderStatus.completed) {
                       
            if (cartViewModel.orderType() != constants.orderType.bar && cartViewModel.orderType() != constants.orderType.takeAway) {
                return false;
            }
        }
      
        var param = {
            ItemName: item.Title(),
            Price: item.Price(),
            MenuId: item.MenuId()                    
        };
           
        if (item.IsMenuAddons())
        {
            addonViewModel.clear();
            addonViewModel.Name(item.Title());
            addonViewModel.MenuId(item.MenuId());
            addonViewModel.SellingPrice(item.Price());

            $.each(item.MenuAddons(), function (i, value)
            {            
                var create = new CreateAddOn(value);
                addonViewModel.add(create);
            });          

            $("#addonDialog").dialog("open");

        } else {
           
            CreateOrder(item.MenuId(), param);           
        }  
    };    
}

function CreateOrder(menuId, param)
{   
    var cartModel = new CreateCart(menuId, param);
    cartViewModel.add(cartModel);
}

function CreateMenu(data) {
                    
    var self = this;
    
    self.index = ko.computed(function () {
        var index = menuViewModel.menues.indexOf(self);
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
    self.MenuId = ko.observable(data.Id);
       
    self.sellingPrice = ko.computed(function () {
        return suftnet_grid.formatCurrency(self.Price());
    });   
}

function loadMenu() {
    
    js.ajaxGet($("#defaultMenuUrl").attr("data-defaultMenuUrl")).then(function (data)
    {     
        menuViewModel.clear();
       
        $.each(data.dataobject, function (i, value) {                             
          
            var item = new CreateMenu(value);                  
            menuViewModel.add(item);
        });

    });
}



