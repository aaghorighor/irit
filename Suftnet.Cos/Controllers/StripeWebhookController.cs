namespace Suftnet.Cos.Web
{
    using Common;
    using Core;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Web.Mvc;

    using global::Stripe;
    using Model;
    using Cos.Services;  
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Stripe;
    using Suftnet.Cos.Web.ViewModel;
    using System.Linq;
    using Suftnet.Cos.Web.Command;
    using System.Text;

    [AllowAnonymous]
    public class StripeWebhookController : CommonController.Controllers.BaseController
    {
        private readonly ITenant _tenant;      
        private readonly ISmtp _messenger;
        private readonly IFactoryCommand _factoryCommand;
        private Customer customer;
        private Guid tenantId;
        private string tenantName = string.Empty;
        private string appCode = string.Empty;

        private dynamic obj;
        private dynamic customerId;

        public StripeWebhookController(DataAccess.ITenant tenant, 
            IFactoryCommand factoryCommand, ISmtp messenger)
        {           
            _tenant = tenant;
            _messenger = messenger;
            _factoryCommand = factoryCommand;
        }

        [HttpPost]
        public ActionResult Index()
        {
            var req = Request.InputStream;
            var json = new StreamReader(req).ReadToEndAsync().Result;

            GeneralConfiguration.Configuration.Logger.Log("Incoming stripe object" + json, EventLogSeverity.Debug);

            Event stripeEvent = null;
            try
            {
                stripeEvent = EventUtility.ParseEvent(json, false);
                stripeEvent = VerifyEventSentFromStripe(stripeEvent);

                if (stripeEvent == null || (stripeEvent.Data == null) || (stripeEvent.Data.Object == null))
                {
                    GeneralConfiguration.Configuration.Logger.Log("Incoming request is empty", EventLogSeverity.Debug);
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Incoming request is empty");
                }
            }
            catch (Exception ex)
            {
                GeneralConfiguration.Configuration.Logger.LogError(ex);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, string.Format("Unable to parse incoming event.  The following error occurred: {0}", ex.Message));
            }

            try
            {               
                var emailBody = string.Empty;
                HttpStatusCodeResult test;

                switch (stripeEvent.Type)
                {
                    case "customer.subscription.created":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }
                 
                        emailBody = SubscriptionCreatedContent(tenantName, customer.Email,
                           CalculatePrice(obj.Plan.Amount), (int)obj.Plan.IntervalCount, obj.Plan.Nickname);

                        SendEmailFactory("Irit Subscription Created", emailBody, customer.Email);

                        break;

                    case "customer.subscription.updated":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }
                       
                        emailBody = SubscriptionUpdateContent(tenantName,
                             CalculatePrice(obj.Plan.Amount), (int)obj.Plan.IntervalCount, obj.Plan.Nickname);

                        SendEmailFactory("Irit Subscription Updated", emailBody, customer.Email);

                        break;

                    case "customer.subscription.deleted":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }
                     
                        emailBody = SubscriptionDeleteContent(tenantName,
                             obj.Plan.Nickname, obj.CanceledAt);

                        SendEmailFactory("Irit Subscription Deleted", emailBody, customer.Email);

                        break;

                    case "invoice.payment_succeeded":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }

                        try
                        {
                            System.Threading.Tasks.Task.Run(() => _tenant.UpdateStatus(tenantId, false));
                        }
                        catch (Exception ex)
                        { GeneralConfiguration.Configuration.Logger.LogError(ex); }
                                              
                        emailBody = ChargeSucceededContent(tenantName, customer.Email, obj.HostedInvoiceUrl,
                            obj.AmountPaid);

                        SendEmailFactory("Irit Payment Succeeded", emailBody, customer.Email);

                        break;

                    case "invoice.payment_failed":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }

                        try
                        {
                            System.Threading.Tasks.Task.Run(() => _tenant.UpdateStatus(tenantId, true));
                        }
                        catch (Exception ex)
                        { GeneralConfiguration.Configuration.Logger.LogError(ex); }
                                               
                        emailBody = PaymentFailedContent(tenantName,
                                obj.HostedInvoiceUrl);

                        SendEmailFactory("Irit Payment Failed", emailBody, customer.Email);

                        break;

                    case "charge.refund.updated":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }
                                               
                        emailBody = PaymentRefundedContent(tenantName,
                                    obj.AmountRefunded.ToString(), obj.ReceiptUrl,
                                    obj.PaymentMethodDetails.Card.Last4, obj.Created.ToShortDateString());

                        SendEmailFactory("Irit Charge Refund Updated", emailBody, customer.Email);

                        break;
                }

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                GeneralConfiguration.Configuration.Logger.LogError(ex);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Unable to parse incoming event request");
            }
        }

        #region private function

        private HttpStatusCodeResult StripeEventType(Event stripeEvent)
        {
            switch (stripeEvent.Type)
            {
                case "customer.subscription.created":

                    obj = stripeEvent.Data.Object;
                    customerId = obj.CustomerId;

                    break;
                case "customer.subscription.updated":
                    obj = stripeEvent.Data.Object;
                    customerId = obj.CustomerId;

                    break;

                case "invoice.payment_succeeded":
                    obj = stripeEvent.Data.Object;
                    customerId = obj.CustomerId;

                    break;
                case "invoice.payment_failed":
                    obj = stripeEvent.Data.Object;
                    customerId = obj.customer;

                    break;
                case "charge.refund.updated":
                    obj = stripeEvent.Data.Object;
                    customerId = obj.CustomerId;

                    break;

                case "customer.subscription.deleted":

                    obj = stripeEvent.Data.Object;
                    customerId = obj.CustomerId;

                    break;
            }

            if (customerId == null)
            {
                GeneralConfiguration.Configuration.Logger.Log("Customer ID cannot be found " + customerId, EventLogSeverity.Debug);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Customer ID cannot be found");
            }

            ICustomerProvider _customerProvider = new CustomerProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
            customer = _customerProvider.GetCustomer(customerId);

            if (customer == null)
            {
                GeneralConfiguration.Configuration.Logger.Log("Customer cannot be found " + customerId, EventLogSeverity.Debug);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Tenant cannot be found");
            }

            if (customer.Metadata.Count == 0)
            {
                GeneralConfiguration.Configuration.Logger.Log("Tenant ID cannot be found " + customerId, EventLogSeverity.Debug);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Tenant cannot be found");
            }

            tenantId = new Guid(customer.Metadata["tenantId"].ToString());
            tenantName = customer.Metadata["tenantName"].ToString();
            appCode = customer.Metadata["app_code"].ToString();

            return new HttpStatusCodeResult(HttpStatusCode.OK, "");
        }
        private static Event VerifyEventSentFromStripe(Event stripeEvent)
        {
            var eventService = new EventService(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
            stripeEvent = eventService.Get(stripeEvent.Id);
            return stripeEvent;
        }       
        private string SubscriptionDeleteContent(string userName, string plan, DateTime? cancellationdate)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            var command = _factoryCommand.Create<EmailTemplateCommand>();
            command.VIEW_PATH = this.Server.MapPath("~/App_Data/Email/subscriptionCancellation.html");
            command.Execute();

            var content = command.View;

            sb.Add("[customer]", userName);           
            sb.Add("[plan]", plan);
            sb.Add("[cancellationdate]", cancellationdate == null ? DateTime.Now.ToShortDateString() : cancellationdate.Value.ToShortDateString());

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string ChargeSucceededContent(string userName, string email, string hosted_invoice_url, decimal amount)
        {          
            Dictionary<string, string> sb = new Dictionary<string, string>();
            var command = _factoryCommand.Create<EmailTemplateCommand>();
            command.VIEW_PATH = this.Server.MapPath("~/App_Data/Email/paymentSuccessful.html");
            command.Execute();

            var content = command.View;

            sb.Add("[customer]", userName);         
            sb.Add("[email]", email);
            sb.Add("[hosted_invoice_url]", hosted_invoice_url);

            sb.Add("[Amount]", Constant.CurrencySymbol + "" + Round(amount));

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string SubscriptionCreatedContent(string userName, string email, decimal amount, int billingCycle, string plan)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            var command = _factoryCommand.Create<EmailTemplateCommand>();
            command.VIEW_PATH = this.Server.MapPath("~/App_Data/Email/subscriptionCreated.html");
            command.Execute();

            var content = command.View;

            sb.Add("[customer]", userName);
            sb.Add("[username]", email);
      
            if (plan == "Basic")
            {
                sb.Add("[BillingCycle]", billingCycle.ToString() + " " + "Month");
            }
            else if (plan == "Premium Plus")
            {
                sb.Add("[BillingCycle]", billingCycle.ToString() + " " + "Year");
            }
            else
            {
                sb.Add("[BillingCycle]", billingCycle.ToString() + " " + "Months");
            }

            sb.Add("[Plan]", plan);
            sb.Add("[Amount]", Constant.CurrencySymbol + "" + Round(amount));
            sb.Add("[app_code]", appCode);

            sb.Add("[online_link]", GeneralConfiguration.Configuration.Settings.OnlineLink);
            sb.Add("[mobile_link]", GeneralConfiguration.Configuration.Settings.MobileLink);

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string SubscriptionUpdateContent(string userName, decimal amount, int billingCycle, string plan)
        {          
            Dictionary<string, string> sb = new Dictionary<string, string>();
            var command = _factoryCommand.Create<EmailTemplateCommand>();
            command.VIEW_PATH = this.Server.MapPath("~/App_Data/Email/subscriptionUpdate.html");
            command.Execute();

            var content = command.View;

            sb.Add("[customer]", userName);
          
            if (plan == "Basic")
            {
                sb.Add("[BillingCycle]", billingCycle.ToString() + " " + "Month");
            }
            else if (plan == "Premium Plus")
            {
                sb.Add("[BillingCycle]", billingCycle.ToString() + " " + "Year");
            }
            else
            {
                sb.Add("[BillingCycle]", billingCycle.ToString() + " " + "Months");
            }

            sb.Add("[Plan]", plan);
            sb.Add("[Amount]", Constant.CurrencySymbol + "" + Round(amount));

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string PaymentFailedContent(string userName, string hosted_invoice_url)
        {           
            Dictionary<string, string> sb = new Dictionary<string, string>();
            var command = _factoryCommand.Create<EmailTemplateCommand>();
            command.VIEW_PATH = this.Server.MapPath("~/App_Data/Email/paymentFailed.html");
            command.Execute();

            var content = command.View;

            sb.Add("[customer]", userName);
            sb.Add("[hosted_invoice_url]", hosted_invoice_url);

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string PaymentRefundedContent(string userName, decimal amount, string receiptUrl, string cardigit, string date)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            var command = _factoryCommand.Create<EmailTemplateCommand>();
            command.VIEW_PATH = this.Server.MapPath("~/App_Data/Email/paymentRefunded.html");
            command.Execute();

            var content = command.View;

            sb.Add("[customer]", userName);
            sb.Add("[date]", date);
            sb.Add("[cardigit]", cardigit);
            sb.Add("[amount]", Constant.CurrencySymbol + "" + Round(amount));
            sb.Add("[receiptUrl]", receiptUrl);

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private void SendEmailFactory(string title, string message, string email)
        {
            if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.TEST))
            {
                System.Threading.Tasks.Task.Run(() => SendSmtpEmail(title, message, email));
            }
            else if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.LIVE))
            {
                System.Threading.Tasks.Task.Run(()=> SendGridEmail(title, message, email));
            }
        }
        private void SendSmtpEmail(string title, string message, string email)
        {
            var messageModel = new MessageModel();
            var mailMessage = new System.Net.Mail.MailMessage();

            mailMessage.From = new System.Net.Mail.MailAddress(GeneralConfiguration.Configuration.Settings.General.Email, GeneralConfiguration.Configuration.Settings.General.Company);
            mailMessage.To.Add(email);
            mailMessage.Body = message;
            mailMessage.Subject = title.Replace("[title]", Constant.ProductName);
            messageModel.MailMessage = new MailMessage(mailMessage);

            try
            {
                _messenger.MailProcessor(messageModel);
            }
            catch (Exception ex)
            {
                GeneralConfiguration.Configuration.Logger.LogError(ex);
            }
        }
        private void SendGridEmail(string title, string body, string email)
        {
            var recipients = new List<RecipientModel>();
            var sendGrid = GeneralConfiguration.Configuration.DependencyResolver.GetService<ISendGridMessager>();

            recipients.Add(new RecipientModel { Email = email });
            try
            {
                if (recipients.Any())
                {
                    sendGrid.Recipients = recipients;
                    sendGrid.SendMail(body, true, title.Replace("[title]", Constant.ProductName));
                }
            }
            catch (Exception ex)
            {
                GeneralConfiguration.Configuration.Logger.LogError(ex);
            }
        }
        private decimal? CalculatePrice(decimal amount)
        {
            var taxRate = GeneralConfiguration.Configuration.Settings.General?.TaxRate;
            var vat = taxRate != null ? amount * (taxRate / 100) : 1;
            var total = vat + amount;
            return total;
        }
        private decimal Round(decimal? amount)
        {
            var totalAmount = (amount / 100);
            return Math.Round((decimal)totalAmount, 2);
        }
        #endregion
    }
}