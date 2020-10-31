namespace Suftnet.Cos.Web.Services
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.DataAccess.Action;
    using Suftnet.Cos.DataAccess.Identity;
    using Suftnet.Cos.Model;
    using Suftnet.Cos.Services;
    using Suftnet.Cos.Web.Command;
    using Suftnet.Cos.Web.ViewModel;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApiUserManager : IApiUserManger, IDisposable
    {
        private readonly IUserAccount _userAccount;
        private UserManager<ApplicationUser> _userManager;
        private readonly ISmtp _messenger;
        private readonly IFactoryCommand _factoryCommand;
        private readonly Suftnet.Cos.DataAccess.IUser _user;
        public string VIEW_PATH { get; set; }

        public ApiUserManager(IUserAccount userAccount, ISmtp messenger,
            IFactoryCommand factoryCommand, Suftnet.Cos.DataAccess.IUser user)
        {
            _user = user;
            _factoryCommand = factoryCommand;
            _messenger = messenger;
            _userAccount = userAccount;
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }
        public ApplicationUser CreateAsync(UserManager<ApplicationUser> userManager, string viewPATH, ApplicationUser model, Guid tenantId, string password, bool isSend, bool isBackoffice)
        {
            var identityResult = new IdentityResult();
            var user = _user.CheckEmailAddress(model.Email, tenantId);

            if (user == false)
            {
                model.AreaId = isBackoffice == true ? (int)eArea.BackOffice : (int)eArea.FrontOffice;
                model.Area = isBackoffice == true ? "BackOffice" : "FrontOffice";
                
                if(string.IsNullOrEmpty(password))
                {
                    password = Constant.DefaultPassword;
                }

                identityResult = userManager.Create(model, password);

                if (identityResult.Succeeded)
                {
                    var _user = _userManager.FindByEmail(model.Email);
                    var userAccount = new UserAccount()
                    {
                        UserId = _user.Id,
                        TenantId = tenantId,

                        CreatedBy = model.Email,
                        CreatedDt = DateTime.UtcNow
                    };
                  
                   _userAccount.Insert(userAccount);

                    if(isSend)
                    {
                        Task.Run(() => SendEmailFactory(model, tenantId, password, viewPATH));
                    }             
                    _user.TenantId = tenantId;

                    return _user;
                }
            }

            return null;
        }
      
        private void SendGridEmailConfirmation(ApplicationUser entityToCreate, string password, Guid tenantId, string viewPATH)
        {
            try
            {
                var iTenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
                var tenant = iTenant.Get(tenantId);

                var body = this.CreateConfirmationMessage(entityToCreate, tenant, password, viewPATH);
                var recipients = new List<RecipientModel>();
                var sendGrid = GeneralConfiguration.Configuration.DependencyResolver.GetService<ISendGridMessager>();

                recipients.Add(new RecipientModel { Email = entityToCreate.Email });

                if (recipients.Any())
                {
                    sendGrid.Recipients = recipients;
                    sendGrid.SendMail(body, true, $"Thanks for your registration with {tenant.Name}");
                }

            }
            catch (Exception exception)
            {
                GeneralConfiguration.Configuration.Logger.LogError(exception);
            }
        }
        private void SendEmailFactory(ApplicationUser model, Guid tenantId, string password, string viewPATH)
        {
            if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.TEST))
            {
                SendEmailConfirmation(model, tenantId, password, viewPATH);
            }
            else if (GeneralConfiguration.Configuration.ExecutingContext.Equals(ExecutingContext.LIVE))
            {
                SendGridEmailConfirmation(model, password, tenantId, viewPATH);
            }
        }
        private void SendEmailConfirmation(ApplicationUser entityToCreate, Guid tenantId, string password, string viewPATH)
        {
            try
            {
                var messageModel = new MessageModel();
                var mailMessage = new System.Net.Mail.MailMessage();

                var iTenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
                var tenant = iTenant.Get(tenantId);
                             
                var body = this.CreateConfirmationMessage(entityToCreate,tenant, password, viewPATH);

                mailMessage.From = new System.Net.Mail.MailAddress(GeneralConfiguration.Configuration.Settings.General.ServerEmail, GeneralConfiguration.Configuration.Settings.General.Company);
                mailMessage.To.Add(entityToCreate.Email);
                mailMessage.Body = body;
                mailMessage.Subject = $"Thanks for your registration with {tenant.Name}";
                messageModel.MailMessage = new MailMessage(mailMessage);

                _messenger.MailProcessor(messageModel);

            }
            catch (Exception exception)
            {
                GeneralConfiguration.Configuration.Logger.LogError(exception);
            }
        }
        public ApplicationUser FindByEmail(string email)
        {
            return _userManager.FindByEmail(email);
        }
        private string CreateConfirmationMessage(ApplicationUser entityToCreate, TenantDto tenant, string password, string viewPATH)
        {        
            var command = _factoryCommand.Create<EmailTemplateCommand>();
            command.VIEW_PATH = viewPATH;
            command.Execute();
                       
            var sb = new StringBuilder(command.View);

            sb.Replace("[user]", entityToCreate.FirstName + " " + entityToCreate.LastName);
            sb.Replace("[username]", entityToCreate.Email);
            sb.Replace("[password]", password);

            sb.Replace("[name]", tenant.Name);
            sb.Replace("[app_code]", tenant.AppCode);
      
            return sb.ToString();
        }
       
        public void Dispose()
        {           
            _userManager.Dispose();

        }
    }
}