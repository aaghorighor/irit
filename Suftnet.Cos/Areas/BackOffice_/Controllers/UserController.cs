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

    public class UserController : BackOfficeBaseController
    {
       #region Resolving dependencies       

       private ApplicationSignInManager _signInManager;
       private ApplicationUserManager _userManager;
       private readonly IMemberAccount _memberAccount;
       private readonly Suftnet.Cos.DataAccess.IUser _user;
        public UserController(IMemberAccount memberAccount, DataAccess.IUser user)
       {
            _memberAccount = memberAccount;
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
        [PermissionFilter(BackOfficeViews.User, PermissionType.Edit)]
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

            var model = UserManager.FindById(entityToChange.UserId);
            if (model == null)
            {
                return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var applicationUser = Map(entityToChange, model);
            var result = UserManager.Update(applicationUser);

            if (!result.Succeeded)
            {
                return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            Task.Run(() => this.CreatePermissions(entityToChange));

            return Json(new { ok = true, flag = (int)flag.Update }, JsonRequestBehavior.AllowGet);
        }
     
       [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
       public JsonResult ResetPassword(UserAccountDto entityToChange)
        {
            Ensure.Argument.NotNull(entityToChange.Id);

            var model = UserManager.FindById(entityToChange.Id);
            if (model == null)
            {
                return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var token = UserManager.GeneratePasswordResetToken(entityToChange.Id);

            if (string.IsNullOrEmpty(token))
            {
                return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var result = UserManager.ResetPassword(entityToChange.Id, token, entityToChange.Password);

            if (result.Succeeded)
            {
                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { ok = false, msg = ErrorBuilder(result.Errors) }, JsonRequestBehavior.AllowGet);
        }
       [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
       [PermissionFilter(BackOfficeViews.User, PermissionType.Remove)]
       public JsonResult Delete(string Id)
        {
            Ensure.Argument.NotNull(Id);

            var model = UserManager.FindById(Id);
            if (model == null)
            {
                return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            var result = UserManager.Delete(model);

            if (result.Succeeded)
            {
                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { ok = false, msg = ErrorBuilder(result.Errors) }, JsonRequestBehavior.AllowGet);
        }

        #region private functions
     
       private void CreatePermissions(UserAccountDto entityToCreate)
        {
            switch (entityToCreate.AreaId)
            {
                case (int)eArea.BackOffice:

                    var backOffice = GeneralConfiguration.Configuration.DependencyResolver.GetService<PermissionCommand>();
                    backOffice.UserId = entityToCreate.UserId;
                    backOffice.PermissionTypeId = (int)eSettings.Backofficepages;
                    backOffice.CreatedBy = this.UserName;
                    backOffice.Execute();

                    break;

                case (int)eArea.FrontOffice:

                    var frontOffice = GeneralConfiguration.Configuration.DependencyResolver.GetService<PermissionCommand>();
                    frontOffice.UserId = entityToCreate.UserId;
                    frontOffice.PermissionTypeId = (int)eSettings.FrontOfficepages;
                    frontOffice.CreatedBy = this.UserName;
                    frontOffice.Execute();

                    break;              
            }
        }
       private List<UserAccountDto> Map(IList<ApplicationUser> applicationUsers)
        {
            var _userAccountDto = new List<UserAccountDto>();

            foreach (var model in applicationUsers)
            {
                var userAccountDto = new UserAccountDto()
                {
                    Active = model.Active,
                    Area = GetArea((int)model.AreaId),
                    AreaId = model.AreaId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserId = model.Id,
                    UserName = model.Email,
                };

                _userAccountDto.Add(userAccountDto);
            }

            return _userAccountDto;
        }
       private ApplicationUser Map(UserAccountDto model, ApplicationUser user)
        {
            user.Active = model.Active;
            user.Area = GetArea((int)model.AreaId);
            user.AreaId = model.AreaId;         

            return user;
        }
       private UserAccountDto Map(ApplicationUser model)
        {
            var userAccountDto = new UserAccountDto()
            {
                Active = model.Active,
                Area = model.Area,
                AreaId = model.AreaId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserId = model.Id             
            };

            return userAccountDto;
        }
       private ApplicationUser Map(UserAccountDto model)
        {
            var applicationUser = new ApplicationUser()
            {
                Active = model.Active,
                Area = GetArea((int)model.AreaId),
                AreaId = model.AreaId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                TenantId = this.TenantId
            };

            return applicationUser;

        }
       private string GetArea(int? areaId)
        {
            var service = GeneralConfiguration.Configuration.DependencyResolver.GetService<ICommon>();
            var model = service.Get((int)areaId);
            return model.Title;
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

