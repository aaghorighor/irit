namespace Suftnet.Cos.Core
{
    public class RegistrationSettings
    {
        public RegistrationSettings()
        {
            SendAccountRegistrationConfirmation = true;
            SignInWhenRegistered = true;
        }
       
        public bool SignInWhenRegistered { get; set; }
      
        public bool SendAccountRegistrationConfirmation { get; set; }

    }
}
