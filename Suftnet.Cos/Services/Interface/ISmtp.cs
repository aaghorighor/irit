namespace Suftnet.Cos.Services
{
    using Suftnet.Cos.Model;
    public interface ISmtp
    {
        void MailProcessor(MessageModel messageModel);       
    }
}
