namespace Suftnet.Cos.Web
{
    using System.Web.Mvc;
    using Suftnet.Cos.ViewModel;

    using System.Text;
    using System;

    using Core;
    using Cos.Services;
    using Model;
    using Service;

    public class ContactController : MainController
    {        

        [HttpGet]
        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Create(int? flag)
        {
            return View(new ContactModel { flag = flag });
        }

        [HttpPost]
        public ActionResult Create(ContactModel contactModel)
        {
            try
            {
                Ensure.NotNull(contactModel);

                var messager = GeneralConfiguration.Configuration.DependencyResolver.GetService<Smtp>();

                var messageModel = new MessageModel();
                var body = new System.Net.Mail.MailMessage();

                body.From = new System.Net.Mail.MailAddress(GeneralConfiguration.Configuration.Settings.General.ServerEmail, GeneralConfiguration.Configuration.Settings.General.Company);
                body.To.Add(contactModel.Email);
                body.Body = this.FormatMessages(contactModel);
                body.IsBodyHtml = false;
                body.Subject = contactModel.Subject;
                messageModel.MailMessage = new MailMessage(body);

                messager.MailProcessor(messageModel);
            }
            catch (Exception ex)
            {
                GeneralConfiguration.Configuration.Logger.LogError(ex);
            }
          
            return RedirectToActionPermanent("Create", new { flag = 1 });
        }

        #region private

        private string FormatMessages(ContactModel contactModel)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Hi,");
            builder.AppendLine("");
            builder.AppendLine("Feed back from one of our visitors");
            builder.AppendLine("");
            builder.AppendLine("FirstName :" + contactModel.FirstName);
            builder.AppendLine("LastName :" + contactModel.LastName);
            builder.AppendLine("Phone :" + contactModel.Phone);
            builder.AppendLine("Email :" + contactModel.Email);
            builder.AppendLine("Messages :" + contactModel.Message);

            return builder.ToString();
        }

        #endregion

    }
}
