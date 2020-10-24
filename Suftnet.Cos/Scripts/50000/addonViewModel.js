var AddonViewModel = function () {
    var self = this;

    self.menuAddons = ko.observableArray([]);
    self.MenuId = ko.observable();
    self.Name = ko.observable();
    self.SellingPrice = ko.observable(0);
  
    self.clear = function ()
    {
        self.MenuId('');
        self.Name('');      
        self.menuAddons.removeAll();

        constants.addOn.optionModel = 0;
        constants.addOn.index = -1;

    }.bind(self);

    self.close = function () {

        self.menuAddons.removeAll();
        constants.addOn.optionModel = 0;      

        $("#addonDialog").dialog("close");
       
    }.bind(self);

    self.add = function (item)
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
            optionNames = self.Name()
        } else {
            optionNames = self.Name() + ", " + optionNames;
        }
      
        total += self.SellingPrice();

        if (constants.addOn.optionModel == 1)
        {
            var match = ko.utils.arrayFirst(cartViewModel.items(), function (item)
            {               
                return constants.addOn.index === item.index();
            });
           
            match.name(optionNames);
            match.optionIds =optionIds;
            match.price(suftnet_grid.formatCurrency(total));
            match.sellingprice(total);
            
            constants.addOn.optionModel = 0;

        } else {
            menuViewModel.get(self.MenuId(), total, optionNames, optionIds);
        } 
       
        self.menuAddons.removeAll();

        self.close();

    };
}

function CreateAddOn(data) {
                 
    var self = this;
    
    self.index = ko.computed(function () {
        var index = addonViewModel.menuAddons.indexOf(self);
        return index;
    });    

    if (data.selected != null)
    {
        self.selected = ko.observable(data.selected);
    } else {
        self.selected = ko.observable(false);
    }
   
    self.Id = ko.observable(data.Id);
    self.menuId = ko.observable(data.MenuId);
    self.name = ko.observable(data.Name);   
    self.price = ko.observable(data.Price);
    self.active = ko.observable(data.Active);
    self.addonType = ko.observable(data.AddonType);

    self.sellingPrice = ko.computed(function () {
        return suftnet_grid.formatCurrency(data.Price);
    });   
}



