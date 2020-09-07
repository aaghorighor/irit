var DeliveryViewModel = function () {
    var self = this;
    self.deliverys = ko.observableArray([]);

    self.clear = function (item) {
        self.deliverys.removeAll();
    };

    self.clearAll = function () {
        self.deliverys.removeAll();
    };

    self.addDelivery = function (data) {
        var deliveryModel = new DeliveryModel(data);
        deliveryViewModel.add(deliveryModel);
    };

    self.add = function (item) {
        
        var isItem = true;

        $.each(self.deliverys(), function () {
            if (item.Id() == this.Id())
            {
                isItem = false;
            }
        });             

        if (isItem)
        {
            self.deliverys.push(item);
        }           

    }.bind(self);

    self.remove = function (orderId)
    {      
        $.each(self.deliverys(), function (i, item) {
                       
            var itemId = parseInt(item.Id());

            if (orderId == itemId) {
                var index = self.deliverys.indexOf(item);

                self.deliverys.splice(index, 1);
            }
        });       
    };

    self.updateStatus = function (data) {
        var deliveryModel = new DeliveryModel(data);

        var match = ko.utils.arrayFirst(self.deliverys(), function (item) {
            return deliveryModel.Id() === item.Id();
        });

        match.Status(deliveryModel.Status());
        match.StatusId(deliveryModel.StatusId());
    };

    self.editDelivery = function (data) {
        var deliveryModel = new DeliveryModel(data);

        var match = ko.utils.arrayFirst(self.deliverys(), function (item) {
            return deliveryModel.Id() === item.Id();
        });

        match.Reference(deliveryModel.Reference());
        match.Time(deliveryModel.Time());
        match.FirstName(deliveryModel.FirstName());
        match.LastName(deliveryModel.LastName());
        match.Mobile(deliveryModel.Mobile());
        match.Id(deliveryModel.Id());
        match.DeliveryCost(deliveryModel.DeliveryCost());
        match.Distance(deliveryModel.Distance());
        match.Duration(deliveryModel.Duration());
        match.Address(deliveryModel.Address());
        match.OrderTypeId(deliveryModel.OrderTypeId());
        match.Status(deliveryModel.Status());
        match.StatusId(deliveryModel.StatusId());
        match.Payment(deliveryModel.Payment());
        match.CreatedOn(deliveryModel.CreatedOn());

    };

    self.edit = function (model)
    {
        iuHelper.resetForm("#orderDeliveryform");

        $("#StatusId").val(model.StatusId());
        $("#OrderTypeId").val(model.OrderTypeId());
      
        $("#DeliveryId").val(model.Id());

        $("#orderDeliveryform #FirstName").val(model.FirstName());
        $("#orderDeliveryform #LastName").val(model.LastName());

        $("#orderDeliveryform #Mobile").val(model.Mobile());

        $("#DeliveryCost").val(model.DeliveryCost());

        $("#DeliveryTime").val(model.Time());
        $("#DeliveryDistance").val(model.Distance());
        $("#DeliveryDuration").val(model.Duration());      
        $("#DeliveryAddress").val(model.Address());

        $("#DeliveryDate").val(model.CreatedOn());        

        $("#btnSubmitAddress").trigger("click");

        $("#deliveryOrderDialog").dialog("open");

    };

    self.view = function (model)
    {     
        $("#SalesOrderId").val(model.Id());
        $("#OrderStatusId").val(model.StatusId());

        $("#tableNo").text("Delivery");        
        $("#orderNo").text(model.Id());

        settings.orderId = model.Id();

        cartViewModel.isDelivery(true);
        cartViewModel.delivery(model.DeliveryCost());

        getOrderDetails(model.Id());     
    };

    self.cancel = function (model)
    {
        js.ajaxPost(settings.deleteUrl, { Id: model.Id() }).then(function (data)
        {
            self.remove(model.Id());

            $("#tdSalesOrder").DataTable().fnDraw();
        });       
    };

    self.completed = function (model)
    {       
        js.ajaxPost(settings.changeDeliveryStatusUrl, { orderId: model.Id(), StatusId: OrderStatus.Completed }).then(function (data)
        {
            self.remove(model.Id());

            $("#tdSalesOrder").DataTable().fnDraw();
        });      
    };
}

function DeliveryModel(data) {
       
    var self = this;

    self.index = ko.computed(function () {
        var index = deliveryViewModel.deliverys.indexOf(self);
        return index;
    });
    
    self.Reference = ko.observable(data.Reference);  
    self.FirstName = ko.observable(data.FirstName);
    self.LastName = ko.observable(data.LastName);
    self.Time = ko.observable(data.Time);
    self.Id = ko.observable(data.Id);
    self.DeliveryCost = ko.observable(data.DeliveryCost);
    self.Distance = ko.observable(data.DeliveryDistance);
    self.Duration = ko.observable(data.DeliveryDuration);
    self.Address = ko.observable(data.DeliveryAddress);
    self.Status = ko.observable(data.Status);
    self.Mobile = ko.observable(data.Mobile);
    self.StatusId = ko.observable(data.StatusId);
    self.OrderTypeId = ko.observable(data.OrderTypeId);
    self.Payment = ko.observable(data.Payment);
    self.CreatedOn = ko.observable(data.CreatedOn);
   
}

function LoadDeliveryOrders() {
       
    js.ajaxGet(settings.deliveryUrl).then(function (data)
    { 
       deliveryViewModel.clearAll();

        $.each(data.dataobject, function (i, value) {         
       
            var deliveryModel = new DeliveryModel(value);

            deliveryViewModel.add(deliveryModel);         
        });
   });  
}


