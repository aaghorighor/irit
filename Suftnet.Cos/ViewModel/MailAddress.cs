namespace Suftnet.Cos.Model
{
    using Suftnet.Cos.Common;

    public class MailAddress
    {
        public MailAddress()
        {
            SendingType = EmailSendingType.To;
        }
        
        public string Address { get; set; }
    
        public string DisplayName { get; set; }
       
        public EmailSendingType SendingType { get; set; }
    }
}