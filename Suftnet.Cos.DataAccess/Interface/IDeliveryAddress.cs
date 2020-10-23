namespace Suftnet.Cos.DataAccess
{
    using Suftnet.Cos.DataAccess.Action;
    using System;
    using System.Collections.Generic;

    public interface IDeliveryAddress
    {
        DeliveryAddressDto GetByOrderId(Guid orderId);
        DeliveryAddressDto Get(Guid Id);
        bool Delete(Guid orderId);
        Guid Insert(DeliveryAddressDto entity);
        bool Update(DeliveryAddressDto entity);
       
    }
}

