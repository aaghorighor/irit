namespace Suftnet.Cos.Web
{
    using Cos.Services;
    using DataAccess.Identity;
    using Model;
    using Services.Implementation;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using System.Web;

    public class AccountController : AccountBaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IEditor _editor;
        private readonly ISmtp _messenger;      
        private readonly IUserAccount _memberAccount;

        public AccountController(ISmtp messenger, IEditor editor, IUserAccount memberAccount)
        {            
            _editor = editor;
            _messenger = messenger;
            _memberAccount = memberAccount;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null && HttpContext == null)
                {
                    return new ApplicationUserManager(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>());
                }

                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }      
         
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel, string returnUrl)
        {
            var url = string.Empty;

            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var test = await PasswordSignInAsync(loginModel.Username.Trim(), loginModel.Password.Trim(), false);

            if (test != SignInStatus.Success)
            {
                ModelState.AddModelError("username", "Password or Username Incorrect.");
                return View(loginModel);
            }

            var user = await UserManager.FindByEmailAsync(loginModel.Username.Trim());

            if (user.Active == false)
            {
                ModelState.AddModelError("username", "Your account has been disabled");
                return View(loginModel);
            }

            await SignInAsync(user, true);          

            switch (user.AreaId)
            {
                case (int)eArea.Admin:                   
                    return Redirect(this.AdminUrl());

                case (int)eArea.SiteAdmin:
                    return Redirect(this.SiteAdminOfficeUrl());                              

                case (int)eArea.BackOffice:                 
                    return Redirect(this.BackOfficerUrl());                                               

                default:
                    break;
            }    

            return RedirectToAction("index", "Account");
        }      
       
        public ActionResult Forgotten()
        {            
            return View(new ForgottenModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgotten(ForgottenModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserManager.FindByEmail(model.Email.Trim());
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email entered not found.");
                return View(model);
            }

            var token = UserManager.GeneratePasswordResetToken(user.Id);

            if (string.IsNullOrEmpty(token))
            {
                return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

           Task.Run(()=> CreateResetPasswordEmail(user.Email, user.FirstName, token));
            
            return RedirectToRoute("Confirmation");
        }

        [HttpGet]
        public ActionResult ResetPassword(string email, string resetToken)
        {
            return View(new ResetPasswordModel { Email = email, ResetToken = resetToken });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserManager.FindByEmail(model.Email.Trim());
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email entered not found.");
                return View(model);
            }

            var result = UserManager.ResetPassword(user.Id, model.ResetToken, model.NewPassword);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Email", "Password cannot be reset at this time, please try later.");
                return View(model);
            }
            
            return RedirectToRoute("logon");
        }

        public ActionResult Confirmation()
        {
            return View();
        }           
        public ActionResult LogOff()
        {
            this.LogOut();
            return Redirect(this.DefaultUrl());
        }
        #region private function
        private void LogOut()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            RemoveCache();
        }
        private void CreateResetPasswordEmail(string email, string customer, string resetToken)
        {
            var messageModel = new MessageModel();
            var messageBody = new System.Net.Mail.MailMessage();

            var emailTemplate = _editor.Get((int)eSettings.ResetPassword);

            if (emailTemplate != null)
            {
                var emailMessages = ResetPasswordEmailFormatter(emailTemplate.Contents, email, resetToken, customer);

                messageBody.From = new System.Net.Mail.MailAddress(GeneralConfiguration.Configuration.Settings.General.ServerEmail, GeneralConfiguration.Configuration.Settings.General.Company);
                messageBody.To.Add(email);
                messageBody.Body = emailMessages;
                messageBody.Subject = emailTemplate.Title;
                messageModel.MailMessage = new MailMessage(messageBody);

                try
                {
                    _messenger.MailProcessor(messageModel);
                }
                catch (Exception exception)
                {
                    GeneralConfiguration.Configuration.Logger.LogError(exception);
                }
            }
        }
        private string ResetPasswordEmailFormatter(string content, string email, string resetToken, string customer)
        {
            Dictionary<string, string> sb = new Dictionary<string, string>();
            string results = string.Empty;

            sb.Add("[customer]", customer);            
            sb.Add("[resetPasswordUrl]", $"{Request.Url.Authority}/account/resetpassword?email={email}&resettoken={resetToken}");

            foreach (KeyValuePair<string, string> _token in sb)
            {
                content = content.Replace(_token.Key, _token.Value);
            }

            return content;
        }       
        private async Task<SignInStatus> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            var result = await SignInManager.PasswordSignInAsync(email, password, rememberMe, shouldLockout: false);

            return StatusCode(result);
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
        private SignInStatus StatusCode(SignInStatus statusId)
        {
            switch (statusId)
            {
                case SignInStatus.Success:
                    return SignInStatus.Success;
                case SignInStatus.LockedOut:
                    return SignInStatus.LockedOut;
                case SignInStatus.RequiresVerification:
                    return SignInStatus.RequiresVerification;
                case SignInStatus.Failure:
                    return SignInStatus.Failure;
                default:
                    return SignInStatus.Failure;
            }
        }       
        private void RemoveCache()
        {
            GeneralConfiguration.Configuration.CacheService.Remove(string.Format(Constant.CacheUserPermissionFormat, this.UserId));
            GeneralConfiguration.Configuration.CacheService.Remove(string.Format(Constant.CacheTenantFormat, this.UserId));
        }


        #endregion
    }
}
