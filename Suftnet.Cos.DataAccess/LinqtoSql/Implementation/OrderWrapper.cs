using System;

namespace Suftnet.Cos.DataAccess
{
    public class OrderWrapper : IOrderWrapper
    {
        private readonly IMenu _menu;
        private readonly IOrder _order;
     
        public OrderWrapper(IMenu menu, IOrder order)
        {
            _menu = menu;
            _order = order;           
        }       

        public OrderWrapperDto Fetch(Guid tenantId, Guid orderTypeId)
        {
           return new OrderWrapperDto { Menu = _menu.GetAll(tenantId) };     
        }
    }
}
