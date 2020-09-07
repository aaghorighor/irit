﻿namespace Suftnet.Cos.BackOffice
{
    using Service;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Web.Mvc;
    using Suftnet.Cos.Extension;
    using System.Threading.Tasks;

    public class TaxController : BackOfficeBaseController
    {     
        #region Resolving dependencies

        private readonly ITax  _common;

        public TaxController(ITax common)
        {             
            _common = common;                     
        }

        #endregion

        #region common settings

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Fetch()
        {
            var model = await Task.Run(() => _common.GetAll(this.TenantId));
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]   
        [ValidateAntiForgeryToken]
        public JsonResult Create(TaxDto entityToCreate)
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
          
            entityToCreate.CreatedDT = DateTime.Now;
            entityToCreate.CreatedBy = this.UserName;

            entityToCreate.TenantId = this.TenantId;
            entityToCreate.Id = Guid.NewGuid();

            _common.Insert(entityToCreate);
            entityToCreate.flag = (int)flag.Add;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]    
        [ValidateAntiForgeryToken]
        public JsonResult Edit(TaxDto entityToCreate)
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
                     
           _common.Update(entityToCreate);
            entityToCreate.flag = (int)flag.Update;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string Id)
        {
            Ensure.Argument.NotNull(Id);
       
            return Json(new { ok = _common.Delete(new Guid(Id)) }, JsonRequestBehavior.AllowGet);
        }      

        #endregion
     
    }
}