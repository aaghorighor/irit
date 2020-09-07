namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface IAddon 
    {
        AddonDto Get(Guid Id);
        bool Delete(Guid Id);
        Guid Insert(AddonDto entity);
        bool Update(AddonDto entity);
        List<AddonDto> GetAll(Guid Id);
    }
}

