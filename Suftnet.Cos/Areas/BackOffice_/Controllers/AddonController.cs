namespace Suftnet.Cos.BackOffice
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Web.Mvc;
          
    public class AddonController : BackOfficeBaseController
    {      
        #region Resolving dependencies

        private readonly IAddon _addon;

        public AddonController(IAddon addon)
        {
            _addon = addon;                 
        }

        #endregion

        public ActionResult entry(int menuId)
        {
            return View(_addon.GetAll(menuId));
        }      

        [HttpGet]
        public JsonResult Get(int Id)
        {
            try
            {
                return Json(new { ok = true, dataobject = _addon.Get(Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpGet]
        public JsonResult GetAll(int Id)
        {
            try
            {
                return Json(new { ok = true, objrow = _addon.GetAll(Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

       
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]      
        public JsonResult Create(AddonDto entityToCreate)
        {
            try
            {
                if (entityToCreate == null)
                {
                    return Json(new { ok = false, msg = Constant.ValidationErrorMessage }, JsonRequestBehavior.AllowGet);
                }

                    entityToCreate.CreatedBy = this.UserName;
                    entityToCreate.CreatedDT = DateTime.Now;

                if (entityToCreate.Id == 0)
                {                  
                    entityToCreate.Id = _addon.Insert(entityToCreate);
                    entityToCreate.flag = (int)flag.Add;                                     
                }
                else
                {
                    _addon.Update(entityToCreate);
                    entityToCreate.flag = (int)flag.Update;
                }

                return Json(new { ok = true, Id = entityToCreate.Id, flag = entityToCreate.flag, objrow = _addon.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]       
        public JsonResult Delete(int Id)
        {
            try
            {
                return Json(new { ok = _addon.Delete(Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }
    }
}

