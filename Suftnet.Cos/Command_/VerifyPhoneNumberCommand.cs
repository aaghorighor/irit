namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using Cos.Services;
    using Core;
    using DataAccess.Identity;
    using System.Threading.Tasks;

    public class VerifyPhoneNumberCommand : ICommand
    {
        private readonly IUser _userAccount;
        private readonly ISms _sms;

        public VerifyPhoneNumberCommand(IUser userAccount, ISms sms)
        {
            _userAccount = userAccount;
            _sms = sms;
        }      
            
        public ApplicationUser ApplicationUser { get; set; }
        public void Execute()
        {
            this.VerifyUserAccount();
        }

        #region private function

        private void VerifyUserAccount()
        {       

            try
            {
                var otp = "".RandomCode();
                ApplicationUser.OTP = otp;
                Task.Run(()=> SendSms(ApplicationUser.PhoneNumber, otp));

                ApplicationUser.OTP = ApplicationUser.OTP.ToEncrypt();
               _userAccount.UpdateAccessCode(ApplicationUser.PhoneNumber, (int)ApplicationUser.TenantId, ApplicationUser.OTP);
            }
            catch(Exception exception)
            {
                GeneralConfiguration.Configuration.Logger.LogError(exception);
            }  
                    
        }

        private void SendSms(string phone, string accessCode)
        {
            _sms.SendMessage(phone, "Jerur code " + accessCode);
        }

        #endregion

    }
}