namespace Suftnet.Cos.Admin.Controllers
{
    using Suftnet.Cos.Common;  
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Web.Mvc;
      
    public class PlanFeatureController : AdminBaseController
    {      

        #region Resolving dependencies
       
        private readonly IPlanFeature _planFeature;

        public PlanFeatureController(IPlanFeature planFeature)
        {
            _planFeature = planFeature;             
        }

        #endregion     

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get(int Id)
        {
            try
            {
                return Json(new { ok = true, dataobject = _planFeature.Get(Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }


        [HttpPost]
        public JsonResult Create(PlanFeatureDto entityToCreate)
        {
            try
            {
                    entityToCreate.CreatedDT = DateTime.UtcNow;
                    entityToCreate.CreatedBy = this.UserName;

                if (entityToCreate.Id == 0)
                {
                    if (_planFeature.IsPlanFeature(entityToCreate.PlanId, entityToCreate.ProductFeatureId))
                    {
                        return Json(new { ok = false, msg = Constant.ValidationErrorMessage }, JsonRequestBehavior.AllowGet);
                    }

                    entityToCreate.Id = _planFeature.Insert(entityToCreate);
                    entityToCreate.flag = (int)flag.Add;
                }
                else
                {
                    _planFeature.Update(entityToCreate);  
                    entityToCreate.flag = (int)flag.Update;
                }

                return Json(new { ok = true, flag = entityToCreate.flag, objrow = _planFeature.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);

            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }
               
        public JsonResult Delete(int Id)
        {
            try
            {
                bool response = _planFeature.Delete(Id);
                return Json(new { ok = response }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }       

    }
}

