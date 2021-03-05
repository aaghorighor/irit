namespace Suftnet.Cos.Web.Command
{
    using Common;
    using DataAccess;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.Stripe;
    using Suftnet.Cos.Web.ViewModel;
    using System;
  
    public class OrderPaymentCommand : IOrderPaymentCommand
    {
        private ICustomerProvider _customerProvider;
        private IChargeProvider _chargeProvider;
        private readonly IFactoryCommand _factoryCommand;
        private readonly ICustomer _customer;     
        private readonly ITenant _tenant;  
        private string chargeCurrency = ChargeCurrency.Pound;

        public OrderPaymentCommand(ICustomer customer, IFactoryCommand factoryCommand,
            ITenant tenant)
        {
            _customer = customer;      
            _tenant = tenant;
            _factoryCommand = factoryCommand;
        }

        public DeliveryOrderAdapter entityToCreate { get; set; }
        public bool Error { get; set; } = true;
        public string Reason { get; set; }     
        public void Execute()
        {
            Charge();
        }       

        private void Charge()
        {
            try
            {                
                string error, _stripeCustomerId = string.Empty;
                var customer = GetCustomer();
                var tenant = GetTenant();              

                if (tenant == null)
                {
                    Error = true;
                    Reason = "No match found for this Tenant";
                    return;
                }

                if (customer == null)
                {
                    Error = true;
                    Reason = "No match found for this Customer";
                    return;
                }

                if (string.IsNullOrEmpty(tenant.StripePublishableKey) || string.IsNullOrEmpty(tenant.StripeSecretKey))
                 {
                    Error = true;
                    Reason = "Payment Account Not Set";
                    return;
                 }

                entityToCreate.OrderId = Guid.NewGuid();
                OnCurrency(tenant);                            

                _customerProvider = new CustomerProvider(tenant.StripeSecretKey);
                _chargeProvider = new ChargeProvider(tenant.StripeSecretKey);

                _stripeCustomerId = CreateCustomer(customer.StripeCustomerId);

                if (string.IsNullOrEmpty(_stripeCustomerId))
                {
                    _stripeCustomerId = _customerProvider.Create(customer.Email, customer.FirstName, customer.LastName);                   
                }

               var _charge = _chargeProvider.Charge((long)entityToCreate.Order.GrandTotal,
                chargeCurrency, _stripeCustomerId, entityToCreate.SourceToken, $"{"Order -" + $"{entityToCreate.OrderId.ToString().Substring(0,10)} from " + tenant.Name}", out error);;

                if (_charge)
                {
                    Error = false;                    

                    System.Threading.Tasks.Task.Run(() => OnPushNotification());

                    if (string.IsNullOrEmpty(customer.StripeCustomerId))
                    {
                        OnUpdateCustomerStripe(_stripeCustomerId);
                    }
                }

            }
            catch (Exception ex)
            {
                Error = true;
                Reason = "There an issue charging your payment card, please try a different card";
                GeneralConfiguration.Configuration.Logger.LogError(ex);             
            }                     
        }
            
        private string CreateCustomer(string stripeCustomerId)
        {
           return stripeCustomerId != null? _customerProvider.GetCustomerId(stripeCustomerId) : string.Empty;
        }       
        private CustomerDto GetCustomer()
        {
            var model = _customer.Get(new Guid(entityToCreate.CustomerId));        
            return model;
        }
        private TenantDto GetTenant()
        {            
            var model = _tenant.Get(new Guid(entityToCreate.ExternalId));
            return model;
        }
        private void OnCurrency(TenantDto tenant)
        {
            if (tenant.CurrencyId == CurrencyCode.UK)
            {
                chargeCurrency = ChargeCurrency.Pound;
            }
            else if (tenant.CurrencyId == CurrencyCode.US)
            {
                chargeCurrency = ChargeCurrency.Dollar;
            }
        }
        private void OnUpdateCustomerStripe(string stripeCustomerId)
        {           
            _customer.Update(stripeCustomerId, new Guid(entityToCreate.CustomerId), new Guid(entityToCreate.ExternalId));
        }
        private void OnPushNotification()
        {
            var command = _factoryCommand.Create<PushNotificationCommand>();
            command.MessageTypeId = MessageType.PaymentStatus;
            command.FcmToken = entityToCreate.FcmToken;
            command.Execute();
        }
    }
}