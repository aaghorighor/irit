namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface ICategory 
    {
        List<CategoryDto> GetAll(Guid tenantId);
        CategoryDto Get(Guid Id);
        bool Delete(Guid Id);
        Guid Insert(CategoryDto entity);
        bool Update(CategoryDto entity);
        List<CategoryDto> GetByStatus(bool status, Guid tenantId);
    }
}

