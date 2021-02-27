namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface ICustomer
    {
        CustomerDto Get(Guid Id);
        bool Delete(Guid Id);
        Guid Insert(CustomerDto entity);
        bool Update(UpadteCustomerDto entity);
        List<CustomerDto> GetAll(Guid tenantId);    
    }

    public interface ICustomerAddress
    {
        CustomerAddressDto Get(Guid Id);
        bool Delete(Guid Id);
        Guid Insert(CreateCustomerAddressDto entity);
        bool Update(CreateCustomerAddressDto entity);
        List<CustomerAddressDto> GetAll(Guid customerId);
    }

    public interface ICustomerDeliveryStatus
    {      
        bool Delete(Guid Id);
        Guid Insert(CustomerDeliveryStatusDto entity);  
        List<CustomerDeliveryStatusDto> GetAll(Guid orderId);
    }

    public interface ICustomerOrderDelivery
    {
        CustomerOrderDeliveryDto Get(Guid orderId);
        bool Delete(Guid Id);
        Guid Insert(CustomerOrderDeliveryDto entity);     
    }

    public interface ICustomerOrder
    {     
        bool Delete(Guid Id);
        Guid Insert(CustomerOrderDto entity);
        List<CustomerOrderDto> GetAll(Guid orderId);
        List<MobileCustomerOrderDto> Fetch(Guid customerId);
    }

    public interface ICustomerNotification
    {
        bool Delete(Guid Id);
        Guid Insert(CustomerOrderNotificationDto entity);
        List<CustomerOrderNotificationDto> GetAll(Guid customerId);
    }
}


