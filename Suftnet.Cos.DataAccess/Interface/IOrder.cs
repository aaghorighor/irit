namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;    

    public interface IOrder 
    {
        OrderDto Get(Guid Id);
        bool Delete(Guid orderId);
        Guid Insert(OrderDto entity);
        bool Update(OrderDto entity);
        bool UpdateSalesOrder(OrderDto entity);
        bool UpdateOrderStatus(Guid tenantId, Guid orderId, Guid statusId, Guid orderTypeId, DateTime createDt, string createdBy);
        bool UpdateOrderStatus(Guid orderId, Guid statusId, DateTime createDt, string createdBy, Guid tenantId);
        List<OrderDto> GetAll(Guid orderTypeId, Guid tenantId, int iskip, int itake, string isearch);
        List<OrderDto> GetAll(Guid orderTypeId, Guid tenantId, int iskip, int itake);        
        IEnumerable<OrderDto> GetAllOrderByStatus(TermDto term);    
        List<OrderDto> GetReserveOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake, string isearch);
        List<OrderDto> GetReserveOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake);
        List<DeliveryAddressDto> GetDeliveryOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake, string isearch);
        List<DeliveryAddressDto> GetDeliveryOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake);       
        int Count(Guid tenantId);
        int Count(Guid statusId, Guid tenantId);
        int Count(Guid statusId, Guid tenantId, Guid orderTypeId);
        bool UpdateReserve(OrderDto entity);
        int CountByOrderType(Guid tenantId, Guid orderTypeId);
        bool UpdateDelivery(OrderDto entity);
        CartOrderDto FetchOrder(Guid orderId);
        OrderDto FetchDeliveryOrder(Guid orderId);
        List<OrderDto> FetchDeliveryByStatus(Guid tenantId, Guid statusId);
        int Count(Guid tenantId, Guid orderTypeId, params Guid[] statuses);
        bool UpdatePayment(OrderDto entity);
        bool CancelOrder(Guid orderId, Guid orderStatusId, Guid paymentStatusId, DateTime createDt, string createdBy, Guid tenantId);
        int CountByOrderType(Guid orderTypeId, Guid tenantId, Guid paymentStatusId);
    }
}
