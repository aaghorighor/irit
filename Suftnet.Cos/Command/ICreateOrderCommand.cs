namespace Suftnet.Cos.Web.Command
{
    using Microsoft.AspNet.Identity;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.DataAccess.Identity;
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

    public interface IAcceptDeliveryOrderCommand
    {       
        Guid StatusId { get; set; }      
        string CreatedBy { get; set; }
        Guid TenantId { get; set; }
        DeliveryOrderDto deliveryOrderDto { get; set; }
        string UserName { get; set; }
        string UserId { get; set; }
        void Execute();
    }

    public interface IUpdateDeliveryOrderStatusCommand
    {
        Guid StatusId { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        Guid TenantId { get; set; }   
        string UserName { get; set; }
        string UserId { get; set; }
        Guid OrderId { get; set; }
        void Execute();
    }
    
    public interface ICancelOrderCommand
    {
        Guid OrderId { get; set; }
        Guid TableId { get; set; }
        Guid TenantId { get; set; }
        string UserName { get; set; }
        DateTime UpdateDate { get; set; }
        void Execute();

    }

    public interface ICreateUserCommand
    {
        UserManager<ApplicationUser> UserManager { get; set; }
        string VIEW_PATH { get; set; }
        CreateCustomerDto User { get; set; }
        MobileTenantDto MobileUser { get; set; }
        bool FLAG { get; set; }
        void Execute();
    }
}
