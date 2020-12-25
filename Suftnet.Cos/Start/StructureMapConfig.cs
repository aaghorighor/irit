namespace Suftnet.Cos.Web
{ 
    using Cos.Services;
    using Services;

    using StructureMap;
    using System;
    using Suftnet.Cos.Services.Interface;
    using Services.Interface;
    using Suftnet.Cos.Web.Command;

    public class StructureMapConfig
    {
        private static string m_Host;
        private static string m_RootPath;

        #region Unity Container
        private static Lazy<IContainer> container = new Lazy<IContainer>(() =>
        {
            var container = Boostrapper.Start();
            RegisterTypes(container);
            return container;
        });      
             
        public static IContainer GetConfiguredContainer(System.Web.HttpContextBase ctx)
        {
            m_Host = ctx.Request.Url.Host;
            m_RootPath = ctx.Server.MapPath("/");     
            return container.Value;
        }

        #endregion

        private static void RegisterTypes(IContainer container)
        {
            container.Configure(x =>
            {              
                x.For<ISmtp>().Use<Smtp>();             
                x.For<IRoutesService>().Use<OneChurchRoutes>();
                x.For<IOptimizationService>().Use<OptimizationService>();
                x.For<ISms>().Use<Sms>();
                x.For<IFactoryCommand>().Use<FactoryCommand>();                                                   
                x.For<ISendGridMessager>().Use<SendGridMessager>();             
                x.For<IJwToken>().Use<JwToken>();
                x.For<IApiUserManger>().Use<ApiUserManager>();
                x.For<IClaimManager>().Use<ClaimManager>();
                x.For<IOrderCommand>().Use<OrderCommand>();
                x.For<IAdminDashboardCommand>().Use<AdminDashboardCommand>();
                x.For<IDashboardCommand>().Use<DashboardCommand>();
                x.For<IBoostrapCommand>().Use<BoostrapCommand>();
                x.For<IItemCommand>().Use<ItemCommand>();
                x.For<ICreateOrderCommand>().Use<CreateOrderCommand>();
                x.For<IUserBoostrapCommand>().Use<UserBoostrapCommand>();
                x.For<IUpdateOrderCommand>().Use<UpdateOrderCommand>();
                x.For<IPaymentCommand>().Use<PaymentCommand>();
                x.For<ICloseOrderCommand>().Use<CloseOrderCommand>();
            });           
        }
    }
}