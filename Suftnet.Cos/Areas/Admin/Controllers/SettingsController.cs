namespace Suftnet.Cos.Admin.Controllers
{
    using System;
    using System.Web.Mvc;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Common;
    using Core;
    using System.Threading.Tasks;
    using Suftnet.Cos.Service;
    using Stripe;

    public class SettingsController : AdminBaseController
    {
        #region Resolving dependencies        
  
        private readonly IGlobal _global;    
        private readonly IAddress _address;

        public SettingsController(IAddress address, IGlobal global)
        {
            _global = global;        
            _address = address;
        }

        #endregion
           
        public ActionResult Index()
        {          
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Fetch()
        {         
            var model = await Task.Run(() => _global.Get());
            return Json(new { ok=true, dataobject = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(GlobalDto entityToCreate)
        {                   
            try
            {
                 entityToCreate.CreatedBy = this.UserName;
                 entityToCreate.CreatedDT = DateTime.Now;

                _address.UpdateByAddressId(entityToCreate);
                _global.Update(entityToCreate);
                 entityToCreate.flag = (int)flag.Update;

                 GeneralConfiguration.Configuration.Settings.General = entityToCreate;

                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }      
    }
}

