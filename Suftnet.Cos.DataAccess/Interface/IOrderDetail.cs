namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface IOrderDetail 
    {
        bool UpdateCompletedOrders(Guid Id);
        List<OrderDetailWrapperDto> FetchPendingOrders(Guid statusId, Guid tenantId);
        OrderDetailDto Get(Guid Id);
        bool Delete(Guid Id);
        void ClearOrderDetailByOrderId(Guid orderId);
        Guid Insert(OrderDetailDto entity);
        List<OrderDetailDto> GetAll(Guid orderId);
        List<KitchenAdapter> FetchKitchenOrders(Guid statusId, Guid tenantId);
    }
}
