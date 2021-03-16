var CategoryViewModel = function () {
    var self = this;
    self.catgories = ko.observableArray([]);

    self.add = function (item) {
       
        self.catgories.push(item);

    }.bind(self);

    self.get = function (item)
    {                   
        var param = {
            categoryId: item.ID()
        };       
       
        js.ajaxGet($("#menuByCategoryUrl").attr("data-menuByCategoryUrl"), param).then(function (data)
        {
            menuViewModel.clear();

            $.each(data.dataobject, function (i, value) {
                var item = new CreateMenu(value);
                menuViewModel.add(item);
            });

        });            
    }    
}

function CreateCategory(data) {
       
    var self = this;

    self.index = ko.computed(function () {
        var index = categoryViewModel.catgories.indexOf(self);
        return index;
    });

    self.Name = ko.observable(data.Name);
    self.ID = ko.observable(data.Id);
   
    if (data.ImageUrl != null)
    {
        self.ImageUrl = ko.observable($("#imageUrl").attr("data-imageUrl") + "/" + data.ImageUrl);
    } else {
        self.ImageUrl = ko.observable($("#imageUrl").attr("data-imageUrl") + "/fec12adb-208a-4d1b-8086-e53ea3e0a022.jpg");
    }  
}

function loadCategory()
{            
    js.ajaxGet($("#categoryUrl").attr("data-categoryUrl")).then(function (data)
    {
        $.each(data.dataobject, function (i, value) {          

            var model = new CreateCategory(value);
            categoryViewModel.add(model);
        });

    });   
}


