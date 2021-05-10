namespace Suftnet.Cos.Web.Command
{
    using Common;
    using Core;
    using Cos.Services;
    using Model;
   
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    using ViewModel;

    public class SendTrialConfirmationCommand : ICommand
    {
        private readonly ISmtp _messenger;    
        private readonly IFactoryCommand _factoryCommand;
        public SendTrialConfirmationCommand(ISmtp messenger, IFactoryCommand factoryCommand)
        {
            _messenger = messenger;         
            _factoryCommand = factoryCommand;
        }

        public CheckoutModel TrialModel { get; set; }
        public int BillingCycle { get; set; }
        public decimal Amount { get; set; }       
        public string Plan { get; set; }
        public string VIEW_PATH { get; set; }

        public void Execute()
        {
            this.SendEmailFactory();
        }

        #region private function

        private void SendEmailFactory()
        {
            if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.TEST))
            {
                SendEmailConfirmation();
            }
            else if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.LIVE))
            {
                SendEmailConfirmation();
            }
        }

        private void SendEmailConfirmation()
        {
            try
            {
                var messageModel = new MessageModel();
                var mailMessage = new System.Net.Mail.MailMessage();
                
                var body = this.CreateConfirmationEmail();

                mailMessage.From = new System.Net.Mail.MailAddress(GeneralConfiguration.Configuration.Settings.General.ServerEmail, GeneralConfiguration.Configuration.Settings.General.Company);
                mailMessage.To.Add(TrialModel.Email);
                mailMessage.Body = body;
                mailMessage.Subject = "Welcome to Irit";
                messageModel.MailMessage = new MailMessage(mailMessage);

                _messenger.MailProcessor(messageModel);

            }
            catch (Exception exception)
            {
                GeneralConfiguration.Configuration.Logger.LogError(exception);
            }
        }
        private void SendGridEmailConfirmation()
        {
            try
            {            
                var body = this.CreateConfirmationEmail();

                var recipients = new List<RecipientModel>();
                var sendGrid = GeneralConfiguration.Configuration.DependencyResolver.GetService<ISendGridMessager>();

                recipients.Add(new RecipientModel { Email = TrialModel.Email });

                if (recipients.Any())
                {
                    sendGrid.Recipients = recipients;
                    sendGrid.SendMail(body, true, "Welcome to Irit");
                }

            }
            catch (Exception exception)
            {
                GeneralConfiguration.Configuration.Logger.LogError(exception);
            }
        }
            
        private string CreateConfirmationEmail()
        {
            var command = _factoryCommand.Create<EmailTemplateCommand>();
            command.VIEW_PATH = VIEW_PATH;
            command.Execute();

            var sb = new StringBuilder(command.View);

            sb.Replace("[customer]", TrialModel.FirstName + " " + TrialModel.LastName);
            sb.Replace("[username]", TrialModel.Email);
            sb.Replace("[email]", TrialModel.Email);
            sb.Replace("[password]", TrialModel.Password);
            sb.Replace("[plan]", Plan);
            sb.Replace("[trialStart]", DateTime.UtcNow.ToShortDateString());
            sb.Replace("[trialend]", DateTime.UtcNow.AddDays(BillingCycle).ToShortDateString());
            sb.Replace("[trialdays]", BillingCycle.ToString());

            sb.Replace("[app_code]", TrialModel.AppCode);
            sb.Replace("[online_link]", GeneralConfiguration.Configuration.Settings.OnlineLink);
            sb.Replace("[mobile_link]", GeneralConfiguration.Configuration.Settings.MobileLink);

            return sb.ToString();
        }
        #endregion
    }
}