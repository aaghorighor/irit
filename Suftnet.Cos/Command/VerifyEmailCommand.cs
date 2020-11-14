namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using Cos.Services;
    using Core;
    using DataAccess.Identity;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Text;
    using Suftnet.Cos.Web.ViewModel;
    using System.Collections.Generic;
    using System.Net.Mail;
    using Suftnet.Cos.Common;

    public class VerifyEmailCommand : ICommand
    {
        private readonly IUser _user;
        private readonly ISms _sms;
        private readonly ISmtp _messenger;
        private readonly IFactoryCommand _factoryCommand;

        public VerifyEmailCommand(IUser user, ISms sms, ISmtp messenger, IFactoryCommand factoryCommand)
        {
            _user = user;
            _sms = sms;
            _factoryCommand = factoryCommand;
            _messenger = messenger;
        }      
            
        public ApplicationUser User { get; set; }
        public int AppCode { get; set; }
        public string VIEW_PATH { get; set; }
        public void Execute()
        {
            this.VerifyUserAccount();
        }

        #region private function

        private void VerifyUserAccount()
        {       
            try
            {
                var otp = Otp();

                SendEmailFactory(otp, "kabelsus@yahoo.com");

                _user.UpdateAccessCode(User.Id, otp);
            }
            catch(Exception exception)
            {
                GeneralConfiguration.Configuration.Logger.LogError(exception);
            }                      
        }

        private void SendEmailFactory(string code,  string email)
        {
            if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.TEST))
            {
                Task.Run(() => SendSmtpEmail(code, email));
            }
            else if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.LIVE))
            {
                Task.Run(() => SendGridEmail(code, email));
            }
        }
        private void SendSms(string code, string phone)
        {
            _sms.SendMessage(phone, "Irit OTP code " + code);
        }        
        private void SendGridEmail(string code, string email)
        {
            try
            {               
                var body = this.CreateBody(code, email);
                var recipients = new List<RecipientModel>();
                var sendGrid = GeneralConfiguration.Configuration.DependencyResolver.GetService<ISendGridMessager>();

                recipients.Add(new RecipientModel { Email = email });

                if (recipients.Any())
                {
                    sendGrid.Recipients = recipients;
                    sendGrid.SendMail(body, true, $"Irit OTP code");
                }

            }
            catch (Exception exception)
            {
                GeneralConfiguration.Configuration.Logger.LogError(exception);
            }
        }
        private void SendSmtpEmail(string code, string email)
        {
            var messageModel = new Model.MessageModel();
            var mailMessage = new System.Net.Mail.MailMessage();
            var body = this.CreateBody(code, email);

            mailMessage.From = new System.Net.Mail.MailAddress(GeneralConfiguration.Configuration.Settings.General.Email, GeneralConfiguration.Configuration.Settings.General.Company);
            mailMessage.To.Add(email);
            mailMessage.Body = body;
            mailMessage.Subject = "Irit OTP code";
            messageModel.MailMessage = new Suftnet.Cos.Model.MailMessage(mailMessage);

            try
            {
                _messenger.MailProcessor(messageModel);
            }
            catch (Exception ex)
            {
                GeneralConfiguration.Configuration.Logger.LogError(ex);
            }
        }
        private string CreateBody(string code, string email)
        {
            var command = _factoryCommand.Create<EmailTemplateCommand>();
            command.VIEW_PATH = VIEW_PATH;
            command.Execute();

            var sb = new StringBuilder(command.View);

            sb.Replace("[code]", code);
            sb.Replace("[email]", email);            

            return sb.ToString();
        }
        private string Otp()
        {
            var newGuid = Guid.NewGuid().GetHashCode();
            var appCode = Math.Abs(newGuid);
            return appCode.ToString().Substring(0,6);
        }

        #endregion

    }
}