﻿namespace Suftnet.Cos.BackOffice
{
    using Service;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class PermissionController : BackOfficeBaseController
    {       
        #region Resolving dependencies
           
        private readonly IPermission _permission;
      
        public PermissionController(IPermission permission)
        {            
            _permission = permission;         
        }

        #endregion

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Entry(string name, string queryString)
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Fetch(string Id)
        {
            Ensure.Argument.NotNull(Id);

            var model = await Task.Run(() => _permission.GetByUserId(Id));
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(PermissionDto entityToCreate)
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

            entityToCreate.UserId = entityToCreate.ExternalId;
            entityToCreate.Id = Guid.NewGuid(); 

           _permission.Insert(entityToCreate);
            entityToCreate.flag = (int)flag.Add;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(PermissionDto entityToCreate)
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
                       
            _permission.Update(entityToCreate);
            entityToCreate.flag = (int)flag.Update;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]       
        public JsonResult Delete(string Id)
        {
            Ensure.Argument.NotNull(Id);
            return Json(new { ok = _permission.Delete(new Guid(Id)) }, JsonRequestBehavior.AllowGet);
        } 
    }
}
