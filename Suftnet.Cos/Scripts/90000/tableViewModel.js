var tableViewModel = function ()
{
    var self = this;
    self.tables = ko.observableArray([]);

    self.addTable = function (item) {
       
        self.tables.push(item);

    }.bind(self);

    self.getItem = function (order)
    {
        if (order.OrderId() == "") {           
                     
            $("#TableId").val(this.Id());

            iuHelper.resetForm("#form"); 
            $("#orderDialog").dialog("open");

        } else {    
           
            window.location.href = $("#cartUrl").attr("data-cartUrl") + "/" + order.OrderId() + "/" + constants.orderType.dineIn + "/" + "Dine-In" + "/" + constants.orderStatus.occupied;
        }
    };

    self.update = function(data)
    {       
        $.each(self.tables(), function ()
        {                
            if (this.Id() == data.tableId)
            {            
                if (data.orderStatusId == constants.orderStatus.completed)
                {
                    this.OrderId(0);
                    this.Status("Free");

                } else if (data.orderStatusId == constants.orderStatus.occupied || data.orderStatusId == constants.orderStatus.Processing)
                {                  
                    this.OrderId(data.orderId);
                    this.Status("Dine In");
                }        
               
                return false;
            }
        });
    }

    self.remove = function (data) {
       
        $.each(self.tables(), function () {

            if (this.Id() == data.TableId) {

                this.Status("Free");
                this.OrderId("");

                return false;
            }
        });
    }
}

function tableItem(data) {
       
    var self = this;

    self.index = ko.computed(function () {
        var index = _tableViewModel.tables.indexOf(self);
        return index;
    });

    self.Number = ko.observable(data.Number);
    self.Size = ko.observable(data.Size);
    self.Id = ko.observable(data.Id);
             
    if (data.OrderId != null)
    {       
        self.Status = ko.observable("Dine In");
        self.OrderId = ko.observable(data.OrderId);
    } else {
        self.Status = ko.observable("Free");
        self.OrderId = ko.observable("");
    } 
}




