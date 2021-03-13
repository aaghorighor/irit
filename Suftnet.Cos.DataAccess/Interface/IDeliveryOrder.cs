namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface IDeliveryOrder
    {       
        bool Delete(Guid Id);
        Guid Insert(DeliveryOrderDto entity);
        List<MobileCustomerOrderDto> FetchByDelivered(string userId, Guid statusId);
        List<MobileCustomerOrderDto> FetchBy(string userId, Guid statusId);
    }
}
