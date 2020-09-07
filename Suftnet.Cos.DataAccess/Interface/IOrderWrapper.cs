using System;

namespace Suftnet.Cos.DataAccess
{
    public interface IOrderWrapper
   {
        OrderWrapperDto Fetch(Guid tenantId, Guid orderTypeId);
   }
}
