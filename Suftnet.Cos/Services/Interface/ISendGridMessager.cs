namespace Suftnet.Cos.Services
{ 
    using System.Collections.Generic;   
    using Web.ViewModel;

    public interface ISendGridMessager
    {
        List<RecipientModel> Recipients { get; set; }     
        void SendMail(string body, bool isBodyHtml, string subject);
    }
}
