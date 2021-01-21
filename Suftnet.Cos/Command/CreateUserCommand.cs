namespace Suftnet.Cos.Web.Command
{
    using Microsoft.AspNet.Identity;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.DataAccess.Action;
    using Suftnet.Cos.DataAccess.Identity;
    using Suftnet.Cos.Model;
    using Suftnet.Cos.Services;
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class CreateUserCommand : ICreateUserCommand, IDisposable
    {       
        private readonly IUserAccount _userAccount;
        private readonly ICustomer _customer;
        private readonly ISmtp _messenger;
        private readonly IFactoryCommand _factoryCommand;
        private UserManager<ApplicationUser> _userManager;
        public CreateUserCommand(
           ISmtp messenger, IFactoryCommand factoryCommand,
           IUserAccount userAccount,ICustomer customer)
        {
            _userAccount = userAccount;
            _messenger = messenger;
            _customer = customer;
            _factoryCommand = factoryCommand;
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        public string VIEW_PATH { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }      
        public CreateCustomerDto User { get; set; }
        public MobileTenantDto MobileUser { get; set; }
        public bool FLAG { get; set; } = false;

        public void Execute()
        {
            var newGuid = Guid.NewGuid();            

            var model = new ApplicationUser()
            {
                 Id = newGuid.ToString(),
                 Active = true,
                 AreaId = (int)eArea.Customer,
                 Area = "Customer",
                 UserCode = GetHashCode(newGuid),
                 UserName = User.Email,
                 ImageUrl = string.Empty,
                 Email = User.Email,
                 FirstName = User.FirstName,
                 LastName = User.LastName,
                 PhoneNumber = User.Mobile                
            };

            var result = _userManager.Create(model, Constant.DefaultPassword);

            if(result.Succeeded)
            {
                MapUserAccount(model);
                CreateUser(model);
                MapMobileUser(model);
                FLAG = true;
            }
        }

        #region
        private string GetHashCode(Guid guid)
        {
            var hashCode = guid.GetHashCode();
            var abs = Math.Abs(hashCode);
            var appCode = abs.ToString();
            var subString = appCode;

            if (subString.Length > 10)
            {
                subString = subString.Substring(0, 10);
            }

            return subString;
        }              
        private void MapUserAccount(ApplicationUser user)
        {
            var userAccount = new UserAccount()
            {
                UserId = user.Id,
                TenantId = User.ExternalId,
                AppCode = User.AppCode,
                UserCode = user.UserCode,
                EmailAddress = User.Email,

                CreatedBy = User.Email,
                CreatedDt = DateTime.UtcNow
            };

            _userAccount.Insert(userAccount);           
        }
        private void CreateUser(ApplicationUser user)
        {
            var customer = new CustomerDto
            {
                Id = Guid.NewGuid(),
                Active = true,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                Mobile = User.Mobile,
                UserId = user.Id,
                DeviceId = User.DeviceId,
                Serial = User.Serial,
                TenantId = User.ExternalId,

                CreatedDT = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = User.Email
            };

            _customer.Insert(customer);
        }
        private void MapMobileUser(ApplicationUser user)
        {
            var mobileUser = new MobileTenantDto
            {
                Id = user.Id,             
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                Mobile = User.Mobile,
                AreaId = user.AreaId,
                Area = user.Area,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber               
            };

            MobileUser = mobileUser;
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
                    sendGrid.SendMail(body, true, $"Thanks for your signing up with {tenant.Name}");
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

                var body = this.CreateConfirmationMessage(entityToCreate, tenant, password, viewPATH);

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
        #endregion

    }
}