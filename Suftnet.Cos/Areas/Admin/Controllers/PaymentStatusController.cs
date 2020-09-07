namespace Suftnet.Cos.Admin.Controllers
{
    using Service;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Web.Mvc;
    using Suftnet.Cos.Extension;
    using System.Threading.Tasks;

    public class PaymentStatusController : AdminBaseController
    {     
        #region Resolving dependencies

        private readonly IPaymentStatus  _common;

        public PaymentStatusController(IPaymentStatus common)
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
            var model = await Task.Run(() => _common.GetAll());
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]   
        [ValidateAntiForgeryToken]
        public JsonResult Create(CommonTypeDto entityToCreate)
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
                     
            entityToCreate.Id = Guid.NewGuid();

            _common.Insert(entityToCreate);
            entityToCreate.flag = (int)flag.Add;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]    
        [ValidateAntiForgeryToken]
        public JsonResult Edit(CommonTypeDto entityToCreate)
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
