namespace Suftnet.Cos.Web.Services
{
    using System;   
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.DataAccess.Action;
    using Suftnet.Cos.DataAccess.Identity;
    using Suftnet.Cos.Model;
    using Suftnet.Cos.Services;

    public class ApiUserManager : IApiUserManger, IDisposable
    {
        private readonly IUserAccount _userAccount;
        private UserManager<ApplicationUser> _userManager;
        private readonly ISmtp _messenger;
        private readonly IEditor _editor;

        public ApiUserManager(IUserAccount userAccount, ISmtp messenger, IEditor editor)
        {
            _editor = editor;
            _messenger = messenger;
            _userAccount = userAccount;
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }
        public async Task<ApplicationUser> CreateAsync(ApplicationUser model, Guid tenantId, string password, bool isSend, bool isBackoffice)
        {
            var identityResult = new IdentityResult();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                model.AreaId = isBackoffice == true ? (int)eArea.BackOffice : (int)eArea.MemberOffice;
                model.Area = GetArea(isBackoffice == true ? (int)eArea.BackOffice : (int)eArea.MemberOffice);
                
                if(string.IsNullOrEmpty(password))
                {
                    password = Constant.DefaultPassword;
                }

                identityResult = await _userManager.CreateAsync(model, password);

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
                        await Task.Run(() => SendEmailConfirmation(model, tenantId, password));
                    }             
                    _user.TenantId = tenantId;

                    return _user;
                }
            }

            return null;
        }
        private void SendEmailConfirmation(ApplicationUser entityToCreate, Guid tenantId, string password)
        {
            try
            {
                var messageModel = new MessageModel();
                var mailMessage = new System.Net.Mail.MailMessage();

                var iTenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
                var tenant = iTenant.Get(tenantId);

                var contentTemplate = new EditorDTO();
                var emailContent = this.PrepareConfirmationMessage(entityToCreate, _editor.Get((int)eEditor.MemberRegistration), tenant, password);

                mailMessage.From = new System.Net.Mail.MailAddress(GeneralConfiguration.Configuration.Settings.General.ServerEmail, GeneralConfiguration.Configuration.Settings.General.Company);
                mailMessage.To.Add(entityToCreate.Email);
                mailMessage.Body = emailContent;
                mailMessage.Subject = $"Thanks for your registration with {tenant.Name}";
                messageModel.MailMessage = new MailMessage(mailMessage);

                _messenger.MailProcessor(messageModel);

            }
            catch (Exception exception)
            {
                GeneralConfiguration.Configuration.Logger.LogError(exception);
            }
        }
        private string PrepareConfirmationMessage(ApplicationUser entityToCreate, EditorDTO editor, TenantDto tenant, string password)
        {
            var sb = new StringBuilder(editor.Contents);

            sb.Replace("[member]", entityToCreate.FirstName);
            sb.Replace("[email]", entityToCreate.Email);
            sb.Replace("[password]", password);

            sb.Replace("[restaurantname]", tenant.Name);
            sb.Replace("[restaurantphone]", tenant.Telephone);
            sb.Replace("[restaurantemail]", tenant.Email);

            return sb.ToString();
        }
        private string GetArea(int? areaId)
        {
            var service = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();
            var model = service.Get((int)areaId);
            return model.Title;
        }

        public void Dispose()
        {           
            _userManager.Dispose();

        }
    }
}