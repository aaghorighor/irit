namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface IReport
    {
        IEnumerable<OrderDto> GetSales(TermDto term);
        IEnumerable<OrderDto> GetSalesByUserName(TermDto term);     
        IEnumerable<MenuDto> GetMenus(Guid tenantId);    
        IEnumerable<PaymentDto> GetPaymentByDates(TermDto term);
        IEnumerable<PaymentDto> GetPaymentByDatesAndAccount(TermDto term);
        IEnumerable<PaymentDto> GetPaymentByDatesAndUsername(TermDto term);
        IEnumerable<BestSellerDto> GetBestSellers(TermDto term);
        IEnumerable<OrderDetailDto> GetItemsSoled(TermDto term);
        IEnumerable<OrderDto> GetOrders(TermDto term);
        List<OrderDto> GetDelivery(Guid statusId, Guid tenantId);
        IEnumerable<OrderDeliveryWrapperDto> GetDelivery(TermDto term);

     }
}
