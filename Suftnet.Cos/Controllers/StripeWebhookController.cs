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

    [AllowAnonymous]
    public class StripeWebhookController : CommonController.Controllers.BaseController
    {
        private readonly ITenant _tenant;
        private readonly IEditor _editor;
        private readonly ISmtp _messenger;

        private Customer customer;
        private Guid tenantId;
        private string tenantName = string.Empty;

        private dynamic obj;
        private dynamic customerId;

        public StripeWebhookController(DataAccess.ITenant tenant, DataAccess.IEditor editor, ISmtp messenger)
        {
            _editor = editor;
            _tenant = tenant;
            _messenger = messenger;
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
                var editor = new EditorDTO();
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
                      
                        _tenant.UpdateCustomer(new TenantDto
                        {
                            Id = tenantId,
                            StartDate = obj.CurrentPeriodStart,
                            IsExpired = true,
                            SubscriptionId = obj.Id,
                            CustomerStripeId = obj.CustomerId,
                            PlanTypeId = obj.Plan.Id,
                            ExpirationDate = obj.CurrentPeriodEnd
                        });

                        editor = _editor.Get((int)eEditor.SubscriptionCreated);
                        emailBody = FormatSubscriptionCreatedContent(editor.Contents, Constant.ProductName, tenantName,
                            obj.Plan.Amount.ToString(), (int)obj.Plan.IntervalCount, obj.Plan.Nickname);

                        CreateEmail(editor.Title, emailBody, customer.Email);

                        break;

                    case "customer.subscription.updated":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }

                        editor = _editor.Get((int)eEditor.SubscriptionUpdated);
                        emailBody = FormatSubscriptionUpdateContent(editor.Contents, Constant.ProductName, tenantName,
                             obj.Plan.Amount.ToString(), (int)obj.Plan.IntervalCount, obj.Plan.Nickname);

                        CreateEmail(editor.Title, emailBody, customer.Email);

                        break;

                    case "customer.subscription.deleted":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }

                        editor = _editor.Get((int)eEditor.SubscriptionDeleted);
                        emailBody = FormatSubscriptionDeleteContent(editor.Contents, Constant.ProductName, tenantName,
                             obj.Plan.Nickname, obj.CanceledAt);

                        CreateEmail(editor.Title, emailBody, customer.Email);

                        break;

                    case "invoice.payment_succeeded":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }

                        _tenant.UpdateStatus(tenantId,false);

                        editor = _editor.Get((int)eEditor.ChargeSucceeded);
                        emailBody = FormatChargeSucceededContent(editor.Contents,
                            Constant.ProductName, tenantName,
                            obj.Charge != null ? obj.Charge.Amount.ToString() : "0");

                        CreateEmail(editor.Title, emailBody, customer.Email);                                   

                        break;

                    case "invoice.payment_failed":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }

                        _tenant.UpdateCustomer(new TenantDto
                        {
                            Id = tenantId,
                            IsExpired = true
                        });

                        editor = _editor.Get((int)eEditor.ChargeFailed);
                        
                        if(obj.Charge != null)
                        {
                            emailBody = FormatPaymentFailed(editor.Contents, Constant.ProductName, tenantName,
                                obj.Charge.Amount.ToString(), obj.Charge.Source.Object,
                                obj.Charge.Created.ToShortDateString());

                            CreateEmail(editor.Title, emailBody, customer.Email);
                        }

                        break;

                    case "charge.refund.updated":

                        test = StripeEventType(stripeEvent);

                        if (test.StatusCode != (int)HttpStatusCode.OK)
                        {
                            return test;
                        }

                        editor = _editor.Get((int)eEditor.ChargeRefunded);

                        if (obj.Charge != null)
                        {
                            if (obj.Charge.Refunded == true)
                            {
                                emailBody = FormatPaymentRefunded(editor.Contents, Constant.ProductName,
                                    tenantName, obj.Charge.Amount.ToString(),
                                    obj.Charge.Source.Object, obj.Charge.Created.ToShortDateString());

                                CreateEmail(editor.Title, emailBody, customer.Email);
                            }
                        }                                    

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
            switch(stripeEvent.Type)
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

            if (customer.Metadata == null)
            {
                GeneralConfiguration.Configuration.Logger.Log("Tenant ID cannot be found " + customerId, EventLogSeverity.Debug);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Tenant cannot be found");
            }
                      
            tenantId = new Guid(customer.Metadata["tenantId"].ToString());
            tenantName = customer.Metadata["tenantName"].ToString();

            return new HttpStatusCodeResult(HttpStatusCode.OK, "");
        }
        private static Event VerifyEventSentFromStripe(Event stripeEvent)
        {
            var eventService = new EventService(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
            stripeEvent = eventService.Get(stripeEvent.Id);
            return stripeEvent;
        }
        private void CreateEmail(string title, string message, string email)
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
        private string FormatContent(string content, string product, string userName)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            string results = string.Empty;

            sb.Add("[customer]", userName);
            sb.Add("[product]", product);

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string FormatSubscriptionDeleteContent(string content, string product, string userName, string plan, DateTime? cancellationdate)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            string results = string.Empty;

            sb.Add("[customer]", userName);
            sb.Add("[product]", product);
            sb.Add("[plan]", plan);
            sb.Add("[cancellationdate]", cancellationdate == null ? DateTime.Now.ToShortDateString() : cancellationdate.Value.ToShortDateString());

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string FormatChargeSucceededContent(string content, string product, string userName, string amount)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            string results = string.Empty;

            sb.Add("[customer]", userName);
            sb.Add("[product]", product);
            sb.Add("[amount]", amount);
           
            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string FormatSubscriptionCreatedContent(string content, string product, string userName, string amount, int billingCycle, string plan)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            string results = string.Empty;

            sb.Add("[customer]", userName);
            sb.Add("[product]", product);

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
            sb.Add("[Amount]", amount);

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string FormatSubscriptionUpdateContent(string content, string product, string userName, string amount, int billingCycle, string plan)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            string results = string.Empty;

            sb.Add("[customer]", userName);
            sb.Add("[product]", product);

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
            sb.Add("[Amount]", amount);

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string FormatPaymentFailed(string content, string product, string userName, string amount, string cardigit, string date)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            string results = string.Empty;

            sb.Add("[customer]", userName);
            sb.Add("[date]", date);
            sb.Add("[cardigit]", cardigit);
            sb.Add("[Amount]", amount);

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
        private string FormatPaymentRefunded(string content, string product, string userName, string amount, string cardigit, string date)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            string results = string.Empty;

            sb.Add("[customer]", userName);
            sb.Add("[date]", date);
            sb.Add("[cardigit]", cardigit);          
            sb.Add("[Amount]", amount);

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }
            
        #endregion
    }
}