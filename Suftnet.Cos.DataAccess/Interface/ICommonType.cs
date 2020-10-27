namespace Suftnet.Cos.DataAccess
{
   using System;
   using System.Collections.Generic;

    public interface ICommonType
    {     
       List<CommonTypeDto> GetAll();
       List<MobileCommonTypeDto> Fetch();       
       bool Update(CommonTypeDto entity);
       Guid Insert(CommonTypeDto entity);
       bool Delete(Guid id);    
       CommonTypeDto Get(Guid id);      
    }

    public interface IOrderStatus : ICommonType
    {
      
    }

    public interface IOrderType : ICommonType
    {

    }

    public interface IPaymentMethod : ICommonType
    {

    }

    public interface IPaymentStatus : ICommonType
    {

    }

    public interface ITenantStatus : ICommonType
    {

    }
}
