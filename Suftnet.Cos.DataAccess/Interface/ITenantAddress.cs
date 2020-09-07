using System;

namespace Suftnet.Cos.DataAccess
{
    public interface ITenantAddress
    {
        TenantAddressDto Get(Action.TenantAddress o);
        bool UpdateByAddressId(TenantAddressDto entity);
        TenantAddressDto Get(Guid Id);
        Guid Insert(TenantAddressDto entity);      
        bool Delete(Guid id);
        bool Update(TenantAddressDto entity);
    }
}
