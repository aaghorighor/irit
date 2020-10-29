namespace Suftnet.Cos.Admin.Controllers
{
    using Stripe;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.ViewModel;

    public class PlanController : AdminBaseController
    {      

        #region Resolving dependencies
       
        private readonly IPlan _plan; 
        private readonly IPlanFeature _planFeature;

        public PlanController(IPlan plan, IPlanFeature planFeature)
        {
            _plan = plan;         
            _planFeature = planFeature;
        }

        #endregion     

        public ActionResult Index()
        {
            return View(_plan.GetAll());
        }

        [HttpGet]
        public JsonResult Get(int Id)
        {
            try
            {
                return Json(new { ok = true, dataobject = _plan.Get(Id) }, JsonRequestBehavior.AllowGet);
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
                var plan = _plan.Get(Id);
             
                return Json(new { ok = true, dataobject = _planFeature.GetAll(plan.Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }


        [HttpPost]
        public JsonResult Create(PlanDto entityToCreate)
        {
            try
            {                
                    entityToCreate.CreatedDT = DateTime.UtcNow;
                    entityToCreate.CreatedBy = this.UserName;

                if (entityToCreate.Id == 0)
                {
                    if (_plan.IsProductInPlan(0))
                    {
                        return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
                    }

                    entityToCreate.Id = _plan.Insert(entityToCreate);                   

                    entityToCreate.flag = (int)flag.Add;
                }
                else
                {
                    _plan.Update(entityToCreate);                   

                    entityToCreate.flag = (int)flag.Update;
                }

                return Json(new { ok = true, flag = entityToCreate.flag, objrow = _plan.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);

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
                return Json(new { ok = _plan.Delete(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }       

    }
}

