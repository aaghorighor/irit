namespace Suftnet.Cos.Admin.Controllers
{
    using Service;

    using Suftnet.Cos.Common; 
    using Suftnet.Cos.DataAccess;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
      
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Core;
    using Web.Command;
    using System.Text;
    using DataAccess.Identity;
    using Web.Services.Implementation;
    using System.Threading.Tasks;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web;

    public class UserController : AdminBaseController
    {
        #region Resolving dependencies       

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly Suftnet.Cos.DataAccess.IUserAccount _memberAccount;
        private readonly Suftnet.Cos.DataAccess.IUser _user;
        public UserController(Suftnet.Cos.DataAccess.IUserAccount memberAccount, DataAccess.IUser user)
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
        public ActionResult Index()
        {
            return View(Map(UserManager.Users));
        }

        public JsonResult Fetch(DataTableAjaxPostModel param)
        {
            var model = _user.GetAll(param.start, param.length, param.search.value);

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.Count(),
                recordsFiltered = _user.Count(),
                data = model
            },
                
            JsonRequestBehavior.AllowGet);
        }
        public ActionResult entry(int tenantId)
        {
            Ensure.Argument.NotNull(tenantId);
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetById(string Id)
        {
            Ensure.Argument.NotNull(Id);

            var model = await Task.Run(() => _memberAccount.GetById(this.TenantId));
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> Get(string Id)
        {
            Ensure.Argument.NotNull(Id);

            var applicationUser = await UserManager.FindByIdAsync(Id);

            if (applicationUser == null)
            {
                return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { ok = true, dataobject = Map(applicationUser) }, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
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
                return Json(new { ok = false, msg = Constant.UserNameAlreadyExist }, JsonRequestBehavior.AllowGet);
            }
          
            return Json(new { ok = true, flag = (int)flag.Update }, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
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
                      
            var result = UserManager.Create(Map(entityToCreate), Constant.DefaultPassword);

            if (!result.Succeeded)
            {
                return Json(new { ok = false, msg = ErrorBuilder(result.Errors) }, JsonRequestBehavior.AllowGet);
            }
                       
            return Json(new { ok = true, flag = (int)flag.Add }, JsonRequestBehavior.AllowGet);

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
        private List<UserAccountDto> Map(IQueryable<ApplicationUser> applicationUsers)
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
        private ApplicationUser Map(UserAccountDto model, ApplicationUser applicationUser)
        {
            applicationUser.Active = model.Active;
            applicationUser.Area = GetArea((int)model.AreaId);
            applicationUser.AreaId = model.AreaId;
            applicationUser.FirstName = model.FirstName;
            applicationUser.LastName = model.LastName;
            applicationUser.Email = model.Email;
            applicationUser.UserName = model.Email;
            applicationUser.Id = model.UserId;         

            return applicationUser;
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
                Email = model.Email               
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

