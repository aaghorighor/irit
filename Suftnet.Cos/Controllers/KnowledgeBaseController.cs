namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.Model;
    using Suftnet.Cos.Service;
    using Suftnet.Cos.Services;
    using Suftnet.Cos.ViewModel;
    using System;
  
    using System.Text;
    using System.Web.Mvc; 
    public class KnowledgeBaseController : MainController
    {              
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(ContactModel contactModel)
        {
            try
            {
                Ensure.NotNull(contactModel);

                var messager = GeneralConfiguration.Configuration.DependencyResolver.GetService<Smtp>();

                var messageModel = new MessageModel();
                var body = new System.Net.Mail.MailMessage();

                body.From = new System.Net.Mail.MailAddress(GeneralConfiguration.Configuration.Settings.General.ServerEmail, 
                    GeneralConfiguration.Configuration.Settings.General.Company);
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
                return Json(new { ok = true, msg = Constant.DangerCode }, JsonRequestBehavior.AllowGet);
            }

            return Json( new { ok= true, msg = Constant.SuccessCode }, JsonRequestBehavior.AllowGet);
        }

        #region private

        private string FormatMessages(ContactModel contactModel)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Hi,");
            builder.AppendLine("");
            builder.AppendLine("Support request from our customer");
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
