namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using System.Collections.Generic;

    public interface ICreateOrderCommand
    {       
        CreateOrder entityToCreate { get; set; }
        void Execute();
   }

    public interface IUpdateOrderCommand
    {
        OrderAdapter OrderAdapter { get; set; }
        void Execute();
    }

    public interface ICloseOrderCommand
    {
        Guid OrderId { get; set; }
        Guid StatusId { get; set; }
        DateTime CreatedDt { get; set; }
        string CreatedBy { get; set; }
        Guid TenantId { get; set; }
        void Execute();
        IList<BasketDto> Baskets { get; set; }
    }
}
