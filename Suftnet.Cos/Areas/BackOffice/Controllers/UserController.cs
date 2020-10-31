namespace Suftnet.Cos.BackOffice
{  
    using Service;

    using Suftnet.Cos.Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;
       
    using System.Collections.Generic;   
    using System.Web;
    using System.Web.Mvc;

    using Suftnet.Cos.Extension;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Core;
    using Web.Command;
    using System.Text;
    using DataAccess.Identity;
    using Web.Services.Implementation;
    using System.Threading.Tasks;
    using Suftnet.Cos.Web;
    using System.Linq;
    using System;

    public class UserController : BackOfficeBaseController
    {
       #region Resolving dependencies       

       private ApplicationSignInManager _signInManager;
       private ApplicationUserManager _userManager;
       private readonly IUserAccount _userAccount;
       private readonly Suftnet.Cos.DataAccess.IUser _user;
        public UserController(IUserAccount userAccount, DataAccess.IUser user)
       {
            _userAccount = userAccount;
            _user = user;
       }

       #endregion

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

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Fetch(DataTableAjaxPostModel param)
        {
            var model = _user.GetAll(this.TenantId, param.start, param.length, param.search.value);

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.Count(),
                recordsFiltered = _user.Count(this.TenantId),
                data = model
            },
                      JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        //[PermissionFilter(BackOfficeViews.User, PermissionType.Create)]
        public JsonResult Create(UserAccountDto entityToCreate)
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

            entityToCreate.Id = Guid.NewGuid().ToString();

            var result = UserManager.Create(Map(entityToCreate), entityToCreate.Password);

            if (!result.Succeeded)
            {
                return Json(new { ok = false, msg = ErrorBuilder(result.Errors) }, JsonRequestBehavior.AllowGet);
            }
                      
            Map(entityToCreate.Email);
            Task.Run(() => this.CreatePermissions(entityToCreate));

            return Json(new { ok = true, flag = (int)flag.Add }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        //[PermissionFilter(BackOfficeViews.User, PermissionType.Edit)]
        public JsonResult Edit(UserAccountDto entityToChange)
        {
            Ensure.Argument.NotNull(entityToChange);

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.AjaxErrors()
                });
            }

            var model = UserManager.FindById(entityToChange.Id);
            if (model == null)
            {
                return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var user = Map(entityToChange, model);
            var result = UserManager.Update(user);

            if (!result.Succeeded)
            {
                return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            if (entityToChange.ChangePassword)
            {
                ChangePassword(entityToChange);
            }

            Task.Run(() => this.CreatePermissions(entityToChange));

            return Json(new { ok = true, flag = (int)flag.Update }, JsonRequestBehavior.AllowGet);
        }

        private void ChangePassword(UserAccountDto entityToChange)
        {
            var token = UserManager.GeneratePasswordResetToken(entityToChange.UserId);

            if (!string.IsNullOrEmpty(token))
            {
                UserManager.ResetPassword(entityToChange.UserId, token, entityToChange.Password);
            }
        }
       
       [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
       [PermissionFilter(BackOfficeViews.User, PermissionType.Remove)]
       public JsonResult Delete(string Id)
        {
            Ensure.Argument.NotNull(Id);
           
            var result = _user.Delete(Id);

            if (result)
            {
                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { ok = false, msg= Constant.DeleteUserError }, JsonRequestBehavior.AllowGet);
        }

        #region private functions
     
       private void CreatePermissions(UserAccountDto entityToCreate)
        {
            switch (entityToCreate.AreaId)
            {
                case (int)eArea.BackOffice:

                    var backOffice = GeneralConfiguration.Configuration.DependencyResolver.GetService<PermissionCommand>();
                    backOffice.UserId = entityToCreate.Id;
                    backOffice.PermissionTypeId = (int)eSettings.Backofficepages;
                    backOffice.CreatedBy = this.UserName;
                    backOffice.Execute();

                    break;

                case (int)eArea.FrontOffice:

                    var frontOffice = GeneralConfiguration.Configuration.DependencyResolver.GetService<PermissionCommand>();
                    frontOffice.UserId = entityToCreate.Id;
                    frontOffice.PermissionTypeId = (int)eSettings.FrontOfficepages;
                    frontOffice.CreatedBy = this.UserName;
                    frontOffice.Execute();

                    break;              
            }
        }     
       private ApplicationUser Map(UserAccountDto model, ApplicationUser user)
        {
            user.Active = model.Active;
            user.Area = Map((int)model.AreaId);
            user.AreaId = model.AreaId;         

            return user;
        }      
       private ApplicationUser Map(UserAccountDto model)
        {
            var user = new ApplicationUser()
            {
                Id = model.Id,
                Active = model.Active,
                Area = Map((int)model.AreaId),
                AreaId = model.AreaId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email              
            };

            return user;

        }
       private void Map(string userName)
        {
            try {

                var model = UserManager.FindByEmail(userName);

                _userAccount.Insert(new DataAccess.Action.UserAccount()
                {
                    UserId = model.Id,
                    TenantId = this.TenantId,
                    CreatedBy = this.UserName,
                    CreatedDt = DateTime.UtcNow
                });

            }
            catch(Exception ex)
            {
                LogError(ex);
            }            
        }
       private string Map(int? areaId)
        {
            var title = string.Empty;

            switch (areaId)
            {
                case (int)eArea.BackOffice:
                    title = "Back Office";
                    break;               

                case (int)eArea.FrontOffice:
                    title = "Front Office";
                    break;
                default:
                    title = "Front Office";
                    break;
            }

            return title;
        }
       private string ErrorBuilder(IEnumerable<string> errors)
        {
            StringBuilder srt = new StringBuilder();

            foreach (var error in errors)
            {
                srt.Append(error);
            }

            return srt.ToString();

        }

        #endregion      
    }
}

