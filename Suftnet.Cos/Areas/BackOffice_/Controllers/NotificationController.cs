namespace Suftnet.Cos.BackOffice
{
    using Core;
    using Service;

    using Suftnet.Cos.Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.ViewModel;

    using Services; 
    using Web.Command;
    
    public class NotificationController : BackOfficeBaseController
    {
        #region Resolving dependencies

        private readonly IFactoryCommand _factoryCommand;
        private readonly INotification _notification;
        private readonly ITenant _tenant;

        public NotificationController(
            INotification notification, ITenant tenant,
             IFactoryCommand factoryCommand
           )
        {
            _notification = notification;
            _tenant = tenant;
            _factoryCommand = factoryCommand;
        }

        #endregion       

        public ActionResult Index()
        {
            return View(_notification.GetAll(this.TenantId));
        }

        [HttpGet]
        public JsonResult Get(int Id)
        {
            Ensure.Argument.NotNull(Id);
            Ensure.Argument.IsNot(Id == 0);
            return Json(new { ok = true, dataobject = _notification.Get(Id) }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateInput(false)]
        [PermissionFilter(BackOfficeViews.Notification, PermissionType.Create)]
        [ValidateAntiForgeryToken]
        public JsonResult Create(NotificationDto entityToCreate)
        {
            Ensure.Argument.NotNull(entityToCreate);

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.AjaxErrors()
                });
            }

            entityToCreate.CreatedBy = this.UserName;
            entityToCreate.CreatedDT = DateTime.UtcNow;
            entityToCreate.TenantId = this.TenantId;

            switch (entityToCreate.MessageTypeId)
            {
                case NotiticationType.Email:
                case NotiticationType.Sms:

                    if (entityToCreate.StatusId == MessageStatus.Now)
                    {
                        this.PrepareMessage(entityToCreate);
                    }

                    break;

                case NotiticationType.PushNotification:

                    if (entityToCreate.StatusId == MessageStatus.Now)
                    {
                        var command = _factoryCommand.Create<PushNotificationCommand>();

                        command.Title = entityToCreate.Subject;
                        command.Body = HtmlToText.ConvertHtml(entityToCreate.Body);
                        command.TenantId = this.TenantId;
                        command.ClickAction = "notifyActivity";

                        command.Execute();                      
                    }

                    break;
            }

            if (entityToCreate.Id == 0)
            {
                entityToCreate.Id = _notification.Insert(entityToCreate);
                entityToCreate.flag = (int)flag.Add;
            }
            else
            {
                _notification.Update(entityToCreate);
                entityToCreate.flag = (int)flag.Update;
            }

            return Json(new { ok = true, flag = entityToCreate.flag, objrow = _notification.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [PermissionFilter(BackOfficeViews.Notification, PermissionType.Remove)]
        public JsonResult Delete(int Id)
        {
            Ensure.Argument.NotNull(Id);
            Ensure.Argument.IsNot(Id == 0);

            return Json(new { ok = _notification.Delete(Id) }, JsonRequestBehavior.AllowGet);
        }

        #region private function

        private void PrepareMessage(NotificationDto entityToCreate)
        {
            switch (entityToCreate.MessageTypeId)
            {
                case MessageType.Sms:
                    SendSmsMessages(entityToCreate);
                    break;

                case MessageType.Email:
                    SendEmailMessages(entityToCreate);
                    break;
            }
        }
        private void SendSmsMessages(NotificationDto entityToCreate)
        {
            var recipients = new List<RecipientModel>();

            var sms = GeneralConfiguration.Configuration.DependencyResolver.GetService<ISms>();

            recipients.AddRange(PrepareSmsRecipient());

            foreach (var recipient in recipients)
            {
                sms.SendMessage(recipient.Mobile, entityToCreate.Body);
            }
        }
        private void SendEmailMessages(NotificationDto entityToCreate)
        {
            var recipients = new List<RecipientModel>();

            var sendGrid = GeneralConfiguration.Configuration.DependencyResolver.GetService<ISendGridMessager>();

            recipients.AddRange(PrepareEmailRecipient());

            if (recipients.Count > 0)
            {
                sendGrid.Recipients = recipients;
                sendGrid.SendMail(entityToCreate.Body, true, entityToCreate.Subject);
            }
        }
        private List<RecipientModel> PrepareEmailRecipient()
        {
            var recipients = new List<RecipientModel>();

            var __member = GeneralConfiguration.Configuration.DependencyResolver.GetService<IMember>();           

            var members = __member.GetMembersForEmails(this.TenantId, (int)MemberType.Adult);

            foreach (var item in members)
            {
                if (!(bool)item.IsEmail)
                {
                    continue;
                }

                var recipientModel = new RecipientModel()
                {
                    Email = item.Email,
                    Recipient = item.FirstName
                };

                recipients.Add(recipientModel);
            }

            return recipients;
        }
        private List<RecipientModel> PrepareSmsRecipient()
        {
            var __member = GeneralConfiguration.Configuration.DependencyResolver.GetService<IMember>();

            var recipients = new List<RecipientModel>();

            var members = __member.GetMembersForSms(this.TenantId, (int)MemberType.Adult);

            foreach (var item in members)
            {
                if (!(bool)item.IsSms)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(item.Mobile))
                {
                    continue;
                }

                var recipientModel = new RecipientModel()
                {
                    Mobile = item.Mobile,
                    Recipient = item.FirstName
                };

                recipients.Add(recipientModel);
            }

            return recipients;
        }
        
        #endregion
    }
}
