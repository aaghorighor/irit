
namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface IMenu 
    {
        MenuDto Get(Guid Id);
        bool Delete(Guid Id);
        Guid Insert(MenuDto entity);
        bool Update(MenuDto entity);
        List<MenuDto> GetCutOffMenu(Guid tenantId);         
        List<MenuDto> GetAll(Guid tenantId, int iskip, int itake, string isearch);
        List<MenuDto> GetAll(Guid tenantId, int iskip, int itake);       
        List<MenuDto> GetAll(Guid categoryId, Guid tenantId);
        int CutOffCount(Guid tenantId);
        int Count(Guid tenantId);
        int Count(DateTime datetime, Guid tenantId);
        void UpdateMenuQuantity(int quantity, Guid menuId);
        List<MenuDto> GetAll(Guid tenantId);
        List<MenuDto> GetBy(Guid tenantId, int take);
        List<MobileMenuDto> GetBy(Guid tenantId);
        List<MenuDto> GetByCategoryId(Guid categoryId, Guid tenantId);
    }
}
