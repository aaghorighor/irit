namespace Suftnet.Cos.Services
{
    using Core;
    using System;
    using System.Collections.Generic;   
     
    using SendGrid;
    using SendGrid.Helpers.Mail;  

    using Web.ViewModel;
    using System.Threading.Tasks;

    public class SendGridMessager : ISendGridMessager
    {
        public void SendMail(string body, bool isBodyHtml, string subject)
        {
            try
            {
                SendAsync(body,isBodyHtml, subject);
            }
            catch (Exception exception)
            {
                GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>().LogError(exception);             
            }           
        }

        private List<EmailAddress> PrepareRecipientEmail(List<RecipientModel> recipientModel)
        {
            var emailAddresses = new List<EmailAddress>();

            foreach(var recipient in recipientModel)
            {
                emailAddresses.Add(new EmailAddress(recipient.Email, recipient.Recipient));
            }
           
            return emailAddresses;
        }

        public List<RecipientModel> Recipients { get; set; }
        private void SendAsync(string body, bool isBodyHtml, string subject)
        {           
            var client = new SendGridClient(GeneralConfiguration.Configuration.Settings.SendGridApi);

            var from = new EmailAddress(GeneralConfiguration.Configuration.Settings.General.Email, GeneralConfiguration.Configuration.Settings.General.Company);
                            
            var response = client.SendEmailAsync(MailHelper.CreateSingleEmailToMultipleRecipients(from, PrepareRecipientEmail(this.Recipients), subject, HtmlToText.ConvertHtml(body), body)).Result;
        }
    }
}