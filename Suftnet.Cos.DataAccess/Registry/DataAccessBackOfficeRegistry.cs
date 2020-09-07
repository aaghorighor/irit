namespace Suftnet.Cos.DataAccess
{
   using StructureMap.Configuration.DSL;  

   public class DataAccessBackOfficeRegistry : Registry
   {      
        public DataAccessBackOfficeRegistry(int tenantId)
       {                      
                                             
            For<IReport>().Use<Report>();
            For<IMobilePermission>().Use<MobilePermission>();
            For<IUser>().Use<User>();
            For<IUserAccount>().Use<UserAccount>();

            For<ICategory>().Use<Category>();              
            For<IAddon>().Use<Addon>();
            For<IMenu>().Use<Menu>();
            For<IOrder>().Use<Order>();
            For<IOrderDetail>().Use<OrderDetail>();
            For<IOrderPayment>().Use<OrderPayment>();
            For<IOrderWrapper>().Use<OrderWrapper>();           
            For<ITable>().Use<Table>();
            For<IPayment>().Use<Payment>();
            For<ITenantAddress>().Use<TenantAddress>();
            For<IUnit>().Use<Unit>();
            For<ITax>().Use<Tax>();
            For<IDiscount>().Use<Discount>();
            For<IAddOnType>().Use<AddOnType>();
            For<IOrderStatus>().Use<OrderStatus>();           
            For<IOrderType>().Use<OrderType>();
            For<IPaymentMethod>().Use<PaymentMethod>();
            For<IPaymentStatus>().Use<PaymentStatus>();
        }

   } 
}
