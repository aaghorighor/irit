namespace Suftnet.Cos.BackOffice
{
    using Service;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Web.Mvc;
    using Suftnet.Cos.Extension;

    public class TenantCommonController : BackOfficeBaseController
    {     
        #region Resolving dependencies

        private readonly ITenantCommon  _common;

        public TenantCommonController(ITenantCommon common)
        {             
            _common = common;                     
        }

        #endregion       
      
        #region common settings
        
        [HttpPost]   
        [ValidateAntiForgeryToken]
        public JsonResult Create(TenantCommonDto entityToCreate)
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

            entityToCreate.SettingId = entityToCreate.ExternalId.ToDecrypt().ToInt();
            entityToCreate.Id = _common.Insert(entityToCreate);
            entityToCreate.flag = (int)flag.Add;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]    
        [ValidateAntiForgeryToken]
        public JsonResult Edit(TenantCommonDto entityToCreate)
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

            entityToCreate.Id = entityToCreate.QueryString.ToDecrypt().ToInt();
           _common.Update(entityToCreate);
            entityToCreate.flag = (int)flag.Update;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string Id)
        {
            Ensure.Argument.NotNull(Id);
       
            return Json(new { ok = _common.Delete(Id.ToDecrypt().ToInt()) }, JsonRequestBehavior.AllowGet);
        }      

        #endregion
     
    }
}
