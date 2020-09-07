namespace Suftnet.Cos.Web.Command
{
    using Common;
    using Core;
    using Cos.Services;
    using Model;
    using Suftnet.Cos.DataAccess;

    using System;
    using System.Text;

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
            this.SendEmailConfirmation();
        }

        #region private function
        private void SendEmailConfirmation()
        {
            try
            {
                var messageModel = new MessageModel();
                var mailMessage = new System.Net.Mail.MailMessage();

                var contentTemplate = new EditorDTO();
                var emailContent = string.Empty;

                if (SubscriptionModel.PlanId == (int)ePlan.Trial)
                {
                    emailContent = this.PrepareSubscriptionTrialContent(_editor.Get((int)eEditor.SubscriptionTrialConfirmation));                                       
                }
                else
                {
                    emailContent = this.PrepareSubscriptionContent(_editor.Get((int)eEditor.SubscriptionConfirmation));
                }

                mailMessage.From = new System.Net.Mail.MailAddress(GeneralConfiguration.Configuration.Settings.General.ServerEmail, GeneralConfiguration.Configuration.Settings.General.Company);
                mailMessage.To.Add(SubscriptionModel.Email);
                mailMessage.Body = emailContent;
                mailMessage.Subject = "Welcome to Jerur";
                messageModel.MailMessage = new MailMessage(mailMessage);

                _messenger.MailProcessor(messageModel);

            }
            catch (Exception exception)
            {
                GeneralConfiguration.Configuration.Logger.LogError(exception);
            }
        }
        private string PrepareSubscriptionContent(EditorDTO editor)
        {
            var sb = new StringBuilder(editor.Contents);           

            sb.Replace("[customer]", SubscriptionModel.FirstName);
            sb.Replace("[username]", SubscriptionModel.Email);
            sb.Replace("[email]", SubscriptionModel.Email);
            sb.Replace("[password]", Constant.DefaultPassword);
            sb.Replace("[plan]", Plan);
            sb.Replace("[Amount]", Amount.ToString());
            sb.Replace("[BillingCycle]", BillingCycle + " Days");
            sb.Replace("[product]", Constant.ProductName);
            sb.Replace("[url]", Constant.LoginUrl);

            return sb.ToString();
        }
        private string PrepareSubscriptionTrialContent(EditorDTO editor)
        {
            var sb = new StringBuilder(editor.Contents);

            sb.Replace("[customer]", SubscriptionModel.FirstName);
            sb.Replace("[username]", SubscriptionModel.Email);
            sb.Replace("[email]", SubscriptionModel.Email);
            sb.Replace("[password]", Constant.DefaultPassword);
            sb.Replace("[plan]", Plan);
            sb.Replace("[trialStart]", DateTime.UtcNow.ToShortDateString());
            sb.Replace("[trialend]", DateTime.UtcNow.AddDays(BillingCycle).ToShortDateString());
            sb.Replace("[trialdays]", BillingCycle.ToString());
            sb.Replace("[product]", Constant.ProductName);
            sb.Replace("[url]", Constant.LoginUrl);

            return sb.ToString();
        }
        #endregion


    }
}