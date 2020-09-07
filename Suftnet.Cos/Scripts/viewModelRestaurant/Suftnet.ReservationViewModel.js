var ReservationViewModel = function () {
    var self = this;
    self.reservations = ko.observableArray([]);

    self.clear = function (item) {
        self.reservations.removeAll();
    };

    self.clearAll = function () {
        self.reservations.removeAll();
    };

    self.addReservation = function (data)
    {       
        var reservationModel = new ReservationModel(data);
        reservationViewModel.add(reservationModel);      
    };

    self.editReservation = function (data)
    {      
        var reservationModel = new ReservationModel(data);
            
        var match = ko.utils.arrayFirst(self.reservations(), function (item) {
            return reservationModel.Id() === item.Id();
        });     

        match.Reference(reservationModel.Reference());
        match.NumberOfGuest(reservationModel.NumberOfGuest());
        match.Time(reservationModel.Time());
        match.FirstName(reservationModel.FirstName());
        match.LastName(reservationModel.LastName());
        match.Mobile(reservationModel.Mobile());
        match.Id(reservationModel.Id());
        match.Table(reservationModel.Table());

        match.TableId(reservationModel.TableId());
        match.OrderTypeId(reservationModel.OrderTypeId());
        match.Status(reservationModel.Status());
        match.StatusId(reservationModel.StatusId());
        match.Payment(reservationModel.Payment());
        match.CreatedOn(reservationModel.CreatedOn());
      
    };  

    self.add = function (item) {
        
        var isItem = true;

        $.each(self.reservations(), function () {
            if (item.Id() == this.Id())
            {
                isItem = false;
            }
        });             

        if (isItem)
        {
            self.reservations.push(item);
        }           

    }.bind(self);

    self.remove = function (orderId)
    {      
        $.each(self.reservations(), function (i, item) {
                       
            var itemId = parseInt(item.Id());

            if (orderId == itemId) {
                var index = self.reservations.indexOf(item);
                self.reservations.splice(index, 1);
            }
        });       
    };

    self.edit = function (model) {

        iuHelper.resetForm("#reservationOrderform");

        $("#StatusId").val(model.StatusId());
        $("#OrderTypeId").val(model.OrderTypeId());

        $("#ReservationTime").val(model.Time());
        $("#reservationOrderform #NumberOfGuest").val(model.NumberOfGuest());
        
        $("#ReservationId").val(model.Id());

        $("#ReservationDate").val(model.CreatedOn());    
        $("#ReservationTableId").val(model.TableId());

        $("#reservationOrderform #FirstName").val(model.FirstName());
        $("#reservationOrderform #LastName").val(model.LastName());

        $("#reservationOrderform #Mobile").val(model.Mobile());
 
        $("#reservationOrderDialog").dialog("open");

    };

    self.view = function (order)
    {       
        $("#SalesOrderId").val(order.Id());
        $("#OrderStatusId").val(order.StatusId());
        $("#tableNo").text(order.Table());
        $("#tableStatus").text(order.StatusId());

        $("#TableId").val(order.TableId());
        $("#orderNo").text(order.Id());

        settings.orderId = order.Id();

        getOrderDetails(order.Id());    
    };

    self.cancel = function (order)
    {
        js.ajaxPost(settings.deleteUrl, { Id: order.Id() }).then(function (data)
        {
            self.remove(order.Id());

            $("#tdSalesOrder").DataTable().fnDraw();
        });       
    };

    self.dineIn = function (order)
    {
        js.ajaxPost(settings.changeStatusUrl, { orderId: order.Id(), StatusId: OrderStatus.Occupied, tableId: order.TableId()  }).then(function (data)
        {
            self.remove(order.Id());

            tableViewModel.update({ TableId: order.TableId(), StatusId: OrderStatus.Occupied, Id: order.Id() });

            $("#tdSalesOrder").DataTable().fnDraw();
        });      
    };
}

function ReservationModel(data) {
       
    var self = this;
   
    self.index = ko.computed(function () {
        var index = reservationViewModel.reservations.indexOf(self);
        return index;
    });
        
    self.Reference = ko.observable(data.Reference);
    self.NumberOfGuest = ko.observable(data.NumberOfGuest);
    self.FirstName = ko.observable(data.FirstName);
    self.LastName = ko.observable(data.LastName);
    self.Mobile = ko.observable(data.Mobile);
    self.Time = ko.observable(data.Time);
    self.Id = ko.observable(data.Id);
    self.Table = ko.observable(data.Table);
    self.TableId = ko.observable(data.TableId);
    self.OrderTypeId = ko.observable(data.OrderTypeId);
    self.Status = ko.observable(data.Status);
    self.StatusId = ko.observable(data.StatusId);
    self.Payment = ko.observable(data.Payment);
    self.CreatedOn = ko.observable(data.CreatedOn);    

}

function LoadReserveOrder() {
         
   js.ajaxGet(settings.reserveUrl).then(function (data)
   {      
       reservationViewModel.clearAll();

        $.each(data.dataobject, function (i, value) {          
          
            var reservationModel = new ReservationModel(value);

            reservationViewModel.add(reservationModel);
        });
   });  
}


