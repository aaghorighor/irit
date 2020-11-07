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

    public class VerifyEmailCommand : ICommand
    {
        private readonly IUser _user;
        private readonly ISms _sms;
        private readonly IFactoryCommand _factoryCommand;

        public VerifyEmailCommand(IUser user, ISms sms, IFactoryCommand factoryCommand)
        {
            _user = user;
            _sms = sms;
            _factoryCommand = factoryCommand;
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
                               
                SendEmail(otp, User.Email);

                Task.Run(()=> _user.UpdateAccessCode(User.Id, AppCode, otp));
            }
            catch(Exception exception)
            {
                GeneralConfiguration.Configuration.Logger.LogError(exception);
            }                      
        }

        private void SendSms(string code, string phone)
        {
            _sms.SendMessage(phone, "Irit OTP code " + code);
        }        
        private void SendEmail(string code, string email)
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