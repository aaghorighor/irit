var CategoryViewModel = function () {
    var self = this;
    self.catgories = ko.observableArray([]);

    self.addCategory = function (item) {
       
        self.catgories.push(item);

    }.bind(self);

    self.getItems = function (item)
    {                   
        var param = {
            Id: item.ID()
        };       
       
        js.ajaxGet(settings.ProductUrl, param).then(function (data)
        {
            productViewModel.clear();

            $.each(data.dataobject, function (i, value) {
                var item = new ProductModel(value);
                productViewModel.addProduct(item);
            });

        });            
    }    
}

function CategoryModel(data) {
       
    var self = this;

    self.index = ko.computed(function () {
        var index = categoryViewModel.catgories.indexOf(self);
        return index;
    });

    self.Name = ko.observable(data.Name);
    self.ID = ko.observable(data.Id);
   
    if (data.ImageUrl != null)
    {
        self.ImageUrl = ko.observable(settings.contentUrl + "/" + data.ImageUrl);
    } else {
        self.ImageUrl = ko.observable(settings.contentUrl + "/fec12adb-208a-4d1b-8086-e53ea3e0a022.jpg");
    }  
}

function InitCategory(url)
{            
    js.ajaxGet(url).then(function (data)
    {
        $.each(data.dataobject, function (i, value) {          

            var categoryModel = new CategoryModel(value);
            categoryViewModel.addCategory(categoryModel);
        });

    });   
}


