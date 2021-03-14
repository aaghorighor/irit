namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface IReport
    {       
        IEnumerable<MenuDto> GetMenus(Guid tenantId);    
        IEnumerable<PaymentDto> GetPaymentByDates(TermDto term);
        IEnumerable<PaymentDto> GetPaymentByDatesAndUsername(TermDto term);
        IEnumerable<BestSellerDto> GetBestSellers(TermDto term);    
        IEnumerable<OrderDto> GetOrders(TermDto term);       
        IEnumerable<OrderDto> GetReservationOrders(TermDto term);
        IEnumerable<MobilePaymentDto> FetchPayments(Guid externalId, int take);
        IEnumerable<MobilePaymentDto> FetchPaymentByDates(DateQueryDto dateQueryDto);
        IEnumerable<MobilePaymentDto> FetchPaymentByAccountType(DateQueryDto dateQueryDto);

     }
}
