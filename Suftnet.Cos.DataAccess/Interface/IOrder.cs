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
        IEnumerable<OrderDto> GetAllSalesByUserName(TermDto term);
        IEnumerable<OrderDto> GetAllSales(TermDto term);
        IEnumerable<OrderDto> GetAllOrderByStatus(TermDto term);
        List<OrderDto> Filter(Guid tenantId, DateTime filterDate, Guid orderTypeId);
        List<OrderDto> Filter(Guid tenantId, DateTime filterDate);
        List<OrderDto> GetReserveOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake, string isearch);
        List<OrderDto> GetReserveOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake);
        List<DeliveryAddressDto> GetDeliveryOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake, string isearch);
        List<DeliveryAddressDto> GetDeliveryOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake);
        List<OrderDto> GetByOrderType(Guid tenantId, Guid orderTypeId);
        int Count(Guid tenantId);
        int Count(Guid statusId, Guid tenantId);
        int Count(Guid statusId, Guid tenantId, Guid orderTypeId);
        bool UpdateReserve(OrderDto entity);
        int CountByOrderType(Guid tenantId, Guid orderTypeId);
        bool UpdateDelivery(OrderDto entity);
    }
}
