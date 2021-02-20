namespace Suftnet.Cos.BackOffice
{
    using Service;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Web.Mvc;
    using Suftnet.Cos.Extension;
    using System.Threading.Tasks;

    public class AddonController : BackOfficeBaseController
    {     
        #region Resolving dependencies

        private readonly IAddon _addon;

        public AddonController(IAddon addon)
        {
            _addon = addon;                     
        }
               
        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Entry(string name, string menuId)
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Fetch(string menuId)
        {
            var model = await Task.Run(() => _addon.GetAll(new Guid(menuId)));
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]   
        [ValidateAntiForgeryToken]
        public JsonResult Create(AddonDto entityToCreate)
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

            _addon.Insert(entityToCreate);
            entityToCreate.flag = (int)flag.Add;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]    
        [ValidateAntiForgeryToken]
        public JsonResult Edit(AddonDto entityToCreate)
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

            _addon.Update(entityToCreate);
            entityToCreate.flag = (int)flag.Update;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(string Id)
        {
            Ensure.Argument.NotNull(Id);
       
            return Json(new { ok = _addon.Delete(new Guid(Id)) }, JsonRequestBehavior.AllowGet);
        }      

        #endregion
     
    }
}
