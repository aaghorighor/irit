namespace Suftnet.Cos.Services
{   
    using Twilio;

    public interface ISms
    {
        Message SendMessage(string to, string body);
        Message SendMessage(string from, string to, string body);
    }
}
