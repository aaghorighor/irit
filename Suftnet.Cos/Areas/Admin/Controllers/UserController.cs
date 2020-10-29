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
        private readonly Suftnet.Cos.DataAccess.IUser _user;
        public UserController(DataAccess.IUser user)
        {      
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
            return View();
        }

        [HttpPost]
        public JsonResult Fetch(DataTableAjaxPostModel param)
        {
            var model = _user.Fetch((int)eArea.Admin, param.start, param.length, param.search.value);

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.Count(),
                recordsFiltered = _user.Count(),
                data = model
            },
                
            JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0, VaryByParam = "*")]
        public ActionResult Entry(string name, string queryString)
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Fetch(string queryString)
        {
            Ensure.Argument.NotNull(queryString);

            var model = await Task.Run(() => _user.GetById(new Guid(queryString)));
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

            if(entityToChange.ChangePassword)
            {
                ChangePassword(entityToChange);
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
        private void ChangePassword(UserAccountDto entityToChange)
        {
            var token = UserManager.GeneratePasswordResetToken(entityToChange.UserId);

            if (!string.IsNullOrEmpty(token))
            {
                UserManager.ResetPassword(entityToChange.UserId, token, entityToChange.Password);
            }
        }
        private ApplicationUser Map(UserAccountDto model, ApplicationUser user)
        {
            user.Active = model.Active;
            user.Area = GetArea((int)model.AreaId);
            user.AreaId = model.AreaId;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.Id = model.UserId;         

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
                Email = model.Email               
            };

            return applicationUser;

        }
        private string GetArea(int areaId)
        {
            var title = string.Empty;

            switch (areaId)
            {
                case (int)eArea.BackOffice:
                    title = "Back Office";
                    break;

                case (int)eArea.SiteAdmin:

                    title = "Site Admin";
                    break;

                case (int)eArea.Admin:
                    title = "Admin";
                    break;

                case (int)eArea.FrontOffice:
                    title = "Front Office";
                    break;               
                default:
                    title = "Member Office";
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

