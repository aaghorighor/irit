namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface IOrderDetail 
    {
        bool UpdateCompletedOrders(Guid Id);
        List<OrderDetailWrapperDto> GetByTableOrders(Guid statusId, Guid secondarystatusId, Guid tenantId);
        OrderDetailDto Get(Guid Id);
        bool Delete(Guid Id);
        void ClearOrderDetailByOrderId(Guid Id);
        Guid Insert(OrderDetailDto entity);
        List<OrderDetailDto> GetAll(Guid orderId);
    }
}
