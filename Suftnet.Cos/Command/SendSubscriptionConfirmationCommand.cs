namespace Suftnet.Cos.Web.Command
{
    using Common;
    using Core;
    using Cos.Services;
    using Model;
    using Suftnet.Cos.DataAccess;

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    using ViewModel;

    public class SendSubscriptionConfirmationCommand : ICommand
    {
        private readonly ISmtp _messenger;
        private readonly IEditor _editor;
        public SendSubscriptionConfirmationCommand(ISmtp messenger, IEditor editor)
        {
            _messenger = messenger;
            _editor = editor;
        }

        public CheckoutModel SubscriptionModel { get; set; }
        public int BillingCycle { get; set; }
        public decimal Amount { get; set; }       
        public string Plan { get; set; }

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
                SendGridEmailConfirmation();
            }
        }

        private void SendEmailConfirmation()
        {
            try
            {
                var messageModel = new MessageModel();
                var mailMessage = new System.Net.Mail.MailMessage();

                var contentTemplate = new EditorDTO();
                var emailContent = string.Empty;

                emailContent = this.CreateConfirmationEmail(_editor.Get((int)eEditor.SubscriptionTrialConfirmation));

                mailMessage.From = new System.Net.Mail.MailAddress(GeneralConfiguration.Configuration.Settings.General.ServerEmail, GeneralConfiguration.Configuration.Settings.General.Company);
                mailMessage.To.Add(SubscriptionModel.Email);
                mailMessage.Body = emailContent;
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
                var contentTemplate = new EditorDTO();
                var emailContent = string.Empty;

                var body = this.CreateConfirmationEmail(_editor.Get((int)eEditor.SubscriptionTrialConfirmation));
                var recipients = new List<RecipientModel>();
                var sendGrid = GeneralConfiguration.Configuration.DependencyResolver.GetService<ISendGridMessager>();

                recipients.Add(new RecipientModel { Email = SubscriptionModel.Email });

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
            
        private string CreateConfirmationEmail(EditorDTO editor)
        {
            var sb = new StringBuilder(editor.Contents);

            sb.Replace("[customer]", SubscriptionModel.FirstName + " " + SubscriptionModel.LastName);
            sb.Replace("[username]", SubscriptionModel.Email);
            sb.Replace("[email]", SubscriptionModel.Email);
            sb.Replace("[password]", Constant.DefaultPassword);
            sb.Replace("[plan]", Plan);
            sb.Replace("[trialStart]", DateTime.UtcNow.ToShortDateString());
            sb.Replace("[trialend]", DateTime.UtcNow.AddDays(BillingCycle).ToShortDateString());
            sb.Replace("[trialdays]", BillingCycle.ToString());
            sb.Replace("[product]", Constant.ProductName);

            sb.Replace("[onlinelink]", GeneralConfiguration.Configuration.Settings.OnlineLink);
            sb.Replace("[mobilelink]", GeneralConfiguration.Configuration.Settings.MobileLink);

            return sb.ToString();
        }
        #endregion
    }
}