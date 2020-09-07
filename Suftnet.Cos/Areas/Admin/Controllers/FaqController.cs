namespace Suftnet.Cos.Admin.Controllers
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;

    using System;
    using System.Web.Mvc;

    public class FaqController : AdminBaseController
    {      
        #region Resolving dependencies

        private readonly IFaq _faq;

        public FaqController(IFaq faq)
        {
            _faq = faq;         
        }

        #endregion
       
        public ActionResult Index()
        {
            return View(_faq.GetAll());
        } 
        
        [HttpGet]
        public JsonResult Get(int Id)
        {
            try
            {
                return Json(new { ok = true, dataobject = _faq.Get(Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }
      
        [HttpPost]
         public JsonResult Create(FaqDto entityToCreate)
        {           
            try
            {
                   entityToCreate.CreatedBy = this.UserName;
                   entityToCreate.CreatedDT = DateTime.UtcNow;                  

                if (entityToCreate.Id == 0)
                {
                    entityToCreate.Id = _faq.Insert(entityToCreate);
                    entityToCreate.flag = (int)flag.Add;
                }
                else
                {
                    _faq.Update(entityToCreate);
                    entityToCreate.flag = (int)flag.Update;
                }

                return Json(new { ok = true, flag = entityToCreate.flag, objrow = _faq.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);    
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                return Json(new { ok = _faq.Delete(Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }       
    }
}

