var MenuAddonModel = function () {
    var self = this;
    self.menuAddons = ko.observableArray([]);
    self.ProductId = ko.observable();
    self.ProductName = ko.observable();
    self.SellingPrice = ko.observable(0);
    self.SellingPrice = ko.observable(0);
 
    self.clear = function ()
    {
        self.ProductId('');
        self.ProductName('');      
        self.menuAddons.removeAll();
        settings.optionModel = 0;
        settings.index = -1;

    }.bind(self);

    self.close = function () {

        self.menuAddons.removeAll();
        settings.optionModel = 0;      

        $("#MenuAddonDialog").dialog("close");
       
    }.bind(self);

    self.addMenuAddon = function (item)
    {
        if (item.active())
        {
            self.menuAddons.push(item);
        }
      
    }.bind(self);

    self.submit = function()
    {
        var optionNames = '';
        var optionIds = '';
        var total = 0;
        var flag = false;       

        $.map(self.menuAddons(), function (option) {            

            if (option.selected())
            {                
                if (optionNames == '')
                {
                    optionNames = optionNames + option.name();
                    optionIds = optionIds + option.Id();
                } else {
                    optionNames = optionNames + "," + option.name();
                    optionIds = optionIds + "," + option.Id();
                }

                total += option.price()
            }           
        });       

        if (optionNames == '') {
            optionNames = self.ProductName()
        } else {
            optionNames = self.ProductName() + ", " + optionNames;
        }
      
        total += self.SellingPrice();

        if (settings.optionModel == 1)
        {
            var match = ko.utils.arrayFirst(cartViewModel.items(), function (item)
            {               
                return settings.index === item.index();
            });

            match.name(optionNames);
            match.optionIds =optionIds;
            match.price(suftnet_grid.formatCurrency(total));
            match.sellingprice(total);
            
            settings.optionModel = 0;

        } else {
            productViewModel.getProduct(self.ProductId(), total, optionNames, optionIds);
        } 
       
        self.menuAddons.removeAll();

        self.close();

    };
}

function MenuAddOnItem(data) {
                 
    var self = this;
    
    self.index = ko.computed(function () {
        var index = menuAddonModel.menuAddons.indexOf(self);
        return index;
    });    

    if (data.selected != null)
    {
        self.selected = ko.observable(data.selected);
    } else {
        self.selected = ko.observable(false);
    }
   
    self.Id = ko.observable(data.Id);
    self.productId = ko.observable(data.ProductId);
    self.name = ko.observable(data.Name);   
    self.price = ko.observable(data.Price);
    self.active = ko.observable(data.Active);
    self.addonType = ko.observable(data.AddonType);
    self.sellingPrice = ko.computed(function () {
        return suftnet_grid.formatCurrency(data.Price);
    });   
}



